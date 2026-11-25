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
using PureQL.CSharp.Model.GroupExpressions;
using PureQL.CSharp.Model.Keys;
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.RowExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.TimeZones;

/// <summary>
/// Groups schema_with_foreign_keys.orders placed from 2024-06-02T00:00:00Z on by the
/// dayOfWeek and the hour of placed_at in America/New_York, and selects both keys, the
/// count of orders, the sum of order_total, whether the weekday is a Saturday or Sunday,
/// whether the hour is from 9 up to 18, and the count divided by the count over all rows,
/// ordered by the weekday and hour columns.
/// </summary>
public sealed record LocalWeekdayAndHourQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainGroupedQueryWithoutPagination(
            from: new FromEntity(
                new JoinedString(
                    new DotString(),
                    [new RelationalSchemaWithForeignKeys().Name, new OrdersTable().Name]
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
                new LiteralDatetime(
                    new DateTimeOffset(2024, 6, 2, 0, 0, 0, TimeSpan.Zero)
                )
            ),
            groupBy:
            [
                new GroupKey(
                    "weekday",
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
                        new LiteralString("America/New_York")
                    )
                ),
                new GroupKey(
                    "hour",
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
                        new LiteralString("America/New_York")
                    )
                ),
            ],
            select:
            [
                new SelectItemGroup("weekday", new KeyInteger("weekday")),
                new SelectItemGroup("hour", new KeyInteger("hour")),
                new SelectItemGroup("orders", new CountGroup()),
                new SelectItemGroup(
                    "revenue",
                    new SumDecimalGroup(
                        new FieldDecimal(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrdersTable().Name,
                                ]
                            ).TextValue,
                            new OrderTotalColumn().Name.TextValue
                        )
                    )
                ),
                new SelectItemGroup(
                    "at_the_weekend",
                    new GreaterThanOrEqualDecimalGroup(
                        new KeyInteger("weekday"),
                        new LiteralInteger(6)
                    )
                ),
                new SelectItemGroup(
                    "in_business_hours",
                    new AndGroup([
                        new GreaterThanOrEqualDecimalGroup(
                            new KeyInteger("hour"),
                            new LiteralInteger(9)
                        ),
                        new LessThanDecimalGroup(
                            new KeyInteger("hour"),
                            new LiteralInteger(18)
                        ),
                    ])
                ),
                new SelectItemGroup(
                    "share_of_all_orders",
                    new DivideDecimalGroup([
                        new CountGroup(),
                        new CountGroup(over: AggregateOver.All),
                    ])
                ),
            ],
            orderBy:
            [
                new OrderItemGroupColumn("weekday"),
                new OrderItemGroupColumn("hour"),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and
    /// <see cref="AuditSchemaDataSet"/>, in this order.
    /// Local times are read with the tz database of PostgreSQL 17 and checked with Python
    /// zoneinfo; no value falls in a skipped or repeated local hour.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("weekday"), new LongColumnType()),
                    new Column(new String("hour"), new LongColumnType()),
                    new Column(new String("orders"), new LongColumnType()),
                    new Column(new String("revenue"), new DoubleColumnType()),
                    new Column(new String("at_the_weekend"), new BoolColumnType()),
                    new Column(new String("in_business_hours"), new BoolColumnType()),
                    new Column(new String("share_of_all_orders"), new DoubleColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("weekday"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("hour"), new LongColumnType()),
                                new InvariantCell(new Long(8))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("revenue"), new DoubleColumnType()),
                                new InvariantCell(new Double(200))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("at_the_weekend"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("in_business_hours"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("share_of_all_orders"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.2))
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
                                new Column(new String("weekday"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("hour"), new LongColumnType()),
                                new InvariantCell(new Long(9))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("revenue"), new DoubleColumnType()),
                                new InvariantCell(new Double(75.25))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("at_the_weekend"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("in_business_hours"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("share_of_all_orders"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.2))
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
                                new Column(new String("weekday"), new LongColumnType()),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("hour"), new LongColumnType()),
                                new InvariantCell(new Long(10))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("revenue"), new DoubleColumnType()),
                                new InvariantCell(new Double(300))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("at_the_weekend"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("in_business_hours"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("share_of_all_orders"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.2))
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
                                new Column(new String("weekday"), new LongColumnType()),
                                new InvariantCell(new Long(4))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("hour"), new LongColumnType()),
                                new InvariantCell(new Long(11))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("revenue"), new DoubleColumnType()),
                                new InvariantCell(new Double(100.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("at_the_weekend"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("in_business_hours"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("share_of_all_orders"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.2))
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
                                new Column(new String("weekday"), new LongColumnType()),
                                new InvariantCell(new Long(7))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("hour"), new LongColumnType()),
                                new InvariantCell(new Long(7))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("revenue"), new DoubleColumnType()),
                                new InvariantCell(new Double(50))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("at_the_weekend"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("in_business_hours"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("share_of_all_orders"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.2))
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
