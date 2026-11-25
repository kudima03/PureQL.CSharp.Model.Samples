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
using PureQL.CSharp.Model.ProjectionExpressions;
using PureQL.CSharp.Model.Samples.Cells;
using Column = Pure.RelationalSchema.Column.Column;
using DateTime = Pure.Primitives.DateTime.DateTime;
using Guid = Pure.Primitives.Guid.Guid;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.TimeZones;

/// <summary>
/// Selects from a subquery members, which gives each user of
/// schema_with_foreign_keys.users its user_active flag, the zone America/New_York for
/// tenant #901 and Asia/Tokyo otherwise, and member_since, the first instant of
/// signup_date in that zone, the active members with the whole months and years from
/// member_since to 2024-06-08T12:00:00Z in their zone (datetimeDiffMonths with a zone),
/// the next monthly renewal and the next anniversary at the same local time
/// (datetimeAddMonths with a zone), whether the renewal falls before the start of the
/// next local month, and member_since plus 1 month, ordered by the next_monthly_renewal
/// and user_id columns.
/// </summary>
public sealed record MonthsInALocalCalendarQuery
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
                                "user_active",
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
                                        new LiteralUuid(
                                            new System.Guid(901, 0, 0, new byte[8])
                                        )
                                    )
                                )
                            ),
                            new SelectItemProjection(
                                "member_since",
                                new AsDatetimeDateDatetimeProjection(
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
                                            new LiteralUuid(
                                                new System.Guid(901, 0, 0, new byte[8])
                                            )
                                        )
                                    )
                                )
                            ),
                        ]
                    )
                ),
            ],
            from: new FromSubquery("members"),
            where: new FieldBoolean("members", "user_active"),
            select:
            [
                new SelectItemProjection("user_id", new FieldUuid("members", "user_id")),
                new SelectItemProjection(
                    "member_since",
                    new FieldDatetime("members", "member_since")
                ),
                new SelectItemProjection(
                    "whole_months_as_a_member",
                    new DatetimeDiffMonthsIntegerProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 6, 8, 12, 0, 0, TimeSpan.Zero)
                        ),
                        new FieldDatetime("members", "member_since"),
                        new FieldString("members", "time_zone")
                    )
                ),
                new SelectItemProjection(
                    "whole_years_as_a_member",
                    new IntegerDivideIntegerProjection(
                        new DatetimeDiffMonthsIntegerProjection(
                            new LiteralDatetime(
                                new DateTimeOffset(2024, 6, 8, 12, 0, 0, TimeSpan.Zero)
                            ),
                            new FieldDatetime("members", "member_since"),
                            new FieldString("members", "time_zone")
                        ),
                        new LiteralInteger(12)
                    )
                ),
                new SelectItemProjection(
                    "next_monthly_renewal",
                    new DatetimeAddMonthsDatetimeProjection(
                        new FieldDatetime("members", "member_since"),
                        new AddIntegerProjection([
                            new DatetimeDiffMonthsIntegerProjection(
                                new LiteralDatetime(
                                    new DateTimeOffset(
                                        2024,
                                        6,
                                        8,
                                        12,
                                        0,
                                        0,
                                        TimeSpan.Zero
                                    )
                                ),
                                new FieldDatetime("members", "member_since"),
                                new FieldString("members", "time_zone")
                            ),
                            new LiteralInteger(1),
                        ]),
                        new FieldString("members", "time_zone")
                    )
                ),
                new SelectItemProjection(
                    "next_anniversary",
                    new DatetimeAddMonthsDatetimeProjection(
                        new FieldDatetime("members", "member_since"),
                        new MultiplyIntegerProjection([
                            new AddIntegerProjection([
                                new IntegerDivideIntegerProjection(
                                    new DatetimeDiffMonthsIntegerProjection(
                                        new LiteralDatetime(
                                            new DateTimeOffset(
                                                2024,
                                                6,
                                                8,
                                                12,
                                                0,
                                                0,
                                                TimeSpan.Zero
                                            )
                                        ),
                                        new FieldDatetime("members", "member_since"),
                                        new FieldString("members", "time_zone")
                                    ),
                                    new LiteralInteger(12)
                                ),
                                new LiteralInteger(1),
                            ]),
                            new LiteralInteger(12),
                        ]),
                        new FieldString("members", "time_zone")
                    )
                ),
                new SelectItemProjection(
                    "renews_this_local_month",
                    new LessThanDatetimeProjection(
                        new DatetimeAddMonthsDatetimeProjection(
                            new FieldDatetime("members", "member_since"),
                            new AddIntegerProjection([
                                new DatetimeDiffMonthsIntegerProjection(
                                    new LiteralDatetime(
                                        new DateTimeOffset(
                                            2024,
                                            6,
                                            8,
                                            12,
                                            0,
                                            0,
                                            TimeSpan.Zero
                                        )
                                    ),
                                    new FieldDatetime("members", "member_since"),
                                    new FieldString("members", "time_zone")
                                ),
                                new LiteralInteger(1),
                            ]),
                            new FieldString("members", "time_zone")
                        ),
                        new DatetimeAddMonthsDatetimeProjection(
                            new StartOfMonthDatetimeProjection(
                                new LiteralDatetime(
                                    new DateTimeOffset(
                                        2024,
                                        6,
                                        8,
                                        12,
                                        0,
                                        0,
                                        TimeSpan.Zero
                                    )
                                ),
                                new FieldString("members", "time_zone")
                            ),
                            new LiteralInteger(1),
                            new FieldString("members", "time_zone")
                        )
                    )
                ),
                new SelectItemProjection(
                    "trial_ended_at",
                    new DatetimeAddMonthsDatetimeProjection(
                        new FieldDatetime("members", "member_since"),
                        new LiteralInteger(1),
                        new FieldString("members", "time_zone")
                    )
                ),
            ],
            orderBy:
            [
                new OrderItemProjectionColumn("next_monthly_renewal"),
                new OrderItemProjectionColumn("user_id"),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and
    /// <see cref="AuditSchemaDataSet"/>, in this order.
    /// Local times are read with the tz database of PostgreSQL 17 and checked with Python
    /// zoneinfo; no value falls in a skipped or repeated local hour. datetimeDiffMonths
    /// is translated as the inverse of adding months in the local calendar.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new UserIdColumn(),
                    new Column(new String("member_since"), new DateTimeColumnType()),
                    new Column(
                        new String("whole_months_as_a_member"),
                        new LongColumnType()
                    ),
                    new Column(
                        new String("whole_years_as_a_member"),
                        new LongColumnType()
                    ),
                    new Column(
                        new String("next_monthly_renewal"),
                        new DateTimeColumnType()
                    ),
                    new Column(new String("next_anniversary"), new DateTimeColumnType()),
                    new Column(
                        new String("renews_this_local_month"),
                        new BoolColumnType()
                    ),
                    new Column(new String("trial_ended_at"), new DateTimeColumnType()),
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
                                            "00000003-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("member_since"),
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
                                            new UShort(4),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("whole_months_as_a_member"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(58))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("whole_years_as_a_member"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(4))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("next_monthly_renewal"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(10),
                                            new UShort(6),
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
                                    new String("next_anniversary"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(10),
                                            new UShort(7),
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
                                    new String("renews_this_local_month"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("trial_ended_at"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(10),
                                            new UShort(8),
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
                                new UserIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000006-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("member_since"),
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
                                            new UShort(15),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("whole_months_as_a_member"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(52))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("whole_years_as_a_member"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(4))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("next_monthly_renewal"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(14),
                                            new UShort(6),
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
                                    new String("next_anniversary"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(14),
                                            new UShort(1),
                                            new UShort(2025)
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
                                    new String("renews_this_local_month"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("trial_ended_at"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(14),
                                            new UShort(2),
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
                                            "00000001-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("member_since"),
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
                                            new UShort(5),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("whole_months_as_a_member"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(52))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("whole_years_as_a_member"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(4))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("next_monthly_renewal"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(15),
                                            new UShort(6),
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
                                    new String("next_anniversary"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(15),
                                            new UShort(1),
                                            new UShort(2025)
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
                                    new String("renews_this_local_month"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("trial_ended_at"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(15),
                                            new UShort(2),
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
                                    new String("member_since"),
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
                                            new UShort(15),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("whole_months_as_a_member"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(19))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("whole_years_as_a_member"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("next_monthly_renewal"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(4),
                                            new UShort(7),
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
                                    new String("next_anniversary"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(4),
                                            new UShort(11),
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
                                    new String("renews_this_local_month"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("trial_ended_at"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(4),
                                            new UShort(12),
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
            ]
        );
}
