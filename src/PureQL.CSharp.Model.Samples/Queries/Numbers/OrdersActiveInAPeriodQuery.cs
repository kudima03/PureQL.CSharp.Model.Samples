using Pure.Collections.Generic;
using Pure.Primitives.Date;
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
using PureQL.CSharp.Model.RowExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using Guid = Pure.Primitives.Guid.Guid;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Numbers;

/// <summary>
/// Selects order_id from schema_with_foreign_keys.orders left joined to its shipped
/// schema_with_foreign_keys.orders (aliased shipped, on order_id and an order_status of
/// shipped) and to its active schema_with_foreign_keys.users, where the later of
/// placed_on and 2024-06-03 is not after the earlier of the shipped placed_on (coalesced
/// with 2024-06-10) and 2024-06-05: that later date as active_from, that earlier date as
/// active_to, the days between them plus 1, the least of twice the user_score (coalesced
/// with 0) and order_total, order_total less that, floored at 0, and the least of twice
/// the nullable user_score and order_total, ordered by the later date and order_id.
/// </summary>
public sealed record OrdersActiveInAPeriodQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithoutPagination(
            from: new FromEntity(
                new JoinedString(
                    new DotString(),
                    [new RelationalSchemaWithForeignKeys().Name, new OrdersTable().Name]
                ).TextValue
            ),
            joins:
            [
                new JoinEntity(
                    JoinKind.Left,
                    new JoinedString(
                        new DotString(),
                        [
                            new RelationalSchemaWithForeignKeys().Name,
                            new OrdersTable().Name,
                        ]
                    ).TextValue,
                    new AndRow([
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
                            new FieldUuid("shipped", new OrderIdColumn().Name.TextValue)
                        ),
                        new EqualStringRow(
                            new FieldString(
                                "shipped",
                                new OrderStatusColumn().Name.TextValue
                            ),
                            new LiteralString("shipped")
                        ),
                    ]),
                    "shipped"
                ),
                new JoinEntity(
                    JoinKind.Left,
                    new JoinedString(
                        new DotString(),
                        [
                            new RelationalSchemaWithForeignKeys().Name,
                            new UsersTable().Name,
                        ]
                    ).TextValue,
                    new AndRow([
                        new EqualUuidRow(
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
                        ),
                        new FieldBoolean(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue,
                            new UserActiveColumn().Name.TextValue
                        ),
                    ])
                ),
            ],
            where: new LessThanOrEqualDateRow(
                new GreatestDateRow([
                    new FieldDate(
                        new JoinedString(
                            new DotString(),
                            [
                                new RelationalSchemaWithForeignKeys().Name,
                                new OrdersTable().Name,
                            ]
                        ).TextValue,
                        new PlacedOnColumn().Name.TextValue
                    ),
                    new LiteralDate(new DateOnly(2024, 6, 3)),
                ]),
                new LeastDateRow([
                    new CoalesceDateRow([
                        new FieldDateNullable(
                            "shipped",
                            new PlacedOnColumn().Name.TextValue
                        ),
                        new LiteralDate(new DateOnly(2024, 6, 10)),
                    ]),
                    new LiteralDate(new DateOnly(2024, 6, 5)),
                ])
            ),
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
                    "active_from",
                    new GreatestDateProjection([
                        new FieldDate(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrdersTable().Name,
                                ]
                            ).TextValue,
                            new PlacedOnColumn().Name.TextValue
                        ),
                        new LiteralDate(new DateOnly(2024, 6, 3)),
                    ])
                ),
                new SelectItemProjection(
                    "active_to",
                    new LeastDateProjection([
                        new CoalesceDateProjection([
                            new FieldDateNullable(
                                "shipped",
                                new PlacedOnColumn().Name.TextValue
                            ),
                            new LiteralDate(new DateOnly(2024, 6, 10)),
                        ]),
                        new LiteralDate(new DateOnly(2024, 6, 5)),
                    ])
                ),
                new SelectItemProjection(
                    "days_in_period",
                    new AddIntegerProjection([
                        new DateDiffDaysIntegerProjection(
                            new LeastDateProjection([
                                new CoalesceDateProjection([
                                    new FieldDateNullable(
                                        "shipped",
                                        new PlacedOnColumn().Name.TextValue
                                    ),
                                    new LiteralDate(new DateOnly(2024, 6, 10)),
                                ]),
                                new LiteralDate(new DateOnly(2024, 6, 5)),
                            ]),
                            new GreatestDateProjection([
                                new FieldDate(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new OrdersTable().Name,
                                        ]
                                    ).TextValue,
                                    new PlacedOnColumn().Name.TextValue
                                ),
                                new LiteralDate(new DateOnly(2024, 6, 3)),
                            ])
                        ),
                        new LiteralInteger(1),
                    ])
                ),
                new SelectItemProjection(
                    "discount_applied",
                    new LeastDecimalProjection([
                        new CoalesceDecimalProjection([
                            new MultiplyDecimalNullableProjection([
                                new FieldDecimalNullable(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new UsersTable().Name,
                                        ]
                                    ).TextValue,
                                    new UserScoreColumn().Name.TextValue
                                ),
                                new LiteralInteger(2),
                            ]),
                            new LiteralInteger(0),
                        ]),
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
                    ])
                ),
                new SelectItemProjection(
                    "payable",
                    new GreatestDecimalProjection([
                        new SubtractDecimalProjection([
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
                            new LeastDecimalProjection([
                                new CoalesceDecimalProjection([
                                    new MultiplyDecimalNullableProjection([
                                        new FieldDecimalNullable(
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new RelationalSchemaWithForeignKeys().Name,
                                                    new UsersTable().Name,
                                                ]
                                            ).TextValue,
                                            new UserScoreColumn().Name.TextValue
                                        ),
                                        new LiteralInteger(2),
                                    ]),
                                    new LiteralInteger(0),
                                ]),
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
                            ]),
                        ]),
                        new LiteralInteger(0),
                    ])
                ),
                new SelectItemProjection(
                    "discount_or_null",
                    new LeastDecimalNullableProjection([
                        new MultiplyDecimalNullableProjection([
                            new FieldDecimalNullable(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new UsersTable().Name,
                                    ]
                                ).TextValue,
                                new UserScoreColumn().Name.TextValue
                            ),
                            new LiteralInteger(2),
                        ]),
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
                    ])
                ),
            ],
            orderBy:
            [
                new OrderItemProjectionExpression(
                    new GreatestDateProjection([
                        new FieldDate(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrdersTable().Name,
                                ]
                            ).TextValue,
                            new PlacedOnColumn().Name.TextValue
                        ),
                        new LiteralDate(new DateOnly(2024, 6, 3)),
                    ])
                ),
                new OrderItemProjectionExpression(
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
    /// The rows the query returns under the PureQL specification's semantics over <see
    /// cref="SchemaDataSetWithForeignKeys"/> and <see cref="AuditSchemaDataSet"/>, in
    /// this order.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new OrderIdColumn(),
                    new Column(new String("active_from"), new DateColumnType()),
                    new Column(new String("active_to"), new DateColumnType()),
                    new Column(new String("days_in_period"), new LongColumnType()),
                    new Column(new String("discount_applied"), new DoubleColumnType()),
                    new Column(new String("payable"), new DoubleColumnType()),
                    new Column(new String("discount_or_null"), new DoubleColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new OrderIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000066-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("active_from"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(3),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("active_to"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(5),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("days_in_period"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("discount_applied"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(50))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("payable"), new DoubleColumnType()),
                                new InvariantCell(new Double(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("discount_or_null"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(50))
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
                                new OrderIdColumn(),
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
                                    new String("active_from"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(3),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("active_to"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(3),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("days_in_period"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("discount_applied"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("payable"), new DoubleColumnType()),
                                new InvariantCell(new Double(200))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("discount_or_null"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
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
                                new OrderIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000068-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("active_from"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(4),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("active_to"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(5),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("days_in_period"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("discount_applied"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(60))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("payable"), new DoubleColumnType()),
                                new InvariantCell(new Double(15.25))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("discount_or_null"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(60))
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
                                new OrderIdColumn(),
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
                                    new String("active_from"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(5),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("active_to"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(5),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("days_in_period"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("discount_applied"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(60))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("payable"), new DoubleColumnType()),
                                new InvariantCell(new Double(240))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("discount_or_null"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(60))
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
