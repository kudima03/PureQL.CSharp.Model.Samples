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
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Dashboards;

/// <summary>
/// Selects weekday, the count of rows as orders and the decimal 0 if that count is the
/// integer 0, else the decimal 100 times the count of rows whose order_tenant_id is the
/// uuid 00000385-0000-0000-0000-000000000000 divided by the count, rounded to 1 digit, as
/// tenant_385_share_pct from schema_with_foreign_keys.orders, where order_status is not
/// equal to cancelled, grouped by 1 Monday through 7 Sunday as weekday depending on
/// whether the days from 2024-05-27 to placed_on modulo 7 is 0 through 5, ordered by
/// weekday.
/// </summary>
public sealed record OrdersPerWeekdayQuery
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
            where: new NotEqualStringRow(
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
                new LiteralString("cancelled")
            ),
            groupBy:
            [
                new GroupKey(
                    "weekday",
                    new IfStringRow(
                        new LiteralString("1 Monday"),
                        new IfStringRow(
                            new LiteralString("2 Tuesday"),
                            new IfStringRow(
                                new LiteralString("3 Wednesday"),
                                new IfStringRow(
                                    new LiteralString("4 Thursday"),
                                    new IfStringRow(
                                        new LiteralString("5 Friday"),
                                        new IfStringRow(
                                            new LiteralString("6 Saturday"),
                                            new LiteralString("7 Sunday"),
                                            new EqualDecimalRow(
                                                new ModuloIntegerRow(
                                                    new DateDiffDaysIntegerRow(
                                                        new FieldDate(
                                                            new JoinedString(
                                                                new DotString(),
                                                                [
                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                    new OrdersTable().Name,
                                                                ]
                                                            ).TextValue,
                                                            new PlacedOnColumn()
                                                                .Name
                                                                .TextValue
                                                        ),
                                                        new LiteralDate(
                                                            new DateOnly(2024, 5, 27)
                                                        )
                                                    ),
                                                    new LiteralInteger(7)
                                                ),
                                                new LiteralInteger(5)
                                            )
                                        ),
                                        new EqualDecimalRow(
                                            new ModuloIntegerRow(
                                                new DateDiffDaysIntegerRow(
                                                    new FieldDate(
                                                        new JoinedString(
                                                            new DotString(),
                                                            [
                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                new OrdersTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new PlacedOnColumn()
                                                            .Name
                                                            .TextValue
                                                    ),
                                                    new LiteralDate(
                                                        new DateOnly(2024, 5, 27)
                                                    )
                                                ),
                                                new LiteralInteger(7)
                                            ),
                                            new LiteralInteger(4)
                                        )
                                    ),
                                    new EqualDecimalRow(
                                        new ModuloIntegerRow(
                                            new DateDiffDaysIntegerRow(
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
                                                new LiteralDate(new DateOnly(2024, 5, 27))
                                            ),
                                            new LiteralInteger(7)
                                        ),
                                        new LiteralInteger(3)
                                    )
                                ),
                                new EqualDecimalRow(
                                    new ModuloIntegerRow(
                                        new DateDiffDaysIntegerRow(
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
                                            new LiteralDate(new DateOnly(2024, 5, 27))
                                        ),
                                        new LiteralInteger(7)
                                    ),
                                    new LiteralInteger(2)
                                )
                            ),
                            new EqualDecimalRow(
                                new ModuloIntegerRow(
                                    new DateDiffDaysIntegerRow(
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
                                        new LiteralDate(new DateOnly(2024, 5, 27))
                                    ),
                                    new LiteralInteger(7)
                                ),
                                new LiteralInteger(1)
                            )
                        ),
                        new EqualDecimalRow(
                            new ModuloIntegerRow(
                                new DateDiffDaysIntegerRow(
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
                                    new LiteralDate(new DateOnly(2024, 5, 27))
                                ),
                                new LiteralInteger(7)
                            ),
                            new LiteralInteger(0)
                        )
                    )
                ),
            ],
            select:
            [
                new SelectItemGroup("weekday", new KeyString("weekday")),
                new SelectItemGroup("orders", new CountGroup()),
                new SelectItemGroup(
                    "tenant_385_share_pct",
                    new IfDecimalGroup(
                        new LiteralDecimal(0m),
                        new RoundDecimalGroup(
                            new MultiplyDecimalGroup([
                                new LiteralDecimal(100m),
                                new DivideDecimalGroup([
                                    new CountGroup(
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
                                                new Guid(901, 0, 0, new byte[8])
                                            )
                                        )
                                    ),
                                    new CountGroup(),
                                ]),
                            ]),
                            new LiteralInteger(1)
                        ),
                        new EqualDecimalGroup(new CountGroup(), new LiteralInteger(0))
                    )
                ),
            ],
            orderBy: [new OrderItemGroup(new KeyString("weekday"))]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/>, in this order.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("weekday"), new StringColumnType()),
                    new Column(new String("orders"), new LongColumnType()),
                    new Column(
                        new String("tenant_385_share_pct"),
                        new DoubleColumnType()
                    ),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("weekday"), new StringColumnType()),
                                new InvariantCell(new String("1 Monday"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("tenant_385_share_pct"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(100))
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
                                new Column(new String("weekday"), new StringColumnType()),
                                new InvariantCell(new String("3 Wednesday"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("tenant_385_share_pct"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0))
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
                                new Column(new String("weekday"), new StringColumnType()),
                                new InvariantCell(new String("4 Thursday"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("tenant_385_share_pct"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0))
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
                                new Column(new String("weekday"), new StringColumnType()),
                                new InvariantCell(new String("6 Saturday"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("tenant_385_share_pct"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(100))
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
                                new Column(new String("weekday"), new StringColumnType()),
                                new InvariantCell(new String("7 Sunday"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("tenant_385_share_pct"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(100))
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
