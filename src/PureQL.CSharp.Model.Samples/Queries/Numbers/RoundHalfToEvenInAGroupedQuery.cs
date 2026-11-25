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
using Guid = Pure.Primitives.Guid.Guid;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Numbers;

/// <summary>
/// Selects, from schema_with_foreign_keys.order_items inner joined to products on
/// item_product_id, grouped by item_product_id as product_id and product_price rounded
/// half to even to a whole number as rounded_price, having the sum of product_price times
/// item_qty rounded to a whole number times 0.13 rounded half to even to 2 digits greater
/// than 2.7: product_id, rounded_price, that sum rounded once as tax_rounded_once, the
/// sum of the same tax rounded half to even to 2 digits per row as tax_rounded_per_line,
/// their difference as rounding_difference, the average of product_price rounded half to
/// even to 2 digits as average_price_to_cents, and the count of rows whose tax rounded
/// half to even is less than the tax rounded by the default rule as lines_rounding_down,
/// ordered by that tax rounded once, product_id and rounded_price.
/// </summary>
public sealed record RoundHalfToEvenInAGroupedQuery
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
                    "product_id",
                    new FieldUuid(
                        new JoinedString(
                            new DotString(),
                            [
                                new RelationalSchemaWithForeignKeys().Name,
                                new OrderItemsTable().Name,
                            ]
                        ).TextValue,
                        new ItemProductIdColumn().Name.TextValue
                    )
                ),
                new GroupKey(
                    "rounded_price",
                    new RoundWithoutDigitsIntegerRow(
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
            ],
            having: new GreaterThanDecimalGroup(
                new RoundDecimalGroup(
                    new SumDecimalGroup(
                        new MultiplyDecimalRow([
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
                            ]),
                            new LiteralDecimal(0.13m),
                        ])
                    ),
                    new LiteralInteger(2),
                    RoundingMode.HalfEven
                ),
                new LiteralDecimal(2.7m)
            ),
            select:
            [
                new SelectItemGroup("product_id", new KeyUuid("product_id")),
                new SelectItemGroup("rounded_price", new KeyInteger("rounded_price")),
                new SelectItemGroup(
                    "tax_rounded_once",
                    new RoundDecimalGroup(
                        new SumDecimalGroup(
                            new MultiplyDecimalRow([
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
                                ]),
                                new LiteralDecimal(0.13m),
                            ])
                        ),
                        new LiteralInteger(2),
                        RoundingMode.HalfEven
                    )
                ),
                new SelectItemGroup(
                    "tax_rounded_per_line",
                    new SumDecimalGroup(
                        new RoundDecimalRow(
                            new MultiplyDecimalRow([
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
                                ]),
                                new LiteralDecimal(0.13m),
                            ]),
                            new LiteralInteger(2),
                            RoundingMode.HalfEven
                        )
                    )
                ),
                new SelectItemGroup(
                    "rounding_difference",
                    new SubtractDecimalGroup([
                        new RoundDecimalGroup(
                            new SumDecimalGroup(
                                new MultiplyDecimalRow([
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
                                    ]),
                                    new LiteralDecimal(0.13m),
                                ])
                            ),
                            new LiteralInteger(2),
                            RoundingMode.HalfEven
                        ),
                        new SumDecimalGroup(
                            new RoundDecimalRow(
                                new MultiplyDecimalRow([
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
                                    ]),
                                    new LiteralDecimal(0.13m),
                                ]),
                                new LiteralInteger(2),
                                RoundingMode.HalfEven
                            )
                        ),
                    ])
                ),
                new SelectItemGroup(
                    "average_price_to_cents",
                    new RoundDecimalGroup(
                        new AverageDecimalGroup(
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
                        new LiteralInteger(2),
                        RoundingMode.HalfEven
                    )
                ),
                new SelectItemGroup(
                    "lines_rounding_down",
                    new CountGroup(
                        new LessThanDecimalRow(
                            new RoundDecimalRow(
                                new MultiplyDecimalRow([
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
                                    ]),
                                    new LiteralDecimal(0.13m),
                                ]),
                                new LiteralInteger(2),
                                RoundingMode.HalfEven
                            ),
                            new RoundDecimalRow(
                                new MultiplyDecimalRow([
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
                                    ]),
                                    new LiteralDecimal(0.13m),
                                ]),
                                new LiteralInteger(2)
                            )
                        )
                    )
                ),
            ],
            orderBy:
            [
                new OrderItemGroupExpression(
                    new RoundDecimalGroup(
                        new SumDecimalGroup(
                            new MultiplyDecimalRow([
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
                                ]),
                                new LiteralDecimal(0.13m),
                            ])
                        ),
                        new LiteralInteger(2),
                        RoundingMode.HalfEven
                    )
                ),
                new OrderItemGroupExpression(new KeyUuid("product_id")),
                new OrderItemGroupExpression(new KeyInteger("rounded_price")),
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
                    new ProductIdColumn(),
                    new Column(new String("rounded_price"), new LongColumnType()),
                    new Column(new String("tax_rounded_once"), new DoubleColumnType()),
                    new Column(
                        new String("tax_rounded_per_line"),
                        new DoubleColumnType()
                    ),
                    new Column(new String("rounding_difference"), new DoubleColumnType()),
                    new Column(
                        new String("average_price_to_cents"),
                        new DoubleColumnType()
                    ),
                    new Column(new String("lines_rounding_down"), new LongColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new ProductIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "000000cb-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rounded_price"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(4))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("tax_rounded_once"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(2.92))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("tax_rounded_per_line"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(2.92))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rounding_difference"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("average_price_to_cents"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(4.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lines_rounding_down"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
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
                                new ProductIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "000000c9-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rounded_price"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(10))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("tax_rounded_once"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(6.49))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("tax_rounded_per_line"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(6.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rounding_difference"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-0.01))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("average_price_to_cents"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(9.99))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lines_rounding_down"),
                                    new LongColumnType()
                                ),
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
