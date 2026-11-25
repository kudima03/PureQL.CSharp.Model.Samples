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
/// Selects, per order_user_id of schema_with_foreign_keys.orders, the key, the mean hour
/// of the day as an angle (the atan2 of the sums of the sines and of the cosines of the
/// hour of placed_at times pi divided by 12), the same as an hour (that angle times 12
/// divided by pi, plus 24, modulo 24), the concentration (the square root of the sum of
/// the two squared sums divided by the count), the average sine, and the count of orders
/// whose cosine is below -0.9, keeping the users for whom the sum of the two squared sums
/// is above 2, ordered by the mean angle, then by the key.
/// </summary>
public sealed record CircularMeanHourPerUserQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainGroupedQueryWithoutPagination(
            from: new FromEntity(
                new JoinedString(
                    new DotString(),
                    [new RelationalSchemaWithForeignKeys().Name, new OrdersTable().Name]
                ).TextValue
            ),
            groupBy:
            [
                new GroupKey(
                    "user_id",
                    new FieldUuid(
                        new JoinedString(
                            new DotString(),
                            [
                                new RelationalSchemaWithForeignKeys().Name,
                                new OrdersTable().Name,
                            ]
                        ).TextValue,
                        new OrderUserIdColumn().Name.TextValue
                    )
                ),
            ],
            having: new GreaterThanDecimalGroup(
                new AddDecimalGroup([
                    new PowerDecimalGroup(
                        new SumDecimalGroup(
                            new SinDecimalRow(
                                new MultiplyDecimalRow([
                                    new HourDatetimeIntegerRow(
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
                                    new DivideDecimalRow([
                                        new PiRow(),
                                        new LiteralInteger(12),
                                    ]),
                                ])
                            )
                        ),
                        new LiteralInteger(2)
                    ),
                    new PowerDecimalGroup(
                        new SumDecimalGroup(
                            new CosDecimalRow(
                                new MultiplyDecimalRow([
                                    new HourDatetimeIntegerRow(
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
                                    new DivideDecimalRow([
                                        new PiRow(),
                                        new LiteralInteger(12),
                                    ]),
                                ])
                            )
                        ),
                        new LiteralInteger(2)
                    ),
                ]),
                new LiteralInteger(2)
            ),
            select:
            [
                new SelectItemGroup("user_id", new KeyUuid("user_id")),
                new SelectItemGroup(
                    "mean_hour_angle",
                    new Atan2DecimalGroup(
                        new SumDecimalGroup(
                            new SinDecimalRow(
                                new MultiplyDecimalRow([
                                    new HourDatetimeIntegerRow(
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
                                    new DivideDecimalRow([
                                        new PiRow(),
                                        new LiteralInteger(12),
                                    ]),
                                ])
                            )
                        ),
                        new SumDecimalGroup(
                            new CosDecimalRow(
                                new MultiplyDecimalRow([
                                    new HourDatetimeIntegerRow(
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
                                    new DivideDecimalRow([
                                        new PiRow(),
                                        new LiteralInteger(12),
                                    ]),
                                ])
                            )
                        )
                    )
                ),
                new SelectItemGroup(
                    "mean_hour",
                    new ModuloDecimalGroup(
                        new AddDecimalGroup([
                            new MultiplyDecimalGroup([
                                new Atan2DecimalGroup(
                                    new SumDecimalGroup(
                                        new SinDecimalRow(
                                            new MultiplyDecimalRow([
                                                new HourDatetimeIntegerRow(
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
                                                new DivideDecimalRow([
                                                    new PiRow(),
                                                    new LiteralInteger(12),
                                                ]),
                                            ])
                                        )
                                    ),
                                    new SumDecimalGroup(
                                        new CosDecimalRow(
                                            new MultiplyDecimalRow([
                                                new HourDatetimeIntegerRow(
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
                                                new DivideDecimalRow([
                                                    new PiRow(),
                                                    new LiteralInteger(12),
                                                ]),
                                            ])
                                        )
                                    )
                                ),
                                new DivideDecimalGroup([
                                    new LiteralInteger(12),
                                    new PiGroup(),
                                ]),
                            ]),
                            new LiteralInteger(24),
                        ]),
                        new LiteralInteger(24)
                    )
                ),
                new SelectItemGroup(
                    "concentration",
                    new DivideDecimalGroup([
                        new SqrtDecimalGroup(
                            new AddDecimalGroup([
                                new PowerDecimalGroup(
                                    new SumDecimalGroup(
                                        new SinDecimalRow(
                                            new MultiplyDecimalRow([
                                                new HourDatetimeIntegerRow(
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
                                                new DivideDecimalRow([
                                                    new PiRow(),
                                                    new LiteralInteger(12),
                                                ]),
                                            ])
                                        )
                                    ),
                                    new LiteralInteger(2)
                                ),
                                new PowerDecimalGroup(
                                    new SumDecimalGroup(
                                        new CosDecimalRow(
                                            new MultiplyDecimalRow([
                                                new HourDatetimeIntegerRow(
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
                                                new DivideDecimalRow([
                                                    new PiRow(),
                                                    new LiteralInteger(12),
                                                ]),
                                            ])
                                        )
                                    ),
                                    new LiteralInteger(2)
                                ),
                            ])
                        ),
                        new CountGroup(),
                    ])
                ),
                new SelectItemGroup(
                    "average_sine",
                    new AverageDecimalGroup(
                        new SinDecimalRow(
                            new MultiplyDecimalRow([
                                new HourDatetimeIntegerRow(
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
                                new DivideDecimalRow([
                                    new PiRow(),
                                    new LiteralInteger(12),
                                ]),
                            ])
                        )
                    )
                ),
                new SelectItemGroup(
                    "midday_orders",
                    new CountGroup(
                        predicate: new LessThanDecimalRow(
                            new CosDecimalRow(
                                new MultiplyDecimalRow([
                                    new HourDatetimeIntegerRow(
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
                                    new DivideDecimalRow([
                                        new PiRow(),
                                        new LiteralInteger(12),
                                    ]),
                                ])
                            ),
                            new LiteralDecimal(-0.9m)
                        )
                    )
                ),
            ],
            orderBy:
            [
                new OrderItemGroupExpression(
                    new Atan2DecimalGroup(
                        new SumDecimalGroup(
                            new SinDecimalRow(
                                new MultiplyDecimalRow([
                                    new HourDatetimeIntegerRow(
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
                                    new DivideDecimalRow([
                                        new PiRow(),
                                        new LiteralInteger(12),
                                    ]),
                                ])
                            )
                        ),
                        new SumDecimalGroup(
                            new CosDecimalRow(
                                new MultiplyDecimalRow([
                                    new HourDatetimeIntegerRow(
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
                                    new DivideDecimalRow([
                                        new PiRow(),
                                        new LiteralInteger(12),
                                    ]),
                                ])
                            )
                        )
                    )
                ),
                new OrderItemGroupExpression(new KeyUuid("user_id")),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and
    /// <see cref="AuditSchemaDataSet"/>, in this order.
    /// The trigonometric functions of the specification are computed with Python
    /// decimal at 120 digits, each result rounded as the specification rounds a
    /// decimal, and then written as a double.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("user_id"), new UuidColumnType()),
                    new Column(new String("mean_hour_angle"), new DoubleColumnType()),
                    new Column(new String("mean_hour"), new DoubleColumnType()),
                    new Column(new String("concentration"), new DoubleColumnType()),
                    new Column(new String("average_sine"), new DoubleColumnType()),
                    new Column(new String("midday_orders"), new LongColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("user_id"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000003-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("mean_hour_angle"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-2.748893571891069))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("mean_hour"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(13.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("concentration"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.9914448613738104))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("average_sine"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-0.37940952255126037))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("midday_orders"),
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
                                new Column(new String("user_id"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000001-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("mean_hour_angle"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(2.748893571891069))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("mean_hour"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(10.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("concentration"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.9914448613738104))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("average_sine"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.37940952255126037))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("midday_orders"),
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
