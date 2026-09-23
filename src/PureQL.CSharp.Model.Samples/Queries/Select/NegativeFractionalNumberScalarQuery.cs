using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
using PureQL.CSharp.Model.Returnings;
using PureQL.CSharp.Model.Scalars;

namespace PureQL.CSharp.Model.Samples.Queries.Select;

/// <summary>
/// Selects -12.75 as adjustment from schema_with_foreign_keys.products.
/// </summary>
public sealed record NegativeFractionalNumberScalarQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public Query Value =>
        new Query(
            new FromExpression(
                new JoinedString(
                    new DotString(),
                    [new RelationalSchemaWithForeignKeys().Name, new ProductsTable().Name]
                ).TextValue
            ),
            [
                new SelectExpression(
                    new SingleValueReturning(
                        new NumberReturning(new NumberScalar(-12.75))
                    ),
                    "adjustment"
                ),
            ]
        );
}
