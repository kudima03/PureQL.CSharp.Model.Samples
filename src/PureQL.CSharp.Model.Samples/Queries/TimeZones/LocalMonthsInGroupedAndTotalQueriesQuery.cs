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
/// Selects the status, the months_since and the orders_of_twelve_local_months of a
/// subquery recent inner joined to a subquery year on true. recent groups
/// schema_with_foreign_keys.orders by the startOfWeek of placed_at and order_status,
/// keeps the groups whose week is not before 2024-12-01T12:00:00Z minus 6 months
/// (datetimeAddMonths with the zone America/New_York), and selects the status key, that
/// week plus 1 month in the zone, the whole months from 2024-12-01T12:00:00Z to the week
/// in Europe/Moscow (datetimeDiffMonths), the whole months from the earliest to the
/// latest shipped_at (placed_at plus a lag of round(order_total / 100) months) in the
/// zone, and the count of orders placed from 6 months before that instant in the zone.
/// year totals the orders placed from 12 months before the start of the local month of
/// that instant (startOfMonth and datetimeAddMonths): their count, the whole months from
/// the earliest to the latest placed_at, the latest plus 1 month and the mode of the
/// whole months since each order, all in the zone.
/// </summary>
public sealed record LocalMonthsInGroupedAndTotalQueriesQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithoutPagination(
            subqueries:
            [
                new SubqueryQuery(
                    "recent",
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
                                "ordered_week",
                                new StartOfWeekDatetimeRow(
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
                            new KeyDatetime("ordered_week"),
                            new DatetimeAddMonthsDatetimeGroup(
                                new LiteralDatetime(
                                    new DateTimeOffset(
                                        2024,
                                        12,
                                        1,
                                        12,
                                        0,
                                        0,
                                        TimeSpan.Zero
                                    )
                                ),
                                new LiteralInteger(-6),
                                new LiteralString("America/New_York")
                            )
                        ),
                        select:
                        [
                            new SelectItemGroup("status", new KeyString("status")),
                            new SelectItemGroup(
                                "a_month_after_the_key",
                                new DatetimeAddMonthsDatetimeGroup(
                                    new KeyDatetime("ordered_week"),
                                    new LiteralInteger(1),
                                    new LiteralString("America/New_York")
                                )
                            ),
                            new SelectItemGroup(
                                "months_since_the_key",
                                new DatetimeDiffMonthsIntegerGroup(
                                    new LiteralDatetime(
                                        new DateTimeOffset(
                                            2024,
                                            12,
                                            1,
                                            12,
                                            0,
                                            0,
                                            TimeSpan.Zero
                                        )
                                    ),
                                    new KeyDatetime("ordered_week"),
                                    new LiteralString("Europe/Moscow")
                                )
                            ),
                            new SelectItemGroup(
                                "months_between_first_and_last_shipment",
                                new DatetimeDiffMonthsIntegerGroup(
                                    new MaxDatetimeGroup(
                                        new DatetimeAddMonthsDatetimeRow(
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
                                            new RoundWithoutDigitsIntegerRow(
                                                new DivideDecimalRow([
                                                    new FieldDecimal(
                                                        new JoinedString(
                                                            new DotString(),
                                                            [
                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                new OrdersTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new OrderTotalColumn()
                                                            .Name
                                                            .TextValue
                                                    ),
                                                    new LiteralInteger(100),
                                                ])
                                            )
                                        )
                                    ),
                                    new MinDatetimeGroup(
                                        new DatetimeAddMonthsDatetimeRow(
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
                                            new RoundWithoutDigitsIntegerRow(
                                                new DivideDecimalRow([
                                                    new FieldDecimal(
                                                        new JoinedString(
                                                            new DotString(),
                                                            [
                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                new OrdersTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new OrderTotalColumn()
                                                            .Name
                                                            .TextValue
                                                    ),
                                                    new LiteralInteger(100),
                                                ])
                                            )
                                        )
                                    ),
                                    new LiteralString("America/New_York")
                                )
                            ),
                            new SelectItemGroup(
                                "orders_in_the_last_six_local_months",
                                new CountGroup(
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
                                        new DatetimeAddMonthsDatetimeRow(
                                            new LiteralDatetime(
                                                new DateTimeOffset(
                                                    2024,
                                                    12,
                                                    1,
                                                    12,
                                                    0,
                                                    0,
                                                    TimeSpan.Zero
                                                )
                                            ),
                                            new LiteralInteger(-6),
                                            new LiteralString("America/New_York")
                                        )
                                    )
                                )
                            ),
                        ]
                    )
                ),
                new SubqueryQuery(
                    "year",
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
                        where: new GreaterThanOrEqualDatetimeRow(
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
                            new DatetimeAddMonthsDatetimeRow(
                                new StartOfMonthDatetimeRow(
                                    new LiteralDatetime(
                                        new DateTimeOffset(
                                            2024,
                                            12,
                                            1,
                                            12,
                                            0,
                                            0,
                                            TimeSpan.Zero
                                        )
                                    ),
                                    new LiteralString("America/New_York")
                                ),
                                new LiteralInteger(-12),
                                new LiteralString("America/New_York")
                            )
                        ),
                        aggregate:
                        [
                            new SelectItemTotal(
                                "orders_of_twelve_local_months",
                                new CountTotal()
                            ),
                            new SelectItemTotal(
                                "months_covered",
                                new DatetimeDiffMonthsIntegerNullableTotal(
                                    new MaxDatetimeNullableTotal(
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
                                    new LiteralString("America/New_York")
                                )
                            ),
                            new SelectItemTotal(
                                "a_month_after_the_last_order",
                                new DatetimeAddMonthsDatetimeNullableTotal(
                                    new MaxDatetimeNullableTotal(
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
                                    new LiteralInteger(1),
                                    new LiteralString("America/New_York")
                                )
                            ),
                            new SelectItemTotal(
                                "most_common_age_in_months",
                                new ModeIntegerNullableTotal(
                                    new DatetimeDiffMonthsIntegerRow(
                                        new LiteralDatetime(
                                            new DateTimeOffset(
                                                2024,
                                                12,
                                                1,
                                                12,
                                                0,
                                                0,
                                                TimeSpan.Zero
                                            )
                                        ),
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
                                        new LiteralString("America/New_York")
                                    )
                                )
                            ),
                        ]
                    )
                ),
            ],
            from: new FromSubquery("recent"),
            joins: [new JoinSubquery(JoinKind.Inner, "year", new LiteralBoolean(true))],
            select:
            [
                new SelectItemProjection("status", new FieldString("recent", "status")),
                new SelectItemProjection(
                    "months_since",
                    new FieldInteger("recent", "months_since_the_key")
                ),
                new SelectItemProjection(
                    "orders_of_twelve_local_months",
                    new FieldInteger("year", "orders_of_twelve_local_months")
                ),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and
    /// <see cref="AuditSchemaDataSet"/>, in no particular order.
    /// Local times are read with the tz database of PostgreSQL 17 and checked with Python
    /// zoneinfo; no value falls in a skipped or repeated local hour. datetimeDiffMonths
    /// is translated as the inverse of adding months in the local calendar.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("status"), new StringColumnType()),
                    new Column(new String("months_since"), new LongColumnType()),
                    new Column(
                        new String("orders_of_twelve_local_months"),
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
                                    new String("months_since"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("orders_of_twelve_local_months"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(6))
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
                                    new String("months_since"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("orders_of_twelve_local_months"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(6))
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
                                    new String("months_since"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("orders_of_twelve_local_months"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(6))
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
