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
/// Selects user_name, its first 2 code points, the rest of it from the second code point,
/// and the first 3 code points of the employee_name of the employee the user is, nullable
/// after the left join, from schema_with_foreign_keys.users left joined to
/// schema_with_foreign_keys.employees on employee_user_id. The where keeps the users
/// whose user_tenant_id as text starts with 00000386, ordered by user_name.
/// </summary>
public sealed record SubstringQuery
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
            where: new EqualStringRow(
                new SubstringStringRow(
                    new AsStringUuidStringRow(
                        new FieldUuid(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue,
                            new UserTenantIdColumn().Name.TextValue
                        )
                    ),
                    new LiteralInteger(1),
                    new LiteralInteger(8)
                ),
                new LiteralString("00000386")
            ),
            select:
            [
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
                new SelectItemProjection(
                    "name_prefix",
                    new SubstringStringProjection(
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
                        new LiteralInteger(1),
                        new LiteralInteger(2)
                    )
                ),
                new SelectItemProjection(
                    "name_rest",
                    new SubstringStringProjection(
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
                        new LiteralInteger(2)
                    )
                ),
                new SelectItemProjection(
                    "employee_name_preview",
                    new SubstringStringNullableProjection(
                        new FieldStringNullable(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new EmployeesTable().Name,
                                ]
                            ).TextValue,
                            new EmployeeNameColumn().Name.TextValue
                        ),
                        new LiteralInteger(1),
                        new LiteralInteger(3)
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
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new UserNameColumn(),
                    new Column(new String("name_prefix"), new StringColumnType()),
                    new Column(new String("name_rest"), new StringColumnType()),
                    new Column(
                        new String("employee_name_preview"),
                        new StringColumnType()
                    ),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new UserNameColumn(),
                                new InvariantCell(new String("Dan"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("name_prefix"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Da"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("name_rest"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("an"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("employee_name_preview"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Jac"))
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
                                new UserNameColumn(),
                                new InvariantCell(new String("Eve"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("name_prefix"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Ev"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("name_rest"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("ve"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("employee_name_preview"),
                                    new StringColumnType()
                                ),
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
                                new UserNameColumn(),
                                new InvariantCell(new String("Fay"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("name_prefix"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Fa"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("name_rest"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("ay"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("employee_name_preview"),
                                    new StringColumnType()
                                ),
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
