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
using PureQL.CSharp.Model.Lists;
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.RowExpressions;
using PureQL.CSharp.Model.SelectItems;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Dashboards;

/// <summary>
/// Selects an order stage as stage, the count of rows as orders and the coalesce of the
/// average user_score with 0 rounded to 1 digit as avg_user_score from
/// schema_with_foreign_keys.orders inner joined with schema_with_foreign_keys.users on
/// order_user_id equal to user_id, where order_status is in pending, shipped, cancelled
/// and refunded and user_name is not equal to Ann, grouped by the stage, which is
/// 1. Placed if order_status equals pending, otherwise 2. Shipped if it equals shipped,
/// otherwise 3. Cancelled if it equals cancelled and otherwise 4. Refunded, ordered by
/// stage.
/// </summary>
public sealed record OrderStageExcludingUserQuery
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
                            new GroupKeyString(
                                "stage",
                                new StringRow(
                                    new ConditionalStringRow(
                                        new IfStringRow(
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
                                                                        "pending"
                                                                    )
                                                                )
                                                            )
                                                        )
                                                    )
                                                )
                                            ),
                                            new StringRow(new LiteralString("1. Placed")),
                                            new StringRow(
                                                new ConditionalStringRow(
                                                    new IfStringRow(
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
                                                                                    "shipped"
                                                                                )
                                                                            )
                                                                        )
                                                                    )
                                                                )
                                                            )
                                                        ),
                                                        new StringRow(
                                                            new LiteralString(
                                                                "2. Shipped"
                                                            )
                                                        ),
                                                        new StringRow(
                                                            new ConditionalStringRow(
                                                                new IfStringRow(
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
                                                                    ),
                                                                    new StringRow(
                                                                        new LiteralString(
                                                                            "3. Cancelled"
                                                                        )
                                                                    ),
                                                                    new StringRow(
                                                                        new LiteralString(
                                                                            "4. Refunded"
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
                                "orders",
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
                subqueries: null,
                [
                    new Join(
                        new JoinEntity(
                            JoinKind.Inner,
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue,
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
                                                                new OrdersTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new OrderUserIdColumn()
                                                            .Name
                                                            .TextValue
                                                    )
                                                )
                                            ),
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
                                            )
                                        )
                                    )
                                )
                            )
                        )
                    ),
                ],
                new BooleanRow(
                    new LogicalRow(
                        new AndRow([
                            new BooleanRow(
                                new ComparisonRow(
                                    new InRow(
                                        new InStringRow(
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
                                            new ListString(
                                                new ListLiteralString([
                                                    "pending",
                                                    "shipped",
                                                    "cancelled",
                                                    "refunded",
                                                ])
                                            )
                                        )
                                    )
                                )
                            ),
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
                                                                new UsersTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new UserNameColumn()
                                                            .Name
                                                            .TextValue
                                                    )
                                                )
                                            ),
                                            new StringNullableRow(
                                                new StringRow(new LiteralString("Ann"))
                                            )
                                        )
                                    )
                                )
                            ),
                        ])
                    )
                ),
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
                    new Column(new String("orders"), new LongColumnType()),
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
                                new InvariantCell(new String("1. Placed"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("avg_user_score"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0))
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
                                new InvariantCell(new String("2. Shipped"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
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
                                new InvariantCell(new String("3. Cancelled"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
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
