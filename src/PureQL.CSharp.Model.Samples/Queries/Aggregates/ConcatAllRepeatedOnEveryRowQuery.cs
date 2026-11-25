using Pure.Collections.Generic;
using Pure.Primitives.Bool;
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
using Guid = Pure.Primitives.Guid.Guid;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Aggregates;

/// <summary>
/// Selects order_id as id, order_status as status, the distinct order_status values of
/// all rows joined by ", " in order of order_status as all_statuses, whether that list
/// starts with the order_status of the row as status_is_listed_first, and a timeline of
/// every row (placed_on as text, a space and order_status) joined by a line break in
/// order of placed_at and order_id, from schema_with_foreign_keys.orders, ordered by
/// placed_at, then by order_id.
/// </summary>
public sealed record ConcatAllRepeatedOnEveryRowQuery
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
            select:
            [
                new SelectItemProjection(
                    "id",
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
                    "status",
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
                ),
                new SelectItemProjection(
                    "all_statuses",
                    new ConcatAllProjection(
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
                        new LiteralString(", "),
                        [
                            new OrderItemRow(
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
                            ),
                        ],
                        distinct: true
                    )
                ),
                new SelectItemProjection(
                    "status_is_listed_first",
                    new StartsWithProjection(
                        new ConcatAllProjection(
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
                            new LiteralString(", "),
                            [
                                new OrderItemRow(
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
                                ),
                            ],
                            distinct: true
                        ),
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
                ),
                new SelectItemProjection(
                    "timeline",
                    new ConcatAllProjection(
                        new ConcatWithRow(
                            [
                                new AsStringDateStringRow(
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
                                ),
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
                            ],
                            new LiteralString(" ")
                        ),
                        new LiteralString("\n"),
                        [
                            new OrderItemRow(
                                new FieldDatetime(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new OrdersTable().Name,
                                        ]
                                    ).TextValue,
                                    new PlacedAtColumn().Name.TextValue
                                )
                            ),
                            new OrderItemRow(
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
                    )
                ),
            ],
            orderBy:
            [
                new OrderItemProjectionExpression(
                    new FieldDatetime(
                        new JoinedString(
                            new DotString(),
                            [
                                new RelationalSchemaWithForeignKeys().Name,
                                new OrdersTable().Name,
                            ]
                        ).TextValue,
                        new PlacedAtColumn().Name.TextValue
                    )
                ),
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
                    new Column(new String("id"), new UuidColumnType()),
                    new Column(new String("status"), new StringColumnType()),
                    new Column(new String("all_statuses"), new StringColumnType()),
                    new Column(
                        new String("status_is_listed_first"),
                        new BoolColumnType()
                    ),
                    new Column(new String("timeline"), new StringColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("id"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000065-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("status"), new StringColumnType()),
                                new InvariantCell(new String("shipped"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("all_statuses"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(
                                    new String("cancelled, pending, shipped")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("status_is_listed_first"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("timeline"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(
                                    new String(
                                        "2024-06-01 shipped\n2024-06-02 pending\n2024-06-03 shipped\n2024-06-04 cancelled\n2024-06-05 shipped\n2024-06-06 pending"
                                    )
                                )
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
                                new Column(new String("id"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000066-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("status"), new StringColumnType()),
                                new InvariantCell(new String("pending"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("all_statuses"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(
                                    new String("cancelled, pending, shipped")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("status_is_listed_first"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("timeline"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(
                                    new String(
                                        "2024-06-01 shipped\n2024-06-02 pending\n2024-06-03 shipped\n2024-06-04 cancelled\n2024-06-05 shipped\n2024-06-06 pending"
                                    )
                                )
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
                                new Column(new String("id"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000067-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("status"), new StringColumnType()),
                                new InvariantCell(new String("shipped"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("all_statuses"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(
                                    new String("cancelled, pending, shipped")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("status_is_listed_first"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("timeline"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(
                                    new String(
                                        "2024-06-01 shipped\n2024-06-02 pending\n2024-06-03 shipped\n2024-06-04 cancelled\n2024-06-05 shipped\n2024-06-06 pending"
                                    )
                                )
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
                                new Column(new String("id"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000068-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("status"), new StringColumnType()),
                                new InvariantCell(new String("cancelled"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("all_statuses"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(
                                    new String("cancelled, pending, shipped")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("status_is_listed_first"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("timeline"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(
                                    new String(
                                        "2024-06-01 shipped\n2024-06-02 pending\n2024-06-03 shipped\n2024-06-04 cancelled\n2024-06-05 shipped\n2024-06-06 pending"
                                    )
                                )
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
                                new Column(new String("id"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000069-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("status"), new StringColumnType()),
                                new InvariantCell(new String("shipped"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("all_statuses"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(
                                    new String("cancelled, pending, shipped")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("status_is_listed_first"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("timeline"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(
                                    new String(
                                        "2024-06-01 shipped\n2024-06-02 pending\n2024-06-03 shipped\n2024-06-04 cancelled\n2024-06-05 shipped\n2024-06-06 pending"
                                    )
                                )
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
                                new Column(new String("id"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "0000006a-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("status"), new StringColumnType()),
                                new InvariantCell(new String("pending"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("all_statuses"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(
                                    new String("cancelled, pending, shipped")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("status_is_listed_first"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("timeline"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(
                                    new String(
                                        "2024-06-01 shipped\n2024-06-02 pending\n2024-06-03 shipped\n2024-06-04 cancelled\n2024-06-05 shipped\n2024-06-06 pending"
                                    )
                                )
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
