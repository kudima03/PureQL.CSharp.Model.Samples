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
/// Selects the status and the local hour, in Pacific/Pago_Pago, of the keys of a subquery
/// night_orders, joined on true to a subquery totals, with the count of the night orders.
/// night_orders groups schema_with_foreign_keys.orders by placed_at and order_status,
/// keeps the groups whose key falls before 06:00 local time in Pacific/Pago_Pago, and
/// selects the status, the local hour and the local month in Europe/Moscow of the key,
/// the local hour of the latest placed_at, the number of different local weekdays and the
/// latest local hour. totals is a total query over all orders: the count of orders before
/// 06:00 local time, the local year of the first order (nullable), the most frequent
/// local hour (nullable) and the number of different local ISO weeks (weekYear times 100
/// plus week).
/// </summary>
public sealed record LocalPartsInGroupedAndTotalQueriesQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithoutPagination(
            subqueries:
            [
                new SubqueryQuery(
                    "night_orders",
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
                                "ordered_at",
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
                        having: new LessThanDecimalGroup(
                            new HourDatetimeIntegerGroup(
                                new KeyDatetime("ordered_at"),
                                zone: new LiteralString("Pacific/Pago_Pago")
                            ),
                            new LiteralInteger(6)
                        ),
                        select:
                        [
                            new SelectItemGroup("status", new KeyString("status")),
                            new SelectItemGroup(
                                "local_hour_of_the_key",
                                new HourDatetimeIntegerGroup(
                                    new KeyDatetime("ordered_at"),
                                    zone: new LiteralString("Pacific/Pago_Pago")
                                )
                            ),
                            new SelectItemGroup(
                                "local_month_of_the_key",
                                new MonthDatetimeIntegerGroup(
                                    new KeyDatetime("ordered_at"),
                                    zone: new LiteralString("Europe/Moscow")
                                )
                            ),
                            new SelectItemGroup(
                                "local_hour_of_the_last_order",
                                new HourDatetimeIntegerGroup(
                                    new MaxDatetimeGroup(
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
                                    zone: new LiteralString("Pacific/Pago_Pago")
                                )
                            ),
                            new SelectItemGroup(
                                "weekdays_ordered_on",
                                new CountDistinctGroup(
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
                                        zone: new LiteralString("Pacific/Pago_Pago")
                                    )
                                )
                            ),
                            new SelectItemGroup(
                                "latest_local_hour",
                                new MaxIntegerGroup(
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
                                        zone: new LiteralString("Pacific/Pago_Pago")
                                    )
                                )
                            ),
                        ]
                    )
                ),
                new SubqueryQuery(
                    "totals",
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
                        aggregate:
                        [
                            new SelectItemTotal(
                                "orders_at_night",
                                new CountTotal(
                                    predicate: new LessThanDecimalRow(
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
                                            zone: new LiteralString("Pacific/Pago_Pago")
                                        ),
                                        new LiteralInteger(6)
                                    )
                                )
                            ),
                            new SelectItemTotal(
                                "local_year_of_the_first_order",
                                new YearDatetimeIntegerNullableTotal(
                                    new MinDatetimeNullableTotal(
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
                                    zone: new LiteralString("Pacific/Pago_Pago")
                                )
                            ),
                            new SelectItemTotal(
                                "busiest_local_hour",
                                new ModeIntegerNullableTotal(
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
                                        zone: new LiteralString("Pacific/Pago_Pago")
                                    )
                                )
                            ),
                            new SelectItemTotal(
                                "weeks_with_orders",
                                new CountDistinctTotal(
                                    new AddIntegerRow([
                                        new MultiplyIntegerRow([
                                            new WeekYearDatetimeIntegerRow(
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
                                                zone: new LiteralString(
                                                    "Pacific/Pago_Pago"
                                                )
                                            ),
                                            new LiteralInteger(100),
                                        ]),
                                        new WeekDatetimeIntegerRow(
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
                                            zone: new LiteralString("Pacific/Pago_Pago")
                                        ),
                                    ])
                                )
                            ),
                        ]
                    )
                ),
            ],
            from: new FromSubquery("night_orders"),
            joins: [new JoinSubquery(JoinKind.Inner, "totals", new LiteralBoolean(true))],
            select:
            [
                new SelectItemProjection(
                    "status",
                    new FieldString("night_orders", "status")
                ),
                new SelectItemProjection(
                    "local_hour",
                    new FieldInteger("night_orders", "local_hour_of_the_key")
                ),
                new SelectItemProjection(
                    "orders_at_night",
                    new FieldInteger("totals", "orders_at_night")
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
                    new Column(new String("status"), new StringColumnType()),
                    new Column(new String("local_hour"), new LongColumnType()),
                    new Column(new String("orders_at_night"), new LongColumnType()),
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
                                    new String("local_hour"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("orders_at_night"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(5))
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
                                    new String("local_hour"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("orders_at_night"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(5))
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
                                    new String("local_hour"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(4))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("orders_at_night"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(5))
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
                                    new String("local_hour"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("orders_at_night"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(5))
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
                                    new String("local_hour"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("orders_at_night"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(5))
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
