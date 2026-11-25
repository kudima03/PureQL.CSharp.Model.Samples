using Pure.Collections.Generic;
using Pure.Primitives.Bool;
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
/// Selects from schema_with_foreign_keys.orders, with x the dateDiffDays of placed_on
/// from 2024-06-01: the id, the regressionSlope of order_total on x over all rows, over
/// the order_tenant_id partition, and over a window of that partition ordered by
/// placed_at and id with a frame of the 11 preceding rows, the regressionIntercept and
/// regressionRSquared over that window, the regressionSlope over a range of the 1
/// preceding day of placed_on in the partition, and whether order_total is greater than
/// the value the rolling line predicts (its intercept plus its slope times x, coalesced
/// with order_total itself), ordered by id.
/// </summary>
public sealed record RegressionOverWindowsQuery
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
                    "id",
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
                    "trend_of_all_orders",
                    new RegressionSlopeProjection(
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
                        new DateDiffDaysIntegerRow(
                            new FieldDate(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new OrdersTable().Name,
                                    ]
                                ).TextValue,
                                new PlacedOnColumn().Name.TextValue
                            ),
                            new LiteralDate(new DateOnly(2024, 6, 1))
                        )
                    )
                ),
                new SelectItemProjection(
                    "trend_of_the_customer",
                    new RegressionSlopeProjection(
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
                        new DateDiffDaysIntegerRow(
                            new FieldDate(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new OrdersTable().Name,
                                    ]
                                ).TextValue,
                                new PlacedOnColumn().Name.TextValue
                            ),
                            new LiteralDate(new DateOnly(2024, 6, 1))
                        ),
                        over: new WindowWithoutFrameWithoutRange([
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
                        ])
                    )
                ),
                new SelectItemProjection(
                    "rolling_trend",
                    new RegressionSlopeProjection(
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
                        new DateDiffDaysIntegerRow(
                            new FieldDate(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new OrdersTable().Name,
                                    ]
                                ).TextValue,
                                new PlacedOnColumn().Name.TextValue
                            ),
                            new LiteralDate(new DateOnly(2024, 6, 1))
                        ),
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
                            new Frame(11),
                            partitionBy:
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
                    "rolling_intercept",
                    new RegressionInterceptProjection(
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
                        new DateDiffDaysIntegerRow(
                            new FieldDate(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new OrdersTable().Name,
                                    ]
                                ).TextValue,
                                new PlacedOnColumn().Name.TextValue
                            ),
                            new LiteralDate(new DateOnly(2024, 6, 1))
                        ),
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
                            new Frame(11),
                            partitionBy:
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
                    "rolling_fit",
                    new RegressionRSquaredProjection(
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
                        new DateDiffDaysIntegerRow(
                            new FieldDate(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new OrdersTable().Name,
                                    ]
                                ).TextValue,
                                new PlacedOnColumn().Name.TextValue
                            ),
                            new LiteralDate(new DateOnly(2024, 6, 1))
                        ),
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
                            new Frame(11),
                            partitionBy:
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
                    "trend_of_ninety_days",
                    new RegressionSlopeProjection(
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
                        new DateDiffDaysIntegerRow(
                            new FieldDate(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new OrdersTable().Name,
                                    ]
                                ).TextValue,
                                new PlacedOnColumn().Name.TextValue
                            ),
                            new LiteralDate(new DateOnly(2024, 6, 1))
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
                            partitionBy:
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
                    "above_its_trend",
                    new GreaterThanDecimalProjection(
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
                        new CoalesceDecimalProjection([
                            new AddDecimalNullableProjection([
                                new RegressionInterceptProjection(
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
                                    new DateDiffDaysIntegerRow(
                                        new FieldDate(
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new RelationalSchemaWithForeignKeys().Name,
                                                    new OrdersTable().Name,
                                                ]
                                            ).TextValue,
                                            new PlacedOnColumn().Name.TextValue
                                        ),
                                        new LiteralDate(new DateOnly(2024, 6, 1))
                                    ),
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
                                        new Frame(11),
                                        partitionBy:
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
                                ),
                                new MultiplyDecimalNullableProjection([
                                    new RegressionSlopeProjection(
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
                                        new DateDiffDaysIntegerRow(
                                            new FieldDate(
                                                new JoinedString(
                                                    new DotString(),
                                                    [
                                                        new RelationalSchemaWithForeignKeys().Name,
                                                        new OrdersTable().Name,
                                                    ]
                                                ).TextValue,
                                                new PlacedOnColumn().Name.TextValue
                                            ),
                                            new LiteralDate(new DateOnly(2024, 6, 1))
                                        ),
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
                                                        new PlacedAtColumn()
                                                            .Name
                                                            .TextValue
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
                                            new Frame(11),
                                            partitionBy:
                                            [
                                                new FieldUuid(
                                                    new JoinedString(
                                                        new DotString(),
                                                        [
                                                            new RelationalSchemaWithForeignKeys().Name,
                                                            new OrdersTable().Name,
                                                        ]
                                                    ).TextValue,
                                                    new OrderTenantIdColumn()
                                                        .Name
                                                        .TextValue
                                                ),
                                            ]
                                        )
                                    ),
                                    new DateDiffDaysIntegerProjection(
                                        new FieldDate(
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new RelationalSchemaWithForeignKeys().Name,
                                                    new OrdersTable().Name,
                                                ]
                                            ).TextValue,
                                            new PlacedOnColumn().Name.TextValue
                                        ),
                                        new LiteralDate(new DateOnly(2024, 6, 1))
                                    ),
                                ]),
                            ]),
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
                        new OrderIdColumn().Name.TextValue
                    )
                ),
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
                    new Column(new String("id"), new UuidColumnType()),
                    new Column(new String("trend_of_all_orders"), new DoubleColumnType()),
                    new Column(
                        new String("trend_of_the_customer"),
                        new DoubleColumnType()
                    ),
                    new Column(new String("rolling_trend"), new DoubleColumnType()),
                    new Column(new String("rolling_intercept"), new DoubleColumnType()),
                    new Column(new String("rolling_fit"), new DoubleColumnType()),
                    new Column(
                        new String("trend_of_ninety_days"),
                        new DoubleColumnType()
                    ),
                    new Column(new String("above_its_trend"), new BoolColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("id"), new UuidColumnType()),
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
                                    new String("trend_of_all_orders"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(17.864285714285714))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("trend_of_the_customer"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(49.75))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rolling_trend"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rolling_intercept"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rolling_fit"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("trend_of_ninety_days"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("above_its_trend"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
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
                                new Column(new String("id"), new UuidColumnType()),
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
                                    new String("trend_of_all_orders"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(17.864285714285714))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("trend_of_the_customer"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(49.75))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rolling_trend"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-50.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rolling_intercept"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(100.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rolling_fit"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("trend_of_ninety_days"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-50.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("above_its_trend"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
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
                                new Column(new String("id"), new UuidColumnType()),
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
                                    new String("trend_of_all_orders"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(17.864285714285714))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("trend_of_the_customer"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(49.75))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rolling_trend"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(49.75))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rolling_intercept"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(67.08333333333333))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rolling_fit"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.424897354830403))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("trend_of_ninety_days"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(150))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("above_its_trend"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
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
                                new Column(new String("id"), new UuidColumnType()),
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
                                    new String("trend_of_all_orders"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(17.864285714285714))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("trend_of_the_customer"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(12.625))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rolling_trend"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rolling_intercept"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rolling_fit"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("trend_of_ninety_days"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("above_its_trend"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
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
                                new Column(new String("id"), new UuidColumnType()),
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
                                    new String("trend_of_all_orders"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(17.864285714285714))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("trend_of_the_customer"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(12.625))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rolling_trend"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(224.75))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rolling_intercept"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-599))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rolling_fit"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("trend_of_ninety_days"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(224.75))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("above_its_trend"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
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
                                new Column(new String("id"), new UuidColumnType()),
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
                                    new String("trend_of_all_orders"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(17.864285714285714))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("trend_of_the_customer"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(12.625))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rolling_trend"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(12.625))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rolling_intercept"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(108.08333333333333))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rolling_fit"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.010515006122844464))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("trend_of_ninety_days"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-199.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("above_its_trend"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
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
