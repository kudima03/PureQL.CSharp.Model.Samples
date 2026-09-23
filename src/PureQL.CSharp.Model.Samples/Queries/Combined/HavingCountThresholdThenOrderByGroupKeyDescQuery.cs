using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Samples.Columns;
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
using PureQL.CSharp.Model.Aggregates;
using PureQL.CSharp.Model.ArrayReturnings;
using PureQL.CSharp.Model.Comparisons;
using PureQL.CSharp.Model.Fields;
using PureQL.CSharp.Model.Returnings;
using PureQL.CSharp.Model.Scalars;

namespace PureQL.CSharp.Model.Samples.Queries.Combined;

/// <summary>
/// Selects order_status and the count of order_id as orderCount from
/// schema_with_foreign_keys.orders, grouped by order_status, keeping the groups where the
/// count of order_id is at least 2, ordered by order_status descending.
/// </summary>
public sealed record HavingCountThresholdThenOrderByGroupKeyDescQuery
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
            new BooleanReturning(
                new Comparison(
                    new NumberComparison(
                        ComparisonOperator.GreaterThanOrEqual,
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
                        ),
                        new NumberReturning(new NumberScalar(2))
                    )
                )
            ),
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
                    ),
                    SortDirection.Desc
                ),
            ],
            pagination: null
        );
}
