using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Samples.Columns;
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
using PureQL.CSharp.Model.ArrayReturnings;
using PureQL.CSharp.Model.EachEqualities;
using PureQL.CSharp.Model.Fields;

namespace PureQL.CSharp.Model.Samples.Queries.Joins;

/// <summary>
/// Selects sp.id as specId from schema_with_indexes.table_with_indexes (aliased need),
/// inner-joined to schema_with_indexes.table_with_single_index on
/// table_with_indexes.tenant_id equals table_with_single_index.id.
/// </summary>
public sealed record SelectOfCollidingColumnViaUndeclaredAliasQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public Query Value =>
        new Query(
            new FromExpression(
                new JoinedString(
                    new DotString(),
                    [new RelationalSchemaWithIndexes().Name, new TableWithIndexes().Name]
                ).TextValue,
                "need"
            ),
            [
                new SelectExpression(
                    new ArrayReturning(
                        new UuidArrayReturning(
                            new UuidField("sp", new IdColumn().Name.TextValue)
                        )
                    ),
                    "specId"
                ),
            ],
            where: null,
            [
                new Join(
                    JoinType.Inner,
                    new JoinedString(
                        new DotString(),
                        [
                            new RelationalSchemaWithIndexes().Name,
                            new TableWithSingleIndex().Name,
                        ]
                    ).TextValue,
                    new BooleanArrayReturning(
                        new EachEquality(
                            new EachUuidEquality(
                                new UuidArrayReturning(
                                    new UuidField(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithIndexes().Name,
                                                new TableWithIndexes().Name,
                                            ]
                                        ).TextValue,
                                        new TenantIdColumn().Name.TextValue
                                    )
                                ),
                                new UuidArrayReturning(
                                    new UuidField(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithIndexes().Name,
                                                new TableWithSingleIndex().Name,
                                            ]
                                        ).TextValue,
                                        new IdColumn().Name.TextValue
                                    )
                                )
                            )
                        )
                    )
                ),
            ],
            groupBy: null,
            having: null,
            orderBy: null,
            pagination: null
        );
}
