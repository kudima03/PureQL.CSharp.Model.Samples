using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Samples.Columns;
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
using PureQL.CSharp.Model.Aggregates;
using PureQL.CSharp.Model.ArrayReturnings;
using PureQL.CSharp.Model.Fields;
using PureQL.CSharp.Model.Returnings;
using PureQL.CSharp.Model.Scalars;

namespace PureQL.CSharp.Model.Samples.Queries.GroupBy;

/// <summary>
/// Selects true as flag, '9b2b1f6e-3c86-4c50-8f6a-2f6d1a8f2c11' as marker and the count
/// of order_id as order_count from schema_with_foreign_keys.orders.
/// </summary>
public sealed record BooleanAndUuidScalarsInGroupModeQuery
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
                        new BooleanReturning(new BooleanScalar(true))
                    ),
                    "flag"
                ),
                new SelectExpression(
                    new SingleValueReturning(
                        new UuidReturning(
                            new UuidScalar(
                                new Guid("9b2b1f6e-3c86-4c50-8f6a-2f6d1a8f2c11")
                            )
                        )
                    ),
                    "marker"
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
                    "order_count"
                ),
            ]
        );
}
