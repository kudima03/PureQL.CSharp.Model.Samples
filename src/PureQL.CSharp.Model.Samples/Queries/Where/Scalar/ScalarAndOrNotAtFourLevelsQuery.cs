using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Samples.Columns;
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
using PureQL.CSharp.Model.ArrayReturnings;
using PureQL.CSharp.Model.BooleanOperations;
using PureQL.CSharp.Model.Comparisons;
using PureQL.CSharp.Model.Equalities;
using PureQL.CSharp.Model.Fields;
using PureQL.CSharp.Model.Returnings;
using PureQL.CSharp.Model.Scalars;

namespace PureQL.CSharp.Model.Samples.Queries.Where.Scalar;

/// <summary>
/// Selects order_status from schema_with_foreign_keys.orders, filtered by (not (true and
/// true) or '2024-01-02' is greater than '2024-01-01') and '12:00:00' equals '12:00:00'.
/// </summary>
public sealed record ScalarAndOrNotAtFourLevelsQuery
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
                        new StringArrayReturning(
                            new StringField(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new OrdersTable().Name,
                                    ]
                                ).TextValue,
                                new OrderStatusColumn().Name.TextValue
                            )
                        )
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
                                        new BooleanOperator(
                                            new NotOperator(
                                                new BooleanReturning(
                                                    new BooleanOperator(
                                                        new AndOperator([
                                                            new BooleanReturning(
                                                                new BooleanScalar(true)
                                                            ),
                                                            new BooleanReturning(
                                                                new BooleanScalar(true)
                                                            ),
                                                        ])
                                                    )
                                                )
                                            )
                                        )
                                    ),
                                    new BooleanReturning(
                                        new Comparison(
                                            new DateComparison(
                                                ComparisonOperator.GreaterThan,
                                                new DateReturning(
                                                    new DateScalar(
                                                        new DateOnly(2024, 1, 2)
                                                    )
                                                ),
                                                new DateReturning(
                                                    new DateScalar(
                                                        new DateOnly(2024, 1, 1)
                                                    )
                                                )
                                            )
                                        )
                                    ),
                                ])
                            )
                        ),
                        new BooleanReturning(
                            new Equality(
                                new SingleValueEquality(
                                    new TimeEquality(
                                        new TimeReturning(
                                            new TimeScalar(new TimeOnly(12, 0, 0))
                                        ),
                                        new TimeReturning(
                                            new TimeScalar(new TimeOnly(12, 0, 0))
                                        )
                                    )
                                )
                            )
                        ),
                    ])
                )
            ),
            join: null,
            groupBy: null,
            having: null,
            orderBy: null,
            pagination: null
        );
}
