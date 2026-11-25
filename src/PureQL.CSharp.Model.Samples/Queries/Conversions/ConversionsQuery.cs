using Pure.Collections.Generic;
using Pure.Primitives.Bool;
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
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.ProjectionExpressions;
using PureQL.CSharp.Model.RowExpressions;
using PureQL.CSharp.Model.Samples.Cells;
using Column = Pure.RelationalSchema.Column.Column;
using DateTime = Pure.Primitives.DateTime.DateTime;
using Guid = Pure.Primitives.Guid.Guid;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Conversions;

/// <summary>
/// Selects from schema_with_foreign_keys.orders inner joined to
/// schema_with_foreign_keys.users on user_id equal to order_user_id, where asDate of
/// placed_at is at most 2024-06-05: order_id, a label concatenating "Order of ", asString
/// of placed_on, ": " and asString of order_total, asString of user_active, asString of
/// placed_on for a shipped order and of a typed null otherwise, asDate and asTime of
/// placed_at, asDatetime of placed_on, and whether asDate of the user's last_login equals
/// placed_on, ordered by order_id.
/// </summary>
public sealed record ConversionsQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithoutPagination(
            from: new FromEntity(
                new JoinedString(
                    new DotString(),
                    [new RelationalSchemaWithForeignKeys().Name, new OrdersTable().Name]
                ).TextValue
            ),
            joins:
            [
                new JoinEntity(
                    JoinKind.Inner,
                    new JoinedString(
                        new DotString(),
                        [
                            new RelationalSchemaWithForeignKeys().Name,
                            new UsersTable().Name,
                        ]
                    ).TextValue,
                    new EqualUuidRow(
                        new FieldUuid(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue,
                            new UserIdColumn().Name.TextValue
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
                        )
                    )
                ),
            ],
            where: new LessThanOrEqualDateRow(
                new AsDateDatetimeDateRow(
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
                ),
                new LiteralDate(new DateOnly(2024, 6, 5))
            ),
            select:
            [
                new SelectItemProjection(
                    "order_id",
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
                ),
                new SelectItemProjection(
                    "label",
                    new ConcatStringProjection([
                        new LiteralString("Order of "),
                        new AsStringDateStringProjection(
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
                        new LiteralString(": "),
                        new AsStringDecimalStringProjection(
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
                    ])
                ),
                new SelectItemProjection(
                    "user_active_text",
                    new AsStringBooleanStringProjection(
                        new FieldBoolean(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue,
                            new UserActiveColumn().Name.TextValue
                        )
                    )
                ),
                new SelectItemProjection(
                    "shipped_on_text",
                    new AsStringDateStringNullableProjection(
                        new IfDateNullableProjection(
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
                            new LiteralDateNullable(),
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
                        )
                    )
                ),
                new SelectItemProjection(
                    "placed_day",
                    new AsDateDatetimeDateProjection(
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
                ),
                new SelectItemProjection(
                    "placed_time",
                    new AsTimeDatetimeTimeProjection(
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
                ),
                new SelectItemProjection(
                    "placed_day_start",
                    new AsDatetimeDateDatetimeProjection(
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
                    "logged_in_on_the_order_day",
                    new EqualDateProjection(
                        new AsDateDatetimeDateProjection(
                            new FieldDatetime(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new UsersTable().Name,
                                    ]
                                ).TextValue,
                                new LastLoginColumn().Name.TextValue
                            )
                        ),
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
            ],
            orderBy:
            [
                new OrderItemProjectionExpression(
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
                    new OrderIdColumn(),
                    new Column(new String("label"), new StringColumnType()),
                    new Column(new String("user_active_text"), new StringColumnType()),
                    new Column(new String("shipped_on_text"), new StringColumnType()),
                    new Column(new String("placed_day"), new DateColumnType()),
                    new Column(new String("placed_time"), new TimeColumnType()),
                    new Column(new String("placed_day_start"), new DateTimeColumnType()),
                    new Column(
                        new String("logged_in_on_the_order_day"),
                        new BoolColumnType()
                    ),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new OrderIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000065-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("label"), new StringColumnType()),
                                new InvariantCell(
                                    new String("Order of 2024-06-01: 100.5")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("user_active_text"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("true"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("shipped_on_text"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("2024-06-01"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("placed_day"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(1),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("placed_time"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(
                                    new Time(new UShort(10), new UShort(0), new UShort(0))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("placed_day_start"),
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
                                            new UShort(0),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("logged_in_on_the_order_day"),
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
                                new OrderIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000066-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("label"), new StringColumnType()),
                                new InvariantCell(new String("Order of 2024-06-02: 50"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("user_active_text"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("true"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("shipped_on_text"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("placed_day"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(2),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("placed_time"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(
                                    new Time(new UShort(11), new UShort(0), new UShort(0))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("placed_day_start"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(2),
                                            new UShort(6),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(0),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("logged_in_on_the_order_day"),
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
                                new OrderIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000067-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("label"), new StringColumnType()),
                                new InvariantCell(new String("Order of 2024-06-03: 200"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("user_active_text"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("false"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("shipped_on_text"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("2024-06-03"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("placed_day"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(3),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("placed_time"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(
                                    new Time(new UShort(12), new UShort(0), new UShort(0))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("placed_day_start"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(3),
                                            new UShort(6),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(0),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("logged_in_on_the_order_day"),
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
                                new OrderIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000068-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("label"), new StringColumnType()),
                                new InvariantCell(
                                    new String("Order of 2024-06-04: 75.25")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("user_active_text"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("true"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("shipped_on_text"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("placed_day"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(4),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("placed_time"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(
                                    new Time(new UShort(13), new UShort(0), new UShort(0))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("placed_day_start"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(4),
                                            new UShort(6),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(0),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("logged_in_on_the_order_day"),
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
                                new OrderIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000069-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("label"), new StringColumnType()),
                                new InvariantCell(new String("Order of 2024-06-05: 300"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("user_active_text"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("true"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("shipped_on_text"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("2024-06-05"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("placed_day"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(5),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("placed_time"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(
                                    new Time(new UShort(14), new UShort(0), new UShort(0))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("placed_day_start"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(5),
                                            new UShort(6),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(0),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("logged_in_on_the_order_day"),
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
