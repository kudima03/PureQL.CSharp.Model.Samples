using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Samples.Columns;
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
using PureQL.CSharp.Model.Aggregates.DateTime;
using PureQL.CSharp.Model.ArrayReturnings;
using PureQL.CSharp.Model.Fields;
using PureQL.CSharp.Model.Returnings;

namespace PureQL.CSharp.Model.Samples.Queries.Aggregates;

/// <summary>
/// Selects the minimum of placed_at as min_placed_at from
/// schema_with_foreign_keys.orders, grouped by order_user_id.
/// </summary>
public sealed record MinPlacedAtPerUserQuery
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
                    new SingleValueReturning(
                        new DateTimeReturning(
                            new DateTimeAggregate(
                                new MinDateTime(
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
                            )
                        )
                    ),
                    "min_placed_at"
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
            having: null,
            orderBy: null,
            pagination: null
        );
}
