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

namespace PureQL.CSharp.Model.Samples.Queries.GroupBy;

/// <summary>
/// Selects from schema_with_foreign_keys.order_items inner joined to
/// schema_with_foreign_keys.orders on item_order_id, grouped by item_product_id, with the
/// unit price order_total divided by item_qty, the quantity the rounded item_qty and the
/// returned quantity order_total divided by 50 and rounded. A group is kept when the
/// absolute difference of its average unit price and 40 is greater than 0.5. It selects
/// the key, that absolute difference, the largest absolute difference of a unit price and
/// 40, the absolute difference of the sums of the quantity and of the returned quantity
/// (which stay integers), the sign of the sum of the quantity minus 5, and the average
/// unit price truncated to 2 digits and without digits, ordered by the absolute
/// difference descending, then the key.
/// </summary>
public sealed record PriceDeviationPerProductQuery
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
                new AbsDecimalGroup(
                    new SubtractDecimalGroup([
                        new AverageDecimalGroup(
                            new DivideDecimalRow([
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
                        ),
                        new LiteralDecimal(40m),
                    ])
                ),
                new LiteralDecimal(0.5m)
            ),
            select:
            [
                new SelectItemGroup("product_id", new KeyUuid("product_id")),
                new SelectItemGroup(
                    "average_gap",
                    new AbsDecimalGroup(
                        new SubtractDecimalGroup([
                            new AverageDecimalGroup(
                                new DivideDecimalRow([
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
                            ),
                            new LiteralDecimal(40m),
                        ])
                    )
                ),
                new SelectItemGroup(
                    "largest_gap",
                    new MaxDecimalGroup(
                        new AbsDecimalRow(
                            new SubtractDecimalRow([
                                new DivideDecimalRow([
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
                                ]),
                                new LiteralDecimal(40m),
                            ])
                        )
                    )
                ),
                new SelectItemGroup(
                    "net_units",
                    new AbsIntegerGroup(
                        new SubtractIntegerGroup([
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
                            new SumIntegerGroup(
                                new RoundWithoutDigitsIntegerRow(
                                    new DivideDecimalRow([
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
                                        new LiteralDecimal(50m),
                                    ])
                                )
                            ),
                        ])
                    )
                ),
                new SelectItemGroup(
                    "against_plan",
                    new SignIntegerGroup(
                        new SubtractIntegerGroup([
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
                            new LiteralInteger(5),
                        ])
                    )
                ),
                new SelectItemGroup(
                    "average_price_to_cents",
                    new TruncateDecimalGroup(
                        new AverageDecimalGroup(
                            new DivideDecimalRow([
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
                        ),
                        new LiteralInteger(2)
                    )
                ),
                new SelectItemGroup(
                    "whole_average_price",
                    new TruncateWithoutDigitsIntegerGroup(
                        new AverageDecimalGroup(
                            new DivideDecimalRow([
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
                    )
                ),
            ],
            orderBy:
            [
                new OrderItemGroupExpression(
                    new AbsDecimalGroup(
                        new SubtractDecimalGroup([
                            new AverageDecimalGroup(
                                new DivideDecimalRow([
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
                            ),
                            new LiteralDecimal(40m),
                        ])
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
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new ProductIdColumn(),
                    new Column(new String("average_gap"), new DoubleColumnType()),
                    new Column(new String("largest_gap"), new DoubleColumnType()),
                    new Column(new String("net_units"), new LongColumnType()),
                    new Column(new String("against_plan"), new LongColumnType()),
                    new Column(
                        new String("average_price_to_cents"),
                        new DoubleColumnType()
                    ),
                    new Column(new String("whole_average_price"), new LongColumnType()),
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
                                            "000000ca-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("average_gap"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(60.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("largest_gap"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(60.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("net_units"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("against_plan"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(-1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("average_price_to_cents"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(100.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("whole_average_price"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(100))
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
                                    new String("average_gap"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(35.125))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("largest_gap"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(60.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("net_units"), new LongColumnType()),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("against_plan"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("average_price_to_cents"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(75.12))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("whole_average_price"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(75))
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
