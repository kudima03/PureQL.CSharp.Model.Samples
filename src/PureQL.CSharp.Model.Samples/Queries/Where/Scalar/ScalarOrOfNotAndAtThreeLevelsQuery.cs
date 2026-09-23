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
/// Selects order_status from schema_with_foreign_keys.orders, filtered by not (5 is
/// greater than 3 and 'x' equals 'y') or false.
/// </summary>
public sealed record ScalarOrOfNotAndAtThreeLevelsQuery
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
                    new OrOperator([
                        new BooleanReturning(
                            new BooleanOperator(
                                new NotOperator(
                                    new BooleanReturning(
                                        new BooleanOperator(
                                            new AndOperator([
                                                new BooleanReturning(
                                                    new Comparison(
                                                        new NumberComparison(
                                                            ComparisonOperator.GreaterThan,
                                                            new NumberReturning(
                                                                new NumberScalar(5)
                                                            ),
                                                            new NumberReturning(
                                                                new NumberScalar(3)
                                                            )
                                                        )
                                                    )
                                                ),
                                                new BooleanReturning(
                                                    new Equality(
                                                        new SingleValueEquality(
                                                            new StringEquality(
                                                                new StringReturning(
                                                                    new StringScalar("x")
                                                                ),
                                                                new StringReturning(
                                                                    new StringScalar("y")
                                                                )
                                                            )
                                                        )
                                                    )
                                                ),
                                            ])
                                        )
                                    )
                                )
                            )
                        ),
                        new BooleanReturning(new BooleanScalar(false)),
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
