using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Samples.Columns;
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
using PureQL.CSharp.Model.ArrayReturnings;
using PureQL.CSharp.Model.ArrayScalars;
using PureQL.CSharp.Model.EachArithmetics;
using PureQL.CSharp.Model.EachBooleanOperations;
using PureQL.CSharp.Model.EachComparisons;
using PureQL.CSharp.Model.Fields;
using PureQL.CSharp.Model.Returnings;
using PureQL.CSharp.Model.Scalars;

namespace PureQL.CSharp.Model.Samples.Queries.Where.Each;

/// <summary>
/// Selects order_id from schema_with_foreign_keys.orders, keeping the rows where the
/// whole order_total column is greater than the list [90, -1] and (order_total minus 200)
/// is less than 0.
/// </summary>
public sealed record EachAndOfFieldLiteralArrayAndNestedArithmeticOperandsQuery
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
                                new OrderIdColumn().Name.TextValue
                            )
                        )
                    )
                ),
            ],
            new BooleanArrayReturning(
                new EachAndOperator([
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
                                new NumberArrayReturning(new NumberArrayScalar([90, -1]))
                            )
                        )
                    ),
                    new BooleanArrayReturning(
                        new EachComparison(
                            new EachNumberComparison(
                                EachComparisonOperator.EachLessThan,
                                new NumberArrayReturning(
                                    new EachArithmetic(
                                        new EachSubtract([
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
                                            new NumberReturning(new NumberScalar(200)),
                                        ])
                                    )
                                ),
                                new NumberReturning(new NumberScalar(0))
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
