using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Samples.Columns;
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
using PureQL.CSharp.Model.ArrayReturnings;
using PureQL.CSharp.Model.EachEqualities;
using PureQL.CSharp.Model.Fields;
using PureQL.CSharp.Model.Returnings;
using PureQL.CSharp.Model.Scalars;

namespace PureQL.CSharp.Model.Samples.Queries.Types;

public sealed record UuidCasingQuery
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
            ],
            new BooleanArrayReturning(
                new EachEquality(
                    new EachUuidEquality(
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
                        ),
                        new UuidReturning(
                            new UuidScalar(
                                new Guid("0f9e8d7c-6b5a-4938-8271-605f4e3d2c1b")
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
