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

namespace PureQL.CSharp.Model.Samples.Queries.Conversions;

/// <summary>
/// Selects from schema_with_foreign_keys.orders a single row, with no where: summary, a
/// label concatenating asString of the count, " orders, ", asString of the sum of
/// order_total and " in total", last_day, asDate of the latest placed_at, last_day_label,
/// asString of that day with "no orders" through coalesce, first_day_start, asDatetime of
/// the earliest placed_on, days_with_orders, the count of orders whose asTime of
/// placed_at is not 12:00:00, and any_paid, asString of whether any order_status equals
/// "shipped".
/// </summary>
public sealed record ConversionsInATotalQuery
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
            aggregate:
            [
                new SelectItemTotal(
                    "summary",
                    new ConcatStringTotal([
                        new AsStringDecimalStringTotal(new CountTotal()),
                        new LiteralString(" orders, "),
                        new AsStringDecimalStringTotal(
                            new SumDecimalTotal(
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
                        new LiteralString(" in total"),
                    ])
                ),
                new SelectItemTotal(
                    "last_day",
                    new AsDateDatetimeDateNullableTotal(
                        new MaxDatetimeNullableTotal(
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
                new SelectItemTotal(
                    "last_day_label",
                    new CoalesceStringTotal([
                        new AsStringDateStringNullableTotal(
                            new AsDateDatetimeDateNullableTotal(
                                new MaxDatetimeNullableTotal(
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
                        new LiteralString("no orders"),
                    ])
                ),
                new SelectItemTotal(
                    "first_day_start",
                    new AsDatetimeDateDatetimeNullableTotal(
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
                        )
                    )
                ),
                new SelectItemTotal(
                    "days_with_orders",
                    new CountTotal(
                        predicate: new NotEqualTimeRow(
                            new AsTimeDatetimeTimeRow(
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
                            new LiteralTime(new TimeOnly(12, 0, 0))
                        )
                    )
                ),
                new SelectItemTotal(
                    "any_paid",
                    new AsStringBooleanStringTotal(
                        new AnyTotal(
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
                        )
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
                    new Column(new String("summary"), new StringColumnType()),
                    new Column(new String("last_day"), new DateColumnType()),
                    new Column(new String("last_day_label"), new StringColumnType()),
                    new Column(new String("first_day_start"), new DateTimeColumnType()),
                    new Column(new String("days_with_orders"), new LongColumnType()),
                    new Column(new String("any_paid"), new StringColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("summary"), new StringColumnType()),
                                new InvariantCell(new String("6 orders, 826.25 in total"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("last_day"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(6),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_day_label"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("2024-06-06"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_day_start"),
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
                                    new String("days_with_orders"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("any_paid"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("true"))
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
