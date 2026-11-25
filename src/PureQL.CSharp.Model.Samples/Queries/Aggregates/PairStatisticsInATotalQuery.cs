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
/// Totals over schema_with_foreign_keys.order_items inner joined to
/// schema_with_foreign_keys.products on item_product_id = product_id, where the unit
/// price is product_price and the quantity is item_qty rounded to a whole number: the
/// population covariance, the sample covariance and the correlation of the two, their
/// correlation over quantities above 1 (a predicate), r_squared (that correlation to the
/// power 2), slope (the population covariance divided by the population variance of
/// product_price, which nullIfEqual turns into null at 0) and the correlation of
/// product_price times a nullable quantity (item_qty, null where it is 2) with the
/// quantity.
/// </summary>
public sealed record PairStatisticsInATotalQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainTotalQuery(
            from: new FromEntity(
                new JoinedString(
                    new DotString(),
                    [
                        new RelationalSchemaWithForeignKeys().Name,
                        new OrderItemsTable().Name,
                    ]
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
                            new ProductsTable().Name,
                        ]
                    ).TextValue,
                    new EqualUuidRow(
                        new FieldUuid(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrderItemsTable().Name,
                                ]
                            ).TextValue,
                            new ItemProductIdColumn().Name.TextValue
                        ),
                        new FieldUuid(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new ProductsTable().Name,
                                ]
                            ).TextValue,
                            new ProductIdColumn().Name.TextValue
                        )
                    )
                ),
            ],
            aggregate:
            [
                new SelectItemTotal(
                    "covariance",
                    new CovariancePopulationTotal(
                        new FieldDecimal(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new ProductsTable().Name,
                                ]
                            ).TextValue,
                            new ProductPriceColumn().Name.TextValue
                        ),
                        new RoundWithoutDigitsIntegerRow(
                            new FieldDecimal(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new OrderItemsTable().Name,
                                    ]
                                ).TextValue,
                                new ItemQtyColumn().Name.TextValue
                            )
                        )
                    )
                ),
                new SelectItemTotal(
                    "sample_covariance",
                    new CovarianceSampleTotal(
                        new FieldDecimal(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new ProductsTable().Name,
                                ]
                            ).TextValue,
                            new ProductPriceColumn().Name.TextValue
                        ),
                        new RoundWithoutDigitsIntegerRow(
                            new FieldDecimal(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new OrderItemsTable().Name,
                                    ]
                                ).TextValue,
                                new ItemQtyColumn().Name.TextValue
                            )
                        )
                    )
                ),
                new SelectItemTotal(
                    "correlation",
                    new CorrelationTotal(
                        new FieldDecimal(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new ProductsTable().Name,
                                ]
                            ).TextValue,
                            new ProductPriceColumn().Name.TextValue
                        ),
                        new RoundWithoutDigitsIntegerRow(
                            new FieldDecimal(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new OrderItemsTable().Name,
                                    ]
                                ).TextValue,
                                new ItemQtyColumn().Name.TextValue
                            )
                        )
                    )
                ),
                new SelectItemTotal(
                    "correlation_of_large_lines",
                    new CorrelationTotal(
                        new FieldDecimal(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new ProductsTable().Name,
                                ]
                            ).TextValue,
                            new ProductPriceColumn().Name.TextValue
                        ),
                        new RoundWithoutDigitsIntegerRow(
                            new FieldDecimal(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new OrderItemsTable().Name,
                                    ]
                                ).TextValue,
                                new ItemQtyColumn().Name.TextValue
                            )
                        ),
                        new GreaterThanDecimalRow(
                            new RoundWithoutDigitsIntegerRow(
                                new FieldDecimal(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new OrderItemsTable().Name,
                                        ]
                                    ).TextValue,
                                    new ItemQtyColumn().Name.TextValue
                                )
                            ),
                            new LiteralInteger(1)
                        )
                    )
                ),
                new SelectItemTotal(
                    "r_squared",
                    new PowerDecimalNullableTotal(
                        new CorrelationTotal(
                            new FieldDecimal(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new ProductsTable().Name,
                                    ]
                                ).TextValue,
                                new ProductPriceColumn().Name.TextValue
                            ),
                            new RoundWithoutDigitsIntegerRow(
                                new FieldDecimal(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new OrderItemsTable().Name,
                                        ]
                                    ).TextValue,
                                    new ItemQtyColumn().Name.TextValue
                                )
                            )
                        ),
                        new LiteralInteger(2)
                    )
                ),
                new SelectItemTotal(
                    "slope",
                    new DivideDecimalNullableTotal([
                        new CovariancePopulationTotal(
                            new FieldDecimal(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new ProductsTable().Name,
                                    ]
                                ).TextValue,
                                new ProductPriceColumn().Name.TextValue
                            ),
                            new RoundWithoutDigitsIntegerRow(
                                new FieldDecimal(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new OrderItemsTable().Name,
                                        ]
                                    ).TextValue,
                                    new ItemQtyColumn().Name.TextValue
                                )
                            )
                        ),
                        new NullIfEqualDecimalNullableTotal(
                            new VariancePopulationTotal(
                                new FieldDecimal(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new ProductsTable().Name,
                                        ]
                                    ).TextValue,
                                    new ProductPriceColumn().Name.TextValue
                                )
                            ),
                            new LiteralInteger(0)
                        ),
                    ])
                ),
                new SelectItemTotal(
                    "with_a_nullable_operand",
                    new CorrelationTotal(
                        new MultiplyDecimalNullableRow([
                            new FieldDecimal(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new ProductsTable().Name,
                                    ]
                                ).TextValue,
                                new ProductPriceColumn().Name.TextValue
                            ),
                            new NullIfEqualDecimalNullableRow(
                                new FieldDecimal(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new OrderItemsTable().Name,
                                        ]
                                    ).TextValue,
                                    new ItemQtyColumn().Name.TextValue
                                ),
                                new LiteralInteger(2)
                            ),
                        ]),
                        new RoundWithoutDigitsIntegerRow(
                            new FieldDecimal(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new OrderItemsTable().Name,
                                    ]
                                ).TextValue,
                                new ItemQtyColumn().Name.TextValue
                            )
                        )
                    )
                ),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and
    /// <see cref="AuditSchemaDataSet"/>, in no particular order.
    /// The statistics are computed exactly with Python decimal from the fixture rows and
    /// rounded once as the specification rounds a decimal, since PostgreSQL computes
    /// covariances and correlations in double precision.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("covariance"), new DoubleColumnType()),
                    new Column(new String("sample_covariance"), new DoubleColumnType()),
                    new Column(new String("correlation"), new DoubleColumnType()),
                    new Column(
                        new String("correlation_of_large_lines"),
                        new DoubleColumnType()
                    ),
                    new Column(new String("r_squared"), new DoubleColumnType()),
                    new Column(new String("slope"), new DoubleColumnType()),
                    new Column(
                        new String("with_a_nullable_operand"),
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
                                new Column(
                                    new String("covariance"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-7.463125))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sample_covariance"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-9.950833333333334))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("correlation"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-0.902455965285698))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("correlation_of_large_lines"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-0.944911182523068))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("r_squared"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.8144267692797411))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("slope"), new DoubleColumnType()),
                                new InvariantCell(new Double(-0.23871482224931695))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("with_a_nullable_operand"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.24174776130126538))
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
