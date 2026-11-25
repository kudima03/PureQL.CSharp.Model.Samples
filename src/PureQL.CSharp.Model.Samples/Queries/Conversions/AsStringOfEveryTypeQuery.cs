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
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.ProjectionExpressions;
using PureQL.CSharp.Model.RowExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Conversions;

/// <summary>
/// Selects from schema_with_foreign_keys.users left joined on user_id to an inline
/// subquery profiles of verified, trial_start, last_seen and referrer_id for four users,
/// each with a null cell somewhere: asString of floor of user_score, of -42, of 19.9, of
/// 1 divided by 8, of user_active, of the nullable verified, of signup_date, of the
/// nullable trial_start, of shift_start, of last_login, of the nullable last_seen, of
/// user_id, of the nullable referrer_id, of the datetime 2024-06-06T15:00:00Z, of asDate
/// of last_login and of asTime of last_seen, the length of asString of floor of
/// user_score, the first 8 characters of asString of user_id, and user_name followed by "
/// (", asString of floor of user_score or "?" through coalesce, and ")".
/// </summary>
public sealed record AsStringOfEveryTypeQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithoutPagination(
            subqueries:
            [
                new SubqueryRows(
                    "profiles",
                    [
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(1, 0, 0, new byte[8]))
                            ),
                            new RowItem("verified", new LiteralBoolean(true)),
                            new RowItem(
                                "trial_start",
                                new LiteralDate(new DateOnly(2024, 1, 15))
                            ),
                            new RowItem(
                                "last_seen",
                                new LiteralDatetime(
                                    new DateTimeOffset(
                                        2024,
                                        6,
                                        3,
                                        10,
                                        20,
                                        30,
                                        TimeSpan.Zero
                                    )
                                )
                            ),
                            new RowItem(
                                "referrer_id",
                                new LiteralUuid(new Guid(2, 0, 0, new byte[8]))
                            ),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(2, 0, 0, new byte[8]))
                            ),
                            new RowItem("verified", new LiteralBoolean(false)),
                            new RowItem("trial_start", new LiteralDateNullable()),
                            new RowItem(
                                "last_seen",
                                new LiteralDatetime(
                                    new DateTimeOffset(
                                        2024,
                                        5,
                                        31,
                                        23,
                                        59,
                                        59,
                                        TimeSpan.Zero
                                    )
                                )
                            ),
                            new RowItem("referrer_id", new LiteralUuidNullable()),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(3, 0, 0, new byte[8]))
                            ),
                            new RowItem("verified", new LiteralBooleanNullable()),
                            new RowItem(
                                "trial_start",
                                new LiteralDate(new DateOnly(2024, 2, 29))
                            ),
                            new RowItem("last_seen", new LiteralDatetimeNullable()),
                            new RowItem(
                                "referrer_id",
                                new LiteralUuid(new Guid(1, 0, 0, new byte[8]))
                            ),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(4, 0, 0, new byte[8]))
                            ),
                            new RowItem("verified", new LiteralBoolean(true)),
                            new RowItem(
                                "trial_start",
                                new LiteralDate(new DateOnly(2024, 7, 1))
                            ),
                            new RowItem(
                                "last_seen",
                                new LiteralDatetime(
                                    new DateTimeOffset(2024, 6, 4, 0, 0, 0, TimeSpan.Zero)
                                )
                            ),
                            new RowItem("referrer_id", new LiteralUuidNullable()),
                        ],
                    ]
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
                    "profiles",
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
                        new FieldUuid("profiles", "user_id")
                    )
                ),
            ],
            select:
            [
                new SelectItemProjection(
                    "integer",
                    new AsStringDecimalStringNullableProjection(
                        new FloorIntegerNullableProjection(
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
                        )
                    )
                ),
                new SelectItemProjection(
                    "integer_literal",
                    new AsStringDecimalStringProjection(new LiteralInteger(-42))
                ),
                new SelectItemProjection(
                    "decimal_literal",
                    new AsStringDecimalStringProjection(new LiteralDecimal(19.9m))
                ),
                new SelectItemProjection(
                    "decimal_computed",
                    new AsStringDecimalStringProjection(
                        new DivideDecimalProjection([
                            new LiteralInteger(1),
                            new LiteralInteger(8),
                        ])
                    )
                ),
                new SelectItemProjection(
                    "boolean",
                    new AsStringBooleanStringProjection(
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
                ),
                new SelectItemProjection(
                    "boolean_nullable",
                    new AsStringBooleanStringNullableProjection(
                        new FieldBooleanNullable("profiles", "verified")
                    )
                ),
                new SelectItemProjection(
                    "date",
                    new AsStringDateStringProjection(
                        new FieldDate(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue,
                            new SignupDateColumn().Name.TextValue
                        )
                    )
                ),
                new SelectItemProjection(
                    "date_nullable",
                    new AsStringDateStringNullableProjection(
                        new FieldDateNullable("profiles", "trial_start")
                    )
                ),
                new SelectItemProjection(
                    "time",
                    new AsStringTimeStringProjection(
                        new FieldTime(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue,
                            new ShiftStartColumn().Name.TextValue
                        )
                    )
                ),
                new SelectItemProjection(
                    "datetime",
                    new AsStringDatetimeStringProjection(
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
                    )
                ),
                new SelectItemProjection(
                    "datetime_nullable",
                    new AsStringDatetimeStringNullableProjection(
                        new FieldDatetimeNullable("profiles", "last_seen")
                    )
                ),
                new SelectItemProjection(
                    "uuid",
                    new AsStringUuidStringProjection(
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
                new SelectItemProjection(
                    "uuid_nullable",
                    new AsStringUuidStringNullableProjection(
                        new FieldUuidNullable("profiles", "referrer_id")
                    )
                ),
                new SelectItemProjection(
                    "parameter",
                    new AsStringDatetimeStringProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 6, 6, 15, 0, 0, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemProjection(
                    "date_of_a_datetime",
                    new AsStringDateStringProjection(
                        new AsDateDatetimeDateProjection(
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
                        )
                    )
                ),
                new SelectItemProjection(
                    "time_of_a_datetime",
                    new AsStringTimeStringNullableProjection(
                        new AsTimeDatetimeTimeNullableProjection(
                            new FieldDatetimeNullable("profiles", "last_seen")
                        )
                    )
                ),
                new SelectItemProjection(
                    "length_of_an_integer",
                    new LengthIntegerNullableProjection(
                        new AsStringDecimalStringNullableProjection(
                            new FloorIntegerNullableProjection(
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
                            )
                        )
                    )
                ),
                new SelectItemProjection(
                    "short_id",
                    new SubstringStringProjection(
                        new AsStringUuidStringProjection(
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
                        new LiteralInteger(1),
                        length: new LiteralInteger(8)
                    )
                ),
                new SelectItemProjection(
                    "name_and_age",
                    new ConcatStringProjection([
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
                        new LiteralString(" ("),
                        new CoalesceStringProjection([
                            new AsStringDecimalStringNullableProjection(
                                new FloorIntegerNullableProjection(
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
                                )
                            ),
                            new LiteralString("?"),
                        ]),
                        new LiteralString(")"),
                    ])
                ),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and
    /// <see cref="AuditSchemaDataSet"/>, in no particular order.
    /// The text each type gives is the specification's one spelling of that type.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("integer"), new StringColumnType()),
                    new Column(new String("integer_literal"), new StringColumnType()),
                    new Column(new String("decimal_literal"), new StringColumnType()),
                    new Column(new String("decimal_computed"), new StringColumnType()),
                    new Column(new String("boolean"), new StringColumnType()),
                    new Column(new String("boolean_nullable"), new StringColumnType()),
                    new Column(new String("date"), new StringColumnType()),
                    new Column(new String("date_nullable"), new StringColumnType()),
                    new Column(new String("time"), new StringColumnType()),
                    new Column(new String("datetime"), new StringColumnType()),
                    new Column(new String("datetime_nullable"), new StringColumnType()),
                    new Column(new String("uuid"), new StringColumnType()),
                    new Column(new String("uuid_nullable"), new StringColumnType()),
                    new Column(new String("parameter"), new StringColumnType()),
                    new Column(new String("date_of_a_datetime"), new StringColumnType()),
                    new Column(new String("time_of_a_datetime"), new StringColumnType()),
                    new Column(new String("length_of_an_integer"), new LongColumnType()),
                    new Column(new String("short_id"), new StringColumnType()),
                    new Column(new String("name_and_age"), new StringColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("integer"), new StringColumnType()),
                                new InvariantCell(new String("30"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("integer_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("-42"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("19.9"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_computed"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("0.125"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("boolean"), new StringColumnType()),
                                new InvariantCell(new String("true"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("boolean_nullable"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("true"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("date"), new StringColumnType()),
                                new InvariantCell(new String("2020-01-15"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("date_nullable"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("2024-01-15"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("time"), new StringColumnType()),
                                new InvariantCell(new String("09:00:00"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("datetime"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("2024-06-01T08:30:00Z"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("datetime_nullable"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("2024-06-03T10:20:30Z"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("uuid"), new StringColumnType()),
                                new InvariantCell(
                                    new String("00000001-0000-0000-0000-000000000000")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("uuid_nullable"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(
                                    new String("00000002-0000-0000-0000-000000000000")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("parameter"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("2024-06-06T15:00:00Z"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("date_of_a_datetime"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("2024-06-01"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("time_of_a_datetime"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("10:20:30"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("length_of_an_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("short_id"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("00000001"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("name_and_age"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Ann (30)"))
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
                                new Column(new String("integer"), new StringColumnType()),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("integer_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("-42"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("19.9"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_computed"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("0.125"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("boolean"), new StringColumnType()),
                                new InvariantCell(new String("false"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("boolean_nullable"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("false"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("date"), new StringColumnType()),
                                new InvariantCell(new String("2021-03-20"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("date_nullable"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("time"), new StringColumnType()),
                                new InvariantCell(new String("10:00:00"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("datetime"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("2024-06-02T09:15:00Z"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("datetime_nullable"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("2024-05-31T23:59:59Z"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("uuid"), new StringColumnType()),
                                new InvariantCell(
                                    new String("00000002-0000-0000-0000-000000000000")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("uuid_nullable"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("parameter"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("2024-06-06T15:00:00Z"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("date_of_a_datetime"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("2024-06-02"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("time_of_a_datetime"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("23:59:59"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("length_of_an_integer"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("short_id"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("00000002"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("name_and_age"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Bob (?)"))
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
                                new Column(new String("integer"), new StringColumnType()),
                                new InvariantCell(new String("30"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("integer_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("-42"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("19.9"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_computed"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("0.125"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("boolean"), new StringColumnType()),
                                new InvariantCell(new String("true"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("boolean_nullable"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("date"), new StringColumnType()),
                                new InvariantCell(new String("2019-07-10"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("date_nullable"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("2024-02-29"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("time"), new StringColumnType()),
                                new InvariantCell(new String("09:00:00"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("datetime"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("2024-05-30T14:00:00Z"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("datetime_nullable"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("uuid"), new StringColumnType()),
                                new InvariantCell(
                                    new String("00000003-0000-0000-0000-000000000000")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("uuid_nullable"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(
                                    new String("00000001-0000-0000-0000-000000000000")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("parameter"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("2024-06-06T15:00:00Z"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("date_of_a_datetime"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("2024-05-30"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("time_of_a_datetime"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("length_of_an_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("short_id"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("00000003"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("name_and_age"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Cara (30)"))
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
                                new Column(new String("integer"), new StringColumnType()),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("integer_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("-42"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("19.9"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_computed"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("0.125"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("boolean"), new StringColumnType()),
                                new InvariantCell(new String("true"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("boolean_nullable"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("true"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("date"), new StringColumnType()),
                                new InvariantCell(new String("2022-11-05"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("date_nullable"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("2024-07-01"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("time"), new StringColumnType()),
                                new InvariantCell(new String("11:30:00"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("datetime"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("2024-06-03T18:45:00Z"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("datetime_nullable"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("2024-06-04T00:00:00Z"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("uuid"), new StringColumnType()),
                                new InvariantCell(
                                    new String("00000004-0000-0000-0000-000000000000")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("uuid_nullable"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("parameter"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("2024-06-06T15:00:00Z"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("date_of_a_datetime"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("2024-06-03"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("time_of_a_datetime"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("00:00:00"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("length_of_an_integer"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("short_id"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("00000004"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("name_and_age"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Dan (?)"))
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
                                new Column(new String("integer"), new StringColumnType()),
                                new InvariantCell(new String("10"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("integer_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("-42"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("19.9"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_computed"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("0.125"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("boolean"), new StringColumnType()),
                                new InvariantCell(new String("false"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("boolean_nullable"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("date"), new StringColumnType()),
                                new InvariantCell(new String("2023-02-28"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("date_nullable"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("time"), new StringColumnType()),
                                new InvariantCell(new String("08:00:00"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("datetime"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("2024-06-04T07:05:00Z"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("datetime_nullable"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("uuid"), new StringColumnType()),
                                new InvariantCell(
                                    new String("00000005-0000-0000-0000-000000000000")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("uuid_nullable"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("parameter"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("2024-06-06T15:00:00Z"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("date_of_a_datetime"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("2024-06-04"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("time_of_a_datetime"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("length_of_an_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("short_id"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("00000005"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("name_and_age"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Eve (10)"))
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
                                new Column(new String("integer"), new StringColumnType()),
                                new InvariantCell(new String("28"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("integer_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("-42"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("19.9"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_computed"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("0.125"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("boolean"), new StringColumnType()),
                                new InvariantCell(new String("true"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("boolean_nullable"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("date"), new StringColumnType()),
                                new InvariantCell(new String("2020-01-15"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("date_nullable"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("time"), new StringColumnType()),
                                new InvariantCell(new String("09:00:00"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("datetime"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("2024-06-01T08:30:00Z"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("datetime_nullable"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("uuid"), new StringColumnType()),
                                new InvariantCell(
                                    new String("00000006-0000-0000-0000-000000000000")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("uuid_nullable"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("parameter"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("2024-06-06T15:00:00Z"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("date_of_a_datetime"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("2024-06-01"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("time_of_a_datetime"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("length_of_an_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("short_id"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("00000006"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("name_and_age"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Fay (28)"))
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
