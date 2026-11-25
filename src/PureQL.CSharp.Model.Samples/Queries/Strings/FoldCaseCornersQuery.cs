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
using PureQL.CSharp.Model.ProjectionExpressions;
using PureQL.CSharp.Model.RowExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Strings;

/// <summary>
/// Selects the corners of foldCase over a subquery people of inline rows (id, first_name,
/// last_name, a nickname that is null for some, email and birth_date of six people):
/// first_name case-folded twice, the case-folded upper of first_name, whether the lower
/// of last_name equals its case-folded form, the case-folded literal Strasse with a sharp
/// s, the case-folded null string, the case-folded concat of first_name, a space and
/// last_name, the case-folded nickname coalesced with the case-folded first_name, the
/// length of the case-folded last_name minus the length of last_name, the position of the
/// case-folded SS in the case-folded last_name, the case-folded last_name without it
/// (replace), the part after the at sign of the case-folded email (splitPart),
/// organisation or other by whether the case-folded email ends with .org (if), the
/// case-folded birth_date as text, the rowNumber and the count over a window partitioned
/// by the case-folded last_name (the rowNumber ordered by the case-folded first_name and
/// id), and the previous last_name in the order of the case-folded last_name, last_name
/// and id. The rows are ordered by the case-folded last_name and id.
/// </summary>
public sealed record FoldCaseCornersQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithoutPagination(
            subqueries:
            [
                new SubqueryRows(
                    "people",
                    [
                        [
                            new RowItem(
                                "id",
                                new LiteralUuid(new Guid(1, 0, 0, new byte[8]))
                            ),
                            new RowItem("first_name", new LiteralString("Ann")),
                            new RowItem("last_name", new LiteralString("Stra\u00DFe")),
                            new RowItem("nickname", new LiteralString("Annie")),
                            new RowItem("email", new LiteralString("ann@Example.ORG")),
                            new RowItem(
                                "birth_date",
                                new LiteralDate(new DateOnly(2000, 1, 15))
                            ),
                        ],
                        [
                            new RowItem(
                                "id",
                                new LiteralUuid(new Guid(2, 0, 0, new byte[8]))
                            ),
                            new RowItem("first_name", new LiteralString("Bo\u00DF")),
                            new RowItem("last_name", new LiteralString("STRASSE")),
                            new RowItem("nickname", new LiteralStringNullable()),
                            new RowItem("email", new LiteralString("Bob@example.com")),
                            new RowItem(
                                "birth_date",
                                new LiteralDate(new DateOnly(1999, 3, 20))
                            ),
                        ],
                        [
                            new RowItem(
                                "id",
                                new LiteralUuid(new Guid(3, 0, 0, new byte[8]))
                            ),
                            new RowItem("first_name", new LiteralString("Cara")),
                            new RowItem("last_name", new LiteralString("M\u00FCller")),
                            new RowItem("nickname", new LiteralString("CC")),
                            new RowItem("email", new LiteralString("CARA@EXAMPLE.org")),
                            new RowItem(
                                "birth_date",
                                new LiteralDate(new DateOnly(1985, 7, 10))
                            ),
                        ],
                        [
                            new RowItem(
                                "id",
                                new LiteralUuid(new Guid(4, 0, 0, new byte[8]))
                            ),
                            new RowItem("first_name", new LiteralString("dan")),
                            new RowItem("last_name", new LiteralString("M\u00DCLLER")),
                            new RowItem("nickname", new LiteralStringNullable()),
                            new RowItem("email", new LiteralString("dan@Example.com")),
                            new RowItem(
                                "birth_date",
                                new LiteralDate(new DateOnly(1990, 11, 5))
                            ),
                        ],
                        [
                            new RowItem(
                                "id",
                                new LiteralUuid(new Guid(5, 0, 0, new byte[8]))
                            ),
                            new RowItem("first_name", new LiteralString("Eve")),
                            new RowItem("last_name", new LiteralString("McCoy")),
                            new RowItem("nickname", new LiteralString("EVE")),
                            new RowItem("email", new LiteralString("Eve@Example.Org")),
                            new RowItem(
                                "birth_date",
                                new LiteralDate(new DateOnly(1995, 2, 28))
                            ),
                        ],
                        [
                            new RowItem(
                                "id",
                                new LiteralUuid(new Guid(6, 0, 0, new byte[8]))
                            ),
                            new RowItem("first_name", new LiteralString("Fay")),
                            new RowItem("last_name", new LiteralString("mccoy")),
                            new RowItem("nickname", new LiteralStringNullable()),
                            new RowItem("email", new LiteralString("fay@example.COM")),
                            new RowItem(
                                "birth_date",
                                new LiteralDate(new DateOnly(1988, 12, 31))
                            ),
                        ],
                    ]
                ),
            ],
            from: new FromSubquery("people"),
            select:
            [
                new SelectItemProjection(
                    "folded_twice",
                    new FoldCaseStringProjection(
                        new FoldCaseStringProjection(
                            new FieldString("people", "first_name")
                        )
                    )
                ),
                new SelectItemProjection(
                    "fold_of_upper",
                    new FoldCaseStringProjection(
                        new UpperStringProjection(new FieldString("people", "first_name"))
                    )
                ),
                new SelectItemProjection(
                    "lower_is_already_folded",
                    new EqualStringProjection(
                        new LowerStringProjection(new FieldString("people", "last_name")),
                        new FoldCaseStringProjection(
                            new FieldString("people", "last_name")
                        )
                    )
                ),
                new SelectItemProjection(
                    "of_a_literal",
                    new FoldCaseStringProjection(new LiteralString("Stra\u00DFe"))
                ),
                new SelectItemProjection(
                    "of_a_nullable_parameter",
                    new FoldCaseStringNullableProjection(new LiteralStringNullable())
                ),
                new SelectItemProjection(
                    "of_a_concat",
                    new FoldCaseStringProjection(
                        new ConcatStringProjection([
                            new FieldString("people", "first_name"),
                            new LiteralString(" "),
                            new FieldString("people", "last_name"),
                        ])
                    )
                ),
                new SelectItemProjection(
                    "first_folded_name",
                    new CoalesceStringProjection([
                        new FoldCaseStringNullableProjection(
                            new FieldStringNullable("people", "nickname")
                        ),
                        new FoldCaseStringProjection(
                            new FieldString("people", "first_name")
                        ),
                    ])
                ),
                new SelectItemProjection(
                    "length_may_change",
                    new SubtractIntegerProjection([
                        new LengthIntegerProjection(
                            new FoldCaseStringProjection(
                                new FieldString("people", "last_name")
                            )
                        ),
                        new LengthIntegerProjection(
                            new FieldString("people", "last_name")
                        ),
                    ])
                ),
                new SelectItemProjection(
                    "position_of_needle",
                    new PositionIntegerProjection(
                        new FoldCaseStringProjection(
                            new FieldString("people", "last_name")
                        ),
                        new FoldCaseStringProjection(new LiteralString("SS"))
                    )
                ),
                new SelectItemProjection(
                    "without_needle",
                    new ReplaceStringProjection(
                        new FoldCaseStringProjection(
                            new FieldString("people", "last_name")
                        ),
                        new FoldCaseStringProjection(new LiteralString("SS")),
                        new LiteralString("")
                    )
                ),
                new SelectItemProjection(
                    "email_domain",
                    new SplitPartStringProjection(
                        new FoldCaseStringProjection(new FieldString("people", "email")),
                        new LiteralString("@"),
                        new LiteralInteger(2)
                    )
                ),
                new SelectItemProjection(
                    "kind",
                    new IfStringProjection(
                        new LiteralString("organisation"),
                        new LiteralString("other"),
                        new EndsWithProjection(
                            new FoldCaseStringProjection(
                                new FieldString("people", "email")
                            ),
                            new LiteralString(".org")
                        )
                    )
                ),
                new SelectItemProjection(
                    "birth_date_key",
                    new FoldCaseStringProjection(
                        new AsStringDateStringProjection(
                            new FieldDate("people", "birth_date")
                        )
                    )
                ),
                new SelectItemProjection(
                    "position_among_namesakes",
                    new RowNumberProjection(
                        new OrderedWindow(
                            [
                                new OrderItemRow(
                                    new FoldCaseStringRow(
                                        new FieldString("people", "first_name")
                                    )
                                ),
                                new OrderItemRow(new FieldUuid("people", "id")),
                            ],
                            partitionBy:
                            [
                                new FoldCaseStringRow(
                                    new FieldString("people", "last_name")
                                ),
                            ]
                        )
                    )
                ),
                new SelectItemProjection(
                    "namesakes",
                    new CountProjection(
                        over: new WindowWithoutFrameWithoutRange([
                            new FoldCaseStringRow(new FieldString("people", "last_name")),
                        ])
                    )
                ),
                new SelectItemProjection(
                    "previous_in_order_without_case",
                    new PreviousStringNullableProjection(
                        new FieldString("people", "last_name"),
                        new OrderedWindow([
                            new OrderItemRow(
                                new FoldCaseStringRow(
                                    new FieldString("people", "last_name")
                                )
                            ),
                            new OrderItemRow(new FieldString("people", "last_name")),
                            new OrderItemRow(new FieldUuid("people", "id")),
                        ])
                    )
                ),
            ],
            orderBy:
            [
                new OrderItemProjectionExpression(
                    new FoldCaseStringProjection(new FieldString("people", "last_name"))
                ),
                new OrderItemProjectionExpression(new FieldUuid("people", "id")),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics, in this
    /// order. foldCase, upper and lower are computed with Python str.casefold, str.upper
    /// and str.lower (the full case mappings: the sharp s folds to ss and upper-cases to
    /// SS), which PostgreSQL 17 does not have.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("folded_twice"), new StringColumnType()),
                    new Column(new String("fold_of_upper"), new StringColumnType()),
                    new Column(
                        new String("lower_is_already_folded"),
                        new BoolColumnType()
                    ),
                    new Column(new String("of_a_literal"), new StringColumnType()),
                    new Column(
                        new String("of_a_nullable_parameter"),
                        new StringColumnType()
                    ),
                    new Column(new String("of_a_concat"), new StringColumnType()),
                    new Column(new String("first_folded_name"), new StringColumnType()),
                    new Column(new String("length_may_change"), new LongColumnType()),
                    new Column(new String("position_of_needle"), new LongColumnType()),
                    new Column(new String("without_needle"), new StringColumnType()),
                    new Column(new String("email_domain"), new StringColumnType()),
                    new Column(new String("kind"), new StringColumnType()),
                    new Column(new String("birth_date_key"), new StringColumnType()),
                    new Column(
                        new String("position_among_namesakes"),
                        new LongColumnType()
                    ),
                    new Column(new String("namesakes"), new LongColumnType()),
                    new Column(
                        new String("previous_in_order_without_case"),
                        new StringColumnType()
                    ),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("folded_twice"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("eve"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("fold_of_upper"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("eve"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lower_is_already_folded"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("strasse"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_nullable_parameter"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_concat"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("eve mccoy"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_folded_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("eve"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("length_may_change"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_of_needle"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("without_needle"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("mccoy"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("email_domain"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("example.org"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("kind"), new StringColumnType()),
                                new InvariantCell(new String("organisation"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("birth_date_key"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("1995-02-28"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_among_namesakes"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("namesakes"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("previous_in_order_without_case"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
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
                                    new String("folded_twice"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("fay"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("fold_of_upper"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("fay"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lower_is_already_folded"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("strasse"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_nullable_parameter"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_concat"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("fay mccoy"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_folded_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("fay"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("length_may_change"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_of_needle"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("without_needle"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("mccoy"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("email_domain"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("example.com"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("kind"), new StringColumnType()),
                                new InvariantCell(new String("other"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("birth_date_key"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("1988-12-31"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_among_namesakes"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("namesakes"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("previous_in_order_without_case"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("McCoy"))
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
                                    new String("folded_twice"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("cara"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("fold_of_upper"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("cara"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lower_is_already_folded"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("strasse"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_nullable_parameter"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_concat"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("cara m\u00FCller"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_folded_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("cc"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("length_may_change"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_of_needle"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("without_needle"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("m\u00FCller"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("email_domain"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("example.org"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("kind"), new StringColumnType()),
                                new InvariantCell(new String("organisation"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("birth_date_key"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("1985-07-10"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_among_namesakes"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("namesakes"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("previous_in_order_without_case"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("M\u00DCLLER"))
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
                                    new String("folded_twice"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("dan"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("fold_of_upper"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("dan"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lower_is_already_folded"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("strasse"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_nullable_parameter"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_concat"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("dan m\u00FCller"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_folded_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("dan"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("length_may_change"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_of_needle"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("without_needle"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("m\u00FCller"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("email_domain"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("example.com"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("kind"), new StringColumnType()),
                                new InvariantCell(new String("other"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("birth_date_key"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("1990-11-05"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_among_namesakes"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("namesakes"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("previous_in_order_without_case"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("mccoy"))
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
                                    new String("folded_twice"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("ann"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("fold_of_upper"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("ann"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lower_is_already_folded"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("strasse"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_nullable_parameter"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_concat"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("ann strasse"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_folded_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("annie"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("length_may_change"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_of_needle"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("without_needle"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("strae"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("email_domain"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("example.org"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("kind"), new StringColumnType()),
                                new InvariantCell(new String("organisation"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("birth_date_key"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("2000-01-15"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_among_namesakes"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("namesakes"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("previous_in_order_without_case"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("STRASSE"))
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
                                    new String("folded_twice"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("boss"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("fold_of_upper"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("boss"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lower_is_already_folded"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("strasse"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_nullable_parameter"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_concat"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("boss strasse"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_folded_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("boss"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("length_may_change"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_of_needle"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("without_needle"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("strae"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("email_domain"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("example.com"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("kind"), new StringColumnType()),
                                new InvariantCell(new String("other"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("birth_date_key"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("1999-03-20"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_among_namesakes"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("namesakes"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("previous_in_order_without_case"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("M\u00FCller"))
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
