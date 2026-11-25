using Pure.Collections.Generic;
using Pure.Primitives.Bool;
using Pure.Primitives.Number;
using Pure.Primitives.String;
using Pure.RelationalSchema.Abstractions.Column;
using Pure.RelationalSchema.ColumnType;
using Pure.RelationalSchema.HashCodes;
using Pure.RelationalSchema.Storage;
using Pure.RelationalSchema.Storage.Abstractions;
using Pure.RelationalSchema.Storage.Samples.Cells;
using Pure.RelationalSchema.Storage.Samples.TableDataSets;
using PureQL.CSharp.Model.Fields;
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.RowExpressions;
using PureQL.CSharp.Model.TotalExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Aggregates;

/// <summary>
/// Selects, in a total query over a subquery people of inline rows (country, tier,
/// last_name and a nickname that is null for some, six rows), from the rows whose
/// case-folded country differs from the case-folded literal FR: the count of the rows
/// whose case-folded tier equals gold, the case-folded min of last_name, the min of the
/// case-folded last_name, the distinct case-folded tiers joined by a comma in order,
/// whether any case-folded last_name starts with mc, the mode of the case-folded country,
/// and the count of the rows whose case-folded nickname differs from a null string.
/// </summary>
public sealed record FoldCaseInATotalQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainTotalQuery(
            subqueries:
            [
                new SubqueryRows(
                    "people",
                    [
                        [
                            new RowItem("country", new LiteralString("DE")),
                            new RowItem("tier", new LiteralString("Gold")),
                            new RowItem("last_name", new LiteralString("McCoy")),
                            new RowItem("nickname", new LiteralString("Mac")),
                        ],
                        [
                            new RowItem("country", new LiteralString("de")),
                            new RowItem("tier", new LiteralString("GOLD")),
                            new RowItem("last_name", new LiteralString("Meyer")),
                            new RowItem("nickname", new LiteralStringNullable()),
                        ],
                        [
                            new RowItem("country", new LiteralString("De")),
                            new RowItem("tier", new LiteralString("Silver")),
                            new RowItem("last_name", new LiteralString("brandt")),
                            new RowItem("nickname", new LiteralStringNullable()),
                        ],
                        [
                            new RowItem("country", new LiteralString("FR")),
                            new RowItem("tier", new LiteralString("gold")),
                            new RowItem("last_name", new LiteralString("Dupont")),
                            new RowItem("nickname", new LiteralString("Zed")),
                        ],
                        [
                            new RowItem("country", new LiteralString("US")),
                            new RowItem("tier", new LiteralString("Bronze")),
                            new RowItem("last_name", new LiteralString("Zimmer")),
                            new RowItem("nickname", new LiteralString("Zim")),
                        ],
                        [
                            new RowItem("country", new LiteralString("fr")),
                            new RowItem("tier", new LiteralString("silver")),
                            new RowItem("last_name", new LiteralString("Martin")),
                            new RowItem("nickname", new LiteralStringNullable()),
                        ],
                    ]
                ),
            ],
            from: new FromSubquery("people"),
            where: new NotEqualStringRow(
                new FoldCaseStringRow(new FieldString("people", "country")),
                new FoldCaseStringRow(new LiteralString("FR"))
            ),
            aggregate:
            [
                new SelectItemTotal(
                    "gold",
                    new CountTotal(
                        predicate: new EqualStringRow(
                            new FoldCaseStringRow(new FieldString("people", "tier")),
                            new LiteralString("gold")
                        )
                    )
                ),
                new SelectItemTotal(
                    "first_last_name",
                    new FoldCaseStringNullableTotal(
                        new MinStringNullableTotal(new FieldString("people", "last_name"))
                    )
                ),
                new SelectItemTotal(
                    "first_folded_last_name",
                    new MinStringNullableTotal(
                        new FoldCaseStringRow(new FieldString("people", "last_name"))
                    )
                ),
                new SelectItemTotal(
                    "tiers",
                    new ConcatAllTotal(
                        new FoldCaseStringRow(new FieldString("people", "tier")),
                        new LiteralString(", "),
                        [
                            new OrderItemRow(
                                new FoldCaseStringRow(new FieldString("people", "tier"))
                            ),
                        ],
                        distinct: true
                    )
                ),
                new SelectItemTotal(
                    "any_mc",
                    new AnyTotal(
                        new StartsWithRow(
                            new FoldCaseStringRow(new FieldString("people", "last_name")),
                            new LiteralString("mc")
                        )
                    )
                ),
                new SelectItemTotal(
                    "most_common_country",
                    new ModeStringNullableTotal(
                        new FoldCaseStringRow(new FieldString("people", "country"))
                    )
                ),
                new SelectItemTotal(
                    "nicknames",
                    new CountTotal(
                        predicate: new NotEqualStringRow(
                            new FoldCaseStringNullableRow(
                                new FieldStringNullable("people", "nickname")
                            ),
                            new LiteralStringNullable()
                        )
                    )
                ),
            ]
        );

    /// <summary>
    /// The row the query returns under the PureQL specification's semantics. foldCase is
    /// computed with Python str.casefold, which PostgreSQL 17 does not have.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("gold"), new LongColumnType()),
                    new Column(new String("first_last_name"), new StringColumnType()),
                    new Column(
                        new String("first_folded_last_name"),
                        new StringColumnType()
                    ),
                    new Column(new String("tiers"), new StringColumnType()),
                    new Column(new String("any_mc"), new BoolColumnType()),
                    new Column(new String("most_common_country"), new StringColumnType()),
                    new Column(new String("nicknames"), new LongColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("gold"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_last_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("mccoy"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_folded_last_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("brandt"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("tiers"), new StringColumnType()),
                                new InvariantCell(new String("bronze, gold, silver"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("any_mc"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("most_common_country"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("de"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("nicknames"), new LongColumnType()),
                                new InvariantCell(new Long(2))
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
