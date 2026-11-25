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
using PureQL.CSharp.Model.Samples.Cells;
using PureQL.CSharp.Model.TotalExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using DateTime = Pure.Primitives.DateTime.DateTime;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.TimeZones;

/// <summary>
/// Selects the status, the local_midnight and the orders_of_the_local_day of a subquery
/// by_day inner joined to a subquery one_day on true. by_day groups
/// schema_with_foreign_keys.orders by placed_on and order_status, keeps the groups whose
/// earliest placed_at is not before asDatetime of the placed_on key in Pacific/Pago_Pago,
/// and selects the status key, that local midnight of the key, datetimeFromParts of the
/// key, noon and Pacific/Pago_Pago, datetimeFromParts of the key, the earliest time of
/// placed_at (asTime) and the zone, and the count of orders placed before
/// datetimeFromParts of their own placed_on, noon and the zone. one_day totals the orders
/// placed from asDatetime of 2024-06-03 up to that of the next day in the zone: their
/// count, the count of those placed before 18:00 local time (datetimeFromParts), the
/// first window as datetimeFromParts of the day, the earliest time and the zone, and
/// asDatetime of the latest placed_on in the zone.
/// </summary>
public sealed record LocalInstantsInGroupedAndTotalQueriesQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithoutPagination(
            subqueries:
            [
                new SubqueryQuery(
                    "by_day",
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
                        groupBy:
                        [
                            new GroupKey(
                                "order_date",
                                new FieldDate(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new OrdersTable().Name,
                                        ]
                                    ).TextValue,
                                    new PlacedOnColumn().Name.TextValue
                                )
                            ),
                            new GroupKey(
                                "status",
                                new FieldString(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new OrdersTable().Name,
                                        ]
                                    ).TextValue,
                                    new OrderStatusColumn().Name.TextValue
                                )
                            ),
                        ],
                        having: new GreaterThanOrEqualDatetimeGroup(
                            new MinDatetimeGroup(
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
                            ),
                            new AsDatetimeDateDatetimeGroup(
                                new KeyDate("order_date"),
                                new LiteralString("Pacific/Pago_Pago")
                            )
                        ),
                        select:
                        [
                            new SelectItemGroup("status", new KeyString("status")),
                            new SelectItemGroup(
                                "local_midnight_of_the_key",
                                new AsDatetimeDateDatetimeGroup(
                                    new KeyDate("order_date"),
                                    new LiteralString("Pacific/Pago_Pago")
                                )
                            ),
                            new SelectItemGroup(
                                "local_noon_of_the_key",
                                new DatetimeFromPartsDatetimeGroup(
                                    new KeyDate("order_date"),
                                    new LiteralTime(new TimeOnly(12, 0, 0)),
                                    new LiteralString("Europe/Moscow")
                                )
                            ),
                            new SelectItemGroup(
                                "earliest_window",
                                new DatetimeFromPartsDatetimeGroup(
                                    new KeyDate("order_date"),
                                    new MinTimeGroup(
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
                                            )
                                        )
                                    ),
                                    new LiteralString("Pacific/Pago_Pago")
                                )
                            ),
                            new SelectItemGroup(
                                "orders_before_local_noon",
                                new CountGroup(
                                    new LessThanDatetimeRow(
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
                                        new DatetimeFromPartsDatetimeRow(
                                            new FieldDate(
                                                new JoinedString(
                                                    new DotString(),
                                                    [
                                                        new RelationalSchemaWithForeignKeys().Name,
                                                        new OrdersTable().Name,
                                                    ]
                                                ).TextValue,
                                                new PlacedOnColumn().Name.TextValue
                                            ),
                                            new LiteralTime(new TimeOnly(12, 0, 0)),
                                            new LiteralString("Pacific/Pago_Pago")
                                        )
                                    )
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
                        where: new AndRow([
                            new GreaterThanOrEqualDatetimeRow(
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
                                new AsDatetimeDateDatetimeRow(
                                    new LiteralDate(new DateOnly(2024, 6, 3)),
                                    new LiteralString("Pacific/Pago_Pago")
                                )
                            ),
                            new LessThanDatetimeRow(
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
                                new AsDatetimeDateDatetimeRow(
                                    new DateAddDaysDateRow(
                                        new LiteralDate(new DateOnly(2024, 6, 3)),
                                        new LiteralInteger(1)
                                    ),
                                    new LiteralString("Pacific/Pago_Pago")
                                )
                            ),
                        ]),
                        aggregate:
                        [
                            new SelectItemTotal(
                                "orders_of_the_local_day",
                                new CountTotal()
                            ),
                            new SelectItemTotal(
                                "before_the_cutoff",
                                new CountTotal(
                                    new LessThanDatetimeRow(
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
                                        new DatetimeFromPartsDatetimeRow(
                                            new LiteralDate(new DateOnly(2024, 6, 3)),
                                            new LiteralTime(new TimeOnly(18, 0, 0)),
                                            new LiteralString("Pacific/Pago_Pago")
                                        )
                                    )
                                )
                            ),
                            new SelectItemTotal(
                                "first_window",
                                new DatetimeFromPartsDatetimeNullableTotal(
                                    new LiteralDate(new DateOnly(2024, 6, 3)),
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
                                            )
                                        )
                                    ),
                                    new LiteralString("Pacific/Pago_Pago")
                                )
                            ),
                            new SelectItemTotal(
                                "local_midnight_of_the_last_order_date",
                                new AsDatetimeDateDatetimeNullableTotal(
                                    new MaxDateNullableTotal(
                                        new FieldDate(
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new RelationalSchemaWithForeignKeys().Name,
                                                    new OrdersTable().Name,
                                                ]
                                            ).TextValue,
                                            new PlacedOnColumn().Name.TextValue
                                        )
                                    ),
                                    new LiteralString("Pacific/Pago_Pago")
                                )
                            ),
                        ]
                    )
                ),
            ],
            from: new FromSubquery("by_day"),
            joins:
            [
                new JoinSubquery(JoinKind.Inner, "one_day", new LiteralBoolean(true)),
            ],
            select:
            [
                new SelectItemProjection("status", new FieldString("by_day", "status")),
                new SelectItemProjection(
                    "local_midnight",
                    new FieldDatetime("by_day", "local_midnight_of_the_key")
                ),
                new SelectItemProjection(
                    "orders_of_the_local_day",
                    new FieldInteger("one_day", "orders_of_the_local_day")
                ),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and
    /// <see cref="AuditSchemaDataSet"/>, in no particular order.
    /// Local times are read with the tz database of PostgreSQL 17 and checked with Python
    /// zoneinfo; no value falls in a skipped or repeated local hour.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("status"), new StringColumnType()),
                    new Column(new String("local_midnight"), new DateTimeColumnType()),
                    new Column(
                        new String("orders_of_the_local_day"),
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
                                new Column(new String("status"), new StringColumnType()),
                                new InvariantCell(new String("cancelled"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("local_midnight"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(4),
                                            new UShort(6),
                                            new UShort(2024)
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
                                    new String("orders_of_the_local_day"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
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
                                new Column(new String("status"), new StringColumnType()),
                                new InvariantCell(new String("pending"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("local_midnight"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(2),
                                            new UShort(6),
                                            new UShort(2024)
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
                                    new String("orders_of_the_local_day"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
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
                                new Column(new String("status"), new StringColumnType()),
                                new InvariantCell(new String("pending"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("local_midnight"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(6),
                                            new UShort(6),
                                            new UShort(2024)
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
                                    new String("orders_of_the_local_day"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
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
                                new Column(new String("status"), new StringColumnType()),
                                new InvariantCell(new String("shipped"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("local_midnight"),
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
                                            new UShort(11),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("orders_of_the_local_day"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
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
                                new Column(new String("status"), new StringColumnType()),
                                new InvariantCell(new String("shipped"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("local_midnight"),
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
                                            new UShort(11),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("orders_of_the_local_day"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
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
