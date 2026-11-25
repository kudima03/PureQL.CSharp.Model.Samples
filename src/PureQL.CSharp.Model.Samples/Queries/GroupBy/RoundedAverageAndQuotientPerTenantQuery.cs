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
using Guid = Pure.Primitives.Guid.Guid;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.GroupBy;

/// <summary>
/// Selects tenant_id, price_band, the average quantity rounded to an integer as
/// average_quantity, the sum of product_price times the quantity floored as total_floor,
/// the sum of the quantity divided by the count rounded up as units_per_line_rounded_up,
/// the sum of the quantity as units and the sum of the quantity truncated to -1 digits as
/// units_in_tens from schema_with_foreign_keys.order_items inner joined with
/// schema_with_foreign_keys.products on item_product_id equal to product_id, with the
/// quantity standing for an integer column as item_qty minus the integer 1 rounded to an
/// integer, grouped by item_tenant_id as tenant_id and product_price divided by the
/// integer 10 floored as price_band, having the average quantity rounded to an integer
/// greater than the integer 1, ordered by the average quantity rounded to an integer,
/// tenant_id and price_band.
/// </summary>
public sealed record RoundedAverageAndQuotientPerTenantQuery
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
                    "tenant_id",
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
                new GroupKey(
                    "price_band",
                    new FloorIntegerRow(
                        new DivideDecimalRow([
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
                            new LiteralInteger(10),
                        ])
                    )
                ),
            ],
            having: new GreaterThanDecimalGroup(
                new RoundWithoutDigitsIntegerGroup(
                    new AverageDecimalGroup(
                        new RoundWithoutDigitsIntegerRow(
                            new SubtractDecimalRow([
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
                                new LiteralInteger(1),
                            ])
                        )
                    )
                ),
                new LiteralInteger(1)
            ),
            select:
            [
                new SelectItemGroup("tenant_id", new KeyUuid("tenant_id")),
                new SelectItemGroup("price_band", new KeyInteger("price_band")),
                new SelectItemGroup(
                    "average_quantity",
                    new RoundWithoutDigitsIntegerGroup(
                        new AverageDecimalGroup(
                            new RoundWithoutDigitsIntegerRow(
                                new SubtractDecimalRow([
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
                                    new LiteralInteger(1),
                                ])
                            )
                        )
                    )
                ),
                new SelectItemGroup(
                    "total_floor",
                    new FloorIntegerGroup(
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
                                    new SubtractDecimalRow([
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
                                        new LiteralInteger(1),
                                    ])
                                ),
                            ])
                        )
                    )
                ),
                new SelectItemGroup(
                    "units_per_line_rounded_up",
                    new CeilingIntegerGroup(
                        new DivideDecimalGroup([
                            new SumIntegerGroup(
                                new RoundWithoutDigitsIntegerRow(
                                    new SubtractDecimalRow([
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
                                        new LiteralInteger(1),
                                    ])
                                )
                            ),
                            new CountGroup(),
                        ])
                    )
                ),
                new SelectItemGroup(
                    "units",
                    new SumIntegerGroup(
                        new RoundWithoutDigitsIntegerRow(
                            new SubtractDecimalRow([
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
                                new LiteralInteger(1),
                            ])
                        )
                    )
                ),
                new SelectItemGroup(
                    "units_in_tens",
                    new TruncateWithDigitsIntegerGroup(
                        new SumIntegerGroup(
                            new RoundWithoutDigitsIntegerRow(
                                new SubtractDecimalRow([
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
                                    new LiteralInteger(1),
                                ])
                            )
                        ),
                        new LiteralInteger(-1)
                    )
                ),
            ],
            orderBy:
            [
                new OrderItemGroupExpression(
                    new RoundWithoutDigitsIntegerGroup(
                        new AverageDecimalGroup(
                            new RoundWithoutDigitsIntegerRow(
                                new SubtractDecimalRow([
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
                                    new LiteralInteger(1),
                                ])
                            )
                        )
                    )
                ),
                new OrderItemGroupExpression(new KeyUuid("tenant_id")),
                new OrderItemGroupExpression(new KeyInteger("price_band")),
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
                    new TenantIdColumn(),
                    new Column(new String("price_band"), new LongColumnType()),
                    new Column(new String("average_quantity"), new LongColumnType()),
                    new Column(new String("total_floor"), new LongColumnType()),
                    new Column(
                        new String("units_per_line_rounded_up"),
                        new LongColumnType()
                    ),
                    new Column(new String("units"), new LongColumnType()),
                    new Column(new String("units_in_tens"), new LongColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new TenantIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000386-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("price_band"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("average_quantity"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("total_floor"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(19))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("units_per_line_rounded_up"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("units"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("units_in_tens"),
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
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new TenantIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000385-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("price_band"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("average_quantity"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("total_floor"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(27))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("units_per_line_rounded_up"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("units"), new LongColumnType()),
                                new InvariantCell(new Long(5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("units_in_tens"),
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
