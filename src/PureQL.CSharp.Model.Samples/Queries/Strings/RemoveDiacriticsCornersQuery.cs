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
/// Selects, from schema_with_foreign_keys.users, the last name without accents twice,
/// Creme Brulee written with accents without them, a typed null without accents, the
/// first name, a space and the last name without accents, the last name case-folded
/// without accents, the last name without accents case-folded, the decomposed last name
/// without accents, whether the last name without accents is already composed, the number
/// of code points the accents add, whether the last name case-folded without accents
/// starts with Lef and an accented E case-folded without accents, whether the last name
/// without accents is a capital letter followed by lower case letters, the first letter
/// of the first name without accents in upper case, the nickname without accents or else
/// the first name without accents, and the row_number among the users whose last names
/// are equal case-folded and without accents, ordered by the first name case-folded and
/// without accents then user_id. The orderBy is the last name case-folded and without
/// accents then user_id. The first name, last name and nickname are stand-ins built from
/// user_id, user_tenant_id and user_name.
/// </summary>
public sealed record RemoveDiacriticsCornersQuery
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
                    "removed_twice",
                    new RemoveDiacriticsStringProjection(
                        new RemoveDiacriticsStringProjection(
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
                        )
                    )
                ),
                new SelectItemProjection(
                    "of_a_literal",
                    new RemoveDiacriticsStringProjection(
                        new LiteralString("Cr\u00E8me Br\u00FBl\u00E9e")
                    )
                ),
                new SelectItemProjection(
                    "of_a_nullable_literal",
                    new RemoveDiacriticsStringNullableProjection(
                        new LiteralStringNullable()
                    )
                ),
                new SelectItemProjection(
                    "of_a_concat",
                    new RemoveDiacriticsStringProjection(
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
                        ])
                    )
                ),
                new SelectItemProjection(
                    "fold_then_remove",
                    new RemoveDiacriticsStringProjection(
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
                        )
                    )
                ),
                new SelectItemProjection(
                    "remove_then_fold",
                    new FoldCaseStringProjection(
                        new RemoveDiacriticsStringProjection(
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
                        )
                    )
                ),
                new SelectItemProjection(
                    "of_a_decomposed_string",
                    new RemoveDiacriticsStringProjection(
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
                        )
                    )
                ),
                new SelectItemProjection(
                    "is_already_composed",
                    new EqualStringProjection(
                        new RemoveDiacriticsStringProjection(
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
                        new NormalizeStringProjection(
                            new RemoveDiacriticsStringProjection(
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
                                )
                            ),
                            NormalizationForm.NFC
                        )
                    )
                ),
                new SelectItemProjection(
                    "marks_removed",
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
                                new RemoveDiacriticsStringProjection(
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
                                                        new Guid(
                                                            6,
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
                                    )
                                ),
                                NormalizationForm.NFD
                            )
                        ),
                    ])
                ),
                new SelectItemProjection(
                    "starts_with_needle",
                    new StartsWithProjection(
                        new RemoveDiacriticsStringProjection(
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
                                        new LiteralUuid(
                                            new Guid(2, 0, 0, new byte[8])
                                        )
                                    )
                                )
                            )
                        ),
                        new RemoveDiacriticsStringProjection(
                            new FoldCaseStringProjection(new LiteralString("Lef\u00C8"))
                        )
                    )
                ),
                new SelectItemProjection(
                    "ascii_only",
                    new MatchesRegexProjection(
                        new RemoveDiacriticsStringProjection(
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
                        new LiteralString("^[A-Z][a-z]*$")
                    )
                ),
                new SelectItemProjection(
                    "initial",
                    new UpperStringProjection(
                        new SubstringStringProjection(
                            new RemoveDiacriticsStringProjection(
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
                                        new LiteralUuid(
                                            new Guid(3, 0, 0, new byte[8])
                                        )
                                    )
                                )
                            ),
                            new LiteralInteger(1),
                            new LiteralInteger(1)
                        )
                    )
                ),
                new SelectItemProjection(
                    "name_or_nickname",
                    new CoalesceStringProjection([
                        new RemoveDiacriticsStringNullableProjection(
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
                            )
                        ),
                        new RemoveDiacriticsStringProjection(
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
                            )
                        ),
                    ])
                ),
                new SelectItemProjection(
                    "position_among_namesakes",
                    new RowNumberProjection(
                        new OrderedWindow(
                            [
                                new OrderItemRow(
                                    new RemoveDiacriticsStringRow(
                                        new FoldCaseStringRow(
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
                                                        new UserNameColumn()
                                                            .Name
                                                            .TextValue
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
                                                        new Guid(
                                                            3,
                                                            0,
                                                            0,
                                                            new byte[8]
                                                        )
                                                    )
                                                )
                                            )
                                        )
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
                                new RemoveDiacriticsStringRow(
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
                                    )
                                ),
                            ]
                        )
                    )
                ),
            ],
            orderBy:
            [
                new OrderItemProjectionExpression(
                    new RemoveDiacriticsStringProjection(
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
                        )
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
    /// this order. foldCase and removeDiacritics are computed with Python str.casefold
    /// and unicodedata, which PostgreSQL 17 does not provide.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("removed_twice"), new StringColumnType()),
                    new Column(new String("of_a_literal"), new StringColumnType()),
                    new Column(
                        new String("of_a_nullable_literal"),
                        new StringColumnType()
                    ),
                    new Column(new String("of_a_concat"), new StringColumnType()),
                    new Column(new String("fold_then_remove"), new StringColumnType()),
                    new Column(new String("remove_then_fold"), new StringColumnType()),
                    new Column(
                        new String("of_a_decomposed_string"),
                        new StringColumnType()
                    ),
                    new Column(new String("is_already_composed"), new BoolColumnType()),
                    new Column(new String("marks_removed"), new LongColumnType()),
                    new Column(new String("starts_with_needle"), new BoolColumnType()),
                    new Column(new String("ascii_only"), new BoolColumnType()),
                    new Column(new String("initial"), new StringColumnType()),
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
                                new Column(
                                    new String("removed_twice"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Lefevre"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Creme Brulee"))
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
                                new InvariantCell(new String("Ann Lefevre"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("fold_then_remove"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("lefevre"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("remove_then_fold"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("lefevre"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_decomposed_string"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Lefevre"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("is_already_composed"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("marks_removed"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("starts_with_needle"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("ascii_only"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("initial"), new StringColumnType()),
                                new InvariantCell(new String("A"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("name_or_nickname"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Zoe"))
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
                                new Column(
                                    new String("removed_twice"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("LEFEVRE"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Creme Brulee"))
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
                                new InvariantCell(new String("Bob LEFEVRE"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("fold_then_remove"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("lefevre"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("remove_then_fold"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("lefevre"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_decomposed_string"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("LEFEVRE"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("is_already_composed"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("marks_removed"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("starts_with_needle"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("ascii_only"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("initial"), new StringColumnType()),
                                new InvariantCell(new String("B"))
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
                                new Column(
                                    new String("removed_twice"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Lefevre"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Creme Brulee"))
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
                                new InvariantCell(new String("ANN Lefevre"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("fold_then_remove"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("lefevre"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("remove_then_fold"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("lefevre"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_decomposed_string"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Lefevre"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("is_already_composed"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("marks_removed"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("starts_with_needle"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("ascii_only"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("initial"), new StringColumnType()),
                                new InvariantCell(new String("A"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("name_or_nickname"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Zoe"))
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
                                new Column(
                                    new String("removed_twice"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Muller"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Creme Brulee"))
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
                                new InvariantCell(new String("Dan Muller"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("fold_then_remove"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("muller"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("remove_then_fold"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("muller"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_decomposed_string"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Muller"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("is_already_composed"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("marks_removed"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("starts_with_needle"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("ascii_only"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("initial"), new StringColumnType()),
                                new InvariantCell(new String("D"))
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
                                new Column(
                                    new String("removed_twice"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Muller"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Creme Brulee"))
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
                                new InvariantCell(new String("Eve Muller"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("fold_then_remove"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("muller"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("remove_then_fold"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("muller"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_decomposed_string"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Muller"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("is_already_composed"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("marks_removed"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("starts_with_needle"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("ascii_only"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("initial"), new StringColumnType()),
                                new InvariantCell(new String("E"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("name_or_nickname"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Eve"))
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
                                new Column(
                                    new String("removed_twice"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("MULLER"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Creme Brulee"))
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
                                    new String("fold_then_remove"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("muller"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("remove_then_fold"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("muller"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_decomposed_string"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("MULLER"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("is_already_composed"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("marks_removed"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("starts_with_needle"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("ascii_only"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("initial"), new StringColumnType()),
                                new InvariantCell(new String("F"))
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
                                new InvariantCell(new Long(3))
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
