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
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Nulls;

/// <summary>
/// Selects, from schema_with_foreign_keys.users, round of the decimal 2.5 without digits,
/// with digits that are the nullable user_score rounded, taken modulo 3, and with the
/// same digits under halfEven, truncate of 2.5 without digits, user_name from position 2
/// to the end, the same from position 2 for a nullable length, which is the rounded
/// user_score integer divided by 10, user_name from that nullable start, the position of
/// "a" in user_name and from that nullable start, and substring of a nullable nickname
/// (user_name with "Cara" turned to null) from the nullable start for the nullable
/// length, and from 1 for 2: an operand that is absent does not make the result nullable,
/// one that is written and nullable does.
/// </summary>
public sealed record AbsentAndNullableOperandsQuery
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
            select:
            [
                new SelectItemProjection(
                    "round_without_digits",
                    new RoundWithoutDigitsIntegerProjection(new LiteralDecimal(2.5m))
                ),
                new SelectItemProjection(
                    "round_with_nullable_digits",
                    new RoundDecimalNullableProjection(
                        new LiteralDecimal(2.5m),
                        new ModuloIntegerNullableProjection(
                            new RoundWithoutDigitsIntegerNullableProjection(
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
                            new LiteralInteger(3)
                        )
                    )
                ),
                new SelectItemProjection(
                    "round_nullable_half_even",
                    new RoundDecimalNullableProjection(
                        new LiteralDecimal(2.5m),
                        new ModuloIntegerNullableProjection(
                            new RoundWithoutDigitsIntegerNullableProjection(
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
                            new LiteralInteger(3)
                        ),
                        RoundingMode.HalfEven
                    )
                ),
                new SelectItemProjection(
                    "truncate_without_digits",
                    new TruncateWithoutDigitsIntegerProjection(new LiteralDecimal(2.5m))
                ),
                new SelectItemProjection(
                    "substring_to_the_end",
                    new SubstringStringProjection(
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
                        new LiteralInteger(2)
                    )
                ),
                new SelectItemProjection(
                    "substring_nullable_length",
                    new SubstringStringNullableProjection(
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
                        new LiteralInteger(2),
                        new IntegerDivideIntegerNullableProjection(
                            new RoundWithoutDigitsIntegerNullableProjection(
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
                ),
                new SelectItemProjection(
                    "substring_nullable_start",
                    new SubstringStringNullableProjection(
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
                        new IntegerDivideIntegerNullableProjection(
                            new RoundWithoutDigitsIntegerNullableProjection(
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
                ),
                new SelectItemProjection(
                    "position_from_the_start",
                    new PositionIntegerProjection(
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
                        new LiteralString("a")
                    )
                ),
                new SelectItemProjection(
                    "position_nullable_start",
                    new PositionIntegerNullableProjection(
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
                        new LiteralString("a"),
                        new IntegerDivideIntegerNullableProjection(
                            new RoundWithoutDigitsIntegerNullableProjection(
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
                ),
                new SelectItemProjection(
                    "everything_nullable",
                    new SubstringStringNullableProjection(
                        new NullIfEqualStringNullableProjection(
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
                        ),
                        new IntegerDivideIntegerNullableProjection(
                            new RoundWithoutDigitsIntegerNullableProjection(
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
                        ),
                        new IntegerDivideIntegerNullableProjection(
                            new RoundWithoutDigitsIntegerNullableProjection(
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
                ),
                new SelectItemProjection(
                    "only_operand_nullable",
                    new SubstringStringNullableProjection(
                        new NullIfEqualStringNullableProjection(
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
                        ),
                        new LiteralInteger(1),
                        new LiteralInteger(2)
                    )
                ),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and <see cref="AuditSchemaDataSet"/>,
    /// in no particular order.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("round_without_digits"), new LongColumnType()),
                    new Column(
                        new String("round_with_nullable_digits"),
                        new DoubleColumnType()
                    ),
                    new Column(
                        new String("round_nullable_half_even"),
                        new DoubleColumnType()
                    ),
                    new Column(
                        new String("truncate_without_digits"),
                        new LongColumnType()
                    ),
                    new Column(
                        new String("substring_to_the_end"),
                        new StringColumnType()
                    ),
                    new Column(
                        new String("substring_nullable_length"),
                        new StringColumnType()
                    ),
                    new Column(
                        new String("substring_nullable_start"),
                        new StringColumnType()
                    ),
                    new Column(
                        new String("position_from_the_start"),
                        new LongColumnType()
                    ),
                    new Column(
                        new String("position_nullable_start"),
                        new LongColumnType()
                    ),
                    new Column(new String("everything_nullable"), new StringColumnType()),
                    new Column(
                        new String("only_operand_nullable"),
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
                                new Column(
                                    new String("round_without_digits"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("round_with_nullable_digits"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("round_nullable_half_even"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("truncate_without_digits"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("substring_to_the_end"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("nn"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("substring_nullable_length"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("nn"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("substring_nullable_start"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("n"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_from_the_start"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_nullable_start"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("everything_nullable"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("n"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("only_operand_nullable"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("An"))
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
                                    new String("round_without_digits"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("round_with_nullable_digits"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("round_nullable_half_even"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("truncate_without_digits"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("substring_to_the_end"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("ob"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("substring_nullable_length"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("substring_nullable_start"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_from_the_start"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_nullable_start"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("everything_nullable"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("only_operand_nullable"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Bo"))
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
                                    new String("round_without_digits"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("round_with_nullable_digits"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("round_nullable_half_even"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("truncate_without_digits"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("substring_to_the_end"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("ara"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("substring_nullable_length"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("ara"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("substring_nullable_start"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("ra"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_from_the_start"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_nullable_start"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(4))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("everything_nullable"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("only_operand_nullable"),
                                    new StringColumnType()
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
                                new Column(
                                    new String("round_without_digits"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("round_with_nullable_digits"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("round_nullable_half_even"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("truncate_without_digits"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("substring_to_the_end"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("an"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("substring_nullable_length"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("substring_nullable_start"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_from_the_start"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_nullable_start"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("everything_nullable"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("only_operand_nullable"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Da"))
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
                                    new String("round_without_digits"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("round_with_nullable_digits"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(2.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("round_nullable_half_even"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(2.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("truncate_without_digits"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("substring_to_the_end"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("ve"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("substring_nullable_length"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("v"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("substring_nullable_start"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Eve"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_from_the_start"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_nullable_start"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("everything_nullable"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("E"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("only_operand_nullable"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Ev"))
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
                                    new String("round_without_digits"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("round_with_nullable_digits"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(2.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("round_nullable_half_even"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(2.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("truncate_without_digits"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("substring_to_the_end"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("ay"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("substring_nullable_length"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("ay"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("substring_nullable_start"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("ay"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_from_the_start"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_nullable_start"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("everything_nullable"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("ay"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("only_operand_nullable"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Fa"))
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
