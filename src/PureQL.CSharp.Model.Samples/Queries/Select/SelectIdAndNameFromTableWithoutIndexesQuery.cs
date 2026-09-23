using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Samples.Columns;
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
using PureQL.CSharp.Model.ArrayReturnings;
using PureQL.CSharp.Model.Fields;

namespace PureQL.CSharp.Model.Samples.Queries.Select;

public sealed record SelectIdAndNameFromTableWithoutIndexesQuery
{
    public Query Value =>
        new Query(
            new FromExpression(
                new JoinedString(
                    new DotString(),
                    [
                        new RelationalSchemaWithoutForeignKeys().Name,
                        new TableWithoutIndexes().Name,
                    ]
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
                                        new RelationalSchemaWithoutForeignKeys().Name,
                                        new TableWithoutIndexes().Name,
                                    ]
                                ).TextValue,
                                new IdColumn().Name.TextValue
                            )
                        )
                    )
                ),
                new SelectExpression(
                    new ArrayReturning(
                        new StringArrayReturning(
                            new StringField(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithoutForeignKeys().Name,
                                        new TableWithoutIndexes().Name,
                                    ]
                                ).TextValue,
                                new NameColumn().Name.TextValue
                            )
                        )
                    )
                ),
            ]
        );
}
