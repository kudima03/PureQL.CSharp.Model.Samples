using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
using PureQL.CSharp.Model.Literals;

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
                    new LiteralUuid(new Guid("abcdefab-cdef-4abc-8def-abcdefabcdef"))
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
}
