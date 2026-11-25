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
using PureQL.CSharp.Model.TotalExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Strings;

/// <summary>
/// Aggregates schema_with_foreign_keys.users into one row: the character = repeated once
/// per user as bar, the count of users and the count of active ones (a predicate on
/// user_active) as text joined by " / " as summary, the least and the greatest user_name
/// joined by " … " as first_and_last_name, the least user_name reversed as
/// first_name_backwards, the count as text padded on the left with 0 to 5 characters as
/// count_in_five_digits, and the greatest user_name padded on the right with spaces to 30
/// characters as widest_name.
/// </summary>
public sealed record PaddingAndJoiningInATotalQuery
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
                    "bar",
                    new RepeatStringTotal(new LiteralString("="), new CountTotal())
                ),
                new SelectItemTotal(
                    "summary",
                    new ConcatWithTotal(
                        [
                            new AsStringDecimalStringTotal(new CountTotal()),
                            new AsStringDecimalStringTotal(
                                new CountTotal(
                                    predicate: new FieldBoolean(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new UsersTable().Name,
                                            ]
                                        ).TextValue,
                                        new UserActiveColumn().Name.TextValue
                                    )
                                )
                            ),
                        ],
                        new LiteralString(" / ")
                    )
                ),
                new SelectItemTotal(
                    "first_and_last_name",
                    new ConcatWithTotal(
                        [
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
                            new MaxStringNullableTotal(
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
                        ],
                        new LiteralString(" … ")
                    )
                ),
                new SelectItemTotal(
                    "first_name_backwards",
                    new ReverseStringNullableTotal(
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
                        )
                    )
                ),
                new SelectItemTotal(
                    "count_in_five_digits",
                    new PadStartStringTotal(
                        new AsStringDecimalStringTotal(new CountTotal()),
                        new LiteralInteger(5),
                        new LiteralString("0")
                    )
                ),
                new SelectItemTotal(
                    "widest_name",
                    new PadEndStringNullableTotal(
                        new MaxStringNullableTotal(
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
                        new LiteralInteger(30),
                        new LiteralString(" ")
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
                    new Column(new String("bar"), new StringColumnType()),
                    new Column(new String("summary"), new StringColumnType()),
                    new Column(new String("first_and_last_name"), new StringColumnType()),
                    new Column(
                        new String("first_name_backwards"),
                        new StringColumnType()
                    ),
                    new Column(
                        new String("count_in_five_digits"),
                        new StringColumnType()
                    ),
                    new Column(new String("widest_name"), new StringColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("bar"), new StringColumnType()),
                                new InvariantCell(new String("======"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("summary"), new StringColumnType()),
                                new InvariantCell(new String("6 / 4"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_and_last_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Ann … Fay"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("first_name_backwards"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("nnA"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("count_in_five_digits"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("00006"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("widest_name"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(
                                    new String("Fay                           ")
                                )
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
