using Pure.Collections.Generic;
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

namespace PureQL.CSharp.Model.Samples.Queries.Aggregates;

/// <summary>
/// Selects, per country of schema_with_foreign_keys.users, a user being from Sweden when
/// not user_active, from Denmark when user_age is at least 40 and from Norway otherwise,
/// the country; the user_name values joined by ", " in order of signup_date and user_id
/// as first_names; the distinct tiers (gold from age 40, silver from 30, bronze below)
/// joined by "/" in descending order as tiers; the nicknames (the lower-case user_name of
/// an active user, null for the others, so those are skipped) joined by ", " in their own
/// order as nicknames; the upper-case first letters of user_name joined by "" in order of
/// user_name and user_id as initials; the distinct countries of all users joined by ", "
/// in order (over all) as countries_of_all; the e-mail addresses (the lower-case
/// user_name and "@example.com") of the active users joined by "; " in order as
/// active_emails; and the country, ": " and first_names joined as label. Keeps the
/// countries whose first_names are longer than 3 characters, ordered by that length
/// descending, then by country.
/// </summary>
public sealed record ConcatAllNamesPerCountryQuery
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
                    "country",
                    new IfStringRow(
                        new LiteralString("Sweden"),
                        new IfStringRow(
                            new LiteralString("Denmark"),
                            new LiteralString("Norway"),
                            new GreaterThanOrEqualDecimalRow(
                                new FieldDecimal(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new UsersTable().Name,
                                        ]
                                    ).TextValue,
                                    new UserAgeColumn().Name.TextValue
                                ),
                                new LiteralInteger(40)
                            )
                        ),
                        new NotRow(
                            new FieldBoolean(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new UsersTable().Name,
                                    ]
                                ).TextValue,
                                new UserActiveColumn().Name.TextValue
                            )
                        )
                    )
                ),
            ],
            having: new GreaterThanDecimalGroup(
                new LengthIntegerGroup(
                    new ConcatAllGroup(
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
                        new LiteralString(", "),
                        [
                            new OrderItemRow(
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
                        ]
                    )
                ),
                new LiteralInteger(3)
            ),
            select:
            [
                new SelectItemGroup("country", new KeyString("country")),
                new SelectItemGroup(
                    "first_names",
                    new ConcatAllGroup(
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
                        new LiteralString(", "),
                        [
                            new OrderItemRow(
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
                        ]
                    )
                ),
                new SelectItemGroup(
                    "tiers",
                    new ConcatAllGroup(
                        new IfStringRow(
                            new LiteralString("gold"),
                            new IfStringRow(
                                new LiteralString("silver"),
                                new LiteralString("bronze"),
                                new GreaterThanOrEqualDecimalRow(
                                    new FieldDecimal(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new UsersTable().Name,
                                            ]
                                        ).TextValue,
                                        new UserAgeColumn().Name.TextValue
                                    ),
                                    new LiteralInteger(30)
                                )
                            ),
                            new GreaterThanOrEqualDecimalRow(
                                new FieldDecimal(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new UsersTable().Name,
                                        ]
                                    ).TextValue,
                                    new UserAgeColumn().Name.TextValue
                                ),
                                new LiteralInteger(40)
                            )
                        ),
                        new LiteralString("/"),
                        [
                            new OrderItemRow(
                                new IfStringRow(
                                    new LiteralString("gold"),
                                    new IfStringRow(
                                        new LiteralString("silver"),
                                        new LiteralString("bronze"),
                                        new GreaterThanOrEqualDecimalRow(
                                            new FieldDecimal(
                                                new JoinedString(
                                                    new DotString(),
                                                    [
                                                        new RelationalSchemaWithForeignKeys().Name,
                                                        new UsersTable().Name,
                                                    ]
                                                ).TextValue,
                                                new UserAgeColumn().Name.TextValue
                                            ),
                                            new LiteralInteger(30)
                                        )
                                    ),
                                    new GreaterThanOrEqualDecimalRow(
                                        new FieldDecimal(
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new RelationalSchemaWithForeignKeys().Name,
                                                    new UsersTable().Name,
                                                ]
                                            ).TextValue,
                                            new UserAgeColumn().Name.TextValue
                                        ),
                                        new LiteralInteger(40)
                                    )
                                ),
                                SortDirection.Desc
                            ),
                        ],
                        distinct: true
                    )
                ),
                new SelectItemGroup(
                    "nicknames",
                    new ConcatAllGroup(
                        new IfStringNullableRow(
                            new LowerStringRow(
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
                            new LiteralStringNullable(),
                            new FieldBoolean(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new UsersTable().Name,
                                    ]
                                ).TextValue,
                                new UserActiveColumn().Name.TextValue
                            )
                        ),
                        new LiteralString(", "),
                        [
                            new OrderItemRow(
                                new IfStringNullableRow(
                                    new LowerStringRow(
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
                                    new LiteralStringNullable(),
                                    new FieldBoolean(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new UsersTable().Name,
                                            ]
                                        ).TextValue,
                                        new UserActiveColumn().Name.TextValue
                                    )
                                )
                            ),
                        ]
                    )
                ),
                new SelectItemGroup(
                    "initials",
                    new ConcatAllGroup(
                        new UpperStringRow(
                            new SubstringStringRow(
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
                                new LiteralInteger(1),
                                new LiteralInteger(1)
                            )
                        ),
                        new LiteralString(""),
                        [
                            new OrderItemRow(
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
                        ]
                    )
                ),
                new SelectItemGroup(
                    "countries_of_all",
                    new ConcatAllGroup(
                        new IfStringRow(
                            new LiteralString("Sweden"),
                            new IfStringRow(
                                new LiteralString("Denmark"),
                                new LiteralString("Norway"),
                                new GreaterThanOrEqualDecimalRow(
                                    new FieldDecimal(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new UsersTable().Name,
                                            ]
                                        ).TextValue,
                                        new UserAgeColumn().Name.TextValue
                                    ),
                                    new LiteralInteger(40)
                                )
                            ),
                            new NotRow(
                                new FieldBoolean(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new UsersTable().Name,
                                        ]
                                    ).TextValue,
                                    new UserActiveColumn().Name.TextValue
                                )
                            )
                        ),
                        new LiteralString(", "),
                        [
                            new OrderItemRow(
                                new IfStringRow(
                                    new LiteralString("Sweden"),
                                    new IfStringRow(
                                        new LiteralString("Denmark"),
                                        new LiteralString("Norway"),
                                        new GreaterThanOrEqualDecimalRow(
                                            new FieldDecimal(
                                                new JoinedString(
                                                    new DotString(),
                                                    [
                                                        new RelationalSchemaWithForeignKeys().Name,
                                                        new UsersTable().Name,
                                                    ]
                                                ).TextValue,
                                                new UserAgeColumn().Name.TextValue
                                            ),
                                            new LiteralInteger(40)
                                        )
                                    ),
                                    new NotRow(
                                        new FieldBoolean(
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new RelationalSchemaWithForeignKeys().Name,
                                                    new UsersTable().Name,
                                                ]
                                            ).TextValue,
                                            new UserActiveColumn().Name.TextValue
                                        )
                                    )
                                )
                            ),
                        ],
                        over: AggregateOver.All,
                        distinct: true
                    )
                ),
                new SelectItemGroup(
                    "active_emails",
                    new ConcatAllGroup(
                        new ConcatStringRow([
                            new LowerStringRow(
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
                            new LiteralString("@example.com"),
                        ]),
                        new LiteralString("; "),
                        [
                            new OrderItemRow(
                                new ConcatStringRow([
                                    new LowerStringRow(
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
                                    new LiteralString("@example.com"),
                                ])
                            ),
                        ],
                        predicate: new FieldBoolean(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue,
                            new UserActiveColumn().Name.TextValue
                        )
                    )
                ),
                new SelectItemGroup(
                    "label",
                    new ConcatStringGroup([
                        new KeyString("country"),
                        new LiteralString(": "),
                        new ConcatAllGroup(
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
                            new LiteralString(", "),
                            [
                                new OrderItemRow(
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
                            ]
                        ),
                    ])
                ),
            ],
            orderBy:
            [
                new OrderItemGroupExpression(
                    new LengthIntegerGroup(
                        new ConcatAllGroup(
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
                            new LiteralString(", "),
                            [
                                new OrderItemRow(
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
                            ]
                        )
                    ),
                    SortDirection.Desc
                ),
                new OrderItemGroupExpression(new KeyString("country")),
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
                    new Column(new String("country"), new StringColumnType()),
                    new Column(new String("first_names"), new StringColumnType()),
                    new Column(new String("tiers"), new StringColumnType()),
                    new Column(new String("nicknames"), new StringColumnType()),
                    new Column(new String("initials"), new StringColumnType()),
                    new Column(new String("countries_of_all"), new StringColumnType()),
                    new Column(new String("active_emails"), new StringColumnType()),
                    new Column(new String("label"), new StringColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("country"), new StringColumnType()),
                                new InvariantCell(new String("Norway"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_names"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Cara, Ann, Fay"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("tiers"), new StringColumnType()),
                                new InvariantCell(new String("silver/bronze"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nicknames"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("ann, cara, fay"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("initials"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("ACF"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("countries_of_all"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Denmark, Norway, Sweden"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("active_emails"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(
                                    new String(
                                        "ann@example.com; cara@example.com; fay@example.com"
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("label"), new StringColumnType()),
                                new InvariantCell(new String("Norway: Cara, Ann, Fay"))
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
                                new Column(new String("country"), new StringColumnType()),
                                new InvariantCell(new String("Sweden"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_names"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Bob, Eve"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("tiers"), new StringColumnType()),
                                new InvariantCell(new String("bronze"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nicknames"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String(""))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("initials"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("BE"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("countries_of_all"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Denmark, Norway, Sweden"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("active_emails"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String(""))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("label"), new StringColumnType()),
                                new InvariantCell(new String("Sweden: Bob, Eve"))
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
