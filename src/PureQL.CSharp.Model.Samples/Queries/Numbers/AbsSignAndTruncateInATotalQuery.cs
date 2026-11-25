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
/// Totals over schema_with_foreign_keys.orders in one row: the absolute value and the
/// sign of the difference between the sum of order_total over pending orders and over the
/// other orders, the average of order_total truncated to 2 digits and to a whole number,
/// the absolute value of the smallest order_total, and the sign of the number of orders
/// minus 10.
/// </summary>
public sealed record AbsSignAndTruncateInATotalQuery
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
            aggregate:
            [
                new SelectItemTotal(
                    "imbalance",
                    new AbsDecimalTotal(
                        new SubtractDecimalTotal([
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
                                ),
                                new EqualStringRow(
                                    new FieldString(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new OrdersTable().Name,
                                            ]
                                        ).TextValue,
                                        new OrderStatusColumn().Name.TextValue
                                    ),
                                    new LiteralString("pending")
                                )
                            ),
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
                                ),
                                new NotRow(
                                    new EqualStringRow(
                                        new FieldString(
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new RelationalSchemaWithForeignKeys().Name,
                                                    new OrdersTable().Name,
                                                ]
                                            ).TextValue,
                                            new OrderStatusColumn().Name.TextValue
                                        ),
                                        new LiteralString("pending")
                                    )
                                )
                            ),
                        ])
                    )
                ),
                new SelectItemTotal(
                    "mostly",
                    new SignIntegerTotal(
                        new SubtractDecimalTotal([
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
                                ),
                                new EqualStringRow(
                                    new FieldString(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new OrdersTable().Name,
                                            ]
                                        ).TextValue,
                                        new OrderStatusColumn().Name.TextValue
                                    ),
                                    new LiteralString("pending")
                                )
                            ),
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
                                ),
                                new NotRow(
                                    new EqualStringRow(
                                        new FieldString(
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new RelationalSchemaWithForeignKeys().Name,
                                                    new OrdersTable().Name,
                                                ]
                                            ).TextValue,
                                            new OrderStatusColumn().Name.TextValue
                                        ),
                                        new LiteralString("pending")
                                    )
                                )
                            ),
                        ])
                    )
                ),
                new SelectItemTotal(
                    "average_to_cents",
                    new TruncateDecimalNullableTotal(
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
                        new LiteralInteger(2)
                    )
                ),
                new SelectItemTotal(
                    "whole_average",
                    new TruncateWithoutDigitsIntegerNullableTotal(
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
                    "smallest_size",
                    new AbsDecimalNullableTotal(
                        new MinDecimalNullableTotal(
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
                    "orders_against_target",
                    new SignIntegerTotal(
                        new SubtractIntegerTotal([
                            new CountTotal(),
                            new LiteralInteger(10),
                        ])
                    )
                ),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over <see
    /// cref="SchemaDataSetWithForeignKeys"/> and <see cref="AuditSchemaDataSet"/>, in no
    /// particular order.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("imbalance"), new DoubleColumnType()),
                    new Column(new String("mostly"), new LongColumnType()),
                    new Column(new String("average_to_cents"), new DoubleColumnType()),
                    new Column(new String("whole_average"), new LongColumnType()),
                    new Column(new String("smallest_size"), new DoubleColumnType()),
                    new Column(new String("orders_against_target"), new LongColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("imbalance"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(525.25))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("mostly"), new LongColumnType()),
                                new InvariantCell(new Long(-1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("average_to_cents"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(137.7))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("whole_average"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(137))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("smallest_size"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(50))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("orders_against_target"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(-1))
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
