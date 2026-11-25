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
using PureQL.CSharp.Model.Samples.Cells;
using Column = Pure.RelationalSchema.Column.Column;
using DateTime = Pure.Primitives.DateTime.DateTime;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.TimeZones;

/// <summary>
/// Selects from a subquery members, which gives each user of
/// schema_with_foreign_keys.users the zone America/New_York for tenant #901 and
/// Asia/Tokyo otherwise, with created_at, last_login_at, optional_zone, birth_date and
/// preferred_call_time as in LocalPartKindsQuery, every startOf that takes a zone: the
/// starts of the year, quarter, month, week, day, hour and minute of created_at in
/// Europe/Moscow, the start of the day in Pacific/Auckland, of the week in the zone
/// column, of the month in a computed zone, of the year in a nullable zone column, of the
/// hour in a null zone, of the quarter of a nullable last_login_at, of the minute of a
/// nullable last_login_at in a nullable zone, of the hour of a datetime literal in
/// Asia/Kathmandu, and the startOfSecond of created_at, the start of the month of
/// birth_date, the start of the hour of preferred_call_time, the local date of the start
/// of the local month and the start of the local day of that start.
/// </summary>
public sealed record LocalPeriodStartKindsQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithoutPagination(
            subqueries:
            [
                new SubqueryQuery(
                    "members",
                    new PlainQueryWithoutPaginationWithoutOrderBy(
                        from: new FromEntity(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue
                        ),
                        select:
                        [
                            new SelectItemProjection(
                                "created_at",
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
                                    )
                                )
                            ),
                            new SelectItemProjection(
                                "last_login_at",
                                new IfDatetimeNullableProjection(
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
                                    new LiteralDatetimeNullable(),
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
                            ),
                            new SelectItemProjection(
                                "time_zone",
                                new IfStringProjection(
                                    new LiteralString("America/New_York"),
                                    new LiteralString("Asia/Tokyo"),
                                    new EqualUuidProjection(
                                        new FieldUuid(
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new RelationalSchemaWithForeignKeys().Name,
                                                    new UsersTable().Name,
                                                ]
                                            ).TextValue,
                                            new UserTenantIdColumn().Name.TextValue
                                        ),
                                        new LiteralUuid(new Guid(901, 0, 0, new byte[8]))
                                    )
                                )
                            ),
                            new SelectItemProjection(
                                "optional_zone",
                                new IfStringNullableProjection(
                                    new LiteralString("Pacific/Auckland"),
                                    new LiteralStringNullable(),
                                    new EqualUuidProjection(
                                        new FieldUuid(
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new RelationalSchemaWithForeignKeys().Name,
                                                    new UsersTable().Name,
                                                ]
                                            ).TextValue,
                                            new UserTenantIdColumn().Name.TextValue
                                        ),
                                        new LiteralUuid(new Guid(901, 0, 0, new byte[8]))
                                    )
                                )
                            ),
                            new SelectItemProjection(
                                "birth_date",
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
                                "preferred_call_time",
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
                        ]
                    )
                ),
            ],
            from: new FromSubquery("members"),
            select:
            [
                new SelectItemProjection(
                    "start_of_year_in_a_literal_zone",
                    new StartOfYearDatetimeProjection(
                        new FieldDatetime("members", "created_at"),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemProjection(
                    "start_of_quarter_in_a_literal_zone",
                    new StartOfQuarterDatetimeProjection(
                        new FieldDatetime("members", "created_at"),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemProjection(
                    "start_of_month_in_a_literal_zone",
                    new StartOfMonthDatetimeProjection(
                        new FieldDatetime("members", "created_at"),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemProjection(
                    "start_of_week_in_a_literal_zone",
                    new StartOfWeekDatetimeProjection(
                        new FieldDatetime("members", "created_at"),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemProjection(
                    "start_of_day_in_a_literal_zone",
                    new StartOfDayDatetimeProjection(
                        new FieldDatetime("members", "created_at"),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemProjection(
                    "start_of_hour_in_a_literal_zone",
                    new StartOfHourDatetimeProjection(
                        new FieldDatetime("members", "created_at"),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemProjection(
                    "start_of_minute_in_a_literal_zone",
                    new StartOfMinuteDatetimeProjection(
                        new FieldDatetime("members", "created_at"),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemProjection(
                    "other_literal_zone",
                    new StartOfDayDatetimeProjection(
                        new FieldDatetime("members", "created_at"),
                        zone: new LiteralString("Pacific/Auckland")
                    )
                ),
                new SelectItemProjection(
                    "column_zone",
                    new StartOfWeekDatetimeProjection(
                        new FieldDatetime("members", "created_at"),
                        zone: new FieldString("members", "time_zone")
                    )
                ),
                new SelectItemProjection(
                    "computed_zone",
                    new StartOfMonthDatetimeProjection(
                        new FieldDatetime("members", "created_at"),
                        zone: new CoalesceStringProjection([
                            new FieldStringNullable("members", "optional_zone"),
                            new FieldString("members", "time_zone"),
                        ])
                    )
                ),
                new SelectItemProjection(
                    "nullable_zone",
                    new StartOfYearDatetimeNullableProjection(
                        new FieldDatetime("members", "created_at"),
                        zone: new FieldStringNullable("members", "optional_zone")
                    )
                ),
                new SelectItemProjection(
                    "null_zone",
                    new StartOfHourDatetimeNullableProjection(
                        new FieldDatetime("members", "created_at"),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemProjection(
                    "nullable_operand",
                    new StartOfQuarterDatetimeNullableProjection(
                        new FieldDatetimeNullable("members", "last_login_at"),
                        zone: new FieldString("members", "time_zone")
                    )
                ),
                new SelectItemProjection(
                    "nullable_operand_and_zone",
                    new StartOfMinuteDatetimeNullableProjection(
                        new FieldDatetimeNullable("members", "last_login_at"),
                        zone: new FieldStringNullable("members", "optional_zone")
                    )
                ),
                new SelectItemProjection(
                    "literal_operand",
                    new StartOfHourDatetimeProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(
                                2024,
                                8,
                                15,
                                18,
                                30,
                                15,
                                250,
                                TimeSpan.Zero
                            )
                        ),
                        zone: new LiteralString("Asia/Kathmandu")
                    )
                ),
                new SelectItemProjection(
                    "second_needs_no_zone",
                    new StartOfSecondDatetimeProjection(
                        new FieldDatetime("members", "created_at")
                    )
                ),
                new SelectItemProjection(
                    "of_a_date",
                    new StartOfMonthDateProjection(new FieldDate("members", "birth_date"))
                ),
                new SelectItemProjection(
                    "of_a_time",
                    new StartOfHourTimeProjection(
                        new FieldTime("members", "preferred_call_time")
                    )
                ),
                new SelectItemProjection(
                    "local_day_of_the_start",
                    new AsDateDatetimeDateProjection(
                        new StartOfMonthDatetimeProjection(
                            new FieldDatetime("members", "created_at"),
                            zone: new FieldString("members", "time_zone")
                        ),
                        zone: new FieldString("members", "time_zone")
                    )
                ),
                new SelectItemProjection(
                    "nested",
                    new StartOfDayDatetimeProjection(
                        new StartOfMonthDatetimeProjection(
                            new FieldDatetime("members", "created_at"),
                            zone: new FieldString("members", "time_zone")
                        ),
                        zone: new FieldString("members", "time_zone")
                    )
                ),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and
    /// <see cref="AuditSchemaDataSet"/>, in no particular order.
    /// Local times are read with the tz database of PostgreSQL 17 and checked with
    /// Python zoneinfo; no value falls in a skipped or repeated local hour.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(
                        new String("start_of_year_in_a_literal_zone"),
                        new DateTimeColumnType()
                    ),
                    new Column(
                        new String("start_of_quarter_in_a_literal_zone"),
                        new DateTimeColumnType()
                    ),
                    new Column(
                        new String("start_of_month_in_a_literal_zone"),
                        new DateTimeColumnType()
                    ),
                    new Column(
                        new String("start_of_week_in_a_literal_zone"),
                        new DateTimeColumnType()
                    ),
                    new Column(
                        new String("start_of_day_in_a_literal_zone"),
                        new DateTimeColumnType()
                    ),
                    new Column(
                        new String("start_of_hour_in_a_literal_zone"),
                        new DateTimeColumnType()
                    ),
                    new Column(
                        new String("start_of_minute_in_a_literal_zone"),
                        new DateTimeColumnType()
                    ),
                    new Column(
                        new String("other_literal_zone"),
                        new DateTimeColumnType()
                    ),
                    new Column(new String("column_zone"), new DateTimeColumnType()),
                    new Column(new String("computed_zone"), new DateTimeColumnType()),
                    new Column(new String("nullable_zone"), new DateTimeColumnType()),
                    new Column(new String("null_zone"), new DateTimeColumnType()),
                    new Column(new String("nullable_operand"), new DateTimeColumnType()),
                    new Column(
                        new String("nullable_operand_and_zone"),
                        new DateTimeColumnType()
                    ),
                    new Column(new String("literal_operand"), new DateTimeColumnType()),
                    new Column(
                        new String("second_needs_no_zone"),
                        new DateTimeColumnType()
                    ),
                    new Column(new String("of_a_date"), new DateColumnType()),
                    new Column(new String("of_a_time"), new TimeColumnType()),
                    new Column(
                        new String("local_day_of_the_start"),
                        new DateColumnType()
                    ),
                    new Column(new String("nested"), new DateTimeColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("start_of_year_in_a_literal_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(31),
                                            new UShort(12),
                                            new UShort(2018)
                                        ),
                                        new Time(
                                            new UShort(21),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("start_of_quarter_in_a_literal_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(30),
                                            new UShort(6),
                                            new UShort(2019)
                                        ),
                                        new Time(
                                            new UShort(21),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("start_of_month_in_a_literal_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(30),
                                            new UShort(6),
                                            new UShort(2019)
                                        ),
                                        new Time(
                                            new UShort(21),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("start_of_week_in_a_literal_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(7),
                                            new UShort(7),
                                            new UShort(2019)
                                        ),
                                        new Time(
                                            new UShort(21),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("start_of_day_in_a_literal_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(9),
                                            new UShort(7),
                                            new UShort(2019)
                                        ),
                                        new Time(
                                            new UShort(21),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("start_of_hour_in_a_literal_zone"),
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
                                            new UShort(9),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("start_of_minute_in_a_literal_zone"),
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
                                            new UShort(9),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("other_literal_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(9),
                                            new UShort(7),
                                            new UShort(2019)
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
                                new Column(
                                    new String("column_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(8),
                                            new UShort(7),
                                            new UShort(2019)
                                        ),
                                        new Time(
                                            new UShort(4),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("computed_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(30),
                                            new UShort(6),
                                            new UShort(2019)
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
                                new Column(
                                    new String("nullable_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(31),
                                            new UShort(12),
                                            new UShort(2018)
                                        ),
                                        new Time(
                                            new UShort(11),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("null_zone"),
                                    new DateTimeColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_operand"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(1),
                                            new UShort(4),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(4),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_operand_and_zone"),
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
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("literal_operand"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(15),
                                            new UShort(8),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(18),
                                            new UShort(15),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("second_needs_no_zone"),
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
                                            new UShort(9),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("of_a_date"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(1),
                                        new UShort(7),
                                        new UShort(2019)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("of_a_time"), new TimeColumnType()),
                                new InvariantCell(
                                    new Time(new UShort(9), new UShort(0), new UShort(0))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("local_day_of_the_start"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(1),
                                        new UShort(7),
                                        new UShort(2019)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nested"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(1),
                                            new UShort(7),
                                            new UShort(2019)
                                        ),
                                        new Time(
                                            new UShort(4),
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
                                    new String("start_of_year_in_a_literal_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(31),
                                            new UShort(12),
                                            new UShort(2019)
                                        ),
                                        new Time(
                                            new UShort(21),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("start_of_quarter_in_a_literal_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(31),
                                            new UShort(12),
                                            new UShort(2019)
                                        ),
                                        new Time(
                                            new UShort(21),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("start_of_month_in_a_literal_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(31),
                                            new UShort(12),
                                            new UShort(2019)
                                        ),
                                        new Time(
                                            new UShort(21),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("start_of_week_in_a_literal_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(12),
                                            new UShort(1),
                                            new UShort(2020)
                                        ),
                                        new Time(
                                            new UShort(21),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("start_of_day_in_a_literal_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(14),
                                            new UShort(1),
                                            new UShort(2020)
                                        ),
                                        new Time(
                                            new UShort(21),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("start_of_hour_in_a_literal_zone"),
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
                                            new UShort(9),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("start_of_minute_in_a_literal_zone"),
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
                                            new UShort(9),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("other_literal_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(14),
                                            new UShort(1),
                                            new UShort(2020)
                                        ),
                                        new Time(
                                            new UShort(11),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("column_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(12),
                                            new UShort(1),
                                            new UShort(2020)
                                        ),
                                        new Time(
                                            new UShort(15),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("computed_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(31),
                                            new UShort(12),
                                            new UShort(2019)
                                        ),
                                        new Time(
                                            new UShort(15),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_zone"),
                                    new DateTimeColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("null_zone"),
                                    new DateTimeColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_operand"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(31),
                                            new UShort(3),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(15),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_operand_and_zone"),
                                    new DateTimeColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("literal_operand"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(15),
                                            new UShort(8),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(18),
                                            new UShort(15),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("second_needs_no_zone"),
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
                                            new UShort(9),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("of_a_date"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(1),
                                        new UShort(1),
                                        new UShort(2020)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("of_a_time"), new TimeColumnType()),
                                new InvariantCell(
                                    new Time(new UShort(9), new UShort(0), new UShort(0))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("local_day_of_the_start"),
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
                                    new String("nested"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(31),
                                            new UShort(12),
                                            new UShort(2019)
                                        ),
                                        new Time(
                                            new UShort(15),
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
                                    new String("start_of_year_in_a_literal_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(31),
                                            new UShort(12),
                                            new UShort(2019)
                                        ),
                                        new Time(
                                            new UShort(21),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("start_of_quarter_in_a_literal_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(31),
                                            new UShort(12),
                                            new UShort(2019)
                                        ),
                                        new Time(
                                            new UShort(21),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("start_of_month_in_a_literal_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(31),
                                            new UShort(12),
                                            new UShort(2019)
                                        ),
                                        new Time(
                                            new UShort(21),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("start_of_week_in_a_literal_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(12),
                                            new UShort(1),
                                            new UShort(2020)
                                        ),
                                        new Time(
                                            new UShort(21),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("start_of_day_in_a_literal_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(14),
                                            new UShort(1),
                                            new UShort(2020)
                                        ),
                                        new Time(
                                            new UShort(21),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("start_of_hour_in_a_literal_zone"),
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
                                            new UShort(9),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("start_of_minute_in_a_literal_zone"),
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
                                            new UShort(9),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("other_literal_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(14),
                                            new UShort(1),
                                            new UShort(2020)
                                        ),
                                        new Time(
                                            new UShort(11),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("column_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(13),
                                            new UShort(1),
                                            new UShort(2020)
                                        ),
                                        new Time(
                                            new UShort(5),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("computed_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(31),
                                            new UShort(12),
                                            new UShort(2019)
                                        ),
                                        new Time(
                                            new UShort(11),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(31),
                                            new UShort(12),
                                            new UShort(2019)
                                        ),
                                        new Time(
                                            new UShort(11),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("null_zone"),
                                    new DateTimeColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_operand"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(1),
                                            new UShort(4),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(4),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_operand_and_zone"),
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
                                new Column(
                                    new String("literal_operand"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(15),
                                            new UShort(8),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(18),
                                            new UShort(15),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("second_needs_no_zone"),
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
                                            new UShort(9),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("of_a_date"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(1),
                                        new UShort(1),
                                        new UShort(2020)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("of_a_time"), new TimeColumnType()),
                                new InvariantCell(
                                    new Time(new UShort(9), new UShort(0), new UShort(0))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("local_day_of_the_start"),
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
                                    new String("nested"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(1),
                                            new UShort(1),
                                            new UShort(2020)
                                        ),
                                        new Time(
                                            new UShort(5),
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
                                    new String("start_of_year_in_a_literal_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(31),
                                            new UShort(12),
                                            new UShort(2020)
                                        ),
                                        new Time(
                                            new UShort(21),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("start_of_quarter_in_a_literal_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(31),
                                            new UShort(12),
                                            new UShort(2020)
                                        ),
                                        new Time(
                                            new UShort(21),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("start_of_month_in_a_literal_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(28),
                                            new UShort(2),
                                            new UShort(2021)
                                        ),
                                        new Time(
                                            new UShort(21),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("start_of_week_in_a_literal_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(14),
                                            new UShort(3),
                                            new UShort(2021)
                                        ),
                                        new Time(
                                            new UShort(21),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("start_of_day_in_a_literal_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(19),
                                            new UShort(3),
                                            new UShort(2021)
                                        ),
                                        new Time(
                                            new UShort(21),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("start_of_hour_in_a_literal_zone"),
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
                                            new UShort(10),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("start_of_minute_in_a_literal_zone"),
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
                                            new UShort(10),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("other_literal_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(19),
                                            new UShort(3),
                                            new UShort(2021)
                                        ),
                                        new Time(
                                            new UShort(11),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("column_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(15),
                                            new UShort(3),
                                            new UShort(2021)
                                        ),
                                        new Time(
                                            new UShort(4),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("computed_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(28),
                                            new UShort(2),
                                            new UShort(2021)
                                        ),
                                        new Time(
                                            new UShort(11),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(31),
                                            new UShort(12),
                                            new UShort(2020)
                                        ),
                                        new Time(
                                            new UShort(11),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("null_zone"),
                                    new DateTimeColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_operand"),
                                    new DateTimeColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_operand_and_zone"),
                                    new DateTimeColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("literal_operand"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(15),
                                            new UShort(8),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(18),
                                            new UShort(15),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("second_needs_no_zone"),
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
                                            new UShort(10),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("of_a_date"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(1),
                                        new UShort(3),
                                        new UShort(2021)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("of_a_time"), new TimeColumnType()),
                                new InvariantCell(
                                    new Time(new UShort(10), new UShort(0), new UShort(0))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("local_day_of_the_start"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(1),
                                        new UShort(3),
                                        new UShort(2021)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nested"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(1),
                                            new UShort(3),
                                            new UShort(2021)
                                        ),
                                        new Time(
                                            new UShort(5),
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
                                    new String("start_of_year_in_a_literal_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(31),
                                            new UShort(12),
                                            new UShort(2021)
                                        ),
                                        new Time(
                                            new UShort(21),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("start_of_quarter_in_a_literal_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(30),
                                            new UShort(9),
                                            new UShort(2022)
                                        ),
                                        new Time(
                                            new UShort(21),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("start_of_month_in_a_literal_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(31),
                                            new UShort(10),
                                            new UShort(2022)
                                        ),
                                        new Time(
                                            new UShort(21),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("start_of_week_in_a_literal_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(30),
                                            new UShort(10),
                                            new UShort(2022)
                                        ),
                                        new Time(
                                            new UShort(21),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("start_of_day_in_a_literal_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(4),
                                            new UShort(11),
                                            new UShort(2022)
                                        ),
                                        new Time(
                                            new UShort(21),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("start_of_hour_in_a_literal_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(5),
                                            new UShort(11),
                                            new UShort(2022)
                                        ),
                                        new Time(
                                            new UShort(11),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("start_of_minute_in_a_literal_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(5),
                                            new UShort(11),
                                            new UShort(2022)
                                        ),
                                        new Time(
                                            new UShort(11),
                                            new UShort(30),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("other_literal_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(5),
                                            new UShort(11),
                                            new UShort(2022)
                                        ),
                                        new Time(
                                            new UShort(11),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("column_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(30),
                                            new UShort(10),
                                            new UShort(2022)
                                        ),
                                        new Time(
                                            new UShort(15),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("computed_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(31),
                                            new UShort(10),
                                            new UShort(2022)
                                        ),
                                        new Time(
                                            new UShort(15),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_zone"),
                                    new DateTimeColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("null_zone"),
                                    new DateTimeColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_operand"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(31),
                                            new UShort(3),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(15),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_operand_and_zone"),
                                    new DateTimeColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("literal_operand"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(15),
                                            new UShort(8),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(18),
                                            new UShort(15),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("second_needs_no_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(5),
                                            new UShort(11),
                                            new UShort(2022)
                                        ),
                                        new Time(
                                            new UShort(11),
                                            new UShort(30),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("of_a_date"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(1),
                                        new UShort(11),
                                        new UShort(2022)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("of_a_time"), new TimeColumnType()),
                                new InvariantCell(
                                    new Time(new UShort(11), new UShort(0), new UShort(0))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("local_day_of_the_start"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(1),
                                        new UShort(11),
                                        new UShort(2022)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nested"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(31),
                                            new UShort(10),
                                            new UShort(2022)
                                        ),
                                        new Time(
                                            new UShort(15),
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
                                    new String("start_of_year_in_a_literal_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(31),
                                            new UShort(12),
                                            new UShort(2022)
                                        ),
                                        new Time(
                                            new UShort(21),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("start_of_quarter_in_a_literal_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(31),
                                            new UShort(12),
                                            new UShort(2022)
                                        ),
                                        new Time(
                                            new UShort(21),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("start_of_month_in_a_literal_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(31),
                                            new UShort(1),
                                            new UShort(2023)
                                        ),
                                        new Time(
                                            new UShort(21),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("start_of_week_in_a_literal_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(26),
                                            new UShort(2),
                                            new UShort(2023)
                                        ),
                                        new Time(
                                            new UShort(21),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("start_of_day_in_a_literal_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(27),
                                            new UShort(2),
                                            new UShort(2023)
                                        ),
                                        new Time(
                                            new UShort(21),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("start_of_hour_in_a_literal_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(28),
                                            new UShort(2),
                                            new UShort(2023)
                                        ),
                                        new Time(
                                            new UShort(8),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("start_of_minute_in_a_literal_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(28),
                                            new UShort(2),
                                            new UShort(2023)
                                        ),
                                        new Time(
                                            new UShort(8),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("other_literal_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(27),
                                            new UShort(2),
                                            new UShort(2023)
                                        ),
                                        new Time(
                                            new UShort(11),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("column_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(26),
                                            new UShort(2),
                                            new UShort(2023)
                                        ),
                                        new Time(
                                            new UShort(15),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("computed_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(31),
                                            new UShort(1),
                                            new UShort(2023)
                                        ),
                                        new Time(
                                            new UShort(15),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_zone"),
                                    new DateTimeColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("null_zone"),
                                    new DateTimeColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_operand"),
                                    new DateTimeColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_operand_and_zone"),
                                    new DateTimeColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("literal_operand"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(15),
                                            new UShort(8),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(18),
                                            new UShort(15),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("second_needs_no_zone"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(28),
                                            new UShort(2),
                                            new UShort(2023)
                                        ),
                                        new Time(
                                            new UShort(8),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("of_a_date"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(1),
                                        new UShort(2),
                                        new UShort(2023)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("of_a_time"), new TimeColumnType()),
                                new InvariantCell(
                                    new Time(new UShort(8), new UShort(0), new UShort(0))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("local_day_of_the_start"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(1),
                                        new UShort(2),
                                        new UShort(2023)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nested"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(31),
                                            new UShort(1),
                                            new UShort(2023)
                                        ),
                                        new Time(
                                            new UShort(15),
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
            ]
        );
}
