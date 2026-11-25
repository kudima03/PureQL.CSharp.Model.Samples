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
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Strings;

/// <summary>
/// Aggregates schema_with_foreign_keys.users inner joined on user_id to an inline
/// subquery contacts of emails and full names into one row: the count of emails that
/// match ^[^@\s]+@[^@\s]+\.[a-z]{2,}$ as well_formed_emails, whether every email matches
/// @ as all_emails_have_an_at, the least text after the at sign of an email (null where
/// there is none) as first_domain, its trailing lower-case letters as
/// first_domain_top_level, the greatest length of full_name without its white space as
/// longest_name_without_spaces, and whether the count of rows, as text, matches
/// ^[0-9]{3}$ as count_has_three_digits.
/// </summary>
public sealed record RegularExpressionsInATotalQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainTotalQuery(
            subqueries:
            [
                new SubqueryRows(
                    "contacts",
                    [
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(1, 0, 0, new byte[8]))
                            ),
                            new RowItem("email", new LiteralString("ann@example.com")),
                            new RowItem("full_name", new LiteralString("Ann Lee")),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(2, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "email",
                                new LiteralString("bob@mail.example.org")
                            ),
                            new RowItem(
                                "full_name",
                                new LiteralString("Bob van der Berg")
                            ),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(3, 0, 0, new byte[8]))
                            ),
                            new RowItem("email", new LiteralString("cara@example.net")),
                            new RowItem("full_name", new LiteralString("Cara")),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(4, 0, 0, new byte[8]))
                            ),
                            new RowItem("email", new LiteralString("dan@localhost")),
                            new RowItem("full_name", new LiteralString("Dan O'Neil")),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(5, 0, 0, new byte[8]))
                            ),
                            new RowItem("email", new LiteralString("eve.at.example.net")),
                            new RowItem("full_name", new LiteralString("Eve")),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(6, 0, 0, new byte[8]))
                            ),
                            new RowItem("email", new LiteralString("fay@example.com")),
                            new RowItem("full_name", new LiteralString("Fay Wong")),
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
                    "contacts",
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
                        new FieldUuid("contacts", "user_id")
                    )
                ),
            ],
            aggregate:
            [
                new SelectItemTotal(
                    "well_formed_emails",
                    new CountTotal(
                        predicate: new MatchesRegexRow(
                            new FieldString("contacts", "email"),
                            new LiteralString("^[^@\\s]+@[^@\\s]+\\.[a-z]{2,}$")
                        )
                    )
                ),
                new SelectItemTotal(
                    "all_emails_have_an_at",
                    new AllTotal(
                        new MatchesRegexRow(
                            new FieldString("contacts", "email"),
                            new LiteralString("@")
                        )
                    )
                ),
                new SelectItemTotal(
                    "first_domain",
                    new MinStringNullableTotal(
                        new ExtractRegexRow(
                            new FieldString("contacts", "email"),
                            new LiteralString("@(.+)$"),
                            group: new LiteralInteger(1)
                        )
                    )
                ),
                new SelectItemTotal(
                    "first_domain_top_level",
                    new ExtractRegexTotal(
                        new MinStringNullableTotal(
                            new ExtractRegexRow(
                                new FieldString("contacts", "email"),
                                new LiteralString("@(.+)$"),
                                group: new LiteralInteger(1)
                            )
                        ),
                        new LiteralString("[a-z]+$")
                    )
                ),
                new SelectItemTotal(
                    "longest_name_without_spaces",
                    new MaxIntegerNullableTotal(
                        new LengthIntegerRow(
                            new ReplaceRegexStringRow(
                                new FieldString("contacts", "full_name"),
                                new LiteralString(""),
                                new LiteralString("\\s")
                            )
                        )
                    )
                ),
                new SelectItemTotal(
                    "count_has_three_digits",
                    new MatchesRegexTotal(
                        new AsStringDecimalStringTotal(new CountTotal()),
                        new LiteralString("^[0-9]{3}$")
                    )
                ),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and
    /// <see cref="AuditSchemaDataSet"/>, in no particular order.
    /// The matches are computed with Python re over the ASCII classes, with $ read as the end
    /// of the text.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("well_formed_emails"), new LongColumnType()),
                    new Column(new String("all_emails_have_an_at"), new BoolColumnType()),
                    new Column(new String("first_domain"), new StringColumnType()),
                    new Column(
                        new String("first_domain_top_level"),
                        new StringColumnType()
                    ),
                    new Column(
                        new String("longest_name_without_spaces"),
                        new LongColumnType()
                    ),
                    new Column(
                        new String("count_has_three_digits"),
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
                                new Column(
                                    new String("well_formed_emails"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(4))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("all_emails_have_an_at"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_domain"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("example.com"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_domain_top_level"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("com"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("longest_name_without_spaces"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(13))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("count_has_three_digits"),
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
