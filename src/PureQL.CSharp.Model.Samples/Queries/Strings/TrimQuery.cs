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
/// Selects employee_name trimmed of spaces, the employee_name of the manager, nullable
/// after the left join, trimmed of spaces and tabs, the text of employee_id without its
/// leading zeros and with the trailing hyphens and zeros trimmed from its end, and the
/// text of employee_shift_start with every trailing :00 trimmed from
/// schema_with_foreign_keys.employees e left joined to schema_with_foreign_keys.employees
/// m on the manager's employee_id. The where skips names that are blank once trimmed,
/// ordered by employee_name.
/// </summary>
public sealed record TrimQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithoutPagination(
            from: new FromEntity(
                new JoinedString(
                    new DotString(),
                    [
                        new RelationalSchemaWithForeignKeys().Name,
                        new EmployeesTable().Name,
                    ]
                ).TextValue,
                "e"
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
                new TrimStringRow(
                    new FieldString("e", new EmployeeNameColumn().Name.TextValue),
                    [new LiteralString(" ")]
                ),
                new LiteralString("")
            ),
            select:
            [
                new SelectItemProjection(
                    "employee_name",
                    new TrimStringProjection(
                        new FieldString("e", new EmployeeNameColumn().Name.TextValue),
                        [new LiteralString(" ")]
                    )
                ),
                new SelectItemProjection(
                    "manager_name",
                    new TrimStringNullableProjection(
                        new FieldStringNullable(
                            "m",
                            new EmployeeNameColumn().Name.TextValue
                        ),
                        [new LiteralString(" "), new LiteralString("\t")]
                    )
                ),
                new SelectItemProjection(
                    "id_without_leading_zeros",
                    new TrimStartStringProjection(
                        new AsStringUuidStringProjection(
                            new FieldUuid("e", new EmployeeIdColumn().Name.TextValue)
                        ),
                        [new LiteralString("0")]
                    )
                ),
                new SelectItemProjection(
                    "id_without_trailing_zeros",
                    new TrimEndStringProjection(
                        new AsStringUuidStringProjection(
                            new FieldUuid("e", new EmployeeIdColumn().Name.TextValue)
                        ),
                        [new LiteralString("-"), new LiteralString("0")]
                    )
                ),
                new SelectItemProjection(
                    "shift_start_short",
                    new TrimEndStringProjection(
                        new AsStringTimeStringProjection(
                            new FieldTime(
                                "e",
                                new EmployeeShiftStartColumn().Name.TextValue
                            )
                        ),
                        [new LiteralString(":00")]
                    )
                ),
            ],
            orderBy:
            [
                new OrderItemProjectionExpression(
                    new FieldString("e", new EmployeeNameColumn().Name.TextValue)
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
                    new EmployeeNameColumn(),
                    new Column(new String("manager_name"), new StringColumnType()),
                    new Column(
                        new String("id_without_leading_zeros"),
                        new StringColumnType()
                    ),
                    new Column(
                        new String("id_without_trailing_zeros"),
                        new StringColumnType()
                    ),
                    new Column(new String("shift_start_short"), new StringColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new EmployeeNameColumn(),
                                new InvariantCell(new String("Grace"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("manager_name"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("id_without_leading_zeros"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(
                                    new String("2bd-0000-0000-0000-000000000000")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("id_without_trailing_zeros"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("000002bd"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("shift_start_short"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("09"))
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
                                new EmployeeNameColumn(),
                                new InvariantCell(new String("Hank"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("manager_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Grace"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("id_without_leading_zeros"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(
                                    new String("2be-0000-0000-0000-000000000000")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("id_without_trailing_zeros"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("000002be"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("shift_start_short"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("10"))
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
                                new EmployeeNameColumn(),
                                new InvariantCell(new String("Iris"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("manager_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Grace"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("id_without_leading_zeros"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(
                                    new String("2bf-0000-0000-0000-000000000000")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("id_without_trailing_zeros"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("000002bf"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("shift_start_short"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("11:30"))
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
                                new EmployeeNameColumn(),
                                new InvariantCell(new String("Jack"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("manager_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Hank"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("id_without_leading_zeros"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(
                                    new String("2c0-0000-0000-0000-000000000000")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("id_without_trailing_zeros"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("000002c"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("shift_start_short"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("08"))
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
