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
using PureQL.CSharp.Model.Keys;
using PureQL.CSharp.Model.Lists;
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.RowExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Dashboards;

/// <summary>
/// Selects 2024-06-01 plus period_no times 3 days as period_start and the sums of
/// order_total whose lead employee_name equals Grace and Hank, each rounded to 0 digits,
/// as revenue_team_grace and revenue_team_hank from schema_with_foreign_keys.orders
/// inner joined with schema_with_foreign_keys.employees as rep on order_user_id equal to
/// the rep employee_user_id and with schema_with_foreign_keys.employees as lead on the
/// rep employee_manager_id equal to the lead employee_id, where order_status is not in
/// cancelled and refunded, grouped by the days from 2024-06-01 to placed_on integer
/// divided by 3 as period_no, ordered by period_no.
/// </summary>
public sealed record RevenuePerTeamLeadPerPeriodQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainGroupedQueryWithoutPagination(
            from: new FromEntity(
                new JoinedString(
                    new DotString(),
                    [new RelationalSchemaWithForeignKeys().Name, new OrdersTable().Name]
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
                            new EmployeesTable().Name,
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
                        new FieldUuid("rep", new EmployeeUserIdColumn().Name.TextValue)
                    ),
                    "rep"
                ),
                new JoinEntity(
                    JoinKind.Inner,
                    new JoinedString(
                        new DotString(),
                        [
                            new RelationalSchemaWithForeignKeys().Name,
                            new EmployeesTable().Name,
                        ]
                    ).TextValue,
                    new EqualUuidRow(
                        new FieldUuidNullable(
                            "rep",
                            new EmployeeManagerIdColumn().Name.TextValue
                        ),
                        new FieldUuid("lead", new EmployeeIdColumn().Name.TextValue)
                    ),
                    "lead"
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
                    "period_no",
                    new IntegerDivideIntegerRow(
                        new DateDiffDaysIntegerRow(
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
                            new LiteralDate(new DateOnly(2024, 6, 1))
                        ),
                        new LiteralInteger(3)
                    )
                ),
            ],
            select:
            [
                new SelectItemGroup(
                    "period_start",
                    new DateAddDaysDateGroup(
                        new LiteralDate(new DateOnly(2024, 6, 1)),
                        new MultiplyIntegerGroup([
                            new KeyInteger("period_no"),
                            new LiteralInteger(3),
                        ])
                    )
                ),
                new SelectItemGroup(
                    "revenue_team_grace",
                    new RoundDecimalGroup(
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
                            new EqualStringRow(
                                new FieldString(
                                    "lead",
                                    new EmployeeNameColumn().Name.TextValue
                                ),
                                new LiteralString("Grace")
                            )
                        ),
                        new LiteralInteger(0)
                    )
                ),
                new SelectItemGroup(
                    "revenue_team_hank",
                    new RoundDecimalGroup(
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
                            new EqualStringRow(
                                new FieldString(
                                    "lead",
                                    new EmployeeNameColumn().Name.TextValue
                                ),
                                new LiteralString("Hank")
                            )
                        ),
                        new LiteralInteger(0)
                    )
                ),
            ],
            orderBy: [new OrderItemGroup(new KeyInteger("period_no"))]
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
                    new Column(new String("period_start"), new DateColumnType()),
                    new Column(new String("revenue_team_grace"), new DoubleColumnType()),
                    new Column(new String("revenue_team_hank"), new DoubleColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("period_start"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(1),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("revenue_team_grace"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(200))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("revenue_team_hank"),
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
                                    new String("period_start"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(4),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("revenue_team_grace"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(300))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("revenue_team_hank"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(101))
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
