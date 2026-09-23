using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
using PureQL.CSharp.Model.Returnings;
using PureQL.CSharp.Model.Scalars;

namespace PureQL.CSharp.Model.Samples.Queries.Select;

public sealed record AllSevenScalarTypesQuery
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
                        new BooleanReturning(new BooleanScalar(true))
                    ),
                    "active"
                ),
                new SelectExpression(
                    new SingleValueReturning(
                        new DateReturning(new DateScalar(new DateOnly(2024, 12, 31)))
                    ),
                    "release"
                ),
                new SelectExpression(
                    new SingleValueReturning(
                        new DateTimeReturning(
                            new DateTimeScalar(new DateTime(2024, 12, 31, 23, 59, 58))
                        )
                    ),
                    "built_at"
                ),
                new SelectExpression(
                    new SingleValueReturning(new NumberReturning(new NumberScalar(42.5))),
                    "amount"
                ),
                new SelectExpression(
                    new SingleValueReturning(new StringReturning(new StringScalar("v2"))),
                    "label"
                ),
                new SelectExpression(
                    new SingleValueReturning(
                        new TimeReturning(new TimeScalar(new TimeOnly(17, 30, 15)))
                    ),
                    "cutoff"
                ),
                new SelectExpression(
                    new SingleValueReturning(
                        new UuidReturning(
                            new UuidScalar(
                                new Guid("0f8fad5b-d9cb-469f-a165-70867728950e")
                            )
                        )
                    ),
                    "marker"
                ),
            ]
        );
}
