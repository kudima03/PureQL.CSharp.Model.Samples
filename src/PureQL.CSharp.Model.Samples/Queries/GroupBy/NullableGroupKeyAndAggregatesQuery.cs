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
using PureQL.CSharp.Model.GroupKeys;
using PureQL.CSharp.Model.Keys;
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.RowExpressions;
using PureQL.CSharp.Model.SelectItems;
using Column = Pure.RelationalSchema.Column.Column;
using DateTime = Pure.Primitives.DateTime.DateTime;
using Double = Pure.Primitives.Number.Double;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.GroupBy;

/// <summary>
/// Selects the nullable buyer, the max of placed_at as last_order_at, the average of
/// placed_at as average_order_at and the min of order_total over orders above the integer
/// 100, or else the integer 0, as min_large_total from schema_with_foreign_keys.orders
/// left joined with schema_with_foreign_keys.users on order_user_id equal to user_id and
/// user_active, grouped by the nullable user_name as buyer, having a max of placed_at
/// after 2024-06-02T12:00:00Z, ordered by the buyer.
/// </summary>
public sealed record NullableGroupKeyAndAggregatesQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new PureQLQuery(
            new MainGroupedQuery(
                new From(
                    new FromEntity(
                        new JoinedString(
                            new DotString(),
                            [
                                new RelationalSchemaWithForeignKeys().Name,
                                new OrdersTable().Name,
                            ]
                        ).TextValue
                    )
                ),
                [
                    new GroupKey(
                        new GroupKeyNullable(
                            new GroupKeyStringNullable(
                                new StringNullableRow(
                                    new FieldAsStringNullable(
                                        new FieldStringNullable(
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new RelationalSchemaWithForeignKeys().Name,
                                                    new UsersTable().Name,
                                                ]
                                            ).TextValue,
                                            new UserNameColumn().Name.TextValue
                                        )
                                    )
                                ),
                                "buyer"
                            )
                        )
                    ),
                ],
                [
                    new SelectItemGroup(
                        new SelectItemGroupNullable(
                            new SelectItemGroupStringNullable(
                                "buyer",
                                new StringNullableGroup(
                                    new KeyAsStringNullable(new KeyStringNullable(0))
                                )
                            )
                        )
                    ),
                    new SelectItemGroup(
                        new SelectItemGroupNonNullable(
                            new SelectItemGroupDatetime(
                                "last_order_at",
                                new DatetimeGroup(
                                    new AggregateDatetimeGroup(
                                        new MaxDatetimeGroup(
                                            new DatetimeRow(
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
                                    )
                                )
                            )
                        )
                    ),
                    new SelectItemGroup(
                        new SelectItemGroupNonNullable(
                            new SelectItemGroupDatetime(
                                "average_order_at",
                                new DatetimeGroup(
                                    new AggregateDatetimeGroup(
                                        new AverageDatetimeGroup(
                                            new DatetimeRow(
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
                                    )
                                )
                            )
                        )
                    ),
                    new SelectItemGroup(
                        new SelectItemGroupNonNullable(
                            new SelectItemGroupDecimal(
                                "min_large_total",
                                new DecimalGroup(
                                    new ConditionalDecimalGroup(
                                        new CoalesceDecimalGroup([
                                            new DecimalNullableGroup(
                                                new AggregateDecimalNullableGroup(
                                                    new MinDecimalNullableGroup(
                                                        new DecimalNullableRow(
                                                            new FieldAsDecimalNullable(
                                                                new FieldDecimal(
                                                                    new JoinedString(
                                                                        new DotString(),
                                                                        [
                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                            new OrdersTable().Name,
                                                                        ]
                                                                    ).TextValue,
                                                                    new OrderTotalColumn()
                                                                        .Name
                                                                        .TextValue
                                                                )
                                                            )
                                                        ),
                                                        new BooleanRow(
                                                            new ComparisonRow(
                                                                new GreaterThanRow(
                                                                    new GreaterThanDecimalRow(
                                                                        new DecimalNullableRow(
                                                                            new FieldAsDecimalNullable(
                                                                                new FieldDecimal(
                                                                                    new JoinedString(
                                                                                        new DotString(),
                                                                                        [
                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                            new OrdersTable().Name,
                                                                                        ]
                                                                                    ).TextValue,
                                                                                    new OrderTotalColumn()
                                                                                        .Name
                                                                                        .TextValue
                                                                                )
                                                                            )
                                                                        ),
                                                                        new DecimalNullableRow(
                                                                            new LiteralAsDecimalNullable(
                                                                                new LiteralInteger(
                                                                                    100
                                                                                )
                                                                            )
                                                                        )
                                                                    )
                                                                )
                                                            )
                                                        )
                                                    )
                                                )
                                            ),
                                            new DecimalNullableGroup(
                                                new LiteralAsDecimalNullable(
                                                    new LiteralInteger(0)
                                                )
                                            ),
                                        ])
                                    )
                                )
                            )
                        )
                    ),
                ],
                subqueries: null,
                [
                    new Join(
                        new JoinEntity(
                            JoinType.Left,
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue,
                            new BooleanRow(
                                new LogicalRow(
                                    new AndRow([
                                        new BooleanRow(
                                            new ComparisonRow(
                                                new EqualRow(
                                                    new EqualUuidRow(
                                                        new UuidNullableRow(
                                                            new FieldAsUuidNullable(
                                                                new FieldUuid(
                                                                    new JoinedString(
                                                                        new DotString(),
                                                                        [
                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                            new OrdersTable().Name,
                                                                        ]
                                                                    ).TextValue,
                                                                    new OrderUserIdColumn()
                                                                        .Name
                                                                        .TextValue
                                                                )
                                                            )
                                                        ),
                                                        new UuidNullableRow(
                                                            new FieldAsUuidNullable(
                                                                new FieldUuid(
                                                                    new JoinedString(
                                                                        new DotString(),
                                                                        [
                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                            new UsersTable().Name,
                                                                        ]
                                                                    ).TextValue,
                                                                    new UserIdColumn()
                                                                        .Name
                                                                        .TextValue
                                                                )
                                                            )
                                                        )
                                                    )
                                                )
                                            )
                                        ),
                                        new BooleanRow(
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
                                        ),
                                    ])
                                )
                            )
                        )
                    ),
                ],
                where: null,
                new BooleanGroup(
                    new ComparisonGroup(
                        new GreaterThanGroup(
                            new GreaterThanDatetimeGroup(
                                new DatetimeNullableGroup(
                                    new AggregateDatetimeNullableGroup(
                                        new MaxDatetimeNullableGroup(
                                            new DatetimeNullableRow(
                                                new FieldAsDatetimeNullable(
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
                                                    )
                                                )
                                            )
                                        )
                                    )
                                ),
                                new DatetimeNullableGroup(
                                    new LiteralAsDatetimeNullable(
                                        new LiteralDatetime(
                                            new DateTimeOffset(
                                                2024,
                                                6,
                                                2,
                                                12,
                                                0,
                                                0,
                                                TimeSpan.Zero
                                            )
                                        )
                                    )
                                )
                            )
                        )
                    )
                ),
                [
                    new OrderItemGroup(
                        new ValueGroup(
                            new StringNullableGroup(
                                new KeyAsStringNullable(new KeyStringNullable(0))
                            )
                        )
                    ),
                ],
                pagination: null,
                distinct: false
            )
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
                    new Column(new String("buyer"), new StringColumnType()),
                    new Column(new String("last_order_at"), new DateTimeColumnType()),
                    new Column(new String("average_order_at"), new DateTimeColumnType()),
                    new Column(new String("min_large_total"), new DoubleColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("buyer"), new StringColumnType()),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_order_at"),
                                    new DateTimeColumnType()
                                ),
                                new InvariantCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(3),
                                            new UShort(6),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(12),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("average_order_at"),
                                    new DateTimeColumnType()
                                ),
                                new InvariantCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(3),
                                            new UShort(6),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(12),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("min_large_total"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(200))
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
                                new Column(new String("buyer"), new StringColumnType()),
                                new InvariantCell(new String("Cara"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_order_at"),
                                    new DateTimeColumnType()
                                ),
                                new InvariantCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(5),
                                            new UShort(6),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(14),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("average_order_at"),
                                    new DateTimeColumnType()
                                ),
                                new InvariantCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(5),
                                            new UShort(6),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(1),
                                            new UShort(30),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("min_large_total"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(300))
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
                                new Column(new String("buyer"), new StringColumnType()),
                                new InvariantCell(new String("Dan"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_order_at"),
                                    new DateTimeColumnType()
                                ),
                                new InvariantCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(6),
                                            new UShort(6),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(15),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("average_order_at"),
                                    new DateTimeColumnType()
                                ),
                                new InvariantCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(6),
                                            new UShort(6),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(15),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("min_large_total"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(100.5))
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
