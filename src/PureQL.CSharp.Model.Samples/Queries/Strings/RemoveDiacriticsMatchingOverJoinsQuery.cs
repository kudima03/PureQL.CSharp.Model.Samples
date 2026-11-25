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
using PureQL.CSharp.Model.Lists;
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.ProjectionExpressions;
using PureQL.CSharp.Model.RowExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using Guid = Pure.Primitives.Guid.Guid;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Strings;

/// <summary>
/// Selects the user_id of schema_with_foreign_keys.users as id, the user_id of the users
/// it is joined to as other_id, whether the first names of the two are equal case-folded
/// and without accents, the order_status of the orders of the first user without accents,
/// nullable after the left join, and whether it equals shipped case-folded and without
/// accents. The users are joined to schema_with_foreign_keys.users as other when their
/// last names are equal case-folded and without accents and the user_id is less than the
/// other user_id, then left joined to schema_with_foreign_keys.orders on order_user_id.
/// The where keeps the pairs whose first user has a country that, case-folded and without
/// accents, is bresil, ordered by the last name case-folded and without accents, then by
/// the two user_id values and order_status. The first name, last name and country are
/// stand-ins built from user_id, user_tenant_id and user_name.
/// </summary>
public sealed record RemoveDiacriticsMatchingOverJoinsQuery
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
                    JoinKind.Inner,
                    new JoinedString(
                        new DotString(),
                        [
                            new RelationalSchemaWithForeignKeys().Name,
                            new UsersTable().Name,
                        ]
                    ).TextValue,
                    new AndRow([
                        new EqualStringRow(
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
                                                            new System.Guid(
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
                                                        new System.Guid(
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
                                                    new System.Guid(3, 0, 0, new byte[8])
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
                                                new System.Guid(2, 0, 0, new byte[8])
                                            )
                                        )
                                    )
                                )
                            ),
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
                                                            "other",
                                                            new UserTenantIdColumn()
                                                                .Name
                                                                .TextValue
                                                        ),
                                                        new LiteralUuid(
                                                            new System.Guid(
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
                                                        "other",
                                                        new UserIdColumn().Name.TextValue
                                                    ),
                                                    new LiteralUuid(
                                                        new System.Guid(
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
                                                    "other",
                                                    new UserIdColumn().Name.TextValue
                                                ),
                                                new LiteralUuid(
                                                    new System.Guid(3, 0, 0, new byte[8])
                                                )
                                            )
                                        ),
                                        new EqualUuidRow(
                                            new FieldUuid(
                                                "other",
                                                new UserIdColumn().Name.TextValue
                                            ),
                                            new LiteralUuid(
                                                new System.Guid(2, 0, 0, new byte[8])
                                            )
                                        )
                                    )
                                )
                            )
                        ),
                        new LessThanUuidRow(
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
                            new FieldUuid("other", new UserIdColumn().Name.TextValue)
                        ),
                    ]),
                    alias: "other"
                ),
                new JoinEntity(
                    JoinKind.Left,
                    new JoinedString(
                        new DotString(),
                        [
                            new RelationalSchemaWithForeignKeys().Name,
                            new OrdersTable().Name,
                        ]
                    ).TextValue,
                    new EqualUuidRow(
                        new FieldUuid(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrdersTable().Name,
                                ]
                            ).TextValue,
                            new OrderUserIdColumn().Name.TextValue
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
            where: new InStringRow(
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
                                            new System.Guid(2, 0, 0, new byte[8])
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
                                            new System.Guid(5, 0, 0, new byte[8])
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
                                        new System.Guid(901, 0, 0, new byte[8])
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
                                new LiteralUuid(new System.Guid(6, 0, 0, new byte[8]))
                            )
                        )
                    )
                ),
                new ListLiteralString(["bresil"])
            ),
            select:
            [
                new SelectItemProjection(
                    "id",
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
                    "other_id",
                    new FieldUuid("other", new UserIdColumn().Name.TextValue)
                ),
                new SelectItemProjection(
                    "same_first_name",
                    new EqualStringProjection(
                        new RemoveDiacriticsStringProjection(
                            new FoldCaseStringProjection(
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
                                                new System.Guid(5, 0, 0, new byte[8])
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
                                            new System.Guid(3, 0, 0, new byte[8])
                                        )
                                    )
                                )
                            )
                        ),
                        new RemoveDiacriticsStringProjection(
                            new FoldCaseStringProjection(
                                new IfStringProjection(
                                    new LiteralString("\u00C1NN"),
                                    new IfStringProjection(
                                        new LiteralString("E\u0301ve"),
                                        new FieldString(
                                            "other",
                                            new UserNameColumn().Name.TextValue
                                        ),
                                        new EqualUuidProjection(
                                            new FieldUuid(
                                                "other",
                                                new UserIdColumn().Name.TextValue
                                            ),
                                            new LiteralUuid(
                                                new System.Guid(5, 0, 0, new byte[8])
                                            )
                                        )
                                    ),
                                    new EqualUuidProjection(
                                        new FieldUuid(
                                            "other",
                                            new UserIdColumn().Name.TextValue
                                        ),
                                        new LiteralUuid(
                                            new System.Guid(3, 0, 0, new byte[8])
                                        )
                                    )
                                )
                            )
                        )
                    )
                ),
                new SelectItemProjection(
                    "status",
                    new RemoveDiacriticsStringNullableProjection(
                        new FieldStringNullable(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrdersTable().Name,
                                ]
                            ).TextValue,
                            new OrderStatusColumn().Name.TextValue
                        )
                    )
                ),
                new SelectItemProjection(
                    "status_is_shipped",
                    new EqualStringProjection(
                        new RemoveDiacriticsStringNullableProjection(
                            new FoldCaseStringNullableProjection(
                                new FieldStringNullable(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new OrdersTable().Name,
                                        ]
                                    ).TextValue,
                                    new OrderStatusColumn().Name.TextValue
                                )
                            )
                        ),
                        new LiteralString("shipped")
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
                                                    new System.Guid(
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
                                                new System.Guid(6, 0, 0, new byte[8])
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
                                            new System.Guid(3, 0, 0, new byte[8])
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
                                    new LiteralUuid(new System.Guid(2, 0, 0, new byte[8]))
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
                new OrderItemProjectionExpression(
                    new FieldUuid("other", new UserIdColumn().Name.TextValue)
                ),
                new OrderItemProjectionExpression(
                    new FieldStringNullable(
                        new JoinedString(
                            new DotString(),
                            [
                                new RelationalSchemaWithForeignKeys().Name,
                                new OrdersTable().Name,
                            ]
                        ).TextValue,
                        new OrderStatusColumn().Name.TextValue
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
                    new Column(new String("id"), new UuidColumnType()),
                    new Column(new String("other_id"), new UuidColumnType()),
                    new Column(new String("same_first_name"), new BoolColumnType()),
                    new Column(new String("status"), new StringColumnType()),
                    new Column(new String("status_is_shipped"), new BoolColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("id"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000001-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("other_id"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000002-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("same_first_name"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("status"), new StringColumnType()),
                                new InvariantCell(new String("pending"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("status_is_shipped"),
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
                                new Column(new String("id"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000001-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("other_id"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000002-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("same_first_name"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("status"), new StringColumnType()),
                                new InvariantCell(new String("shipped"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("status_is_shipped"),
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
                                new Column(new String("id"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000001-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("other_id"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000003-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("same_first_name"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("status"), new StringColumnType()),
                                new InvariantCell(new String("pending"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("status_is_shipped"),
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
                                new Column(new String("id"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000001-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("other_id"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000003-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("same_first_name"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("status"), new StringColumnType()),
                                new InvariantCell(new String("shipped"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("status_is_shipped"),
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
                                new Column(new String("id"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000002-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("other_id"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000003-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("same_first_name"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("status"), new StringColumnType()),
                                new InvariantCell(new String("shipped"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("status_is_shipped"),
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
