using Pure.Collections.Generic;
using Pure.Primitives.String;
using Pure.RelationalSchema.Abstractions.Column;
using Pure.RelationalSchema.ColumnType;
using Pure.RelationalSchema.HashCodes;
using Pure.RelationalSchema.Samples.Columns;
using Pure.RelationalSchema.Storage;
using Pure.RelationalSchema.Storage.Abstractions;
using Pure.RelationalSchema.Storage.Samples.Cells;
using Pure.RelationalSchema.Storage.Samples.SchemaDataSets;
using Pure.RelationalSchema.Storage.Samples.TableDataSets;
using PureQL.CSharp.Model.Fields;
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.ProjectionExpressions;
using PureQL.CSharp.Model.RowExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Numbers;

/// <summary>
/// Selects from the inline rows stores (Berlin, Paris and Madrid with a latitude and a
/// longitude in degrees) the stores within 600 kilometres of 50.1109 N, 8.6821 E by the
/// haversine formula (sin, cos, asin, sqrt, power and pi over the coordinates converted
/// to radians, times 2 and the radius 6371.0088): name, distance_km (that distance
/// rounded to 1 digit) and bearing_degrees (atan2 of the sine of the longitude difference
/// times the cosine of the latitude and the difference of the cosine-sine products, times
/// 180 divided by pi), ordered by the distance and name.
/// </summary>
public sealed record GreatCircleDistanceQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithoutPagination(
            subqueries:
            [
                new SubqueryRows(
                    "stores",
                    [
                        [
                            new RowItem("name", new LiteralString("Berlin")),
                            new RowItem("latitude", new LiteralDecimal(52.52m)),
                            new RowItem("longitude", new LiteralDecimal(13.405m)),
                        ],
                        [
                            new RowItem("name", new LiteralString("Paris")),
                            new RowItem("latitude", new LiteralDecimal(48.8566m)),
                            new RowItem("longitude", new LiteralDecimal(2.3522m)),
                        ],
                        [
                            new RowItem("name", new LiteralString("Madrid")),
                            new RowItem("latitude", new LiteralDecimal(40.4168m)),
                            new RowItem("longitude", new LiteralDecimal(-3.7038m)),
                        ],
                    ]
                ),
            ],
            from: new FromSubquery("stores"),
            where: new LessThanOrEqualDecimalRow(
                new MultiplyDecimalRow([
                    new LiteralInteger(2),
                    new LiteralDecimal(6371.0088m),
                    new AsinDecimalRow(
                        new SqrtDecimalRow(
                            new AddDecimalRow([
                                new PowerDecimalRow(
                                    new SinDecimalRow(
                                        new DivideDecimalRow([
                                            new SubtractDecimalRow([
                                                new MultiplyDecimalRow([
                                                    new FieldDecimal(
                                                        "stores",
                                                        "latitude"
                                                    ),
                                                    new DivideDecimalRow([
                                                        new PiRow(),
                                                        new LiteralInteger(180),
                                                    ]),
                                                ]),
                                                new MultiplyDecimalRow([
                                                    new LiteralDecimal(50.1109m),
                                                    new DivideDecimalRow([
                                                        new PiRow(),
                                                        new LiteralInteger(180),
                                                    ]),
                                                ]),
                                            ]),
                                            new LiteralInteger(2),
                                        ])
                                    ),
                                    new LiteralInteger(2)
                                ),
                                new MultiplyDecimalRow([
                                    new CosDecimalRow(
                                        new MultiplyDecimalRow([
                                            new LiteralDecimal(50.1109m),
                                            new DivideDecimalRow([
                                                new PiRow(),
                                                new LiteralInteger(180),
                                            ]),
                                        ])
                                    ),
                                    new CosDecimalRow(
                                        new MultiplyDecimalRow([
                                            new FieldDecimal("stores", "latitude"),
                                            new DivideDecimalRow([
                                                new PiRow(),
                                                new LiteralInteger(180),
                                            ]),
                                        ])
                                    ),
                                    new PowerDecimalRow(
                                        new SinDecimalRow(
                                            new DivideDecimalRow([
                                                new SubtractDecimalRow([
                                                    new MultiplyDecimalRow([
                                                        new FieldDecimal(
                                                            "stores",
                                                            "longitude"
                                                        ),
                                                        new DivideDecimalRow([
                                                            new PiRow(),
                                                            new LiteralInteger(180),
                                                        ]),
                                                    ]),
                                                    new MultiplyDecimalRow([
                                                        new LiteralDecimal(8.6821m),
                                                        new DivideDecimalRow([
                                                            new PiRow(),
                                                            new LiteralInteger(180),
                                                        ]),
                                                    ]),
                                                ]),
                                                new LiteralInteger(2),
                                            ])
                                        ),
                                        new LiteralInteger(2)
                                    ),
                                ]),
                            ])
                        )
                    ),
                ]),
                new LiteralDecimal(600m)
            ),
            select:
            [
                new SelectItemProjection("name", new FieldString("stores", "name")),
                new SelectItemProjection(
                    "distance_km",
                    new RoundDecimalProjection(
                        new MultiplyDecimalProjection([
                            new LiteralInteger(2),
                            new LiteralDecimal(6371.0088m),
                            new AsinDecimalProjection(
                                new SqrtDecimalProjection(
                                    new AddDecimalProjection([
                                        new PowerDecimalProjection(
                                            new SinDecimalProjection(
                                                new DivideDecimalProjection([
                                                    new SubtractDecimalProjection([
                                                        new MultiplyDecimalProjection([
                                                            new FieldDecimal(
                                                                "stores",
                                                                "latitude"
                                                            ),
                                                            new DivideDecimalProjection([
                                                                new PiProjection(),
                                                                new LiteralInteger(180),
                                                            ]),
                                                        ]),
                                                        new MultiplyDecimalProjection([
                                                            new LiteralDecimal(50.1109m),
                                                            new DivideDecimalProjection([
                                                                new PiProjection(),
                                                                new LiteralInteger(180),
                                                            ]),
                                                        ]),
                                                    ]),
                                                    new LiteralInteger(2),
                                                ])
                                            ),
                                            new LiteralInteger(2)
                                        ),
                                        new MultiplyDecimalProjection([
                                            new CosDecimalProjection(
                                                new MultiplyDecimalProjection([
                                                    new LiteralDecimal(50.1109m),
                                                    new DivideDecimalProjection([
                                                        new PiProjection(),
                                                        new LiteralInteger(180),
                                                    ]),
                                                ])
                                            ),
                                            new CosDecimalProjection(
                                                new MultiplyDecimalProjection([
                                                    new FieldDecimal(
                                                        "stores",
                                                        "latitude"
                                                    ),
                                                    new DivideDecimalProjection([
                                                        new PiProjection(),
                                                        new LiteralInteger(180),
                                                    ]),
                                                ])
                                            ),
                                            new PowerDecimalProjection(
                                                new SinDecimalProjection(
                                                    new DivideDecimalProjection([
                                                        new SubtractDecimalProjection([
                                                            new MultiplyDecimalProjection([
                                                                new FieldDecimal(
                                                                    "stores",
                                                                    "longitude"
                                                                ),
                                                                new DivideDecimalProjection([
                                                                    new PiProjection(),
                                                                    new LiteralInteger(
                                                                        180
                                                                    ),
                                                                ]),
                                                            ]),
                                                            new MultiplyDecimalProjection([
                                                                new LiteralDecimal(
                                                                    8.6821m
                                                                ),
                                                                new DivideDecimalProjection([
                                                                    new PiProjection(),
                                                                    new LiteralInteger(
                                                                        180
                                                                    ),
                                                                ]),
                                                            ]),
                                                        ]),
                                                        new LiteralInteger(2),
                                                    ])
                                                ),
                                                new LiteralInteger(2)
                                            ),
                                        ]),
                                    ])
                                )
                            ),
                        ]),
                        new LiteralInteger(1)
                    )
                ),
                new SelectItemProjection(
                    "bearing_degrees",
                    new MultiplyDecimalProjection([
                        new Atan2DecimalProjection(
                            new MultiplyDecimalProjection([
                                new SinDecimalProjection(
                                    new SubtractDecimalProjection([
                                        new MultiplyDecimalProjection([
                                            new FieldDecimal("stores", "longitude"),
                                            new DivideDecimalProjection([
                                                new PiProjection(),
                                                new LiteralInteger(180),
                                            ]),
                                        ]),
                                        new MultiplyDecimalProjection([
                                            new LiteralDecimal(8.6821m),
                                            new DivideDecimalProjection([
                                                new PiProjection(),
                                                new LiteralInteger(180),
                                            ]),
                                        ]),
                                    ])
                                ),
                                new CosDecimalProjection(
                                    new MultiplyDecimalProjection([
                                        new FieldDecimal("stores", "latitude"),
                                        new DivideDecimalProjection([
                                            new PiProjection(),
                                            new LiteralInteger(180),
                                        ]),
                                    ])
                                ),
                            ]),
                            new SubtractDecimalProjection([
                                new MultiplyDecimalProjection([
                                    new CosDecimalProjection(
                                        new MultiplyDecimalProjection([
                                            new LiteralDecimal(50.1109m),
                                            new DivideDecimalProjection([
                                                new PiProjection(),
                                                new LiteralInteger(180),
                                            ]),
                                        ])
                                    ),
                                    new SinDecimalProjection(
                                        new MultiplyDecimalProjection([
                                            new FieldDecimal("stores", "latitude"),
                                            new DivideDecimalProjection([
                                                new PiProjection(),
                                                new LiteralInteger(180),
                                            ]),
                                        ])
                                    ),
                                ]),
                                new MultiplyDecimalProjection([
                                    new SinDecimalProjection(
                                        new MultiplyDecimalProjection([
                                            new LiteralDecimal(50.1109m),
                                            new DivideDecimalProjection([
                                                new PiProjection(),
                                                new LiteralInteger(180),
                                            ]),
                                        ])
                                    ),
                                    new CosDecimalProjection(
                                        new MultiplyDecimalProjection([
                                            new FieldDecimal("stores", "latitude"),
                                            new DivideDecimalProjection([
                                                new PiProjection(),
                                                new LiteralInteger(180),
                                            ]),
                                        ])
                                    ),
                                    new CosDecimalProjection(
                                        new SubtractDecimalProjection([
                                            new MultiplyDecimalProjection([
                                                new FieldDecimal("stores", "longitude"),
                                                new DivideDecimalProjection([
                                                    new PiProjection(),
                                                    new LiteralInteger(180),
                                                ]),
                                            ]),
                                            new MultiplyDecimalProjection([
                                                new LiteralDecimal(8.6821m),
                                                new DivideDecimalProjection([
                                                    new PiProjection(),
                                                    new LiteralInteger(180),
                                                ]),
                                            ]),
                                        ])
                                    ),
                                ]),
                            ])
                        ),
                        new DivideDecimalProjection([
                            new LiteralInteger(180),
                            new PiProjection(),
                        ]),
                    ])
                ),
            ],
            orderBy:
            [
                new OrderItemProjectionExpression(
                    new MultiplyDecimalProjection([
                        new LiteralInteger(2),
                        new LiteralDecimal(6371.0088m),
                        new AsinDecimalProjection(
                            new SqrtDecimalProjection(
                                new AddDecimalProjection([
                                    new PowerDecimalProjection(
                                        new SinDecimalProjection(
                                            new DivideDecimalProjection([
                                                new SubtractDecimalProjection([
                                                    new MultiplyDecimalProjection([
                                                        new FieldDecimal(
                                                            "stores",
                                                            "latitude"
                                                        ),
                                                        new DivideDecimalProjection([
                                                            new PiProjection(),
                                                            new LiteralInteger(180),
                                                        ]),
                                                    ]),
                                                    new MultiplyDecimalProjection([
                                                        new LiteralDecimal(50.1109m),
                                                        new DivideDecimalProjection([
                                                            new PiProjection(),
                                                            new LiteralInteger(180),
                                                        ]),
                                                    ]),
                                                ]),
                                                new LiteralInteger(2),
                                            ])
                                        ),
                                        new LiteralInteger(2)
                                    ),
                                    new MultiplyDecimalProjection([
                                        new CosDecimalProjection(
                                            new MultiplyDecimalProjection([
                                                new LiteralDecimal(50.1109m),
                                                new DivideDecimalProjection([
                                                    new PiProjection(),
                                                    new LiteralInteger(180),
                                                ]),
                                            ])
                                        ),
                                        new CosDecimalProjection(
                                            new MultiplyDecimalProjection([
                                                new FieldDecimal("stores", "latitude"),
                                                new DivideDecimalProjection([
                                                    new PiProjection(),
                                                    new LiteralInteger(180),
                                                ]),
                                            ])
                                        ),
                                        new PowerDecimalProjection(
                                            new SinDecimalProjection(
                                                new DivideDecimalProjection([
                                                    new SubtractDecimalProjection([
                                                        new MultiplyDecimalProjection([
                                                            new FieldDecimal(
                                                                "stores",
                                                                "longitude"
                                                            ),
                                                            new DivideDecimalProjection([
                                                                new PiProjection(),
                                                                new LiteralInteger(180),
                                                            ]),
                                                        ]),
                                                        new MultiplyDecimalProjection([
                                                            new LiteralDecimal(8.6821m),
                                                            new DivideDecimalProjection([
                                                                new PiProjection(),
                                                                new LiteralInteger(180),
                                                            ]),
                                                        ]),
                                                    ]),
                                                    new LiteralInteger(2),
                                                ])
                                            ),
                                            new LiteralInteger(2)
                                        ),
                                    ]),
                                ])
                            )
                        ),
                    ])
                ),
                new OrderItemProjectionExpression(new FieldString("stores", "name")),
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
                    new NameColumn(),
                    new Column(new String("distance_km"), new DoubleColumnType()),
                    new Column(new String("bearing_degrees"), new DoubleColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new NameColumn(),
                                new InvariantCell(new String("Berlin"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("distance_km"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(423.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("bearing_degrees"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(48.95589927738846))
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
                                new NameColumn(),
                                new InvariantCell(new String("Paris"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("distance_km"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(477.9))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("bearing_degrees"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-104.53847359097716))
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
