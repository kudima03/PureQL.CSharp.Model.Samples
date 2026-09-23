using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Samples.Columns;
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
using PureQL.CSharp.Model.Aggregates.String;
using PureQL.CSharp.Model.ArrayReturnings;
using PureQL.CSharp.Model.Equalities;
using PureQL.CSharp.Model.Fields;
using PureQL.CSharp.Model.Returnings;
using PureQL.CSharp.Model.Scalars;

namespace PureQL.CSharp.Model.Samples.Queries.GroupBy;

/// <summary>
/// Selects order_user_id from schema_with_foreign_keys.orders, grouped by order_user_id,
/// keeping the groups where the minimum of order_status equals 'unknown'.
/// </summary>
public sealed record HavingMinStatusEqualAbsentValueQuery
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
                ),
            ],
            where: null,
            join: null,
            [
                new Field(
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
            ],
            new BooleanReturning(
                new Equality(
                    new SingleValueEquality(
                        new StringEquality(
                            new StringReturning(
                                new StringAggregate(
                                    new MinString(
                                        new StringArrayReturning(
                                            new StringField(
                                                new JoinedString(
                                                    new DotString(),
                                                    [
                                                        new RelationalSchemaWithForeignKeys().Name,
                                                        new OrdersTable().Name,
                                                    ]
                                                ).TextValue,
                                                new OrderStatusColumn().Name.TextValue
                                            )
                                        )
                                    )
                                )
                            ),
                            new StringReturning(new StringScalar("unknown"))
                        )
                    )
                )
            ),
            orderBy: null,
            pagination: null
        );
}
