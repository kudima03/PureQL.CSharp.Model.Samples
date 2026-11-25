using Pure.Collections.Generic;
using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Abstractions.Column;
using Pure.RelationalSchema.ColumnType;
using Pure.RelationalSchema.HashCodes;
using Pure.RelationalSchema.Samples.Columns;
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
using Pure.RelationalSchema.Storage;
using Pure.RelationalSchema.Storage.Abstractions;
using Pure.RelationalSchema.Storage.Samples.Cells;
using Pure.RelationalSchema.Storage.Samples.SchemaDataSets;
using Pure.RelationalSchema.Storage.Samples.TableDataSets;
using PureQL.CSharp.Model.Fields;
using PureQL.CSharp.Model.GroupExpressions;
using PureQL.CSharp.Model.Keys;
using PureQL.CSharp.Model.ProjectionExpressions;
using PureQL.CSharp.Model.RowExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using Guid = Pure.Primitives.Guid.Guid;
using ModelPagination = PureQL.CSharp.Model.Pagination;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Subqueries;

/// <summary>
/// Selects user_id, last_order, last_total and revenue from a subquery latest (the orders
/// of schema_with_foreign_keys.orders as user_id, id, the asDate of placed_at as
/// ordered_on and total, ordered by the columns ordered_on descending, total descending
/// and id, keeping per user_id only the first row in that order) inner joined to a
/// subquery top (the sum of order_total per order_user_id as revenue, ordered by the
/// columns revenue descending and user_id, taking the first 3) on the user_id of both,
/// ordered by the columns revenue descending, last_total and user_id, keeping per user_id
/// only the first row and taking the first 2.
/// </summary>
public sealed record SortedSubqueriesDistinctAndPagedQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithPagination(
            subqueries:
            [
                new SubqueryQuery(
                    "latest",
                    new PlainQueryWithoutPaginationWithOrderBy(
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
                            new SelectItemProjection(
                                "id",
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
                                "ordered_on",
                                new AsDateDatetimeDateProjection(
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
                            new SelectItemProjection(
                                "total",
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
                        distinctBy: ["user_id"],
                        orderBy:
                        [
                            new OrderItemProjectionColumn(
                                "ordered_on",
                                SortDirection.Desc
                            ),
                            new OrderItemProjectionColumn("total", SortDirection.Desc),
                            new OrderItemProjectionColumn("id"),
                        ]
                    )
                ),
                new SubqueryQuery(
                    "top",
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
                            new SelectItemGroup(
                                "revenue",
                                new SumDecimalGroup(
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
                                )
                            ),
                        ],
                        orderBy:
                        [
                            new OrderItemGroupColumn("revenue", SortDirection.Desc),
                            new OrderItemGroupColumn("user_id"),
                        ],
                        pagination: new ModelPagination(take: 3)
                    )
                ),
            ],
            from: new FromSubquery("latest"),
            joins:
            [
                new JoinSubquery(
                    JoinKind.Inner,
                    "top",
                    new EqualUuidRow(
                        new FieldUuid("latest", "user_id"),
                        new FieldUuid("top", "user_id")
                    )
                ),
            ],
            select:
            [
                new SelectItemProjection("user_id", new FieldUuid("latest", "user_id")),
                new SelectItemProjection("last_order", new FieldUuid("latest", "id")),
                new SelectItemProjection(
                    "last_total",
                    new FieldDecimal("latest", "total")
                ),
                new SelectItemProjection("revenue", new FieldDecimal("top", "revenue")),
            ],
            distinctBy: ["user_id"],
            orderBy:
            [
                new OrderItemProjectionColumn("revenue", SortDirection.Desc),
                new OrderItemProjectionColumn("last_total"),
                new OrderItemProjectionColumn("user_id"),
            ],
            pagination: new ModelPagination(take: 2)
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and
    /// <see cref="AuditSchemaDataSet"/>, in this order.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new UserIdColumn(),
                    new Column(new String("last_order"), new UuidColumnType()),
                    new Column(new String("last_total"), new DoubleColumnType()),
                    new Column(new String("revenue"), new DoubleColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new UserIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000003-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_order"),
                                    new UuidColumnType()
                                ),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000069-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_total"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(300))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("revenue"), new DoubleColumnType()),
                                new InvariantCell(new Double(375.25))
                            ),
                        ],
                        pair => pair.Key,
                        pair => pair.Value,
                        column => new ColumnHash(column)
                    )
                ),
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new UserIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000002-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_order"),
                                    new UuidColumnType()
                                ),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000067-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_total"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(200))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("revenue"), new DoubleColumnType()),
                                new InvariantCell(new Double(200))
                            ),
                        ],
                        pair => pair.Key,
                        pair => pair.Value,
                        column => new ColumnHash(column)
                    )
                ),
            ]
        );
}
