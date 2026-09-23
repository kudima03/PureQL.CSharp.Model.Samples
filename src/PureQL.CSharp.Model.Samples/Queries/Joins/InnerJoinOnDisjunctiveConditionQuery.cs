using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Samples.Columns;
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
using PureQL.CSharp.Model.ArrayReturnings;
using PureQL.CSharp.Model.EachBooleanOperations;
using PureQL.CSharp.Model.EachEqualities;
using PureQL.CSharp.Model.Fields;
using PureQL.CSharp.Model.Returnings;
using PureQL.CSharp.Model.Scalars;

namespace PureQL.CSharp.Model.Samples.Queries.Joins;

/// <summary>
/// Selects users.user_name from schema_with_foreign_keys.users, inner-joined to
/// schema_with_foreign_keys.orders on users.user_id equals orders.order_user_id or
/// orders.order_total equals 200.
/// </summary>
public sealed record InnerJoinOnDisjunctiveConditionQuery
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
                        [
                            new RelationalSchemaWithForeignKeys().Name,
                            new OrdersTable().Name,
                        ]
                    ).TextValue,
                    new BooleanArrayReturning(
                        new EachOrOperator([
                            new BooleanArrayReturning(
                                new EachEquality(
                                    new EachUuidEquality(
                                        new UuidArrayReturning(
                                            new UuidField(
                                                new JoinedString(
                                                    new DotString(),
                                                    [
                                                        new RelationalSchemaWithForeignKeys().Name,
                                                        new UsersTable().Name,
                                                    ]
                                                ).TextValue,
                                                new UserIdColumn().Name.TextValue
                                            )
                                        ),
                                        new UuidArrayReturning(
                                            new UuidField(
                                                new JoinedString(
                                                    new DotString(),
                                                    [
                                                        new RelationalSchemaWithForeignKeys().Name,
                                                        new OrdersTable().Name,
                                                    ]
                                                ).TextValue,
                                                new OrderUserIdColumn().Name.TextValue
                                            )
                                        )
                                    )
                                )
                            ),
                            new BooleanArrayReturning(
                                new EachEquality(
                                    new EachNumberEquality(
                                        new NumberArrayReturning(
                                            new NumberField(
                                                new JoinedString(
                                                    new DotString(),
                                                    [
                                                        new RelationalSchemaWithForeignKeys().Name,
                                                        new OrdersTable().Name,
                                                    ]
                                                ).TextValue,
                                                new OrderTotalColumn().Name.TextValue
                                            )
                                        ),
                                        new NumberReturning(new NumberScalar(200))
                                    )
                                )
                            ),
                        ])
                    )
                ),
            ],
            groupBy: null,
            having: null,
            orderBy: null,
            pagination: null
        );
}
