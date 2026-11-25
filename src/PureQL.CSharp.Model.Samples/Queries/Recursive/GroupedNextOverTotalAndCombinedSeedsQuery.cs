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
using PureQL.CSharp.Model.TotalExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Recursive;

/// <summary>
/// Selects the balances of three recursive subqueries side by side: an inline rows
/// subquery cash_flows holds the period and amount of four cash flows; a recursive
/// subquery totals is seeded with a total query (step 0 and the sum of amount as balance)
/// and its grouped next level joins cash_flows on period greater than step, groups by
/// step and selects step plus 1 and the sum of amount (maxDepth 5); a recursive subquery
/// from_a_union is seeded with a unionAll of the period and amount of cash_flows and the
/// step and balance of totals, and its grouped next level, where period is below 12,
/// groups by period plus 1, keeps the groups whose sum of amount is above 0 and selects
/// the period and half of that sum (maxDepth 12); a recursive subquery without_aggregates
/// is seeded with the step of totals and its plain next level, where step is below 3,
/// selects step plus 1 (maxDepth 3). The main query selects the step and balance of
/// totals left joined with from_a_union on period equal to step and with
/// without_aggregates on step equal to step, with the amount and the plain_step.
/// </summary>
public sealed record GroupedNextOverTotalAndCombinedSeedsQuery
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
                            new RowItem("period", new LiteralInteger(0)),
                            new RowItem("amount", new LiteralDecimal(-1000m)),
                        ],
                        [
                            new RowItem("period", new LiteralInteger(1)),
                            new RowItem("amount", new LiteralDecimal(300m)),
                        ],
                        [
                            new RowItem("period", new LiteralInteger(2)),
                            new RowItem("amount", new LiteralDecimal(420m)),
                        ],
                        [
                            new RowItem("period", new LiteralInteger(3)),
                            new RowItem("amount", new LiteralDecimal(680m)),
                        ],
                    ]
                ),
                new SubqueryRecursive(
                    "totals",
                    new Recursion(
                        new TotalQuery(
                            from: new FromSubquery("cash_flows"),
                            aggregate:
                            [
                                new SelectItemTotal("step", new LiteralInteger(0)),
                                new SelectItemTotal(
                                    "balance",
                                    new SumDecimalTotal(
                                        new FieldDecimal("cash_flows", "amount")
                                    )
                                ),
                            ]
                        ),
                        new GroupedNextQuery(
                            from: new FromSubquery("totals"),
                            joins:
                            [
                                new JoinSubquery(
                                    JoinKind.Inner,
                                    "cash_flows",
                                    new GreaterThanDecimalRow(
                                        new FieldInteger("cash_flows", "period"),
                                        new FieldInteger("totals", "step")
                                    )
                                ),
                            ],
                            groupBy:
                            [
                                new GroupKey("step", new FieldInteger("totals", "step")),
                            ],
                            select:
                            [
                                new SelectItemGroup(
                                    "step",
                                    new AddIntegerGroup([
                                        new KeyInteger("step"),
                                        new LiteralInteger(1),
                                    ])
                                ),
                                new SelectItemGroup(
                                    "balance",
                                    new SumDecimalGroup(
                                        new FieldDecimal("cash_flows", "amount")
                                    )
                                ),
                            ]
                        ),
                        5
                    )
                ),
                new SubqueryRecursive(
                    "from_a_union",
                    new Recursion(
                        new CombinedQueryUnionAll([
                            new PlainQueryWithoutPaginationWithoutOrderBy(
                                from: new FromSubquery("cash_flows"),
                                select:
                                [
                                    new SelectItemProjection(
                                        "period",
                                        new FieldInteger("cash_flows", "period")
                                    ),
                                    new SelectItemProjection(
                                        "amount",
                                        new FieldDecimal("cash_flows", "amount")
                                    ),
                                ]
                            ),
                            new PlainQueryWithoutPaginationWithoutOrderBy(
                                from: new FromSubquery("totals"),
                                select:
                                [
                                    new SelectItemProjection(
                                        "period",
                                        new FieldInteger("totals", "step")
                                    ),
                                    new SelectItemProjection(
                                        "amount",
                                        new FieldDecimal("totals", "balance")
                                    ),
                                ]
                            ),
                        ]),
                        new GroupedNextQuery(
                            from: new FromSubquery("from_a_union"),
                            where: new LessThanDecimalRow(
                                new FieldInteger("from_a_union", "period"),
                                new LiteralInteger(12)
                            ),
                            groupBy:
                            [
                                new GroupKey(
                                    "period",
                                    new AddIntegerRow([
                                        new FieldInteger("from_a_union", "period"),
                                        new LiteralInteger(1),
                                    ])
                                ),
                            ],
                            having: new GreaterThanDecimalGroup(
                                new SumDecimalGroup(
                                    new FieldDecimal("from_a_union", "amount")
                                ),
                                new LiteralInteger(0)
                            ),
                            select:
                            [
                                new SelectItemGroup("period", new KeyInteger("period")),
                                new SelectItemGroup(
                                    "amount",
                                    new DivideDecimalGroup([
                                        new SumDecimalGroup(
                                            new FieldDecimal("from_a_union", "amount")
                                        ),
                                        new LiteralInteger(2),
                                    ])
                                ),
                            ]
                        ),
                        12
                    )
                ),
                new SubqueryRecursive(
                    "without_aggregates",
                    new Recursion(
                        new PlainQueryWithoutPaginationWithoutOrderBy(
                            from: new FromSubquery("totals"),
                            select:
                            [
                                new SelectItemProjection(
                                    "step",
                                    new FieldInteger("totals", "step")
                                ),
                            ]
                        ),
                        new PlainNextQuery(
                            from: new FromSubquery("without_aggregates"),
                            where: new LessThanDecimalRow(
                                new FieldInteger("without_aggregates", "step"),
                                new LiteralInteger(3)
                            ),
                            select:
                            [
                                new SelectItemRow(
                                    "step",
                                    new AddIntegerRow([
                                        new FieldInteger("without_aggregates", "step"),
                                        new LiteralInteger(1),
                                    ])
                                ),
                            ]
                        ),
                        3
                    )
                ),
            ],
            from: new FromSubquery("totals"),
            joins:
            [
                new JoinSubquery(
                    JoinKind.Left,
                    "from_a_union",
                    new EqualDecimalRow(
                        new FieldInteger("from_a_union", "period"),
                        new FieldInteger("totals", "step")
                    )
                ),
                new JoinSubquery(
                    JoinKind.Left,
                    "without_aggregates",
                    new EqualDecimalRow(
                        new FieldInteger("without_aggregates", "step"),
                        new FieldInteger("totals", "step")
                    )
                ),
            ],
            select:
            [
                new SelectItemProjection("step", new FieldInteger("totals", "step")),
                new SelectItemProjection(
                    "balance",
                    new FieldDecimal("totals", "balance")
                ),
                new SelectItemProjection(
                    "amount",
                    new FieldDecimalNullable("from_a_union", "amount")
                ),
                new SelectItemProjection(
                    "plain_step",
                    new FieldIntegerNullable("without_aggregates", "step")
                ),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and
    /// <see cref="AuditSchemaDataSet"/>, in no particular order.
    /// The levels of the grouped recursive subqueries are computed one at a time, as
    /// PostgreSQL forbids aggregates in a recursive term, and checked against exact decimal
    /// arithmetic rounded as the specification rounds.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("step"), new LongColumnType()),
                    new Column(new String("balance"), new DoubleColumnType()),
                    new Column(new String("amount"), new DoubleColumnType()),
                    new Column(new String("plain_step"), new LongColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("step"), new LongColumnType()),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("balance"), new DoubleColumnType()),
                                new InvariantCell(new Double(400.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("amount"), new DoubleColumnType()),
                                new InvariantCell(new Double(-1000.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("plain_step"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
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
                                new Column(new String("step"), new LongColumnType()),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("balance"), new DoubleColumnType()),
                                new InvariantCell(new Double(400.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("amount"), new DoubleColumnType()),
                                new InvariantCell(new Double(400.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("plain_step"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
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
                                new Column(new String("step"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("balance"), new DoubleColumnType()),
                                new InvariantCell(new Double(1400.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("amount"), new DoubleColumnType()),
                                new InvariantCell(new Double(300.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("plain_step"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
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
                                new Column(new String("step"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("balance"), new DoubleColumnType()),
                                new InvariantCell(new Double(1400.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("amount"), new DoubleColumnType()),
                                new InvariantCell(new Double(300.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("plain_step"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
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
                                new Column(new String("step"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("balance"), new DoubleColumnType()),
                                new InvariantCell(new Double(1400.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("amount"), new DoubleColumnType()),
                                new InvariantCell(new Double(1400.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("plain_step"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
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
                                new Column(new String("step"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("balance"), new DoubleColumnType()),
                                new InvariantCell(new Double(1400.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("amount"), new DoubleColumnType()),
                                new InvariantCell(new Double(1400.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("plain_step"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
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
                                new Column(new String("step"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("balance"), new DoubleColumnType()),
                                new InvariantCell(new Double(1100.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("amount"), new DoubleColumnType()),
                                new InvariantCell(new Double(420.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("plain_step"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
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
                                new Column(new String("step"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("balance"), new DoubleColumnType()),
                                new InvariantCell(new Double(1100.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("amount"), new DoubleColumnType()),
                                new InvariantCell(new Double(420.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("plain_step"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
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
                                new Column(new String("step"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("balance"), new DoubleColumnType()),
                                new InvariantCell(new Double(1100.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("amount"), new DoubleColumnType()),
                                new InvariantCell(new Double(420.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("plain_step"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
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
                                new Column(new String("step"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("balance"), new DoubleColumnType()),
                                new InvariantCell(new Double(1100.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("amount"), new DoubleColumnType()),
                                new InvariantCell(new Double(850.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("plain_step"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
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
                                new Column(new String("step"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("balance"), new DoubleColumnType()),
                                new InvariantCell(new Double(1100.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("amount"), new DoubleColumnType()),
                                new InvariantCell(new Double(850.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("plain_step"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
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
                                new Column(new String("step"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("balance"), new DoubleColumnType()),
                                new InvariantCell(new Double(1100.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("amount"), new DoubleColumnType()),
                                new InvariantCell(new Double(850.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("plain_step"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
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
                                new Column(new String("step"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("balance"), new DoubleColumnType()),
                                new InvariantCell(new Double(1100.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("amount"), new DoubleColumnType()),
                                new InvariantCell(new Double(1100.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("plain_step"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
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
                                new Column(new String("step"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("balance"), new DoubleColumnType()),
                                new InvariantCell(new Double(1100.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("amount"), new DoubleColumnType()),
                                new InvariantCell(new Double(1100.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("plain_step"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
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
                                new Column(new String("step"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("balance"), new DoubleColumnType()),
                                new InvariantCell(new Double(1100.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("amount"), new DoubleColumnType()),
                                new InvariantCell(new Double(1100.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("plain_step"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
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
                                new Column(new String("step"), new LongColumnType()),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("balance"), new DoubleColumnType()),
                                new InvariantCell(new Double(680.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("amount"), new DoubleColumnType()),
                                new InvariantCell(new Double(425.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("plain_step"),
                                    new LongColumnType()
                                ),
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
                                new Column(new String("step"), new LongColumnType()),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("balance"), new DoubleColumnType()),
                                new InvariantCell(new Double(680.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("amount"), new DoubleColumnType()),
                                new InvariantCell(new Double(425.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("plain_step"),
                                    new LongColumnType()
                                ),
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
                                new Column(new String("step"), new LongColumnType()),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("balance"), new DoubleColumnType()),
                                new InvariantCell(new Double(680.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("amount"), new DoubleColumnType()),
                                new InvariantCell(new Double(425.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("plain_step"),
                                    new LongColumnType()
                                ),
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
                                new Column(new String("step"), new LongColumnType()),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("balance"), new DoubleColumnType()),
                                new InvariantCell(new Double(680.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("amount"), new DoubleColumnType()),
                                new InvariantCell(new Double(425.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("plain_step"),
                                    new LongColumnType()
                                ),
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
                                new Column(new String("step"), new LongColumnType()),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("balance"), new DoubleColumnType()),
                                new InvariantCell(new Double(680.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("amount"), new DoubleColumnType()),
                                new InvariantCell(new Double(680.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("plain_step"),
                                    new LongColumnType()
                                ),
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
                                new Column(new String("step"), new LongColumnType()),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("balance"), new DoubleColumnType()),
                                new InvariantCell(new Double(680.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("amount"), new DoubleColumnType()),
                                new InvariantCell(new Double(680.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("plain_step"),
                                    new LongColumnType()
                                ),
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
                                new Column(new String("step"), new LongColumnType()),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("balance"), new DoubleColumnType()),
                                new InvariantCell(new Double(680.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("amount"), new DoubleColumnType()),
                                new InvariantCell(new Double(680.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("plain_step"),
                                    new LongColumnType()
                                ),
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
                                new Column(new String("step"), new LongColumnType()),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("balance"), new DoubleColumnType()),
                                new InvariantCell(new Double(680.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("amount"), new DoubleColumnType()),
                                new InvariantCell(new Double(680.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("plain_step"),
                                    new LongColumnType()
                                ),
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
                                new Column(new String("step"), new LongColumnType()),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("balance"), new DoubleColumnType()),
                                new InvariantCell(new Double(680.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("amount"), new DoubleColumnType()),
                                new InvariantCell(new Double(680.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("plain_step"),
                                    new LongColumnType()
                                ),
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
                                new Column(new String("step"), new LongColumnType()),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("balance"), new DoubleColumnType()),
                                new InvariantCell(new Double(680.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("amount"), new DoubleColumnType()),
                                new InvariantCell(new Double(680.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("plain_step"),
                                    new LongColumnType()
                                ),
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
                                new Column(new String("step"), new LongColumnType()),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("balance"), new DoubleColumnType()),
                                new InvariantCell(new Double(680.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("amount"), new DoubleColumnType()),
                                new InvariantCell(new Double(680.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("plain_step"),
                                    new LongColumnType()
                                ),
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
                                new Column(new String("step"), new LongColumnType()),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("balance"), new DoubleColumnType()),
                                new InvariantCell(new Double(680.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("amount"), new DoubleColumnType()),
                                new InvariantCell(new Double(680.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("plain_step"),
                                    new LongColumnType()
                                ),
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
                                new Column(new String("step"), new LongColumnType()),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("balance"), new DoubleColumnType()),
                                new InvariantCell(new Double(680.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("amount"), new DoubleColumnType()),
                                new InvariantCell(new Double(760.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("plain_step"),
                                    new LongColumnType()
                                ),
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
                                new Column(new String("step"), new LongColumnType()),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("balance"), new DoubleColumnType()),
                                new InvariantCell(new Double(680.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("amount"), new DoubleColumnType()),
                                new InvariantCell(new Double(760.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("plain_step"),
                                    new LongColumnType()
                                ),
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
                                new Column(new String("step"), new LongColumnType()),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("balance"), new DoubleColumnType()),
                                new InvariantCell(new Double(680.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("amount"), new DoubleColumnType()),
                                new InvariantCell(new Double(760.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("plain_step"),
                                    new LongColumnType()
                                ),
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
                                new Column(new String("step"), new LongColumnType()),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("balance"), new DoubleColumnType()),
                                new InvariantCell(new Double(680.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("amount"), new DoubleColumnType()),
                                new InvariantCell(new Double(760.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("plain_step"),
                                    new LongColumnType()
                                ),
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
