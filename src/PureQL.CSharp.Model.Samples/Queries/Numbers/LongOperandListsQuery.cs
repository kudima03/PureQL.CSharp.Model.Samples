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
/// Selects from schema_with_foreign_keys.order_items inner joined to
/// schema_with_foreign_keys.products on item_product_id and to
/// schema_with_foreign_keys.orders on item_order_id, and left joined to
/// schema_with_foreign_keys.users on order_user_id for active users only, so that
/// user_score is null for the items of an inactive user, operators over lists of three to
/// six values: the rounded item_qty, its remainder modulo 2, 1, -2, 5 and the rounded
/// item_qty added as an integer, product_price minus the rounded item_qty, the remainder
/// and 1 as a decimal first, the rounded item_qty and the remainder plus 1 and 0.5 as a
/// decimal last, the rounded item_qty times 1.5, the remainder and 3 as a decimal in the
/// middle, product_price times the rounded item_qty, 0.2 and the nullable user_score,
/// user_score divided by the rounded item_qty, 2 and 3, the rounded item_qty minus the
/// remainder and the rounded nullable user_score, the rounded item_qty divided by 3 and
/// then by 2, 1.5 times the smallest decimal step times 3, and the largest integer plus
/// -1 plus 1.
/// </summary>
public sealed record LongOperandListsQuery
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
                    JoinKind.Left,
                    new JoinedString(
                        new DotString(),
                        [
                            new RelationalSchemaWithForeignKeys().Name,
                            new UsersTable().Name,
                        ]
                    ).TextValue,
                    new AndRow([
                        new EqualUuidRow(
                            new FieldUuid(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new UsersTable().Name,
                                    ]
                                ).TextValue,
                                new UserIdColumn().Name.TextValue
                            ),
                            new FieldUuid(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new OrdersTable().Name,
                                    ]
                                ).TextValue,
                                new OrderUserIdColumn().Name.TextValue
                            )
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
                        ),
                    ])
                ),
            ],
            select:
            [
                new SelectItemProjection(
                    "integers",
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
                        new ModuloIntegerProjection(
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
                            new LiteralInteger(2)
                        ),
                        new LiteralInteger(1),
                        new LiteralInteger(-2),
                        new LiteralInteger(5),
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
                    ])
                ),
                new SelectItemProjection(
                    "decimal_first",
                    new SubtractDecimalProjection([
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
                        new ModuloIntegerProjection(
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
                            new LiteralInteger(2)
                        ),
                        new LiteralInteger(1),
                    ])
                ),
                new SelectItemProjection(
                    "decimal_last",
                    new AddDecimalProjection([
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
                        new ModuloIntegerProjection(
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
                            new LiteralInteger(2)
                        ),
                        new LiteralInteger(1),
                        new LiteralDecimal(0.5m),
                    ])
                ),
                new SelectItemProjection(
                    "decimal_inside",
                    new MultiplyDecimalProjection([
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
                        new LiteralDecimal(1.5m),
                        new ModuloIntegerProjection(
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
                            new LiteralInteger(2)
                        ),
                        new LiteralInteger(3),
                    ])
                ),
                new SelectItemProjection(
                    "nullable_last",
                    new MultiplyDecimalNullableProjection([
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
                        new LiteralDecimal(0.2m),
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
                    ])
                ),
                new SelectItemProjection(
                    "nullable_first",
                    new DivideDecimalNullableProjection([
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
                        new LiteralInteger(2),
                        new LiteralInteger(3),
                    ])
                ),
                new SelectItemProjection(
                    "integer_param",
                    new SubtractIntegerNullableProjection([
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
                        new ModuloIntegerProjection(
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
                            new LiteralInteger(2)
                        ),
                        new RoundWithoutDigitsIntegerNullableProjection(
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
                        ),
                    ])
                ),
                new SelectItemProjection(
                    "divide_integers",
                    new DivideDecimalProjection([
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
                        new LiteralInteger(3),
                        new LiteralInteger(2),
                    ])
                ),
                new SelectItemProjection(
                    "smallest_step",
                    new MultiplyDecimalProjection([
                        new LiteralDecimal(1.5m),
                        new LiteralDecimal(0.0000000000000000000000000001m),
                        new LiteralInteger(3),
                    ])
                ),
                new SelectItemProjection(
                    "largest_integers",
                    new AddIntegerProjection([
                        new LiteralInteger(9223372036854775807),
                        new LiteralInteger(-1),
                        new LiteralInteger(1),
                    ])
                ),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and <see cref="AuditSchemaDataSet"/>,
    /// in no particular order.
    /// Each step of an operator over more than two values is rounded on its own, as the
    /// specification folds them, and computed in exact decimal arithmetic.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("integers"), new LongColumnType()),
                    new Column(new String("decimal_first"), new DoubleColumnType()),
                    new Column(new String("decimal_last"), new DoubleColumnType()),
                    new Column(new String("decimal_inside"), new DoubleColumnType()),
                    new Column(new String("nullable_last"), new DoubleColumnType()),
                    new Column(new String("nullable_first"), new DoubleColumnType()),
                    new Column(new String("integer_param"), new LongColumnType()),
                    new Column(new String("divide_integers"), new DoubleColumnType()),
                    new Column(new String("smallest_step"), new DoubleColumnType()),
                    new Column(new String("largest_integers"), new LongColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("integers"), new LongColumnType()),
                                new InvariantCell(new Long(8))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_first"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(6.99))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_last"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(3.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_inside"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_last"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(119.88))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_first"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(2.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("integer_param"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(-28))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("divide_integers"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.3333333333333333))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("smallest_step"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(6E-28))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("largest_integers"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(9223372036854775807))
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
                                new Column(new String("integers"), new LongColumnType()),
                                new InvariantCell(new Long(7))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_first"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(16.99))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_last"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(3.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_inside"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(4.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_last"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(119.94))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_first"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("integer_param"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(-30))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("divide_integers"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.16666666666666666))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("smallest_step"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(6E-28))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("largest_integers"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(9223372036854775807))
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
                                new Column(new String("integers"), new LongColumnType()),
                                new InvariantCell(new Long(15))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_first"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-2.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_last"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(7.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_inside"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(22.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_last"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_first"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("integer_param"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("divide_integers"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.8333333333333334))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("smallest_step"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(6E-28))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("largest_integers"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(9223372036854775807))
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
                                new Column(new String("integers"), new LongColumnType()),
                                new InvariantCell(new Long(11))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_first"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(4.99))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_last"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(5.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_inside"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(13.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_last"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(179.82))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_first"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1.6666666666666667))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("integer_param"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(-28))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("divide_integers"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("smallest_step"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(6E-28))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("largest_integers"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(9223372036854775807))
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
