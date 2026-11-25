using Pure.Collections.Generic;
using Pure.Primitives.Date;
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
using PureQL.CSharp.Model.GroupExpressions;
using PureQL.CSharp.Model.Keys;
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.RowExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Conversions;

/// <summary>
/// Selects from schema_with_foreign_keys.users inner joined on user_id to an inline
/// subquery imports of country, tier and postal_code texts for the six users, grouped by
/// country and by tier, tryAsInteger of the text, keeping groups with at least one
/// postal_code that tryAsInteger reads as a typed null: country, tier, the number of
/// users, the number of unreadable postal codes, the lowest and highest tryAsInteger of
/// postal_code, the sum of it with 0 through coalesce, tier plus 1 and asDate of the text
/// "2024-06-30", ordered by the number of unreadable postal codes descending, country and
/// tier.
/// </summary>
public sealed record UnparsedPostalCodesPerCountryQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainGroupedQueryWithoutPagination(
            subqueries:
            [
                new SubqueryRows(
                    "imports",
                    [
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(1, 0, 0, new byte[8]))
                            ),
                            new RowItem("country", new LiteralString("DE")),
                            new RowItem("tier", new LiteralString("1")),
                            new RowItem("postal_code", new LiteralString("10115")),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(2, 0, 0, new byte[8]))
                            ),
                            new RowItem("country", new LiteralString("DE")),
                            new RowItem("tier", new LiteralString("1")),
                            new RowItem("postal_code", new LiteralString("n/a")),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(3, 0, 0, new byte[8]))
                            ),
                            new RowItem("country", new LiteralString("FR")),
                            new RowItem("tier", new LiteralString("2")),
                            new RowItem("postal_code", new LiteralString("75001")),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(4, 0, 0, new byte[8]))
                            ),
                            new RowItem("country", new LiteralString("DE")),
                            new RowItem("tier", new LiteralString("1")),
                            new RowItem("postal_code", new LiteralString("80331")),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(5, 0, 0, new byte[8]))
                            ),
                            new RowItem("country", new LiteralString("US")),
                            new RowItem("tier", new LiteralString("gold")),
                            new RowItem("postal_code", new LiteralString("n/a")),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(6, 0, 0, new byte[8]))
                            ),
                            new RowItem("country", new LiteralString("US")),
                            new RowItem("tier", new LiteralString("gold")),
                            new RowItem("postal_code", new LiteralString("tbd")),
                        ],
                    ]
                ),
            ],
            from: new FromEntity(
                new JoinedString(
                    new DotString(),
                    [new RelationalSchemaWithForeignKeys().Name, new UsersTable().Name]
                ).TextValue
            ),
            joins:
            [
                new JoinSubquery(
                    JoinKind.Inner,
                    "imports",
                    new EqualUuidRow(
                        new FieldUuid(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue,
                            new UserIdColumn().Name.TextValue
                        ),
                        new FieldUuid("imports", "user_id")
                    )
                ),
            ],
            groupBy:
            [
                new GroupKey("country", new FieldString("imports", "country")),
                new GroupKey(
                    "tier",
                    new TryAsIntegerRow(new FieldString("imports", "tier"))
                ),
            ],
            having: new GreaterThanDecimalGroup(
                new CountGroup(
                    predicate: new EqualDecimalRow(
                        new TryAsIntegerRow(new FieldString("imports", "postal_code")),
                        new LiteralIntegerNullable()
                    )
                ),
                new LiteralInteger(0)
            ),
            select:
            [
                new SelectItemGroup("country", new KeyString("country")),
                new SelectItemGroup("tier", new KeyIntegerNullable("tier")),
                new SelectItemGroup("users", new CountGroup()),
                new SelectItemGroup(
                    "unparsed_postal_codes",
                    new CountGroup(
                        predicate: new EqualDecimalRow(
                            new TryAsIntegerRow(
                                new FieldString("imports", "postal_code")
                            ),
                            new LiteralIntegerNullable()
                        )
                    )
                ),
                new SelectItemGroup(
                    "lowest_postal_number",
                    new MinIntegerNullableGroup(
                        new TryAsIntegerRow(new FieldString("imports", "postal_code"))
                    )
                ),
                new SelectItemGroup(
                    "highest_postal_number",
                    new MaxIntegerNullableGroup(
                        new TryAsIntegerRow(new FieldString("imports", "postal_code"))
                    )
                ),
                new SelectItemGroup(
                    "sum_of_parsed",
                    new SumIntegerGroup(
                        new CoalesceIntegerRow([
                            new TryAsIntegerRow(
                                new FieldString("imports", "postal_code")
                            ),
                            new LiteralInteger(0),
                        ])
                    )
                ),
                new SelectItemGroup(
                    "next_tier",
                    new AddIntegerNullableGroup([
                        new KeyIntegerNullable("tier"),
                        new LiteralInteger(1),
                    ])
                ),
                new SelectItemGroup(
                    "as_of",
                    new AsDateStringDateGroup(new LiteralString("2024-06-30"))
                ),
            ],
            orderBy:
            [
                new OrderItemGroupExpression(
                    new CountGroup(
                        predicate: new EqualDecimalRow(
                            new TryAsIntegerRow(
                                new FieldString("imports", "postal_code")
                            ),
                            new LiteralIntegerNullable()
                        )
                    ),
                    SortDirection.Desc
                ),
                new OrderItemGroupExpression(new KeyString("country")),
                new OrderItemGroupExpression(new KeyIntegerNullable("tier")),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and
    /// <see cref="AuditSchemaDataSet"/>, in this order.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("country"), new StringColumnType()),
                    new Column(new String("tier"), new LongColumnType()),
                    new Column(new String("users"), new LongColumnType()),
                    new Column(new String("unparsed_postal_codes"), new LongColumnType()),
                    new Column(new String("lowest_postal_number"), new LongColumnType()),
                    new Column(new String("highest_postal_number"), new LongColumnType()),
                    new Column(new String("sum_of_parsed"), new LongColumnType()),
                    new Column(new String("next_tier"), new LongColumnType()),
                    new Column(new String("as_of"), new DateColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("country"), new StringColumnType()),
                                new InvariantCell(new String("US"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("tier"), new LongColumnType()),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("users"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("unparsed_postal_codes"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lowest_postal_number"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("highest_postal_number"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sum_of_parsed"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("next_tier"), new LongColumnType()),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("as_of"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(30),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
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
                                new Column(new String("country"), new StringColumnType()),
                                new InvariantCell(new String("DE"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("tier"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("users"), new LongColumnType()),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("unparsed_postal_codes"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lowest_postal_number"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(10115))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("highest_postal_number"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(80331))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sum_of_parsed"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(90446))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("next_tier"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("as_of"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(30),
                                        new UShort(6),
                                        new UShort(2024)
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
