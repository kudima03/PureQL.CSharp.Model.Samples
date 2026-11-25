using Pure.Collections.Generic;
using Pure.Primitives.Date;
using Pure.Primitives.Number;
using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.Primitives.Time;
using Pure.RelationalSchema.Abstractions.Column;
using Pure.RelationalSchema.ColumnType;
using Pure.RelationalSchema.HashCodes;
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
using Pure.RelationalSchema.Storage;
using Pure.RelationalSchema.Storage.Abstractions;
using Pure.RelationalSchema.Storage.Samples.Cells;
using Pure.RelationalSchema.Storage.Samples.SchemaDataSets;
using Pure.RelationalSchema.Storage.Samples.TableDataSets;
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.Samples.Cells;
using Column = Pure.RelationalSchema.Column.Column;
using DateTime = Pure.Primitives.DateTime.DateTime;
using Double = Pure.Primitives.Number.Double;
using Guid = Pure.Primitives.Guid.Guid;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Select;

/// <summary>
/// Selects from schema_with_foreign_keys.orders, on every row, as literals at the edges
/// of each format: the leap days of 0004, 0400, 1600, 2000, 2400 and 9996, the last day
/// of February of 1900 and 2100, 2024-12-31 and 2024-11-30, the first instant
/// 0001-01-01T00:00:00Z, the last instant 9999-12-31T23:59:59.999999Z and the leap
/// instant 2024-02-29T23:59:59.000001Z, the times 00:00:00.000001, 23:59:59 and
/// 23:59:59.999999, the nil and the largest uuid, the decimals 0, -0.5, the largest
/// 9999999999999999999999999999, the smallest fraction 0.0000000000000000000000000001 and
/// 99999999999999999999999999.99, the integers 0, 0 and 100, and the strings empty, a
/// newline between a and b, a quote and a backslash, an emoji with an accented e, three
/// spaces and 2024-02-30.
/// </summary>
public sealed record LiteralsAtTheEdgesOfEveryFormatQuery
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
            select:
            [
                new SelectItemProjection(
                    "leap_day_0004",
                    new LiteralDate(new DateOnly(4, 2, 29))
                ),
                new SelectItemProjection(
                    "leap_day_0400",
                    new LiteralDate(new DateOnly(400, 2, 29))
                ),
                new SelectItemProjection(
                    "leap_day_1600",
                    new LiteralDate(new DateOnly(1600, 2, 29))
                ),
                new SelectItemProjection(
                    "leap_day_2000",
                    new LiteralDate(new DateOnly(2000, 2, 29))
                ),
                new SelectItemProjection(
                    "leap_day_2400",
                    new LiteralDate(new DateOnly(2400, 2, 29))
                ),
                new SelectItemProjection(
                    "leap_day_9996",
                    new LiteralDate(new DateOnly(9996, 2, 29))
                ),
                new SelectItemProjection(
                    "last_day_of_february_1900",
                    new LiteralDate(new DateOnly(1900, 2, 28))
                ),
                new SelectItemProjection(
                    "last_day_of_february_2100",
                    new LiteralDate(new DateOnly(2100, 2, 28))
                ),
                new SelectItemProjection(
                    "thirty_first_of_december",
                    new LiteralDate(new DateOnly(2024, 12, 31))
                ),
                new SelectItemProjection(
                    "thirtieth_of_november",
                    new LiteralDate(new DateOnly(2024, 11, 30))
                ),
                new SelectItemProjection(
                    "first_instant",
                    new LiteralDatetime(
                        new DateTimeOffset(1, 1, 1, 0, 0, 0, TimeSpan.Zero)
                    )
                ),
                new SelectItemProjection(
                    "last_instant",
                    new LiteralDatetime(
                        new DateTimeOffset(
                            9999,
                            12,
                            31,
                            23,
                            59,
                            59,
                            999,
                            999,
                            TimeSpan.Zero
                        )
                    )
                ),
                new SelectItemProjection(
                    "leap_instant",
                    new LiteralDatetime(
                        new DateTimeOffset(2024, 2, 29, 23, 59, 59, 0, 1, TimeSpan.Zero)
                    )
                ),
                new SelectItemProjection(
                    "one_microsecond",
                    new LiteralTime(new TimeOnly(0, 0, 0, 0, 1))
                ),
                new SelectItemProjection(
                    "last_second",
                    new LiteralTime(new TimeOnly(23, 59, 59))
                ),
                new SelectItemProjection(
                    "last_microsecond",
                    new LiteralTime(new TimeOnly(23, 59, 59, 999, 999))
                ),
                new SelectItemProjection(
                    "nil_uuid",
                    new LiteralUuid(new System.Guid(0, 0, 0, new byte[8]))
                ),
                new SelectItemProjection(
                    "max_uuid",
                    new LiteralUuid(
                        new System.Guid("ffffffff-ffff-ffff-ffff-ffffffffffff")
                    )
                ),
                new SelectItemProjection("zero", new LiteralDecimal(0m)),
                new SelectItemProjection("negative_half", new LiteralDecimal(-0.5m)),
                new SelectItemProjection(
                    "largest_decimal",
                    new LiteralDecimal(9999999999999999999999999999m)
                ),
                new SelectItemProjection(
                    "smallest_fraction",
                    new LiteralDecimal(0.0000000000000000000000000001m)
                ),
                new SelectItemProjection(
                    "most_digits_with_fraction",
                    new LiteralDecimal(99999999999999999999999999.99m)
                ),
                new SelectItemProjection("negative_zero_integer", new LiteralInteger(0)),
                new SelectItemProjection("zero_integer", new LiteralInteger(0)),
                new SelectItemProjection(
                    "integer_written_with_fraction_zero",
                    new LiteralInteger(100)
                ),
                new SelectItemProjection("empty_string", new LiteralString("")),
                new SelectItemProjection(
                    "string_with_newline",
                    new LiteralString("a\nb")
                ),
                new SelectItemProjection(
                    "string_with_quote_and_backslash",
                    new LiteralString("say \"hi\" \\ bye")
                ),
                new SelectItemProjection(
                    "string_with_emoji",
                    new LiteralString("\U0001f600 \u00e9")
                ),
                new SelectItemProjection("string_of_spaces", new LiteralString("   ")),
                new SelectItemProjection(
                    "string_looking_like_a_date",
                    new LiteralString("2024-02-30")
                ),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and
    /// <see cref="AuditSchemaDataSet"/>, in no particular order: the same literals on
    /// each of the six orders. A decimal is the nearest double, and a time or datetime with
    /// a fractional second is its text as the specification spells it, since a
    /// Pure.Primitives time holds whole seconds.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("leap_day_0004"), new DateColumnType()),
                    new Column(new String("leap_day_0400"), new DateColumnType()),
                    new Column(new String("leap_day_1600"), new DateColumnType()),
                    new Column(new String("leap_day_2000"), new DateColumnType()),
                    new Column(new String("leap_day_2400"), new DateColumnType()),
                    new Column(new String("leap_day_9996"), new DateColumnType()),
                    new Column(
                        new String("last_day_of_february_1900"),
                        new DateColumnType()
                    ),
                    new Column(
                        new String("last_day_of_february_2100"),
                        new DateColumnType()
                    ),
                    new Column(
                        new String("thirty_first_of_december"),
                        new DateColumnType()
                    ),
                    new Column(new String("thirtieth_of_november"), new DateColumnType()),
                    new Column(new String("first_instant"), new DateTimeColumnType()),
                    new Column(new String("last_instant"), new DateTimeColumnType()),
                    new Column(new String("leap_instant"), new DateTimeColumnType()),
                    new Column(new String("one_microsecond"), new TimeColumnType()),
                    new Column(new String("last_second"), new TimeColumnType()),
                    new Column(new String("last_microsecond"), new TimeColumnType()),
                    new Column(new String("nil_uuid"), new UuidColumnType()),
                    new Column(new String("max_uuid"), new UuidColumnType()),
                    new Column(new String("zero"), new DoubleColumnType()),
                    new Column(new String("negative_half"), new DoubleColumnType()),
                    new Column(new String("largest_decimal"), new DoubleColumnType()),
                    new Column(new String("smallest_fraction"), new DoubleColumnType()),
                    new Column(
                        new String("most_digits_with_fraction"),
                        new DoubleColumnType()
                    ),
                    new Column(new String("negative_zero_integer"), new LongColumnType()),
                    new Column(new String("zero_integer"), new LongColumnType()),
                    new Column(
                        new String("integer_written_with_fraction_zero"),
                        new LongColumnType()
                    ),
                    new Column(new String("empty_string"), new StringColumnType()),
                    new Column(new String("string_with_newline"), new StringColumnType()),
                    new Column(
                        new String("string_with_quote_and_backslash"),
                        new StringColumnType()
                    ),
                    new Column(new String("string_with_emoji"), new StringColumnType()),
                    new Column(new String("string_of_spaces"), new StringColumnType()),
                    new Column(
                        new String("string_looking_like_a_date"),
                        new StringColumnType()
                    ),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("leap_day_0004"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(new UShort(29), new UShort(2), new UShort(4))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("leap_day_0400"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(29),
                                        new UShort(2),
                                        new UShort(400)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("leap_day_1600"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(29),
                                        new UShort(2),
                                        new UShort(1600)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("leap_day_2000"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(29),
                                        new UShort(2),
                                        new UShort(2000)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("leap_day_2400"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(29),
                                        new UShort(2),
                                        new UShort(2400)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("leap_day_9996"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(29),
                                        new UShort(2),
                                        new UShort(9996)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_day_of_february_1900"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(28),
                                        new UShort(2),
                                        new UShort(1900)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_day_of_february_2100"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(28),
                                        new UShort(2),
                                        new UShort(2100)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("thirty_first_of_december"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(31),
                                        new UShort(12),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("thirtieth_of_november"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(30),
                                        new UShort(11),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_instant"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(1),
                                            new UShort(1),
                                            new UShort(1)
                                        ),
                                        new Time(
                                            new UShort(0),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_instant"),
                                    new DateTimeColumnType()
                                ),
                                new InvariantCell(
                                    new String("9999-12-31T23:59:59.999999Z")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("leap_instant"),
                                    new DateTimeColumnType()
                                ),
                                new InvariantCell(
                                    new String("2024-02-29T23:59:59.000001Z")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("one_microsecond"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(new String("00:00:00.000001"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_second"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(
                                    new Time(
                                        new UShort(23),
                                        new UShort(59),
                                        new UShort(59)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_microsecond"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(new String("23:59:59.999999"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("nil_uuid"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000000-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("max_uuid"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "ffffffff-ffff-ffff-ffff-ffffffffffff"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("zero"), new DoubleColumnType()),
                                new InvariantCell(new Double(0d))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("negative_half"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-0.5d))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("largest_decimal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1E+28d))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("smallest_fraction"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1E-28d))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("most_digits_with_fraction"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1E+26d))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("negative_zero_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("zero_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("integer_written_with_fraction_zero"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(100))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("empty_string"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String(""))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("string_with_newline"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("a\nb"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("string_with_quote_and_backslash"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("say \"hi\" \\ bye"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("string_with_emoji"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("\U0001f600 \u00e9"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("string_of_spaces"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("   "))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("string_looking_like_a_date"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("2024-02-30"))
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
                                    new String("leap_day_0004"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(new UShort(29), new UShort(2), new UShort(4))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("leap_day_0400"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(29),
                                        new UShort(2),
                                        new UShort(400)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("leap_day_1600"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(29),
                                        new UShort(2),
                                        new UShort(1600)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("leap_day_2000"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(29),
                                        new UShort(2),
                                        new UShort(2000)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("leap_day_2400"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(29),
                                        new UShort(2),
                                        new UShort(2400)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("leap_day_9996"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(29),
                                        new UShort(2),
                                        new UShort(9996)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_day_of_february_1900"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(28),
                                        new UShort(2),
                                        new UShort(1900)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_day_of_february_2100"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(28),
                                        new UShort(2),
                                        new UShort(2100)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("thirty_first_of_december"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(31),
                                        new UShort(12),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("thirtieth_of_november"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(30),
                                        new UShort(11),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_instant"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(1),
                                            new UShort(1),
                                            new UShort(1)
                                        ),
                                        new Time(
                                            new UShort(0),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_instant"),
                                    new DateTimeColumnType()
                                ),
                                new InvariantCell(
                                    new String("9999-12-31T23:59:59.999999Z")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("leap_instant"),
                                    new DateTimeColumnType()
                                ),
                                new InvariantCell(
                                    new String("2024-02-29T23:59:59.000001Z")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("one_microsecond"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(new String("00:00:00.000001"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_second"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(
                                    new Time(
                                        new UShort(23),
                                        new UShort(59),
                                        new UShort(59)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_microsecond"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(new String("23:59:59.999999"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("nil_uuid"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000000-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("max_uuid"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "ffffffff-ffff-ffff-ffff-ffffffffffff"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("zero"), new DoubleColumnType()),
                                new InvariantCell(new Double(0d))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("negative_half"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-0.5d))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("largest_decimal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1E+28d))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("smallest_fraction"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1E-28d))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("most_digits_with_fraction"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1E+26d))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("negative_zero_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("zero_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("integer_written_with_fraction_zero"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(100))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("empty_string"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String(""))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("string_with_newline"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("a\nb"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("string_with_quote_and_backslash"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("say \"hi\" \\ bye"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("string_with_emoji"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("\U0001f600 \u00e9"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("string_of_spaces"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("   "))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("string_looking_like_a_date"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("2024-02-30"))
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
                                    new String("leap_day_0004"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(new UShort(29), new UShort(2), new UShort(4))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("leap_day_0400"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(29),
                                        new UShort(2),
                                        new UShort(400)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("leap_day_1600"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(29),
                                        new UShort(2),
                                        new UShort(1600)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("leap_day_2000"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(29),
                                        new UShort(2),
                                        new UShort(2000)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("leap_day_2400"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(29),
                                        new UShort(2),
                                        new UShort(2400)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("leap_day_9996"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(29),
                                        new UShort(2),
                                        new UShort(9996)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_day_of_february_1900"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(28),
                                        new UShort(2),
                                        new UShort(1900)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_day_of_february_2100"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(28),
                                        new UShort(2),
                                        new UShort(2100)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("thirty_first_of_december"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(31),
                                        new UShort(12),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("thirtieth_of_november"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(30),
                                        new UShort(11),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_instant"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(1),
                                            new UShort(1),
                                            new UShort(1)
                                        ),
                                        new Time(
                                            new UShort(0),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_instant"),
                                    new DateTimeColumnType()
                                ),
                                new InvariantCell(
                                    new String("9999-12-31T23:59:59.999999Z")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("leap_instant"),
                                    new DateTimeColumnType()
                                ),
                                new InvariantCell(
                                    new String("2024-02-29T23:59:59.000001Z")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("one_microsecond"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(new String("00:00:00.000001"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_second"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(
                                    new Time(
                                        new UShort(23),
                                        new UShort(59),
                                        new UShort(59)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_microsecond"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(new String("23:59:59.999999"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("nil_uuid"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000000-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("max_uuid"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "ffffffff-ffff-ffff-ffff-ffffffffffff"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("zero"), new DoubleColumnType()),
                                new InvariantCell(new Double(0d))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("negative_half"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-0.5d))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("largest_decimal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1E+28d))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("smallest_fraction"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1E-28d))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("most_digits_with_fraction"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1E+26d))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("negative_zero_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("zero_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("integer_written_with_fraction_zero"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(100))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("empty_string"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String(""))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("string_with_newline"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("a\nb"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("string_with_quote_and_backslash"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("say \"hi\" \\ bye"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("string_with_emoji"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("\U0001f600 \u00e9"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("string_of_spaces"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("   "))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("string_looking_like_a_date"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("2024-02-30"))
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
                                    new String("leap_day_0004"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(new UShort(29), new UShort(2), new UShort(4))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("leap_day_0400"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(29),
                                        new UShort(2),
                                        new UShort(400)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("leap_day_1600"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(29),
                                        new UShort(2),
                                        new UShort(1600)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("leap_day_2000"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(29),
                                        new UShort(2),
                                        new UShort(2000)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("leap_day_2400"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(29),
                                        new UShort(2),
                                        new UShort(2400)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("leap_day_9996"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(29),
                                        new UShort(2),
                                        new UShort(9996)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_day_of_february_1900"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(28),
                                        new UShort(2),
                                        new UShort(1900)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_day_of_february_2100"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(28),
                                        new UShort(2),
                                        new UShort(2100)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("thirty_first_of_december"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(31),
                                        new UShort(12),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("thirtieth_of_november"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(30),
                                        new UShort(11),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_instant"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(1),
                                            new UShort(1),
                                            new UShort(1)
                                        ),
                                        new Time(
                                            new UShort(0),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_instant"),
                                    new DateTimeColumnType()
                                ),
                                new InvariantCell(
                                    new String("9999-12-31T23:59:59.999999Z")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("leap_instant"),
                                    new DateTimeColumnType()
                                ),
                                new InvariantCell(
                                    new String("2024-02-29T23:59:59.000001Z")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("one_microsecond"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(new String("00:00:00.000001"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_second"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(
                                    new Time(
                                        new UShort(23),
                                        new UShort(59),
                                        new UShort(59)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_microsecond"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(new String("23:59:59.999999"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("nil_uuid"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000000-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("max_uuid"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "ffffffff-ffff-ffff-ffff-ffffffffffff"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("zero"), new DoubleColumnType()),
                                new InvariantCell(new Double(0d))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("negative_half"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-0.5d))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("largest_decimal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1E+28d))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("smallest_fraction"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1E-28d))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("most_digits_with_fraction"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1E+26d))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("negative_zero_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("zero_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("integer_written_with_fraction_zero"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(100))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("empty_string"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String(""))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("string_with_newline"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("a\nb"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("string_with_quote_and_backslash"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("say \"hi\" \\ bye"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("string_with_emoji"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("\U0001f600 \u00e9"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("string_of_spaces"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("   "))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("string_looking_like_a_date"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("2024-02-30"))
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
                                    new String("leap_day_0004"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(new UShort(29), new UShort(2), new UShort(4))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("leap_day_0400"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(29),
                                        new UShort(2),
                                        new UShort(400)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("leap_day_1600"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(29),
                                        new UShort(2),
                                        new UShort(1600)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("leap_day_2000"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(29),
                                        new UShort(2),
                                        new UShort(2000)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("leap_day_2400"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(29),
                                        new UShort(2),
                                        new UShort(2400)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("leap_day_9996"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(29),
                                        new UShort(2),
                                        new UShort(9996)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_day_of_february_1900"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(28),
                                        new UShort(2),
                                        new UShort(1900)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_day_of_february_2100"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(28),
                                        new UShort(2),
                                        new UShort(2100)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("thirty_first_of_december"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(31),
                                        new UShort(12),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("thirtieth_of_november"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(30),
                                        new UShort(11),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_instant"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(1),
                                            new UShort(1),
                                            new UShort(1)
                                        ),
                                        new Time(
                                            new UShort(0),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_instant"),
                                    new DateTimeColumnType()
                                ),
                                new InvariantCell(
                                    new String("9999-12-31T23:59:59.999999Z")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("leap_instant"),
                                    new DateTimeColumnType()
                                ),
                                new InvariantCell(
                                    new String("2024-02-29T23:59:59.000001Z")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("one_microsecond"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(new String("00:00:00.000001"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_second"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(
                                    new Time(
                                        new UShort(23),
                                        new UShort(59),
                                        new UShort(59)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_microsecond"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(new String("23:59:59.999999"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("nil_uuid"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000000-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("max_uuid"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "ffffffff-ffff-ffff-ffff-ffffffffffff"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("zero"), new DoubleColumnType()),
                                new InvariantCell(new Double(0d))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("negative_half"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-0.5d))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("largest_decimal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1E+28d))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("smallest_fraction"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1E-28d))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("most_digits_with_fraction"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1E+26d))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("negative_zero_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("zero_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("integer_written_with_fraction_zero"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(100))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("empty_string"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String(""))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("string_with_newline"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("a\nb"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("string_with_quote_and_backslash"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("say \"hi\" \\ bye"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("string_with_emoji"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("\U0001f600 \u00e9"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("string_of_spaces"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("   "))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("string_looking_like_a_date"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("2024-02-30"))
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
                                    new String("leap_day_0004"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(new UShort(29), new UShort(2), new UShort(4))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("leap_day_0400"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(29),
                                        new UShort(2),
                                        new UShort(400)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("leap_day_1600"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(29),
                                        new UShort(2),
                                        new UShort(1600)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("leap_day_2000"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(29),
                                        new UShort(2),
                                        new UShort(2000)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("leap_day_2400"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(29),
                                        new UShort(2),
                                        new UShort(2400)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("leap_day_9996"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(29),
                                        new UShort(2),
                                        new UShort(9996)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_day_of_february_1900"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(28),
                                        new UShort(2),
                                        new UShort(1900)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_day_of_february_2100"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(28),
                                        new UShort(2),
                                        new UShort(2100)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("thirty_first_of_december"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(31),
                                        new UShort(12),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("thirtieth_of_november"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(30),
                                        new UShort(11),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_instant"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(1),
                                            new UShort(1),
                                            new UShort(1)
                                        ),
                                        new Time(
                                            new UShort(0),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_instant"),
                                    new DateTimeColumnType()
                                ),
                                new InvariantCell(
                                    new String("9999-12-31T23:59:59.999999Z")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("leap_instant"),
                                    new DateTimeColumnType()
                                ),
                                new InvariantCell(
                                    new String("2024-02-29T23:59:59.000001Z")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("one_microsecond"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(new String("00:00:00.000001"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_second"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(
                                    new Time(
                                        new UShort(23),
                                        new UShort(59),
                                        new UShort(59)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_microsecond"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(new String("23:59:59.999999"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("nil_uuid"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000000-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("max_uuid"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "ffffffff-ffff-ffff-ffff-ffffffffffff"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("zero"), new DoubleColumnType()),
                                new InvariantCell(new Double(0d))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("negative_half"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-0.5d))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("largest_decimal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1E+28d))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("smallest_fraction"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1E-28d))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("most_digits_with_fraction"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1E+26d))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("negative_zero_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("zero_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("integer_written_with_fraction_zero"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(100))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("empty_string"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String(""))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("string_with_newline"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("a\nb"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("string_with_quote_and_backslash"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("say \"hi\" \\ bye"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("string_with_emoji"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("\U0001f600 \u00e9"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("string_of_spaces"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("   "))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("string_looking_like_a_date"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("2024-02-30"))
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
