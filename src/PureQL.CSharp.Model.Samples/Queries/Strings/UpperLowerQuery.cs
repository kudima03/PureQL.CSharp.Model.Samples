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
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Strings;

/// <summary>
/// Selects user_name in upper case, the employee_name of the employee the user is in
/// lower case, nullable after the left join, the greeting Gruesse (with a u umlaut and a
/// sharp s) followed by user_name in upper case, and user_name from
/// schema_with_foreign_keys.users left joined to schema_with_foreign_keys.employees on
/// employee_user_id. The where keeps the names at or after CARA when both sides are in
/// lower case, ordered by user_name.
/// </summary>
public sealed record UpperLowerQuery
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
            where: new GreaterThanOrEqualStringRow(
                new LowerStringRow(
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
                ),
                new LowerStringRow(new LiteralString("CARA"))
            ),
            select:
            [
                new SelectItemProjection(
                    "user_name_upper",
                    new UpperStringProjection(
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
                new SelectItemProjection(
                    "employee_name_lower",
                    new LowerStringNullableProjection(
                        new FieldStringNullable(
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
                new SelectItemProjection(
                    "greeting_upper",
                    new UpperStringProjection(
                        new ConcatStringProjection([
                            new LiteralString("Gr\u00FC\u00DFe, "),
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
                        ])
                    )
                ),
                new SelectItemProjection(
                    "user_name",
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
                ),
            ],
            orderBy:
            [
                new OrderItemProjectionExpression(
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
                ),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and
    /// <see cref="AuditSchemaDataSet"/>, in this order.
    /// upper of the greeting uses the full case mappings (the sharp s becomes SS),
    /// computed with Python str.upper, which PostgreSQL does not apply.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("user_name_upper"), new StringColumnType()),
                    new Column(new String("employee_name_lower"), new StringColumnType()),
                    new Column(new String("greeting_upper"), new StringColumnType()),
                    new UserNameColumn(),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("user_name_upper"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("CARA"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("employee_name_lower"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("iris"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greeting_upper"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("GR\u00DCSSE, CARA"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new UserNameColumn(),
                                new InvariantCell(new String("Cara"))
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
                                    new String("user_name_upper"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("DAN"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("employee_name_lower"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("jack"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greeting_upper"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("GR\u00DCSSE, DAN"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new UserNameColumn(),
                                new InvariantCell(new String("Dan"))
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
                                    new String("user_name_upper"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("EVE"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("employee_name_lower"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greeting_upper"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("GR\u00DCSSE, EVE"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new UserNameColumn(),
                                new InvariantCell(new String("Eve"))
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
                                    new String("user_name_upper"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("FAY"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("employee_name_lower"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greeting_upper"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("GR\u00DCSSE, FAY"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new UserNameColumn(),
                                new InvariantCell(new String("Fay"))
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
