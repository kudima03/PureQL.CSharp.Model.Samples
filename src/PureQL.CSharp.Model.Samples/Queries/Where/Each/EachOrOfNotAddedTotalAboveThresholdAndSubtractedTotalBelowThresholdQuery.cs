using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Samples.Columns;
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
using PureQL.CSharp.Model.ArrayReturnings;
using PureQL.CSharp.Model.EachArithmetics;
using PureQL.CSharp.Model.EachBooleanOperations;
using PureQL.CSharp.Model.EachComparisons;
using PureQL.CSharp.Model.Fields;
using PureQL.CSharp.Model.Returnings;
using PureQL.CSharp.Model.Scalars;

namespace PureQL.CSharp.Model.Samples.Queries.Where.Each;

public sealed record EachOrOfNotAddedTotalAboveThresholdAndSubtractedTotalBelowThresholdQuery
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
            ],
            new BooleanArrayReturning(
                new EachOrOperator([
                    new BooleanArrayReturning(
                        new EachNotOperator(
                            new BooleanArrayReturning(
                                new EachComparison(
                                    new EachNumberComparison(
                                        EachComparisonOperator.EachGreaterThan,
                                        new NumberArrayReturning(
                                            new EachArithmetic(
                                                new EachAdd([
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
                                                        new NumberScalar(20)
                                                    ),
                                                ])
                                            )
                                        ),
                                        new NumberReturning(new NumberScalar(150))
                                    )
                                )
                            )
                        )
                    ),
                    new BooleanArrayReturning(
                        new EachComparison(
                            new EachNumberComparison(
                                EachComparisonOperator.EachLessThan,
                                new NumberArrayReturning(
                                    new EachArithmetic(
                                        new EachSubtract([
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
                                            new NumberReturning(new NumberScalar(30)),
                                        ])
                                    )
                                ),
                                new NumberReturning(new NumberScalar(175))
                            )
                        )
                    ),
                ])
            ),
            join: null,
            groupBy: null,
            having: null,
            orderBy: null,
            pagination: null
        );
}
