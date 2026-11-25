using Pure.Collections.Generic;
using Pure.Primitives.Bool;
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

namespace PureQL.CSharp.Model.Samples.Queries.Strings;

/// <summary>
/// Selects user_id, user_name, user_name case-folded, the case-folded employee_name of
/// the employee the user is, nullable after the left join, whether it equals GRACE
/// case-folded, whether the folded user_name starts with CA folded, the greeting Gruesse
/// (with a u umlaut and a sharp s) followed by user_name case-folded, and whether the
/// folded user_name matches the folded pattern ^(ann|fay)$ from
/// schema_with_foreign_keys.users left joined to schema_with_foreign_keys.employees on
/// employee_user_id. The where keeps the names that contain A when both sides are
/// case-folded, ordered by the folded user_name, user_name and user_id.
/// </summary>
public sealed record FoldCaseQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithoutPagination(
            from: new FromEntity(
                new JoinedString(
                    new DotString(),
                    [new RelationalSchemaWithForeignKeys().Name, new UsersTable().Name]
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
                            new EmployeesTable().Name,
                        ]
                    ).TextValue,
                    new EqualUuidRow(
                        new FieldUuidNullable(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new EmployeesTable().Name,
                                ]
                            ).TextValue,
                            new EmployeeUserIdColumn().Name.TextValue
                        ),
                        new FieldUuid(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue,
                            new UserIdColumn().Name.TextValue
                        )
                    )
                ),
            ],
            where: new ContainsRow(
                new FoldCaseStringRow(
                    new FieldString(
                        new JoinedString(
                            new DotString(),
                            [
                                new RelationalSchemaWithForeignKeys().Name,
                                new UsersTable().Name,
                            ]
                        ).TextValue,
                        new UserNameColumn().Name.TextValue
                    )
                ),
                new FoldCaseStringRow(new LiteralString("A"))
            ),
            select:
            [
                new SelectItemProjection(
                    "user_id",
                    new FieldUuid(
                        new JoinedString(
                            new DotString(),
                            [
                                new RelationalSchemaWithForeignKeys().Name,
                                new UsersTable().Name,
                            ]
                        ).TextValue,
                        new UserIdColumn().Name.TextValue
                    )
                ),
                new SelectItemProjection(
                    "user_name",
                    new FieldString(
                        new JoinedString(
                            new DotString(),
                            [
                                new RelationalSchemaWithForeignKeys().Name,
                                new UsersTable().Name,
                            ]
                        ).TextValue,
                        new UserNameColumn().Name.TextValue
                    )
                ),
                new SelectItemProjection(
                    "name_key",
                    new FoldCaseStringProjection(
                        new FieldString(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue,
                            new UserNameColumn().Name.TextValue
                        )
                    )
                ),
                new SelectItemProjection(
                    "employee_name_key",
                    new FoldCaseStringNullableProjection(
                        new FieldStringNullable(
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
                new SelectItemProjection(
                    "employee_is_grace",
                    new EqualStringProjection(
                        new FoldCaseStringNullableProjection(
                            new FieldStringNullable(
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
                        new FoldCaseStringProjection(new LiteralString("GRACE"))
                    )
                ),
                new SelectItemProjection(
                    "starts_with_prefix",
                    new StartsWithProjection(
                        new FoldCaseStringProjection(
                            new FieldString(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new UsersTable().Name,
                                    ]
                                ).TextValue,
                                new UserNameColumn().Name.TextValue
                            )
                        ),
                        new FoldCaseStringProjection(new LiteralString("CA"))
                    )
                ),
                new SelectItemProjection(
                    "greeting_key",
                    new FoldCaseStringProjection(
                        new ConcatStringProjection([
                            new LiteralString("Gr\u00FC\u00DFe "),
                            new FieldString(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new UsersTable().Name,
                                    ]
                                ).TextValue,
                                new UserNameColumn().Name.TextValue
                            ),
                        ])
                    )
                ),
                new SelectItemProjection(
                    "is_ann_or_fay",
                    new MatchesRegexProjection(
                        new FoldCaseStringProjection(
                            new FieldString(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new UsersTable().Name,
                                    ]
                                ).TextValue,
                                new UserNameColumn().Name.TextValue
                            )
                        ),
                        new LiteralString("^(ann|fay)$")
                    )
                ),
            ],
            orderBy:
            [
                new OrderItemProjectionExpression(
                    new FoldCaseStringProjection(
                        new FieldString(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue,
                            new UserNameColumn().Name.TextValue
                        )
                    )
                ),
                new OrderItemProjectionExpression(
                    new FieldString(
                        new JoinedString(
                            new DotString(),
                            [
                                new RelationalSchemaWithForeignKeys().Name,
                                new UsersTable().Name,
                            ]
                        ).TextValue,
                        new UserNameColumn().Name.TextValue
                    )
                ),
                new OrderItemProjectionExpression(
                    new FieldUuid(
                        new JoinedString(
                            new DotString(),
                            [
                                new RelationalSchemaWithForeignKeys().Name,
                                new UsersTable().Name,
                            ]
                        ).TextValue,
                        new UserIdColumn().Name.TextValue
                    )
                ),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and
    /// <see cref="AuditSchemaDataSet"/>, in this order.
    /// foldCase is computed with Python str.casefold (the sharp s folds to ss), which
    /// PostgreSQL 17 does not have; for the ASCII names of the fixtures it equals lower.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new UserIdColumn(),
                    new UserNameColumn(),
                    new Column(new String("name_key"), new StringColumnType()),
                    new Column(new String("employee_name_key"), new StringColumnType()),
                    new Column(new String("employee_is_grace"), new BoolColumnType()),
                    new Column(new String("starts_with_prefix"), new BoolColumnType()),
                    new Column(new String("greeting_key"), new StringColumnType()),
                    new Column(new String("is_ann_or_fay"), new BoolColumnType()),
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
                                new UserNameColumn(),
                                new InvariantCell(new String("Ann"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("name_key"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("ann"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("employee_name_key"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("grace"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("employee_is_grace"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("starts_with_prefix"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greeting_key"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("gr\u00FCsse ann"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("is_ann_or_fay"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
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
                                new UserNameColumn(),
                                new InvariantCell(new String("Cara"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("name_key"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("cara"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("employee_name_key"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("iris"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("employee_is_grace"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("starts_with_prefix"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greeting_key"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("gr\u00FCsse cara"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("is_ann_or_fay"),
                                    new BoolColumnType()
                                ),
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
                                new UserNameColumn(),
                                new InvariantCell(new String("Dan"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("name_key"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("dan"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("employee_name_key"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("jack"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("employee_is_grace"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("starts_with_prefix"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greeting_key"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("gr\u00FCsse dan"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("is_ann_or_fay"),
                                    new BoolColumnType()
                                ),
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
                                new UserIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000006-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new UserNameColumn(),
                                new InvariantCell(new String("Fay"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("name_key"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("fay"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("employee_name_key"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("employee_is_grace"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("starts_with_prefix"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greeting_key"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("gr\u00FCsse fay"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("is_ann_or_fay"),
                                    new BoolColumnType()
                                ),
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
