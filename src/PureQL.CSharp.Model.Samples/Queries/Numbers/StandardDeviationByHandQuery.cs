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

namespace PureQL.CSharp.Model.Samples.Queries.Numbers;

/// <summary>
/// Totals over the shipped orders of schema_with_foreign_keys.orders in one row: the
/// average of order_total; the variance, the mean of the squares of order_total less the
/// square of the mean, floored at 0; its square root; the square root of the variance
/// over the mean (nullIfEqual with 0); the exp of the average of the ln of the
/// order_total above 0; the square root of the mean of the squares; and the logarithm to
/// base 2 of the greatest of the count of orders and 1.
/// </summary>
public sealed record StandardDeviationByHandQuery
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
            where: new EqualStringRow(
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
                new LiteralString("shipped")
            ),
            aggregate:
            [
                new SelectItemTotal(
                    "mean",
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
                ),
                new SelectItemTotal(
                    "variance",
                    new GreatestDecimalNullableTotal([
                        new SubtractDecimalNullableTotal([
                            new AverageDecimalNullableTotal(
                                new PowerDecimalRow(
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
                                    new LiteralInteger(2)
                                )
                            ),
                            new PowerDecimalNullableTotal(
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
                            ),
                        ]),
                        new LiteralInteger(0),
                    ])
                ),
                new SelectItemTotal(
                    "standard_deviation",
                    new SqrtDecimalNullableTotal(
                        new GreatestDecimalNullableTotal([
                            new SubtractDecimalNullableTotal([
                                new AverageDecimalNullableTotal(
                                    new PowerDecimalRow(
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
                                        new LiteralInteger(2)
                                    )
                                ),
                                new PowerDecimalNullableTotal(
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
                                ),
                            ]),
                            new LiteralInteger(0),
                        ])
                    )
                ),
                new SelectItemTotal(
                    "coefficient_of_variation",
                    new DivideDecimalNullableTotal([
                        new SqrtDecimalNullableTotal(
                            new GreatestDecimalNullableTotal([
                                new SubtractDecimalNullableTotal([
                                    new AverageDecimalNullableTotal(
                                        new PowerDecimalRow(
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
                                            new LiteralInteger(2)
                                        )
                                    ),
                                    new PowerDecimalNullableTotal(
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
                                    ),
                                ]),
                                new LiteralInteger(0),
                            ])
                        ),
                        new NullIfEqualDecimalNullableTotal(
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
                            new LiteralInteger(0)
                        ),
                    ])
                ),
                new SelectItemTotal(
                    "geometric_mean",
                    new ExpDecimalNullableTotal(
                        new AverageDecimalNullableTotal(
                            new LnDecimalRow(
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
                            new GreaterThanDecimalRow(
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
                                new LiteralInteger(0)
                            )
                        )
                    )
                ),
                new SelectItemTotal(
                    "root_mean_square",
                    new SqrtDecimalNullableTotal(
                        new AverageDecimalNullableTotal(
                            new PowerDecimalRow(
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
                                new LiteralInteger(2)
                            )
                        )
                    )
                ),
                new SelectItemTotal(
                    "orders_as_a_power_of_two",
                    new LogDecimalTotal(
                        new GreatestIntegerTotal([
                            new CountTotal(),
                            new LiteralInteger(1),
                        ]),
                        new LiteralInteger(2)
                    )
                ),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over <see
    /// cref="SchemaDataSetWithForeignKeys"/> and <see cref="AuditSchemaDataSet"/>, in no
    /// particular order. Every value is computed exactly with Python decimal at 120
    /// digits, rounding each operator result as the specification rounds a decimal, and
    /// checked against PostgreSQL numeric at 50 decimal places.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("mean"), new DoubleColumnType()),
                    new Column(new String("variance"), new DoubleColumnType()),
                    new Column(new String("standard_deviation"), new DoubleColumnType()),
                    new Column(
                        new String("coefficient_of_variation"),
                        new DoubleColumnType()
                    ),
                    new Column(new String("geometric_mean"), new DoubleColumnType()),
                    new Column(new String("root_mean_square"), new DoubleColumnType()),
                    new Column(
                        new String("orders_as_a_power_of_two"),
                        new DoubleColumnType()
                    ),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("mean"), new DoubleColumnType()),
                                new InvariantCell(new Double(200.16666666666666))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("variance"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(6633.388888888889))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("standard_deviation"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(81.44561921238544))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("coefficient_of_variation"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.4068890218770297))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("geometric_mean"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(182.01440935707507))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("root_mean_square"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(216.102020659996))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("orders_as_a_power_of_two"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1.584962500721156))
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
