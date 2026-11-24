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
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.ProjectionExpressions;
using PureQL.CSharp.Model.RowExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using Guid = Pure.Primitives.Guid.Guid;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Conversions;

/// <summary>
/// Selects from schema_with_foreign_keys.users inner joined on user_id to an inline
/// subquery imports of text columns (tier, postal_code, a nullable mobile and a nullable
/// discount_text), where signup_date is at least asDate of the text "2020-01-01":
/// user_id, user_name, asInteger of tier, tryAsInteger of postal_code, tryAsInteger of
/// postal_code trimmed of spaces with 0 through coalesce, tryAsInteger of mobile, and
/// asDecimal of discount_text, ordered by user_id.
/// </summary>
public sealed record ReadingTextQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithoutPagination(
            subqueries:
            [
                new SubqueryRows(
                    "imports",
                    [
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new System.Guid(1, 0, 0, new byte[8]))
                            ),
                            new RowItem("tier", new LiteralString("1")),
                            new RowItem("postal_code", new LiteralString("10115")),
                            new RowItem("mobile", new LiteralString("4915112345678")),
                            new RowItem("discount_text", new LiteralString("0.1")),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new System.Guid(2, 0, 0, new byte[8]))
                            ),
                            new RowItem("tier", new LiteralString("2")),
                            new RowItem("postal_code", new LiteralString("01234")),
                            new RowItem("mobile", new LiteralStringNullable()),
                            new RowItem("discount_text", new LiteralStringNullable()),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new System.Guid(3, 0, 0, new byte[8]))
                            ),
                            new RowItem("tier", new LiteralString("3")),
                            new RowItem("postal_code", new LiteralString("75001")),
                            new RowItem("mobile", new LiteralString("33612345678")),
                            new RowItem("discount_text", new LiteralString("0.25")),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new System.Guid(4, 0, 0, new byte[8]))
                            ),
                            new RowItem("tier", new LiteralString("1")),
                            new RowItem("postal_code", new LiteralString("2040")),
                            new RowItem("mobile", new LiteralString("+61412345678")),
                            new RowItem("discount_text", new LiteralString("0.05")),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new System.Guid(5, 0, 0, new byte[8]))
                            ),
                            new RowItem("tier", new LiteralString("2")),
                            new RowItem("postal_code", new LiteralString(" 94105")),
                            new RowItem("mobile", new LiteralString("4155550100")),
                            new RowItem("discount_text", new LiteralStringNullable()),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new System.Guid(6, 0, 0, new byte[8]))
                            ),
                            new RowItem("tier", new LiteralString("3")),
                            new RowItem("postal_code", new LiteralString("n/a")),
                            new RowItem("mobile", new LiteralString("415 555 0100")),
                            new RowItem("discount_text", new LiteralString("0.2")),
                        ],
                    ]
                ),
            ],
            from: new FromEntity(
                new JoinedString(
                    new DotString(),
                    [new RelationalSchemaWithForeignKeys().Name, new UsersTable().Name]
                ).TextValue
            ),
            joins:
            [
                new JoinSubquery(
                    JoinKind.Inner,
                    "imports",
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
                        new FieldUuid("imports", "user_id")
                    )
                ),
            ],
            where: new GreaterThanOrEqualDateRow(
                new FieldDate(
                    new JoinedString(
                        new DotString(),
                        [
                            new RelationalSchemaWithForeignKeys().Name,
                            new UsersTable().Name,
                        ]
                    ).TextValue,
                    new SignupDateColumn().Name.TextValue
                ),
                new AsDateStringDateRow(new LiteralString("2020-01-01"))
            ),
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
                    "user_name",
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
                new SelectItemProjection(
                    "tier_number",
                    new AsIntegerIntegerProjection(new FieldString("imports", "tier"))
                ),
                new SelectItemProjection(
                    "postal_number",
                    new TryAsIntegerProjection(new FieldString("imports", "postal_code"))
                ),
                new SelectItemProjection(
                    "postal_number_or_zero",
                    new CoalesceIntegerProjection([
                        new TryAsIntegerProjection(
                            new TrimStringProjection(
                                new FieldString("imports", "postal_code"),
                                [new LiteralString(" ")]
                            )
                        ),
                        new LiteralInteger(0),
                    ])
                ),
                new SelectItemProjection(
                    "mobile_number",
                    new TryAsIntegerProjection(
                        new FieldStringNullable("imports", "mobile")
                    )
                ),
                new SelectItemProjection(
                    "discount",
                    new AsDecimalStringDecimalNullableProjection(
                        new FieldStringNullable("imports", "discount_text")
                    )
                ),
            ],
            orderBy:
            [
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
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new UserIdColumn(),
                    new UserNameColumn(),
                    new Column(new String("tier_number"), new LongColumnType()),
                    new Column(new String("postal_number"), new LongColumnType()),
                    new Column(new String("postal_number_or_zero"), new LongColumnType()),
                    new Column(new String("mobile_number"), new LongColumnType()),
                    new Column(new String("discount"), new DoubleColumnType()),
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
                                new UserNameColumn(),
                                new InvariantCell(new String("Ann"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("tier_number"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("postal_number"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(10115))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("postal_number_or_zero"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(10115))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("mobile_number"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(4915112345678))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("discount"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.1))
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
                                            "00000002-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new UserNameColumn(),
                                new InvariantCell(new String("Bob"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("tier_number"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("postal_number"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("postal_number_or_zero"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("mobile_number"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("discount"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
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
                                            "00000004-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new UserNameColumn(),
                                new InvariantCell(new String("Dan"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("tier_number"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("postal_number"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2040))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("postal_number_or_zero"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2040))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("mobile_number"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("discount"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.05))
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
                                            "00000005-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new UserNameColumn(),
                                new InvariantCell(new String("Eve"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("tier_number"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("postal_number"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("postal_number_or_zero"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(94105))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("mobile_number"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(4155550100))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("discount"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
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
                                new UserNameColumn(),
                                new InvariantCell(new String("Fay"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("tier_number"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("postal_number"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("postal_number_or_zero"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("mobile_number"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("discount"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.2))
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
