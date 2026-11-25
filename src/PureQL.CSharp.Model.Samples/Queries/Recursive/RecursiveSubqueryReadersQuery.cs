using Pure.Collections.Generic;
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
using PureQL.CSharp.Model.GroupExpressions;
using PureQL.CSharp.Model.Keys;
using PureQL.CSharp.Model.Lists;
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.ProjectionExpressions;
using PureQL.CSharp.Model.RowExpressions;
using PureQL.CSharp.Model.TotalExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using Guid = Pure.Primitives.Guid.Guid;
using ModelPagination = PureQL.CSharp.Model.Pagination;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Recursive;

/// <summary>
/// Selects the employees on the deepest level of the hierarchy below Grace from a
/// recursive subquery referrals, which starts from the employee 000002bd of
/// schema_with_foreign_keys.employees (employee_id as id, employee_name as first_name,
/// depth 0) and adds every employee whose employee_manager_id is the id of a row, with
/// depth + 1, only from rows whose depth is below 1 (maxDepth 1). Two subqueries read it: levels, grouped by depth with the
/// count as members, and deepest, a total query with the max of depth. The main query
/// reads referrals, inner joins levels on depth and schema_with_foreign_keys.employees on
/// employee_id equal to id, and keeps the rows whose depth is in the column depth of
/// deepest. It selects id, first_name, employee_shift_start as shift_start, members as
/// members_at_this_depth, the rowNumber over a partition by depth ordered by first_name
/// and id as position_in_level, and 1 divided by the count of all rows as share_of_tree,
/// ordered by first_name and id, with distinctBy id and a first page of 100 rows.
/// </summary>
public sealed record RecursiveSubqueryReadersQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithPagination(
            subqueries:
            [
                new SubqueryRecursive(
                    "referrals",
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
                                new SelectItemProjection(
                                    "first_name",
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
                                new SelectItemProjection("depth", new LiteralInteger(0)),
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
                                    "referrals",
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
                                        new FieldUuid("referrals", "id")
                                    )
                                ),
                            ],
                            where: new LessThanDecimalRow(
                                new FieldInteger("referrals", "depth"),
                                new LiteralInteger(1)
                            ),
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
                                    "first_name",
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
                                new SelectItemRow(
                                    "depth",
                                    new AddIntegerRow([
                                        new FieldInteger("referrals", "depth"),
                                        new LiteralInteger(1),
                                    ])
                                ),
                            ]
                        ),
                        1
                    )
                ),
                new SubqueryQuery(
                    "levels",
                    new GroupedQueryWithoutPaginationWithoutOrderBy(
                        from: new FromSubquery("referrals"),
                        groupBy:
                        [
                            new GroupKey("depth", new FieldInteger("referrals", "depth")),
                        ],
                        select:
                        [
                            new SelectItemGroup("depth", new KeyInteger("depth")),
                            new SelectItemGroup("members", new CountGroup()),
                        ]
                    )
                ),
                new SubqueryQuery(
                    "deepest",
                    new TotalQuery(
                        from: new FromSubquery("referrals"),
                        aggregate:
                        [
                            new SelectItemTotal(
                                "depth",
                                new MaxIntegerNullableTotal(
                                    new FieldInteger("referrals", "depth")
                                )
                            ),
                        ]
                    )
                ),
            ],
            from: new FromSubquery("referrals"),
            joins:
            [
                new JoinSubquery(
                    JoinKind.Inner,
                    "levels",
                    new EqualDecimalRow(
                        new FieldInteger("levels", "depth"),
                        new FieldInteger("referrals", "depth")
                    )
                ),
                new JoinEntity(
                    JoinKind.Inner,
                    new JoinedString(
                        new DotString(),
                        [
                            new RelationalSchemaWithForeignKeys().Name,
                            new EmployeesTable().Name,
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
                            new EmployeeIdColumn().Name.TextValue
                        ),
                        new FieldUuid("referrals", "id")
                    )
                ),
            ],
            where: new InDecimalRow(
                new FieldInteger("referrals", "depth"),
                new ListSubqueryColumnIntegerNullable("deepest", "depth")
            ),
            select:
            [
                new SelectItemProjection("id", new FieldUuid("referrals", "id")),
                new SelectItemProjection(
                    "first_name",
                    new FieldString("referrals", "first_name")
                ),
                new SelectItemProjection(
                    "shift_start",
                    new FieldTime(
                        new JoinedString(
                            new DotString(),
                            [
                                new RelationalSchemaWithForeignKeys().Name,
                                new EmployeesTable().Name,
                            ]
                        ).TextValue,
                        new EmployeeShiftStartColumn().Name.TextValue
                    )
                ),
                new SelectItemProjection(
                    "members_at_this_depth",
                    new FieldInteger("levels", "members")
                ),
                new SelectItemProjection(
                    "position_in_level",
                    new RowNumberProjection(
                        new OrderedWindow(
                            [
                                new OrderItemRow(
                                    new FieldString("referrals", "first_name")
                                ),
                                new OrderItemRow(new FieldUuid("referrals", "id")),
                            ],
                            partitionBy: [new FieldInteger("referrals", "depth")]
                        )
                    )
                ),
                new SelectItemProjection(
                    "share_of_tree",
                    new DivideDecimalProjection([
                        new LiteralInteger(1),
                        new CountProjection(),
                    ])
                ),
            ],
            distinctBy: ["id"],
            orderBy:
            [
                new OrderItemProjectionExpression(
                    new FieldString("referrals", "first_name")
                ),
                new OrderItemProjectionExpression(new FieldUuid("referrals", "id")),
            ],
            pagination: new ModelPagination(take: 100)
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over <see
    /// cref="SchemaDataSetWithForeignKeys"/> and <see cref="AuditSchemaDataSet"/>, in
    /// this order.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("id"), new UuidColumnType()),
                    new Column(new String("first_name"), new StringColumnType()),
                    new Column(new String("shift_start"), new TimeColumnType()),
                    new Column(new String("members_at_this_depth"), new LongColumnType()),
                    new Column(new String("position_in_level"), new LongColumnType()),
                    new Column(new String("share_of_tree"), new DoubleColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("id"), new UuidColumnType()),
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
                                    new String("first_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Hank"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("shift_start"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(
                                    new Time(new UShort(10), new UShort(0), new UShort(0))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("members_at_this_depth"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_in_level"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("share_of_tree"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.5))
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
                                new Column(new String("id"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "000002bf-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Iris"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("shift_start"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(
                                    new Time(
                                        new UShort(11),
                                        new UShort(30),
                                        new UShort(0)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("members_at_this_depth"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_in_level"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("share_of_tree"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.5))
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
