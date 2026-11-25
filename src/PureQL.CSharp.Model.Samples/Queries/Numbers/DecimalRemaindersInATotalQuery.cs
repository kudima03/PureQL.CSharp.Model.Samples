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
using PureQL.CSharp.Model.RowExpressions;
using PureQL.CSharp.Model.TotalExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Numbers;

/// <summary>
/// Selects, from schema_with_foreign_keys.orders where order_total modulo 0.1 equals 0,
/// the total of: the sum of order_total integer-divided by 50 as fifties and modulo 50 as
/// left_over, the average of order_total modulo 1 as average_cents, the average of the
/// seconds from 2024-05-31T12:30:00Z to placed_at integer-divided by 3600 as
/// average_hours, the count of rows modulo 10 as orders_per_ten and modulo 1.5 as
/// orders_per_one_and_a_half.
/// </summary>
public sealed record DecimalRemaindersInATotalQuery
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
            where: new EqualDecimalRow(
                new ModuloDecimalRow(
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
                    new LiteralDecimal(0.1m)
                ),
                new LiteralInteger(0)
            ),
            aggregate:
            [
                new SelectItemTotal(
                    "fifties",
                    new IntegerDivideIntegerTotal(
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
                        ),
                        new LiteralInteger(50)
                    )
                ),
                new SelectItemTotal(
                    "left_over",
                    new ModuloDecimalTotal(
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
                        ),
                        new LiteralInteger(50)
                    )
                ),
                new SelectItemTotal(
                    "average_cents",
                    new AverageDecimalNullableTotal(
                        new ModuloDecimalRow(
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
                            new LiteralInteger(1)
                        )
                    )
                ),
                new SelectItemTotal(
                    "average_hours",
                    new IntegerDivideIntegerNullableTotal(
                        new AverageDecimalNullableTotal(
                            new DatetimeDiffSecondsDecimalRow(
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
                                new LiteralDatetime(
                                    new DateTimeOffset(
                                        2024,
                                        5,
                                        31,
                                        12,
                                        30,
                                        0,
                                        TimeSpan.Zero
                                    )
                                )
                            )
                        ),
                        new LiteralInteger(3600)
                    )
                ),
                new SelectItemTotal(
                    "orders_per_ten",
                    new ModuloIntegerTotal(new CountTotal(), new LiteralInteger(10))
                ),
                new SelectItemTotal(
                    "orders_per_one_and_a_half",
                    new ModuloDecimalTotal(new CountTotal(), new LiteralDecimal(1.5m))
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
                    new Column(new String("fifties"), new LongColumnType()),
                    new Column(new String("left_over"), new DoubleColumnType()),
                    new Column(new String("average_cents"), new DoubleColumnType()),
                    new Column(new String("average_hours"), new LongColumnType()),
                    new Column(new String("orders_per_ten"), new LongColumnType()),
                    new Column(
                        new String("orders_per_one_and_a_half"),
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
                                new Column(new String("fifties"), new LongColumnType()),
                                new InvariantCell(new Long(15))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("left_over"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("average_cents"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("average_hours"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(81))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("orders_per_ten"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("orders_per_one_and_a_half"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.5))
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
