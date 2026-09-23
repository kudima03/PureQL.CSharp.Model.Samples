using PureQL.CSharp.Model.ArrayReturnings;
using PureQL.CSharp.Model.Fields;

namespace PureQL.CSharp.Model.Samples.Queries.Errors;

public sealed record FromEntityNotInSuppliedDatasetsQuery
{
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
