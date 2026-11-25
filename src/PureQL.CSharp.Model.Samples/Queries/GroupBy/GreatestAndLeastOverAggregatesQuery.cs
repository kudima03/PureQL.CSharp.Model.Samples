using Pure.Collections.Generic;
using Pure.Primitives.Date;
using Pure.Primitives.Number;
using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.Primitives.Time;
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
using PureQL.CSharp.Model.Samples.Cells;
using Column = Pure.RelationalSchema.Column.Column;
using DateTime = Pure.Primitives.DateTime.DateTime;
using Double = Pure.Primitives.Number.Double;
using Guid = Pure.Primitives.Guid.Guid;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.GroupBy;

/// <summary>
/// Groups schema_with_foreign_keys.orders joined to schema_with_foreign_keys.users by
/// order_tenant_id as tenant_id and by the least of order_total truncated to hundreds and
/// 100 as capped_hundreds, having the least of the sum of order_total and the max of
/// three times user_age greater than 60: the keys, the greatest of the max of placed_at
/// and the max of last_login, the least of the sum of order_total and 250, the least of
/// the sum of order_total over the sum over all rows and 0.4, the sum of the least of
/// order_total and three times user_age, the count of orders whose greatest of
/// order_total and three times user_age is above three times user_age, and the greatest
/// of the count divided by 2 (integer) and 1, ordered by the last of the two datetimes
/// descending, tenant_id and capped_hundreds.
/// </summary>
public sealed record GreatestAndLeastOverAggregatesQuery
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
                    )
                ),
            ],
            groupBy:
            [
                new GroupKey(
                    "tenant_id",
                    new FieldUuid(
                        new JoinedString(
                            new DotString(),
                            [
                                new RelationalSchemaWithForeignKeys().Name,
                                new OrdersTable().Name,
                            ]
                        ).TextValue,
                        new OrderTenantIdColumn().Name.TextValue
                    )
                ),
                new GroupKey(
                    "capped_hundreds",
                    new LeastDecimalRow([
                        new TruncateDecimalRow(
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
                            new LiteralInteger(-2)
                        ),
                        new LiteralInteger(100),
                    ])
                ),
            ],
            having: new GreaterThanDecimalGroup(
                new LeastDecimalGroup([
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
                    new MaxDecimalGroup(
                        new MultiplyDecimalRow([
                            new FieldDecimal(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new UsersTable().Name,
                                    ]
                                ).TextValue,
                                new UserAgeColumn().Name.TextValue
                            ),
                            new LiteralInteger(3),
                        ])
                    ),
                ]),
                new LiteralInteger(60)
            ),
            select:
            [
                new SelectItemGroup("tenant_id", new KeyUuid("tenant_id")),
                new SelectItemGroup("capped_hundreds", new KeyDecimal("capped_hundreds")),
                new SelectItemGroup(
                    "last_seen",
                    new GreatestDatetimeGroup([
                        new MaxDatetimeGroup(
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
                        ),
                        new MaxDatetimeGroup(
                            new FieldDatetime(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new UsersTable().Name,
                                    ]
                                ).TextValue,
                                new LastLoginColumn().Name.TextValue
                            )
                        ),
                    ])
                ),
                new SelectItemGroup(
                    "capped_revenue",
                    new LeastDecimalGroup([
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
                        new LiteralDecimal(250m),
                    ])
                ),
                new SelectItemGroup(
                    "share_of_all_capped",
                    new LeastDecimalGroup([
                        new DivideDecimalGroup([
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
                                ),
                                over: AggregateOver.All
                            ),
                        ]),
                        new LiteralDecimal(0.4m),
                    ])
                ),
                new SelectItemGroup(
                    "covered",
                    new SumDecimalGroup(
                        new LeastDecimalRow([
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
                            new MultiplyDecimalRow([
                                new FieldDecimal(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new UsersTable().Name,
                                        ]
                                    ).TextValue,
                                    new UserAgeColumn().Name.TextValue
                                ),
                                new LiteralInteger(3),
                            ]),
                        ])
                    )
                ),
                new SelectItemGroup(
                    "over_limit_orders",
                    new CountGroup(
                        new GreaterThanDecimalRow(
                            new GreatestDecimalRow([
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
                                new MultiplyDecimalRow([
                                    new FieldDecimal(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new UsersTable().Name,
                                            ]
                                        ).TextValue,
                                        new UserAgeColumn().Name.TextValue
                                    ),
                                    new LiteralInteger(3),
                                ]),
                            ]),
                            new MultiplyDecimalRow([
                                new FieldDecimal(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new UsersTable().Name,
                                        ]
                                    ).TextValue,
                                    new UserAgeColumn().Name.TextValue
                                ),
                                new LiteralInteger(3),
                            ])
                        )
                    )
                ),
                new SelectItemGroup(
                    "pages",
                    new GreatestIntegerGroup([
                        new IntegerDivideIntegerGroup(
                            new CountGroup(),
                            new LiteralInteger(2)
                        ),
                        new LiteralInteger(1),
                    ])
                ),
            ],
            orderBy:
            [
                new OrderItemGroupExpression(
                    new GreatestDatetimeGroup([
                        new MaxDatetimeGroup(
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
                        ),
                        new MaxDatetimeGroup(
                            new FieldDatetime(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new UsersTable().Name,
                                    ]
                                ).TextValue,
                                new LastLoginColumn().Name.TextValue
                            )
                        ),
                    ]),
                    SortDirection.Desc
                ),
                new OrderItemGroupExpression(new KeyUuid("tenant_id")),
                new OrderItemGroupExpression(new KeyDecimal("capped_hundreds")),
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
                    new TenantIdColumn(),
                    new Column(new String("capped_hundreds"), new DoubleColumnType()),
                    new Column(new String("last_seen"), new DateTimeColumnType()),
                    new Column(new String("capped_revenue"), new DoubleColumnType()),
                    new Column(new String("share_of_all_capped"), new DoubleColumnType()),
                    new Column(new String("covered"), new DoubleColumnType()),
                    new Column(new String("over_limit_orders"), new LongColumnType()),
                    new Column(new String("pages"), new LongColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new TenantIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000386-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("capped_hundreds"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(100))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_seen"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(6),
                                            new UShort(6),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(15),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("capped_revenue"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(250))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("share_of_all_capped"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.4))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("covered"), new DoubleColumnType()),
                                new InvariantCell(new Double(190.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("over_limit_orders"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("pages"), new LongColumnType()),
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
                                new TenantIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000386-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("capped_hundreds"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_seen"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(4),
                                            new UShort(6),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(13),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("capped_revenue"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(75.25))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("share_of_all_capped"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.09107413010590015))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("covered"), new DoubleColumnType()),
                                new InvariantCell(new Double(75.25))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("over_limit_orders"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("pages"), new LongColumnType()),
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
                                new TenantIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000385-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("capped_hundreds"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(100))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_seen"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(3),
                                            new UShort(6),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(12),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("capped_revenue"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(250))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("share_of_all_capped"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.3636913767019667))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("covered"), new DoubleColumnType()),
                                new InvariantCell(new Double(165))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("over_limit_orders"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("pages"), new LongColumnType()),
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
