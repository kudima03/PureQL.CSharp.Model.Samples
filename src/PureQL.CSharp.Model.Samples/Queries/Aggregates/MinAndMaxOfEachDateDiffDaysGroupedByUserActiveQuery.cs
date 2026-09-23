using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Samples.Columns;
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
using PureQL.CSharp.Model.Aggregates.Numeric;
using PureQL.CSharp.Model.ArrayReturnings;
using PureQL.CSharp.Model.EachDateArithmetics;
using PureQL.CSharp.Model.EachEqualities;
using PureQL.CSharp.Model.Fields;
using PureQL.CSharp.Model.Returnings;

namespace PureQL.CSharp.Model.Samples.Queries.Aggregates;

/// <summary>
/// Selects users.user_active, the minimum of the days between orders.placed_on and
/// users.signup_date as minSpanDays and the maximum of the days between orders.placed_on
/// and users.signup_date as maxSpanDays from schema_with_foreign_keys.orders,
/// inner-joined to schema_with_foreign_keys.users on orders.order_user_id equals
/// users.user_id, grouped by users.user_active.
/// </summary>
public sealed record MinAndMaxOfEachDateDiffDaysGroupedByUserActiveQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public Query Value =>
        new Query(
            new FromExpression(
                new JoinedString(
                    new DotString(),
                    [new RelationalSchemaWithForeignKeys().Name, new OrdersTable().Name]
                ).TextValue
            ),
            [
                new SelectExpression(
                    new ArrayReturning(
                        new BooleanArrayReturning(
                            new BooleanField(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new UsersTable().Name,
                                    ]
                                ).TextValue,
                                new UserActiveColumn().Name.TextValue
                            )
                        )
                    )
                ),
                new SelectExpression(
                    new SingleValueReturning(
                        new NumberReturning(
                            new NumberAggregate(
                                new MinNumber(
                                    new NumberArrayReturning(
                                        new EachDateDiffDays(
                                            new DateArrayReturning(
                                                new DateField(
                                                    new JoinedString(
                                                        new DotString(),
                                                        [
                                                            new RelationalSchemaWithForeignKeys().Name,
                                                            new OrdersTable().Name,
                                                        ]
                                                    ).TextValue,
                                                    new PlacedOnColumn().Name.TextValue
                                                )
                                            ),
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
                                            )
                                        )
                                    )
                                )
                            )
                        )
                    ),
                    "minSpanDays"
                ),
                new SelectExpression(
                    new SingleValueReturning(
                        new NumberReturning(
                            new NumberAggregate(
                                new MaxNumber(
                                    new NumberArrayReturning(
                                        new EachDateDiffDays(
                                            new DateArrayReturning(
                                                new DateField(
                                                    new JoinedString(
                                                        new DotString(),
                                                        [
                                                            new RelationalSchemaWithForeignKeys().Name,
                                                            new OrdersTable().Name,
                                                        ]
                                                    ).TextValue,
                                                    new PlacedOnColumn().Name.TextValue
                                                )
                                            ),
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
                                            )
                                        )
                                    )
                                )
                            )
                        )
                    ),
                    "maxSpanDays"
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
                            new UsersTable().Name,
                        ]
                    ).TextValue,
                    new BooleanArrayReturning(
                        new EachEquality(
                            new EachUuidEquality(
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
                                ),
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
                                )
                            )
                        )
                    )
                ),
            ],
            [
                new Field(
                    new BooleanField(
                        new JoinedString(
                            new DotString(),
                            [
                                new RelationalSchemaWithForeignKeys().Name,
                                new UsersTable().Name,
                            ]
                        ).TextValue,
                        new UserActiveColumn().Name.TextValue
                    )
                ),
            ],
            having: null,
            orderBy: null,
            pagination: null
        );
}
