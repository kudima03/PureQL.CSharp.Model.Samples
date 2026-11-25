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

namespace PureQL.CSharp.Model.Samples.Queries.Conversions;

/// <summary>
/// Selects from schema_with_foreign_keys.orders where placed_at is at least asDatetime of
/// the text "2024-06-04T00:00:00Z": asInteger, asDecimal, asBoolean, asDate, asTime,
/// asDatetime and asUuid of a text literal of each type, tryAsInteger through tryAsUuid
/// of the same texts, the same seven operators over a nullable string, which is
/// asNullable of "7", "false", "2024-02-29" and "2024-01-31T23:30:00Z" for integer,
/// boolean, date and datetime and a typed null for decimal, time and uuid, as and tryAs,
/// whether asDate of asString of placed_on is placed_on, whether asString of tryAsDecimal
/// of "19.9" is "19.9", asDate of the text built from asString of year of placed_on and
/// "-01-01", tryAsInteger of the first 3 characters of order_status and of the first 2 of
/// asString of order_total, asDate of asDatetime of a text, asDate of placed_at and
/// asDate of a text.
/// </summary>
public sealed record ReadingTextOfEveryTypeQuery
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
            where: new GreaterThanOrEqualDatetimeRow(
                new FieldDatetime(
                    new JoinedString(
                        new DotString(),
                        [
                            new RelationalSchemaWithForeignKeys().Name,
                            new OrdersTable().Name,
                        ]
                    ).TextValue,
                    new PlacedAtColumn().Name.TextValue
                ),
                new AsDatetimeStringDatetimeRow(new LiteralString("2024-06-04T00:00:00Z"))
            ),
            select:
            [
                new SelectItemProjection(
                    "integer_parsed",
                    new AsIntegerIntegerProjection(new LiteralString("42"))
                ),
                new SelectItemProjection(
                    "decimal_parsed",
                    new AsDecimalStringDecimalProjection(new LiteralString("19.9"))
                ),
                new SelectItemProjection(
                    "boolean_parsed",
                    new AsBooleanBooleanProjection(new LiteralString("true"))
                ),
                new SelectItemProjection(
                    "date_parsed",
                    new AsDateStringDateProjection(new LiteralString("2024-06-15"))
                ),
                new SelectItemProjection(
                    "time_parsed",
                    new AsTimeStringTimeProjection(new LiteralString("13:45:30"))
                ),
                new SelectItemProjection(
                    "datetime_parsed",
                    new AsDatetimeStringDatetimeProjection(
                        new LiteralString("2024-06-15T13:45:30Z")
                    )
                ),
                new SelectItemProjection(
                    "uuid_parsed",
                    new AsUuidUuidProjection(
                        new LiteralString("0000007b-0000-0000-0000-000000000000")
                    )
                ),
                new SelectItemProjection(
                    "integer_or_null",
                    new TryAsIntegerProjection(new LiteralString("42"))
                ),
                new SelectItemProjection(
                    "decimal_or_null",
                    new TryAsDecimalProjection(new LiteralString("19.9"))
                ),
                new SelectItemProjection(
                    "boolean_or_null",
                    new TryAsBooleanProjection(new LiteralString("true"))
                ),
                new SelectItemProjection(
                    "date_or_null",
                    new TryAsDateProjection(new LiteralString("2024-06-15"))
                ),
                new SelectItemProjection(
                    "time_or_null",
                    new TryAsTimeProjection(new LiteralString("13:45:30"))
                ),
                new SelectItemProjection(
                    "datetime_or_null",
                    new TryAsDatetimeProjection(new LiteralString("2024-06-15T13:45:30Z"))
                ),
                new SelectItemProjection(
                    "uuid_or_null",
                    new TryAsUuidProjection(
                        new LiteralString("0000007b-0000-0000-0000-000000000000")
                    )
                ),
                new SelectItemProjection(
                    "integer_lifted",
                    new AsIntegerIntegerNullableProjection(
                        new AsNullableStringNullableProjection(new LiteralString("7"))
                    )
                ),
                new SelectItemProjection(
                    "decimal_lifted",
                    new AsDecimalStringDecimalNullableProjection(
                        new LiteralStringNullable()
                    )
                ),
                new SelectItemProjection(
                    "boolean_lifted",
                    new AsBooleanBooleanNullableProjection(
                        new AsNullableStringNullableProjection(new LiteralString("false"))
                    )
                ),
                new SelectItemProjection(
                    "date_lifted",
                    new AsDateStringDateNullableProjection(
                        new AsNullableStringNullableProjection(
                            new LiteralString("2024-02-29")
                        )
                    )
                ),
                new SelectItemProjection(
                    "time_lifted",
                    new AsTimeStringTimeNullableProjection(new LiteralStringNullable())
                ),
                new SelectItemProjection(
                    "datetime_lifted",
                    new AsDatetimeStringDatetimeNullableProjection(
                        new AsNullableStringNullableProjection(
                            new LiteralString("2024-01-31T23:30:00Z")
                        )
                    )
                ),
                new SelectItemProjection(
                    "uuid_lifted",
                    new AsUuidUuidNullableProjection(new LiteralStringNullable())
                ),
                new SelectItemProjection(
                    "integer_of_nullable_or_null",
                    new TryAsIntegerProjection(
                        new AsNullableStringNullableProjection(new LiteralString("7"))
                    )
                ),
                new SelectItemProjection(
                    "decimal_of_nullable_or_null",
                    new TryAsDecimalProjection(new LiteralStringNullable())
                ),
                new SelectItemProjection(
                    "boolean_of_nullable_or_null",
                    new TryAsBooleanProjection(
                        new AsNullableStringNullableProjection(new LiteralString("false"))
                    )
                ),
                new SelectItemProjection(
                    "date_of_nullable_or_null",
                    new TryAsDateProjection(
                        new AsNullableStringNullableProjection(
                            new LiteralString("2024-02-29")
                        )
                    )
                ),
                new SelectItemProjection(
                    "time_of_nullable_or_null",
                    new TryAsTimeProjection(new LiteralStringNullable())
                ),
                new SelectItemProjection(
                    "datetime_of_nullable_or_null",
                    new TryAsDatetimeProjection(
                        new AsNullableStringNullableProjection(
                            new LiteralString("2024-01-31T23:30:00Z")
                        )
                    )
                ),
                new SelectItemProjection(
                    "uuid_of_nullable_or_null",
                    new TryAsUuidProjection(new LiteralStringNullable())
                ),
                new SelectItemProjection(
                    "round_trip",
                    new EqualDateProjection(
                        new AsDateStringDateProjection(
                            new AsStringDateStringProjection(
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
                            )
                        ),
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
                    )
                ),
                new SelectItemProjection(
                    "same_text",
                    new EqualStringProjection(
                        new AsStringDecimalStringNullableProjection(
                            new TryAsDecimalProjection(new LiteralString("19.9"))
                        ),
                        new LiteralString("19.9")
                    )
                ),
                new SelectItemProjection(
                    "first_of_year",
                    new AsDateStringDateProjection(
                        new ConcatStringProjection([
                            new AsStringDecimalStringProjection(
                                new YearDateIntegerProjection(
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
                                )
                            ),
                            new LiteralString("-01-01"),
                        ])
                    )
                ),
                new SelectItemProjection(
                    "status_code",
                    new TryAsIntegerProjection(
                        new SubstringStringProjection(
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
                            new LiteralInteger(1),
                            length: new LiteralInteger(3)
                        )
                    )
                ),
                new SelectItemProjection(
                    "total_prefix",
                    new TryAsIntegerProjection(
                        new SubstringStringProjection(
                            new AsStringDecimalStringProjection(
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
                            new LiteralInteger(1),
                            length: new LiteralInteger(2)
                        )
                    )
                ),
                new SelectItemProjection(
                    "day_of_a_datetime_text",
                    new AsDateDatetimeDateProjection(
                        new AsDatetimeStringDatetimeProjection(
                            new LiteralString("2024-06-15T13:45:30Z")
                        )
                    )
                ),
                new SelectItemProjection(
                    "day_of_a_datetime",
                    new AsDateDatetimeDateProjection(
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
                    )
                ),
                new SelectItemProjection(
                    "day_of_a_date_text",
                    new AsDateStringDateProjection(new LiteralString("2024-06-15"))
                ),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and
    /// <see cref="AuditSchemaDataSet"/>, in no particular order.
    /// Each operator reads exactly the text asString writes for its type.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("integer_parsed"), new LongColumnType()),
                    new Column(new String("decimal_parsed"), new DoubleColumnType()),
                    new Column(new String("boolean_parsed"), new BoolColumnType()),
                    new Column(new String("date_parsed"), new DateColumnType()),
                    new Column(new String("time_parsed"), new TimeColumnType()),
                    new Column(new String("datetime_parsed"), new DateTimeColumnType()),
                    new Column(new String("uuid_parsed"), new UuidColumnType()),
                    new Column(new String("integer_or_null"), new LongColumnType()),
                    new Column(new String("decimal_or_null"), new DoubleColumnType()),
                    new Column(new String("boolean_or_null"), new BoolColumnType()),
                    new Column(new String("date_or_null"), new DateColumnType()),
                    new Column(new String("time_or_null"), new TimeColumnType()),
                    new Column(new String("datetime_or_null"), new DateTimeColumnType()),
                    new Column(new String("uuid_or_null"), new UuidColumnType()),
                    new Column(new String("integer_lifted"), new LongColumnType()),
                    new Column(new String("decimal_lifted"), new DoubleColumnType()),
                    new Column(new String("boolean_lifted"), new BoolColumnType()),
                    new Column(new String("date_lifted"), new DateColumnType()),
                    new Column(new String("time_lifted"), new TimeColumnType()),
                    new Column(new String("datetime_lifted"), new DateTimeColumnType()),
                    new Column(new String("uuid_lifted"), new UuidColumnType()),
                    new Column(
                        new String("integer_of_nullable_or_null"),
                        new LongColumnType()
                    ),
                    new Column(
                        new String("decimal_of_nullable_or_null"),
                        new DoubleColumnType()
                    ),
                    new Column(
                        new String("boolean_of_nullable_or_null"),
                        new BoolColumnType()
                    ),
                    new Column(
                        new String("date_of_nullable_or_null"),
                        new DateColumnType()
                    ),
                    new Column(
                        new String("time_of_nullable_or_null"),
                        new TimeColumnType()
                    ),
                    new Column(
                        new String("datetime_of_nullable_or_null"),
                        new DateTimeColumnType()
                    ),
                    new Column(
                        new String("uuid_of_nullable_or_null"),
                        new UuidColumnType()
                    ),
                    new Column(new String("round_trip"), new BoolColumnType()),
                    new Column(new String("same_text"), new BoolColumnType()),
                    new Column(new String("first_of_year"), new DateColumnType()),
                    new Column(new String("status_code"), new LongColumnType()),
                    new Column(new String("total_prefix"), new LongColumnType()),
                    new Column(
                        new String("day_of_a_datetime_text"),
                        new DateColumnType()
                    ),
                    new Column(new String("day_of_a_datetime"), new DateColumnType()),
                    new Column(new String("day_of_a_date_text"), new DateColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("integer_parsed"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(42))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_parsed"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(19.9))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("boolean_parsed"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("date_parsed"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(15),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("time_parsed"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(
                                    new Time(
                                        new UShort(13),
                                        new UShort(45),
                                        new UShort(30)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("datetime_parsed"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(15),
                                            new UShort(6),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(13),
                                            new UShort(45),
                                            new UShort(30)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("uuid_parsed"),
                                    new UuidColumnType()
                                ),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "0000007b-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("integer_or_null"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(42))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_or_null"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(19.9))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("boolean_or_null"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("date_or_null"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(15),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("time_or_null"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(
                                    new Time(
                                        new UShort(13),
                                        new UShort(45),
                                        new UShort(30)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("datetime_or_null"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(15),
                                            new UShort(6),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(13),
                                            new UShort(45),
                                            new UShort(30)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("uuid_or_null"),
                                    new UuidColumnType()
                                ),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "0000007b-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("integer_lifted"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(7))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_lifted"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("boolean_lifted"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("date_lifted"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(29),
                                        new UShort(2),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("time_lifted"),
                                    new TimeColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("datetime_lifted"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(31),
                                            new UShort(1),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(23),
                                            new UShort(30),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("uuid_lifted"),
                                    new UuidColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("integer_of_nullable_or_null"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(7))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_of_nullable_or_null"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("boolean_of_nullable_or_null"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("date_of_nullable_or_null"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(29),
                                        new UShort(2),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("time_of_nullable_or_null"),
                                    new TimeColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("datetime_of_nullable_or_null"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(31),
                                            new UShort(1),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(23),
                                            new UShort(30),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("uuid_of_nullable_or_null"),
                                    new UuidColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("round_trip"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("same_text"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_of_year"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(1),
                                        new UShort(1),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("status_code"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("total_prefix"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(75))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("day_of_a_datetime_text"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(15),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("day_of_a_datetime"),
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
                                new Column(
                                    new String("day_of_a_date_text"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(15),
                                        new UShort(6),
                                        new UShort(2024)
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
                                new Column(
                                    new String("integer_parsed"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(42))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_parsed"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(19.9))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("boolean_parsed"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("date_parsed"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(15),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("time_parsed"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(
                                    new Time(
                                        new UShort(13),
                                        new UShort(45),
                                        new UShort(30)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("datetime_parsed"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(15),
                                            new UShort(6),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(13),
                                            new UShort(45),
                                            new UShort(30)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("uuid_parsed"),
                                    new UuidColumnType()
                                ),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "0000007b-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("integer_or_null"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(42))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_or_null"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(19.9))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("boolean_or_null"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("date_or_null"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(15),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("time_or_null"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(
                                    new Time(
                                        new UShort(13),
                                        new UShort(45),
                                        new UShort(30)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("datetime_or_null"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(15),
                                            new UShort(6),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(13),
                                            new UShort(45),
                                            new UShort(30)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("uuid_or_null"),
                                    new UuidColumnType()
                                ),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "0000007b-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("integer_lifted"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(7))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_lifted"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("boolean_lifted"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("date_lifted"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(29),
                                        new UShort(2),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("time_lifted"),
                                    new TimeColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("datetime_lifted"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(31),
                                            new UShort(1),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(23),
                                            new UShort(30),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("uuid_lifted"),
                                    new UuidColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("integer_of_nullable_or_null"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(7))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_of_nullable_or_null"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("boolean_of_nullable_or_null"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("date_of_nullable_or_null"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(29),
                                        new UShort(2),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("time_of_nullable_or_null"),
                                    new TimeColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("datetime_of_nullable_or_null"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(31),
                                            new UShort(1),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(23),
                                            new UShort(30),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("uuid_of_nullable_or_null"),
                                    new UuidColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("round_trip"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("same_text"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_of_year"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(1),
                                        new UShort(1),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("status_code"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("total_prefix"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(30))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("day_of_a_datetime_text"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(15),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("day_of_a_datetime"),
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
                                new Column(
                                    new String("day_of_a_date_text"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(15),
                                        new UShort(6),
                                        new UShort(2024)
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
                                new Column(
                                    new String("integer_parsed"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(42))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_parsed"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(19.9))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("boolean_parsed"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("date_parsed"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(15),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("time_parsed"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(
                                    new Time(
                                        new UShort(13),
                                        new UShort(45),
                                        new UShort(30)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("datetime_parsed"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(15),
                                            new UShort(6),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(13),
                                            new UShort(45),
                                            new UShort(30)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("uuid_parsed"),
                                    new UuidColumnType()
                                ),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "0000007b-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("integer_or_null"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(42))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_or_null"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(19.9))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("boolean_or_null"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("date_or_null"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(15),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("time_or_null"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(
                                    new Time(
                                        new UShort(13),
                                        new UShort(45),
                                        new UShort(30)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("datetime_or_null"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(15),
                                            new UShort(6),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(13),
                                            new UShort(45),
                                            new UShort(30)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("uuid_or_null"),
                                    new UuidColumnType()
                                ),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "0000007b-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("integer_lifted"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(7))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_lifted"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("boolean_lifted"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("date_lifted"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(29),
                                        new UShort(2),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("time_lifted"),
                                    new TimeColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("datetime_lifted"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(31),
                                            new UShort(1),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(23),
                                            new UShort(30),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("uuid_lifted"),
                                    new UuidColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("integer_of_nullable_or_null"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(7))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_of_nullable_or_null"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("boolean_of_nullable_or_null"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("date_of_nullable_or_null"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(29),
                                        new UShort(2),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("time_of_nullable_or_null"),
                                    new TimeColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("datetime_of_nullable_or_null"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(31),
                                            new UShort(1),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(23),
                                            new UShort(30),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("uuid_of_nullable_or_null"),
                                    new UuidColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("round_trip"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("same_text"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_of_year"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(1),
                                        new UShort(1),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("status_code"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("total_prefix"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(10))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("day_of_a_datetime_text"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(15),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("day_of_a_datetime"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(6),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("day_of_a_date_text"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(15),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
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
