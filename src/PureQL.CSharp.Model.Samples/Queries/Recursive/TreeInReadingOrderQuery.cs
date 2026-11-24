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
using PureQL.CSharp.Model.RowExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Recursive;

/// <summary>
/// Selects every employee as a tree in reading order from a recursive subquery tree:
/// seeded with the employees of schema_with_foreign_keys.employees whose
/// employee_manager_id equals a typed null, at depth 0 with their name as path and label;
/// its next level joins the employees managed by a row of the level before, at depth + 1,
/// the path extended with concat and " / ", and the label indented with repeat of two
/// spaces depth + 1 times, with a maxDepth of 10. The main query selects label, depth and
/// path, ordered by path and id.
/// </summary>
public sealed record TreeInReadingOrderQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithoutPagination(
            subqueries:
            [
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
                                new SelectItemProjection("depth", new LiteralInteger(0)),
                                new SelectItemProjection(
                                    "path",
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
                                    "tree",
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
                                        new FieldUuid("tree", "id")
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
                                    "depth",
                                    new AddIntegerRow([
                                        new FieldInteger("tree", "depth"),
                                        new LiteralInteger(1),
                                    ])
                                ),
                                new SelectItemRow(
                                    "path",
                                    new ConcatStringRow([
                                        new FieldString("tree", "path"),
                                        new LiteralString(" / "),
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
                                new SelectItemRow(
                                    "label",
                                    new ConcatStringRow([
                                        new RepeatStringRow(
                                            new LiteralString("  "),
                                            new AddIntegerRow([
                                                new FieldInteger("tree", "depth"),
                                                new LiteralInteger(1),
                                            ])
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
                        10
                    )
                ),
            ],
            from: new FromSubquery("tree"),
            select:
            [
                new SelectItemProjection("label", new FieldString("tree", "label")),
                new SelectItemProjection("depth", new FieldInteger("tree", "depth")),
                new SelectItemProjection("path", new FieldString("tree", "path")),
            ],
            orderBy:
            [
                new OrderItemProjectionExpression(new FieldString("tree", "path")),
                new OrderItemProjectionExpression(new FieldUuid("tree", "id")),
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
                    new Column(new String("label"), new StringColumnType()),
                    new Column(new String("depth"), new LongColumnType()),
                    new Column(new String("path"), new StringColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("label"), new StringColumnType()),
                                new InvariantCell(new String("Grace"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("depth"), new LongColumnType()),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("path"), new StringColumnType()),
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
                                new Column(new String("label"), new StringColumnType()),
                                new InvariantCell(new String("  Hank"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("depth"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("path"), new StringColumnType()),
                                new InvariantCell(new String("Grace / Hank"))
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
                                new Column(new String("label"), new StringColumnType()),
                                new InvariantCell(new String("    Jack"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("depth"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("path"), new StringColumnType()),
                                new InvariantCell(new String("Grace / Hank / Jack"))
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
                                new Column(new String("label"), new StringColumnType()),
                                new InvariantCell(new String("  Iris"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("depth"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("path"), new StringColumnType()),
                                new InvariantCell(new String("Grace / Iris"))
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
