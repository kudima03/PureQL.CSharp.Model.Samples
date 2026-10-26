using Pure.Collections.Generic;
using Pure.Primitives.Date;
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
using PureQL.CSharp.Model.GroupKeys;
using PureQL.CSharp.Model.Keys;
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.RowExpressions;
using PureQL.CSharp.Model.SelectItems;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Dashboards;

/// <summary>
/// Selects placed_on, the sum of item_qty times product_price rounded to 2 digits as
/// revenue, that sum over rows whose product_in_stock is true rounded to 2 digits as
/// in_stock_revenue, the sum minus the in-stock sum rounded to 2 digits as
/// out_of_stock_revenue and the decimal 100 times the in-stock sum divided by the sum
/// rounded to 1 digit as in_stock_pct from schema_with_foreign_keys.order_items inner
/// joined with schema_with_foreign_keys.orders on item_order_id equal to order_id and
/// with schema_with_foreign_keys.products on item_product_id equal to product_id, grouped
/// by placed_on, ordered by placed_on.
/// </summary>
public sealed record DailyInStockRevenueShareQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new PureQLQuery(
            new MainGroupedQuery(
                new From(
                    new FromEntity(
                        new JoinedString(
                            new DotString(),
                            [
                                new RelationalSchemaWithForeignKeys().Name,
                                new OrderItemsTable().Name,
                            ]
                        ).TextValue
                    )
                ),
                [
                    new GroupKey(
                        new GroupKeyNonNullable(
                            new GroupKeyDate(
                                "placed_on",
                                new DateRow(
                                    new FieldDate(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new OrdersTable().Name,
                                            ]
                                        ).TextValue,
                                        new PlacedOnColumn().Name.TextValue
                                    )
                                )
                            )
                        )
                    ),
                ],
                [
                    new SelectItemGroup(
                        new SelectItemGroupNonNullable(
                            new SelectItemGroupDate(
                                "placed_on",
                                new DateGroup(new KeyDate("placed_on"))
                            )
                        )
                    ),
                    new SelectItemGroup(
                        new SelectItemGroupNonNullable(
                            new SelectItemGroupDecimal(
                                "revenue",
                                new ExactDecimalGroup(
                                    new RoundDecimalGroup(
                                        new DecimalGroup(
                                            new ExactDecimalGroup(
                                                new AggregateDecimalGroup(
                                                    new SumDecimalGroup(
                                                        new DecimalNullableRow(
                                                            new ExactDecimalRow(
                                                                new ArithmeticDecimalRow(
                                                                    new MultiplyDecimalRow([
                                                                        new DecimalRow(
                                                                            new ExactDecimalRow(
                                                                                new FieldDecimal(
                                                                                    new JoinedString(
                                                                                        new DotString(),
                                                                                        [
                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                            new OrderItemsTable().Name,
                                                                                        ]
                                                                                    ).TextValue,
                                                                                    new ItemQtyColumn()
                                                                                        .Name
                                                                                        .TextValue
                                                                                )
                                                                            )
                                                                        ),
                                                                        new DecimalRow(
                                                                            new ExactDecimalRow(
                                                                                new FieldDecimal(
                                                                                    new JoinedString(
                                                                                        new DotString(),
                                                                                        [
                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                            new ProductsTable().Name,
                                                                                        ]
                                                                                    ).TextValue,
                                                                                    new ProductPriceColumn()
                                                                                        .Name
                                                                                        .TextValue
                                                                                )
                                                                            )
                                                                        ),
                                                                    ])
                                                                )
                                                            )
                                                        )
                                                    )
                                                )
                                            )
                                        ),
                                        new IntegerGroup(new LiteralInteger(2))
                                    )
                                )
                            )
                        )
                    ),
                    new SelectItemGroup(
                        new SelectItemGroupNonNullable(
                            new SelectItemGroupDecimal(
                                "in_stock_revenue",
                                new ExactDecimalGroup(
                                    new RoundDecimalGroup(
                                        new DecimalGroup(
                                            new ExactDecimalGroup(
                                                new AggregateDecimalGroup(
                                                    new SumDecimalGroup(
                                                        new DecimalNullableRow(
                                                            new ExactDecimalRow(
                                                                new ArithmeticDecimalRow(
                                                                    new MultiplyDecimalRow([
                                                                        new DecimalRow(
                                                                            new ExactDecimalRow(
                                                                                new FieldDecimal(
                                                                                    new JoinedString(
                                                                                        new DotString(),
                                                                                        [
                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                            new OrderItemsTable().Name,
                                                                                        ]
                                                                                    ).TextValue,
                                                                                    new ItemQtyColumn()
                                                                                        .Name
                                                                                        .TextValue
                                                                                )
                                                                            )
                                                                        ),
                                                                        new DecimalRow(
                                                                            new ExactDecimalRow(
                                                                                new FieldDecimal(
                                                                                    new JoinedString(
                                                                                        new DotString(),
                                                                                        [
                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                            new ProductsTable().Name,
                                                                                        ]
                                                                                    ).TextValue,
                                                                                    new ProductPriceColumn()
                                                                                        .Name
                                                                                        .TextValue
                                                                                )
                                                                            )
                                                                        ),
                                                                    ])
                                                                )
                                                            )
                                                        ),
                                                        new BooleanRow(
                                                            new FieldBoolean(
                                                                new JoinedString(
                                                                    new DotString(),
                                                                    [
                                                                        new RelationalSchemaWithForeignKeys().Name,
                                                                        new ProductsTable().Name,
                                                                    ]
                                                                ).TextValue,
                                                                new ProductInStockColumn()
                                                                    .Name
                                                                    .TextValue
                                                            )
                                                        )
                                                    )
                                                )
                                            )
                                        ),
                                        new IntegerGroup(new LiteralInteger(2))
                                    )
                                )
                            )
                        )
                    ),
                    new SelectItemGroup(
                        new SelectItemGroupNonNullable(
                            new SelectItemGroupDecimal(
                                "out_of_stock_revenue",
                                new ExactDecimalGroup(
                                    new RoundDecimalGroup(
                                        new DecimalGroup(
                                            new ExactDecimalGroup(
                                                new ArithmeticDecimalGroup(
                                                    new SubtractDecimalGroup([
                                                        new DecimalGroup(
                                                            new ExactDecimalGroup(
                                                                new AggregateDecimalGroup(
                                                                    new SumDecimalGroup(
                                                                        new DecimalNullableRow(
                                                                            new ExactDecimalRow(
                                                                                new ArithmeticDecimalRow(
                                                                                    new MultiplyDecimalRow([
                                                                                        new DecimalRow(
                                                                                            new ExactDecimalRow(
                                                                                                new FieldDecimal(
                                                                                                    new JoinedString(
                                                                                                        new DotString(),
                                                                                                        [
                                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                                            new OrderItemsTable().Name,
                                                                                                        ]
                                                                                                    ).TextValue,
                                                                                                    new ItemQtyColumn()
                                                                                                        .Name
                                                                                                        .TextValue
                                                                                                )
                                                                                            )
                                                                                        ),
                                                                                        new DecimalRow(
                                                                                            new ExactDecimalRow(
                                                                                                new FieldDecimal(
                                                                                                    new JoinedString(
                                                                                                        new DotString(),
                                                                                                        [
                                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                                            new ProductsTable().Name,
                                                                                                        ]
                                                                                                    ).TextValue,
                                                                                                    new ProductPriceColumn()
                                                                                                        .Name
                                                                                                        .TextValue
                                                                                                )
                                                                                            )
                                                                                        ),
                                                                                    ])
                                                                                )
                                                                            )
                                                                        )
                                                                    )
                                                                )
                                                            )
                                                        ),
                                                        new DecimalGroup(
                                                            new ExactDecimalGroup(
                                                                new AggregateDecimalGroup(
                                                                    new SumDecimalGroup(
                                                                        new DecimalNullableRow(
                                                                            new ExactDecimalRow(
                                                                                new ArithmeticDecimalRow(
                                                                                    new MultiplyDecimalRow([
                                                                                        new DecimalRow(
                                                                                            new ExactDecimalRow(
                                                                                                new FieldDecimal(
                                                                                                    new JoinedString(
                                                                                                        new DotString(),
                                                                                                        [
                                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                                            new OrderItemsTable().Name,
                                                                                                        ]
                                                                                                    ).TextValue,
                                                                                                    new ItemQtyColumn()
                                                                                                        .Name
                                                                                                        .TextValue
                                                                                                )
                                                                                            )
                                                                                        ),
                                                                                        new DecimalRow(
                                                                                            new ExactDecimalRow(
                                                                                                new FieldDecimal(
                                                                                                    new JoinedString(
                                                                                                        new DotString(),
                                                                                                        [
                                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                                            new ProductsTable().Name,
                                                                                                        ]
                                                                                                    ).TextValue,
                                                                                                    new ProductPriceColumn()
                                                                                                        .Name
                                                                                                        .TextValue
                                                                                                )
                                                                                            )
                                                                                        ),
                                                                                    ])
                                                                                )
                                                                            )
                                                                        ),
                                                                        new BooleanRow(
                                                                            new FieldBoolean(
                                                                                new JoinedString(
                                                                                    new DotString(),
                                                                                    [
                                                                                        new RelationalSchemaWithForeignKeys().Name,
                                                                                        new ProductsTable().Name,
                                                                                    ]
                                                                                ).TextValue,
                                                                                new ProductInStockColumn()
                                                                                    .Name
                                                                                    .TextValue
                                                                            )
                                                                        )
                                                                    )
                                                                )
                                                            )
                                                        ),
                                                    ])
                                                )
                                            )
                                        ),
                                        new IntegerGroup(new LiteralInteger(2))
                                    )
                                )
                            )
                        )
                    ),
                    new SelectItemGroup(
                        new SelectItemGroupNonNullable(
                            new SelectItemGroupDecimal(
                                "in_stock_pct",
                                new ExactDecimalGroup(
                                    new RoundDecimalGroup(
                                        new DecimalGroup(
                                            new ExactDecimalGroup(
                                                new ArithmeticDecimalGroup(
                                                    new MultiplyDecimalGroup([
                                                        new DecimalGroup(
                                                            new ExactDecimalGroup(
                                                                new LiteralDecimal(100m)
                                                            )
                                                        ),
                                                        new DecimalGroup(
                                                            new ExactDecimalGroup(
                                                                new ArithmeticDecimalGroup(
                                                                    new DivideDecimalGroup([
                                                                        new DecimalGroup(
                                                                            new ExactDecimalGroup(
                                                                                new AggregateDecimalGroup(
                                                                                    new SumDecimalGroup(
                                                                                        new DecimalNullableRow(
                                                                                            new ExactDecimalRow(
                                                                                                new ArithmeticDecimalRow(
                                                                                                    new MultiplyDecimalRow([
                                                                                                        new DecimalRow(
                                                                                                            new ExactDecimalRow(
                                                                                                                new FieldDecimal(
                                                                                                                    new JoinedString(
                                                                                                                        new DotString(),
                                                                                                                        [
                                                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                                                            new OrderItemsTable().Name,
                                                                                                                        ]
                                                                                                                    ).TextValue,
                                                                                                                    new ItemQtyColumn()
                                                                                                                        .Name
                                                                                                                        .TextValue
                                                                                                                )
                                                                                                            )
                                                                                                        ),
                                                                                                        new DecimalRow(
                                                                                                            new ExactDecimalRow(
                                                                                                                new FieldDecimal(
                                                                                                                    new JoinedString(
                                                                                                                        new DotString(),
                                                                                                                        [
                                                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                                                            new ProductsTable().Name,
                                                                                                                        ]
                                                                                                                    ).TextValue,
                                                                                                                    new ProductPriceColumn()
                                                                                                                        .Name
                                                                                                                        .TextValue
                                                                                                                )
                                                                                                            )
                                                                                                        ),
                                                                                                    ])
                                                                                                )
                                                                                            )
                                                                                        ),
                                                                                        new BooleanRow(
                                                                                            new FieldBoolean(
                                                                                                new JoinedString(
                                                                                                    new DotString(),
                                                                                                    [
                                                                                                        new RelationalSchemaWithForeignKeys().Name,
                                                                                                        new ProductsTable().Name,
                                                                                                    ]
                                                                                                ).TextValue,
                                                                                                new ProductInStockColumn()
                                                                                                    .Name
                                                                                                    .TextValue
                                                                                            )
                                                                                        )
                                                                                    )
                                                                                )
                                                                            )
                                                                        ),
                                                                        new DecimalGroup(
                                                                            new ExactDecimalGroup(
                                                                                new AggregateDecimalGroup(
                                                                                    new SumDecimalGroup(
                                                                                        new DecimalNullableRow(
                                                                                            new ExactDecimalRow(
                                                                                                new ArithmeticDecimalRow(
                                                                                                    new MultiplyDecimalRow([
                                                                                                        new DecimalRow(
                                                                                                            new ExactDecimalRow(
                                                                                                                new FieldDecimal(
                                                                                                                    new JoinedString(
                                                                                                                        new DotString(),
                                                                                                                        [
                                                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                                                            new OrderItemsTable().Name,
                                                                                                                        ]
                                                                                                                    ).TextValue,
                                                                                                                    new ItemQtyColumn()
                                                                                                                        .Name
                                                                                                                        .TextValue
                                                                                                                )
                                                                                                            )
                                                                                                        ),
                                                                                                        new DecimalRow(
                                                                                                            new ExactDecimalRow(
                                                                                                                new FieldDecimal(
                                                                                                                    new JoinedString(
                                                                                                                        new DotString(),
                                                                                                                        [
                                                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                                                            new ProductsTable().Name,
                                                                                                                        ]
                                                                                                                    ).TextValue,
                                                                                                                    new ProductPriceColumn()
                                                                                                                        .Name
                                                                                                                        .TextValue
                                                                                                                )
                                                                                                            )
                                                                                                        ),
                                                                                                    ])
                                                                                                )
                                                                                            )
                                                                                        )
                                                                                    )
                                                                                )
                                                                            )
                                                                        ),
                                                                    ])
                                                                )
                                                            )
                                                        ),
                                                    ])
                                                )
                                            )
                                        ),
                                        new IntegerGroup(new LiteralInteger(1))
                                    )
                                )
                            )
                        )
                    ),
                ],
                subqueries: null,
                [
                    new Join(
                        new JoinEntity(
                            JoinKind.Inner,
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrdersTable().Name,
                                ]
                            ).TextValue,
                            new BooleanRow(
                                new ComparisonRow(
                                    new EqualRow(
                                        new EqualUuidRow(
                                            new UuidNullableRow(
                                                new UuidRow(
                                                    new FieldUuid(
                                                        new JoinedString(
                                                            new DotString(),
                                                            [
                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                new OrderItemsTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new ItemOrderIdColumn()
                                                            .Name
                                                            .TextValue
                                                    )
                                                )
                                            ),
                                            new UuidNullableRow(
                                                new UuidRow(
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
                                            )
                                        )
                                    )
                                )
                            )
                        )
                    ),
                    new Join(
                        new JoinEntity(
                            JoinKind.Inner,
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new ProductsTable().Name,
                                ]
                            ).TextValue,
                            new BooleanRow(
                                new ComparisonRow(
                                    new EqualRow(
                                        new EqualUuidRow(
                                            new UuidNullableRow(
                                                new UuidRow(
                                                    new FieldUuid(
                                                        new JoinedString(
                                                            new DotString(),
                                                            [
                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                new OrderItemsTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new ItemProductIdColumn()
                                                            .Name
                                                            .TextValue
                                                    )
                                                )
                                            ),
                                            new UuidNullableRow(
                                                new UuidRow(
                                                    new FieldUuid(
                                                        new JoinedString(
                                                            new DotString(),
                                                            [
                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                new ProductsTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new ProductIdColumn()
                                                            .Name
                                                            .TextValue
                                                    )
                                                )
                                            )
                                        )
                                    )
                                )
                            )
                        )
                    ),
                ],
                where: null,
                having: null,
                [
                    new OrderItemGroup(
                        new ExprGroup(
                            new DateNullableGroup(new DateGroup(new KeyDate("placed_on")))
                        )
                    ),
                ],
                pagination: null,
                distinctBy: null
            )
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/>, in this order.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new PlacedOnColumn(),
                    new Column(new String("revenue"), new DoubleColumnType()),
                    new Column(new String("in_stock_revenue"), new DoubleColumnType()),
                    new Column(
                        new String("out_of_stock_revenue"),
                        new DoubleColumnType()
                    ),
                    new Column(new String("in_stock_pct"), new DoubleColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new PlacedOnColumn(),
                                new InvariantCell(
                                    new Date(
                                        new UShort(1),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("revenue"), new DoubleColumnType()),
                                new InvariantCell(new Double(39.97))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("in_stock_revenue"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(19.98))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("out_of_stock_revenue"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(19.99))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("in_stock_pct"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(50))
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
                                new PlacedOnColumn(),
                                new InvariantCell(
                                    new Date(
                                        new UShort(3),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("revenue"), new DoubleColumnType()),
                                new InvariantCell(new Double(22.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("in_stock_revenue"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(22.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("out_of_stock_revenue"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("in_stock_pct"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(100))
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
                                new PlacedOnColumn(),
                                new InvariantCell(
                                    new Date(
                                        new UShort(5),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("revenue"), new DoubleColumnType()),
                                new InvariantCell(new Double(29.97))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("in_stock_revenue"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(29.97))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("out_of_stock_revenue"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("in_stock_pct"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(100))
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
