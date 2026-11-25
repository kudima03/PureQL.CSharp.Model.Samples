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
using PureQL.CSharp.Model.GroupExpressions;
using PureQL.CSharp.Model.Keys;
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.RowExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Strings;

/// <summary>
/// Selects, per the lower-cased domain after the at sign of email and per whether the
/// nickname holds a digit, the domain, that flag, the count of well-formed emails, the
/// least part of email before the at sign, the most digits left in a nullable mobile once
/// the other characters are removed, the top-level label of the domain, whether the
/// domain matches ^example\.com$ and the domain with every lower-case letter replaced by
/// *, from schema_with_foreign_keys.users inner joined on user_id to an inline subquery
/// contacts of emails, nicknames and nullable mobiles. The where keeps the emails that
/// contain an at sign, the having keeps the groups with at least one well-formed email,
/// and the groups are ordered by the count of well-formed emails descending, then by
/// domain and the flag.
/// </summary>
public sealed record UsersPerEmailDomainByRegexQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainGroupedQueryWithoutPagination(
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
                            new RowItem("nickname", new LiteralString("ann")),
                            new RowItem("mobile", new LiteralString("+49 151 1234567")),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(2, 0, 0, new byte[8]))
                            ),
                            new RowItem("email", new LiteralString("bob@Example.com")),
                            new RowItem("nickname", new LiteralString("bob7")),
                            new RowItem("mobile", new LiteralStringNullable()),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(3, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "email",
                                new LiteralString("cara@mail.example.org")
                            ),
                            new RowItem("nickname", new LiteralString("cara")),
                            new RowItem("mobile", new LiteralString("336 1234 5678")),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(4, 0, 0, new byte[8]))
                            ),
                            new RowItem("email", new LiteralString("dan@localhost")),
                            new RowItem("nickname", new LiteralString("dan")),
                            new RowItem("mobile", new LiteralStringNullable()),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(5, 0, 0, new byte[8]))
                            ),
                            new RowItem("email", new LiteralString("eve.at.example.net")),
                            new RowItem("nickname", new LiteralString("eve")),
                            new RowItem("mobile", new LiteralString("415-555-0100")),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(6, 0, 0, new byte[8]))
                            ),
                            new RowItem("email", new LiteralString("fay@example.com")),
                            new RowItem("nickname", new LiteralString("fay")),
                            new RowItem("mobile", new LiteralString("415 555 0100")),
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
            where: new MatchesRegexRow(
                new FieldString("contacts", "email"),
                new LiteralString("@")
            ),
            groupBy:
            [
                new GroupKey(
                    "domain",
                    new LowerStringNullableRow(
                        new ExtractRegexRow(
                            new FieldString("contacts", "email"),
                            new LiteralString("@(.+)$"),
                            group: new LiteralInteger(1)
                        )
                    )
                ),
                new GroupKey(
                    "has_digits",
                    new MatchesRegexRow(
                        new FieldString("contacts", "nickname"),
                        new LiteralString("\\d")
                    )
                ),
            ],
            having: new GreaterThanDecimalGroup(
                new CountGroup(
                    predicate: new MatchesRegexRow(
                        new FieldString("contacts", "email"),
                        new LiteralString("^[^@\\s]+@[^@\\s]+\\.[a-z]{2,}$")
                    )
                ),
                new LiteralInteger(0)
            ),
            select:
            [
                new SelectItemGroup("domain", new KeyStringNullable("domain")),
                new SelectItemGroup("has_digits", new KeyBoolean("has_digits")),
                new SelectItemGroup(
                    "well_formed",
                    new CountGroup(
                        predicate: new MatchesRegexRow(
                            new FieldString("contacts", "email"),
                            new LiteralString("^[^@\\s]+@[^@\\s]+\\.[a-z]{2,}$")
                        )
                    )
                ),
                new SelectItemGroup(
                    "shortest_user_part",
                    new MinStringNullableGroup(
                        new ExtractRegexRow(
                            new FieldString("contacts", "email"),
                            new LiteralString("^[^@]+")
                        )
                    )
                ),
                new SelectItemGroup(
                    "longest_digits",
                    new MaxIntegerNullableGroup(
                        new LengthIntegerNullableRow(
                            new ReplaceRegexStringNullableRow(
                                new FieldStringNullable("contacts", "mobile"),
                                new LiteralString(""),
                                new LiteralString("\\D")
                            )
                        )
                    )
                ),
                new SelectItemGroup(
                    "top_level",
                    new ExtractRegexGroup(
                        new KeyStringNullable("domain"),
                        new LiteralString("\\.([a-z]+)$"),
                        group: new LiteralInteger(1)
                    )
                ),
                new SelectItemGroup(
                    "is_company_domain",
                    new MatchesRegexGroup(
                        new KeyStringNullable("domain"),
                        new LiteralString("^example\\.com$")
                    )
                ),
                new SelectItemGroup(
                    "masked_domain",
                    new ReplaceRegexStringNullableGroup(
                        new KeyStringNullable("domain"),
                        new LiteralString("*"),
                        new LiteralString("[a-z]")
                    )
                ),
            ],
            orderBy:
            [
                new OrderItemGroupExpression(
                    new CountGroup(
                        predicate: new MatchesRegexRow(
                            new FieldString("contacts", "email"),
                            new LiteralString("^[^@\\s]+@[^@\\s]+\\.[a-z]{2,}$")
                        )
                    ),
                    SortDirection.Desc
                ),
                new OrderItemGroupExpression(new KeyStringNullable("domain")),
                new OrderItemGroupExpression(new KeyBoolean("has_digits")),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and
    /// <see cref="AuditSchemaDataSet"/>, in this order.
    /// The matches are computed with Python re over the ASCII classes, with $ read as the end
    /// of the text.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("domain"), new StringColumnType()),
                    new Column(new String("has_digits"), new BoolColumnType()),
                    new Column(new String("well_formed"), new LongColumnType()),
                    new Column(new String("shortest_user_part"), new StringColumnType()),
                    new Column(new String("longest_digits"), new LongColumnType()),
                    new Column(new String("top_level"), new StringColumnType()),
                    new Column(new String("is_company_domain"), new BoolColumnType()),
                    new Column(new String("masked_domain"), new StringColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("domain"), new StringColumnType()),
                                new InvariantCell(new String("example.com"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("has_digits"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("well_formed"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("shortest_user_part"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("ann"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("longest_digits"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(12))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("top_level"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("com"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("is_company_domain"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("masked_domain"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("*******.***"))
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
                                new Column(new String("domain"), new StringColumnType()),
                                new InvariantCell(new String("example.com"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("has_digits"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("well_formed"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("shortest_user_part"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("bob"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("longest_digits"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("top_level"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("com"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("is_company_domain"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("masked_domain"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("*******.***"))
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
                                new Column(new String("domain"), new StringColumnType()),
                                new InvariantCell(new String("mail.example.org"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("has_digits"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("well_formed"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("shortest_user_part"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("cara"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("longest_digits"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(11))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("top_level"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("org"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("is_company_domain"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("masked_domain"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("****.*******.***"))
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
