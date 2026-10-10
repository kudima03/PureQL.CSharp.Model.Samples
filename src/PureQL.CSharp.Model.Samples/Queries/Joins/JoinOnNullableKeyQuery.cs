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
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Joins;

/// <summary>
/// Selects a.employee_name as employee and b.employee_name as colleague from
/// schema_with_foreign_keys.employees aliased a inner joined with
/// schema_with_foreign_keys.employees aliased b on equal employee_manager_id,
/// a.employee_manager_id not equal to a typed null uuid and a different employee_id.
/// </summary>
public sealed record JoinOnNullableKeyQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new PureQLQuery(
            new MainPlainQuery(
                new From(
                    new FromEntity(
                        new JoinedString(
                            new DotString(),
                            [
                                new RelationalSchemaWithForeignKeys().Name,
                                new EmployeesTable().Name,
                            ]
                        ).TextValue,
                        "a"
                    )
                ),
                [
                    new SelectItemProjection(
                        new SelectItemProjectionNonNullable(
                            new SelectItemProjectionString(
                                "employee",
                                new StringProjection(
                                    new FieldString(
                                        "a",
                                        new EmployeeNameColumn().Name.TextValue
                                    )
                                )
                            )
                        )
                    ),
                    new SelectItemProjection(
                        new SelectItemProjectionNonNullable(
                            new SelectItemProjectionString(
                                "colleague",
                                new StringProjection(
                                    new FieldString(
                                        "b",
                                        new EmployeeNameColumn().Name.TextValue
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
                            JoinType.Inner,
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new EmployeesTable().Name,
                                ]
                            ).TextValue,
                            new BooleanRow(
                                new LogicalRow(
                                    new AndRow([
                                        new BooleanRow(
                                            new ComparisonRow(
                                                new EqualRow(
                                                    new EqualUuidRow(
                                                        new UuidNullableRow(
                                                            new FieldAsUuidNullable(
                                                                new FieldUuidNullable(
                                                                    "a",
                                                                    new EmployeeManagerIdColumn()
                                                                        .Name
                                                                        .TextValue
                                                                )
                                                            )
                                                        ),
                                                        new UuidNullableRow(
                                                            new FieldAsUuidNullable(
                                                                new FieldUuidNullable(
                                                                    "b",
                                                                    new EmployeeManagerIdColumn()
                                                                        .Name
                                                                        .TextValue
                                                                )
                                                            )
                                                        )
                                                    )
                                                )
                                            )
                                        ),
                                        new BooleanRow(
                                            new ComparisonRow(
                                                new NotEqualRow(
                                                    new NotEqualUuidRow(
                                                        new UuidNullableRow(
                                                            new FieldAsUuidNullable(
                                                                new FieldUuidNullable(
                                                                    "a",
                                                                    new EmployeeManagerIdColumn()
                                                                        .Name
                                                                        .TextValue
                                                                )
                                                            )
                                                        ),
                                                        new UuidNullableRow(
                                                            new LiteralAsUuidNullable(
                                                                new LiteralUuidNullable()
                                                            )
                                                        )
                                                    )
                                                )
                                            )
                                        ),
                                        new BooleanRow(
                                            new ComparisonRow(
                                                new NotEqualRow(
                                                    new NotEqualUuidRow(
                                                        new UuidNullableRow(
                                                            new FieldAsUuidNullable(
                                                                new FieldUuid(
                                                                    "a",
                                                                    new EmployeeIdColumn()
                                                                        .Name
                                                                        .TextValue
                                                                )
                                                            )
                                                        ),
                                                        new UuidNullableRow(
                                                            new FieldAsUuidNullable(
                                                                new FieldUuid(
                                                                    "b",
                                                                    new EmployeeIdColumn()
                                                                        .Name
                                                                        .TextValue
                                                                )
                                                            )
                                                        )
                                                    )
                                                )
                                            )
                                        ),
                                    ])
                                )
                            ),
                            "b"
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
                    new Column(new String("employee"), new StringColumnType()),
                    new Column(new String("colleague"), new StringColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("employee"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Hank"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("colleague"),
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
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("employee"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Iris"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("colleague"),
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
            ]
        );
}
