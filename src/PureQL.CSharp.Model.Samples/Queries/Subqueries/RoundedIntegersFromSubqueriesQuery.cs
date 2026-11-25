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
using PureQL.CSharp.Model.ProjectionExpressions;
using PureQL.CSharp.Model.RowExpressions;
using PureQL.CSharp.Model.TotalExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using Guid = Pure.Primitives.Guid.Guid;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Subqueries;

/// <summary>
/// Selects, from an inline rows subquery lines holding the id, order_id, product_id,
/// quantity and returned_quantity of fifteen lines, the rounded integers of three
/// subqueries read as integers: bands groups lines by the quantity truncated to tens,
/// keeps the groups whose sum of quantity rounded to hundreds is at least 100 and selects
/// the band, the next band, units_in_hundreds, rounded_units, largest_rounded,
/// lines_in_tens, lines_of_all_in_tens, rounded_bands and the average rounded to tens;
/// totals is a total query with the count rounded to hundreds, the sum of quantity
/// truncated to thousands, the max rounded to tens, the median rounded to tens and the
/// sum of quantity truncated to tens as rounded_units; steps is a recursive subquery
/// seeded with the id and quantity of every line whose next level, while units modulo 10
/// is above 0, truncates them to tens (maxDepth 3). The main query reads lines inner
/// joined with bands on the quantity truncated to tens equal to quantity_band, with
/// totals on true, and left joined with steps on id equal to the line and units equal to
/// the quantity rounded to tens. It selects the id, running_units_in_tens (the sum of
/// quantity over a window partitioned by product_id and ordered by id with a frame
/// preceding all, rounded to tens), rank_in_tens (the row number over id truncated to
/// tens), share_of_rounded_units (the quantity rounded to tens divided by nullIfEqual of
/// rounded_units and 0), band_units (units_in_hundreds of bands) and step_units, ordered
/// by the id column.
/// </summary>
public sealed record RoundedIntegersFromSubqueriesQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithoutPagination(
            subqueries:
            [
                new SubqueryRows(
                    "lines",
                    [
                        [
                            new RowItem(
                                "id",
                                new LiteralUuid(new System.Guid(801, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new System.Guid(101, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "product_id",
                                new LiteralUuid(new System.Guid(201, 0, 0, new byte[8]))
                            ),
                            new RowItem("quantity", new LiteralInteger(4)),
                            new RowItem("returned_quantity", new LiteralInteger(1)),
                        ],
                        [
                            new RowItem(
                                "id",
                                new LiteralUuid(new System.Guid(802, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new System.Guid(102, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "product_id",
                                new LiteralUuid(new System.Guid(202, 0, 0, new byte[8]))
                            ),
                            new RowItem("quantity", new LiteralInteger(15)),
                            new RowItem("returned_quantity", new LiteralInteger(3)),
                        ],
                        [
                            new RowItem(
                                "id",
                                new LiteralUuid(new System.Guid(803, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new System.Guid(103, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "product_id",
                                new LiteralUuid(new System.Guid(203, 0, 0, new byte[8]))
                            ),
                            new RowItem("quantity", new LiteralInteger(18)),
                            new RowItem("returned_quantity", new LiteralInteger(2)),
                        ],
                        [
                            new RowItem(
                                "id",
                                new LiteralUuid(new System.Guid(804, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new System.Guid(104, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "product_id",
                                new LiteralUuid(new System.Guid(204, 0, 0, new byte[8]))
                            ),
                            new RowItem("quantity", new LiteralInteger(25)),
                            new RowItem("returned_quantity", new LiteralInteger(0)),
                        ],
                        [
                            new RowItem(
                                "id",
                                new LiteralUuid(new System.Guid(805, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new System.Guid(105, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "product_id",
                                new LiteralUuid(new System.Guid(201, 0, 0, new byte[8]))
                            ),
                            new RowItem("quantity", new LiteralInteger(61)),
                            new RowItem("returned_quantity", new LiteralInteger(4)),
                        ],
                        [
                            new RowItem(
                                "id",
                                new LiteralUuid(new System.Guid(806, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new System.Guid(106, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "product_id",
                                new LiteralUuid(new System.Guid(202, 0, 0, new byte[8]))
                            ),
                            new RowItem("quantity", new LiteralInteger(62)),
                            new RowItem("returned_quantity", new LiteralInteger(1)),
                        ],
                        [
                            new RowItem(
                                "id",
                                new LiteralUuid(new System.Guid(807, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new System.Guid(101, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "product_id",
                                new LiteralUuid(new System.Guid(203, 0, 0, new byte[8]))
                            ),
                            new RowItem("quantity", new LiteralInteger(65)),
                            new RowItem("returned_quantity", new LiteralInteger(6)),
                        ],
                        [
                            new RowItem(
                                "id",
                                new LiteralUuid(new System.Guid(808, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new System.Guid(102, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "product_id",
                                new LiteralUuid(new System.Guid(204, 0, 0, new byte[8]))
                            ),
                            new RowItem("quantity", new LiteralInteger(66)),
                            new RowItem("returned_quantity", new LiteralInteger(2)),
                        ],
                        [
                            new RowItem(
                                "id",
                                new LiteralUuid(new System.Guid(809, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new System.Guid(103, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "product_id",
                                new LiteralUuid(new System.Guid(201, 0, 0, new byte[8]))
                            ),
                            new RowItem("quantity", new LiteralInteger(68)),
                            new RowItem("returned_quantity", new LiteralInteger(0)),
                        ],
                        [
                            new RowItem(
                                "id",
                                new LiteralUuid(new System.Guid(810, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new System.Guid(104, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "product_id",
                                new LiteralUuid(new System.Guid(202, 0, 0, new byte[8]))
                            ),
                            new RowItem("quantity", new LiteralInteger(100)),
                            new RowItem("returned_quantity", new LiteralInteger(7)),
                        ],
                        [
                            new RowItem(
                                "id",
                                new LiteralUuid(new System.Guid(811, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new System.Guid(105, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "product_id",
                                new LiteralUuid(new System.Guid(203, 0, 0, new byte[8]))
                            ),
                            new RowItem("quantity", new LiteralInteger(104)),
                            new RowItem("returned_quantity", new LiteralInteger(3)),
                        ],
                        [
                            new RowItem(
                                "id",
                                new LiteralUuid(new System.Guid(812, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new System.Guid(106, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "product_id",
                                new LiteralUuid(new System.Guid(204, 0, 0, new byte[8]))
                            ),
                            new RowItem("quantity", new LiteralInteger(107)),
                            new RowItem("returned_quantity", new LiteralInteger(8)),
                        ],
                        [
                            new RowItem(
                                "id",
                                new LiteralUuid(new System.Guid(813, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new System.Guid(101, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "product_id",
                                new LiteralUuid(new System.Guid(201, 0, 0, new byte[8]))
                            ),
                            new RowItem("quantity", new LiteralInteger(145)),
                            new RowItem("returned_quantity", new LiteralInteger(12)),
                        ],
                        [
                            new RowItem(
                                "id",
                                new LiteralUuid(new System.Guid(814, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new System.Guid(102, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "product_id",
                                new LiteralUuid(new System.Guid(202, 0, 0, new byte[8]))
                            ),
                            new RowItem("quantity", new LiteralInteger(149)),
                            new RowItem("returned_quantity", new LiteralInteger(9)),
                        ],
                        [
                            new RowItem(
                                "id",
                                new LiteralUuid(new System.Guid(815, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new System.Guid(103, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "product_id",
                                new LiteralUuid(new System.Guid(203, 0, 0, new byte[8]))
                            ),
                            new RowItem("quantity", new LiteralInteger(260)),
                            new RowItem("returned_quantity", new LiteralInteger(30)),
                        ],
                    ]
                ),
                new SubqueryQuery(
                    "bands",
                    new GroupedQueryWithoutPaginationWithoutOrderBy(
                        from: new FromSubquery("lines"),
                        groupBy:
                        [
                            new GroupKey(
                                "quantity_band",
                                new TruncateWithDigitsIntegerRow(
                                    new FieldInteger("lines", "quantity"),
                                    new LiteralInteger(-1)
                                )
                            ),
                        ],
                        having: new GreaterThanOrEqualDecimalGroup(
                            new RoundWithDigitsIntegerGroup(
                                new SumIntegerGroup(
                                    new FieldInteger("lines", "quantity")
                                ),
                                new LiteralInteger(-2)
                            ),
                            new LiteralInteger(100)
                        ),
                        select:
                        [
                            new SelectItemGroup(
                                "quantity_band",
                                new KeyInteger("quantity_band")
                            ),
                            new SelectItemGroup(
                                "next_band",
                                new AddIntegerGroup([
                                    new KeyInteger("quantity_band"),
                                    new LiteralInteger(10),
                                ])
                            ),
                            new SelectItemGroup(
                                "units_in_hundreds",
                                new RoundWithDigitsIntegerGroup(
                                    new SumIntegerGroup(
                                        new FieldInteger("lines", "quantity")
                                    ),
                                    new LiteralInteger(-2)
                                )
                            ),
                            new SelectItemGroup(
                                "rounded_units",
                                new SumIntegerGroup(
                                    new RoundWithDigitsIntegerRow(
                                        new FieldInteger("lines", "quantity"),
                                        new LiteralInteger(-1)
                                    )
                                )
                            ),
                            new SelectItemGroup(
                                "largest_rounded",
                                new MaxIntegerGroup(
                                    new RoundWithDigitsIntegerRow(
                                        new FieldInteger("lines", "quantity"),
                                        new LiteralInteger(-1)
                                    )
                                )
                            ),
                            new SelectItemGroup(
                                "lines_in_tens",
                                new RoundWithDigitsIntegerGroup(
                                    new CountGroup(),
                                    new LiteralInteger(-1)
                                )
                            ),
                            new SelectItemGroup(
                                "lines_of_all_in_tens",
                                new RoundWithDigitsIntegerGroup(
                                    new CountGroup(over: AggregateOver.All),
                                    new LiteralInteger(-1)
                                )
                            ),
                            new SelectItemGroup(
                                "rounded_bands",
                                new CountDistinctGroup(
                                    new RoundWithDigitsIntegerRow(
                                        new FieldInteger("lines", "quantity"),
                                        new LiteralInteger(-2)
                                    )
                                )
                            ),
                            new SelectItemGroup(
                                "average_stays_decimal",
                                new RoundDecimalGroup(
                                    new AverageDecimalGroup(
                                        new FieldInteger("lines", "quantity")
                                    ),
                                    new LiteralInteger(-1)
                                )
                            ),
                        ]
                    )
                ),
                new SubqueryQuery(
                    "totals",
                    new TotalQuery(
                        from: new FromSubquery("lines"),
                        aggregate:
                        [
                            new SelectItemTotal(
                                "lines_in_hundreds",
                                new RoundWithDigitsIntegerTotal(
                                    new CountTotal(),
                                    new LiteralInteger(-2)
                                )
                            ),
                            new SelectItemTotal(
                                "units_in_thousands",
                                new TruncateWithDigitsIntegerTotal(
                                    new SumIntegerTotal(
                                        new FieldInteger("lines", "quantity")
                                    ),
                                    new LiteralInteger(-3)
                                )
                            ),
                            new SelectItemTotal(
                                "largest_in_tens",
                                new RoundWithDigitsIntegerNullableTotal(
                                    new MaxIntegerNullableTotal(
                                        new FieldInteger("lines", "quantity")
                                    ),
                                    new LiteralInteger(-1)
                                )
                            ),
                            new SelectItemTotal(
                                "median_stays_decimal",
                                new RoundDecimalNullableTotal(
                                    new MedianDecimalNullableTotal(
                                        new FieldInteger("lines", "quantity")
                                    ),
                                    new LiteralInteger(-1)
                                )
                            ),
                            new SelectItemTotal(
                                "rounded_units",
                                new SumIntegerTotal(
                                    new TruncateWithDigitsIntegerRow(
                                        new FieldInteger("lines", "quantity"),
                                        new LiteralInteger(-1)
                                    )
                                )
                            ),
                        ]
                    )
                ),
                new SubqueryRecursive(
                    "steps",
                    new Recursion(
                        new PlainQueryWithoutPaginationWithoutOrderBy(
                            from: new FromSubquery("lines"),
                            select:
                            [
                                new SelectItemProjection(
                                    "id",
                                    new FieldUuid("lines", "id")
                                ),
                                new SelectItemProjection(
                                    "units",
                                    new FieldInteger("lines", "quantity")
                                ),
                            ]
                        ),
                        new PlainNextQuery(
                            from: new FromSubquery("steps"),
                            where: new GreaterThanDecimalRow(
                                new ModuloIntegerRow(
                                    new FieldInteger("steps", "units"),
                                    new LiteralInteger(10)
                                ),
                                new LiteralInteger(0)
                            ),
                            select:
                            [
                                new SelectItemRow("id", new FieldUuid("steps", "id")),
                                new SelectItemRow(
                                    "units",
                                    new TruncateWithDigitsIntegerRow(
                                        new FieldInteger("steps", "units"),
                                        new LiteralInteger(-1)
                                    )
                                ),
                            ]
                        ),
                        3
                    )
                ),
            ],
            from: new FromSubquery("lines"),
            joins:
            [
                new JoinSubquery(
                    JoinKind.Inner,
                    "bands",
                    new EqualDecimalRow(
                        new TruncateWithDigitsIntegerRow(
                            new FieldInteger("lines", "quantity"),
                            new LiteralInteger(-1)
                        ),
                        new FieldInteger("bands", "quantity_band")
                    )
                ),
                new JoinSubquery(JoinKind.Inner, "totals", new LiteralBoolean(true)),
                new JoinSubquery(
                    JoinKind.Left,
                    "steps",
                    new AndRow([
                        new EqualUuidRow(
                            new FieldUuid("steps", "id"),
                            new FieldUuid("lines", "id")
                        ),
                        new EqualDecimalRow(
                            new FieldInteger("steps", "units"),
                            new RoundWithDigitsIntegerRow(
                                new FieldInteger("lines", "quantity"),
                                new LiteralInteger(-1)
                            )
                        ),
                    ])
                ),
            ],
            select:
            [
                new SelectItemProjection("id", new FieldUuid("lines", "id")),
                new SelectItemProjection(
                    "running_units_in_tens",
                    new RoundWithDigitsIntegerProjection(
                        new SumIntegerProjection(
                            new FieldInteger("lines", "quantity"),
                            over: new WindowWithFrame(
                                [new OrderItemRow(new FieldUuid("lines", "id"))],
                                new Frame(preceding: new All()),
                                partitionBy: [new FieldUuid("lines", "product_id")]
                            )
                        ),
                        new LiteralInteger(-1)
                    )
                ),
                new SelectItemProjection(
                    "rank_in_tens",
                    new TruncateWithDigitsIntegerProjection(
                        new RowNumberProjection(
                            over: new OrderedWindow([
                                new OrderItemRow(new FieldUuid("lines", "id")),
                            ])
                        ),
                        new LiteralInteger(-1)
                    )
                ),
                new SelectItemProjection(
                    "share_of_rounded_units",
                    new DivideDecimalNullableProjection([
                        new RoundWithDigitsIntegerProjection(
                            new FieldInteger("lines", "quantity"),
                            new LiteralInteger(-1)
                        ),
                        new NullIfEqualIntegerNullableProjection(
                            new FieldInteger("totals", "rounded_units"),
                            new LiteralInteger(0)
                        ),
                    ])
                ),
                new SelectItemProjection(
                    "band_units",
                    new FieldInteger("bands", "units_in_hundreds")
                ),
                new SelectItemProjection(
                    "step_units",
                    new FieldIntegerNullable("steps", "units")
                ),
            ],
            orderBy: [new OrderItemProjectionColumn("id")]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and
    /// <see cref="AuditSchemaDataSet"/>, in this order.
    /// The shares are exact quotients rounded as the specification rounds a decimal.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("id"), new UuidColumnType()),
                    new Column(new String("running_units_in_tens"), new LongColumnType()),
                    new Column(new String("rank_in_tens"), new LongColumnType()),
                    new Column(
                        new String("share_of_rounded_units"),
                        new DoubleColumnType()
                    ),
                    new Column(new String("band_units"), new LongColumnType()),
                    new Column(new String("step_units"), new LongColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("id"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000325-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("running_units_in_tens"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(60))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rank_in_tens"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("share_of_rounded_units"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.05084745762711865))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("band_units"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(300))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("step_units"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(60))
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
                                new Column(new String("id"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000326-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("running_units_in_tens"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(60))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rank_in_tens"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("share_of_rounded_units"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.05084745762711865))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("band_units"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(300))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("step_units"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(60))
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
                                new Column(new String("id"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000327-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("running_units_in_tens"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(70))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rank_in_tens"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("share_of_rounded_units"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.059322033898305086))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("band_units"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(300))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("step_units"),
                                    new LongColumnType()
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
                                new Column(new String("id"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000328-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("running_units_in_tens"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(70))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rank_in_tens"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("share_of_rounded_units"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.059322033898305086))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("band_units"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(300))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("step_units"),
                                    new LongColumnType()
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
                                new Column(new String("id"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000329-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("running_units_in_tens"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(130))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rank_in_tens"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("share_of_rounded_units"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.059322033898305086))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("band_units"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(300))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("step_units"),
                                    new LongColumnType()
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
                                new Column(new String("id"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "0000032a-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("running_units_in_tens"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(160))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rank_in_tens"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("share_of_rounded_units"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.0847457627118644))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("band_units"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(300))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("step_units"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(100))
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
                                new Column(new String("id"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "0000032b-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("running_units_in_tens"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(170))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rank_in_tens"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("share_of_rounded_units"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.0847457627118644))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("band_units"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(300))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("step_units"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(100))
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
                                new Column(new String("id"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "0000032c-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("running_units_in_tens"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(170))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rank_in_tens"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("share_of_rounded_units"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.09322033898305085))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("band_units"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(300))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("step_units"),
                                    new LongColumnType()
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
                                new Column(new String("id"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "0000032d-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("running_units_in_tens"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(270))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rank_in_tens"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("share_of_rounded_units"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.1271186440677966))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("band_units"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(300))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("step_units"),
                                    new LongColumnType()
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
                                new Column(new String("id"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "0000032e-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("running_units_in_tens"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(310))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rank_in_tens"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(10))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("share_of_rounded_units"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.1271186440677966))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("band_units"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(300))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("step_units"),
                                    new LongColumnType()
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
                                new Column(new String("id"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "0000032f-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("running_units_in_tens"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(430))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rank_in_tens"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(10))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("share_of_rounded_units"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.22033898305084745))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("band_units"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(300))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("step_units"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(260))
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
