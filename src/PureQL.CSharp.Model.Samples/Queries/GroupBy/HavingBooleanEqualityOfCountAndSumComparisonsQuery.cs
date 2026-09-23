using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Samples.Columns;
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
using PureQL.CSharp.Model.Aggregates;
using PureQL.CSharp.Model.Aggregates.Numeric;
using PureQL.CSharp.Model.ArrayReturnings;
using PureQL.CSharp.Model.Comparisons;
using PureQL.CSharp.Model.Equalities;
using PureQL.CSharp.Model.Fields;
using PureQL.CSharp.Model.Returnings;
using PureQL.CSharp.Model.Scalars;

namespace PureQL.CSharp.Model.Samples.Queries.GroupBy;

public sealed record HavingBooleanEqualityOfCountAndSumComparisonsQuery
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
                new Equality(
                    new SingleValueEquality(
                        new BooleanEquality(
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
                                        new NumberReturning(new NumberScalar(1))
                                    )
                                )
                            ),
                            new BooleanReturning(
                                new Comparison(
                                    new NumberComparison(
                                        ComparisonOperator.GreaterThan,
                                        new NumberReturning(
                                            new NumberAggregate(
                                                new SumNumber(
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
                                        new NumberReturning(new NumberScalar(150))
                                    )
                                )
                            )
                        )
                    )
                )
            ),
            orderBy: null,
            pagination: null
        );
}
