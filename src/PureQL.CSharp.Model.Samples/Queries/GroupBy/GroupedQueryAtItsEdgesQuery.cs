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
using PureQL.CSharp.Model.GroupExpressions;
using PureQL.CSharp.Model.Keys;
using PureQL.CSharp.Model.Lists;
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.RowExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.GroupBy;

/// <summary>
/// Selects, from schema_with_foreign_keys.orders inner joined on order_id to the subquery
/// facts of inline rows (is_paid, bonus_points, quantity, discount, name, shipped_date
/// and order_date of each order, shipped_date null on every row), grouped by bonus_points
/// as points, order_date as day, is_paid as paid and as verified, the integer 1 as one,
/// the string "web" as bucket and discount coalesced with the integer 0, having paid, any
/// is_paid, the count of all rows greater than the count of the group and the max of
/// order_total over all rows greater than the integer 100: the average of quantity, the
/// average of discount, the max of name, the min of quantity, the string "web", whether
/// the key points equals the sum of order_total, the key verified, the min of order_date
/// plus the days from the key day to the max of order_date, "paid " or "open " by the key
/// paid joined to the max of name over all rows, whether the min of quantity (0 where not
/// is_paid) equals the key points and whether the max of order_date plus 1 day is in the
/// dates 2024-06-03 and 2024-12-25. Ordered by the sum of order_total over all rows, then
/// by the min of shipped_date over all rows coalesced with the key day descending.
/// </summary>
public sealed record GroupedQueryAtItsEdgesQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainGroupedQueryWithoutPagination(
            subqueries:
            [
                new SubqueryRows(
                    "facts",
                    [
                        [
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new Guid(101, 0, 0, new byte[8]))
                            ),
                            new RowItem("is_paid", new LiteralBoolean(true)),
                            new RowItem("bonus_points", new LiteralInteger(2)),
                            new RowItem("quantity", new LiteralInteger(2)),
                            new RowItem("discount", new LiteralDecimal(0.5m)),
                            new RowItem("name", new LiteralString("Ann")),
                            new RowItem("shipped_date", new LiteralDateNullable()),
                            new RowItem(
                                "order_date",
                                new LiteralDate(new DateOnly(2024, 6, 1))
                            ),
                        ],
                        [
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new Guid(102, 0, 0, new byte[8]))
                            ),
                            new RowItem("is_paid", new LiteralBoolean(true)),
                            new RowItem("bonus_points", new LiteralInteger(50)),
                            new RowItem("quantity", new LiteralInteger(4)),
                            new RowItem("discount", new LiteralDecimal(0.25m)),
                            new RowItem("name", new LiteralString("Bob")),
                            new RowItem("shipped_date", new LiteralDateNullable()),
                            new RowItem(
                                "order_date",
                                new LiteralDate(new DateOnly(2024, 6, 3))
                            ),
                        ],
                        [
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new Guid(103, 0, 0, new byte[8]))
                            ),
                            new RowItem("is_paid", new LiteralBoolean(true)),
                            new RowItem("bonus_points", new LiteralIntegerNullable()),
                            new RowItem("quantity", new LiteralInteger(1)),
                            new RowItem("discount", new LiteralDecimalNullable()),
                            new RowItem("name", new LiteralString("Cara")),
                            new RowItem("shipped_date", new LiteralDateNullable()),
                            new RowItem(
                                "order_date",
                                new LiteralDate(new DateOnly(2024, 6, 2))
                            ),
                        ],
                        [
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new Guid(104, 0, 0, new byte[8]))
                            ),
                            new RowItem("is_paid", new LiteralBoolean(false)),
                            new RowItem("bonus_points", new LiteralIntegerNullable()),
                            new RowItem("quantity", new LiteralInteger(3)),
                            new RowItem("discount", new LiteralDecimalNullable()),
                            new RowItem("name", new LiteralString("Dan")),
                            new RowItem("shipped_date", new LiteralDateNullable()),
                            new RowItem(
                                "order_date",
                                new LiteralDate(new DateOnly(2024, 6, 2))
                            ),
                        ],
                        [
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new Guid(105, 0, 0, new byte[8]))
                            ),
                            new RowItem("is_paid", new LiteralBoolean(true)),
                            new RowItem("bonus_points", new LiteralIntegerNullable()),
                            new RowItem("quantity", new LiteralInteger(5)),
                            new RowItem("discount", new LiteralDecimalNullable()),
                            new RowItem("name", new LiteralString("Eve")),
                            new RowItem("shipped_date", new LiteralDateNullable()),
                            new RowItem(
                                "order_date",
                                new LiteralDate(new DateOnly(2024, 6, 2))
                            ),
                        ],
                        [
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new Guid(106, 0, 0, new byte[8]))
                            ),
                            new RowItem("is_paid", new LiteralBoolean(true)),
                            new RowItem("bonus_points", new LiteralInteger(2)),
                            new RowItem("quantity", new LiteralInteger(3)),
                            new RowItem("discount", new LiteralDecimal(0.5m)),
                            new RowItem("name", new LiteralString("Fay")),
                            new RowItem("shipped_date", new LiteralDateNullable()),
                            new RowItem(
                                "order_date",
                                new LiteralDate(new DateOnly(2024, 6, 1))
                            ),
                        ],
                    ]
                ),
            ],
            from: new FromEntity(
                new JoinedString(
                    new DotString(),
                    [new RelationalSchemaWithForeignKeys().Name, new OrdersTable().Name]
                ).TextValue
            ),
            joins:
            [
                new JoinSubquery(
                    JoinKind.Inner,
                    "facts",
                    new EqualUuidRow(
                        new FieldUuid(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrdersTable().Name,
                                ]
                            ).TextValue,
                            new OrderIdColumn().Name.TextValue
                        ),
                        new FieldUuid("facts", "order_id")
                    )
                ),
            ],
            groupBy:
            [
                new GroupKey("points", new FieldIntegerNullable("facts", "bonus_points")),
                new GroupKey("day", new FieldDate("facts", "order_date")),
                new GroupKey("paid", new FieldBoolean("facts", "is_paid")),
                new GroupKey("verified", new FieldBoolean("facts", "is_paid")),
                new GroupKey("one", new LiteralInteger(1)),
                new GroupKey("bucket", new LiteralString("web")),
                new GroupKey(
                    "discount",
                    new CoalesceDecimalRow([
                        new FieldDecimalNullable("facts", "discount"),
                        new LiteralInteger(0),
                    ])
                ),
            ],
            having: new AndGroup([
                new KeyBoolean("paid"),
                new AnyGroup(new FieldBoolean("facts", "is_paid")),
                new GreaterThanDecimalGroup(
                    new CountGroup(over: AggregateOver.All),
                    new CountGroup()
                ),
                new GreaterThanDecimalGroup(
                    new MaxDecimalGroup(
                        new FieldDecimal(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrdersTable().Name,
                                ]
                            ).TextValue,
                            new OrderTotalColumn().Name.TextValue
                        ),
                        AggregateOver.All
                    ),
                    new LiteralInteger(100)
                ),
            ]),
            select:
            [
                new SelectItemGroup(
                    "avg_quantity",
                    new AverageDecimalGroup(new FieldInteger("facts", "quantity"))
                ),
                new SelectItemGroup(
                    "avg_discount",
                    new AverageDecimalNullableGroup(
                        new FieldDecimalNullable("facts", "discount")
                    )
                ),
                new SelectItemGroup(
                    "last_name",
                    new MaxStringGroup(new FieldString("facts", "name"))
                ),
                new SelectItemGroup(
                    "min_quantity",
                    new MinIntegerGroup(new FieldInteger("facts", "quantity"))
                ),
                new SelectItemGroup("bucket_param", new LiteralString("web")),
                new SelectItemGroup(
                    "points_match",
                    new EqualDecimalGroup(
                        new KeyIntegerNullable("points"),
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
                    )
                ),
                new SelectItemGroup("verified_key", new KeyBoolean("verified")),
                new SelectItemGroup(
                    "span_end",
                    new DateAddDaysDateGroup(
                        new MinDateGroup(new FieldDate("facts", "order_date")),
                        new DateDiffDaysIntegerGroup(
                            new MaxDateGroup(new FieldDate("facts", "order_date")),
                            new KeyDate("day")
                        )
                    )
                ),
                new SelectItemGroup(
                    "label",
                    new ConcatStringGroup([
                        new IfStringGroup(
                            new LiteralString("paid "),
                            new LiteralString("open "),
                            new KeyBoolean("paid")
                        ),
                        new MaxStringGroup(
                            new FieldString("facts", "name"),
                            AggregateOver.All
                        ),
                    ])
                ),
                new SelectItemGroup(
                    "min_points_is_key",
                    new EqualDecimalGroup(
                        new MinIntegerGroup(
                            new IfIntegerRow(
                                new FieldInteger("facts", "quantity"),
                                new LiteralInteger(0),
                                new FieldBoolean("facts", "is_paid")
                            )
                        ),
                        new KeyIntegerNullable("points")
                    )
                ),
                new SelectItemGroup(
                    "late_day_listed",
                    new InDateGroup(
                        new MaxDateGroup(
                            new DateAddDaysDateRow(
                                new FieldDate("facts", "order_date"),
                                new LiteralInteger(1)
                            )
                        ),
                        new ListLiteralDate([
                            new DateOnly(2024, 6, 3),
                            new DateOnly(2024, 12, 25),
                        ])
                    )
                ),
            ],
            orderBy:
            [
                new OrderItemGroupExpression(
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
                        ),
                        over: AggregateOver.All
                    )
                ),
                new OrderItemGroupExpression(
                    new CoalesceDateGroup([
                        new MinDateNullableGroup(
                            new FieldDateNullable("facts", "shipped_date"),
                            over: AggregateOver.All
                        ),
                        new KeyDate("day"),
                    ]),
                    SortDirection.Desc
                ),
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
                    new Column(new String("avg_quantity"), new DoubleColumnType()),
                    new Column(new String("avg_discount"), new DoubleColumnType()),
                    new Column(new String("last_name"), new StringColumnType()),
                    new Column(new String("min_quantity"), new LongColumnType()),
                    new Column(new String("bucket_param"), new StringColumnType()),
                    new Column(new String("points_match"), new BoolColumnType()),
                    new Column(new String("verified_key"), new BoolColumnType()),
                    new Column(new String("span_end"), new DateColumnType()),
                    new Column(new String("label"), new StringColumnType()),
                    new Column(new String("min_points_is_key"), new BoolColumnType()),
                    new Column(new String("late_day_listed"), new BoolColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("avg_quantity"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(4))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("avg_discount"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.25))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Bob"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("min_quantity"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(4))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("bucket_param"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("web"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("points_match"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("verified_key"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("span_end"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(3),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("label"), new StringColumnType()),
                                new InvariantCell(new String("paid Fay"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("min_points_is_key"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("late_day_listed"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
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
                                    new String("avg_quantity"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("avg_discount"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Eve"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("min_quantity"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("bucket_param"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("web"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("points_match"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("verified_key"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("span_end"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(2),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("label"), new StringColumnType()),
                                new InvariantCell(new String("paid Fay"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("min_points_is_key"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("late_day_listed"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
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
                                    new String("avg_quantity"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(2.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("avg_discount"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Fay"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("min_quantity"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("bucket_param"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("web"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("points_match"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("verified_key"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("span_end"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(1),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("label"), new StringColumnType()),
                                new InvariantCell(new String("paid Fay"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("min_points_is_key"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("late_day_listed"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
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
