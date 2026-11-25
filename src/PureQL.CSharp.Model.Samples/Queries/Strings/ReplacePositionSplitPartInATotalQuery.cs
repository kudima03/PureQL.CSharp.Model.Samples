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
/// subquery contacts of emails into one row: the count of rows whose position of "@" in
/// email is the integer 0 as emails_without_at, the max of the length of the last part
/// of email split at the "@" as longest_domain, the min of that part as first_domain,
/// that min with every dot replaced by " dot " as first_domain_label, the length of the
/// text of the count of rows with every "0" replaced by nothing as digits_in_count, and
/// whether the position of "@" in email is greater than the integer 1 for every row as
/// all_have_a_user_part.
/// </summary>
public sealed record ReplacePositionSplitPartInATotalQuery
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
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(1, 0, 0, new byte[8]))
                            ),
                            new RowItem(
                                "email",
                                new LiteralString("ann.lee@work.example.org")
                            ),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(2, 0, 0, new byte[8]))
                            ),
                            new RowItem("email", new LiteralString("bob@example.com")),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(3, 0, 0, new byte[8]))
                            ),
                            new RowItem("email", new LiteralString("cara@mail.co.uk")),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(3, 0, 0, new byte[8]))
                            ),
                            new RowItem("email", new LiteralString("cara.mail")),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(4, 0, 0, new byte[8]))
                            ),
                            new RowItem("email", new LiteralString("dan@dan.net")),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(5, 0, 0, new byte[8]))
                            ),
                            new RowItem("email", new LiteralString("@eve.org")),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(5, 0, 0, new byte[8]))
                            ),
                            new RowItem("email", new LiteralString("eve@eve.org")),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(6, 0, 0, new byte[8]))
                            ),
                            new RowItem("email", new LiteralString("fay@x.io")),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(6, 0, 0, new byte[8]))
                            ),
                            new RowItem("email", new LiteralString("fay@example.com")),
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
                    "emails_without_at",
                    new CountTotal(
                        new EqualDecimalRow(
                            new PositionIntegerRow(
                                new FieldString("contacts", "email"),
                                new LiteralString("@")
                            ),
                            new LiteralInteger(0)
                        )
                    )
                ),
                new SelectItemTotal(
                    "longest_domain",
                    new MaxIntegerNullableTotal(
                        new LengthIntegerRow(
                            new SplitPartStringRow(
                                new FieldString("contacts", "email"),
                                new LiteralString("@"),
                                new LiteralInteger(-1)
                            )
                        )
                    )
                ),
                new SelectItemTotal(
                    "first_domain",
                    new MinStringNullableTotal(
                        new SplitPartStringRow(
                            new FieldString("contacts", "email"),
                            new LiteralString("@"),
                            new LiteralInteger(-1)
                        )
                    )
                ),
                new SelectItemTotal(
                    "first_domain_label",
                    new ReplaceStringNullableTotal(
                        new MinStringNullableTotal(
                            new SplitPartStringRow(
                                new FieldString("contacts", "email"),
                                new LiteralString("@"),
                                new LiteralInteger(-1)
                            )
                        ),
                        new LiteralString("."),
                        new LiteralString(" dot ")
                    )
                ),
                new SelectItemTotal(
                    "digits_in_count",
                    new LengthIntegerTotal(
                        new ReplaceStringTotal(
                            new AsStringDecimalStringTotal(new CountTotal()),
                            new LiteralString("0"),
                            new LiteralString("")
                        )
                    )
                ),
                new SelectItemTotal(
                    "all_have_a_user_part",
                    new AllTotal(
                        new GreaterThanDecimalRow(
                            new PositionIntegerRow(
                                new FieldString("contacts", "email"),
                                new LiteralString("@")
                            ),
                            new LiteralInteger(1)
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
                    new Column(new String("emails_without_at"), new LongColumnType()),
                    new Column(new String("longest_domain"), new LongColumnType()),
                    new Column(new String("first_domain"), new StringColumnType()),
                    new Column(new String("first_domain_label"), new StringColumnType()),
                    new Column(new String("digits_in_count"), new LongColumnType()),
                    new Column(new String("all_have_a_user_part"), new BoolColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("emails_without_at"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("longest_domain"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(16))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_domain"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("cara.mail"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_domain_label"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("cara dot mail"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("digits_in_count"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("all_have_a_user_part"),
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
