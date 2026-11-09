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
using Guid = Pure.Primitives.Guid.Guid;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Dashboards;

/// <summary>
/// Selects user_tenant_id, the count of rows as logins, the average of the seconds from
/// last_login to login_at divided by 3600 rounded to 1 digit as
/// avg_hours_after_last_login, the average of the nullable user_score or else 0, times
/// 10, rounded to 1 digit as avg_user_score_x10 and, unless the count of rows is 0, 100
/// times the count of rows with user_active divided by the count of rows rounded to 1
/// digit as active_share_pct from audit.logins inner joined with
/// schema_with_foreign_keys.users on login_user_id equal to user_id, grouped by
/// user_tenant_id, ordered by user_tenant_id.
/// </summary>
public sealed record LoginStatsPerTenantQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainGroupedQueryWithoutPagination(
            from: new FromEntity(
                new JoinedString(
                    new DotString(),
                    [new AuditRelationalSchema().Name, new LoginsTable().Name]
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
                                [new AuditRelationalSchema().Name, new LoginsTable().Name]
                            ).TextValue,
                            new LoginUserIdColumn().Name.TextValue
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
                    "user_tenant_id",
                    new FieldUuid(
                        new JoinedString(
                            new DotString(),
                            [
                                new RelationalSchemaWithForeignKeys().Name,
                                new UsersTable().Name,
                            ]
                        ).TextValue,
                        new UserTenantIdColumn().Name.TextValue
                    )
                ),
            ],
            select:
            [
                new SelectItemGroup("user_tenant_id", new KeyUuid("user_tenant_id")),
                new SelectItemGroup("logins", new CountGroup()),
                new SelectItemGroup(
                    "avg_hours_after_last_login",
                    new RoundDecimalGroup(
                        new AverageDecimalGroup(
                            new DivideDecimalRow([
                                new DatetimeDiffSecondsDecimalRow(
                                    new FieldDatetime(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new AuditRelationalSchema().Name,
                                                new LoginsTable().Name,
                                            ]
                                        ).TextValue,
                                        new LoginAtColumn().Name.TextValue
                                    ),
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
                                new LiteralDecimal(3600m),
                            ])
                        ),
                        new LiteralInteger(1)
                    )
                ),
                new SelectItemGroup(
                    "avg_user_score_x10",
                    new RoundDecimalGroup(
                        new MultiplyDecimalGroup([
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
                            new LiteralDecimal(10m),
                        ]),
                        new LiteralInteger(1)
                    )
                ),
                new SelectItemGroup(
                    "active_share_pct",
                    new IfDecimalGroup(
                        new LiteralDecimal(0m),
                        new RoundDecimalGroup(
                            new MultiplyDecimalGroup([
                                new LiteralDecimal(100m),
                                new DivideDecimalGroup([
                                    new CountGroup(
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
                                    ),
                                    new CountGroup(),
                                ]),
                            ]),
                            new LiteralInteger(1)
                        ),
                        new EqualDecimalGroup(new CountGroup(), new LiteralInteger(0))
                    )
                ),
            ],
            orderBy: [new OrderItemGroup(new KeyUuid("user_tenant_id"))]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and
    /// <see cref="AuditSchemaDataSet"/>, in this order.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new UserTenantIdColumn(),
                    new Column(new String("logins"), new LongColumnType()),
                    new Column(
                        new String("avg_hours_after_last_login"),
                        new DoubleColumnType()
                    ),
                    new Column(new String("avg_user_score_x10"), new DoubleColumnType()),
                    new Column(new String("active_share_pct"), new DoubleColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new UserTenantIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000385-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("logins"), new LongColumnType()),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("avg_hours_after_last_login"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(14.8))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("avg_user_score_x10"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(300))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("active_share_pct"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(66.7))
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
                                new UserTenantIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000386-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("logins"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("avg_hours_after_last_login"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-0.3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("avg_user_score_x10"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(100))
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
            ]
        );
}
