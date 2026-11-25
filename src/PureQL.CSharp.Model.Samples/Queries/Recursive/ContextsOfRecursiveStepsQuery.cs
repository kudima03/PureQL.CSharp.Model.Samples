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
using PureQL.CSharp.Model.GroupExpressions;
using PureQL.CSharp.Model.Keys;
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.RowExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using Guid = Pure.Primitives.Guid.Guid;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Recursive;

/// <summary>
/// Selects from two recursive subqueries over schema_with_foreign_keys.employees, both
/// seeded with employee #701 (Grace) at depth 0 with the employee_name as label. chain
/// walks down the employee_manager_id: its next level joins the employees whose manager
/// is a row of the level before and whose lowercased name differs from that row's
/// lowercased label, keeps depth below 10 and the employees whose address (the name and
/// @example.org) contains k@, at depth + 1 and the label joined by > with concatWith
/// over the label before, a middle name that is Mid for an employee whose
/// employee_shift_start is after 09:00:00 and null otherwise, and the name, with a
/// maxDepth of 50. reach is a grouped next level: it joins the employees to the row of
/// the level before at depth below 1, groups them by employee_id and the uppercased
/// name, keeps the groups with a count above 0 or the name ROOT, and selects the key,
/// the least depth of the group plus 1 and the label of the key name, of and the count
/// over all the rows of the step as a string, with a maxDepth of 1. The main query
/// selects id, depth and label from chain left joined to reach on id, with the label of
/// reach as the nullable reach_label, ordered by depth, label and id.
/// </summary>
public sealed record ContextsOfRecursiveStepsQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithoutPagination(
            subqueries:
            [
                new SubqueryRecursive(
                    "chain",
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
                            where: new EqualUuidRow(
                                new FieldUuid(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new EmployeesTable().Name,
                                        ]
                                    ).TextValue,
                                    new EmployeeIdColumn().Name.TextValue
                                ),
                                new LiteralUuid(new System.Guid(701, 0, 0, new byte[8]))
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
                                new SelectItemProjection("depth", new LiteralInteger(0)),
                                new SelectItemProjection(
                                    "label",
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
                                    "chain",
                                    new AndRow([
                                        new EqualUuidRow(
                                            new FieldUuidNullable(
                                                new JoinedString(
                                                    new DotString(),
                                                    [
                                                        new RelationalSchemaWithForeignKeys().Name,
                                                        new EmployeesTable().Name,
                                                    ]
                                                ).TextValue,
                                                new EmployeeManagerIdColumn()
                                                    .Name
                                                    .TextValue
                                            ),
                                            new FieldUuid("chain", "id")
                                        ),
                                        new NotEqualStringRow(
                                            new FoldCaseStringRow(
                                                new FieldString(
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
                                            ),
                                            new FoldCaseStringRow(
                                                new FieldString("chain", "label")
                                            )
                                        ),
                                    ])
                                ),
                            ],
                            where: new AndRow([
                                new LessThanDecimalRow(
                                    new FieldInteger("chain", "depth"),
                                    new LiteralInteger(10)
                                ),
                                new ContainsRow(
                                    new ConcatStringRow([
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
                                        new LiteralString("@example.org"),
                                    ]),
                                    new LiteralString("k@")
                                ),
                            ]),
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
                                    "depth",
                                    new AddIntegerRow([
                                        new FieldInteger("chain", "depth"),
                                        new LiteralInteger(1),
                                    ])
                                ),
                                new SelectItemRow(
                                    "label",
                                    new ConcatWithRow(
                                        [
                                            new FieldString("chain", "label"),
                                            new IfStringNullableRow(
                                                new LiteralString("Mid"),
                                                new LiteralStringNullable(),
                                                new GreaterThanTimeRow(
                                                    new FieldTime(
                                                        new JoinedString(
                                                            new DotString(),
                                                            [
                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                new EmployeesTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new EmployeeShiftStartColumn()
                                                            .Name
                                                            .TextValue
                                                    ),
                                                    new LiteralTime(new TimeOnly(9, 0, 0))
                                                )
                                            ),
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
                                        ],
                                        new LiteralString(" > ")
                                    )
                                ),
                            ]
                        ),
                        50
                    )
                ),
                new SubqueryRecursive(
                    "reach",
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
                            where: new EqualUuidRow(
                                new FieldUuid(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new EmployeesTable().Name,
                                        ]
                                    ).TextValue,
                                    new EmployeeIdColumn().Name.TextValue
                                ),
                                new LiteralUuid(new System.Guid(701, 0, 0, new byte[8]))
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
                                new SelectItemProjection("depth", new LiteralInteger(0)),
                                new SelectItemProjection(
                                    "label",
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
                                ),
                            ]
                        ),
                        new GroupedNextQuery(
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
                                    "reach",
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
                                        new FieldUuid("reach", "id")
                                    )
                                ),
                            ],
                            where: new LessThanDecimalRow(
                                new FieldInteger("reach", "depth"),
                                new LiteralInteger(1)
                            ),
                            groupBy:
                            [
                                new GroupKey(
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
                                new GroupKey(
                                    "name",
                                    new UpperStringRow(
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
                            ],
                            having: new OrGroup([
                                new GreaterThanDecimalGroup(
                                    new CountGroup(),
                                    new LiteralInteger(0)
                                ),
                                new EqualStringGroup(
                                    new KeyString("name"),
                                    new LiteralString("ROOT")
                                ),
                            ]),
                            select:
                            [
                                new SelectItemGroup("id", new KeyUuid("id")),
                                new SelectItemGroup(
                                    "depth",
                                    new AddIntegerGroup([
                                        new MinIntegerGroup(
                                            new FieldInteger("reach", "depth")
                                        ),
                                        new LiteralInteger(1),
                                    ])
                                ),
                                new SelectItemGroup(
                                    "label",
                                    new ConcatStringGroup([
                                        new KeyString("name"),
                                        new LiteralString(" of "),
                                        new AsStringDecimalStringGroup(
                                            new CountGroup(over: AggregateOver.All)
                                        ),
                                    ])
                                ),
                            ]
                        ),
                        1
                    )
                ),
            ],
            from: new FromSubquery("chain"),
            joins:
            [
                new JoinSubquery(
                    JoinKind.Left,
                    "reach",
                    new EqualUuidRow(
                        new FieldUuid("chain", "id"),
                        new FieldUuid("reach", "id")
                    )
                ),
            ],
            select:
            [
                new SelectItemProjection("id", new FieldUuid("chain", "id")),
                new SelectItemProjection("depth", new FieldInteger("chain", "depth")),
                new SelectItemProjection("label", new FieldString("chain", "label")),
                new SelectItemProjection(
                    "reach_label",
                    new FieldStringNullable("reach", "label")
                ),
            ],
            orderBy:
            [
                new OrderItemProjectionExpression(new FieldInteger("chain", "depth")),
                new OrderItemProjectionExpression(new FieldString("chain", "label")),
                new OrderItemProjectionExpression(new FieldUuid("chain", "id")),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over <see
    /// cref="SchemaDataSetWithForeignKeys"/> and <see cref="AuditSchemaDataSet"/>, in
    /// this order. The grouped step of reach is computed level by level.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new IdColumn(),
                    new Column(new String("depth"), new LongColumnType()),
                    new Column(new String("label"), new StringColumnType()),
                    new Column(new String("reach_label"), new StringColumnType()),
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
                                new Column(new String("depth"), new LongColumnType()),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("label"), new StringColumnType()),
                                new InvariantCell(new String("Grace"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("reach_label"),
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
                                new Column(new String("depth"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("label"), new StringColumnType()),
                                new InvariantCell(new String("Grace > Mid > Hank"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("reach_label"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("HANK of 2"))
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
                                new Column(new String("depth"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("label"), new StringColumnType()),
                                new InvariantCell(new String("Grace > Mid > Hank > Jack"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("reach_label"),
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
