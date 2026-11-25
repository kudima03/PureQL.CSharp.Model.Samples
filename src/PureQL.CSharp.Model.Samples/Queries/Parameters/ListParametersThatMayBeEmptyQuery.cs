using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Samples.Columns;
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
using PureQL.CSharp.Model.Fields;
using PureQL.CSharp.Model.Lists;
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.Parameters;
using PureQL.CSharp.Model.ProjectionExpressions;
using PureQL.CSharp.Model.RowExpressions;

namespace PureQL.CSharp.Model.Samples.Queries.Parameters;

/// <summary>
/// Selects order_id from schema_with_foreign_keys.orders, with, for each of eight types,
/// whether a literal (integer -3, decimal 0.1, string b, boolean false, date 2024-03-01,
/// time 17:45:30.5, datetime 2024-03-01T17:45:30.5Z and a uuid) is in the list parameter
/// of that type (integers, decimals, strings, booleans, dates, times, datetimes and
/// uuids). Orders are kept where, for every type, the boolean parameter named use_ and
/// the type (use_integer, use_decimal and so on) is false or a literal (integer 7,
/// decimal 2.5, string a, boolean true, date 2024-01-31, time 08:30:00, datetime
/// 2024-01-31T08:30:00Z and a uuid) is in the list parameter: a set chosen at execution
/// time may be empty, which a literal cannot be.
/// </summary>
public sealed record ListParametersThatMayBeEmptyQuery
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
            where: new AndRow([
                new OrRow([
                    new EqualBooleanRow(
                        new ParamBoolean("use_integer"),
                        new LiteralBoolean(false)
                    ),
                    new InDecimalRow(
                        new LiteralInteger(7),
                        new ListParamInteger("integers")
                    ),
                ]),
                new OrRow([
                    new EqualBooleanRow(
                        new ParamBoolean("use_decimal"),
                        new LiteralBoolean(false)
                    ),
                    new InDecimalRow(
                        new LiteralDecimal(2.5m),
                        new ListParamDecimal("decimals")
                    ),
                ]),
                new OrRow([
                    new EqualBooleanRow(
                        new ParamBoolean("use_string"),
                        new LiteralBoolean(false)
                    ),
                    new InStringRow(
                        new LiteralString("a"),
                        new ListParamString("strings")
                    ),
                ]),
                new OrRow([
                    new EqualBooleanRow(
                        new ParamBoolean("use_boolean"),
                        new LiteralBoolean(false)
                    ),
                    new InBooleanRow(
                        new LiteralBoolean(true),
                        new ListParamBoolean("booleans")
                    ),
                ]),
                new OrRow([
                    new EqualBooleanRow(
                        new ParamBoolean("use_date"),
                        new LiteralBoolean(false)
                    ),
                    new InDateRow(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new ListParamDate("dates")
                    ),
                ]),
                new OrRow([
                    new EqualBooleanRow(
                        new ParamBoolean("use_time"),
                        new LiteralBoolean(false)
                    ),
                    new InTimeRow(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new ListParamTime("times")
                    ),
                ]),
                new OrRow([
                    new EqualBooleanRow(
                        new ParamBoolean("use_datetime"),
                        new LiteralBoolean(false)
                    ),
                    new InDatetimeRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new ListParamDatetime("datetimes")
                    ),
                ]),
                new OrRow([
                    new EqualBooleanRow(
                        new ParamBoolean("use_uuid"),
                        new LiteralBoolean(false)
                    ),
                    new InUuidRow(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new ListParamUuid("uuids")
                    ),
                ]),
            ]),
            select:
            [
                new SelectItemProjection(
                    "order_id",
                    new FieldUuid(
                        new JoinedString(
                            new DotString(),
                            [
                                new RelationalSchemaWithForeignKeys().Name,
                                new OrdersTable().Name,
                            ]
                        ).TextValue,
                        new OrderIdColumn().Name.TextValue
                    )
                ),
                new SelectItemProjection(
                    "integer_is_chosen",
                    new InDecimalProjection(
                        new LiteralInteger(-3),
                        new ListParamInteger("integers")
                    )
                ),
                new SelectItemProjection(
                    "decimal_is_chosen",
                    new InDecimalProjection(
                        new LiteralDecimal(0.1m),
                        new ListParamDecimal("decimals")
                    )
                ),
                new SelectItemProjection(
                    "string_is_chosen",
                    new InStringProjection(
                        new LiteralString("b"),
                        new ListParamString("strings")
                    )
                ),
                new SelectItemProjection(
                    "boolean_is_chosen",
                    new InBooleanProjection(
                        new LiteralBoolean(false),
                        new ListParamBoolean("booleans")
                    )
                ),
                new SelectItemProjection(
                    "date_is_chosen",
                    new InDateProjection(
                        new LiteralDate(new DateOnly(2024, 3, 1)),
                        new ListParamDate("dates")
                    )
                ),
                new SelectItemProjection(
                    "time_is_chosen",
                    new InTimeProjection(
                        new LiteralTime(new TimeOnly(17, 45, 30, 500)),
                        new ListParamTime("times")
                    )
                ),
                new SelectItemProjection(
                    "datetime_is_chosen",
                    new InDatetimeProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        ),
                        new ListParamDatetime("datetimes")
                    )
                ),
                new SelectItemProjection(
                    "uuid_is_chosen",
                    new InUuidProjection(
                        new LiteralUuid(new Guid(2, 0, 0, new byte[8])),
                        new ListParamUuid("uuids")
                    )
                ),
            ]
        );
}
