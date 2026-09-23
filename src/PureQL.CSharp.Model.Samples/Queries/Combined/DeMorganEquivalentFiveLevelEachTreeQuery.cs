using Pure.Collections.Generic;
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
using PureQL.CSharp.Model.Aggregates;
using PureQL.CSharp.Model.Aggregates.Numeric;
using PureQL.CSharp.Model.ArrayReturnings;
using PureQL.CSharp.Model.BooleanOperations;
using PureQL.CSharp.Model.Comparisons;
using PureQL.CSharp.Model.EachBooleanOperations;
using PureQL.CSharp.Model.EachComparisons;
using PureQL.CSharp.Model.EachEqualities;
using PureQL.CSharp.Model.Fields;
using PureQL.CSharp.Model.Returnings;
using PureQL.CSharp.Model.Scalars;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using Guid = Pure.Primitives.Guid.Guid;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Combined;

/// <summary>
/// Selects the distinct rows of orders.order_id, the sum of order_items.item_qty as
/// qtySum and the count of order_items.item_id as itemCount from
/// schema_with_foreign_keys.orders, inner-joined to schema_with_foreign_keys.users on
/// orders.order_user_id equals users.user_id, inner-joined to
/// schema_with_foreign_keys.order_items on orders.order_id equals
/// order_items.item_order_id, keeping the rows where a 5-level and/not/or condition on
/// orders.order_status and orders.order_total, grouped by orders.order_id, keeping the
/// groups where (the count of order_items.item_id is at least 2 or the sum of
/// order_items.item_qty is at least 5) and not (the count of order_items.item_id is at
/// least 100), ordered by orders.order_id.
/// </summary>
public sealed record DeMorganEquivalentFiveLevelEachTreeQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public Query Value =>
        new Query(
            new FromExpression(
                new JoinedString(
                    new DotString(),
                    [new RelationalSchemaWithForeignKeys().Name, new OrdersTable().Name]
                ).TextValue
            ),
            [
                new SelectExpression(
                    new ArrayReturning(
                        new UuidArrayReturning(
                            new UuidField(
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
                    )
                ),
                new SelectExpression(
                    new SingleValueReturning(
                        new NumberReturning(
                            new NumberAggregate(
                                new SumNumber(
                                    new NumberArrayReturning(
                                        new NumberField(
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
                                )
                            )
                        )
                    ),
                    "qtySum"
                ),
                new SelectExpression(
                    new SingleValueReturning(
                        new NumberReturning(
                            new Count(
                                new ArrayReturning(
                                    new UuidArrayReturning(
                                        new UuidField(
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new RelationalSchemaWithForeignKeys().Name,
                                                    new OrderItemsTable().Name,
                                                ]
                                            ).TextValue,
                                            new ItemIdColumn().Name.TextValue
                                        )
                                    )
                                )
                            )
                        )
                    ),
                    "itemCount"
                ),
            ],
            new BooleanArrayReturning(
                new EachAndOperator([
                    new BooleanArrayReturning(
                        new EachOrOperator([
                            new BooleanArrayReturning(
                                new EachOrOperator([
                                    new BooleanArrayReturning(
                                        new EachNotOperator(
                                            new BooleanArrayReturning(
                                                new EachComparison(
                                                    new EachNumberComparison(
                                                        EachComparisonOperator.EachGreaterThan,
                                                        new NumberArrayReturning(
                                                            new NumberField(
                                                                new JoinedString(
                                                                    new DotString(),
                                                                    [
                                                                        new RelationalSchemaWithForeignKeys().Name,
                                                                        new OrdersTable().Name,
                                                                    ]
                                                                ).TextValue,
                                                                new OrderTotalColumn()
                                                                    .Name
                                                                    .TextValue
                                                            )
                                                        ),
                                                        new NumberReturning(
                                                            new NumberScalar(100)
                                                        )
                                                    )
                                                )
                                            )
                                        )
                                    ),
                                    new BooleanArrayReturning(
                                        new EachNotOperator(
                                            new BooleanArrayReturning(
                                                new EachEquality(
                                                    new EachStringEquality(
                                                        new StringArrayReturning(
                                                            new StringField(
                                                                new JoinedString(
                                                                    new DotString(),
                                                                    [
                                                                        new RelationalSchemaWithForeignKeys().Name,
                                                                        new OrdersTable().Name,
                                                                    ]
                                                                ).TextValue,
                                                                new OrderStatusColumn()
                                                                    .Name
                                                                    .TextValue
                                                            )
                                                        ),
                                                        new StringReturning(
                                                            new StringScalar("pending")
                                                        )
                                                    )
                                                )
                                            )
                                        )
                                    ),
                                ])
                            ),
                            new BooleanArrayReturning(
                                new EachComparison(
                                    new EachNumberComparison(
                                        EachComparisonOperator.EachGreaterThanOrEqual,
                                        new NumberArrayReturning(
                                            new NumberField(
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
                                        new NumberReturning(new NumberScalar(300))
                                    )
                                )
                            ),
                        ])
                    ),
                    new BooleanArrayReturning(
                        new EachOrOperator([
                            new BooleanArrayReturning(
                                new EachNotOperator(
                                    new BooleanArrayReturning(
                                        new EachEquality(
                                            new EachStringEquality(
                                                new StringArrayReturning(
                                                    new StringField(
                                                        new JoinedString(
                                                            new DotString(),
                                                            [
                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                new OrdersTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new OrderStatusColumn()
                                                            .Name
                                                            .TextValue
                                                    )
                                                ),
                                                new StringReturning(
                                                    new StringScalar("cancelled")
                                                )
                                            )
                                        )
                                    )
                                )
                            ),
                            new BooleanArrayReturning(
                                new EachAndOperator([
                                    new BooleanArrayReturning(
                                        new EachComparison(
                                            new EachNumberComparison(
                                                EachComparisonOperator.EachLessThan,
                                                new NumberArrayReturning(
                                                    new NumberField(
                                                        new JoinedString(
                                                            new DotString(),
                                                            [
                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                new OrdersTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new OrderTotalColumn()
                                                            .Name
                                                            .TextValue
                                                    )
                                                ),
                                                new NumberReturning(new NumberScalar(100))
                                            )
                                        )
                                    ),
                                    new BooleanArrayReturning(
                                        new EachEquality(
                                            new EachStringEquality(
                                                new StringArrayReturning(
                                                    new StringField(
                                                        new JoinedString(
                                                            new DotString(),
                                                            [
                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                new OrdersTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new OrderStatusColumn()
                                                            .Name
                                                            .TextValue
                                                    )
                                                ),
                                                new StringReturning(
                                                    new StringScalar("shipped")
                                                )
                                            )
                                        )
                                    ),
                                ])
                            ),
                        ])
                    ),
                ])
            ),
            [
                new Join(
                    JoinType.Inner,
                    new JoinedString(
                        new DotString(),
                        [
                            new RelationalSchemaWithForeignKeys().Name,
                            new UsersTable().Name,
                        ]
                    ).TextValue,
                    new BooleanArrayReturning(
                        new EachEquality(
                            new EachUuidEquality(
                                new UuidArrayReturning(
                                    new UuidField(
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
                                new UuidArrayReturning(
                                    new UuidField(
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
                            )
                        )
                    )
                ),
                new Join(
                    JoinType.Inner,
                    new JoinedString(
                        new DotString(),
                        [
                            new RelationalSchemaWithForeignKeys().Name,
                            new OrderItemsTable().Name,
                        ]
                    ).TextValue,
                    new BooleanArrayReturning(
                        new EachEquality(
                            new EachUuidEquality(
                                new UuidArrayReturning(
                                    new UuidField(
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
                                new UuidArrayReturning(
                                    new UuidField(
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
                            )
                        )
                    )
                ),
            ],
            [
                new Field(
                    new UuidField(
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
            new BooleanReturning(
                new BooleanOperator(
                    new AndOperator([
                        new BooleanReturning(
                            new BooleanOperator(
                                new OrOperator([
                                    new BooleanReturning(
                                        new Comparison(
                                            new NumberComparison(
                                                ComparisonOperator.GreaterThanOrEqual,
                                                new NumberReturning(
                                                    new Count(
                                                        new ArrayReturning(
                                                            new UuidArrayReturning(
                                                                new UuidField(
                                                                    new JoinedString(
                                                                        new DotString(),
                                                                        [
                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                            new OrderItemsTable().Name,
                                                                        ]
                                                                    ).TextValue,
                                                                    new ItemIdColumn()
                                                                        .Name
                                                                        .TextValue
                                                                )
                                                            )
                                                        )
                                                    )
                                                ),
                                                new NumberReturning(new NumberScalar(2))
                                            )
                                        )
                                    ),
                                    new BooleanReturning(
                                        new Comparison(
                                            new NumberComparison(
                                                ComparisonOperator.GreaterThanOrEqual,
                                                new NumberReturning(
                                                    new NumberAggregate(
                                                        new SumNumber(
                                                            new NumberArrayReturning(
                                                                new NumberField(
                                                                    new JoinedString(
                                                                        new DotString(),
                                                                        [
                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                            new OrderItemsTable().Name,
                                                                        ]
                                                                    ).TextValue,
                                                                    new ItemQtyColumn()
                                                                        .Name
                                                                        .TextValue
                                                                )
                                                            )
                                                        )
                                                    )
                                                ),
                                                new NumberReturning(new NumberScalar(5))
                                            )
                                        )
                                    ),
                                ])
                            )
                        ),
                        new BooleanReturning(
                            new BooleanOperator(
                                new NotOperator(
                                    new BooleanReturning(
                                        new Comparison(
                                            new NumberComparison(
                                                ComparisonOperator.GreaterThanOrEqual,
                                                new NumberReturning(
                                                    new Count(
                                                        new ArrayReturning(
                                                            new UuidArrayReturning(
                                                                new UuidField(
                                                                    new JoinedString(
                                                                        new DotString(),
                                                                        [
                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                            new OrderItemsTable().Name,
                                                                        ]
                                                                    ).TextValue,
                                                                    new ItemIdColumn()
                                                                        .Name
                                                                        .TextValue
                                                                )
                                                            )
                                                        )
                                                    )
                                                ),
                                                new NumberReturning(new NumberScalar(100))
                                            )
                                        )
                                    )
                                )
                            )
                        ),
                    ])
                )
            ),
            [
                new OrderByItem(
                    new Field(
                        new UuidField(
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
            pagination: null,
            true
        );

    /// <summary>
    /// The rows the query returns under SQL semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and <see cref="AuditSchemaDataSet"/>,
    /// in the order the query sorts them; rows tied on every sort key may come in either
    /// order.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new OrderIdColumn(),
                    new Column(new String("qtySum"), new DoubleColumnType()),
                    new Column(new String("itemCount"), new DoubleColumnType()),
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
                                new Column(new String("qtySum"), new DoubleColumnType()),
                                new InvariantCell(new Double(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("itemCount"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(2))
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
                                new Column(new String("qtySum"), new DoubleColumnType()),
                                new InvariantCell(new Double(5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("itemCount"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1))
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
