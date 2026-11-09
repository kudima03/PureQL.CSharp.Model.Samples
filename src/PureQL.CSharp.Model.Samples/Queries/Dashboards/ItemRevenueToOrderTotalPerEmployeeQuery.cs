using Pure.Collections.Generic;
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
using PureQL.CSharp.Model.Keys;
using PureQL.CSharp.Model.Lists;
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.ProjectionExpressions;
using PureQL.CSharp.Model.RowExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Dashboards;

/// <summary>
/// Selects employee_name, employee_shift_start, the subquery ordered rounded to 0 digits
/// as ordered, the subquery item_revenue rounded to 0 digits as item_revenue and
/// item_revenue divided by ordered rounded to 2 digits as item_revenue_per_ordered from
/// schema_with_foreign_keys.employees inner joined with the subquery item_revenue_by_user
/// on employee_user_id and with the subquery ordered_by_user on employee_user_id, where
/// employee_name is in Hank, Iris and Jack, ordered by the unrounded item_revenue
/// descending. item_revenue_by_user sums item_qty times product_price of
/// schema_with_foreign_keys.order_items inner joined with schema_with_foreign_keys.orders
/// on item_order_id equal to order_id and with schema_with_foreign_keys.products on
/// item_product_id equal to product_id, where order_status is not in cancelled and
/// refunded, per order_user_id, and ordered_by_user sums order_total of
/// schema_with_foreign_keys.orders per order_user_id.
/// </summary>
public sealed record ItemRevenueToOrderTotalPerEmployeeQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithoutPagination(
            subqueries:
            [
                new Subquery(
                    "item_revenue_by_user",
                    new GroupedQueryWithoutPaginationWithoutOrderBy(
                        from: new FromEntity(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrderItemsTable().Name,
                                ]
                            ).TextValue
                        ),
                        joins:
                        [
                            new JoinEntity(
                                JoinKind.Inner,
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new OrdersTable().Name,
                                    ]
                                ).TextValue,
                                new EqualUuidRow(
                                    new FieldUuid(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new OrderItemsTable().Name,
                                            ]
                                        ).TextValue,
                                        new ItemOrderIdColumn().Name.TextValue
                                    ),
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
                            ),
                            new JoinEntity(
                                JoinKind.Inner,
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new ProductsTable().Name,
                                    ]
                                ).TextValue,
                                new EqualUuidRow(
                                    new FieldUuid(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new OrderItemsTable().Name,
                                            ]
                                        ).TextValue,
                                        new ItemProductIdColumn().Name.TextValue
                                    ),
                                    new FieldUuid(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new ProductsTable().Name,
                                            ]
                                        ).TextValue,
                                        new ProductIdColumn().Name.TextValue
                                    )
                                )
                            ),
                        ],
                        where: new NotRow(
                            new InStringRow(
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
                                new ListLiteralString(["cancelled", "refunded"])
                            )
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
                            new SelectItemGroup(
                                "item_revenue",
                                new SumDecimalGroup(
                                    new MultiplyDecimalRow([
                                        new FieldDecimal(
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new RelationalSchemaWithForeignKeys().Name,
                                                    new OrderItemsTable().Name,
                                                ]
                                            ).TextValue,
                                            new ItemQtyColumn().Name.TextValue
                                        ),
                                        new FieldDecimal(
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new RelationalSchemaWithForeignKeys().Name,
                                                    new ProductsTable().Name,
                                                ]
                                            ).TextValue,
                                            new ProductPriceColumn().Name.TextValue
                                        ),
                                    ])
                                )
                            ),
                        ]
                    )
                ),
                new Subquery(
                    "ordered_by_user",
                    new GroupedQueryWithoutPaginationWithoutOrderBy(
                        from: new FromEntity(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrdersTable().Name,
                                ]
                            ).TextValue
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
                            new SelectItemGroup(
                                "ordered",
                                new SumDecimalGroup(
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
                        ]
                    )
                ),
            ],
            from: new FromEntity(
                new JoinedString(
                    new DotString(),
                    [
                        new RelationalSchemaWithForeignKeys().Name,
                        new EmployeesTable().Name,
                    ]
                ).TextValue
            ),
            joins:
            [
                new JoinSubquery(
                    JoinKind.Inner,
                    "item_revenue_by_user",
                    new EqualUuidRow(
                        new FieldUuid(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new EmployeesTable().Name,
                                ]
                            ).TextValue,
                            new EmployeeUserIdColumn().Name.TextValue
                        ),
                        new FieldUuid("item_revenue_by_user", "user_id")
                    )
                ),
                new JoinSubquery(
                    JoinKind.Inner,
                    "ordered_by_user",
                    new EqualUuidRow(
                        new FieldUuid(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new EmployeesTable().Name,
                                ]
                            ).TextValue,
                            new EmployeeUserIdColumn().Name.TextValue
                        ),
                        new FieldUuid("ordered_by_user", "user_id")
                    )
                ),
            ],
            where: new InStringRow(
                new FieldString(
                    new JoinedString(
                        new DotString(),
                        [
                            new RelationalSchemaWithForeignKeys().Name,
                            new EmployeesTable().Name,
                        ]
                    ).TextValue,
                    new EmployeeNameColumn().Name.TextValue
                ),
                new ListLiteralString(["Hank", "Iris", "Jack"])
            ),
            select:
            [
                new SelectItemProjection(
                    "employee_name",
                    new FieldString(
                        new JoinedString(
                            new DotString(),
                            [
                                new RelationalSchemaWithForeignKeys().Name,
                                new EmployeesTable().Name,
                            ]
                        ).TextValue,
                        new EmployeeNameColumn().Name.TextValue
                    )
                ),
                new SelectItemProjection(
                    "employee_shift_start",
                    new FieldTime(
                        new JoinedString(
                            new DotString(),
                            [
                                new RelationalSchemaWithForeignKeys().Name,
                                new EmployeesTable().Name,
                            ]
                        ).TextValue,
                        new EmployeeShiftStartColumn().Name.TextValue
                    )
                ),
                new SelectItemProjection(
                    "ordered",
                    new RoundDecimalProjection(
                        new FieldDecimal("ordered_by_user", "ordered"),
                        new LiteralInteger(0)
                    )
                ),
                new SelectItemProjection(
                    "item_revenue",
                    new RoundDecimalProjection(
                        new FieldDecimal("item_revenue_by_user", "item_revenue"),
                        new LiteralInteger(0)
                    )
                ),
                new SelectItemProjection(
                    "item_revenue_per_ordered",
                    new RoundDecimalProjection(
                        new DivideDecimalProjection([
                            new FieldDecimal("item_revenue_by_user", "item_revenue"),
                            new FieldDecimal("ordered_by_user", "ordered"),
                        ]),
                        new LiteralInteger(2)
                    )
                ),
            ],
            orderBy:
            [
                new OrderItemProjection(
                    new FieldDecimal("item_revenue_by_user", "item_revenue"),
                    SortDirection.Desc
                ),
            ]
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
                    new EmployeeNameColumn(),
                    new EmployeeShiftStartColumn(),
                    new Column(new String("ordered"), new DoubleColumnType()),
                    new Column(new String("item_revenue"), new DoubleColumnType()),
                    new Column(
                        new String("item_revenue_per_ordered"),
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
                                new EmployeeNameColumn(),
                                new InvariantCell(new String("Iris"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new EmployeeShiftStartColumn(),
                                new InvariantCell(
                                    new Time(
                                        new UShort(11),
                                        new UShort(30),
                                        new UShort(0)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("ordered"), new DoubleColumnType()),
                                new InvariantCell(new Double(375))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("item_revenue"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(30))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("item_revenue_per_ordered"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.08))
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
                                new EmployeeNameColumn(),
                                new InvariantCell(new String("Hank"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new EmployeeShiftStartColumn(),
                                new InvariantCell(
                                    new Time(new UShort(10), new UShort(0), new UShort(0))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("ordered"), new DoubleColumnType()),
                                new InvariantCell(new Double(200))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("item_revenue"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(23))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("item_revenue_per_ordered"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.11))
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
