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
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.RowExpressions;
using PureQL.CSharp.Model.TotalExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Joins;

/// <summary>
/// Aggregates the rows of a subquery urgency (urgent, not urgent) inner joined on true to a
/// subquery importance (important, not important), both of inline rows, into one row: the
/// count of rows as quadrants, and as names the urgency level and the importance level of
/// each row joined with " and ", all joined with "; " in the order of the urgency level
/// descending, then of the importance level.
/// </summary>
public sealed record QuadrantsOfTwoInlineListsQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainTotalQuery(
            subqueries:
            [
                new SubqueryRows(
                    "urgency",
                    [
                        [new RowItem("level", new LiteralString("urgent"))],
                        [new RowItem("level", new LiteralString("not urgent"))],
                    ]
                ),
                new SubqueryRows(
                    "importance",
                    [
                        [new RowItem("level", new LiteralString("important"))],
                        [new RowItem("level", new LiteralString("not important"))],
                    ]
                ),
            ],
            from: new FromSubquery("urgency"),
            joins:
            [
                new JoinSubquery(JoinKind.Inner, "importance", new LiteralBoolean(true)),
            ],
            aggregate:
            [
                new SelectItemTotal("quadrants", new CountTotal()),
                new SelectItemTotal(
                    "names",
                    new ConcatAllTotal(
                        new ConcatWithRow(
                            [
                                new FieldString("urgency", "level"),
                                new FieldString("importance", "level"),
                            ],
                            new LiteralString(" and ")
                        ),
                        new Separator(new LiteralString("; ")),
                        [
                            new OrderItemRow(
                                new FieldString("urgency", "level"),
                                SortDirection.Desc
                            ),
                            new OrderItemRow(new FieldString("importance", "level")),
                        ]
                    )
                ),
            ]
        );

    /// <summary>
    /// The row the query returns under the PureQL specification's semantics, which reads no
    /// stored data set.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("quadrants"), new LongColumnType()),
                    new Column(new String("names"), new StringColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("quadrants"), new LongColumnType()),
                                new InvariantCell(new Long(4))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("names"), new StringColumnType()),
                                new InvariantCell(
                                    new String(
                                        "urgent and important; urgent and not important; not urgent and important; not urgent and not important"
                                    )
                                )
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
