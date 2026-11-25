using Pure.Collections.Generic;
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
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.RowExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Conversions;

/// <summary>
/// Selects the text of each value of a group of schema_with_foreign_keys.orders grouped
/// by the startOfMonth of placed_on, which is left joined with
/// schema_with_foreign_keys.orders aliased shipped on the same order_id and an
/// order_status equal to shipped: the month as month, the count as orders, the sum of
/// order_total as revenue, the latest placed_at as latest, the nullable latest
/// shipped.placed_on as last_shipped, and as stamps the concatAll, ordered by placed_at
/// then order_id and separated by a comma and a space, of the microsecond of an instant,
/// an at sign, the startOfSecond of that instant, a slash and order_total divided by 3,
/// as text, where the instant is placed_at plus order_total divided by 100 seconds.
/// </summary>
public sealed record AsStringOfOperatorResultsQuery
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
                    JoinKind.Left,
                    new JoinedString(
                        new DotString(),
                        [
                            new RelationalSchemaWithForeignKeys().Name,
                            new OrdersTable().Name,
                        ]
                    ).TextValue,
                    new AndRow([
                        new EqualUuidRow(
                            new FieldUuid("shipped", new OrderIdColumn().Name.TextValue),
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
            groupBy:
            [
                new GroupKey(
                    "month",
                    new StartOfMonthDateRow(
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
            select:
            [
                new SelectItemGroup(
                    "month",
                    new AsStringDateStringGroup(new KeyDate("month"))
                ),
                new SelectItemGroup(
                    "orders",
                    new AsStringDecimalStringGroup(new CountGroup())
                ),
                new SelectItemGroup(
                    "revenue",
                    new AsStringDecimalStringGroup(
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
                new SelectItemGroup(
                    "latest",
                    new AsStringDatetimeStringGroup(
                        new MaxDatetimeGroup(
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
                new SelectItemGroup(
                    "last_shipped",
                    new AsStringDateStringNullableGroup(
                        new MaxDateNullableGroup(
                            new FieldDateNullable(
                                "shipped",
                                new PlacedOnColumn().Name.TextValue
                            )
                        )
                    )
                ),
                new SelectItemGroup(
                    "stamps",
                    new ConcatAllGroup(
                        new ConcatStringRow([
                            new AsStringDecimalStringRow(
                                new MicrosecondDatetimeIntegerRow(
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
                                        new DivideDecimalRow([
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
                                            new LiteralInteger(100),
                                        ])
                                    )
                                )
                            ),
                            new LiteralString("@"),
                            new AsStringDatetimeStringRow(
                                new StartOfSecondDatetimeRow(
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
                                        new DivideDecimalRow([
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
                                            new LiteralInteger(100),
                                        ])
                                    )
                                )
                            ),
                            new LiteralString("/"),
                            new AsStringDecimalStringRow(
                                new DivideDecimalRow([
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
                                    new LiteralInteger(3),
                                ])
                            ),
                        ]),
                        new LiteralString(", "),
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
                        ]
                    )
                ),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and
    /// <see cref="AuditSchemaDataSet"/>, in no particular order.
    /// The quotients of order_total by 3 are computed with Python decimal and rounded as the
    /// specification rounds a decimal, to the most decimal places that fit in 28 digits.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("month"), new StringColumnType()),
                    new Column(new String("orders"), new StringColumnType()),
                    new Column(new String("revenue"), new StringColumnType()),
                    new Column(new String("latest"), new StringColumnType()),
                    new Column(new String("last_shipped"), new StringColumnType()),
                    new Column(new String("stamps"), new StringColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("month"), new StringColumnType()),
                                new InvariantCell(new String("2024-06-01"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new StringColumnType()),
                                new InvariantCell(new String("6"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("revenue"), new StringColumnType()),
                                new InvariantCell(new String("826.25"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("latest"), new StringColumnType()),
                                new InvariantCell(new String("2024-06-06T15:00:00Z"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_shipped"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("2024-06-05"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("stamps"), new StringColumnType()),
                                new InvariantCell(
                                    new String(
                                        "5000@2024-06-01T10:00:01Z/33.5, 500000@2024-06-02T11:00:00Z/16.66666666666666666666666667, 0@2024-06-03T12:00:02Z/66.66666666666666666666666667, 752500@2024-06-04T13:00:00Z/25.08333333333333333333333333, 0@2024-06-05T14:00:03Z/100, 5000@2024-06-06T15:00:01Z/33.5"
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
