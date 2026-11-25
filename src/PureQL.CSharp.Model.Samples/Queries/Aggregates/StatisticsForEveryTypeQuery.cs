using Pure.Collections.Generic;
using Pure.Primitives.Bool;
using Pure.Primitives.Date;
using Pure.Primitives.Number;
using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.Primitives.Time;
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
using PureQL.CSharp.Model.RowExpressions;
using PureQL.CSharp.Model.Samples.Cells;
using PureQL.CSharp.Model.TotalExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using DateTime = Pure.Primitives.DateTime.DateTime;
using Double = Pure.Primitives.Number.Double;
using Guid = Pure.Primitives.Guid.Guid;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Aggregates;

/// <summary>
/// Aggregates schema_with_foreign_keys.orders inner joined to
/// schema_with_foreign_keys.users on order_user_id = user_id into one row, over every
/// type the statistics take: the median and the continuous percentile of 0.25 of
/// order_total, placed_on, the shift_start of the user and placed_at; the discrete
/// percentile of 0.75 of order_total, order_status, user_active, placed_on, shift_start,
/// placed_at and order_user_id; the mode of the same seven; the median of a nullable date
/// (the shipping date of a shipped order, placed_on plus order_total over 150 rounded up
/// in days, null for the others), the median and the mode of the days to ship, the
/// discrete percentile of 0 and the continuous percentile of 1 of order_total, and the
/// population and sample variances and standard deviations of order_total.
/// </summary>
public sealed record StatisticsForEveryTypeQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainTotalQuery(
            from: new FromEntity(
                new JoinedString(
                    new DotString(),
                    [new RelationalSchemaWithForeignKeys().Name, new OrdersTable().Name]
                ).TextValue
            ),
            joins:
            [
                new JoinEntity(
                    JoinKind.Inner,
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
            aggregate:
            [
                new SelectItemTotal(
                    "median_decimal",
                    new MedianDecimalNullableTotal(
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
                ),
                new SelectItemTotal(
                    "median_date",
                    new MedianDateNullableTotal(
                        new FieldDate(
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
                ),
                new SelectItemTotal(
                    "median_time",
                    new MedianTimeNullableTotal(
                        new FieldTime(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue,
                            new ShiftStartColumn().Name.TextValue
                        )
                    )
                ),
                new SelectItemTotal(
                    "median_datetime",
                    new MedianDatetimeNullableTotal(
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
                new SelectItemTotal(
                    "percentile_continuous_decimal",
                    new PercentileContinuousDecimalNullableTotal(
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
                        new LiteralDecimal(0.25m)
                    )
                ),
                new SelectItemTotal(
                    "percentile_continuous_date",
                    new PercentileContinuousDateNullableTotal(
                        new FieldDate(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrdersTable().Name,
                                ]
                            ).TextValue,
                            new PlacedOnColumn().Name.TextValue
                        ),
                        new LiteralDecimal(0.25m)
                    )
                ),
                new SelectItemTotal(
                    "percentile_continuous_time",
                    new PercentileContinuousTimeNullableTotal(
                        new FieldTime(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue,
                            new ShiftStartColumn().Name.TextValue
                        ),
                        new LiteralDecimal(0.25m)
                    )
                ),
                new SelectItemTotal(
                    "percentile_continuous_datetime",
                    new PercentileContinuousDatetimeNullableTotal(
                        new FieldDatetime(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrdersTable().Name,
                                ]
                            ).TextValue,
                            new PlacedAtColumn().Name.TextValue
                        ),
                        new LiteralDecimal(0.25m)
                    )
                ),
                new SelectItemTotal(
                    "percentile_discrete_decimal",
                    new PercentileDiscreteDecimalNullableTotal(
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
                        new LiteralDecimal(0.75m)
                    )
                ),
                new SelectItemTotal(
                    "percentile_discrete_string",
                    new PercentileDiscreteStringNullableTotal(
                        new FieldString(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrdersTable().Name,
                                ]
                            ).TextValue,
                            new OrderStatusColumn().Name.TextValue
                        ),
                        new LiteralDecimal(0.75m)
                    )
                ),
                new SelectItemTotal(
                    "percentile_discrete_boolean",
                    new PercentileDiscreteBooleanNullableTotal(
                        new FieldBoolean(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue,
                            new UserActiveColumn().Name.TextValue
                        ),
                        new LiteralDecimal(0.75m)
                    )
                ),
                new SelectItemTotal(
                    "percentile_discrete_date",
                    new PercentileDiscreteDateNullableTotal(
                        new FieldDate(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrdersTable().Name,
                                ]
                            ).TextValue,
                            new PlacedOnColumn().Name.TextValue
                        ),
                        new LiteralDecimal(0.75m)
                    )
                ),
                new SelectItemTotal(
                    "percentile_discrete_time",
                    new PercentileDiscreteTimeNullableTotal(
                        new FieldTime(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue,
                            new ShiftStartColumn().Name.TextValue
                        ),
                        new LiteralDecimal(0.75m)
                    )
                ),
                new SelectItemTotal(
                    "percentile_discrete_datetime",
                    new PercentileDiscreteDatetimeNullableTotal(
                        new FieldDatetime(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrdersTable().Name,
                                ]
                            ).TextValue,
                            new PlacedAtColumn().Name.TextValue
                        ),
                        new LiteralDecimal(0.75m)
                    )
                ),
                new SelectItemTotal(
                    "percentile_discrete_uuid",
                    new PercentileDiscreteUuidNullableTotal(
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
                        new LiteralDecimal(0.75m)
                    )
                ),
                new SelectItemTotal(
                    "mode_decimal",
                    new ModeDecimalNullableTotal(
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
                ),
                new SelectItemTotal(
                    "mode_string",
                    new ModeStringNullableTotal(
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
                ),
                new SelectItemTotal(
                    "mode_boolean",
                    new ModeBooleanNullableTotal(
                        new FieldBoolean(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue,
                            new UserActiveColumn().Name.TextValue
                        )
                    )
                ),
                new SelectItemTotal(
                    "mode_date",
                    new ModeDateNullableTotal(
                        new FieldDate(
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
                ),
                new SelectItemTotal(
                    "mode_time",
                    new ModeTimeNullableTotal(
                        new FieldTime(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue,
                            new ShiftStartColumn().Name.TextValue
                        )
                    )
                ),
                new SelectItemTotal(
                    "mode_datetime",
                    new ModeDatetimeNullableTotal(
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
                new SelectItemTotal(
                    "mode_uuid",
                    new ModeUuidNullableTotal(
                        new FieldUuid(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrdersTable().Name,
                                ]
                            ).TextValue,
                            new OrderUserIdColumn().Name.TextValue
                        )
                    )
                ),
                new SelectItemTotal(
                    "median_of_a_nullable_date",
                    new MedianDateNullableTotal(
                        new IfDateNullableRow(
                            new DateAddDaysDateRow(
                                new FieldDate(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new OrdersTable().Name,
                                        ]
                                    ).TextValue,
                                    new PlacedOnColumn().Name.TextValue
                                ),
                                new CeilingIntegerRow(
                                    new DivideDecimalRow([
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
                                        new LiteralDecimal(150m),
                                    ])
                                )
                            ),
                            new LiteralDateNullable(),
                            new EqualStringRow(
                                new FieldString(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new OrdersTable().Name,
                                        ]
                                    ).TextValue,
                                    new OrderStatusColumn().Name.TextValue
                                ),
                                new LiteralString("shipped")
                            )
                        )
                    )
                ),
                new SelectItemTotal(
                    "median_days_to_ship",
                    new MedianDecimalNullableTotal(
                        new DateDiffDaysIntegerNullableRow(
                            new IfDateNullableRow(
                                new DateAddDaysDateRow(
                                    new FieldDate(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new OrdersTable().Name,
                                            ]
                                        ).TextValue,
                                        new PlacedOnColumn().Name.TextValue
                                    ),
                                    new CeilingIntegerRow(
                                        new DivideDecimalRow([
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
                                            new LiteralDecimal(150m),
                                        ])
                                    )
                                ),
                                new LiteralDateNullable(),
                                new EqualStringRow(
                                    new FieldString(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new OrdersTable().Name,
                                            ]
                                        ).TextValue,
                                        new OrderStatusColumn().Name.TextValue
                                    ),
                                    new LiteralString("shipped")
                                )
                            ),
                            new FieldDate(
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
                ),
                new SelectItemTotal(
                    "usual_days_to_ship",
                    new ModeIntegerNullableTotal(
                        new DateDiffDaysIntegerNullableRow(
                            new IfDateNullableRow(
                                new DateAddDaysDateRow(
                                    new FieldDate(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new OrdersTable().Name,
                                            ]
                                        ).TextValue,
                                        new PlacedOnColumn().Name.TextValue
                                    ),
                                    new CeilingIntegerRow(
                                        new DivideDecimalRow([
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
                                            new LiteralDecimal(150m),
                                        ])
                                    )
                                ),
                                new LiteralDateNullable(),
                                new EqualStringRow(
                                    new FieldString(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new OrdersTable().Name,
                                            ]
                                        ).TextValue,
                                        new OrderStatusColumn().Name.TextValue
                                    ),
                                    new LiteralString("shipped")
                                )
                            ),
                            new FieldDate(
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
                ),
                new SelectItemTotal(
                    "smallest_total",
                    new PercentileDiscreteDecimalNullableTotal(
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
                        new LiteralDecimal(0m)
                    )
                ),
                new SelectItemTotal(
                    "largest_total",
                    new PercentileContinuousDecimalNullableTotal(
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
                        new LiteralDecimal(1m)
                    )
                ),
                new SelectItemTotal(
                    "variance_population",
                    new VariancePopulationTotal(
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
                ),
                new SelectItemTotal(
                    "variance_sample",
                    new VarianceSampleTotal(
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
                ),
                new SelectItemTotal(
                    "standard_deviation_population",
                    new StandardDeviationPopulationTotal(
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
                ),
                new SelectItemTotal(
                    "standard_deviation_sample",
                    new StandardDeviationSampleTotal(
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
                ),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and
    /// <see cref="AuditSchemaDataSet"/>, in no particular order.
    /// The statistics are computed exactly with Python decimal and fractions, as the
    /// specification defines them, and rounded as it rounds a decimal.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("median_decimal"), new DoubleColumnType()),
                    new Column(new String("median_date"), new DateColumnType()),
                    new Column(new String("median_time"), new TimeColumnType()),
                    new Column(new String("median_datetime"), new DateTimeColumnType()),
                    new Column(
                        new String("percentile_continuous_decimal"),
                        new DoubleColumnType()
                    ),
                    new Column(
                        new String("percentile_continuous_date"),
                        new DateColumnType()
                    ),
                    new Column(
                        new String("percentile_continuous_time"),
                        new TimeColumnType()
                    ),
                    new Column(
                        new String("percentile_continuous_datetime"),
                        new DateTimeColumnType()
                    ),
                    new Column(
                        new String("percentile_discrete_decimal"),
                        new DoubleColumnType()
                    ),
                    new Column(
                        new String("percentile_discrete_string"),
                        new StringColumnType()
                    ),
                    new Column(
                        new String("percentile_discrete_boolean"),
                        new BoolColumnType()
                    ),
                    new Column(
                        new String("percentile_discrete_date"),
                        new DateColumnType()
                    ),
                    new Column(
                        new String("percentile_discrete_time"),
                        new TimeColumnType()
                    ),
                    new Column(
                        new String("percentile_discrete_datetime"),
                        new DateTimeColumnType()
                    ),
                    new Column(
                        new String("percentile_discrete_uuid"),
                        new UuidColumnType()
                    ),
                    new Column(new String("mode_decimal"), new DoubleColumnType()),
                    new Column(new String("mode_string"), new StringColumnType()),
                    new Column(new String("mode_boolean"), new BoolColumnType()),
                    new Column(new String("mode_date"), new DateColumnType()),
                    new Column(new String("mode_time"), new TimeColumnType()),
                    new Column(new String("mode_datetime"), new DateTimeColumnType()),
                    new Column(new String("mode_uuid"), new UuidColumnType()),
                    new Column(
                        new String("median_of_a_nullable_date"),
                        new DateColumnType()
                    ),
                    new Column(new String("median_days_to_ship"), new DoubleColumnType()),
                    new Column(new String("usual_days_to_ship"), new LongColumnType()),
                    new Column(new String("smallest_total"), new DoubleColumnType()),
                    new Column(new String("largest_total"), new DoubleColumnType()),
                    new Column(new String("variance_population"), new DoubleColumnType()),
                    new Column(new String("variance_sample"), new DoubleColumnType()),
                    new Column(
                        new String("standard_deviation_population"),
                        new DoubleColumnType()
                    ),
                    new Column(
                        new String("standard_deviation_sample"),
                        new DoubleColumnType()
                    ),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("median_decimal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(100.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("median_date"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(4),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("median_time"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(
                                    new Time(new UShort(9), new UShort(0), new UShort(0))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("median_datetime"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(4),
                                            new UShort(6),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(0),
                                            new UShort(30),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("percentile_continuous_decimal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(81.5625))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("percentile_continuous_date"),
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
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("percentile_continuous_time"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(
                                    new Time(new UShort(9), new UShort(0), new UShort(0))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("percentile_continuous_datetime"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(2),
                                            new UShort(6),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(17),
                                            new UShort(15),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("percentile_discrete_decimal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(200.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("percentile_discrete_string"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("shipped"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("percentile_discrete_boolean"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("percentile_discrete_date"),
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
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("percentile_discrete_time"),
                                    new TimeColumnType()
                                ),
                                new InvariantCell(
                                    new Time(new UShort(10), new UShort(0), new UShort(0))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("percentile_discrete_datetime"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(5),
                                            new UShort(6),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(14),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("percentile_discrete_uuid"),
                                    new UuidColumnType()
                                ),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000003-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("mode_decimal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(100.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("mode_string"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("shipped"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("mode_boolean"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("mode_date"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(1),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("mode_time"), new TimeColumnType()),
                                new InvariantCell(
                                    new Time(new UShort(9), new UShort(0), new UShort(0))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("mode_datetime"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(1),
                                            new UShort(6),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(10),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("mode_uuid"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000001-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("median_of_a_nullable_date"),
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
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("median_days_to_ship"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(2.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("usual_days_to_ship"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("smallest_total"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(50.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("largest_total"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(300.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("variance_population"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(7430.258680555556))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("variance_sample"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(8916.310416666667))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("standard_deviation_population"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(86.19894825666701))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("standard_deviation_sample"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(94.42621678679426))
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
