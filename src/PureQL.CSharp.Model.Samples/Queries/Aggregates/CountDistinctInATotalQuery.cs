using Pure.Collections.Generic;
using Pure.Primitives.Bool;
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
/// Selects, as a total query over schema_with_foreign_keys.orders left joined to
/// schema_with_foreign_keys.employees on employee_user_id equal to order_user_id (the
/// employee_manager_id standing in for a nullable coupon) and kept where order_status is
/// not "cancelled": the count of orders, countDistinct of order_user_id, of the nullable
/// employee_manager_id, and of order_user_id with the predicate that employee_manager_id
/// is not null, the count divided by countDistinct of order_user_id guarded with
/// nullIfEqual against 0, whether the count equals countDistinct of order_user_id, and
/// countDistinct of the startOfMonth of placed_on.
/// </summary>
public sealed record CountDistinctInATotalQuery
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
            joins:
            [
                new JoinEntity(
                    JoinKind.Left,
                    new JoinedString(
                        new DotString(),
                        [
                            new RelationalSchemaWithForeignKeys().Name,
                            new EmployeesTable().Name,
                        ]
                    ).TextValue,
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
                    )
                ),
            ],
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
            aggregate:
            [
                new SelectItemTotal("orders", new CountTotal()),
                new SelectItemTotal(
                    "customers",
                    new CountDistinctTotal(
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
                    )
                ),
                new SelectItemTotal(
                    "coupons",
                    new CountDistinctTotal(
                        new FieldUuidNullable(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new EmployeesTable().Name,
                                ]
                            ).TextValue,
                            new EmployeeManagerIdColumn().Name.TextValue
                        )
                    )
                ),
                new SelectItemTotal(
                    "customers_with_a_coupon",
                    new CountDistinctTotal(
                        new FieldUuid(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrdersTable().Name,
                                ]
                            ).TextValue,
                            new OrderUserIdColumn().Name.TextValue
                        ),
                        new NotEqualUuidRow(
                            new FieldUuidNullable(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new EmployeesTable().Name,
                                    ]
                                ).TextValue,
                                new EmployeeManagerIdColumn().Name.TextValue
                            ),
                            new LiteralUuidNullable()
                        )
                    )
                ),
                new SelectItemTotal(
                    "orders_per_customer",
                    new DivideDecimalNullableTotal([
                        new CountTotal(),
                        new NullIfEqualIntegerNullableTotal(
                            new CountDistinctTotal(
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
                            new LiteralInteger(0)
                        ),
                    ])
                ),
                new SelectItemTotal(
                    "every_order_has_its_own_customer",
                    new EqualDecimalTotal(
                        new CountTotal(),
                        new CountDistinctTotal(
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
                        )
                    )
                ),
                new SelectItemTotal(
                    "months",
                    new CountDistinctTotal(
                        new StartOfMonthDateRow(
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
                    )
                ),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and
    /// <see cref="AuditSchemaDataSet"/>, as one row.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("orders"), new LongColumnType()),
                    new Column(new String("customers"), new LongColumnType()),
                    new Column(new String("coupons"), new LongColumnType()),
                    new Column(
                        new String("customers_with_a_coupon"),
                        new LongColumnType()
                    ),
                    new Column(new String("orders_per_customer"), new DoubleColumnType()),
                    new Column(
                        new String("every_order_has_its_own_customer"),
                        new BoolColumnType()
                    ),
                    new Column(new String("months"), new LongColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("customers"), new LongColumnType()),
                                new InvariantCell(new Long(4))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("coupons"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("customers_with_a_coupon"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("orders_per_customer"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1.25))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("every_order_has_its_own_customer"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("months"), new LongColumnType()),
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
