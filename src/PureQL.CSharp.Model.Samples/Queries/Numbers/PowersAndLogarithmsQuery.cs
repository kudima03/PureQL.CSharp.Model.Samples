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

namespace PureQL.CSharp.Model.Samples.Queries.Numbers;

/// <summary>
/// Selects from schema_with_foreign_keys.orders inner joined to
/// schema_with_foreign_keys.users on order_user_id, where log10 of the greatest of
/// order_total and 1 is at least 2: order_id, order_total compounded at 0.05 for 3 years
/// with power and continuously with exp, the yearly growth from user_score to order_total
/// over 3 years (a power of the ratio guarded by nullIfEqual, to 1 divided by 3, minus 1,
/// which is nullable), the natural logarithm of 1.05, the number of digits before the
/// point from floor of log10 plus 1, the logarithm of order_total to base 2, and the
/// square root of order_total, ordered by order_id.
/// </summary>
public sealed record PowersAndLogarithmsQuery
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
            joins:
            [
                new JoinEntity(
                    JoinKind.Inner,
                    new JoinedString(
                        new DotString(),
                        [
                            new RelationalSchemaWithForeignKeys().Name,
                            new UsersTable().Name,
                        ]
                    ).TextValue,
                    new EqualUuidRow(
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
                        new FieldUuid(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue,
                            new UserIdColumn().Name.TextValue
                        )
                    )
                ),
            ],
            where: new GreaterThanOrEqualDecimalRow(
                new Log10DecimalRow(
                    new GreatestDecimalRow([
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
                        new LiteralInteger(1),
                    ])
                ),
                new LiteralInteger(2)
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
                    "compounded_total",
                    new MultiplyDecimalProjection([
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
                        new PowerDecimalProjection(
                            new AddDecimalProjection([
                                new LiteralInteger(1),
                                new LiteralDecimal(0.05m),
                            ]),
                            new LiteralInteger(3)
                        ),
                    ])
                ),
                new SelectItemProjection(
                    "continuously_compounded_total",
                    new MultiplyDecimalProjection([
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
                        new ExpDecimalProjection(
                            new MultiplyDecimalProjection([
                                new LiteralDecimal(0.05m),
                                new LiteralInteger(3),
                            ])
                        ),
                    ])
                ),
                new SelectItemProjection(
                    "yearly_growth_from_score",
                    new SubtractDecimalNullableProjection([
                        new PowerDecimalNullableProjection(
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
                                    new FieldDecimalNullable(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new UsersTable().Name,
                                            ]
                                        ).TextValue,
                                        new UserScoreColumn().Name.TextValue
                                    ),
                                    new LiteralInteger(0)
                                ),
                            ]),
                            new DivideDecimalProjection([
                                new LiteralInteger(1),
                                new LiteralInteger(3),
                            ])
                        ),
                        new LiteralInteger(1),
                    ])
                ),
                new SelectItemProjection(
                    "continuous_rate",
                    new LnDecimalProjection(
                        new AddDecimalProjection([
                            new LiteralInteger(1),
                            new LiteralDecimal(0.05m),
                        ])
                    )
                ),
                new SelectItemProjection(
                    "digits_before_the_point",
                    new AddIntegerProjection([
                        new FloorIntegerProjection(
                            new Log10DecimalProjection(
                                new GreatestDecimalProjection([
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
                                    new LiteralInteger(1),
                                ])
                            )
                        ),
                        new LiteralInteger(1),
                    ])
                ),
                new SelectItemProjection(
                    "doublings_from_one",
                    new LogDecimalProjection(
                        new GreatestDecimalProjection([
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
                            new LiteralInteger(1),
                        ]),
                        new LiteralInteger(2)
                    )
                ),
                new SelectItemProjection(
                    "square_root_of_total",
                    new SqrtDecimalProjection(
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
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and
    /// <see cref="AuditSchemaDataSet"/>, in this order.
    /// PostgreSQL computes the powers and logarithms in numeric at 50 decimal places, and
    /// each value matches the exact one rounded as the specification rounds a decimal,
    /// checked with Python decimal.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new OrderIdColumn(),
                    new Column(new String("compounded_total"), new DoubleColumnType()),
                    new Column(
                        new String("continuously_compounded_total"),
                        new DoubleColumnType()
                    ),
                    new Column(
                        new String("yearly_growth_from_score"),
                        new DoubleColumnType()
                    ),
                    new Column(new String("continuous_rate"), new DoubleColumnType()),
                    new Column(
                        new String("digits_before_the_point"),
                        new LongColumnType()
                    ),
                    new Column(new String("doublings_from_one"), new DoubleColumnType()),
                    new Column(
                        new String("square_root_of_total"),
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
                                    new String("compounded_total"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(116.3413125))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("continuously_compounded_total"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(116.76434139419246))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("yearly_growth_from_score"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.4962871135284913))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("continuous_rate"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.048790164169432))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("digits_before_the_point"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("doublings_from_one"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(6.651051691178929))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("square_root_of_total"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(10.024968827881711))
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
                                    new String("compounded_total"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(231.525))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("continuously_compounded_total"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(232.36684854565664))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("yearly_growth_from_score"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("continuous_rate"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.048790164169432))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("digits_before_the_point"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("doublings_from_one"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(7.643856189774724))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("square_root_of_total"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(14.142135623730951))
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
                                    new String("compounded_total"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(347.2875))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("continuously_compounded_total"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(348.5502728184849))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("yearly_growth_from_score"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1.1544346900318838))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("continuous_rate"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.048790164169432))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("digits_before_the_point"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("doublings_from_one"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(8.228818690495881))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("square_root_of_total"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(17.320508075688775))
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
                                    new String("compounded_total"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(116.3413125))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("continuously_compounded_total"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(116.76434139419246))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("yearly_growth_from_score"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("continuous_rate"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.048790164169432))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("digits_before_the_point"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("doublings_from_one"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(6.651051691178929))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("square_root_of_total"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(10.024968827881711))
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
