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

public sealed record HavingFourLevelNestedNotOrAndNotQuery
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
                new BooleanOperator(
                    new NotOperator(
                        new BooleanReturning(
                            new BooleanOperator(
                                new OrOperator([
                                    new BooleanReturning(
                                        new BooleanOperator(
                                            new AndOperator([
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
                                                                new NumberScalar(1)
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
                                                                new NumberScalar(200)
                                                            )
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
                                                            new NumberReturning(
                                                                new NumberScalar(100)
                                                            )
                                                        )
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
            orderBy: null,
            pagination: null
        );
}
