using Pure.Collections.Generic;
using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Abstractions.Column;
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
using PureQL.CSharp.Model.Lists;
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.RowExpressions;
using Guid = Pure.Primitives.Guid.Guid;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Where;

/// <summary>
/// Selects order_id as id from schema_with_foreign_keys.orders inner joined on order_id
/// to the subquery facts of inline rows (is_paid and quantity of each order), where
/// order_status is not in the list "cancelled", is_paid is in the list true, quantity is
/// in the decimal list 1.5, 1, 2, order_total is in the integer list 50, 200, a null
/// string is not in the list "pending", "shipped", order_user_id is in the referrer_id
/// column of the subquery referrers (the user of the manager of each employee, null for
/// the top of the hierarchy) and placed_at is in the datetime list 2024-06-02T11:00:00Z,
/// 2024-06-03T12:00:00Z, 2024-06-03T12:00:00.5Z.
/// </summary>
public sealed record InOverEveryKindOfListQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithoutPagination(
            subqueries:
            [
                new SubqueryQuery(
                    "referrers",
                    new PlainQueryWithoutPaginationWithoutOrderBy(
                        from: new FromEntity(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new EmployeesTable().Name,
                                ]
                            ).TextValue,
                            "e"
                        ),
                        joins:
                        [
                            new JoinEntity(
                                JoinKind.Left,
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new EmployeesTable().Name,
                                    ]
                                ).TextValue,
                                new EqualUuidRow(
                                    new FieldUuidNullable(
                                        "e",
                                        new EmployeeManagerIdColumn().Name.TextValue
                                    ),
                                    new FieldUuid(
                                        "m",
                                        new EmployeeIdColumn().Name.TextValue
                                    )
                                ),
                                "m"
                            ),
                        ],
                        select:
                        [
                            new SelectItemProjection(
                                "referrer_id",
                                new FieldUuidNullable(
                                    "m",
                                    new EmployeeUserIdColumn().Name.TextValue
                                )
                            ),
                        ]
                    )
                ),
                new SubqueryRows(
                    "facts",
                    [
                        [
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new System.Guid(101, 0, 0, new byte[8]))
                            ),
                            new RowItem("is_paid", new LiteralBoolean(true)),
                            new RowItem("quantity", new LiteralInteger(2)),
                        ],
                        [
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new System.Guid(102, 0, 0, new byte[8]))
                            ),
                            new RowItem("is_paid", new LiteralBoolean(true)),
                            new RowItem("quantity", new LiteralInteger(1)),
                        ],
                        [
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new System.Guid(103, 0, 0, new byte[8]))
                            ),
                            new RowItem("is_paid", new LiteralBoolean(true)),
                            new RowItem("quantity", new LiteralInteger(2)),
                        ],
                        [
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new System.Guid(104, 0, 0, new byte[8]))
                            ),
                            new RowItem("is_paid", new LiteralBoolean(false)),
                            new RowItem("quantity", new LiteralInteger(4)),
                        ],
                        [
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new System.Guid(105, 0, 0, new byte[8]))
                            ),
                            new RowItem("is_paid", new LiteralBoolean(true)),
                            new RowItem("quantity", new LiteralInteger(3)),
                        ],
                        [
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new System.Guid(106, 0, 0, new byte[8]))
                            ),
                            new RowItem("is_paid", new LiteralBoolean(false)),
                            new RowItem("quantity", new LiteralInteger(1)),
                        ],
                    ]
                ),
            ],
            from: new FromEntity(
                new JoinedString(
                    new DotString(),
                    [new RelationalSchemaWithForeignKeys().Name, new OrdersTable().Name]
                ).TextValue
            ),
            joins:
            [
                new JoinSubquery(
                    JoinKind.Inner,
                    "facts",
                    new EqualUuidRow(
                        new FieldUuid(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrdersTable().Name,
                                ]
                            ).TextValue,
                            new OrderIdColumn().Name.TextValue
                        ),
                        new FieldUuid("facts", "order_id")
                    )
                ),
            ],
            where: new AndRow([
                new NotRow(
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
                        new ListLiteralString(["cancelled"])
                    )
                ),
                new InBooleanRow(
                    new FieldBoolean("facts", "is_paid"),
                    new ListLiteralBoolean([true])
                ),
                new InDecimalRow(
                    new FieldInteger("facts", "quantity"),
                    new ListLiteralDecimal([1.5m, 1m, 2m])
                ),
                new InDecimalRow(
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
                    new ListLiteralInteger([50, 200])
                ),
                new NotRow(
                    new InStringRow(
                        new LiteralStringNullable(),
                        new ListLiteralString(["pending", "shipped"])
                    )
                ),
                new InUuidRow(
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
                    new ListSubqueryColumnUuidNullable("referrers", "referrer_id")
                ),
                new InDatetimeRow(
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
                    new ListLiteralDatetime([
                        new DateTimeOffset(2024, 6, 2, 11, 0, 0, TimeSpan.Zero),
                        new DateTimeOffset(2024, 6, 3, 12, 0, 0, TimeSpan.Zero),
                        new DateTimeOffset(2024, 6, 3, 12, 0, 0, 500, TimeSpan.Zero),
                    ])
                ),
            ]),
            select:
            [
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
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and
    /// <see cref="AuditSchemaDataSet"/>, in no particular order.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(new EmptyString(), [new IdColumn()], []),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new IdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000066-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
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
                                new IdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000067-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
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
