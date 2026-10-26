using Pure.Collections.Generic;
using Pure.Primitives.Bool;
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
/// Selects product_name, product_in_stock, item_lines, early_pct and
/// max_days_since_signup from the subquery sales_performance, its
/// avg_days_after_last_login rounded to 2 digits, its order_total_sum divided by its
/// units rounded to 3 digits as order_total_per_unit, the subquery inactive_user_lines'
/// item_lines or else the integer 0 as inactive_user_lines, and 100 times those lines
/// divided by the sales_performance item_lines rounded to 2 digits as
/// inactive_per_100_lines from schema_with_foreign_keys.products inner joined with the
/// subquery sales_performance and left joined with the subquery inactive_user_lines on
/// product_id, ordered by early_pct descending then product_name. sales_performance reads
/// schema_with_foreign_keys.order_items inner joined with schema_with_foreign_keys.orders
/// on item_order_id equal to order_id and with schema_with_foreign_keys.users on
/// order_user_id equal to user_id, where order_status is shipped, grouped by
/// item_product_id, and selects the count of rows as item_lines, 0 if that count equals
/// the integer 0 and otherwise 100 times the count of rows placed on or before 2024-06-03
/// divided by the count of rows rounded to 1 digit as early_pct, the average of the
/// seconds from last_login to placed_at divided by 86400 as avg_days_after_last_login, the
/// maximum of the days from signup_date to placed_on as max_days_since_signup, the sum of
/// order_total as order_total_sum and the sum of item_qty as units. inactive_user_lines
/// counts the same joined rows whose user_active equals false per item_product_id.
/// </summary>
public sealed record ProductSalesPerformanceQuery
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
                                new ProductsTable().Name,
                            ]
                        ).TextValue
                    )
                ),
                [
                    new SelectItemProjection(
                        new SelectItemProjectionNonNullable(
                            new SelectItemProjectionString(
                                "product_name",
                                new StringProjection(
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
                                )
                            )
                        )
                    ),
                    new SelectItemProjection(
                        new SelectItemProjectionNonNullable(
                            new SelectItemProjectionBoolean(
                                "product_in_stock",
                                new BooleanProjection(
                                    new FieldBoolean(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new ProductsTable().Name,
                                            ]
                                        ).TextValue,
                                        new ProductInStockColumn().Name.TextValue
                                    )
                                )
                            )
                        )
                    ),
                    new SelectItemProjection(
                        new SelectItemProjectionNonNullable(
                            new SelectItemProjectionInteger(
                                "item_lines",
                                new IntegerProjection(
                                    new FieldInteger("sales_performance", "item_lines")
                                )
                            )
                        )
                    ),
                    new SelectItemProjection(
                        new SelectItemProjectionNonNullable(
                            new SelectItemProjectionDecimal(
                                "early_pct",
                                new ExactDecimalProjection(
                                    new FieldDecimal("sales_performance", "early_pct")
                                )
                            )
                        )
                    ),
                    new SelectItemProjection(
                        new SelectItemProjectionNonNullable(
                            new SelectItemProjectionDecimal(
                                "avg_days_after_last_login",
                                new ExactDecimalProjection(
                                    new RoundDecimalProjection(
                                        new DecimalProjection(
                                            new ExactDecimalProjection(
                                                new FieldDecimal(
                                                    "sales_performance",
                                                    "avg_days_after_last_login"
                                                )
                                            )
                                        ),
                                        new IntegerProjection(new LiteralInteger(2))
                                    )
                                )
                            )
                        )
                    ),
                    new SelectItemProjection(
                        new SelectItemProjectionNonNullable(
                            new SelectItemProjectionInteger(
                                "max_days_since_signup",
                                new IntegerProjection(
                                    new FieldInteger(
                                        "sales_performance",
                                        "max_days_since_signup"
                                    )
                                )
                            )
                        )
                    ),
                    new SelectItemProjection(
                        new SelectItemProjectionNonNullable(
                            new SelectItemProjectionDecimal(
                                "order_total_per_unit",
                                new ExactDecimalProjection(
                                    new RoundDecimalProjection(
                                        new DecimalProjection(
                                            new ExactDecimalProjection(
                                                new ArithmeticDecimalProjection(
                                                    new DivideDecimalProjection([
                                                        new DecimalProjection(
                                                            new ExactDecimalProjection(
                                                                new FieldDecimal(
                                                                    "sales_performance",
                                                                    "order_total_sum"
                                                                )
                                                            )
                                                        ),
                                                        new DecimalProjection(
                                                            new ExactDecimalProjection(
                                                                new FieldDecimal(
                                                                    "sales_performance",
                                                                    "units"
                                                                )
                                                            )
                                                        ),
                                                    ])
                                                )
                                            )
                                        ),
                                        new IntegerProjection(new LiteralInteger(3))
                                    )
                                )
                            )
                        )
                    ),
                    new SelectItemProjection(
                        new SelectItemProjectionNonNullable(
                            new SelectItemProjectionInteger(
                                "inactive_user_lines",
                                new IntegerProjection(
                                    new ConditionalIntegerProjection(
                                        new CoalesceIntegerProjection([
                                            new IntegerNullableProjection(
                                                new ExactIntegerNullableProjection(
                                                    new FieldIntegerNullable(
                                                        "inactive_user_lines",
                                                        "item_lines"
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
                    new SelectItemProjection(
                        new SelectItemProjectionNonNullable(
                            new SelectItemProjectionDecimal(
                                "inactive_per_100_lines",
                                new ExactDecimalProjection(
                                    new RoundDecimalProjection(
                                        new DecimalProjection(
                                            new ExactDecimalProjection(
                                                new ArithmeticDecimalProjection(
                                                    new MultiplyDecimalProjection([
                                                        new DecimalProjection(
                                                            new ExactDecimalProjection(
                                                                new LiteralDecimal(100m)
                                                            )
                                                        ),
                                                        new DecimalProjection(
                                                            new ExactDecimalProjection(
                                                                new ArithmeticDecimalProjection(
                                                                    new DivideDecimalProjection([
                                                                        new DecimalProjection(
                                                                            new IntegerProjection(
                                                                                new ConditionalIntegerProjection(
                                                                                    new CoalesceIntegerProjection([
                                                                                        new IntegerNullableProjection(
                                                                                            new ExactIntegerNullableProjection(
                                                                                                new FieldIntegerNullable(
                                                                                                    "inactive_user_lines",
                                                                                                    "item_lines"
                                                                                                )
                                                                                            )
                                                                                        ),
                                                                                        new IntegerNullableProjection(
                                                                                            new IntegerProjection(
                                                                                                new LiteralInteger(
                                                                                                    0
                                                                                                )
                                                                                            )
                                                                                        ),
                                                                                    ])
                                                                                )
                                                                            )
                                                                        ),
                                                                        new DecimalProjection(
                                                                            new IntegerProjection(
                                                                                new FieldInteger(
                                                                                    "sales_performance",
                                                                                    "item_lines"
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
                                        new IntegerProjection(new LiteralInteger(2))
                                    )
                                )
                            )
                        )
                    ),
                ],
                [
                    new Subquery(
                        "sales_performance",
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
                                                "product_id",
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
                                            )
                                        )
                                    ),
                                ],
                                [
                                    new SelectItemGroup(
                                        new SelectItemGroupNonNullable(
                                            new SelectItemGroupUuid(
                                                "product_id",
                                                new UuidGroup(new KeyUuid("product_id"))
                                            )
                                        )
                                    ),
                                    new SelectItemGroup(
                                        new SelectItemGroupNonNullable(
                                            new SelectItemGroupInteger(
                                                "item_lines",
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
                                            new SelectItemGroupDecimal(
                                                "early_pct",
                                                new ExactDecimalGroup(
                                                    new ConditionalDecimalGroup(
                                                        new IfDecimalGroup(
                                                            new BooleanGroup(
                                                                new ComparisonGroup(
                                                                    new EqualGroup(
                                                                        new EqualDecimalGroup(
                                                                            new DecimalNullableGroup(
                                                                                new IntegerGroup(
                                                                                    new AggregateIntegerGroup(
                                                                                        new CountGroup()
                                                                                    )
                                                                                )
                                                                            ),
                                                                            new DecimalNullableGroup(
                                                                                new IntegerGroup(
                                                                                    new LiteralInteger(
                                                                                        0
                                                                                    )
                                                                                )
                                                                            )
                                                                        )
                                                                    )
                                                                )
                                                            ),
                                                            new DecimalGroup(
                                                                new ExactDecimalGroup(
                                                                    new LiteralDecimal(0m)
                                                                )
                                                            ),
                                                            new DecimalGroup(
                                                                new ExactDecimalGroup(
                                                                    new RoundDecimalGroup(
                                                                        new DecimalGroup(
                                                                            new ExactDecimalGroup(
                                                                                new ArithmeticDecimalGroup(
                                                                                    new MultiplyDecimalGroup([
                                                                                        new DecimalGroup(
                                                                                            new ExactDecimalGroup(
                                                                                                new LiteralDecimal(
                                                                                                    100m
                                                                                                )
                                                                                            )
                                                                                        ),
                                                                                        new DecimalGroup(
                                                                                            new ExactDecimalGroup(
                                                                                                new ArithmeticDecimalGroup(
                                                                                                    new DivideDecimalGroup([
                                                                                                        new DecimalGroup(
                                                                                                            new IntegerGroup(
                                                                                                                new AggregateIntegerGroup(
                                                                                                                    new CountGroup(
                                                                                                                        new BooleanRow(
                                                                                                                            new ComparisonRow(
                                                                                                                                new LessThanOrEqualRow(
                                                                                                                                    new LessThanOrEqualDateRow(
                                                                                                                                        new DateNullableRow(
                                                                                                                                            new DateRow(
                                                                                                                                                new FieldDate(
                                                                                                                                                    new JoinedString(
                                                                                                                                                        new DotString(),
                                                                                                                                                        [
                                                                                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                            new OrdersTable().Name,
                                                                                                                                                        ]
                                                                                                                                                    ).TextValue,
                                                                                                                                                    new PlacedOnColumn()
                                                                                                                                                        .Name
                                                                                                                                                        .TextValue
                                                                                                                                                )
                                                                                                                                            )
                                                                                                                                        ),
                                                                                                                                        new DateNullableRow(
                                                                                                                                            new DateRow(
                                                                                                                                                new LiteralDate(
                                                                                                                                                    new DateOnly(
                                                                                                                                                        2024,
                                                                                                                                                        6,
                                                                                                                                                        3
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
                                                                                                        new DecimalGroup(
                                                                                                            new IntegerGroup(
                                                                                                                new AggregateIntegerGroup(
                                                                                                                    new CountGroup()
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
                                                                        new IntegerGroup(
                                                                            new LiteralInteger(
                                                                                1
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
                                    new SelectItemGroup(
                                        new SelectItemGroupNonNullable(
                                            new SelectItemGroupDecimal(
                                                "avg_days_after_last_login",
                                                new ExactDecimalGroup(
                                                    new AggregateDecimalGroup(
                                                        new AverageDecimalGroup(
                                                            new DecimalRow(
                                                                new ExactDecimalRow(
                                                                    new ArithmeticDecimalRow(
                                                                        new DivideDecimalRow([
                                                                            new DecimalRow(
                                                                                new ExactDecimalRow(
                                                                                    new DifferenceDecimalRow(
                                                                                        new DatetimeDiffSecondsDecimalRow(
                                                                                            new DatetimeRow(
                                                                                                new FieldDatetime(
                                                                                                    new JoinedString(
                                                                                                        new DotString(),
                                                                                                        [
                                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                                            new OrdersTable().Name,
                                                                                                        ]
                                                                                                    ).TextValue,
                                                                                                    new PlacedAtColumn()
                                                                                                        .Name
                                                                                                        .TextValue
                                                                                                )
                                                                                            ),
                                                                                            new DatetimeRow(
                                                                                                new FieldDatetime(
                                                                                                    new JoinedString(
                                                                                                        new DotString(),
                                                                                                        [
                                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                                            new UsersTable().Name,
                                                                                                        ]
                                                                                                    ).TextValue,
                                                                                                    new LastLoginColumn()
                                                                                                        .Name
                                                                                                        .TextValue
                                                                                                )
                                                                                            )
                                                                                        )
                                                                                    )
                                                                                )
                                                                            ),
                                                                            new DecimalRow(
                                                                                new ExactDecimalRow(
                                                                                    new LiteralDecimal(
                                                                                        86400m
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
                                            new SelectItemGroupInteger(
                                                "max_days_since_signup",
                                                new IntegerGroup(
                                                    new AggregateIntegerGroup(
                                                        new MaxIntegerGroup(
                                                            new IntegerRow(
                                                                new DateDiffDaysIntegerRow(
                                                                    new DateRow(
                                                                        new FieldDate(
                                                                            new JoinedString(
                                                                                new DotString(),
                                                                                [
                                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                                    new OrdersTable().Name,
                                                                                ]
                                                                            ).TextValue,
                                                                            new PlacedOnColumn()
                                                                                .Name
                                                                                .TextValue
                                                                        )
                                                                    ),
                                                                    new DateRow(
                                                                        new FieldDate(
                                                                            new JoinedString(
                                                                                new DotString(),
                                                                                [
                                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                                    new UsersTable().Name,
                                                                                ]
                                                                            ).TextValue,
                                                                            new SignupDateColumn()
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
                                    ),
                                    new SelectItemGroup(
                                        new SelectItemGroupNonNullable(
                                            new SelectItemGroupDecimal(
                                                "order_total_sum",
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
                                                                                new OrdersTable().Name,
                                                                            ]
                                                                        ).TextValue,
                                                                        new OrderTotalColumn()
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
                                                    new UsersTable().Name,
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
                                                                                new OrdersTable().Name,
                                                                            ]
                                                                        ).TextValue,
                                                                        new OrderUserIdColumn()
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
                                                                                new UsersTable().Name,
                                                                            ]
                                                                        ).TextValue,
                                                                        new UserIdColumn()
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
                                                        new LiteralString("shipped")
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
                        "inactive_user_lines",
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
                                                "product_id",
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
                                            )
                                        )
                                    ),
                                ],
                                [
                                    new SelectItemGroup(
                                        new SelectItemGroupNonNullable(
                                            new SelectItemGroupUuid(
                                                "product_id",
                                                new UuidGroup(new KeyUuid("product_id"))
                                            )
                                        )
                                    ),
                                    new SelectItemGroup(
                                        new SelectItemGroupNonNullable(
                                            new SelectItemGroupInteger(
                                                "item_lines",
                                                new IntegerGroup(
                                                    new AggregateIntegerGroup(
                                                        new CountGroup()
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
                                                    new UsersTable().Name,
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
                                                                                new OrdersTable().Name,
                                                                            ]
                                                                        ).TextValue,
                                                                        new OrderUserIdColumn()
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
                                                                                new UsersTable().Name,
                                                                            ]
                                                                        ).TextValue,
                                                                        new UserIdColumn()
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
                                    new ComparisonRow(
                                        new EqualRow(
                                            new EqualBooleanRow(
                                                new BooleanNullableRow(
                                                    new BooleanRow(
                                                        new FieldBoolean(
                                                            new JoinedString(
                                                                new DotString(),
                                                                [
                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                    new UsersTable().Name,
                                                                ]
                                                            ).TextValue,
                                                            new UserActiveColumn()
                                                                .Name
                                                                .TextValue
                                                        )
                                                    )
                                                ),
                                                new BooleanNullableRow(
                                                    new BooleanRow(
                                                        new LiteralBoolean(false)
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
                ],
                [
                    new Join(
                        new JoinSubquery(
                            JoinKind.Inner,
                            "sales_performance",
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
                                                                new ProductsTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new ProductIdColumn()
                                                            .Name
                                                            .TextValue
                                                    )
                                                )
                                            ),
                                            new UuidNullableRow(
                                                new UuidRow(
                                                    new FieldUuid(
                                                        "sales_performance",
                                                        "product_id"
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
                            "inactive_user_lines",
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
                                                                new ProductsTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new ProductIdColumn()
                                                            .Name
                                                            .TextValue
                                                    )
                                                )
                                            ),
                                            new UuidNullableRow(
                                                new UuidRow(
                                                    new FieldUuid(
                                                        "inactive_user_lines",
                                                        "product_id"
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
                                    new FieldDecimal("sales_performance", "early_pct")
                                )
                            )
                        ),
                        SortDirection.Desc
                    ),
                    new OrderItemProjection(
                        new ExprProjection(
                            new StringNullableProjection(
                                new StringProjection(
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
                                )
                            )
                        )
                    ),
                ],
                pagination: null,
                distinctBy: null
            )
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
                    new ProductNameColumn(),
                    new ProductInStockColumn(),
                    new Column(new String("item_lines"), new LongColumnType()),
                    new Column(new String("early_pct"), new DoubleColumnType()),
                    new Column(
                        new String("avg_days_after_last_login"),
                        new DoubleColumnType()
                    ),
                    new Column(new String("max_days_since_signup"), new LongColumnType()),
                    new Column(
                        new String("order_total_per_unit"),
                        new DoubleColumnType()
                    ),
                    new Column(new String("inactive_user_lines"), new LongColumnType()),
                    new Column(
                        new String("inactive_per_100_lines"),
                        new DoubleColumnType()
                    ),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new ProductNameColumn(),
                                new InvariantCell(new String("Gadget"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new ProductInStockColumn(),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("item_lines"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("early_pct"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(100))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("avg_days_after_last_login"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.06))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("max_days_since_signup"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1599))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("order_total_per_unit"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(100.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("inactive_user_lines"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("inactive_per_100_lines"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0))
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
                                new ProductNameColumn(),
                                new InvariantCell(new String("Gizmo"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new ProductInStockColumn(),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("item_lines"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("early_pct"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(100))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("avg_days_after_last_login"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1.11))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("max_days_since_signup"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1171))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("order_total_per_unit"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(40))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("inactive_user_lines"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("inactive_per_100_lines"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(100))
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
                                new ProductNameColumn(),
                                new InvariantCell(new String("Widget"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new ProductInStockColumn(),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("item_lines"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("early_pct"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(50))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("avg_days_after_last_login"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(3.03))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("max_days_since_signup"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1792))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("order_total_per_unit"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(80.1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("inactive_user_lines"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("inactive_per_100_lines"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0))
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
