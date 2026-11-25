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
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using Guid = Pure.Primitives.Guid.Guid;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Select;

/// <summary>
/// Selects, for each row of schema_with_foreign_keys.orders, one literal per column at
/// the edge of its format: the integer 5, -42, the largest and the smallest 64-bit
/// integers; the decimals 5 and 0.00000015, of 28 digits, of 28 digits with a fraction
/// and of 28 decimal places; an empty string; the dates 2024-02-29, 0001-01-01 and
/// 9999-12-31; the times 00:00:00 and 23:59:59.999999; the UTC datetimes
/// 2024-12-31T23:59:59.5Z and 2024-01-01T00:00:00.000001Z; and a uuid of hex letters.
/// </summary>
public sealed record LiteralEdgeValuesOfEveryTypeQuery
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
                new SelectItemProjection("int_written_as_5_0", new LiteralInteger(5)),
                new SelectItemProjection("negative_int", new LiteralInteger(-42)),
                new SelectItemProjection(
                    "decimal_without_fraction",
                    new LiteralDecimal(5m)
                ),
                new SelectItemProjection(
                    "decimal_exponent",
                    new LiteralDecimal(0.00000015m)
                ),
                new SelectItemProjection("empty_string", new LiteralString("")),
                new SelectItemProjection(
                    "leap_day",
                    new LiteralDate(new DateOnly(2024, 2, 29))
                ),
                new SelectItemProjection(
                    "first_day",
                    new LiteralDate(new DateOnly(1, 1, 1))
                ),
                new SelectItemProjection(
                    "last_day",
                    new LiteralDate(new DateOnly(9999, 12, 31))
                ),
                new SelectItemProjection(
                    "midnight",
                    new LiteralTime(new TimeOnly(0, 0, 0))
                ),
                new SelectItemProjection(
                    "microseconds",
                    new LiteralTime(new TimeOnly(23, 59, 59, 999, 999))
                ),
                new SelectItemProjection(
                    "utc_half_second",
                    new LiteralDatetime(
                        new DateTimeOffset(2024, 12, 31, 23, 59, 59, 500, TimeSpan.Zero)
                    )
                ),
                new SelectItemProjection(
                    "utc_one_microsecond",
                    new LiteralDatetime(
                        new DateTimeOffset(2024, 1, 1, 0, 0, 0, 0, 1, TimeSpan.Zero)
                    )
                ),
                new SelectItemProjection(
                    "hex_letters_uuid",
                    new LiteralUuid(
                        new System.Guid("abcdefab-cdef-4abc-8def-abcdefabcdef")
                    )
                ),
                new SelectItemProjection(
                    "decimal_28_digits",
                    new LiteralDecimal(-9999999999999999999999999999m)
                ),
                new SelectItemProjection(
                    "decimal_28_digits_with_fraction",
                    new LiteralDecimal(1234567890123456.789012345678m)
                ),
                new SelectItemProjection(
                    "decimal_28_places",
                    new LiteralDecimal(-0.0000000000000000000000000001m)
                ),
                new SelectItemProjection(
                    "largest_integer",
                    new LiteralInteger(9223372036854775807)
                ),
                new SelectItemProjection(
                    "smallest_integer",
                    new LiteralInteger(-9223372036854775808)
                ),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and
    /// <see cref="AuditSchemaDataSet"/>, in no particular order. Every row holds the same
    /// literals. A <c>decimal</c> is the exact value rounded to <c>double</c>, and the
    /// times and datetimes with a fraction of a second are written as text, since the
    /// typed time and datetime cells keep whole seconds only.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("int_written_as_5_0"), new LongColumnType()),
                    new Column(new String("negative_int"), new LongColumnType()),
                    new Column(
                        new String("decimal_without_fraction"),
                        new DoubleColumnType()
                    ),
                    new Column(new String("decimal_exponent"), new DoubleColumnType()),
                    new Column(new String("empty_string"), new StringColumnType()),
                    new Column(new String("leap_day"), new DateColumnType()),
                    new Column(new String("first_day"), new DateColumnType()),
                    new Column(new String("last_day"), new DateColumnType()),
                    new Column(new String("midnight"), new TimeColumnType()),
                    new Column(new String("microseconds"), new TimeColumnType()),
                    new Column(new String("utc_half_second"), new DateTimeColumnType()),
                    new Column(
                        new String("utc_one_microsecond"),
                        new DateTimeColumnType()
                    ),
                    new Column(new String("hex_letters_uuid"), new UuidColumnType()),
                    new Column(new String("decimal_28_digits"), new DoubleColumnType()),
                    new Column(
                        new String("decimal_28_digits_with_fraction"),
                        new DoubleColumnType()
                    ),
                    new Column(new String("decimal_28_places"), new DoubleColumnType()),
                    new Column(new String("largest_integer"), new LongColumnType()),
                    new Column(new String("smallest_integer"), new LongColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("int_written_as_5_0"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("negative_int"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(-42))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_without_fraction"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(5d))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_exponent"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.00000015d))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("empty_string"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String(""))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("leap_day"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(29),
                                        new UShort(2),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("first_day"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(new UShort(1), new UShort(1), new UShort(1))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("last_day"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(31),
                                        new UShort(12),
                                        new UShort(9999)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("midnight"), new TimeColumnType()),
                                new InvariantCell(
                                    new Time(new UShort(0), new UShort(0), new UShort(0))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("microseconds"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(new String("23:59:59.999999"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("utc_half_second"),
                                    new DateTimeColumnType()
                                ),
                                new InvariantCell(new String("2024-12-31T23:59:59.5Z"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("utc_one_microsecond"),
                                    new DateTimeColumnType()
                                ),
                                new InvariantCell(
                                    new String("2024-01-01T00:00:00.000001Z")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("hex_letters_uuid"),
                                    new UuidColumnType()
                                ),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "abcdefab-cdef-4abc-8def-abcdefabcdef"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_28_digits"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(
                                    new Double(-9999999999999999999999999999d)
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_28_digits_with_fraction"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(
                                    new Double(1234567890123456.789012345678d)
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_28_places"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(
                                    new Double(-0.0000000000000000000000000001d)
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("largest_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(9223372036854775807))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("smallest_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(-9223372036854775808))
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
                                    new String("int_written_as_5_0"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("negative_int"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(-42))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_without_fraction"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(5d))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_exponent"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.00000015d))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("empty_string"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String(""))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("leap_day"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(29),
                                        new UShort(2),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("first_day"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(new UShort(1), new UShort(1), new UShort(1))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("last_day"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(31),
                                        new UShort(12),
                                        new UShort(9999)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("midnight"), new TimeColumnType()),
                                new InvariantCell(
                                    new Time(new UShort(0), new UShort(0), new UShort(0))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("microseconds"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(new String("23:59:59.999999"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("utc_half_second"),
                                    new DateTimeColumnType()
                                ),
                                new InvariantCell(new String("2024-12-31T23:59:59.5Z"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("utc_one_microsecond"),
                                    new DateTimeColumnType()
                                ),
                                new InvariantCell(
                                    new String("2024-01-01T00:00:00.000001Z")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("hex_letters_uuid"),
                                    new UuidColumnType()
                                ),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "abcdefab-cdef-4abc-8def-abcdefabcdef"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_28_digits"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(
                                    new Double(-9999999999999999999999999999d)
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_28_digits_with_fraction"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(
                                    new Double(1234567890123456.789012345678d)
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_28_places"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(
                                    new Double(-0.0000000000000000000000000001d)
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("largest_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(9223372036854775807))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("smallest_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(-9223372036854775808))
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
                                    new String("int_written_as_5_0"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("negative_int"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(-42))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_without_fraction"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(5d))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_exponent"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.00000015d))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("empty_string"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String(""))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("leap_day"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(29),
                                        new UShort(2),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("first_day"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(new UShort(1), new UShort(1), new UShort(1))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("last_day"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(31),
                                        new UShort(12),
                                        new UShort(9999)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("midnight"), new TimeColumnType()),
                                new InvariantCell(
                                    new Time(new UShort(0), new UShort(0), new UShort(0))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("microseconds"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(new String("23:59:59.999999"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("utc_half_second"),
                                    new DateTimeColumnType()
                                ),
                                new InvariantCell(new String("2024-12-31T23:59:59.5Z"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("utc_one_microsecond"),
                                    new DateTimeColumnType()
                                ),
                                new InvariantCell(
                                    new String("2024-01-01T00:00:00.000001Z")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("hex_letters_uuid"),
                                    new UuidColumnType()
                                ),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "abcdefab-cdef-4abc-8def-abcdefabcdef"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_28_digits"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(
                                    new Double(-9999999999999999999999999999d)
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_28_digits_with_fraction"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(
                                    new Double(1234567890123456.789012345678d)
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_28_places"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(
                                    new Double(-0.0000000000000000000000000001d)
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("largest_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(9223372036854775807))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("smallest_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(-9223372036854775808))
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
                                    new String("int_written_as_5_0"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("negative_int"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(-42))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_without_fraction"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(5d))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_exponent"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.00000015d))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("empty_string"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String(""))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("leap_day"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(29),
                                        new UShort(2),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("first_day"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(new UShort(1), new UShort(1), new UShort(1))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("last_day"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(31),
                                        new UShort(12),
                                        new UShort(9999)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("midnight"), new TimeColumnType()),
                                new InvariantCell(
                                    new Time(new UShort(0), new UShort(0), new UShort(0))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("microseconds"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(new String("23:59:59.999999"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("utc_half_second"),
                                    new DateTimeColumnType()
                                ),
                                new InvariantCell(new String("2024-12-31T23:59:59.5Z"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("utc_one_microsecond"),
                                    new DateTimeColumnType()
                                ),
                                new InvariantCell(
                                    new String("2024-01-01T00:00:00.000001Z")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("hex_letters_uuid"),
                                    new UuidColumnType()
                                ),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "abcdefab-cdef-4abc-8def-abcdefabcdef"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_28_digits"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(
                                    new Double(-9999999999999999999999999999d)
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_28_digits_with_fraction"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(
                                    new Double(1234567890123456.789012345678d)
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_28_places"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(
                                    new Double(-0.0000000000000000000000000001d)
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("largest_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(9223372036854775807))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("smallest_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(-9223372036854775808))
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
                                    new String("int_written_as_5_0"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("negative_int"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(-42))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_without_fraction"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(5d))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_exponent"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.00000015d))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("empty_string"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String(""))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("leap_day"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(29),
                                        new UShort(2),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("first_day"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(new UShort(1), new UShort(1), new UShort(1))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("last_day"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(31),
                                        new UShort(12),
                                        new UShort(9999)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("midnight"), new TimeColumnType()),
                                new InvariantCell(
                                    new Time(new UShort(0), new UShort(0), new UShort(0))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("microseconds"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(new String("23:59:59.999999"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("utc_half_second"),
                                    new DateTimeColumnType()
                                ),
                                new InvariantCell(new String("2024-12-31T23:59:59.5Z"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("utc_one_microsecond"),
                                    new DateTimeColumnType()
                                ),
                                new InvariantCell(
                                    new String("2024-01-01T00:00:00.000001Z")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("hex_letters_uuid"),
                                    new UuidColumnType()
                                ),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "abcdefab-cdef-4abc-8def-abcdefabcdef"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_28_digits"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(
                                    new Double(-9999999999999999999999999999d)
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_28_digits_with_fraction"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(
                                    new Double(1234567890123456.789012345678d)
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_28_places"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(
                                    new Double(-0.0000000000000000000000000001d)
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("largest_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(9223372036854775807))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("smallest_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(-9223372036854775808))
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
                                    new String("int_written_as_5_0"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("negative_int"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(-42))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_without_fraction"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(5d))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_exponent"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.00000015d))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("empty_string"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String(""))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("leap_day"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(29),
                                        new UShort(2),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("first_day"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(new UShort(1), new UShort(1), new UShort(1))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("last_day"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(31),
                                        new UShort(12),
                                        new UShort(9999)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("midnight"), new TimeColumnType()),
                                new InvariantCell(
                                    new Time(new UShort(0), new UShort(0), new UShort(0))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("microseconds"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(new String("23:59:59.999999"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("utc_half_second"),
                                    new DateTimeColumnType()
                                ),
                                new InvariantCell(new String("2024-12-31T23:59:59.5Z"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("utc_one_microsecond"),
                                    new DateTimeColumnType()
                                ),
                                new InvariantCell(
                                    new String("2024-01-01T00:00:00.000001Z")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("hex_letters_uuid"),
                                    new UuidColumnType()
                                ),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "abcdefab-cdef-4abc-8def-abcdefabcdef"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_28_digits"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(
                                    new Double(-9999999999999999999999999999d)
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_28_digits_with_fraction"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(
                                    new Double(1234567890123456.789012345678d)
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_28_places"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(
                                    new Double(-0.0000000000000000000000000001d)
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("largest_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(9223372036854775807))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("smallest_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(-9223372036854775808))
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
