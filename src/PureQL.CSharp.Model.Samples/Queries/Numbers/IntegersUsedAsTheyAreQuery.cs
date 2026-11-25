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
/// Selects quantity, kept, dozens, left_over, lines, units, magnitude and as_a_decimal
/// from schema_with_foreign_keys.order_items inner joined with
/// schema_with_foreign_keys.products on item_product_id equal to product_id, where the
/// quantity is equal to product_price times item_qty rounded to an integer, with the
/// quantity standing for an integer column as item_qty times the integer 10 rounded to an
/// integer and the returned quantity as item_qty times the integer 20 plus the integer 55
/// rounded to an integer: the quantity, the quantity minus the returned quantity as kept,
/// the quantity integer-divided by the integer 12 as dozens, the quantity modulo the
/// integer 12 as left_over, the count of rows as lines, the sum of the quantity as units,
/// the abs of the quantity minus the returned quantity as magnitude and the quantity plus
/// the decimal 0 as as_a_decimal.
/// </summary>
public sealed record IntegersUsedAsTheyAreQuery
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
            where: new EqualDecimalRow(
                new RoundWithoutDigitsIntegerRow(
                    new MultiplyDecimalRow([
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
                        new LiteralInteger(10),
                    ])
                ),
                new RoundWithoutDigitsIntegerRow(
                    new MultiplyDecimalRow([
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
                                    new OrderItemsTable().Name,
                                ]
                            ).TextValue,
                            new ItemQtyColumn().Name.TextValue
                        ),
                    ])
                )
            ),
            select:
            [
                new SelectItemProjection(
                    "quantity",
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
                            new LiteralInteger(10),
                        ])
                    )
                ),
                new SelectItemProjection(
                    "kept",
                    new SubtractIntegerProjection([
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
                                new LiteralInteger(10),
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
                    ])
                ),
                new SelectItemProjection(
                    "dozens",
                    new IntegerDivideIntegerProjection(
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
                                new LiteralInteger(10),
                            ])
                        ),
                        new LiteralInteger(12)
                    )
                ),
                new SelectItemProjection(
                    "left_over",
                    new ModuloIntegerProjection(
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
                                new LiteralInteger(10),
                            ])
                        ),
                        new LiteralInteger(12)
                    )
                ),
                new SelectItemProjection("lines", new CountProjection()),
                new SelectItemProjection(
                    "units",
                    new SumIntegerProjection(
                        new RoundWithoutDigitsIntegerRow(
                            new MultiplyDecimalRow([
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
                                new LiteralInteger(10),
                            ])
                        )
                    )
                ),
                new SelectItemProjection(
                    "magnitude",
                    new AbsIntegerProjection(
                        new SubtractIntegerProjection([
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
                                    new LiteralInteger(10),
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
                        ])
                    )
                ),
                new SelectItemProjection(
                    "as_a_decimal",
                    new AddDecimalProjection([
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
                                new LiteralInteger(10),
                            ])
                        ),
                        new LiteralDecimal(0m),
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
                    new Column(new String("quantity"), new LongColumnType()),
                    new Column(new String("kept"), new LongColumnType()),
                    new Column(new String("dozens"), new LongColumnType()),
                    new Column(new String("left_over"), new LongColumnType()),
                    new Column(new String("lines"), new LongColumnType()),
                    new Column(new String("units"), new LongColumnType()),
                    new Column(new String("magnitude"), new LongColumnType()),
                    new Column(new String("as_a_decimal"), new DoubleColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("quantity"), new LongColumnType()),
                                new InvariantCell(new Long(20))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("kept"), new LongColumnType()),
                                new InvariantCell(new Long(-75))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("dozens"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("left_over"), new LongColumnType()),
                                new InvariantCell(new Long(8))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("lines"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("units"), new LongColumnType()),
                                new InvariantCell(new Long(50))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("magnitude"), new LongColumnType()),
                                new InvariantCell(new Long(75))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("as_a_decimal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(20.0))
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
                                new Column(new String("quantity"), new LongColumnType()),
                                new InvariantCell(new Long(30))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("kept"), new LongColumnType()),
                                new InvariantCell(new Long(-85))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("dozens"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("left_over"), new LongColumnType()),
                                new InvariantCell(new Long(6))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("lines"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("units"), new LongColumnType()),
                                new InvariantCell(new Long(50))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("magnitude"), new LongColumnType()),
                                new InvariantCell(new Long(85))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("as_a_decimal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(30.0))
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
