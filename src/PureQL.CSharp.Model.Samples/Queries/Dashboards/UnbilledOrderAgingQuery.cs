using Pure.Collections.Generic;
using Pure.Primitives.Bool;
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
using PureQL.CSharp.Model.ProjectionExpressions;
using PureQL.CSharp.Model.RowExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using ModelPagination = PureQL.CSharp.Model.Pagination;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Dashboards;

/// <summary>
/// Selects user_name, user_active, order_status, placed_on, order_total, order_total
/// minus the subquery's item_value or else 0 rounded to 2 digits as unbilled, the days
/// from placed_on to 2024-06-10 as days_open, new, 1-4, 5-6, 7-8 or 9+ by those days as
/// aging_bucket and the subquery's item_lines or else the integer 0 as item_lines from
/// schema_with_foreign_keys.orders left joined with the subquery item_value_by_order on
/// order_id and inner joined with schema_with_foreign_keys.users on order_user_id equal
/// to user_id, where the unbilled amount is greater than 100, ordered by the days open
/// descending then the unbilled amount descending, skipping 0 rows and taking 100.
/// item_value_by_order sums item_qty times product_price and counts the lines of
/// schema_with_foreign_keys.order_items inner joined with
/// schema_with_foreign_keys.products on item_product_id equal to product_id per
/// item_order_id.
/// </summary>
public sealed record UnbilledOrderAgingQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithPagination(
            subqueries:
            [
                new Subquery(
                    "item_value_by_order",
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
                        groupBy:
                        [
                            new GroupKey(
                                "order_id",
                                new FieldUuid(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new OrderItemsTable().Name,
                                        ]
                                    ).TextValue,
                                    new ItemOrderIdColumn().Name.TextValue
                                )
                            ),
                        ],
                        select:
                        [
                            new SelectItemGroup("order_id", new KeyUuid("order_id")),
                            new SelectItemGroup(
                                "item_value",
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
                            new SelectItemGroup("item_lines", new CountGroup()),
                        ]
                    )
                ),
            ],
            from: new FromEntity(
                new JoinedString(
                    new DotString(),
                    [new RelationalSchemaWithForeignKeys().Name, new OrdersTable().Name]
                ).TextValue
            ),
            joins:
            [
                new JoinSubquery(
                    JoinKind.Left,
                    "item_value_by_order",
                    new EqualUuidRow(
                        new FieldUuid(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrdersTable().Name,
                                ]
                            ).TextValue,
                            new OrderIdColumn().Name.TextValue
                        ),
                        new FieldUuid("item_value_by_order", "order_id")
                    )
                ),
                new JoinEntity(
                    JoinKind.Inner,
                    new JoinedString(
                        new DotString(),
                        [
                            new RelationalSchemaWithForeignKeys().Name,
                            new UsersTable().Name,
                        ]
                    ).TextValue,
                    new EqualUuidRow(
                        new FieldUuid(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrdersTable().Name,
                                ]
                            ).TextValue,
                            new OrderUserIdColumn().Name.TextValue
                        ),
                        new FieldUuid(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue,
                            new UserIdColumn().Name.TextValue
                        )
                    )
                ),
            ],
            where: new GreaterThanDecimalRow(
                new SubtractDecimalRow([
                    new FieldDecimal(
                        new JoinedString(
                            new DotString(),
                            [
                                new RelationalSchemaWithForeignKeys().Name,
                                new OrdersTable().Name,
                            ]
                        ).TextValue,
                        new OrderTotalColumn().Name.TextValue
                    ),
                    new CoalesceDecimalRow([
                        new FieldDecimalNullable("item_value_by_order", "item_value"),
                        new LiteralDecimal(0m),
                    ]),
                ]),
                new LiteralDecimal(100m)
            ),
            select:
            [
                new SelectItemProjection(
                    "user_name",
                    new FieldString(
                        new JoinedString(
                            new DotString(),
                            [
                                new RelationalSchemaWithForeignKeys().Name,
                                new UsersTable().Name,
                            ]
                        ).TextValue,
                        new UserNameColumn().Name.TextValue
                    )
                ),
                new SelectItemProjection(
                    "user_active",
                    new FieldBoolean(
                        new JoinedString(
                            new DotString(),
                            [
                                new RelationalSchemaWithForeignKeys().Name,
                                new UsersTable().Name,
                            ]
                        ).TextValue,
                        new UserActiveColumn().Name.TextValue
                    )
                ),
                new SelectItemProjection(
                    "order_status",
                    new FieldString(
                        new JoinedString(
                            new DotString(),
                            [
                                new RelationalSchemaWithForeignKeys().Name,
                                new OrdersTable().Name,
                            ]
                        ).TextValue,
                        new OrderStatusColumn().Name.TextValue
                    )
                ),
                new SelectItemProjection(
                    "placed_on",
                    new FieldDate(
                        new JoinedString(
                            new DotString(),
                            [
                                new RelationalSchemaWithForeignKeys().Name,
                                new OrdersTable().Name,
                            ]
                        ).TextValue,
                        new PlacedOnColumn().Name.TextValue
                    )
                ),
                new SelectItemProjection(
                    "order_total",
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
                ),
                new SelectItemProjection(
                    "unbilled",
                    new RoundDecimalProjection(
                        new SubtractDecimalProjection([
                            new FieldDecimal(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new OrdersTable().Name,
                                    ]
                                ).TextValue,
                                new OrderTotalColumn().Name.TextValue
                            ),
                            new CoalesceDecimalProjection([
                                new FieldDecimalNullable(
                                    "item_value_by_order",
                                    "item_value"
                                ),
                                new LiteralDecimal(0m),
                            ]),
                        ]),
                        new LiteralInteger(2)
                    )
                ),
                new SelectItemProjection(
                    "days_open",
                    new DateDiffDaysIntegerProjection(
                        new LiteralDate(new DateOnly(2024, 6, 10)),
                        new FieldDate(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrdersTable().Name,
                                ]
                            ).TextValue,
                            new PlacedOnColumn().Name.TextValue
                        )
                    )
                ),
                new SelectItemProjection(
                    "aging_bucket",
                    new IfStringProjection(
                        new LiteralString("new"),
                        new IfStringProjection(
                            new LiteralString("1-4"),
                            new IfStringProjection(
                                new LiteralString("5-6"),
                                new IfStringProjection(
                                    new LiteralString("7-8"),
                                    new LiteralString("9+"),
                                    new LessThanOrEqualDecimalProjection(
                                        new DateDiffDaysIntegerProjection(
                                            new LiteralDate(new DateOnly(2024, 6, 10)),
                                            new FieldDate(
                                                new JoinedString(
                                                    new DotString(),
                                                    [
                                                        new RelationalSchemaWithForeignKeys().Name,
                                                        new OrdersTable().Name,
                                                    ]
                                                ).TextValue,
                                                new PlacedOnColumn().Name.TextValue
                                            )
                                        ),
                                        new LiteralInteger(8)
                                    )
                                ),
                                new LessThanOrEqualDecimalProjection(
                                    new DateDiffDaysIntegerProjection(
                                        new LiteralDate(new DateOnly(2024, 6, 10)),
                                        new FieldDate(
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new RelationalSchemaWithForeignKeys().Name,
                                                    new OrdersTable().Name,
                                                ]
                                            ).TextValue,
                                            new PlacedOnColumn().Name.TextValue
                                        )
                                    ),
                                    new LiteralInteger(6)
                                )
                            ),
                            new LessThanOrEqualDecimalProjection(
                                new DateDiffDaysIntegerProjection(
                                    new LiteralDate(new DateOnly(2024, 6, 10)),
                                    new FieldDate(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new OrdersTable().Name,
                                            ]
                                        ).TextValue,
                                        new PlacedOnColumn().Name.TextValue
                                    )
                                ),
                                new LiteralInteger(4)
                            )
                        ),
                        new LessThanOrEqualDecimalProjection(
                            new DateDiffDaysIntegerProjection(
                                new LiteralDate(new DateOnly(2024, 6, 10)),
                                new FieldDate(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new OrdersTable().Name,
                                        ]
                                    ).TextValue,
                                    new PlacedOnColumn().Name.TextValue
                                )
                            ),
                            new LiteralInteger(0)
                        )
                    )
                ),
                new SelectItemProjection(
                    "item_lines",
                    new CoalesceIntegerProjection([
                        new FieldIntegerNullable("item_value_by_order", "item_lines"),
                        new LiteralInteger(0),
                    ])
                ),
            ],
            orderBy:
            [
                new OrderItemProjection(
                    new DateDiffDaysIntegerProjection(
                        new LiteralDate(new DateOnly(2024, 6, 10)),
                        new FieldDate(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrdersTable().Name,
                                ]
                            ).TextValue,
                            new PlacedOnColumn().Name.TextValue
                        )
                    ),
                    SortDirection.Desc
                ),
                new OrderItemProjection(
                    new SubtractDecimalProjection([
                        new FieldDecimal(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrdersTable().Name,
                                ]
                            ).TextValue,
                            new OrderTotalColumn().Name.TextValue
                        ),
                        new CoalesceDecimalProjection([
                            new FieldDecimalNullable("item_value_by_order", "item_value"),
                            new LiteralDecimal(0m),
                        ]),
                    ]),
                    SortDirection.Desc
                ),
            ],
            pagination: new ModelPagination(0, 100)
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
                    new UserNameColumn(),
                    new UserActiveColumn(),
                    new OrderStatusColumn(),
                    new PlacedOnColumn(),
                    new OrderTotalColumn(),
                    new Column(new String("unbilled"), new DoubleColumnType()),
                    new Column(new String("days_open"), new LongColumnType()),
                    new Column(new String("aging_bucket"), new StringColumnType()),
                    new Column(new String("item_lines"), new LongColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new UserNameColumn(),
                                new InvariantCell(new String("Bob"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new UserActiveColumn(),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new OrderStatusColumn(),
                                new InvariantCell(new String("shipped"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new PlacedOnColumn(),
                                new InvariantCell(
                                    new Date(
                                        new UShort(3),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new OrderTotalColumn(),
                                new InvariantCell(new Double(200))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("unbilled"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(177.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("days_open"), new LongColumnType()),
                                new InvariantCell(new Long(7))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("aging_bucket"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("7-8"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("item_lines"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
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
                                new UserNameColumn(),
                                new InvariantCell(new String("Cara"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new UserActiveColumn(),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new OrderStatusColumn(),
                                new InvariantCell(new String("shipped"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new PlacedOnColumn(),
                                new InvariantCell(
                                    new Date(
                                        new UShort(5),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new OrderTotalColumn(),
                                new InvariantCell(new Double(300))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("unbilled"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(270.03))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("days_open"), new LongColumnType()),
                                new InvariantCell(new Long(5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("aging_bucket"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("5-6"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("item_lines"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
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
                                new UserNameColumn(),
                                new InvariantCell(new String("Dan"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new UserActiveColumn(),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new OrderStatusColumn(),
                                new InvariantCell(new String("pending"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new PlacedOnColumn(),
                                new InvariantCell(
                                    new Date(
                                        new UShort(6),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new OrderTotalColumn(),
                                new InvariantCell(new Double(100.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("unbilled"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(100.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("days_open"), new LongColumnType()),
                                new InvariantCell(new Long(4))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("aging_bucket"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("1-4"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("item_lines"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
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
