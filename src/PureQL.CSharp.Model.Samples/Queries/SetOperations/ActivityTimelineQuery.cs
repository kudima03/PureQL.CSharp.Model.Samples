using Pure.Collections.Generic;
using Pure.Primitives.Date;
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
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.ProjectionExpressions;
using PureQL.CSharp.Model.RowExpressions;
using PureQL.CSharp.Model.Samples.Cells;
using Column = Pure.RelationalSchema.Column.Column;
using DateTime = Pure.Primitives.DateTime.DateTime;
using Double = Pure.Primitives.Number.Double;
using ModelPagination = PureQL.CSharp.Model.Pagination;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.SetOperations;

/// <summary>
/// Selects at, kind and amount from a subquery events that is the unionAll of the orders
/// of schema_with_foreign_keys.orders (placed_at, "placed", order_total), the logins of
/// audit.logins (login_at, "logged in", a null decimal) and the sign-ups of
/// schema_with_foreign_keys.users (signup_date as a datetime, "signed up", a null
/// decimal), keeping the events at or after 2022-01-01T00:00:00Z, ordered by at
/// descending, then by kind, and taking the first 11.
/// </summary>
public sealed record ActivityTimelineQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithPagination(
            subqueries:
            [
                new SubqueryQuery(
                    "events",
                    new CombinedQueryUnionAll(
                        unionAll:
                        [
                            new PlainQueryWithoutPaginationWithoutOrderBy(
                                from: new FromEntity(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new OrdersTable().Name,
                                        ]
                                    ).TextValue
                                ),
                                select:
                                [
                                    new SelectItemProjection(
                                        "at",
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
                                    new SelectItemProjection(
                                        "kind",
                                        new LiteralString("placed")
                                    ),
                                    new SelectItemProjection(
                                        "amount",
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
                                ]
                            ),
                            new PlainQueryWithoutPaginationWithoutOrderBy(
                                from: new FromEntity(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new AuditRelationalSchema().Name,
                                            new LoginsTable().Name,
                                        ]
                                    ).TextValue
                                ),
                                select:
                                [
                                    new SelectItemProjection(
                                        "at",
                                        new FieldDatetime(
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new AuditRelationalSchema().Name,
                                                    new LoginsTable().Name,
                                                ]
                                            ).TextValue,
                                            new LoginAtColumn().Name.TextValue
                                        )
                                    ),
                                    new SelectItemProjection(
                                        "kind",
                                        new LiteralString("logged in")
                                    ),
                                    new SelectItemProjection(
                                        "amount",
                                        new LiteralDecimalNullable()
                                    ),
                                ]
                            ),
                            new PlainQueryWithoutPaginationWithoutOrderBy(
                                from: new FromEntity(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new UsersTable().Name,
                                        ]
                                    ).TextValue
                                ),
                                select:
                                [
                                    new SelectItemProjection(
                                        "at",
                                        new AsDatetimeDateDatetimeProjection(
                                            new FieldDate(
                                                new JoinedString(
                                                    new DotString(),
                                                    [
                                                        new RelationalSchemaWithForeignKeys().Name,
                                                        new UsersTable().Name,
                                                    ]
                                                ).TextValue,
                                                new SignupDateColumn().Name.TextValue
                                            )
                                        )
                                    ),
                                    new SelectItemProjection(
                                        "kind",
                                        new LiteralString("signed up")
                                    ),
                                    new SelectItemProjection(
                                        "amount",
                                        new LiteralDecimalNullable()
                                    ),
                                ]
                            ),
                        ]
                    )
                ),
            ],
            from: new FromSubquery("events"),
            where: new GreaterThanOrEqualDatetimeRow(
                new FieldDatetime("events", "at"),
                new LiteralDatetime(
                    new DateTimeOffset(2022, 1, 1, 0, 0, 0, TimeSpan.Zero)
                )
            ),
            select:
            [
                new SelectItemProjection("at", new FieldDatetime("events", "at")),
                new SelectItemProjection("kind", new FieldString("events", "kind")),
                new SelectItemProjection(
                    "amount",
                    new FieldDecimalNullable("events", "amount")
                ),
            ],
            orderBy:
            [
                new OrderItemProjectionExpression(
                    new FieldDatetime("events", "at"),
                    SortDirection.Desc
                ),
                new OrderItemProjectionExpression(new FieldString("events", "kind")),
            ],
            pagination: new ModelPagination(take: 11)
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
                    new Column(new String("at"), new DateTimeColumnType()),
                    new Column(new String("kind"), new StringColumnType()),
                    new Column(new String("amount"), new DoubleColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("at"), new DateTimeColumnType()),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(6),
                                            new UShort(6),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(15),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("kind"), new StringColumnType()),
                                new InvariantCell(new String("placed"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("amount"), new DoubleColumnType()),
                                new InvariantCell(new Double(100.5))
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
                                new Column(new String("at"), new DateTimeColumnType()),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(5),
                                            new UShort(6),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(14),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("kind"), new StringColumnType()),
                                new InvariantCell(new String("placed"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("amount"), new DoubleColumnType()),
                                new InvariantCell(new Double(300))
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
                                new Column(new String("at"), new DateTimeColumnType()),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(4),
                                            new UShort(6),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(13),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("kind"), new StringColumnType()),
                                new InvariantCell(new String("placed"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("amount"), new DoubleColumnType()),
                                new InvariantCell(new Double(75.25))
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
                                new Column(new String("at"), new DateTimeColumnType()),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(4),
                                            new UShort(6),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(6),
                                            new UShort(45),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("kind"), new StringColumnType()),
                                new InvariantCell(new String("logged in"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("amount"), new DoubleColumnType()),
                                new EmptyCell()
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
                                new Column(new String("at"), new DateTimeColumnType()),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(3),
                                            new UShort(6),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(12),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("kind"), new StringColumnType()),
                                new InvariantCell(new String("placed"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("amount"), new DoubleColumnType()),
                                new InvariantCell(new Double(200))
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
                                new Column(new String("at"), new DateTimeColumnType()),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(3),
                                            new UShort(6),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(8),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("kind"), new StringColumnType()),
                                new InvariantCell(new String("logged in"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("amount"), new DoubleColumnType()),
                                new EmptyCell()
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
                                new Column(new String("at"), new DateTimeColumnType()),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(2),
                                            new UShort(6),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(11),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("kind"), new StringColumnType()),
                                new InvariantCell(new String("placed"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("amount"), new DoubleColumnType()),
                                new InvariantCell(new Double(50))
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
                                new Column(new String("at"), new DateTimeColumnType()),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(2),
                                            new UShort(6),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(7),
                                            new UShort(30),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("kind"), new StringColumnType()),
                                new InvariantCell(new String("logged in"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("amount"), new DoubleColumnType()),
                                new EmptyCell()
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
                                new Column(new String("at"), new DateTimeColumnType()),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(1),
                                            new UShort(6),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(10),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("kind"), new StringColumnType()),
                                new InvariantCell(new String("placed"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("amount"), new DoubleColumnType()),
                                new InvariantCell(new Double(100.5))
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
                                new Column(new String("at"), new DateTimeColumnType()),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(1),
                                            new UShort(6),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(7),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("kind"), new StringColumnType()),
                                new InvariantCell(new String("logged in"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("amount"), new DoubleColumnType()),
                                new EmptyCell()
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
                                new Column(new String("at"), new DateTimeColumnType()),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(28),
                                            new UShort(2),
                                            new UShort(2023)
                                        ),
                                        new Time(
                                            new UShort(0),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("kind"), new StringColumnType()),
                                new InvariantCell(new String("signed up"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("amount"), new DoubleColumnType()),
                                new EmptyCell()
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
