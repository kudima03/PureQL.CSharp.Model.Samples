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
/// Selects order_id from schema_with_foreign_keys.orders, filtered by the whole
/// order_total column equals a list of 6 numbers.
/// </summary>
public sealed record WholeArrayEqualityOfFieldAgainstLiteralQuery
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
                        new NumberArrayEquality(
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
                            new NumberArrayReturning(
                                new NumberArrayScalar([100.5, 300, 75.25, 200, 50, 100.5])
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
