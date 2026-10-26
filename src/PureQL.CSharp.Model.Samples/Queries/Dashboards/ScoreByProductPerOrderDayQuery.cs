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
/// Selects the days from 2024-06-01 to placed_on as days_since_launch, the count of rows
/// as item_lines, the average of the nullable user_score over rows whose product_name
/// equals Widget as avg_score_widget, the same average over rows whose product_name does
/// not equal Widget as avg_score_other and, unless the count of rows is 0, 100 times the
/// count of rows with user_active divided by the count of rows rounded to 1 digit as
/// active_share_pct from schema_with_foreign_keys.order_items inner joined with
/// schema_with_foreign_keys.orders on item_order_id equal to order_id, with
/// schema_with_foreign_keys.users on order_user_id equal to user_id and with
/// schema_with_foreign_keys.products on item_product_id equal to product_id, where
/// order_status equals shipped, grouped by days_since_launch, ordered by
/// days_since_launch.
/// </summary>
public sealed record ScoreByProductPerOrderDayQuery
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
                                new OrderItemsTable().Name,
                            ]
                        ).TextValue
                    )
                ),
                [
                    new GroupKey(
                        new GroupKeyNonNullable(
                            new GroupKeyInteger(
                                "days_since_launch",
                                new IntegerRow(
                                    new DateDiffDaysIntegerRow(
                                        new DateRow(
                                            new FieldDate(
                                                new JoinedString(
                                                    new DotString(),
                                                    [
                                                        new RelationalSchemaWithForeignKeys().Name,
                                                        new OrdersTable().Name,
                                                    ]
                                                ).TextValue,
                                                new PlacedOnColumn().Name.TextValue
                                            )
                                        ),
                                        new DateRow(
                                            new LiteralDate(new DateOnly(2024, 6, 1))
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
                            new SelectItemGroupInteger(
                                "days_since_launch",
                                new IntegerGroup(new KeyInteger("days_since_launch"))
                            )
                        )
                    ),
                    new SelectItemGroup(
                        new SelectItemGroupNonNullable(
                            new SelectItemGroupInteger(
                                "item_lines",
                                new IntegerGroup(
                                    new AggregateIntegerGroup(new CountGroup())
                                )
                            )
                        )
                    ),
                    new SelectItemGroup(
                        new SelectItemGroupNullable(
                            new SelectItemGroupDecimalNullable(
                                "avg_score_widget",
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
                                            ),
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
                                                                                new ProductsTable().Name,
                                                                            ]
                                                                        ).TextValue,
                                                                        new ProductNameColumn()
                                                                            .Name
                                                                            .TextValue
                                                                    )
                                                                )
                                                            ),
                                                            new StringNullableRow(
                                                                new StringRow(
                                                                    new LiteralString(
                                                                        "Widget"
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
                    new SelectItemGroup(
                        new SelectItemGroupNullable(
                            new SelectItemGroupDecimalNullable(
                                "avg_score_other",
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
                                                                                new ProductsTable().Name,
                                                                            ]
                                                                        ).TextValue,
                                                                        new ProductNameColumn()
                                                                            .Name
                                                                            .TextValue
                                                                    )
                                                                )
                                                            ),
                                                            new StringNullableRow(
                                                                new StringRow(
                                                                    new LiteralString(
                                                                        "Widget"
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
                    new SelectItemGroup(
                        new SelectItemGroupNonNullable(
                            new SelectItemGroupDecimal(
                                "active_share_pct",
                                new ExactDecimalGroup(
                                    new ConditionalDecimalGroup(
                                        new IfDecimalGroup(
                                            new BooleanGroup(
                                                new ComparisonGroup(
                                                    new EqualGroup(
                                                        new EqualDecimalGroup(
                                                            new DecimalNullableGroup(
                                                                new IntegerGroup(
                                                                    new AggregateIntegerGroup(
                                                                        new CountGroup()
                                                                    )
                                                                )
                                                            ),
                                                            new DecimalNullableGroup(
                                                                new IntegerGroup(
                                                                    new LiteralInteger(0)
                                                                )
                                                            )
                                                        )
                                                    )
                                                )
                                            ),
                                            new DecimalGroup(
                                                new ExactDecimalGroup(
                                                    new LiteralDecimal(0m)
                                                )
                                            ),
                                            new DecimalGroup(
                                                new ExactDecimalGroup(
                                                    new RoundDecimalGroup(
                                                        new DecimalGroup(
                                                            new ExactDecimalGroup(
                                                                new ArithmeticDecimalGroup(
                                                                    new MultiplyDecimalGroup([
                                                                        new DecimalGroup(
                                                                            new ExactDecimalGroup(
                                                                                new LiteralDecimal(
                                                                                    100m
                                                                                )
                                                                            )
                                                                        ),
                                                                        new DecimalGroup(
                                                                            new ExactDecimalGroup(
                                                                                new ArithmeticDecimalGroup(
                                                                                    new DivideDecimalGroup([
                                                                                        new DecimalGroup(
                                                                                            new IntegerGroup(
                                                                                                new AggregateIntegerGroup(
                                                                                                    new CountGroup(
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
                                                                                            )
                                                                                        ),
                                                                                        new DecimalGroup(
                                                                                            new IntegerGroup(
                                                                                                new AggregateIntegerGroup(
                                                                                                    new CountGroup()
                                                                                                )
                                                                                            )
                                                                                        ),
                                                                                    ])
                                                                                )
                                                                            )
                                                                        ),
                                                                    ])
                                                                )
                                                            )
                                                        ),
                                                        new IntegerGroup(
                                                            new LiteralInteger(1)
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
                subqueries: null,
                [
                    new Join(
                        new JoinEntity(
                            JoinKind.Inner,
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrdersTable().Name,
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
                                                                new OrderItemsTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new ItemOrderIdColumn()
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
                                                                new OrdersTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new OrderIdColumn().Name.TextValue
                                                    )
                                                )
                                            )
                                        )
                                    )
                                )
                            )
                        )
                    ),
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
                    new Join(
                        new JoinEntity(
                            JoinKind.Inner,
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new ProductsTable().Name,
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
                                                                new OrderItemsTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new ItemProductIdColumn()
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
                                                                new ProductsTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new ProductIdColumn()
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
                                            new OrderStatusColumn().Name.TextValue
                                        )
                                    )
                                ),
                                new StringNullableRow(
                                    new StringRow(new LiteralString("shipped"))
                                )
                            )
                        )
                    )
                ),
                having: null,
                [
                    new OrderItemGroup(
                        new ExprGroup(
                            new DecimalNullableGroup(
                                new IntegerGroup(new KeyInteger("days_since_launch"))
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
                    new Column(new String("days_since_launch"), new LongColumnType()),
                    new Column(new String("item_lines"), new LongColumnType()),
                    new Column(new String("avg_score_widget"), new DoubleColumnType()),
                    new Column(new String("avg_score_other"), new DoubleColumnType()),
                    new Column(new String("active_share_pct"), new DoubleColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("days_since_launch"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("item_lines"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("avg_score_widget"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(30))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("avg_score_other"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(30))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("active_share_pct"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(100))
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
                                    new String("days_since_launch"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("item_lines"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("avg_score_widget"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("avg_score_other"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("active_share_pct"),
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
                                new Column(
                                    new String("days_since_launch"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(4))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("item_lines"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("avg_score_widget"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(30))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("avg_score_other"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("active_share_pct"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(100))
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
