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

namespace PureQL.CSharp.Model.Samples.Queries.Aggregates;

/// <summary>
/// Selects, per item_product_id of schema_with_foreign_keys.order_items inner joined to
/// schema_with_foreign_keys.orders on item_order_id = order_id, with the order_total of
/// the order as the price of the item and item_qty rounded as its quantity, the product
/// id, the median price, the median price of all rows, the median and the mode of the
/// quantity, the mode of the quantity of all rows, the continuous percentiles of 0.75 and
/// 0.25 of the price subtracted, the discrete percentile of 0.75 of the quantity, the
/// median of the quantity times the price, the median price of the items with a quantity
/// above 2 (a predicate, null where there is none), the population standard deviation of
/// the price divided by its average (null through nullIfEqual where that is 0), the
/// sample standard deviation of the price of all rows and the population variance of the
/// quantity. The having keeps the products whose sample standard deviation of the price,
/// 0 where it is null, is greater than 10, and the groups are ordered by the median price
/// descending, then by product id.
/// </summary>
public sealed record PriceStatisticsPerProductQuery
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
            ],
            having: new GreaterThanDecimalGroup(
                new CoalesceDecimalGroup([
                    new StandardDeviationSampleGroup(
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
                    new LiteralInteger(0),
                ]),
                new LiteralDecimal(10m)
            ),
            select:
            [
                new SelectItemGroup("product_id", new KeyUuid("product_id")),
                new SelectItemGroup(
                    "median_price",
                    new MedianDecimalGroup(
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
                    )
                ),
                new SelectItemGroup(
                    "median_price_of_all",
                    new MedianDecimalGroup(
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
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "median_quantity",
                    new MedianDecimalGroup(
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
                new SelectItemGroup(
                    "usual_quantity",
                    new ModeIntegerGroup(
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
                new SelectItemGroup(
                    "usual_quantity_of_all",
                    new ModeIntegerGroup(
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
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "quartile_range",
                    new SubtractDecimalGroup([
                        new PercentileContinuousDecimalGroup(
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
                            new LiteralDecimal(0.75m)
                        ),
                        new PercentileContinuousDecimalGroup(
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
                            new LiteralDecimal(0.25m)
                        ),
                    ])
                ),
                new SelectItemGroup(
                    "third_quartile_quantity",
                    new PercentileDiscreteIntegerGroup(
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
                        new LiteralDecimal(0.75m)
                    )
                ),
                new SelectItemGroup(
                    "median_line_value",
                    new MedianDecimalGroup(
                        new MultiplyDecimalRow([
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
                                        new OrdersTable().Name,
                                    ]
                                ).TextValue,
                                new OrderTotalColumn().Name.TextValue
                            ),
                        ])
                    )
                ),
                new SelectItemGroup(
                    "median_of_large_lines",
                    new MedianDecimalNullableGroup(
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
                            new LiteralInteger(2)
                        )
                    )
                ),
                new SelectItemGroup(
                    "relative_spread",
                    new DivideDecimalNullableGroup([
                        new StandardDeviationPopulationDecimalGroup(
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
                        new NullIfEqualDecimalNullableGroup(
                            new AverageDecimalGroup(
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
                            new LiteralInteger(0)
                        ),
                    ])
                ),
                new SelectItemGroup(
                    "spread_of_all",
                    new StandardDeviationSampleGroup(
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
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "quantity_variance",
                    new VariancePopulationDecimalGroup(
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
            ],
            orderBy:
            [
                new OrderItemGroupExpression(
                    new MedianDecimalGroup(
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
                    SortDirection.Desc
                ),
                new OrderItemGroupExpression(new KeyUuid("product_id")),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and
    /// <see cref="AuditSchemaDataSet"/>, in this order.
    /// The statistics are computed exactly with Python decimal and fractions, as the
    /// specification defines them, and rounded as it rounds a decimal.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("product_id"), new UuidColumnType()),
                    new Column(new String("median_price"), new DoubleColumnType()),
                    new Column(new String("median_price_of_all"), new DoubleColumnType()),
                    new Column(new String("median_quantity"), new DoubleColumnType()),
                    new Column(new String("usual_quantity"), new LongColumnType()),
                    new Column(new String("usual_quantity_of_all"), new LongColumnType()),
                    new Column(new String("quartile_range"), new DoubleColumnType()),
                    new Column(
                        new String("third_quartile_quantity"),
                        new LongColumnType()
                    ),
                    new Column(new String("median_line_value"), new DoubleColumnType()),
                    new Column(
                        new String("median_of_large_lines"),
                        new DoubleColumnType()
                    ),
                    new Column(new String("relative_spread"), new DoubleColumnType()),
                    new Column(new String("spread_of_all"), new DoubleColumnType()),
                    new Column(new String("quantity_variance"), new DoubleColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("product_id"),
                                    new UuidColumnType()
                                ),
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
                                    new String("median_price"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(200.25))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("median_price_of_all"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(150.25))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("median_quantity"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(2.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("usual_quantity"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("usual_quantity_of_all"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("quartile_range"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(99.75))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("third_quartile_quantity"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("median_line_value"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(550.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("median_of_large_lines"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(300))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("relative_spread"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.49812734082397003))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("spread_of_all"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(95.48167363426344))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("quantity_variance"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.25))
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
