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
using Guid = Pure.Primitives.Guid.Guid;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Recursive;

/// <summary>
/// Selects the revenue rolled up the employee hierarchy: a recursive subquery lineage
/// pairs every employee of schema_with_foreign_keys.employees with itself at distance 0
/// and, level by level, with everyone below it at distance + 1 (maxDepth 50); the main
/// query reads lineage, inner joins schema_with_foreign_keys.orders on order_user_id
/// equal to the user_id of each pair, groups by ancestor_id, and selects it as
/// employee_id with the sum of order_total, the sum with a predicate distance = 0, count
/// and the max of distance, ordered by the sum of order_total descending and the key.
/// </summary>
public sealed record TreeRollUpQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainGroupedQueryWithoutPagination(
            subqueries:
            [
                new SubqueryRecursive(
                    "lineage",
                    new Recursion(
                        new PlainQueryWithoutPaginationWithoutOrderBy(
                            from: new FromEntity(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new EmployeesTable().Name,
                                    ]
                                ).TextValue
                            ),
                            select:
                            [
                                new SelectItemProjection(
                                    "ancestor_id",
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
                                ),
                                new SelectItemProjection(
                                    "employee_id",
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
                                ),
                                new SelectItemProjection(
                                    "user_id",
                                    new FieldUuid(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new EmployeesTable().Name,
                                            ]
                                        ).TextValue,
                                        new EmployeeUserIdColumn().Name.TextValue
                                    )
                                ),
                                new SelectItemProjection(
                                    "distance",
                                    new LiteralInteger(0)
                                ),
                            ]
                        ),
                        new PlainNextQuery(
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
                                    "lineage",
                                    new EqualUuidRow(
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
                                        new FieldUuid("lineage", "employee_id")
                                    )
                                ),
                            ],
                            select:
                            [
                                new SelectItemRow(
                                    "ancestor_id",
                                    new FieldUuid("lineage", "ancestor_id")
                                ),
                                new SelectItemRow(
                                    "employee_id",
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
                                ),
                                new SelectItemRow(
                                    "user_id",
                                    new FieldUuid(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new EmployeesTable().Name,
                                            ]
                                        ).TextValue,
                                        new EmployeeUserIdColumn().Name.TextValue
                                    )
                                ),
                                new SelectItemRow(
                                    "distance",
                                    new AddIntegerRow([
                                        new FieldInteger("lineage", "distance"),
                                        new LiteralInteger(1),
                                    ])
                                ),
                            ]
                        ),
                        50
                    )
                ),
            ],
            from: new FromSubquery("lineage"),
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
                                    new OrdersTable().Name,
                                ]
                            ).TextValue,
                            new OrderUserIdColumn().Name.TextValue
                        ),
                        new FieldUuid("lineage", "user_id")
                    )
                ),
            ],
            groupBy:
            [
                new GroupKey("ancestor_id", new FieldUuid("lineage", "ancestor_id")),
            ],
            select:
            [
                new SelectItemGroup("employee_id", new KeyUuid("ancestor_id")),
                new SelectItemGroup(
                    "network_revenue",
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
                new SelectItemGroup(
                    "own_revenue",
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
                        ),
                        new EqualDecimalRow(
                            new FieldInteger("lineage", "distance"),
                            new LiteralInteger(0)
                        )
                    )
                ),
                new SelectItemGroup("network_orders", new CountGroup()),
                new SelectItemGroup(
                    "deepest_level_with_orders",
                    new MaxIntegerGroup(new FieldInteger("lineage", "distance"))
                ),
            ],
            orderBy:
            [
                new OrderItemGroupExpression(
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
                    ),
                    SortDirection.Desc
                ),
                new OrderItemGroupExpression(new KeyUuid("ancestor_id")),
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
                    new EmployeeIdColumn(),
                    new Column(new String("network_revenue"), new DoubleColumnType()),
                    new Column(new String("own_revenue"), new DoubleColumnType()),
                    new Column(new String("network_orders"), new LongColumnType()),
                    new Column(
                        new String("deepest_level_with_orders"),
                        new LongColumnType()
                    ),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new EmployeeIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "000002bd-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("network_revenue"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(826.25))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("own_revenue"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(150.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("network_orders"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(6))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("deepest_level_with_orders"),
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
                                new EmployeeIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "000002bf-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("network_revenue"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(375.25))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("own_revenue"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(375.25))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("network_orders"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("deepest_level_with_orders"),
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
                                new EmployeeIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "000002be-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("network_revenue"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(300.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("own_revenue"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(200))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("network_orders"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("deepest_level_with_orders"),
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
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new EmployeeIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "000002c0-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("network_revenue"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(100.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("own_revenue"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(100.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("network_orders"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("deepest_level_with_orders"),
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
            ]
        );
}
