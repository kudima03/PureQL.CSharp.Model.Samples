using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Samples.Columns;
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
using PureQL.CSharp.Model.Fields;
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.Parameters;
using PureQL.CSharp.Model.RowExpressions;
using ModelPagination = PureQL.CSharp.Model.Pagination;

namespace PureQL.CSharp.Model.Samples.Queries.Where;

/// <summary>
/// Selects order_id and order_status from schema_with_foreign_keys.orders where the
/// nullable string parameter status is a typed null or equal to order_status, and the
/// nullable datetime parameter placed_since is a typed null or at most placed_at, so a
/// parameter bound to null switches its filter off, ordered by placed_at descending,
/// taking the first 50 rows.
/// </summary>
public sealed record OptionalFiltersSwitchedOffByNullQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithPagination(
            from: new FromEntity(
                new JoinedString(
                    new DotString(),
                    [new RelationalSchemaWithForeignKeys().Name, new OrdersTable().Name]
                ).TextValue
            ),
            where: new AndRow([
                new OrRow([
                    new EqualStringRow(
                        new ParamStringNullable("status"),
                        new LiteralStringNullable()
                    ),
                    new EqualStringRow(
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
                        new ParamStringNullable("status")
                    ),
                ]),
                new OrRow([
                    new EqualDatetimeRow(
                        new ParamDatetimeNullable("placed_since"),
                        new LiteralDatetimeNullable()
                    ),
                    new GreaterThanOrEqualDatetimeRow(
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
                        new ParamDatetimeNullable("placed_since")
                    ),
                ]),
            ]),
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
                    "order_status",
                    new FieldString(
                        new JoinedString(
                            new DotString(),
                            [
                                new RelationalSchemaWithForeignKeys().Name,
                                new OrdersTable().Name,
                            ]
                        ).TextValue,
                        new OrderStatusColumn().Name.TextValue
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
            ],
            pagination: new ModelPagination(take: 50)
        );
}
