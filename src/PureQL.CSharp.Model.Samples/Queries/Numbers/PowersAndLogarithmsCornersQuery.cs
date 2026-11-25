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
using PureQL.CSharp.Model.ProjectionExpressions;
using PureQL.CSharp.Model.RowExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Numbers;

/// <summary>
/// Selects from schema_with_foreign_keys.order_items joined to its
/// schema_with_foreign_keys.products, orders and users, with the whole units taken as the
/// round of item_qty: 2 to the power of 10, that as a whole number, product_price to the
/// power of -2, 0.5 and one over 3, the units to the power of 1.5, the nullable
/// user_score squared, the power of a power, the square root of the units and of the
/// nullable user_score, the exp of the units, the exp of the ln of the nullable
/// user_score, the ln of 10, the log10 of 0.001, and logarithms of product_price to base
/// 4, of the nullable user_score to the base of the units plus 1, and of the units to
/// base 0.5.
/// </summary>
public sealed record PowersAndLogarithmsCornersQuery
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
                new JoinEntity(
                    JoinKind.Inner,
                    new JoinedString(
                        new DotString(),
                        [
                            new RelationalSchemaWithForeignKeys().Name,
                            new OrdersTable().Name,
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
                            new ItemOrderIdColumn().Name.TextValue
                        ),
                        new FieldUuid(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrdersTable().Name,
                                ]
                            ).TextValue,
                            new OrderIdColumn().Name.TextValue
                        )
                    )
                ),
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
            select:
            [
                new SelectItemProjection(
                    "integer_operands",
                    new PowerDecimalProjection(
                        new LiteralInteger(2),
                        new LiteralInteger(10)
                    )
                ),
                new SelectItemProjection(
                    "as_an_integer",
                    new RoundWithoutDigitsIntegerProjection(
                        new PowerDecimalProjection(
                            new LiteralInteger(2),
                            new LiteralInteger(10)
                        )
                    )
                ),
                new SelectItemProjection(
                    "negative_exponent",
                    new PowerDecimalProjection(
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
                        new LiteralInteger(-2)
                    )
                ),
                new SelectItemProjection(
                    "fractional_exponent",
                    new PowerDecimalProjection(
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
                    "cube_root",
                    new PowerDecimalProjection(
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
                        new DivideDecimalProjection([
                            new LiteralInteger(1),
                            new LiteralInteger(3),
                        ])
                    )
                ),
                new SelectItemProjection(
                    "parameter_exponent",
                    new PowerDecimalProjection(
                        new RoundWithoutDigitsIntegerProjection(
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
                        new LiteralDecimal(1.5m)
                    )
                ),
                new SelectItemProjection(
                    "nullable_operand",
                    new PowerDecimalNullableProjection(
                        new FieldDecimalNullable(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue,
                            new UserScoreColumn().Name.TextValue
                        ),
                        new LiteralInteger(2)
                    )
                ),
                new SelectItemProjection(
                    "power_of_a_power",
                    new PowerDecimalProjection(
                        new PowerDecimalProjection(
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
                            new LiteralInteger(2)
                        ),
                        new LiteralDecimal(0.5m)
                    )
                ),
                new SelectItemProjection(
                    "sqrt_of_integer",
                    new SqrtDecimalProjection(
                        new RoundWithoutDigitsIntegerProjection(
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
                    "sqrt_nullable",
                    new SqrtDecimalNullableProjection(
                        new FieldDecimalNullable(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue,
                            new UserScoreColumn().Name.TextValue
                        )
                    )
                ),
                new SelectItemProjection(
                    "exp_of_integer",
                    new ExpDecimalProjection(
                        new RoundWithoutDigitsIntegerProjection(
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
                    "exp_of_ln",
                    new ExpDecimalNullableProjection(
                        new LnDecimalNullableProjection(
                            new FieldDecimalNullable(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new UsersTable().Name,
                                    ]
                                ).TextValue,
                                new UserScoreColumn().Name.TextValue
                            )
                        )
                    )
                ),
                new SelectItemProjection(
                    "ln_of_integer_literal",
                    new LnDecimalProjection(new LiteralInteger(10))
                ),
                new SelectItemProjection(
                    "log10_of_decimal_literal",
                    new Log10DecimalProjection(new LiteralDecimal(0.001m))
                ),
                new SelectItemProjection(
                    "log_to_a_parameter_base",
                    new LogDecimalProjection(
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
                        new LiteralDecimal(4m)
                    )
                ),
                new SelectItemProjection(
                    "log_to_a_computed_base",
                    new LogDecimalNullableProjection(
                        new FieldDecimalNullable(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue,
                            new UserScoreColumn().Name.TextValue
                        ),
                        new AddIntegerProjection([
                            new RoundWithoutDigitsIntegerProjection(
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
                            new LiteralInteger(1),
                        ])
                    )
                ),
                new SelectItemProjection(
                    "log_to_a_fractional_base",
                    new LogDecimalProjection(
                        new RoundWithoutDigitsIntegerProjection(
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
                        new LiteralDecimal(0.5m)
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
                    new Column(new String("integer_operands"), new DoubleColumnType()),
                    new Column(new String("as_an_integer"), new LongColumnType()),
                    new Column(new String("negative_exponent"), new DoubleColumnType()),
                    new Column(new String("fractional_exponent"), new DoubleColumnType()),
                    new Column(new String("cube_root"), new DoubleColumnType()),
                    new Column(new String("parameter_exponent"), new DoubleColumnType()),
                    new Column(new String("nullable_operand"), new DoubleColumnType()),
                    new Column(new String("power_of_a_power"), new DoubleColumnType()),
                    new Column(new String("sqrt_of_integer"), new DoubleColumnType()),
                    new Column(new String("sqrt_nullable"), new DoubleColumnType()),
                    new Column(new String("exp_of_integer"), new DoubleColumnType()),
                    new Column(new String("exp_of_ln"), new DoubleColumnType()),
                    new Column(
                        new String("ln_of_integer_literal"),
                        new DoubleColumnType()
                    ),
                    new Column(
                        new String("log10_of_decimal_literal"),
                        new DoubleColumnType()
                    ),
                    new Column(
                        new String("log_to_a_parameter_base"),
                        new DoubleColumnType()
                    ),
                    new Column(
                        new String("log_to_a_computed_base"),
                        new DoubleColumnType()
                    ),
                    new Column(
                        new String("log_to_a_fractional_base"),
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
                                    new String("integer_operands"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1024))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("as_an_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1024))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("negative_exponent"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.01002003004005006))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("fractional_exponent"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(3.1606961258558215))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("cube_root"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(2.1537163056204958))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("parameter_exponent"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(2.8284271247461903))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_operand"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(900))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("power_of_a_power"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(9.99))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sqrt_of_integer"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1.4142135623730951))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sqrt_nullable"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(5.477225575051661))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("exp_of_integer"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(7.38905609893065))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("exp_of_ln"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(30))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("ln_of_integer_literal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(2.302585092994046))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("log10_of_decimal_literal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("log_to_a_parameter_base"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1.6602423390088468))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("log_to_a_computed_base"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(3.0959032742893844))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("log_to_a_fractional_base"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-1))
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
                                    new String("integer_operands"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1024))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("as_an_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1024))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("negative_exponent"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.002502501876250782))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("fractional_exponent"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(4.471017781221631))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("cube_root"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(2.7139651382373664))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("parameter_exponent"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_operand"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(900))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("power_of_a_power"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(19.99))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sqrt_of_integer"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sqrt_nullable"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(5.477225575051661))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("exp_of_integer"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(2.718281828459045))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("exp_of_ln"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(30))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("ln_of_integer_literal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(2.302585092994046))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("log10_of_decimal_literal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("log_to_a_parameter_base"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(2.1606032834849516))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("log_to_a_computed_base"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(4.906890595608519))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("log_to_a_fractional_base"),
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
                                new Column(
                                    new String("integer_operands"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1024))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("as_an_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1024))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("negative_exponent"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.04938271604938271))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("fractional_exponent"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(2.1213203435596424))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("cube_root"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1.6509636244473134))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("parameter_exponent"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(11.180339887498949))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_operand"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("power_of_a_power"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(4.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sqrt_of_integer"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(2.23606797749979))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sqrt_nullable"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("exp_of_integer"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(148.4131591025766))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("exp_of_ln"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("ln_of_integer_literal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(2.302585092994046))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("log10_of_decimal_literal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("log_to_a_parameter_base"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1.084962500721156))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("log_to_a_computed_base"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("log_to_a_fractional_base"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-2.321928094887362))
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
                                    new String("integer_operands"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1024))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("as_an_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1024))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("negative_exponent"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.01002003004005006))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("fractional_exponent"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(3.1606961258558215))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("cube_root"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(2.1537163056204958))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("parameter_exponent"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(5.196152422706632))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_operand"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(900))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("power_of_a_power"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(9.99))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sqrt_of_integer"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1.7320508075688772))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sqrt_nullable"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(5.477225575051661))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("exp_of_integer"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(20.085536923187668))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("exp_of_ln"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(30))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("ln_of_integer_literal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(2.302585092994046))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("log10_of_decimal_literal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("log_to_a_parameter_base"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1.6602423390088468))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("log_to_a_computed_base"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(2.4534452978042594))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("log_to_a_fractional_base"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-1.584962500721156))
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
