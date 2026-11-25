using Pure.Collections.Generic;
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
using Guid = Pure.Primitives.Guid.Guid;
using ModelPagination = PureQL.CSharp.Model.Pagination;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Numbers;

/// <summary>
/// Declares two subqueries over schema_with_foreign_keys.order_items inner joined to
/// schema_with_foreign_keys.products on item_product_id. per_order keeps the items whose
/// rounded item_qty minus its remainder modulo 2 minus 1 is above 0, groups them by
/// item_order_id and by the rounded item_qty plus the remainder plus 1 (units), keeps the
/// groups where the sum of product_price plus the count plus units is above 13.5, and
/// selects item_order_id, the sum of product_price times the rounded item_qty times 0.2
/// divided by the count and by units, and the sum of product_price divided by its sum
/// over all rows ("all") and by 100. totals is a total query: the sum of the rounded
/// item_qty minus the sum of the remainder minus the count, and the largest product_price
/// minus the smallest minus 0.01, nullable. The main query selects from order_items inner
/// joined to products, per_order on item_order_id and totals where totals.units plus the
/// rounded item_qty plus 1 is above 0: item_id, the running sum of product_price times
/// the rounded item_qty times 0.2 over a partition by item_order_id ordered by item_id
/// with a frame from the first row, product_price minus the previous product_price
/// (default 0, ordered by item_id) minus the average product_price, and
/// per_order.weighted, ordered by product_price times the rounded item_qty times 0.2
/// descending and item_id, with a take of 20.
/// </summary>
public sealed record OperatorsOverAListInEveryContextQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithPagination(
            subqueries:
            [
                new SubqueryQuery(
                    "per_order",
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
                        where: new GreaterThanDecimalRow(
                            new SubtractIntegerRow([
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
                            ]),
                            new LiteralInteger(0)
                        ),
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
                            new GroupKey(
                                "units",
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
                            ),
                        ],
                        having: new GreaterThanDecimalGroup(
                            new AddDecimalGroup([
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
                                new CountGroup(),
                                new KeyInteger("units"),
                            ]),
                            new LiteralDecimal(13.5m)
                        ),
                        select:
                        [
                            new SelectItemGroup("order_id", new KeyUuid("order_id")),
                            new SelectItemGroup(
                                "weighted",
                                new DivideDecimalGroup([
                                    new SumDecimalGroup(
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
                                        ])
                                    ),
                                    new CountGroup(),
                                    new KeyInteger("units"),
                                ])
                            ),
                            new SelectItemGroup(
                                "share",
                                new DivideDecimalGroup([
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
                                        over: AggregateOver.All
                                    ),
                                    new LiteralInteger(100),
                                ])
                            ),
                        ]
                    )
                ),
                new SubqueryQuery(
                    "totals",
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
                                new SubtractIntegerTotal([
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
                                    ),
                                    new SumIntegerTotal(
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
                                        )
                                    ),
                                    new CountTotal(),
                                ])
                            ),
                            new SelectItemTotal(
                                "spread",
                                new SubtractDecimalNullableTotal([
                                    new MaxDecimalNullableTotal(
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
                                    new MinDecimalNullableTotal(
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
                                    new LiteralDecimal(0.01m),
                                ])
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
                new JoinSubquery(
                    JoinKind.Inner,
                    "per_order",
                    new EqualUuidRow(
                        new FieldUuid("per_order", "order_id"),
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
                    )
                ),
                new JoinSubquery(
                    JoinKind.Inner,
                    "totals",
                    new GreaterThanDecimalRow(
                        new AddIntegerRow([
                            new FieldInteger("totals", "units"),
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
                            new LiteralInteger(1),
                        ]),
                        new LiteralInteger(0)
                    )
                ),
            ],
            select:
            [
                new SelectItemProjection(
                    "id",
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
                new SelectItemProjection(
                    "running",
                    new SumDecimalProjection(
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
                            new Frame(preceding: new All()),
                            partitionBy:
                            [
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
                            ]
                        )
                    )
                ),
                new SelectItemProjection(
                    "change",
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
                        new PreviousDecimalProjection(
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
                            new LiteralInteger(0),
                            new OrderedWindow([
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
                            ])
                        ),
                        new AverageDecimalProjection(
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
                    ])
                ),
                new SelectItemProjection(
                    "weighted",
                    new FieldDecimal("per_order", "weighted")
                ),
            ],
            orderBy:
            [
                new OrderItemProjectionExpression(
                    new MultiplyDecimalProjection([
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
                    ]),
                    direction: SortDirection.Desc
                ),
                new OrderItemProjectionExpression(
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
            pagination: new ModelPagination(take: 20)
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and <see cref="AuditSchemaDataSet"/>,
    /// in this order.
    /// Each step of an operator over more than two values is rounded on its own, as the
    /// specification folds them, and computed in exact decimal arithmetic.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new IdColumn(),
                    new Column(new String("running"), new DoubleColumnType()),
                    new Column(new String("change"), new DoubleColumnType()),
                    new Column(new String("weighted"), new DoubleColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new IdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000130-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("running"), new DoubleColumnType()),
                                new InvariantCell(new Double(5.994))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("change"), new DoubleColumnType()),
                                new InvariantCell(new Double(-23.323333333333334))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("weighted"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1.1988))
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
                                new IdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "0000012e-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("running"), new DoubleColumnType()),
                                new InvariantCell(new Double(7.994))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("change"), new DoubleColumnType()),
                                new InvariantCell(new Double(-3.3233333333333333))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("weighted"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1.332))
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
                                new IdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "0000012d-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("running"), new DoubleColumnType()),
                                new InvariantCell(new Double(3.996))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("change"), new DoubleColumnType()),
                                new InvariantCell(new Double(-3.3333333333333335))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("weighted"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1.332))
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
