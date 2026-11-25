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
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.ProjectionExpressions;
using PureQL.CSharp.Model.RowExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using Guid = Pure.Primitives.Guid.Guid;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Windows;

/// <summary>
/// Selects order_id, order_tenant_id, placed_on and order_total from
/// schema_with_foreign_keys.orders next to aggregates over ranges of values in a
/// partition by order_tenant_id: the sum of order_total over the orders placed_on the
/// same day or 1 day before, over the 11 months before the startOfMonth of placed_on, and
/// over every earlier placed_on (a balance that includes every order of the day), the
/// count of orders placed_at within 90000 seconds on either side, and, over all rows, the
/// count of orders whose order_total lies from 25 below to 25.25 above, ordered by
/// order_tenant_id, placed_on and order_id.
/// </summary>
public sealed record RangeFramesQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithoutPagination(
            from: new FromEntity(
                new JoinedString(
                    new DotString(),
                    [new RelationalSchemaWithForeignKeys().Name, new OrdersTable().Name]
                ).TextValue
            ),
            select:
            [
                new SelectItemProjection(
                    "order_id",
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
                ),
                new SelectItemProjection(
                    "order_tenant_id",
                    new FieldUuid(
                        new JoinedString(
                            new DotString(),
                            [
                                new RelationalSchemaWithForeignKeys().Name,
                                new OrdersTable().Name,
                            ]
                        ).TextValue,
                        new OrderTenantIdColumn().Name.TextValue
                    )
                ),
                new SelectItemProjection(
                    "placed_on",
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
                ),
                new SelectItemProjection(
                    "order_total",
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
                new SelectItemProjection(
                    "spent_on_the_day_and_the_day_before",
                    new SumDecimalProjection(
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
                        over: new WindowWithoutFrameWithRangeDate(
                            [
                                new OrderItemDateRow(
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
                                ),
                            ],
                            new RangeDate(new DistanceDateDays(1)),
                            [
                                new FieldUuid(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new OrdersTable().Name,
                                        ]
                                    ).TextValue,
                                    new OrderTenantIdColumn().Name.TextValue
                                ),
                            ]
                        )
                    )
                ),
                new SelectItemProjection(
                    "spent_in_last_12_months",
                    new SumDecimalProjection(
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
                        over: new WindowWithoutFrameWithRangeDate(
                            [
                                new OrderItemDateRow(
                                    new StartOfMonthDateRow(
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
                                ),
                            ],
                            new RangeDate(new DistanceDateMonths(11)),
                            [
                                new FieldUuid(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new OrdersTable().Name,
                                        ]
                                    ).TextValue,
                                    new OrderTenantIdColumn().Name.TextValue
                                ),
                            ]
                        )
                    )
                ),
                new SelectItemProjection(
                    "balance_to_date",
                    new SumDecimalProjection(
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
                        over: new WindowWithoutFrameWithRangeDate(
                            [
                                new OrderItemDateRow(
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
                                ),
                            ],
                            new RangeDate(new All()),
                            [
                                new FieldUuid(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new OrdersTable().Name,
                                        ]
                                    ).TextValue,
                                    new OrderTenantIdColumn().Name.TextValue
                                ),
                            ]
                        )
                    )
                ),
                new SelectItemProjection(
                    "orders_within_25_hours",
                    new CountProjection(
                        over: new WindowWithoutFrameWithRangeDatetime(
                            [
                                new OrderItemDatetimeRow(
                                    new FieldDatetime(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new OrdersTable().Name,
                                            ]
                                        ).TextValue,
                                        new PlacedAtColumn().Name.TextValue
                                    )
                                ),
                            ],
                            new RangeDatetime(
                                new DistanceDatetimeSeconds(90000),
                                new DistanceDatetimeSeconds(90000)
                            ),
                            [
                                new FieldUuid(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new OrdersTable().Name,
                                        ]
                                    ).TextValue,
                                    new OrderTenantIdColumn().Name.TextValue
                                ),
                            ]
                        )
                    )
                ),
                new SelectItemProjection(
                    "orders_of_similar_size",
                    new CountProjection(
                        over: new WindowWithoutFrameWithRangeDecimal(
                            [
                                new OrderItemDecimalRow(
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
                            ],
                            new RangeDecimal(25, new LiteralDecimal(25.25m))
                        )
                    )
                ),
            ],
            orderBy:
            [
                new OrderItemProjectionExpression(
                    new FieldUuid(
                        new JoinedString(
                            new DotString(),
                            [
                                new RelationalSchemaWithForeignKeys().Name,
                                new OrdersTable().Name,
                            ]
                        ).TextValue,
                        new OrderTenantIdColumn().Name.TextValue
                    )
                ),
                new OrderItemProjectionExpression(
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
                ),
                new OrderItemProjectionExpression(
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
                ),
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
                    new OrderIdColumn(),
                    new OrderTenantIdColumn(),
                    new PlacedOnColumn(),
                    new OrderTotalColumn(),
                    new Column(
                        new String("spent_on_the_day_and_the_day_before"),
                        new DoubleColumnType()
                    ),
                    new Column(
                        new String("spent_in_last_12_months"),
                        new DoubleColumnType()
                    ),
                    new Column(new String("balance_to_date"), new DoubleColumnType()),
                    new Column(
                        new String("orders_within_25_hours"),
                        new LongColumnType()
                    ),
                    new Column(
                        new String("orders_of_similar_size"),
                        new LongColumnType()
                    ),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new OrderIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000065-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new OrderTenantIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000385-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
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
                                new OrderTotalColumn(),
                                new InvariantCell(new Double(100.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("spent_on_the_day_and_the_day_before"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(100.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("spent_in_last_12_months"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(350.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("balance_to_date"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(100.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("orders_within_25_hours"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("orders_of_similar_size"),
                                    new LongColumnType()
                                ),
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
                                new OrderIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000066-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new OrderTenantIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000385-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new PlacedOnColumn(),
                                new InvariantCell(
                                    new Date(
                                        new UShort(2),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new OrderTotalColumn(),
                                new InvariantCell(new Double(50))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("spent_on_the_day_and_the_day_before"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(150.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("spent_in_last_12_months"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(350.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("balance_to_date"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(150.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("orders_within_25_hours"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("orders_of_similar_size"),
                                    new LongColumnType()
                                ),
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
                                new OrderIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000067-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new OrderTenantIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000385-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
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
                                new OrderTotalColumn(),
                                new InvariantCell(new Double(200))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("spent_on_the_day_and_the_day_before"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(250))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("spent_in_last_12_months"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(350.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("balance_to_date"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(350.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("orders_within_25_hours"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("orders_of_similar_size"),
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
                                new OrderIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000068-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new OrderTenantIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000386-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new PlacedOnColumn(),
                                new InvariantCell(
                                    new Date(
                                        new UShort(4),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new OrderTotalColumn(),
                                new InvariantCell(new Double(75.25))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("spent_on_the_day_and_the_day_before"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(75.25))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("spent_in_last_12_months"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(475.75))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("balance_to_date"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(75.25))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("orders_within_25_hours"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("orders_of_similar_size"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(3))
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
                                new OrderIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000069-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new OrderTenantIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000386-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
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
                                new OrderTotalColumn(),
                                new InvariantCell(new Double(300))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("spent_on_the_day_and_the_day_before"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(375.25))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("spent_in_last_12_months"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(475.75))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("balance_to_date"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(375.25))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("orders_within_25_hours"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("orders_of_similar_size"),
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
                                new OrderIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "0000006a-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new OrderTenantIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000386-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new PlacedOnColumn(),
                                new InvariantCell(
                                    new Date(
                                        new UShort(6),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new OrderTotalColumn(),
                                new InvariantCell(new Double(100.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("spent_on_the_day_and_the_day_before"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(400.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("spent_in_last_12_months"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(475.75))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("balance_to_date"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(475.75))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("orders_within_25_hours"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("orders_of_similar_size"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
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
