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
using PureQL.CSharp.Model.RowExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.GroupBy;

/// <summary>
/// Groups schema_with_foreign_keys.orders joined to schema_with_foreign_keys.users on
/// order_user_id by due_month, the start of the month of placed_on plus 30 days, and by
/// window_end, shift_start plus 7200 seconds. Keeps the groups whose latest placed_on
/// plus 1 month is before 2024-07-06 and selects both keys, the latest placed_on plus 30
/// days, the due_month plus as many days as orders, the earliest placed_at plus 0.5
/// seconds, the due_month plus 1 month and the count of orders shipped more than 3 days
/// after placed_on (shipped_date is the stand-in for the nullable date the fixtures lack:
/// placed_on plus order_total divided by 50 in whole days where the order is shipped, a
/// typed null otherwise), ordered by the due_month plus 1 month and by window_end.
/// </summary>
public sealed record AddedAmountsInAGroupedQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainGroupedQueryWithoutPagination(
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
            groupBy:
            [
                new GroupKey(
                    "due_month",
                    new StartOfMonthDateRow(
                        new DateAddDaysDateRow(
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
                            new LiteralInteger(30)
                        )
                    )
                ),
                new GroupKey(
                    "window_end",
                    new TimeAddSecondsTimeRow(
                        new FieldTime(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue,
                            new ShiftStartColumn().Name.TextValue
                        ),
                        new LiteralInteger(7200)
                    )
                ),
            ],
            having: new LessThanDateGroup(
                new DateAddMonthsDateGroup(
                    new MaxDateGroup(
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
                    new LiteralInteger(1)
                ),
                new LiteralDate(new DateOnly(2024, 7, 6))
            ),
            select:
            [
                new SelectItemGroup("due_month", new KeyDate("due_month")),
                new SelectItemGroup("window_end", new KeyTime("window_end")),
                new SelectItemGroup(
                    "last_due",
                    new DateAddDaysDateGroup(
                        new MaxDateGroup(
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
                        new LiteralInteger(30)
                    )
                ),
                new SelectItemGroup(
                    "by_as_many_days_as_orders",
                    new DateAddDaysDateGroup(new KeyDate("due_month"), new CountGroup())
                ),
                new SelectItemGroup(
                    "first_reminder",
                    new DatetimeAddSecondsDatetimeGroup(
                        new MinDatetimeGroup(
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
                        new LiteralDecimal(0.5m)
                    )
                ),
                new SelectItemGroup(
                    "next_month",
                    new DateAddMonthsDateGroup(
                        new KeyDate("due_month"),
                        new LiteralInteger(1)
                    )
                ),
                new SelectItemGroup(
                    "late",
                    new CountGroup(
                        predicate: new GreaterThanDateRow(
                            new IfDateNullableRow(
                                new DateAddDaysDateRow(
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
                                    new IntegerDivideIntegerRow(
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
                                        new LiteralInteger(50)
                                    )
                                ),
                                new LiteralDateNullable(),
                                new EqualStringRow(
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
                            new DateAddDaysDateRow(
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
                                new LiteralInteger(3)
                            )
                        )
                    )
                ),
            ],
            orderBy:
            [
                new OrderItemGroupExpression(
                    new DateAddMonthsDateGroup(
                        new KeyDate("due_month"),
                        new LiteralInteger(1)
                    )
                ),
                new OrderItemGroupExpression(new KeyTime("window_end")),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and <see cref="AuditSchemaDataSet"/>,
    /// in this order. A datetime with a fraction of a second is the text of its value, as
    /// the specification spells it.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("due_month"), new DateColumnType()),
                    new Column(new String("window_end"), new TimeColumnType()),
                    new Column(new String("last_due"), new DateColumnType()),
                    new Column(
                        new String("by_as_many_days_as_orders"),
                        new DateColumnType()
                    ),
                    new Column(new String("first_reminder"), new DateTimeColumnType()),
                    new Column(new String("next_month"), new DateColumnType()),
                    new Column(new String("late"), new LongColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("due_month"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(1),
                                        new UShort(7),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("window_end"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(
                                    new Time(new UShort(11), new UShort(0), new UShort(0))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("last_due"), new DateColumnType()),
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
                                    new String("by_as_many_days_as_orders"),
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
                                    new String("first_reminder"),
                                    new DateTimeColumnType()
                                ),
                                new InvariantCell(new String("2024-06-01T10:00:00.5Z"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("next_month"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(1),
                                        new UShort(8),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("late"), new LongColumnType()),
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
                                new Column(new String("due_month"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(1),
                                        new UShort(7),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("window_end"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(
                                    new Time(new UShort(12), new UShort(0), new UShort(0))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("last_due"), new DateColumnType()),
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
                                    new String("by_as_many_days_as_orders"),
                                    new DateColumnType()
                                ),
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
                                    new String("first_reminder"),
                                    new DateTimeColumnType()
                                ),
                                new InvariantCell(new String("2024-06-03T12:00:00.5Z"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("next_month"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(1),
                                        new UShort(8),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("late"), new LongColumnType()),
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
