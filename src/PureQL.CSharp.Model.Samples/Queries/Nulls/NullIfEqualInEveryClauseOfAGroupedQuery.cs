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

namespace PureQL.CSharp.Model.Samples.Queries.Nulls;

/// <summary>
/// Selects from schema_with_foreign_keys.orders inner joined to
/// schema_with_foreign_keys.users on nullIfEqual of order_user_id and the uuid of user 4,
/// the placeholder user, equal to user_id, where nullIfEqual of order_status and
/// cancelled is not null, grouped by nullIfEqual of order_status and pending as status
/// and nullIfEqual of user_name and Cara as user_name, having the sum of order_total
/// divided by nullIfEqual of the sum of user_score and 0 greater than the decimal 0.5:
/// the two keys, that ratio as total_per_score, the average of nullIfEqual of order_total
/// and 50 as average_non_placeholder_total and the count of orders whose nullIfEqual of
/// order_total and 50 equals a null decimal as placeholder_totals, ordered by the ratio
/// or else 0 descending, status and user_name.
/// </summary>
public sealed record NullIfEqualInEveryClauseOfAGroupedQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainGroupedQueryWithoutPagination(
            from: new FromEntity(
                new JoinedString(
                    new DotString(),
                    [new RelationalSchemaWithForeignKeys().Name, new OrdersTable().Name]
                ).TextValue
            ),
            joins:
            [
                new JoinEntity(
                    JoinKind.Inner,
                    new JoinedString(
                        new DotString(),
                        [
                            new RelationalSchemaWithForeignKeys().Name,
                            new UsersTable().Name,
                        ]
                    ).TextValue,
                    new EqualUuidRow(
                        new NullIfEqualUuidNullableRow(
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
                            new LiteralUuid(new Guid(4, 0, 0, new byte[8]))
                        ),
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
            ],
            where: new NotEqualStringRow(
                new NullIfEqualStringNullableRow(
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
                new LiteralStringNullable()
            ),
            groupBy:
            [
                new GroupKey(
                    "status",
                    new NullIfEqualStringNullableRow(
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
                        new LiteralString("pending")
                    )
                ),
                new GroupKey(
                    "user_name",
                    new NullIfEqualStringNullableRow(
                        new FieldString(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue,
                            new UserNameColumn().Name.TextValue
                        ),
                        new LiteralString("Cara")
                    )
                ),
            ],
            having: new GreaterThanDecimalGroup(
                new DivideDecimalNullableGroup([
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
                    ),
                    new NullIfEqualDecimalNullableGroup(
                        new SumDecimalGroup(
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
                        new LiteralInteger(0)
                    ),
                ]),
                new LiteralDecimal(0.5m)
            ),
            select:
            [
                new SelectItemGroup("status", new KeyStringNullable("status")),
                new SelectItemGroup("user_name", new KeyStringNullable("user_name")),
                new SelectItemGroup(
                    "total_per_score",
                    new DivideDecimalNullableGroup([
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
                        ),
                        new NullIfEqualDecimalNullableGroup(
                            new SumDecimalGroup(
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
                            new LiteralInteger(0)
                        ),
                    ])
                ),
                new SelectItemGroup(
                    "average_non_placeholder_total",
                    new AverageDecimalNullableGroup(
                        new NullIfEqualDecimalNullableRow(
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
                            new LiteralInteger(50)
                        )
                    )
                ),
                new SelectItemGroup(
                    "placeholder_totals",
                    new CountGroup(
                        new EqualDecimalRow(
                            new NullIfEqualDecimalNullableRow(
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
                                new LiteralInteger(50)
                            ),
                            new LiteralDecimalNullable()
                        )
                    )
                ),
            ],
            orderBy:
            [
                new OrderItemGroupExpression(
                    new CoalesceDecimalGroup([
                        new DivideDecimalNullableGroup([
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
                            ),
                            new NullIfEqualDecimalNullableGroup(
                                new SumDecimalGroup(
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
                                new LiteralInteger(0)
                            ),
                        ]),
                        new LiteralDecimal(0m),
                    ]),
                    SortDirection.Desc
                ),
                new OrderItemGroupExpression(new KeyStringNullable("status")),
                new OrderItemGroupExpression(new KeyStringNullable("user_name")),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over <see
    /// cref="SchemaDataSetWithForeignKeys"/> and <see cref="AuditSchemaDataSet"/>, in
    /// this order. The ratios are computed from the exact sums with Python decimal.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("status"), new StringColumnType()),
                    new UserNameColumn(),
                    new Column(new String("total_per_score"), new DoubleColumnType()),
                    new Column(
                        new String("average_non_placeholder_total"),
                        new DoubleColumnType()
                    ),
                    new Column(new String("placeholder_totals"), new LongColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("status"), new StringColumnType()),
                                new InvariantCell(new String("shipped"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new UserNameColumn(),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("total_per_score"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(10))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("average_non_placeholder_total"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(300))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("placeholder_totals"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
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
                                new Column(new String("status"), new StringColumnType()),
                                new InvariantCell(new String("shipped"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new UserNameColumn(),
                                new InvariantCell(new String("Ann"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("total_per_score"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(3.35))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("average_non_placeholder_total"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(100.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("placeholder_totals"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
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
                                new Column(new String("status"), new StringColumnType()),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new UserNameColumn(),
                                new InvariantCell(new String("Ann"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("total_per_score"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1.6666666666666667))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("average_non_placeholder_total"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("placeholder_totals"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
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
