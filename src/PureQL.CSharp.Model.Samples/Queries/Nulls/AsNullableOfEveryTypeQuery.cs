using Pure.Collections.Generic;
using Pure.Primitives.Bool;
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
/// Selects from schema_with_foreign_keys.users the asNullable of user_id as id, of
/// user_name as name, of user_active as active, of signup_date as born, of shift_start as
/// call_time, of last_login as created, of the integer 1 as one, of the decimal 0.5 as
/// half, of the string guest as of_a_literal, of user_name joined with a space and Smith
/// as of_an_expression, of the integer 1 widened with asDecimal as of_a_widened_integer,
/// the asNullable of user_id not equal to a null uuid as is_never_null, the first of
/// user_name null if equal to the string Bob and the asNullable of user_name as
/// in_a_coalesce, and the next asNullable of user_name over user_id order with the
/// default string - as next_name.
/// </summary>
public sealed record AsNullableOfEveryTypeQuery
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
            select:
            [
                new SelectItemProjection(
                    "id",
                    new AsNullableUuidNullableProjection(
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
                    "name",
                    new AsNullableStringNullableProjection(
                        new FieldString(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue,
                            new UserNameColumn().Name.TextValue
                        )
                    )
                ),
                new SelectItemProjection(
                    "active",
                    new AsNullableBooleanNullableProjection(
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
                    "born",
                    new AsNullableDateNullableProjection(
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
                    "call_time",
                    new AsNullableTimeNullableProjection(
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
                    "created",
                    new AsNullableDatetimeNullableProjection(
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
                    "one",
                    new AsNullableIntegerNullableProjection(new LiteralInteger(1))
                ),
                new SelectItemProjection(
                    "half",
                    new AsNullableDecimalNullableProjection(new LiteralDecimal(0.5m))
                ),
                new SelectItemProjection(
                    "of_a_literal",
                    new AsNullableStringNullableProjection(new LiteralString("guest"))
                ),
                new SelectItemProjection(
                    "of_an_expression",
                    new AsNullableStringNullableProjection(
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
                            new LiteralString(" "),
                            new LiteralString("Smith"),
                        ])
                    )
                ),
                new SelectItemProjection(
                    "of_a_widened_integer",
                    new AsNullableDecimalNullableProjection(
                        new AsDecimalIntegerDecimalProjection(new LiteralInteger(1))
                    )
                ),
                new SelectItemProjection(
                    "is_never_null",
                    new NotEqualUuidProjection(
                        new AsNullableUuidNullableProjection(
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
                        new LiteralUuidNullable()
                    )
                ),
                new SelectItemProjection(
                    "in_a_coalesce",
                    new CoalesceStringNullableProjection([
                        new NullIfEqualStringNullableProjection(
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
                            new LiteralString("Bob")
                        ),
                        new AsNullableStringNullableProjection(
                            new FieldString(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new UsersTable().Name,
                                    ]
                                ).TextValue,
                                new UserNameColumn().Name.TextValue
                            )
                        ),
                    ])
                ),
                new SelectItemProjection(
                    "next_name",
                    new NextStringNullableProjection(
                        new AsNullableStringNullableRow(
                            new FieldString(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new UsersTable().Name,
                                    ]
                                ).TextValue,
                                new UserNameColumn().Name.TextValue
                            )
                        ),
                        new OrderedWindow([
                            new OrderItemRow(
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
                        ]),
                        @default: new LiteralString("-")
                    )
                ),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and
    /// <see cref="AuditSchemaDataSet"/>, in no particular order.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new IdColumn(),
                    new NameColumn(),
                    new Column(new String("active"), new BoolColumnType()),
                    new Column(new String("born"), new DateColumnType()),
                    new Column(new String("call_time"), new TimeColumnType()),
                    new Column(new String("created"), new DateTimeColumnType()),
                    new Column(new String("one"), new LongColumnType()),
                    new Column(new String("half"), new DoubleColumnType()),
                    new Column(new String("of_a_literal"), new StringColumnType()),
                    new Column(new String("of_an_expression"), new StringColumnType()),
                    new Column(
                        new String("of_a_widened_integer"),
                        new DoubleColumnType()
                    ),
                    new Column(new String("is_never_null"), new BoolColumnType()),
                    new Column(new String("in_a_coalesce"), new StringColumnType()),
                    new Column(new String("next_name"), new StringColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new IdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000001-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new NameColumn(),
                                new InvariantCell(new String("Ann"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("active"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("born"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(15),
                                        new UShort(1),
                                        new UShort(2020)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("call_time"), new TimeColumnType()),
                                new InvariantCell(
                                    new Time(new UShort(9), new UShort(0), new UShort(0))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("created"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(1),
                                            new UShort(6),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(8),
                                            new UShort(30),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("one"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("half"), new DoubleColumnType()),
                                new InvariantCell(new Double(0.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("guest"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_an_expression"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Ann Smith"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_widened_integer"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("is_never_null"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("in_a_coalesce"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Ann"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("next_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Bob"))
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
                                new IdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000002-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new NameColumn(),
                                new InvariantCell(new String("Bob"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("active"), new BoolColumnType()),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("born"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(20),
                                        new UShort(3),
                                        new UShort(2021)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("call_time"), new TimeColumnType()),
                                new InvariantCell(
                                    new Time(new UShort(10), new UShort(0), new UShort(0))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("created"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(2),
                                            new UShort(6),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(9),
                                            new UShort(15),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("one"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("half"), new DoubleColumnType()),
                                new InvariantCell(new Double(0.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("guest"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_an_expression"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Bob Smith"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_widened_integer"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("is_never_null"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("in_a_coalesce"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Bob"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("next_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Cara"))
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
                                new IdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000003-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new NameColumn(),
                                new InvariantCell(new String("Cara"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("active"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("born"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(10),
                                        new UShort(7),
                                        new UShort(2019)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("call_time"), new TimeColumnType()),
                                new InvariantCell(
                                    new Time(new UShort(9), new UShort(0), new UShort(0))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("created"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(30),
                                            new UShort(5),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(14),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("one"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("half"), new DoubleColumnType()),
                                new InvariantCell(new Double(0.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("guest"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_an_expression"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Cara Smith"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_widened_integer"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("is_never_null"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("in_a_coalesce"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Cara"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("next_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Dan"))
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
                                new IdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000004-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new NameColumn(),
                                new InvariantCell(new String("Dan"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("active"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("born"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(5),
                                        new UShort(11),
                                        new UShort(2022)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("call_time"), new TimeColumnType()),
                                new InvariantCell(
                                    new Time(
                                        new UShort(11),
                                        new UShort(30),
                                        new UShort(0)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("created"),
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
                                            new UShort(18),
                                            new UShort(45),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("one"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("half"), new DoubleColumnType()),
                                new InvariantCell(new Double(0.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("guest"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_an_expression"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Dan Smith"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_widened_integer"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("is_never_null"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("in_a_coalesce"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Dan"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("next_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Eve"))
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
                                new IdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000005-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new NameColumn(),
                                new InvariantCell(new String("Eve"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("active"), new BoolColumnType()),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("born"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(28),
                                        new UShort(2),
                                        new UShort(2023)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("call_time"), new TimeColumnType()),
                                new InvariantCell(
                                    new Time(new UShort(8), new UShort(0), new UShort(0))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("created"),
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
                                            new UShort(7),
                                            new UShort(5),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("one"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("half"), new DoubleColumnType()),
                                new InvariantCell(new Double(0.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("guest"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_an_expression"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Eve Smith"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_widened_integer"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("is_never_null"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("in_a_coalesce"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Eve"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("next_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Fay"))
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
                                new IdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000006-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new NameColumn(),
                                new InvariantCell(new String("Fay"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("active"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("born"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(15),
                                        new UShort(1),
                                        new UShort(2020)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("call_time"), new TimeColumnType()),
                                new InvariantCell(
                                    new Time(new UShort(9), new UShort(0), new UShort(0))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("created"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(1),
                                            new UShort(6),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(8),
                                            new UShort(30),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("one"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("half"), new DoubleColumnType()),
                                new InvariantCell(new Double(0.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("guest"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_an_expression"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Fay Smith"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_widened_integer"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("is_never_null"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("in_a_coalesce"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Fay"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("next_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("-"))
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
