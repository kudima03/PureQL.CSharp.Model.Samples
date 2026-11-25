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
using PureQL.CSharp.Model.RowExpressions;
using PureQL.CSharp.Model.TotalExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Strings;

/// <summary>
/// Aggregates schema_with_foreign_keys.users into one row: the count of users whose last
/// name in the form NFC differs from the last name without accents as with_accents, the
/// min of the last name without accents as first_last_name, the min of the last name
/// without accents as first_plain_last_name, the distinct countries without accents
/// joined by a comma and a space in their own order as countries, whether any last name
/// case-folded without accents contains an accented U with two L case-folded without
/// accents as any_like_needle, and the mode of the last name case-folded without accents
/// as most_common_key. The where keeps the users whose country case-folded without
/// accents differs from BRESIL written with an accented E case-folded without accents.
/// The last name and country are stand-ins built from user_id, user_tenant_id and
/// user_name.
/// </summary>
public sealed record RemoveDiacriticsInATotalQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainTotalQuery(
            from: new FromEntity(
                new JoinedString(
                    new DotString(),
                    [new RelationalSchemaWithForeignKeys().Name, new UsersTable().Name]
                ).TextValue
            ),
            where: new NotEqualStringRow(
                new RemoveDiacriticsStringRow(
                    new FoldCaseStringRow(
                        new IfStringRow(
                            new LiteralString("Chile"),
                            new IfStringRow(
                                new IfStringRow(
                                    new LiteralString("Bresil"),
                                    new LiteralString("Br\u00E9sil"),
                                    new EqualUuidRow(
                                        new FieldUuid(
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new RelationalSchemaWithForeignKeys().Name,
                                                    new UsersTable().Name,
                                                ]
                                            ).TextValue,
                                            new UserIdColumn().Name.TextValue
                                        ),
                                        new LiteralUuid(new Guid(2, 0, 0, new byte[8]))
                                    )
                                ),
                                new IfStringRow(
                                    new LiteralString("P\u00C9RU"),
                                    new LiteralString("Peru"),
                                    new EqualUuidRow(
                                        new FieldUuid(
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new RelationalSchemaWithForeignKeys().Name,
                                                    new UsersTable().Name,
                                                ]
                                            ).TextValue,
                                            new UserIdColumn().Name.TextValue
                                        ),
                                        new LiteralUuid(new Guid(5, 0, 0, new byte[8]))
                                    )
                                ),
                                new EqualUuidRow(
                                    new FieldUuid(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new UsersTable().Name,
                                            ]
                                        ).TextValue,
                                        new UserTenantIdColumn().Name.TextValue
                                    ),
                                    new LiteralUuid(new Guid(901, 0, 0, new byte[8]))
                                )
                            ),
                            new EqualUuidRow(
                                new FieldUuid(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new UsersTable().Name,
                                        ]
                                    ).TextValue,
                                    new UserIdColumn().Name.TextValue
                                ),
                                new LiteralUuid(new Guid(6, 0, 0, new byte[8]))
                            )
                        )
                    )
                ),
                new RemoveDiacriticsStringRow(
                    new FoldCaseStringRow(new LiteralString("BR\u00C9SIL"))
                )
            ),
            aggregate:
            [
                new SelectItemTotal(
                    "with_accents",
                    new CountTotal(
                        new NotEqualStringRow(
                            new NormalizeStringRow(
                                new IfStringRow(
                                    new LiteralString("LEF\u00C8VRE"),
                                    new IfStringRow(
                                        new LiteralString("Lef\u00E8vre"),
                                        new IfStringRow(
                                            new LiteralString("MULLER"),
                                            new IfStringRow(
                                                new LiteralString("Lefe\u0300vre"),
                                                new LiteralString("Mu\u0308ller"),
                                                new EqualUuidRow(
                                                    new FieldUuid(
                                                        new JoinedString(
                                                            new DotString(),
                                                            [
                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                new UsersTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new UserTenantIdColumn()
                                                            .Name
                                                            .TextValue
                                                    ),
                                                    new LiteralUuid(
                                                        new Guid(901, 0, 0, new byte[8])
                                                    )
                                                )
                                            ),
                                            new EqualUuidRow(
                                                new FieldUuid(
                                                    new JoinedString(
                                                        new DotString(),
                                                        [
                                                            new RelationalSchemaWithForeignKeys().Name,
                                                            new UsersTable().Name,
                                                        ]
                                                    ).TextValue,
                                                    new UserIdColumn().Name.TextValue
                                                ),
                                                new LiteralUuid(
                                                    new Guid(6, 0, 0, new byte[8])
                                                )
                                            )
                                        ),
                                        new EqualUuidRow(
                                            new FieldUuid(
                                                new JoinedString(
                                                    new DotString(),
                                                    [
                                                        new RelationalSchemaWithForeignKeys().Name,
                                                        new UsersTable().Name,
                                                    ]
                                                ).TextValue,
                                                new UserIdColumn().Name.TextValue
                                            ),
                                            new LiteralUuid(
                                                new Guid(3, 0, 0, new byte[8])
                                            )
                                        )
                                    ),
                                    new EqualUuidRow(
                                        new FieldUuid(
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new RelationalSchemaWithForeignKeys().Name,
                                                    new UsersTable().Name,
                                                ]
                                            ).TextValue,
                                            new UserIdColumn().Name.TextValue
                                        ),
                                        new LiteralUuid(new Guid(2, 0, 0, new byte[8]))
                                    )
                                ),
                                NormalizationForm.NFC
                            ),
                            new RemoveDiacriticsStringRow(
                                new IfStringRow(
                                    new LiteralString("LEF\u00C8VRE"),
                                    new IfStringRow(
                                        new LiteralString("Lef\u00E8vre"),
                                        new IfStringRow(
                                            new LiteralString("MULLER"),
                                            new IfStringRow(
                                                new LiteralString("Lefe\u0300vre"),
                                                new LiteralString("Mu\u0308ller"),
                                                new EqualUuidRow(
                                                    new FieldUuid(
                                                        new JoinedString(
                                                            new DotString(),
                                                            [
                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                new UsersTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new UserTenantIdColumn()
                                                            .Name
                                                            .TextValue
                                                    ),
                                                    new LiteralUuid(
                                                        new Guid(901, 0, 0, new byte[8])
                                                    )
                                                )
                                            ),
                                            new EqualUuidRow(
                                                new FieldUuid(
                                                    new JoinedString(
                                                        new DotString(),
                                                        [
                                                            new RelationalSchemaWithForeignKeys().Name,
                                                            new UsersTable().Name,
                                                        ]
                                                    ).TextValue,
                                                    new UserIdColumn().Name.TextValue
                                                ),
                                                new LiteralUuid(
                                                    new Guid(6, 0, 0, new byte[8])
                                                )
                                            )
                                        ),
                                        new EqualUuidRow(
                                            new FieldUuid(
                                                new JoinedString(
                                                    new DotString(),
                                                    [
                                                        new RelationalSchemaWithForeignKeys().Name,
                                                        new UsersTable().Name,
                                                    ]
                                                ).TextValue,
                                                new UserIdColumn().Name.TextValue
                                            ),
                                            new LiteralUuid(
                                                new Guid(3, 0, 0, new byte[8])
                                            )
                                        )
                                    ),
                                    new EqualUuidRow(
                                        new FieldUuid(
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new RelationalSchemaWithForeignKeys().Name,
                                                    new UsersTable().Name,
                                                ]
                                            ).TextValue,
                                            new UserIdColumn().Name.TextValue
                                        ),
                                        new LiteralUuid(new Guid(2, 0, 0, new byte[8]))
                                    )
                                )
                            )
                        )
                    )
                ),
                new SelectItemTotal(
                    "first_last_name",
                    new RemoveDiacriticsStringNullableTotal(
                        new MinStringNullableTotal(
                            new IfStringRow(
                                new LiteralString("LEF\u00C8VRE"),
                                new IfStringRow(
                                    new LiteralString("Lef\u00E8vre"),
                                    new IfStringRow(
                                        new LiteralString("MULLER"),
                                        new IfStringRow(
                                            new LiteralString("Lefe\u0300vre"),
                                            new LiteralString("Mu\u0308ller"),
                                            new EqualUuidRow(
                                                new FieldUuid(
                                                    new JoinedString(
                                                        new DotString(),
                                                        [
                                                            new RelationalSchemaWithForeignKeys().Name,
                                                            new UsersTable().Name,
                                                        ]
                                                    ).TextValue,
                                                    new UserTenantIdColumn()
                                                        .Name
                                                        .TextValue
                                                ),
                                                new LiteralUuid(
                                                    new Guid(901, 0, 0, new byte[8])
                                                )
                                            )
                                        ),
                                        new EqualUuidRow(
                                            new FieldUuid(
                                                new JoinedString(
                                                    new DotString(),
                                                    [
                                                        new RelationalSchemaWithForeignKeys().Name,
                                                        new UsersTable().Name,
                                                    ]
                                                ).TextValue,
                                                new UserIdColumn().Name.TextValue
                                            ),
                                            new LiteralUuid(
                                                new Guid(6, 0, 0, new byte[8])
                                            )
                                        )
                                    ),
                                    new EqualUuidRow(
                                        new FieldUuid(
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new RelationalSchemaWithForeignKeys().Name,
                                                    new UsersTable().Name,
                                                ]
                                            ).TextValue,
                                            new UserIdColumn().Name.TextValue
                                        ),
                                        new LiteralUuid(new Guid(3, 0, 0, new byte[8]))
                                    )
                                ),
                                new EqualUuidRow(
                                    new FieldUuid(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new UsersTable().Name,
                                            ]
                                        ).TextValue,
                                        new UserIdColumn().Name.TextValue
                                    ),
                                    new LiteralUuid(new Guid(2, 0, 0, new byte[8]))
                                )
                            )
                        )
                    )
                ),
                new SelectItemTotal(
                    "first_plain_last_name",
                    new MinStringNullableTotal(
                        new RemoveDiacriticsStringRow(
                            new IfStringRow(
                                new LiteralString("LEF\u00C8VRE"),
                                new IfStringRow(
                                    new LiteralString("Lef\u00E8vre"),
                                    new IfStringRow(
                                        new LiteralString("MULLER"),
                                        new IfStringRow(
                                            new LiteralString("Lefe\u0300vre"),
                                            new LiteralString("Mu\u0308ller"),
                                            new EqualUuidRow(
                                                new FieldUuid(
                                                    new JoinedString(
                                                        new DotString(),
                                                        [
                                                            new RelationalSchemaWithForeignKeys().Name,
                                                            new UsersTable().Name,
                                                        ]
                                                    ).TextValue,
                                                    new UserTenantIdColumn()
                                                        .Name
                                                        .TextValue
                                                ),
                                                new LiteralUuid(
                                                    new Guid(901, 0, 0, new byte[8])
                                                )
                                            )
                                        ),
                                        new EqualUuidRow(
                                            new FieldUuid(
                                                new JoinedString(
                                                    new DotString(),
                                                    [
                                                        new RelationalSchemaWithForeignKeys().Name,
                                                        new UsersTable().Name,
                                                    ]
                                                ).TextValue,
                                                new UserIdColumn().Name.TextValue
                                            ),
                                            new LiteralUuid(
                                                new Guid(6, 0, 0, new byte[8])
                                            )
                                        )
                                    ),
                                    new EqualUuidRow(
                                        new FieldUuid(
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new RelationalSchemaWithForeignKeys().Name,
                                                    new UsersTable().Name,
                                                ]
                                            ).TextValue,
                                            new UserIdColumn().Name.TextValue
                                        ),
                                        new LiteralUuid(new Guid(3, 0, 0, new byte[8]))
                                    )
                                ),
                                new EqualUuidRow(
                                    new FieldUuid(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new UsersTable().Name,
                                            ]
                                        ).TextValue,
                                        new UserIdColumn().Name.TextValue
                                    ),
                                    new LiteralUuid(new Guid(2, 0, 0, new byte[8]))
                                )
                            )
                        )
                    )
                ),
                new SelectItemTotal(
                    "countries",
                    new ConcatAllTotal(
                        new RemoveDiacriticsStringRow(
                            new IfStringRow(
                                new LiteralString("Chile"),
                                new IfStringRow(
                                    new IfStringRow(
                                        new LiteralString("Bresil"),
                                        new LiteralString("Br\u00E9sil"),
                                        new EqualUuidRow(
                                            new FieldUuid(
                                                new JoinedString(
                                                    new DotString(),
                                                    [
                                                        new RelationalSchemaWithForeignKeys().Name,
                                                        new UsersTable().Name,
                                                    ]
                                                ).TextValue,
                                                new UserIdColumn().Name.TextValue
                                            ),
                                            new LiteralUuid(
                                                new Guid(2, 0, 0, new byte[8])
                                            )
                                        )
                                    ),
                                    new IfStringRow(
                                        new LiteralString("P\u00C9RU"),
                                        new LiteralString("Peru"),
                                        new EqualUuidRow(
                                            new FieldUuid(
                                                new JoinedString(
                                                    new DotString(),
                                                    [
                                                        new RelationalSchemaWithForeignKeys().Name,
                                                        new UsersTable().Name,
                                                    ]
                                                ).TextValue,
                                                new UserIdColumn().Name.TextValue
                                            ),
                                            new LiteralUuid(
                                                new Guid(5, 0, 0, new byte[8])
                                            )
                                        )
                                    ),
                                    new EqualUuidRow(
                                        new FieldUuid(
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new RelationalSchemaWithForeignKeys().Name,
                                                    new UsersTable().Name,
                                                ]
                                            ).TextValue,
                                            new UserTenantIdColumn().Name.TextValue
                                        ),
                                        new LiteralUuid(new Guid(901, 0, 0, new byte[8]))
                                    )
                                ),
                                new EqualUuidRow(
                                    new FieldUuid(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new UsersTable().Name,
                                            ]
                                        ).TextValue,
                                        new UserIdColumn().Name.TextValue
                                    ),
                                    new LiteralUuid(new Guid(6, 0, 0, new byte[8]))
                                )
                            )
                        ),
                        new LiteralString(", "),
                        [
                            new OrderItemRow(
                                new RemoveDiacriticsStringRow(
                                    new IfStringRow(
                                        new LiteralString("Chile"),
                                        new IfStringRow(
                                            new IfStringRow(
                                                new LiteralString("Bresil"),
                                                new LiteralString("Br\u00E9sil"),
                                                new EqualUuidRow(
                                                    new FieldUuid(
                                                        new JoinedString(
                                                            new DotString(),
                                                            [
                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                new UsersTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new UserIdColumn().Name.TextValue
                                                    ),
                                                    new LiteralUuid(
                                                        new Guid(2, 0, 0, new byte[8])
                                                    )
                                                )
                                            ),
                                            new IfStringRow(
                                                new LiteralString("P\u00C9RU"),
                                                new LiteralString("Peru"),
                                                new EqualUuidRow(
                                                    new FieldUuid(
                                                        new JoinedString(
                                                            new DotString(),
                                                            [
                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                new UsersTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new UserIdColumn().Name.TextValue
                                                    ),
                                                    new LiteralUuid(
                                                        new Guid(5, 0, 0, new byte[8])
                                                    )
                                                )
                                            ),
                                            new EqualUuidRow(
                                                new FieldUuid(
                                                    new JoinedString(
                                                        new DotString(),
                                                        [
                                                            new RelationalSchemaWithForeignKeys().Name,
                                                            new UsersTable().Name,
                                                        ]
                                                    ).TextValue,
                                                    new UserTenantIdColumn()
                                                        .Name
                                                        .TextValue
                                                ),
                                                new LiteralUuid(
                                                    new Guid(901, 0, 0, new byte[8])
                                                )
                                            )
                                        ),
                                        new EqualUuidRow(
                                            new FieldUuid(
                                                new JoinedString(
                                                    new DotString(),
                                                    [
                                                        new RelationalSchemaWithForeignKeys().Name,
                                                        new UsersTable().Name,
                                                    ]
                                                ).TextValue,
                                                new UserIdColumn().Name.TextValue
                                            ),
                                            new LiteralUuid(
                                                new Guid(6, 0, 0, new byte[8])
                                            )
                                        )
                                    )
                                )
                            ),
                        ],
                        distinct: true
                    )
                ),
                new SelectItemTotal(
                    "any_like_needle",
                    new AnyTotal(
                        new ContainsRow(
                            new RemoveDiacriticsStringRow(
                                new FoldCaseStringRow(
                                    new IfStringRow(
                                        new LiteralString("LEF\u00C8VRE"),
                                        new IfStringRow(
                                            new LiteralString("Lef\u00E8vre"),
                                            new IfStringRow(
                                                new LiteralString("MULLER"),
                                                new IfStringRow(
                                                    new LiteralString("Lefe\u0300vre"),
                                                    new LiteralString("Mu\u0308ller"),
                                                    new EqualUuidRow(
                                                        new FieldUuid(
                                                            new JoinedString(
                                                                new DotString(),
                                                                [
                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                    new UsersTable().Name,
                                                                ]
                                                            ).TextValue,
                                                            new UserTenantIdColumn()
                                                                .Name
                                                                .TextValue
                                                        ),
                                                        new LiteralUuid(
                                                            new Guid(
                                                                901,
                                                                0,
                                                                0,
                                                                new byte[8]
                                                            )
                                                        )
                                                    )
                                                ),
                                                new EqualUuidRow(
                                                    new FieldUuid(
                                                        new JoinedString(
                                                            new DotString(),
                                                            [
                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                new UsersTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new UserIdColumn().Name.TextValue
                                                    ),
                                                    new LiteralUuid(
                                                        new Guid(6, 0, 0, new byte[8])
                                                    )
                                                )
                                            ),
                                            new EqualUuidRow(
                                                new FieldUuid(
                                                    new JoinedString(
                                                        new DotString(),
                                                        [
                                                            new RelationalSchemaWithForeignKeys().Name,
                                                            new UsersTable().Name,
                                                        ]
                                                    ).TextValue,
                                                    new UserIdColumn().Name.TextValue
                                                ),
                                                new LiteralUuid(
                                                    new Guid(3, 0, 0, new byte[8])
                                                )
                                            )
                                        ),
                                        new EqualUuidRow(
                                            new FieldUuid(
                                                new JoinedString(
                                                    new DotString(),
                                                    [
                                                        new RelationalSchemaWithForeignKeys().Name,
                                                        new UsersTable().Name,
                                                    ]
                                                ).TextValue,
                                                new UserIdColumn().Name.TextValue
                                            ),
                                            new LiteralUuid(
                                                new Guid(2, 0, 0, new byte[8])
                                            )
                                        )
                                    )
                                )
                            ),
                            new RemoveDiacriticsStringRow(
                                new FoldCaseStringRow(new LiteralString("\u00DCLL"))
                            )
                        )
                    )
                ),
                new SelectItemTotal(
                    "most_common_key",
                    new ModeStringNullableTotal(
                        new RemoveDiacriticsStringRow(
                            new FoldCaseStringRow(
                                new IfStringRow(
                                    new LiteralString("LEF\u00C8VRE"),
                                    new IfStringRow(
                                        new LiteralString("Lef\u00E8vre"),
                                        new IfStringRow(
                                            new LiteralString("MULLER"),
                                            new IfStringRow(
                                                new LiteralString("Lefe\u0300vre"),
                                                new LiteralString("Mu\u0308ller"),
                                                new EqualUuidRow(
                                                    new FieldUuid(
                                                        new JoinedString(
                                                            new DotString(),
                                                            [
                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                new UsersTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new UserTenantIdColumn()
                                                            .Name
                                                            .TextValue
                                                    ),
                                                    new LiteralUuid(
                                                        new Guid(901, 0, 0, new byte[8])
                                                    )
                                                )
                                            ),
                                            new EqualUuidRow(
                                                new FieldUuid(
                                                    new JoinedString(
                                                        new DotString(),
                                                        [
                                                            new RelationalSchemaWithForeignKeys().Name,
                                                            new UsersTable().Name,
                                                        ]
                                                    ).TextValue,
                                                    new UserIdColumn().Name.TextValue
                                                ),
                                                new LiteralUuid(
                                                    new Guid(6, 0, 0, new byte[8])
                                                )
                                            )
                                        ),
                                        new EqualUuidRow(
                                            new FieldUuid(
                                                new JoinedString(
                                                    new DotString(),
                                                    [
                                                        new RelationalSchemaWithForeignKeys().Name,
                                                        new UsersTable().Name,
                                                    ]
                                                ).TextValue,
                                                new UserIdColumn().Name.TextValue
                                            ),
                                            new LiteralUuid(
                                                new Guid(3, 0, 0, new byte[8])
                                            )
                                        )
                                    ),
                                    new EqualUuidRow(
                                        new FieldUuid(
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new RelationalSchemaWithForeignKeys().Name,
                                                    new UsersTable().Name,
                                                ]
                                            ).TextValue,
                                            new UserIdColumn().Name.TextValue
                                        ),
                                        new LiteralUuid(new Guid(2, 0, 0, new byte[8]))
                                    )
                                )
                            )
                        )
                    )
                ),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over <see
    /// cref="SchemaDataSetWithForeignKeys"/> and <see cref="AuditSchemaDataSet"/>, in no
    /// particular order. foldCase and removeDiacritics are computed with Python
    /// str.casefold and unicodedata, which PostgreSQL 17 does not provide.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("with_accents"), new LongColumnType()),
                    new Column(new String("first_last_name"), new StringColumnType()),
                    new Column(
                        new String("first_plain_last_name"),
                        new StringColumnType()
                    ),
                    new Column(new String("countries"), new StringColumnType()),
                    new Column(new String("any_like_needle"), new BoolColumnType()),
                    new Column(new String("most_common_key"), new StringColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("with_accents"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_last_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("MULLER"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_plain_last_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("MULLER"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("countries"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Chile, PERU, Peru"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("any_like_needle"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("most_common_key"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("muller"))
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
