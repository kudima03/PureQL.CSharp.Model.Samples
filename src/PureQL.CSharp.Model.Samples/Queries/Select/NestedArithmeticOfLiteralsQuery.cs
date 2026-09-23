using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
using PureQL.CSharp.Model.Arithmetics;
using PureQL.CSharp.Model.Returnings;
using PureQL.CSharp.Model.Scalars;

namespace PureQL.CSharp.Model.Samples.Queries.Select;

public sealed record NestedArithmeticOfLiteralsQuery
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
                    new SingleValueReturning(
                        new NumberReturning(
                            new Arithmetic(
                                new Multiply([
                                    new NumberReturning(
                                        new Arithmetic(
                                            new Add([
                                                new NumberReturning(new NumberScalar(1)),
                                                new NumberReturning(new NumberScalar(2)),
                                            ])
                                        )
                                    ),
                                    new NumberReturning(new NumberScalar(3)),
                                ])
                            )
                        )
                    ),
                    "result"
                ),
            ]
        );
}
