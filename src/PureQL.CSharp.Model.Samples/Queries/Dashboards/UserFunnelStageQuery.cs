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
using PureQL.CSharp.Model.GroupExpressions;
using PureQL.CSharp.Model.Keys;
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.RowExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Dashboards;

/// <summary>
/// Selects a funnel stage as stage, the count of rows as users and the coalesce of the
/// average user_score with 0 rounded to 1 digit as avg_user_score from
/// schema_with_foreign_keys.users left joined with the subquery orders_by_user on
/// user_id equal to its user_id, where orders_by_user counts the rows of
/// schema_with_foreign_keys.orders whose order_status is not equal to cancelled as
/// orders per order_user_id as user_id, grouped by the stage, which is 1. Inactive if
/// user_active is not true, otherwise 2. Active, no order yet if the coalesce of orders
/// with 0 equals 0, otherwise 3. Ordered once if orders is less than 2 and otherwise
/// 4. Repeat buyer (2+ orders), ordered by stage.
/// </summary>
public sealed record UserFunnelStageQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainGroupedQueryWithoutPagination(
            subqueries:
            [
                new Subquery(
                    "orders_by_user",
                    new GroupedQueryWithoutPaginationWithoutOrderBy(
                        from: new FromEntity(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrdersTable().Name,
                                ]
                            ).TextValue
                        ),
                        where: new NotEqualStringRow(
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
                            new LiteralString("cancelled")
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
                        ]
                    )
                ),
            ],
            from: new FromEntity(
                new JoinedString(
                    new DotString(),
                    [new RelationalSchemaWithForeignKeys().Name, new UsersTable().Name]
                ).TextValue
            ),
            joins:
            [
                new JoinSubquery(
                    JoinKind.Left,
                    "orders_by_user",
                    new EqualUuidRow(
                        new FieldUuid(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue,
                            new UserIdColumn().Name.TextValue
                        ),
                        new FieldUuid("orders_by_user", "user_id")
                    )
                ),
            ],
            groupBy:
            [
                new GroupKey(
                    "stage",
                    new IfStringRow(
                        new LiteralString("1. Inactive"),
                        new IfStringRow(
                            new LiteralString("2. Active, no order yet"),
                            new IfStringRow(
                                new LiteralString("3. Ordered once"),
                                new LiteralString("4. Repeat buyer (2+ orders)"),
                                new LessThanDecimalRow(
                                    new FieldIntegerNullable("orders_by_user", "orders"),
                                    new LiteralInteger(2)
                                )
                            ),
                            new EqualDecimalRow(
                                new CoalesceIntegerRow([
                                    new FieldIntegerNullable("orders_by_user", "orders"),
                                    new LiteralInteger(0),
                                ]),
                                new LiteralInteger(0)
                            )
                        ),
                        new NotRow(
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
                    )
                ),
            ],
            select:
            [
                new SelectItemGroup("stage", new KeyString("stage")),
                new SelectItemGroup("users", new CountGroup()),
                new SelectItemGroup(
                    "avg_user_score",
                    new RoundDecimalGroup(
                        new CoalesceDecimalGroup([
                            new AverageDecimalNullableGroup(
                                new FieldDecimalNullable(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new UsersTable().Name,
                                        ]
                                    ).TextValue,
                                    new UserScoreColumn().Name.TextValue
                                )
                            ),
                            new LiteralDecimal(0m),
                        ]),
                        new LiteralInteger(1)
                    )
                ),
            ],
            orderBy: [new OrderItemGroup(new KeyString("stage"))]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/>, in this order.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("stage"), new StringColumnType()),
                    new Column(new String("users"), new LongColumnType()),
                    new Column(new String("avg_user_score"), new DoubleColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("stage"), new StringColumnType()),
                                new InvariantCell(new String("1. Inactive"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("users"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("avg_user_score"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(10))
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
                                new Column(new String("stage"), new StringColumnType()),
                                new InvariantCell(new String("2. Active, no order yet"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("users"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("avg_user_score"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(28))
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
                                new Column(new String("stage"), new StringColumnType()),
                                new InvariantCell(new String("3. Ordered once"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("users"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("avg_user_score"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(30))
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
                                new Column(new String("stage"), new StringColumnType()),
                                new InvariantCell(
                                    new String("4. Repeat buyer (2+ orders)")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("users"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("avg_user_score"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(30))
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
