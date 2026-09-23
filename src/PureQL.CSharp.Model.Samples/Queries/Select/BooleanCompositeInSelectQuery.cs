using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
using PureQL.CSharp.Model.Comparisons;
using PureQL.CSharp.Model.Returnings;
using PureQL.CSharp.Model.Scalars;

namespace PureQL.CSharp.Model.Samples.Queries.Select;

/// <summary>
/// Selects 2 is greater than 1 as flag from schema_with_foreign_keys.users.
/// </summary>
public sealed record BooleanCompositeInSelectQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
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
                        new BooleanReturning(
                            new Comparison(
                                new NumberComparison(
                                    ComparisonOperator.GreaterThan,
                                    new NumberReturning(new NumberScalar(2)),
                                    new NumberReturning(new NumberScalar(1))
                                )
                            )
                        )
                    ),
                    "flag"
                ),
            ]
        );
}
