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
/// Selects user_id, the first capital letter of user_name captured by group 1, the first
/// number in the text of user_tenant_id, the match nn in user_name, null where there is
/// none, the employee_name of the employee the user is without its vowels, nullable after
/// the left join, a label of user_name and the text of signup_date joined by two spaces
/// with every run of white space replaced by one space, whether employee_name ends with
/// k, false where it is null, and whether user_name matches a|e from
/// schema_with_foreign_keys.users left joined to schema_with_foreign_keys.employees on
/// employee_user_id. The where keeps the names that match ^[A-Z][a-z]{2}$, ordered by
/// user_name.
/// </summary>
public sealed record RegularExpressionsQuery
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
            where: new MatchesRegexRow(
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
                new LiteralString("^[A-Z][a-z]{2}$")
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
                    "initial",
                    new ExtractRegexProjection(
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
                        new LiteralString("^([A-Z])"),
                        new LiteralInteger(1)
                    )
                ),
                new SelectItemProjection(
                    "tenant_number",
                    new ExtractRegexProjection(
                        new AsStringUuidStringProjection(
                            new FieldUuid(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new UsersTable().Name,
                                    ]
                                ).TextValue,
                                new UserTenantIdColumn().Name.TextValue
                            )
                        ),
                        new LiteralString("[1-9][0-9]*")
                    )
                ),
                new SelectItemProjection(
                    "double_letter",
                    new ExtractRegexProjection(
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
                        new LiteralString("nn")
                    )
                ),
                new SelectItemProjection(
                    "employee_consonants",
                    new ReplaceRegexStringNullableProjection(
                        new FieldStringNullable(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new EmployeesTable().Name,
                                ]
                            ).TextValue,
                            new EmployeeNameColumn().Name.TextValue
                        ),
                        new LiteralString(""),
                        new LiteralString("[aeiou]")
                    )
                ),
                new SelectItemProjection(
                    "label_with_single_spaces",
                    new ReplaceRegexStringProjection(
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
                            new LiteralString("  "),
                            new AsStringDateStringProjection(
                                new FieldDate(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new UsersTable().Name,
                                        ]
                                    ).TextValue,
                                    new SignupDateColumn().Name.TextValue
                                )
                            ),
                        ]),
                        new LiteralString(" "),
                        new LiteralString("\\s+")
                    )
                ),
                new SelectItemProjection(
                    "employee_name_ends_with_k",
                    new MatchesRegexProjection(
                        new FieldStringNullable(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new EmployeesTable().Name,
                                ]
                            ).TextValue,
                            new EmployeeNameColumn().Name.TextValue
                        ),
                        new LiteralString("k$")
                    )
                ),
                new SelectItemProjection(
                    "name_matches_search",
                    new MatchesRegexProjection(
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
                        new LiteralString("a|e")
                    )
                ),
            ],
            orderBy:
            [
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
                    new UserIdColumn(),
                    new Column(new String("initial"), new StringColumnType()),
                    new Column(new String("tenant_number"), new StringColumnType()),
                    new Column(new String("double_letter"), new StringColumnType()),
                    new Column(new String("employee_consonants"), new StringColumnType()),
                    new Column(
                        new String("label_with_single_spaces"),
                        new StringColumnType()
                    ),
                    new Column(
                        new String("employee_name_ends_with_k"),
                        new BoolColumnType()
                    ),
                    new Column(new String("name_matches_search"), new BoolColumnType()),
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
                                new Column(new String("initial"), new StringColumnType()),
                                new InvariantCell(new String("A"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("tenant_number"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("385"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("double_letter"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("nn"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("employee_consonants"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Grc"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("label_with_single_spaces"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Ann 2020-01-15"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("employee_name_ends_with_k"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("name_matches_search"),
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
                                            "00000002-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("initial"), new StringColumnType()),
                                new InvariantCell(new String("B"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("tenant_number"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("385"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("double_letter"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("employee_consonants"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Hnk"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("label_with_single_spaces"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Bob 2021-03-20"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("employee_name_ends_with_k"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("name_matches_search"),
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
                                new Column(new String("initial"), new StringColumnType()),
                                new InvariantCell(new String("D"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("tenant_number"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("386"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("double_letter"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("employee_consonants"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Jck"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("label_with_single_spaces"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Dan 2022-11-05"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("employee_name_ends_with_k"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("name_matches_search"),
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
                                            "00000005-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("initial"), new StringColumnType()),
                                new InvariantCell(new String("E"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("tenant_number"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("386"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("double_letter"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("employee_consonants"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("label_with_single_spaces"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Eve 2023-02-28"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("employee_name_ends_with_k"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("name_matches_search"),
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
                                            "00000006-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("initial"), new StringColumnType()),
                                new InvariantCell(new String("F"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("tenant_number"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("386"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("double_letter"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("employee_consonants"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("label_with_single_spaces"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Fay 2020-01-15"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("employee_name_ends_with_k"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("name_matches_search"),
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
