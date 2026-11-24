using Pure.Collections.Generic;
using Pure.Primitives.Number;
using Pure.Primitives.String;
using Pure.RelationalSchema.Abstractions.Column;
using Pure.RelationalSchema.ColumnType;
using Pure.RelationalSchema.HashCodes;
using Pure.RelationalSchema.Storage;
using Pure.RelationalSchema.Storage.Abstractions;
using Pure.RelationalSchema.Storage.Samples.Cells;
using Pure.RelationalSchema.Storage.Samples.SchemaDataSets;
using Pure.RelationalSchema.Storage.Samples.TableDataSets;
using PureQL.CSharp.Model.Fields;
using PureQL.CSharp.Model.GroupExpressions;
using PureQL.CSharp.Model.Keys;
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.ProjectionExpressions;
using PureQL.CSharp.Model.RowExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using Guid = Pure.Primitives.Guid.Guid;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Recursive;

/// <summary>
/// Selects the internal rate of return of two investments by Newton's method: an inline
/// rows subquery cash_flows holds the investment_id, period and amount of each cash flow;
/// a recursive subquery rate is seeded with each investment_id at step 0 and rate 0.1,
/// and its grouped next level joins cash_flows, groups by investment_id, step and rate,
/// keeps the groups whose net present value (the sum of amount divided by power of 1 +
/// rate to period) is above 0.0000001 in absolute value, and moves the rate by that value
/// divided by the sum of period times amount over power of 1 + rate to period + 1, with a
/// maxDepth of 50. The main query selects investment_id, the rate rounded to 6 digits and
/// the step, ordered by the investment_id and steps descending columns, with distinctBy
/// investment_id keeping the last step of each.
/// </summary>
public sealed record RateOfReturnByIterationQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithoutPagination(
            subqueries:
            [
                new SubqueryRows(
                    "cash_flows",
                    [
                        [
                            new RowItem(
                                "investment_id",
                                new LiteralUuid(new System.Guid(1001, 0, 0, new byte[8]))
                            ),
                            new RowItem("period", new LiteralInteger(0)),
                            new RowItem("amount", new LiteralDecimal(-1000m)),
                        ],
                        [
                            new RowItem(
                                "investment_id",
                                new LiteralUuid(new System.Guid(1001, 0, 0, new byte[8]))
                            ),
                            new RowItem("period", new LiteralInteger(1)),
                            new RowItem("amount", new LiteralDecimal(300m)),
                        ],
                        [
                            new RowItem(
                                "investment_id",
                                new LiteralUuid(new System.Guid(1001, 0, 0, new byte[8]))
                            ),
                            new RowItem("period", new LiteralInteger(2)),
                            new RowItem("amount", new LiteralDecimal(400m)),
                        ],
                        [
                            new RowItem(
                                "investment_id",
                                new LiteralUuid(new System.Guid(1001, 0, 0, new byte[8]))
                            ),
                            new RowItem("period", new LiteralInteger(3)),
                            new RowItem("amount", new LiteralDecimal(500m)),
                        ],
                        [
                            new RowItem(
                                "investment_id",
                                new LiteralUuid(new System.Guid(1002, 0, 0, new byte[8]))
                            ),
                            new RowItem("period", new LiteralInteger(0)),
                            new RowItem("amount", new LiteralDecimal(-500m)),
                        ],
                        [
                            new RowItem(
                                "investment_id",
                                new LiteralUuid(new System.Guid(1002, 0, 0, new byte[8]))
                            ),
                            new RowItem("period", new LiteralInteger(1)),
                            new RowItem("amount", new LiteralDecimal(200m)),
                        ],
                        [
                            new RowItem(
                                "investment_id",
                                new LiteralUuid(new System.Guid(1002, 0, 0, new byte[8]))
                            ),
                            new RowItem("period", new LiteralInteger(2)),
                            new RowItem("amount", new LiteralDecimal(200m)),
                        ],
                        [
                            new RowItem(
                                "investment_id",
                                new LiteralUuid(new System.Guid(1002, 0, 0, new byte[8]))
                            ),
                            new RowItem("period", new LiteralInteger(3)),
                            new RowItem("amount", new LiteralDecimal(200m)),
                        ],
                    ]
                ),
                new SubqueryRecursive(
                    "rate",
                    new Recursion(
                        new GroupedQueryWithoutPaginationWithoutOrderBy(
                            from: new FromSubquery("cash_flows"),
                            groupBy:
                            [
                                new GroupKey(
                                    "investment_id",
                                    new FieldUuid("cash_flows", "investment_id")
                                ),
                            ],
                            select:
                            [
                                new SelectItemGroup(
                                    "investment_id",
                                    new KeyUuid("investment_id")
                                ),
                                new SelectItemGroup("step", new LiteralInteger(0)),
                                new SelectItemGroup("rate", new LiteralDecimal(0.1m)),
                            ]
                        ),
                        new GroupedNextQuery(
                            from: new FromSubquery("rate"),
                            joins:
                            [
                                new JoinSubquery(
                                    JoinKind.Inner,
                                    "cash_flows",
                                    new EqualUuidRow(
                                        new FieldUuid("cash_flows", "investment_id"),
                                        new FieldUuid("rate", "investment_id")
                                    )
                                ),
                            ],
                            groupBy:
                            [
                                new GroupKey(
                                    "investment_id",
                                    new FieldUuid("rate", "investment_id")
                                ),
                                new GroupKey("step", new FieldInteger("rate", "step")),
                                new GroupKey("rate", new FieldDecimal("rate", "rate")),
                            ],
                            having: new GreaterThanDecimalGroup(
                                new AbsDecimalGroup(
                                    new SumDecimalGroup(
                                        new DivideDecimalRow([
                                            new FieldDecimal("cash_flows", "amount"),
                                            new PowerDecimalRow(
                                                new AddDecimalRow([
                                                    new LiteralInteger(1),
                                                    new FieldDecimal("rate", "rate"),
                                                ]),
                                                new FieldInteger("cash_flows", "period")
                                            ),
                                        ])
                                    )
                                ),
                                new LiteralDecimal(0.0000001m)
                            ),
                            select:
                            [
                                new SelectItemGroup(
                                    "investment_id",
                                    new KeyUuid("investment_id")
                                ),
                                new SelectItemGroup(
                                    "step",
                                    new AddIntegerGroup([
                                        new KeyInteger("step"),
                                        new LiteralInteger(1),
                                    ])
                                ),
                                new SelectItemGroup(
                                    "rate",
                                    new AddDecimalGroup([
                                        new KeyDecimal("rate"),
                                        new DivideDecimalGroup([
                                            new SumDecimalGroup(
                                                new DivideDecimalRow([
                                                    new FieldDecimal(
                                                        "cash_flows",
                                                        "amount"
                                                    ),
                                                    new PowerDecimalRow(
                                                        new AddDecimalRow([
                                                            new LiteralInteger(1),
                                                            new FieldDecimal(
                                                                "rate",
                                                                "rate"
                                                            ),
                                                        ]),
                                                        new FieldInteger(
                                                            "cash_flows",
                                                            "period"
                                                        )
                                                    ),
                                                ])
                                            ),
                                            new SumDecimalGroup(
                                                new DivideDecimalRow([
                                                    new MultiplyDecimalRow([
                                                        new FieldInteger(
                                                            "cash_flows",
                                                            "period"
                                                        ),
                                                        new FieldDecimal(
                                                            "cash_flows",
                                                            "amount"
                                                        ),
                                                    ]),
                                                    new PowerDecimalRow(
                                                        new AddDecimalRow([
                                                            new LiteralInteger(1),
                                                            new FieldDecimal(
                                                                "rate",
                                                                "rate"
                                                            ),
                                                        ]),
                                                        new AddIntegerRow([
                                                            new FieldInteger(
                                                                "cash_flows",
                                                                "period"
                                                            ),
                                                            new LiteralInteger(1),
                                                        ])
                                                    ),
                                                ])
                                            ),
                                        ]),
                                    ])
                                ),
                            ]
                        ),
                        50
                    )
                ),
            ],
            from: new FromSubquery("rate"),
            select:
            [
                new SelectItemProjection(
                    "investment_id",
                    new FieldUuid("rate", "investment_id")
                ),
                new SelectItemProjection(
                    "internal_rate_of_return",
                    new RoundDecimalProjection(
                        new FieldDecimal("rate", "rate"),
                        new LiteralInteger(6)
                    )
                ),
                new SelectItemProjection("steps", new FieldInteger("rate", "step")),
            ],
            distinctBy: ["investment_id"],
            orderBy:
            [
                new OrderItemProjectionColumn("investment_id"),
                new OrderItemProjectionColumn("steps", SortDirection.Desc),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and
    /// <see cref="AuditSchemaDataSet"/>, in this order.
    /// The grouped recursive step is computed with aggregates in a lateral subquery, as
    /// PostgreSQL forbids them in a recursive term, and checked against exact decimal
    /// arithmetic rounded as the specification rounds.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("investment_id"), new UuidColumnType()),
                    new Column(
                        new String("internal_rate_of_return"),
                        new DoubleColumnType()
                    ),
                    new Column(new String("steps"), new LongColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("investment_id"),
                                    new UuidColumnType()
                                ),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "000003e9-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("internal_rate_of_return"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.088963))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("steps"), new LongColumnType()),
                                new InvariantCell(new Long(3))
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
                                    new String("investment_id"),
                                    new UuidColumnType()
                                ),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "000003ea-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("internal_rate_of_return"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.09701))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("steps"), new LongColumnType()),
                                new InvariantCell(new Long(3))
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
