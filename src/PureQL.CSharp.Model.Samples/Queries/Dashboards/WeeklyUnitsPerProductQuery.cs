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
using PureQL.CSharp.Model.RowExpressions;
using PureQL.CSharp.Model.SelectItems;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Dashboards;

/// <summary>
/// Selects 2024-05-27 plus week_no times 7 in days as week_start and the sum of item_qty
/// over rows whose product_name is Widget, Gadget, Gizmo and Deluxe as units_widget,
/// units_gadget, units_gizmo and units_deluxe from schema_with_foreign_keys.order_items
/// inner joined with schema_with_foreign_keys.orders on item_order_id equal to order_id
/// and with schema_with_foreign_keys.products on item_product_id equal to product_id,
/// where placed_on is on or after 2024-06-01, grouped by the days from 2024-05-27 to
/// placed_on integer-divided by 7 as week_no, ordered by week_no.
/// </summary>
public sealed record WeeklyUnitsPerProductQuery
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
                                new OrderItemsTable().Name,
                            ]
                        ).TextValue
                    )
                ),
                [
                    new GroupKey(
                        new GroupKeyNonNullable(
                            new GroupKeyInteger(
                                "week_no",
                                new IntegerRow(
                                    new ArithmeticIntegerRow(
                                        new IntegerDivideIntegerRow(
                                            new IntegerRow(
                                                new DateDiffDaysIntegerRow(
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
                                                    ),
                                                    new DateRow(
                                                        new LiteralDate(
                                                            new DateOnly(2024, 5, 27)
                                                        )
                                                    )
                                                )
                                            ),
                                            new IntegerRow(new LiteralInteger(7))
                                        )
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
                                "week_start",
                                new DateGroup(
                                    new DateAddDaysDateGroup(
                                        new DateGroup(
                                            new LiteralDate(new DateOnly(2024, 5, 27))
                                        ),
                                        new IntegerGroup(
                                            new ArithmeticIntegerGroup(
                                                new MultiplyIntegerGroup([
                                                    new IntegerGroup(
                                                        new KeyInteger("week_no")
                                                    ),
                                                    new IntegerGroup(
                                                        new LiteralInteger(7)
                                                    ),
                                                ])
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
                                "units_widget",
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
                                                                new OrderItemsTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new ItemQtyColumn().Name.TextValue
                                                    )
                                                )
                                            ),
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
                                                                                new ProductsTable().Name,
                                                                            ]
                                                                        ).TextValue,
                                                                        new ProductNameColumn()
                                                                            .Name
                                                                            .TextValue
                                                                    )
                                                                )
                                                            ),
                                                            new StringNullableRow(
                                                                new StringRow(
                                                                    new LiteralString(
                                                                        "Widget"
                                                                    )
                                                                )
                                                            )
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
                    new SelectItemGroup(
                        new SelectItemGroupNonNullable(
                            new SelectItemGroupDecimal(
                                "units_gadget",
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
                                                                new OrderItemsTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new ItemQtyColumn().Name.TextValue
                                                    )
                                                )
                                            ),
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
                                                                                new ProductsTable().Name,
                                                                            ]
                                                                        ).TextValue,
                                                                        new ProductNameColumn()
                                                                            .Name
                                                                            .TextValue
                                                                    )
                                                                )
                                                            ),
                                                            new StringNullableRow(
                                                                new StringRow(
                                                                    new LiteralString(
                                                                        "Gadget"
                                                                    )
                                                                )
                                                            )
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
                    new SelectItemGroup(
                        new SelectItemGroupNonNullable(
                            new SelectItemGroupDecimal(
                                "units_gizmo",
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
                                                                new OrderItemsTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new ItemQtyColumn().Name.TextValue
                                                    )
                                                )
                                            ),
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
                                                                                new ProductsTable().Name,
                                                                            ]
                                                                        ).TextValue,
                                                                        new ProductNameColumn()
                                                                            .Name
                                                                            .TextValue
                                                                    )
                                                                )
                                                            ),
                                                            new StringNullableRow(
                                                                new StringRow(
                                                                    new LiteralString(
                                                                        "Gizmo"
                                                                    )
                                                                )
                                                            )
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
                    new SelectItemGroup(
                        new SelectItemGroupNonNullable(
                            new SelectItemGroupDecimal(
                                "units_deluxe",
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
                                                                new OrderItemsTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new ItemQtyColumn().Name.TextValue
                                                    )
                                                )
                                            ),
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
                                                                                new ProductsTable().Name,
                                                                            ]
                                                                        ).TextValue,
                                                                        new ProductNameColumn()
                                                                            .Name
                                                                            .TextValue
                                                                    )
                                                                )
                                                            ),
                                                            new StringNullableRow(
                                                                new StringRow(
                                                                    new LiteralString(
                                                                        "Deluxe"
                                                                    )
                                                                )
                                                            )
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
                ],
                subqueries: null,
                [
                    new Join(
                        new JoinEntity(
                            JoinKind.Inner,
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrdersTable().Name,
                                ]
                            ).TextValue,
                            new BooleanRow(
                                new ComparisonRow(
                                    new EqualRow(
                                        new EqualUuidRow(
                                            new UuidNullableRow(
                                                new UuidRow(
                                                    new FieldUuid(
                                                        new JoinedString(
                                                            new DotString(),
                                                            [
                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                new OrderItemsTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new ItemOrderIdColumn()
                                                            .Name
                                                            .TextValue
                                                    )
                                                )
                                            ),
                                            new UuidNullableRow(
                                                new UuidRow(
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
                                                )
                                            )
                                        )
                                    )
                                )
                            )
                        )
                    ),
                    new Join(
                        new JoinEntity(
                            JoinKind.Inner,
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new ProductsTable().Name,
                                ]
                            ).TextValue,
                            new BooleanRow(
                                new ComparisonRow(
                                    new EqualRow(
                                        new EqualUuidRow(
                                            new UuidNullableRow(
                                                new UuidRow(
                                                    new FieldUuid(
                                                        new JoinedString(
                                                            new DotString(),
                                                            [
                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                new OrderItemsTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new ItemProductIdColumn()
                                                            .Name
                                                            .TextValue
                                                    )
                                                )
                                            ),
                                            new UuidNullableRow(
                                                new UuidRow(
                                                    new FieldUuid(
                                                        new JoinedString(
                                                            new DotString(),
                                                            [
                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                new ProductsTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new ProductIdColumn()
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
                new BooleanRow(
                    new ComparisonRow(
                        new GreaterThanOrEqualRow(
                            new GreaterThanOrEqualDateRow(
                                new DateNullableRow(
                                    new DateRow(
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
                                new DateNullableRow(
                                    new DateRow(new LiteralDate(new DateOnly(2024, 6, 1)))
                                )
                            )
                        )
                    )
                ),
                having: null,
                [
                    new OrderItemGroup(
                        new ExprGroup(
                            new DecimalNullableGroup(
                                new IntegerGroup(new KeyInteger("week_no"))
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
                    new Column(new String("week_start"), new DateColumnType()),
                    new Column(new String("units_widget"), new DoubleColumnType()),
                    new Column(new String("units_gadget"), new DoubleColumnType()),
                    new Column(new String("units_gizmo"), new DoubleColumnType()),
                    new Column(new String("units_deluxe"), new DoubleColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("week_start"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(27),
                                        new UShort(5),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("units_widget"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("units_gadget"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("units_gizmo"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("units_deluxe"),
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
                                new Column(
                                    new String("week_start"),
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
                                    new String("units_widget"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("units_gadget"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("units_gizmo"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("units_deluxe"),
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
            ]
        );
}
