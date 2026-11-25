using Pure.Collections.Generic;
using Pure.Primitives.Bool;
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
/// Groups schema_with_foreign_keys.order_items inner joined to
/// schema_with_foreign_keys.products on item_product_id = product_id by item_tenant_id,
/// keeps the groups whose regressionRSquared of the rounded item_qty on product_price,
/// coalesced with 0, is greater than 0.3, and selects the item_tenant_id, the count of
/// lines, the regressionSlope, regressionIntercept and regressionRSquared of the rounded
/// item_qty on product_price, the regressionSlope over all groups, whether the slope
/// coalesced with 0 is greater than that over all groups coalesced with 0, and the
/// regressionSlope of product_price times the rounded item_qty on the rounded item_qty
/// minus its integer half, ordered by the fit column descending and item_tenant_id.
/// </summary>
public sealed record RegressionInAGroupedQuery
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
                    "item_tenant_id",
                    new FieldUuid(
                        new JoinedString(
                            new DotString(),
                            [
                                new RelationalSchemaWithForeignKeys().Name,
                                new OrderItemsTable().Name,
                            ]
                        ).TextValue,
                        new ItemTenantIdColumn().Name.TextValue
                    )
                ),
            ],
            having: new GreaterThanDecimalGroup(
                new CoalesceDecimalGroup([
                    new RegressionRSquaredGroup(
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
                                    new ProductsTable().Name,
                                ]
                            ).TextValue,
                            new ProductPriceColumn().Name.TextValue
                        )
                    ),
                    new LiteralInteger(0),
                ]),
                new LiteralDecimal(0.3m)
            ),
            select:
            [
                new SelectItemGroup("item_tenant_id", new KeyUuid("item_tenant_id")),
                new SelectItemGroup("lines", new CountGroup()),
                new SelectItemGroup(
                    "quantity_per_unit_of_price",
                    new RegressionSlopeGroup(
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
                                    new ProductsTable().Name,
                                ]
                            ).TextValue,
                            new ProductPriceColumn().Name.TextValue
                        )
                    )
                ),
                new SelectItemGroup(
                    "quantity_at_no_price",
                    new RegressionInterceptGroup(
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
                                    new ProductsTable().Name,
                                ]
                            ).TextValue,
                            new ProductPriceColumn().Name.TextValue
                        )
                    )
                ),
                new SelectItemGroup(
                    "fit",
                    new RegressionRSquaredGroup(
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
                                    new ProductsTable().Name,
                                ]
                            ).TextValue,
                            new ProductPriceColumn().Name.TextValue
                        )
                    )
                ),
                new SelectItemGroup(
                    "slope_over_all_tenants",
                    new RegressionSlopeGroup(
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
                                    new ProductsTable().Name,
                                ]
                            ).TextValue,
                            new ProductPriceColumn().Name.TextValue
                        ),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "steeper_than_all",
                    new GreaterThanDecimalGroup(
                        new CoalesceDecimalGroup([
                            new RegressionSlopeGroup(
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
                                            new ProductsTable().Name,
                                        ]
                                    ).TextValue,
                                    new ProductPriceColumn().Name.TextValue
                                )
                            ),
                            new LiteralInteger(0),
                        ]),
                        new CoalesceDecimalGroup([
                            new RegressionSlopeGroup(
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
                                            new ProductsTable().Name,
                                        ]
                                    ).TextValue,
                                    new ProductPriceColumn().Name.TextValue
                                ),
                                over: AggregateOver.All
                            ),
                            new LiteralInteger(0),
                        ])
                    )
                ),
                new SelectItemGroup(
                    "of_computed_values",
                    new RegressionSlopeGroup(
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
                            new IntegerDivideIntegerRow(
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
                        ])
                    )
                ),
            ],
            orderBy:
            [
                new OrderItemGroupColumn("fit", SortDirection.Desc),
                new OrderItemGroupColumn("item_tenant_id"),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over <see
    /// cref="SchemaDataSetWithForeignKeys"/> and <see cref="AuditSchemaDataSet"/>, in
    /// this order. The regression values are computed exactly in Python (Fraction,
    /// rounded as the specification rounds a decimal), as PostgreSQL computes them in
    /// double.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new ItemTenantIdColumn(),
                    new Column(new String("lines"), new LongColumnType()),
                    new Column(
                        new String("quantity_per_unit_of_price"),
                        new DoubleColumnType()
                    ),
                    new Column(
                        new String("quantity_at_no_price"),
                        new DoubleColumnType()
                    ),
                    new Column(new String("fit"), new DoubleColumnType()),
                    new Column(
                        new String("slope_over_all_tenants"),
                        new DoubleColumnType()
                    ),
                    new Column(new String("steeper_than_all"), new BoolColumnType()),
                    new Column(new String("of_computed_values"), new DoubleColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new ItemTenantIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000385-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("lines"), new LongColumnType()),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("quantity_per_unit_of_price"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-0.23894820636175618))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("quantity_at_no_price"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(5.412978051784451))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("fit"), new DoubleColumnType()),
                                new InvariantCell(new Double(0.8126996110988499))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("slope_over_all_tenants"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-0.23871482224931695))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("steeper_than_all"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_computed_values"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1.2575))
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
