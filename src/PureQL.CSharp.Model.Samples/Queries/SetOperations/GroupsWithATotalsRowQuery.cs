using Pure.Collections.Generic;
using Pure.Primitives.Number;
using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Abstractions.Column;
using Pure.RelationalSchema.ColumnType;
using Pure.RelationalSchema.HashCodes;
using Pure.RelationalSchema.Samples.Columns;
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
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
using PureQL.CSharp.Model.TotalExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.SetOperations;

/// <summary>
/// Selects label, orders, revenue and the average (revenue divided by orders, null for no
/// orders) from a subquery report that is the unionAll of the orders of each order_status
/// of schema_with_foreign_keys.orders (the status as label, count and the sum of
/// order_total as revenue) and one total row over all of its orders (the label "all",
/// count and the same sum), ordered by whether label equals "all", then by label, so that
/// the totals row comes last.
/// </summary>
public sealed record GroupsWithATotalsRowQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithoutPagination(
            subqueries:
            [
                new SubqueryQuery(
                    "report",
                    new CombinedQueryUnionAll(
                        unionAll:
                        [
                            new GroupedQueryWithoutPaginationWithoutOrderBy(
                                from: new FromEntity(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new OrdersTable().Name,
                                        ]
                                    ).TextValue
                                ),
                                groupBy:
                                [
                                    new GroupKey(
                                        "status",
                                        new FieldString(
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new RelationalSchemaWithForeignKeys().Name,
                                                    new OrdersTable().Name,
                                                ]
                                            ).TextValue,
                                            new OrderStatusColumn().Name.TextValue
                                        )
                                    ),
                                ],
                                select:
                                [
                                    new SelectItemGroup("label", new KeyString("status")),
                                    new SelectItemGroup("orders", new CountGroup()),
                                    new SelectItemGroup(
                                        "revenue",
                                        new SumDecimalGroup(
                                            new FieldDecimal(
                                                new JoinedString(
                                                    new DotString(),
                                                    [
                                                        new RelationalSchemaWithForeignKeys().Name,
                                                        new OrdersTable().Name,
                                                    ]
                                                ).TextValue,
                                                new OrderTotalColumn().Name.TextValue
                                            )
                                        )
                                    ),
                                ]
                            ),
                            new TotalQuery(
                                from: new FromEntity(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new OrdersTable().Name,
                                        ]
                                    ).TextValue
                                ),
                                aggregate:
                                [
                                    new SelectItemTotal(
                                        "label",
                                        new LiteralString("all")
                                    ),
                                    new SelectItemTotal("orders", new CountTotal()),
                                    new SelectItemTotal(
                                        "revenue",
                                        new SumDecimalTotal(
                                            new FieldDecimal(
                                                new JoinedString(
                                                    new DotString(),
                                                    [
                                                        new RelationalSchemaWithForeignKeys().Name,
                                                        new OrdersTable().Name,
                                                    ]
                                                ).TextValue,
                                                new OrderTotalColumn().Name.TextValue
                                            )
                                        )
                                    ),
                                ]
                            ),
                        ]
                    )
                ),
            ],
            from: new FromSubquery("report"),
            select:
            [
                new SelectItemProjection("label", new FieldString("report", "label")),
                new SelectItemProjection("orders", new FieldInteger("report", "orders")),
                new SelectItemProjection(
                    "revenue",
                    new FieldDecimal("report", "revenue")
                ),
                new SelectItemProjection(
                    "average",
                    new DivideDecimalNullableProjection([
                        new FieldDecimal("report", "revenue"),
                        new NullIfEqualIntegerNullableProjection(
                            new FieldInteger("report", "orders"),
                            new LiteralInteger(0)
                        ),
                    ])
                ),
            ],
            orderBy:
            [
                new OrderItemProjectionExpression(
                    new EqualStringProjection(
                        new FieldString("report", "label"),
                        new LiteralString("all")
                    )
                ),
                new OrderItemProjectionExpression(new FieldString("report", "label")),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and
    /// <see cref="AuditSchemaDataSet"/>, in this order.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("label"), new StringColumnType()),
                    new Column(new String("orders"), new LongColumnType()),
                    new Column(new String("revenue"), new DoubleColumnType()),
                    new Column(new String("average"), new DoubleColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("label"), new StringColumnType()),
                                new InvariantCell(new String("cancelled"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("revenue"), new DoubleColumnType()),
                                new InvariantCell(new Double(75.25))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("average"), new DoubleColumnType()),
                                new InvariantCell(new Double(75.25))
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
                                new Column(new String("label"), new StringColumnType()),
                                new InvariantCell(new String("pending"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("revenue"), new DoubleColumnType()),
                                new InvariantCell(new Double(150.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("average"), new DoubleColumnType()),
                                new InvariantCell(new Double(75.25))
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
                                new Column(new String("label"), new StringColumnType()),
                                new InvariantCell(new String("shipped"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("revenue"), new DoubleColumnType()),
                                new InvariantCell(new Double(600.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("average"), new DoubleColumnType()),
                                new InvariantCell(new Double(200.16666666666666))
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
                                new Column(new String("label"), new StringColumnType()),
                                new InvariantCell(new String("all"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(6))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("revenue"), new DoubleColumnType()),
                                new InvariantCell(new Double(826.25))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("average"), new DoubleColumnType()),
                                new InvariantCell(new Double(137.70833333333334))
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
