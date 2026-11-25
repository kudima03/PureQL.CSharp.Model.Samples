using Pure.Collections.Generic;
using Pure.Primitives.Bool;
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
using PureQL.CSharp.Model.RowExpressions;
using PureQL.CSharp.Model.TotalExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Aggregates;

/// <summary>
/// Selects, as one row aggregated over schema_with_foreign_keys.orders of one tenant
/// (order_tenant_id equal to the uuid 00000385-0000-0000-0000-000000000000), the distinct
/// order_status values joined by ", " as statuses; placed_on as text joined by "; " in
/// order of placed_at and order_id as order_dates; order_total as text joined by " + " in
/// order of order_total descending and order_id, over the orders whose order_status
/// equals "shipped", as paid_totals; whether statuses is not the empty string as
/// has_statuses; statuses with the empty string turned into "no orders" by nullIfEqual
/// and coalesce as statuses_or_placeholder; and the length of statuses as
/// number_of_characters.
/// </summary>
public sealed record ConcatAllInATotalQuery
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
            where: new EqualUuidRow(
                new FieldUuid(
                    new JoinedString(
                        new DotString(),
                        [
                            new RelationalSchemaWithForeignKeys().Name,
                            new OrdersTable().Name,
                        ]
                    ).TextValue,
                    new OrderTenantIdColumn().Name.TextValue
                ),
                new LiteralUuid(new Guid(901, 0, 0, new byte[8]))
            ),
            aggregate:
            [
                new SelectItemTotal(
                    "statuses",
                    new ConcatAllTotal(
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
                        new LiteralString(", "),
                        [
                            new OrderItemRow(
                                new FieldString(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new OrdersTable().Name,
                                        ]
                                    ).TextValue,
                                    new OrderStatusColumn().Name.TextValue
                                )
                            ),
                        ],
                        distinct: true
                    )
                ),
                new SelectItemTotal(
                    "order_dates",
                    new ConcatAllTotal(
                        new AsStringDateStringRow(
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
                        new LiteralString("; "),
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
                new SelectItemTotal(
                    "paid_totals",
                    new ConcatAllTotal(
                        new AsStringDecimalStringRow(
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
                        new LiteralString(" + "),
                        [
                            new OrderItemRow(
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
                                SortDirection.Desc
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
                        predicate: new EqualStringRow(
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
                ),
                new SelectItemTotal(
                    "has_statuses",
                    new NotEqualStringTotal(
                        new ConcatAllTotal(
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
                            new LiteralString(", "),
                            [
                                new OrderItemRow(
                                    new FieldString(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new OrdersTable().Name,
                                            ]
                                        ).TextValue,
                                        new OrderStatusColumn().Name.TextValue
                                    )
                                ),
                            ],
                            distinct: true
                        ),
                        new LiteralString("")
                    )
                ),
                new SelectItemTotal(
                    "statuses_or_placeholder",
                    new CoalesceStringTotal([
                        new NullIfEqualStringNullableTotal(
                            new ConcatAllTotal(
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
                                new LiteralString(", "),
                                [
                                    new OrderItemRow(
                                        new FieldString(
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new RelationalSchemaWithForeignKeys().Name,
                                                    new OrdersTable().Name,
                                                ]
                                            ).TextValue,
                                            new OrderStatusColumn().Name.TextValue
                                        )
                                    ),
                                ],
                                distinct: true
                            ),
                            new LiteralString("")
                        ),
                        new LiteralString("no orders"),
                    ])
                ),
                new SelectItemTotal(
                    "number_of_characters",
                    new LengthIntegerTotal(
                        new ConcatAllTotal(
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
                            new LiteralString(", "),
                            [
                                new OrderItemRow(
                                    new FieldString(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new OrdersTable().Name,
                                            ]
                                        ).TextValue,
                                        new OrderStatusColumn().Name.TextValue
                                    )
                                ),
                            ],
                            distinct: true
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
                    new Column(new String("statuses"), new StringColumnType()),
                    new Column(new String("order_dates"), new StringColumnType()),
                    new Column(new String("paid_totals"), new StringColumnType()),
                    new Column(new String("has_statuses"), new BoolColumnType()),
                    new Column(
                        new String("statuses_or_placeholder"),
                        new StringColumnType()
                    ),
                    new Column(new String("number_of_characters"), new LongColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("statuses"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("pending, shipped"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("order_dates"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(
                                    new String("2024-06-01; 2024-06-02; 2024-06-03")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("paid_totals"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("200 + 100.5"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("has_statuses"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("statuses_or_placeholder"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("pending, shipped"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("number_of_characters"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(16))
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
