using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Samples.Columns;
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
using PureQL.CSharp.Model.Aggregates.Date;
using PureQL.CSharp.Model.ArrayReturnings;
using PureQL.CSharp.Model.EachDateArithmetics;
using PureQL.CSharp.Model.Fields;
using PureQL.CSharp.Model.Returnings;
using PureQL.CSharp.Model.Scalars;

namespace PureQL.CSharp.Model.Samples.Queries.Aggregates;

/// <summary>
/// Selects the average of signup_date plus 30 days as meanProjectedDate from
/// schema_with_foreign_keys.users.
/// </summary>
public sealed record AverageOfEachDateAddDaysQuery
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
                        new DateReturning(
                            new DateAggregate(
                                new AverageDate(
                                    new DateArrayReturning(
                                        new EachDateAddDays(
                                            new DateArrayReturning(
                                                new DateField(
                                                    new JoinedString(
                                                        new DotString(),
                                                        [
                                                            new RelationalSchemaWithForeignKeys().Name,
                                                            new UsersTable().Name,
                                                        ]
                                                    ).TextValue,
                                                    new SignupDateColumn().Name.TextValue
                                                )
                                            ),
                                            new NumberReturning(new NumberScalar(30))
                                        )
                                    )
                                )
                            )
                        )
                    ),
                    "meanProjectedDate"
                ),
            ]
        );
}
