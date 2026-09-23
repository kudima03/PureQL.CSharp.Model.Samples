using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Samples.Columns;
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
using PureQL.CSharp.Model.ArrayReturnings;
using PureQL.CSharp.Model.EachBooleanOperations;
using PureQL.CSharp.Model.EachComparisons;
using PureQL.CSharp.Model.EachEqualities;
using PureQL.CSharp.Model.Fields;
using PureQL.CSharp.Model.Returnings;
using PureQL.CSharp.Model.Scalars;

namespace PureQL.CSharp.Model.Samples.Queries.Where.Each;

/// <summary>
/// Selects order_status from schema_with_foreign_keys.orders, keeping the rows where
/// (order_total is greater than 100 or order_status equals 'pending') and not
/// (order_status equals 'cancelled').
/// </summary>
public sealed record AndOfOrAndNotQuery
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
            ],
            new BooleanArrayReturning(
                new EachAndOperator([
                    new BooleanArrayReturning(
                        new EachOrOperator([
                            new BooleanArrayReturning(
                                new EachComparison(
                                    new EachNumberComparison(
                                        EachComparisonOperator.EachGreaterThan,
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
                                        new NumberReturning(new NumberScalar(100))
                                    )
                                )
                            ),
                            new BooleanArrayReturning(
                                new EachEquality(
                                    new EachStringEquality(
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
                                        ),
                                        new StringReturning(new StringScalar("pending"))
                                    )
                                )
                            ),
                        ])
                    ),
                    new BooleanArrayReturning(
                        new EachNotOperator(
                            new BooleanArrayReturning(
                                new EachEquality(
                                    new EachStringEquality(
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
                                        ),
                                        new StringReturning(new StringScalar("cancelled"))
                                    )
                                )
                            )
                        )
                    ),
                ])
            ),
            join: null,
            groupBy: null,
            having: null,
            orderBy: null,
            pagination: null
        );
}
