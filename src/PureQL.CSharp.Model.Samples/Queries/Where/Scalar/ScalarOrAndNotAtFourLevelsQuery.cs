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

public sealed record ScalarOrAndNotAtFourLevelsQuery
{
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
                                new AndOperator([
                                    new BooleanReturning(
                                        new BooleanOperator(
                                            new NotOperator(
                                                new BooleanReturning(
                                                    new BooleanOperator(
                                                        new OrOperator([
                                                            new BooleanReturning(
                                                                new BooleanScalar(false)
                                                            ),
                                                            new BooleanReturning(
                                                                new BooleanScalar(false)
                                                            ),
                                                        ])
                                                    )
                                                )
                                            )
                                        )
                                    ),
                                    new BooleanReturning(
                                        new Comparison(
                                            new NumberComparison(
                                                ComparisonOperator.GreaterThan,
                                                new NumberReturning(new NumberScalar(1)),
                                                new NumberReturning(new NumberScalar(2))
                                            )
                                        )
                                    ),
                                ])
                            )
                        ),
                        new BooleanReturning(
                            new Equality(
                                new SingleValueEquality(
                                    new StringEquality(
                                        new StringReturning(new StringScalar("x")),
                                        new StringReturning(new StringScalar("y"))
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
