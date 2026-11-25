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
using PureQL.CSharp.Model.RowExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using Guid = Pure.Primitives.Guid.Guid;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Numbers;

/// <summary>
/// Selects order_id from schema_with_foreign_keys.orders left joined to
/// schema_with_foreign_keys.users on order_user_id, next to the trigonometry of x, the
/// order_total divided by 100: sin squared plus cos squared, tan, sin divided by cos,
/// asin of its sin and atan of its tan (which give x back only within a quarter turn),
/// acos of 0 as half pi, atan of 1 as a quarter pi, atan2 of 0 and -1 as pi, the sin of
/// pi, which is not zero, the cos of pi, asin of 0.5 times 180 divided by pi as degrees,
/// the sin of 30 times pi divided by 180, the cos of the nullable user_score and its
/// atan2 with 1, 2 times pi, and pi times order_total squared as an area, ordered by
/// order_id.
/// </summary>
public sealed record TrigonometricIdentitiesQuery
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
            joins:
            [
                new JoinEntity(
                    JoinKind.Left,
                    new JoinedString(
                        new DotString(),
                        [
                            new RelationalSchemaWithForeignKeys().Name,
                            new UsersTable().Name,
                        ]
                    ).TextValue,
                    new EqualUuidRow(
                        new FieldUuid(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrdersTable().Name,
                                ]
                            ).TextValue,
                            new OrderUserIdColumn().Name.TextValue
                        ),
                        new FieldUuid(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue,
                            new UserIdColumn().Name.TextValue
                        )
                    )
                ),
            ],
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
                    "sine_squared_plus_cosine_squared",
                    new AddDecimalProjection([
                        new PowerDecimalProjection(
                            new SinDecimalProjection(
                                new DivideDecimalProjection([
                                    new FieldDecimal(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new OrdersTable().Name,
                                            ]
                                        ).TextValue,
                                        new OrderTotalColumn().Name.TextValue
                                    ),
                                    new LiteralInteger(100),
                                ])
                            ),
                            new LiteralInteger(2)
                        ),
                        new PowerDecimalProjection(
                            new CosDecimalProjection(
                                new DivideDecimalProjection([
                                    new FieldDecimal(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new OrdersTable().Name,
                                            ]
                                        ).TextValue,
                                        new OrderTotalColumn().Name.TextValue
                                    ),
                                    new LiteralInteger(100),
                                ])
                            ),
                            new LiteralInteger(2)
                        ),
                    ])
                ),
                new SelectItemProjection(
                    "tangent",
                    new TanDecimalProjection(
                        new DivideDecimalProjection([
                            new FieldDecimal(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new OrdersTable().Name,
                                    ]
                                ).TextValue,
                                new OrderTotalColumn().Name.TextValue
                            ),
                            new LiteralInteger(100),
                        ])
                    )
                ),
                new SelectItemProjection(
                    "sine_over_cosine",
                    new DivideDecimalProjection([
                        new SinDecimalProjection(
                            new DivideDecimalProjection([
                                new FieldDecimal(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new OrdersTable().Name,
                                        ]
                                    ).TextValue,
                                    new OrderTotalColumn().Name.TextValue
                                ),
                                new LiteralInteger(100),
                            ])
                        ),
                        new CosDecimalProjection(
                            new DivideDecimalProjection([
                                new FieldDecimal(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new OrdersTable().Name,
                                        ]
                                    ).TextValue,
                                    new OrderTotalColumn().Name.TextValue
                                ),
                                new LiteralInteger(100),
                            ])
                        ),
                    ])
                ),
                new SelectItemProjection(
                    "angle_of_its_sine",
                    new AsinDecimalProjection(
                        new SinDecimalProjection(
                            new DivideDecimalProjection([
                                new FieldDecimal(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new OrdersTable().Name,
                                        ]
                                    ).TextValue,
                                    new OrderTotalColumn().Name.TextValue
                                ),
                                new LiteralInteger(100),
                            ])
                        )
                    )
                ),
                new SelectItemProjection(
                    "angle_of_its_tangent",
                    new AtanDecimalProjection(
                        new TanDecimalProjection(
                            new DivideDecimalProjection([
                                new FieldDecimal(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new OrdersTable().Name,
                                        ]
                                    ).TextValue,
                                    new OrderTotalColumn().Name.TextValue
                                ),
                                new LiteralInteger(100),
                            ])
                        )
                    )
                ),
                new SelectItemProjection(
                    "half_pi",
                    new AcosDecimalProjection(new LiteralInteger(0))
                ),
                new SelectItemProjection(
                    "quarter_pi",
                    new AtanDecimalProjection(new LiteralInteger(1))
                ),
                new SelectItemProjection(
                    "pi_from_atan2",
                    new Atan2DecimalProjection(
                        new LiteralInteger(0),
                        new LiteralInteger(-1)
                    )
                ),
                new SelectItemProjection(
                    "sine_of_pi_is_not_zero",
                    new SinDecimalProjection(new PiProjection())
                ),
                new SelectItemProjection(
                    "cosine_of_pi",
                    new CosDecimalProjection(new PiProjection())
                ),
                new SelectItemProjection(
                    "in_degrees",
                    new MultiplyDecimalProjection([
                        new AsinDecimalProjection(new LiteralDecimal(0.5m)),
                        new DivideDecimalProjection([
                            new LiteralInteger(180),
                            new PiProjection(),
                        ]),
                    ])
                ),
                new SelectItemProjection(
                    "of_degrees",
                    new SinDecimalProjection(
                        new MultiplyDecimalProjection([
                            new LiteralInteger(30),
                            new DivideDecimalProjection([
                                new PiProjection(),
                                new LiteralInteger(180),
                            ]),
                        ])
                    )
                ),
                new SelectItemProjection(
                    "nullable",
                    new CosDecimalNullableProjection(
                        new FieldDecimalNullable(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue,
                            new UserScoreColumn().Name.TextValue
                        )
                    )
                ),
                new SelectItemProjection(
                    "nullable_point",
                    new Atan2DecimalNullableProjection(
                        new FieldDecimalNullable(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue,
                            new UserScoreColumn().Name.TextValue
                        ),
                        new LiteralInteger(1)
                    )
                ),
                new SelectItemProjection(
                    "two_pi",
                    new MultiplyDecimalProjection([
                        new LiteralInteger(2),
                        new PiProjection(),
                    ])
                ),
                new SelectItemProjection(
                    "area",
                    new MultiplyDecimalProjection([
                        new PiProjection(),
                        new PowerDecimalProjection(
                            new FieldDecimal(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new OrdersTable().Name,
                                    ]
                                ).TextValue,
                                new OrderTotalColumn().Name.TextValue
                            ),
                            new LiteralInteger(2)
                        ),
                    ])
                ),
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
    /// The trigonometric functions of the specification are computed with Python
    /// decimal at 120 digits, each result rounded as the specification rounds a
    /// decimal, and then written as a double.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new OrderIdColumn(),
                    new Column(
                        new String("sine_squared_plus_cosine_squared"),
                        new DoubleColumnType()
                    ),
                    new Column(new String("tangent"), new DoubleColumnType()),
                    new Column(new String("sine_over_cosine"), new DoubleColumnType()),
                    new Column(new String("angle_of_its_sine"), new DoubleColumnType()),
                    new Column(
                        new String("angle_of_its_tangent"),
                        new DoubleColumnType()
                    ),
                    new Column(new String("half_pi"), new DoubleColumnType()),
                    new Column(new String("quarter_pi"), new DoubleColumnType()),
                    new Column(new String("pi_from_atan2"), new DoubleColumnType()),
                    new Column(
                        new String("sine_of_pi_is_not_zero"),
                        new DoubleColumnType()
                    ),
                    new Column(new String("cosine_of_pi"), new DoubleColumnType()),
                    new Column(new String("in_degrees"), new DoubleColumnType()),
                    new Column(new String("of_degrees"), new DoubleColumnType()),
                    new Column(new String("nullable"), new DoubleColumnType()),
                    new Column(new String("nullable_point"), new DoubleColumnType()),
                    new Column(new String("two_pi"), new DoubleColumnType()),
                    new Column(new String("area"), new DoubleColumnType()),
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
                                new Column(
                                    new String("sine_squared_plus_cosine_squared"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("tangent"), new DoubleColumnType()),
                                new InvariantCell(new Double(1.574669883709823))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sine_over_cosine"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1.574669883709823))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("angle_of_its_sine"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1.005))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("angle_of_its_tangent"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1.005))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("half_pi"), new DoubleColumnType()),
                                new InvariantCell(new Double(1.5707963267948966))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("quarter_pi"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.7853981633974483))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("pi_from_atan2"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(3.141592653589793))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sine_of_pi_is_not_zero"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(3e-28))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("cosine_of_pi"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-1.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("in_degrees"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(30.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_degrees"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.15425144988758405))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_point"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1.5374753309166493))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("two_pi"), new DoubleColumnType()),
                                new InvariantCell(new Double(6.283185307179586))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("area"), new DoubleColumnType()),
                                new InvariantCell(new Double(31730.871199420308))
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
                                new Column(
                                    new String("sine_squared_plus_cosine_squared"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("tangent"), new DoubleColumnType()),
                                new InvariantCell(new Double(0.5463024898437905))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sine_over_cosine"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.5463024898437905))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("angle_of_its_sine"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("angle_of_its_tangent"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("half_pi"), new DoubleColumnType()),
                                new InvariantCell(new Double(1.5707963267948966))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("quarter_pi"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.7853981633974483))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("pi_from_atan2"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(3.141592653589793))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sine_of_pi_is_not_zero"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(3e-28))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("cosine_of_pi"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-1.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("in_degrees"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(30.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_degrees"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.15425144988758405))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_point"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1.5374753309166493))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("two_pi"), new DoubleColumnType()),
                                new InvariantCell(new Double(6.283185307179586))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("area"), new DoubleColumnType()),
                                new InvariantCell(new Double(7853.981633974483))
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
                                new Column(
                                    new String("sine_squared_plus_cosine_squared"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("tangent"), new DoubleColumnType()),
                                new InvariantCell(new Double(-2.185039863261519))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sine_over_cosine"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-2.185039863261519))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("angle_of_its_sine"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1.1415926535897933))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("angle_of_its_tangent"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-1.1415926535897933))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("half_pi"), new DoubleColumnType()),
                                new InvariantCell(new Double(1.5707963267948966))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("quarter_pi"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.7853981633974483))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("pi_from_atan2"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(3.141592653589793))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sine_of_pi_is_not_zero"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(3e-28))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("cosine_of_pi"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-1.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("in_degrees"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(30.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_degrees"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_point"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("two_pi"), new DoubleColumnType()),
                                new InvariantCell(new Double(6.283185307179586))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("area"), new DoubleColumnType()),
                                new InvariantCell(new Double(125663.70614359173))
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
                                new Column(
                                    new String("sine_squared_plus_cosine_squared"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("tangent"), new DoubleColumnType()),
                                new InvariantCell(new Double(0.9362770506601118))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sine_over_cosine"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.9362770506601118))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("angle_of_its_sine"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.7525))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("angle_of_its_tangent"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.7525))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("half_pi"), new DoubleColumnType()),
                                new InvariantCell(new Double(1.5707963267948966))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("quarter_pi"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.7853981633974483))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("pi_from_atan2"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(3.141592653589793))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sine_of_pi_is_not_zero"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(3e-28))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("cosine_of_pi"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-1.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("in_degrees"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(30.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_degrees"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.15425144988758405))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_point"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1.5374753309166493))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("two_pi"), new DoubleColumnType()),
                                new InvariantCell(new Double(6.283185307179586))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("area"), new DoubleColumnType()),
                                new InvariantCell(new Double(17789.464750493054))
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
                                new Column(
                                    new String("sine_squared_plus_cosine_squared"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("tangent"), new DoubleColumnType()),
                                new InvariantCell(new Double(-0.1425465430742778))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sine_over_cosine"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-0.1425465430742778))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("angle_of_its_sine"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.14159265358979323))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("angle_of_its_tangent"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-0.14159265358979323))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("half_pi"), new DoubleColumnType()),
                                new InvariantCell(new Double(1.5707963267948966))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("quarter_pi"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.7853981633974483))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("pi_from_atan2"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(3.141592653589793))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sine_of_pi_is_not_zero"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(3e-28))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("cosine_of_pi"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-1.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("in_degrees"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(30.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_degrees"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.15425144988758405))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_point"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1.5374753309166493))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("two_pi"), new DoubleColumnType()),
                                new InvariantCell(new Double(6.283185307179586))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("area"), new DoubleColumnType()),
                                new InvariantCell(new Double(282743.3388230814))
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
                                new Column(
                                    new String("sine_squared_plus_cosine_squared"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("tangent"), new DoubleColumnType()),
                                new InvariantCell(new Double(1.574669883709823))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sine_over_cosine"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1.574669883709823))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("angle_of_its_sine"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1.005))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("angle_of_its_tangent"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1.005))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("half_pi"), new DoubleColumnType()),
                                new InvariantCell(new Double(1.5707963267948966))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("quarter_pi"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.7853981633974483))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("pi_from_atan2"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(3.141592653589793))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("sine_of_pi_is_not_zero"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(3e-28))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("cosine_of_pi"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-1.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("in_degrees"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(30.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_degrees"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_point"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("two_pi"), new DoubleColumnType()),
                                new InvariantCell(new Double(6.283185307179586))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("area"), new DoubleColumnType()),
                                new InvariantCell(new Double(31730.871199420308))
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
