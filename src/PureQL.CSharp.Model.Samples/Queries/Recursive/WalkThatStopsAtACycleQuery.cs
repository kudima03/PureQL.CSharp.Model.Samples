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
using PureQL.CSharp.Model.ProjectionExpressions;
using PureQL.CSharp.Model.RowExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Recursive;

/// <summary>
/// Selects node, steps and path of a walk over edges written as inline rows, ordered by
/// steps and path. edges holds the rows (from, to) of ("a", "b"), ("b", "c"), ("c", "a")
/// and ("c", "d"), which contain a cycle; start holds the one row node "a". walk seeds
/// with the node of start, steps 0 and the path "/a/", and, level by level (maxDepth 100),
/// inner joins edges on from equal to the node, keeping the edges whose "/" + to + "/" is
/// not contained in the path, and selects to as node, steps + 1 and the path extended with
/// to and "/", so the walk ends at the cycle by itself.
/// </summary>
public sealed record WalkThatStopsAtACycleQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithoutPagination(
            subqueries:
            [
                new SubqueryRows(
                    "edges",
                    [
                        [
                            new RowItem("from", new LiteralString("a")),
                            new RowItem("to", new LiteralString("b")),
                        ],
                        [
                            new RowItem("from", new LiteralString("b")),
                            new RowItem("to", new LiteralString("c")),
                        ],
                        [
                            new RowItem("from", new LiteralString("c")),
                            new RowItem("to", new LiteralString("a")),
                        ],
                        [
                            new RowItem("from", new LiteralString("c")),
                            new RowItem("to", new LiteralString("d")),
                        ],
                    ]
                ),
                new SubqueryRows(
                    "start",
                    [
                        [new RowItem("node", new LiteralString("a"))],
                    ]
                ),
                new SubqueryRecursive(
                    "walk",
                    new Recursion(
                        new PlainQueryWithoutPaginationWithoutOrderBy(
                            from: new FromSubquery("start"),
                            select:
                            [
                                new SelectItemProjection(
                                    "node",
                                    new FieldString("start", "node")
                                ),
                                new SelectItemProjection("steps", new LiteralInteger(0)),
                                new SelectItemProjection(
                                    "path",
                                    new ConcatStringProjection([
                                        new LiteralString("/"),
                                        new FieldString("start", "node"),
                                        new LiteralString("/"),
                                    ])
                                ),
                            ]
                        ),
                        new PlainNextQuery(
                            from: new FromSubquery("walk"),
                            joins:
                            [
                                new JoinSubquery(
                                    JoinKind.Inner,
                                    "edges",
                                    new EqualStringRow(
                                        new FieldString("edges", "from"),
                                        new FieldString("walk", "node")
                                    )
                                ),
                            ],
                            where: new NotRow(
                                new ContainsRow(
                                    new FieldString("walk", "path"),
                                    new ConcatStringRow([
                                        new LiteralString("/"),
                                        new FieldString("edges", "to"),
                                        new LiteralString("/"),
                                    ])
                                )
                            ),
                            select:
                            [
                                new SelectItemRow("node", new FieldString("edges", "to")),
                                new SelectItemRow(
                                    "steps",
                                    new AddIntegerRow([
                                        new FieldInteger("walk", "steps"),
                                        new LiteralInteger(1),
                                    ])
                                ),
                                new SelectItemRow(
                                    "path",
                                    new ConcatStringRow([
                                        new FieldString("walk", "path"),
                                        new FieldString("edges", "to"),
                                        new LiteralString("/"),
                                    ])
                                ),
                            ]
                        ),
                        100
                    )
                ),
            ],
            from: new FromSubquery("walk"),
            select:
            [
                new SelectItemProjection("node", new FieldString("walk", "node")),
                new SelectItemProjection("steps", new FieldInteger("walk", "steps")),
                new SelectItemProjection("path", new FieldString("walk", "path")),
            ],
            orderBy:
            [
                new OrderItemProjectionExpression(new FieldInteger("walk", "steps")),
                new OrderItemProjectionExpression(new FieldString("walk", "path")),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics, in this order.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("node"), new StringColumnType()),
                    new Column(new String("steps"), new LongColumnType()),
                    new Column(new String("path"), new StringColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("node"), new StringColumnType()),
                                new InvariantCell(new String("a"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("steps"), new LongColumnType()),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("path"), new StringColumnType()),
                                new InvariantCell(new String("/a/"))
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
                                new Column(new String("node"), new StringColumnType()),
                                new InvariantCell(new String("b"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("steps"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("path"), new StringColumnType()),
                                new InvariantCell(new String("/a/b/"))
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
                                new Column(new String("node"), new StringColumnType()),
                                new InvariantCell(new String("c"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("steps"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("path"), new StringColumnType()),
                                new InvariantCell(new String("/a/b/c/"))
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
                                new Column(new String("node"), new StringColumnType()),
                                new InvariantCell(new String("d"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("steps"), new LongColumnType()),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("path"), new StringColumnType()),
                                new InvariantCell(new String("/a/b/c/d/"))
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
