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
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Numbers;

/// <summary>
/// Selects from schema_with_foreign_keys.orders inner joined to
/// schema_with_foreign_keys.users on order_user_id equal to user_id and order_total to
/// the power 2 below user_age times 2000, where the square root of order_total is below
/// 12, grouped by magnitude, floor of log10 of the greater of order_total and 1, and by
/// doublings, floor of the logarithm to base 2 of the same, keeping groups where exp of
/// the average natural logarithm of that value is above 60: magnitude, doublings,
/// lower_bound as 10 to the power magnitude, the number of orders and the average square
/// root of order_total, ordered by magnitude and doublings.
/// </summary>
public sealed record PowersAndLogarithmsInEveryClauseQuery
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
                            new UsersTable().Name,
                        ]
                    ).TextValue,
                    new AndRow([
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
                        ),
                        new LessThanDecimalRow(
                            new PowerDecimalRow(
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
                                new LiteralInteger(2)
                            ),
                            new MultiplyDecimalRow([
                                new FieldDecimal(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new UsersTable().Name,
                                        ]
                                    ).TextValue,
                                    new UserAgeColumn().Name.TextValue
                                ),
                                new LiteralInteger(2000),
                            ])
                        ),
                    ])
                ),
            ],
            where: new LessThanDecimalRow(
                new SqrtDecimalRow(
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
                new LiteralDecimal(12m)
            ),
            groupBy:
            [
                new GroupKey(
                    "magnitude",
                    new FloorIntegerRow(
                        new Log10DecimalRow(
                            new GreatestDecimalRow([
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
                                new LiteralInteger(1),
                            ])
                        )
                    )
                ),
                new GroupKey(
                    "doublings",
                    new FloorIntegerRow(
                        new LogDecimalRow(
                            new GreatestDecimalRow([
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
                                new LiteralInteger(1),
                            ]),
                            new LiteralInteger(2)
                        )
                    )
                ),
            ],
            having: new GreaterThanDecimalGroup(
                new ExpDecimalGroup(
                    new AverageDecimalGroup(
                        new LnDecimalRow(
                            new GreatestDecimalRow([
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
                                new LiteralInteger(1),
                            ])
                        )
                    )
                ),
                new LiteralInteger(60)
            ),
            select:
            [
                new SelectItemGroup("magnitude", new KeyInteger("magnitude")),
                new SelectItemGroup("doublings", new KeyInteger("doublings")),
                new SelectItemGroup(
                    "lower_bound",
                    new PowerDecimalGroup(
                        new LiteralInteger(10),
                        new KeyInteger("magnitude")
                    )
                ),
                new SelectItemGroup("orders", new CountGroup()),
                new SelectItemGroup(
                    "average_root",
                    new AverageDecimalGroup(
                        new SqrtDecimalRow(
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
                    )
                ),
            ],
            orderBy:
            [
                new OrderItemGroupExpression(new KeyInteger("magnitude")),
                new OrderItemGroupExpression(new KeyInteger("doublings")),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and
    /// <see cref="AuditSchemaDataSet"/>, in this order.
    /// The square roots, logarithms and exponential are the exact ones rounded at each step
    /// as the specification rounds a decimal, computed with Python decimal at 120 digits
    /// and checked against PostgreSQL numeric.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("magnitude"), new LongColumnType()),
                    new Column(new String("doublings"), new LongColumnType()),
                    new Column(new String("lower_bound"), new DoubleColumnType()),
                    new Column(new String("orders"), new LongColumnType()),
                    new Column(new String("average_root"), new DoubleColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("magnitude"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("doublings"), new LongColumnType()),
                                new InvariantCell(new Long(6))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lower_bound"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(10.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("average_root"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(8.674675786448736))
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
                                new Column(new String("magnitude"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("doublings"), new LongColumnType()),
                                new InvariantCell(new Long(6))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lower_bound"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(100.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("average_root"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(10.024968827881711))
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
