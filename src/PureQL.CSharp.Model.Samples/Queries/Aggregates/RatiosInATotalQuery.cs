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

namespace PureQL.CSharp.Model.Samples.Queries.Aggregates;

/// <summary>
/// Aggregates schema_with_foreign_keys.orders placed on or after 2024-06-07 into one row,
/// which a total query returns even over no rows: the count of rows as orders, the sum of
/// order_total divided by nullIfEqual of the count and 0 as average_total, the count of
/// shipped orders (order_status is shipped, standing for paid ones) divided the same way
/// as paid_share, that share coalesced with 0 as paid_share_or_zero, and nullIfEqual of
/// the count and 0 as orders_or_null.
/// </summary>
public sealed record RatiosInATotalQuery
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
                new LiteralDate(new DateOnly(2024, 6, 7))
            ),
            aggregate:
            [
                new SelectItemTotal("orders", new CountTotal()),
                new SelectItemTotal(
                    "average_total",
                    new DivideDecimalNullableTotal([
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
                        new NullIfEqualIntegerNullableTotal(
                            new CountTotal(),
                            new LiteralInteger(0)
                        ),
                    ])
                ),
                new SelectItemTotal(
                    "paid_share",
                    new DivideDecimalNullableTotal([
                        new CountTotal(
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
                        ),
                        new NullIfEqualIntegerNullableTotal(
                            new CountTotal(),
                            new LiteralInteger(0)
                        ),
                    ])
                ),
                new SelectItemTotal(
                    "paid_share_or_zero",
                    new CoalesceDecimalTotal([
                        new DivideDecimalNullableTotal([
                            new CountTotal(
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
                            ),
                            new NullIfEqualIntegerNullableTotal(
                                new CountTotal(),
                                new LiteralInteger(0)
                            ),
                        ]),
                        new LiteralDecimal(0m),
                    ])
                ),
                new SelectItemTotal(
                    "orders_or_null",
                    new NullIfEqualIntegerNullableTotal(
                        new CountTotal(),
                        new LiteralInteger(0)
                    )
                ),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and
    /// <see cref="AuditSchemaDataSet"/>, in no particular order.
    /// No order is at or after the date, so the query returns its one row over no
    /// rows: count is 0, the divisions by nullIfEqual(count, 0) are null and the
    /// coalesce falls back to 0.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("orders"), new LongColumnType()),
                    new Column(new String("average_total"), new DoubleColumnType()),
                    new Column(new String("paid_share"), new DoubleColumnType()),
                    new Column(new String("paid_share_or_zero"), new DoubleColumnType()),
                    new Column(new String("orders_or_null"), new LongColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("average_total"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("paid_share"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("paid_share_or_zero"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("orders_or_null"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
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
