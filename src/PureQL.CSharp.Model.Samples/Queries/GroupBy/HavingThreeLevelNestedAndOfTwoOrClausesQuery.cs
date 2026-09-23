using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Samples.Columns;
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
using PureQL.CSharp.Model.Aggregates;
using PureQL.CSharp.Model.Aggregates.Numeric;
using PureQL.CSharp.Model.ArrayReturnings;
using PureQL.CSharp.Model.BooleanOperations;
using PureQL.CSharp.Model.Comparisons;
using PureQL.CSharp.Model.Fields;
using PureQL.CSharp.Model.Returnings;
using PureQL.CSharp.Model.Scalars;

namespace PureQL.CSharp.Model.Samples.Queries.GroupBy;

/// <summary>
/// Selects order_user_id from schema_with_foreign_keys.orders, grouped by order_user_id,
/// keeping the groups where (the count of order_id is greater than 2 or not (the maximum
/// of order_total is at least 300)) and (not (the count of order_id is greater than 0) or
/// the minimum of order_total is at least 50).
/// </summary>
public sealed record HavingThreeLevelNestedAndOfTwoOrClausesQuery
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
                                new OrderUserIdColumn().Name.TextValue
                            )
                        )
                    )
                ),
            ],
            where: null,
            join: null,
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
                        new OrderUserIdColumn().Name.TextValue
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
                                                ComparisonOperator.GreaterThan,
                                                new NumberReturning(
                                                    new Count(
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
                                                                    new OrderIdColumn()
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
                                        new BooleanOperator(
                                            new NotOperator(
                                                new BooleanReturning(
                                                    new Comparison(
                                                        new NumberComparison(
                                                            ComparisonOperator.GreaterThanOrEqual,
                                                            new NumberReturning(
                                                                new NumberAggregate(
                                                                    new MaxNumber(
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
                                                                        )
                                                                    )
                                                                )
                                                            ),
                                                            new NumberReturning(
                                                                new NumberScalar(300)
                                                            )
                                                        )
                                                    )
                                                )
                                            )
                                        )
                                    ),
                                ])
                            )
                        ),
                        new BooleanReturning(
                            new BooleanOperator(
                                new OrOperator([
                                    new BooleanReturning(
                                        new BooleanOperator(
                                            new NotOperator(
                                                new BooleanReturning(
                                                    new Comparison(
                                                        new NumberComparison(
                                                            ComparisonOperator.GreaterThan,
                                                            new NumberReturning(
                                                                new Count(
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
                                                                                new OrderIdColumn()
                                                                                    .Name
                                                                                    .TextValue
                                                                            )
                                                                        )
                                                                    )
                                                                )
                                                            ),
                                                            new NumberReturning(
                                                                new NumberScalar(0)
                                                            )
                                                        )
                                                    )
                                                )
                                            )
                                        )
                                    ),
                                    new BooleanReturning(
                                        new Comparison(
                                            new NumberComparison(
                                                ComparisonOperator.GreaterThanOrEqual,
                                                new NumberReturning(
                                                    new NumberAggregate(
                                                        new MinNumber(
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
                                                            )
                                                        )
                                                    )
                                                ),
                                                new NumberReturning(new NumberScalar(50))
                                            )
                                        )
                                    ),
                                ])
                            )
                        ),
                    ])
                )
            ),
            orderBy: null,
            pagination: null
        );
}
