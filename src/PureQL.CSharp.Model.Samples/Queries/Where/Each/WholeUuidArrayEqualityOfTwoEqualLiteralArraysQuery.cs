using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Samples.Columns;
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
using PureQL.CSharp.Model.ArrayEqualities;
using PureQL.CSharp.Model.ArrayReturnings;
using PureQL.CSharp.Model.ArrayScalars;
using PureQL.CSharp.Model.Fields;
using PureQL.CSharp.Model.Returnings;

namespace PureQL.CSharp.Model.Samples.Queries.Where.Each;

/// <summary>
/// Selects order_id from schema_with_foreign_keys.orders, filtered by a list of 3 uuids
/// equals the same list.
/// </summary>
public sealed record WholeUuidArrayEqualityOfTwoEqualLiteralArraysQuery
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
            new BooleanReturning(
                new Equality(
                    new ArrayEquality(
                        new UuidArrayEquality(
                            new UuidArrayReturning(
                                new UuidArrayScalar([
                                    new Guid(101, 0, 0, new byte[8]),
                                    new Guid(102, 0, 0, new byte[8]),
                                    new Guid(103, 0, 0, new byte[8]),
                                ])
                            ),
                            new UuidArrayReturning(
                                new UuidArrayScalar([
                                    new Guid(101, 0, 0, new byte[8]),
                                    new Guid(102, 0, 0, new byte[8]),
                                    new Guid(103, 0, 0, new byte[8]),
                                ])
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
