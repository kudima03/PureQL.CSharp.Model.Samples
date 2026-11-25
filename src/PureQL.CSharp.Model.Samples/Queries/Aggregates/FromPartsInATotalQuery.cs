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
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.RowExpressions;
using PureQL.CSharp.Model.Samples.Cells;
using PureQL.CSharp.Model.TotalExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using DateTime = Pure.Primitives.DateTime.DateTime;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Aggregates;

/// <summary>
/// Totals over schema_with_foreign_keys.orders inner joined to
/// schema_with_foreign_keys.users on order_user_id = user_id, where placed_on is on or
/// after dateFromParts of 2024, 6 and 3: year_end (dateFromParts of the maximum year of
/// placed_on, 12 and 31), latest_start_hour (timeFromParts of the maximum hour of
/// shift_start, 0 and 0), first_window (datetimeFromParts of the minimum placed_on and
/// the minimum shift_start), orders_this_year (the count of orders placed before
/// dateFromParts of 2023 + 1, 6 and 5) and fixed_instant (datetimeFromParts of the date
/// 2024-01-31 and the time 18:30:00).
/// </summary>
public sealed record FromPartsInATotalQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainTotalQuery(
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
            where: new GreaterThanOrEqualDateRow(
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
                new DateFromPartsDateRow(
                    new LiteralInteger(2024),
                    new LiteralInteger(6),
                    new LiteralInteger(3)
                )
            ),
            aggregate:
            [
                new SelectItemTotal(
                    "year_end",
                    new DateFromPartsDateNullableTotal(
                        new MaxIntegerNullableTotal(
                            new YearDateIntegerRow(
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
                        new LiteralInteger(12),
                        new LiteralInteger(31)
                    )
                ),
                new SelectItemTotal(
                    "latest_start_hour",
                    new TimeFromPartsTimeNullableTotal(
                        new MaxIntegerNullableTotal(
                            new HourTimeIntegerRow(
                                new FieldTime(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new UsersTable().Name,
                                        ]
                                    ).TextValue,
                                    new ShiftStartColumn().Name.TextValue
                                )
                            )
                        ),
                        new LiteralInteger(0),
                        new LiteralInteger(0)
                    )
                ),
                new SelectItemTotal(
                    "first_window",
                    new DatetimeFromPartsDatetimeNullableTotal(
                        new MinDateNullableTotal(
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
                        new MinTimeNullableTotal(
                            new FieldTime(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new UsersTable().Name,
                                    ]
                                ).TextValue,
                                new ShiftStartColumn().Name.TextValue
                            )
                        )
                    )
                ),
                new SelectItemTotal(
                    "orders_this_year",
                    new CountTotal(
                        new LessThanDateRow(
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
                            new DateFromPartsDateRow(
                                new AddIntegerRow([
                                    new LiteralInteger(2023),
                                    new LiteralInteger(1),
                                ]),
                                new LiteralInteger(6),
                                new LiteralInteger(5)
                            )
                        )
                    )
                ),
                new SelectItemTotal(
                    "fixed_instant",
                    new DatetimeFromPartsDatetimeTotal(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralTime(new TimeOnly(18, 30, 0))
                    )
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
                    new Column(new String("year_end"), new DateColumnType()),
                    new Column(new String("latest_start_hour"), new TimeColumnType()),
                    new Column(new String("first_window"), new DateTimeColumnType()),
                    new Column(new String("orders_this_year"), new LongColumnType()),
                    new Column(new String("fixed_instant"), new DateTimeColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("year_end"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(31),
                                        new UShort(12),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("latest_start_hour"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(
                                    new Time(new UShort(11), new UShort(0), new UShort(0))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_window"),
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
                                            new UShort(9),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("orders_this_year"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("fixed_instant"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(31),
                                            new UShort(1),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(18),
                                            new UShort(30),
                                            new UShort(0)
                                        )
                                    )
                                )
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
