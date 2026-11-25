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
using Double = Pure.Primitives.Number.Double;
using Guid = Pure.Primitives.Guid.Guid;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Nulls;

/// <summary>
/// Selects from schema_with_foreign_keys.users left joined on user_id to an inline
/// subquery nicknames (a nickname and a number of letters for four users): user_id, power
/// of 1.1 to the nullable user_score, user_age divided by 7 rounded to letters digits,
/// "*" repeated letters times, user_name with the nickname replaced by the empty string,
/// the position of the nickname in user_name, the first letters of user_name, and
/// user_name padded with "." to letters plus 4, each nullable through an operand named by
/// its role, ordered by user_id.
/// </summary>
public sealed record LiftedOverEveryOperandQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithoutPagination(
            subqueries:
            [
                new SubqueryRows(
                    "nicknames",
                    [
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new System.Guid(1, 0, 0, new byte[8]))
                            ),
                            new RowItem("nickname", new LiteralString("An")),
                            new RowItem("letters", new LiteralInteger(2)),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new System.Guid(3, 0, 0, new byte[8]))
                            ),
                            new RowItem("nickname", new LiteralString("ra")),
                            new RowItem("letters", new LiteralInteger(3)),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new System.Guid(4, 0, 0, new byte[8]))
                            ),
                            new RowItem("nickname", new LiteralString("Da")),
                            new RowItem("letters", new LiteralInteger(1)),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new System.Guid(6, 0, 0, new byte[8]))
                            ),
                            new RowItem("nickname", new LiteralString("ay")),
                            new RowItem("letters", new LiteralInteger(2)),
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
                    "nicknames",
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
                        new FieldUuid("nicknames", "user_id")
                    )
                ),
            ],
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
                    "growth",
                    new PowerDecimalNullableProjection(
                        new LiteralDecimal(1.1m),
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
                new SelectItemProjection(
                    "age_in_weeks_rounded",
                    new RoundDecimalNullableProjection(
                        new DivideDecimalProjection([
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
                            new LiteralInteger(7),
                        ]),
                        new FieldIntegerNullable("nicknames", "letters")
                    )
                ),
                new SelectItemProjection(
                    "stars",
                    new RepeatStringNullableProjection(
                        new LiteralString("*"),
                        new FieldIntegerNullable("nicknames", "letters")
                    )
                ),
                new SelectItemProjection(
                    "name_without_nickname",
                    new ReplaceStringNullableProjection(
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
                        new FieldStringNullable("nicknames", "nickname"),
                        new LiteralString("")
                    )
                ),
                new SelectItemProjection(
                    "nickname_starts_at",
                    new PositionIntegerNullableProjection(
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
                        new FieldStringNullable("nicknames", "nickname")
                    )
                ),
                new SelectItemProjection(
                    "first_letters",
                    new SubstringStringNullableProjection(
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
                        new LiteralInteger(1),
                        new FieldIntegerNullable("nicknames", "letters")
                    )
                ),
                new SelectItemProjection(
                    "padded_name",
                    new PadEndStringNullableProjection(
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
                        new AddIntegerNullableProjection([
                            new FieldIntegerNullable("nicknames", "letters"),
                            new LiteralInteger(4),
                        ]),
                        new LiteralString(".")
                    )
                ),
            ],
            orderBy:
            [
                new OrderItemProjectionExpression(
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
            ]
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
                    new UserIdColumn(),
                    new Column(new String("growth"), new DoubleColumnType()),
                    new Column(
                        new String("age_in_weeks_rounded"),
                        new DoubleColumnType()
                    ),
                    new Column(new String("stars"), new StringColumnType()),
                    new Column(
                        new String("name_without_nickname"),
                        new StringColumnType()
                    ),
                    new Column(new String("nickname_starts_at"), new LongColumnType()),
                    new Column(new String("first_letters"), new StringColumnType()),
                    new Column(new String("padded_name"), new StringColumnType()),
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
                                new Column(new String("growth"), new DoubleColumnType()),
                                new InvariantCell(new Double(17.449402268886406))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("age_in_weeks_rounded"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(4.29))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("stars"), new StringColumnType()),
                                new InvariantCell(new String("**"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("name_without_nickname"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("n"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nickname_starts_at"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_letters"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("An"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("padded_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Ann..."))
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
                                            "00000002-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("growth"), new DoubleColumnType()),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("age_in_weeks_rounded"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("stars"), new StringColumnType()),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("name_without_nickname"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nickname_starts_at"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_letters"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("padded_name"),
                                    new StringColumnType()
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
                                new Column(new String("growth"), new DoubleColumnType()),
                                new InvariantCell(new Double(17.449402268886406))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("age_in_weeks_rounded"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(4.286))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("stars"), new StringColumnType()),
                                new InvariantCell(new String("***"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("name_without_nickname"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Ca"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nickname_starts_at"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_letters"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Car"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("padded_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Cara..."))
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
                                            "00000004-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("growth"), new DoubleColumnType()),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("age_in_weeks_rounded"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(6))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("stars"), new StringColumnType()),
                                new InvariantCell(new String("*"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("name_without_nickname"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("n"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nickname_starts_at"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_letters"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("D"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("padded_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Dan.."))
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
                                new Column(new String("growth"), new DoubleColumnType()),
                                new InvariantCell(new Double(2.5937424601))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("age_in_weeks_rounded"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("stars"), new StringColumnType()),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("name_without_nickname"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nickname_starts_at"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_letters"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("padded_name"),
                                    new StringColumnType()
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
                                            "00000006-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("growth"), new DoubleColumnType()),
                                new InvariantCell(new Double(14.420993610649923))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("age_in_weeks_rounded"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(4))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("stars"), new StringColumnType()),
                                new InvariantCell(new String("**"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("name_without_nickname"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("F"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nickname_starts_at"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_letters"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Fa"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("padded_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Fay..."))
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
