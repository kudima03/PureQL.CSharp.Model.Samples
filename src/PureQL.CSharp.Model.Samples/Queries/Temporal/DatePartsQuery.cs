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
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.ProjectionExpressions;
using PureQL.CSharp.Model.RowExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using Guid = Pure.Primitives.Guid.Guid;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Temporal;

/// <summary>
/// Selects user_id from schema_with_foreign_keys.users left joined to its shipped
/// schema_with_foreign_keys.orders, with the year, quarter, month, day, ISO week and week
/// year of signup_date, the year, week and week year of user_edge_date (a last day of
/// December in week 1 of the next year), the month of the nullable placed_on, the hour of
/// last_login in UTC and the minute of shift_start, where signup_date falls on a Saturday
/// or Sunday (dayOfWeek at least 6), ordered by the dayOfYear of signup_date and
/// last_login.
/// </summary>
public sealed record DatePartsQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithoutPagination(
            from: new FromEntity(
                new JoinedString(
                    new DotString(),
                    [new RelationalSchemaWithForeignKeys().Name, new UsersTable().Name]
                ).TextValue
            ),
            joins:
            [
                new JoinEntity(
                    JoinKind.Left,
                    new JoinedString(
                        new DotString(),
                        [
                            new RelationalSchemaWithForeignKeys().Name,
                            new OrdersTable().Name,
                        ]
                    ).TextValue,
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
                        ),
                        new EqualStringRow(
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
                            new LiteralString("shipped")
                        ),
                    ])
                ),
            ],
            where: new GreaterThanOrEqualDecimalRow(
                new DayOfWeekDateIntegerRow(
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
                new LiteralInteger(6)
            ),
            select:
            [
                new SelectItemProjection(
                    "user_id",
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
                ),
                new SelectItemProjection(
                    "signup_year",
                    new YearDateIntegerProjection(
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
                new SelectItemProjection(
                    "signup_quarter",
                    new QuarterDateIntegerProjection(
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
                new SelectItemProjection(
                    "signup_month",
                    new MonthDateIntegerProjection(
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
                new SelectItemProjection(
                    "signup_day",
                    new DayDateIntegerProjection(
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
                new SelectItemProjection(
                    "signup_week",
                    new WeekDateIntegerProjection(
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
                new SelectItemProjection(
                    "signup_week_year",
                    new WeekYearDateIntegerProjection(
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
                new SelectItemProjection(
                    "edge_year",
                    new YearDateIntegerProjection(
                        new FieldDate(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue,
                            new UserEdgeDateColumn().Name.TextValue
                        )
                    )
                ),
                new SelectItemProjection(
                    "edge_week",
                    new WeekDateIntegerProjection(
                        new FieldDate(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue,
                            new UserEdgeDateColumn().Name.TextValue
                        )
                    )
                ),
                new SelectItemProjection(
                    "edge_week_year",
                    new WeekYearDateIntegerProjection(
                        new FieldDate(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue,
                            new UserEdgeDateColumn().Name.TextValue
                        )
                    )
                ),
                new SelectItemProjection(
                    "shipped_month",
                    new MonthDateIntegerNullableProjection(
                        new FieldDateNullable(
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
                    "last_login_hour_utc",
                    new HourDatetimeIntegerProjection(
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
                    "shift_start_minute",
                    new MinuteTimeIntegerProjection(
                        new FieldTime(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue,
                            new ShiftStartColumn().Name.TextValue
                        )
                    )
                ),
            ],
            orderBy:
            [
                new OrderItemProjectionExpression(
                    new DayOfYearDateIntegerProjection(
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
                new OrderItemProjectionExpression(
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
                    new UserIdColumn(),
                    new Column(new String("signup_year"), new LongColumnType()),
                    new Column(new String("signup_quarter"), new LongColumnType()),
                    new Column(new String("signup_month"), new LongColumnType()),
                    new Column(new String("signup_day"), new LongColumnType()),
                    new Column(new String("signup_week"), new LongColumnType()),
                    new Column(new String("signup_week_year"), new LongColumnType()),
                    new Column(new String("edge_year"), new LongColumnType()),
                    new Column(new String("edge_week"), new LongColumnType()),
                    new Column(new String("edge_week_year"), new LongColumnType()),
                    new Column(new String("shipped_month"), new LongColumnType()),
                    new Column(new String("last_login_hour_utc"), new LongColumnType()),
                    new Column(new String("shift_start_minute"), new LongColumnType()),
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
                                            "00000002-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("signup_year"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2021))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("signup_quarter"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("signup_month"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("signup_day"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(20))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("signup_week"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(11))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("signup_week_year"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2021))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("edge_year"), new LongColumnType()),
                                new InvariantCell(new Long(2024))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("edge_week"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("edge_week_year"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2025))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("shipped_month"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(6))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_login_hour_utc"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(9))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("shift_start_minute"),
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
                                new Column(
                                    new String("signup_year"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2022))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("signup_quarter"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(4))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("signup_month"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(11))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("signup_day"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("signup_week"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(44))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("signup_week_year"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2022))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("edge_year"), new LongColumnType()),
                                new InvariantCell(new Long(2024))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("edge_week"), new LongColumnType()),
                                new InvariantCell(new Long(44))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("edge_week_year"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2024))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("shipped_month"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_login_hour_utc"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(18))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("shift_start_minute"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(30))
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
