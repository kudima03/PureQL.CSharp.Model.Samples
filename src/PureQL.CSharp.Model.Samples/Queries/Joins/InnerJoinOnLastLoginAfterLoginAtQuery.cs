using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Samples.Columns;
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
using PureQL.CSharp.Model.ArrayReturnings;
using PureQL.CSharp.Model.EachComparisons;
using PureQL.CSharp.Model.Fields;

namespace PureQL.CSharp.Model.Samples.Queries.Joins;

/// <summary>
/// Selects users.user_name from schema_with_foreign_keys.users, inner-joined to
/// audit.logins on users.last_login is greater than logins.login_at.
/// </summary>
public sealed record InnerJoinOnLastLoginAfterLoginAtQuery
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
            where: null,
            [
                new Join(
                    JoinType.Inner,
                    new JoinedString(
                        new DotString(),
                        [new AuditRelationalSchema().Name, new LoginsTable().Name]
                    ).TextValue,
                    new BooleanArrayReturning(
                        new EachComparison(
                            new EachDateTimeComparison(
                                EachComparisonOperator.EachGreaterThan,
                                new DateTimeArrayReturning(
                                    new DateTimeField(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new UsersTable().Name,
                                            ]
                                        ).TextValue,
                                        new LastLoginColumn().Name.TextValue
                                    )
                                ),
                                new DateTimeArrayReturning(
                                    new DateTimeField(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new AuditRelationalSchema().Name,
                                                new LoginsTable().Name,
                                            ]
                                        ).TextValue,
                                        new LoginAtColumn().Name.TextValue
                                    )
                                )
                            )
                        )
                    )
                ),
            ],
            groupBy: null,
            having: null,
            orderBy: null,
            pagination: null
        );
}
