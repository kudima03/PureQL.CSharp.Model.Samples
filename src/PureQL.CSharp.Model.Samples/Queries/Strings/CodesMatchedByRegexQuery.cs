using Pure.Collections.Generic;
using Pure.Primitives.Number;
using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Abstractions.Column;
using Pure.RelationalSchema.ColumnType;
using Pure.RelationalSchema.HashCodes;
using Pure.RelationalSchema.Samples.Columns;
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
using Pure.RelationalSchema.Storage;
using Pure.RelationalSchema.Storage.Abstractions;
using Pure.RelationalSchema.Storage.Samples.Cells;
using Pure.RelationalSchema.Storage.Samples.SchemaDataSets;
using Pure.RelationalSchema.Storage.Samples.TableDataSets;
using PureQL.CSharp.Model.Fields;
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.ProjectionExpressions;
using PureQL.CSharp.Model.RowExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using Guid = Pure.Primitives.Guid.Guid;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Strings;

/// <summary>
/// Selects item_id as id, the series and the number of a code read from the start of sku
/// by ^([A-Z]+)-([0-9]+) (null where it does not match, the number read with
/// tryAsInteger), group 2 of the code ^([A-Za-z]+)-([0-9]+), the first word of a nullable
/// description, the first run of four digits or more in sku or n/a through coalesce, and
/// sku with its leading capital letters replaced by the rounded item_qty as text, from
/// schema_with_foreign_keys.order_items inner joined to an inline subquery skus on the
/// item's product_id without hyphens equal to sku in lower case without the characters
/// that are not hexadecimal digits. The where keeps the skus that match
/// ^([A-Za-z]+)-([0-9]+) and drops the descriptions that match
/// \b(?:discontinued|recalled)\b, ordered by the first run of digits in sku, then by
/// item_id.
/// </summary>
public sealed record CodesMatchedByRegexQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithoutPagination(
            subqueries:
            [
                new SubqueryRows(
                    "skus",
                    [
                        [
                            new RowItem(
                                "sku",
                                new LiteralString(
                                    "WGT-0-00000C9-0000-0000-0000-000000000000"
                                )
                            ),
                            new RowItem("description", new LiteralString("Basic widget")),
                        ],
                        [
                            new RowItem(
                                "sku",
                                new LiteralString(
                                    "GZM-0-00000CA-0000-0000-0000-000000000000"
                                )
                            ),
                            new RowItem(
                                "description",
                                new LiteralString("Premium gadget, recalled in 2024")
                            ),
                        ],
                        [
                            new RowItem(
                                "sku",
                                new LiteralString(
                                    "giz-000-000CB-000-000-000-000-000-000-000-000"
                                )
                            ),
                            new RowItem("description", new LiteralStringNullable()),
                        ],
                        [
                            new RowItem(
                                "sku",
                                new LiteralString(
                                    "LUX-0-00000CC-0000-0000-0000-000000000000"
                                )
                            ),
                            new RowItem(
                                "description",
                                new LiteralString("Deluxe bundle")
                            ),
                        ],
                    ]
                ),
            ],
            from: new FromEntity(
                new JoinedString(
                    new DotString(),
                    [
                        new RelationalSchemaWithForeignKeys().Name,
                        new OrderItemsTable().Name,
                    ]
                ).TextValue
            ),
            joins:
            [
                new JoinSubquery(
                    JoinKind.Inner,
                    "skus",
                    new EqualStringRow(
                        new ReplaceRegexStringRow(
                            new AsStringUuidStringRow(
                                new FieldUuid(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new OrderItemsTable().Name,
                                        ]
                                    ).TextValue,
                                    new ItemProductIdColumn().Name.TextValue
                                )
                            ),
                            new LiteralString(""),
                            new LiteralString("-")
                        ),
                        new ReplaceRegexStringRow(
                            new LowerStringRow(new FieldString("skus", "sku")),
                            new LiteralString(""),
                            new LiteralString("[^0-9a-f]")
                        )
                    )
                ),
            ],
            where: new AndRow([
                new MatchesRegexRow(
                    new FieldString("skus", "sku"),
                    new LiteralString("^([A-Za-z]+)-([0-9]+)")
                ),
                new NotRow(
                    new MatchesRegexRow(
                        new FieldStringNullable("skus", "description"),
                        new LiteralString("\\b(?:discontinued|recalled)\\b")
                    )
                ),
            ]),
            select:
            [
                new SelectItemProjection(
                    "id",
                    new FieldUuid(
                        new JoinedString(
                            new DotString(),
                            [
                                new RelationalSchemaWithForeignKeys().Name,
                                new OrderItemsTable().Name,
                            ]
                        ).TextValue,
                        new ItemIdColumn().Name.TextValue
                    )
                ),
                new SelectItemProjection(
                    "sku_series",
                    new ExtractRegexProjection(
                        new FieldString("skus", "sku"),
                        new LiteralString("^([A-Z]+)-([0-9]+)"),
                        group: new LiteralInteger(1)
                    )
                ),
                new SelectItemProjection(
                    "sku_number",
                    new TryAsIntegerProjection(
                        new ExtractRegexProjection(
                            new FieldString("skus", "sku"),
                            new LiteralString("^([A-Z]+)-([0-9]+)"),
                            group: new LiteralInteger(2)
                        )
                    )
                ),
                new SelectItemProjection(
                    "chosen_group",
                    new ExtractRegexProjection(
                        new FieldString("skus", "sku"),
                        new LiteralString("^([A-Za-z]+)-([0-9]+)"),
                        group: new LiteralInteger(2)
                    )
                ),
                new SelectItemProjection(
                    "first_word_of_description",
                    new ExtractRegexProjection(
                        new FieldStringNullable("skus", "description"),
                        new LiteralString("\\w+")
                    )
                ),
                new SelectItemProjection(
                    "sku_or_placeholder",
                    new CoalesceStringProjection([
                        new ExtractRegexProjection(
                            new FieldString("skus", "sku"),
                            new LiteralString("[0-9]{4,}")
                        ),
                        new LiteralString("n/a"),
                    ])
                ),
                new SelectItemProjection(
                    "replacement_from_a_field",
                    new ReplaceRegexStringProjection(
                        new FieldString("skus", "sku"),
                        new AsStringDecimalStringProjection(
                            new RoundWithoutDigitsIntegerProjection(
                                new FieldDecimal(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new OrderItemsTable().Name,
                                        ]
                                    ).TextValue,
                                    new ItemQtyColumn().Name.TextValue
                                )
                            )
                        ),
                        new LiteralString("^[A-Z]+")
                    )
                ),
            ],
            orderBy:
            [
                new OrderItemProjectionExpression(
                    new ExtractRegexProjection(
                        new FieldString("skus", "sku"),
                        new LiteralString("[0-9]+")
                    )
                ),
                new OrderItemProjectionExpression(
                    new FieldUuid(
                        new JoinedString(
                            new DotString(),
                            [
                                new RelationalSchemaWithForeignKeys().Name,
                                new OrderItemsTable().Name,
                            ]
                        ).TextValue,
                        new ItemIdColumn().Name.TextValue
                    )
                ),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and
    /// <see cref="AuditSchemaDataSet"/>, in this order.
    /// The matches are computed with Python re over the ASCII classes, with $ read as the end
    /// of the text.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("id"), new UuidColumnType()),
                    new Column(new String("sku_series"), new StringColumnType()),
                    new Column(new String("sku_number"), new LongColumnType()),
                    new Column(new String("chosen_group"), new StringColumnType()),
                    new Column(
                        new String("first_word_of_description"),
                        new StringColumnType()
                    ),
                    new Column(new String("sku_or_placeholder"), new StringColumnType()),
                    new Column(
                        new String("replacement_from_a_field"),
                        new StringColumnType()
                    ),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("id"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "0000012d-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sku_series"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("WGT"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sku_number"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("chosen_group"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("0"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_word_of_description"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Basic"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sku_or_placeholder"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("00000"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("replacement_from_a_field"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(
                                    new String("2-0-00000C9-0000-0000-0000-000000000000")
                                )
                            ),
                        ],
                        pair => pair.Key,
                        pair => pair.Value,
                        column => new ColumnHash(column)
                    )
                ),
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("id"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000130-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sku_series"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("WGT"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sku_number"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("chosen_group"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("0"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_word_of_description"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Basic"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sku_or_placeholder"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("00000"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("replacement_from_a_field"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(
                                    new String("3-0-00000C9-0000-0000-0000-000000000000")
                                )
                            ),
                        ],
                        pair => pair.Key,
                        pair => pair.Value,
                        column => new ColumnHash(column)
                    )
                ),
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("id"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "0000012f-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sku_series"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sku_number"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("chosen_group"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("000"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_word_of_description"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sku_or_placeholder"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("n/a"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("replacement_from_a_field"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(
                                    new String(
                                        "giz-000-000CB-000-000-000-000-000-000-000-000"
                                    )
                                )
                            ),
                        ],
                        pair => pair.Key,
                        pair => pair.Value,
                        column => new ColumnHash(column)
                    )
                ),
            ]
        );
}
