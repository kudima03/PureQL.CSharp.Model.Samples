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
using PureQL.CSharp.Model.SelectItems;
using Column = Pure.RelationalSchema.Column.Column;
using Guid = Pure.Primitives.Guid.Guid;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Joins;

/// <summary>
/// Selects login_user_id or else employee_user_id as user_id and employee_name or else
/// none as employee from audit.logins full joined with schema_with_foreign_keys.employees
/// on login_user_id equal to employee_user_id.
/// </summary>
public sealed record FullJoinQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new PureQLQuery(
            new MainPlainQuery(
                new From(
                    new FromEntity(
                        new JoinedString(
                            new DotString(),
                            [new AuditRelationalSchema().Name, new LoginsTable().Name]
                        ).TextValue
                    )
                ),
                [
                    new SelectItemProjection(
                        new SelectItemProjectionNullable(
                            new SelectItemProjectionUuidNullable(
                                "user_id",
                                new UuidNullableProjection(
                                    new ConditionalUuidNullableProjection(
                                        new CoalesceUuidNullableProjection([
                                            new UuidNullableProjection(
                                                new FieldAsUuidNullable(
                                                    new FieldUuidNullable(
                                                        new JoinedString(
                                                            new DotString(),
                                                            [
                                                                new AuditRelationalSchema().Name,
                                                                new LoginsTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new LoginUserIdColumn()
                                                            .Name
                                                            .TextValue
                                                    )
                                                )
                                            ),
                                            new UuidNullableProjection(
                                                new FieldAsUuidNullable(
                                                    new FieldUuidNullable(
                                                        new JoinedString(
                                                            new DotString(),
                                                            [
                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                new EmployeesTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new EmployeeUserIdColumn()
                                                            .Name
                                                            .TextValue
                                                    )
                                                )
                                            ),
                                        ])
                                    )
                                )
                            )
                        )
                    ),
                    new SelectItemProjection(
                        new SelectItemProjectionNonNullable(
                            new SelectItemProjectionString(
                                "employee",
                                new StringProjection(
                                    new ConditionalStringProjection(
                                        new CoalesceStringProjection([
                                            new StringNullableProjection(
                                                new FieldAsStringNullable(
                                                    new FieldStringNullable(
                                                        new JoinedString(
                                                            new DotString(),
                                                            [
                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                new EmployeesTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new EmployeeNameColumn()
                                                            .Name
                                                            .TextValue
                                                    )
                                                )
                                            ),
                                            new StringNullableProjection(
                                                new LiteralAsStringNullable(
                                                    new LiteralString("none")
                                                )
                                            ),
                                        ])
                                    )
                                )
                            )
                        )
                    ),
                ],
                subqueries: null,
                [
                    new Join(
                        new JoinEntity(
                            JoinType.Full,
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new EmployeesTable().Name,
                                ]
                            ).TextValue,
                            new BooleanRow(
                                new ComparisonRow(
                                    new EqualRow(
                                        new EqualUuidRow(
                                            new UuidNullableRow(
                                                new FieldAsUuidNullable(
                                                    new FieldUuid(
                                                        new JoinedString(
                                                            new DotString(),
                                                            [
                                                                new AuditRelationalSchema().Name,
                                                                new LoginsTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new LoginUserIdColumn()
                                                            .Name
                                                            .TextValue
                                                    )
                                                )
                                            ),
                                            new UuidNullableRow(
                                                new FieldAsUuidNullable(
                                                    new FieldUuid(
                                                        new JoinedString(
                                                            new DotString(),
                                                            [
                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                new EmployeesTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new EmployeeUserIdColumn()
                                                            .Name
                                                            .TextValue
                                                    )
                                                )
                                            )
                                        )
                                    )
                                )
                            )
                        )
                    ),
                ],
                where: null,
                orderBy: null,
                pagination: null,
                distinct: false
            )
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and
    /// <see cref="AuditSchemaDataSet"/>, in no particular order.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new UserIdColumn(),
                    new Column(new String("employee"), new StringColumnType()),
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
                                    new String("employee"),
                                    new StringColumnType()
                                ),
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
                                    new String("employee"),
                                    new StringColumnType()
                                ),
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
                                    new String("employee"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Hank"))
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
                                            "00000005-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("employee"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("none"))
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
                                    new String("employee"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Jack"))
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
                                    new String("employee"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Iris"))
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
