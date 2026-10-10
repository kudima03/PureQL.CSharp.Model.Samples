using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Samples.Columns;
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
using PureQL.CSharp.Model.Fields;
using PureQL.CSharp.Model.Lists;
using PureQL.CSharp.Model.ProjectionExpressions;
using PureQL.CSharp.Model.RowExpressions;
using PureQL.CSharp.Model.SelectItems;

namespace PureQL.CSharp.Model.Samples.Queries.Where;

/// <summary>
/// Selects order_id from schema_with_foreign_keys.orders, where order_status is in the
/// stringList parameter statuses and order_user_id is not in the uuid list #2, #4.
/// </summary>
public sealed record InListParameterAndNotInListLiteralQuery
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
                ],
                subqueries: null,
                joins: null,
                new BooleanRow(
                    new LogicalRow(
                        new AndRow([
                            new BooleanRow(
                                new ComparisonRow(
                                    new InRow(
                                        new InStringRow(
                                            new StringNullableRow(
                                                new FieldAsStringNullable(
                                                    new FieldString(
                                                        new JoinedString(
                                                            new DotString(),
                                                            [
                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                new OrdersTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new OrderStatusColumn()
                                                            .Name
                                                            .TextValue
                                                    )
                                                )
                                            ),
                                            new ListString(
                                                new ListParamString("statuses")
                                            )
                                        )
                                    )
                                )
                            ),
                            new BooleanRow(
                                new LogicalRow(
                                    new NotRow(
                                        new BooleanRow(
                                            new ComparisonRow(
                                                new InRow(
                                                    new InUuidRow(
                                                        new UuidNullableRow(
                                                            new FieldAsUuidNullable(
                                                                new FieldUuid(
                                                                    new JoinedString(
                                                                        new DotString(),
                                                                        [
                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                            new OrdersTable().Name,
                                                                        ]
                                                                    ).TextValue,
                                                                    new OrderUserIdColumn()
                                                                        .Name
                                                                        .TextValue
                                                                )
                                                            )
                                                        ),
                                                        new ListUuid(
                                                            new ListLiteralUuid([
                                                                new Guid(
                                                                    2,
                                                                    0,
                                                                    0,
                                                                    new byte[8]
                                                                ),
                                                                new Guid(
                                                                    4,
                                                                    0,
                                                                    0,
                                                                    new byte[8]
                                                                ),
                                                            ])
                                                        )
                                                    )
                                                )
                                            )
                                        )
                                    )
                                )
                            ),
                        ])
                    )
                ),
                orderBy: null,
                pagination: null,
                distinct: false
            )
        );
}
