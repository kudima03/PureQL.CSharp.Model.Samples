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
using PureQL.CSharp.Model.GroupExpressions;
using PureQL.CSharp.Model.Keys;
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.RowExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using Guid = Pure.Primitives.Guid.Guid;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.GroupBy;

/// <summary>
/// Groups schema_with_foreign_keys.orders, where an order's quantity (1 for the tenant
/// with the integer id 901, else 2) plus the integer 1 equals the decimal 2, by
/// order_user_id as user_id, and selects user_id, the count as orders, the sum of the
/// quantity as items, its average as avg_items, the min of order_total as smallest and
/// over all rows as smallest_overall, the coalesced max of the bonus points (none for the
/// user with the integer id 2, else order_total divided by 25 in whole numbers) with the
/// decimal 0.5 as points, the max quantity when the count is greater than 1 and else the
/// nullable average of the discount (none while the order is pending, else a tenth of
/// order_total) as best, and the count rounded to tens as rounded.
/// </summary>
public sealed record DeclaredOperatorTypesInGroupsQuery
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
            where: new EqualDecimalRow(
                new AddIntegerRow([
                    new IfIntegerRow(
                        new LiteralInteger(1),
                        new LiteralInteger(2),
                        new EqualUuidRow(
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
                            new LiteralUuid(new System.Guid(901, 0, 0, new byte[8]))
                        )
                    ),
                    new LiteralInteger(1),
                ]),
                new LiteralDecimal(2m)
            ),
            groupBy:
            [
                new GroupKey(
                    "user_id",
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
                ),
            ],
            select:
            [
                new SelectItemGroup("user_id", new KeyUuid("user_id")),
                new SelectItemGroup("orders", new CountGroup()),
                new SelectItemGroup(
                    "items",
                    new SumIntegerGroup(
                        new IfIntegerRow(
                            new LiteralInteger(1),
                            new LiteralInteger(2),
                            new EqualUuidRow(
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
                                new LiteralUuid(new System.Guid(901, 0, 0, new byte[8]))
                            )
                        )
                    )
                ),
                new SelectItemGroup(
                    "avg_items",
                    new AverageDecimalGroup(
                        new IfIntegerRow(
                            new LiteralInteger(1),
                            new LiteralInteger(2),
                            new EqualUuidRow(
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
                                new LiteralUuid(new System.Guid(901, 0, 0, new byte[8]))
                            )
                        )
                    )
                ),
                new SelectItemGroup(
                    "smallest",
                    new MinDecimalGroup(
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
                new SelectItemGroup(
                    "smallest_overall",
                    new MinDecimalGroup(
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
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "points",
                    new CoalesceDecimalGroup([
                        new MaxIntegerNullableGroup(
                            new IfIntegerNullableRow(
                                new LiteralIntegerNullable(),
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
                                    new LiteralInteger(25)
                                ),
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
                                    new LiteralUuid(new System.Guid(2, 0, 0, new byte[8]))
                                )
                            )
                        ),
                        new LiteralDecimal(0.5m),
                    ])
                ),
                new SelectItemGroup(
                    "best",
                    new IfDecimalNullableGroup(
                        new MaxIntegerGroup(
                            new IfIntegerRow(
                                new LiteralInteger(1),
                                new LiteralInteger(2),
                                new EqualUuidRow(
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
                                    new LiteralUuid(
                                        new System.Guid(901, 0, 0, new byte[8])
                                    )
                                )
                            )
                        ),
                        new AverageDecimalNullableGroup(
                            new IfDecimalNullableRow(
                                new LiteralDecimalNullable(),
                                new MultiplyDecimalRow([
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
                                    new LiteralDecimal(0.1m),
                                ]),
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
                                    new LiteralString("pending")
                                )
                            )
                        ),
                        new GreaterThanDecimalGroup(
                            new CountGroup(),
                            new LiteralInteger(1)
                        )
                    )
                ),
                new SelectItemGroup(
                    "rounded",
                    new RoundWithDigitsIntegerGroup(
                        new CountGroup(),
                        new LiteralInteger(-1)
                    )
                ),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over <see
    /// cref="SchemaDataSetWithForeignKeys"/> and <see cref="AuditSchemaDataSet"/>, in no
    /// particular order.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new UserIdColumn(),
                    new Column(new String("orders"), new LongColumnType()),
                    new Column(new String("items"), new LongColumnType()),
                    new Column(new String("avg_items"), new DoubleColumnType()),
                    new Column(new String("smallest"), new DoubleColumnType()),
                    new Column(new String("smallest_overall"), new DoubleColumnType()),
                    new Column(new String("points"), new DoubleColumnType()),
                    new Column(new String("best"), new DoubleColumnType()),
                    new Column(new String("rounded"), new LongColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new UserIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000001-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("items"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("avg_items"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("smallest"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(50))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("smallest_overall"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(50))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("points"), new DoubleColumnType()),
                                new InvariantCell(new Double(4))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("best"), new DoubleColumnType()),
                                new InvariantCell(new Double(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("rounded"), new LongColumnType()),
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
                                new UserIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000002-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("items"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("avg_items"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("smallest"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(200))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("smallest_overall"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(50))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("points"), new DoubleColumnType()),
                                new InvariantCell(new Double(0.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("best"), new DoubleColumnType()),
                                new InvariantCell(new Double(20))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("rounded"), new LongColumnType()),
                                new InvariantCell(new Long(0))
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
