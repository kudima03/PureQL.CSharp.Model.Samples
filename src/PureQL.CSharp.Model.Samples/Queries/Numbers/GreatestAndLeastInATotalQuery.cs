using Pure.Collections.Generic;
using Pure.Primitives.Date;
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
using PureQL.CSharp.Model.TotalExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Numbers;

/// <summary>
/// Totals over schema_with_foreign_keys.orders in one row: the greatest of the count of
/// orders and 1, the least of the nullable max of order_total and 250, the greatest of
/// the nullable min of placed_on and 2024-06-03, that date coalesced with 2024-06-03, and
/// the greatest of the sum of order_total and 0.
/// </summary>
public sealed record GreatestAndLeastInATotalQuery
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
                    "orders_at_least_one",
                    new GreatestIntegerTotal([new CountTotal(), new LiteralInteger(1)])
                ),
                new SelectItemTotal(
                    "largest_capped",
                    new LeastDecimalNullableTotal([
                        new MaxDecimalNullableTotal(
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
                        new LiteralDecimal(250m),
                    ])
                ),
                new SelectItemTotal(
                    "first_day_in_period",
                    new GreatestDateNullableTotal([
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
                        new LiteralDate(new DateOnly(2024, 6, 3)),
                    ])
                ),
                new SelectItemTotal(
                    "first_day_or_start",
                    new CoalesceDateTotal([
                        new GreatestDateNullableTotal([
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
                            new LiteralDate(new DateOnly(2024, 6, 3)),
                        ]),
                        new LiteralDate(new DateOnly(2024, 6, 3)),
                    ])
                ),
                new SelectItemTotal(
                    "revenue_floor",
                    new GreatestDecimalTotal([
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
                        new LiteralInteger(0),
                    ])
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
                    new Column(new String("orders_at_least_one"), new LongColumnType()),
                    new Column(new String("largest_capped"), new DoubleColumnType()),
                    new Column(new String("first_day_in_period"), new DateColumnType()),
                    new Column(new String("first_day_or_start"), new DateColumnType()),
                    new Column(new String("revenue_floor"), new DoubleColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("orders_at_least_one"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(6))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("largest_capped"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(250))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_day_in_period"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(3),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_day_or_start"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(3),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("revenue_floor"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(826.25))
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
