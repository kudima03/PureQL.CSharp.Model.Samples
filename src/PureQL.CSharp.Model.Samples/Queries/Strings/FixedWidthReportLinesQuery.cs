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
/// Selects a report line, the group key tier, the min and max of last_name joined by "
/// … " as name_range, the max length of first_name, middle_name and last_name joined by
/// spaces while skipping a null middle_name as longest_full_name, the count of rows
/// whose lower-cased first_name reads the same reversed as palindromes, and the group
/// key country reversed as country_backwards from schema_with_foreign_keys.users inner
/// joined on user_id to an inline subquery members of names, countries and tiers. The
/// line is country padded with spaces to 12 code points, the text of the count padded
/// with spaces at the start to 6, a space and one "#" for every row. The rows are
/// grouped by country and by tier padded with zeros at the start to 2 code points,
/// keeping the groups whose bar of "#" is longer than the integer 1, ordered by the
/// text of the count padded with zeros to 6 descending, then by country and tier.
/// </summary>
public sealed record FixedWidthReportLinesQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainGroupedQueryWithoutPagination(
            subqueries:
            [
                new SubqueryRows(
                    "members",
                    [
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(1, 0, 0, new byte[8]))
                            ),
                            new RowItem("country", new LiteralString("DE")),
                            new RowItem("tier", new LiteralString("1")),
                            new RowItem("first_name", new LiteralString("Ann")),
                            new RowItem("middle_name", new LiteralStringNullable()),
                            new RowItem("last_name", new LiteralString("Stone")),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(2, 0, 0, new byte[8]))
                            ),
                            new RowItem("country", new LiteralString("DE")),
                            new RowItem("tier", new LiteralString("01")),
                            new RowItem("first_name", new LiteralString("Bob")),
                            new RowItem("middle_name", new LiteralString("J")),
                            new RowItem("last_name", new LiteralString("Day")),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(3, 0, 0, new byte[8]))
                            ),
                            new RowItem("country", new LiteralString("DE")),
                            new RowItem("tier", new LiteralString("1")),
                            new RowItem("first_name", new LiteralString("Hannah")),
                            new RowItem("middle_name", new LiteralStringNullable()),
                            new RowItem("last_name", new LiteralString("Lee")),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(4, 0, 0, new byte[8]))
                            ),
                            new RowItem("country", new LiteralString("FR")),
                            new RowItem("tier", new LiteralString("2")),
                            new RowItem("first_name", new LiteralString("Anna")),
                            new RowItem("middle_name", new LiteralString("Marie")),
                            new RowItem("last_name", new LiteralString("Roy")),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(5, 0, 0, new byte[8]))
                            ),
                            new RowItem("country", new LiteralString("FR")),
                            new RowItem("tier", new LiteralString("02")),
                            new RowItem("first_name", new LiteralString("Eve")),
                            new RowItem("middle_name", new LiteralStringNullable()),
                            new RowItem("last_name", new LiteralString("Bell")),
                        ],
                        [
                            new RowItem(
                                "user_id",
                                new LiteralUuid(new Guid(6, 0, 0, new byte[8]))
                            ),
                            new RowItem("country", new LiteralString("US")),
                            new RowItem("tier", new LiteralString("1")),
                            new RowItem("first_name", new LiteralString("Fay")),
                            new RowItem("middle_name", new LiteralStringNullable()),
                            new RowItem("last_name", new LiteralString("Tan")),
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
                    "members",
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
                        new FieldUuid("members", "user_id")
                    )
                ),
            ],
            groupBy:
            [
                new GroupKey("country", new FieldString("members", "country")),
                new GroupKey(
                    "tier",
                    new PadStartStringRow(
                        new FieldString("members", "tier"),
                        new LiteralInteger(2),
                        new LiteralString("0")
                    )
                ),
            ],
            having: new GreaterThanDecimalGroup(
                new LengthIntegerGroup(
                    new RepeatStringGroup(new LiteralString("#"), new CountGroup())
                ),
                new LiteralInteger(1)
            ),
            select:
            [
                new SelectItemGroup(
                    "line",
                    new ConcatStringGroup([
                        new PadEndStringGroup(
                            new KeyString("country"),
                            new LiteralInteger(12),
                            new LiteralString(" ")
                        ),
                        new PadStartStringGroup(
                            new AsStringDecimalStringGroup(new CountGroup()),
                            new LiteralInteger(6),
                            new LiteralString(" ")
                        ),
                        new LiteralString(" "),
                        new RepeatStringGroup(new LiteralString("#"), new CountGroup()),
                    ])
                ),
                new SelectItemGroup("tier", new KeyString("tier")),
                new SelectItemGroup(
                    "name_range",
                    new ConcatWithGroup(
                        [
                            new MinStringGroup(new FieldString("members", "last_name")),
                            new MaxStringGroup(new FieldString("members", "last_name")),
                        ],
                        new LiteralString(" … ")
                    )
                ),
                new SelectItemGroup(
                    "longest_full_name",
                    new MaxIntegerGroup(
                        new LengthIntegerRow(
                            new ConcatWithRow(
                                [
                                    new FieldString("members", "first_name"),
                                    new FieldStringNullable("members", "middle_name"),
                                    new FieldString("members", "last_name"),
                                ],
                                new LiteralString(" ")
                            )
                        )
                    )
                ),
                new SelectItemGroup(
                    "palindromes",
                    new CountGroup(
                        new EqualStringRow(
                            new ReverseStringRow(
                                new LowerStringRow(
                                    new FieldString("members", "first_name")
                                )
                            ),
                            new LowerStringRow(new FieldString("members", "first_name"))
                        )
                    )
                ),
                new SelectItemGroup(
                    "country_backwards",
                    new ReverseStringGroup(new KeyString("country"))
                ),
            ],
            orderBy:
            [
                new OrderItemGroupExpression(
                    new PadStartStringGroup(
                        new AsStringDecimalStringGroup(new CountGroup()),
                        new LiteralInteger(6),
                        new LiteralString("0")
                    ),
                    SortDirection.Desc
                ),
                new OrderItemGroupExpression(new KeyString("country")),
                new OrderItemGroupExpression(new KeyString("tier")),
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
                    new Column(new String("line"), new StringColumnType()),
                    new Column(new String("tier"), new StringColumnType()),
                    new Column(new String("name_range"), new StringColumnType()),
                    new Column(new String("longest_full_name"), new LongColumnType()),
                    new Column(new String("palindromes"), new LongColumnType()),
                    new Column(new String("country_backwards"), new StringColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("line"), new StringColumnType()),
                                new InvariantCell(new String("DE               3 ###"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("tier"), new StringColumnType()),
                                new InvariantCell(new String("01"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("name_range"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Day … Stone"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("longest_full_name"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(10))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("palindromes"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("country_backwards"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("ED"))
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
                                new Column(new String("line"), new StringColumnType()),
                                new InvariantCell(new String("FR               2 ##"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("tier"), new StringColumnType()),
                                new InvariantCell(new String("02"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("name_range"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Bell … Roy"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("longest_full_name"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(14))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("palindromes"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("country_backwards"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("RF"))
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
