using Pure.Collections.Generic;
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

namespace PureQL.CSharp.Model.Samples.Queries.Joins;

/// <summary>
/// Selects the order_items item_id as id, the part of sku before its colon as
/// sku_prefix, the part after it as sku_code and the first part of the text of
/// item_product_id split at the hyphens as product_id_group from
/// schema_with_foreign_keys.order_items inner joined to an inline subquery catalogue of
/// sku texts where the text of item_product_id equals sku lower-cased with "sku:"
/// replaced by nothing. The where keeps the skus whose position of "SKU:" is the
/// integer 1, ordered by the part of sku after its colon, then by item_id.
/// </summary>
public sealed record JoinOnNormalizedSkuQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithoutPagination(
            subqueries:
            [
                new SubqueryRows(
                    "catalogue",
                    [
                        [
                            new RowItem(
                                "sku",
                                new LiteralString(
                                    "SKU:000000C9-0000-0000-0000-000000000000"
                                )
                            ),
                        ],
                        [
                            new RowItem(
                                "sku",
                                new LiteralString(
                                    "sku:000000ca-0000-0000-0000-000000000000"
                                )
                            ),
                        ],
                        [
                            new RowItem(
                                "sku",
                                new LiteralString(
                                    "SKU:000000CB-0000-0000-0000-000000000000"
                                )
                            ),
                        ],
                        [
                            new RowItem(
                                "sku",
                                new LiteralString(
                                    "SKU:000000CC-0000-0000-0000-000000000000"
                                )
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
                    "catalogue",
                    new EqualStringRow(
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
                        new ReplaceStringRow(
                            new LowerStringRow(new FieldString("catalogue", "sku")),
                            new LiteralString("sku:"),
                            new LiteralString("")
                        )
                    )
                ),
            ],
            where: new EqualDecimalRow(
                new PositionIntegerRow(
                    new FieldString("catalogue", "sku"),
                    new LiteralString("SKU:")
                ),
                new LiteralInteger(1)
            ),
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
                    "sku_prefix",
                    new SplitPartStringProjection(
                        new FieldString("catalogue", "sku"),
                        new LiteralString(":"),
                        new LiteralInteger(1)
                    )
                ),
                new SelectItemProjection(
                    "sku_code",
                    new SplitPartStringProjection(
                        new FieldString("catalogue", "sku"),
                        new LiteralString(":"),
                        new LiteralInteger(2)
                    )
                ),
                new SelectItemProjection(
                    "product_id_group",
                    new SplitPartStringProjection(
                        new AsStringUuidStringProjection(
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
                        new LiteralString("-"),
                        new LiteralInteger(1)
                    )
                ),
            ],
            orderBy:
            [
                new OrderItemProjectionExpression(
                    new SplitPartStringProjection(
                        new FieldString("catalogue", "sku"),
                        new LiteralString(":"),
                        new LiteralInteger(2)
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
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("id"), new UuidColumnType()),
                    new Column(new String("sku_prefix"), new StringColumnType()),
                    new Column(new String("sku_code"), new StringColumnType()),
                    new Column(new String("product_id_group"), new StringColumnType()),
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
                                    new String("sku_prefix"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("SKU"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sku_code"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(
                                    new String("000000C9-0000-0000-0000-000000000000")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("product_id_group"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("000000c9"))
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
                                    new String("sku_prefix"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("SKU"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sku_code"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(
                                    new String("000000C9-0000-0000-0000-000000000000")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("product_id_group"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("000000c9"))
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
                                    new String("sku_prefix"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("SKU"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sku_code"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(
                                    new String("000000CB-0000-0000-0000-000000000000")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("product_id_group"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("000000cb"))
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
