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

namespace PureQL.CSharp.Model.Samples.Queries.Nulls;

/// <summary>
/// Selects, from schema_with_foreign_keys.users left joined on user_id to an inline
/// subquery settings (a digits, exponent, base, start, length, pattern, replacement,
/// delimiter, index, fill and count for two users), user_id and then, each with one
/// operand named by its role read from settings and so nullable: 2.5 rounded and
/// truncated to digits, 2.5 to the power of exponent, the logarithm of 2.5 to base, "abc"
/// from start for 2 and from 1 for length, "abc" with pattern replaced by "x" and "b"
/// replaced by replacement, the position of pattern in "abc" and of "b" in "abc" from
/// start, "a-b" split at delimiter and at "-", the part index, "abc" padded at the start
/// and at the end to length with "0" and to 7 with fill, "abc" repeated count times and
/// "abc" with the regular expression "b+" replaced by replacement, ordered by user_id.
/// </summary>
public sealed record NullableOperandsByRoleQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithoutPagination(
            subqueries:
            [
                new SubqueryRows(
                    "settings",
                    [
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new System.Guid(1, 0, 0, new byte[8]))
                            ),
                            new RowItem("digits", new LiteralInteger(1)),
                            new RowItem("exponent", new LiteralDecimal(3m)),
                            new RowItem("base", new LiteralDecimal(2m)),
                            new RowItem("start", new LiteralInteger(2)),
                            new RowItem("length", new LiteralInteger(5)),
                            new RowItem("pattern", new LiteralString("b")),
                            new RowItem("replacement", new LiteralString("X")),
                            new RowItem("delimiter", new LiteralString("-")),
                            new RowItem("index", new LiteralInteger(2)),
                            new RowItem("fill", new LiteralString("*")),
                            new RowItem("count", new LiteralInteger(2)),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new System.Guid(4, 0, 0, new byte[8]))
                            ),
                            new RowItem("digits", new LiteralInteger(0)),
                            new RowItem("exponent", new LiteralDecimal(2m)),
                            new RowItem("base", new LiteralDecimal(10m)),
                            new RowItem("start", new LiteralInteger(3)),
                            new RowItem("length", new LiteralInteger(2)),
                            new RowItem("pattern", new LiteralString("c")),
                            new RowItem("replacement", new LiteralString("YY")),
                            new RowItem("delimiter", new LiteralString("b")),
                            new RowItem("index", new LiteralInteger(1)),
                            new RowItem("fill", new LiteralString("xy")),
                            new RowItem("count", new LiteralInteger(0)),
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
                    JoinKind.Left,
                    "settings",
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
                        new FieldUuid("settings", "user_id")
                    )
                ),
            ],
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
                    "round_digits",
                    new RoundDecimalNullableProjection(
                        new LiteralDecimal(2.5m),
                        new FieldIntegerNullable("settings", "digits")
                    )
                ),
                new SelectItemProjection(
                    "truncate_digits",
                    new TruncateDecimalNullableProjection(
                        new LiteralDecimal(2.5m),
                        new FieldIntegerNullable("settings", "digits")
                    )
                ),
                new SelectItemProjection(
                    "power_exponent",
                    new PowerDecimalNullableProjection(
                        new LiteralDecimal(2.5m),
                        new FieldDecimalNullable("settings", "exponent")
                    )
                ),
                new SelectItemProjection(
                    "log_base",
                    new LogDecimalNullableProjection(
                        new LiteralDecimal(2.5m),
                        new FieldDecimalNullable("settings", "base")
                    )
                ),
                new SelectItemProjection(
                    "substring_start",
                    new SubstringStringNullableProjection(
                        new LiteralString("abc"),
                        new FieldIntegerNullable("settings", "start"),
                        new LiteralInteger(2)
                    )
                ),
                new SelectItemProjection(
                    "substring_length",
                    new SubstringStringNullableProjection(
                        new LiteralString("abc"),
                        new LiteralInteger(1),
                        new FieldIntegerNullable("settings", "length")
                    )
                ),
                new SelectItemProjection(
                    "replace_pattern",
                    new ReplaceStringNullableProjection(
                        new LiteralString("abc"),
                        new FieldStringNullable("settings", "pattern"),
                        new LiteralString("x")
                    )
                ),
                new SelectItemProjection(
                    "replace_replacement",
                    new ReplaceStringNullableProjection(
                        new LiteralString("abc"),
                        new LiteralString("b"),
                        new FieldStringNullable("settings", "replacement")
                    )
                ),
                new SelectItemProjection(
                    "position_pattern",
                    new PositionIntegerNullableProjection(
                        new LiteralString("abc"),
                        new FieldStringNullable("settings", "pattern"),
                        new LiteralInteger(1)
                    )
                ),
                new SelectItemProjection(
                    "position_start",
                    new PositionIntegerNullableProjection(
                        new LiteralString("abc"),
                        new LiteralString("b"),
                        new FieldIntegerNullable("settings", "start")
                    )
                ),
                new SelectItemProjection(
                    "splitPart_delimiter",
                    new SplitPartStringNullableProjection(
                        new LiteralString("a-b"),
                        new FieldStringNullable("settings", "delimiter"),
                        new LiteralInteger(1)
                    )
                ),
                new SelectItemProjection(
                    "splitPart_index",
                    new SplitPartStringNullableProjection(
                        new LiteralString("a-b"),
                        new LiteralString("-"),
                        new FieldIntegerNullable("settings", "index")
                    )
                ),
                new SelectItemProjection(
                    "padStart_length",
                    new PadStartStringNullableProjection(
                        new LiteralString("abc"),
                        new FieldIntegerNullable("settings", "length"),
                        new LiteralString("0")
                    )
                ),
                new SelectItemProjection(
                    "padStart_fill",
                    new PadStartStringNullableProjection(
                        new LiteralString("abc"),
                        new LiteralInteger(7),
                        new FieldStringNullable("settings", "fill")
                    )
                ),
                new SelectItemProjection(
                    "padEnd_length",
                    new PadEndStringNullableProjection(
                        new LiteralString("abc"),
                        new FieldIntegerNullable("settings", "length"),
                        new LiteralString("0")
                    )
                ),
                new SelectItemProjection(
                    "padEnd_fill",
                    new PadEndStringNullableProjection(
                        new LiteralString("abc"),
                        new LiteralInteger(7),
                        new FieldStringNullable("settings", "fill")
                    )
                ),
                new SelectItemProjection(
                    "repeat_count",
                    new RepeatStringNullableProjection(
                        new LiteralString("abc"),
                        new FieldIntegerNullable("settings", "count")
                    )
                ),
                new SelectItemProjection(
                    "replaceRegex_replacement",
                    new ReplaceRegexStringNullableProjection(
                        new LiteralString("abc"),
                        new FieldStringNullable("settings", "replacement"),
                        new LiteralString("b+")
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
    /// The logarithms are computed with Python decimal, as the powers are checked, and
    /// rounded as the specification rounds a decimal.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new UserIdColumn(),
                    new Column(new String("round_digits"), new DoubleColumnType()),
                    new Column(new String("truncate_digits"), new DoubleColumnType()),
                    new Column(new String("power_exponent"), new DoubleColumnType()),
                    new Column(new String("log_base"), new DoubleColumnType()),
                    new Column(new String("substring_start"), new StringColumnType()),
                    new Column(new String("substring_length"), new StringColumnType()),
                    new Column(new String("replace_pattern"), new StringColumnType()),
                    new Column(new String("replace_replacement"), new StringColumnType()),
                    new Column(new String("position_pattern"), new LongColumnType()),
                    new Column(new String("position_start"), new LongColumnType()),
                    new Column(new String("splitPart_delimiter"), new StringColumnType()),
                    new Column(new String("splitPart_index"), new StringColumnType()),
                    new Column(new String("padStart_length"), new StringColumnType()),
                    new Column(new String("padStart_fill"), new StringColumnType()),
                    new Column(new String("padEnd_length"), new StringColumnType()),
                    new Column(new String("padEnd_fill"), new StringColumnType()),
                    new Column(new String("repeat_count"), new StringColumnType()),
                    new Column(
                        new String("replaceRegex_replacement"),
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
                                new Column(
                                    new String("round_digits"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(2.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("truncate_digits"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(2.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("power_exponent"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(15.625))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("log_base"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1.3219280948873624))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("substring_start"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("bc"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("substring_length"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("abc"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("replace_pattern"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("axc"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("replace_replacement"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("aXc"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_pattern"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_start"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("splitPart_delimiter"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("a"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("splitPart_index"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("b"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("padStart_length"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("00abc"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("padStart_fill"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("****abc"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("padEnd_length"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("abc00"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("padEnd_fill"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("abc****"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("repeat_count"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("abcabc"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("replaceRegex_replacement"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("aXc"))
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
                                new Column(
                                    new String("round_digits"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("truncate_digits"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("power_exponent"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("log_base"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("substring_start"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("substring_length"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("replace_pattern"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("replace_replacement"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_pattern"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_start"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("splitPart_delimiter"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("splitPart_index"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("padStart_length"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("padStart_fill"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("padEnd_length"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("padEnd_fill"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("repeat_count"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("replaceRegex_replacement"),
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
                                new UserIdColumn(),
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
                                    new String("round_digits"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("truncate_digits"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("power_exponent"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("log_base"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("substring_start"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("substring_length"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("replace_pattern"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("replace_replacement"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_pattern"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_start"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("splitPart_delimiter"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("splitPart_index"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("padStart_length"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("padStart_fill"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("padEnd_length"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("padEnd_fill"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("repeat_count"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("replaceRegex_replacement"),
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
                                new Column(
                                    new String("round_digits"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("truncate_digits"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("power_exponent"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(6.25))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("log_base"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.3979400086720376))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("substring_start"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("c"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("substring_length"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("ab"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("replace_pattern"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("abx"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("replace_replacement"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("aYYc"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_pattern"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_start"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("splitPart_delimiter"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("a-"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("splitPart_index"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("a"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("padStart_length"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("abc"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("padStart_fill"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("xyxyabc"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("padEnd_length"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("abc"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("padEnd_fill"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("abcxyxy"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("repeat_count"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String(""))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("replaceRegex_replacement"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("aYYc"))
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
                                new Column(
                                    new String("round_digits"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("truncate_digits"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("power_exponent"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("log_base"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("substring_start"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("substring_length"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("replace_pattern"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("replace_replacement"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_pattern"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_start"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("splitPart_delimiter"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("splitPart_index"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("padStart_length"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("padStart_fill"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("padEnd_length"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("padEnd_fill"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("repeat_count"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("replaceRegex_replacement"),
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
                                new Column(
                                    new String("round_digits"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("truncate_digits"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("power_exponent"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("log_base"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("substring_start"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("substring_length"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("replace_pattern"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("replace_replacement"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_pattern"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_start"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("splitPart_delimiter"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("splitPart_index"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("padStart_length"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("padStart_fill"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("padEnd_length"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("padEnd_fill"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("repeat_count"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("replaceRegex_replacement"),
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
            ]
        );
}
