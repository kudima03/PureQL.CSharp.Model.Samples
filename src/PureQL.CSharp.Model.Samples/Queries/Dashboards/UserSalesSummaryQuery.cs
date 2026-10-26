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
using PureQL.CSharp.Model.GroupExpressions;
using PureQL.CSharp.Model.GroupKeys;
using PureQL.CSharp.Model.Keys;
using PureQL.CSharp.Model.Lists;
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.ProjectionExpressions;
using PureQL.CSharp.Model.RowExpressions;
using PureQL.CSharp.Model.SelectItems;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Dashboards;

/// <summary>
/// Selects user_name, user_age, the nullable user_score, orders and cancelled from the
/// subquery orders_by_user, the subquery's units or else 0 as units, its revenue or else
/// 0 rounded to 0 digits as revenue, 0 if that revenue equals the integer 0 and otherwise
/// 100 times the revenue minus the out_of_stock_revenue or else 0 divided by the revenue
/// rounded to 1 digit as in_stock_share_pct, and the subquery's logins or else the
/// integer 0 as logins from schema_with_foreign_keys.users inner joined with the subquery
/// orders_by_user and left joined with the subqueries item_sales_by_user and
/// logins_by_user on user_id, ordered by the revenue or else 0 descending.
/// item_sales_by_user sums item_qty as units, item_qty times product_price as revenue and
/// the same for the products not in stock as out_of_stock_revenue of
/// schema_with_foreign_keys.order_items inner joined with schema_with_foreign_keys.orders
/// on item_order_id equal to order_id and with schema_with_foreign_keys.products on
/// item_product_id equal to product_id, where order_status is not in cancelled and
/// refunded, per order_user_id. orders_by_user counts the orders and the cancelled orders
/// of schema_with_foreign_keys.orders per order_user_id, and logins_by_user counts the
/// rows of audit.logins per login_user_id.
/// </summary>
public sealed record UserSalesSummaryQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new PureQLQuery(
            new MainPlainQuery(
                new From(
                    new FromEntity(
                        new JoinedString(
                            new DotString(),
                            [
                                new RelationalSchemaWithForeignKeys().Name,
                                new UsersTable().Name,
                            ]
                        ).TextValue
                    )
                ),
                [
                    new SelectItemProjection(
                        new SelectItemProjectionNonNullable(
                            new SelectItemProjectionString(
                                "user_name",
                                new StringProjection(
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
                                )
                            )
                        )
                    ),
                    new SelectItemProjection(
                        new SelectItemProjectionNonNullable(
                            new SelectItemProjectionDecimal(
                                "user_age",
                                new ExactDecimalProjection(
                                    new FieldDecimal(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new UsersTable().Name,
                                            ]
                                        ).TextValue,
                                        new UserAgeColumn().Name.TextValue
                                    )
                                )
                            )
                        )
                    ),
                    new SelectItemProjection(
                        new SelectItemProjectionNullable(
                            new SelectItemProjectionDecimalNullable(
                                "user_score",
                                new ExactDecimalNullableProjection(
                                    new FieldDecimalNullable(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new UsersTable().Name,
                                            ]
                                        ).TextValue,
                                        new UserScoreColumn().Name.TextValue
                                    )
                                )
                            )
                        )
                    ),
                    new SelectItemProjection(
                        new SelectItemProjectionNonNullable(
                            new SelectItemProjectionInteger(
                                "orders",
                                new IntegerProjection(
                                    new FieldInteger("orders_by_user", "orders")
                                )
                            )
                        )
                    ),
                    new SelectItemProjection(
                        new SelectItemProjectionNonNullable(
                            new SelectItemProjectionInteger(
                                "cancelled",
                                new IntegerProjection(
                                    new FieldInteger("orders_by_user", "cancelled")
                                )
                            )
                        )
                    ),
                    new SelectItemProjection(
                        new SelectItemProjectionNonNullable(
                            new SelectItemProjectionDecimal(
                                "units",
                                new ExactDecimalProjection(
                                    new ConditionalDecimalProjection(
                                        new CoalesceDecimalProjection([
                                            new DecimalNullableProjection(
                                                new ExactDecimalNullableProjection(
                                                    new FieldDecimalNullable(
                                                        "item_sales_by_user",
                                                        "units"
                                                    )
                                                )
                                            ),
                                            new DecimalNullableProjection(
                                                new ExactDecimalProjection(
                                                    new LiteralDecimal(0m)
                                                )
                                            ),
                                        ])
                                    )
                                )
                            )
                        )
                    ),
                    new SelectItemProjection(
                        new SelectItemProjectionNonNullable(
                            new SelectItemProjectionDecimal(
                                "revenue",
                                new ExactDecimalProjection(
                                    new RoundDecimalProjection(
                                        new DecimalProjection(
                                            new ExactDecimalProjection(
                                                new ConditionalDecimalProjection(
                                                    new CoalesceDecimalProjection([
                                                        new DecimalNullableProjection(
                                                            new ExactDecimalNullableProjection(
                                                                new FieldDecimalNullable(
                                                                    "item_sales_by_user",
                                                                    "revenue"
                                                                )
                                                            )
                                                        ),
                                                        new DecimalNullableProjection(
                                                            new ExactDecimalProjection(
                                                                new LiteralDecimal(0m)
                                                            )
                                                        ),
                                                    ])
                                                )
                                            )
                                        ),
                                        new IntegerProjection(new LiteralInteger(0))
                                    )
                                )
                            )
                        )
                    ),
                    new SelectItemProjection(
                        new SelectItemProjectionNonNullable(
                            new SelectItemProjectionDecimal(
                                "in_stock_share_pct",
                                new ExactDecimalProjection(
                                    new ConditionalDecimalProjection(
                                        new IfDecimalProjection(
                                            new BooleanProjection(
                                                new ComparisonProjection(
                                                    new EqualProjection(
                                                        new EqualDecimalProjection(
                                                            new DecimalNullableProjection(
                                                                new ExactDecimalProjection(
                                                                    new ConditionalDecimalProjection(
                                                                        new CoalesceDecimalProjection([
                                                                            new DecimalNullableProjection(
                                                                                new ExactDecimalNullableProjection(
                                                                                    new FieldDecimalNullable(
                                                                                        "item_sales_by_user",
                                                                                        "revenue"
                                                                                    )
                                                                                )
                                                                            ),
                                                                            new DecimalNullableProjection(
                                                                                new ExactDecimalProjection(
                                                                                    new LiteralDecimal(
                                                                                        0m
                                                                                    )
                                                                                )
                                                                            ),
                                                                        ])
                                                                    )
                                                                )
                                                            ),
                                                            new DecimalNullableProjection(
                                                                new IntegerProjection(
                                                                    new LiteralInteger(0)
                                                                )
                                                            )
                                                        )
                                                    )
                                                )
                                            ),
                                            new DecimalProjection(
                                                new ExactDecimalProjection(
                                                    new LiteralDecimal(0m)
                                                )
                                            ),
                                            new DecimalProjection(
                                                new ExactDecimalProjection(
                                                    new RoundDecimalProjection(
                                                        new DecimalProjection(
                                                            new ExactDecimalProjection(
                                                                new ArithmeticDecimalProjection(
                                                                    new MultiplyDecimalProjection([
                                                                        new DecimalProjection(
                                                                            new ExactDecimalProjection(
                                                                                new LiteralDecimal(
                                                                                    100m
                                                                                )
                                                                            )
                                                                        ),
                                                                        new DecimalProjection(
                                                                            new ExactDecimalProjection(
                                                                                new ArithmeticDecimalProjection(
                                                                                    new DivideDecimalProjection([
                                                                                        new DecimalProjection(
                                                                                            new ExactDecimalProjection(
                                                                                                new ArithmeticDecimalProjection(
                                                                                                    new SubtractDecimalProjection([
                                                                                                        new DecimalProjection(
                                                                                                            new ExactDecimalProjection(
                                                                                                                new ConditionalDecimalProjection(
                                                                                                                    new CoalesceDecimalProjection([
                                                                                                                        new DecimalNullableProjection(
                                                                                                                            new ExactDecimalNullableProjection(
                                                                                                                                new FieldDecimalNullable(
                                                                                                                                    "item_sales_by_user",
                                                                                                                                    "revenue"
                                                                                                                                )
                                                                                                                            )
                                                                                                                        ),
                                                                                                                        new DecimalNullableProjection(
                                                                                                                            new ExactDecimalProjection(
                                                                                                                                new LiteralDecimal(
                                                                                                                                    0m
                                                                                                                                )
                                                                                                                            )
                                                                                                                        ),
                                                                                                                    ])
                                                                                                                )
                                                                                                            )
                                                                                                        ),
                                                                                                        new DecimalProjection(
                                                                                                            new ExactDecimalProjection(
                                                                                                                new ConditionalDecimalProjection(
                                                                                                                    new CoalesceDecimalProjection([
                                                                                                                        new DecimalNullableProjection(
                                                                                                                            new ExactDecimalNullableProjection(
                                                                                                                                new FieldDecimalNullable(
                                                                                                                                    "item_sales_by_user",
                                                                                                                                    "out_of_stock_revenue"
                                                                                                                                )
                                                                                                                            )
                                                                                                                        ),
                                                                                                                        new DecimalNullableProjection(
                                                                                                                            new ExactDecimalProjection(
                                                                                                                                new LiteralDecimal(
                                                                                                                                    0m
                                                                                                                                )
                                                                                                                            )
                                                                                                                        ),
                                                                                                                    ])
                                                                                                                )
                                                                                                            )
                                                                                                        ),
                                                                                                    ])
                                                                                                )
                                                                                            )
                                                                                        ),
                                                                                        new DecimalProjection(
                                                                                            new ExactDecimalProjection(
                                                                                                new ConditionalDecimalProjection(
                                                                                                    new CoalesceDecimalProjection([
                                                                                                        new DecimalNullableProjection(
                                                                                                            new ExactDecimalNullableProjection(
                                                                                                                new FieldDecimalNullable(
                                                                                                                    "item_sales_by_user",
                                                                                                                    "revenue"
                                                                                                                )
                                                                                                            )
                                                                                                        ),
                                                                                                        new DecimalNullableProjection(
                                                                                                            new ExactDecimalProjection(
                                                                                                                new LiteralDecimal(
                                                                                                                    0m
                                                                                                                )
                                                                                                            )
                                                                                                        ),
                                                                                                    ])
                                                                                                )
                                                                                            )
                                                                                        ),
                                                                                    ])
                                                                                )
                                                                            )
                                                                        ),
                                                                    ])
                                                                )
                                                            )
                                                        ),
                                                        new IntegerProjection(
                                                            new LiteralInteger(1)
                                                        )
                                                    )
                                                )
                                            )
                                        )
                                    )
                                )
                            )
                        )
                    ),
                    new SelectItemProjection(
                        new SelectItemProjectionNonNullable(
                            new SelectItemProjectionInteger(
                                "logins",
                                new IntegerProjection(
                                    new ConditionalIntegerProjection(
                                        new CoalesceIntegerProjection([
                                            new IntegerNullableProjection(
                                                new ExactIntegerNullableProjection(
                                                    new FieldIntegerNullable(
                                                        "logins_by_user",
                                                        "logins"
                                                    )
                                                )
                                            ),
                                            new IntegerNullableProjection(
                                                new IntegerProjection(
                                                    new LiteralInteger(0)
                                                )
                                            ),
                                        ])
                                    )
                                )
                            )
                        )
                    ),
                ],
                [
                    new Subquery(
                        "item_sales_by_user",
                        new Query(
                            new GroupedQuery(
                                new From(
                                    new FromEntity(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new OrderItemsTable().Name,
                                            ]
                                        ).TextValue
                                    )
                                ),
                                [
                                    new GroupKey(
                                        new GroupKeyNonNullable(
                                            new GroupKeyUuid(
                                                "user_id",
                                                new UuidRow(
                                                    new FieldUuid(
                                                        new JoinedString(
                                                            new DotString(),
                                                            [
                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                new OrdersTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new OrderUserIdColumn()
                                                            .Name
                                                            .TextValue
                                                    )
                                                )
                                            )
                                        )
                                    ),
                                ],
                                [
                                    new SelectItemGroup(
                                        new SelectItemGroupNonNullable(
                                            new SelectItemGroupUuid(
                                                "user_id",
                                                new UuidGroup(new KeyUuid("user_id"))
                                            )
                                        )
                                    ),
                                    new SelectItemGroup(
                                        new SelectItemGroupNonNullable(
                                            new SelectItemGroupDecimal(
                                                "units",
                                                new ExactDecimalGroup(
                                                    new AggregateDecimalGroup(
                                                        new SumDecimalGroup(
                                                            new DecimalNullableRow(
                                                                new ExactDecimalRow(
                                                                    new FieldDecimal(
                                                                        new JoinedString(
                                                                            new DotString(),
                                                                            [
                                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                                new OrderItemsTable().Name,
                                                                            ]
                                                                        ).TextValue,
                                                                        new ItemQtyColumn()
                                                                            .Name
                                                                            .TextValue
                                                                    )
                                                                )
                                                            )
                                                        )
                                                    )
                                                )
                                            )
                                        )
                                    ),
                                    new SelectItemGroup(
                                        new SelectItemGroupNonNullable(
                                            new SelectItemGroupDecimal(
                                                "revenue",
                                                new ExactDecimalGroup(
                                                    new AggregateDecimalGroup(
                                                        new SumDecimalGroup(
                                                            new DecimalNullableRow(
                                                                new ExactDecimalRow(
                                                                    new ArithmeticDecimalRow(
                                                                        new MultiplyDecimalRow([
                                                                            new DecimalRow(
                                                                                new ExactDecimalRow(
                                                                                    new FieldDecimal(
                                                                                        new JoinedString(
                                                                                            new DotString(),
                                                                                            [
                                                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                                                new OrderItemsTable().Name,
                                                                                            ]
                                                                                        ).TextValue,
                                                                                        new ItemQtyColumn()
                                                                                            .Name
                                                                                            .TextValue
                                                                                    )
                                                                                )
                                                                            ),
                                                                            new DecimalRow(
                                                                                new ExactDecimalRow(
                                                                                    new FieldDecimal(
                                                                                        new JoinedString(
                                                                                            new DotString(),
                                                                                            [
                                                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                                                new ProductsTable().Name,
                                                                                            ]
                                                                                        ).TextValue,
                                                                                        new ProductPriceColumn()
                                                                                            .Name
                                                                                            .TextValue
                                                                                    )
                                                                                )
                                                                            ),
                                                                        ])
                                                                    )
                                                                )
                                                            )
                                                        )
                                                    )
                                                )
                                            )
                                        )
                                    ),
                                    new SelectItemGroup(
                                        new SelectItemGroupNonNullable(
                                            new SelectItemGroupDecimal(
                                                "out_of_stock_revenue",
                                                new ExactDecimalGroup(
                                                    new AggregateDecimalGroup(
                                                        new SumDecimalGroup(
                                                            new DecimalNullableRow(
                                                                new ExactDecimalRow(
                                                                    new ArithmeticDecimalRow(
                                                                        new MultiplyDecimalRow([
                                                                            new DecimalRow(
                                                                                new ExactDecimalRow(
                                                                                    new FieldDecimal(
                                                                                        new JoinedString(
                                                                                            new DotString(),
                                                                                            [
                                                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                                                new OrderItemsTable().Name,
                                                                                            ]
                                                                                        ).TextValue,
                                                                                        new ItemQtyColumn()
                                                                                            .Name
                                                                                            .TextValue
                                                                                    )
                                                                                )
                                                                            ),
                                                                            new DecimalRow(
                                                                                new ExactDecimalRow(
                                                                                    new FieldDecimal(
                                                                                        new JoinedString(
                                                                                            new DotString(),
                                                                                            [
                                                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                                                new ProductsTable().Name,
                                                                                            ]
                                                                                        ).TextValue,
                                                                                        new ProductPriceColumn()
                                                                                            .Name
                                                                                            .TextValue
                                                                                    )
                                                                                )
                                                                            ),
                                                                        ])
                                                                    )
                                                                )
                                                            ),
                                                            new BooleanRow(
                                                                new LogicalRow(
                                                                    new NotRow(
                                                                        new BooleanRow(
                                                                            new FieldBoolean(
                                                                                new JoinedString(
                                                                                    new DotString(),
                                                                                    [
                                                                                        new RelationalSchemaWithForeignKeys().Name,
                                                                                        new ProductsTable().Name,
                                                                                    ]
                                                                                ).TextValue,
                                                                                new ProductInStockColumn()
                                                                                    .Name
                                                                                    .TextValue
                                                                            )
                                                                        )
                                                                    )
                                                                )
                                                            )
                                                        )
                                                    )
                                                )
                                            )
                                        )
                                    ),
                                ],
                                [
                                    new Join(
                                        new JoinEntity(
                                            JoinKind.Inner,
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new RelationalSchemaWithForeignKeys().Name,
                                                    new OrdersTable().Name,
                                                ]
                                            ).TextValue,
                                            new BooleanRow(
                                                new ComparisonRow(
                                                    new EqualRow(
                                                        new EqualUuidRow(
                                                            new UuidNullableRow(
                                                                new UuidRow(
                                                                    new FieldUuid(
                                                                        new JoinedString(
                                                                            new DotString(),
                                                                            [
                                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                                new OrderItemsTable().Name,
                                                                            ]
                                                                        ).TextValue,
                                                                        new ItemOrderIdColumn()
                                                                            .Name
                                                                            .TextValue
                                                                    )
                                                                )
                                                            ),
                                                            new UuidNullableRow(
                                                                new UuidRow(
                                                                    new FieldUuid(
                                                                        new JoinedString(
                                                                            new DotString(),
                                                                            [
                                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                                new OrdersTable().Name,
                                                                            ]
                                                                        ).TextValue,
                                                                        new OrderIdColumn()
                                                                            .Name
                                                                            .TextValue
                                                                    )
                                                                )
                                                            )
                                                        )
                                                    )
                                                )
                                            )
                                        )
                                    ),
                                    new Join(
                                        new JoinEntity(
                                            JoinKind.Inner,
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new RelationalSchemaWithForeignKeys().Name,
                                                    new ProductsTable().Name,
                                                ]
                                            ).TextValue,
                                            new BooleanRow(
                                                new ComparisonRow(
                                                    new EqualRow(
                                                        new EqualUuidRow(
                                                            new UuidNullableRow(
                                                                new UuidRow(
                                                                    new FieldUuid(
                                                                        new JoinedString(
                                                                            new DotString(),
                                                                            [
                                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                                new OrderItemsTable().Name,
                                                                            ]
                                                                        ).TextValue,
                                                                        new ItemProductIdColumn()
                                                                            .Name
                                                                            .TextValue
                                                                    )
                                                                )
                                                            ),
                                                            new UuidNullableRow(
                                                                new UuidRow(
                                                                    new FieldUuid(
                                                                        new JoinedString(
                                                                            new DotString(),
                                                                            [
                                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                                new ProductsTable().Name,
                                                                            ]
                                                                        ).TextValue,
                                                                        new ProductIdColumn()
                                                                            .Name
                                                                            .TextValue
                                                                    )
                                                                )
                                                            )
                                                        )
                                                    )
                                                )
                                            )
                                        )
                                    ),
                                ],
                                new BooleanRow(
                                    new LogicalRow(
                                        new NotRow(
                                            new BooleanRow(
                                                new ComparisonRow(
                                                    new InRow(
                                                        new InStringRow(
                                                            new StringNullableRow(
                                                                new StringRow(
                                                                    new FieldString(
                                                                        new JoinedString(
                                                                            new DotString(),
                                                                            [
                                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                                new OrdersTable().Name,
                                                                            ]
                                                                        ).TextValue,
                                                                        new OrderStatusColumn()
                                                                            .Name
                                                                            .TextValue
                                                                    )
                                                                )
                                                            ),
                                                            new ListString(
                                                                new ListLiteralString([
                                                                    "cancelled",
                                                                    "refunded",
                                                                ])
                                                            )
                                                        )
                                                    )
                                                )
                                            )
                                        )
                                    )
                                ),
                                having: null,
                                orderBy: null,
                                pagination: null,
                                distinctBy: null
                            )
                        )
                    ),
                    new Subquery(
                        "orders_by_user",
                        new Query(
                            new GroupedQuery(
                                new From(
                                    new FromEntity(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new OrdersTable().Name,
                                            ]
                                        ).TextValue
                                    )
                                ),
                                [
                                    new GroupKey(
                                        new GroupKeyNonNullable(
                                            new GroupKeyUuid(
                                                "user_id",
                                                new UuidRow(
                                                    new FieldUuid(
                                                        new JoinedString(
                                                            new DotString(),
                                                            [
                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                new OrdersTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new OrderUserIdColumn()
                                                            .Name
                                                            .TextValue
                                                    )
                                                )
                                            )
                                        )
                                    ),
                                ],
                                [
                                    new SelectItemGroup(
                                        new SelectItemGroupNonNullable(
                                            new SelectItemGroupUuid(
                                                "user_id",
                                                new UuidGroup(new KeyUuid("user_id"))
                                            )
                                        )
                                    ),
                                    new SelectItemGroup(
                                        new SelectItemGroupNonNullable(
                                            new SelectItemGroupInteger(
                                                "orders",
                                                new IntegerGroup(
                                                    new AggregateIntegerGroup(
                                                        new CountGroup()
                                                    )
                                                )
                                            )
                                        )
                                    ),
                                    new SelectItemGroup(
                                        new SelectItemGroupNonNullable(
                                            new SelectItemGroupInteger(
                                                "cancelled",
                                                new IntegerGroup(
                                                    new AggregateIntegerGroup(
                                                        new CountGroup(
                                                            new BooleanRow(
                                                                new ComparisonRow(
                                                                    new EqualRow(
                                                                        new EqualStringRow(
                                                                            new StringNullableRow(
                                                                                new StringRow(
                                                                                    new FieldString(
                                                                                        new JoinedString(
                                                                                            new DotString(),
                                                                                            [
                                                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                                                new OrdersTable().Name,
                                                                                            ]
                                                                                        ).TextValue,
                                                                                        new OrderStatusColumn()
                                                                                            .Name
                                                                                            .TextValue
                                                                                    )
                                                                                )
                                                                            ),
                                                                            new StringNullableRow(
                                                                                new StringRow(
                                                                                    new LiteralString(
                                                                                        "cancelled"
                                                                                    )
                                                                                )
                                                                            )
                                                                        )
                                                                    )
                                                                )
                                                            )
                                                        )
                                                    )
                                                )
                                            )
                                        )
                                    ),
                                ],
                                joins: null,
                                where: null,
                                having: null,
                                orderBy: null,
                                pagination: null,
                                distinctBy: null
                            )
                        )
                    ),
                    new Subquery(
                        "logins_by_user",
                        new Query(
                            new GroupedQuery(
                                new From(
                                    new FromEntity(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new AuditRelationalSchema().Name,
                                                new LoginsTable().Name,
                                            ]
                                        ).TextValue
                                    )
                                ),
                                [
                                    new GroupKey(
                                        new GroupKeyNonNullable(
                                            new GroupKeyUuid(
                                                "user_id",
                                                new UuidRow(
                                                    new FieldUuid(
                                                        new JoinedString(
                                                            new DotString(),
                                                            [
                                                                new AuditRelationalSchema().Name,
                                                                new LoginsTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new LoginUserIdColumn()
                                                            .Name
                                                            .TextValue
                                                    )
                                                )
                                            )
                                        )
                                    ),
                                ],
                                [
                                    new SelectItemGroup(
                                        new SelectItemGroupNonNullable(
                                            new SelectItemGroupUuid(
                                                "user_id",
                                                new UuidGroup(new KeyUuid("user_id"))
                                            )
                                        )
                                    ),
                                    new SelectItemGroup(
                                        new SelectItemGroupNonNullable(
                                            new SelectItemGroupInteger(
                                                "logins",
                                                new IntegerGroup(
                                                    new AggregateIntegerGroup(
                                                        new CountGroup()
                                                    )
                                                )
                                            )
                                        )
                                    ),
                                ],
                                joins: null,
                                where: null,
                                having: null,
                                orderBy: null,
                                pagination: null,
                                distinctBy: null
                            )
                        )
                    ),
                ],
                [
                    new Join(
                        new JoinSubquery(
                            JoinKind.Inner,
                            "orders_by_user",
                            new BooleanRow(
                                new ComparisonRow(
                                    new EqualRow(
                                        new EqualUuidRow(
                                            new UuidNullableRow(
                                                new UuidRow(
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
                                            new UuidNullableRow(
                                                new UuidRow(
                                                    new FieldUuid(
                                                        "orders_by_user",
                                                        "user_id"
                                                    )
                                                )
                                            )
                                        )
                                    )
                                )
                            )
                        )
                    ),
                    new Join(
                        new JoinSubquery(
                            JoinKind.Left,
                            "item_sales_by_user",
                            new BooleanRow(
                                new ComparisonRow(
                                    new EqualRow(
                                        new EqualUuidRow(
                                            new UuidNullableRow(
                                                new UuidRow(
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
                                            new UuidNullableRow(
                                                new UuidRow(
                                                    new FieldUuid(
                                                        "item_sales_by_user",
                                                        "user_id"
                                                    )
                                                )
                                            )
                                        )
                                    )
                                )
                            )
                        )
                    ),
                    new Join(
                        new JoinSubquery(
                            JoinKind.Left,
                            "logins_by_user",
                            new BooleanRow(
                                new ComparisonRow(
                                    new EqualRow(
                                        new EqualUuidRow(
                                            new UuidNullableRow(
                                                new UuidRow(
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
                                            new UuidNullableRow(
                                                new UuidRow(
                                                    new FieldUuid(
                                                        "logins_by_user",
                                                        "user_id"
                                                    )
                                                )
                                            )
                                        )
                                    )
                                )
                            )
                        )
                    ),
                ],
                where: null,
                [
                    new OrderItemProjection(
                        new ExprProjection(
                            new DecimalNullableProjection(
                                new ExactDecimalProjection(
                                    new ConditionalDecimalProjection(
                                        new CoalesceDecimalProjection([
                                            new DecimalNullableProjection(
                                                new ExactDecimalNullableProjection(
                                                    new FieldDecimalNullable(
                                                        "item_sales_by_user",
                                                        "revenue"
                                                    )
                                                )
                                            ),
                                            new DecimalNullableProjection(
                                                new ExactDecimalProjection(
                                                    new LiteralDecimal(0m)
                                                )
                                            ),
                                        ])
                                    )
                                )
                            )
                        ),
                        SortDirection.Desc
                    ),
                ],
                pagination: null,
                distinctBy: null
            )
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
                    new UserNameColumn(),
                    new UserAgeColumn(),
                    new UserScoreColumn(),
                    new Column(new String("orders"), new LongColumnType()),
                    new Column(new String("cancelled"), new LongColumnType()),
                    new Column(new String("units"), new DoubleColumnType()),
                    new Column(new String("revenue"), new DoubleColumnType()),
                    new Column(new String("in_stock_share_pct"), new DoubleColumnType()),
                    new Column(new String("logins"), new LongColumnType()),
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
                                new UserAgeColumn(),
                                new InvariantCell(new Double(30))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new UserScoreColumn(),
                                new InvariantCell(new Double(30))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("cancelled"), new LongColumnType()),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("units"), new DoubleColumnType()),
                                new InvariantCell(new Double(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("revenue"), new DoubleColumnType()),
                                new InvariantCell(new Double(40))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("in_stock_share_pct"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(50))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("logins"), new LongColumnType()),
                                new InvariantCell(new Long(2))
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
                                new UserAgeColumn(),
                                new InvariantCell(new Double(30))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new UserScoreColumn(),
                                new InvariantCell(new Double(30))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("cancelled"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("units"), new DoubleColumnType()),
                                new InvariantCell(new Double(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("revenue"), new DoubleColumnType()),
                                new InvariantCell(new Double(30))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("in_stock_share_pct"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(100))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("logins"), new LongColumnType()),
                                new InvariantCell(new Long(0))
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
                                new UserAgeColumn(),
                                new InvariantCell(new Double(25))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new UserScoreColumn(),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("cancelled"), new LongColumnType()),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("units"), new DoubleColumnType()),
                                new InvariantCell(new Double(5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("revenue"), new DoubleColumnType()),
                                new InvariantCell(new Double(23))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("in_stock_share_pct"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(100))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("logins"), new LongColumnType()),
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
                                new UserAgeColumn(),
                                new InvariantCell(new Double(42))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new UserScoreColumn(),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("cancelled"), new LongColumnType()),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("units"), new DoubleColumnType()),
                                new InvariantCell(new Double(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("revenue"), new DoubleColumnType()),
                                new InvariantCell(new Double(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("in_stock_share_pct"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("logins"), new LongColumnType()),
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
