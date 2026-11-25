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

namespace PureQL.CSharp.Model.Samples.Queries.TimeZones;

/// <summary>
/// Selects from schema_with_foreign_keys.orders inner joined to
/// schema_with_foreign_keys.users on order_user_id = user_id and the year of placed_at
/// equal to that of last_login, both in the zone of the user (America/St_Johns for tenant
/// #901 and America/Chicago otherwise), the orders placed from the hour 9 up to 18 on a
/// day of the week below 4 in that zone: the id, the local year, week and weekYear of
/// placed_at, its hour in UTC and in the zone, the count of orders of its user in the
/// same local year and month, the latest local hour over all rows, and the seconds past
/// the local hour (the minute times 60 plus the second), ordered by the local dayOfYear,
/// the local hour descending and the id column.
/// </summary>
public sealed record LocalPartsInEveryClauseQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithoutPagination(
            from: new FromEntity(
                new JoinedString(
                    new DotString(),
                    [new RelationalSchemaWithForeignKeys().Name, new OrdersTable().Name]
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
                        new EqualDecimalRow(
                            new YearDatetimeIntegerRow(
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
                                    new LiteralString("America/St_Johns"),
                                    new LiteralString("America/Chicago"),
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
                                            new System.Guid(901, 0, 0, new byte[8])
                                        )
                                    )
                                )
                            ),
                            new YearDatetimeIntegerRow(
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
                                zone: new IfStringRow(
                                    new LiteralString("America/St_Johns"),
                                    new LiteralString("America/Chicago"),
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
                                            new System.Guid(901, 0, 0, new byte[8])
                                        )
                                    )
                                )
                            )
                        ),
                    ])
                ),
            ],
            where: new AndRow([
                new GreaterThanOrEqualDecimalRow(
                    new HourDatetimeIntegerRow(
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
                            new LiteralString("America/St_Johns"),
                            new LiteralString("America/Chicago"),
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
                                new LiteralUuid(new System.Guid(901, 0, 0, new byte[8]))
                            )
                        )
                    ),
                    new LiteralInteger(9)
                ),
                new LessThanDecimalRow(
                    new HourDatetimeIntegerRow(
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
                            new LiteralString("America/St_Johns"),
                            new LiteralString("America/Chicago"),
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
                                new LiteralUuid(new System.Guid(901, 0, 0, new byte[8]))
                            )
                        )
                    ),
                    new LiteralInteger(18)
                ),
                new LessThanDecimalRow(
                    new DayOfWeekDatetimeIntegerRow(
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
                            new LiteralString("America/St_Johns"),
                            new LiteralString("America/Chicago"),
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
                                new LiteralUuid(new System.Guid(901, 0, 0, new byte[8]))
                            )
                        )
                    ),
                    new LiteralInteger(4)
                ),
            ]),
            select:
            [
                new SelectItemProjection(
                    "id",
                    new FieldUuid(
                        new JoinedString(
                            new DotString(),
                            [
                                new RelationalSchemaWithForeignKeys().Name,
                                new OrdersTable().Name,
                            ]
                        ).TextValue,
                        new OrderIdColumn().Name.TextValue
                    )
                ),
                new SelectItemProjection(
                    "local_year",
                    new YearDatetimeIntegerProjection(
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
                        zone: new IfStringProjection(
                            new LiteralString("America/St_Johns"),
                            new LiteralString("America/Chicago"),
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
                                new LiteralUuid(new System.Guid(901, 0, 0, new byte[8]))
                            )
                        )
                    )
                ),
                new SelectItemProjection(
                    "local_week",
                    new WeekDatetimeIntegerProjection(
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
                        zone: new IfStringProjection(
                            new LiteralString("America/St_Johns"),
                            new LiteralString("America/Chicago"),
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
                                new LiteralUuid(new System.Guid(901, 0, 0, new byte[8]))
                            )
                        )
                    )
                ),
                new SelectItemProjection(
                    "local_week_year",
                    new WeekYearDatetimeIntegerProjection(
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
                        zone: new IfStringProjection(
                            new LiteralString("America/St_Johns"),
                            new LiteralString("America/Chicago"),
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
                                new LiteralUuid(new System.Guid(901, 0, 0, new byte[8]))
                            )
                        )
                    )
                ),
                new SelectItemProjection(
                    "utc_hour",
                    new HourDatetimeIntegerProjection(
                        new FieldDatetime(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrdersTable().Name,
                                ]
                            ).TextValue,
                            new PlacedAtColumn().Name.TextValue
                        )
                    )
                ),
                new SelectItemProjection(
                    "local_hour",
                    new HourDatetimeIntegerProjection(
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
                        zone: new IfStringProjection(
                            new LiteralString("America/St_Johns"),
                            new LiteralString("America/Chicago"),
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
                                new LiteralUuid(new System.Guid(901, 0, 0, new byte[8]))
                            )
                        )
                    )
                ),
                new SelectItemProjection(
                    "orders_in_the_local_month",
                    new CountProjection(
                        over: new WindowWithoutFrameWithoutRange([
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
                            new YearDatetimeIntegerRow(
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
                                    new LiteralString("America/St_Johns"),
                                    new LiteralString("America/Chicago"),
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
                                            new System.Guid(901, 0, 0, new byte[8])
                                        )
                                    )
                                )
                            ),
                            new MonthDatetimeIntegerRow(
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
                                    new LiteralString("America/St_Johns"),
                                    new LiteralString("America/Chicago"),
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
                                            new System.Guid(901, 0, 0, new byte[8])
                                        )
                                    )
                                )
                            ),
                        ])
                    )
                ),
                new SelectItemProjection(
                    "latest_local_hour",
                    new MaxIntegerProjection(
                        new HourDatetimeIntegerRow(
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
                                new LiteralString("America/St_Johns"),
                                new LiteralString("America/Chicago"),
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
                                        new System.Guid(901, 0, 0, new byte[8])
                                    )
                                )
                            )
                        )
                    )
                ),
                new SelectItemProjection(
                    "seconds_past_the_local_hour",
                    new AddIntegerProjection([
                        new MultiplyIntegerProjection([
                            new MinuteDatetimeIntegerProjection(
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
                                zone: new IfStringProjection(
                                    new LiteralString("America/St_Johns"),
                                    new LiteralString("America/Chicago"),
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
                            new LiteralInteger(60),
                        ]),
                        new SecondDatetimeIntegerProjection(
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
                            zone: new IfStringProjection(
                                new LiteralString("America/St_Johns"),
                                new LiteralString("America/Chicago"),
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
                    ])
                ),
            ],
            orderBy:
            [
                new OrderItemProjectionExpression(
                    new DayOfYearDatetimeIntegerProjection(
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
                        zone: new IfStringProjection(
                            new LiteralString("America/St_Johns"),
                            new LiteralString("America/Chicago"),
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
                                new LiteralUuid(new System.Guid(901, 0, 0, new byte[8]))
                            )
                        )
                    )
                ),
                new OrderItemProjectionExpression(
                    new HourDatetimeIntegerProjection(
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
                        zone: new IfStringProjection(
                            new LiteralString("America/St_Johns"),
                            new LiteralString("America/Chicago"),
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
                                new LiteralUuid(new System.Guid(901, 0, 0, new byte[8]))
                            )
                        )
                    ),
                    SortDirection.Desc
                ),
                new OrderItemProjectionColumn("id"),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over <see
    /// cref="SchemaDataSetWithForeignKeys"/> and <see cref="AuditSchemaDataSet"/>, in
    /// this order. The local parts were checked with Python zoneinfo.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("id"), new UuidColumnType()),
                    new Column(new String("local_year"), new LongColumnType()),
                    new Column(new String("local_week"), new LongColumnType()),
                    new Column(new String("local_week_year"), new LongColumnType()),
                    new Column(new String("utc_hour"), new LongColumnType()),
                    new Column(new String("local_hour"), new LongColumnType()),
                    new Column(
                        new String("orders_in_the_local_month"),
                        new LongColumnType()
                    ),
                    new Column(new String("latest_local_hour"), new LongColumnType()),
                    new Column(
                        new String("seconds_past_the_local_hour"),
                        new LongColumnType()
                    ),
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
                                            "00000067-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("local_year"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2024))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("local_week"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(23))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("local_week_year"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2024))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("utc_hour"), new LongColumnType()),
                                new InvariantCell(new Long(12))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("local_hour"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(9))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("orders_in_the_local_month"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("latest_local_hour"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(11))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("seconds_past_the_local_hour"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1800))
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
                                            "00000068-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("local_year"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2024))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("local_week"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(23))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("local_week_year"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2024))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("utc_hour"), new LongColumnType()),
                                new InvariantCell(new Long(13))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("local_hour"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(10))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("orders_in_the_local_month"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("latest_local_hour"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(11))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("seconds_past_the_local_hour"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1800))
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
                                            "00000069-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("local_year"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2024))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("local_week"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(23))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("local_week_year"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2024))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("utc_hour"), new LongColumnType()),
                                new InvariantCell(new Long(14))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("local_hour"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(11))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("orders_in_the_local_month"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("latest_local_hour"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(11))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("seconds_past_the_local_hour"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1800))
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
