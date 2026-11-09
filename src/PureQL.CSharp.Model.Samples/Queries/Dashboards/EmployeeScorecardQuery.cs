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
using PureQL.CSharp.Model.Keys;
using PureQL.CSharp.Model.Lists;
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.ProjectionExpressions;
using PureQL.CSharp.Model.RowExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Dashboards;

/// <summary>
/// Selects rep's employee_name as rep, lead's employee_name as team_lead, user_name,
/// orders, revenue rounded to 0 digits, avg_order_total rounded to 2 digits,
/// tenant_385_share_pct, the subquery's logins or else the integer 0 as logins and
/// revenue divided by the subquery's logins or else the integer 1 rounded to 2 digits
/// as revenue_per_login from schema_with_foreign_keys.employees aliased rep inner
/// joined with schema_with_foreign_keys.employees aliased lead on rep's nullable
/// employee_manager_id equal to lead's employee_id, with
/// schema_with_foreign_keys.users on rep's employee_user_id equal to user_id and with
/// the subqueries revenue_by_user and orders_by_user on rep's employee_user_id equal
/// to their user_id, and left joined with the subquery logins_by_user the same way,
/// ordered by revenue descending.
/// revenue_by_user sums item_qty times product_price of the order items joined with
/// their orders and products per order_user_id, and orders_by_user counts the orders,
/// averages order_total and, if the count is 0, takes 0, else rounds to 1 digit 100
/// times the count of the orders of tenant 00000385-0000-0000-0000-000000000000
/// divided by the count as tenant_385_share_pct per order_user_id, both over the
/// orders whose order_status is not in cancelled and refunded and whose placed_on is
/// on or after 2024-06-01 and before 2024-06-06. logins_by_user counts the logins of
/// audit.logins per login_user_id.
/// </summary>
public sealed record EmployeeScorecardQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithoutPagination(
            subqueries:
            [
                new Subquery(
                    "revenue_by_user",
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
                            new NotRow(
                                new InStringRow(
                                    new FieldString(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new OrdersTable().Name,
                                            ]
                                        ).TextValue,
                                        new OrderStatusColumn().Name.TextValue
                                    ),
                                    new ListLiteralString(["cancelled", "refunded"])
                                )
                            ),
                            new AndRow([
                                new GreaterThanOrEqualDateRow(
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
                                    new LiteralDate(new DateOnly(2024, 6, 1))
                                ),
                                new LessThanDateRow(
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
                                    new LiteralDate(new DateOnly(2024, 6, 6))
                                ),
                            ]),
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
                                "revenue",
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
                new Subquery(
                    "orders_by_user",
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
                        where: new AndRow([
                            new NotRow(
                                new InStringRow(
                                    new FieldString(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new OrdersTable().Name,
                                            ]
                                        ).TextValue,
                                        new OrderStatusColumn().Name.TextValue
                                    ),
                                    new ListLiteralString(["cancelled", "refunded"])
                                )
                            ),
                            new AndRow([
                                new GreaterThanOrEqualDateRow(
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
                                    new LiteralDate(new DateOnly(2024, 6, 1))
                                ),
                                new LessThanDateRow(
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
                                    new LiteralDate(new DateOnly(2024, 6, 6))
                                ),
                            ]),
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
                            new SelectItemGroup("orders", new CountGroup()),
                            new SelectItemGroup(
                                "avg_order_total",
                                new AverageDecimalGroup(
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
                            new SelectItemGroup(
                                "tenant_385_share_pct",
                                new IfDecimalGroup(
                                    new LiteralDecimal(0m),
                                    new RoundDecimalGroup(
                                        new MultiplyDecimalGroup([
                                            new LiteralDecimal(100m),
                                            new DivideDecimalGroup([
                                                new CountGroup(
                                                    new EqualUuidRow(
                                                        new FieldUuid(
                                                            new JoinedString(
                                                                new DotString(),
                                                                [
                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                    new OrdersTable().Name,
                                                                ]
                                                            ).TextValue,
                                                            new OrderTenantIdColumn()
                                                                .Name
                                                                .TextValue
                                                        ),
                                                        new LiteralUuid(
                                                            new Guid(
                                                                901,
                                                                0,
                                                                0,
                                                                new byte[8]
                                                            )
                                                        )
                                                    )
                                                ),
                                                new CountGroup(),
                                            ]),
                                        ]),
                                        new LiteralInteger(1)
                                    ),
                                    new EqualDecimalGroup(
                                        new CountGroup(),
                                        new LiteralInteger(0)
                                    )
                                )
                            ),
                        ]
                    )
                ),
                new Subquery(
                    "logins_by_user",
                    new GroupedQueryWithoutPaginationWithoutOrderBy(
                        from: new FromEntity(
                            new JoinedString(
                                new DotString(),
                                [new AuditRelationalSchema().Name, new LoginsTable().Name]
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
                                            new AuditRelationalSchema().Name,
                                            new LoginsTable().Name,
                                        ]
                                    ).TextValue,
                                    new LoginUserIdColumn().Name.TextValue
                                )
                            ),
                        ],
                        select:
                        [
                            new SelectItemGroup("user_id", new KeyUuid("user_id")),
                            new SelectItemGroup("logins", new CountGroup()),
                        ]
                    )
                ),
            ],
            from: new FromEntity(
                new JoinedString(
                    new DotString(),
                    [
                        new RelationalSchemaWithForeignKeys().Name,
                        new EmployeesTable().Name,
                    ]
                ).TextValue,
                "rep"
            ),
            joins:
            [
                new JoinEntity(
                    JoinKind.Inner,
                    new JoinedString(
                        new DotString(),
                        [
                            new RelationalSchemaWithForeignKeys().Name,
                            new EmployeesTable().Name,
                        ]
                    ).TextValue,
                    new EqualUuidRow(
                        new FieldUuidNullable(
                            "rep",
                            new EmployeeManagerIdColumn().Name.TextValue
                        ),
                        new FieldUuid("lead", new EmployeeIdColumn().Name.TextValue)
                    ),
                    "lead"
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
                        new FieldUuid("rep", new EmployeeUserIdColumn().Name.TextValue),
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
                new JoinSubquery(
                    JoinKind.Inner,
                    "revenue_by_user",
                    new EqualUuidRow(
                        new FieldUuid("rep", new EmployeeUserIdColumn().Name.TextValue),
                        new FieldUuid("revenue_by_user", "user_id")
                    )
                ),
                new JoinSubquery(
                    JoinKind.Inner,
                    "orders_by_user",
                    new EqualUuidRow(
                        new FieldUuid("rep", new EmployeeUserIdColumn().Name.TextValue),
                        new FieldUuid("orders_by_user", "user_id")
                    )
                ),
                new JoinSubquery(
                    JoinKind.Left,
                    "logins_by_user",
                    new EqualUuidRow(
                        new FieldUuid("rep", new EmployeeUserIdColumn().Name.TextValue),
                        new FieldUuid("logins_by_user", "user_id")
                    )
                ),
            ],
            select:
            [
                new SelectItemProjection(
                    "rep",
                    new FieldString("rep", new EmployeeNameColumn().Name.TextValue)
                ),
                new SelectItemProjection(
                    "team_lead",
                    new FieldString("lead", new EmployeeNameColumn().Name.TextValue)
                ),
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
                    "orders",
                    new FieldInteger("orders_by_user", "orders")
                ),
                new SelectItemProjection(
                    "revenue",
                    new RoundDecimalProjection(
                        new FieldDecimal("revenue_by_user", "revenue"),
                        new LiteralInteger(0)
                    )
                ),
                new SelectItemProjection(
                    "avg_order_total",
                    new RoundDecimalProjection(
                        new FieldDecimal("orders_by_user", "avg_order_total"),
                        new LiteralInteger(2)
                    )
                ),
                new SelectItemProjection(
                    "tenant_385_share_pct",
                    new FieldDecimal("orders_by_user", "tenant_385_share_pct")
                ),
                new SelectItemProjection(
                    "logins",
                    new CoalesceIntegerProjection([
                        new FieldIntegerNullable("logins_by_user", "logins"),
                        new LiteralInteger(0),
                    ])
                ),
                new SelectItemProjection(
                    "revenue_per_login",
                    new RoundDecimalProjection(
                        new DivideDecimalProjection([
                            new FieldDecimal("revenue_by_user", "revenue"),
                            new CoalesceIntegerProjection([
                                new FieldIntegerNullable("logins_by_user", "logins"),
                                new LiteralInteger(1),
                            ]),
                        ]),
                        new LiteralInteger(2)
                    )
                ),
            ],
            orderBy:
            [
                new OrderItemProjection(
                    new FieldDecimal("revenue_by_user", "revenue"),
                    SortDirection.Desc
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
                    new Column(new String("rep"), new StringColumnType()),
                    new Column(new String("team_lead"), new StringColumnType()),
                    new UserNameColumn(),
                    new Column(new String("orders"), new LongColumnType()),
                    new Column(new String("revenue"), new DoubleColumnType()),
                    new Column(new String("avg_order_total"), new DoubleColumnType()),
                    new Column(
                        new String("tenant_385_share_pct"),
                        new DoubleColumnType()
                    ),
                    new Column(new String("logins"), new LongColumnType()),
                    new Column(new String("revenue_per_login"), new DoubleColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("rep"), new StringColumnType()),
                                new InvariantCell(new String("Iris"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("team_lead"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Grace"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new UserNameColumn(),
                                new InvariantCell(new String("Cara"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("revenue"), new DoubleColumnType()),
                                new InvariantCell(new Double(30))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("avg_order_total"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(300))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("tenant_385_share_pct"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("logins"), new LongColumnType()),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("revenue_per_login"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(29.97))
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
                                new Column(new String("rep"), new StringColumnType()),
                                new InvariantCell(new String("Hank"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("team_lead"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Grace"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new UserNameColumn(),
                                new InvariantCell(new String("Bob"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("revenue"), new DoubleColumnType()),
                                new InvariantCell(new Double(23))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("avg_order_total"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(200))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("tenant_385_share_pct"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(100))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("logins"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("revenue_per_login"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(22.5))
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
