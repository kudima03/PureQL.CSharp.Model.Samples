using Pure.Collections.Generic;
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
using PureQL.CSharp.Model.ProjectionExpressions;
using PureQL.CSharp.Model.RowExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using Guid = Pure.Primitives.Guid.Guid;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Nulls;

/// <summary>
/// Selects from schema_with_foreign_keys.orders inner joined to
/// schema_with_foreign_keys.users on user_id equal to order_user_id, where order_total
/// divided by nullIfEqual of user_age minus 25 and 0 is less than 50: order_id,
/// nullIfEqual of order_status and the sentinel pending, nullIfEqual of user_age minus 25
/// and 0, order_total divided by it, and that ratio with 0 through coalesce, ordered by
/// order_id.
/// </summary>
public sealed record NullIfEqualQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithoutPagination(
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
                    new EqualUuidRow(
                        new FieldUuid(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue,
                            new UserIdColumn().Name.TextValue
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
            where: new LessThanDecimalRow(
                new DivideDecimalNullableRow([
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
                    new NullIfEqualDecimalNullableRow(
                        new SubtractDecimalRow([
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
                            new LiteralInteger(25),
                        ]),
                        new LiteralInteger(0)
                    ),
                ]),
                new LiteralInteger(50)
            ),
            select:
            [
                new SelectItemProjection(
                    "order_id",
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
                ),
                new SelectItemProjection(
                    "known_status",
                    new NullIfEqualStringNullableProjection(
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
                        new LiteralString("pending")
                    )
                ),
                new SelectItemProjection(
                    "years_past_25",
                    new NullIfEqualDecimalNullableProjection(
                        new SubtractDecimalProjection([
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
                            new LiteralInteger(25),
                        ]),
                        new LiteralInteger(0)
                    )
                ),
                new SelectItemProjection(
                    "total_per_year_past_25",
                    new DivideDecimalNullableProjection([
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
                        new NullIfEqualDecimalNullableProjection(
                            new SubtractDecimalProjection([
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
                                new LiteralInteger(25),
                            ]),
                            new LiteralInteger(0)
                        ),
                    ])
                ),
                new SelectItemProjection(
                    "total_per_year_past_25_or_zero",
                    new CoalesceDecimalProjection([
                        new DivideDecimalNullableProjection([
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
                            new NullIfEqualDecimalNullableProjection(
                                new SubtractDecimalProjection([
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
                                    new LiteralInteger(25),
                                ]),
                                new LiteralInteger(0)
                            ),
                        ]),
                        new LiteralDecimal(0m),
                    ])
                ),
            ],
            orderBy:
            [
                new OrderItemProjectionExpression(
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
                ),
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
                    new OrderIdColumn(),
                    new Column(new String("known_status"), new StringColumnType()),
                    new Column(new String("years_past_25"), new DoubleColumnType()),
                    new Column(
                        new String("total_per_year_past_25"),
                        new DoubleColumnType()
                    ),
                    new Column(
                        new String("total_per_year_past_25_or_zero"),
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
                                new OrderIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000065-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("known_status"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("shipped"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("years_past_25"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("total_per_year_past_25"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(20.1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("total_per_year_past_25_or_zero"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(20.1))
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
                                new OrderIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000066-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("known_status"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("years_past_25"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("total_per_year_past_25"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(10))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("total_per_year_past_25_or_zero"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(10))
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
                                new OrderIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000068-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("known_status"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("cancelled"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("years_past_25"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("total_per_year_past_25"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(15.05))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("total_per_year_past_25_or_zero"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(15.05))
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
                                new OrderIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "0000006a-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("known_status"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("years_past_25"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(17))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("total_per_year_past_25"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(5.911764705882353))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("total_per_year_past_25_or_zero"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(5.911764705882353))
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
