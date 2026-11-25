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
using PureQL.CSharp.Model.ProjectionExpressions;
using PureQL.CSharp.Model.RowExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Aggregates;

/// <summary>
/// Selects from schema_with_foreign_keys.order_items inner joined to
/// schema_with_foreign_keys.products on item_product_id = product_id, over all rows, the
/// population covariance of product_price with itself, of the whole-number item_qty and
/// product_price, of the quantity and its returned part (the quantity integer-divided by
/// 3), of a nullable quantity (item_qty, null where it is 2) as the left and as the right
/// operand, of price and quantity for quantities above 1 (a predicate) and of the
/// literals 2.5 and 7, then the sample covariance of price and quantity, the correlation
/// of product_price with itself and the correlation of product_price with the literal
/// 0.5.
/// </summary>
public sealed record PairStatisticsOperandKindsQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithoutPagination(
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
            select:
            [
                new SelectItemProjection(
                    "of_decimals",
                    new CovariancePopulationDecimalProjection(
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
                    )
                ),
                new SelectItemProjection(
                    "of_an_integer_and_a_decimal",
                    new CovariancePopulationDecimalProjection(
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
                    )
                ),
                new SelectItemProjection(
                    "of_integers",
                    new CovariancePopulationDecimalProjection(
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
                        new IntegerDivideIntegerRow(
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
                            new LiteralInteger(3)
                        )
                    )
                ),
                new SelectItemProjection(
                    "of_a_nullable_left",
                    new CovariancePopulationDecimalNullableProjection(
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
                    )
                ),
                new SelectItemProjection(
                    "of_a_nullable_right",
                    new CovariancePopulationDecimalNullableProjection(
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
                        )
                    )
                ),
                new SelectItemProjection(
                    "with_a_predicate",
                    new CovariancePopulationDecimalNullableProjection(
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
                new SelectItemProjection(
                    "of_literals",
                    new CovariancePopulationDecimalProjection(
                        new LiteralDecimal(2.5m),
                        new LiteralInteger(7)
                    )
                ),
                new SelectItemProjection(
                    "sample_of_non_null_operands",
                    new CovarianceSampleProjection(
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
                new SelectItemProjection(
                    "correlation_of_a_column_with_itself",
                    new CorrelationProjection(
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
                    )
                ),
                new SelectItemProjection(
                    "of_a_parameter",
                    new CorrelationProjection(
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
                        new LiteralDecimal(0.5m)
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
                    new Column(new String("of_decimals"), new DoubleColumnType()),
                    new Column(
                        new String("of_an_integer_and_a_decimal"),
                        new DoubleColumnType()
                    ),
                    new Column(new String("of_integers"), new DoubleColumnType()),
                    new Column(new String("of_a_nullable_left"), new DoubleColumnType()),
                    new Column(new String("of_a_nullable_right"), new DoubleColumnType()),
                    new Column(new String("with_a_predicate"), new DoubleColumnType()),
                    new Column(new String("of_literals"), new DoubleColumnType()),
                    new Column(
                        new String("sample_of_non_null_operands"),
                        new DoubleColumnType()
                    ),
                    new Column(
                        new String("correlation_of_a_column_with_itself"),
                        new DoubleColumnType()
                    ),
                    new Column(new String("of_a_parameter"), new DoubleColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_decimals"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(31.26376875))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_an_integer_and_a_decimal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-7.463125))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_integers"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.625))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_nullable_left"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-10.326666666666666))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_nullable_right"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-10.326666666666666))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("with_a_predicate"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-3.05))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_literals"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sample_of_non_null_operands"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-9.950833333333334))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("correlation_of_a_column_with_itself"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_parameter"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
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
                                new Column(
                                    new String("of_decimals"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(31.26376875))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_an_integer_and_a_decimal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-7.463125))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_integers"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.625))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_nullable_left"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-10.326666666666666))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_nullable_right"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-10.326666666666666))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("with_a_predicate"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-3.05))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_literals"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sample_of_non_null_operands"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-9.950833333333334))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("correlation_of_a_column_with_itself"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_parameter"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
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
                                new Column(
                                    new String("of_decimals"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(31.26376875))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_an_integer_and_a_decimal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-7.463125))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_integers"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.625))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_nullable_left"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-10.326666666666666))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_nullable_right"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-10.326666666666666))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("with_a_predicate"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-3.05))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_literals"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sample_of_non_null_operands"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-9.950833333333334))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("correlation_of_a_column_with_itself"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_parameter"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
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
                                new Column(
                                    new String("of_decimals"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(31.26376875))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_an_integer_and_a_decimal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-7.463125))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_integers"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.625))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_nullable_left"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-10.326666666666666))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_nullable_right"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-10.326666666666666))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("with_a_predicate"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-3.05))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_literals"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sample_of_non_null_operands"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-9.950833333333334))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("correlation_of_a_column_with_itself"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_parameter"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
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
