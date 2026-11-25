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
using Guid = Pure.Primitives.Guid.Guid;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Strings;

/// <summary>
/// Selects user_id, user_name joined with the employee_name of the employee the user is
/// and of that employee's manager by spaces, skipping the ones that are null, the two
/// employee names joined by a slash, the text of the nullable user_score padded with
/// zeros to 3 code points, user_name padded with dots to 8, a star for every whole ten
/// years of user_age, and user_name reversed from schema_with_foreign_keys.users left
/// joined to schema_with_foreign_keys.employees e on employee_user_id and to
/// schema_with_foreign_keys.employees m on the manager's employee_id. The where keeps the
/// users with an employee name, ordered by user_id.
/// </summary>
public sealed record PaddingAndJoiningQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithoutPagination(
            from: new FromEntity(
                new JoinedString(
                    new DotString(),
                    [new RelationalSchemaWithForeignKeys().Name, new UsersTable().Name]
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
                        new FieldUuidNullable(
                            "e",
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
                    ),
                    "e"
                ),
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
                        new FieldUuidNullable("m", new EmployeeIdColumn().Name.TextValue),
                        new FieldUuidNullable(
                            "e",
                            new EmployeeManagerIdColumn().Name.TextValue
                        )
                    ),
                    "m"
                ),
            ],
            where: new NotEqualStringRow(
                new ConcatWithRow(
                    [
                        new FieldStringNullable(
                            "e",
                            new EmployeeNameColumn().Name.TextValue
                        ),
                        new FieldStringNullable(
                            "m",
                            new EmployeeNameColumn().Name.TextValue
                        ),
                    ],
                    new LiteralString(" / ")
                ),
                new LiteralString("")
            ),
            select:
            [
                new SelectItemProjection(
                    "user_id",
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
                new SelectItemProjection(
                    "full_name",
                    new ConcatWithProjection(
                        [
                            new FieldString(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new UsersTable().Name,
                                    ]
                                ).TextValue,
                                new UserNameColumn().Name.TextValue
                            ),
                            new FieldStringNullable(
                                "e",
                                new EmployeeNameColumn().Name.TextValue
                            ),
                            new FieldStringNullable(
                                "m",
                                new EmployeeNameColumn().Name.TextValue
                            ),
                        ],
                        new LiteralString(" ")
                    )
                ),
                new SelectItemProjection(
                    "reporting_line",
                    new ConcatWithProjection(
                        [
                            new FieldStringNullable(
                                "e",
                                new EmployeeNameColumn().Name.TextValue
                            ),
                            new FieldStringNullable(
                                "m",
                                new EmployeeNameColumn().Name.TextValue
                            ),
                        ],
                        new LiteralString(" / ")
                    )
                ),
                new SelectItemProjection(
                    "score_in_three_digits",
                    new PadStartStringNullableProjection(
                        new AsStringDecimalStringNullableProjection(
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
                        ),
                        new LiteralInteger(3),
                        new LiteralString("0")
                    )
                ),
                new SelectItemProjection(
                    "name_column",
                    new PadEndStringProjection(
                        new FieldString(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue,
                            new UserNameColumn().Name.TextValue
                        ),
                        new LiteralInteger(8),
                        new LiteralString(".")
                    )
                ),
                new SelectItemProjection(
                    "age_stars",
                    new RepeatStringProjection(
                        new LiteralString("*"),
                        new FloorIntegerProjection(
                            new DivideDecimalProjection([
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
                                new LiteralInteger(10),
                            ])
                        )
                    )
                ),
                new SelectItemProjection(
                    "reversed_name",
                    new ReverseStringProjection(
                        new FieldString(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue,
                            new UserNameColumn().Name.TextValue
                        )
                    )
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
                                new UsersTable().Name,
                            ]
                        ).TextValue,
                        new UserIdColumn().Name.TextValue
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
                    new UserIdColumn(),
                    new Column(new String("full_name"), new StringColumnType()),
                    new Column(new String("reporting_line"), new StringColumnType()),
                    new Column(
                        new String("score_in_three_digits"),
                        new StringColumnType()
                    ),
                    new Column(new String("name_column"), new StringColumnType()),
                    new Column(new String("age_stars"), new StringColumnType()),
                    new Column(new String("reversed_name"), new StringColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new UserIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000001-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("full_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Ann Grace"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("reporting_line"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Grace"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("score_in_three_digits"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("030"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("name_column"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Ann....."))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("age_stars"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("***"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("reversed_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("nnA"))
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
                                new UserIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000002-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("full_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Bob Hank Grace"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("reporting_line"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Hank / Grace"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("score_in_three_digits"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("name_column"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Bob....."))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("age_stars"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("**"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("reversed_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("boB"))
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
                                new UserIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000003-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("full_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Cara Iris Grace"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("reporting_line"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Iris / Grace"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("score_in_three_digits"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("030"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("name_column"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Cara...."))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("age_stars"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("***"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("reversed_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("araC"))
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
                                new UserIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000004-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("full_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Dan Jack Hank"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("reporting_line"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Jack / Hank"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("score_in_three_digits"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("name_column"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Dan....."))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("age_stars"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("****"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("reversed_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("naD"))
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
