using Pure.Collections.Generic;
using Pure.Primitives.Bool;
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
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Expressions;

/// <summary>
/// Selects, from schema_with_foreign_keys.orders inner joined on order_id to the subquery
/// facts of inline rows (is_paid, name, coupon_code, shipped_date and window_start of
/// each order), ten boolean columns: a is a coalesce of an if over typed nulls and the
/// decimal 2.5 equal to the integer 1, b an if of a typed null or order_total equal to
/// the decimal 75.25, c the round of order_total equal to its floor, d the days from
/// placed_on to shipped_date less than 1.5, e the count of rows equal to the decimal 6, f
/// the min of placed_on equal to 2024-06-01, g the average of placed_on in the dates
/// 2024-06-04 and 2024-07-04, h "#" joined to name in the code column of the subquery
/// codes (the coupon_code of facts), i the average of window_start not equal to
/// window_start plus 1 divided by 2 seconds, and j placed_at plus 60 seconds less than
/// the max of placed_at. Ordered by an if of a null or name by is_paid, by order_id
/// coalesced with a null uuid, and by an if of order_total rounded to 1 digit or 0
/// descending.
/// </summary>
public sealed record OperandsReadFromDeclaredTypesQuery
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
                                new LiteralUuid(new Guid(101, 0, 0, new byte[8]))
                            ),
                            new RowItem("is_paid", new LiteralBoolean(true)),
                            new RowItem("name", new LiteralString("Ann")),
                            new RowItem("coupon_code", new LiteralString("#Ann")),
                            new RowItem(
                                "shipped_date",
                                new LiteralDate(new DateOnly(2024, 6, 2))
                            ),
                            new RowItem(
                                "window_start",
                                new LiteralTime(new TimeOnly(9, 0, 0))
                            ),
                        ],
                        [
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new Guid(102, 0, 0, new byte[8]))
                            ),
                            new RowItem("is_paid", new LiteralBoolean(false)),
                            new RowItem("name", new LiteralString("Ann")),
                            new RowItem("coupon_code", new LiteralStringNullable()),
                            new RowItem("shipped_date", new LiteralDateNullable()),
                            new RowItem(
                                "window_start",
                                new LiteralTime(new TimeOnly(10, 0, 0))
                            ),
                        ],
                        [
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new Guid(103, 0, 0, new byte[8]))
                            ),
                            new RowItem("is_paid", new LiteralBoolean(true)),
                            new RowItem("name", new LiteralString("Bob")),
                            new RowItem("coupon_code", new LiteralString("#Eve")),
                            new RowItem(
                                "shipped_date",
                                new LiteralDate(new DateOnly(2024, 6, 6))
                            ),
                            new RowItem(
                                "window_start",
                                new LiteralTime(new TimeOnly(11, 30, 0))
                            ),
                        ],
                        [
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new Guid(104, 0, 0, new byte[8]))
                            ),
                            new RowItem("is_paid", new LiteralBoolean(false)),
                            new RowItem("name", new LiteralString("Cara")),
                            new RowItem("coupon_code", new LiteralStringNullable()),
                            new RowItem("shipped_date", new LiteralDateNullable()),
                            new RowItem(
                                "window_start",
                                new LiteralTime(new TimeOnly(8, 0, 0))
                            ),
                        ],
                        [
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new Guid(105, 0, 0, new byte[8]))
                            ),
                            new RowItem("is_paid", new LiteralBoolean(true)),
                            new RowItem("name", new LiteralString("Cara")),
                            new RowItem("coupon_code", new LiteralString("#Cara")),
                            new RowItem(
                                "shipped_date",
                                new LiteralDate(new DateOnly(2024, 6, 6))
                            ),
                            new RowItem(
                                "window_start",
                                new LiteralTime(new TimeOnly(12, 30, 0))
                            ),
                        ],
                        [
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new Guid(106, 0, 0, new byte[8]))
                            ),
                            new RowItem("is_paid", new LiteralBoolean(false)),
                            new RowItem("name", new LiteralString("Dan")),
                            new RowItem("coupon_code", new LiteralStringNullable()),
                            new RowItem(
                                "shipped_date",
                                new LiteralDate(new DateOnly(2024, 6, 6))
                            ),
                            new RowItem(
                                "window_start",
                                new LiteralTime(new TimeOnly(14, 0, 0))
                            ),
                        ],
                    ]
                ),
                new SubqueryQuery(
                    "codes",
                    new PlainQueryWithoutPaginationWithoutOrderBy(
                        from: new FromSubquery("facts"),
                        select:
                        [
                            new SelectItemProjection(
                                "code",
                                new FieldStringNullable("facts", "coupon_code")
                            ),
                        ]
                    )
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
            select:
            [
                new SelectItemProjection(
                    "a",
                    new EqualDecimalProjection(
                        new CoalesceDecimalProjection([
                            new IfIntegerNullableProjection(
                                new CoalesceIntegerNullableProjection([
                                    new LiteralIntegerNullable(),
                                    new LiteralIntegerNullable(),
                                ]),
                                new LiteralIntegerNullable(),
                                new FieldBoolean("facts", "is_paid")
                            ),
                            new LiteralDecimal(2.5m),
                        ]),
                        new LiteralInteger(1)
                    )
                ),
                new SelectItemProjection(
                    "b",
                    new EqualDecimalProjection(
                        new IfDecimalNullableProjection(
                            new LiteralIntegerNullable(),
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
                            new FieldBoolean("facts", "is_paid")
                        ),
                        new LiteralDecimal(75.25m)
                    )
                ),
                new SelectItemProjection(
                    "c",
                    new EqualDecimalProjection(
                        new RoundWithoutDigitsIntegerProjection(
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
                    )
                ),
                new SelectItemProjection(
                    "d",
                    new LessThanDecimalProjection(
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
                        ),
                        new LiteralDecimal(1.5m)
                    )
                ),
                new SelectItemProjection(
                    "e",
                    new EqualDecimalProjection(
                        new CountProjection(),
                        new LiteralDecimal(6m)
                    )
                ),
                new SelectItemProjection(
                    "f",
                    new EqualDateProjection(
                        new MinDateProjection(
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
                        new LiteralDate(new DateOnly(2024, 6, 1))
                    )
                ),
                new SelectItemProjection(
                    "g",
                    new InDateProjection(
                        new AverageDateProjection(
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
                        new ListLiteralDate([
                            new DateOnly(2024, 6, 4),
                            new DateOnly(2024, 7, 4),
                        ])
                    )
                ),
                new SelectItemProjection(
                    "h",
                    new InStringProjection(
                        new ConcatStringProjection([
                            new LiteralString("#"),
                            new FieldString("facts", "name"),
                        ]),
                        new ListSubqueryColumnStringNullable("codes", "code")
                    )
                ),
                new SelectItemProjection(
                    "i",
                    new NotEqualTimeProjection(
                        new AverageTimeProjection(new FieldTime("facts", "window_start")),
                        new TimeAddSecondsTimeProjection(
                            new FieldTime("facts", "window_start"),
                            new DivideDecimalProjection([
                                new LiteralInteger(1),
                                new LiteralInteger(2),
                            ])
                        )
                    )
                ),
                new SelectItemProjection(
                    "j",
                    new LessThanDatetimeProjection(
                        new DatetimeAddSecondsDatetimeProjection(
                            new FieldDatetime(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new OrdersTable().Name,
                                    ]
                                ).TextValue,
                                new PlacedAtColumn().Name.TextValue
                            ),
                            new LiteralInteger(60)
                        ),
                        new MaxDatetimeProjection(
                            new FieldDatetime(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new OrdersTable().Name,
                                    ]
                                ).TextValue,
                                new PlacedAtColumn().Name.TextValue
                            )
                        )
                    )
                ),
            ],
            orderBy:
            [
                new OrderItemProjectionExpression(
                    new IfStringNullableProjection(
                        new LiteralStringNullable(),
                        new FieldString("facts", "name"),
                        new FieldBoolean("facts", "is_paid")
                    )
                ),
                new OrderItemProjectionExpression(
                    new CoalesceUuidProjection([
                        new LiteralUuidNullable(),
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
                    ])
                ),
                new OrderItemProjectionExpression(
                    new IfDecimalProjection(
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
                            new LiteralInteger(1)
                        ),
                        new LiteralInteger(0),
                        new FieldBoolean("facts", "is_paid")
                    ),
                    SortDirection.Desc
                ),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and
    /// <see cref="AuditSchemaDataSet"/>, in this order.
    /// The average of dates and times is rounded as the specification rounds a mean, a
    /// value halfway going to the later one.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("a"), new BoolColumnType()),
                    new Column(new String("b"), new BoolColumnType()),
                    new Column(new String("c"), new BoolColumnType()),
                    new Column(new String("d"), new BoolColumnType()),
                    new Column(new String("e"), new BoolColumnType()),
                    new Column(new String("f"), new BoolColumnType()),
                    new Column(new String("g"), new BoolColumnType()),
                    new Column(new String("h"), new BoolColumnType()),
                    new Column(new String("i"), new BoolColumnType()),
                    new Column(new String("j"), new BoolColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("a"), new BoolColumnType()),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("b"), new BoolColumnType()),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("c"), new BoolColumnType()),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("d"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("e"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("f"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("g"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("h"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("i"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("j"), new BoolColumnType()),
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
                                new Column(new String("a"), new BoolColumnType()),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("b"), new BoolColumnType()),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("c"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("d"), new BoolColumnType()),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("e"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("f"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("g"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("h"), new BoolColumnType()),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("i"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("j"), new BoolColumnType()),
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
                                new Column(new String("a"), new BoolColumnType()),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("b"), new BoolColumnType()),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("c"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("d"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("e"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("f"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("g"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("h"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("i"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("j"), new BoolColumnType()),
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
                                new Column(new String("a"), new BoolColumnType()),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("b"), new BoolColumnType()),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("c"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("d"), new BoolColumnType()),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("e"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("f"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("g"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("h"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("i"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("j"), new BoolColumnType()),
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
                                new Column(new String("a"), new BoolColumnType()),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("b"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("c"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("d"), new BoolColumnType()),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("e"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("f"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("g"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("h"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("i"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("j"), new BoolColumnType()),
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
                                new Column(new String("a"), new BoolColumnType()),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("b"), new BoolColumnType()),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("c"), new BoolColumnType()),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("d"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("e"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("f"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("g"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("h"), new BoolColumnType()),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("i"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("j"), new BoolColumnType()),
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
