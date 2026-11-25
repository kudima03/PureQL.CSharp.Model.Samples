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
using PureQL.CSharp.Model.RowExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using Guid = Pure.Primitives.Guid.Guid;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Recursive;

/// <summary>
/// A rows subquery cash_flows holds the investment_id, period and amount of the cash
/// flows of three investments; a recursive subquery bounds is seeded with each
/// investment_id at step 0 and the interval from -0.99 to 10, and its grouped next level
/// joins cash_flows, takes the steps below 40, groups by investment_id, step, low and
/// high, keeps the groups where high minus low is above 0.001, and replaces the half of
/// the interval that does not hold the root by its middle (the sum of amount divided by
/// power of 1 + the middle to period decides), with a maxDepth of 50. The main query
/// groups bounds by investment_id and selects it, the highest low, the lowest high and
/// the highest step.
/// </summary>
public sealed record BisectionOfARateQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainGroupedQueryWithoutPagination(
            subqueries:
            [
                new SubqueryRows(
                    "cash_flows",
                    [
                        [
                            new RowItem(
                                "investment_id",
                                new LiteralUuid(new System.Guid(2001, 0, 0, new byte[8]))
                            ),
                            new RowItem("period", new LiteralInteger(0)),
                            new RowItem("amount", new LiteralDecimal(-1500m)),
                        ],
                        [
                            new RowItem(
                                "investment_id",
                                new LiteralUuid(new System.Guid(2001, 0, 0, new byte[8]))
                            ),
                            new RowItem("period", new LiteralInteger(1)),
                            new RowItem("amount", new LiteralDecimal(500m)),
                        ],
                        [
                            new RowItem(
                                "investment_id",
                                new LiteralUuid(new System.Guid(2001, 0, 0, new byte[8]))
                            ),
                            new RowItem("period", new LiteralInteger(2)),
                            new RowItem("amount", new LiteralDecimal(700m)),
                        ],
                        [
                            new RowItem(
                                "investment_id",
                                new LiteralUuid(new System.Guid(2001, 0, 0, new byte[8]))
                            ),
                            new RowItem("period", new LiteralInteger(3)),
                            new RowItem("amount", new LiteralDecimal(600m)),
                        ],
                        [
                            new RowItem(
                                "investment_id",
                                new LiteralUuid(new System.Guid(2002, 0, 0, new byte[8]))
                            ),
                            new RowItem("period", new LiteralInteger(0)),
                            new RowItem("amount", new LiteralDecimal(-1000m)),
                        ],
                        [
                            new RowItem(
                                "investment_id",
                                new LiteralUuid(new System.Guid(2002, 0, 0, new byte[8]))
                            ),
                            new RowItem("period", new LiteralInteger(1)),
                            new RowItem("amount", new LiteralDecimal(100m)),
                        ],
                        [
                            new RowItem(
                                "investment_id",
                                new LiteralUuid(new System.Guid(2002, 0, 0, new byte[8]))
                            ),
                            new RowItem("period", new LiteralInteger(2)),
                            new RowItem("amount", new LiteralDecimal(200m)),
                        ],
                        [
                            new RowItem(
                                "investment_id",
                                new LiteralUuid(new System.Guid(2002, 0, 0, new byte[8]))
                            ),
                            new RowItem("period", new LiteralInteger(3)),
                            new RowItem("amount", new LiteralDecimal(1000m)),
                        ],
                        [
                            new RowItem(
                                "investment_id",
                                new LiteralUuid(new System.Guid(2003, 0, 0, new byte[8]))
                            ),
                            new RowItem("period", new LiteralInteger(0)),
                            new RowItem("amount", new LiteralDecimal(-300m)),
                        ],
                        [
                            new RowItem(
                                "investment_id",
                                new LiteralUuid(new System.Guid(2003, 0, 0, new byte[8]))
                            ),
                            new RowItem("period", new LiteralInteger(1)),
                            new RowItem("amount", new LiteralDecimal(150m)),
                        ],
                        [
                            new RowItem(
                                "investment_id",
                                new LiteralUuid(new System.Guid(2003, 0, 0, new byte[8]))
                            ),
                            new RowItem("period", new LiteralInteger(2)),
                            new RowItem("amount", new LiteralDecimal(150m)),
                        ],
                        [
                            new RowItem(
                                "investment_id",
                                new LiteralUuid(new System.Guid(2003, 0, 0, new byte[8]))
                            ),
                            new RowItem("period", new LiteralInteger(3)),
                            new RowItem("amount", new LiteralDecimal(150m)),
                        ],
                    ]
                ),
                new SubqueryRecursive(
                    "bounds",
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
                                new SelectItemGroup("low", new LiteralDecimal(-0.99m)),
                                new SelectItemGroup("high", new LiteralDecimal(10m)),
                            ]
                        ),
                        new GroupedNextQuery(
                            from: new FromSubquery("bounds"),
                            joins:
                            [
                                new JoinSubquery(
                                    JoinKind.Inner,
                                    "cash_flows",
                                    new EqualUuidRow(
                                        new FieldUuid("cash_flows", "investment_id"),
                                        new FieldUuid("bounds", "investment_id")
                                    )
                                ),
                            ],
                            where: new LessThanDecimalRow(
                                new FieldInteger("bounds", "step"),
                                new LiteralInteger(40)
                            ),
                            groupBy:
                            [
                                new GroupKey(
                                    "investment_id",
                                    new FieldUuid("bounds", "investment_id")
                                ),
                                new GroupKey("step", new FieldInteger("bounds", "step")),
                                new GroupKey("low", new FieldDecimal("bounds", "low")),
                                new GroupKey("high", new FieldDecimal("bounds", "high")),
                            ],
                            having: new GreaterThanDecimalGroup(
                                new SubtractDecimalGroup([
                                    new KeyDecimal("high"),
                                    new KeyDecimal("low"),
                                ]),
                                new LiteralDecimal(0.001m)
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
                                    "low",
                                    new IfDecimalGroup(
                                        new DivideDecimalGroup([
                                            new AddDecimalGroup([
                                                new KeyDecimal("low"),
                                                new KeyDecimal("high"),
                                            ]),
                                            new LiteralInteger(2),
                                        ]),
                                        new KeyDecimal("low"),
                                        new GreaterThanDecimalGroup(
                                            new SumDecimalGroup(
                                                new DivideDecimalRow([
                                                    new FieldDecimal(
                                                        "cash_flows",
                                                        "amount"
                                                    ),
                                                    new PowerDecimalRow(
                                                        new AddDecimalRow([
                                                            new LiteralInteger(1),
                                                            new DivideDecimalRow([
                                                                new AddDecimalRow([
                                                                    new FieldDecimal(
                                                                        "bounds",
                                                                        "low"
                                                                    ),
                                                                    new FieldDecimal(
                                                                        "bounds",
                                                                        "high"
                                                                    ),
                                                                ]),
                                                                new LiteralInteger(2),
                                                            ]),
                                                        ]),
                                                        new FieldInteger(
                                                            "cash_flows",
                                                            "period"
                                                        )
                                                    ),
                                                ])
                                            ),
                                            new LiteralInteger(0)
                                        )
                                    )
                                ),
                                new SelectItemGroup(
                                    "high",
                                    new IfDecimalGroup(
                                        new KeyDecimal("high"),
                                        new DivideDecimalGroup([
                                            new AddDecimalGroup([
                                                new KeyDecimal("low"),
                                                new KeyDecimal("high"),
                                            ]),
                                            new LiteralInteger(2),
                                        ]),
                                        new GreaterThanDecimalGroup(
                                            new SumDecimalGroup(
                                                new DivideDecimalRow([
                                                    new FieldDecimal(
                                                        "cash_flows",
                                                        "amount"
                                                    ),
                                                    new PowerDecimalRow(
                                                        new AddDecimalRow([
                                                            new LiteralInteger(1),
                                                            new DivideDecimalRow([
                                                                new AddDecimalRow([
                                                                    new FieldDecimal(
                                                                        "bounds",
                                                                        "low"
                                                                    ),
                                                                    new FieldDecimal(
                                                                        "bounds",
                                                                        "high"
                                                                    ),
                                                                ]),
                                                                new LiteralInteger(2),
                                                            ]),
                                                        ]),
                                                        new FieldInteger(
                                                            "cash_flows",
                                                            "period"
                                                        )
                                                    ),
                                                ])
                                            ),
                                            new LiteralInteger(0)
                                        )
                                    )
                                ),
                            ]
                        ),
                        50
                    )
                ),
            ],
            from: new FromSubquery("bounds"),
            groupBy:
            [
                new GroupKey("investment_id", new FieldUuid("bounds", "investment_id")),
            ],
            select:
            [
                new SelectItemGroup("investment_id", new KeyUuid("investment_id")),
                new SelectItemGroup(
                    "low",
                    new MaxDecimalGroup(new FieldDecimal("bounds", "low"))
                ),
                new SelectItemGroup(
                    "high",
                    new MinDecimalGroup(new FieldDecimal("bounds", "high"))
                ),
                new SelectItemGroup(
                    "steps",
                    new MaxIntegerGroup(new FieldInteger("bounds", "step"))
                ),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and
    /// <see cref="AuditSchemaDataSet"/>, in no particular order.
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
                    new Column(new String("low"), new DoubleColumnType()),
                    new Column(new String("high"), new DoubleColumnType()),
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
                                            "000007d1-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("low"), new DoubleColumnType()),
                                new InvariantCell(new Double(0.093974609375))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("high"), new DoubleColumnType()),
                                new InvariantCell(new Double(0.0946453857421875))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("steps"), new LongColumnType()),
                                new InvariantCell(new Long(14))
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
                                            "000007d2-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("low"), new DoubleColumnType()),
                                new InvariantCell(new Double(0.1026947021484375))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("high"), new DoubleColumnType()),
                                new InvariantCell(new Double(0.103365478515625))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("steps"), new LongColumnType()),
                                new InvariantCell(new Long(14))
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
                                            "000007d3-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("low"), new DoubleColumnType()),
                                new InvariantCell(new Double(0.23349609375))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("high"), new DoubleColumnType()),
                                new InvariantCell(new Double(0.2341668701171875))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("steps"), new LongColumnType()),
                                new InvariantCell(new Long(14))
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
