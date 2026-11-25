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
/// Aggregates schema_with_foreign_keys.users into one row: the count of users whose
/// stored last name differs from itself in the form NFC as stored_decomposed, the min of
/// the last name in the form NFC as first_last_name, the min of the last name in the form
/// NFC as first_normalized_last_name, the distinct countries in the form NFKC joined by a
/// comma and a space in their own order as countries, whether every first name equals
/// itself in the form NFC as all_composed, and the mode of the case-folded last name in
/// the form NFKC as most_common_key. The where keeps the users whose country in the form
/// NFC differs from Bresil written with a combining acute accent in the form NFC. The
/// last name, first name and country are stand-ins built from user_id, user_tenant_id and
/// user_name.
/// </summary>
public sealed record NormalizeInATotalQuery
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
                new NormalizeStringRow(
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
                    ),
                    NormalizationForm.NFC
                ),
                new NormalizeStringRow(
                    new LiteralString("Bre\u0301sil"),
                    NormalizationForm.NFC
                )
            ),
            aggregate:
            [
                new SelectItemTotal(
                    "stored_decomposed",
                    new CountTotal(
                        new NotEqualStringRow(
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
                                    new LiteralUuid(new Guid(2, 0, 0, new byte[8]))
                                )
                            ),
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
                                ),
                                NormalizationForm.NFC
                            )
                        )
                    )
                ),
                new SelectItemTotal(
                    "first_last_name",
                    new NormalizeStringNullableTotal(
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
                                    new LiteralUuid(new Guid(2, 0, 0, new byte[8]))
                                )
                            )
                        ),
                        NormalizationForm.NFC
                    )
                ),
                new SelectItemTotal(
                    "first_normalized_last_name",
                    new MinStringNullableTotal(
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
                                    new LiteralUuid(new Guid(2, 0, 0, new byte[8]))
                                )
                            ),
                            NormalizationForm.NFC
                        )
                    )
                ),
                new SelectItemTotal(
                    "countries",
                    new ConcatAllTotal(
                        new NormalizeStringRow(
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
                                    new LiteralUuid(new Guid(6, 0, 0, new byte[8]))
                                )
                            ),
                            NormalizationForm.NFKC
                        ),
                        new LiteralString(", "),
                        [
                            new OrderItemRow(
                                new NormalizeStringRow(
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
                                                        new Guid(
                                                            2,
                                                            0,
                                                            0,
                                                            new byte[8]
                                                        )
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
                                    NormalizationForm.NFKC
                                )
                            ),
                        ],
                        distinct: true
                    )
                ),
                new SelectItemTotal(
                    "all_composed",
                    new AllTotal(
                        new EqualStringRow(
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
                                        new UserIdColumn().Name.TextValue
                                    ),
                                    new LiteralUuid(new Guid(3, 0, 0, new byte[8]))
                                )
                            ),
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
                                            new UserIdColumn().Name.TextValue
                                        ),
                                        new LiteralUuid(
                                            new Guid(3, 0, 0, new byte[8])
                                        )
                                    )
                                ),
                                NormalizationForm.NFC
                            )
                        )
                    )
                ),
                new SelectItemTotal(
                    "most_common_key",
                    new ModeStringNullableTotal(
                        new NormalizeStringRow(
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
                            ),
                            NormalizationForm.NFKC
                        )
                    )
                ),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over <see
    /// cref="SchemaDataSetWithForeignKeys"/> and <see cref="AuditSchemaDataSet"/>, in no
    /// particular order. foldCase is computed with Python str.casefold, which PostgreSQL
    /// 17 does not provide; normalize is PostgreSQL's, checked with Python unicodedata.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("stored_decomposed"), new LongColumnType()),
                    new Column(new String("first_last_name"), new StringColumnType()),
                    new Column(
                        new String("first_normalized_last_name"),
                        new StringColumnType()
                    ),
                    new Column(new String("countries"), new StringColumnType()),
                    new Column(new String("all_composed"), new BoolColumnType()),
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
                                    new String("stored_decomposed"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_last_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("LEF\u00C8VRE"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_normalized_last_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("LEF\u00C8VRE"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("countries"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(
                                    new String("Bresil, Chile, Peru, P\u00C9RU")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("all_composed"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("most_common_key"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("m\u00FCller"))
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
