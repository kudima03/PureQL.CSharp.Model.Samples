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
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Aggregates;

/// <summary>
/// Selects, per order_status of schema_with_foreign_keys.orders inner joined to
/// schema_with_foreign_keys.users on order_user_id = user_id, the median order_total, its
/// continuous percentile of 0.9 and its discrete percentile of 0.5, the mode of
/// shift_start, the median placed_on, the median order_total of active users (a predicate
/// on user_active), the population and sample standard deviations of order_total and its
/// population variance, ordered by the median order_total descending, then by
/// order_status.
/// </summary>
public sealed record StatisticsPerStatusQuery
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
                    "order_status",
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
            select:
            [
                new SelectItemGroup("order_status", new KeyString("order_status")),
                new SelectItemGroup(
                    "median_total",
                    new MedianDecimalGroup(
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
                    "ninth_decile_total",
                    new PercentileContinuousDecimalGroup(
                        new FieldDecimal(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrdersTable().Name,
                                ]
                            ).TextValue,
                            new OrderTotalColumn().Name.TextValue
                        ),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemGroup(
                    "largest_total_of_the_lower_half",
                    new PercentileDiscreteDecimalGroup(
                        new FieldDecimal(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrdersTable().Name,
                                ]
                            ).TextValue,
                            new OrderTotalColumn().Name.TextValue
                        ),
                        new LiteralDecimal(0.5m)
                    )
                ),
                new SelectItemGroup(
                    "usual_shift_start",
                    new ModeTimeGroup(
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
                new SelectItemGroup(
                    "median_placed_on",
                    new MedianDateGroup(
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
                    )
                ),
                new SelectItemGroup(
                    "median_active_user_total",
                    new MedianDecimalNullableGroup(
                        new FieldDecimal(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrdersTable().Name,
                                ]
                            ).TextValue,
                            new OrderTotalColumn().Name.TextValue
                        ),
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
                    )
                ),
                new SelectItemGroup(
                    "spread",
                    new StandardDeviationPopulationDecimalGroup(
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
                    "spread_estimate",
                    new StandardDeviationSampleGroup(
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
                    "variance",
                    new VariancePopulationDecimalGroup(
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
            ],
            orderBy:
            [
                new OrderItemGroupExpression(
                    new MedianDecimalGroup(
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
                    ),
                    SortDirection.Desc
                ),
                new OrderItemGroupExpression(new KeyString("order_status")),
            ]
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
                    new OrderStatusColumn(),
                    new Column(new String("median_total"), new DoubleColumnType()),
                    new Column(new String("ninth_decile_total"), new DoubleColumnType()),
                    new Column(
                        new String("largest_total_of_the_lower_half"),
                        new DoubleColumnType()
                    ),
                    new Column(new String("usual_shift_start"), new TimeColumnType()),
                    new Column(new String("median_placed_on"), new DateColumnType()),
                    new Column(
                        new String("median_active_user_total"),
                        new DoubleColumnType()
                    ),
                    new Column(new String("spread"), new DoubleColumnType()),
                    new Column(new String("spread_estimate"), new DoubleColumnType()),
                    new Column(new String("variance"), new DoubleColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new OrderStatusColumn(),
                                new InvariantCell(new String("shipped"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("median_total"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(200))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("ninth_decile_total"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(280))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("largest_total_of_the_lower_half"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(200))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("usual_shift_start"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(
                                    new Time(new UShort(9), new UShort(0), new UShort(0))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("median_placed_on"),
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
                                    new String("median_active_user_total"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(200.25))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("spread"), new DoubleColumnType()),
                                new InvariantCell(new Double(81.44561921238544))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("spread_estimate"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(99.75010442768135))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("variance"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(6633.388888888889))
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
                                new OrderStatusColumn(),
                                new InvariantCell(new String("cancelled"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("median_total"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(75.25))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("ninth_decile_total"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(75.25))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("largest_total_of_the_lower_half"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(75.25))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("usual_shift_start"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(
                                    new Time(new UShort(9), new UShort(0), new UShort(0))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("median_placed_on"),
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
                                    new String("median_active_user_total"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(75.25))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("spread"), new DoubleColumnType()),
                                new InvariantCell(new Double(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("spread_estimate"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("variance"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0))
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
                                new OrderStatusColumn(),
                                new InvariantCell(new String("pending"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("median_total"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(75.25))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("ninth_decile_total"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(95.45))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("largest_total_of_the_lower_half"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(50))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("usual_shift_start"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(
                                    new Time(new UShort(9), new UShort(0), new UShort(0))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("median_placed_on"),
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
                                    new String("median_active_user_total"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(75.25))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("spread"), new DoubleColumnType()),
                                new InvariantCell(new Double(25.25))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("spread_estimate"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(35.70889244992065))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("variance"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(637.5625))
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
