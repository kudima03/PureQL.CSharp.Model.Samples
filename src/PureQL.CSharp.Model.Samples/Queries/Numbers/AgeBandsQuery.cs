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

namespace PureQL.CSharp.Model.Samples.Queries.Numbers;

/// <summary>
/// Selects from schema_with_foreign_keys.users where user_active, grouped by the age_band
/// key (user_age rounded to an integer, then truncated to tens with digits -1, which
/// keeps an integer): the key, the key plus 9, the count, the count rounded to the
/// nearest ten, the minimum rounded user_age, and the key written with asString and
/// followed by s, ordered by the age_band column.
/// </summary>
public sealed record AgeBandsQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainGroupedQueryWithoutPagination(
            from: new FromEntity(
                new JoinedString(
                    new DotString(),
                    [new RelationalSchemaWithForeignKeys().Name, new UsersTable().Name]
                ).TextValue
            ),
            where: new FieldBoolean(
                new JoinedString(
                    new DotString(),
                    [new RelationalSchemaWithForeignKeys().Name, new UsersTable().Name]
                ).TextValue,
                new UserActiveColumn().Name.TextValue
            ),
            groupBy:
            [
                new GroupKey(
                    "age_band",
                    new TruncateWithDigitsIntegerRow(
                        new RoundWithoutDigitsIntegerRow(
                            new FieldDecimal(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new UsersTable().Name,
                                    ]
                                ).TextValue,
                                new UserAgeColumn().Name.TextValue
                            )
                        ),
                        new LiteralInteger(-1)
                    )
                ),
            ],
            select:
            [
                new SelectItemGroup("age_band", new KeyInteger("age_band")),
                new SelectItemGroup(
                    "up_to",
                    new AddIntegerGroup([
                        new KeyInteger("age_band"),
                        new LiteralInteger(9),
                    ])
                ),
                new SelectItemGroup("users", new CountGroup()),
                new SelectItemGroup(
                    "users_to_the_nearest_ten",
                    new RoundWithDigitsIntegerGroup(
                        new CountGroup(),
                        new LiteralInteger(-1)
                    )
                ),
                new SelectItemGroup(
                    "youngest",
                    new MinIntegerGroup(
                        new RoundWithoutDigitsIntegerRow(
                            new FieldDecimal(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new UsersTable().Name,
                                    ]
                                ).TextValue,
                                new UserAgeColumn().Name.TextValue
                            )
                        )
                    )
                ),
                new SelectItemGroup(
                    "label",
                    new ConcatStringGroup([
                        new AsStringDecimalStringGroup(new KeyInteger("age_band")),
                        new LiteralString("s"),
                    ])
                ),
            ],
            orderBy: [new OrderItemGroupColumn("age_band")]
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
                    new Column(new String("age_band"), new LongColumnType()),
                    new Column(new String("up_to"), new LongColumnType()),
                    new Column(new String("users"), new LongColumnType()),
                    new Column(
                        new String("users_to_the_nearest_ten"),
                        new LongColumnType()
                    ),
                    new Column(new String("youngest"), new LongColumnType()),
                    new Column(new String("label"), new StringColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("age_band"), new LongColumnType()),
                                new InvariantCell(new Long(20))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("up_to"), new LongColumnType()),
                                new InvariantCell(new Long(29))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("users"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("users_to_the_nearest_ten"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("youngest"), new LongColumnType()),
                                new InvariantCell(new Long(28))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("label"), new StringColumnType()),
                                new InvariantCell(new String("20s"))
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
                                new Column(new String("age_band"), new LongColumnType()),
                                new InvariantCell(new Long(30))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("up_to"), new LongColumnType()),
                                new InvariantCell(new Long(39))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("users"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("users_to_the_nearest_ten"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("youngest"), new LongColumnType()),
                                new InvariantCell(new Long(30))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("label"), new StringColumnType()),
                                new InvariantCell(new String("30s"))
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
                                new Column(new String("age_band"), new LongColumnType()),
                                new InvariantCell(new Long(40))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("up_to"), new LongColumnType()),
                                new InvariantCell(new Long(49))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("users"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("users_to_the_nearest_ten"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("youngest"), new LongColumnType()),
                                new InvariantCell(new Long(42))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("label"), new StringColumnType()),
                                new InvariantCell(new String("40s"))
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
