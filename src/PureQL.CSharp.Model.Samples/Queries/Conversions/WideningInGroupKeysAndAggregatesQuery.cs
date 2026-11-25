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

namespace PureQL.CSharp.Model.Samples.Queries.Conversions;

/// <summary>
/// Selects quantity, optional_order_id, lines, units, largest_quantity, optional_units,
/// first_price_or_nothing, lines_unless_one and larger_of_lines_and_key from
/// schema_with_foreign_keys.order_items inner joined with
/// schema_with_foreign_keys.products on item_product_id equal to product_id, with the
/// quantity standing for an integer column as item_qty times the decimal 0.5 rounded to
/// an integer, grouped by the quantity widened with asDecimal as quantity and
/// item_order_id widened with asNullable as optional_order_id, having the count widened
/// with asDecimal greater than the decimal 1.5, and selecting the key quantity, the key
/// optional_order_id, the count widened with asDecimal as lines, the sum of the quantity
/// widened with asDecimal as units, the max of the quantity widened with asDecimal as
/// largest_quantity, the sum of the quantity widened with asNullable as optional_units,
/// the min of product_price widened with asNullable as first_price_or_nothing, the count
/// widened with asDecimal null if equal to the decimal 1 as lines_unless_one and the
/// greatest of the count widened with asDecimal and the key quantity as
/// larger_of_lines_and_key, ordered by the count widened with asDecimal, the key quantity
/// and the key optional_order_id.
/// </summary>
public sealed record WideningInGroupKeysAndAggregatesQuery
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
                    "quantity",
                    new AsDecimalIntegerDecimalRow(
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
                                new LiteralDecimal(0.5m),
                            ])
                        )
                    )
                ),
                new GroupKey(
                    "optional_order_id",
                    new AsNullableUuidNullableRow(
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
            ],
            having: new GreaterThanDecimalGroup(
                new AsDecimalIntegerDecimalGroup(new CountGroup()),
                new LiteralDecimal(1.5m)
            ),
            select:
            [
                new SelectItemGroup("quantity", new KeyDecimal("quantity")),
                new SelectItemGroup(
                    "optional_order_id",
                    new KeyUuidNullable("optional_order_id")
                ),
                new SelectItemGroup(
                    "lines",
                    new AsDecimalIntegerDecimalGroup(new CountGroup())
                ),
                new SelectItemGroup(
                    "units",
                    new AsDecimalIntegerDecimalGroup(
                        new SumIntegerGroup(
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
                                    new LiteralDecimal(0.5m),
                                ])
                            )
                        )
                    )
                ),
                new SelectItemGroup(
                    "largest_quantity",
                    new MaxDecimalGroup(
                        new AsDecimalIntegerDecimalRow(
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
                                    new LiteralDecimal(0.5m),
                                ])
                            )
                        )
                    )
                ),
                new SelectItemGroup(
                    "optional_units",
                    new AsNullableIntegerNullableGroup(
                        new SumIntegerGroup(
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
                                    new LiteralDecimal(0.5m),
                                ])
                            )
                        )
                    )
                ),
                new SelectItemGroup(
                    "first_price_or_nothing",
                    new AsNullableDecimalNullableGroup(
                        new MinDecimalGroup(
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
                    )
                ),
                new SelectItemGroup(
                    "lines_unless_one",
                    new NullIfEqualDecimalNullableGroup(
                        new AsDecimalIntegerDecimalGroup(new CountGroup()),
                        new LiteralDecimal(1m)
                    )
                ),
                new SelectItemGroup(
                    "larger_of_lines_and_key",
                    new GreatestDecimalGroup([
                        new AsDecimalIntegerDecimalGroup(new CountGroup()),
                        new KeyDecimal("quantity"),
                    ])
                ),
            ],
            orderBy:
            [
                new OrderItemGroupExpression(
                    new AsDecimalIntegerDecimalGroup(new CountGroup())
                ),
                new OrderItemGroupExpression(new KeyDecimal("quantity")),
                new OrderItemGroupExpression(new KeyUuidNullable("optional_order_id")),
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
                    new Column(new String("quantity"), new DoubleColumnType()),
                    new Column(new String("optional_order_id"), new UuidColumnType()),
                    new Column(new String("lines"), new DoubleColumnType()),
                    new Column(new String("units"), new DoubleColumnType()),
                    new Column(new String("largest_quantity"), new DoubleColumnType()),
                    new Column(new String("optional_units"), new LongColumnType()),
                    new Column(
                        new String("first_price_or_nothing"),
                        new DoubleColumnType()
                    ),
                    new Column(new String("lines_unless_one"), new DoubleColumnType()),
                    new Column(
                        new String("larger_of_lines_and_key"),
                        new DoubleColumnType()
                    ),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("quantity"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("optional_order_id"),
                                    new UuidColumnType()
                                ),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000065-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("lines"), new DoubleColumnType()),
                                new InvariantCell(new Double(2.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("units"), new DoubleColumnType()),
                                new InvariantCell(new Double(2.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("largest_quantity"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("optional_units"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_price_or_nothing"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(9.99))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lines_unless_one"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(2.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("larger_of_lines_and_key"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(2.0))
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
