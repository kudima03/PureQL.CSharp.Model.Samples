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
using Guid = Pure.Primitives.Guid.Guid;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.TimeZones;

/// <summary>
/// Selects from a subquery members, which gives each user of
/// schema_with_foreign_keys.users the zone America/New_York for tenant #901 and
/// Asia/Tokyo otherwise, created_at, birth_date, preferred_call_time and is_active, the
/// active ones, with id, the local hour at the instant 2020-01-15T01:00:00Z in the user's
/// zone, whether the user can be called then (the local hour not before the hour of the
/// preferred call time and before 21, on a weekday: dayOfWeek below 6), a greeting by the
/// local hour, whether the user signed up in the current local year, and whether the
/// user's birth_date is the local day (month and day), ordered by the id column.
/// </summary>
public sealed record LocalPartsInConditionsQuery
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
                                "id",
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
                            new SelectItemProjection(
                                "is_active",
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
                        ]
                    )
                ),
            ],
            from: new FromSubquery("members"),
            where: new FieldBoolean("members", "is_active"),
            select:
            [
                new SelectItemProjection("id", new FieldUuid("members", "id")),
                new SelectItemProjection(
                    "local_hour_now",
                    new HourDatetimeIntegerProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2020, 1, 15, 1, 0, 0, TimeSpan.Zero)
                        ),
                        zone: new FieldString("members", "time_zone")
                    )
                ),
                new SelectItemProjection(
                    "can_be_called_now",
                    new AndProjection([
                        new GreaterThanOrEqualDecimalProjection(
                            new HourDatetimeIntegerProjection(
                                new LiteralDatetime(
                                    new DateTimeOffset(
                                        2020,
                                        1,
                                        15,
                                        1,
                                        0,
                                        0,
                                        TimeSpan.Zero
                                    )
                                ),
                                zone: new FieldString("members", "time_zone")
                            ),
                            new HourTimeIntegerProjection(
                                new FieldTime("members", "preferred_call_time")
                            )
                        ),
                        new LessThanDecimalProjection(
                            new HourDatetimeIntegerProjection(
                                new LiteralDatetime(
                                    new DateTimeOffset(
                                        2020,
                                        1,
                                        15,
                                        1,
                                        0,
                                        0,
                                        TimeSpan.Zero
                                    )
                                ),
                                zone: new FieldString("members", "time_zone")
                            ),
                            new LiteralInteger(21)
                        ),
                        new LessThanDecimalProjection(
                            new DayOfWeekDatetimeIntegerProjection(
                                new LiteralDatetime(
                                    new DateTimeOffset(
                                        2020,
                                        1,
                                        15,
                                        1,
                                        0,
                                        0,
                                        TimeSpan.Zero
                                    )
                                ),
                                zone: new FieldString("members", "time_zone")
                            ),
                            new LiteralInteger(6)
                        ),
                    ])
                ),
                new SelectItemProjection(
                    "greeting",
                    new IfStringProjection(
                        new LiteralString("Good morning"),
                        new LiteralString("Good afternoon"),
                        new LessThanDecimalProjection(
                            new HourDatetimeIntegerProjection(
                                new LiteralDatetime(
                                    new DateTimeOffset(
                                        2020,
                                        1,
                                        15,
                                        1,
                                        0,
                                        0,
                                        TimeSpan.Zero
                                    )
                                ),
                                zone: new FieldString("members", "time_zone")
                            ),
                            new LiteralInteger(12)
                        )
                    )
                ),
                new SelectItemProjection(
                    "signed_up_this_local_year",
                    new EqualDecimalProjection(
                        new YearDatetimeIntegerProjection(
                            new FieldDatetime("members", "created_at"),
                            zone: new FieldString("members", "time_zone")
                        ),
                        new YearDatetimeIntegerProjection(
                            new LiteralDatetime(
                                new DateTimeOffset(2020, 1, 15, 1, 0, 0, TimeSpan.Zero)
                            ),
                            zone: new FieldString("members", "time_zone")
                        )
                    )
                ),
                new SelectItemProjection(
                    "birthday_today",
                    new AndProjection([
                        new EqualDecimalProjection(
                            new MonthDateIntegerProjection(
                                new FieldDate("members", "birth_date")
                            ),
                            new MonthDatetimeIntegerProjection(
                                new LiteralDatetime(
                                    new DateTimeOffset(
                                        2020,
                                        1,
                                        15,
                                        1,
                                        0,
                                        0,
                                        TimeSpan.Zero
                                    )
                                ),
                                zone: new FieldString("members", "time_zone")
                            )
                        ),
                        new EqualDecimalProjection(
                            new DayDateIntegerProjection(
                                new FieldDate("members", "birth_date")
                            ),
                            new DayDatetimeIntegerProjection(
                                new LiteralDatetime(
                                    new DateTimeOffset(
                                        2020,
                                        1,
                                        15,
                                        1,
                                        0,
                                        0,
                                        TimeSpan.Zero
                                    )
                                ),
                                zone: new FieldString("members", "time_zone")
                            )
                        ),
                    ])
                ),
            ],
            orderBy: [new OrderItemProjectionColumn("id")]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and
    /// <see cref="AuditSchemaDataSet"/>, in this order.
    /// Local times are read with the tz database of PostgreSQL 17 and checked with
    /// Python zoneinfo; no value falls in a skipped or repeated local hour.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("id"), new UuidColumnType()),
                    new Column(new String("local_hour_now"), new LongColumnType()),
                    new Column(new String("can_be_called_now"), new BoolColumnType()),
                    new Column(new String("greeting"), new StringColumnType()),
                    new Column(
                        new String("signed_up_this_local_year"),
                        new BoolColumnType()
                    ),
                    new Column(new String("birthday_today"), new BoolColumnType()),
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
                                            "00000001-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("local_hour_now"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(20))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("can_be_called_now"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greeting"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Good afternoon"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("signed_up_this_local_year"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("birthday_today"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
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
                                            "00000003-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("local_hour_now"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(20))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("can_be_called_now"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greeting"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Good afternoon"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("signed_up_this_local_year"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("birthday_today"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
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
                                            "00000004-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("local_hour_now"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(10))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("can_be_called_now"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greeting"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Good morning"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("signed_up_this_local_year"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("birthday_today"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
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
                                            "00000006-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("local_hour_now"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(10))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("can_be_called_now"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greeting"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Good morning"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("signed_up_this_local_year"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("birthday_today"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
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
