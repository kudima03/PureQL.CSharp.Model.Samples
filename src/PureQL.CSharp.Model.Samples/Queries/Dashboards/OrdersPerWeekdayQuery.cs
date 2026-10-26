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
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.RowExpressions;
using PureQL.CSharp.Model.SelectItems;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Dashboards;

/// <summary>
/// Selects weekday, the count of rows as orders and the decimal 0 if that count is the
/// integer 0, else the decimal 100 times the count of rows whose order_tenant_id is the
/// uuid 00000385-0000-0000-0000-000000000000 divided by the count, rounded to 1 digit, as
/// tenant_385_share_pct from schema_with_foreign_keys.orders, where order_status is not
/// equal to cancelled, grouped by 1 Monday through 7 Sunday as weekday depending on
/// whether the days from 2024-05-27 to placed_on modulo 7 is 0 through 5, ordered by
/// weekday.
/// </summary>
public sealed record OrdersPerWeekdayQuery
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
                                "weekday",
                                new StringRow(
                                    new ConditionalStringRow(
                                        new IfStringRow(
                                            new BooleanRow(
                                                new ComparisonRow(
                                                    new EqualRow(
                                                        new EqualDecimalRow(
                                                            new DecimalNullableRow(
                                                                new IntegerRow(
                                                                    new ArithmeticIntegerRow(
                                                                        new ModuloIntegerRow(
                                                                            new IntegerRow(
                                                                                new DateDiffDaysIntegerRow(
                                                                                    new DateRow(
                                                                                        new FieldDate(
                                                                                            new JoinedString(
                                                                                                new DotString(),
                                                                                                [
                                                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                                                    new OrdersTable().Name,
                                                                                                ]
                                                                                            ).TextValue,
                                                                                            new PlacedOnColumn()
                                                                                                .Name
                                                                                                .TextValue
                                                                                        )
                                                                                    ),
                                                                                    new DateRow(
                                                                                        new LiteralDate(
                                                                                            new DateOnly(
                                                                                                2024,
                                                                                                5,
                                                                                                27
                                                                                            )
                                                                                        )
                                                                                    )
                                                                                )
                                                                            ),
                                                                            new IntegerRow(
                                                                                new LiteralInteger(
                                                                                    7
                                                                                )
                                                                            )
                                                                        )
                                                                    )
                                                                )
                                                            ),
                                                            new DecimalNullableRow(
                                                                new IntegerRow(
                                                                    new LiteralInteger(0)
                                                                )
                                                            )
                                                        )
                                                    )
                                                )
                                            ),
                                            new StringRow(new LiteralString("1 Monday")),
                                            new StringRow(
                                                new ConditionalStringRow(
                                                    new IfStringRow(
                                                        new BooleanRow(
                                                            new ComparisonRow(
                                                                new EqualRow(
                                                                    new EqualDecimalRow(
                                                                        new DecimalNullableRow(
                                                                            new IntegerRow(
                                                                                new ArithmeticIntegerRow(
                                                                                    new ModuloIntegerRow(
                                                                                        new IntegerRow(
                                                                                            new DateDiffDaysIntegerRow(
                                                                                                new DateRow(
                                                                                                    new FieldDate(
                                                                                                        new JoinedString(
                                                                                                            new DotString(),
                                                                                                            [
                                                                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                                                                new OrdersTable().Name,
                                                                                                            ]
                                                                                                        ).TextValue,
                                                                                                        new PlacedOnColumn()
                                                                                                            .Name
                                                                                                            .TextValue
                                                                                                    )
                                                                                                ),
                                                                                                new DateRow(
                                                                                                    new LiteralDate(
                                                                                                        new DateOnly(
                                                                                                            2024,
                                                                                                            5,
                                                                                                            27
                                                                                                        )
                                                                                                    )
                                                                                                )
                                                                                            )
                                                                                        ),
                                                                                        new IntegerRow(
                                                                                            new LiteralInteger(
                                                                                                7
                                                                                            )
                                                                                        )
                                                                                    )
                                                                                )
                                                                            )
                                                                        ),
                                                                        new DecimalNullableRow(
                                                                            new IntegerRow(
                                                                                new LiteralInteger(
                                                                                    1
                                                                                )
                                                                            )
                                                                        )
                                                                    )
                                                                )
                                                            )
                                                        ),
                                                        new StringRow(
                                                            new LiteralString("2 Tuesday")
                                                        ),
                                                        new StringRow(
                                                            new ConditionalStringRow(
                                                                new IfStringRow(
                                                                    new BooleanRow(
                                                                        new ComparisonRow(
                                                                            new EqualRow(
                                                                                new EqualDecimalRow(
                                                                                    new DecimalNullableRow(
                                                                                        new IntegerRow(
                                                                                            new ArithmeticIntegerRow(
                                                                                                new ModuloIntegerRow(
                                                                                                    new IntegerRow(
                                                                                                        new DateDiffDaysIntegerRow(
                                                                                                            new DateRow(
                                                                                                                new FieldDate(
                                                                                                                    new JoinedString(
                                                                                                                        new DotString(),
                                                                                                                        [
                                                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                                                            new OrdersTable().Name,
                                                                                                                        ]
                                                                                                                    ).TextValue,
                                                                                                                    new PlacedOnColumn()
                                                                                                                        .Name
                                                                                                                        .TextValue
                                                                                                                )
                                                                                                            ),
                                                                                                            new DateRow(
                                                                                                                new LiteralDate(
                                                                                                                    new DateOnly(
                                                                                                                        2024,
                                                                                                                        5,
                                                                                                                        27
                                                                                                                    )
                                                                                                                )
                                                                                                            )
                                                                                                        )
                                                                                                    ),
                                                                                                    new IntegerRow(
                                                                                                        new LiteralInteger(
                                                                                                            7
                                                                                                        )
                                                                                                    )
                                                                                                )
                                                                                            )
                                                                                        )
                                                                                    ),
                                                                                    new DecimalNullableRow(
                                                                                        new IntegerRow(
                                                                                            new LiteralInteger(
                                                                                                2
                                                                                            )
                                                                                        )
                                                                                    )
                                                                                )
                                                                            )
                                                                        )
                                                                    ),
                                                                    new StringRow(
                                                                        new LiteralString(
                                                                            "3 Wednesday"
                                                                        )
                                                                    ),
                                                                    new StringRow(
                                                                        new ConditionalStringRow(
                                                                            new IfStringRow(
                                                                                new BooleanRow(
                                                                                    new ComparisonRow(
                                                                                        new EqualRow(
                                                                                            new EqualDecimalRow(
                                                                                                new DecimalNullableRow(
                                                                                                    new IntegerRow(
                                                                                                        new ArithmeticIntegerRow(
                                                                                                            new ModuloIntegerRow(
                                                                                                                new IntegerRow(
                                                                                                                    new DateDiffDaysIntegerRow(
                                                                                                                        new DateRow(
                                                                                                                            new FieldDate(
                                                                                                                                new JoinedString(
                                                                                                                                    new DotString(),
                                                                                                                                    [
                                                                                                                                        new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                        new OrdersTable().Name,
                                                                                                                                    ]
                                                                                                                                ).TextValue,
                                                                                                                                new PlacedOnColumn()
                                                                                                                                    .Name
                                                                                                                                    .TextValue
                                                                                                                            )
                                                                                                                        ),
                                                                                                                        new DateRow(
                                                                                                                            new LiteralDate(
                                                                                                                                new DateOnly(
                                                                                                                                    2024,
                                                                                                                                    5,
                                                                                                                                    27
                                                                                                                                )
                                                                                                                            )
                                                                                                                        )
                                                                                                                    )
                                                                                                                ),
                                                                                                                new IntegerRow(
                                                                                                                    new LiteralInteger(
                                                                                                                        7
                                                                                                                    )
                                                                                                                )
                                                                                                            )
                                                                                                        )
                                                                                                    )
                                                                                                ),
                                                                                                new DecimalNullableRow(
                                                                                                    new IntegerRow(
                                                                                                        new LiteralInteger(
                                                                                                            3
                                                                                                        )
                                                                                                    )
                                                                                                )
                                                                                            )
                                                                                        )
                                                                                    )
                                                                                ),
                                                                                new StringRow(
                                                                                    new LiteralString(
                                                                                        "4 Thursday"
                                                                                    )
                                                                                ),
                                                                                new StringRow(
                                                                                    new ConditionalStringRow(
                                                                                        new IfStringRow(
                                                                                            new BooleanRow(
                                                                                                new ComparisonRow(
                                                                                                    new EqualRow(
                                                                                                        new EqualDecimalRow(
                                                                                                            new DecimalNullableRow(
                                                                                                                new IntegerRow(
                                                                                                                    new ArithmeticIntegerRow(
                                                                                                                        new ModuloIntegerRow(
                                                                                                                            new IntegerRow(
                                                                                                                                new DateDiffDaysIntegerRow(
                                                                                                                                    new DateRow(
                                                                                                                                        new FieldDate(
                                                                                                                                            new JoinedString(
                                                                                                                                                new DotString(),
                                                                                                                                                [
                                                                                                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                    new OrdersTable().Name,
                                                                                                                                                ]
                                                                                                                                            ).TextValue,
                                                                                                                                            new PlacedOnColumn()
                                                                                                                                                .Name
                                                                                                                                                .TextValue
                                                                                                                                        )
                                                                                                                                    ),
                                                                                                                                    new DateRow(
                                                                                                                                        new LiteralDate(
                                                                                                                                            new DateOnly(
                                                                                                                                                2024,
                                                                                                                                                5,
                                                                                                                                                27
                                                                                                                                            )
                                                                                                                                        )
                                                                                                                                    )
                                                                                                                                )
                                                                                                                            ),
                                                                                                                            new IntegerRow(
                                                                                                                                new LiteralInteger(
                                                                                                                                    7
                                                                                                                                )
                                                                                                                            )
                                                                                                                        )
                                                                                                                    )
                                                                                                                )
                                                                                                            ),
                                                                                                            new DecimalNullableRow(
                                                                                                                new IntegerRow(
                                                                                                                    new LiteralInteger(
                                                                                                                        4
                                                                                                                    )
                                                                                                                )
                                                                                                            )
                                                                                                        )
                                                                                                    )
                                                                                                )
                                                                                            ),
                                                                                            new StringRow(
                                                                                                new LiteralString(
                                                                                                    "5 Friday"
                                                                                                )
                                                                                            ),
                                                                                            new StringRow(
                                                                                                new ConditionalStringRow(
                                                                                                    new IfStringRow(
                                                                                                        new BooleanRow(
                                                                                                            new ComparisonRow(
                                                                                                                new EqualRow(
                                                                                                                    new EqualDecimalRow(
                                                                                                                        new DecimalNullableRow(
                                                                                                                            new IntegerRow(
                                                                                                                                new ArithmeticIntegerRow(
                                                                                                                                    new ModuloIntegerRow(
                                                                                                                                        new IntegerRow(
                                                                                                                                            new DateDiffDaysIntegerRow(
                                                                                                                                                new DateRow(
                                                                                                                                                    new FieldDate(
                                                                                                                                                        new JoinedString(
                                                                                                                                                            new DotString(),
                                                                                                                                                            [
                                                                                                                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                new OrdersTable().Name,
                                                                                                                                                            ]
                                                                                                                                                        ).TextValue,
                                                                                                                                                        new PlacedOnColumn()
                                                                                                                                                            .Name
                                                                                                                                                            .TextValue
                                                                                                                                                    )
                                                                                                                                                ),
                                                                                                                                                new DateRow(
                                                                                                                                                    new LiteralDate(
                                                                                                                                                        new DateOnly(
                                                                                                                                                            2024,
                                                                                                                                                            5,
                                                                                                                                                            27
                                                                                                                                                        )
                                                                                                                                                    )
                                                                                                                                                )
                                                                                                                                            )
                                                                                                                                        ),
                                                                                                                                        new IntegerRow(
                                                                                                                                            new LiteralInteger(
                                                                                                                                                7
                                                                                                                                            )
                                                                                                                                        )
                                                                                                                                    )
                                                                                                                                )
                                                                                                                            )
                                                                                                                        ),
                                                                                                                        new DecimalNullableRow(
                                                                                                                            new IntegerRow(
                                                                                                                                new LiteralInteger(
                                                                                                                                    5
                                                                                                                                )
                                                                                                                            )
                                                                                                                        )
                                                                                                                    )
                                                                                                                )
                                                                                                            )
                                                                                                        ),
                                                                                                        new StringRow(
                                                                                                            new LiteralString(
                                                                                                                "6 Saturday"
                                                                                                            )
                                                                                                        ),
                                                                                                        new StringRow(
                                                                                                            new LiteralString(
                                                                                                                "7 Sunday"
                                                                                                            )
                                                                                                        )
                                                                                                    )
                                                                                                )
                                                                                            )
                                                                                        )
                                                                                    )
                                                                                )
                                                                            )
                                                                        )
                                                                    )
                                                                )
                                                            )
                                                        )
                                                    )
                                                )
                                            )
                                        )
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
                                "weekday",
                                new StringGroup(new KeyString("weekday"))
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
                    new SelectItemGroup(
                        new SelectItemGroupNonNullable(
                            new SelectItemGroupDecimal(
                                "tenant_385_share_pct",
                                new ExactDecimalGroup(
                                    new ConditionalDecimalGroup(
                                        new IfDecimalGroup(
                                            new BooleanGroup(
                                                new ComparisonGroup(
                                                    new EqualGroup(
                                                        new EqualDecimalGroup(
                                                            new DecimalNullableGroup(
                                                                new IntegerGroup(
                                                                    new AggregateIntegerGroup(
                                                                        new CountGroup()
                                                                    )
                                                                )
                                                            ),
                                                            new DecimalNullableGroup(
                                                                new IntegerGroup(
                                                                    new LiteralInteger(0)
                                                                )
                                                            )
                                                        )
                                                    )
                                                )
                                            ),
                                            new DecimalGroup(
                                                new ExactDecimalGroup(
                                                    new LiteralDecimal(0m)
                                                )
                                            ),
                                            new DecimalGroup(
                                                new ExactDecimalGroup(
                                                    new RoundDecimalGroup(
                                                        new DecimalGroup(
                                                            new ExactDecimalGroup(
                                                                new ArithmeticDecimalGroup(
                                                                    new MultiplyDecimalGroup([
                                                                        new DecimalGroup(
                                                                            new ExactDecimalGroup(
                                                                                new LiteralDecimal(
                                                                                    100m
                                                                                )
                                                                            )
                                                                        ),
                                                                        new DecimalGroup(
                                                                            new ExactDecimalGroup(
                                                                                new ArithmeticDecimalGroup(
                                                                                    new DivideDecimalGroup([
                                                                                        new DecimalGroup(
                                                                                            new IntegerGroup(
                                                                                                new AggregateIntegerGroup(
                                                                                                    new CountGroup(
                                                                                                        new BooleanRow(
                                                                                                            new ComparisonRow(
                                                                                                                new EqualRow(
                                                                                                                    new EqualUuidRow(
                                                                                                                        new UuidNullableRow(
                                                                                                                            new UuidRow(
                                                                                                                                new FieldUuid(
                                                                                                                                    new JoinedString(
                                                                                                                                        new DotString(),
                                                                                                                                        [
                                                                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                            new OrdersTable().Name,
                                                                                                                                        ]
                                                                                                                                    ).TextValue,
                                                                                                                                    new OrderTenantIdColumn()
                                                                                                                                        .Name
                                                                                                                                        .TextValue
                                                                                                                                )
                                                                                                                            )
                                                                                                                        ),
                                                                                                                        new UuidNullableRow(
                                                                                                                            new UuidRow(
                                                                                                                                new LiteralUuid(
                                                                                                                                    new Guid(
                                                                                                                                        901,
                                                                                                                                        0,
                                                                                                                                        0,
                                                                                                                                        new byte[
                                                                                                                                            8
                                                                                                                                        ]
                                                                                                                                    )
                                                                                                                                )
                                                                                                                            )
                                                                                                                        )
                                                                                                                    )
                                                                                                                )
                                                                                                            )
                                                                                                        )
                                                                                                    )
                                                                                                )
                                                                                            )
                                                                                        ),
                                                                                        new DecimalGroup(
                                                                                            new IntegerGroup(
                                                                                                new AggregateIntegerGroup(
                                                                                                    new CountGroup()
                                                                                                )
                                                                                            )
                                                                                        ),
                                                                                    ])
                                                                                )
                                                                            )
                                                                        ),
                                                                    ])
                                                                )
                                                            )
                                                        ),
                                                        new IntegerGroup(
                                                            new LiteralInteger(1)
                                                        )
                                                    )
                                                )
                                            )
                                        )
                                    )
                                )
                            )
                        )
                    ),
                ],
                subqueries: null,
                joins: null,
                new BooleanRow(
                    new ComparisonRow(
                        new NotEqualRow(
                            new NotEqualStringRow(
                                new StringNullableRow(
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
                                ),
                                new StringNullableRow(
                                    new StringRow(new LiteralString("cancelled"))
                                )
                            )
                        )
                    )
                ),
                having: null,
                [
                    new OrderItemGroup(
                        new ExprGroup(
                            new StringNullableGroup(
                                new StringGroup(new KeyString("weekday"))
                            )
                        )
                    ),
                ],
                pagination: null,
                distinctBy: null
            )
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/>, in this order.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("weekday"), new StringColumnType()),
                    new Column(new String("orders"), new LongColumnType()),
                    new Column(
                        new String("tenant_385_share_pct"),
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
                                new Column(new String("weekday"), new StringColumnType()),
                                new InvariantCell(new String("1 Monday"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("tenant_385_share_pct"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(100))
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
                                new Column(new String("weekday"), new StringColumnType()),
                                new InvariantCell(new String("3 Wednesday"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("tenant_385_share_pct"),
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
                                new Column(new String("weekday"), new StringColumnType()),
                                new InvariantCell(new String("4 Thursday"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("tenant_385_share_pct"),
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
                                new Column(new String("weekday"), new StringColumnType()),
                                new InvariantCell(new String("6 Saturday"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("tenant_385_share_pct"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(100))
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
                                new Column(new String("weekday"), new StringColumnType()),
                                new InvariantCell(new String("7 Sunday"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("tenant_385_share_pct"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(100))
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
