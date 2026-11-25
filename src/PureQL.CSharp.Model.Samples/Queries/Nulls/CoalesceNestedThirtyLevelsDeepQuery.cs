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
using PureQL.CSharp.Model.ProjectionExpressions;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Nulls;

/// <summary>
/// Selects from schema_with_foreign_keys.orders the order_status as a coalesce of a typed
/// null string and a coalesce of a typed null string and a coalesce of a typed null
/// string, thirty levels deep, the innermost operand being the non-null order_status, so
/// that every level is a non-null string.
/// </summary>
public sealed record CoalesceNestedThirtyLevelsDeepQuery
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
                    "order_status",
                    new CoalesceStringProjection([
                        new LiteralStringNullable(),
                        new CoalesceStringProjection([
                            new LiteralStringNullable(),
                            new CoalesceStringProjection([
                                new LiteralStringNullable(),
                                new CoalesceStringProjection([
                                    new LiteralStringNullable(),
                                    new CoalesceStringProjection([
                                        new LiteralStringNullable(),
                                        new CoalesceStringProjection([
                                            new LiteralStringNullable(),
                                            new CoalesceStringProjection([
                                                new LiteralStringNullable(),
                                                new CoalesceStringProjection([
                                                    new LiteralStringNullable(),
                                                    new CoalesceStringProjection([
                                                        new LiteralStringNullable(),
                                                        new CoalesceStringProjection([
                                                            new LiteralStringNullable(),
                                                            new CoalesceStringProjection([
                                                                new LiteralStringNullable(),
                                                                new CoalesceStringProjection([
                                                                    new LiteralStringNullable(),
                                                                    new CoalesceStringProjection([
                                                                        new LiteralStringNullable(),
                                                                        new CoalesceStringProjection([
                                                                            new LiteralStringNullable(),
                                                                            new CoalesceStringProjection([
                                                                                new LiteralStringNullable(),
                                                                                new CoalesceStringProjection([
                                                                                    new LiteralStringNullable(),
                                                                                    new CoalesceStringProjection([
                                                                                        new LiteralStringNullable(),
                                                                                        new CoalesceStringProjection([
                                                                                            new LiteralStringNullable(),
                                                                                            new CoalesceStringProjection([
                                                                                                new LiteralStringNullable(),
                                                                                                new CoalesceStringProjection([
                                                                                                    new LiteralStringNullable(),
                                                                                                    new CoalesceStringProjection([
                                                                                                        new LiteralStringNullable(),
                                                                                                        new CoalesceStringProjection([
                                                                                                            new LiteralStringNullable(),
                                                                                                            new CoalesceStringProjection([
                                                                                                                new LiteralStringNullable(),
                                                                                                                new CoalesceStringProjection([
                                                                                                                    new LiteralStringNullable(),
                                                                                                                    new CoalesceStringProjection([
                                                                                                                        new LiteralStringNullable(),
                                                                                                                        new CoalesceStringProjection([
                                                                                                                            new LiteralStringNullable(),
                                                                                                                            new CoalesceStringProjection([
                                                                                                                                new LiteralStringNullable(),
                                                                                                                                new CoalesceStringProjection([
                                                                                                                                    new LiteralStringNullable(),
                                                                                                                                    new CoalesceStringProjection([
                                                                                                                                        new LiteralStringNullable(),
                                                                                                                                        new CoalesceStringProjection([
                                                                                                                                            new LiteralStringNullable(),
                                                                                                                                            new FieldString(
                                                                                                                                                new JoinedString(
                                                                                                                                                    new DotString(),
                                                                                                                                                    [
                                                                                                                                                        new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                        new OrdersTable().Name,
                                                                                                                                                    ]
                                                                                                                                                ).TextValue,
                                                                                                                                                new OrderStatusColumn()
                                                                                                                                                    .Name
                                                                                                                                                    .TextValue
                                                                                                                                            ),
                                                                                                                                        ]),
                                                                                                                                    ]),
                                                                                                                                ]),
                                                                                                                            ]),
                                                                                                                        ]),
                                                                                                                    ]),
                                                                                                                ]),
                                                                                                            ]),
                                                                                                        ]),
                                                                                                    ]),
                                                                                                ]),
                                                                                            ]),
                                                                                        ]),
                                                                                    ]),
                                                                                ]),
                                                                            ]),
                                                                        ]),
                                                                    ]),
                                                                ]),
                                                            ]),
                                                        ]),
                                                    ]),
                                                ]),
                                            ]),
                                        ]),
                                    ]),
                                ]),
                            ]),
                        ]),
                    ])
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
            new Table(new EmptyString(), [new OrderStatusColumn()], []),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new OrderStatusColumn(),
                                new InvariantCell(new String("shipped"))
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
                                new InvariantCell(new String("cancelled"))
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
                        ],
                        pair => pair.Key,
                        pair => pair.Value,
                        column => new ColumnHash(column)
                    )
                ),
            ]
        );
}
