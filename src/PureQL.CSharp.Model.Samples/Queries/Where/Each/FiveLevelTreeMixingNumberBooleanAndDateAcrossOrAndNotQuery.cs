using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Samples.Columns;
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
using PureQL.CSharp.Model.ArrayReturnings;
using PureQL.CSharp.Model.EachBooleanOperations;
using PureQL.CSharp.Model.EachComparisons;
using PureQL.CSharp.Model.EachEqualities;
using PureQL.CSharp.Model.Fields;
using PureQL.CSharp.Model.Returnings;
using PureQL.CSharp.Model.Scalars;

namespace PureQL.CSharp.Model.Samples.Queries.Where.Each;

public sealed record FiveLevelTreeMixingNumberBooleanAndDateAcrossOrAndNotQuery
{
    public Query Value =>
        new Query(
            new FromExpression(
                new JoinedString(
                    new DotString(),
                    [new RelationalSchemaWithForeignKeys().Name, new UsersTable().Name]
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
                                        new UsersTable().Name,
                                    ]
                                ).TextValue,
                                new UserIdColumn().Name.TextValue
                            )
                        )
                    )
                ),
            ],
            new BooleanArrayReturning(
                new EachAndOperator([
                    new BooleanArrayReturning(
                        new EachOrOperator([
                            new BooleanArrayReturning(
                                new EachNotOperator(
                                    new BooleanArrayReturning(
                                        new EachAndOperator([
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
                                                                        new UsersTable().Name,
                                                                    ]
                                                                ).TextValue,
                                                                new UserAgeColumn()
                                                                    .Name
                                                                    .TextValue
                                                            )
                                                        ),
                                                        new NumberReturning(
                                                            new NumberScalar(26)
                                                        )
                                                    )
                                                )
                                            ),
                                            new BooleanArrayReturning(
                                                new EachEquality(
                                                    new EachBooleanEquality(
                                                        new BooleanArrayReturning(
                                                            new BooleanField(
                                                                new JoinedString(
                                                                    new DotString(),
                                                                    [
                                                                        new RelationalSchemaWithForeignKeys().Name,
                                                                        new UsersTable().Name,
                                                                    ]
                                                                ).TextValue,
                                                                new UserActiveColumn()
                                                                    .Name
                                                                    .TextValue
                                                            )
                                                        ),
                                                        new BooleanReturning(
                                                            new BooleanScalar(true)
                                                        )
                                                    )
                                                )
                                            ),
                                        ])
                                    )
                                )
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
                                                        new UsersTable().Name,
                                                    ]
                                                ).TextValue,
                                                new UserAgeColumn().Name.TextValue
                                            )
                                        ),
                                        new NumberReturning(new NumberScalar(30))
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
                                            new EachBooleanEquality(
                                                new BooleanArrayReturning(
                                                    new BooleanField(
                                                        new JoinedString(
                                                            new DotString(),
                                                            [
                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                new UsersTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new UserActiveColumn()
                                                            .Name
                                                            .TextValue
                                                    )
                                                ),
                                                new BooleanReturning(
                                                    new BooleanScalar(true)
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
                                            new EachDateComparison(
                                                EachComparisonOperator.EachLessThan,
                                                new DateArrayReturning(
                                                    new DateField(
                                                        new JoinedString(
                                                            new DotString(),
                                                            [
                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                new UsersTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new SignupDateColumn()
                                                            .Name
                                                            .TextValue
                                                    )
                                                ),
                                                new DateReturning(
                                                    new DateScalar(
                                                        new DateOnly(2021, 1, 1)
                                                    )
                                                )
                                            )
                                        )
                                    ),
                                    new BooleanArrayReturning(
                                        new EachEquality(
                                            new EachBooleanEquality(
                                                new BooleanArrayReturning(
                                                    new BooleanField(
                                                        new JoinedString(
                                                            new DotString(),
                                                            [
                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                new UsersTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new UserActiveColumn()
                                                            .Name
                                                            .TextValue
                                                    )
                                                ),
                                                new BooleanReturning(
                                                    new BooleanScalar(false)
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
            join: null,
            groupBy: null,
            having: null,
            orderBy: null,
            pagination: null
        );
}
