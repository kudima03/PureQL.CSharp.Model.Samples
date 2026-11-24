using Pure.Collections.Generic;
using Pure.Primitives.Date;
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
using PureQL.CSharp.Model.ProjectionExpressions;
using PureQL.CSharp.Model.RowExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Windows;

/// <summary>
/// Selects, from a grouped subquery daily that sums order_total and counts the orders of
/// schema_with_foreign_keys.orders per placed_on, each day with its revenue and orders
/// next to windows over the grouped rows ordered by day: the cumulative revenue, the
/// revenue week to date in a partition by the startOfWeek of the day, the average of the
/// current and 2 preceding days, the previous day's revenue and the change from it, the
/// share of the week (divided by nullIfEqual of the week's sum and 0) and the rank by
/// revenue descending in the week, ordered by day.
/// </summary>
public sealed record RunningTotalOfDailySumsQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithoutPagination(
            subqueries:
            [
                new SubqueryQuery(
                    "daily",
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
                                "day",
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
                        ],
                        select:
                        [
                            new SelectItemGroup("day", new KeyDate("day")),
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
                            new SelectItemGroup("orders", new CountGroup()),
                        ]
                    )
                ),
            ],
            from: new FromSubquery("daily"),
            select:
            [
                new SelectItemProjection("day", new FieldDate("daily", "day")),
                new SelectItemProjection("revenue", new FieldDecimal("daily", "revenue")),
                new SelectItemProjection("orders", new FieldInteger("daily", "orders")),
                new SelectItemProjection(
                    "cumulative_revenue",
                    new SumDecimalProjection(
                        new FieldDecimal("daily", "revenue"),
                        over: new WindowWithFrame(
                            [new OrderItemRow(new FieldDate("daily", "day"))],
                            new Frame(new All())
                        )
                    )
                ),
                new SelectItemProjection(
                    "revenue_week_to_date",
                    new SumDecimalProjection(
                        new FieldDecimal("daily", "revenue"),
                        over: new WindowWithFrame(
                            [new OrderItemRow(new FieldDate("daily", "day"))],
                            new Frame(new All()),
                            [new StartOfWeekDateRow(new FieldDate("daily", "day"))]
                        )
                    )
                ),
                new SelectItemProjection(
                    "average_of_3_days",
                    new AverageDecimalProjection(
                        new FieldDecimal("daily", "revenue"),
                        new WindowWithFrame(
                            [new OrderItemRow(new FieldDate("daily", "day"))],
                            new Frame(2)
                        )
                    )
                ),
                new SelectItemProjection(
                    "previous_day_revenue",
                    new PreviousDecimalNullableProjection(
                        new FieldDecimal("daily", "revenue"),
                        new OrderedWindow([
                            new OrderItemRow(new FieldDate("daily", "day")),
                        ])
                    )
                ),
                new SelectItemProjection(
                    "change",
                    new SubtractDecimalNullableProjection([
                        new FieldDecimal("daily", "revenue"),
                        new PreviousDecimalNullableProjection(
                            new FieldDecimal("daily", "revenue"),
                            new OrderedWindow([
                                new OrderItemRow(new FieldDate("daily", "day")),
                            ])
                        ),
                    ])
                ),
                new SelectItemProjection(
                    "share_of_week",
                    new DivideDecimalNullableProjection([
                        new FieldDecimal("daily", "revenue"),
                        new NullIfEqualDecimalNullableProjection(
                            new SumDecimalProjection(
                                new FieldDecimal("daily", "revenue"),
                                over: new WindowWithoutFrameWithoutRange([
                                    new StartOfWeekDateRow(new FieldDate("daily", "day")),
                                ])
                            ),
                            new LiteralInteger(0)
                        ),
                    ])
                ),
                new SelectItemProjection(
                    "rank_in_week",
                    new RankProjection(
                        new OrderedWindow(
                            [
                                new OrderItemRow(
                                    new FieldDecimal("daily", "revenue"),
                                    SortDirection.Desc
                                ),
                            ],
                            [new StartOfWeekDateRow(new FieldDate("daily", "day"))]
                        )
                    )
                ),
            ],
            orderBy: [new OrderItemProjectionExpression(new FieldDate("daily", "day"))]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and
    /// <see cref="AuditSchemaDataSet"/>, in this order.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("day"), new DateColumnType()),
                    new Column(new String("revenue"), new DoubleColumnType()),
                    new Column(new String("orders"), new LongColumnType()),
                    new Column(new String("cumulative_revenue"), new DoubleColumnType()),
                    new Column(
                        new String("revenue_week_to_date"),
                        new DoubleColumnType()
                    ),
                    new Column(new String("average_of_3_days"), new DoubleColumnType()),
                    new Column(
                        new String("previous_day_revenue"),
                        new DoubleColumnType()
                    ),
                    new Column(new String("change"), new DoubleColumnType()),
                    new Column(new String("share_of_week"), new DoubleColumnType()),
                    new Column(new String("rank_in_week"), new LongColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("day"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(1),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("revenue"), new DoubleColumnType()),
                                new InvariantCell(new Double(100.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("cumulative_revenue"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(100.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("revenue_week_to_date"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(100.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("average_of_3_days"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(100.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("previous_day_revenue"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("change"), new DoubleColumnType()),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("share_of_week"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.6677740863787376))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rank_in_week"),
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
                                new Column(new String("day"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(2),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("revenue"), new DoubleColumnType()),
                                new InvariantCell(new Double(50))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("cumulative_revenue"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(150.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("revenue_week_to_date"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(150.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("average_of_3_days"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(75.25))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("previous_day_revenue"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(100.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("change"), new DoubleColumnType()),
                                new InvariantCell(new Double(-50.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("share_of_week"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.33222591362126247))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rank_in_week"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
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
                                new Column(new String("day"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(3),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("revenue"), new DoubleColumnType()),
                                new InvariantCell(new Double(200))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("cumulative_revenue"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(350.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("revenue_week_to_date"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(200))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("average_of_3_days"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(116.83333333333333))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("previous_day_revenue"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(50))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("change"), new DoubleColumnType()),
                                new InvariantCell(new Double(150))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("share_of_week"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.2959674435812061))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rank_in_week"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
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
                                new Column(new String("day"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(4),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("revenue"), new DoubleColumnType()),
                                new InvariantCell(new Double(75.25))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("cumulative_revenue"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(425.75))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("revenue_week_to_date"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(275.25))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("average_of_3_days"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(108.41666666666667))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("previous_day_revenue"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(200))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("change"), new DoubleColumnType()),
                                new InvariantCell(new Double(-124.75))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("share_of_week"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.11135775064742878))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rank_in_week"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(4))
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
                                new Column(new String("day"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(5),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("revenue"), new DoubleColumnType()),
                                new InvariantCell(new Double(300))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("cumulative_revenue"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(725.75))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("revenue_week_to_date"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(575.25))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("average_of_3_days"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(191.75))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("previous_day_revenue"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(75.25))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("change"), new DoubleColumnType()),
                                new InvariantCell(new Double(224.75))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("share_of_week"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.4439511653718091))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rank_in_week"),
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
                                new Column(new String("day"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(6),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("revenue"), new DoubleColumnType()),
                                new InvariantCell(new Double(100.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("cumulative_revenue"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(826.25))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("revenue_week_to_date"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(675.75))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("average_of_3_days"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(158.58333333333334))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("previous_day_revenue"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(300))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("change"), new DoubleColumnType()),
                                new InvariantCell(new Double(-199.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("share_of_week"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.14872364039955605))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rank_in_week"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(3))
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
