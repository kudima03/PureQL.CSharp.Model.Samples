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
using Double = Pure.Primitives.Number.Double;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Conversions;

/// <summary>
/// Aggregates schema_with_foreign_keys.users inner joined on user_id to an inline
/// subquery imports of text columns (tier, postal_code and a nullable mobile), where
/// last_login is at most asDatetime of the text "2024-06-04T00:00:00Z", into one row:
/// the count of rows as users, the count of rows whose tier tryAsInteger is not null as
/// numeric_tiers, the max of tryAsInteger of tier as highest_tier, the average of
/// tryAsInteger of postal_code as average_postal_number, asInteger of the text "4" as
/// target, whether the count of rows is at least that asInteger as target_reached, and
/// whether tryAsInteger of mobile is not null for every row as all_mobiles_are_numbers.
/// </summary>
public sealed record ReadingTextInATotalQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainTotalQuery(
            subqueries:
            [
                new SubqueryRows(
                    "imports",
                    [
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(1, 0, 0, new byte[8]))
                            ),
                            new RowItem("tier", new LiteralString("2")),
                            new RowItem("postal_code", new LiteralString("10115")),
                            new RowItem("mobile", new LiteralString("4915112345678")),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(2, 0, 0, new byte[8]))
                            ),
                            new RowItem("tier", new LiteralString("gold")),
                            new RowItem("postal_code", new LiteralString("01234")),
                            new RowItem("mobile", new LiteralString("33612345678")),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(3, 0, 0, new byte[8]))
                            ),
                            new RowItem("tier", new LiteralString("10")),
                            new RowItem("postal_code", new LiteralString("75001")),
                            new RowItem("mobile", new LiteralString("4155550100")),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(4, 0, 0, new byte[8]))
                            ),
                            new RowItem("tier", new LiteralString("5")),
                            new RowItem("postal_code", new LiteralString("2040")),
                            new RowItem("mobile", new LiteralStringNullable()),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(5, 0, 0, new byte[8]))
                            ),
                            new RowItem("tier", new LiteralString("1")),
                            new RowItem("postal_code", new LiteralString("94105")),
                            new RowItem("mobile", new LiteralString("+61412345678")),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(6, 0, 0, new byte[8]))
                            ),
                            new RowItem("tier", new LiteralString(" 4")),
                            new RowItem("postal_code", new LiteralString("n/a")),
                            new RowItem("mobile", new LiteralString("3225551234")),
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
            where: new LessThanOrEqualDatetimeRow(
                new FieldDatetime(
                    new JoinedString(
                        new DotString(),
                        [
                            new RelationalSchemaWithForeignKeys().Name,
                            new UsersTable().Name,
                        ]
                    ).TextValue,
                    new LastLoginColumn().Name.TextValue
                ),
                new AsDatetimeStringDatetimeRow(new LiteralString("2024-06-04T00:00:00Z"))
            ),
            aggregate:
            [
                new SelectItemTotal("users", new CountTotal()),
                new SelectItemTotal(
                    "numeric_tiers",
                    new CountTotal(
                        new NotEqualDecimalRow(
                            new TryAsIntegerRow(new FieldString("imports", "tier")),
                            new LiteralIntegerNullable()
                        )
                    )
                ),
                new SelectItemTotal(
                    "highest_tier",
                    new MaxIntegerNullableTotal(
                        new TryAsIntegerRow(new FieldString("imports", "tier"))
                    )
                ),
                new SelectItemTotal(
                    "average_postal_number",
                    new AverageDecimalNullableTotal(
                        new TryAsIntegerRow(new FieldString("imports", "postal_code"))
                    )
                ),
                new SelectItemTotal(
                    "target",
                    new AsIntegerIntegerTotal(new LiteralString("4"))
                ),
                new SelectItemTotal(
                    "target_reached",
                    new GreaterThanOrEqualDecimalTotal(
                        new CountTotal(),
                        new AsIntegerIntegerTotal(new LiteralString("4"))
                    )
                ),
                new SelectItemTotal(
                    "all_mobiles_are_numbers",
                    new AllTotal(
                        new NotEqualDecimalRow(
                            new TryAsIntegerRow(
                                new FieldStringNullable("imports", "mobile")
                            ),
                            new LiteralIntegerNullable()
                        )
                    )
                ),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and
    /// <see cref="AuditSchemaDataSet"/>, in no particular order.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("users"), new LongColumnType()),
                    new Column(new String("numeric_tiers"), new LongColumnType()),
                    new Column(new String("highest_tier"), new LongColumnType()),
                    new Column(
                        new String("average_postal_number"),
                        new DoubleColumnType()
                    ),
                    new Column(new String("target"), new LongColumnType()),
                    new Column(new String("target_reached"), new BoolColumnType()),
                    new Column(
                        new String("all_mobiles_are_numbers"),
                        new BoolColumnType()
                    ),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("users"), new LongColumnType()),
                                new InvariantCell(new Long(5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("numeric_tiers"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("highest_tier"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(10))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("average_postal_number"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(29052))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("target"), new LongColumnType()),
                                new InvariantCell(new Long(4))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("target_reached"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("all_mobiles_are_numbers"),
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
