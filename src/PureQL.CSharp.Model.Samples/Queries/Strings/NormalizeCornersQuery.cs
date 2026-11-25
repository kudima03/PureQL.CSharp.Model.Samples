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
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Strings;

/// <summary>
/// Selects, from schema_with_foreign_keys.users, the last name in each of the forms NFC,
/// NFD, NFKC and NFKD, in the form NFC twice, in the form NFD and then NFC, a ligature fi
/// literal in the form NFKC, a typed null in the form NFD, the first name, a space and
/// the last name in the form NFC, the last name case-folded in the form NFC, the
/// case-folded decomposed last name in the form NFD, the number of code points the
/// decomposition adds, whether the last name in the form NFC contains e with a combining
/// grave accent followed by vr in the form NFC, the first letter of the first name in the
/// form NFC, whether the decomposed last name has a character outside letters, space,
/// apostrophe and hyphen, the nickname in the form NFC or else the first name in the form
/// NFC, and the row_number among the users whose case-folded last names are equal in the
/// form NFKC, ordered by the first name in the form NFC then user_id. The last name,
/// first name and nickname are stand-ins built from user_id, user_tenant_id and
/// user_name. The orderBy is the case-folded last name in the form NFKC then user_id.
/// </summary>
public sealed record NormalizeCornersQuery
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
            select:
            [
                new SelectItemProjection(
                    "in_nfc",
                    new NormalizeStringProjection(
                        new IfStringProjection(
                            new LiteralString("LEF\u00C8VRE"),
                            new IfStringProjection(
                                new LiteralString("Lef\u00E8vre"),
                                new IfStringProjection(
                                    new LiteralString("MULLER"),
                                    new IfStringProjection(
                                        new LiteralString("Lefe\u0300vre"),
                                        new LiteralString("Mu\u0308ller"),
                                        new EqualUuidProjection(
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
                                            new LiteralUuid(
                                                new Guid(901, 0, 0, new byte[8])
                                            )
                                        )
                                    ),
                                    new EqualUuidProjection(
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
                                new EqualUuidProjection(
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
                            new EqualUuidProjection(
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
                    )
                ),
                new SelectItemProjection(
                    "in_nfd",
                    new NormalizeStringProjection(
                        new IfStringProjection(
                            new LiteralString("LEF\u00C8VRE"),
                            new IfStringProjection(
                                new LiteralString("Lef\u00E8vre"),
                                new IfStringProjection(
                                    new LiteralString("MULLER"),
                                    new IfStringProjection(
                                        new LiteralString("Lefe\u0300vre"),
                                        new LiteralString("Mu\u0308ller"),
                                        new EqualUuidProjection(
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
                                            new LiteralUuid(
                                                new Guid(901, 0, 0, new byte[8])
                                            )
                                        )
                                    ),
                                    new EqualUuidProjection(
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
                                new EqualUuidProjection(
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
                            new EqualUuidProjection(
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
                        NormalizationForm.NFD
                    )
                ),
                new SelectItemProjection(
                    "in_nfkc",
                    new NormalizeStringProjection(
                        new IfStringProjection(
                            new LiteralString("LEF\u00C8VRE"),
                            new IfStringProjection(
                                new LiteralString("Lef\u00E8vre"),
                                new IfStringProjection(
                                    new LiteralString("MULLER"),
                                    new IfStringProjection(
                                        new LiteralString("Lefe\u0300vre"),
                                        new LiteralString("Mu\u0308ller"),
                                        new EqualUuidProjection(
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
                                            new LiteralUuid(
                                                new Guid(901, 0, 0, new byte[8])
                                            )
                                        )
                                    ),
                                    new EqualUuidProjection(
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
                                new EqualUuidProjection(
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
                            new EqualUuidProjection(
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
                        NormalizationForm.NFKC
                    )
                ),
                new SelectItemProjection(
                    "in_nfkd",
                    new NormalizeStringProjection(
                        new IfStringProjection(
                            new LiteralString("LEF\u00C8VRE"),
                            new IfStringProjection(
                                new LiteralString("Lef\u00E8vre"),
                                new IfStringProjection(
                                    new LiteralString("MULLER"),
                                    new IfStringProjection(
                                        new LiteralString("Lefe\u0300vre"),
                                        new LiteralString("Mu\u0308ller"),
                                        new EqualUuidProjection(
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
                                            new LiteralUuid(
                                                new Guid(901, 0, 0, new byte[8])
                                            )
                                        )
                                    ),
                                    new EqualUuidProjection(
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
                                new EqualUuidProjection(
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
                            new EqualUuidProjection(
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
                        NormalizationForm.NFKD
                    )
                ),
                new SelectItemProjection(
                    "normalized_twice",
                    new NormalizeStringProjection(
                        new NormalizeStringProjection(
                            new IfStringProjection(
                                new LiteralString("LEF\u00C8VRE"),
                                new IfStringProjection(
                                    new LiteralString("Lef\u00E8vre"),
                                    new IfStringProjection(
                                        new LiteralString("MULLER"),
                                        new IfStringProjection(
                                            new LiteralString("Lefe\u0300vre"),
                                            new LiteralString("Mu\u0308ller"),
                                            new EqualUuidProjection(
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
                                        new EqualUuidProjection(
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
                                    new EqualUuidProjection(
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
                                new EqualUuidProjection(
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
                        NormalizationForm.NFC
                    )
                ),
                new SelectItemProjection(
                    "composed_after_decomposing",
                    new NormalizeStringProjection(
                        new NormalizeStringProjection(
                            new IfStringProjection(
                                new LiteralString("LEF\u00C8VRE"),
                                new IfStringProjection(
                                    new LiteralString("Lef\u00E8vre"),
                                    new IfStringProjection(
                                        new LiteralString("MULLER"),
                                        new IfStringProjection(
                                            new LiteralString("Lefe\u0300vre"),
                                            new LiteralString("Mu\u0308ller"),
                                            new EqualUuidProjection(
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
                                        new EqualUuidProjection(
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
                                    new EqualUuidProjection(
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
                                new EqualUuidProjection(
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
                            NormalizationForm.NFD
                        ),
                        NormalizationForm.NFC
                    )
                ),
                new SelectItemProjection(
                    "of_a_literal",
                    new NormalizeStringProjection(
                        new LiteralString("\uFB01anc\u00E9"),
                        NormalizationForm.NFKC
                    )
                ),
                new SelectItemProjection(
                    "of_a_nullable_literal",
                    new NormalizeStringNullableProjection(
                        new LiteralStringNullable(),
                        NormalizationForm.NFD
                    )
                ),
                new SelectItemProjection(
                    "of_a_concat",
                    new NormalizeStringProjection(
                        new ConcatStringProjection([
                            new IfStringProjection(
                                new LiteralString("\u00C1NN"),
                                new IfStringProjection(
                                    new LiteralString("E\u0301ve"),
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
                                    new EqualUuidProjection(
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
                                new EqualUuidProjection(
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
                            new LiteralString(" "),
                            new IfStringProjection(
                                new LiteralString("LEF\u00C8VRE"),
                                new IfStringProjection(
                                    new LiteralString("Lef\u00E8vre"),
                                    new IfStringProjection(
                                        new LiteralString("MULLER"),
                                        new IfStringProjection(
                                            new LiteralString("Lefe\u0300vre"),
                                            new LiteralString("Mu\u0308ller"),
                                            new EqualUuidProjection(
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
                                        new EqualUuidProjection(
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
                                    new EqualUuidProjection(
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
                                new EqualUuidProjection(
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
                        ]),
                        NormalizationForm.NFC
                    )
                ),
                new SelectItemProjection(
                    "fold_then_normalize",
                    new NormalizeStringProjection(
                        new FoldCaseStringProjection(
                            new IfStringProjection(
                                new LiteralString("LEF\u00C8VRE"),
                                new IfStringProjection(
                                    new LiteralString("Lef\u00E8vre"),
                                    new IfStringProjection(
                                        new LiteralString("MULLER"),
                                        new IfStringProjection(
                                            new LiteralString("Lefe\u0300vre"),
                                            new LiteralString("Mu\u0308ller"),
                                            new EqualUuidProjection(
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
                                        new EqualUuidProjection(
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
                                    new EqualUuidProjection(
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
                                new EqualUuidProjection(
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
                        ),
                        NormalizationForm.NFC
                    )
                ),
                new SelectItemProjection(
                    "canonical_caseless_key",
                    new NormalizeStringProjection(
                        new FoldCaseStringProjection(
                            new NormalizeStringProjection(
                                new IfStringProjection(
                                    new LiteralString("LEF\u00C8VRE"),
                                    new IfStringProjection(
                                        new LiteralString("Lef\u00E8vre"),
                                        new IfStringProjection(
                                            new LiteralString("MULLER"),
                                            new IfStringProjection(
                                                new LiteralString("Lefe\u0300vre"),
                                                new LiteralString("Mu\u0308ller"),
                                                new EqualUuidProjection(
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
                                            new EqualUuidProjection(
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
                                        new EqualUuidProjection(
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
                                    new EqualUuidProjection(
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
                                NormalizationForm.NFD
                            )
                        ),
                        NormalizationForm.NFD
                    )
                ),
                new SelectItemProjection(
                    "accents_as_code_points",
                    new SubtractIntegerProjection([
                        new LengthIntegerProjection(
                            new NormalizeStringProjection(
                                new IfStringProjection(
                                    new LiteralString("LEF\u00C8VRE"),
                                    new IfStringProjection(
                                        new LiteralString("Lef\u00E8vre"),
                                        new IfStringProjection(
                                            new LiteralString("MULLER"),
                                            new IfStringProjection(
                                                new LiteralString("Lefe\u0300vre"),
                                                new LiteralString("Mu\u0308ller"),
                                                new EqualUuidProjection(
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
                                            new EqualUuidProjection(
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
                                        new EqualUuidProjection(
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
                                    new EqualUuidProjection(
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
                                NormalizationForm.NFD
                            )
                        ),
                        new LengthIntegerProjection(
                            new NormalizeStringProjection(
                                new IfStringProjection(
                                    new LiteralString("LEF\u00C8VRE"),
                                    new IfStringProjection(
                                        new LiteralString("Lef\u00E8vre"),
                                        new IfStringProjection(
                                            new LiteralString("MULLER"),
                                            new IfStringProjection(
                                                new LiteralString("Lefe\u0300vre"),
                                                new LiteralString("Mu\u0308ller"),
                                                new EqualUuidProjection(
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
                                            new EqualUuidProjection(
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
                                        new EqualUuidProjection(
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
                                    new EqualUuidProjection(
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
                                NormalizationForm.NFC
                            )
                        ),
                    ])
                ),
                new SelectItemProjection(
                    "contains_needle",
                    new ContainsProjection(
                        new NormalizeStringProjection(
                            new IfStringProjection(
                                new LiteralString("LEF\u00C8VRE"),
                                new IfStringProjection(
                                    new LiteralString("Lef\u00E8vre"),
                                    new IfStringProjection(
                                        new LiteralString("MULLER"),
                                        new IfStringProjection(
                                            new LiteralString("Lefe\u0300vre"),
                                            new LiteralString("Mu\u0308ller"),
                                            new EqualUuidProjection(
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
                                        new EqualUuidProjection(
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
                                    new EqualUuidProjection(
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
                                new EqualUuidProjection(
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
                        new NormalizeStringProjection(
                            new LiteralString("e\u0300vr"),
                            NormalizationForm.NFC
                        )
                    )
                ),
                new SelectItemProjection(
                    "first_letter",
                    new SubstringStringProjection(
                        new NormalizeStringProjection(
                            new IfStringProjection(
                                new LiteralString("\u00C1NN"),
                                new IfStringProjection(
                                    new LiteralString("E\u0301ve"),
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
                                    new EqualUuidProjection(
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
                                new EqualUuidProjection(
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
                            NormalizationForm.NFC
                        ),
                        new LiteralInteger(1),
                        new LiteralInteger(1)
                    )
                ),
                new SelectItemProjection(
                    "has_combining_marks",
                    new MatchesRegexProjection(
                        new NormalizeStringProjection(
                            new IfStringProjection(
                                new LiteralString("LEF\u00C8VRE"),
                                new IfStringProjection(
                                    new LiteralString("Lef\u00E8vre"),
                                    new IfStringProjection(
                                        new LiteralString("MULLER"),
                                        new IfStringProjection(
                                            new LiteralString("Lefe\u0300vre"),
                                            new LiteralString("Mu\u0308ller"),
                                            new EqualUuidProjection(
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
                                        new EqualUuidProjection(
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
                                    new EqualUuidProjection(
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
                                new EqualUuidProjection(
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
                            NormalizationForm.NFD
                        ),
                        new LiteralString("[^a-zA-Z '-]")
                    )
                ),
                new SelectItemProjection(
                    "name_or_nickname",
                    new CoalesceStringProjection([
                        new NormalizeStringNullableProjection(
                            new IfStringNullableProjection(
                                new IfStringNullableProjection(
                                    new LiteralStringNullable(),
                                    new LiteralString("Zo\u00EB"),
                                    new EqualUuidProjection(
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
                                new LiteralStringNullable(),
                                new EqualUuidProjection(
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
                                    new LiteralUuid(
                                        new Guid(901, 0, 0, new byte[8])
                                    )
                                )
                            ),
                            NormalizationForm.NFC
                        ),
                        new NormalizeStringProjection(
                            new IfStringProjection(
                                new LiteralString("\u00C1NN"),
                                new IfStringProjection(
                                    new LiteralString("E\u0301ve"),
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
                                    new EqualUuidProjection(
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
                                new EqualUuidProjection(
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
                            NormalizationForm.NFC
                        ),
                    ])
                ),
                new SelectItemProjection(
                    "position_among_namesakes",
                    new RowNumberProjection(
                        new OrderedWindow(
                            [
                                new OrderItemRow(
                                    new NormalizeStringRow(
                                        new IfStringRow(
                                            new LiteralString("\u00C1NN"),
                                            new IfStringRow(
                                                new LiteralString("E\u0301ve"),
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
                                                        new Guid(
                                                            5,
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
                                                    new Guid(3, 0, 0, new byte[8])
                                                )
                                            )
                                        ),
                                        NormalizationForm.NFC
                                    )
                                ),
                                new OrderItemRow(
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
                            ],
                            [
                                new NormalizeStringRow(
                                    new FoldCaseStringRow(
                                        new IfStringRow(
                                            new LiteralString("LEF\u00C8VRE"),
                                            new IfStringRow(
                                                new LiteralString("Lef\u00E8vre"),
                                                new IfStringRow(
                                                    new LiteralString("MULLER"),
                                                    new IfStringRow(
                                                        new LiteralString(
                                                            "Lefe\u0300vre"
                                                        ),
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
                                                            new UserIdColumn()
                                                                .Name
                                                                .TextValue
                                                        ),
                                                        new LiteralUuid(
                                                            new Guid(
                                                                6,
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
                                                        new Guid(
                                                            3,
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
                                                    new Guid(2, 0, 0, new byte[8])
                                                )
                                            )
                                        )
                                    ),
                                    NormalizationForm.NFKC
                                ),
                            ]
                        )
                    )
                ),
            ],
            orderBy:
            [
                new OrderItemProjectionExpression(
                    new NormalizeStringProjection(
                        new FoldCaseStringProjection(
                            new IfStringProjection(
                                new LiteralString("LEF\u00C8VRE"),
                                new IfStringProjection(
                                    new LiteralString("Lef\u00E8vre"),
                                    new IfStringProjection(
                                        new LiteralString("MULLER"),
                                        new IfStringProjection(
                                            new LiteralString("Lefe\u0300vre"),
                                            new LiteralString("Mu\u0308ller"),
                                            new EqualUuidProjection(
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
                                        new EqualUuidProjection(
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
                                    new EqualUuidProjection(
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
                                new EqualUuidProjection(
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
                        ),
                        NormalizationForm.NFKC
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
    /// The rows the query returns under the PureQL specification's semantics over <see
    /// cref="SchemaDataSetWithForeignKeys"/> and <see cref="AuditSchemaDataSet"/>, in
    /// this order. foldCase is computed with Python str.casefold, which PostgreSQL 17
    /// does not provide; normalize is PostgreSQL's, checked with Python unicodedata.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("in_nfc"), new StringColumnType()),
                    new Column(new String("in_nfd"), new StringColumnType()),
                    new Column(new String("in_nfkc"), new StringColumnType()),
                    new Column(new String("in_nfkd"), new StringColumnType()),
                    new Column(new String("normalized_twice"), new StringColumnType()),
                    new Column(
                        new String("composed_after_decomposing"),
                        new StringColumnType()
                    ),
                    new Column(new String("of_a_literal"), new StringColumnType()),
                    new Column(
                        new String("of_a_nullable_literal"),
                        new StringColumnType()
                    ),
                    new Column(new String("of_a_concat"), new StringColumnType()),
                    new Column(new String("fold_then_normalize"), new StringColumnType()),
                    new Column(
                        new String("canonical_caseless_key"),
                        new StringColumnType()
                    ),
                    new Column(
                        new String("accents_as_code_points"),
                        new LongColumnType()
                    ),
                    new Column(new String("contains_needle"), new BoolColumnType()),
                    new Column(new String("first_letter"), new StringColumnType()),
                    new Column(new String("has_combining_marks"), new BoolColumnType()),
                    new Column(new String("name_or_nickname"), new StringColumnType()),
                    new Column(
                        new String("position_among_namesakes"),
                        new LongColumnType()
                    ),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("in_nfc"), new StringColumnType()),
                                new InvariantCell(new String("Lef\u00E8vre"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("in_nfd"), new StringColumnType()),
                                new InvariantCell(new String("Lefe\u0300vre"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("in_nfkc"), new StringColumnType()),
                                new InvariantCell(new String("Lef\u00E8vre"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("in_nfkd"), new StringColumnType()),
                                new InvariantCell(new String("Lefe\u0300vre"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("normalized_twice"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Lef\u00E8vre"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("composed_after_decomposing"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Lef\u00E8vre"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("fianc\u00E9"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_nullable_literal"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_concat"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Ann Lef\u00E8vre"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("fold_then_normalize"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("lef\u00E8vre"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("canonical_caseless_key"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("lefe\u0300vre"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("accents_as_code_points"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("contains_needle"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_letter"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("A"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("has_combining_marks"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("name_or_nickname"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Zo\u00EB"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_among_namesakes"),
                                    new LongColumnType()
                                ),
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
                                new Column(new String("in_nfc"), new StringColumnType()),
                                new InvariantCell(new String("LEF\u00C8VRE"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("in_nfd"), new StringColumnType()),
                                new InvariantCell(new String("LEFE\u0300VRE"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("in_nfkc"), new StringColumnType()),
                                new InvariantCell(new String("LEF\u00C8VRE"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("in_nfkd"), new StringColumnType()),
                                new InvariantCell(new String("LEFE\u0300VRE"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("normalized_twice"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("LEF\u00C8VRE"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("composed_after_decomposing"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("LEF\u00C8VRE"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("fianc\u00E9"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_nullable_literal"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_concat"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Bob LEF\u00C8VRE"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("fold_then_normalize"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("lef\u00E8vre"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("canonical_caseless_key"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("lefe\u0300vre"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("accents_as_code_points"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("contains_needle"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_letter"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("B"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("has_combining_marks"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("name_or_nickname"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Bob"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_among_namesakes"),
                                    new LongColumnType()
                                ),
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
                                new Column(new String("in_nfc"), new StringColumnType()),
                                new InvariantCell(new String("Lef\u00E8vre"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("in_nfd"), new StringColumnType()),
                                new InvariantCell(new String("Lefe\u0300vre"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("in_nfkc"), new StringColumnType()),
                                new InvariantCell(new String("Lef\u00E8vre"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("in_nfkd"), new StringColumnType()),
                                new InvariantCell(new String("Lefe\u0300vre"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("normalized_twice"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Lef\u00E8vre"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("composed_after_decomposing"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Lef\u00E8vre"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("fianc\u00E9"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_nullable_literal"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_concat"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("\u00C1NN Lef\u00E8vre"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("fold_then_normalize"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("lef\u00E8vre"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("canonical_caseless_key"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("lefe\u0300vre"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("accents_as_code_points"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("contains_needle"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_letter"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("\u00C1"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("has_combining_marks"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("name_or_nickname"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Zo\u00EB"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_among_namesakes"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(3))
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
                                new Column(new String("in_nfc"), new StringColumnType()),
                                new InvariantCell(new String("MULLER"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("in_nfd"), new StringColumnType()),
                                new InvariantCell(new String("MULLER"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("in_nfkc"), new StringColumnType()),
                                new InvariantCell(new String("MULLER"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("in_nfkd"), new StringColumnType()),
                                new InvariantCell(new String("MULLER"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("normalized_twice"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("MULLER"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("composed_after_decomposing"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("MULLER"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("fianc\u00E9"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_nullable_literal"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_concat"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Fay MULLER"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("fold_then_normalize"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("muller"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("canonical_caseless_key"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("muller"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("accents_as_code_points"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("contains_needle"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_letter"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("F"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("has_combining_marks"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("name_or_nickname"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Fay"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_among_namesakes"),
                                    new LongColumnType()
                                ),
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
                                new Column(new String("in_nfc"), new StringColumnType()),
                                new InvariantCell(new String("M\u00FCller"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("in_nfd"), new StringColumnType()),
                                new InvariantCell(new String("Mu\u0308ller"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("in_nfkc"), new StringColumnType()),
                                new InvariantCell(new String("M\u00FCller"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("in_nfkd"), new StringColumnType()),
                                new InvariantCell(new String("Mu\u0308ller"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("normalized_twice"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("M\u00FCller"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("composed_after_decomposing"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("M\u00FCller"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("fianc\u00E9"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_nullable_literal"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_concat"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Dan M\u00FCller"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("fold_then_normalize"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("m\u00FCller"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("canonical_caseless_key"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("mu\u0308ller"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("accents_as_code_points"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("contains_needle"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_letter"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("D"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("has_combining_marks"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("name_or_nickname"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Dan"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_among_namesakes"),
                                    new LongColumnType()
                                ),
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
                                new Column(new String("in_nfc"), new StringColumnType()),
                                new InvariantCell(new String("M\u00FCller"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("in_nfd"), new StringColumnType()),
                                new InvariantCell(new String("Mu\u0308ller"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("in_nfkc"), new StringColumnType()),
                                new InvariantCell(new String("M\u00FCller"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("in_nfkd"), new StringColumnType()),
                                new InvariantCell(new String("Mu\u0308ller"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("normalized_twice"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("M\u00FCller"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("composed_after_decomposing"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("M\u00FCller"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("fianc\u00E9"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_nullable_literal"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_concat"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("\u00C9ve M\u00FCller"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("fold_then_normalize"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("m\u00FCller"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("canonical_caseless_key"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("mu\u0308ller"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("accents_as_code_points"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("contains_needle"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_letter"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("\u00C9"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("has_combining_marks"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("name_or_nickname"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("\u00C9ve"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_among_namesakes"),
                                    new LongColumnType()
                                ),
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
