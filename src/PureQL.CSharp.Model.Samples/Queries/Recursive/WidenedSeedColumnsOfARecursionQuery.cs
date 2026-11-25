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
using Guid = Pure.Primitives.Guid.Guid;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Recursive;

/// <summary>
/// Selects id, parent_id, seen_at, score, share and label from a recursive subquery tree
/// (maxDepth 20), ordered by id. Its seed reads schema_with_foreign_keys.employees inner
/// joined with schema_with_foreign_keys.users on employee_user_id equal to user_id, where
/// employee_manager_id is equal to a null uuid, and declares its columns as the next step
/// fills them: employee_id as id, asNullable of employee_id as parent_id, asNullable of
/// last_login as seen_at, asDecimal of user_score rounded to an integer as score,
/// asDecimal of the integer 1 as share and asNullable of employee_name as label. Its next
/// step reads schema_with_foreign_keys.employees inner joined with tree on
/// employee_manager_id equal to the id of tree and left joined with audit.logins on
/// login_user_id equal to employee_user_id: employee_id as id, the id of tree as
/// parent_id, login_at as seen_at, the score of tree times the decimal 0.5 as score, the
/// share of tree divided by the integer 2 as share and employee_name null if equal to the
/// string Hank as label.
/// </summary>
public sealed record WidenedSeedColumnsOfARecursionQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithoutPagination(
            subqueries:
            [
                new SubqueryRecursive(
                    "tree",
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
                                                    new UsersTable().Name,
                                                ]
                                            ).TextValue,
                                            new UserIdColumn().Name.TextValue
                                        )
                                    )
                                ),
                            ],
                            where: new EqualUuidRow(
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
                                                new EmployeesTable().Name,
                                            ]
                                        ).TextValue,
                                        new EmployeeIdColumn().Name.TextValue
                                    )
                                ),
                                new SelectItemProjection(
                                    "parent_id",
                                    new AsNullableUuidNullableProjection(
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
                                ),
                                new SelectItemProjection(
                                    "seen_at",
                                    new AsNullableDatetimeNullableProjection(
                                        new FieldDatetime(
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new RelationalSchemaWithForeignKeys().Name,
                                                    new UsersTable().Name,
                                                ]
                                            ).TextValue,
                                            new LastLoginColumn().Name.TextValue
                                        )
                                    )
                                ),
                                new SelectItemProjection(
                                    "score",
                                    new AsDecimalIntegerDecimalNullableProjection(
                                        new RoundWithoutDigitsIntegerNullableProjection(
                                            new FieldDecimalNullable(
                                                new JoinedString(
                                                    new DotString(),
                                                    [
                                                        new RelationalSchemaWithForeignKeys().Name,
                                                        new UsersTable().Name,
                                                    ]
                                                ).TextValue,
                                                new UserScoreColumn().Name.TextValue
                                            )
                                        )
                                    )
                                ),
                                new SelectItemProjection(
                                    "share",
                                    new AsDecimalIntegerDecimalProjection(
                                        new LiteralInteger(1)
                                    )
                                ),
                                new SelectItemProjection(
                                    "label",
                                    new AsNullableStringNullableProjection(
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
                                    "tree",
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
                                        new FieldUuid("tree", "id")
                                    )
                                ),
                                new JoinEntity(
                                    JoinKind.Left,
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new AuditRelationalSchema().Name,
                                            new LoginsTable().Name,
                                        ]
                                    ).TextValue,
                                    new EqualUuidRow(
                                        new FieldUuid(
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new AuditRelationalSchema().Name,
                                                    new LoginsTable().Name,
                                                ]
                                            ).TextValue,
                                            new LoginUserIdColumn().Name.TextValue
                                        ),
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
                                    )
                                ),
                            ],
                            select:
                            [
                                new SelectItemRow(
                                    "id",
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
                                    "parent_id",
                                    new FieldUuid("tree", "id")
                                ),
                                new SelectItemRow(
                                    "seen_at",
                                    new FieldDatetimeNullable(
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
                                new SelectItemRow(
                                    "score",
                                    new MultiplyDecimalNullableRow([
                                        new FieldDecimalNullable("tree", "score"),
                                        new LiteralDecimal(0.5m),
                                    ])
                                ),
                                new SelectItemRow(
                                    "share",
                                    new DivideDecimalRow([
                                        new FieldDecimal("tree", "share"),
                                        new LiteralInteger(2),
                                    ])
                                ),
                                new SelectItemRow(
                                    "label",
                                    new NullIfEqualStringNullableRow(
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
                                        new LiteralString("Hank")
                                    )
                                ),
                            ]
                        ),
                        20
                    )
                ),
            ],
            from: new FromSubquery("tree"),
            select:
            [
                new SelectItemProjection("id", new FieldUuid("tree", "id")),
                new SelectItemProjection(
                    "parent_id",
                    new FieldUuidNullable("tree", "parent_id")
                ),
                new SelectItemProjection(
                    "seen_at",
                    new FieldDatetimeNullable("tree", "seen_at")
                ),
                new SelectItemProjection(
                    "score",
                    new FieldDecimalNullable("tree", "score")
                ),
                new SelectItemProjection("share", new FieldDecimal("tree", "share")),
                new SelectItemProjection(
                    "label",
                    new FieldStringNullable("tree", "label")
                ),
            ],
            orderBy: [new OrderItemProjectionExpression(new FieldUuid("tree", "id"))]
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
                    new IdColumn(),
                    new Column(new String("parent_id"), new UuidColumnType()),
                    new Column(new String("seen_at"), new DateTimeColumnType()),
                    new Column(new String("score"), new DoubleColumnType()),
                    new Column(new String("share"), new DoubleColumnType()),
                    new Column(new String("label"), new StringColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new IdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "000002bd-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("parent_id"), new UuidColumnType()),
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
                                    new String("seen_at"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(1),
                                            new UShort(6),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(8),
                                            new UShort(30),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("score"), new DoubleColumnType()),
                                new InvariantCell(new Double(30.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("share"), new DoubleColumnType()),
                                new InvariantCell(new Double(1.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("label"), new StringColumnType()),
                                new InvariantCell(new String("Grace"))
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
                                new IdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "000002be-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("parent_id"), new UuidColumnType()),
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
                                    new String("seen_at"),
                                    new DateTimeColumnType()
                                ),
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
                                new Column(new String("score"), new DoubleColumnType()),
                                new InvariantCell(new Double(15.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("share"), new DoubleColumnType()),
                                new InvariantCell(new Double(0.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("label"), new StringColumnType()),
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
                                new IdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "000002bf-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("parent_id"), new UuidColumnType()),
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
                                    new String("seen_at"),
                                    new DateTimeColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("score"), new DoubleColumnType()),
                                new InvariantCell(new Double(15.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("share"), new DoubleColumnType()),
                                new InvariantCell(new Double(0.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("label"), new StringColumnType()),
                                new InvariantCell(new String("Iris"))
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
                                new IdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "000002c0-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("parent_id"), new UuidColumnType()),
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
                                    new String("seen_at"),
                                    new DateTimeColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("score"), new DoubleColumnType()),
                                new InvariantCell(new Double(7.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("share"), new DoubleColumnType()),
                                new InvariantCell(new Double(0.25))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("label"), new StringColumnType()),
                                new InvariantCell(new String("Jack"))
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
