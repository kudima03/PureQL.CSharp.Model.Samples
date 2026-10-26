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
/// Selects user_name, order_status, the count of rows as item_lines, the count of rows
/// whose item_qty equals 1 and whose product_in_stock is not true as
/// single_unit_out_of_stock, the count of rows whose order_total minus item_qty times
/// product_price is less than 100 as leaving_under_100, the sum of item_qty times
/// product_price rounded to 0 digits as item_value, the count of rows whose
/// product_in_stock is true as in_stock_lines and the count of rows whose user_active is
/// true as active_user_lines from schema_with_foreign_keys.order_items inner joined with
/// schema_with_foreign_keys.orders on item_order_id equal to order_id, with
/// schema_with_foreign_keys.products on item_product_id equal to product_id and with
/// schema_with_foreign_keys.users on order_user_id equal to user_id, grouped by
/// user_name and order_status, ordered by the single_unit_out_of_stock count descending
/// and then by user_name.
/// </summary>
public sealed record LineFlagsPerUserAndStatusQuery
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
                            new GroupKeyString(
                                "user_name",
                                new StringRow(
                                    new FieldString(
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
                            )
                        )
                    ),
                    new GroupKey(
                        new GroupKeyNonNullable(
                            new GroupKeyString(
                                "order_status",
                                new StringRow(
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
                                )
                            )
                        )
                    ),
                ],
                [
                    new SelectItemGroup(
                        new SelectItemGroupNonNullable(
                            new SelectItemGroupString(
                                "user_name",
                                new StringGroup(new KeyString("user_name"))
                            )
                        )
                    ),
                    new SelectItemGroup(
                        new SelectItemGroupNonNullable(
                            new SelectItemGroupString(
                                "order_status",
                                new StringGroup(new KeyString("order_status"))
                            )
                        )
                    ),
                    new SelectItemGroup(
                        new SelectItemGroupNonNullable(
                            new SelectItemGroupInteger(
                                "item_lines",
                                new IntegerGroup(
                                    new AggregateIntegerGroup(new CountGroup())
                                )
                            )
                        )
                    ),
                    new SelectItemGroup(
                        new SelectItemGroupNonNullable(
                            new SelectItemGroupInteger(
                                "single_unit_out_of_stock",
                                new IntegerGroup(
                                    new AggregateIntegerGroup(
                                        new CountGroup(
                                            new BooleanRow(
                                                new LogicalRow(
                                                    new AndRow([
                                                        new BooleanRow(
                                                            new ComparisonRow(
                                                                new EqualRow(
                                                                    new EqualDecimalRow(
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
                                                                                    new ItemQtyColumn()
                                                                                        .Name
                                                                                        .TextValue
                                                                                )
                                                                            )
                                                                        ),
                                                                        new DecimalNullableRow(
                                                                            new ExactDecimalRow(
                                                                                new LiteralDecimal(
                                                                                    1m
                                                                                )
                                                                            )
                                                                        )
                                                                    )
                                                                )
                                                            )
                                                        ),
                                                        new BooleanRow(
                                                            new LogicalRow(
                                                                new NotRow(
                                                                    new BooleanRow(
                                                                        new FieldBoolean(
                                                                            new JoinedString(
                                                                                new DotString(),
                                                                                [
                                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                                    new ProductsTable().Name,
                                                                                ]
                                                                            ).TextValue,
                                                                            new ProductInStockColumn()
                                                                                .Name
                                                                                .TextValue
                                                                        )
                                                                    )
                                                                )
                                                            )
                                                        ),
                                                    ])
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
                                "leaving_under_100",
                                new IntegerGroup(
                                    new AggregateIntegerGroup(
                                        new CountGroup(
                                            new BooleanRow(
                                                new ComparisonRow(
                                                    new LessThanRow(
                                                        new LessThanDecimalRow(
                                                            new DecimalNullableRow(
                                                                new ExactDecimalRow(
                                                                    new ArithmeticDecimalRow(
                                                                        new SubtractDecimalRow([
                                                                            new DecimalRow(
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
                                                                            ),
                                                                            new DecimalRow(
                                                                                new ExactDecimalRow(
                                                                                    new ArithmeticDecimalRow(
                                                                                        new MultiplyDecimalRow([
                                                                                            new DecimalRow(
                                                                                                new ExactDecimalRow(
                                                                                                    new FieldDecimal(
                                                                                                        new JoinedString(
                                                                                                            new DotString(),
                                                                                                            [
                                                                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                                                                new OrderItemsTable().Name,
                                                                                                            ]
                                                                                                        ).TextValue,
                                                                                                        new ItemQtyColumn()
                                                                                                            .Name
                                                                                                            .TextValue
                                                                                                    )
                                                                                                )
                                                                                            ),
                                                                                            new DecimalRow(
                                                                                                new ExactDecimalRow(
                                                                                                    new FieldDecimal(
                                                                                                        new JoinedString(
                                                                                                            new DotString(),
                                                                                                            [
                                                                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                                                                new ProductsTable().Name,
                                                                                                            ]
                                                                                                        ).TextValue,
                                                                                                        new ProductPriceColumn()
                                                                                                            .Name
                                                                                                            .TextValue
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
                                                            new DecimalNullableRow(
                                                                new ExactDecimalRow(
                                                                    new LiteralDecimal(
                                                                        100m
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
                                "item_value",
                                new ExactDecimalGroup(
                                    new RoundDecimalGroup(
                                        new DecimalGroup(
                                            new ExactDecimalGroup(
                                                new AggregateDecimalGroup(
                                                    new SumDecimalGroup(
                                                        new DecimalNullableRow(
                                                            new ExactDecimalRow(
                                                                new ArithmeticDecimalRow(
                                                                    new MultiplyDecimalRow([
                                                                        new DecimalRow(
                                                                            new ExactDecimalRow(
                                                                                new FieldDecimal(
                                                                                    new JoinedString(
                                                                                        new DotString(),
                                                                                        [
                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                            new OrderItemsTable().Name,
                                                                                        ]
                                                                                    ).TextValue,
                                                                                    new ItemQtyColumn()
                                                                                        .Name
                                                                                        .TextValue
                                                                                )
                                                                            )
                                                                        ),
                                                                        new DecimalRow(
                                                                            new ExactDecimalRow(
                                                                                new FieldDecimal(
                                                                                    new JoinedString(
                                                                                        new DotString(),
                                                                                        [
                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                            new ProductsTable().Name,
                                                                                        ]
                                                                                    ).TextValue,
                                                                                    new ProductPriceColumn()
                                                                                        .Name
                                                                                        .TextValue
                                                                                )
                                                                            )
                                                                        ),
                                                                    ])
                                                                )
                                                            )
                                                        )
                                                    )
                                                )
                                            )
                                        ),
                                        new IntegerGroup(new LiteralInteger(0))
                                    )
                                )
                            )
                        )
                    ),
                    new SelectItemGroup(
                        new SelectItemGroupNonNullable(
                            new SelectItemGroupInteger(
                                "in_stock_lines",
                                new IntegerGroup(
                                    new AggregateIntegerGroup(
                                        new CountGroup(
                                            new BooleanRow(
                                                new FieldBoolean(
                                                    new JoinedString(
                                                        new DotString(),
                                                        [
                                                            new RelationalSchemaWithForeignKeys().Name,
                                                            new ProductsTable().Name,
                                                        ]
                                                    ).TextValue,
                                                    new ProductInStockColumn()
                                                        .Name
                                                        .TextValue
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
                                "active_user_lines",
                                new IntegerGroup(
                                    new AggregateIntegerGroup(
                                        new CountGroup(
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
                    new Join(
                        new JoinEntity(
                            JoinKind.Inner,
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
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
                                                new UuidRow(
                                                    new FieldUuid(
                                                        new JoinedString(
                                                            new DotString(),
                                                            [
                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                new UsersTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new UserIdColumn().Name.TextValue
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
                having: null,
                [
                    new OrderItemGroup(
                        new ExprGroup(
                            new DecimalNullableGroup(
                                new IntegerGroup(
                                    new AggregateIntegerGroup(
                                        new CountGroup(
                                            new BooleanRow(
                                                new LogicalRow(
                                                    new AndRow([
                                                        new BooleanRow(
                                                            new ComparisonRow(
                                                                new EqualRow(
                                                                    new EqualDecimalRow(
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
                                                                                    new ItemQtyColumn()
                                                                                        .Name
                                                                                        .TextValue
                                                                                )
                                                                            )
                                                                        ),
                                                                        new DecimalNullableRow(
                                                                            new ExactDecimalRow(
                                                                                new LiteralDecimal(
                                                                                    1m
                                                                                )
                                                                            )
                                                                        )
                                                                    )
                                                                )
                                                            )
                                                        ),
                                                        new BooleanRow(
                                                            new LogicalRow(
                                                                new NotRow(
                                                                    new BooleanRow(
                                                                        new FieldBoolean(
                                                                            new JoinedString(
                                                                                new DotString(),
                                                                                [
                                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                                    new ProductsTable().Name,
                                                                                ]
                                                                            ).TextValue,
                                                                            new ProductInStockColumn()
                                                                                .Name
                                                                                .TextValue
                                                                        )
                                                                    )
                                                                )
                                                            )
                                                        ),
                                                    ])
                                                )
                                            )
                                        )
                                    )
                                )
                            )
                        ),
                        SortDirection.Desc
                    ),
                    new OrderItemGroup(
                        new ExprGroup(
                            new StringNullableGroup(
                                new StringGroup(new KeyString("user_name"))
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
                    new UserNameColumn(),
                    new OrderStatusColumn(),
                    new Column(new String("item_lines"), new LongColumnType()),
                    new Column(
                        new String("single_unit_out_of_stock"),
                        new LongColumnType()
                    ),
                    new Column(new String("leaving_under_100"), new LongColumnType()),
                    new Column(new String("item_value"), new DoubleColumnType()),
                    new Column(new String("in_stock_lines"), new LongColumnType()),
                    new Column(new String("active_user_lines"), new LongColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new UserNameColumn(),
                                new InvariantCell(new String("Ann"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new OrderStatusColumn(),
                                new InvariantCell(new String("shipped"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("item_lines"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("single_unit_out_of_stock"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("leaving_under_100"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("item_value"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(40))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("in_stock_lines"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("active_user_lines"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
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
                                new UserNameColumn(),
                                new InvariantCell(new String("Bob"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new OrderStatusColumn(),
                                new InvariantCell(new String("shipped"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("item_lines"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("single_unit_out_of_stock"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("leaving_under_100"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("item_value"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(23))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("in_stock_lines"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("active_user_lines"),
                                    new LongColumnType()
                                ),
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
                                new UserNameColumn(),
                                new InvariantCell(new String("Cara"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new OrderStatusColumn(),
                                new InvariantCell(new String("shipped"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("item_lines"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("single_unit_out_of_stock"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("leaving_under_100"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("item_value"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(30))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("in_stock_lines"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("active_user_lines"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
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
