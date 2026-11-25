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
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Strings;

/// <summary>
/// Selects, from schema_with_foreign_keys.users inner joined on user_id to an inline
/// subquery profiles of names (first_name, last_name, a nullable middle_name and
/// nickname, country and postal_code): first_name padded at the start to 10 code points
/// with "-=", padded at the end to 10 with the empty text, padded at the start to 8
/// with spaces, padded at the end to the length of last_name plus 2, padded at the
/// start to the integer -1 with "0" and padded at the end to 12 with country; nickname
/// padded at the start to 8 with dots; first_name repeated 0 times, the literal "ab"
/// repeated 3 times and nickname repeated twice; nickname reversed, first_name reversed
/// twice and the text of signup_date reversed; nickname and middle_name joined by a
/// space, first_name and last_name joined by " &amp; " and by the empty text,
/// first_name, middle_name, last_name, nickname and country joined by "|", and
/// first_name with last_name and postal_code with country, each pair joined by a space,
/// joined by "; ", all of them skipping null values.
/// </summary>
public sealed record PaddingAndJoiningCornersQuery
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
                            new RowItem("first_name", new LiteralString("Ann")),
                            new RowItem("last_name", new LiteralString("Stone")),
                            new RowItem("middle_name", new LiteralString("Marie")),
                            new RowItem("nickname", new LiteralString("annie")),
                            new RowItem("country", new LiteralString("DE")),
                            new RowItem("postal_code", new LiteralString("10115")),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(2, 0, 0, new byte[8]))
                            ),
                            new RowItem("first_name", new LiteralString("Bob")),
                            new RowItem("last_name", new LiteralString("Day")),
                            new RowItem("middle_name", new LiteralStringNullable()),
                            new RowItem("nickname", new LiteralStringNullable()),
                            new RowItem("country", new LiteralString("FR")),
                            new RowItem("postal_code", new LiteralString("75001")),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(3, 0, 0, new byte[8]))
                            ),
                            new RowItem("first_name", new LiteralString("Cara")),
                            new RowItem("last_name", new LiteralString("Lee")),
                            new RowItem("middle_name", new LiteralString("Jo")),
                            new RowItem("nickname", new LiteralString("cl")),
                            new RowItem("country", new LiteralString("PL")),
                            new RowItem("postal_code", new LiteralString("00-950")),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(4, 0, 0, new byte[8]))
                            ),
                            new RowItem("first_name", new LiteralString("Dan")),
                            new RowItem("last_name", new LiteralString("Fox")),
                            new RowItem("middle_name", new LiteralStringNullable()),
                            new RowItem("nickname", new LiteralString("foxy")),
                            new RowItem("country", new LiteralString("US")),
                            new RowItem("postal_code", new LiteralString("94105")),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(5, 0, 0, new byte[8]))
                            ),
                            new RowItem("first_name", new LiteralString("Eve")),
                            new RowItem("last_name", new LiteralString("Bell")),
                            new RowItem("middle_name", new LiteralStringNullable()),
                            new RowItem("nickname", new LiteralStringNullable()),
                            new RowItem("country", new LiteralString("DE")),
                            new RowItem("postal_code", new LiteralString("80331")),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(6, 0, 0, new byte[8]))
                            ),
                            new RowItem("first_name", new LiteralString("Fay")),
                            new RowItem("last_name", new LiteralString("Roy")),
                            new RowItem("middle_name", new LiteralString("Ann")),
                            new RowItem("nickname", new LiteralString("fr")),
                            new RowItem("country", new LiteralString("AT")),
                            new RowItem("postal_code", new LiteralString("1010")),
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
                    "fill_of_several_code_points",
                    new PadStartStringProjection(
                        new FieldString("profiles", "first_name"),
                        new LiteralInteger(10),
                        new LiteralString("-=")
                    )
                ),
                new SelectItemProjection(
                    "empty_fill_adds_nothing",
                    new PadEndStringProjection(
                        new FieldString("profiles", "first_name"),
                        new LiteralInteger(10),
                        new LiteralString("")
                    )
                ),
                new SelectItemProjection(
                    "length_from_a_literal",
                    new PadStartStringProjection(
                        new FieldString("profiles", "first_name"),
                        new LiteralInteger(8),
                        new LiteralString(" ")
                    )
                ),
                new SelectItemProjection(
                    "computed_length",
                    new PadEndStringProjection(
                        new FieldString("profiles", "first_name"),
                        new AddIntegerProjection([
                            new LengthIntegerProjection(
                                new FieldString("profiles", "last_name")
                            ),
                            new LiteralInteger(2),
                        ]),
                        new LiteralString(" ")
                    )
                ),
                new SelectItemProjection(
                    "shorter_length_cuts_nothing",
                    new PadStartStringProjection(
                        new FieldString("profiles", "first_name"),
                        new LiteralInteger(-1),
                        new LiteralString("0")
                    )
                ),
                new SelectItemProjection(
                    "fill_from_a_field",
                    new PadEndStringProjection(
                        new FieldString("profiles", "first_name"),
                        new LiteralInteger(12),
                        new FieldString("profiles", "country")
                    )
                ),
                new SelectItemProjection(
                    "nullable_operand",
                    new PadStartStringNullableProjection(
                        new FieldStringNullable("profiles", "nickname"),
                        new LiteralInteger(8),
                        new LiteralString(".")
                    )
                ),
                new SelectItemProjection(
                    "repeated_zero_times",
                    new RepeatStringProjection(
                        new FieldString("profiles", "first_name"),
                        new LiteralInteger(0)
                    )
                ),
                new SelectItemProjection(
                    "repeated_by_a_literal",
                    new RepeatStringProjection(
                        new LiteralString("ab"),
                        new LiteralInteger(3)
                    )
                ),
                new SelectItemProjection(
                    "repeated_nullable",
                    new RepeatStringNullableProjection(
                        new FieldStringNullable("profiles", "nickname"),
                        new LiteralInteger(2)
                    )
                ),
                new SelectItemProjection(
                    "reversed_nullable",
                    new ReverseStringNullableProjection(
                        new FieldStringNullable("profiles", "nickname")
                    )
                ),
                new SelectItemProjection(
                    "reversed_twice",
                    new ReverseStringProjection(
                        new ReverseStringProjection(
                            new FieldString("profiles", "first_name")
                        )
                    )
                ),
                new SelectItemProjection(
                    "reversed_date_text",
                    new ReverseStringProjection(
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
                        )
                    )
                ),
                new SelectItemProjection(
                    "only_null_values",
                    new ConcatWithProjection(
                        [
                            new FieldStringNullable("profiles", "nickname"),
                            new FieldStringNullable("profiles", "middle_name"),
                        ],
                        new LiteralString(" ")
                    )
                ),
                new SelectItemProjection(
                    "separator_from_a_literal",
                    new ConcatWithProjection(
                        [
                            new FieldString("profiles", "first_name"),
                            new FieldString("profiles", "last_name"),
                        ],
                        new LiteralString(" & ")
                    )
                ),
                new SelectItemProjection(
                    "five_values",
                    new ConcatWithProjection(
                        [
                            new FieldString("profiles", "first_name"),
                            new FieldStringNullable("profiles", "middle_name"),
                            new FieldString("profiles", "last_name"),
                            new FieldStringNullable("profiles", "nickname"),
                            new FieldString("profiles", "country"),
                        ],
                        new LiteralString("|")
                    )
                ),
                new SelectItemProjection(
                    "empty_separator",
                    new ConcatWithProjection(
                        [
                            new FieldString("profiles", "first_name"),
                            new FieldString("profiles", "last_name"),
                        ],
                        new LiteralString("")
                    )
                ),
                new SelectItemProjection(
                    "nested",
                    new ConcatWithProjection(
                        [
                            new ConcatWithProjection(
                                [
                                    new FieldString("profiles", "first_name"),
                                    new FieldString("profiles", "last_name"),
                                ],
                                new LiteralString(" ")
                            ),
                            new ConcatWithProjection(
                                [
                                    new FieldString("profiles", "postal_code"),
                                    new FieldString("profiles", "country"),
                                ],
                                new LiteralString(" ")
                            ),
                        ],
                        new LiteralString("; ")
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
                    new Column(
                        new String("fill_of_several_code_points"),
                        new StringColumnType()
                    ),
                    new Column(
                        new String("empty_fill_adds_nothing"),
                        new StringColumnType()
                    ),
                    new Column(
                        new String("length_from_a_literal"),
                        new StringColumnType()
                    ),
                    new Column(new String("computed_length"), new StringColumnType()),
                    new Column(
                        new String("shorter_length_cuts_nothing"),
                        new StringColumnType()
                    ),
                    new Column(new String("fill_from_a_field"), new StringColumnType()),
                    new Column(new String("nullable_operand"), new StringColumnType()),
                    new Column(new String("repeated_zero_times"), new StringColumnType()),
                    new Column(
                        new String("repeated_by_a_literal"),
                        new StringColumnType()
                    ),
                    new Column(new String("repeated_nullable"), new StringColumnType()),
                    new Column(new String("reversed_nullable"), new StringColumnType()),
                    new Column(new String("reversed_twice"), new StringColumnType()),
                    new Column(new String("reversed_date_text"), new StringColumnType()),
                    new Column(new String("only_null_values"), new StringColumnType()),
                    new Column(
                        new String("separator_from_a_literal"),
                        new StringColumnType()
                    ),
                    new Column(new String("five_values"), new StringColumnType()),
                    new Column(new String("empty_separator"), new StringColumnType()),
                    new Column(new String("nested"), new StringColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("fill_of_several_code_points"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("-=-=-=-Ann"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("empty_fill_adds_nothing"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Ann"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("length_from_a_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("     Ann"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("computed_length"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Ann    "))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("shorter_length_cuts_nothing"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Ann"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("fill_from_a_field"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("AnnDEDEDEDED"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_operand"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("...annie"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("repeated_zero_times"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String(""))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("repeated_by_a_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("ababab"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("repeated_nullable"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("annieannie"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("reversed_nullable"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("einna"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("reversed_twice"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Ann"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("reversed_date_text"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("51-10-0202"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("only_null_values"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("annie Marie"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("separator_from_a_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Ann & Stone"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("five_values"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Ann|Marie|Stone|annie|DE"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("empty_separator"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("AnnStone"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("nested"), new StringColumnType()),
                                new InvariantCell(new String("Ann Stone; 10115 DE"))
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
                                    new String("fill_of_several_code_points"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("-=-=-=-Bob"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("empty_fill_adds_nothing"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Bob"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("length_from_a_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("     Bob"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("computed_length"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Bob  "))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("shorter_length_cuts_nothing"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Bob"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("fill_from_a_field"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("BobFRFRFRFRF"))
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
                                    new String("repeated_zero_times"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String(""))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("repeated_by_a_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("ababab"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("repeated_nullable"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("reversed_nullable"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("reversed_twice"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Bob"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("reversed_date_text"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("02-30-1202"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("only_null_values"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String(""))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("separator_from_a_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Bob & Day"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("five_values"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Bob|Day|FR"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("empty_separator"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("BobDay"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("nested"), new StringColumnType()),
                                new InvariantCell(new String("Bob Day; 75001 FR"))
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
                                    new String("fill_of_several_code_points"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("-=-=-=Cara"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("empty_fill_adds_nothing"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Cara"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("length_from_a_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("    Cara"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("computed_length"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Cara "))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("shorter_length_cuts_nothing"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Cara"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("fill_from_a_field"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("CaraPLPLPLPL"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_operand"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("......cl"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("repeated_zero_times"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String(""))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("repeated_by_a_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("ababab"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("repeated_nullable"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("clcl"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("reversed_nullable"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("lc"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("reversed_twice"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Cara"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("reversed_date_text"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("01-70-9102"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("only_null_values"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("cl Jo"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("separator_from_a_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Cara & Lee"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("five_values"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Cara|Jo|Lee|cl|PL"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("empty_separator"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("CaraLee"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("nested"), new StringColumnType()),
                                new InvariantCell(new String("Cara Lee; 00-950 PL"))
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
                                    new String("fill_of_several_code_points"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("-=-=-=-Dan"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("empty_fill_adds_nothing"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Dan"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("length_from_a_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("     Dan"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("computed_length"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Dan  "))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("shorter_length_cuts_nothing"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Dan"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("fill_from_a_field"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("DanUSUSUSUSU"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_operand"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("....foxy"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("repeated_zero_times"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String(""))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("repeated_by_a_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("ababab"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("repeated_nullable"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("foxyfoxy"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("reversed_nullable"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("yxof"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("reversed_twice"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Dan"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("reversed_date_text"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("50-11-2202"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("only_null_values"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("foxy"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("separator_from_a_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Dan & Fox"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("five_values"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Dan|Fox|foxy|US"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("empty_separator"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("DanFox"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("nested"), new StringColumnType()),
                                new InvariantCell(new String("Dan Fox; 94105 US"))
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
                                    new String("fill_of_several_code_points"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("-=-=-=-Eve"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("empty_fill_adds_nothing"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Eve"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("length_from_a_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("     Eve"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("computed_length"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Eve   "))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("shorter_length_cuts_nothing"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Eve"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("fill_from_a_field"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("EveDEDEDEDED"))
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
                                    new String("repeated_zero_times"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String(""))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("repeated_by_a_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("ababab"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("repeated_nullable"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("reversed_nullable"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("reversed_twice"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Eve"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("reversed_date_text"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("82-20-3202"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("only_null_values"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String(""))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("separator_from_a_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Eve & Bell"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("five_values"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Eve|Bell|DE"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("empty_separator"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("EveBell"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("nested"), new StringColumnType()),
                                new InvariantCell(new String("Eve Bell; 80331 DE"))
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
                                    new String("fill_of_several_code_points"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("-=-=-=-Fay"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("empty_fill_adds_nothing"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Fay"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("length_from_a_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("     Fay"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("computed_length"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Fay  "))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("shorter_length_cuts_nothing"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Fay"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("fill_from_a_field"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("FayATATATATA"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_operand"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("......fr"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("repeated_zero_times"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String(""))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("repeated_by_a_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("ababab"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("repeated_nullable"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("frfr"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("reversed_nullable"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("rf"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("reversed_twice"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Fay"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("reversed_date_text"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("51-10-0202"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("only_null_values"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("fr Ann"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("separator_from_a_literal"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Fay & Roy"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("five_values"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Fay|Ann|Roy|fr|AT"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("empty_separator"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("FayRoy"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("nested"), new StringColumnType()),
                                new InvariantCell(new String("Fay Roy; 1010 AT"))
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
