using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Samples.Columns;
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
using PureQL.CSharp.Model.Aggregates.DateTime;
using PureQL.CSharp.Model.ArrayReturnings;
using PureQL.CSharp.Model.EachDateTimeArithmetics;
using PureQL.CSharp.Model.Fields;
using PureQL.CSharp.Model.Returnings;
using PureQL.CSharp.Model.Scalars;

namespace PureQL.CSharp.Model.Samples.Queries.Aggregates;

/// <summary>
/// Selects order_user_id and the minimum of placed_at plus 1800 seconds as
/// earliestProjectedInstant from schema_with_foreign_keys.orders, grouped by
/// order_user_id.
/// </summary>
public sealed record MinOfEachDateTimeAddSecondsGroupedByOrderUserIdQuery
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
                new SelectExpression(
                    new SingleValueReturning(
                        new DateTimeReturning(
                            new DateTimeAggregate(
                                new MinDateTime(
                                    new DateTimeArrayReturning(
                                        new EachDateTimeAddSeconds(
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
                                            ),
                                            new NumberReturning(new NumberScalar(1800))
                                        )
                                    )
                                )
                            )
                        )
                    ),
                    "earliestProjectedInstant"
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
