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
/// Selects, from a subquery people of inline rows (last_name, and a nickname that is null
/// for some; seven rows written with other code points), the groups with the same
/// last_name case-folded and normalized to NFKC and the same nickname normalized to NFC,
/// nullable, as the keys last_name and nickname, that have more than one row. It selects
/// both keys, the count as users, the distinct last_names in the form NFC joined by " | "
/// in order, the count of the rows whose last_name differs from its NFC form, the max
/// length of the last_name in the form NFD, and the key last_name in the form NFD,
/// ordered by the count descending, the key last_name and the key nickname.
/// </summary>
public sealed record GroupingByNormalizedKeysQuery
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
                            new RowItem("last_name", new LiteralString("M\u00FCller")),
                            new RowItem("nickname", new LiteralString("Jos\u00E9")),
                        ],
                        [
                            new RowItem("last_name", new LiteralString("Mu\u0308ller")),
                            new RowItem("nickname", new LiteralString("Jose\u0301")),
                        ],
                        [
                            new RowItem("last_name", new LiteralString("M\u00DCLLER")),
                            new RowItem("nickname", new LiteralStringNullable()),
                        ],
                        [
                            new RowItem(
                                "last_name",
                                new LiteralString("\uFF2D\u00DCLLER")
                            ),
                            new RowItem("nickname", new LiteralStringNullable()),
                        ],
                        [
                            new RowItem("last_name", new LiteralString("Stra\u00DFe")),
                            new RowItem("nickname", new LiteralStringNullable()),
                        ],
                        [
                            new RowItem("last_name", new LiteralString("STRASSE")),
                            new RowItem("nickname", new LiteralStringNullable()),
                        ],
                        [
                            new RowItem("last_name", new LiteralString("Smith")),
                            new RowItem("nickname", new LiteralString("Zed")),
                        ],
                    ]
                ),
            ],
            from: new FromSubquery("people"),
            groupBy:
            [
                new GroupKey(
                    "last_name",
                    new NormalizeStringRow(
                        new FoldCaseStringRow(new FieldString("people", "last_name")),
                        NormalizationForm.NFKC
                    )
                ),
                new GroupKey(
                    "nickname",
                    new NormalizeStringNullableRow(
                        new FieldStringNullable("people", "nickname"),
                        NormalizationForm.NFC
                    )
                ),
            ],
            having: new GreaterThanDecimalGroup(new CountGroup(), new LiteralInteger(1)),
            select:
            [
                new SelectItemGroup("last_name", new KeyString("last_name")),
                new SelectItemGroup("nickname", new KeyStringNullable("nickname")),
                new SelectItemGroup("users", new CountGroup()),
                new SelectItemGroup(
                    "spellings",
                    new ConcatAllGroup(
                        new NormalizeStringRow(
                            new FieldString("people", "last_name"),
                            NormalizationForm.NFC
                        ),
                        new LiteralString(" | "),
                        [
                            new OrderItemRow(
                                new NormalizeStringRow(
                                    new FieldString("people", "last_name"),
                                    NormalizationForm.NFC
                                )
                            ),
                        ],
                        distinct: true
                    )
                ),
                new SelectItemGroup(
                    "stored_decomposed",
                    new CountGroup(
                        predicate: new NotEqualStringRow(
                            new FieldString("people", "last_name"),
                            new NormalizeStringRow(
                                new FieldString("people", "last_name"),
                                NormalizationForm.NFC
                            )
                        )
                    )
                ),
                new SelectItemGroup(
                    "longest_decomposed",
                    new MaxIntegerGroup(
                        new LengthIntegerRow(
                            new NormalizeStringRow(
                                new FieldString("people", "last_name"),
                                NormalizationForm.NFD
                            )
                        )
                    )
                ),
                new SelectItemGroup(
                    "key_decomposed",
                    new NormalizeStringGroup(
                        new KeyString("last_name"),
                        NormalizationForm.NFD
                    )
                ),
            ],
            orderBy:
            [
                new OrderItemGroupExpression(new CountGroup(), SortDirection.Desc),
                new OrderItemGroupExpression(new KeyString("last_name")),
                new OrderItemGroupExpression(new KeyStringNullable("nickname")),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics, in this
    /// order. foldCase is computed with Python str.casefold, which PostgreSQL 17 does not
    /// have; normalize is checked with Python unicodedata.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("last_name"), new StringColumnType()),
                    new Column(new String("nickname"), new StringColumnType()),
                    new Column(new String("users"), new LongColumnType()),
                    new Column(new String("spellings"), new StringColumnType()),
                    new Column(new String("stored_decomposed"), new LongColumnType()),
                    new Column(new String("longest_decomposed"), new LongColumnType()),
                    new Column(new String("key_decomposed"), new StringColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("m\u00FCller"))
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
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("spellings"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(
                                    new String("M\u00DCLLER | \uFF2D\u00DCLLER")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("stored_decomposed"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("longest_decomposed"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(7))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("key_decomposed"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("mu\u0308ller"))
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
                                    new String("last_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("m\u00FCller"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nickname"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Jos\u00E9"))
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
                                new InvariantCell(new String("M\u00FCller"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("stored_decomposed"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("longest_decomposed"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(7))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("key_decomposed"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("mu\u0308ller"))
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
                                    new String("last_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("strasse"))
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
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("spellings"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("STRASSE | Stra\u00DFe"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("stored_decomposed"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("longest_decomposed"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(7))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("key_decomposed"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("strasse"))
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
