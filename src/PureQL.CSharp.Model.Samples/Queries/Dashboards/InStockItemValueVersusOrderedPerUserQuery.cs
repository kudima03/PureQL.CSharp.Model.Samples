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
using PureQL.CSharp.Model.GroupExpressions;
using PureQL.CSharp.Model.Keys;
using PureQL.CSharp.Model.Lists;
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.ProjectionExpressions;
using PureQL.CSharp.Model.RowExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using Guid = Pure.Primitives.Guid.Guid;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Dashboards;

/// <summary>
/// Selects user_id, ordered rounded to 0 digits as ordered, the nullable in_stock_value
/// or else the decimal 0 rounded to 0 digits as in_stock_value and the decimal 100 times
/// that coalesced in_stock_value minus ordered divided by ordered rounded to 1 digit as
/// value_gap_pct from the subquery ordered_by_user left joined with the subquery
/// in_stock_value_by_user on user_id, ordered by user_id. ordered_by_user sums
/// order_total of schema_with_foreign_keys.orders per order_user_id, and
/// in_stock_value_by_user sums item_qty times product_price of
/// schema_with_foreign_keys.order_items inner joined with schema_with_foreign_keys.orders
/// on item_order_id equal to order_id and with schema_with_foreign_keys.products on
/// item_product_id equal to product_id, where product_in_stock equals true and
/// product_name is in Widget, Gizmo and Deluxe, per order_user_id.
/// </summary>
public sealed record InStockItemValueVersusOrderedPerUserQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithoutPagination(
            subqueries:
            [
                new Subquery(
                    "ordered_by_user",
                    new GroupedQueryWithoutPaginationWithoutOrderBy(
                        from: new FromEntity(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrdersTable().Name,
                                ]
                            ).TextValue
                        ),
                        groupBy:
                        [
                            new GroupKey(
                                "user_id",
                                new FieldUuid(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new OrdersTable().Name,
                                        ]
                                    ).TextValue,
                                    new OrderUserIdColumn().Name.TextValue
                                )
                            ),
                        ],
                        select:
                        [
                            new SelectItemGroup("user_id", new KeyUuid("user_id")),
                            new SelectItemGroup(
                                "ordered",
                                new SumDecimalGroup(
                                    new FieldDecimal(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new OrdersTable().Name,
                                            ]
                                        ).TextValue,
                                        new OrderTotalColumn().Name.TextValue
                                    )
                                )
                            ),
                        ]
                    )
                ),
                new Subquery(
                    "in_stock_value_by_user",
                    new GroupedQueryWithoutPaginationWithoutOrderBy(
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
                            new JoinEntity(
                                JoinKind.Inner,
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new OrdersTable().Name,
                                    ]
                                ).TextValue,
                                new EqualUuidRow(
                                    new FieldUuid(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new OrderItemsTable().Name,
                                            ]
                                        ).TextValue,
                                        new ItemOrderIdColumn().Name.TextValue
                                    ),
                                    new FieldUuid(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new OrdersTable().Name,
                                            ]
                                        ).TextValue,
                                        new OrderIdColumn().Name.TextValue
                                    )
                                )
                            ),
                            new JoinEntity(
                                JoinKind.Inner,
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new ProductsTable().Name,
                                    ]
                                ).TextValue,
                                new EqualUuidRow(
                                    new FieldUuid(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new OrderItemsTable().Name,
                                            ]
                                        ).TextValue,
                                        new ItemProductIdColumn().Name.TextValue
                                    ),
                                    new FieldUuid(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new ProductsTable().Name,
                                            ]
                                        ).TextValue,
                                        new ProductIdColumn().Name.TextValue
                                    )
                                )
                            ),
                        ],
                        where: new AndRow([
                            new EqualBooleanRow(
                                new FieldBoolean(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new ProductsTable().Name,
                                        ]
                                    ).TextValue,
                                    new ProductInStockColumn().Name.TextValue
                                ),
                                new LiteralBoolean(true)
                            ),
                            new InStringRow(
                                new FieldString(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new ProductsTable().Name,
                                        ]
                                    ).TextValue,
                                    new ProductNameColumn().Name.TextValue
                                ),
                                new ListLiteralString(["Widget", "Gizmo", "Deluxe"])
                            ),
                        ]),
                        groupBy:
                        [
                            new GroupKey(
                                "user_id",
                                new FieldUuid(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new OrdersTable().Name,
                                        ]
                                    ).TextValue,
                                    new OrderUserIdColumn().Name.TextValue
                                )
                            ),
                        ],
                        select:
                        [
                            new SelectItemGroup("user_id", new KeyUuid("user_id")),
                            new SelectItemGroup(
                                "in_stock_value",
                                new SumDecimalGroup(
                                    new MultiplyDecimalRow([
                                        new FieldDecimal(
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new RelationalSchemaWithForeignKeys().Name,
                                                    new OrderItemsTable().Name,
                                                ]
                                            ).TextValue,
                                            new ItemQtyColumn().Name.TextValue
                                        ),
                                        new FieldDecimal(
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new RelationalSchemaWithForeignKeys().Name,
                                                    new ProductsTable().Name,
                                                ]
                                            ).TextValue,
                                            new ProductPriceColumn().Name.TextValue
                                        ),
                                    ])
                                )
                            ),
                        ]
                    )
                ),
            ],
            from: new FromSubquery("ordered_by_user"),
            joins:
            [
                new JoinSubquery(
                    JoinKind.Left,
                    "in_stock_value_by_user",
                    new EqualUuidRow(
                        new FieldUuid("ordered_by_user", "user_id"),
                        new FieldUuid("in_stock_value_by_user", "user_id")
                    )
                ),
            ],
            select:
            [
                new SelectItemProjection(
                    "user_id",
                    new FieldUuid("ordered_by_user", "user_id")
                ),
                new SelectItemProjection(
                    "ordered",
                    new RoundDecimalProjection(
                        new FieldDecimal("ordered_by_user", "ordered"),
                        new LiteralInteger(0)
                    )
                ),
                new SelectItemProjection(
                    "in_stock_value",
                    new RoundDecimalProjection(
                        new CoalesceDecimalProjection([
                            new FieldDecimalNullable(
                                "in_stock_value_by_user",
                                "in_stock_value"
                            ),
                            new LiteralDecimal(0m),
                        ]),
                        new LiteralInteger(0)
                    )
                ),
                new SelectItemProjection(
                    "value_gap_pct",
                    new RoundDecimalProjection(
                        new MultiplyDecimalProjection([
                            new LiteralDecimal(100m),
                            new DivideDecimalProjection([
                                new SubtractDecimalProjection([
                                    new CoalesceDecimalProjection([
                                        new FieldDecimalNullable(
                                            "in_stock_value_by_user",
                                            "in_stock_value"
                                        ),
                                        new LiteralDecimal(0m),
                                    ]),
                                    new FieldDecimal("ordered_by_user", "ordered"),
                                ]),
                                new FieldDecimal("ordered_by_user", "ordered"),
                            ]),
                        ]),
                        new LiteralInteger(1)
                    )
                ),
            ],
            orderBy:
            [
                new OrderItemProjection(new FieldUuid("ordered_by_user", "user_id")),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/>, in this order.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new UserIdColumn(),
                    new Column(new String("ordered"), new DoubleColumnType()),
                    new Column(new String("in_stock_value"), new DoubleColumnType()),
                    new Column(new String("value_gap_pct"), new DoubleColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new UserIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000001-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("ordered"), new DoubleColumnType()),
                                new InvariantCell(new Double(151))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("in_stock_value"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(20))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("value_gap_pct"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-86.7))
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
                                new UserIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000002-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("ordered"), new DoubleColumnType()),
                                new InvariantCell(new Double(200))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("in_stock_value"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(23))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("value_gap_pct"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-88.8))
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
                                new UserIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000003-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("ordered"), new DoubleColumnType()),
                                new InvariantCell(new Double(375))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("in_stock_value"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(30))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("value_gap_pct"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-92))
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
                                new UserIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000004-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("ordered"), new DoubleColumnType()),
                                new InvariantCell(new Double(101))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("in_stock_value"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("value_gap_pct"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-100))
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
