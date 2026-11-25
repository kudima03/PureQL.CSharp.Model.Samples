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
using PureQL.CSharp.Model.RowExpressions;
using PureQL.CSharp.Model.TotalExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Nulls;

/// <summary>
/// Totals schema_with_foreign_keys.users, with the role operands that are aggregates,
/// which are nullable in a total query: the log of the count to the base of the largest
/// rounded user_score, "*" repeated as many times as that largest age, the first 3
/// characters of the least user_name, the least user_name with the least nickname
/// (user_name with "Cara" turned to null) replaced by "-", the part, numbered by the
/// least of the rounded user_score divided by 10, of the greatest address (user_name
/// followed by "@mail.test") split at "@", and the count rounded to tens, which stays
/// non-null.
/// </summary>
public sealed record NullableRoleOperandsInATotalQuery
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
            aggregate:
            [
                new SelectItemTotal(
                    "log_of_count",
                    new LogDecimalNullableTotal(
                        new CountTotal(),
                        new MaxIntegerNullableTotal(
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
                new SelectItemTotal(
                    "oldest_as_stars",
                    new RepeatStringNullableTotal(
                        new LiteralString("*"),
                        new MaxIntegerNullableTotal(
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
                new SelectItemTotal(
                    "first_name_cut",
                    new SubstringStringNullableTotal(
                        new MinStringNullableTotal(
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
                        new LiteralInteger(1),
                        new LiteralInteger(3)
                    )
                ),
                new SelectItemTotal(
                    "without_first_nickname",
                    new ReplaceStringNullableTotal(
                        new MinStringNullableTotal(
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
                        new MinStringNullableTotal(
                            new NullIfEqualStringNullableRow(
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
                                new LiteralString("Cara")
                            )
                        ),
                        new LiteralString("-")
                    )
                ),
                new SelectItemTotal(
                    "part_of_the_longest",
                    new SplitPartStringNullableTotal(
                        new MaxStringNullableTotal(
                            new ConcatStringRow([
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
                                new LiteralString("@mail.test"),
                            ])
                        ),
                        new LiteralString("@"),
                        new MinIntegerNullableTotal(
                            new IntegerDivideIntegerNullableRow(
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
                                ),
                                new LiteralInteger(10)
                            )
                        )
                    )
                ),
                new SelectItemTotal(
                    "still_non_null",
                    new RoundWithDigitsIntegerTotal(
                        new CountTotal(),
                        new LiteralInteger(-1)
                    )
                ),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and <see cref="AuditSchemaDataSet"/>,
    /// in this order. The logarithm is computed exactly with Python decimal, as the other
    /// results of its kind.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("log_of_count"), new DoubleColumnType()),
                    new Column(new String("oldest_as_stars"), new StringColumnType()),
                    new Column(new String("first_name_cut"), new StringColumnType()),
                    new Column(
                        new String("without_first_nickname"),
                        new StringColumnType()
                    ),
                    new Column(new String("part_of_the_longest"), new StringColumnType()),
                    new Column(new String("still_non_null"), new LongColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("log_of_count"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.5268025545616607))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("oldest_as_stars"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(
                                    new String("******************************")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_name_cut"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Ann"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("without_first_nickname"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("-"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("part_of_the_longest"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Fay"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("still_non_null"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(10))
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
