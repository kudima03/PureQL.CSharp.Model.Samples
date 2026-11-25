using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
using PureQL.CSharp.Model.Literals;

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
                    new LiteralUuid(new Guid(0, 0, 0, new byte[8]))
                ),
                new SelectItemProjection(
                    "max_uuid",
                    new LiteralUuid(new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"))
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
}
