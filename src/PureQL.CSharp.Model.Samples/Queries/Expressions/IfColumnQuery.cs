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
using PureQL.CSharp.Model.ProjectionExpressions;
using PureQL.CSharp.Model.SelectItems;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using Guid = Pure.Primitives.Guid.Guid;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Expressions;

/// <summary>
/// Selects order_id, large or small as size depending on whether order_total is greater
/// than the integer 100, and a tenth of order_total for shipped orders or the integer 0
/// otherwise as discount from schema_with_foreign_keys.orders.
/// </summary>
public sealed record IfColumnQuery
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
                            new SelectItemProjectionString(
                                "size",
                                new StringProjection(
                                    new ConditionalStringProjection(
                                        new IfStringProjection(
                                            new BooleanProjection(
                                                new ComparisonProjection(
                                                    new GreaterThanProjection(
                                                        new GreaterThanDecimalProjection(
                                                            new DecimalNullableProjection(
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
                                                            ),
                                                            new DecimalNullableProjection(
                                                                new LiteralAsDecimalNullable(
                                                                    new LiteralInteger(
                                                                        100
                                                                    )
                                                                )
                                                            )
                                                        )
                                                    )
                                                )
                                            ),
                                            new StringProjection(
                                                new LiteralString("large")
                                            ),
                                            new StringProjection(
                                                new LiteralString("small")
                                            )
                                        )
                                    )
                                )
                            )
                        )
                    ),
                    new SelectItemProjection(
                        new SelectItemProjectionNonNullable(
                            new SelectItemProjectionDecimal(
                                "discount",
                                new DecimalProjection(
                                    new ConditionalDecimalProjection(
                                        new IfDecimalProjection(
                                            new BooleanProjection(
                                                new ComparisonProjection(
                                                    new EqualProjection(
                                                        new EqualStringProjection(
                                                            new StringNullableProjection(
                                                                new FieldAsStringNullable(
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
                                                                    )
                                                                )
                                                            ),
                                                            new StringNullableProjection(
                                                                new LiteralAsStringNullable(
                                                                    new LiteralString(
                                                                        "shipped"
                                                                    )
                                                                )
                                                            )
                                                        )
                                                    )
                                                )
                                            ),
                                            new DecimalProjection(
                                                new ArithmeticDecimalProjection(
                                                    new MultiplyDecimalProjection([
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
                                                            new LiteralAsDecimal(
                                                                new LiteralDecimal(0.1m)
                                                            )
                                                        ),
                                                    ])
                                                )
                                            ),
                                            new DecimalProjection(
                                                new LiteralAsDecimal(
                                                    new LiteralInteger(0)
                                                )
                                            )
                                        )
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
                    new Column(new String("size"), new StringColumnType()),
                    new Column(new String("discount"), new DoubleColumnType()),
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
                                new Column(new String("size"), new StringColumnType()),
                                new InvariantCell(new String("large"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("discount"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(10.05))
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
                                new Column(new String("size"), new StringColumnType()),
                                new InvariantCell(new String("small"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("discount"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0))
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
                                new Column(new String("size"), new StringColumnType()),
                                new InvariantCell(new String("large"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("discount"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(20))
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
                                new Column(new String("size"), new StringColumnType()),
                                new InvariantCell(new String("small"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("discount"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0))
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
                                new Column(new String("size"), new StringColumnType()),
                                new InvariantCell(new String("large"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("discount"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(30))
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
                                new Column(new String("size"), new StringColumnType()),
                                new InvariantCell(new String("large"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("discount"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0))
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
