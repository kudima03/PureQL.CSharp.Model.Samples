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
using PureQL.CSharp.Model.RowExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Subqueries;

/// <summary>
/// Selects, per row of a subquery statuses of inline rows (pending ranked 1 and labelled
/// "Awaiting shipment", shipped ranked 2 and labelled "Shipped", cancelled ranked 3 with a
/// null label, refunded ranked 4 and labelled "Refunded") left joined to
/// schema_with_foreign_keys.orders on order_status, grouped by rank, status and label: the
/// label, or the status where the label is null, as status; the count of the joined
/// order_id values that are not null as orders; and the sum of the joined order_total as
/// revenue; ordered by rank.
/// </summary>
public sealed record OrdersPerListedStatusQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainGroupedQueryWithoutPagination(
            subqueries:
            [
                new SubqueryRows(
                    "statuses",
                    [
                        [
                            new RowItem("status", new LiteralString("pending")),
                            new RowItem("rank", new LiteralInteger(1)),
                            new RowItem("label", new LiteralString("Awaiting shipment")),
                        ],
                        [
                            new RowItem("status", new LiteralString("shipped")),
                            new RowItem("rank", new LiteralInteger(2)),
                            new RowItem("label", new LiteralString("Shipped")),
                        ],
                        [
                            new RowItem("status", new LiteralString("cancelled")),
                            new RowItem("rank", new LiteralInteger(3)),
                            new RowItem("label", new LiteralStringNullable()),
                        ],
                        [
                            new RowItem("status", new LiteralString("refunded")),
                            new RowItem("rank", new LiteralInteger(4)),
                            new RowItem("label", new LiteralString("Refunded")),
                        ],
                    ]
                ),
            ],
            from: new FromSubquery("statuses"),
            joins:
            [
                new JoinEntity(
                    JoinKind.Left,
                    new JoinedString(
                        new DotString(),
                        [
                            new RelationalSchemaWithForeignKeys().Name,
                            new OrdersTable().Name,
                        ]
                    ).TextValue,
                    new EqualStringRow(
                        new FieldString("statuses", "status"),
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
                    )
                ),
            ],
            groupBy:
            [
                new GroupKey("rank", new FieldInteger("statuses", "rank")),
                new GroupKey("status", new FieldString("statuses", "status")),
                new GroupKey("label", new FieldStringNullable("statuses", "label")),
            ],
            select:
            [
                new SelectItemGroup(
                    "status",
                    new CoalesceStringGroup([
                        new KeyStringNullable("label"),
                        new KeyString("status"),
                    ])
                ),
                new SelectItemGroup(
                    "orders",
                    new CountGroup(
                        new NotEqualUuidRow(
                            new FieldUuidNullable(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new OrdersTable().Name,
                                    ]
                                ).TextValue,
                                new OrderIdColumn().Name.TextValue
                            ),
                            new LiteralUuidNullable()
                        )
                    )
                ),
                new SelectItemGroup(
                    "revenue",
                    new SumDecimalGroup(
                        new FieldDecimalNullable(
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
            ],
            orderBy: [new OrderItemGroupExpression(new KeyInteger("rank"))]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over <see
    /// cref="SchemaDataSetWithForeignKeys"/> and <see cref="AuditSchemaDataSet"/>, in this
    /// order.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("status"), new StringColumnType()),
                    new Column(new String("orders"), new LongColumnType()),
                    new Column(new String("revenue"), new DoubleColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("status"), new StringColumnType()),
                                new InvariantCell(new String("Awaiting shipment"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("revenue"), new DoubleColumnType()),
                                new InvariantCell(new Double(150.5))
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
                                new Column(new String("status"), new StringColumnType()),
                                new InvariantCell(new String("Shipped"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("revenue"), new DoubleColumnType()),
                                new InvariantCell(new Double(600.5))
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
                                new Column(new String("status"), new StringColumnType()),
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
                                new Column(new String("status"), new StringColumnType()),
                                new InvariantCell(new String("Refunded"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("revenue"), new DoubleColumnType()),
                                new InvariantCell(new Double(0))
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
