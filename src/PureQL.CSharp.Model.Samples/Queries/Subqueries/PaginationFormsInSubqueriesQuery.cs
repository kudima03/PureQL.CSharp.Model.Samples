using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Samples.Columns;
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
using PureQL.CSharp.Model.Fields;
using PureQL.CSharp.Model.GroupExpressions;
using PureQL.CSharp.Model.Keys;
using PureQL.CSharp.Model.Lists;
using PureQL.CSharp.Model.Parameters;
using PureQL.CSharp.Model.RowExpressions;
using ModelPagination = PureQL.CSharp.Model.Pagination;

namespace PureQL.CSharp.Model.Samples.Queries.Subqueries;

/// <summary>
/// Selects order_id and order_total from the subquery latest, where order_id is not in
/// the order_id column of the subquery older, keeping the first row per order_id, ordered
/// by order_total descending and order_id, with pagination skipping 1 row and taking the
/// integer parameter page_size. The subquery latest selects order_id and order_total from
/// schema_with_foreign_keys.orders ordered by placed_at descending and order_id, taking 5
/// rows with no skip; older does the same skipping 3 rows with no take; top_users groups
/// the orders by order_user_id into user_id and a count as orders, ordered by the count
/// descending and user_id, skipping the integer parameter skipped_users rows and taking
/// 10.
/// </summary>
public sealed record PaginationFormsInSubqueriesQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithPagination(
            subqueries:
            [
                new SubqueryQuery(
                    "latest",
                    new PlainQueryWithPagination(
                        from: new FromEntity(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrdersTable().Name,
                                ]
                            ).TextValue
                        ),
                        select:
                        [
                            new SelectItemProjection(
                                "order_id",
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
                            ),
                            new SelectItemProjection(
                                "order_total",
                                new FieldDecimal(
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
                        ],
                        orderBy:
                        [
                            new OrderItemProjectionExpression(
                                new FieldDatetime(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new OrdersTable().Name,
                                        ]
                                    ).TextValue,
                                    new PlacedAtColumn().Name.TextValue
                                ),
                                SortDirection.Desc
                            ),
                            new OrderItemProjectionExpression(
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
                            ),
                        ],
                        pagination: new ModelPagination(take: 5)
                    )
                ),
                new SubqueryQuery(
                    "older",
                    new PlainQueryWithPagination(
                        from: new FromEntity(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrdersTable().Name,
                                ]
                            ).TextValue
                        ),
                        select:
                        [
                            new SelectItemProjection(
                                "order_id",
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
                            ),
                            new SelectItemProjection(
                                "order_total",
                                new FieldDecimal(
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
                        ],
                        orderBy:
                        [
                            new OrderItemProjectionExpression(
                                new FieldDatetime(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new OrdersTable().Name,
                                        ]
                                    ).TextValue,
                                    new PlacedAtColumn().Name.TextValue
                                ),
                                SortDirection.Desc
                            ),
                            new OrderItemProjectionExpression(
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
                            ),
                        ],
                        pagination: new ModelPagination(skip: 3)
                    )
                ),
                new SubqueryQuery(
                    "top_users",
                    new GroupedQueryWithPagination(
                        from: new FromEntity(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrdersTable().Name,
                                ]
                            ).TextValue
                        ),
                        groupBy:
                        [
                            new GroupKey(
                                "user_id",
                                new FieldUuid(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new OrdersTable().Name,
                                        ]
                                    ).TextValue,
                                    new OrderUserIdColumn().Name.TextValue
                                )
                            ),
                        ],
                        select:
                        [
                            new SelectItemGroup("user_id", new KeyUuid("user_id")),
                            new SelectItemGroup("orders", new CountGroup()),
                        ],
                        orderBy:
                        [
                            new OrderItemGroupExpression(
                                new CountGroup(),
                                SortDirection.Desc
                            ),
                            new OrderItemGroupExpression(new KeyUuid("user_id")),
                        ],
                        pagination: new ModelPagination(
                            skip: new ParamInteger("skipped_users"),
                            take: 10
                        )
                    )
                ),
            ],
            from: new FromSubquery("latest"),
            where: new NotRow(
                new InUuidRow(
                    new FieldUuid("latest", "order_id"),
                    new ListSubqueryColumnUuid("older", "order_id")
                )
            ),
            select:
            [
                new SelectItemProjection("order_id", new FieldUuid("latest", "order_id")),
                new SelectItemProjection(
                    "order_total",
                    new FieldDecimal("latest", "order_total")
                ),
            ],
            distinctBy: ["order_id"],
            orderBy:
            [
                new OrderItemProjectionExpression(
                    new FieldDecimal("latest", "order_total"),
                    SortDirection.Desc
                ),
                new OrderItemProjectionExpression(new FieldUuid("latest", "order_id")),
            ],
            pagination: new ModelPagination(skip: 1, take: new ParamInteger("page_size"))
        );
}
