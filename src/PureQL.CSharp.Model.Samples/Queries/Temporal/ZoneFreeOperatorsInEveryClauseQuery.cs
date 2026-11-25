using Pure.Collections.Generic;
using Pure.Primitives.Date;
using Pure.Primitives.Number;
using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.Primitives.Time;
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
using PureQL.CSharp.Model.RowExpressions;
using PureQL.CSharp.Model.Samples.Cells;
using Column = Pure.RelationalSchema.Column.Column;
using DateTime = Pure.Primitives.DateTime.DateTime;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Temporal;

/// <summary>
/// Selects from schema_with_foreign_keys.orders, inner joined with a subquery ranked
/// on its order_id and left joined with schema_with_foreign_keys.orders aliased shipped
/// on the same order_id and an order_status equal to shipped, the orders at least one
/// whole month before 2024-07-05 (dateDiffMonths) whose instant has a microsecond below
/// 700000, where the instant is 2024-06-01T10:00:00Z plus order_total divided by 100
/// seconds. They are grouped by the startOfSecond of the instant as second, by placed_on
/// plus 1 month as renewal and by the nullable shipped.placed_on plus 1 month as
/// shipped_renewal, and the groups whose renewal is at least a whole month after
/// 2024-06-02 are kept. Each group gives the keys, the latest microsecond, the count of
/// orders with rank 1 in their second and the count of orders whose shipped.placed_on
/// lies 0 months from placed_on. The subquery ranked gives each order_id the
/// rowNumber of the order in the partition of its second, ordered by the microsecond
/// and then by order_id. None of dateAddMonths, dateDiffMonths, microsecond and
/// startOfSecond takes a zone.
/// </summary>
public sealed record ZoneFreeOperatorsInEveryClauseQuery
{
    private static string OrdersName =>
        new JoinedString(
            new DotString(),
            [new RelationalSchemaWithForeignKeys().Name, new OrdersTable().Name]
        ).TextValue;

    private static DatetimeAddSecondsDatetimeRow Instant =>
        new DatetimeAddSecondsDatetimeRow(
            new LiteralDatetime(new DateTimeOffset(2024, 6, 1, 10, 0, 0, TimeSpan.Zero)),
            new DivideDecimalRow([
                new FieldDecimal(OrdersName, new OrderTotalColumn().Name.TextValue),
                new LiteralInteger(100),
            ])
        );

    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainGroupedQueryWithoutPagination(
            subqueries:
            [
                new SubqueryQuery(
                    "ranked",
                    new PlainQueryWithoutPaginationWithoutOrderBy(
                        from: new FromEntity(OrdersName),
                        select:
                        [
                            new SelectItemProjection(
                                "id",
                                new FieldUuid(
                                    OrdersName,
                                    new OrderIdColumn().Name.TextValue
                                )
                            ),
                            new SelectItemProjection(
                                "rank_in_second",
                                new RowNumberProjection(
                                    new OrderedWindow(
                                        [
                                            new OrderItemRow(
                                                new MicrosecondDatetimeIntegerRow(Instant)
                                            ),
                                            new OrderItemRow(
                                                new FieldUuid(
                                                    OrdersName,
                                                    new OrderIdColumn().Name.TextValue
                                                )
                                            ),
                                        ],
                                        [new StartOfSecondDatetimeRow(Instant)]
                                    )
                                )
                            ),
                        ]
                    )
                ),
            ],
            from: new FromEntity(OrdersName),
            joins:
            [
                new JoinSubquery(
                    JoinKind.Inner,
                    "ranked",
                    new EqualUuidRow(
                        new FieldUuid("ranked", "id"),
                        new FieldUuid(OrdersName, new OrderIdColumn().Name.TextValue)
                    )
                ),
                new JoinEntity(
                    JoinKind.Left,
                    OrdersName,
                    new AndRow([
                        new EqualUuidRow(
                            new FieldUuid("shipped", new OrderIdColumn().Name.TextValue),
                            new FieldUuid(OrdersName, new OrderIdColumn().Name.TextValue)
                        ),
                        new EqualStringRow(
                            new FieldString(
                                "shipped",
                                new OrderStatusColumn().Name.TextValue
                            ),
                            new LiteralString("shipped")
                        ),
                    ]),
                    "shipped"
                ),
            ],
            where: new AndRow([
                new GreaterThanOrEqualDecimalRow(
                    new DateDiffMonthsIntegerRow(
                        new LiteralDate(new DateOnly(2024, 7, 5)),
                        new FieldDate(OrdersName, new PlacedOnColumn().Name.TextValue)
                    ),
                    new LiteralInteger(1)
                ),
                new LessThanDecimalRow(
                    new MicrosecondDatetimeIntegerRow(Instant),
                    new LiteralInteger(700000)
                ),
            ]),
            groupBy:
            [
                new GroupKey("second", new StartOfSecondDatetimeRow(Instant)),
                new GroupKey(
                    "renewal",
                    new DateAddMonthsDateRow(
                        new FieldDate(OrdersName, new PlacedOnColumn().Name.TextValue),
                        new LiteralInteger(1)
                    )
                ),
                new GroupKey(
                    "shipped_renewal",
                    new DateAddMonthsDateNullableRow(
                        new FieldDateNullable(
                            "shipped",
                            new PlacedOnColumn().Name.TextValue
                        ),
                        new LiteralInteger(1)
                    )
                ),
            ],
            having: new GreaterThanDecimalGroup(
                new DateDiffMonthsIntegerGroup(
                    new KeyDate("renewal"),
                    new LiteralDate(new DateOnly(2024, 6, 2))
                ),
                new LiteralInteger(0)
            ),
            select:
            [
                new SelectItemGroup("second", new KeyDatetime("second")),
                new SelectItemGroup("renewal", new KeyDate("renewal")),
                new SelectItemGroup(
                    "shipped_renewal",
                    new KeyDateNullable("shipped_renewal")
                ),
                new SelectItemGroup(
                    "latest_microsecond",
                    new MaxIntegerGroup(new MicrosecondDatetimeIntegerRow(Instant))
                ),
                new SelectItemGroup(
                    "first_ranks",
                    new CountGroup(
                        new EqualDecimalRow(
                            new FieldInteger("ranked", "rank_in_second"),
                            new LiteralInteger(1)
                        )
                    )
                ),
                new SelectItemGroup(
                    "shipped_same_month",
                    new CountGroup(
                        new EqualDecimalRow(
                            new DateDiffMonthsIntegerNullableRow(
                                new FieldDateNullable(
                                    "shipped",
                                    new PlacedOnColumn().Name.TextValue
                                ),
                                new FieldDate(
                                    OrdersName,
                                    new PlacedOnColumn().Name.TextValue
                                )
                            ),
                            new LiteralInteger(0)
                        )
                    )
                ),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/>, in no particular order.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("second"), new DateTimeColumnType()),
                    new Column(new String("renewal"), new DateColumnType()),
                    new Column(new String("shipped_renewal"), new DateColumnType()),
                    new Column(new String("latest_microsecond"), new LongColumnType()),
                    new Column(new String("first_ranks"), new LongColumnType()),
                    new Column(new String("shipped_same_month"), new LongColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("second"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(1),
                                            new UShort(6),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(10),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("renewal"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(2),
                                        new UShort(7),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("shipped_renewal"),
                                    new DateColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("latest_microsecond"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(500000))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_ranks"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("shipped_same_month"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
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
                                    new String("second"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(1),
                                            new UShort(6),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(10),
                                            new UShort(0),
                                            new UShort(2)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("renewal"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(3),
                                        new UShort(7),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("shipped_renewal"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(3),
                                        new UShort(7),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("latest_microsecond"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_ranks"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("shipped_same_month"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
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
                                    new String("second"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(1),
                                            new UShort(6),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(10),
                                            new UShort(0),
                                            new UShort(3)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("renewal"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(5),
                                        new UShort(7),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("shipped_renewal"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(5),
                                        new UShort(7),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("latest_microsecond"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_ranks"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("shipped_same_month"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
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
