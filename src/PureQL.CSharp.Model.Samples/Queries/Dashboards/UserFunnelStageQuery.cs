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
using PureQL.CSharp.Model.GroupKeys;
using PureQL.CSharp.Model.Keys;
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.RowExpressions;
using PureQL.CSharp.Model.SelectItems;
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
        new PureQLQuery(
            new MainGroupedQuery(
                new From(
                    new FromEntity(
                        new JoinedString(
                            new DotString(),
                            [
                                new RelationalSchemaWithForeignKeys().Name,
                                new UsersTable().Name,
                            ]
                        ).TextValue
                    )
                ),
                [
                    new GroupKey(
                        new GroupKeyNonNullable(
                            new GroupKeyString(
                                "stage",
                                new StringRow(
                                    new ConditionalStringRow(
                                        new IfStringRow(
                                            new BooleanRow(
                                                new LogicalRow(
                                                    new NotRow(
                                                        new BooleanRow(
                                                            new FieldBoolean(
                                                                new JoinedString(
                                                                    new DotString(),
                                                                    [
                                                                        new RelationalSchemaWithForeignKeys().Name,
                                                                        new UsersTable().Name,
                                                                    ]
                                                                ).TextValue,
                                                                new UserActiveColumn()
                                                                    .Name
                                                                    .TextValue
                                                            )
                                                        )
                                                    )
                                                )
                                            ),
                                            new StringRow(
                                                new LiteralString("1. Inactive")
                                            ),
                                            new StringRow(
                                                new ConditionalStringRow(
                                                    new IfStringRow(
                                                        new BooleanRow(
                                                            new ComparisonRow(
                                                                new EqualRow(
                                                                    new EqualDecimalRow(
                                                                        new DecimalNullableRow(
                                                                            new IntegerRow(
                                                                                new ConditionalIntegerRow(
                                                                                    new CoalesceIntegerRow([
                                                                                        new IntegerNullableRow(
                                                                                            new ExactIntegerNullableRow(
                                                                                                new FieldIntegerNullable(
                                                                                                    "orders_by_user",
                                                                                                    "orders"
                                                                                                )
                                                                                            )
                                                                                        ),
                                                                                        new IntegerNullableRow(
                                                                                            new IntegerRow(
                                                                                                new LiteralInteger(
                                                                                                    0
                                                                                                )
                                                                                            )
                                                                                        ),
                                                                                    ])
                                                                                )
                                                                            )
                                                                        ),
                                                                        new DecimalNullableRow(
                                                                            new IntegerRow(
                                                                                new LiteralInteger(
                                                                                    0
                                                                                )
                                                                            )
                                                                        )
                                                                    )
                                                                )
                                                            )
                                                        ),
                                                        new StringRow(
                                                            new LiteralString(
                                                                "2. Active, no order yet"
                                                            )
                                                        ),
                                                        new StringRow(
                                                            new ConditionalStringRow(
                                                                new IfStringRow(
                                                                    new BooleanRow(
                                                                        new ComparisonRow(
                                                                            new LessThanRow(
                                                                                new LessThanDecimalRow(
                                                                                    new DecimalNullableRow(
                                                                                        new ExactIntegerNullableRow(
                                                                                            new FieldIntegerNullable(
                                                                                                "orders_by_user",
                                                                                                "orders"
                                                                                            )
                                                                                        )
                                                                                    ),
                                                                                    new DecimalNullableRow(
                                                                                        new IntegerRow(
                                                                                            new LiteralInteger(
                                                                                                2
                                                                                            )
                                                                                        )
                                                                                    )
                                                                                )
                                                                            )
                                                                        )
                                                                    ),
                                                                    new StringRow(
                                                                        new LiteralString(
                                                                            "3. Ordered once"
                                                                        )
                                                                    ),
                                                                    new StringRow(
                                                                        new LiteralString(
                                                                            "4. Repeat buyer (2+ orders)"
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
                        )
                    ),
                ],
                [
                    new SelectItemGroup(
                        new SelectItemGroupNonNullable(
                            new SelectItemGroupString(
                                "stage",
                                new StringGroup(new KeyString("stage"))
                            )
                        )
                    ),
                    new SelectItemGroup(
                        new SelectItemGroupNonNullable(
                            new SelectItemGroupInteger(
                                "users",
                                new IntegerGroup(
                                    new AggregateIntegerGroup(new CountGroup())
                                )
                            )
                        )
                    ),
                    new SelectItemGroup(
                        new SelectItemGroupNonNullable(
                            new SelectItemGroupDecimal(
                                "avg_user_score",
                                new ExactDecimalGroup(
                                    new RoundDecimalGroup(
                                        new DecimalGroup(
                                            new ExactDecimalGroup(
                                                new ConditionalDecimalGroup(
                                                    new CoalesceDecimalGroup([
                                                        new DecimalNullableGroup(
                                                            new ExactDecimalNullableGroup(
                                                                new AggregateDecimalNullableGroup(
                                                                    new AverageDecimalNullableGroup(
                                                                        new DecimalNullableRow(
                                                                            new ExactDecimalNullableRow(
                                                                                new FieldDecimalNullable(
                                                                                    new JoinedString(
                                                                                        new DotString(),
                                                                                        [
                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                            new UsersTable().Name,
                                                                                        ]
                                                                                    ).TextValue,
                                                                                    new UserScoreColumn()
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
                                                            new ExactDecimalGroup(
                                                                new LiteralDecimal(0m)
                                                            )
                                                        ),
                                                    ])
                                                )
                                            )
                                        ),
                                        new IntegerGroup(new LiteralInteger(1))
                                    )
                                )
                            )
                        )
                    ),
                ],
                [
                    new Subquery(
                        "orders_by_user",
                        new Query(
                            new GroupedQuery(
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
                                                "user_id",
                                                new UuidRow(
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
                                            )
                                        )
                                    ),
                                ],
                                [
                                    new SelectItemGroup(
                                        new SelectItemGroupNonNullable(
                                            new SelectItemGroupUuid(
                                                "user_id",
                                                new UuidGroup(new KeyUuid("user_id"))
                                            )
                                        )
                                    ),
                                    new SelectItemGroup(
                                        new SelectItemGroupNonNullable(
                                            new SelectItemGroupInteger(
                                                "orders",
                                                new IntegerGroup(
                                                    new AggregateIntegerGroup(
                                                        new CountGroup()
                                                    )
                                                )
                                            )
                                        )
                                    ),
                                ],
                                joins: null,
                                new BooleanRow(
                                    new ComparisonRow(
                                        new NotEqualRow(
                                            new NotEqualStringRow(
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
                                                        new LiteralString("cancelled")
                                                    )
                                                )
                                            )
                                        )
                                    )
                                ),
                                having: null,
                                orderBy: null,
                                pagination: null,
                                distinctBy: null
                            )
                        )
                    ),
                ],
                [
                    new Join(
                        new JoinSubquery(
                            JoinKind.Left,
                            "orders_by_user",
                            new BooleanRow(
                                new ComparisonRow(
                                    new EqualRow(
                                        new EqualUuidRow(
                                            new UuidNullableRow(
                                                new UuidRow(
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
                                                )
                                            ),
                                            new UuidNullableRow(
                                                new UuidRow(
                                                    new FieldUuid(
                                                        "orders_by_user",
                                                        "user_id"
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
                where: null,
                having: null,
                [
                    new OrderItemGroup(
                        new ExprGroup(
                            new StringNullableGroup(
                                new StringGroup(new KeyString("stage"))
                            )
                        )
                    ),
                ],
                pagination: null,
                distinctBy: null
            )
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
