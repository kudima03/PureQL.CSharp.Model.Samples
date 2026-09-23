using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
using PureQL.CSharp.Model.Parameters;
using PureQL.CSharp.Model.Returnings;

namespace PureQL.CSharp.Model.Samples.Queries.Select;

/// <summary>
/// Selects parameter limit as limit from schema_with_foreign_keys.users.
/// </summary>
public sealed record NumberParameterInSelectQuery
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
                        new NumberReturning(new NumberParameter("limit"))
                    ),
                    "limit"
                ),
            ]
        );
}
