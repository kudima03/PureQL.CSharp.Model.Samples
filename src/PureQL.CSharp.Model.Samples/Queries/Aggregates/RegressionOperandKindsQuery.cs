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
/// Selects, from schema_with_foreign_keys.order_items inner joined to
/// schema_with_foreign_keys.products on item_product_id = product_id, eleven regression
/// aggregates over all rows: the regressionSlope of product_price on itself, of the
/// rounded item_qty on product_price and of product_price on the rounded item_qty, the
/// regressionIntercept of the rounded item_qty on that quantity integer-divided by 2, the
/// regressionSlope with a nullable y and with a nullable x (item_qty where it is greater
/// than 1, otherwise null), the regressionRSquared of product_price on the rounded
/// item_qty with a predicate of the rounded item_qty greater than 1, the regressionSlope
/// of the literals 2.5 and 7, the regressionIntercept of two typed nulls, the
/// regressionRSquared of product_price on the literal 0.5 and the regressionSlope of
/// product_price on the literal 1.
/// </summary>
public sealed record RegressionOperandKindsQuery
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
                    new RegressionSlopeProjection(
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
                    new RegressionSlopeProjection(
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
                    "of_a_decimal_and_an_integer",
                    new RegressionSlopeProjection(
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
                    "of_integers",
                    new RegressionInterceptProjection(
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
                            new LiteralInteger(2)
                        )
                    )
                ),
                new SelectItemProjection(
                    "of_a_nullable_y",
                    new RegressionSlopeProjection(
                        new IfDecimalNullableRow(
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
                            new LiteralDecimalNullable(),
                            new GreaterThanDecimalRow(
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
                                new LiteralDecimal(1m)
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
                    "of_a_nullable_x",
                    new RegressionSlopeProjection(
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
                        new IfDecimalNullableRow(
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
                            new LiteralDecimalNullable(),
                            new GreaterThanDecimalRow(
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
                                new LiteralDecimal(1m)
                            )
                        )
                    )
                ),
                new SelectItemProjection(
                    "with_a_predicate",
                    new RegressionRSquaredProjection(
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
                        predicate: new GreaterThanDecimalRow(
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
                    new RegressionSlopeProjection(
                        new LiteralDecimal(2.5m),
                        new LiteralInteger(7)
                    )
                ),
                new SelectItemProjection(
                    "of_nulls",
                    new RegressionInterceptProjection(
                        new LiteralDecimalNullable(),
                        new LiteralIntegerNullable()
                    )
                ),
                new SelectItemProjection(
                    "of_a_parameter",
                    new RegressionRSquaredProjection(
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
                new SelectItemProjection(
                    "of_a_constant_x",
                    new RegressionSlopeProjection(
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
                        new LiteralInteger(1)
                    )
                ),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over <see
    /// cref="SchemaDataSetWithForeignKeys"/> and <see cref="AuditSchemaDataSet"/>, in no
    /// particular order. The regression values are computed exactly in Python (Fraction,
    /// rounded as the specification rounds a decimal), as PostgreSQL computes them in
    /// double.
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
                    new Column(
                        new String("of_a_decimal_and_an_integer"),
                        new DoubleColumnType()
                    ),
                    new Column(new String("of_integers"), new DoubleColumnType()),
                    new Column(new String("of_a_nullable_y"), new DoubleColumnType()),
                    new Column(new String("of_a_nullable_x"), new DoubleColumnType()),
                    new Column(new String("with_a_predicate"), new DoubleColumnType()),
                    new Column(new String("of_literals"), new DoubleColumnType()),
                    new Column(new String("of_nulls"), new DoubleColumnType()),
                    new Column(new String("of_a_parameter"), new DoubleColumnType()),
                    new Column(new String("of_a_constant_x"), new DoubleColumnType()),
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
                                new InvariantCell(new Double(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_an_integer_and_a_decimal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-0.23871482224931695))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_decimal_and_an_integer"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-3.411714285714286))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_integers"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.75))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_nullable_y"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-0.4553734061930783))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_nullable_x"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-1.9607142857142856))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("with_a_predicate"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.8928571428571429))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_literals"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_nulls"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_parameter"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_constant_x"),
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
                                new InvariantCell(new Double(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_an_integer_and_a_decimal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-0.23871482224931695))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_decimal_and_an_integer"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-3.411714285714286))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_integers"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.75))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_nullable_y"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-0.4553734061930783))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_nullable_x"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-1.9607142857142856))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("with_a_predicate"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.8928571428571429))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_literals"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_nulls"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_parameter"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_constant_x"),
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
                                new InvariantCell(new Double(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_an_integer_and_a_decimal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-0.23871482224931695))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_decimal_and_an_integer"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-3.411714285714286))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_integers"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.75))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_nullable_y"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-0.4553734061930783))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_nullable_x"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-1.9607142857142856))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("with_a_predicate"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.8928571428571429))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_literals"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_nulls"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_parameter"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_constant_x"),
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
                                new InvariantCell(new Double(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_an_integer_and_a_decimal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-0.23871482224931695))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_decimal_and_an_integer"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-3.411714285714286))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_integers"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.75))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_nullable_y"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-0.4553734061930783))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_nullable_x"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-1.9607142857142856))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("with_a_predicate"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.8928571428571429))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_literals"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_nulls"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_parameter"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_constant_x"),
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
