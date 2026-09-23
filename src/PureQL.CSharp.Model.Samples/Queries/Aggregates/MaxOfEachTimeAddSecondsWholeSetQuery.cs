using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Samples.Columns;
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
using PureQL.CSharp.Model.Aggregates.Time;
using PureQL.CSharp.Model.ArrayReturnings;
using PureQL.CSharp.Model.EachTimeArithmetics;
using PureQL.CSharp.Model.Fields;
using PureQL.CSharp.Model.Returnings;
using PureQL.CSharp.Model.Scalars;

namespace PureQL.CSharp.Model.Samples.Queries.Aggregates;

/// <summary>
/// Selects the maximum of shift_start plus 3600 seconds as latestProjectedTime from
/// schema_with_foreign_keys.users.
/// </summary>
public sealed record MaxOfEachTimeAddSecondsWholeSetQuery
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
                        new TimeReturning(
                            new TimeAggregate(
                                new MaxTime(
                                    new TimeArrayReturning(
                                        new EachTimeAddSeconds(
                                            new TimeArrayReturning(
                                                new TimeField(
                                                    new JoinedString(
                                                        new DotString(),
                                                        [
                                                            new RelationalSchemaWithForeignKeys().Name,
                                                            new UsersTable().Name,
                                                        ]
                                                    ).TextValue,
                                                    new ShiftStartColumn().Name.TextValue
                                                )
                                            ),
                                            new NumberReturning(new NumberScalar(3600))
                                        )
                                    )
                                )
                            )
                        )
                    ),
                    "latestProjectedTime"
                ),
            ]
        );
}
