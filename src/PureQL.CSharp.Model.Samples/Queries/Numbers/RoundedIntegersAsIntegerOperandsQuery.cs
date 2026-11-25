using Pure.Collections.Generic;
using Pure.Primitives.Bool;
using Pure.Primitives.Date;
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
using PureQL.CSharp.Model.Lists;
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.ProjectionExpressions;
using PureQL.CSharp.Model.RowExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using Guid = Pure.Primitives.Guid.Guid;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Numbers;

/// <summary>
/// Selects, from an inline rows subquery lines holding the id, order_id, quantity and
/// returned_quantity of seven lines inner joined with schema_with_foreign_keys.orders on
/// order_id, the lines whose quantity rounded to tens (round with digits -1) is above 0,
/// with the integers made by rounding and truncating used where an integer is expected:
/// id, days (placed_on plus the rounded quantity days), months (placed_on plus the
/// quantity truncated to tens months), decade (dateFromParts of the placed_on year
/// truncated to tens, January 1), stars ("*" repeated by the rounded quantity
/// integer-divided by 10), padded (order_status padded at the start with spaces to the
/// rounded quantity), left_over (quantity modulo the greatest of the rounded quantity and
/// 10), sum_of_integers (the rounded quantity plus returned_quantity), absolute (the
/// absolute difference of the rounded quantity and quantity), unless_zero (nullIfEqual of
/// the rounded quantity and 0), either (if quantity is above 40 the quantity rounded to
/// hundreds, else to tens), bucket (ntile of 4 ordered by the rounded quantity and id),
/// previous_tens (previous rounded quantity ordered by id), in_a_list (the rounded
/// quantity in 10, 20 and 50), as_text (asString of the rounded quantity) and
/// widened_on_purpose (asDecimal of it), ordered by the rounded quantity descending and
/// the id column.
/// </summary>
public sealed record RoundedIntegersAsIntegerOperandsQuery
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
                                new LiteralUuid(new System.Guid(821, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new System.Guid(101, 0, 0, new byte[8]))
                            ),
                            new RowItem("quantity", new LiteralInteger(4)),
                            new RowItem("returned_quantity", new LiteralInteger(1)),
                        ],
                        [
                            new RowItem(
                                "id",
                                new LiteralUuid(new System.Guid(822, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new System.Guid(102, 0, 0, new byte[8]))
                            ),
                            new RowItem("quantity", new LiteralInteger(25)),
                            new RowItem("returned_quantity", new LiteralInteger(0)),
                        ],
                        [
                            new RowItem(
                                "id",
                                new LiteralUuid(new System.Guid(823, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new System.Guid(103, 0, 0, new byte[8]))
                            ),
                            new RowItem("quantity", new LiteralInteger(15)),
                            new RowItem("returned_quantity", new LiteralInteger(3)),
                        ],
                        [
                            new RowItem(
                                "id",
                                new LiteralUuid(new System.Guid(824, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new System.Guid(104, 0, 0, new byte[8]))
                            ),
                            new RowItem("quantity", new LiteralInteger(18)),
                            new RowItem("returned_quantity", new LiteralInteger(2)),
                        ],
                        [
                            new RowItem(
                                "id",
                                new LiteralUuid(new System.Guid(825, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new System.Guid(105, 0, 0, new byte[8]))
                            ),
                            new RowItem("quantity", new LiteralInteger(42)),
                            new RowItem("returned_quantity", new LiteralInteger(5)),
                        ],
                        [
                            new RowItem(
                                "id",
                                new LiteralUuid(new System.Guid(826, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new System.Guid(106, 0, 0, new byte[8]))
                            ),
                            new RowItem("quantity", new LiteralInteger(36)),
                            new RowItem("returned_quantity", new LiteralInteger(0)),
                        ],
                        [
                            new RowItem(
                                "id",
                                new LiteralUuid(new System.Guid(827, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new System.Guid(101, 0, 0, new byte[8]))
                            ),
                            new RowItem("quantity", new LiteralInteger(49)),
                            new RowItem("returned_quantity", new LiteralInteger(9)),
                        ],
                    ]
                ),
            ],
            from: new FromSubquery("lines"),
            joins:
            [
                new JoinEntity(
                    JoinKind.Inner,
                    new JoinedString(
                        new DotString(),
                        [
                            new RelationalSchemaWithForeignKeys().Name,
                            new OrdersTable().Name,
                        ]
                    ).TextValue,
                    new EqualUuidRow(
                        new FieldUuid("lines", "order_id"),
                        new FieldUuid(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrdersTable().Name,
                                ]
                            ).TextValue,
                            new OrderIdColumn().Name.TextValue
                        )
                    )
                ),
            ],
            where: new GreaterThanDecimalRow(
                new RoundWithDigitsIntegerRow(
                    new FieldInteger("lines", "quantity"),
                    new LiteralInteger(-1)
                ),
                new LiteralInteger(0)
            ),
            select:
            [
                new SelectItemProjection("id", new FieldUuid("lines", "id")),
                new SelectItemProjection(
                    "days",
                    new DateAddDaysDateProjection(
                        new FieldDate(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrdersTable().Name,
                                ]
                            ).TextValue,
                            new PlacedOnColumn().Name.TextValue
                        ),
                        new RoundWithDigitsIntegerProjection(
                            new FieldInteger("lines", "quantity"),
                            new LiteralInteger(-1)
                        )
                    )
                ),
                new SelectItemProjection(
                    "months",
                    new DateAddMonthsDateProjection(
                        new FieldDate(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrdersTable().Name,
                                ]
                            ).TextValue,
                            new PlacedOnColumn().Name.TextValue
                        ),
                        new TruncateWithDigitsIntegerProjection(
                            new FieldInteger("lines", "quantity"),
                            new LiteralInteger(-1)
                        )
                    )
                ),
                new SelectItemProjection(
                    "decade",
                    new DateFromPartsDateProjection(
                        new TruncateWithDigitsIntegerProjection(
                            new YearDateIntegerProjection(
                                new FieldDate(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new OrdersTable().Name,
                                        ]
                                    ).TextValue,
                                    new PlacedOnColumn().Name.TextValue
                                )
                            ),
                            new LiteralInteger(-1)
                        ),
                        new LiteralInteger(1),
                        new LiteralInteger(1)
                    )
                ),
                new SelectItemProjection(
                    "stars",
                    new RepeatStringProjection(
                        new LiteralString("*"),
                        new IntegerDivideIntegerProjection(
                            new RoundWithDigitsIntegerProjection(
                                new FieldInteger("lines", "quantity"),
                                new LiteralInteger(-1)
                            ),
                            new LiteralInteger(10)
                        )
                    )
                ),
                new SelectItemProjection(
                    "padded",
                    new PadStartStringProjection(
                        new FieldString(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrdersTable().Name,
                                ]
                            ).TextValue,
                            new OrderStatusColumn().Name.TextValue
                        ),
                        new RoundWithDigitsIntegerProjection(
                            new FieldInteger("lines", "quantity"),
                            new LiteralInteger(-1)
                        ),
                        new LiteralString(" ")
                    )
                ),
                new SelectItemProjection(
                    "left_over",
                    new ModuloIntegerProjection(
                        new FieldInteger("lines", "quantity"),
                        new GreatestIntegerProjection([
                            new RoundWithDigitsIntegerProjection(
                                new FieldInteger("lines", "quantity"),
                                new LiteralInteger(-1)
                            ),
                            new LiteralInteger(10),
                        ])
                    )
                ),
                new SelectItemProjection(
                    "sum_of_integers",
                    new AddIntegerProjection([
                        new RoundWithDigitsIntegerProjection(
                            new FieldInteger("lines", "quantity"),
                            new LiteralInteger(-1)
                        ),
                        new FieldInteger("lines", "returned_quantity"),
                    ])
                ),
                new SelectItemProjection(
                    "absolute",
                    new AbsIntegerProjection(
                        new SubtractIntegerProjection([
                            new RoundWithDigitsIntegerProjection(
                                new FieldInteger("lines", "quantity"),
                                new LiteralInteger(-1)
                            ),
                            new FieldInteger("lines", "quantity"),
                        ])
                    )
                ),
                new SelectItemProjection(
                    "unless_zero",
                    new NullIfEqualIntegerNullableProjection(
                        new RoundWithDigitsIntegerProjection(
                            new FieldInteger("lines", "quantity"),
                            new LiteralInteger(-1)
                        ),
                        new LiteralInteger(0)
                    )
                ),
                new SelectItemProjection(
                    "either",
                    new IfIntegerProjection(
                        new RoundWithDigitsIntegerProjection(
                            new FieldInteger("lines", "quantity"),
                            new LiteralInteger(-2)
                        ),
                        new RoundWithDigitsIntegerProjection(
                            new FieldInteger("lines", "quantity"),
                            new LiteralInteger(-1)
                        ),
                        new GreaterThanDecimalProjection(
                            new FieldInteger("lines", "quantity"),
                            new LiteralInteger(40)
                        )
                    )
                ),
                new SelectItemProjection(
                    "bucket",
                    new NtileProjection(
                        new LiteralInteger(4),
                        over: new OrderedWindow([
                            new OrderItemRow(
                                new RoundWithDigitsIntegerRow(
                                    new FieldInteger("lines", "quantity"),
                                    new LiteralInteger(-1)
                                )
                            ),
                            new OrderItemRow(new FieldUuid("lines", "id")),
                        ])
                    )
                ),
                new SelectItemProjection(
                    "previous_tens",
                    new PreviousIntegerNullableProjection(
                        new RoundWithDigitsIntegerRow(
                            new FieldInteger("lines", "quantity"),
                            new LiteralInteger(-1)
                        ),
                        over: new OrderedWindow([
                            new OrderItemRow(new FieldUuid("lines", "id")),
                        ])
                    )
                ),
                new SelectItemProjection(
                    "in_a_list",
                    new InDecimalProjection(
                        new RoundWithDigitsIntegerProjection(
                            new FieldInteger("lines", "quantity"),
                            new LiteralInteger(-1)
                        ),
                        new ListLiteralInteger([10, 20, 50])
                    )
                ),
                new SelectItemProjection(
                    "as_text",
                    new AsStringDecimalStringProjection(
                        new RoundWithDigitsIntegerProjection(
                            new FieldInteger("lines", "quantity"),
                            new LiteralInteger(-1)
                        )
                    )
                ),
                new SelectItemProjection(
                    "widened_on_purpose",
                    new AsDecimalIntegerDecimalProjection(
                        new RoundWithDigitsIntegerProjection(
                            new FieldInteger("lines", "quantity"),
                            new LiteralInteger(-1)
                        )
                    )
                ),
            ],
            orderBy:
            [
                new OrderItemProjectionExpression(
                    new RoundWithDigitsIntegerProjection(
                        new FieldInteger("lines", "quantity"),
                        new LiteralInteger(-1)
                    ),
                    SortDirection.Desc
                ),
                new OrderItemProjectionColumn("id"),
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
                    new Column(new String("id"), new UuidColumnType()),
                    new Column(new String("days"), new DateColumnType()),
                    new Column(new String("months"), new DateColumnType()),
                    new Column(new String("decade"), new DateColumnType()),
                    new Column(new String("stars"), new StringColumnType()),
                    new Column(new String("padded"), new StringColumnType()),
                    new Column(new String("left_over"), new LongColumnType()),
                    new Column(new String("sum_of_integers"), new LongColumnType()),
                    new Column(new String("absolute"), new LongColumnType()),
                    new Column(new String("unless_zero"), new LongColumnType()),
                    new Column(new String("either"), new LongColumnType()),
                    new Column(new String("bucket"), new LongColumnType()),
                    new Column(new String("previous_tens"), new LongColumnType()),
                    new Column(new String("in_a_list"), new BoolColumnType()),
                    new Column(new String("as_text"), new StringColumnType()),
                    new Column(new String("widened_on_purpose"), new DoubleColumnType()),
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
                                            "0000033b-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("days"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(21),
                                        new UShort(7),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("months"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(1),
                                        new UShort(10),
                                        new UShort(2027)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("decade"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(1),
                                        new UShort(1),
                                        new UShort(2020)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("stars"), new StringColumnType()),
                                new InvariantCell(new String("*****"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("padded"), new StringColumnType()),
                                new InvariantCell(
                                    new String(
                                        "                                           shipped"
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("left_over"), new LongColumnType()),
                                new InvariantCell(new Long(49))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sum_of_integers"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(59))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("absolute"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("unless_zero"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(50))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("either"), new LongColumnType()),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("bucket"), new LongColumnType()),
                                new InvariantCell(new Long(4))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("previous_tens"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(40))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("in_a_list"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("as_text"), new StringColumnType()),
                                new InvariantCell(new String("50"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("widened_on_purpose"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(50.0))
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
                                            "00000339-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("days"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(15),
                                        new UShort(7),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("months"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(5),
                                        new UShort(10),
                                        new UShort(2027)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("decade"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(1),
                                        new UShort(1),
                                        new UShort(2020)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("stars"), new StringColumnType()),
                                new InvariantCell(new String("****"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("padded"), new StringColumnType()),
                                new InvariantCell(
                                    new String("                                 shipped")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("left_over"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sum_of_integers"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(45))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("absolute"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("unless_zero"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(40))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("either"), new LongColumnType()),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("bucket"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("previous_tens"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(20))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("in_a_list"), new BoolColumnType()),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("as_text"), new StringColumnType()),
                                new InvariantCell(new String("40"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("widened_on_purpose"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(40.0))
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
                                            "0000033a-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("days"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(16),
                                        new UShort(7),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("months"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(6),
                                        new UShort(12),
                                        new UShort(2026)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("decade"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(1),
                                        new UShort(1),
                                        new UShort(2020)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("stars"), new StringColumnType()),
                                new InvariantCell(new String("****"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("padded"), new StringColumnType()),
                                new InvariantCell(
                                    new String("                                 pending")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("left_over"), new LongColumnType()),
                                new InvariantCell(new Long(36))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sum_of_integers"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(40))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("absolute"), new LongColumnType()),
                                new InvariantCell(new Long(4))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("unless_zero"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(40))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("either"), new LongColumnType()),
                                new InvariantCell(new Long(40))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("bucket"), new LongColumnType()),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("previous_tens"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(40))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("in_a_list"), new BoolColumnType()),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("as_text"), new StringColumnType()),
                                new InvariantCell(new String("40"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("widened_on_purpose"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(40.0))
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
                                            "00000336-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("days"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(2),
                                        new UShort(7),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("months"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(2),
                                        new UShort(2),
                                        new UShort(2026)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("decade"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(1),
                                        new UShort(1),
                                        new UShort(2020)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("stars"), new StringColumnType()),
                                new InvariantCell(new String("***"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("padded"), new StringColumnType()),
                                new InvariantCell(
                                    new String("                       pending")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("left_over"), new LongColumnType()),
                                new InvariantCell(new Long(25))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sum_of_integers"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(30))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("absolute"), new LongColumnType()),
                                new InvariantCell(new Long(5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("unless_zero"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(30))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("either"), new LongColumnType()),
                                new InvariantCell(new Long(30))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("bucket"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("previous_tens"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("in_a_list"), new BoolColumnType()),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("as_text"), new StringColumnType()),
                                new InvariantCell(new String("30"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("widened_on_purpose"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(30.0))
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
                                            "00000337-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("days"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(23),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("months"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(3),
                                        new UShort(4),
                                        new UShort(2025)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("decade"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(1),
                                        new UShort(1),
                                        new UShort(2020)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("stars"), new StringColumnType()),
                                new InvariantCell(new String("**"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("padded"), new StringColumnType()),
                                new InvariantCell(new String("             shipped"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("left_over"), new LongColumnType()),
                                new InvariantCell(new Long(15))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sum_of_integers"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(23))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("absolute"), new LongColumnType()),
                                new InvariantCell(new Long(5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("unless_zero"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(20))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("either"), new LongColumnType()),
                                new InvariantCell(new Long(20))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("bucket"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("previous_tens"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(30))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("in_a_list"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("as_text"), new StringColumnType()),
                                new InvariantCell(new String("20"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("widened_on_purpose"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(20.0))
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
                                            "00000338-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("days"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(24),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("months"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(4),
                                        new UShort(4),
                                        new UShort(2025)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("decade"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(1),
                                        new UShort(1),
                                        new UShort(2020)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("stars"), new StringColumnType()),
                                new InvariantCell(new String("**"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("padded"), new StringColumnType()),
                                new InvariantCell(new String("           cancelled"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("left_over"), new LongColumnType()),
                                new InvariantCell(new Long(18))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sum_of_integers"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(22))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("absolute"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("unless_zero"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(20))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("either"), new LongColumnType()),
                                new InvariantCell(new Long(20))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("bucket"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("previous_tens"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(20))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("in_a_list"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("as_text"), new StringColumnType()),
                                new InvariantCell(new String("20"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("widened_on_purpose"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(20.0))
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
