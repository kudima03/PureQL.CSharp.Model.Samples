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
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.ProjectionExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.TimeZones;

/// <summary>
/// Selects from a subquery members, which gives each user of
/// schema_with_foreign_keys.users the zone America/New_York for tenant #901 and
/// Asia/Tokyo otherwise, created_at (the first instant of signup_date at shift_start),
/// last_login_at (last_login of an active user, otherwise null), optional_zone
/// (Pacific/Auckland for tenant #901, otherwise null), birth_date (signup_date) and
/// preferred_call_time (shift_start). Every part that takes a zone is read in the literal
/// zone Europe/Moscow (year, quarter, month, day, dayOfWeek, dayOfYear, week, weekYear,
/// hour, minute and second of created_at), in another literal zone (Pacific/Auckland), in
/// the zone column, in a computed zone (the optional zone, else the zone column), in a
/// nullable zone column, and in a null zone; quarter and second of a nullable
/// last_login_at give a nullable part; the hour of a datetime literal in Asia/Kathmandu,
/// whether the local day of created_at equals the day of its local date, and the
/// microsecond of created_at, the year of birth_date and the hour of preferred_call_time
/// take no zone.
/// </summary>
public sealed record LocalPartKindsQuery
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
                    "year_in_a_literal_zone",
                    new YearDatetimeIntegerProjection(
                        new FieldDatetime("members", "created_at"),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemProjection(
                    "quarter_in_a_literal_zone",
                    new QuarterDatetimeIntegerProjection(
                        new FieldDatetime("members", "created_at"),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemProjection(
                    "month_in_a_literal_zone",
                    new MonthDatetimeIntegerProjection(
                        new FieldDatetime("members", "created_at"),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemProjection(
                    "day_in_a_literal_zone",
                    new DayDatetimeIntegerProjection(
                        new FieldDatetime("members", "created_at"),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemProjection(
                    "dayOfWeek_in_a_literal_zone",
                    new DayOfWeekDatetimeIntegerProjection(
                        new FieldDatetime("members", "created_at"),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemProjection(
                    "dayOfYear_in_a_literal_zone",
                    new DayOfYearDatetimeIntegerProjection(
                        new FieldDatetime("members", "created_at"),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemProjection(
                    "week_in_a_literal_zone",
                    new WeekDatetimeIntegerProjection(
                        new FieldDatetime("members", "created_at"),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemProjection(
                    "weekYear_in_a_literal_zone",
                    new WeekYearDatetimeIntegerProjection(
                        new FieldDatetime("members", "created_at"),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemProjection(
                    "hour_in_a_literal_zone",
                    new HourDatetimeIntegerProjection(
                        new FieldDatetime("members", "created_at"),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemProjection(
                    "minute_in_a_literal_zone",
                    new MinuteDatetimeIntegerProjection(
                        new FieldDatetime("members", "created_at"),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemProjection(
                    "second_in_a_literal_zone",
                    new SecondDatetimeIntegerProjection(
                        new FieldDatetime("members", "created_at"),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemProjection(
                    "other_literal_zone",
                    new HourDatetimeIntegerProjection(
                        new FieldDatetime("members", "created_at"),
                        zone: new LiteralString("Pacific/Auckland")
                    )
                ),
                new SelectItemProjection(
                    "column_zone",
                    new DayOfWeekDatetimeIntegerProjection(
                        new FieldDatetime("members", "created_at"),
                        zone: new FieldString("members", "time_zone")
                    )
                ),
                new SelectItemProjection(
                    "computed_zone",
                    new MonthDatetimeIntegerProjection(
                        new FieldDatetime("members", "created_at"),
                        zone: new CoalesceStringProjection([
                            new FieldStringNullable("members", "optional_zone"),
                            new FieldString("members", "time_zone"),
                        ])
                    )
                ),
                new SelectItemProjection(
                    "nullable_zone",
                    new YearDatetimeIntegerNullableProjection(
                        new FieldDatetime("members", "created_at"),
                        zone: new FieldStringNullable("members", "optional_zone")
                    )
                ),
                new SelectItemProjection(
                    "null_zone",
                    new MinuteDatetimeIntegerNullableProjection(
                        new FieldDatetime("members", "created_at"),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemProjection(
                    "nullable_operand",
                    new QuarterDatetimeIntegerNullableProjection(
                        new FieldDatetimeNullable("members", "last_login_at"),
                        zone: new FieldString("members", "time_zone")
                    )
                ),
                new SelectItemProjection(
                    "nullable_operand_and_zone",
                    new SecondDatetimeIntegerNullableProjection(
                        new FieldDatetimeNullable("members", "last_login_at"),
                        zone: new FieldStringNullable("members", "optional_zone")
                    )
                ),
                new SelectItemProjection(
                    "literal_operand",
                    new HourDatetimeIntegerProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 31, 22, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Asia/Kathmandu")
                    )
                ),
                new SelectItemProjection(
                    "same_as_the_part_of_the_local_date",
                    new EqualDecimalProjection(
                        new DayDatetimeIntegerProjection(
                            new FieldDatetime("members", "created_at"),
                            zone: new FieldString("members", "time_zone")
                        ),
                        new DayDateIntegerProjection(
                            new AsDateDatetimeDateProjection(
                                new FieldDatetime("members", "created_at"),
                                zone: new FieldString("members", "time_zone")
                            )
                        )
                    )
                ),
                new SelectItemProjection(
                    "microsecond_needs_no_zone",
                    new MicrosecondDatetimeIntegerProjection(
                        new FieldDatetime("members", "created_at")
                    )
                ),
                new SelectItemProjection(
                    "of_a_date",
                    new YearDateIntegerProjection(new FieldDate("members", "birth_date"))
                ),
                new SelectItemProjection(
                    "of_a_time",
                    new HourTimeIntegerProjection(
                        new FieldTime("members", "preferred_call_time")
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
                        new String("year_in_a_literal_zone"),
                        new LongColumnType()
                    ),
                    new Column(
                        new String("quarter_in_a_literal_zone"),
                        new LongColumnType()
                    ),
                    new Column(
                        new String("month_in_a_literal_zone"),
                        new LongColumnType()
                    ),
                    new Column(new String("day_in_a_literal_zone"), new LongColumnType()),
                    new Column(
                        new String("dayOfWeek_in_a_literal_zone"),
                        new LongColumnType()
                    ),
                    new Column(
                        new String("dayOfYear_in_a_literal_zone"),
                        new LongColumnType()
                    ),
                    new Column(
                        new String("week_in_a_literal_zone"),
                        new LongColumnType()
                    ),
                    new Column(
                        new String("weekYear_in_a_literal_zone"),
                        new LongColumnType()
                    ),
                    new Column(
                        new String("hour_in_a_literal_zone"),
                        new LongColumnType()
                    ),
                    new Column(
                        new String("minute_in_a_literal_zone"),
                        new LongColumnType()
                    ),
                    new Column(
                        new String("second_in_a_literal_zone"),
                        new LongColumnType()
                    ),
                    new Column(new String("other_literal_zone"), new LongColumnType()),
                    new Column(new String("column_zone"), new LongColumnType()),
                    new Column(new String("computed_zone"), new LongColumnType()),
                    new Column(new String("nullable_zone"), new LongColumnType()),
                    new Column(new String("null_zone"), new LongColumnType()),
                    new Column(new String("nullable_operand"), new LongColumnType()),
                    new Column(
                        new String("nullable_operand_and_zone"),
                        new LongColumnType()
                    ),
                    new Column(new String("literal_operand"), new LongColumnType()),
                    new Column(
                        new String("same_as_the_part_of_the_local_date"),
                        new BoolColumnType()
                    ),
                    new Column(
                        new String("microsecond_needs_no_zone"),
                        new LongColumnType()
                    ),
                    new Column(new String("of_a_date"), new LongColumnType()),
                    new Column(new String("of_a_time"), new LongColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("year_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2019))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("quarter_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("month_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(7))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("day_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(10))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("dayOfWeek_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("dayOfYear_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(191))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("week_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(28))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("weekYear_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2019))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("hour_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(12))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("minute_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("second_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("other_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(21))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("column_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("computed_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(7))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2019))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("null_zone"), new LongColumnType()),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_operand"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_operand_and_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("literal_operand"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(4))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("same_as_the_part_of_the_local_date"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("microsecond_needs_no_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("of_a_date"), new LongColumnType()),
                                new InvariantCell(new Long(2019))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("of_a_time"), new LongColumnType()),
                                new InvariantCell(new Long(9))
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
                                    new String("year_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2020))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("quarter_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("month_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("day_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(15))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("dayOfWeek_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("dayOfYear_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(15))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("week_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("weekYear_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2020))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("hour_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(12))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("minute_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("second_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("other_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(22))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("column_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("computed_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_zone"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("null_zone"), new LongColumnType()),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_operand"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_operand_and_zone"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("literal_operand"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(4))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("same_as_the_part_of_the_local_date"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("microsecond_needs_no_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("of_a_date"), new LongColumnType()),
                                new InvariantCell(new Long(2020))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("of_a_time"), new LongColumnType()),
                                new InvariantCell(new Long(9))
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
                                    new String("year_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2020))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("quarter_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("month_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("day_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(15))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("dayOfWeek_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("dayOfYear_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(15))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("week_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("weekYear_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2020))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("hour_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(12))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("minute_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("second_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("other_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(22))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("column_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("computed_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2020))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("null_zone"), new LongColumnType()),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_operand"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_operand_and_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("literal_operand"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(4))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("same_as_the_part_of_the_local_date"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("microsecond_needs_no_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("of_a_date"), new LongColumnType()),
                                new InvariantCell(new Long(2020))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("of_a_time"), new LongColumnType()),
                                new InvariantCell(new Long(9))
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
                                    new String("year_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2021))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("quarter_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("month_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("day_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(20))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("dayOfWeek_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(6))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("dayOfYear_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(79))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("week_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(11))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("weekYear_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2021))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("hour_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(13))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("minute_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("second_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("other_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(23))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("column_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(6))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("computed_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2021))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("null_zone"), new LongColumnType()),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_operand"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_operand_and_zone"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("literal_operand"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(4))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("same_as_the_part_of_the_local_date"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("microsecond_needs_no_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("of_a_date"), new LongColumnType()),
                                new InvariantCell(new Long(2021))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("of_a_time"), new LongColumnType()),
                                new InvariantCell(new Long(10))
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
                                    new String("year_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2022))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("quarter_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(4))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("month_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(11))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("day_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("dayOfWeek_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(6))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("dayOfYear_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(309))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("week_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(44))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("weekYear_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2022))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("hour_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(14))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("minute_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(30))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("second_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("other_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("column_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(6))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("computed_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(11))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_zone"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("null_zone"), new LongColumnType()),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_operand"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_operand_and_zone"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("literal_operand"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(4))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("same_as_the_part_of_the_local_date"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("microsecond_needs_no_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("of_a_date"), new LongColumnType()),
                                new InvariantCell(new Long(2022))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("of_a_time"), new LongColumnType()),
                                new InvariantCell(new Long(11))
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
                                    new String("year_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2023))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("quarter_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("month_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("day_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(28))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("dayOfWeek_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("dayOfYear_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(59))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("week_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(9))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("weekYear_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2023))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("hour_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(11))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("minute_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("second_in_a_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("other_literal_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(21))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("column_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("computed_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_zone"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("null_zone"), new LongColumnType()),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_operand"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_operand_and_zone"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("literal_operand"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(4))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("same_as_the_part_of_the_local_date"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("microsecond_needs_no_zone"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("of_a_date"), new LongColumnType()),
                                new InvariantCell(new Long(2023))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("of_a_time"), new LongColumnType()),
                                new InvariantCell(new Long(8))
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
