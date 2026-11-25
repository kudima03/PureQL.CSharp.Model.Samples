using PureQL.CSharp.Model.Fields;
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.Parameters;

namespace PureQL.CSharp.Model.Samples.Queries.Subqueries;

/// <summary>
/// Selects amount, label, flag, day, at, time and id from a subquery values of three inline
/// rows that list their columns in different orders: a row of literals of every type, a row
/// of a decimal and typed nulls, and a row of parameters (a nullable integer amount and a
/// parameter for every other column). A column takes the least type that accepts every row,
/// so amount is a nullable decimal and label and day are nullable. Ordered by amount, then
/// by id.
/// </summary>
public sealed record InlineRowsOfEveryCellTypeQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithoutPagination(
            subqueries:
            [
                new SubqueryRows(
                    "values",
                    [
                        [
                            new RowItem("amount", new LiteralInteger(1)),
                            new RowItem("label", new LiteralString("one")),
                            new RowItem("flag", new LiteralBoolean(true)),
                            new RowItem(
                                "day",
                                new LiteralDate(new DateOnly(2024, 1, 31))
                            ),
                            new RowItem(
                                "at",
                                new LiteralDatetime(
                                    new DateTimeOffset(
                                        2024,
                                        1,
                                        31,
                                        8,
                                        30,
                                        0,
                                        TimeSpan.Zero
                                    )
                                )
                            ),
                            new RowItem("time", new LiteralTime(new TimeOnly(8, 30, 0))),
                            new RowItem(
                                "id",
                                new LiteralUuid(
                                    new Guid("3f2a6c1e-8b4d-4e2a-9c1f-1a2b3c4d5e6f")
                                )
                            ),
                        ],
                        [
                            new RowItem("label", new LiteralStringNullable()),
                            new RowItem("amount", new LiteralDecimal(2.5m)),
                            new RowItem(
                                "id",
                                new LiteralUuid(
                                    new Guid("9b1deb4d-3b7d-4bad-9bdd-2b0d7b3dcb6d")
                                )
                            ),
                            new RowItem(
                                "time",
                                new LiteralTime(new TimeOnly(17, 45, 30, 500))
                            ),
                            new RowItem(
                                "at",
                                new LiteralDatetime(
                                    new DateTimeOffset(
                                        2024,
                                        3,
                                        1,
                                        17,
                                        45,
                                        30,
                                        500,
                                        TimeSpan.Zero
                                    )
                                )
                            ),
                            new RowItem("day", new LiteralDateNullable()),
                            new RowItem("flag", new LiteralBoolean(false)),
                        ],
                        [
                            new RowItem("amount", new ParamIntegerNullable("amount")),
                            new RowItem("label", new ParamString("label")),
                            new RowItem("flag", new ParamBoolean("flag")),
                            new RowItem("day", new ParamDate("day")),
                            new RowItem("at", new ParamDatetime("at")),
                            new RowItem("time", new ParamTime("time")),
                            new RowItem("id", new ParamUuid("id")),
                        ],
                    ]
                ),
            ],
            from: new FromSubquery("values"),
            select:
            [
                new SelectItemProjection(
                    "amount",
                    new FieldDecimalNullable("values", "amount")
                ),
                new SelectItemProjection(
                    "label",
                    new FieldStringNullable("values", "label")
                ),
                new SelectItemProjection("flag", new FieldBoolean("values", "flag")),
                new SelectItemProjection("day", new FieldDateNullable("values", "day")),
                new SelectItemProjection("at", new FieldDatetime("values", "at")),
                new SelectItemProjection("time", new FieldTime("values", "time")),
                new SelectItemProjection("id", new FieldUuid("values", "id")),
            ],
            orderBy:
            [
                new OrderItemProjectionExpression(
                    new FieldDecimalNullable("values", "amount")
                ),
                new OrderItemProjectionExpression(new FieldUuid("values", "id")),
            ]
        );
}
