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
using PureQL.CSharp.Model.Literals;
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
/// Selects every column of a recursive subquery rows, ordered by level and id. Its seed
/// starts from Grace, the employee 000002bd, of schema_with_foreign_keys.employees inner
/// joined to schema_with_foreign_keys.users on user_id equal to employee_user_id and left
/// joined to audit.logins on login_user_id equal to employee_user_id and login_at before
/// 2024-06-02, and fixes the columns of rows: id (employee_id), parent_id as a null uuid,
/// level 0, weight as the decimal 1, label as a null string, is_root true, since
/// (signup_date), call_time (employee_shift_start) and seen_at (login_at, nullable after
/// the left join). Its next adds every employee whose employee_manager_id is the id of a
/// row of rows, with the same joins but the left join on login_user_id only, and returns
/// values those columns accept: the id of the row as parent_id, level + 1, the floor of
/// weight times 2.5 as weight, employee_name as label, is_root false, since moved forward
/// by level days, the call_time of the row, and seen_at as the coalesce of login_at and
/// the last_login of the user (maxDepth 12).
/// </summary>
public sealed record RecursiveColumnTypesQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithoutPagination(
            subqueries:
            [
                new SubqueryRecursive(
                    "rows",
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
                                new JoinEntity(
                                    JoinKind.Left,
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new AuditRelationalSchema().Name,
                                            new LoginsTable().Name,
                                        ]
                                    ).TextValue,
                                    new AndRow([
                                        new EqualUuidRow(
                                            new FieldUuidNullable(
                                                new JoinedString(
                                                    new DotString(),
                                                    [
                                                        new AuditRelationalSchema().Name,
                                                        new LoginsTable().Name,
                                                    ]
                                                ).TextValue,
                                                new LoginUserIdColumn().Name.TextValue
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
                                        ),
                                        new LessThanDatetimeRow(
                                            new FieldDatetimeNullable(
                                                new JoinedString(
                                                    new DotString(),
                                                    [
                                                        new AuditRelationalSchema().Name,
                                                        new LoginsTable().Name,
                                                    ]
                                                ).TextValue,
                                                new LoginAtColumn().Name.TextValue
                                            ),
                                            new LiteralDatetime(
                                                new DateTimeOffset(
                                                    2024,
                                                    6,
                                                    2,
                                                    0,
                                                    0,
                                                    0,
                                                    TimeSpan.Zero
                                                )
                                            )
                                        ),
                                    ])
                                ),
                            ],
                            where: new EqualUuidRow(
                                new FieldUuid(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new EmployeesTable().Name,
                                        ]
                                    ).TextValue,
                                    new EmployeeIdColumn().Name.TextValue
                                ),
                                new LiteralUuid(new System.Guid(701, 0, 0, new byte[8]))
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
                                new SelectItemProjection(
                                    "parent_id",
                                    new LiteralUuidNullable()
                                ),
                                new SelectItemProjection("level", new LiteralInteger(0)),
                                new SelectItemProjection(
                                    "weight",
                                    new LiteralDecimal(1m)
                                ),
                                new SelectItemProjection(
                                    "label",
                                    new LiteralStringNullable()
                                ),
                                new SelectItemProjection(
                                    "is_root",
                                    new LiteralBoolean(true)
                                ),
                                new SelectItemProjection(
                                    "since",
                                    new FieldDate(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new UsersTable().Name,
                                            ]
                                        ).TextValue,
                                        new SignupDateColumn().Name.TextValue
                                    )
                                ),
                                new SelectItemProjection(
                                    "call_time",
                                    new FieldTime(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new EmployeesTable().Name,
                                            ]
                                        ).TextValue,
                                        new EmployeeShiftStartColumn().Name.TextValue
                                    )
                                ),
                                new SelectItemProjection(
                                    "seen_at",
                                    new FieldDatetimeNullable(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new AuditRelationalSchema().Name,
                                                new LoginsTable().Name,
                                            ]
                                        ).TextValue,
                                        new LoginAtColumn().Name.TextValue
                                    )
                                ),
                            ]
                        ),
                        new PlainNextQuery(
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
                                new JoinSubquery(
                                    JoinKind.Inner,
                                    "rows",
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
                                        new FieldUuid("rows", "id")
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
                                new JoinEntity(
                                    JoinKind.Left,
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new AuditRelationalSchema().Name,
                                            new LoginsTable().Name,
                                        ]
                                    ).TextValue,
                                    new EqualUuidRow(
                                        new FieldUuidNullable(
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new AuditRelationalSchema().Name,
                                                    new LoginsTable().Name,
                                                ]
                                            ).TextValue,
                                            new LoginUserIdColumn().Name.TextValue
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
                            select:
                            [
                                new SelectItemRow(
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
                                new SelectItemRow(
                                    "parent_id",
                                    new FieldUuid("rows", "id")
                                ),
                                new SelectItemRow(
                                    "level",
                                    new AddIntegerRow([
                                        new FieldInteger("rows", "level"),
                                        new LiteralInteger(1),
                                    ])
                                ),
                                new SelectItemRow(
                                    "weight",
                                    new FloorIntegerRow(
                                        new MultiplyDecimalRow([
                                            new FieldDecimal("rows", "weight"),
                                            new LiteralDecimal(2.5m),
                                        ])
                                    )
                                ),
                                new SelectItemRow(
                                    "label",
                                    new FieldString(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new EmployeesTable().Name,
                                            ]
                                        ).TextValue,
                                        new EmployeeNameColumn().Name.TextValue
                                    )
                                ),
                                new SelectItemRow("is_root", new LiteralBoolean(false)),
                                new SelectItemRow(
                                    "since",
                                    new DateAddDaysDateRow(
                                        new FieldDate("rows", "since"),
                                        new FieldInteger("rows", "level")
                                    )
                                ),
                                new SelectItemRow(
                                    "call_time",
                                    new FieldTime("rows", "call_time")
                                ),
                                new SelectItemRow(
                                    "seen_at",
                                    new CoalesceDatetimeRow([
                                        new FieldDatetimeNullable(
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new AuditRelationalSchema().Name,
                                                    new LoginsTable().Name,
                                                ]
                                            ).TextValue,
                                            new LoginAtColumn().Name.TextValue
                                        ),
                                        new FieldDatetime(
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new RelationalSchemaWithForeignKeys().Name,
                                                    new UsersTable().Name,
                                                ]
                                            ).TextValue,
                                            new LastLoginColumn().Name.TextValue
                                        ),
                                    ])
                                ),
                            ]
                        ),
                        12
                    )
                ),
            ],
            from: new FromSubquery("rows"),
            select:
            [
                new SelectItemProjection("id", new FieldUuid("rows", "id")),
                new SelectItemProjection(
                    "parent_id",
                    new FieldUuidNullable("rows", "parent_id")
                ),
                new SelectItemProjection("level", new FieldInteger("rows", "level")),
                new SelectItemProjection("weight", new FieldDecimal("rows", "weight")),
                new SelectItemProjection(
                    "label",
                    new FieldStringNullable("rows", "label")
                ),
                new SelectItemProjection("is_root", new FieldBoolean("rows", "is_root")),
                new SelectItemProjection("since", new FieldDate("rows", "since")),
                new SelectItemProjection("call_time", new FieldTime("rows", "call_time")),
                new SelectItemProjection(
                    "seen_at",
                    new FieldDatetimeNullable("rows", "seen_at")
                ),
            ],
            orderBy:
            [
                new OrderItemProjectionExpression(new FieldInteger("rows", "level")),
                new OrderItemProjectionExpression(new FieldUuid("rows", "id")),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over <see
    /// cref="SchemaDataSetWithForeignKeys"/> and <see cref="AuditSchemaDataSet"/>, in
    /// this order.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("id"), new UuidColumnType()),
                    new Column(new String("parent_id"), new UuidColumnType()),
                    new Column(new String("level"), new LongColumnType()),
                    new Column(new String("weight"), new DoubleColumnType()),
                    new Column(new String("label"), new StringColumnType()),
                    new Column(new String("is_root"), new BoolColumnType()),
                    new Column(new String("since"), new DateColumnType()),
                    new Column(new String("call_time"), new TimeColumnType()),
                    new Column(new String("seen_at"), new DateTimeColumnType()),
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
                                new Column(new String("parent_id"), new UuidColumnType()),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("level"), new LongColumnType()),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("weight"), new DoubleColumnType()),
                                new InvariantCell(new Double(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("label"), new StringColumnType()),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("is_root"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("since"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(15),
                                        new UShort(1),
                                        new UShort(2020)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("call_time"), new TimeColumnType()),
                                new InvariantCell(
                                    new Time(new UShort(9), new UShort(0), new UShort(0))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("seen_at"),
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
                                            new UShort(7),
                                            new UShort(0),
                                            new UShort(0)
                                        )
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
                                new Column(new String("parent_id"), new UuidColumnType()),
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
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("weight"), new DoubleColumnType()),
                                new InvariantCell(new Double(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("label"), new StringColumnType()),
                                new InvariantCell(new String("Hank"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("is_root"), new BoolColumnType()),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("since"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(15),
                                        new UShort(1),
                                        new UShort(2020)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("call_time"), new TimeColumnType()),
                                new InvariantCell(
                                    new Time(new UShort(9), new UShort(0), new UShort(0))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("seen_at"),
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
                                            new UShort(8),
                                            new UShort(0),
                                            new UShort(0)
                                        )
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
                                new Column(new String("parent_id"), new UuidColumnType()),
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
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("weight"), new DoubleColumnType()),
                                new InvariantCell(new Double(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("label"), new StringColumnType()),
                                new InvariantCell(new String("Iris"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("is_root"), new BoolColumnType()),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("since"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(15),
                                        new UShort(1),
                                        new UShort(2020)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("call_time"), new TimeColumnType()),
                                new InvariantCell(
                                    new Time(new UShort(9), new UShort(0), new UShort(0))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("seen_at"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(30),
                                            new UShort(5),
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
                                            "000002c0-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("parent_id"), new UuidColumnType()),
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
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("weight"), new DoubleColumnType()),
                                new InvariantCell(new Double(5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("label"), new StringColumnType()),
                                new InvariantCell(new String("Jack"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("is_root"), new BoolColumnType()),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("since"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(16),
                                        new UShort(1),
                                        new UShort(2020)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("call_time"), new TimeColumnType()),
                                new InvariantCell(
                                    new Time(new UShort(9), new UShort(0), new UShort(0))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("seen_at"),
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
                                            new UShort(18),
                                            new UShort(45),
                                            new UShort(0)
                                        )
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
