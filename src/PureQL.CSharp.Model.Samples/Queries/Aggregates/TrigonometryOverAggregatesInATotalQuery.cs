using Pure.Collections.Generic;
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
using PureQL.CSharp.Model.RowExpressions;
using PureQL.CSharp.Model.TotalExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Aggregates;

/// <summary>
/// Selects, from the schema_with_foreign_keys.orders whose sin of the hour of placed_at
/// times pi divided by 12 is at least -0.3, one row of totals: the atan2 of the sums of
/// the sines and of the cosines of that hour angle, the cos of its smallest value
/// (nullable), the atan of the average order_total (nullable), pi times the count, and
/// the sum of the tan of the hour angle divided by 4.
/// </summary>
public sealed record TrigonometryOverAggregatesInATotalQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainTotalQuery(
            from: new FromEntity(
                new JoinedString(
                    new DotString(),
                    [new RelationalSchemaWithForeignKeys().Name, new OrdersTable().Name]
                ).TextValue
            ),
            where: new GreaterThanOrEqualDecimalRow(
                new SinDecimalRow(
                    new MultiplyDecimalRow([
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
                            )
                        ),
                        new DivideDecimalRow([new PiRow(), new LiteralInteger(12)]),
                    ])
                ),
                new LiteralDecimal(-0.3m)
            ),
            aggregate:
            [
                new SelectItemTotal(
                    "mean_hour_angle",
                    new Atan2DecimalTotal(
                        new SumDecimalTotal(
                            new SinDecimalRow(
                                new MultiplyDecimalRow([
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
                                        )
                                    ),
                                    new DivideDecimalRow([
                                        new PiRow(),
                                        new LiteralInteger(12),
                                    ]),
                                ])
                            )
                        ),
                        new SumDecimalTotal(
                            new CosDecimalRow(
                                new MultiplyDecimalRow([
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
                                        )
                                    ),
                                    new DivideDecimalRow([
                                        new PiRow(),
                                        new LiteralInteger(12),
                                    ]),
                                ])
                            )
                        )
                    )
                ),
                new SelectItemTotal(
                    "largest_cosine",
                    new CosDecimalNullableTotal(
                        new MinDecimalNullableTotal(
                            new MultiplyDecimalRow([
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
                                    )
                                ),
                                new DivideDecimalRow([
                                    new PiRow(),
                                    new LiteralInteger(12),
                                ]),
                            ])
                        )
                    )
                ),
                new SelectItemTotal(
                    "angle_of_the_average",
                    new AtanDecimalNullableTotal(
                        new AverageDecimalNullableTotal(
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
                    )
                ),
                new SelectItemTotal(
                    "pi_times_count",
                    new MultiplyDecimalTotal([new PiTotal(), new CountTotal()])
                ),
                new SelectItemTotal(
                    "running",
                    new SumDecimalTotal(
                        new TanDecimalRow(
                            new DivideDecimalRow([
                                new MultiplyDecimalRow([
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
                                        )
                                    ),
                                    new DivideDecimalRow([
                                        new PiRow(),
                                        new LiteralInteger(12),
                                    ]),
                                ]),
                                new LiteralInteger(4),
                            ])
                        )
                    )
                ),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and
    /// <see cref="AuditSchemaDataSet"/>, in no particular order.
    /// The trigonometric functions of the specification are computed with Python
    /// decimal at 120 digits, each result rounded as the specification rounds a
    /// decimal, and then written as a double.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("mean_hour_angle"), new DoubleColumnType()),
                    new Column(new String("largest_cosine"), new DoubleColumnType()),
                    new Column(
                        new String("angle_of_the_average"),
                        new DoubleColumnType()
                    ),
                    new Column(new String("pi_times_count"), new DoubleColumnType()),
                    new Column(new String("running"), new DoubleColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("mean_hour_angle"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(3.0106929596902186))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("largest_cosine"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-0.8660254037844386))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("angle_of_the_average"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1.561401418248648))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("pi_times_count"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(12.566370614359172))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("running"), new DoubleColumnType()),
                                new InvariantCell(new Double(3.784584909139266))
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
