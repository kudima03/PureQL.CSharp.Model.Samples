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

namespace PureQL.CSharp.Model.Samples.Queries.Joins;

/// <summary>
/// Selects pairs of profiles whose email addresses are the same without regard to case,
/// with the status of the orders of the first of each pair, from a subquery profiles of
/// inline rows (user_id, email, first_name, last_name and tier of six users) inner joined
/// to itself as other on the case-folded emails being equal and the user_id being below
/// the other user_id, and left joined to schema_with_foreign_keys.orders on order_user_id
/// equal to the user_id of the profile. The where keeps the profiles whose case-folded
/// tier is in the list gold, silver. It selects both user_ids and emails, whether the
/// case-folded first names and the case-folded last names are both equal as same_name,
/// the case-folded order_status, nullable after the left join, and whether it equals
/// shipped, ordered by the case-folded email, both user_ids and the order_id.
/// </summary>
public sealed record MatchingWithoutCaseOverJoinsQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithoutPagination(
            subqueries:
            [
                new SubqueryRows(
                    "profiles",
                    [
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new System.Guid(1, 0, 0, new byte[8]))
                            ),
                            new RowItem("email", new LiteralString("Ann@Example.com")),
                            new RowItem("first_name", new LiteralString("Ann")),
                            new RowItem("last_name", new LiteralString("Strau\u00DF")),
                            new RowItem("tier", new LiteralString("Gold")),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new System.Guid(2, 0, 0, new byte[8]))
                            ),
                            new RowItem("email", new LiteralString("ann@EXAMPLE.com")),
                            new RowItem("first_name", new LiteralString("ANN")),
                            new RowItem("last_name", new LiteralString("STRAUSS")),
                            new RowItem("tier", new LiteralString("silver")),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new System.Guid(3, 0, 0, new byte[8]))
                            ),
                            new RowItem("email", new LiteralString("ANN@example.COM")),
                            new RowItem("first_name", new LiteralString("Cara")),
                            new RowItem("last_name", new LiteralString("Lang")),
                            new RowItem("tier", new LiteralString("Silver")),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new System.Guid(4, 0, 0, new byte[8]))
                            ),
                            new RowItem("email", new LiteralString("team@example.com")),
                            new RowItem("first_name", new LiteralString("Dan")),
                            new RowItem("last_name", new LiteralString("Day")),
                            new RowItem("tier", new LiteralString("bronze")),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new System.Guid(5, 0, 0, new byte[8]))
                            ),
                            new RowItem("email", new LiteralString("Team@Example.com")),
                            new RowItem("first_name", new LiteralString("Eve")),
                            new RowItem("last_name", new LiteralString("Ray")),
                            new RowItem("tier", new LiteralString("GOLD")),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new System.Guid(6, 0, 0, new byte[8]))
                            ),
                            new RowItem("email", new LiteralString("TEAM@EXAMPLE.COM")),
                            new RowItem("first_name", new LiteralString("Fay")),
                            new RowItem("last_name", new LiteralString("Poe")),
                            new RowItem("tier", new LiteralString("gold")),
                        ],
                    ]
                ),
            ],
            from: new FromSubquery("profiles"),
            joins:
            [
                new JoinSubquery(
                    JoinKind.Inner,
                    "profiles",
                    new AndRow([
                        new EqualStringRow(
                            new FoldCaseStringRow(new FieldString("profiles", "email")),
                            new FoldCaseStringRow(new FieldString("other", "email"))
                        ),
                        new LessThanUuidRow(
                            new FieldUuid("profiles", "user_id"),
                            new FieldUuid("other", "user_id")
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
                        new FieldUuidNullable(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrdersTable().Name,
                                ]
                            ).TextValue,
                            new OrderUserIdColumn().Name.TextValue
                        ),
                        new FieldUuid("profiles", "user_id")
                    )
                ),
            ],
            where: new InStringRow(
                new FoldCaseStringRow(new FieldString("profiles", "tier")),
                new ListLiteralString(["gold", "silver"])
            ),
            select:
            [
                new SelectItemProjection("id", new FieldUuid("profiles", "user_id")),
                new SelectItemProjection("other_id", new FieldUuid("other", "user_id")),
                new SelectItemProjection("email", new FieldString("profiles", "email")),
                new SelectItemProjection(
                    "other_email",
                    new FieldString("other", "email")
                ),
                new SelectItemProjection(
                    "same_name",
                    new AndProjection([
                        new EqualStringProjection(
                            new FoldCaseStringProjection(
                                new FieldString("profiles", "first_name")
                            ),
                            new FoldCaseStringProjection(
                                new FieldString("other", "first_name")
                            )
                        ),
                        new EqualStringProjection(
                            new FoldCaseStringProjection(
                                new FieldString("profiles", "last_name")
                            ),
                            new FoldCaseStringProjection(
                                new FieldString("other", "last_name")
                            )
                        ),
                    ])
                ),
                new SelectItemProjection(
                    "status_key",
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
                new SelectItemProjection(
                    "is_shipped_status",
                    new EqualStringProjection(
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
                        ),
                        new LiteralString("shipped")
                    )
                ),
            ],
            orderBy:
            [
                new OrderItemProjectionExpression(
                    new FoldCaseStringProjection(new FieldString("profiles", "email"))
                ),
                new OrderItemProjectionExpression(new FieldUuid("profiles", "user_id")),
                new OrderItemProjectionExpression(new FieldUuid("other", "user_id")),
                new OrderItemProjectionExpression(
                    new FieldUuidNullable(
                        new JoinedString(
                            new DotString(),
                            [
                                new RelationalSchemaWithForeignKeys().Name,
                                new OrdersTable().Name,
                            ]
                        ).TextValue,
                        new OrderIdColumn().Name.TextValue
                    )
                ),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over <see
    /// cref="SchemaDataSetWithForeignKeys"/> and <see cref="AuditSchemaDataSet"/>, in
    /// this order. foldCase is computed with Python str.casefold, which PostgreSQL 17
    /// does not have (the sharp s folds to ss).
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("id"), new UuidColumnType()),
                    new Column(new String("other_id"), new UuidColumnType()),
                    new Column(new String("email"), new StringColumnType()),
                    new Column(new String("other_email"), new StringColumnType()),
                    new Column(new String("same_name"), new BoolColumnType()),
                    new Column(new String("status_key"), new StringColumnType()),
                    new Column(new String("is_shipped_status"), new BoolColumnType()),
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
                                new Column(new String("email"), new StringColumnType()),
                                new InvariantCell(new String("Ann@Example.com"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("other_email"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("ann@EXAMPLE.com"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("same_name"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("status_key"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("shipped"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("is_shipped_status"),
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
                                            "00000002-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("email"), new StringColumnType()),
                                new InvariantCell(new String("Ann@Example.com"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("other_email"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("ann@EXAMPLE.com"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("same_name"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("status_key"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("pending"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("is_shipped_status"),
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
                                new Column(new String("email"), new StringColumnType()),
                                new InvariantCell(new String("Ann@Example.com"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("other_email"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("ANN@example.COM"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("same_name"), new BoolColumnType()),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("status_key"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("shipped"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("is_shipped_status"),
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
                                new Column(new String("email"), new StringColumnType()),
                                new InvariantCell(new String("Ann@Example.com"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("other_email"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("ANN@example.COM"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("same_name"), new BoolColumnType()),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("status_key"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("pending"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("is_shipped_status"),
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
                                new Column(new String("email"), new StringColumnType()),
                                new InvariantCell(new String("ann@EXAMPLE.com"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("other_email"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("ANN@example.COM"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("same_name"), new BoolColumnType()),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("status_key"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("shipped"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("is_shipped_status"),
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
                                            "00000005-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("other_id"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000006-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("email"), new StringColumnType()),
                                new InvariantCell(new String("Team@Example.com"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("other_email"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("TEAM@EXAMPLE.COM"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("same_name"), new BoolColumnType()),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("status_key"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("is_shipped_status"),
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
            ]
        );
}
