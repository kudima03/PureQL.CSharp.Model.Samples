using Pure.Collections.Generic;
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
using PureQL.CSharp.Model.GroupExpressions;
using PureQL.CSharp.Model.Keys;
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.RowExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.GroupBy;

/// <summary>
/// Selects, from a subquery people of inline rows (country, a nickname that is null for
/// some, and last_name; eight rows whose spellings differ in case), the groups with the
/// same case-folded country and case-folded nickname, nullable, as the keys country and
/// nickname that have at least one row whose country differs from its case-folded form
/// (the count with that predicate above 0). It selects both keys, the count as users, the
/// distinct spellings of country joined by a comma in order, the min of the case-folded
/// last_name, a label of the upper-cased key country, a colon and the count as text, and
/// the nickname key coalesced with the case-folded country key, ordered by the count
/// descending, the key country and the key nickname.
/// </summary>
public sealed record GroupingWithoutCaseQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainGroupedQueryWithoutPagination(
            subqueries:
            [
                new SubqueryRows(
                    "people",
                    [
                        [
                            new RowItem("country", new LiteralString("DE")),
                            new RowItem("nickname", new LiteralString("Ace")),
                            new RowItem("last_name", new LiteralString("Meyer")),
                        ],
                        [
                            new RowItem("country", new LiteralString("de")),
                            new RowItem("nickname", new LiteralString("ACE")),
                            new RowItem("last_name", new LiteralString("meyer")),
                        ],
                        [
                            new RowItem("country", new LiteralString("De")),
                            new RowItem("nickname", new LiteralStringNullable()),
                            new RowItem("last_name", new LiteralString("Meier")),
                        ],
                        [
                            new RowItem("country", new LiteralString("de")),
                            new RowItem("nickname", new LiteralStringNullable()),
                            new RowItem("last_name", new LiteralString("MEIER")),
                        ],
                        [
                            new RowItem("country", new LiteralString("DE")),
                            new RowItem("nickname", new LiteralStringNullable()),
                            new RowItem("last_name", new LiteralString("Maier")),
                        ],
                        [
                            new RowItem("country", new LiteralString("FR")),
                            new RowItem("nickname", new LiteralString("Zed")),
                            new RowItem("last_name", new LiteralString("Stra\u00DFe")),
                        ],
                        [
                            new RowItem("country", new LiteralString("fr")),
                            new RowItem("nickname", new LiteralString("zed")),
                            new RowItem("last_name", new LiteralString("STRASSE")),
                        ],
                        [
                            new RowItem("country", new LiteralString("us")),
                            new RowItem("nickname", new LiteralString("Max")),
                            new RowItem("last_name", new LiteralString("Smith")),
                        ],
                    ]
                ),
            ],
            from: new FromSubquery("people"),
            groupBy:
            [
                new GroupKey(
                    "country",
                    new FoldCaseStringRow(new FieldString("people", "country"))
                ),
                new GroupKey(
                    "nickname",
                    new FoldCaseStringNullableRow(
                        new FieldStringNullable("people", "nickname")
                    )
                ),
            ],
            having: new GreaterThanDecimalGroup(
                new CountGroup(
                    predicate: new NotEqualStringRow(
                        new FieldString("people", "country"),
                        new FoldCaseStringRow(new FieldString("people", "country"))
                    )
                ),
                new LiteralInteger(0)
            ),
            select:
            [
                new SelectItemGroup("country", new KeyString("country")),
                new SelectItemGroup("nickname", new KeyStringNullable("nickname")),
                new SelectItemGroup("users", new CountGroup()),
                new SelectItemGroup(
                    "spellings",
                    new ConcatAllGroup(
                        new FieldString("people", "country"),
                        new LiteralString(", "),
                        [new OrderItemRow(new FieldString("people", "country"))],
                        distinct: true
                    )
                ),
                new SelectItemGroup(
                    "first_last_name",
                    new MinStringGroup(
                        new FoldCaseStringRow(new FieldString("people", "last_name"))
                    )
                ),
                new SelectItemGroup(
                    "label",
                    new ConcatStringGroup([
                        new UpperStringGroup(new KeyString("country")),
                        new LiteralString(": "),
                        new AsStringDecimalStringGroup(new CountGroup()),
                    ])
                ),
                new SelectItemGroup(
                    "nickname_or_country",
                    new CoalesceStringGroup([
                        new KeyStringNullable("nickname"),
                        new FoldCaseStringGroup(new KeyString("country")),
                    ])
                ),
            ],
            orderBy:
            [
                new OrderItemGroupExpression(new CountGroup(), SortDirection.Desc),
                new OrderItemGroupExpression(new KeyString("country")),
                new OrderItemGroupExpression(new KeyStringNullable("nickname")),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics, in this
    /// order. foldCase is computed with Python str.casefold, which PostgreSQL 17 does not
    /// have (the sharp s folds to ss).
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("country"), new StringColumnType()),
                    new Column(new String("nickname"), new StringColumnType()),
                    new Column(new String("users"), new LongColumnType()),
                    new Column(new String("spellings"), new StringColumnType()),
                    new Column(new String("first_last_name"), new StringColumnType()),
                    new Column(new String("label"), new StringColumnType()),
                    new Column(new String("nickname_or_country"), new StringColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("country"), new StringColumnType()),
                                new InvariantCell(new String("de"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nickname"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("users"), new LongColumnType()),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("spellings"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("DE, De, de"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_last_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("maier"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("label"), new StringColumnType()),
                                new InvariantCell(new String("DE: 3"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nickname_or_country"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("de"))
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
                                new Column(new String("country"), new StringColumnType()),
                                new InvariantCell(new String("de"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nickname"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("ace"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("users"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("spellings"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("DE, de"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_last_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("meyer"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("label"), new StringColumnType()),
                                new InvariantCell(new String("DE: 2"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nickname_or_country"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("ace"))
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
                                new Column(new String("country"), new StringColumnType()),
                                new InvariantCell(new String("fr"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nickname"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("zed"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("users"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("spellings"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("FR, fr"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_last_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("strasse"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("label"), new StringColumnType()),
                                new InvariantCell(new String("FR: 2"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nickname_or_country"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("zed"))
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
