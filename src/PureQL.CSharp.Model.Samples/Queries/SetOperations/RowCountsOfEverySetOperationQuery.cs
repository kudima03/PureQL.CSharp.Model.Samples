using Pure.Collections.Generic;
using Pure.Primitives.Number;
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
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.ProjectionExpressions;
using PureQL.CSharp.Model.TotalExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.SetOperations;

/// <summary>
/// Selects operation and rows from a subquery counts, ordered by operation, where counts is
/// the union all of four total queries that each count the rows of one subquery as rows
/// under its set operation name: all_rows, the union all of the order_status of
/// schema_with_foreign_keys.orders and a status of schema_with_foreign_keys.users (pending
/// where user_active, else cancelled); distinct_rows, their union; shared_rows, their
/// intersect; and order_only_rows, their except.
/// </summary>
public sealed record RowCountsOfEverySetOperationQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithoutPagination(
            subqueries:
            [
                new SubqueryQuery(
                    "all_rows",
                    new CombinedQueryUnionAll(
                        unionAll:
                        [
                            new PlainQueryWithoutPaginationWithoutOrderBy(
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
                                        "status",
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
                                ]
                            ),
                            new PlainQueryWithoutPaginationWithoutOrderBy(
                                from: new FromEntity(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new UsersTable().Name,
                                        ]
                                    ).TextValue
                                ),
                                select:
                                [
                                    new SelectItemProjection(
                                        "status",
                                        new IfStringProjection(
                                            new LiteralString("pending"),
                                            new LiteralString("cancelled"),
                                            new FieldBoolean(
                                                new JoinedString(
                                                    new DotString(),
                                                    [
                                                        new RelationalSchemaWithForeignKeys().Name,
                                                        new UsersTable().Name,
                                                    ]
                                                ).TextValue,
                                                new UserActiveColumn().Name.TextValue
                                            )
                                        )
                                    ),
                                ]
                            ),
                        ]
                    )
                ),
                new SubqueryQuery(
                    "distinct_rows",
                    new CombinedQueryUnion(
                        union:
                        [
                            new PlainQueryWithoutPaginationWithoutOrderBy(
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
                                        "status",
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
                                ]
                            ),
                            new PlainQueryWithoutPaginationWithoutOrderBy(
                                from: new FromEntity(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new UsersTable().Name,
                                        ]
                                    ).TextValue
                                ),
                                select:
                                [
                                    new SelectItemProjection(
                                        "status",
                                        new IfStringProjection(
                                            new LiteralString("pending"),
                                            new LiteralString("cancelled"),
                                            new FieldBoolean(
                                                new JoinedString(
                                                    new DotString(),
                                                    [
                                                        new RelationalSchemaWithForeignKeys().Name,
                                                        new UsersTable().Name,
                                                    ]
                                                ).TextValue,
                                                new UserActiveColumn().Name.TextValue
                                            )
                                        )
                                    ),
                                ]
                            ),
                        ]
                    )
                ),
                new SubqueryQuery(
                    "shared_rows",
                    new CombinedQueryIntersect(
                        intersect:
                        [
                            new PlainQueryWithoutPaginationWithoutOrderBy(
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
                                        "status",
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
                                ]
                            ),
                            new PlainQueryWithoutPaginationWithoutOrderBy(
                                from: new FromEntity(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new UsersTable().Name,
                                        ]
                                    ).TextValue
                                ),
                                select:
                                [
                                    new SelectItemProjection(
                                        "status",
                                        new IfStringProjection(
                                            new LiteralString("pending"),
                                            new LiteralString("cancelled"),
                                            new FieldBoolean(
                                                new JoinedString(
                                                    new DotString(),
                                                    [
                                                        new RelationalSchemaWithForeignKeys().Name,
                                                        new UsersTable().Name,
                                                    ]
                                                ).TextValue,
                                                new UserActiveColumn().Name.TextValue
                                            )
                                        )
                                    ),
                                ]
                            ),
                        ]
                    )
                ),
                new SubqueryQuery(
                    "order_only_rows",
                    new CombinedQueryExcept(
                        except:
                        [
                            new PlainQueryWithoutPaginationWithoutOrderBy(
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
                                        "status",
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
                                ]
                            ),
                            new PlainQueryWithoutPaginationWithoutOrderBy(
                                from: new FromEntity(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new UsersTable().Name,
                                        ]
                                    ).TextValue
                                ),
                                select:
                                [
                                    new SelectItemProjection(
                                        "status",
                                        new IfStringProjection(
                                            new LiteralString("pending"),
                                            new LiteralString("cancelled"),
                                            new FieldBoolean(
                                                new JoinedString(
                                                    new DotString(),
                                                    [
                                                        new RelationalSchemaWithForeignKeys().Name,
                                                        new UsersTable().Name,
                                                    ]
                                                ).TextValue,
                                                new UserActiveColumn().Name.TextValue
                                            )
                                        )
                                    ),
                                ]
                            ),
                        ]
                    )
                ),
                new SubqueryQuery(
                    "counts",
                    new CombinedQueryUnionAll(
                        unionAll:
                        [
                            new TotalQuery(
                                from: new FromSubquery("all_rows"),
                                aggregate:
                                [
                                    new SelectItemTotal(
                                        "operation",
                                        new LiteralString("unionAll")
                                    ),
                                    new SelectItemTotal("rows", new CountTotal()),
                                ]
                            ),
                            new TotalQuery(
                                from: new FromSubquery("distinct_rows"),
                                aggregate:
                                [
                                    new SelectItemTotal(
                                        "operation",
                                        new LiteralString("union")
                                    ),
                                    new SelectItemTotal("rows", new CountTotal()),
                                ]
                            ),
                            new TotalQuery(
                                from: new FromSubquery("shared_rows"),
                                aggregate:
                                [
                                    new SelectItemTotal(
                                        "operation",
                                        new LiteralString("intersect")
                                    ),
                                    new SelectItemTotal("rows", new CountTotal()),
                                ]
                            ),
                            new TotalQuery(
                                from: new FromSubquery("order_only_rows"),
                                aggregate:
                                [
                                    new SelectItemTotal(
                                        "operation",
                                        new LiteralString("except")
                                    ),
                                    new SelectItemTotal("rows", new CountTotal()),
                                ]
                            ),
                        ]
                    )
                ),
            ],
            from: new FromSubquery("counts"),
            select:
            [
                new SelectItemProjection(
                    "operation",
                    new FieldString("counts", "operation")
                ),
                new SelectItemProjection("rows", new FieldInteger("counts", "rows")),
            ],
            orderBy:
            [
                new OrderItemProjectionExpression(new FieldString("counts", "operation")),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over <see
    /// cref="SchemaDataSetWithForeignKeys"/> and <see cref="AuditSchemaDataSet"/>, in this
    /// order.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("operation"), new StringColumnType()),
                    new Column(new String("rows"), new LongColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("operation"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("except"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("rows"), new LongColumnType()),
                                new InvariantCell(new Long(1))
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
                                new Column(
                                    new String("operation"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("intersect"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("rows"), new LongColumnType()),
                                new InvariantCell(new Long(2))
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
                                new Column(
                                    new String("operation"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("union"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("rows"), new LongColumnType()),
                                new InvariantCell(new Long(3))
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
                                new Column(
                                    new String("operation"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("unionAll"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("rows"), new LongColumnType()),
                                new InvariantCell(new Long(12))
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
