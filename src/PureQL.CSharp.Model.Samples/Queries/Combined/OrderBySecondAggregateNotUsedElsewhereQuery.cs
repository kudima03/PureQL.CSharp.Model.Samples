using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Samples.Columns;
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
using PureQL.CSharp.Model.Aggregates;
using PureQL.CSharp.Model.Aggregates.Numeric;
using PureQL.CSharp.Model.ArrayReturnings;
using PureQL.CSharp.Model.Fields;
using PureQL.CSharp.Model.Returnings;

namespace PureQL.CSharp.Model.Samples.Queries.Combined;

/// <summary>
/// Selects order_user_id, the count of order_id as orderCount and the maximum of
/// order_total as maxTotal from schema_with_foreign_keys.orders, grouped by
/// order_user_id, ordered by maxTotal descending.
/// </summary>
public sealed record OrderBySecondAggregateNotUsedElsewhereQuery
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
                        new NumberReturning(
                            new Count(
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
                                            new OrderIdColumn().Name.TextValue
                                        )
                                    )
                                )
                            )
                        )
                    ),
                    "orderCount"
                ),
                new SelectExpression(
                    new SingleValueReturning(
                        new NumberReturning(
                            new NumberAggregate(
                                new MaxNumber(
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
                                    )
                                )
                            )
                        )
                    ),
                    "maxTotal"
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
            [
                new OrderByItem(
                    new Field(
                        new NumberField(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrdersTable().Name,
                                ]
                            ).TextValue,
                            "maxTotal"
                        )
                    ),
                    SortDirection.Desc
                ),
            ],
            pagination: null
        );
}
