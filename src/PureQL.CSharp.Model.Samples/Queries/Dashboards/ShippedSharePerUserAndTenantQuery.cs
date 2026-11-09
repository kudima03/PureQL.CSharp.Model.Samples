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
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.RowExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using Guid = Pure.Primitives.Guid.Guid;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Dashboards;

/// <summary>
/// Selects order_tenant_id and, for each of Ann, Cara and Dan as shipped_pct_ann,
/// shipped_pct_cara and shipped_pct_dan, the decimal 0 if the count of rows whose
/// user_name is that name is the integer 0, else the decimal 100 times the count of rows
/// whose order_status is shipped and whose user_name is that name divided by that count,
/// rounded to 1 digit, from schema_with_foreign_keys.orders inner joined with
/// schema_with_foreign_keys.users on order_user_id equal to user_id, where user_active is
/// true, grouped by order_tenant_id, ordered by order_tenant_id.
/// </summary>
public sealed record ShippedSharePerUserAndTenantQuery
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
            where: new FieldBoolean(
                new JoinedString(
                    new DotString(),
                    [new RelationalSchemaWithForeignKeys().Name, new UsersTable().Name]
                ).TextValue,
                new UserActiveColumn().Name.TextValue
            ),
            groupBy:
            [
                new GroupKey(
                    "order_tenant_id",
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
            ],
            select:
            [
                new SelectItemGroup("order_tenant_id", new KeyUuid("order_tenant_id")),
                new SelectItemGroup(
                    "shipped_pct_ann",
                    new IfDecimalGroup(
                        new LiteralDecimal(0m),
                        new RoundDecimalGroup(
                            new MultiplyDecimalGroup([
                                new LiteralDecimal(100m),
                                new DivideDecimalGroup([
                                    new CountGroup(
                                        new AndRow([
                                            new EqualStringRow(
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
                                                new LiteralString("shipped")
                                            ),
                                            new EqualStringRow(
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
                                                new LiteralString("Ann")
                                            ),
                                        ])
                                    ),
                                    new CountGroup(
                                        new EqualStringRow(
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
                                            new LiteralString("Ann")
                                        )
                                    ),
                                ]),
                            ]),
                            new LiteralInteger(1)
                        ),
                        new EqualDecimalGroup(
                            new CountGroup(
                                new EqualStringRow(
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
                                    new LiteralString("Ann")
                                )
                            ),
                            new LiteralInteger(0)
                        )
                    )
                ),
                new SelectItemGroup(
                    "shipped_pct_cara",
                    new IfDecimalGroup(
                        new LiteralDecimal(0m),
                        new RoundDecimalGroup(
                            new MultiplyDecimalGroup([
                                new LiteralDecimal(100m),
                                new DivideDecimalGroup([
                                    new CountGroup(
                                        new AndRow([
                                            new EqualStringRow(
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
                                                new LiteralString("shipped")
                                            ),
                                            new EqualStringRow(
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
                                            ),
                                        ])
                                    ),
                                    new CountGroup(
                                        new EqualStringRow(
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
                                ]),
                            ]),
                            new LiteralInteger(1)
                        ),
                        new EqualDecimalGroup(
                            new CountGroup(
                                new EqualStringRow(
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
                            new LiteralInteger(0)
                        )
                    )
                ),
                new SelectItemGroup(
                    "shipped_pct_dan",
                    new IfDecimalGroup(
                        new LiteralDecimal(0m),
                        new RoundDecimalGroup(
                            new MultiplyDecimalGroup([
                                new LiteralDecimal(100m),
                                new DivideDecimalGroup([
                                    new CountGroup(
                                        new AndRow([
                                            new EqualStringRow(
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
                                                new LiteralString("shipped")
                                            ),
                                            new EqualStringRow(
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
                                                new LiteralString("Dan")
                                            ),
                                        ])
                                    ),
                                    new CountGroup(
                                        new EqualStringRow(
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
                                            new LiteralString("Dan")
                                        )
                                    ),
                                ]),
                            ]),
                            new LiteralInteger(1)
                        ),
                        new EqualDecimalGroup(
                            new CountGroup(
                                new EqualStringRow(
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
                                    new LiteralString("Dan")
                                )
                            ),
                            new LiteralInteger(0)
                        )
                    )
                ),
            ],
            orderBy: [new OrderItemGroup(new KeyUuid("order_tenant_id"))]
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
                    new OrderTenantIdColumn(),
                    new Column(new String("shipped_pct_ann"), new DoubleColumnType()),
                    new Column(new String("shipped_pct_cara"), new DoubleColumnType()),
                    new Column(new String("shipped_pct_dan"), new DoubleColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new OrderTenantIdColumn(),
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
                                    new String("shipped_pct_ann"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(50))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("shipped_pct_cara"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("shipped_pct_dan"),
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
                                new OrderTenantIdColumn(),
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
                                    new String("shipped_pct_ann"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("shipped_pct_cara"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(50))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("shipped_pct_dan"),
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
            ]
        );
}
