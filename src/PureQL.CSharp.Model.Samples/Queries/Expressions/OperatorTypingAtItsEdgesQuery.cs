using Pure.Collections.Generic;
using Pure.Primitives.Bool;
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
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.ProjectionExpressions;
using PureQL.CSharp.Model.RowExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using Guid = Pure.Primitives.Guid.Guid;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Expressions;

/// <summary>
/// Selects, from schema_with_foreign_keys.orders inner joined on order_id to the subquery
/// facts of inline rows (is_paid, is_verified, quantity, shipped_date, window_start,
/// coupon_code and bonus_points of each order), where is_paid, is_verified coalesced with
/// false, a null string equals a null string, a null integer equals a null decimal, a
/// null date is not greater than a null date, order_total is at least quantity and
/// quantity is not equal to order_total: the floor of order_total, order_total rounded to
/// hundreds and to 1 + 1 digits, the days from placed_on to shipped_date, window_start
/// plus 60 seconds, 1 + 2 + quantity, whether order_status is shipped, order_id or
/// order_user_id by is_paid, coupon_code coalesced from a null string and "none",
/// bonus_points coalesced with order_total as a decimal, bonus_points itself, and is_paid
/// and is_verified equal to true.
/// </summary>
public sealed record OperatorTypingAtItsEdgesQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithoutPagination(
            subqueries:
            [
                new SubqueryRows(
                    "facts",
                    [
                        [
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new System.Guid(101, 0, 0, new byte[8]))
                            ),
                            new RowItem("is_paid", new LiteralBoolean(true)),
                            new RowItem("is_verified", new LiteralBoolean(true)),
                            new RowItem("quantity", new LiteralInteger(2)),
                            new RowItem(
                                "shipped_date",
                                new LiteralDate(new DateOnly(2024, 6, 4))
                            ),
                            new RowItem(
                                "window_start",
                                new LiteralTime(new TimeOnly(9, 0, 0))
                            ),
                            new RowItem("coupon_code", new LiteralString("SPRING")),
                            new RowItem("bonus_points", new LiteralInteger(10)),
                        ],
                        [
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new System.Guid(102, 0, 0, new byte[8]))
                            ),
                            new RowItem("is_paid", new LiteralBoolean(true)),
                            new RowItem("is_verified", new LiteralBooleanNullable()),
                            new RowItem("quantity", new LiteralInteger(1)),
                            new RowItem("shipped_date", new LiteralDateNullable()),
                            new RowItem(
                                "window_start",
                                new LiteralTime(new TimeOnly(10, 0, 0))
                            ),
                            new RowItem("coupon_code", new LiteralStringNullable()),
                            new RowItem("bonus_points", new LiteralIntegerNullable()),
                        ],
                        [
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new System.Guid(103, 0, 0, new byte[8]))
                            ),
                            new RowItem("is_paid", new LiteralBoolean(false)),
                            new RowItem("is_verified", new LiteralBoolean(true)),
                            new RowItem("quantity", new LiteralInteger(5)),
                            new RowItem(
                                "shipped_date",
                                new LiteralDate(new DateOnly(2024, 6, 5))
                            ),
                            new RowItem(
                                "window_start",
                                new LiteralTime(new TimeOnly(11, 30, 0))
                            ),
                            new RowItem("coupon_code", new LiteralStringNullable()),
                            new RowItem("bonus_points", new LiteralInteger(7)),
                        ],
                        [
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new System.Guid(104, 0, 0, new byte[8]))
                            ),
                            new RowItem("is_paid", new LiteralBoolean(false)),
                            new RowItem("is_verified", new LiteralBooleanNullable()),
                            new RowItem("quantity", new LiteralInteger(4)),
                            new RowItem("shipped_date", new LiteralDateNullable()),
                            new RowItem(
                                "window_start",
                                new LiteralTime(new TimeOnly(8, 0, 0))
                            ),
                            new RowItem("coupon_code", new LiteralString("WINTER")),
                            new RowItem("bonus_points", new LiteralIntegerNullable()),
                        ],
                        [
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new System.Guid(105, 0, 0, new byte[8]))
                            ),
                            new RowItem("is_paid", new LiteralBoolean(true)),
                            new RowItem("is_verified", new LiteralBoolean(true)),
                            new RowItem("quantity", new LiteralInteger(3)),
                            new RowItem("shipped_date", new LiteralDateNullable()),
                            new RowItem(
                                "window_start",
                                new LiteralTime(new TimeOnly(23, 59, 30))
                            ),
                            new RowItem("coupon_code", new LiteralStringNullable()),
                            new RowItem("bonus_points", new LiteralIntegerNullable()),
                        ],
                        [
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new System.Guid(106, 0, 0, new byte[8]))
                            ),
                            new RowItem("is_paid", new LiteralBoolean(true)),
                            new RowItem("is_verified", new LiteralBoolean(true)),
                            new RowItem("quantity", new LiteralInteger(1)),
                            new RowItem(
                                "shipped_date",
                                new LiteralDate(new DateOnly(2024, 6, 9))
                            ),
                            new RowItem(
                                "window_start",
                                new LiteralTime(new TimeOnly(18, 45, 0))
                            ),
                            new RowItem("coupon_code", new LiteralString("SUMMER")),
                            new RowItem("bonus_points", new LiteralInteger(4)),
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
            where: new AndRow([
                new FieldBoolean("facts", "is_paid"),
                new CoalesceBooleanRow([
                    new FieldBooleanNullable("facts", "is_verified"),
                    new LiteralBoolean(false),
                ]),
                new EqualStringRow(
                    new LiteralStringNullable(),
                    new LiteralStringNullable()
                ),
                new EqualDecimalRow(
                    new LiteralIntegerNullable(),
                    new LiteralDecimalNullable()
                ),
                new NotRow(
                    new GreaterThanDateRow(
                        new LiteralDateNullable(),
                        new LiteralDateNullable()
                    )
                ),
                new GreaterThanOrEqualDecimalRow(
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
                    new FieldInteger("facts", "quantity")
                ),
                new NotEqualDecimalRow(
                    new FieldInteger("facts", "quantity"),
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
                ),
            ]),
            select:
            [
                new SelectItemProjection(
                    "floor_of_decimal",
                    new FloorIntegerProjection(
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
                new SelectItemProjection(
                    "hundreds",
                    new RoundDecimalProjection(
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
                        new LiteralInteger(-2)
                    )
                ),
                new SelectItemProjection(
                    "computed_digits",
                    new RoundDecimalProjection(
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
                        new AddIntegerProjection([
                            new LiteralInteger(1),
                            new LiteralInteger(1),
                        ])
                    )
                ),
                new SelectItemProjection(
                    "days_to_ship",
                    new DateDiffDaysIntegerNullableProjection(
                        new FieldDateNullable("facts", "shipped_date"),
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
                    )
                ),
                new SelectItemProjection(
                    "one_minute_later",
                    new TimeAddSecondsTimeProjection(
                        new FieldTime("facts", "window_start"),
                        new LiteralInteger(60)
                    )
                ),
                new SelectItemProjection(
                    "sum3",
                    new AddIntegerProjection([
                        new LiteralInteger(1),
                        new LiteralInteger(2),
                        new FieldInteger("facts", "quantity"),
                    ])
                ),
                new SelectItemProjection(
                    "same_status",
                    new EqualStringProjection(
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
                        new LiteralString("shipped")
                    )
                ),
                new SelectItemProjection(
                    "either_id",
                    new IfUuidProjection(
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
                        new FieldUuid(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrdersTable().Name,
                                ]
                            ).TextValue,
                            new OrderUserIdColumn().Name.TextValue
                        ),
                        new FieldBoolean("facts", "is_paid")
                    )
                ),
                new SelectItemProjection(
                    "code",
                    new CoalesceStringProjection([
                        new LiteralStringNullable(),
                        new FieldStringNullable("facts", "coupon_code"),
                        new LiteralString("none"),
                    ])
                ),
                new SelectItemProjection(
                    "points_or_total",
                    new CoalesceDecimalProjection([
                        new FieldIntegerNullable("facts", "bonus_points"),
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
                    ])
                ),
                new SelectItemProjection(
                    "bonus_points_value",
                    new FieldIntegerNullable("facts", "bonus_points")
                ),
                new SelectItemProjection(
                    "paid_and_verified",
                    new AndProjection([
                        new FieldBoolean("facts", "is_paid"),
                        new EqualBooleanProjection(
                            new FieldBooleanNullable("facts", "is_verified"),
                            new LiteralBoolean(true)
                        ),
                    ])
                ),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and
    /// <see cref="AuditSchemaDataSet"/>, in no particular order.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("floor_of_decimal"), new LongColumnType()),
                    new Column(new String("hundreds"), new DoubleColumnType()),
                    new Column(new String("computed_digits"), new DoubleColumnType()),
                    new Column(new String("days_to_ship"), new LongColumnType()),
                    new Column(new String("one_minute_later"), new TimeColumnType()),
                    new Column(new String("sum3"), new LongColumnType()),
                    new Column(new String("same_status"), new BoolColumnType()),
                    new Column(new String("either_id"), new UuidColumnType()),
                    new Column(new String("code"), new StringColumnType()),
                    new Column(new String("points_or_total"), new DoubleColumnType()),
                    new Column(new String("bonus_points_value"), new LongColumnType()),
                    new Column(new String("paid_and_verified"), new BoolColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("floor_of_decimal"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(100))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("hundreds"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(100))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("computed_digits"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(100.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("days_to_ship"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("one_minute_later"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(
                                    new Time(new UShort(9), new UShort(1), new UShort(0))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("sum3"), new LongColumnType()),
                                new InvariantCell(new Long(5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("same_status"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("either_id"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000065-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("code"), new StringColumnType()),
                                new InvariantCell(new String("SPRING"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("points_or_total"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(10))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("bonus_points_value"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(10))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("paid_and_verified"),
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
                                    new String("floor_of_decimal"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(300))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("hundreds"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(300))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("computed_digits"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(300))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("days_to_ship"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("one_minute_later"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(
                                    new Time(new UShort(0), new UShort(0), new UShort(30))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("sum3"), new LongColumnType()),
                                new InvariantCell(new Long(6))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("same_status"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("either_id"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000069-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("code"), new StringColumnType()),
                                new InvariantCell(new String("none"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("points_or_total"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(300))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("bonus_points_value"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("paid_and_verified"),
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
                                    new String("floor_of_decimal"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(100))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("hundreds"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(100))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("computed_digits"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(100.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("days_to_ship"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("one_minute_later"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(
                                    new Time(
                                        new UShort(18),
                                        new UShort(46),
                                        new UShort(0)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("sum3"), new LongColumnType()),
                                new InvariantCell(new Long(4))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("same_status"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("either_id"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "0000006a-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("code"), new StringColumnType()),
                                new InvariantCell(new String("SUMMER"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("points_or_total"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(4))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("bonus_points_value"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(4))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("paid_and_verified"),
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
            ]
        );
}
