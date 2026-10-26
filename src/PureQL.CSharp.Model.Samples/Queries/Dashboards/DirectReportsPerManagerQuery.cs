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
using PureQL.CSharp.Model.GroupKeys;
using PureQL.CSharp.Model.Keys;
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.RowExpressions;
using PureQL.CSharp.Model.SelectItems;
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
        new PureQLQuery(
            new MainGroupedQuery(
                new From(
                    new FromEntity(
                        new JoinedString(
                            new DotString(),
                            [
                                new RelationalSchemaWithForeignKeys().Name,
                                new EmployeesTable().Name,
                            ]
                        ).TextValue,
                        "employee"
                    )
                ),
                [
                    new GroupKey(
                        new GroupKeyNonNullable(
                            new GroupKeyString(
                                "manager",
                                new StringRow(
                                    new FieldString(
                                        "manager",
                                        new EmployeeNameColumn().Name.TextValue
                                    )
                                )
                            )
                        )
                    ),
                    new GroupKey(
                        new GroupKeyNonNullable(
                            new GroupKeyTime(
                                "manager_shift_start",
                                new TimeRow(
                                    new FieldTime(
                                        "manager",
                                        new EmployeeShiftStartColumn().Name.TextValue
                                    )
                                )
                            )
                        )
                    ),
                    new GroupKey(
                        new GroupKeyNonNullable(
                            new GroupKeyUuid(
                                "manager_id",
                                new UuidRow(
                                    new FieldUuid(
                                        "manager",
                                        new EmployeeIdColumn().Name.TextValue
                                    )
                                )
                            )
                        )
                    ),
                ],
                [
                    new SelectItemGroup(
                        new SelectItemGroupNonNullable(
                            new SelectItemGroupString(
                                "manager",
                                new StringGroup(new KeyString("manager"))
                            )
                        )
                    ),
                    new SelectItemGroup(
                        new SelectItemGroupNonNullable(
                            new SelectItemGroupTime(
                                "manager_shift_start",
                                new TimeGroup(new KeyTime("manager_shift_start"))
                            )
                        )
                    ),
                    new SelectItemGroup(
                        new SelectItemGroupNonNullable(
                            new SelectItemGroupInteger(
                                "direct_reports",
                                new IntegerGroup(
                                    new AggregateIntegerGroup(new CountGroup())
                                )
                            )
                        )
                    ),
                    new SelectItemGroup(
                        new SelectItemGroupNonNullable(
                            new SelectItemGroupDecimal(
                                "avg_report_score",
                                new ExactDecimalGroup(
                                    new RoundDecimalGroup(
                                        new DecimalGroup(
                                            new ExactDecimalGroup(
                                                new ConditionalDecimalGroup(
                                                    new CoalesceDecimalGroup([
                                                        new DecimalNullableGroup(
                                                            new ExactDecimalNullableGroup(
                                                                new AggregateDecimalNullableGroup(
                                                                    new AverageDecimalNullableGroup(
                                                                        new DecimalNullableRow(
                                                                            new ExactDecimalNullableRow(
                                                                                new FieldDecimalNullable(
                                                                                    new JoinedString(
                                                                                        new DotString(),
                                                                                        [
                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                            new UsersTable().Name,
                                                                                        ]
                                                                                    ).TextValue,
                                                                                    new UserScoreColumn()
                                                                                        .Name
                                                                                        .TextValue
                                                                                )
                                                                            )
                                                                        )
                                                                    )
                                                                )
                                                            )
                                                        ),
                                                        new DecimalNullableGroup(
                                                            new ExactDecimalGroup(
                                                                new LiteralDecimal(0m)
                                                            )
                                                        ),
                                                    ])
                                                )
                                            )
                                        ),
                                        new IntegerGroup(new LiteralInteger(0))
                                    )
                                )
                            )
                        )
                    ),
                    new SelectItemGroup(
                        new SelectItemGroupNonNullable(
                            new SelectItemGroupDecimal(
                                "active_share_pct",
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
                                                                    new LiteralInteger(0)
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
                    new SelectItemGroup(
                        new SelectItemGroupNonNullable(
                            new SelectItemGroupTime(
                                "earliest_shift",
                                new TimeGroup(
                                    new AggregateTimeGroup(
                                        new MinTimeGroup(
                                            new TimeRow(
                                                new FieldTime(
                                                    "employee",
                                                    new EmployeeShiftStartColumn()
                                                        .Name
                                                        .TextValue
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
                            new SelectItemGroupDate(
                                "earliest_signup",
                                new DateGroup(
                                    new AggregateDateGroup(
                                        new MinDateGroup(
                                            new DateRow(
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
                                        )
                                    )
                                )
                            )
                        )
                    ),
                ],
                subqueries: null,
                [
                    new Join(
                        new JoinEntity(
                            JoinKind.Inner,
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new EmployeesTable().Name,
                                ]
                            ).TextValue,
                            new BooleanRow(
                                new ComparisonRow(
                                    new EqualRow(
                                        new EqualUuidRow(
                                            new UuidNullableRow(
                                                new ExactUuidNullableRow(
                                                    new FieldUuidNullable(
                                                        "employee",
                                                        new EmployeeManagerIdColumn()
                                                            .Name
                                                            .TextValue
                                                    )
                                                )
                                            ),
                                            new UuidNullableRow(
                                                new UuidRow(
                                                    new FieldUuid(
                                                        "manager",
                                                        new EmployeeIdColumn()
                                                            .Name
                                                            .TextValue
                                                    )
                                                )
                                            )
                                        )
                                    )
                                )
                            ),
                            "manager"
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
                                                        "employee",
                                                        new EmployeeUserIdColumn()
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
                                                        new UserIdColumn().Name.TextValue
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
                        new NotEqualRow(
                            new NotEqualUuidRow(
                                new UuidNullableRow(
                                    new UuidRow(
                                        new FieldUuid(
                                            "employee",
                                            new EmployeeIdColumn().Name.TextValue
                                        )
                                    )
                                ),
                                new UuidNullableRow(
                                    new ExactUuidNullableRow(
                                        new FieldUuidNullable(
                                            "employee",
                                            new EmployeeManagerIdColumn().Name.TextValue
                                        )
                                    )
                                )
                            )
                        )
                    )
                ),
                having: null,
                [
                    new OrderItemGroup(
                        new ExprGroup(
                            new DecimalNullableGroup(
                                new IntegerGroup(
                                    new AggregateIntegerGroup(new CountGroup())
                                )
                            )
                        ),
                        SortDirection.Desc
                    ),
                    new OrderItemGroup(
                        new ExprGroup(
                            new StringNullableGroup(
                                new StringGroup(new KeyString("manager"))
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
