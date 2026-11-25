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
using PureQL.CSharp.Model.TotalExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.TimeZones;

/// <summary>
/// Selects, from a grouped subquery per_local_day joined to a total subquery one_day on
/// true, the zone, local_day and orders of the former and the orders of the latter.
/// per_local_day groups schema_with_foreign_keys.orders inner joined to
/// schema_with_foreign_keys.users on order_user_id = user_id by the zone of the user
/// (America/Los_Angeles for tenant #901 and Asia/Tokyo otherwise) and the local day of
/// placed_at in it (asDate with a zone), keeps the groups with a count greater than 0 of
/// orders placed before 06:00 local time (asTime with a zone), and selects the zone, the
/// local day, the count of orders, the count of those before 06:00, the latest local
/// time, the count of distinct local days of the instant 111600 seconds after placed_at
/// (datetimeAddSeconds), and the day after the local day. one_day totals the orders whose
/// local day in Asia/Tokyo is 2024-06-05: their count, the earliest and median local time
/// and the latest local day of the instant 111600 seconds after placed_at.
/// </summary>
public sealed record LocalDatesGroupedAndTotalledQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithoutPagination(
            subqueries:
            [
                new SubqueryQuery(
                    "per_local_day",
                    new GroupedQueryWithoutPaginationWithoutOrderBy(
                        from: new FromEntity(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrdersTable().Name,
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
                                )
                            ),
                        ],
                        groupBy:
                        [
                            new GroupKey(
                                "zone",
                                new IfStringRow(
                                    new LiteralString("America/Los_Angeles"),
                                    new LiteralString("Asia/Tokyo"),
                                    new EqualUuidRow(
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
                            new GroupKey(
                                "local_day",
                                new AsDateDatetimeDateRow(
                                    new FieldDatetime(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new OrdersTable().Name,
                                            ]
                                        ).TextValue,
                                        new PlacedAtColumn().Name.TextValue
                                    ),
                                    zone: new IfStringRow(
                                        new LiteralString("America/Los_Angeles"),
                                        new LiteralString("Asia/Tokyo"),
                                        new EqualUuidRow(
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
                                                new Guid(901, 0, 0, new byte[8])
                                            )
                                        )
                                    )
                                )
                            ),
                        ],
                        having: new GreaterThanDecimalGroup(
                            new CountGroup(
                                predicate: new LessThanTimeRow(
                                    new AsTimeDatetimeTimeRow(
                                        new FieldDatetime(
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new RelationalSchemaWithForeignKeys().Name,
                                                    new OrdersTable().Name,
                                                ]
                                            ).TextValue,
                                            new PlacedAtColumn().Name.TextValue
                                        ),
                                        zone: new IfStringRow(
                                            new LiteralString("America/Los_Angeles"),
                                            new LiteralString("Asia/Tokyo"),
                                            new EqualUuidRow(
                                                new FieldUuid(
                                                    new JoinedString(
                                                        new DotString(),
                                                        [
                                                            new RelationalSchemaWithForeignKeys().Name,
                                                            new UsersTable().Name,
                                                        ]
                                                    ).TextValue,
                                                    new UserTenantIdColumn()
                                                        .Name
                                                        .TextValue
                                                ),
                                                new LiteralUuid(
                                                    new Guid(901, 0, 0, new byte[8])
                                                )
                                            )
                                        )
                                    ),
                                    new LiteralTime(new TimeOnly(6, 0, 0))
                                )
                            ),
                            new LiteralInteger(0)
                        ),
                        select:
                        [
                            new SelectItemGroup("zone", new KeyString("zone")),
                            new SelectItemGroup("local_day", new KeyDate("local_day")),
                            new SelectItemGroup("orders", new CountGroup()),
                            new SelectItemGroup(
                                "night_orders",
                                new CountGroup(
                                    predicate: new LessThanTimeRow(
                                        new AsTimeDatetimeTimeRow(
                                            new FieldDatetime(
                                                new JoinedString(
                                                    new DotString(),
                                                    [
                                                        new RelationalSchemaWithForeignKeys().Name,
                                                        new OrdersTable().Name,
                                                    ]
                                                ).TextValue,
                                                new PlacedAtColumn().Name.TextValue
                                            ),
                                            zone: new IfStringRow(
                                                new LiteralString("America/Los_Angeles"),
                                                new LiteralString("Asia/Tokyo"),
                                                new EqualUuidRow(
                                                    new FieldUuid(
                                                        new JoinedString(
                                                            new DotString(),
                                                            [
                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                new UsersTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new UserTenantIdColumn()
                                                            .Name
                                                            .TextValue
                                                    ),
                                                    new LiteralUuid(
                                                        new Guid(901, 0, 0, new byte[8])
                                                    )
                                                )
                                            )
                                        ),
                                        new LiteralTime(new TimeOnly(6, 0, 0))
                                    )
                                )
                            ),
                            new SelectItemGroup(
                                "latest_local_time",
                                new MaxTimeGroup(
                                    new AsTimeDatetimeTimeRow(
                                        new FieldDatetime(
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new RelationalSchemaWithForeignKeys().Name,
                                                    new OrdersTable().Name,
                                                ]
                                            ).TextValue,
                                            new PlacedAtColumn().Name.TextValue
                                        ),
                                        zone: new IfStringRow(
                                            new LiteralString("America/Los_Angeles"),
                                            new LiteralString("Asia/Tokyo"),
                                            new EqualUuidRow(
                                                new FieldUuid(
                                                    new JoinedString(
                                                        new DotString(),
                                                        [
                                                            new RelationalSchemaWithForeignKeys().Name,
                                                            new UsersTable().Name,
                                                        ]
                                                    ).TextValue,
                                                    new UserTenantIdColumn()
                                                        .Name
                                                        .TextValue
                                                ),
                                                new LiteralUuid(
                                                    new Guid(901, 0, 0, new byte[8])
                                                )
                                            )
                                        )
                                    )
                                )
                            ),
                            new SelectItemGroup(
                                "local_days_shipped_on",
                                new CountDistinctGroup(
                                    new AsDateDatetimeDateRow(
                                        new DatetimeAddSecondsDatetimeRow(
                                            new FieldDatetime(
                                                new JoinedString(
                                                    new DotString(),
                                                    [
                                                        new RelationalSchemaWithForeignKeys().Name,
                                                        new OrdersTable().Name,
                                                    ]
                                                ).TextValue,
                                                new PlacedAtColumn().Name.TextValue
                                            ),
                                            new LiteralDecimal(111600m)
                                        ),
                                        zone: new IfStringRow(
                                            new LiteralString("America/Los_Angeles"),
                                            new LiteralString("Asia/Tokyo"),
                                            new EqualUuidRow(
                                                new FieldUuid(
                                                    new JoinedString(
                                                        new DotString(),
                                                        [
                                                            new RelationalSchemaWithForeignKeys().Name,
                                                            new UsersTable().Name,
                                                        ]
                                                    ).TextValue,
                                                    new UserTenantIdColumn()
                                                        .Name
                                                        .TextValue
                                                ),
                                                new LiteralUuid(
                                                    new Guid(901, 0, 0, new byte[8])
                                                )
                                            )
                                        )
                                    )
                                )
                            ),
                            new SelectItemGroup(
                                "next_local_day",
                                new DateAddDaysDateGroup(
                                    new KeyDate("local_day"),
                                    new LiteralInteger(1)
                                )
                            ),
                        ]
                    )
                ),
                new SubqueryQuery(
                    "one_day",
                    new TotalQuery(
                        from: new FromEntity(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrdersTable().Name,
                                ]
                            ).TextValue
                        ),
                        where: new EqualDateRow(
                            new AsDateDatetimeDateRow(
                                new FieldDatetime(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new OrdersTable().Name,
                                        ]
                                    ).TextValue,
                                    new PlacedAtColumn().Name.TextValue
                                ),
                                zone: new LiteralString("Asia/Tokyo")
                            ),
                            new LiteralDate(new DateOnly(2024, 6, 5))
                        ),
                        aggregate:
                        [
                            new SelectItemTotal("orders", new CountTotal()),
                            new SelectItemTotal(
                                "first_local_time",
                                new MinTimeNullableTotal(
                                    new AsTimeDatetimeTimeRow(
                                        new FieldDatetime(
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new RelationalSchemaWithForeignKeys().Name,
                                                    new OrdersTable().Name,
                                                ]
                                            ).TextValue,
                                            new PlacedAtColumn().Name.TextValue
                                        ),
                                        zone: new LiteralString("Asia/Tokyo")
                                    )
                                )
                            ),
                            new SelectItemTotal(
                                "last_local_day_shipped",
                                new MaxDateNullableTotal(
                                    new AsDateDatetimeDateRow(
                                        new DatetimeAddSecondsDatetimeRow(
                                            new FieldDatetime(
                                                new JoinedString(
                                                    new DotString(),
                                                    [
                                                        new RelationalSchemaWithForeignKeys().Name,
                                                        new OrdersTable().Name,
                                                    ]
                                                ).TextValue,
                                                new PlacedAtColumn().Name.TextValue
                                            ),
                                            new LiteralDecimal(111600m)
                                        ),
                                        zone: new LiteralString("Asia/Tokyo")
                                    )
                                )
                            ),
                            new SelectItemTotal(
                                "median_local_time",
                                new MedianTimeNullableTotal(
                                    new AsTimeDatetimeTimeRow(
                                        new FieldDatetime(
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new RelationalSchemaWithForeignKeys().Name,
                                                    new OrdersTable().Name,
                                                ]
                                            ).TextValue,
                                            new PlacedAtColumn().Name.TextValue
                                        ),
                                        zone: new LiteralString("Asia/Tokyo")
                                    )
                                )
                            ),
                        ]
                    )
                ),
            ],
            from: new FromSubquery("per_local_day"),
            joins:
            [
                new JoinSubquery(JoinKind.Inner, "one_day", new LiteralBoolean(true)),
            ],
            select:
            [
                new SelectItemProjection(
                    "zone",
                    new FieldString("per_local_day", "zone")
                ),
                new SelectItemProjection(
                    "local_day",
                    new FieldDate("per_local_day", "local_day")
                ),
                new SelectItemProjection(
                    "orders",
                    new FieldInteger("per_local_day", "orders")
                ),
                new SelectItemProjection(
                    "orders_of_the_day",
                    new FieldInteger("one_day", "orders")
                ),
                new SelectItemProjection(
                    "night_orders",
                    new FieldInteger("per_local_day", "night_orders")
                ),
                new SelectItemProjection(
                    "latest_local_time",
                    new FieldTime("per_local_day", "latest_local_time")
                ),
                new SelectItemProjection(
                    "local_days_shipped_on",
                    new FieldInteger("per_local_day", "local_days_shipped_on")
                ),
                new SelectItemProjection(
                    "next_local_day",
                    new FieldDate("per_local_day", "next_local_day")
                ),
                new SelectItemProjection(
                    "first_local_time",
                    new FieldTimeNullable("one_day", "first_local_time")
                ),
                new SelectItemProjection(
                    "last_local_day_shipped",
                    new FieldDateNullable("one_day", "last_local_day_shipped")
                ),
                new SelectItemProjection(
                    "median_local_time",
                    new FieldTimeNullable("one_day", "median_local_time")
                ),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over <see
    /// cref="SchemaDataSetWithForeignKeys"/> and <see cref="AuditSchemaDataSet"/>, in no
    /// particular order.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("zone"), new StringColumnType()),
                    new Column(new String("local_day"), new DateColumnType()),
                    new Column(new String("orders"), new LongColumnType()),
                    new Column(new String("orders_of_the_day"), new LongColumnType()),
                    new Column(new String("night_orders"), new LongColumnType()),
                    new Column(new String("latest_local_time"), new TimeColumnType()),
                    new Column(new String("local_days_shipped_on"), new LongColumnType()),
                    new Column(new String("next_local_day"), new DateColumnType()),
                    new Column(new String("first_local_time"), new TimeColumnType()),
                    new Column(
                        new String("last_local_day_shipped"),
                        new DateColumnType()
                    ),
                    new Column(new String("median_local_time"), new TimeColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("zone"), new StringColumnType()),
                                new InvariantCell(new String("America/Los_Angeles"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("local_day"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(1),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("orders_of_the_day"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("night_orders"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("latest_local_time"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(
                                    new Time(new UShort(3), new UShort(0), new UShort(0))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("local_days_shipped_on"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("next_local_day"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(2),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_local_time"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(
                                    new Time(new UShort(23), new UShort(0), new UShort(0))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_local_day_shipped"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(7),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("median_local_time"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(
                                    new Time(new UShort(23), new UShort(0), new UShort(0))
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
                                new Column(new String("zone"), new StringColumnType()),
                                new InvariantCell(new String("America/Los_Angeles"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("local_day"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(2),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("orders_of_the_day"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("night_orders"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("latest_local_time"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(
                                    new Time(new UShort(4), new UShort(0), new UShort(0))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("local_days_shipped_on"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("next_local_day"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(3),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_local_time"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(
                                    new Time(new UShort(23), new UShort(0), new UShort(0))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_local_day_shipped"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(7),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("median_local_time"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(
                                    new Time(new UShort(23), new UShort(0), new UShort(0))
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
                                new Column(new String("zone"), new StringColumnType()),
                                new InvariantCell(new String("America/Los_Angeles"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("local_day"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(3),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("orders_of_the_day"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("night_orders"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("latest_local_time"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(
                                    new Time(new UShort(5), new UShort(0), new UShort(0))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("local_days_shipped_on"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("next_local_day"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(4),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_local_time"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(
                                    new Time(new UShort(23), new UShort(0), new UShort(0))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_local_day_shipped"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(7),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("median_local_time"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(
                                    new Time(new UShort(23), new UShort(0), new UShort(0))
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
                                new Column(new String("zone"), new StringColumnType()),
                                new InvariantCell(new String("Asia/Tokyo"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("local_day"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(7),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("orders_of_the_day"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("night_orders"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("latest_local_time"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(
                                    new Time(new UShort(0), new UShort(0), new UShort(0))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("local_days_shipped_on"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("next_local_day"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(8),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_local_time"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(
                                    new Time(new UShort(23), new UShort(0), new UShort(0))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_local_day_shipped"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(7),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("median_local_time"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(
                                    new Time(new UShort(23), new UShort(0), new UShort(0))
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
