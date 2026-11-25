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
using Double = Pure.Primitives.Number.Double;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Nulls;

/// <summary>
/// Groups schema_with_foreign_keys.orders joined to schema_with_foreign_keys.users on
/// order_user_id by country, "D" for the tenant 901 and "F" for any other, and by
/// initials, the first characters of user_name, as many as the rounded user_score, which
/// is null where user_score is. Keeps the groups where the count raised to the average of
/// the rounded user_score is greater than 1, which a group with no age never is, and
/// selects both keys, a bar of "#" repeated as the largest age, the average age rounded
/// to 1 digit, 1.05 raised to the smallest age, country padded at the start to 6 with
/// spaces and padded at the end to the count with ".", ordered by country and initials.
/// </summary>
public sealed record NullableRoleOperandsInAGroupedQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainGroupedQueryWithoutPagination(
            from: new FromEntity(
                new JoinedString(
                    new DotString(),
                    [new RelationalSchemaWithForeignKeys().Name, new OrdersTable().Name]
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
            groupBy:
            [
                new GroupKey(
                    "country",
                    new IfStringRow(
                        new LiteralString("D"),
                        new LiteralString("F"),
                        new EqualUuidRow(
                            new FieldUuid(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new OrdersTable().Name,
                                    ]
                                ).TextValue,
                                new OrderTenantIdColumn().Name.TextValue
                            ),
                            new LiteralUuid(new Guid(901, 0, 0, new byte[8]))
                        )
                    )
                ),
                new GroupKey(
                    "initials",
                    new SubstringStringNullableRow(
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
                        new RoundWithoutDigitsIntegerNullableRow(
                            new FieldDecimalNullable(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new UsersTable().Name,
                                    ]
                                ).TextValue,
                                new UserScoreColumn().Name.TextValue
                            )
                        )
                    )
                ),
            ],
            having: new GreaterThanDecimalGroup(
                new PowerDecimalNullableGroup(
                    new CountGroup(),
                    new AverageDecimalNullableGroup(
                        new RoundWithoutDigitsIntegerNullableRow(
                            new FieldDecimalNullable(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new UsersTable().Name,
                                    ]
                                ).TextValue,
                                new UserScoreColumn().Name.TextValue
                            )
                        )
                    )
                ),
                new LiteralInteger(1)
            ),
            select:
            [
                new SelectItemGroup("country", new KeyString("country")),
                new SelectItemGroup("initials", new KeyStringNullable("initials")),
                new SelectItemGroup(
                    "bar",
                    new RepeatStringNullableGroup(
                        new LiteralString("#"),
                        new MaxIntegerNullableGroup(
                            new RoundWithoutDigitsIntegerNullableRow(
                                new FieldDecimalNullable(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new UsersTable().Name,
                                        ]
                                    ).TextValue,
                                    new UserScoreColumn().Name.TextValue
                                )
                            )
                        )
                    )
                ),
                new SelectItemGroup(
                    "rounded_average",
                    new RoundDecimalNullableGroup(
                        new AverageDecimalNullableGroup(
                            new RoundWithoutDigitsIntegerNullableRow(
                                new FieldDecimalNullable(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new UsersTable().Name,
                                        ]
                                    ).TextValue,
                                    new UserScoreColumn().Name.TextValue
                                )
                            )
                        ),
                        new LiteralInteger(1)
                    )
                ),
                new SelectItemGroup(
                    "growth",
                    new PowerDecimalNullableGroup(
                        new LiteralDecimal(1.05m),
                        new MinIntegerNullableGroup(
                            new RoundWithoutDigitsIntegerNullableRow(
                                new FieldDecimalNullable(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new UsersTable().Name,
                                        ]
                                    ).TextValue,
                                    new UserScoreColumn().Name.TextValue
                                )
                            )
                        )
                    )
                ),
                new SelectItemGroup(
                    "padded_key",
                    new PadStartStringGroup(
                        new KeyString("country"),
                        new LiteralInteger(6),
                        new LiteralString(" ")
                    )
                ),
                new SelectItemGroup(
                    "padded_to_count",
                    new PadEndStringGroup(
                        new KeyString("country"),
                        new CountGroup(),
                        new LiteralString(".")
                    )
                ),
            ],
            orderBy:
            [
                new OrderItemGroupExpression(new KeyString("country")),
                new OrderItemGroupExpression(new KeyStringNullable("initials")),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and <see cref="AuditSchemaDataSet"/>,
    /// in this order. The power is computed exactly with Python decimal, as the other
    /// results of its kind.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("country"), new StringColumnType()),
                    new Column(new String("initials"), new StringColumnType()),
                    new Column(new String("bar"), new StringColumnType()),
                    new Column(new String("rounded_average"), new DoubleColumnType()),
                    new Column(new String("growth"), new DoubleColumnType()),
                    new Column(new String("padded_key"), new StringColumnType()),
                    new Column(new String("padded_to_count"), new StringColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("country"), new StringColumnType()),
                                new InvariantCell(new String("D"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("initials"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Ann"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("bar"), new StringColumnType()),
                                new InvariantCell(
                                    new String("##############################")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rounded_average"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(30))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("growth"), new DoubleColumnType()),
                                new InvariantCell(new Double(4.321942375150662))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("padded_key"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("     D"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("padded_to_count"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("D."))
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
                                new InvariantCell(new String("F"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("initials"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Cara"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("bar"), new StringColumnType()),
                                new InvariantCell(
                                    new String("##############################")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rounded_average"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(30))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("growth"), new DoubleColumnType()),
                                new InvariantCell(new Double(4.321942375150662))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("padded_key"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("     F"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("padded_to_count"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("F."))
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
