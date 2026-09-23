using Pure.Collections.Generic;
using Pure.Primitives.Bool;
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
using PureQL.CSharp.Model.Aggregates;
using PureQL.CSharp.Model.ArrayReturnings;
using PureQL.CSharp.Model.Fields;
using PureQL.CSharp.Model.Returnings;
using PureQL.CSharp.Model.Scalars;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using Guid = Pure.Primitives.Guid.Guid;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.GroupBy;

/// <summary>
/// Selects true as flag, '9b2b1f6e-3c86-4c50-8f6a-2f6d1a8f2c11' as marker and the count
/// of order_id as order_count from schema_with_foreign_keys.orders.
/// </summary>
public sealed record BooleanAndUuidScalarsInGroupModeQuery
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
                        new BooleanReturning(new BooleanScalar(true))
                    ),
                    "flag"
                ),
                new SelectExpression(
                    new SingleValueReturning(
                        new UuidReturning(
                            new UuidScalar(
                                new System.Guid("9b2b1f6e-3c86-4c50-8f6a-2f6d1a8f2c11")
                            )
                        )
                    ),
                    "marker"
                ),
                new SelectExpression(
                    new SingleValueReturning(
                        new NumberReturning(
                            new Count(
                                new ArrayReturning(
                                    new UuidArrayReturning(
                                        new UuidField(
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new RelationalSchemaWithForeignKeys().Name,
                                                    new OrdersTable().Name,
                                                ]
                                            ).TextValue,
                                            new OrderIdColumn().Name.TextValue
                                        )
                                    )
                                )
                            )
                        )
                    ),
                    "order_count"
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
                    new Column(new String("flag"), new BoolColumnType()),
                    new Column(new String("marker"), new UuidColumnType()),
                    new Column(new String("order_count"), new DoubleColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("flag"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("marker"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "9b2b1f6e-3c86-4c50-8f6a-2f6d1a8f2c11"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("order_count"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(6))
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
