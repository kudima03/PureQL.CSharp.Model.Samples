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
using PureQL.CSharp.Model.RowExpressions;
using PureQL.CSharp.Model.TotalExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Numbers;

/// <summary>
/// Selects, from schema_with_foreign_keys.orders where order_total equals itself rounded
/// half to even to 1 digit, the total of: the sum of order_total rounded half to even to
/// -3 digits as revenue_to_thousands, the average of order_total rounded half to even to
/// 2 digits as average_to_cents, the median of order_total rounded half to even to a
/// whole number as median_whole, the sum of order_total rounded half to even to a whole
/// number as sum_of_rounded and the max of the same as largest_rounded.
/// </summary>
public sealed record RoundHalfToEvenInATotalQuery
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
            where: new EqualDecimalRow(
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
                new RoundDecimalRow(
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
                    new LiteralInteger(1),
                    RoundingMode.HalfEven
                )
            ),
            aggregate:
            [
                new SelectItemTotal(
                    "revenue_to_thousands",
                    new RoundDecimalTotal(
                        new SumDecimalTotal(
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
                        new LiteralInteger(-3),
                        RoundingMode.HalfEven
                    )
                ),
                new SelectItemTotal(
                    "average_to_cents",
                    new RoundDecimalNullableTotal(
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
                        ),
                        new LiteralInteger(2),
                        RoundingMode.HalfEven
                    )
                ),
                new SelectItemTotal(
                    "median_whole",
                    new RoundWithoutDigitsIntegerNullableTotal(
                        new MedianDecimalNullableTotal(
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
                        RoundingMode.HalfEven
                    )
                ),
                new SelectItemTotal(
                    "sum_of_rounded",
                    new SumIntegerTotal(
                        new RoundWithoutDigitsIntegerRow(
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
                            RoundingMode.HalfEven
                        )
                    )
                ),
                new SelectItemTotal(
                    "largest_rounded",
                    new MaxIntegerNullableTotal(
                        new RoundWithoutDigitsIntegerRow(
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
                            RoundingMode.HalfEven
                        )
                    )
                ),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and
    /// <see cref="AuditSchemaDataSet"/>, in this order.
    /// An odd number of orders is kept, so the median is the middle value, which
    /// PostgreSQL percentile_disc returns exactly.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(
                        new String("revenue_to_thousands"),
                        new DoubleColumnType()
                    ),
                    new Column(new String("average_to_cents"), new DoubleColumnType()),
                    new Column(new String("median_whole"), new LongColumnType()),
                    new Column(new String("sum_of_rounded"), new LongColumnType()),
                    new Column(new String("largest_rounded"), new LongColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("revenue_to_thousands"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1000))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("average_to_cents"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(150.2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("median_whole"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(100))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sum_of_rounded"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(750))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("largest_rounded"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(300))
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
