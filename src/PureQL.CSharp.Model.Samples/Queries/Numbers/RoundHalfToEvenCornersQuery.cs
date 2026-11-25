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
/// Selects, from schema_with_foreign_keys.order_items inner joined to products on
/// item_product_id and left joined on item_id to an inline subquery discounts (an amount
/// for two items), product_price rounded half to even to a whole number, to 2 digits, to
/// -1 digits, to 1 digit and to 4 minus 2 digits, the amount rounded half to even to 2
/// digits, 2.5 and -2.5 rounded half to even to whole numbers and 2.5 rounded by the
/// default rule, product_price rounded half to even to 3 digits and then to 1,
/// product_price divided by 0.05, rounded half to even to a whole number and multiplied
/// by 0.05, product_price divided by 16 rounded half to even to 4 digits, and item_qty
/// rounded to a whole number multiplied by 50 and rounded half to even to -2 digits.
/// </summary>
public sealed record RoundHalfToEvenCornersQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithoutPagination(
            subqueries:
            [
                new SubqueryRows(
                    "discounts",
                    [
                        [
                            new RowItem(
                                "item_id",
                                new LiteralUuid(new Guid(301, 0, 0, new byte[8]))
                            ),
                            new RowItem("amount", new LiteralDecimal(0.125m)),
                        ],
                        [
                            new RowItem(
                                "item_id",
                                new LiteralUuid(new Guid(303, 0, 0, new byte[8]))
                            ),
                            new RowItem("amount", new LiteralDecimal(-0.025m)),
                        ],
                    ]
                ),
            ],
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
                new JoinSubquery(
                    JoinKind.Left,
                    "discounts",
                    new EqualUuidRow(
                        new FieldUuid(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrderItemsTable().Name,
                                ]
                            ).TextValue,
                            new ItemIdColumn().Name.TextValue
                        ),
                        new FieldUuid("discounts", "item_id")
                    )
                ),
            ],
            select:
            [
                new SelectItemProjection(
                    "to_a_whole_number",
                    new RoundWithoutDigitsIntegerProjection(
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
                        RoundingMode.HalfEven
                    )
                ),
                new SelectItemProjection(
                    "to_cents",
                    new RoundDecimalProjection(
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
                        new LiteralInteger(2),
                        RoundingMode.HalfEven
                    )
                ),
                new SelectItemProjection(
                    "to_tens",
                    new RoundDecimalProjection(
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
                        new LiteralInteger(-1),
                        RoundingMode.HalfEven
                    )
                ),
                new SelectItemProjection(
                    "integer_to_hundreds",
                    new RoundWithDigitsIntegerProjection(
                        new MultiplyIntegerProjection([
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
                            new LiteralInteger(50),
                        ]),
                        new LiteralInteger(-2),
                        RoundingMode.HalfEven
                    )
                ),
                new SelectItemProjection(
                    "by_a_digits_literal",
                    new RoundDecimalProjection(
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
                        new LiteralInteger(1),
                        RoundingMode.HalfEven
                    )
                ),
                new SelectItemProjection(
                    "by_computed_digits",
                    new RoundDecimalProjection(
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
                        new SubtractIntegerProjection([
                            new LiteralInteger(4),
                            new LiteralInteger(2),
                        ]),
                        RoundingMode.HalfEven
                    )
                ),
                new SelectItemProjection(
                    "of_a_nullable",
                    new RoundDecimalNullableProjection(
                        new FieldDecimalNullable("discounts", "amount"),
                        new LiteralInteger(2),
                        RoundingMode.HalfEven
                    )
                ),
                new SelectItemProjection(
                    "of_a_literal_halfway",
                    new RoundWithoutDigitsIntegerProjection(
                        new LiteralDecimal(2.5m),
                        RoundingMode.HalfEven
                    )
                ),
                new SelectItemProjection(
                    "of_a_negative_halfway",
                    new RoundWithoutDigitsIntegerProjection(
                        new LiteralDecimal(-2.5m),
                        RoundingMode.HalfEven
                    )
                ),
                new SelectItemProjection(
                    "default_rule_beside_it",
                    new RoundWithoutDigitsIntegerProjection(new LiteralDecimal(2.5m))
                ),
                new SelectItemProjection(
                    "nested",
                    new RoundDecimalProjection(
                        new RoundDecimalProjection(
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
                            new LiteralInteger(3),
                            RoundingMode.HalfEven
                        ),
                        new LiteralInteger(1),
                        RoundingMode.HalfEven
                    )
                ),
                new SelectItemProjection(
                    "cash_rounding",
                    new MultiplyDecimalProjection([
                        new RoundWithoutDigitsIntegerProjection(
                            new DivideDecimalProjection([
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
                                new LiteralDecimal(0.05m),
                            ]),
                            RoundingMode.HalfEven
                        ),
                        new LiteralDecimal(0.05m),
                    ])
                ),
                new SelectItemProjection(
                    "of_a_quotient",
                    new RoundDecimalProjection(
                        new DivideDecimalProjection([
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
                            new LiteralInteger(16),
                        ]),
                        new LiteralInteger(4),
                        RoundingMode.HalfEven
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
                    new Column(new String("to_a_whole_number"), new LongColumnType()),
                    new Column(new String("to_cents"), new DoubleColumnType()),
                    new Column(new String("to_tens"), new DoubleColumnType()),
                    new Column(new String("integer_to_hundreds"), new LongColumnType()),
                    new Column(new String("by_a_digits_literal"), new DoubleColumnType()),
                    new Column(new String("by_computed_digits"), new DoubleColumnType()),
                    new Column(new String("of_a_nullable"), new DoubleColumnType()),
                    new Column(new String("of_a_literal_halfway"), new LongColumnType()),
                    new Column(new String("of_a_negative_halfway"), new LongColumnType()),
                    new Column(
                        new String("default_rule_beside_it"),
                        new LongColumnType()
                    ),
                    new Column(new String("nested"), new DoubleColumnType()),
                    new Column(new String("cash_rounding"), new DoubleColumnType()),
                    new Column(new String("of_a_quotient"), new DoubleColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("to_a_whole_number"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(10))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("to_cents"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(9.99))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("to_tens"), new DoubleColumnType()),
                                new InvariantCell(new Double(10))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("integer_to_hundreds"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(100))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("by_a_digits_literal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(10))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("by_computed_digits"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(9.99))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_nullable"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.12))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_literal_halfway"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_negative_halfway"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(-2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("default_rule_beside_it"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("nested"), new DoubleColumnType()),
                                new InvariantCell(new Double(10))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("cash_rounding"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(10))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_quotient"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.6244))
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
                                    new String("to_a_whole_number"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(20))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("to_cents"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(19.99))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("to_tens"), new DoubleColumnType()),
                                new InvariantCell(new Double(20))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("integer_to_hundreds"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("by_a_digits_literal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(20))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("by_computed_digits"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(19.99))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_nullable"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_literal_halfway"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_negative_halfway"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(-2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("default_rule_beside_it"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("nested"), new DoubleColumnType()),
                                new InvariantCell(new Double(20))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("cash_rounding"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(20))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_quotient"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1.2494))
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
                                    new String("to_a_whole_number"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(4))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("to_cents"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(4.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("to_tens"), new DoubleColumnType()),
                                new InvariantCell(new Double(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("integer_to_hundreds"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(200))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("by_a_digits_literal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(4.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("by_computed_digits"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(4.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_nullable"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-0.02))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_literal_halfway"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_negative_halfway"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(-2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("default_rule_beside_it"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("nested"), new DoubleColumnType()),
                                new InvariantCell(new Double(4.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("cash_rounding"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(4.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_quotient"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.2812))
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
                                    new String("to_a_whole_number"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(10))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("to_cents"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(9.99))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("to_tens"), new DoubleColumnType()),
                                new InvariantCell(new Double(10))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("integer_to_hundreds"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(200))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("by_a_digits_literal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(10))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("by_computed_digits"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(9.99))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_nullable"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_literal_halfway"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_negative_halfway"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(-2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("default_rule_beside_it"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("nested"), new DoubleColumnType()),
                                new InvariantCell(new Double(10))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("cash_rounding"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(10))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_quotient"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.6244))
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
