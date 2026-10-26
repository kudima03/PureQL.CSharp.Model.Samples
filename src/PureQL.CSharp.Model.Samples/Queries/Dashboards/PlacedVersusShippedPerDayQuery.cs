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
using PureQL.CSharp.Model.GroupExpressions;
using PureQL.CSharp.Model.GroupKeys;
using PureQL.CSharp.Model.Keys;
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.ProjectionExpressions;
using PureQL.CSharp.Model.RowExpressions;
using PureQL.CSharp.Model.SelectItems;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Dashboards;

/// <summary>
/// Selects placed_on, placed rounded to 2 digits as placed, the nullable shipped or else
/// the decimal 0 rounded to 2 digits as shipped and placed minus that coalesced shipped
/// rounded to 2 digits as not_yet_shipped from the subquery placed_by_day left joined
/// with the subquery shipped_by_day on placed_on, ordered by placed_on. placed_by_day
/// sums order_total and counts the orders of schema_with_foreign_keys.orders per
/// placed_on, and shipped_by_day sums order_total of the schema_with_foreign_keys.orders
/// whose order_status equals shipped per placed_on.
/// </summary>
public sealed record PlacedVersusShippedPerDayQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new PureQLQuery(
            new MainPlainQuery(
                new From(new FromSubquery("placed_by_day")),
                [
                    new SelectItemProjection(
                        new SelectItemProjectionNonNullable(
                            new SelectItemProjectionDate(
                                "placed_on",
                                new DateProjection(
                                    new FieldDate("placed_by_day", "placed_on")
                                )
                            )
                        )
                    ),
                    new SelectItemProjection(
                        new SelectItemProjectionNonNullable(
                            new SelectItemProjectionDecimal(
                                "placed",
                                new ExactDecimalProjection(
                                    new RoundDecimalProjection(
                                        new DecimalProjection(
                                            new ExactDecimalProjection(
                                                new FieldDecimal(
                                                    "placed_by_day",
                                                    "placed"
                                                )
                                            )
                                        ),
                                        new IntegerProjection(new LiteralInteger(2))
                                    )
                                )
                            )
                        )
                    ),
                    new SelectItemProjection(
                        new SelectItemProjectionNonNullable(
                            new SelectItemProjectionDecimal(
                                "shipped",
                                new ExactDecimalProjection(
                                    new RoundDecimalProjection(
                                        new DecimalProjection(
                                            new ExactDecimalProjection(
                                                new ConditionalDecimalProjection(
                                                    new CoalesceDecimalProjection([
                                                        new DecimalNullableProjection(
                                                            new ExactDecimalNullableProjection(
                                                                new FieldDecimalNullable(
                                                                    "shipped_by_day",
                                                                    "shipped"
                                                                )
                                                            )
                                                        ),
                                                        new DecimalNullableProjection(
                                                            new ExactDecimalProjection(
                                                                new LiteralDecimal(0m)
                                                            )
                                                        ),
                                                    ])
                                                )
                                            )
                                        ),
                                        new IntegerProjection(new LiteralInteger(2))
                                    )
                                )
                            )
                        )
                    ),
                    new SelectItemProjection(
                        new SelectItemProjectionNonNullable(
                            new SelectItemProjectionDecimal(
                                "not_yet_shipped",
                                new ExactDecimalProjection(
                                    new RoundDecimalProjection(
                                        new DecimalProjection(
                                            new ExactDecimalProjection(
                                                new ArithmeticDecimalProjection(
                                                    new SubtractDecimalProjection([
                                                        new DecimalProjection(
                                                            new ExactDecimalProjection(
                                                                new FieldDecimal(
                                                                    "placed_by_day",
                                                                    "placed"
                                                                )
                                                            )
                                                        ),
                                                        new DecimalProjection(
                                                            new ExactDecimalProjection(
                                                                new ConditionalDecimalProjection(
                                                                    new CoalesceDecimalProjection([
                                                                        new DecimalNullableProjection(
                                                                            new ExactDecimalNullableProjection(
                                                                                new FieldDecimalNullable(
                                                                                    "shipped_by_day",
                                                                                    "shipped"
                                                                                )
                                                                            )
                                                                        ),
                                                                        new DecimalNullableProjection(
                                                                            new ExactDecimalProjection(
                                                                                new LiteralDecimal(
                                                                                    0m
                                                                                )
                                                                            )
                                                                        ),
                                                                    ])
                                                                )
                                                            )
                                                        ),
                                                    ])
                                                )
                                            )
                                        ),
                                        new IntegerProjection(new LiteralInteger(2))
                                    )
                                )
                            )
                        )
                    ),
                ],
                [
                    new Subquery(
                        "placed_by_day",
                        new Query(
                            new GroupedQuery(
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
                                        new GroupKeyNonNullable(
                                            new GroupKeyDate(
                                                "placed_on",
                                                new DateRow(
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
                                                    )
                                                )
                                            )
                                        )
                                    ),
                                ],
                                [
                                    new SelectItemGroup(
                                        new SelectItemGroupNonNullable(
                                            new SelectItemGroupDate(
                                                "placed_on",
                                                new DateGroup(new KeyDate("placed_on"))
                                            )
                                        )
                                    ),
                                    new SelectItemGroup(
                                        new SelectItemGroupNonNullable(
                                            new SelectItemGroupDecimal(
                                                "placed",
                                                new ExactDecimalGroup(
                                                    new AggregateDecimalGroup(
                                                        new SumDecimalGroup(
                                                            new DecimalNullableRow(
                                                                new ExactDecimalRow(
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
                                                            )
                                                        )
                                                    )
                                                )
                                            )
                                        )
                                    ),
                                    new SelectItemGroup(
                                        new SelectItemGroupNonNullable(
                                            new SelectItemGroupInteger(
                                                "orders",
                                                new IntegerGroup(
                                                    new AggregateIntegerGroup(
                                                        new CountGroup()
                                                    )
                                                )
                                            )
                                        )
                                    ),
                                ],
                                joins: null,
                                where: null,
                                having: null,
                                orderBy: null,
                                pagination: null,
                                distinctBy: null
                            )
                        )
                    ),
                    new Subquery(
                        "shipped_by_day",
                        new Query(
                            new GroupedQuery(
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
                                        new GroupKeyNonNullable(
                                            new GroupKeyDate(
                                                "placed_on",
                                                new DateRow(
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
                                                    )
                                                )
                                            )
                                        )
                                    ),
                                ],
                                [
                                    new SelectItemGroup(
                                        new SelectItemGroupNonNullable(
                                            new SelectItemGroupDate(
                                                "placed_on",
                                                new DateGroup(new KeyDate("placed_on"))
                                            )
                                        )
                                    ),
                                    new SelectItemGroup(
                                        new SelectItemGroupNonNullable(
                                            new SelectItemGroupDecimal(
                                                "shipped",
                                                new ExactDecimalGroup(
                                                    new AggregateDecimalGroup(
                                                        new SumDecimalGroup(
                                                            new DecimalNullableRow(
                                                                new ExactDecimalRow(
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
                                                            )
                                                        )
                                                    )
                                                )
                                            )
                                        )
                                    ),
                                ],
                                joins: null,
                                new BooleanRow(
                                    new ComparisonRow(
                                        new EqualRow(
                                            new EqualStringRow(
                                                new StringNullableRow(
                                                    new StringRow(
                                                        new FieldString(
                                                            new JoinedString(
                                                                new DotString(),
                                                                [
                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                    new OrdersTable().Name,
                                                                ]
                                                            ).TextValue,
                                                            new OrderStatusColumn()
                                                                .Name
                                                                .TextValue
                                                        )
                                                    )
                                                ),
                                                new StringNullableRow(
                                                    new StringRow(
                                                        new LiteralString("shipped")
                                                    )
                                                )
                                            )
                                        )
                                    )
                                ),
                                having: null,
                                orderBy: null,
                                pagination: null,
                                distinctBy: null
                            )
                        )
                    ),
                ],
                [
                    new Join(
                        new JoinSubquery(
                            JoinKind.Left,
                            "shipped_by_day",
                            new BooleanRow(
                                new ComparisonRow(
                                    new EqualRow(
                                        new EqualDateRow(
                                            new DateNullableRow(
                                                new DateRow(
                                                    new FieldDate(
                                                        "placed_by_day",
                                                        "placed_on"
                                                    )
                                                )
                                            ),
                                            new DateNullableRow(
                                                new DateRow(
                                                    new FieldDate(
                                                        "shipped_by_day",
                                                        "placed_on"
                                                    )
                                                )
                                            )
                                        )
                                    )
                                )
                            )
                        )
                    ),
                ],
                where: null,
                [
                    new OrderItemProjection(
                        new ExprProjection(
                            new DateNullableProjection(
                                new DateProjection(
                                    new FieldDate("placed_by_day", "placed_on")
                                )
                            )
                        )
                    ),
                ],
                pagination: null,
                distinctBy: null
            )
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
                    new PlacedOnColumn(),
                    new Column(new String("placed"), new DoubleColumnType()),
                    new Column(new String("shipped"), new DoubleColumnType()),
                    new Column(new String("not_yet_shipped"), new DoubleColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new PlacedOnColumn(),
                                new InvariantCell(
                                    new Date(
                                        new UShort(1),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("placed"), new DoubleColumnType()),
                                new InvariantCell(new Double(100.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("shipped"), new DoubleColumnType()),
                                new InvariantCell(new Double(100.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("not_yet_shipped"),
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
                                new PlacedOnColumn(),
                                new InvariantCell(
                                    new Date(
                                        new UShort(2),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("placed"), new DoubleColumnType()),
                                new InvariantCell(new Double(50))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("shipped"), new DoubleColumnType()),
                                new InvariantCell(new Double(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("not_yet_shipped"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(50))
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
                                new PlacedOnColumn(),
                                new InvariantCell(
                                    new Date(
                                        new UShort(3),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("placed"), new DoubleColumnType()),
                                new InvariantCell(new Double(200))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("shipped"), new DoubleColumnType()),
                                new InvariantCell(new Double(200))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("not_yet_shipped"),
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
                                new PlacedOnColumn(),
                                new InvariantCell(
                                    new Date(
                                        new UShort(4),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("placed"), new DoubleColumnType()),
                                new InvariantCell(new Double(75.25))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("shipped"), new DoubleColumnType()),
                                new InvariantCell(new Double(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("not_yet_shipped"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(75.25))
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
                                new PlacedOnColumn(),
                                new InvariantCell(
                                    new Date(
                                        new UShort(5),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("placed"), new DoubleColumnType()),
                                new InvariantCell(new Double(300))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("shipped"), new DoubleColumnType()),
                                new InvariantCell(new Double(300))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("not_yet_shipped"),
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
                                new PlacedOnColumn(),
                                new InvariantCell(
                                    new Date(
                                        new UShort(6),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("placed"), new DoubleColumnType()),
                                new InvariantCell(new Double(100.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("shipped"), new DoubleColumnType()),
                                new InvariantCell(new Double(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("not_yet_shipped"),
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
