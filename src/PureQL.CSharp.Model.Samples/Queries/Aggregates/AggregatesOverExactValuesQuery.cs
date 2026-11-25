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
using PureQL.CSharp.Model.ProjectionExpressions;
using PureQL.CSharp.Model.RowExpressions;
using PureQL.CSharp.Model.TotalExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Aggregates;

/// <summary>
/// Declares two subqueries over schema_with_foreign_keys.order_items inner joined to
/// schema_with_foreign_keys.products on item_product_id. by_order groups by item_order_id
/// and selects it with the sum of the rounded item_qty per group and over all rows
/// ("all"), the average product_price, its population variance, and a sum of
/// product_price with a predicate on the rounded item_qty above 1. overall is a total
/// query: the sum of the rounded item_qty, the average product_price and its sample
/// standard deviation, both nullable. The main query selects from the same joined tables
/// the running sum of the rounded item_qty over item_id, the average of product_price
/// times the rounded item_qty times 0.2 over a partition by item_order_id, and the sum of
/// the rounded item_qty plus its remainder modulo 2 plus 1 over all rows.
/// </summary>
public sealed record AggregatesOverExactValuesQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithoutPagination(
            subqueries:
            [
                new SubqueryQuery(
                    "by_order",
                    new GroupedQueryWithoutPaginationWithoutOrderBy(
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
                                "order_id",
                                new FieldUuid(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new OrderItemsTable().Name,
                                        ]
                                    ).TextValue,
                                    new ItemOrderIdColumn().Name.TextValue
                                )
                            ),
                        ],
                        select:
                        [
                            new SelectItemGroup("order_id", new KeyUuid("order_id")),
                            new SelectItemGroup(
                                "units",
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
                                )
                            ),
                            new SelectItemGroup(
                                "all_units",
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
                                    ),
                                    over: AggregateOver.All
                                )
                            ),
                            new SelectItemGroup(
                                "mean",
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
                                )
                            ),
                            new SelectItemGroup(
                                "spread",
                                new VariancePopulationDecimalGroup(
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
                            new SelectItemGroup(
                                "filtered",
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
                        ]
                    )
                ),
                new SubqueryQuery(
                    "overall",
                    new TotalQuery(
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
                        aggregate:
                        [
                            new SelectItemTotal(
                                "units",
                                new SumIntegerTotal(
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
                            new SelectItemTotal(
                                "mean",
                                new AverageDecimalNullableTotal(
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
                            new SelectItemTotal(
                                "deviation",
                                new StandardDeviationSampleTotal(
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
                        ]
                    )
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
            ],
            select:
            [
                new SelectItemProjection(
                    "units_so_far",
                    new SumIntegerProjection(
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
                        over: new WindowWithFrame(
                            [
                                new OrderItemRow(
                                    new FieldUuid(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new OrderItemsTable().Name,
                                            ]
                                        ).TextValue,
                                        new ItemIdColumn().Name.TextValue
                                    )
                                ),
                            ],
                            new Frame(preceding: new All())
                        )
                    )
                ),
                new SelectItemProjection(
                    "mean_of_window",
                    new AverageDecimalProjection(
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
                            new LiteralDecimal(0.2m),
                        ]),
                        over: new WindowWithoutFrameWithoutRange([
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
                        ])
                    )
                ),
                new SelectItemProjection(
                    "units",
                    new SumIntegerProjection(
                        new AddIntegerRow([
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
                            new ModuloIntegerRow(
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
                            ),
                            new LiteralInteger(1),
                        ])
                    )
                ),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and <see cref="AuditSchemaDataSet"/>,
    /// in no particular order.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("units_so_far"), new LongColumnType()),
                    new Column(new String("mean_of_window"), new DoubleColumnType()),
                    new Column(new String("units"), new LongColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("units_so_far"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("mean_of_window"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(3.997))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("units"), new LongColumnType()),
                                new InvariantCell(new Long(18))
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
                                    new String("units_so_far"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("mean_of_window"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(3.997))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("units"), new LongColumnType()),
                                new InvariantCell(new Long(18))
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
                                    new String("units_so_far"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(8))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("mean_of_window"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(4.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("units"), new LongColumnType()),
                                new InvariantCell(new Long(18))
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
                                    new String("units_so_far"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(11))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("mean_of_window"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(5.994))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("units"), new LongColumnType()),
                                new InvariantCell(new Long(18))
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
