using Pure.Collections.Generic;
using Pure.Primitives.Bool;
using Pure.Primitives.Date;
using Pure.Primitives.Number;
using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.Primitives.Time;
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
using PureQL.CSharp.Model.Samples.Cells;
using Column = Pure.RelationalSchema.Column.Column;
using DateTime = Pure.Primitives.DateTime.DateTime;
using Double = Pure.Primitives.Number.Double;
using Guid = Pure.Primitives.Guid.Guid;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Recursive;

/// <summary>
/// Selects every step of a walk down the employee hierarchy: a recursive subquery walk is
/// seeded with the employee_id of the employee of schema_with_foreign_keys.employees
/// without an employee_manager_id, joined to schema_with_foreign_keys.users on user_id
/// equal to employee_user_id, at level 0 with a spent of 0, a share of 1, no orders, the
/// last_login as latest, vip false and no statuses; its grouped next level reads walk
/// inner joined with schema_with_foreign_keys.employees on employee_manager_id equal to
/// id, left joined with schema_with_foreign_keys.orders on order_user_id equal to
/// employee_user_id and with schema_with_foreign_keys.users on user_id equal to
/// employee_user_id, where level is below 5, grouped by employee_id and level plus 1, and
/// keeps the groups with more than one row or with any order_status equal to "shipped".
/// Each group has the sum of order_total as spent, that sum divided by the sum over all
/// rows of the step (nullIfEqual 0) as share, the countDistinct of order_id as orders,
/// the max of placed_at as latest, whether all user_active is true as vip, and the
/// distinct order_status values in order joined with ", " by concatAll as statuses
/// (maxDepth 6). The main query selects id, level, spent, share, orders, latest, vip and
/// statuses from walk.
/// </summary>
public sealed record HierarchyWalkWithAggregatedStepsQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithoutPagination(
            subqueries:
            [
                new SubqueryRecursive(
                    "walk",
                    new Recursion(
                        new PlainQueryWithoutPaginationWithoutOrderBy(
                            from: new FromEntity(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new EmployeesTable().Name,
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
                                            new UsersTable().Name,
                                        ]
                                    ).TextValue,
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
                                        new FieldUuid(
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new RelationalSchemaWithForeignKeys().Name,
                                                    new EmployeesTable().Name,
                                                ]
                                            ).TextValue,
                                            new EmployeeUserIdColumn().Name.TextValue
                                        )
                                    )
                                ),
                            ],
                            where: new EqualUuidRow(
                                new FieldUuidNullable(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new EmployeesTable().Name,
                                        ]
                                    ).TextValue,
                                    new EmployeeManagerIdColumn().Name.TextValue
                                ),
                                new LiteralUuidNullable()
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
                                                new EmployeesTable().Name,
                                            ]
                                        ).TextValue,
                                        new EmployeeIdColumn().Name.TextValue
                                    )
                                ),
                                new SelectItemProjection("level", new LiteralInteger(0)),
                                new SelectItemProjection("spent", new LiteralDecimal(0m)),
                                new SelectItemProjection(
                                    "share",
                                    new AsNullableDecimalNullableProjection(
                                        new LiteralDecimal(1m)
                                    )
                                ),
                                new SelectItemProjection("orders", new LiteralInteger(0)),
                                new SelectItemProjection(
                                    "latest",
                                    new AsNullableDatetimeNullableProjection(
                                        new FieldDatetime(
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new RelationalSchemaWithForeignKeys().Name,
                                                    new UsersTable().Name,
                                                ]
                                            ).TextValue,
                                            new LastLoginColumn().Name.TextValue
                                        )
                                    )
                                ),
                                new SelectItemProjection(
                                    "vip",
                                    new LiteralBoolean(false)
                                ),
                                new SelectItemProjection(
                                    "statuses",
                                    new LiteralString("")
                                ),
                            ]
                        ),
                        new GroupedNextQuery(
                            from: new FromSubquery("walk"),
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
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new RelationalSchemaWithForeignKeys().Name,
                                                    new EmployeesTable().Name,
                                                ]
                                            ).TextValue,
                                            new EmployeeManagerIdColumn().Name.TextValue
                                        ),
                                        new FieldUuid("walk", "id")
                                    )
                                ),
                                new JoinEntity(
                                    JoinKind.Left,
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
                                                    new EmployeesTable().Name,
                                                ]
                                            ).TextValue,
                                            new EmployeeUserIdColumn().Name.TextValue
                                        )
                                    )
                                ),
                                new JoinEntity(
                                    JoinKind.Left,
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
                                                    new UsersTable().Name,
                                                ]
                                            ).TextValue,
                                            new UserIdColumn().Name.TextValue
                                        ),
                                        new FieldUuid(
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new RelationalSchemaWithForeignKeys().Name,
                                                    new EmployeesTable().Name,
                                                ]
                                            ).TextValue,
                                            new EmployeeUserIdColumn().Name.TextValue
                                        )
                                    )
                                ),
                            ],
                            where: new LessThanDecimalRow(
                                new FieldInteger("walk", "level"),
                                new LiteralInteger(5)
                            ),
                            groupBy:
                            [
                                new GroupKey(
                                    "id",
                                    new FieldUuid(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new EmployeesTable().Name,
                                            ]
                                        ).TextValue,
                                        new EmployeeIdColumn().Name.TextValue
                                    )
                                ),
                                new GroupKey(
                                    "level",
                                    new AddIntegerRow([
                                        new FieldInteger("walk", "level"),
                                        new LiteralInteger(1),
                                    ])
                                ),
                            ],
                            having: new OrGroup([
                                new GreaterThanDecimalGroup(
                                    new CountGroup(),
                                    new LiteralInteger(1)
                                ),
                                new AnyGroup(
                                    new EqualStringRow(
                                        new FieldStringNullable(
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new RelationalSchemaWithForeignKeys().Name,
                                                    new OrdersTable().Name,
                                                ]
                                            ).TextValue,
                                            new OrderStatusColumn().Name.TextValue
                                        ),
                                        new LiteralString("shipped")
                                    )
                                ),
                            ]),
                            select:
                            [
                                new SelectItemGroup("id", new KeyUuid("id")),
                                new SelectItemGroup("level", new KeyInteger("level")),
                                new SelectItemGroup(
                                    "spent",
                                    new SumDecimalGroup(
                                        new FieldDecimalNullable(
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
                                    "share",
                                    new DivideDecimalNullableGroup([
                                        new SumDecimalGroup(
                                            new FieldDecimalNullable(
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
                                        new NullIfEqualDecimalNullableGroup(
                                            new SumDecimalGroup(
                                                new FieldDecimalNullable(
                                                    new JoinedString(
                                                        new DotString(),
                                                        [
                                                            new RelationalSchemaWithForeignKeys().Name,
                                                            new OrdersTable().Name,
                                                        ]
                                                    ).TextValue,
                                                    new OrderTotalColumn().Name.TextValue
                                                ),
                                                over: AggregateOver.All
                                            ),
                                            new LiteralInteger(0)
                                        ),
                                    ])
                                ),
                                new SelectItemGroup(
                                    "orders",
                                    new CountDistinctGroup(
                                        new FieldUuidNullable(
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
                                new SelectItemGroup(
                                    "latest",
                                    new MaxDatetimeNullableGroup(
                                        new FieldDatetimeNullable(
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new RelationalSchemaWithForeignKeys().Name,
                                                    new OrdersTable().Name,
                                                ]
                                            ).TextValue,
                                            new PlacedAtColumn().Name.TextValue
                                        )
                                    )
                                ),
                                new SelectItemGroup(
                                    "vip",
                                    new AllGroup(
                                        new CoalesceBooleanRow([
                                            new FieldBooleanNullable(
                                                new JoinedString(
                                                    new DotString(),
                                                    [
                                                        new RelationalSchemaWithForeignKeys().Name,
                                                        new UsersTable().Name,
                                                    ]
                                                ).TextValue,
                                                new UserActiveColumn().Name.TextValue
                                            ),
                                            new LiteralBoolean(false),
                                        ])
                                    )
                                ),
                                new SelectItemGroup(
                                    "statuses",
                                    new ConcatAllGroup(
                                        new FieldStringNullable(
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new RelationalSchemaWithForeignKeys().Name,
                                                    new OrdersTable().Name,
                                                ]
                                            ).TextValue,
                                            new OrderStatusColumn().Name.TextValue
                                        ),
                                        new LiteralString(", "),
                                        [
                                            new OrderItemRow(
                                                new FieldStringNullable(
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
                                        ],
                                        distinct: true
                                    )
                                ),
                            ]
                        ),
                        6
                    )
                ),
            ],
            from: new FromSubquery("walk"),
            select:
            [
                new SelectItemProjection("id", new FieldUuid("walk", "id")),
                new SelectItemProjection("level", new FieldInteger("walk", "level")),
                new SelectItemProjection("spent", new FieldDecimal("walk", "spent")),
                new SelectItemProjection(
                    "share",
                    new FieldDecimalNullable("walk", "share")
                ),
                new SelectItemProjection("orders", new FieldInteger("walk", "orders")),
                new SelectItemProjection(
                    "latest",
                    new FieldDatetimeNullable("walk", "latest")
                ),
                new SelectItemProjection("vip", new FieldBoolean("walk", "vip")),
                new SelectItemProjection("statuses", new FieldString("walk", "statuses")),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and
    /// <see cref="AuditSchemaDataSet"/>, in no particular order.
    /// The shares are exact quotients rounded as the specification rounds a decimal.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("id"), new UuidColumnType()),
                    new Column(new String("level"), new LongColumnType()),
                    new Column(new String("spent"), new DoubleColumnType()),
                    new Column(new String("share"), new DoubleColumnType()),
                    new Column(new String("orders"), new LongColumnType()),
                    new Column(new String("latest"), new DateTimeColumnType()),
                    new Column(new String("vip"), new BoolColumnType()),
                    new Column(new String("statuses"), new StringColumnType()),
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
                                            "000002bd-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("level"), new LongColumnType()),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("spent"), new DoubleColumnType()),
                                new InvariantCell(new Double(0.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("share"), new DoubleColumnType()),
                                new InvariantCell(new Double(1.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("latest"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(1),
                                            new UShort(6),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(8),
                                            new UShort(30),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("vip"), new BoolColumnType()),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("statuses"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String(""))
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
                                            "000002be-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("level"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("spent"), new DoubleColumnType()),
                                new InvariantCell(new Double(200.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("share"), new DoubleColumnType()),
                                new InvariantCell(new Double(0.3476749239461104))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("latest"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(3),
                                            new UShort(6),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(12),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("vip"), new BoolColumnType()),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("statuses"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("shipped"))
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
                                            "000002bf-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("level"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("spent"), new DoubleColumnType()),
                                new InvariantCell(new Double(375.25))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("share"), new DoubleColumnType()),
                                new InvariantCell(new Double(0.6523250760538896))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("latest"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(5),
                                            new UShort(6),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(14),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("vip"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("statuses"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("cancelled, shipped"))
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
