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
/// schema_with_foreign_keys.orders on item_order_id, with the unit price order_total
/// divided by item_qty, the quantity the rounded item_qty and the returned quantity
/// order_total divided by 50 and rounded, fourteen columns of abs, sign and truncate: abs
/// of a nullable integer (the quantity minus the returned quantity from a quantity of 3,
/// null otherwise), of an integer literal and of a decimal literal, of the sum of the
/// quantity and the unit price, sign of the quantity minus the returned quantity, sign of
/// a nullable decimal (a third of the unit price minus 50 from a quantity of 3, null
/// otherwise), truncate of the rounded order_total to -1 digits, of the unit price to -1
/// and to 2 digits, of the nullable decimal to a computed 2 minus 1 digits, abs of
/// truncate of the unit price times that sign to 1 digit, and round, floor and truncate
/// of the unit price minus 75.5.
/// </summary>
public sealed record NumericFunctionCornersQuery
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
            ],
            select:
            [
                new SelectItemProjection(
                    "abs_integer_nullable",
                    new AbsIntegerNullableProjection(
                        new IfIntegerNullableProjection(
                            new SubtractIntegerProjection([
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
                                new RoundWithoutDigitsIntegerProjection(
                                    new DivideDecimalProjection([
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
                                        new LiteralDecimal(50m),
                                    ])
                                ),
                            ]),
                            new LiteralIntegerNullable(),
                            new GreaterThanOrEqualDecimalProjection(
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
                                new LiteralInteger(3)
                            )
                        )
                    )
                ),
                new SelectItemProjection(
                    "abs_integer_literal",
                    new AbsIntegerProjection(new LiteralInteger(-9223372036854775807))
                ),
                new SelectItemProjection(
                    "abs_decimal_literal",
                    new AbsDecimalProjection(new LiteralDecimal(-0.5m))
                ),
                new SelectItemProjection(
                    "abs_of_mixed_sum",
                    new AbsDecimalProjection(
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
                            new DivideDecimalProjection([
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
                            ]),
                        ])
                    )
                ),
                new SelectItemProjection(
                    "sign_of_integer",
                    new SignIntegerProjection(
                        new SubtractIntegerProjection([
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
                            new RoundWithoutDigitsIntegerProjection(
                                new DivideDecimalProjection([
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
                                    new LiteralDecimal(50m),
                                ])
                            ),
                        ])
                    )
                ),
                new SelectItemProjection(
                    "sign_of_nullable",
                    new SignIntegerNullableProjection(
                        new IfDecimalNullableProjection(
                            new DivideDecimalProjection([
                                new SubtractDecimalProjection([
                                    new DivideDecimalProjection([
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
                                    ]),
                                    new LiteralDecimal(50m),
                                ]),
                                new LiteralDecimal(3m),
                            ]),
                            new LiteralDecimalNullable(),
                            new GreaterThanOrEqualDecimalProjection(
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
                                new LiteralInteger(3)
                            )
                        )
                    )
                ),
                new SelectItemProjection(
                    "truncate_of_integer_with_digits",
                    new TruncateWithDigitsIntegerProjection(
                        new RoundWithoutDigitsIntegerProjection(
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
                        new LiteralInteger(-1)
                    )
                ),
                new SelectItemProjection(
                    "truncate_to_tens",
                    new TruncateDecimalProjection(
                        new DivideDecimalProjection([
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
                        ]),
                        new LiteralInteger(-1)
                    )
                ),
                new SelectItemProjection(
                    "truncate_to_two_places",
                    new TruncateDecimalProjection(
                        new DivideDecimalProjection([
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
                        ]),
                        new LiteralInteger(2)
                    )
                ),
                new SelectItemProjection(
                    "truncate_to_computed",
                    new TruncateDecimalNullableProjection(
                        new IfDecimalNullableProjection(
                            new DivideDecimalProjection([
                                new SubtractDecimalProjection([
                                    new DivideDecimalProjection([
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
                                    ]),
                                    new LiteralDecimal(50m),
                                ]),
                                new LiteralDecimal(3m),
                            ]),
                            new LiteralDecimalNullable(),
                            new GreaterThanOrEqualDecimalProjection(
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
                                new LiteralInteger(3)
                            )
                        ),
                        new SubtractIntegerProjection([
                            new LiteralInteger(2),
                            new LiteralInteger(1),
                        ])
                    )
                ),
                new SelectItemProjection(
                    "nested",
                    new AbsDecimalProjection(
                        new TruncateDecimalProjection(
                            new MultiplyDecimalProjection([
                                new DivideDecimalProjection([
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
                                ]),
                                new SignIntegerProjection(
                                    new SubtractIntegerProjection([
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
                                        new RoundWithoutDigitsIntegerProjection(
                                            new DivideDecimalProjection([
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
                                                new LiteralDecimal(50m),
                                            ])
                                        ),
                                    ])
                                ),
                            ]),
                            new LiteralInteger(1)
                        )
                    )
                ),
                new SelectItemProjection(
                    "rounded",
                    new RoundWithoutDigitsIntegerProjection(
                        new SubtractDecimalProjection([
                            new DivideDecimalProjection([
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
                            ]),
                            new LiteralDecimal(75.5m),
                        ])
                    )
                ),
                new SelectItemProjection(
                    "floored",
                    new FloorIntegerProjection(
                        new SubtractDecimalProjection([
                            new DivideDecimalProjection([
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
                            ]),
                            new LiteralDecimal(75.5m),
                        ])
                    )
                ),
                new SelectItemProjection(
                    "truncated",
                    new TruncateWithoutDigitsIntegerProjection(
                        new SubtractDecimalProjection([
                            new DivideDecimalProjection([
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
                            ]),
                            new LiteralDecimal(75.5m),
                        ])
                    )
                ),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and
    /// <see cref="AuditSchemaDataSet"/>, in no particular order.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("abs_integer_nullable"), new LongColumnType()),
                    new Column(new String("abs_integer_literal"), new LongColumnType()),
                    new Column(new String("abs_decimal_literal"), new DoubleColumnType()),
                    new Column(new String("abs_of_mixed_sum"), new DoubleColumnType()),
                    new Column(new String("sign_of_integer"), new LongColumnType()),
                    new Column(new String("sign_of_nullable"), new LongColumnType()),
                    new Column(
                        new String("truncate_of_integer_with_digits"),
                        new LongColumnType()
                    ),
                    new Column(new String("truncate_to_tens"), new DoubleColumnType()),
                    new Column(
                        new String("truncate_to_two_places"),
                        new DoubleColumnType()
                    ),
                    new Column(
                        new String("truncate_to_computed"),
                        new DoubleColumnType()
                    ),
                    new Column(new String("nested"), new DoubleColumnType()),
                    new Column(new String("rounded"), new LongColumnType()),
                    new Column(new String("floored"), new LongColumnType()),
                    new Column(new String("truncated"), new LongColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("abs_integer_nullable"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("abs_integer_literal"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(9223372036854775807))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("abs_decimal_literal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("abs_of_mixed_sum"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(101.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sign_of_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(-1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sign_of_nullable"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("truncate_of_integer_with_digits"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(100))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("truncate_to_tens"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(100.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("truncate_to_two_places"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(100.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("truncate_to_computed"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("nested"), new DoubleColumnType()),
                                new InvariantCell(new Double(100.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("rounded"), new LongColumnType()),
                                new InvariantCell(new Long(25))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("floored"), new LongColumnType()),
                                new InvariantCell(new Long(25))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("truncated"), new LongColumnType()),
                                new InvariantCell(new Long(25))
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
                                    new String("abs_integer_nullable"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("abs_integer_literal"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(9223372036854775807))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("abs_decimal_literal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("abs_of_mixed_sum"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(52.25))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sign_of_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sign_of_nullable"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("truncate_of_integer_with_digits"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(100))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("truncate_to_tens"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(50.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("truncate_to_two_places"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(50.25))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("truncate_to_computed"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("nested"), new DoubleColumnType()),
                                new InvariantCell(new Double(0.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("rounded"), new LongColumnType()),
                                new InvariantCell(new Long(-25))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("floored"), new LongColumnType()),
                                new InvariantCell(new Long(-26))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("truncated"), new LongColumnType()),
                                new InvariantCell(new Long(-25))
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
                                    new String("abs_integer_nullable"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("abs_integer_literal"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(9223372036854775807))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("abs_decimal_literal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("abs_of_mixed_sum"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(45.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sign_of_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sign_of_nullable"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(-1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("truncate_of_integer_with_digits"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(200))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("truncate_to_tens"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(40.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("truncate_to_two_places"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(40.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("truncate_to_computed"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-3.3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("nested"), new DoubleColumnType()),
                                new InvariantCell(new Double(40.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("rounded"), new LongColumnType()),
                                new InvariantCell(new Long(-36))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("floored"), new LongColumnType()),
                                new InvariantCell(new Long(-36))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("truncated"), new LongColumnType()),
                                new InvariantCell(new Long(-35))
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
                                    new String("abs_integer_nullable"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("abs_integer_literal"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(9223372036854775807))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("abs_decimal_literal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("abs_of_mixed_sum"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(103.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sign_of_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(-1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sign_of_nullable"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("truncate_of_integer_with_digits"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(300))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("truncate_to_tens"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(100.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("truncate_to_two_places"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(100.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("truncate_to_computed"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(16.6))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("nested"), new DoubleColumnType()),
                                new InvariantCell(new Double(100.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("rounded"), new LongColumnType()),
                                new InvariantCell(new Long(25))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("floored"), new LongColumnType()),
                                new InvariantCell(new Long(24))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("truncated"), new LongColumnType()),
                                new InvariantCell(new Long(24))
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
