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
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.GroupBy;

/// <summary>
/// Selects from an inline rows subquery lines holding the id, order_id, product_id,
/// quantity and returned_quantity of fifteen lines, grouped by quantity_band, the
/// quantity truncated to tens, the groups whose sum of quantity rounded to hundreds is at
/// least 100, with rounded integers in every clause of a grouped query: quantity_band,
/// next_band (the band plus 10), units_in_hundreds (the sum of quantity rounded to
/// hundreds), rounded_units (the sum of quantity rounded to tens), largest_rounded (the
/// max of quantity rounded to tens), lines_in_tens (the count rounded to tens),
/// lines_of_all_in_tens (the count over all rows rounded to tens), rounded_bands (the
/// countDistinct of quantity rounded to hundreds) and average_stays_decimal (the average
/// of quantity rounded to tens, a decimal), ordered by the sum of quantity rounded to
/// hundreds descending and the quantity_band column.
/// </summary>
public sealed record RoundedIntegersAcrossGroupedClausesQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainGroupedQueryWithoutPagination(
            subqueries:
            [
                new SubqueryRows(
                    "lines",
                    [
                        [
                            new RowItem(
                                "id",
                                new LiteralUuid(new Guid(801, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new Guid(101, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "product_id",
                                new LiteralUuid(new Guid(201, 0, 0, new byte[8]))
                            ),
                            new RowItem("quantity", new LiteralInteger(4)),
                            new RowItem("returned_quantity", new LiteralInteger(1)),
                        ],
                        [
                            new RowItem(
                                "id",
                                new LiteralUuid(new Guid(802, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new Guid(102, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "product_id",
                                new LiteralUuid(new Guid(202, 0, 0, new byte[8]))
                            ),
                            new RowItem("quantity", new LiteralInteger(15)),
                            new RowItem("returned_quantity", new LiteralInteger(3)),
                        ],
                        [
                            new RowItem(
                                "id",
                                new LiteralUuid(new Guid(803, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new Guid(103, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "product_id",
                                new LiteralUuid(new Guid(203, 0, 0, new byte[8]))
                            ),
                            new RowItem("quantity", new LiteralInteger(18)),
                            new RowItem("returned_quantity", new LiteralInteger(2)),
                        ],
                        [
                            new RowItem(
                                "id",
                                new LiteralUuid(new Guid(804, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new Guid(104, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "product_id",
                                new LiteralUuid(new Guid(204, 0, 0, new byte[8]))
                            ),
                            new RowItem("quantity", new LiteralInteger(25)),
                            new RowItem("returned_quantity", new LiteralInteger(0)),
                        ],
                        [
                            new RowItem(
                                "id",
                                new LiteralUuid(new Guid(805, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new Guid(105, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "product_id",
                                new LiteralUuid(new Guid(201, 0, 0, new byte[8]))
                            ),
                            new RowItem("quantity", new LiteralInteger(61)),
                            new RowItem("returned_quantity", new LiteralInteger(4)),
                        ],
                        [
                            new RowItem(
                                "id",
                                new LiteralUuid(new Guid(806, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new Guid(106, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "product_id",
                                new LiteralUuid(new Guid(202, 0, 0, new byte[8]))
                            ),
                            new RowItem("quantity", new LiteralInteger(62)),
                            new RowItem("returned_quantity", new LiteralInteger(1)),
                        ],
                        [
                            new RowItem(
                                "id",
                                new LiteralUuid(new Guid(807, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new Guid(101, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "product_id",
                                new LiteralUuid(new Guid(203, 0, 0, new byte[8]))
                            ),
                            new RowItem("quantity", new LiteralInteger(65)),
                            new RowItem("returned_quantity", new LiteralInteger(6)),
                        ],
                        [
                            new RowItem(
                                "id",
                                new LiteralUuid(new Guid(808, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new Guid(102, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "product_id",
                                new LiteralUuid(new Guid(204, 0, 0, new byte[8]))
                            ),
                            new RowItem("quantity", new LiteralInteger(66)),
                            new RowItem("returned_quantity", new LiteralInteger(2)),
                        ],
                        [
                            new RowItem(
                                "id",
                                new LiteralUuid(new Guid(809, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new Guid(103, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "product_id",
                                new LiteralUuid(new Guid(201, 0, 0, new byte[8]))
                            ),
                            new RowItem("quantity", new LiteralInteger(68)),
                            new RowItem("returned_quantity", new LiteralInteger(0)),
                        ],
                        [
                            new RowItem(
                                "id",
                                new LiteralUuid(new Guid(810, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new Guid(104, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "product_id",
                                new LiteralUuid(new Guid(202, 0, 0, new byte[8]))
                            ),
                            new RowItem("quantity", new LiteralInteger(100)),
                            new RowItem("returned_quantity", new LiteralInteger(7)),
                        ],
                        [
                            new RowItem(
                                "id",
                                new LiteralUuid(new Guid(811, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new Guid(105, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "product_id",
                                new LiteralUuid(new Guid(203, 0, 0, new byte[8]))
                            ),
                            new RowItem("quantity", new LiteralInteger(104)),
                            new RowItem("returned_quantity", new LiteralInteger(3)),
                        ],
                        [
                            new RowItem(
                                "id",
                                new LiteralUuid(new Guid(812, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new Guid(106, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "product_id",
                                new LiteralUuid(new Guid(204, 0, 0, new byte[8]))
                            ),
                            new RowItem("quantity", new LiteralInteger(107)),
                            new RowItem("returned_quantity", new LiteralInteger(8)),
                        ],
                        [
                            new RowItem(
                                "id",
                                new LiteralUuid(new Guid(813, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new Guid(101, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "product_id",
                                new LiteralUuid(new Guid(201, 0, 0, new byte[8]))
                            ),
                            new RowItem("quantity", new LiteralInteger(145)),
                            new RowItem("returned_quantity", new LiteralInteger(12)),
                        ],
                        [
                            new RowItem(
                                "id",
                                new LiteralUuid(new Guid(814, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new Guid(102, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "product_id",
                                new LiteralUuid(new Guid(202, 0, 0, new byte[8]))
                            ),
                            new RowItem("quantity", new LiteralInteger(149)),
                            new RowItem("returned_quantity", new LiteralInteger(9)),
                        ],
                        [
                            new RowItem(
                                "id",
                                new LiteralUuid(new Guid(815, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new Guid(103, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "product_id",
                                new LiteralUuid(new Guid(203, 0, 0, new byte[8]))
                            ),
                            new RowItem("quantity", new LiteralInteger(260)),
                            new RowItem("returned_quantity", new LiteralInteger(30)),
                        ],
                    ]
                ),
            ],
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
                    new SumIntegerGroup(new FieldInteger("lines", "quantity")),
                    new LiteralInteger(-2)
                ),
                new LiteralInteger(100)
            ),
            select:
            [
                new SelectItemGroup("quantity_band", new KeyInteger("quantity_band")),
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
                        new SumIntegerGroup(new FieldInteger("lines", "quantity")),
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
                        new AverageDecimalGroup(new FieldInteger("lines", "quantity")),
                        new LiteralInteger(-1)
                    )
                ),
            ],
            orderBy:
            [
                new OrderItemGroupExpression(
                    new RoundWithDigitsIntegerGroup(
                        new SumIntegerGroup(new FieldInteger("lines", "quantity")),
                        new LiteralInteger(-2)
                    ),
                    SortDirection.Desc
                ),
                new OrderItemGroupColumn("quantity_band"),
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
                    new Column(new String("quantity_band"), new LongColumnType()),
                    new Column(new String("next_band"), new LongColumnType()),
                    new Column(new String("units_in_hundreds"), new LongColumnType()),
                    new Column(new String("rounded_units"), new LongColumnType()),
                    new Column(new String("largest_rounded"), new LongColumnType()),
                    new Column(new String("lines_in_tens"), new LongColumnType()),
                    new Column(new String("lines_of_all_in_tens"), new LongColumnType()),
                    new Column(new String("rounded_bands"), new LongColumnType()),
                    new Column(
                        new String("average_stays_decimal"),
                        new DoubleColumnType()
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
                                    new String("quantity_band"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(60))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("next_band"), new LongColumnType()),
                                new InvariantCell(new Long(70))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("units_in_hundreds"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(300))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rounded_units"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(330))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("largest_rounded"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(70))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lines_in_tens"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(10))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lines_of_all_in_tens"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(20))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rounded_bands"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("average_stays_decimal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(60.0))
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
                                    new String("quantity_band"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(100))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("next_band"), new LongColumnType()),
                                new InvariantCell(new Long(110))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("units_in_hundreds"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(300))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rounded_units"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(310))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("largest_rounded"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(110))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lines_in_tens"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lines_of_all_in_tens"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(20))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rounded_bands"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("average_stays_decimal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(100.0))
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
                                    new String("quantity_band"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(140))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("next_band"), new LongColumnType()),
                                new InvariantCell(new Long(150))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("units_in_hundreds"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(300))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rounded_units"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(300))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("largest_rounded"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(150))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lines_in_tens"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lines_of_all_in_tens"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(20))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rounded_bands"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("average_stays_decimal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(150.0))
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
                                    new String("quantity_band"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(260))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("next_band"), new LongColumnType()),
                                new InvariantCell(new Long(270))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("units_in_hundreds"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(300))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rounded_units"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(260))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("largest_rounded"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(260))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lines_in_tens"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lines_of_all_in_tens"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(20))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rounded_bands"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("average_stays_decimal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(260.0))
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
