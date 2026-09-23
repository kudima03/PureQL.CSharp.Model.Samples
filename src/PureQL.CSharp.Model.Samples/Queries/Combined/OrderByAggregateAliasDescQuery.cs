using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Samples.Columns;
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
using PureQL.CSharp.Model.Aggregates.Numeric;
using PureQL.CSharp.Model.ArrayReturnings;
using PureQL.CSharp.Model.Fields;
using PureQL.CSharp.Model.Returnings;

namespace PureQL.CSharp.Model.Samples.Queries.Combined;

/// <summary>
/// Selects order_status and the sum of order_total as statusSum from
/// schema_with_foreign_keys.orders, grouped by order_status, ordered by statusSum
/// descending.
/// </summary>
public sealed record OrderByAggregateAliasDescQuery
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
                    new SingleValueReturning(
                        new NumberReturning(
                            new NumberAggregate(
                                new SumNumber(
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
                    "statusSum"
                ),
            ],
            where: null,
            join: null,
            [
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
                            "statusSum"
                        )
                    ),
                    SortDirection.Desc
                ),
            ],
            pagination: null
        );
}
