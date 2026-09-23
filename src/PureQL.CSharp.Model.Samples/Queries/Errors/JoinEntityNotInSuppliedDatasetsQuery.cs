using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Samples.Columns;
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
using PureQL.CSharp.Model.ArrayReturnings;
using PureQL.CSharp.Model.EachEqualities;
using PureQL.CSharp.Model.Fields;

namespace PureQL.CSharp.Model.Samples.Queries.Errors;

/// <summary>
/// Selects single_column_table.id from single_table_schema.single_column_table,
/// inner-joined to single_table_schema.nonexistent_join_table on single_column_table.id
/// equals nonexistent_join_table.whatever_id.
/// </summary>
public sealed record JoinEntityNotInSuppliedDatasetsQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public Query Value =>
        new Query(
            new FromExpression(
                new JoinedString(
                    new DotString(),
                    [new SingleTableRelationalSchema().Name, new SingleColumnTable().Name]
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
                                        new SingleTableRelationalSchema().Name,
                                        new SingleColumnTable().Name,
                                    ]
                                ).TextValue,
                                new IdColumn().Name.TextValue
                            )
                        )
                    )
                ),
            ],
            where: null,
            [
                new Join(
                    JoinType.Inner,
                    "single_table_schema.nonexistent_join_table",
                    new BooleanArrayReturning(
                        new EachEquality(
                            new EachUuidEquality(
                                new UuidArrayReturning(
                                    new UuidField(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new SingleTableRelationalSchema().Name,
                                                new SingleColumnTable().Name,
                                            ]
                                        ).TextValue,
                                        new IdColumn().Name.TextValue
                                    )
                                ),
                                new UuidArrayReturning(
                                    new UuidField(
                                        "single_table_schema.nonexistent_join_table",
                                        "whatever_id"
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
