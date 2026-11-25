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
using PureQL.CSharp.Model.Lists;
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.ProjectionExpressions;
using PureQL.CSharp.Model.RowExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using Guid = Pure.Primitives.Guid.Guid;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Recursive;

/// <summary>
/// Selects the rows of a recursive subquery tree read twice, as below and as above, with
/// three subqueries. buyers groups the shipped schema_with_foreign_keys.orders by
/// order_user_id. tree seeds with the employees without an employee_manager_id, left joined to
/// schema_with_foreign_keys.users as account on user_id equal to employee_user_id and
/// user_active true: id, parent_id, root_id, root_user_id, depth 0, the path as the id as
/// text, whether the employee_name is not "Grace" (false), and shown_as, the user_name of
/// the account or else the employee_name. It then reads tree as parent (maxDepth 4), inner
/// joins employees as child on employee_manager_id equal to the parent id, left joins users
/// as top on user_id equal to the root_user_id and as account as before, and keeps the
/// children whose employee_user_id is in buyers, whose parent has depth under 2 and whose
/// top is active; it selects the child id and manager, the root of the parent, depth + 1,
/// the path extended with "/" and the child id, whether the child name is not "Grace" and
/// shown_as, the employee_name when the account has no user_name and its user_name
/// otherwise. sizes groups tree by root_id and counts. The main query reads tree as below,
/// inner joins tree as above on parent_id equal to its id and sizes on root_id, selects
/// employee_id, shown_as, the shown_as of the parent, depth as levels_below_root, the
/// members of the tree and the path, ordered by path.
/// </summary>
public sealed record RecursiveNextWithJoinsAndAliasesQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithoutPagination(
            subqueries:
            [
                new SubqueryQuery(
                    "buyers",
                    new GroupedQueryWithoutPaginationWithoutOrderBy(
                        from: new FromEntity(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrdersTable().Name,
                                ]
                            ).TextValue
                        ),
                        where: new EqualStringRow(
                            new FieldString(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new OrdersTable().Name,
                                    ]
                                ).TextValue,
                                new OrderStatusColumn().Name.TextValue
                            ),
                            new LiteralString("shipped")
                        ),
                        groupBy:
                        [
                            new GroupKey(
                                "user_id",
                                new FieldUuid(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new OrdersTable().Name,
                                        ]
                                    ).TextValue,
                                    new OrderUserIdColumn().Name.TextValue
                                )
                            ),
                        ],
                        select: [new SelectItemGroup("user_id", new KeyUuid("user_id"))]
                    )
                ),
                new SubqueryRecursive(
                    "tree",
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
                            joins:
                            [
                                new JoinEntity(
                                    JoinKind.Left,
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new UsersTable().Name,
                                        ]
                                    ).TextValue,
                                    new AndRow([
                                        new EqualUuidRow(
                                            new FieldUuid(
                                                "account",
                                                new UserIdColumn().Name.TextValue
                                            ),
                                            new FieldUuid(
                                                new JoinedString(
                                                    new DotString(),
                                                    [
                                                        new RelationalSchemaWithForeignKeys().Name,
                                                        new EmployeesTable().Name,
                                                    ]
                                                ).TextValue,
                                                new EmployeeUserIdColumn().Name.TextValue
                                            )
                                        ),
                                        new EqualBooleanRow(
                                            new FieldBoolean(
                                                "account",
                                                new UserActiveColumn().Name.TextValue
                                            ),
                                            new LiteralBoolean(true)
                                        ),
                                    ]),
                                    alias: "account"
                                ),
                            ],
                            where: new EqualUuidRow(
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
                                new LiteralUuidNullable()
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
                                    "parent_id",
                                    new FieldUuidNullable(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new EmployeesTable().Name,
                                            ]
                                        ).TextValue,
                                        new EmployeeManagerIdColumn().Name.TextValue
                                    )
                                ),
                                new SelectItemProjection(
                                    "root_id",
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
                                    "root_user_id",
                                    new FieldUuid(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new EmployeesTable().Name,
                                            ]
                                        ).TextValue,
                                        new EmployeeUserIdColumn().Name.TextValue
                                    )
                                ),
                                new SelectItemProjection("depth", new LiteralInteger(0)),
                                new SelectItemProjection(
                                    "path",
                                    new AsStringUuidStringProjection(
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
                                new SelectItemProjection(
                                    "other_name_than_root",
                                    new LiteralBoolean(false)
                                ),
                                new SelectItemProjection(
                                    "shown_as",
                                    new CoalesceStringProjection([
                                        new FieldStringNullable(
                                            "account",
                                            new UserNameColumn().Name.TextValue
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
                                    ])
                                ),
                            ]
                        ),
                        new PlainNextQuery(
                            from: new FromSubquery("tree", alias: "parent"),
                            joins:
                            [
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
                                        new FieldUuidNullable(
                                            "child",
                                            new EmployeeManagerIdColumn().Name.TextValue
                                        ),
                                        new FieldUuid("parent", "id")
                                    ),
                                    alias: "child"
                                ),
                                new JoinEntity(
                                    JoinKind.Left,
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new UsersTable().Name,
                                        ]
                                    ).TextValue,
                                    new EqualUuidRow(
                                        new FieldUuid(
                                            "top",
                                            new UserIdColumn().Name.TextValue
                                        ),
                                        new FieldUuid("parent", "root_user_id")
                                    ),
                                    alias: "top"
                                ),
                                new JoinEntity(
                                    JoinKind.Left,
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new UsersTable().Name,
                                        ]
                                    ).TextValue,
                                    new AndRow([
                                        new EqualUuidRow(
                                            new FieldUuid(
                                                "account",
                                                new UserIdColumn().Name.TextValue
                                            ),
                                            new FieldUuid(
                                                "child",
                                                new EmployeeUserIdColumn().Name.TextValue
                                            )
                                        ),
                                        new EqualBooleanRow(
                                            new FieldBoolean(
                                                "account",
                                                new UserActiveColumn().Name.TextValue
                                            ),
                                            new LiteralBoolean(true)
                                        ),
                                    ]),
                                    alias: "account"
                                ),
                            ],
                            where: new AndRow([
                                new InUuidRow(
                                    new FieldUuid(
                                        "child",
                                        new EmployeeUserIdColumn().Name.TextValue
                                    ),
                                    new ListSubqueryColumnUuid("buyers", "user_id")
                                ),
                                new LessThanDecimalRow(
                                    new FieldInteger("parent", "depth"),
                                    new LiteralInteger(2)
                                ),
                                new EqualBooleanRow(
                                    new FieldBooleanNullable(
                                        "top",
                                        new UserActiveColumn().Name.TextValue
                                    ),
                                    new LiteralBoolean(true)
                                ),
                            ]),
                            select:
                            [
                                new SelectItemRow(
                                    "id",
                                    new FieldUuid(
                                        "child",
                                        new EmployeeIdColumn().Name.TextValue
                                    )
                                ),
                                new SelectItemRow(
                                    "parent_id",
                                    new FieldUuidNullable(
                                        "child",
                                        new EmployeeManagerIdColumn().Name.TextValue
                                    )
                                ),
                                new SelectItemRow(
                                    "root_id",
                                    new FieldUuid("parent", "root_id")
                                ),
                                new SelectItemRow(
                                    "root_user_id",
                                    new FieldUuid("parent", "root_user_id")
                                ),
                                new SelectItemRow(
                                    "depth",
                                    new AddIntegerRow([
                                        new FieldInteger("parent", "depth"),
                                        new LiteralInteger(1),
                                    ])
                                ),
                                new SelectItemRow(
                                    "path",
                                    new ConcatStringRow([
                                        new FieldString("parent", "path"),
                                        new LiteralString("/"),
                                        new AsStringUuidStringRow(
                                            new FieldUuid(
                                                "child",
                                                new EmployeeIdColumn().Name.TextValue
                                            )
                                        ),
                                    ])
                                ),
                                new SelectItemRow(
                                    "other_name_than_root",
                                    new NotEqualStringRow(
                                        new FieldString(
                                            "child",
                                            new EmployeeNameColumn().Name.TextValue
                                        ),
                                        new LiteralString("Grace")
                                    )
                                ),
                                new SelectItemRow(
                                    "shown_as",
                                    new IfStringRow(
                                        new FieldString(
                                            "child",
                                            new EmployeeNameColumn().Name.TextValue
                                        ),
                                        new CoalesceStringRow([
                                            new FieldStringNullable(
                                                "account",
                                                new UserNameColumn().Name.TextValue
                                            ),
                                            new LiteralString(""),
                                        ]),
                                        new EqualStringRow(
                                            new FieldStringNullable(
                                                "account",
                                                new UserNameColumn().Name.TextValue
                                            ),
                                            new LiteralStringNullable()
                                        )
                                    )
                                ),
                            ]
                        ),
                        4
                    )
                ),
                new SubqueryQuery(
                    "sizes",
                    new GroupedQueryWithoutPaginationWithoutOrderBy(
                        from: new FromSubquery("tree"),
                        groupBy:
                        [
                            new GroupKey("root_id", new FieldUuid("tree", "root_id")),
                        ],
                        select:
                        [
                            new SelectItemGroup("root_id", new KeyUuid("root_id")),
                            new SelectItemGroup("members", new CountGroup()),
                        ]
                    )
                ),
            ],
            from: new FromSubquery("tree", alias: "below"),
            joins:
            [
                new JoinSubquery(
                    JoinKind.Inner,
                    "tree",
                    new EqualUuidRow(
                        new FieldUuidNullable("below", "parent_id"),
                        new FieldUuid("above", "id")
                    ),
                    alias: "above"
                ),
                new JoinSubquery(
                    JoinKind.Inner,
                    "sizes",
                    new EqualUuidRow(
                        new FieldUuid("sizes", "root_id"),
                        new FieldUuid("below", "root_id")
                    )
                ),
            ],
            select:
            [
                new SelectItemProjection("employee_id", new FieldUuid("below", "id")),
                new SelectItemProjection(
                    "shown_as",
                    new FieldString("below", "shown_as")
                ),
                new SelectItemProjection(
                    "parent_shown_as",
                    new FieldString("above", "shown_as")
                ),
                new SelectItemProjection(
                    "levels_below_root",
                    new FieldInteger("below", "depth")
                ),
                new SelectItemProjection(
                    "tree_members",
                    new FieldInteger("sizes", "members")
                ),
                new SelectItemProjection("path", new FieldString("below", "path")),
            ],
            orderBy: [new OrderItemProjectionExpression(new FieldString("below", "path"))]
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
                    new EmployeeIdColumn(),
                    new Column(new String("shown_as"), new StringColumnType()),
                    new Column(new String("parent_shown_as"), new StringColumnType()),
                    new Column(new String("levels_below_root"), new LongColumnType()),
                    new Column(new String("tree_members"), new LongColumnType()),
                    new Column(new String("path"), new StringColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new EmployeeIdColumn(),
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
                                    new String("shown_as"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Hank"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("parent_shown_as"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Ann"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("levels_below_root"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("tree_members"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("path"), new StringColumnType()),
                                new InvariantCell(
                                    new String(
                                        "000002bd-0000-0000-0000-000000000000/000002be-0000-0000-0000-000000000000"
                                    )
                                )
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
                                new EmployeeIdColumn(),
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
                                    new String("shown_as"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Cara"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("parent_shown_as"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Ann"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("levels_below_root"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("tree_members"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("path"), new StringColumnType()),
                                new InvariantCell(
                                    new String(
                                        "000002bd-0000-0000-0000-000000000000/000002bf-0000-0000-0000-000000000000"
                                    )
                                )
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
