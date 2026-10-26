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
using PureQL.CSharp.Model.GroupKeys;
using PureQL.CSharp.Model.Keys;
using PureQL.CSharp.Model.Lists;
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.RowExpressions;
using PureQL.CSharp.Model.SelectItems;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Dashboards;

/// <summary>
/// Selects employee_name, employee_shift_start, user_age, the sum of item_qty times
/// product_price rounded to 0 digits as revenue, the count of rows whose item_qty equals 1
/// as single_unit_lines and that sum divided by user_age rounded to 1 digit as
/// revenue_per_year_of_age from schema_with_foreign_keys.order_items inner joined with
/// schema_with_foreign_keys.orders on item_order_id equal to order_id, with
/// schema_with_foreign_keys.products on item_product_id equal to product_id, with
/// schema_with_foreign_keys.employees on order_user_id equal to employee_user_id and with
/// schema_with_foreign_keys.users on employee_user_id equal to user_id, where
/// order_status is not in cancelled and refunded and employee_name is in Grace, Hank and
/// Iris, grouped by employee_name, user_age, employee_shift_start and employee_id,
/// ordered by the unrounded sum descending.
/// </summary>
public sealed record RevenuePerEmployeeAndAgeQuery
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
                                "employee_name",
                                new StringRow(
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
                                )
                            )
                        )
                    ),
                    new GroupKey(
                        new GroupKeyNonNullable(
                            new GroupKeyDecimal(
                                "user_age",
                                new ExactDecimalRow(
                                    new FieldDecimal(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new UsersTable().Name,
                                            ]
                                        ).TextValue,
                                        new UserAgeColumn().Name.TextValue
                                    )
                                )
                            )
                        )
                    ),
                    new GroupKey(
                        new GroupKeyNonNullable(
                            new GroupKeyTime(
                                "employee_shift_start",
                                new TimeRow(
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
                                )
                            )
                        )
                    ),
                    new GroupKey(
                        new GroupKeyNonNullable(
                            new GroupKeyUuid(
                                "employee_id",
                                new UuidRow(
                                    new FieldUuid(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new EmployeesTable().Name,
                                            ]
                                        ).TextValue,
                                        new EmployeeIdColumn().Name.TextValue
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
                                "employee_name",
                                new StringGroup(new KeyString("employee_name"))
                            )
                        )
                    ),
                    new SelectItemGroup(
                        new SelectItemGroupNonNullable(
                            new SelectItemGroupTime(
                                "employee_shift_start",
                                new TimeGroup(new KeyTime("employee_shift_start"))
                            )
                        )
                    ),
                    new SelectItemGroup(
                        new SelectItemGroupNonNullable(
                            new SelectItemGroupDecimal(
                                "user_age",
                                new ExactDecimalGroup(new KeyDecimal("user_age"))
                            )
                        )
                    ),
                    new SelectItemGroup(
                        new SelectItemGroupNonNullable(
                            new SelectItemGroupDecimal(
                                "revenue",
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
                                "single_unit_lines",
                                new IntegerGroup(
                                    new AggregateIntegerGroup(
                                        new CountGroup(
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
                                                                    new LiteralDecimal(1m)
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
                                "revenue_per_year_of_age",
                                new ExactDecimalGroup(
                                    new RoundDecimalGroup(
                                        new DecimalGroup(
                                            new ExactDecimalGroup(
                                                new ArithmeticDecimalGroup(
                                                    new DivideDecimalGroup([
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
                                                        new DecimalGroup(
                                                            new ExactDecimalGroup(
                                                                new KeyDecimal("user_age")
                                                            )
                                                        ),
                                                    ])
                                                )
                                            )
                                        ),
                                        new IntegerGroup(new LiteralInteger(1))
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
                                    new EmployeesTable().Name,
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
                                                                new EmployeesTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new EmployeeUserIdColumn()
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
                                                                new EmployeesTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new EmployeeUserIdColumn()
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
                new BooleanRow(
                    new LogicalRow(
                        new AndRow([
                            new BooleanRow(
                                new LogicalRow(
                                    new NotRow(
                                        new BooleanRow(
                                            new ComparisonRow(
                                                new InRow(
                                                    new InStringRow(
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
                                                        new ListString(
                                                            new ListLiteralString([
                                                                "cancelled",
                                                                "refunded",
                                                            ])
                                                        )
                                                    )
                                                )
                                            )
                                        )
                                    )
                                )
                            ),
                            new BooleanRow(
                                new ComparisonRow(
                                    new InRow(
                                        new InStringRow(
                                            new StringNullableRow(
                                                new StringRow(
                                                    new FieldString(
                                                        new JoinedString(
                                                            new DotString(),
                                                            [
                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                new EmployeesTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new EmployeeNameColumn()
                                                            .Name
                                                            .TextValue
                                                    )
                                                )
                                            ),
                                            new ListString(
                                                new ListLiteralString([
                                                    "Grace",
                                                    "Hank",
                                                    "Iris",
                                                ])
                                            )
                                        )
                                    )
                                )
                            ),
                        ])
                    )
                ),
                having: null,
                [
                    new OrderItemGroup(
                        new ExprGroup(
                            new DecimalNullableGroup(
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
                            )
                        ),
                        SortDirection.Desc
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
                    new EmployeeNameColumn(),
                    new EmployeeShiftStartColumn(),
                    new UserAgeColumn(),
                    new Column(new String("revenue"), new DoubleColumnType()),
                    new Column(new String("single_unit_lines"), new LongColumnType()),
                    new Column(
                        new String("revenue_per_year_of_age"),
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
                                new InvariantCell(new String("Grace"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new EmployeeShiftStartColumn(),
                                new InvariantCell(
                                    new Time(new UShort(9), new UShort(0), new UShort(0))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new UserAgeColumn(),
                                new InvariantCell(new Double(30))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("revenue"), new DoubleColumnType()),
                                new InvariantCell(new Double(40))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("single_unit_lines"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("revenue_per_year_of_age"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1.3))
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
                                new UserAgeColumn(),
                                new InvariantCell(new Double(30))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("revenue"), new DoubleColumnType()),
                                new InvariantCell(new Double(30))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("single_unit_lines"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("revenue_per_year_of_age"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1))
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
                                new UserAgeColumn(),
                                new InvariantCell(new Double(25))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("revenue"), new DoubleColumnType()),
                                new InvariantCell(new Double(23))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("single_unit_lines"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("revenue_per_year_of_age"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.9))
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
