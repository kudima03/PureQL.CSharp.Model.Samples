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
using Double = Pure.Primitives.Number.Double;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Aggregates;

/// <summary>
/// Selects, from a grouped subquery daily_revenue (the orders of
/// schema_with_foreign_keys.orders placed on or after 2024-06-02, grouped by placed_on,
/// with the days since 2024-06-02 as day and the sum of order_total as revenue), a total
/// of the count of days, the regressionSlope and regressionIntercept of revenue on day,
/// its regressionRSquared, and the revenue the line predicts for day 7.
/// </summary>
public sealed record DailyRevenueTrendQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainTotalQuery(
            subqueries:
            [
                new SubqueryQuery(
                    "daily_revenue",
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
                        where: new GreaterThanOrEqualDateRow(
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
                            new LiteralDate(new DateOnly(2024, 6, 2))
                        ),
                        groupBy:
                        [
                            new GroupKey(
                                "placed_on",
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
                            new SelectItemGroup(
                                "day",
                                new DateDiffDaysIntegerGroup(
                                    new KeyDate("placed_on"),
                                    new LiteralDate(new DateOnly(2024, 6, 2))
                                )
                            ),
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
                        ]
                    )
                ),
            ],
            from: new FromSubquery("daily_revenue"),
            aggregate:
            [
                new SelectItemTotal("days", new CountTotal()),
                new SelectItemTotal(
                    "growth_per_day",
                    new RegressionSlopeTotal(
                        new FieldDecimal("daily_revenue", "revenue"),
                        new FieldInteger("daily_revenue", "day")
                    )
                ),
                new SelectItemTotal(
                    "revenue_on_the_first_day",
                    new RegressionInterceptTotal(
                        new FieldDecimal("daily_revenue", "revenue"),
                        new FieldInteger("daily_revenue", "day")
                    )
                ),
                new SelectItemTotal(
                    "share_of_variance_explained",
                    new RegressionRSquaredTotal(
                        new FieldDecimal("daily_revenue", "revenue"),
                        new FieldInteger("daily_revenue", "day")
                    )
                ),
                new SelectItemTotal(
                    "forecast",
                    new AddDecimalNullableTotal([
                        new RegressionInterceptTotal(
                            new FieldDecimal("daily_revenue", "revenue"),
                            new FieldInteger("daily_revenue", "day")
                        ),
                        new MultiplyDecimalNullableTotal([
                            new RegressionSlopeTotal(
                                new FieldDecimal("daily_revenue", "revenue"),
                                new FieldInteger("daily_revenue", "day")
                            ),
                            new LiteralInteger(7),
                        ]),
                    ])
                ),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and
    /// <see cref="AuditSchemaDataSet"/>, in no particular order.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("days"), new LongColumnType()),
                    new Column(new String("growth_per_day"), new DoubleColumnType()),
                    new Column(
                        new String("revenue_on_the_first_day"),
                        new DoubleColumnType()
                    ),
                    new Column(
                        new String("share_of_variance_explained"),
                        new DoubleColumnType()
                    ),
                    new Column(new String("forecast"), new DoubleColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("days"), new LongColumnType()),
                                new InvariantCell(new Long(5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("growth_per_day"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(20.1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("revenue_on_the_first_day"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(104.95))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("share_of_variance_explained"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.0941305026537621))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("forecast"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(245.65))
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
