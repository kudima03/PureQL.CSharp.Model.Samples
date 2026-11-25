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
using Column = Pure.RelationalSchema.Column.Column;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Numbers;

/// <summary>
/// Selects from schema_with_foreign_keys.order_items, with the quantity standing for an
/// integer column as item_qty times the integer 37 rounded to an integer and the returned
/// quantity as item_qty times the integer 20 plus the integer 55 rounded to an integer:
/// the quantity rounded to -1 digits as to_tens, truncated to -2 digits as
/// to_hundreds_toward_zero, rounded to -2 digits as to_hundreds, truncated to digits
/// computed as the integer 0 minus the integer 1 as by_computed_digits, the nullable
/// quantity rounded to -1 digits as nullable_to_tens, the sum of the quantity and the
/// returned quantity rounded to -1 digits as of_a_sum_of_integers, the quantity rounded
/// to 2 digits as with_no_digits_to_drop and the quantity rounded to -1 digits plus the
/// integer 1 as still_an_integer.
/// </summary>
public sealed record RoundingIntegersToTensAndHundredsQuery
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
            select:
            [
                new SelectItemProjection(
                    "to_tens",
                    new RoundWithDigitsIntegerProjection(
                        new RoundWithoutDigitsIntegerProjection(
                            new MultiplyDecimalProjection([
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
                                new LiteralInteger(37),
                            ])
                        ),
                        new LiteralInteger(-1)
                    )
                ),
                new SelectItemProjection(
                    "to_hundreds_toward_zero",
                    new TruncateWithDigitsIntegerProjection(
                        new RoundWithoutDigitsIntegerProjection(
                            new MultiplyDecimalProjection([
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
                                new LiteralInteger(37),
                            ])
                        ),
                        new LiteralInteger(-2)
                    )
                ),
                new SelectItemProjection(
                    "to_hundreds",
                    new RoundWithDigitsIntegerProjection(
                        new RoundWithoutDigitsIntegerProjection(
                            new MultiplyDecimalProjection([
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
                                new LiteralInteger(37),
                            ])
                        ),
                        new LiteralInteger(-2)
                    )
                ),
                new SelectItemProjection(
                    "by_computed_digits",
                    new TruncateWithDigitsIntegerProjection(
                        new RoundWithoutDigitsIntegerProjection(
                            new MultiplyDecimalProjection([
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
                                new LiteralInteger(37),
                            ])
                        ),
                        new SubtractIntegerProjection([
                            new LiteralInteger(0),
                            new LiteralInteger(1),
                        ])
                    )
                ),
                new SelectItemProjection(
                    "nullable_to_tens",
                    new RoundWithDigitsIntegerNullableProjection(
                        new AsNullableIntegerNullableProjection(
                            new RoundWithoutDigitsIntegerProjection(
                                new MultiplyDecimalProjection([
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
                                    new LiteralInteger(37),
                                ])
                            )
                        ),
                        new LiteralInteger(-1)
                    )
                ),
                new SelectItemProjection(
                    "of_a_sum_of_integers",
                    new RoundWithDigitsIntegerProjection(
                        new AddIntegerProjection([
                            new RoundWithoutDigitsIntegerProjection(
                                new MultiplyDecimalProjection([
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
                                    new LiteralInteger(37),
                                ])
                            ),
                            new RoundWithoutDigitsIntegerProjection(
                                new AddDecimalProjection([
                                    new MultiplyDecimalProjection([
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
                                        new LiteralInteger(20),
                                    ]),
                                    new LiteralInteger(55),
                                ])
                            ),
                        ]),
                        new LiteralInteger(-1)
                    )
                ),
                new SelectItemProjection(
                    "with_no_digits_to_drop",
                    new RoundWithDigitsIntegerProjection(
                        new RoundWithoutDigitsIntegerProjection(
                            new MultiplyDecimalProjection([
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
                                new LiteralInteger(37),
                            ])
                        ),
                        new LiteralInteger(2)
                    )
                ),
                new SelectItemProjection(
                    "still_an_integer",
                    new AddIntegerProjection([
                        new RoundWithDigitsIntegerProjection(
                            new RoundWithoutDigitsIntegerProjection(
                                new MultiplyDecimalProjection([
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
                                    new LiteralInteger(37),
                                ])
                            ),
                            new LiteralInteger(-1)
                        ),
                        new LiteralInteger(1),
                    ])
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
                    new Column(new String("to_tens"), new LongColumnType()),
                    new Column(
                        new String("to_hundreds_toward_zero"),
                        new LongColumnType()
                    ),
                    new Column(new String("to_hundreds"), new LongColumnType()),
                    new Column(new String("by_computed_digits"), new LongColumnType()),
                    new Column(new String("nullable_to_tens"), new LongColumnType()),
                    new Column(new String("of_a_sum_of_integers"), new LongColumnType()),
                    new Column(
                        new String("with_no_digits_to_drop"),
                        new LongColumnType()
                    ),
                    new Column(new String("still_an_integer"), new LongColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("to_tens"), new LongColumnType()),
                                new InvariantCell(new Long(70))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("to_hundreds_toward_zero"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("to_hundreds"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(100))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("by_computed_digits"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(70))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_to_tens"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(70))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_sum_of_integers"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(170))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("with_no_digits_to_drop"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(74))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("still_an_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(71))
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
                                new Column(new String("to_tens"), new LongColumnType()),
                                new InvariantCell(new Long(40))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("to_hundreds_toward_zero"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("to_hundreds"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("by_computed_digits"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(30))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_to_tens"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(40))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_sum_of_integers"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(110))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("with_no_digits_to_drop"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(37))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("still_an_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(41))
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
                                new Column(new String("to_tens"), new LongColumnType()),
                                new InvariantCell(new Long(190))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("to_hundreds_toward_zero"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(100))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("to_hundreds"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(200))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("by_computed_digits"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(180))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_to_tens"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(190))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_sum_of_integers"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(340))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("with_no_digits_to_drop"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(185))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("still_an_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(191))
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
                                new Column(new String("to_tens"), new LongColumnType()),
                                new InvariantCell(new Long(110))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("to_hundreds_toward_zero"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(100))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("to_hundreds"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(100))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("by_computed_digits"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(110))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_to_tens"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(110))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_sum_of_integers"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(230))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("with_no_digits_to_drop"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(111))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("still_an_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(111))
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
