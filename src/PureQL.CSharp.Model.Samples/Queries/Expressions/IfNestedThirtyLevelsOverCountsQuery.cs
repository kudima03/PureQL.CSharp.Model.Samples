using Pure.Collections.Generic;
using Pure.Primitives.Number;
using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Abstractions.Column;
using Pure.RelationalSchema.ColumnType;
using Pure.RelationalSchema.HashCodes;
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
using Pure.RelationalSchema.Storage;
using Pure.RelationalSchema.Storage.Abstractions;
using Pure.RelationalSchema.Storage.Samples.Cells;
using Pure.RelationalSchema.Storage.Samples.SchemaDataSets;
using Pure.RelationalSchema.Storage.Samples.TableDataSets;
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.ProjectionExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Expressions;

/// <summary>
/// Selects from schema_with_foreign_keys.orders, as tier, an if nested thirty levels deep
/// through its else: at level i, counting from 0, the count of all orders greater than 26
/// minus i gives i, and the innermost else is 99. The count is an aggregate in a plain
/// select, so it covers every order and is repeated on every row.
/// </summary>
public sealed record IfNestedThirtyLevelsOverCountsQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithoutPagination(
            from: new FromEntity(
                new JoinedString(
                    new DotString(),
                    [new RelationalSchemaWithForeignKeys().Name, new OrdersTable().Name]
                ).TextValue
            ),
            select:
            [
                new SelectItemProjection(
                    "tier",
                    new IfIntegerProjection(
                        new LiteralInteger(0),
                        new IfIntegerProjection(
                            new LiteralInteger(1),
                            new IfIntegerProjection(
                                new LiteralInteger(2),
                                new IfIntegerProjection(
                                    new LiteralInteger(3),
                                    new IfIntegerProjection(
                                        new LiteralInteger(4),
                                        new IfIntegerProjection(
                                            new LiteralInteger(5),
                                            new IfIntegerProjection(
                                                new LiteralInteger(6),
                                                new IfIntegerProjection(
                                                    new LiteralInteger(7),
                                                    new IfIntegerProjection(
                                                        new LiteralInteger(8),
                                                        new IfIntegerProjection(
                                                            new LiteralInteger(9),
                                                            new IfIntegerProjection(
                                                                new LiteralInteger(10),
                                                                new IfIntegerProjection(
                                                                    new LiteralInteger(
                                                                        11
                                                                    ),
                                                                    new IfIntegerProjection(
                                                                        new LiteralInteger(
                                                                            12
                                                                        ),
                                                                        new IfIntegerProjection(
                                                                            new LiteralInteger(
                                                                                13
                                                                            ),
                                                                            new IfIntegerProjection(
                                                                                new LiteralInteger(
                                                                                    14
                                                                                ),
                                                                                new IfIntegerProjection(
                                                                                    new LiteralInteger(
                                                                                        15
                                                                                    ),
                                                                                    new IfIntegerProjection(
                                                                                        new LiteralInteger(
                                                                                            16
                                                                                        ),
                                                                                        new IfIntegerProjection(
                                                                                            new LiteralInteger(
                                                                                                17
                                                                                            ),
                                                                                            new IfIntegerProjection(
                                                                                                new LiteralInteger(
                                                                                                    18
                                                                                                ),
                                                                                                new IfIntegerProjection(
                                                                                                    new LiteralInteger(
                                                                                                        19
                                                                                                    ),
                                                                                                    new IfIntegerProjection(
                                                                                                        new LiteralInteger(
                                                                                                            20
                                                                                                        ),
                                                                                                        new IfIntegerProjection(
                                                                                                            new LiteralInteger(
                                                                                                                21
                                                                                                            ),
                                                                                                            new IfIntegerProjection(
                                                                                                                new LiteralInteger(
                                                                                                                    22
                                                                                                                ),
                                                                                                                new IfIntegerProjection(
                                                                                                                    new LiteralInteger(
                                                                                                                        23
                                                                                                                    ),
                                                                                                                    new IfIntegerProjection(
                                                                                                                        new LiteralInteger(
                                                                                                                            24
                                                                                                                        ),
                                                                                                                        new IfIntegerProjection(
                                                                                                                            new LiteralInteger(
                                                                                                                                25
                                                                                                                            ),
                                                                                                                            new IfIntegerProjection(
                                                                                                                                new LiteralInteger(
                                                                                                                                    26
                                                                                                                                ),
                                                                                                                                new IfIntegerProjection(
                                                                                                                                    new LiteralInteger(
                                                                                                                                        27
                                                                                                                                    ),
                                                                                                                                    new IfIntegerProjection(
                                                                                                                                        new LiteralInteger(
                                                                                                                                            28
                                                                                                                                        ),
                                                                                                                                        new IfIntegerProjection(
                                                                                                                                            new LiteralInteger(
                                                                                                                                                29
                                                                                                                                            ),
                                                                                                                                            new LiteralInteger(
                                                                                                                                                99
                                                                                                                                            ),
                                                                                                                                            new GreaterThanDecimalProjection(
                                                                                                                                                new CountProjection(),
                                                                                                                                                new LiteralInteger(
                                                                                                                                                    -3
                                                                                                                                                )
                                                                                                                                            )
                                                                                                                                        ),
                                                                                                                                        new GreaterThanDecimalProjection(
                                                                                                                                            new CountProjection(),
                                                                                                                                            new LiteralInteger(
                                                                                                                                                -2
                                                                                                                                            )
                                                                                                                                        )
                                                                                                                                    ),
                                                                                                                                    new GreaterThanDecimalProjection(
                                                                                                                                        new CountProjection(),
                                                                                                                                        new LiteralInteger(
                                                                                                                                            -1
                                                                                                                                        )
                                                                                                                                    )
                                                                                                                                ),
                                                                                                                                new GreaterThanDecimalProjection(
                                                                                                                                    new CountProjection(),
                                                                                                                                    new LiteralInteger(
                                                                                                                                        0
                                                                                                                                    )
                                                                                                                                )
                                                                                                                            ),
                                                                                                                            new GreaterThanDecimalProjection(
                                                                                                                                new CountProjection(),
                                                                                                                                new LiteralInteger(
                                                                                                                                    1
                                                                                                                                )
                                                                                                                            )
                                                                                                                        ),
                                                                                                                        new GreaterThanDecimalProjection(
                                                                                                                            new CountProjection(),
                                                                                                                            new LiteralInteger(
                                                                                                                                2
                                                                                                                            )
                                                                                                                        )
                                                                                                                    ),
                                                                                                                    new GreaterThanDecimalProjection(
                                                                                                                        new CountProjection(),
                                                                                                                        new LiteralInteger(
                                                                                                                            3
                                                                                                                        )
                                                                                                                    )
                                                                                                                ),
                                                                                                                new GreaterThanDecimalProjection(
                                                                                                                    new CountProjection(),
                                                                                                                    new LiteralInteger(
                                                                                                                        4
                                                                                                                    )
                                                                                                                )
                                                                                                            ),
                                                                                                            new GreaterThanDecimalProjection(
                                                                                                                new CountProjection(),
                                                                                                                new LiteralInteger(
                                                                                                                    5
                                                                                                                )
                                                                                                            )
                                                                                                        ),
                                                                                                        new GreaterThanDecimalProjection(
                                                                                                            new CountProjection(),
                                                                                                            new LiteralInteger(
                                                                                                                6
                                                                                                            )
                                                                                                        )
                                                                                                    ),
                                                                                                    new GreaterThanDecimalProjection(
                                                                                                        new CountProjection(),
                                                                                                        new LiteralInteger(
                                                                                                            7
                                                                                                        )
                                                                                                    )
                                                                                                ),
                                                                                                new GreaterThanDecimalProjection(
                                                                                                    new CountProjection(),
                                                                                                    new LiteralInteger(
                                                                                                        8
                                                                                                    )
                                                                                                )
                                                                                            ),
                                                                                            new GreaterThanDecimalProjection(
                                                                                                new CountProjection(),
                                                                                                new LiteralInteger(
                                                                                                    9
                                                                                                )
                                                                                            )
                                                                                        ),
                                                                                        new GreaterThanDecimalProjection(
                                                                                            new CountProjection(),
                                                                                            new LiteralInteger(
                                                                                                10
                                                                                            )
                                                                                        )
                                                                                    ),
                                                                                    new GreaterThanDecimalProjection(
                                                                                        new CountProjection(),
                                                                                        new LiteralInteger(
                                                                                            11
                                                                                        )
                                                                                    )
                                                                                ),
                                                                                new GreaterThanDecimalProjection(
                                                                                    new CountProjection(),
                                                                                    new LiteralInteger(
                                                                                        12
                                                                                    )
                                                                                )
                                                                            ),
                                                                            new GreaterThanDecimalProjection(
                                                                                new CountProjection(),
                                                                                new LiteralInteger(
                                                                                    13
                                                                                )
                                                                            )
                                                                        ),
                                                                        new GreaterThanDecimalProjection(
                                                                            new CountProjection(),
                                                                            new LiteralInteger(
                                                                                14
                                                                            )
                                                                        )
                                                                    ),
                                                                    new GreaterThanDecimalProjection(
                                                                        new CountProjection(),
                                                                        new LiteralInteger(
                                                                            15
                                                                        )
                                                                    )
                                                                ),
                                                                new GreaterThanDecimalProjection(
                                                                    new CountProjection(),
                                                                    new LiteralInteger(16)
                                                                )
                                                            ),
                                                            new GreaterThanDecimalProjection(
                                                                new CountProjection(),
                                                                new LiteralInteger(17)
                                                            )
                                                        ),
                                                        new GreaterThanDecimalProjection(
                                                            new CountProjection(),
                                                            new LiteralInteger(18)
                                                        )
                                                    ),
                                                    new GreaterThanDecimalProjection(
                                                        new CountProjection(),
                                                        new LiteralInteger(19)
                                                    )
                                                ),
                                                new GreaterThanDecimalProjection(
                                                    new CountProjection(),
                                                    new LiteralInteger(20)
                                                )
                                            ),
                                            new GreaterThanDecimalProjection(
                                                new CountProjection(),
                                                new LiteralInteger(21)
                                            )
                                        ),
                                        new GreaterThanDecimalProjection(
                                            new CountProjection(),
                                            new LiteralInteger(22)
                                        )
                                    ),
                                    new GreaterThanDecimalProjection(
                                        new CountProjection(),
                                        new LiteralInteger(23)
                                    )
                                ),
                                new GreaterThanDecimalProjection(
                                    new CountProjection(),
                                    new LiteralInteger(24)
                                )
                            ),
                            new GreaterThanDecimalProjection(
                                new CountProjection(),
                                new LiteralInteger(25)
                            )
                        ),
                        new GreaterThanDecimalProjection(
                            new CountProjection(),
                            new LiteralInteger(26)
                        )
                    )
                ),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over <see
    /// cref="SchemaDataSetWithForeignKeys"/> and <see cref="AuditSchemaDataSet"/>, in no
    /// particular order.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [new Column(new String("tier"), new LongColumnType())],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("tier"), new LongColumnType()),
                                new InvariantCell(new Long(21))
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
                                new Column(new String("tier"), new LongColumnType()),
                                new InvariantCell(new Long(21))
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
                                new Column(new String("tier"), new LongColumnType()),
                                new InvariantCell(new Long(21))
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
                                new Column(new String("tier"), new LongColumnType()),
                                new InvariantCell(new Long(21))
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
                                new Column(new String("tier"), new LongColumnType()),
                                new InvariantCell(new Long(21))
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
                                new Column(new String("tier"), new LongColumnType()),
                                new InvariantCell(new Long(21))
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
