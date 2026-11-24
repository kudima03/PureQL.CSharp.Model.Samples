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
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.ProjectionExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using Guid = Pure.Primitives.Guid.Guid;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Numbers;

/// <summary>
/// Selects from schema_with_foreign_keys.orders: order_id, the hour of placed_at, that
/// hour times pi divided by 12 as an angle in radians, its cosine and sine as a point on
/// a circle, the atan2 of the sine and the cosine times 180 divided by pi as the angle
/// back in degrees, and pi, ordered by order_id.
/// </summary>
public sealed record HourAsAnAngleQuery
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
                new SelectItemProjection(
                    "hour",
                    new HourDatetimeIntegerProjection(
                        new FieldDatetime(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrdersTable().Name,
                                ]
                            ).TextValue,
                            new PlacedAtColumn().Name.TextValue
                        )
                    )
                ),
                new SelectItemProjection(
                    "hour_angle",
                    new MultiplyDecimalProjection([
                        new HourDatetimeIntegerProjection(
                            new FieldDatetime(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new OrdersTable().Name,
                                    ]
                                ).TextValue,
                                new PlacedAtColumn().Name.TextValue
                            )
                        ),
                        new DivideDecimalProjection([
                            new PiProjection(),
                            new LiteralInteger(12),
                        ]),
                    ])
                ),
                new SelectItemProjection(
                    "hour_x",
                    new CosDecimalProjection(
                        new MultiplyDecimalProjection([
                            new HourDatetimeIntegerProjection(
                                new FieldDatetime(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new OrdersTable().Name,
                                        ]
                                    ).TextValue,
                                    new PlacedAtColumn().Name.TextValue
                                )
                            ),
                            new DivideDecimalProjection([
                                new PiProjection(),
                                new LiteralInteger(12),
                            ]),
                        ])
                    )
                ),
                new SelectItemProjection(
                    "hour_y",
                    new SinDecimalProjection(
                        new MultiplyDecimalProjection([
                            new HourDatetimeIntegerProjection(
                                new FieldDatetime(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new OrdersTable().Name,
                                        ]
                                    ).TextValue,
                                    new PlacedAtColumn().Name.TextValue
                                )
                            ),
                            new DivideDecimalProjection([
                                new PiProjection(),
                                new LiteralInteger(12),
                            ]),
                        ])
                    )
                ),
                new SelectItemProjection(
                    "angle_back_in_degrees",
                    new MultiplyDecimalProjection([
                        new Atan2DecimalProjection(
                            new SinDecimalProjection(
                                new MultiplyDecimalProjection([
                                    new HourDatetimeIntegerProjection(
                                        new FieldDatetime(
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new RelationalSchemaWithForeignKeys().Name,
                                                    new OrdersTable().Name,
                                                ]
                                            ).TextValue,
                                            new PlacedAtColumn().Name.TextValue
                                        )
                                    ),
                                    new DivideDecimalProjection([
                                        new PiProjection(),
                                        new LiteralInteger(12),
                                    ]),
                                ])
                            ),
                            new CosDecimalProjection(
                                new MultiplyDecimalProjection([
                                    new HourDatetimeIntegerProjection(
                                        new FieldDatetime(
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new RelationalSchemaWithForeignKeys().Name,
                                                    new OrdersTable().Name,
                                                ]
                                            ).TextValue,
                                            new PlacedAtColumn().Name.TextValue
                                        )
                                    ),
                                    new DivideDecimalProjection([
                                        new PiProjection(),
                                        new LiteralInteger(12),
                                    ]),
                                ])
                            )
                        ),
                        new DivideDecimalProjection([
                            new LiteralInteger(180),
                            new PiProjection(),
                        ]),
                    ])
                ),
                new SelectItemProjection("pi", new PiProjection()),
            ],
            orderBy:
            [
                new OrderItemProjectionExpression(
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
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and
    /// <see cref="AuditSchemaDataSet"/>, in this order.
    /// The trigonometric values are the exact ones rounded at each step as the
    /// specification rounds a decimal, computed with Python decimal at 120 digits, since
    /// PostgreSQL computes them in double precision.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new OrderIdColumn(),
                    new Column(new String("hour"), new LongColumnType()),
                    new Column(new String("hour_angle"), new DoubleColumnType()),
                    new Column(new String("hour_x"), new DoubleColumnType()),
                    new Column(new String("hour_y"), new DoubleColumnType()),
                    new Column(
                        new String("angle_back_in_degrees"),
                        new DoubleColumnType()
                    ),
                    new Column(new String("pi"), new DoubleColumnType()),
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
                                new Column(new String("hour"), new LongColumnType()),
                                new InvariantCell(new Long(10))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("hour_angle"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(2.6179938779914944))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("hour_x"), new DoubleColumnType()),
                                new InvariantCell(new Double(-0.8660254037844386))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("hour_y"), new DoubleColumnType()),
                                new InvariantCell(new Double(0.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("angle_back_in_degrees"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(150))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("pi"), new DoubleColumnType()),
                                new InvariantCell(new Double(3.141592653589793))
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
                                new Column(new String("hour"), new LongColumnType()),
                                new InvariantCell(new Long(11))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("hour_angle"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(2.879793265790644))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("hour_x"), new DoubleColumnType()),
                                new InvariantCell(new Double(-0.9659258262890683))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("hour_y"), new DoubleColumnType()),
                                new InvariantCell(new Double(0.25881904510252074))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("angle_back_in_degrees"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(165))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("pi"), new DoubleColumnType()),
                                new InvariantCell(new Double(3.141592653589793))
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
                                new Column(new String("hour"), new LongColumnType()),
                                new InvariantCell(new Long(12))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("hour_angle"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(3.141592653589793))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("hour_x"), new DoubleColumnType()),
                                new InvariantCell(new Double(-1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("hour_y"), new DoubleColumnType()),
                                new InvariantCell(new Double(-7E-28))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("angle_back_in_degrees"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-180))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("pi"), new DoubleColumnType()),
                                new InvariantCell(new Double(3.141592653589793))
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
                                new Column(new String("hour"), new LongColumnType()),
                                new InvariantCell(new Long(13))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("hour_angle"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(3.4033920413889427))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("hour_x"), new DoubleColumnType()),
                                new InvariantCell(new Double(-0.9659258262890683))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("hour_y"), new DoubleColumnType()),
                                new InvariantCell(new Double(-0.25881904510252074))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("angle_back_in_degrees"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-165))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("pi"), new DoubleColumnType()),
                                new InvariantCell(new Double(3.141592653589793))
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
                                new Column(new String("hour"), new LongColumnType()),
                                new InvariantCell(new Long(14))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("hour_angle"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(3.6651914291880923))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("hour_x"), new DoubleColumnType()),
                                new InvariantCell(new Double(-0.8660254037844386))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("hour_y"), new DoubleColumnType()),
                                new InvariantCell(new Double(-0.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("angle_back_in_degrees"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-150))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("pi"), new DoubleColumnType()),
                                new InvariantCell(new Double(3.141592653589793))
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
                                new Column(new String("hour"), new LongColumnType()),
                                new InvariantCell(new Long(15))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("hour_angle"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(3.9269908169872414))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("hour_x"), new DoubleColumnType()),
                                new InvariantCell(new Double(-0.7071067811865476))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("hour_y"), new DoubleColumnType()),
                                new InvariantCell(new Double(-0.7071067811865476))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("angle_back_in_degrees"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-135))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("pi"), new DoubleColumnType()),
                                new InvariantCell(new Double(3.141592653589793))
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
