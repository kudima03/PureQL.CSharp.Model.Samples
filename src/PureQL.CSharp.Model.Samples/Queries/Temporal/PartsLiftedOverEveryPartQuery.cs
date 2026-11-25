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
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.ProjectionExpressions;
using PureQL.CSharp.Model.RowExpressions;
using PureQL.CSharp.Model.Samples.Cells;
using Column = Pure.RelationalSchema.Column.Column;
using DateTime = Pure.Primitives.DateTime.DateTime;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Temporal;

/// <summary>
/// Selects from schema_with_foreign_keys.users left joined to its shipped
/// schema_with_foreign_keys.orders, where datetimeFromParts of the nullable placed_on
/// and 09:00:00 is after last_login (a null placed_on drops the row), the nullable
/// parts lifted into every parts operator: dateFromParts of the user_score rounded to
/// an integer plus 1990, 1 and 1 (year_of_score) and of the year of signup_date, the
/// user_score integer-divided by 10 and 1 (chosen_month), both nullable, dateFromParts
/// of 2025 and the month and day of signup_date (birthday), timeFromParts of 9, 0 and
/// the nullable user_score as the seconds (slot) and of the hour of shift_start, 30 and
/// 45 (call), datetimeFromParts of the nullable placed_on and shift_start (trial_call),
/// of signup_date and shift_start in the zone America/New_York for an active user and
/// Asia/Tokyo otherwise (local_call) and in that zone made null for Asia/Tokyo
/// (chosen_zone_call), ordered descending by dateFromParts of the same year, 12 and 31,
/// then by user_id.
/// </summary>
public sealed record PartsLiftedOverEveryPartQuery
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
            where: new GreaterThanDatetimeRow(
                new DatetimeFromPartsDatetimeNullableRow(
                    new FieldDateNullable(
                        new JoinedString(
                            new DotString(),
                            [
                                new RelationalSchemaWithForeignKeys().Name,
                                new OrdersTable().Name,
                            ]
                        ).TextValue,
                        new PlacedOnColumn().Name.TextValue
                    ),
                    new LiteralTime(new TimeOnly(9, 0, 0))
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
                )
            ),
            select:
            [
                new SelectItemProjection(
                    "year_of_score",
                    new DateFromPartsDateNullableProjection(
                        new AddIntegerNullableProjection([
                            new RoundWithoutDigitsIntegerNullableProjection(
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
                            new LiteralInteger(1990),
                        ]),
                        new LiteralInteger(1),
                        new LiteralInteger(1)
                    )
                ),
                new SelectItemProjection(
                    "chosen_month",
                    new DateFromPartsDateNullableProjection(
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
                        ),
                        new IntegerDivideIntegerNullableProjection(
                            new FieldDecimalNullable(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new UsersTable().Name,
                                    ]
                                ).TextValue,
                                new UserScoreColumn().Name.TextValue
                            ),
                            new LiteralDecimal(10m)
                        ),
                        new LiteralInteger(1)
                    )
                ),
                new SelectItemProjection(
                    "birthday",
                    new DateFromPartsDateProjection(
                        new LiteralInteger(2025),
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
                        ),
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
                    )
                ),
                new SelectItemProjection(
                    "slot",
                    new TimeFromPartsTimeNullableProjection(
                        new LiteralInteger(9),
                        new LiteralInteger(0),
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
                ),
                new SelectItemProjection(
                    "call",
                    new TimeFromPartsTimeProjection(
                        new HourTimeIntegerProjection(
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
                        ),
                        new LiteralInteger(30),
                        new LiteralDecimal(45m)
                    )
                ),
                new SelectItemProjection(
                    "trial_call",
                    new DatetimeFromPartsDatetimeNullableProjection(
                        new FieldDateNullable(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrdersTable().Name,
                                ]
                            ).TextValue,
                            new PlacedOnColumn().Name.TextValue
                        ),
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
                new SelectItemProjection(
                    "local_call",
                    new DatetimeFromPartsDatetimeProjection(
                        new FieldDate(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue,
                            new SignupDateColumn().Name.TextValue
                        ),
                        new FieldTime(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue,
                            new ShiftStartColumn().Name.TextValue
                        ),
                        new IfStringProjection(
                            new LiteralString("America/New_York"),
                            new LiteralString("Asia/Tokyo"),
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
                        )
                    )
                ),
                new SelectItemProjection(
                    "chosen_zone_call",
                    new DatetimeFromPartsDatetimeNullableProjection(
                        new FieldDate(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue,
                            new SignupDateColumn().Name.TextValue
                        ),
                        new FieldTime(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue,
                            new ShiftStartColumn().Name.TextValue
                        ),
                        new NullIfEqualStringNullableProjection(
                            new IfStringProjection(
                                new LiteralString("America/New_York"),
                                new LiteralString("Asia/Tokyo"),
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
                            new LiteralString("Asia/Tokyo")
                        )
                    )
                ),
            ],
            orderBy:
            [
                new OrderItemProjectionExpression(
                    new DateFromPartsDateNullableProjection(
                        new AddIntegerNullableProjection([
                            new RoundWithoutDigitsIntegerNullableProjection(
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
                            new LiteralInteger(1990),
                        ]),
                        new LiteralInteger(12),
                        new LiteralInteger(31)
                    ),
                    SortDirection.Desc
                ),
                new OrderItemProjectionExpression(
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
                    new Column(new String("year_of_score"), new DateColumnType()),
                    new Column(new String("chosen_month"), new DateColumnType()),
                    new Column(new String("birthday"), new DateColumnType()),
                    new Column(new String("slot"), new TimeColumnType()),
                    new Column(new String("call"), new TimeColumnType()),
                    new Column(new String("trial_call"), new DateTimeColumnType()),
                    new Column(new String("local_call"), new DateTimeColumnType()),
                    new Column(new String("chosen_zone_call"), new DateTimeColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("year_of_score"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(1),
                                        new UShort(1),
                                        new UShort(2020)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("chosen_month"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(1),
                                        new UShort(3),
                                        new UShort(2020)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("birthday"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(15),
                                        new UShort(1),
                                        new UShort(2025)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("slot"), new TimeColumnType()),
                                new InvariantCell(
                                    new Time(new UShort(9), new UShort(0), new UShort(30))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("call"), new TimeColumnType()),
                                new InvariantCell(
                                    new Time(
                                        new UShort(9),
                                        new UShort(30),
                                        new UShort(45)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("trial_call"),
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
                                            new UShort(9),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("local_call"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(15),
                                            new UShort(1),
                                            new UShort(2020)
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
                                new Column(
                                    new String("chosen_zone_call"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(15),
                                            new UShort(1),
                                            new UShort(2020)
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
                                new Column(
                                    new String("year_of_score"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(1),
                                        new UShort(1),
                                        new UShort(2020)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("chosen_month"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(1),
                                        new UShort(3),
                                        new UShort(2019)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("birthday"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(10),
                                        new UShort(7),
                                        new UShort(2025)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("slot"), new TimeColumnType()),
                                new InvariantCell(
                                    new Time(new UShort(9), new UShort(0), new UShort(30))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("call"), new TimeColumnType()),
                                new InvariantCell(
                                    new Time(
                                        new UShort(9),
                                        new UShort(30),
                                        new UShort(45)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("trial_call"),
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
                                            new UShort(9),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("local_call"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(10),
                                            new UShort(7),
                                            new UShort(2019)
                                        ),
                                        new Time(
                                            new UShort(13),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("chosen_zone_call"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(10),
                                            new UShort(7),
                                            new UShort(2019)
                                        ),
                                        new Time(
                                            new UShort(13),
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
                                new Column(
                                    new String("year_of_score"),
                                    new DateColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("chosen_month"),
                                    new DateColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("birthday"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(20),
                                        new UShort(3),
                                        new UShort(2025)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("slot"), new TimeColumnType()),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("call"), new TimeColumnType()),
                                new InvariantCell(
                                    new Time(
                                        new UShort(10),
                                        new UShort(30),
                                        new UShort(45)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("trial_call"),
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
                                            new UShort(10),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("local_call"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(20),
                                            new UShort(3),
                                            new UShort(2021)
                                        ),
                                        new Time(
                                            new UShort(1),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("chosen_zone_call"),
                                    new DateTimeColumnType()
                                ),
                                new EmptyCell()
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
