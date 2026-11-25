using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Samples.Columns;
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
using PureQL.CSharp.Model.Fields;
using PureQL.CSharp.Model.GroupExpressions;
using PureQL.CSharp.Model.Keys;
using PureQL.CSharp.Model.Lists;
using PureQL.CSharp.Model.RowExpressions;

namespace PureQL.CSharp.Model.Samples.Queries.Where;

/// <summary>
/// Groups the orders of schema_with_foreign_keys.orders inner joined with
/// schema_with_foreign_keys.users, on order_user_id equal to user_id and user_name in the
/// list literal [Ann, Cara, Dan], where order_status is in [shipped, pending], by
/// user_tenant_id as tenant_id and by whether order_status is in [pending, cancelled] as
/// is_open, keeping the groups whose row count is in [1, 2, 3]. Selects tenant_id,
/// is_open, the count of orders whose order_total is in the list parameter round_totals
/// as large, whether the row count is in the list parameter counts, and whether tenant_id
/// is in a list literal of one uuid. Ordered by whether tenant_id is in a list literal of
/// two uuids, tenant_id and is_open.
/// </summary>
public sealed record ListLiteralsInEveryClauseQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainGroupedQueryWithoutPagination(
            from: new FromEntity(
                new JoinedString(
                    new DotString(),
                    [new RelationalSchemaWithForeignKeys().Name, new OrdersTable().Name]
                ).TextValue
            ),
            joins:
            [
                new JoinEntity(
                    JoinKind.Inner,
                    new JoinedString(
                        new DotString(),
                        [
                            new RelationalSchemaWithForeignKeys().Name,
                            new UsersTable().Name,
                        ]
                    ).TextValue,
                    new AndRow([
                        new EqualUuidRow(
                            new FieldUuid(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new OrdersTable().Name,
                                    ]
                                ).TextValue,
                                new OrderUserIdColumn().Name.TextValue
                            ),
                            new FieldUuid(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new UsersTable().Name,
                                    ]
                                ).TextValue,
                                new UserIdColumn().Name.TextValue
                            )
                        ),
                        new InStringRow(
                            new FieldString(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new UsersTable().Name,
                                    ]
                                ).TextValue,
                                new UserNameColumn().Name.TextValue
                            ),
                            new ListLiteralString(["Ann", "Cara", "Dan"])
                        ),
                    ])
                ),
            ],
            where: new InStringRow(
                new FieldString(
                    new JoinedString(
                        new DotString(),
                        [
                            new RelationalSchemaWithForeignKeys().Name,
                            new OrdersTable().Name,
                        ]
                    ).TextValue,
                    new OrderStatusColumn().Name.TextValue
                ),
                new ListLiteralString(["shipped", "pending"])
            ),
            groupBy:
            [
                new GroupKey(
                    "tenant_id",
                    new FieldUuid(
                        new JoinedString(
                            new DotString(),
                            [
                                new RelationalSchemaWithForeignKeys().Name,
                                new UsersTable().Name,
                            ]
                        ).TextValue,
                        new UserTenantIdColumn().Name.TextValue
                    )
                ),
                new GroupKey(
                    "is_open",
                    new InStringRow(
                        new FieldString(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrdersTable().Name,
                                ]
                            ).TextValue,
                            new OrderStatusColumn().Name.TextValue
                        ),
                        new ListLiteralString(["pending", "cancelled"])
                    )
                ),
            ],
            having: new InDecimalGroup(
                new CountGroup(),
                new ListLiteralInteger([1, 2, 3])
            ),
            select:
            [
                new SelectItemGroup("tenant_id", new KeyUuid("tenant_id")),
                new SelectItemGroup("is_open", new KeyBoolean("is_open")),
                new SelectItemGroup(
                    "large",
                    new CountGroup(
                        predicate: new InDecimalRow(
                            new FieldDecimal(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new OrdersTable().Name,
                                    ]
                                ).TextValue,
                                new OrderTotalColumn().Name.TextValue
                            ),
                            new ListParamDecimal("round_totals")
                        )
                    )
                ),
                new SelectItemGroup(
                    "count_is_listed",
                    new InDecimalGroup(new CountGroup(), new ListParamInteger("counts"))
                ),
                new SelectItemGroup(
                    "tenant_is_listed",
                    new InUuidGroup(
                        new KeyUuid("tenant_id"),
                        new ListLiteralUuid([new Guid(901, 0, 0, new byte[8])])
                    )
                ),
            ],
            orderBy:
            [
                new OrderItemGroupExpression(
                    new InUuidGroup(
                        new KeyUuid("tenant_id"),
                        new ListLiteralUuid([
                            new Guid(901, 0, 0, new byte[8]),
                            new Guid(902, 0, 0, new byte[8]),
                        ])
                    )
                ),
                new OrderItemGroupExpression(new KeyUuid("tenant_id")),
                new OrderItemGroupExpression(new KeyBoolean("is_open")),
            ]
        );
}
