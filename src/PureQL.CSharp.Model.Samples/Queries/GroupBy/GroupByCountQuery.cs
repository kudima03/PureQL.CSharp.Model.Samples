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
using PureQL.CSharp.Model.GroupKeys;
using PureQL.CSharp.Model.Keys;
using PureQL.CSharp.Model.RowExpressions;
using PureQL.CSharp.Model.SelectItems;
using Column = Pure.RelationalSchema.Column.Column;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.GroupBy;

/// <summary>
/// Selects order_status and the count of rows as orders from
/// schema_with_foreign_keys.orders, grouped by order_status.
/// </summary>
public sealed record GroupByCountQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new PureQLQuery(
            new MainGroupedQuery(
                new From(
                    new FromEntity(
                        new JoinedString(
                            new DotString(),
                            [
                                new RelationalSchemaWithForeignKeys().Name,
                                new OrdersTable().Name,
                            ]
                        ).TextValue
                    )
                ),
                [
                    new GroupKey(
                        new GroupKeyNonNullable(
                            new GroupKeyString(
                                new StringRow(
                                    new FieldString(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new OrdersTable().Name,
                                            ]
                                        ).TextValue,
                                        new OrderStatusColumn().Name.TextValue
                                    )
                                )
                            )
                        )
                    ),
                ],
                [
                    new SelectItemGroup(
                        new SelectItemGroupNonNullable(
                            new SelectItemGroupString(
                                "order_status",
                                new StringGroup(new KeyString(0))
                            )
                        )
                    ),
                    new SelectItemGroup(
                        new SelectItemGroupNonNullable(
                            new SelectItemGroupInteger(
                                "orders",
                                new IntegerGroup(
                                    new AggregateIntegerGroup(new CountGroup())
                                )
                            )
                        )
                    ),
                ]
            )
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
                    new OrderStatusColumn(),
                    new Column(new String("orders"), new LongColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new OrderStatusColumn(),
                                new InvariantCell(new String("cancelled"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
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
                                new OrderStatusColumn(),
                                new InvariantCell(new String("pending"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(2))
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
                                new OrderStatusColumn(),
                                new InvariantCell(new String("shipped"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
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
