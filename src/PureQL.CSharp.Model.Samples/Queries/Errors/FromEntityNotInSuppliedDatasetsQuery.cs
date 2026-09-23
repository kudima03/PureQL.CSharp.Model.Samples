using PureQL.CSharp.Model.ArrayReturnings;
using PureQL.CSharp.Model.Fields;

namespace PureQL.CSharp.Model.Samples.Queries.Errors;

/// <summary>
/// Selects whatever from single_table_schema.nonexistent_table.
/// </summary>
public sealed record FromEntityNotInSuppliedDatasetsQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public Query Value =>
        new Query(
            new FromExpression("single_table_schema.nonexistent_table"),
            [
                new SelectExpression(
                    new ArrayReturning(
                        new StringArrayReturning(
                            new StringField(
                                "single_table_schema.nonexistent_table",
                                "whatever"
                            )
                        )
                    )
                ),
            ]
        );
}
