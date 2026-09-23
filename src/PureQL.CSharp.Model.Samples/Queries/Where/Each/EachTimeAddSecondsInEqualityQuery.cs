using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Samples.Columns;
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
using PureQL.CSharp.Model.ArrayReturnings;
using PureQL.CSharp.Model.EachEqualities;
using PureQL.CSharp.Model.EachTimeArithmetics;
using PureQL.CSharp.Model.Fields;
using PureQL.CSharp.Model.Returnings;
using PureQL.CSharp.Model.Scalars;

namespace PureQL.CSharp.Model.Samples.Queries.Where.Each;

/// <summary>
/// Selects user_name from schema_with_foreign_keys.users, keeping the rows where
/// shift_start plus 3600 seconds equals '10:00:00'.
/// </summary>
public sealed record EachTimeAddSecondsInEqualityQuery
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
                    new ArrayReturning(
                        new StringArrayReturning(
                            new StringField(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new UsersTable().Name,
                                    ]
                                ).TextValue,
                                new UserNameColumn().Name.TextValue
                            )
                        )
                    )
                ),
            ],
            new BooleanArrayReturning(
                new EachEquality(
                    new EachTimeEquality(
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
                        ),
                        new TimeReturning(new TimeScalar(new TimeOnly(10, 0, 0)))
                    )
                )
            ),
            join: null,
            groupBy: null,
            having: null,
            orderBy: null,
            pagination: null
        );
}
