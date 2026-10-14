using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Samples.Columns;
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
using PureQL.CSharp.Model.Fields;
using PureQL.CSharp.Model.GroupExpressions;
using PureQL.CSharp.Model.GroupKeys;
using PureQL.CSharp.Model.Keys;
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.Parameters;
using PureQL.CSharp.Model.RowExpressions;
using PureQL.CSharp.Model.SelectItems;

namespace PureQL.CSharp.Model.Samples.Queries.GroupBy;

/// <summary>
/// Selects order_user_id and the sum of order_total as revenue from
/// schema_with_foreign_keys.orders, grouped by order_user_id, having either a sum of
/// order_total greater than the integer 250 and no cancelled order, or a count of at
/// least the integer parameter min_orders.
/// </summary>
public sealed record HavingBooleanLogicQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new PureQLQuery(
            new MainGroupedQuery(
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
                    new GroupKey(
                        new GroupKeyNonNullable(
                            new GroupKeyUuid(
                                "order_user_id",
                                new UuidRow(
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
                                )
                            )
                        )
                    ),
                ],
                [
                    new SelectItemGroup(
                        new SelectItemGroupNonNullable(
                            new SelectItemGroupUuid(
                                "order_user_id",
                                new UuidGroup(new KeyUuid("order_user_id"))
                            )
                        )
                    ),
                    new SelectItemGroup(
                        new SelectItemGroupNonNullable(
                            new SelectItemGroupDecimal(
                                "revenue",
                                new ExactDecimalGroup(
                                    new AggregateDecimalGroup(
                                        new SumDecimalGroup(
                                            new DecimalNullableRow(
                                                new ExactDecimalRow(
                                                    new FieldDecimal(
                                                        new JoinedString(
                                                            new DotString(),
                                                            [
                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                new OrdersTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new OrderTotalColumn()
                                                            .Name
                                                            .TextValue
                                                    )
                                                )
                                            )
                                        )
                                    )
                                )
                            )
                        )
                    ),
                ],
                subqueries: null,
                joins: null,
                where: null,
                new BooleanGroup(
                    new LogicalGroup(
                        new OrGroup([
                            new BooleanGroup(
                                new LogicalGroup(
                                    new AndGroup([
                                        new BooleanGroup(
                                            new ComparisonGroup(
                                                new GreaterThanGroup(
                                                    new GreaterThanDecimalGroup(
                                                        new DecimalNullableGroup(
                                                            new ExactDecimalGroup(
                                                                new AggregateDecimalGroup(
                                                                    new SumDecimalGroup(
                                                                        new DecimalNullableRow(
                                                                            new ExactDecimalRow(
                                                                                new FieldDecimal(
                                                                                    new JoinedString(
                                                                                        new DotString(),
                                                                                        [
                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                            new OrdersTable().Name,
                                                                                        ]
                                                                                    ).TextValue,
                                                                                    new OrderTotalColumn()
                                                                                        .Name
                                                                                        .TextValue
                                                                                )
                                                                            )
                                                                        )
                                                                    )
                                                                )
                                                            )
                                                        ),
                                                        new DecimalNullableGroup(
                                                            new IntegerGroup(
                                                                new LiteralInteger(250)
                                                            )
                                                        )
                                                    )
                                                )
                                            )
                                        ),
                                        new BooleanGroup(
                                            new LogicalGroup(
                                                new NotGroup(
                                                    new BooleanGroup(
                                                        new AggregateBooleanGroup(
                                                            new AnyGroup(
                                                                new BooleanRow(
                                                                    new ComparisonRow(
                                                                        new EqualRow(
                                                                            new EqualStringRow(
                                                                                new StringNullableRow(
                                                                                    new StringRow(
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
                                                                                new StringNullableRow(
                                                                                    new StringRow(
                                                                                        new LiteralString(
                                                                                            "cancelled"
                                                                                        )
                                                                                    )
                                                                                )
                                                                            )
                                                                        )
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
                            new BooleanGroup(
                                new ComparisonGroup(
                                    new GreaterThanOrEqualGroup(
                                        new GreaterThanOrEqualDecimalGroup(
                                            new DecimalNullableGroup(
                                                new IntegerGroup(
                                                    new AggregateIntegerGroup(
                                                        new CountGroup()
                                                    )
                                                )
                                            ),
                                            new DecimalNullableGroup(
                                                new IntegerGroup(
                                                    new ParamInteger("min_orders")
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
                distinctBy: null
            )
        );
}
