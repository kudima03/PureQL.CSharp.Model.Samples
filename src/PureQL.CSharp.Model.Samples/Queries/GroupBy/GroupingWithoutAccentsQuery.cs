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
using PureQL.CSharp.Model.GroupExpressions;
using PureQL.CSharp.Model.Keys;
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.RowExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.GroupBy;

/// <summary>
/// Groups schema_with_foreign_keys.users by country_key, the country case-folded without
/// accents, and by nickname without accents, nullable. The having keeps the groups with
/// at least one user whose last name in the form NFC differs from the last name without
/// accents. It selects country_key, nickname, the count of users, the distinct countries
/// joined by a bar in their own order as spellings, the min of the last name without
/// accents as first_plain_last_name, and country_key without accents in upper case,
/// ordered by the count descending, country_key and nickname. The last name, country and
/// nickname are stand-ins built from user_id, user_tenant_id and user_name.
/// </summary>
public sealed record GroupingWithoutAccentsQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainGroupedQueryWithoutPagination(
            from: new FromEntity(
                new JoinedString(
                    new DotString(),
                    [new RelationalSchemaWithForeignKeys().Name, new UsersTable().Name]
                ).TextValue
            ),
            groupBy:
            [
                new GroupKey(
                    "country_key",
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
                            )
                        )
                    )
                ),
                new GroupKey(
                    "nickname",
                    new RemoveDiacriticsStringNullableRow(
                        new IfStringNullableRow(
                            new IfStringNullableRow(
                                new LiteralStringNullable(),
                                new LiteralString("Zo\u00EB"),
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
                            new LiteralStringNullable(),
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
                        )
                    )
                ),
            ],
            having: new GreaterThanDecimalGroup(
                new CountGroup(
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
                        )
                    )
                ),
                new LiteralInteger(0)
            ),
            select:
            [
                new SelectItemGroup("country_key", new KeyString("country_key")),
                new SelectItemGroup("nickname", new KeyStringNullable("nickname")),
                new SelectItemGroup("users", new CountGroup()),
                new SelectItemGroup(
                    "spellings",
                    new ConcatAllGroup(
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
                        new LiteralString(" | "),
                        [
                            new OrderItemRow(
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
                                        new LiteralUuid(
                                            new Guid(6, 0, 0, new byte[8])
                                        )
                                    )
                                )
                            ),
                        ],
                        distinct: true
                    )
                ),
                new SelectItemGroup(
                    "first_plain_last_name",
                    new MinStringGroup(
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
                        )
                    )
                ),
                new SelectItemGroup(
                    "key_in_upper_case",
                    new UpperStringGroup(
                        new RemoveDiacriticsStringGroup(new KeyString("country_key"))
                    )
                ),
            ],
            orderBy:
            [
                new OrderItemGroupExpression(new CountGroup(), SortDirection.Desc),
                new OrderItemGroupExpression(new KeyString("country_key")),
                new OrderItemGroupExpression(new KeyStringNullable("nickname")),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over <see
    /// cref="SchemaDataSetWithForeignKeys"/> and <see cref="AuditSchemaDataSet"/>, in
    /// this order. foldCase and removeDiacritics are computed with Python str.casefold
    /// and unicodedata, which PostgreSQL 17 does not provide; normalize is PostgreSQL's,
    /// checked with Python unicodedata.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("country_key"), new StringColumnType()),
                    new Column(new String("nickname"), new StringColumnType()),
                    new Column(new String("users"), new LongColumnType()),
                    new Column(new String("spellings"), new StringColumnType()),
                    new Column(
                        new String("first_plain_last_name"),
                        new StringColumnType()
                    ),
                    new Column(new String("key_in_upper_case"), new StringColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("country_key"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("bresil"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nickname"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Zoe"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("users"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("spellings"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Br\u00E9sil"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_plain_last_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Lefevre"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("key_in_upper_case"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("BRESIL"))
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
                                    new String("country_key"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("peru"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nickname"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("users"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("spellings"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Peru | P\u00C9RU"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_plain_last_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Muller"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("key_in_upper_case"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("PERU"))
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
                                    new String("country_key"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("bresil"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nickname"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("users"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("spellings"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Bresil"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_plain_last_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("LEFEVRE"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("key_in_upper_case"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("BRESIL"))
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
