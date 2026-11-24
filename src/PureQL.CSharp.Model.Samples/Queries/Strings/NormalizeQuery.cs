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

namespace PureQL.CSharp.Model.Samples.Queries.Strings;

/// <summary>
/// Selects user_id, a signature of user_name followed by Lefevre written with a combining
/// grave accent after its first e, in the form NFC, whether user_name and the signature
/// are stored composed, the number of code points of the signature in the form NFD, the
/// employee_name of the employee the user is in the form NFC, nullable after the left
/// join, user_name case-folded in the form NFKC as a search key, the text No followed by
/// user_name from the numero sign in the form NFKC, and the signature in the form NFKD
/// from schema_with_foreign_keys.users left joined to schema_with_foreign_keys.employees
/// on employee_user_id. The where keeps the users whose key equals the key of the
/// full-width ANN or FAY, ordered by user_name in the form NFC and user_id.
/// </summary>
public sealed record NormalizeQuery
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
            where: new OrRow([
                new EqualStringRow(
                    new NormalizeStringRow(
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
                        NormalizationForm.NFKC
                    ),
                    new NormalizeStringRow(
                        new FoldCaseStringRow(new LiteralString("\uFF21\uFF2E\uFF2E")),
                        NormalizationForm.NFKC
                    )
                ),
                new EqualStringRow(
                    new NormalizeStringRow(
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
                        NormalizationForm.NFKC
                    ),
                    new NormalizeStringRow(
                        new FoldCaseStringRow(new LiteralString("\uFF26\uFF21\uFF39")),
                        NormalizationForm.NFKC
                    )
                ),
            ]),
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
                    "signature",
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
                            new LiteralString(" Lefe\u0300vre"),
                        ]),
                        NormalizationForm.NFC
                    )
                ),
                new SelectItemProjection(
                    "name_is_composed",
                    new EqualStringProjection(
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
                        new NormalizeStringProjection(
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
                            NormalizationForm.NFC
                        )
                    )
                ),
                new SelectItemProjection(
                    "signature_is_composed",
                    new EqualStringProjection(
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
                            new LiteralString(" Lefe\u0300vre"),
                        ]),
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
                                new LiteralString(" Lefe\u0300vre"),
                            ]),
                            NormalizationForm.NFC
                        )
                    )
                ),
                new SelectItemProjection(
                    "code_points_when_decomposed",
                    new LengthIntegerProjection(
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
                                new LiteralString(" Lefe\u0300vre"),
                            ]),
                            NormalizationForm.NFD
                        )
                    )
                ),
                new SelectItemProjection(
                    "employee_name",
                    new NormalizeStringNullableProjection(
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
                        NormalizationForm.NFC
                    )
                ),
                new SelectItemProjection(
                    "search_key",
                    new NormalizeStringProjection(
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
                        NormalizationForm.NFKC
                    )
                ),
                new SelectItemProjection(
                    "plain_label",
                    new NormalizeStringProjection(
                        new ConcatStringProjection([
                            new LiteralString("\u2116 "),
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
                        ]),
                        NormalizationForm.NFKC
                    )
                ),
                new SelectItemProjection(
                    "fully_decomposed",
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
                            new LiteralString(" Lefe\u0300vre"),
                        ]),
                        NormalizationForm.NFKD
                    )
                ),
            ],
            orderBy:
            [
                new OrderItemProjectionExpression(
                    new NormalizeStringProjection(
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
                        NormalizationForm.NFC
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
    /// foldCase of the full-width search texts is computed with Python str.casefold;
    /// normalize is PostgreSQL's, checked with Python unicodedata.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new UserIdColumn(),
                    new Column(new String("signature"), new StringColumnType()),
                    new Column(new String("name_is_composed"), new BoolColumnType()),
                    new Column(new String("signature_is_composed"), new BoolColumnType()),
                    new Column(
                        new String("code_points_when_decomposed"),
                        new LongColumnType()
                    ),
                    new EmployeeNameColumn(),
                    new Column(new String("search_key"), new StringColumnType()),
                    new Column(new String("plain_label"), new StringColumnType()),
                    new Column(new String("fully_decomposed"), new StringColumnType()),
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
                                new Column(
                                    new String("signature"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Ann Lef\u00E8vre"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("name_is_composed"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("signature_is_composed"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("code_points_when_decomposed"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(12))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new EmployeeNameColumn(),
                                new InvariantCell(new String("Grace"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("search_key"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("ann"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("plain_label"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("No Ann"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("fully_decomposed"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Ann Lefe\u0300vre"))
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
                                new Column(
                                    new String("signature"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Fay Lef\u00E8vre"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("name_is_composed"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("signature_is_composed"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("code_points_when_decomposed"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(12))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new EmployeeNameColumn(),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("search_key"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("fay"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("plain_label"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("No Fay"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("fully_decomposed"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Fay Lefe\u0300vre"))
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
