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
/// Groups schema_with_foreign_keys.orders joined to schema_with_foreign_keys.users by
/// order_user_id as user_id, having the log10 of the greatest of the sum of order_total
/// and 1 greater than 2.1: the user_id, the last order_total over the first (by placed_at
/// and order_id, nullIfEqual with 0) to the power of 1 over the greatest of the months
/// from the earliest signup_date to the latest placed_on and 1, less 1; the square root
/// of the sum of the squares of order_total; the ln of the greatest of the sum and 2 over
/// the ln of the greatest of the sum over all rows and 2; the sum of the ln of the
/// greatest of order_total and 1; and the count of orders whose square root of
/// order_total is above 10, ordered by the square root of the sum of squares descending
/// and user_id.
/// </summary>
public sealed record GrowthPerUserQuery
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
                new Log10DecimalGroup(
                    new GreatestDecimalGroup([
                        new SumDecimalGroup(
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
                        new LiteralInteger(1),
                    ])
                ),
                new LiteralDecimal(2.1m)
            ),
            select:
            [
                new SelectItemGroup("user_id", new KeyUuid("user_id")),
                new SelectItemGroup(
                    "monthly_growth",
                    new SubtractDecimalNullableGroup([
                        new PowerDecimalNullableGroup(
                            new DivideDecimalNullableGroup([
                                new LastDecimalGroup(
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
                                    ]
                                ),
                                new NullIfEqualDecimalNullableGroup(
                                    new FirstDecimalGroup(
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
                                        ]
                                    ),
                                    new LiteralInteger(0)
                                ),
                            ]),
                            new DivideDecimalGroup([
                                new LiteralInteger(1),
                                new GreatestIntegerGroup([
                                    new DateDiffMonthsIntegerGroup(
                                        new MaxDateGroup(
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
                                        new MinDateGroup(
                                            new FieldDate(
                                                new JoinedString(
                                                    new DotString(),
                                                    [
                                                        new RelationalSchemaWithForeignKeys().Name,
                                                        new UsersTable().Name,
                                                    ]
                                                ).TextValue,
                                                new SignupDateColumn().Name.TextValue
                                            )
                                        )
                                    ),
                                    new LiteralInteger(1),
                                ]),
                            ])
                        ),
                        new LiteralInteger(1),
                    ])
                ),
                new SelectItemGroup(
                    "norm_of_totals",
                    new SqrtDecimalGroup(
                        new SumDecimalGroup(
                            new PowerDecimalRow(
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
                                new LiteralInteger(2)
                            )
                        )
                    )
                ),
                new SelectItemGroup(
                    "log_revenue_share",
                    new DivideDecimalGroup([
                        new LnDecimalGroup(
                            new GreatestDecimalGroup([
                                new SumDecimalGroup(
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
                                new LiteralInteger(2),
                            ])
                        ),
                        new LnDecimalGroup(
                            new GreatestDecimalGroup([
                                new SumDecimalGroup(
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
                                    over: AggregateOver.All
                                ),
                                new LiteralInteger(2),
                            ])
                        ),
                    ])
                ),
                new SelectItemGroup(
                    "sum_of_logarithms",
                    new SumDecimalGroup(
                        new LnDecimalRow(
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
                        )
                    )
                ),
                new SelectItemGroup(
                    "large_orders",
                    new CountGroup(
                        new GreaterThanDecimalRow(
                            new SqrtDecimalRow(
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
                            new LiteralInteger(10)
                        )
                    )
                ),
            ],
            orderBy:
            [
                new OrderItemGroupExpression(
                    new SqrtDecimalGroup(
                        new SumDecimalGroup(
                            new PowerDecimalRow(
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
                                new LiteralInteger(2)
                            )
                        )
                    ),
                    SortDirection.Desc
                ),
                new OrderItemGroupExpression(new KeyUuid("user_id")),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over <see
    /// cref="SchemaDataSetWithForeignKeys"/> and <see cref="AuditSchemaDataSet"/>, in
    /// this order. Every value is computed exactly with Python decimal at 120 digits,
    /// rounding each operator result as the specification rounds a decimal, and checked
    /// against PostgreSQL numeric at 50 decimal places.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new UserIdColumn(),
                    new Column(new String("monthly_growth"), new DoubleColumnType()),
                    new Column(new String("norm_of_totals"), new DoubleColumnType()),
                    new Column(new String("log_revenue_share"), new DoubleColumnType()),
                    new Column(new String("sum_of_logarithms"), new DoubleColumnType()),
                    new Column(new String("large_orders"), new LongColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new UserIdColumn(),
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
                                    new String("monthly_growth"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.024130798350772295))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("norm_of_totals"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(309.2936509209331))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("log_revenue_share"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.8824896563567544))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sum_of_logarithms"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(10.024598378285186))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("large_orders"),
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
                                new UserIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000002-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("monthly_growth"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("norm_of_totals"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(200))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("log_revenue_share"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.7888042734606033))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sum_of_logarithms"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(5.298317366548036))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("large_orders"),
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
                                new UserIdColumn(),
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
                                    new String("monthly_growth"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-0.013335945431202744))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("norm_of_totals"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(112.25083518620251))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("log_revenue_share"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.7464701025183629))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sum_of_logarithms"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(8.522180732927277))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("large_orders"),
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
