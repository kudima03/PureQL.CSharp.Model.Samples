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
/// Selects user_id, user_name, a label of user_name followed by from Lodz (written with
/// an L with stroke and two accents) without its accents, the label case-folded without
/// accents, whether the label has accents, the employee_name of the employee the user is
/// without accents, nullable after the left join, whether user_name case-folded without
/// accents equals ANN written with an accented A, and user_name followed by fiancee
/// (written with a ligature fi and an accent) case-folded, in the form NFKC and without
/// accents, from schema_with_foreign_keys.users left joined to
/// schema_with_foreign_keys.employees on employee_user_id. The where keeps the names that
/// contain an accented A when both sides are case-folded without accents, ordered by
/// user_name case-folded without accents, user_name and user_id.
/// </summary>
public sealed record RemoveDiacriticsQuery
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
                new RemoveDiacriticsStringRow(
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
                    )
                ),
                new RemoveDiacriticsStringRow(
                    new FoldCaseStringRow(new LiteralString("\u00C1"))
                )
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
                    "label_without_accents",
                    new RemoveDiacriticsStringProjection(
                        new ConcatStringProjection([
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
                            new LiteralString(" from \u0141\u00F3d\u017A"),
                        ])
                    )
                ),
                new SelectItemProjection(
                    "label_search_key",
                    new RemoveDiacriticsStringProjection(
                        new FoldCaseStringProjection(
                            new ConcatStringProjection([
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
                                new LiteralString(" from \u0141\u00F3d\u017A"),
                            ])
                        )
                    )
                ),
                new SelectItemProjection(
                    "label_has_accents",
                    new NotEqualStringProjection(
                        new NormalizeStringProjection(
                            new ConcatStringProjection([
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
                                new LiteralString(" from \u0141\u00F3d\u017A"),
                            ]),
                            NormalizationForm.NFC
                        ),
                        new RemoveDiacriticsStringProjection(
                            new ConcatStringProjection([
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
                                new LiteralString(" from \u0141\u00F3d\u017A"),
                            ])
                        )
                    )
                ),
                new SelectItemProjection(
                    "employee_name_without_accents",
                    new RemoveDiacriticsStringNullableProjection(
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
                    "is_ann",
                    new EqualStringProjection(
                        new RemoveDiacriticsStringProjection(
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
                        new RemoveDiacriticsStringProjection(
                            new FoldCaseStringProjection(new LiteralString("\u00C1NN"))
                        )
                    )
                ),
                new SelectItemProjection(
                    "wide_search_key",
                    new RemoveDiacriticsStringProjection(
                        new NormalizeStringProjection(
                            new FoldCaseStringProjection(
                                new ConcatStringProjection([
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
                                    new LiteralString(" \uFB01anc\u00E9e"),
                                ])
                            ),
                            NormalizationForm.NFKC
                        )
                    )
                ),
            ],
            orderBy:
            [
                new OrderItemProjectionExpression(
                    new RemoveDiacriticsStringProjection(
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
    /// removeDiacritics and foldCase are computed with Python unicodedata and
    /// str.casefold, which PostgreSQL 17 does not provide.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new UserIdColumn(),
                    new UserNameColumn(),
                    new Column(
                        new String("label_without_accents"),
                        new StringColumnType()
                    ),
                    new Column(new String("label_search_key"), new StringColumnType()),
                    new Column(new String("label_has_accents"), new BoolColumnType()),
                    new Column(
                        new String("employee_name_without_accents"),
                        new StringColumnType()
                    ),
                    new Column(new String("is_ann"), new BoolColumnType()),
                    new Column(new String("wide_search_key"), new StringColumnType()),
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
                                    new String("label_without_accents"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Ann from \u0141odz"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("label_search_key"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("ann from \u0142odz"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("label_has_accents"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("employee_name_without_accents"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Grace"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("is_ann"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("wide_search_key"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("ann fiancee"))
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
                                    new String("label_without_accents"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Cara from \u0141odz"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("label_search_key"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("cara from \u0142odz"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("label_has_accents"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("employee_name_without_accents"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Iris"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("is_ann"), new BoolColumnType()),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("wide_search_key"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("cara fiancee"))
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
                                    new String("label_without_accents"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Dan from \u0141odz"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("label_search_key"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("dan from \u0142odz"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("label_has_accents"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("employee_name_without_accents"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Jack"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("is_ann"), new BoolColumnType()),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("wide_search_key"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("dan fiancee"))
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
                                    new String("label_without_accents"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Fay from \u0141odz"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("label_search_key"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("fay from \u0142odz"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("label_has_accents"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("employee_name_without_accents"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("is_ann"), new BoolColumnType()),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("wide_search_key"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("fay fiancee"))
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
