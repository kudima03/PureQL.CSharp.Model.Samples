using Pure.Collections.Generic;
using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Abstractions.Column;
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
using Guid = Pure.Primitives.Guid.Guid;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Where;

/// <summary>
/// Selects order_id from schema_with_foreign_keys.orders where, thirty levels deep, a not
/// holds an or of false and an and, the and holding a not of the same shape and true,
/// down to an innermost and of order_total greater than the decimal 100 and true.
/// </summary>
public sealed record BooleanOperatorsNestedThirtyLevelsDeepQuery
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
            where: new NotRow(
                new OrRow([
                    new LiteralBoolean(false),
                    new AndRow([
                        new NotRow(
                            new OrRow([
                                new LiteralBoolean(false),
                                new AndRow([
                                    new NotRow(
                                        new OrRow([
                                            new LiteralBoolean(false),
                                            new AndRow([
                                                new NotRow(
                                                    new OrRow([
                                                        new LiteralBoolean(false),
                                                        new AndRow([
                                                            new NotRow(
                                                                new OrRow([
                                                                    new LiteralBoolean(
                                                                        false
                                                                    ),
                                                                    new AndRow([
                                                                        new NotRow(
                                                                            new OrRow([
                                                                                new LiteralBoolean(
                                                                                    false
                                                                                ),
                                                                                new AndRow([
                                                                                    new NotRow(
                                                                                        new OrRow([
                                                                                            new LiteralBoolean(
                                                                                                false
                                                                                            ),
                                                                                            new AndRow([
                                                                                                new NotRow(
                                                                                                    new OrRow([
                                                                                                        new LiteralBoolean(
                                                                                                            false
                                                                                                        ),
                                                                                                        new AndRow([
                                                                                                            new NotRow(
                                                                                                                new OrRow([
                                                                                                                    new LiteralBoolean(
                                                                                                                        false
                                                                                                                    ),
                                                                                                                    new AndRow([
                                                                                                                        new NotRow(
                                                                                                                            new OrRow([
                                                                                                                                new LiteralBoolean(
                                                                                                                                    false
                                                                                                                                ),
                                                                                                                                new AndRow([
                                                                                                                                    new GreaterThanDecimalRow(
                                                                                                                                        new FieldDecimal(
                                                                                                                                            new JoinedString(
                                                                                                                                                new DotString(),
                                                                                                                                                [
                                                                                                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                    new OrdersTable().Name,
                                                                                                                                                ]
                                                                                                                                            ).TextValue,
                                                                                                                                            new OrderTotalColumn()
                                                                                                                                                .Name
                                                                                                                                                .TextValue
                                                                                                                                        ),
                                                                                                                                        new LiteralDecimal(
                                                                                                                                            100m
                                                                                                                                        )
                                                                                                                                    ),
                                                                                                                                    new LiteralBoolean(
                                                                                                                                        true
                                                                                                                                    ),
                                                                                                                                ]),
                                                                                                                            ])
                                                                                                                        ),
                                                                                                                        new LiteralBoolean(
                                                                                                                            true
                                                                                                                        ),
                                                                                                                    ]),
                                                                                                                ])
                                                                                                            ),
                                                                                                            new LiteralBoolean(
                                                                                                                true
                                                                                                            ),
                                                                                                        ]),
                                                                                                    ])
                                                                                                ),
                                                                                                new LiteralBoolean(
                                                                                                    true
                                                                                                ),
                                                                                            ]),
                                                                                        ])
                                                                                    ),
                                                                                    new LiteralBoolean(
                                                                                        true
                                                                                    ),
                                                                                ]),
                                                                            ])
                                                                        ),
                                                                        new LiteralBoolean(
                                                                            true
                                                                        ),
                                                                    ]),
                                                                ])
                                                            ),
                                                            new LiteralBoolean(true),
                                                        ]),
                                                    ])
                                                ),
                                                new LiteralBoolean(true),
                                            ]),
                                        ])
                                    ),
                                    new LiteralBoolean(true),
                                ]),
                            ])
                        ),
                        new LiteralBoolean(true),
                    ]),
                ])
            ),
            select:
            [
                new SelectItemProjection(
                    "order_id",
                    new FieldUuid(
                        new JoinedString(
                            new DotString(),
                            [
                                new RelationalSchemaWithForeignKeys().Name,
                                new OrdersTable().Name,
                            ]
                        ).TextValue,
                        new OrderIdColumn().Name.TextValue
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
            new Table(new EmptyString(), [new OrderIdColumn()], []),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new OrderIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000065-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
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
                                new OrderIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000067-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
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
                                new OrderIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000069-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
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
                                new OrderIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "0000006a-0000-0000-0000-000000000000"
                                        )
                                    )
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
