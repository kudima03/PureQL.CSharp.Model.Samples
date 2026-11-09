using Pure.Collections.Generic;
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
using PureQL.CSharp.Model.RowExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Dashboards;

/// <summary>
/// Selects the manager's employee_name as manager, the manager's employee_shift_start as
/// manager_shift_start, the count of rows as direct_reports, the average of the nullable
/// user_score or else 0, rounded to 0 digits, as avg_report_score, unless the count of
/// rows is 0, 100 times the count of rows with user_active divided by the count of rows
/// rounded to 1 digit as active_share_pct, the min of the employee's
/// employee_shift_start as earliest_shift and the min of signup_date as earliest_signup
/// from schema_with_foreign_keys.employees as employee inner joined with
/// schema_with_foreign_keys.employees as manager on the employee's nullable
/// employee_manager_id equal to the manager's employee_id and with
/// schema_with_foreign_keys.users on the employee's employee_user_id equal to user_id,
/// where the employee's employee_id does not equal its employee_manager_id, grouped by
/// manager, manager_shift_start and the manager's employee_id as manager_id, ordered by
/// the count of rows descending and then by manager.
/// </summary>
public sealed record DirectReportsPerManagerQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainGroupedQueryWithoutPagination(
            from: new FromEntity(
                new JoinedString(
                    new DotString(),
                    [
                        new RelationalSchemaWithForeignKeys().Name,
                        new EmployeesTable().Name,
                    ]
                ).TextValue,
                "employee"
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
                            "employee",
                            new EmployeeManagerIdColumn().Name.TextValue
                        ),
                        new FieldUuid("manager", new EmployeeIdColumn().Name.TextValue)
                    ),
                    "manager"
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
                            "employee",
                            new EmployeeUserIdColumn().Name.TextValue
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
            where: new NotEqualUuidRow(
                new FieldUuid("employee", new EmployeeIdColumn().Name.TextValue),
                new FieldUuidNullable(
                    "employee",
                    new EmployeeManagerIdColumn().Name.TextValue
                )
            ),
            groupBy:
            [
                new GroupKey(
                    "manager",
                    new FieldString("manager", new EmployeeNameColumn().Name.TextValue)
                ),
                new GroupKey(
                    "manager_shift_start",
                    new FieldTime(
                        "manager",
                        new EmployeeShiftStartColumn().Name.TextValue
                    )
                ),
                new GroupKey(
                    "manager_id",
                    new FieldUuid("manager", new EmployeeIdColumn().Name.TextValue)
                ),
            ],
            select:
            [
                new SelectItemGroup("manager", new KeyString("manager")),
                new SelectItemGroup(
                    "manager_shift_start",
                    new KeyTime("manager_shift_start")
                ),
                new SelectItemGroup("direct_reports", new CountGroup()),
                new SelectItemGroup(
                    "avg_report_score",
                    new RoundDecimalGroup(
                        new CoalesceDecimalGroup([
                            new AverageDecimalNullableGroup(
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
                            ),
                            new LiteralDecimal(0m),
                        ]),
                        new LiteralInteger(0)
                    )
                ),
                new SelectItemGroup(
                    "active_share_pct",
                    new IfDecimalGroup(
                        new LiteralDecimal(0m),
                        new RoundDecimalGroup(
                            new MultiplyDecimalGroup([
                                new LiteralDecimal(100m),
                                new DivideDecimalGroup([
                                    new CountGroup(
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
                                    new CountGroup(),
                                ]),
                            ]),
                            new LiteralInteger(1)
                        ),
                        new EqualDecimalGroup(new CountGroup(), new LiteralInteger(0))
                    )
                ),
                new SelectItemGroup(
                    "earliest_shift",
                    new MinTimeGroup(
                        new FieldTime(
                            "employee",
                            new EmployeeShiftStartColumn().Name.TextValue
                        )
                    )
                ),
                new SelectItemGroup(
                    "earliest_signup",
                    new MinDateGroup(
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
                    )
                ),
            ],
            orderBy:
            [
                new OrderItemGroup(new CountGroup(), SortDirection.Desc),
                new OrderItemGroup(new KeyString("manager")),
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
                    new Column(new String("manager"), new StringColumnType()),
                    new Column(new String("manager_shift_start"), new TimeColumnType()),
                    new Column(new String("direct_reports"), new LongColumnType()),
                    new Column(new String("avg_report_score"), new DoubleColumnType()),
                    new Column(new String("active_share_pct"), new DoubleColumnType()),
                    new Column(new String("earliest_shift"), new TimeColumnType()),
                    new Column(new String("earliest_signup"), new DateColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("manager"), new StringColumnType()),
                                new InvariantCell(new String("Grace"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("manager_shift_start"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(
                                    new Time(new UShort(9), new UShort(0), new UShort(0))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("direct_reports"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("avg_report_score"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(30))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("active_share_pct"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(50))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("earliest_shift"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(
                                    new Time(new UShort(10), new UShort(0), new UShort(0))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("earliest_signup"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(10),
                                        new UShort(7),
                                        new UShort(2019)
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
                                new Column(new String("manager"), new StringColumnType()),
                                new InvariantCell(new String("Hank"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("manager_shift_start"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(
                                    new Time(new UShort(10), new UShort(0), new UShort(0))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("direct_reports"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("avg_report_score"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("active_share_pct"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(100))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("earliest_shift"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(
                                    new Time(new UShort(8), new UShort(0), new UShort(0))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("earliest_signup"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(5),
                                        new UShort(11),
                                        new UShort(2022)
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
