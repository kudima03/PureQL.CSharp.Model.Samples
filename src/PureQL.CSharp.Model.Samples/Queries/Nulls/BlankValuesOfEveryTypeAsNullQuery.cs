using Pure.Collections.Generic;
using Pure.Primitives.Bool;
using Pure.Primitives.Date;
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
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.ProjectionExpressions;
using PureQL.CSharp.Model.RowExpressions;
using PureQL.CSharp.Model.Samples.Cells;
using Column = Pure.RelationalSchema.Column.Column;
using DateTime = Pure.Primitives.DateTime.DateTime;
using Double = Pure.Primitives.Number.Double;
using Guid = Pure.Primitives.Guid.Guid;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Nulls;

/// <summary>
/// Selects from schema_with_foreign_keys.users where nullIfEqual of user_name, or else
/// the empty string when user_score is not above 0, and the empty string is not null:
/// user_id; display_name, the nickname (a space, @, user_name and a space for an active
/// user, three spaces when user_score is above 5, else null) trimmed of spaces, read as
/// null when empty, or else user_name; phone, nullIfEqual of the mobile (+1- and
/// user_name when user_score is above 29, else empty) and the empty string, or else
/// nullIfEqual of the landline (+2- and user_name for an active user, else empty) and the
/// empty string; age, nullIfEqual of user_age and 25; opted_in, nullIfEqual of
/// user_active and false; trial_start, nullIfEqual of user_edge_date and 9999-12-31;
/// call_time, nullIfEqual of user_edge_time and midnight; returned_at, nullIfEqual of
/// last_login and 2024-06-01T08:30:00Z; referrer_id, nullIfEqual of user_tenant_id and
/// the uuid of tenant 385; and other_referrer_id, nullIfEqual of user_id, or else
/// user_tenant_id for an inactive user, and user_id.
/// </summary>
public sealed record BlankValuesOfEveryTypeAsNullQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithoutPagination(
            from: new FromEntity(
                new JoinedString(
                    new DotString(),
                    [new RelationalSchemaWithForeignKeys().Name, new UsersTable().Name]
                ).TextValue
            ),
            where: new NotEqualStringRow(
                new NullIfEqualStringNullableRow(
                    new IfStringRow(
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
                        new LiteralString(""),
                        new GreaterThanDecimalRow(
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
                            new LiteralInteger(0)
                        )
                    ),
                    new LiteralString("")
                ),
                new LiteralStringNullable()
            ),
            select:
            [
                new SelectItemProjection(
                    "user_id",
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
                new SelectItemProjection(
                    "display_name",
                    new CoalesceStringProjection([
                        new NullIfEqualStringNullableProjection(
                            new TrimStringNullableProjection(
                                new IfStringNullableProjection(
                                    new ConcatStringProjection([
                                        new LiteralString(" @"),
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
                                        new LiteralString(" "),
                                    ]),
                                    new IfStringNullableProjection(
                                        new LiteralString("   "),
                                        new LiteralStringNullable(),
                                        new GreaterThanDecimalProjection(
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
                                            new LiteralInteger(5)
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
                                    )
                                ),
                                [new LiteralString(" ")]
                            ),
                            new LiteralString("")
                        ),
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
                    ])
                ),
                new SelectItemProjection(
                    "phone",
                    new CoalesceStringNullableProjection([
                        new NullIfEqualStringNullableProjection(
                            new IfStringProjection(
                                new ConcatStringProjection([
                                    new LiteralString("+1-"),
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
                                ]),
                                new LiteralString(""),
                                new GreaterThanDecimalProjection(
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
                                    new LiteralInteger(29)
                                )
                            ),
                            new LiteralString("")
                        ),
                        new NullIfEqualStringNullableProjection(
                            new IfStringProjection(
                                new ConcatStringProjection([
                                    new LiteralString("+2-"),
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
                                ]),
                                new LiteralString(""),
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
                            new LiteralString("")
                        ),
                    ])
                ),
                new SelectItemProjection(
                    "age",
                    new NullIfEqualDecimalNullableProjection(
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
                        new LiteralInteger(25)
                    )
                ),
                new SelectItemProjection(
                    "opted_in",
                    new NullIfEqualBooleanNullableProjection(
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
                        new LiteralBoolean(false)
                    )
                ),
                new SelectItemProjection(
                    "trial_start",
                    new NullIfEqualDateNullableProjection(
                        new FieldDate(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue,
                            new UserEdgeDateColumn().Name.TextValue
                        ),
                        new LiteralDate(new DateOnly(9999, 12, 31))
                    )
                ),
                new SelectItemProjection(
                    "call_time",
                    new NullIfEqualTimeNullableProjection(
                        new FieldTime(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue,
                            new UserEdgeTimeColumn().Name.TextValue
                        ),
                        new LiteralTime(new TimeOnly(0, 0, 0))
                    )
                ),
                new SelectItemProjection(
                    "returned_at",
                    new NullIfEqualDatetimeNullableProjection(
                        new FieldDatetime(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue,
                            new LastLoginColumn().Name.TextValue
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 6, 1, 8, 30, 0, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemProjection(
                    "referrer_id",
                    new NullIfEqualUuidNullableProjection(
                        new FieldUuid(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue,
                            new UserTenantIdColumn().Name.TextValue
                        ),
                        new LiteralUuid(new System.Guid(901, 0, 0, new byte[8]))
                    )
                ),
                new SelectItemProjection(
                    "other_referrer_id",
                    new NullIfEqualUuidNullableProjection(
                        new IfUuidProjection(
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
                            new FieldUuid(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new UsersTable().Name,
                                    ]
                                ).TextValue,
                                new UserTenantIdColumn().Name.TextValue
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
                            )
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
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over <see
    /// cref="SchemaDataSetWithForeignKeys"/> and <see cref="AuditSchemaDataSet"/>, in no
    /// particular order.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new UserIdColumn(),
                    new Column(new String("display_name"), new StringColumnType()),
                    new Column(new String("phone"), new StringColumnType()),
                    new Column(new String("age"), new DoubleColumnType()),
                    new Column(new String("opted_in"), new BoolColumnType()),
                    new Column(new String("trial_start"), new DateColumnType()),
                    new Column(new String("call_time"), new TimeColumnType()),
                    new Column(new String("returned_at"), new DateTimeColumnType()),
                    new Column(new String("referrer_id"), new UuidColumnType()),
                    new Column(new String("other_referrer_id"), new UuidColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new UserIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000001-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("display_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("@Ann"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("phone"), new StringColumnType()),
                                new InvariantCell(new String("+1-Ann"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("age"), new DoubleColumnType()),
                                new InvariantCell(new Double(30))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("opted_in"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("trial_start"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(new Date(new DateOnly(2024, 2, 29)))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("call_time"), new TimeColumnType()),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("returned_at"),
                                    new DateTimeColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("referrer_id"),
                                    new UuidColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("other_referrer_id"),
                                    new UuidColumnType()
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
                                new UserIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000003-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("display_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("@Cara"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("phone"), new StringColumnType()),
                                new InvariantCell(new String("+1-Cara"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("age"), new DoubleColumnType()),
                                new InvariantCell(new Double(30))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("opted_in"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("trial_start"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(new Date(new DateOnly(2024, 3, 10)))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("call_time"), new TimeColumnType()),
                                new InvariantCell(new Time(new TimeOnly(2, 30, 0)))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("returned_at"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(new DateOnly(2024, 5, 30)),
                                        new Time(new TimeOnly(14, 0, 0))
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("referrer_id"),
                                    new UuidColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("other_referrer_id"),
                                    new UuidColumnType()
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
                                new UserIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000005-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("display_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Eve"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("phone"), new StringColumnType()),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("age"), new DoubleColumnType()),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("opted_in"), new BoolColumnType()),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("trial_start"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(new Date(new DateOnly(1, 1, 1)))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("call_time"), new TimeColumnType()),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("returned_at"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(new DateOnly(2024, 6, 4)),
                                        new Time(new TimeOnly(7, 5, 0))
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("referrer_id"),
                                    new UuidColumnType()
                                ),
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
                                    new String("other_referrer_id"),
                                    new UuidColumnType()
                                ),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000386-0000-0000-0000-000000000000"
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
                                new UserIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000006-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("display_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("@Fay"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("phone"), new StringColumnType()),
                                new InvariantCell(new String("+2-Fay"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("age"), new DoubleColumnType()),
                                new InvariantCell(new Double(28))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("opted_in"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("trial_start"),
                                    new DateColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("call_time"), new TimeColumnType()),
                                new InvariantCell(new Time(new TimeOnly(23, 59, 59)))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("returned_at"),
                                    new DateTimeColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("referrer_id"),
                                    new UuidColumnType()
                                ),
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
                                    new String("other_referrer_id"),
                                    new UuidColumnType()
                                ),
                                new EmptyCell()
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
