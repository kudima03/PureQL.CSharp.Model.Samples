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
using PureQL.CSharp.Model.GroupExpressions;
using PureQL.CSharp.Model.Keys;
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.RowExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Numbers;

/// <summary>
/// Selects, from schema_with_foreign_keys.order_items inner joined to products on
/// item_product_id, grouped by product_price integer-divided by 10 as price_band and
/// product_price modulo 1 as cents, having the sum of product_price modulo 10 greater
/// than the decimal 4.75: price_band, cents, the sum of product_price modulo 10 as
/// remainder_of_the_sum and integer-divided by 10 as tens_in_the_sum, the average of product_price modulo 0.05 as average_remainder,
/// the count of rows whose product_price modulo 0.05 is not 0 as off_step, price_band
/// multiplied by 10 as band_start and the sum of item_qty rounded to a whole number
/// integer-divided by 2 as pairs, ordered by price_band and cents.
/// </summary>
public sealed record DecimalRemaindersInAGroupedQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainGroupedQueryWithoutPagination(
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
            groupBy:
            [
                new GroupKey(
                    "price_band",
                    new IntegerDivideIntegerRow(
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
                        new LiteralInteger(10)
                    )
                ),
                new GroupKey(
                    "cents",
                    new ModuloDecimalRow(
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
            ],
            having: new GreaterThanDecimalGroup(
                new ModuloDecimalGroup(
                    new SumDecimalGroup(
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
                    new LiteralInteger(10)
                ),
                new LiteralDecimal(4.75m)
            ),
            select:
            [
                new SelectItemGroup("price_band", new KeyInteger("price_band")),
                new SelectItemGroup("cents", new KeyDecimal("cents")),
                new SelectItemGroup(
                    "remainder_of_the_sum",
                    new ModuloDecimalGroup(
                        new SumDecimalGroup(
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
                        new LiteralInteger(10)
                    )
                ),
                new SelectItemGroup(
                    "tens_in_the_sum",
                    new IntegerDivideIntegerGroup(
                        new SumDecimalGroup(
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
                        new LiteralInteger(10)
                    )
                ),
                new SelectItemGroup(
                    "average_remainder",
                    new AverageDecimalGroup(
                        new ModuloDecimalRow(
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
                            new LiteralDecimal(0.05m)
                        )
                    )
                ),
                new SelectItemGroup(
                    "off_step",
                    new CountGroup(
                        new NotEqualDecimalRow(
                            new ModuloDecimalRow(
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
                                new LiteralDecimal(0.05m)
                            ),
                            new LiteralInteger(0)
                        )
                    )
                ),
                new SelectItemGroup(
                    "band_start",
                    new MultiplyIntegerGroup([
                        new KeyInteger("price_band"),
                        new LiteralInteger(10),
                    ])
                ),
                new SelectItemGroup(
                    "pairs",
                    new IntegerDivideIntegerGroup(
                        new SumIntegerGroup(
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
            ],
            orderBy:
            [
                new OrderItemGroupExpression(new KeyInteger("price_band")),
                new OrderItemGroupExpression(new KeyDecimal("cents")),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and
    /// <see cref="AuditSchemaDataSet"/>, in this order.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("price_band"), new LongColumnType()),
                    new Column(new String("cents"), new DoubleColumnType()),
                    new Column(
                        new String("remainder_of_the_sum"),
                        new DoubleColumnType()
                    ),
                    new Column(new String("tens_in_the_sum"), new LongColumnType()),
                    new Column(new String("average_remainder"), new DoubleColumnType()),
                    new Column(new String("off_step"), new LongColumnType()),
                    new Column(new String("band_start"), new LongColumnType()),
                    new Column(new String("pairs"), new LongColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("price_band"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("cents"), new DoubleColumnType()),
                                new InvariantCell(new Double(0.99))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("remainder_of_the_sum"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(9.98))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("tens_in_the_sum"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("average_remainder"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.04))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("off_step"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("band_start"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("pairs"), new LongColumnType()),
                                new InvariantCell(new Long(2))
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
                                    new String("price_band"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("cents"), new DoubleColumnType()),
                                new InvariantCell(new Double(0.99))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("remainder_of_the_sum"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(9.99))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("tens_in_the_sum"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("average_remainder"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.04))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("off_step"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("band_start"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(10))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("pairs"), new LongColumnType()),
                                new InvariantCell(new Long(0))
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
