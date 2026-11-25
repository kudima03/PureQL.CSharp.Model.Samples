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
/// Selects, from schema_with_foreign_keys.orders: order_id, the running sum of half of
/// order_total over the orders of the user in order of placed_at and order_id from the
/// first to the current, rounded half to even to 2 digits as running_half_total_to_cents,
/// order_total divided by the sum of order_total (null when it is 0) and multiplied by
/// 100, rounded half to even to 1 digit as share_in_percent, the previous order_total
/// rounded half to even to a whole number in order of placed_at and order_id as
/// previous_rounded, and the rank by order_total rounded half to even to -1 digits,
/// descending, as rank_by_rounded_total, ordered by order_total rounded half to even to 2
/// digits and order_id.
/// </summary>
public sealed record RoundHalfToEvenOverWindowsQuery
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
                    "running_half_total_to_cents",
                    new RoundDecimalProjection(
                        new SumDecimalProjection(
                            new MultiplyDecimalRow([
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
                                new LiteralDecimal(0.5m),
                            ]),
                            over: new WindowWithFrame(
                                [
                                    new OrderItemRow(
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
                                    new OrderItemRow(
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
                                ],
                                new Frame(new All()),
                                [
                                    new FieldUuid(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new OrdersTable().Name,
                                            ]
                                        ).TextValue,
                                        new OrderUserIdColumn().Name.TextValue
                                    ),
                                ]
                            )
                        ),
                        new LiteralInteger(2),
                        RoundingMode.HalfEven
                    )
                ),
                new SelectItemProjection(
                    "share_in_percent",
                    new RoundDecimalNullableProjection(
                        new MultiplyDecimalNullableProjection([
                            new DivideDecimalNullableProjection([
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
                                new NullIfEqualDecimalNullableProjection(
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
                                        )
                                    ),
                                    new LiteralInteger(0)
                                ),
                            ]),
                            new LiteralInteger(100),
                        ]),
                        new LiteralInteger(1),
                        RoundingMode.HalfEven
                    )
                ),
                new SelectItemProjection(
                    "previous_rounded",
                    new PreviousIntegerNullableProjection(
                        new RoundWithoutDigitsIntegerRow(
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
                            RoundingMode.HalfEven
                        ),
                        new OrderedWindow([
                            new OrderItemRow(
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
                            new OrderItemRow(
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
                        ])
                    )
                ),
                new SelectItemProjection(
                    "rank_by_rounded_total",
                    new RankProjection(
                        new OrderedWindow([
                            new OrderItemRow(
                                new RoundDecimalRow(
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
                                    new LiteralInteger(-1),
                                    RoundingMode.HalfEven
                                ),
                                SortDirection.Desc
                            ),
                        ])
                    )
                ),
            ],
            orderBy:
            [
                new OrderItemProjectionExpression(
                    new RoundDecimalProjection(
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
                        new LiteralInteger(2),
                        RoundingMode.HalfEven
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
                    new Column(
                        new String("running_half_total_to_cents"),
                        new DoubleColumnType()
                    ),
                    new Column(new String("share_in_percent"), new DoubleColumnType()),
                    new Column(new String("previous_rounded"), new LongColumnType()),
                    new Column(new String("rank_by_rounded_total"), new LongColumnType()),
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
                                            "00000066-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("running_half_total_to_cents"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(75.25))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("share_in_percent"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(6.1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("previous_rounded"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(100))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rank_by_rounded_total"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(6))
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
                                new Column(
                                    new String("running_half_total_to_cents"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(37.62))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("share_in_percent"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(9.1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("previous_rounded"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(200))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rank_by_rounded_total"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(5))
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
                                            "00000065-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("running_half_total_to_cents"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(50.25))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("share_in_percent"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(12.2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("previous_rounded"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rank_by_rounded_total"),
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
                                            "0000006a-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("running_half_total_to_cents"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(50.25))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("share_in_percent"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(12.2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("previous_rounded"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(300))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rank_by_rounded_total"),
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
                                            "00000067-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("running_half_total_to_cents"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(100))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("share_in_percent"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(24.2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("previous_rounded"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(50))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rank_by_rounded_total"),
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
                                            "00000069-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("running_half_total_to_cents"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(187.62))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("share_in_percent"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(36.3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("previous_rounded"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(75))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rank_by_rounded_total"),
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
            ]
        );
}
