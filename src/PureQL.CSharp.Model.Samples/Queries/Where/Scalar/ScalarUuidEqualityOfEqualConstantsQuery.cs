using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Samples.Columns;
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
using PureQL.CSharp.Model.ArrayReturnings;
using PureQL.CSharp.Model.Equalities;
using PureQL.CSharp.Model.Fields;
using PureQL.CSharp.Model.Returnings;
using PureQL.CSharp.Model.Scalars;

namespace PureQL.CSharp.Model.Samples.Queries.Where.Scalar;

/// <summary>
/// Selects order_status from schema_with_foreign_keys.orders, filtered by
/// '00000001-0000-0000-0000-000000000000' equals '00000001-0000-0000-0000-000000000000'.
/// </summary>
public sealed record ScalarUuidEqualityOfEqualConstantsQuery
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
            new BooleanReturning(
                new Equality(
                    new SingleValueEquality(
                        new UuidEquality(
                            new UuidReturning(
                                new UuidScalar(new Guid(1, 0, 0, new byte[8]))
                            ),
                            new UuidReturning(
                                new UuidScalar(new Guid(1, 0, 0, new byte[8]))
                            )
                        )
                    )
                )
            ),
            join: null,
            groupBy: null,
            having: null,
            orderBy: null,
            pagination: null
        );
}
