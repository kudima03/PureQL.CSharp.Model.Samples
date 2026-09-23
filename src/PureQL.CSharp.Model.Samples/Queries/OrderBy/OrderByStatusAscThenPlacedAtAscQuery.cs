using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Samples.Columns;
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
using PureQL.CSharp.Model.ArrayReturnings;
using PureQL.CSharp.Model.Fields;

namespace PureQL.CSharp.Model.Samples.Queries.OrderBy;

/// <summary>
/// Selects order_status and placed_at from schema_with_foreign_keys.orders, ordered by
/// order_status and placed_at.
/// </summary>
public sealed record OrderByStatusAscThenPlacedAtAscQuery
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
                ),
                new SelectExpression(
                    new ArrayReturning(
                        new DateTimeArrayReturning(
                            new DateTimeField(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new OrdersTable().Name,
                                    ]
                                ).TextValue,
                                new PlacedAtColumn().Name.TextValue
                            )
                        )
                    )
                ),
            ],
            where: null,
            join: null,
            groupBy: null,
            having: null,
            [
                new OrderByItem(
                    new Field(
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
                ),
                new OrderByItem(
                    new Field(
                        new DateTimeField(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrdersTable().Name,
                                ]
                            ).TextValue,
                            new PlacedAtColumn().Name.TextValue
                        )
                    )
                ),
            ],
            pagination: null
        );
}
