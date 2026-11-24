using Pure.Collections.Generic;
using Pure.Primitives.Number;
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
/// Selects employee_name, the first and the last part of the text of employee_shift_start
/// split at the colons, the position of the first hyphen in the text of employee_id and
/// of the next hyphen searched from just after it, employee_name masked after its first
/// code point up to its first a, and the employee_name of the manager, nullable after the
/// left join, with every a and e replaced by nothing from
/// schema_with_foreign_keys.employees e left joined to schema_with_foreign_keys.employees
/// m on the manager's employee_id. The where keeps the names with an a after their first
/// code point, ordered by employee_name.
/// </summary>
public sealed record ReplacePositionSplitPartQuery
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
            where: new GreaterThanDecimalRow(
                new PositionIntegerRow(
                    new FieldString("e", new EmployeeNameColumn().Name.TextValue),
                    new LiteralString("a")
                ),
                new LiteralInteger(1)
            ),
            select:
            [
                new SelectItemProjection(
                    "employee_name",
                    new FieldString("e", new EmployeeNameColumn().Name.TextValue)
                ),
                new SelectItemProjection(
                    "shift_hour",
                    new SplitPartStringProjection(
                        new AsStringTimeStringProjection(
                            new FieldTime(
                                "e",
                                new EmployeeShiftStartColumn().Name.TextValue
                            )
                        ),
                        new LiteralString(":"),
                        new LiteralInteger(1)
                    )
                ),
                new SelectItemProjection(
                    "shift_second",
                    new SplitPartStringProjection(
                        new AsStringTimeStringProjection(
                            new FieldTime(
                                "e",
                                new EmployeeShiftStartColumn().Name.TextValue
                            )
                        ),
                        new LiteralString(":"),
                        new LiteralInteger(-1)
                    )
                ),
                new SelectItemProjection(
                    "first_hyphen_in_id",
                    new PositionIntegerProjection(
                        new AsStringUuidStringProjection(
                            new FieldUuid("e", new EmployeeIdColumn().Name.TextValue)
                        ),
                        new LiteralString("-")
                    )
                ),
                new SelectItemProjection(
                    "second_hyphen_in_id",
                    new PositionIntegerProjection(
                        new AsStringUuidStringProjection(
                            new FieldUuid("e", new EmployeeIdColumn().Name.TextValue)
                        ),
                        new LiteralString("-"),
                        new AddIntegerProjection([
                            new PositionIntegerProjection(
                                new AsStringUuidStringProjection(
                                    new FieldUuid(
                                        "e",
                                        new EmployeeIdColumn().Name.TextValue
                                    )
                                ),
                                new LiteralString("-")
                            ),
                            new LiteralInteger(1),
                        ])
                    )
                ),
                new SelectItemProjection(
                    "masked_name",
                    new ConcatStringProjection([
                        new SubstringStringProjection(
                            new FieldString("e", new EmployeeNameColumn().Name.TextValue),
                            new LiteralInteger(1),
                            new LiteralInteger(1)
                        ),
                        new LiteralString("***"),
                        new SubstringStringProjection(
                            new FieldString("e", new EmployeeNameColumn().Name.TextValue),
                            new PositionIntegerProjection(
                                new FieldString(
                                    "e",
                                    new EmployeeNameColumn().Name.TextValue
                                ),
                                new LiteralString("a")
                            )
                        ),
                    ])
                ),
                new SelectItemProjection(
                    "manager_name_without_a_and_e",
                    new ReplaceStringNullableProjection(
                        new ReplaceStringNullableProjection(
                            new FieldStringNullable(
                                "m",
                                new EmployeeNameColumn().Name.TextValue
                            ),
                            new LiteralString("a"),
                            new LiteralString("")
                        ),
                        new LiteralString("e"),
                        new LiteralString("")
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
                    new Column(new String("shift_hour"), new StringColumnType()),
                    new Column(new String("shift_second"), new StringColumnType()),
                    new Column(new String("first_hyphen_in_id"), new LongColumnType()),
                    new Column(new String("second_hyphen_in_id"), new LongColumnType()),
                    new Column(new String("masked_name"), new StringColumnType()),
                    new Column(
                        new String("manager_name_without_a_and_e"),
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
                                new EmployeeNameColumn(),
                                new InvariantCell(new String("Grace"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("shift_hour"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("09"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("shift_second"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("00"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_hyphen_in_id"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(9))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("second_hyphen_in_id"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(14))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("masked_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("G***ace"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("manager_name_without_a_and_e"),
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
                                new EmployeeNameColumn(),
                                new InvariantCell(new String("Hank"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("shift_hour"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("10"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("shift_second"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("00"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_hyphen_in_id"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(9))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("second_hyphen_in_id"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(14))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("masked_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("H***ank"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("manager_name_without_a_and_e"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Grc"))
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
                                    new String("shift_hour"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("08"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("shift_second"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("00"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_hyphen_in_id"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(9))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("second_hyphen_in_id"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(14))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("masked_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("J***ack"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("manager_name_without_a_and_e"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Hnk"))
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
