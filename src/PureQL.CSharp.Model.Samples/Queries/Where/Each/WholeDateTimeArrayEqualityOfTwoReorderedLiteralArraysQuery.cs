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

public sealed record WholeDateTimeArrayEqualityOfTwoReorderedLiteralArraysQuery
{
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
                        new DateTimeArrayEquality(
                            new DateTimeArrayReturning(
                                new DateTimeArrayScalar([
                                    new DateTime(2024, 1, 1, 8, 0, 0),
                                    new DateTime(2024, 2, 1, 9, 0, 0),
                                    new DateTime(2024, 3, 1, 10, 0, 0),
                                ])
                            ),
                            new DateTimeArrayReturning(
                                new DateTimeArrayScalar([
                                    new DateTime(2024, 3, 1, 10, 0, 0),
                                    new DateTime(2024, 2, 1, 9, 0, 0),
                                    new DateTime(2024, 1, 1, 8, 0, 0),
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
