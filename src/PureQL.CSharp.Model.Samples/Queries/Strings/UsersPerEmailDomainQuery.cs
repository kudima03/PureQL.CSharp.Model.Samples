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
using PureQL.CSharp.Model.GroupExpressions;
using PureQL.CSharp.Model.Keys;
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.RowExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Strings;

/// <summary>
/// Selects the group key domain, the group key top_level, the count of rows as users,
/// the min of the position of "@" in email minus the integer 1 as shortest_user_part,
/// the count of rows whose email has a "+" as with_plus_tag, the min of first_name
/// without spaces as first_name_without_spaces, the domain with every dot replaced by "
/// dot " as label and the number of dots in the domain as dots_in_domain from
/// schema_with_foreign_keys.users inner joined on user_id to an inline subquery
/// contacts of emails and first names, where the email has an "@". The rows are grouped
/// by domain, the lower-cased part of email after its last "@", and by top_level, the
/// part of that domain after its last dot, keeping the groups whose count is greater
/// than the integer 1, ordered by the count descending, then by domain and top_level.
/// </summary>
public sealed record UsersPerEmailDomainQuery
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
                            new RowItem("email", new LiteralString("ann@Example.com")),
                            new RowItem("first_name", new LiteralString("Ann Marie")),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(1, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "email",
                                new LiteralString("ann.marie+shop@example.com")
                            ),
                            new RowItem("first_name", new LiteralString("Ann Marie")),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(2, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "email",
                                new LiteralString("bob+news@EXAMPLE.com")
                            ),
                            new RowItem("first_name", new LiteralString("Bob")),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(3, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "email",
                                new LiteralString("cara@mail.example.co.uk")
                            ),
                            new RowItem("first_name", new LiteralString("Cara Lee")),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(4, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "email",
                                new LiteralString("daniel@Mail.Example.co.uk")
                            ),
                            new RowItem("first_name", new LiteralString("Dan")),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(5, 0, 0, new byte[8]))
                            ),
                            new RowItem("email", new LiteralString("eve.example.com")),
                            new RowItem("first_name", new LiteralString("Eve")),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(6, 0, 0, new byte[8]))
                            ),
                            new RowItem("email", new LiteralString("fay@example.org")),
                            new RowItem("first_name", new LiteralString("Fay Anne")),
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
            where: new GreaterThanDecimalRow(
                new PositionIntegerRow(
                    new FieldString("contacts", "email"),
                    new LiteralString("@")
                ),
                new LiteralInteger(0)
            ),
            groupBy:
            [
                new GroupKey(
                    "domain",
                    new LowerStringRow(
                        new SplitPartStringRow(
                            new FieldString("contacts", "email"),
                            new LiteralString("@"),
                            new LiteralInteger(-1)
                        )
                    )
                ),
                new GroupKey(
                    "top_level",
                    new SplitPartStringRow(
                        new LowerStringRow(
                            new SplitPartStringRow(
                                new FieldString("contacts", "email"),
                                new LiteralString("@"),
                                new LiteralInteger(-1)
                            )
                        ),
                        new LiteralString("."),
                        new LiteralInteger(-1)
                    )
                ),
            ],
            having: new GreaterThanDecimalGroup(new CountGroup(), new LiteralInteger(1)),
            select:
            [
                new SelectItemGroup("domain", new KeyString("domain")),
                new SelectItemGroup("top_level", new KeyString("top_level")),
                new SelectItemGroup("users", new CountGroup()),
                new SelectItemGroup(
                    "shortest_user_part",
                    new MinIntegerGroup(
                        new SubtractIntegerRow([
                            new PositionIntegerRow(
                                new FieldString("contacts", "email"),
                                new LiteralString("@")
                            ),
                            new LiteralInteger(1),
                        ])
                    )
                ),
                new SelectItemGroup(
                    "with_plus_tag",
                    new CountGroup(
                        new GreaterThanDecimalRow(
                            new PositionIntegerRow(
                                new FieldString("contacts", "email"),
                                new LiteralString("+")
                            ),
                            new LiteralInteger(0)
                        )
                    )
                ),
                new SelectItemGroup(
                    "first_name_without_spaces",
                    new MinStringGroup(
                        new ReplaceStringRow(
                            new FieldString("contacts", "first_name"),
                            new LiteralString(" "),
                            new LiteralString("")
                        )
                    )
                ),
                new SelectItemGroup(
                    "label",
                    new ReplaceStringGroup(
                        new KeyString("domain"),
                        new LiteralString("."),
                        new LiteralString(" dot ")
                    )
                ),
                new SelectItemGroup(
                    "dots_in_domain",
                    new SubtractIntegerGroup([
                        new LengthIntegerGroup(new KeyString("domain")),
                        new LengthIntegerGroup(
                            new ReplaceStringGroup(
                                new KeyString("domain"),
                                new LiteralString("."),
                                new LiteralString("")
                            )
                        ),
                    ])
                ),
            ],
            orderBy:
            [
                new OrderItemGroupExpression(new CountGroup(), SortDirection.Desc),
                new OrderItemGroupExpression(new KeyString("domain")),
                new OrderItemGroupExpression(new KeyString("top_level")),
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
                    new Column(new String("domain"), new StringColumnType()),
                    new Column(new String("top_level"), new StringColumnType()),
                    new Column(new String("users"), new LongColumnType()),
                    new Column(new String("shortest_user_part"), new LongColumnType()),
                    new Column(new String("with_plus_tag"), new LongColumnType()),
                    new Column(
                        new String("first_name_without_spaces"),
                        new StringColumnType()
                    ),
                    new Column(new String("label"), new StringColumnType()),
                    new Column(new String("dots_in_domain"), new LongColumnType()),
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
                                    new String("top_level"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("com"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("users"), new LongColumnType()),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("shortest_user_part"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("with_plus_tag"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_name_without_spaces"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("AnnMarie"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("label"), new StringColumnType()),
                                new InvariantCell(new String("example dot com"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("dots_in_domain"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
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
                                new InvariantCell(new String("mail.example.co.uk"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("top_level"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("uk"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("users"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("shortest_user_part"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(4))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("with_plus_tag"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_name_without_spaces"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("CaraLee"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("label"), new StringColumnType()),
                                new InvariantCell(
                                    new String("mail dot example dot co dot uk")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("dots_in_domain"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(3))
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
