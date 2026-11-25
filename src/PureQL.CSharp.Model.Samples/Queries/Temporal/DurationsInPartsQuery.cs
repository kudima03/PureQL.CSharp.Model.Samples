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
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.ProjectionExpressions;
using PureQL.CSharp.Model.RowExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using Guid = Pure.Primitives.Guid.Guid;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Temporal;

/// <summary>
/// Selects, from schema_with_foreign_keys.orders inner joined to users on order_user_id,
/// with the seconds from last_login to placed_at plus 37.5 as the duration: order_id, the
/// duration integer-divided by 86400 as days, by 3600 after modulo 86400 as hours, by 60
/// after modulo 3600 as minutes, the duration modulo 60 as seconds, a label of the
/// duration integer-divided by 3600 as text and the minutes padded at the start to 2
/// characters with "0", joined by ":", and the running sum of the duration
/// integer-divided by 900 over the orders of the user in order of placed_at and order_id
/// from the first to the current, ordered by the duration modulo 86400 and order_id.
/// </summary>
public sealed record DurationsInPartsQuery
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
                                    new OrdersTable().Name,
                                ]
                            ).TextValue,
                            new OrderUserIdColumn().Name.TextValue
                        ),
                        new FieldUuid(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue,
                            new UserIdColumn().Name.TextValue
                        )
                    )
                ),
            ],
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
                    "days",
                    new IntegerDivideIntegerProjection(
                        new DatetimeDiffSecondsDecimalProjection(
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
                                new LiteralDecimal(37.5m)
                            ),
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
                        new LiteralInteger(86400)
                    )
                ),
                new SelectItemProjection(
                    "hours",
                    new IntegerDivideIntegerProjection(
                        new ModuloDecimalProjection(
                            new DatetimeDiffSecondsDecimalProjection(
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
                                    new LiteralDecimal(37.5m)
                                ),
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
                            new LiteralInteger(86400)
                        ),
                        new LiteralInteger(3600)
                    )
                ),
                new SelectItemProjection(
                    "minutes",
                    new IntegerDivideIntegerProjection(
                        new ModuloDecimalProjection(
                            new DatetimeDiffSecondsDecimalProjection(
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
                                    new LiteralDecimal(37.5m)
                                ),
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
                            new LiteralInteger(3600)
                        ),
                        new LiteralInteger(60)
                    )
                ),
                new SelectItemProjection(
                    "seconds",
                    new ModuloDecimalProjection(
                        new DatetimeDiffSecondsDecimalProjection(
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
                                new LiteralDecimal(37.5m)
                            ),
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
                        new LiteralInteger(60)
                    )
                ),
                new SelectItemProjection(
                    "label",
                    new ConcatWithProjection(
                        [
                            new AsStringDecimalStringProjection(
                                new IntegerDivideIntegerProjection(
                                    new DatetimeDiffSecondsDecimalProjection(
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
                                            new LiteralDecimal(37.5m)
                                        ),
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
                                    new LiteralInteger(3600)
                                )
                            ),
                            new PadStartStringProjection(
                                new AsStringDecimalStringProjection(
                                    new IntegerDivideIntegerProjection(
                                        new ModuloDecimalProjection(
                                            new DatetimeDiffSecondsDecimalProjection(
                                                new DatetimeAddSecondsDatetimeProjection(
                                                    new FieldDatetime(
                                                        new JoinedString(
                                                            new DotString(),
                                                            [
                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                new OrdersTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new PlacedAtColumn()
                                                            .Name
                                                            .TextValue
                                                    ),
                                                    new LiteralDecimal(37.5m)
                                                ),
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
                                            new LiteralInteger(3600)
                                        ),
                                        new LiteralInteger(60)
                                    )
                                ),
                                new LiteralInteger(2),
                                new LiteralString("0")
                            ),
                        ],
                        new LiteralString(":")
                    )
                ),
                new SelectItemProjection(
                    "quarter_hours_running",
                    new SumIntegerProjection(
                        new IntegerDivideIntegerRow(
                            new DatetimeDiffSecondsDecimalRow(
                                new DatetimeAddSecondsDatetimeRow(
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
                                    new LiteralDecimal(37.5m)
                                ),
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
                            new LiteralInteger(900)
                        ),
                        over: new WindowWithFrame(
                            [
                                new OrderItemRow(
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
                                new OrderItemRow(
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
                            ],
                            new Frame(new All()),
                            [
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
                            ]
                        )
                    )
                ),
            ],
            orderBy:
            [
                new OrderItemProjectionExpression(
                    new ModuloDecimalProjection(
                        new DatetimeDiffSecondsDecimalProjection(
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
                                new LiteralDecimal(37.5m)
                            ),
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
                        new LiteralInteger(86400)
                    )
                ),
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
                    new Column(new String("days"), new LongColumnType()),
                    new Column(new String("hours"), new LongColumnType()),
                    new Column(new String("minutes"), new LongColumnType()),
                    new Column(new String("seconds"), new DoubleColumnType()),
                    new Column(new String("label"), new StringColumnType()),
                    new Column(new String("quarter_hours_running"), new LongColumnType()),
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
                                            "00000069-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("days"), new LongColumnType()),
                                new InvariantCell(new Long(6))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("hours"), new LongColumnType()),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("minutes"), new LongColumnType()),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("seconds"), new DoubleColumnType()),
                                new InvariantCell(new Double(37.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("label"), new StringColumnType()),
                                new InvariantCell(new String("144:00"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("quarter_hours_running"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1052))
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
                                            "00000065-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("days"), new LongColumnType()),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("hours"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("minutes"), new LongColumnType()),
                                new InvariantCell(new Long(30))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("seconds"), new DoubleColumnType()),
                                new InvariantCell(new Double(37.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("label"), new StringColumnType()),
                                new InvariantCell(new String("1:30"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("quarter_hours_running"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(6))
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
                                new Column(new String("days"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("hours"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("minutes"), new LongColumnType()),
                                new InvariantCell(new Long(30))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("seconds"), new DoubleColumnType()),
                                new InvariantCell(new Double(37.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("label"), new StringColumnType()),
                                new InvariantCell(new String("26:30"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("quarter_hours_running"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(112))
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
                                new Column(new String("days"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("hours"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("minutes"), new LongColumnType()),
                                new InvariantCell(new Long(45))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("seconds"), new DoubleColumnType()),
                                new InvariantCell(new Double(37.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("label"), new StringColumnType()),
                                new InvariantCell(new String("26:45"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("quarter_hours_running"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(107))
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
                                            "0000006a-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("days"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("hours"), new LongColumnType()),
                                new InvariantCell(new Long(20))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("minutes"), new LongColumnType()),
                                new InvariantCell(new Long(15))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("seconds"), new DoubleColumnType()),
                                new InvariantCell(new Double(37.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("label"), new StringColumnType()),
                                new InvariantCell(new String("68:15"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("quarter_hours_running"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(273))
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
                                new Column(new String("days"), new LongColumnType()),
                                new InvariantCell(new Long(4))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("hours"), new LongColumnType()),
                                new InvariantCell(new Long(23))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("minutes"), new LongColumnType()),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("seconds"), new DoubleColumnType()),
                                new InvariantCell(new Double(37.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("label"), new StringColumnType()),
                                new InvariantCell(new String("119:00"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("quarter_hours_running"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(476))
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
