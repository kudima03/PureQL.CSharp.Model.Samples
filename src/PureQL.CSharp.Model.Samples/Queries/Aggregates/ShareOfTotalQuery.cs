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
using PureQL.CSharp.Model.ProjectionExpressions;
using PureQL.CSharp.Model.RowExpressions;
using PureQL.CSharp.Model.SelectItems;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using Guid = Pure.Primitives.Guid.Guid;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Aggregates;

/// <summary>
/// Selects order_id, order_total and order_total divided by the sum of order_total as
/// share_of_total from schema_with_foreign_keys.orders.
/// </summary>
public sealed record ShareOfTotalQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new PureQLQuery(
            new MainPlainQuery(
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
                    new SelectItemProjection(
                        new SelectItemProjectionNonNullable(
                            new SelectItemProjectionUuid(
                                "order_id",
                                new UuidProjection(
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
                                )
                            )
                        )
                    ),
                    new SelectItemProjection(
                        new SelectItemProjectionNonNullable(
                            new SelectItemProjectionDecimal(
                                "order_total",
                                new DecimalProjection(
                                    new FieldAsDecimal(
                                        new FieldDecimal(
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new RelationalSchemaWithForeignKeys().Name,
                                                    new OrdersTable().Name,
                                                ]
                                            ).TextValue,
                                            new OrderTotalColumn().Name.TextValue
                                        )
                                    )
                                )
                            )
                        )
                    ),
                    new SelectItemProjection(
                        new SelectItemProjectionNonNullable(
                            new SelectItemProjectionDecimal(
                                "share_of_total",
                                new DecimalProjection(
                                    new ArithmeticDecimalProjection(
                                        new DivideDecimalProjection([
                                            new DecimalProjection(
                                                new FieldAsDecimal(
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
                                                    )
                                                )
                                            ),
                                            new DecimalProjection(
                                                new AggregateDecimalProjection(
                                                    new SumDecimalProjection(
                                                        new DecimalNullableRow(
                                                            new FieldAsDecimalNullable(
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
                                                                )
                                                            )
                                                        )
                                                    )
                                                )
                                            ),
                                        ])
                                    )
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
                    new OrderIdColumn(),
                    new OrderTotalColumn(),
                    new Column(new String("share_of_total"), new DoubleColumnType()),
                ],
                []
            ),
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
                            new KeyValuePair<IColumn, ICell>(
                                new OrderTotalColumn(),
                                new InvariantCell(new Double(100.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("share_of_total"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.12163388804841149))
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
                                            "00000066-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new OrderTotalColumn(),
                                new InvariantCell(new Double(50))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("share_of_total"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.060514372163388806))
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
                            new KeyValuePair<IColumn, ICell>(
                                new OrderTotalColumn(),
                                new InvariantCell(new Double(200))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("share_of_total"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.24205748865355523))
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
                                            "00000068-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new OrderTotalColumn(),
                                new InvariantCell(new Double(75.25))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("share_of_total"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.09107413010590015))
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
                            new KeyValuePair<IColumn, ICell>(
                                new OrderTotalColumn(),
                                new InvariantCell(new Double(300))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("share_of_total"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.3630862329803328))
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
                            new KeyValuePair<IColumn, ICell>(
                                new OrderTotalColumn(),
                                new InvariantCell(new Double(100.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("share_of_total"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.12163388804841149))
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
