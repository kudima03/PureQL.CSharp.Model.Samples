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
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Recursive;

/// <summary>
/// Selects the number of users and of paths at each depth of a graph of links: an inline
/// rows subquery links holds the source_id and target_id of four links between the
/// employees of schema_with_foreign_keys.employees (one employee reaches two, and both
/// reach a third); a recursive subquery reached is seeded with the employee_id of the
/// first employee at depth 0 with 1 path, and its grouped next level reads
/// schema_with_foreign_keys.employees inner joined with links on target_id equal to
/// employee_id and with reached on id equal to source_id, groups by employee_id, and
/// selects it as id, the max of depth plus 1 as depth and the sum of paths as paths, so
/// an employee reached along several links is one row with the number of paths (maxDepth
/// 20). The main query groups reached by depth and selects the depth, the count of users
/// and the sum of paths, ordered by depth.
/// </summary>
public sealed record PathCountsPerDepthQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainGroupedQueryWithoutPagination(
            subqueries:
            [
                new SubqueryRows(
                    "links",
                    [
                        [
                            new RowItem(
                                "source_id",
                                new LiteralUuid(new Guid(701, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "target_id",
                                new LiteralUuid(new Guid(702, 0, 0, new byte[8]))
                            ),
                        ],
                        [
                            new RowItem(
                                "source_id",
                                new LiteralUuid(new Guid(701, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "target_id",
                                new LiteralUuid(new Guid(703, 0, 0, new byte[8]))
                            ),
                        ],
                        [
                            new RowItem(
                                "source_id",
                                new LiteralUuid(new Guid(702, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "target_id",
                                new LiteralUuid(new Guid(704, 0, 0, new byte[8]))
                            ),
                        ],
                        [
                            new RowItem(
                                "source_id",
                                new LiteralUuid(new Guid(703, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "target_id",
                                new LiteralUuid(new Guid(704, 0, 0, new byte[8]))
                            ),
                        ],
                    ]
                ),
                new SubqueryRecursive(
                    "reached",
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
                                new LiteralUuid(new Guid(701, 0, 0, new byte[8]))
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
                                new SelectItemProjection("paths", new LiteralInteger(1)),
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
                                    "links",
                                    new EqualUuidRow(
                                        new FieldUuid("links", "target_id"),
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
                                new JoinSubquery(
                                    JoinKind.Inner,
                                    "reached",
                                    new EqualUuidRow(
                                        new FieldUuid("reached", "id"),
                                        new FieldUuid("links", "source_id")
                                    )
                                ),
                            ],
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
                            ],
                            select:
                            [
                                new SelectItemGroup("id", new KeyUuid("id")),
                                new SelectItemGroup(
                                    "depth",
                                    new AddIntegerGroup([
                                        new MaxIntegerGroup(
                                            new FieldInteger("reached", "depth")
                                        ),
                                        new LiteralInteger(1),
                                    ])
                                ),
                                new SelectItemGroup(
                                    "paths",
                                    new SumIntegerGroup(
                                        new FieldInteger("reached", "paths")
                                    )
                                ),
                            ]
                        ),
                        20
                    )
                ),
            ],
            from: new FromSubquery("reached"),
            groupBy: [new GroupKey("depth", new FieldInteger("reached", "depth"))],
            select:
            [
                new SelectItemGroup("depth", new KeyInteger("depth")),
                new SelectItemGroup("users", new CountGroup()),
                new SelectItemGroup(
                    "paths",
                    new SumIntegerGroup(new FieldInteger("reached", "paths"))
                ),
            ],
            orderBy: [new OrderItemGroupColumn("depth")]
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
                    new Column(new String("depth"), new LongColumnType()),
                    new Column(new String("users"), new LongColumnType()),
                    new Column(new String("paths"), new LongColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("depth"), new LongColumnType()),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("users"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("paths"), new LongColumnType()),
                                new InvariantCell(new Long(1))
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
                                new Column(new String("depth"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("users"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("paths"), new LongColumnType()),
                                new InvariantCell(new Long(2))
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
                                new Column(new String("depth"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("users"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("paths"), new LongColumnType()),
                                new InvariantCell(new Long(2))
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
