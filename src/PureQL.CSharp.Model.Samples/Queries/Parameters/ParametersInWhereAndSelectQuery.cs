using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Samples.Columns;
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
using PureQL.CSharp.Model.Fields;
using PureQL.CSharp.Model.Parameters;
using PureQL.CSharp.Model.ProjectionExpressions;
using PureQL.CSharp.Model.RowExpressions;
using PureQL.CSharp.Model.SelectItems;

namespace PureQL.CSharp.Model.Samples.Queries.Parameters;

/// <summary>
/// Selects order_id and the string parameter currency as report_currency from
/// schema_with_foreign_keys.orders, where placed_at is at or after the datetime parameter
/// since.
/// </summary>
public sealed record ParametersInWhereAndSelectQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new PureQLQuery(
            new MainPlainQuery(
                new From(
                    new FromEntity(
                        new JoinedString(
                            new DotString(),
                            [
                                new RelationalSchemaWithForeignKeys().Name,
                                new OrdersTable().Name,
                            ]
                        ).TextValue
                    )
                ),
                [
                    new SelectItemProjection(
                        new SelectItemProjectionNonNullable(
                            new SelectItemProjectionUuid(
                                "order_id",
                                new UuidProjection(
                                    new FieldUuid(
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
                    ),
                    new SelectItemProjection(
                        new SelectItemProjectionNonNullable(
                            new SelectItemProjectionString(
                                "report_currency",
                                new StringProjection(new ParamString("currency"))
                            )
                        )
                    ),
                ],
                subqueries: null,
                joins: null,
                new BooleanRow(
                    new ComparisonRow(
                        new GreaterThanOrEqualRow(
                            new GreaterThanOrEqualDatetimeRow(
                                new DatetimeNullableRow(
                                    new FieldAsDatetimeNullable(
                                        new FieldDatetime(
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new RelationalSchemaWithForeignKeys().Name,
                                                    new OrdersTable().Name,
                                                ]
                                            ).TextValue,
                                            new PlacedAtColumn().Name.TextValue
                                        )
                                    )
                                ),
                                new DatetimeNullableRow(
                                    new ParamAsDatetimeNullable(
                                        new ParamDatetime("since")
                                    )
                                )
                            )
                        )
                    )
                ),
                orderBy: null,
                pagination: null,
                distinct: false
            )
        );
}
