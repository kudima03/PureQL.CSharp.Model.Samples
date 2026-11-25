using Pure.Collections.Generic;
using Pure.Primitives.Bool;
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
using Guid = Pure.Primitives.Guid.Guid;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Recursive;

/// <summary>
/// Selects the chain of managers above employee #704 (Jack) from a recursive subquery
/// chain: seeded with that employee at level 0 from schema_with_foreign_keys.employees,
/// its next level reads chain in from and joins the employee whose employee_id is the
/// manager_id of each row at level + 1, with a maxDepth of 50. The main query keeps level
/// above 0 and selects level, id, name and whether manager_id equals a typed null (the
/// top of the chain), ordered by level.
/// </summary>
public sealed record AncestorsQuery
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
                                new LiteralUuid(new System.Guid(704, 0, 0, new byte[8]))
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
                                    "manager_id",
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
                                    "name",
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
                                new SelectItemProjection("level", new LiteralInteger(0)),
                            ]
                        ),
                        new PlainNextQuery(
                            from: new FromSubquery("chain"),
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
                                        new FieldUuidNullable("chain", "manager_id")
                                    )
                                ),
                            ],
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
                                    "manager_id",
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
                                new SelectItemRow(
                                    "name",
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
                                    "level",
                                    new AddIntegerRow([
                                        new FieldInteger("chain", "level"),
                                        new LiteralInteger(1),
                                    ])
                                ),
                            ]
                        ),
                        50
                    )
                ),
            ],
            from: new FromSubquery("chain"),
            where: new GreaterThanDecimalRow(
                new FieldInteger("chain", "level"),
                new LiteralInteger(0)
            ),
            select:
            [
                new SelectItemProjection("level", new FieldInteger("chain", "level")),
                new SelectItemProjection("id", new FieldUuid("chain", "id")),
                new SelectItemProjection("name", new FieldString("chain", "name")),
                new SelectItemProjection(
                    "is_top",
                    new EqualUuidProjection(
                        new FieldUuidNullable("chain", "manager_id"),
                        new LiteralUuidNullable()
                    )
                ),
            ],
            orderBy:
            [
                new OrderItemProjectionExpression(new FieldInteger("chain", "level")),
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
                    new Column(new String("level"), new LongColumnType()),
                    new IdColumn(),
                    new NameColumn(),
                    new Column(new String("is_top"), new BoolColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("level"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
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
                                new NameColumn(),
                                new InvariantCell(new String("Hank"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("is_top"), new BoolColumnType()),
                                new InvariantCell(new False())
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
                                new Column(new String("level"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
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
                                new NameColumn(),
                                new InvariantCell(new String("Grace"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("is_top"), new BoolColumnType()),
                                new InvariantCell(new True())
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
