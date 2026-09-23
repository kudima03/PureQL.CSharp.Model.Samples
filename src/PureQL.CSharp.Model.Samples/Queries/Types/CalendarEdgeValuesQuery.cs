using Pure.Collections.Generic;
using Pure.Primitives.Date;
using Pure.Primitives.Number;
using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.Primitives.Time;
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
using PureQL.CSharp.Model.ArrayReturnings;
using PureQL.CSharp.Model.Fields;
using DateTime = Pure.Primitives.DateTime.DateTime;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Types;

/// <summary>
/// Selects user_edge_date, user_edge_datetime and user_edge_time from
/// schema_with_foreign_keys.users.
/// </summary>
public sealed record CalendarEdgeValuesQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public Query Value =>
        new Query(
            new FromExpression(
                new JoinedString(
                    new DotString(),
                    [new RelationalSchemaWithForeignKeys().Name, new UsersTable().Name]
                ).TextValue
            ),
            [
                new SelectExpression(
                    new ArrayReturning(
                        new DateArrayReturning(
                            new DateField(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new UsersTable().Name,
                                    ]
                                ).TextValue,
                                new UserEdgeDateColumn().Name.TextValue
                            )
                        )
                    )
                ),
                new SelectExpression(
                    new ArrayReturning(
                        new DateTimeArrayReturning(
                            new DateTimeField(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new UsersTable().Name,
                                    ]
                                ).TextValue,
                                new UserEdgeDateTimeColumn().Name.TextValue
                            )
                        )
                    )
                ),
                new SelectExpression(
                    new ArrayReturning(
                        new TimeArrayReturning(
                            new TimeField(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new UsersTable().Name,
                                    ]
                                ).TextValue,
                                new UserEdgeTimeColumn().Name.TextValue
                            )
                        )
                    )
                ),
            ]
        );

    /// <summary>
    /// The rows the query returns under SQL semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and <see cref="AuditSchemaDataSet"/>,
    /// in no particular order.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new UserEdgeDateColumn(),
                    new UserEdgeDateTimeColumn(),
                    new UserEdgeTimeColumn(),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new UserEdgeDateColumn(),
                                new InvariantCell(
                                    new Date(
                                        new UShort(29),
                                        new UShort(2),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new UserEdgeDateTimeColumn(),
                                new InvariantCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(29),
                                            new UShort(2),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(0),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new UserEdgeTimeColumn(),
                                new InvariantCell(
                                    new Time(new UShort(0), new UShort(0), new UShort(0))
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
                                new UserEdgeDateColumn(),
                                new InvariantCell(
                                    new Date(
                                        new UShort(31),
                                        new UShort(12),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new UserEdgeDateTimeColumn(),
                                new InvariantCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(31),
                                            new UShort(12),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(23),
                                            new UShort(59),
                                            new UShort(59)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new UserEdgeTimeColumn(),
                                new InvariantCell(
                                    new Time(
                                        new UShort(23),
                                        new UShort(59),
                                        new UShort(59)
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
                                new UserEdgeDateColumn(),
                                new InvariantCell(
                                    new Date(
                                        new UShort(10),
                                        new UShort(3),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new UserEdgeDateTimeColumn(),
                                new InvariantCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(10),
                                            new UShort(3),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(2),
                                            new UShort(30),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new UserEdgeTimeColumn(),
                                new InvariantCell(
                                    new Time(new UShort(2), new UShort(30), new UShort(0))
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
                                new UserEdgeDateColumn(),
                                new InvariantCell(
                                    new Date(
                                        new UShort(3),
                                        new UShort(11),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new UserEdgeDateTimeColumn(),
                                new InvariantCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(3),
                                            new UShort(11),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(1),
                                            new UShort(30),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new UserEdgeTimeColumn(),
                                new InvariantCell(
                                    new Time(new UShort(1), new UShort(30), new UShort(0))
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
                                new UserEdgeDateColumn(),
                                new InvariantCell(
                                    new Date(new UShort(1), new UShort(1), new UShort(1))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new UserEdgeDateTimeColumn(),
                                new InvariantCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(1),
                                            new UShort(1),
                                            new UShort(1)
                                        ),
                                        new Time(
                                            new UShort(0),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new UserEdgeTimeColumn(),
                                new InvariantCell(
                                    new Time(new UShort(0), new UShort(0), new UShort(0))
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
                                new UserEdgeDateColumn(),
                                new InvariantCell(
                                    new Date(
                                        new UShort(31),
                                        new UShort(12),
                                        new UShort(9999)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new UserEdgeDateTimeColumn(),
                                new InvariantCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(31),
                                            new UShort(12),
                                            new UShort(9999)
                                        ),
                                        new Time(
                                            new UShort(23),
                                            new UShort(59),
                                            new UShort(59)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new UserEdgeTimeColumn(),
                                new InvariantCell(
                                    new Time(
                                        new UShort(23),
                                        new UShort(59),
                                        new UShort(59)
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
