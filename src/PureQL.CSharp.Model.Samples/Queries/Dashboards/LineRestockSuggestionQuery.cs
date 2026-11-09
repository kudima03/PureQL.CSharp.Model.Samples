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
/// Selects user_name, product_name, order_total, item_qty times product_price as
/// line_value, order_total minus that line value as rest_of_order, the subquery's units
/// or else 0 divided by 3 rounded to 2 digits as daily_units, if those daily units are
/// greater than 0 the rest of the order divided by them rounded to 1 digit and otherwise
/// 999 as days_of_cover, and if item_qty times 2 minus the units is greater than 0 that
/// difference and otherwise 0 as suggested_units from schema_with_foreign_keys.order_items
/// inner joined with schema_with_foreign_keys.orders on item_order_id equal to order_id,
/// with schema_with_foreign_keys.users on order_user_id equal to user_id and with
/// schema_with_foreign_keys.products on item_product_id equal to product_id, and left
/// joined with the subquery recent_units on order_user_id and item_product_id, where the
/// rest of the order is less than or equal to 200 and product_in_stock holds, ordered by
/// the rest of the order then product_name, skipping 0 rows and taking 100. recent_units
/// sums item_qty of the order items placed on or after 2024-06-03 per order_user_id and
/// item_product_id.
/// </summary>
public sealed record LineRestockSuggestionQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithPagination(
            subqueries:
            [
                new Subquery(
                    "recent_units",
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
                        ],
                        where: new GreaterThanOrEqualDateRow(
                            new FieldDate(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new OrdersTable().Name,
                                    ]
                                ).TextValue,
                                new PlacedOnColumn().Name.TextValue
                            ),
                            new LiteralDate(new DateOnly(2024, 6, 3))
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
                            new GroupKey(
                                "product_id",
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
                        ],
                        select:
                        [
                            new SelectItemGroup("user_id", new KeyUuid("user_id")),
                            new SelectItemGroup("product_id", new KeyUuid("product_id")),
                            new SelectItemGroup(
                                "units",
                                new SumDecimalGroup(
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
                        ]
                    )
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
                new JoinSubquery(
                    JoinKind.Left,
                    "recent_units",
                    new AndRow([
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
                            new FieldUuid("recent_units", "user_id")
                        ),
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
                            new FieldUuid("recent_units", "product_id")
                        ),
                    ])
                ),
            ],
            where: new AndRow([
                new LessThanOrEqualDecimalRow(
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
                        ]),
                    ]),
                    new LiteralDecimal(200m)
                ),
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
            ]),
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
                    "product_name",
                    new FieldString(
                        new JoinedString(
                            new DotString(),
                            [
                                new RelationalSchemaWithForeignKeys().Name,
                                new ProductsTable().Name,
                            ]
                        ).TextValue,
                        new ProductNameColumn().Name.TextValue
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
                    "line_value",
                    new MultiplyDecimalProjection([
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
                ),
                new SelectItemProjection(
                    "rest_of_order",
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
                        new MultiplyDecimalProjection([
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
                        ]),
                    ])
                ),
                new SelectItemProjection(
                    "daily_units",
                    new RoundDecimalProjection(
                        new DivideDecimalProjection([
                            new CoalesceDecimalProjection([
                                new FieldDecimalNullable("recent_units", "units"),
                                new LiteralDecimal(0m),
                            ]),
                            new LiteralDecimal(3m),
                        ]),
                        new LiteralInteger(2)
                    )
                ),
                new SelectItemProjection(
                    "days_of_cover",
                    new IfDecimalProjection(
                        new RoundDecimalProjection(
                            new DivideDecimalProjection([
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
                                    new MultiplyDecimalProjection([
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
                                    ]),
                                ]),
                                new DivideDecimalProjection([
                                    new CoalesceDecimalProjection([
                                        new FieldDecimalNullable("recent_units", "units"),
                                        new LiteralDecimal(0m),
                                    ]),
                                    new LiteralDecimal(3m),
                                ]),
                            ]),
                            new LiteralInteger(1)
                        ),
                        new LiteralDecimal(999m),
                        new GreaterThanDecimalProjection(
                            new DivideDecimalProjection([
                                new CoalesceDecimalProjection([
                                    new FieldDecimalNullable("recent_units", "units"),
                                    new LiteralDecimal(0m),
                                ]),
                                new LiteralDecimal(3m),
                            ]),
                            new LiteralDecimal(0m)
                        )
                    )
                ),
                new SelectItemProjection(
                    "suggested_units",
                    new IfDecimalProjection(
                        new SubtractDecimalProjection([
                            new MultiplyDecimalProjection([
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
                                new LiteralDecimal(2m),
                            ]),
                            new CoalesceDecimalProjection([
                                new FieldDecimalNullable("recent_units", "units"),
                                new LiteralDecimal(0m),
                            ]),
                        ]),
                        new LiteralDecimal(0m),
                        new GreaterThanDecimalProjection(
                            new SubtractDecimalProjection([
                                new MultiplyDecimalProjection([
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
                                    new LiteralDecimal(2m),
                                ]),
                                new CoalesceDecimalProjection([
                                    new FieldDecimalNullable("recent_units", "units"),
                                    new LiteralDecimal(0m),
                                ]),
                            ]),
                            new LiteralDecimal(0m)
                        )
                    )
                ),
            ],
            orderBy:
            [
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
                        new MultiplyDecimalProjection([
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
                        ]),
                    ])
                ),
                new OrderItemProjection(
                    new FieldString(
                        new JoinedString(
                            new DotString(),
                            [
                                new RelationalSchemaWithForeignKeys().Name,
                                new ProductsTable().Name,
                            ]
                        ).TextValue,
                        new ProductNameColumn().Name.TextValue
                    )
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
                    new ProductNameColumn(),
                    new OrderTotalColumn(),
                    new Column(new String("line_value"), new DoubleColumnType()),
                    new Column(new String("rest_of_order"), new DoubleColumnType()),
                    new Column(new String("daily_units"), new DoubleColumnType()),
                    new Column(new String("days_of_cover"), new DoubleColumnType()),
                    new Column(new String("suggested_units"), new DoubleColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new UserNameColumn(),
                                new InvariantCell(new String("Ann"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new ProductNameColumn(),
                                new InvariantCell(new String("Widget"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new OrderTotalColumn(),
                                new InvariantCell(new Double(100.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("line_value"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(19.98))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rest_of_order"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(80.52))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("daily_units"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("days_of_cover"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(999))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("suggested_units"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(4))
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
                                new InvariantCell(new String("Bob"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new ProductNameColumn(),
                                new InvariantCell(new String("Gizmo"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new OrderTotalColumn(),
                                new InvariantCell(new Double(200))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("line_value"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(22.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rest_of_order"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(177.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("daily_units"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1.67))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("days_of_cover"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(106.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("suggested_units"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(5))
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
