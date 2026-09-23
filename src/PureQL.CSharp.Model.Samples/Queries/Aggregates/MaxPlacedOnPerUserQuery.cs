using Pure.Collections.Generic;
using Pure.Primitives.Date;
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
using PureQL.CSharp.Model.Aggregates.Date;
using PureQL.CSharp.Model.ArrayReturnings;
using PureQL.CSharp.Model.Fields;
using PureQL.CSharp.Model.Returnings;
using Column = Pure.RelationalSchema.Column.Column;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Aggregates;

/// <summary>
/// Selects the maximum of placed_on as max_placed_on from
/// schema_with_foreign_keys.orders, grouped by order_user_id.
/// </summary>
public sealed record MaxPlacedOnPerUserQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public Query Value =>
        new Query(
            new FromExpression(
                new JoinedString(
                    new DotString(),
                    [new RelationalSchemaWithForeignKeys().Name, new OrdersTable().Name]
                ).TextValue
            ),
            [
                new SelectExpression(
                    new SingleValueReturning(
                        new DateReturning(
                            new DateAggregate(
                                new MaxDate(
                                    new DateArrayReturning(
                                        new DateField(
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new RelationalSchemaWithForeignKeys().Name,
                                                    new OrdersTable().Name,
                                                ]
                                            ).TextValue,
                                            new PlacedOnColumn().Name.TextValue
                                        )
                                    )
                                )
                            )
                        )
                    ),
                    "max_placed_on"
                ),
            ],
            where: null,
            join: null,
            [
                new Field(
                    new UuidField(
                        new JoinedString(
                            new DotString(),
                            [
                                new RelationalSchemaWithForeignKeys().Name,
                                new OrdersTable().Name,
                            ]
                        ).TextValue,
                        new OrderUserIdColumn().Name.TextValue
                    )
                ),
            ],
            having: null,
            orderBy: null,
            pagination: null
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
                [new Column(new String("max_placed_on"), new DateColumnType())],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("max_placed_on"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(2),
                                        new UShort(6),
                                        new UShort(2024)
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
                                new Column(
                                    new String("max_placed_on"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(3),
                                        new UShort(6),
                                        new UShort(2024)
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
                                new Column(
                                    new String("max_placed_on"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(6),
                                        new UShort(6),
                                        new UShort(2024)
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
                                new Column(
                                    new String("max_placed_on"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(5),
                                        new UShort(6),
                                        new UShort(2024)
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
