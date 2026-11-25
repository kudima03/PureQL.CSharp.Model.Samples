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
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Strings;

/// <summary>
/// Selects, from schema_with_foreign_keys.users inner joined on user_id to an inline
/// subquery profiles of names (name, first_name, a nullable nickname and a nullable
/// middle_name): name with the empty text replaced by "x", with every space removed,
/// with first_name replaced by "?" and with "Mr. " replaced by "Dr. "; nickname with
/// every underscore replaced by a space; the literal "2024-01-31" with every hyphen
/// replaced by a slash; the position of the empty text in name, of a space searched
/// from the integer 5 and from just after the first space, and of first_name in
/// nickname; the last part of name split at the spaces, its second part, the first part
/// of middle_name split at the hyphens, the first part of name split at the empty text
/// and its second part split at a comma and a space; the integer year of the text of
/// signup_date split at the hyphens; and the first code point of first_name, a dot and
/// a space joined with the last part of name.
/// </summary>
public sealed record ReplacePositionSplitPartCornersQuery
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
                                new LiteralUuid(new Guid(1, 0, 0, new byte[8]))
                            ),
                            new RowItem("name", new LiteralString("Mr. Ann Lee")),
                            new RowItem("first_name", new LiteralString("Ann")),
                            new RowItem("nickname", new LiteralString("Ann_the_Great")),
                            new RowItem("middle_name", new LiteralString("Marie-Claire")),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(2, 0, 0, new byte[8]))
                            ),
                            new RowItem("name", new LiteralString("Bob Stone")),
                            new RowItem("first_name", new LiteralString("Bob")),
                            new RowItem("nickname", new LiteralStringNullable()),
                            new RowItem("middle_name", new LiteralStringNullable()),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(3, 0, 0, new byte[8]))
                            ),
                            new RowItem("name", new LiteralString("Stone, Cara Jo")),
                            new RowItem("first_name", new LiteralString("Cara")),
                            new RowItem("nickname", new LiteralString("the_Cara")),
                            new RowItem("middle_name", new LiteralString("Jo-Anne")),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(4, 0, 0, new byte[8]))
                            ),
                            new RowItem("name", new LiteralString("Mr. Dan Fox")),
                            new RowItem("first_name", new LiteralString("Dan")),
                            new RowItem("nickname", new LiteralString("d_fox")),
                            new RowItem("middle_name", new LiteralString("")),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(5, 0, 0, new byte[8]))
                            ),
                            new RowItem("name", new LiteralString("Eve")),
                            new RowItem("first_name", new LiteralString("Eve")),
                            new RowItem("nickname", new LiteralStringNullable()),
                            new RowItem("middle_name", new LiteralStringNullable()),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(6, 0, 0, new byte[8]))
                            ),
                            new RowItem("name", new LiteralString("Fay Rose Day")),
                            new RowItem("first_name", new LiteralString("Fay")),
                            new RowItem("nickname", new LiteralString("Fay")),
                            new RowItem("middle_name", new LiteralString("Lee")),
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
                    "profiles",
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
                        new FieldUuid("profiles", "user_id")
                    )
                ),
            ],
            select:
            [
                new SelectItemProjection(
                    "empty_pattern_changes_nothing",
                    new ReplaceStringProjection(
                        new FieldString("profiles", "name"),
                        new LiteralString(""),
                        new LiteralString("x")
                    )
                ),
                new SelectItemProjection(
                    "removed",
                    new ReplaceStringProjection(
                        new FieldString("profiles", "name"),
                        new LiteralString(" "),
                        new LiteralString("")
                    )
                ),
                new SelectItemProjection(
                    "pattern_from_a_field",
                    new ReplaceStringProjection(
                        new FieldString("profiles", "name"),
                        new FieldString("profiles", "first_name"),
                        new LiteralString("?")
                    )
                ),
                new SelectItemProjection(
                    "replacement_from_a_literal",
                    new ReplaceStringProjection(
                        new FieldString("profiles", "name"),
                        new LiteralString("Mr. "),
                        new LiteralString("Dr. ")
                    )
                ),
                new SelectItemProjection(
                    "nullable_operand",
                    new ReplaceStringNullableProjection(
                        new FieldStringNullable("profiles", "nickname"),
                        new LiteralString("_"),
                        new LiteralString(" ")
                    )
                ),
                new SelectItemProjection(
                    "replace_in_a_literal",
                    new ReplaceStringProjection(
                        new LiteralString("2024-01-31"),
                        new LiteralString("-"),
                        new LiteralString("/")
                    )
                ),
                new SelectItemProjection(
                    "position_of_the_empty_pattern",
                    new PositionIntegerProjection(
                        new FieldString("profiles", "name"),
                        new LiteralString("")
                    )
                ),
                new SelectItemProjection(
                    "position_from_a_literal_start",
                    new PositionIntegerProjection(
                        new FieldString("profiles", "name"),
                        new LiteralString(" "),
                        new LiteralInteger(5)
                    )
                ),
                new SelectItemProjection(
                    "position_from_a_computed_start",
                    new PositionIntegerProjection(
                        new FieldString("profiles", "name"),
                        new LiteralString(" "),
                        new AddIntegerProjection([
                            new PositionIntegerProjection(
                                new FieldString("profiles", "name"),
                                new LiteralString(" ")
                            ),
                            new LiteralInteger(1),
                        ])
                    )
                ),
                new SelectItemProjection(
                    "position_in_nullable",
                    new PositionIntegerNullableProjection(
                        new FieldStringNullable("profiles", "nickname"),
                        new FieldString("profiles", "first_name")
                    )
                ),
                new SelectItemProjection(
                    "last_part",
                    new SplitPartStringProjection(
                        new FieldString("profiles", "name"),
                        new LiteralString(" "),
                        new LiteralInteger(-1)
                    )
                ),
                new SelectItemProjection(
                    "second_part",
                    new SplitPartStringProjection(
                        new FieldString("profiles", "name"),
                        new LiteralString(" "),
                        new LiteralInteger(2)
                    )
                ),
                new SelectItemProjection(
                    "part_of_nullable",
                    new SplitPartStringNullableProjection(
                        new FieldStringNullable("profiles", "middle_name"),
                        new LiteralString("-"),
                        new LiteralInteger(1)
                    )
                ),
                new SelectItemProjection(
                    "empty_delimiter_gives_one_part",
                    new SplitPartStringProjection(
                        new FieldString("profiles", "name"),
                        new LiteralString(""),
                        new LiteralInteger(1)
                    )
                ),
                new SelectItemProjection(
                    "long_delimiter",
                    new SplitPartStringProjection(
                        new FieldString("profiles", "name"),
                        new LiteralString(", "),
                        new LiteralInteger(2)
                    )
                ),
                new SelectItemProjection(
                    "birth_year",
                    new AsIntegerIntegerProjection(
                        new SplitPartStringProjection(
                            new AsStringDateStringProjection(
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
                            new LiteralString("-"),
                            new LiteralInteger(1)
                        )
                    )
                ),
                new SelectItemProjection(
                    "initial_and_last_name",
                    new ConcatStringProjection([
                        new SubstringStringProjection(
                            new FieldString("profiles", "first_name"),
                            new LiteralInteger(1),
                            new LiteralInteger(1)
                        ),
                        new LiteralString(". "),
                        new SplitPartStringProjection(
                            new FieldString("profiles", "name"),
                            new LiteralString(" "),
                            new LiteralInteger(-1)
                        ),
                    ])
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
                    new Column(
                        new String("empty_pattern_changes_nothing"),
                        new StringColumnType()
                    ),
                    new Column(new String("removed"), new StringColumnType()),
                    new Column(
                        new String("pattern_from_a_field"),
                        new StringColumnType()
                    ),
                    new Column(
                        new String("replacement_from_a_literal"),
                        new StringColumnType()
                    ),
                    new Column(new String("nullable_operand"), new StringColumnType()),
                    new Column(
                        new String("replace_in_a_literal"),
                        new StringColumnType()
                    ),
                    new Column(
                        new String("position_of_the_empty_pattern"),
                        new LongColumnType()
                    ),
                    new Column(
                        new String("position_from_a_literal_start"),
                        new LongColumnType()
                    ),
                    new Column(
                        new String("position_from_a_computed_start"),
                        new LongColumnType()
                    ),
                    new Column(new String("position_in_nullable"), new LongColumnType()),
                    new Column(new String("last_part"), new StringColumnType()),
                    new Column(new String("second_part"), new StringColumnType()),
                    new Column(new String("part_of_nullable"), new StringColumnType()),
                    new Column(
                        new String("empty_delimiter_gives_one_part"),
                        new StringColumnType()
                    ),
                    new Column(new String("long_delimiter"), new StringColumnType()),
                    new Column(new String("birth_year"), new LongColumnType()),
                    new Column(
                        new String("initial_and_last_name"),
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
                                    new String("empty_pattern_changes_nothing"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Mr. Ann Lee"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("removed"), new StringColumnType()),
                                new InvariantCell(new String("Mr.AnnLee"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("pattern_from_a_field"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Mr. ? Lee"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("replacement_from_a_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Dr. Ann Lee"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_operand"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Ann the Great"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("replace_in_a_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("2024/01/31"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_of_the_empty_pattern"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_from_a_literal_start"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(8))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_from_a_computed_start"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(8))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_in_nullable"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_part"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Lee"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("second_part"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Ann"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("part_of_nullable"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Marie"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("empty_delimiter_gives_one_part"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Mr. Ann Lee"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("long_delimiter"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String(""))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("birth_year"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2020))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("initial_and_last_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("A. Lee"))
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
                                    new String("empty_pattern_changes_nothing"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Bob Stone"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("removed"), new StringColumnType()),
                                new InvariantCell(new String("BobStone"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("pattern_from_a_field"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("? Stone"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("replacement_from_a_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Bob Stone"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_operand"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("replace_in_a_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("2024/01/31"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_of_the_empty_pattern"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_from_a_literal_start"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_from_a_computed_start"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_in_nullable"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_part"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Stone"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("second_part"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Stone"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("part_of_nullable"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("empty_delimiter_gives_one_part"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Bob Stone"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("long_delimiter"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String(""))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("birth_year"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2021))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("initial_and_last_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("B. Stone"))
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
                                    new String("empty_pattern_changes_nothing"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Stone, Cara Jo"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("removed"), new StringColumnType()),
                                new InvariantCell(new String("Stone,CaraJo"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("pattern_from_a_field"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Stone, ? Jo"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("replacement_from_a_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Stone, Cara Jo"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_operand"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("the Cara"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("replace_in_a_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("2024/01/31"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_of_the_empty_pattern"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_from_a_literal_start"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(7))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_from_a_computed_start"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(12))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_in_nullable"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_part"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Jo"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("second_part"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Cara"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("part_of_nullable"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Jo"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("empty_delimiter_gives_one_part"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Stone, Cara Jo"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("long_delimiter"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Cara Jo"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("birth_year"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2019))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("initial_and_last_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("C. Jo"))
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
                                    new String("empty_pattern_changes_nothing"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Mr. Dan Fox"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("removed"), new StringColumnType()),
                                new InvariantCell(new String("Mr.DanFox"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("pattern_from_a_field"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Mr. ? Fox"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("replacement_from_a_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Dr. Dan Fox"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_operand"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("d fox"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("replace_in_a_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("2024/01/31"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_of_the_empty_pattern"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_from_a_literal_start"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(8))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_from_a_computed_start"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(8))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_in_nullable"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_part"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Fox"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("second_part"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Dan"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("part_of_nullable"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String(""))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("empty_delimiter_gives_one_part"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Mr. Dan Fox"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("long_delimiter"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String(""))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("birth_year"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2022))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("initial_and_last_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("D. Fox"))
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
                                    new String("empty_pattern_changes_nothing"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Eve"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("removed"), new StringColumnType()),
                                new InvariantCell(new String("Eve"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("pattern_from_a_field"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("?"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("replacement_from_a_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Eve"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_operand"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("replace_in_a_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("2024/01/31"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_of_the_empty_pattern"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_from_a_literal_start"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_from_a_computed_start"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_in_nullable"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_part"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Eve"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("second_part"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String(""))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("part_of_nullable"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("empty_delimiter_gives_one_part"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Eve"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("long_delimiter"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String(""))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("birth_year"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2023))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("initial_and_last_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("E. Eve"))
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
                                    new String("empty_pattern_changes_nothing"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Fay Rose Day"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("removed"), new StringColumnType()),
                                new InvariantCell(new String("FayRoseDay"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("pattern_from_a_field"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("? Rose Day"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("replacement_from_a_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Fay Rose Day"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_operand"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Fay"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("replace_in_a_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("2024/01/31"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_of_the_empty_pattern"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_from_a_literal_start"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(9))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_from_a_computed_start"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(9))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("position_in_nullable"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("last_part"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Day"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("second_part"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Rose"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("part_of_nullable"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Lee"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("empty_delimiter_gives_one_part"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Fay Rose Day"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("long_delimiter"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String(""))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("birth_year"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2020))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("initial_and_last_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("F. Day"))
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
