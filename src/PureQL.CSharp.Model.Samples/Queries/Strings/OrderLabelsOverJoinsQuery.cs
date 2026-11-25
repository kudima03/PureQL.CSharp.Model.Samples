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
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.ProjectionExpressions;
using PureQL.CSharp.Model.RowExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using Guid = Pure.Primitives.Guid.Guid;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Strings;

/// <summary>
/// Selects order_id as id, the last_name and first_name joined by a comma and a space
/// as customer, order_status joined with the nullable coupon code by a colon while
/// skipping a null code as status_and_coupon, the nullable code padded with hyphens to
/// 10 code points as coupon_label, and order_total written with two decimal places (its
/// truncated integer, a dot and the part of the text of its rounding to 2 digits after
/// the dot padded with zeros to 2 code points) as total_with_two_decimals from
/// schema_with_foreign_keys.orders inner joined on order_user_id to an inline subquery
/// profiles of names and postal codes and left joined on order_id to an inline subquery
/// coupons of codes, one of them null. The where keeps the orders whose postal_code,
/// padded with zeros at the start to 5 code points, starts with "0", ordered by the
/// customer, then by order_id.
/// </summary>
public sealed record OrderLabelsOverJoinsQuery
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
                            new RowItem("last_name", new LiteralString("Stone")),
                            new RowItem("first_name", new LiteralString("Ann")),
                            new RowItem("postal_code", new LiteralString("1234")),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new System.Guid(2, 0, 0, new byte[8]))
                            ),
                            new RowItem("last_name", new LiteralString("Day")),
                            new RowItem("first_name", new LiteralString("Bob")),
                            new RowItem("postal_code", new LiteralString("10115")),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new System.Guid(3, 0, 0, new byte[8]))
                            ),
                            new RowItem("last_name", new LiteralString("Lee")),
                            new RowItem("first_name", new LiteralString("Cara")),
                            new RowItem("postal_code", new LiteralString("2040")),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new System.Guid(4, 0, 0, new byte[8]))
                            ),
                            new RowItem("last_name", new LiteralString("Fox")),
                            new RowItem("first_name", new LiteralString("Dan")),
                            new RowItem("postal_code", new LiteralString("94105")),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new System.Guid(5, 0, 0, new byte[8]))
                            ),
                            new RowItem("last_name", new LiteralString("Bell")),
                            new RowItem("first_name", new LiteralString("Eve")),
                            new RowItem("postal_code", new LiteralString("80331")),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new System.Guid(6, 0, 0, new byte[8]))
                            ),
                            new RowItem("last_name", new LiteralString("Roy")),
                            new RowItem("first_name", new LiteralString("Fay")),
                            new RowItem("postal_code", new LiteralString("750")),
                        ],
                    ]
                ),
                new SubqueryRows(
                    "coupons",
                    [
                        [
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new System.Guid(101, 0, 0, new byte[8]))
                            ),
                            new RowItem("code", new LiteralString("SPRING10")),
                        ],
                        [
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new System.Guid(103, 0, 0, new byte[8]))
                            ),
                            new RowItem("code", new LiteralString("GOLD")),
                        ],
                        [
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new System.Guid(104, 0, 0, new byte[8]))
                            ),
                            new RowItem("code", new LiteralStringNullable()),
                        ],
                        [
                            new RowItem(
                                "order_id",
                                new LiteralUuid(new System.Guid(105, 0, 0, new byte[8]))
                            ),
                            new RowItem("code", new LiteralString("VIP")),
                        ],
                    ]
                ),
            ],
            from: new FromEntity(
                new JoinedString(
                    new DotString(),
                    [new RelationalSchemaWithForeignKeys().Name, new OrdersTable().Name]
                ).TextValue
            ),
            joins:
            [
                new JoinSubquery(
                    JoinKind.Inner,
                    "profiles",
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
                        new FieldUuid("profiles", "user_id")
                    )
                ),
                new JoinSubquery(
                    JoinKind.Left,
                    "coupons",
                    new EqualUuidRow(
                        new FieldUuid(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrdersTable().Name,
                                ]
                            ).TextValue,
                            new OrderIdColumn().Name.TextValue
                        ),
                        new FieldUuidNullable("coupons", "order_id")
                    )
                ),
            ],
            where: new StartsWithRow(
                new PadStartStringRow(
                    new FieldString("profiles", "postal_code"),
                    new LiteralInteger(5),
                    new LiteralString("0")
                ),
                new LiteralString("0")
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
                                new OrdersTable().Name,
                            ]
                        ).TextValue,
                        new OrderIdColumn().Name.TextValue
                    )
                ),
                new SelectItemProjection(
                    "customer",
                    new ConcatWithProjection(
                        [
                            new FieldString("profiles", "last_name"),
                            new FieldString("profiles", "first_name"),
                        ],
                        new LiteralString(", ")
                    )
                ),
                new SelectItemProjection(
                    "status_and_coupon",
                    new ConcatWithProjection(
                        [
                            new FieldString(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new OrdersTable().Name,
                                    ]
                                ).TextValue,
                                new OrderStatusColumn().Name.TextValue
                            ),
                            new FieldStringNullable("coupons", "code"),
                        ],
                        new LiteralString(":")
                    )
                ),
                new SelectItemProjection(
                    "coupon_label",
                    new PadEndStringNullableProjection(
                        new FieldStringNullable("coupons", "code"),
                        new LiteralInteger(10),
                        new LiteralString("-")
                    )
                ),
                new SelectItemProjection(
                    "total_with_two_decimals",
                    new ConcatStringProjection([
                        new AsStringDecimalStringProjection(
                            new TruncateWithoutDigitsIntegerProjection(
                                new FieldDecimal(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new OrdersTable().Name,
                                        ]
                                    ).TextValue,
                                    new OrderTotalColumn().Name.TextValue
                                )
                            )
                        ),
                        new LiteralString("."),
                        new PadEndStringProjection(
                            new SplitPartStringProjection(
                                new AsStringDecimalStringProjection(
                                    new RoundDecimalProjection(
                                        new FieldDecimal(
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new RelationalSchemaWithForeignKeys().Name,
                                                    new OrdersTable().Name,
                                                ]
                                            ).TextValue,
                                            new OrderTotalColumn().Name.TextValue
                                        ),
                                        new LiteralInteger(2)
                                    )
                                ),
                                new LiteralString("."),
                                new LiteralInteger(2)
                            ),
                            new LiteralInteger(2),
                            new LiteralString("0")
                        ),
                    ])
                ),
            ],
            orderBy:
            [
                new OrderItemProjectionExpression(
                    new ConcatWithProjection(
                        [
                            new FieldString("profiles", "last_name"),
                            new FieldString("profiles", "first_name"),
                        ],
                        new LiteralString(", ")
                    )
                ),
                new OrderItemProjectionExpression(
                    new FieldUuid(
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
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and
    /// <see cref="AuditSchemaDataSet"/>, in this order.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("id"), new UuidColumnType()),
                    new Column(new String("customer"), new StringColumnType()),
                    new Column(new String("status_and_coupon"), new StringColumnType()),
                    new Column(new String("coupon_label"), new StringColumnType()),
                    new Column(
                        new String("total_with_two_decimals"),
                        new StringColumnType()
                    ),
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
                                            "00000068-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("customer"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Lee, Cara"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("status_and_coupon"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("cancelled"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("coupon_label"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("total_with_two_decimals"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("75.25"))
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
                                            "00000069-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("customer"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Lee, Cara"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("status_and_coupon"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("shipped:VIP"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("coupon_label"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("VIP-------"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("total_with_two_decimals"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("300.00"))
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
                                            "00000065-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("customer"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Stone, Ann"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("status_and_coupon"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("shipped:SPRING10"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("coupon_label"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("SPRING10--"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("total_with_two_decimals"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("100.50"))
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
                                            "00000066-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("customer"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Stone, Ann"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("status_and_coupon"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("pending"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("coupon_label"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("total_with_two_decimals"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("50.00"))
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
