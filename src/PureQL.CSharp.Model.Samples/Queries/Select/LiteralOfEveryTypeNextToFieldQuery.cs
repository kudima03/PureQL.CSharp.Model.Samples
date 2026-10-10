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
using PureQL.CSharp.Model.ProjectionExpressions;
using PureQL.CSharp.Model.SelectItems;
using Column = Pure.RelationalSchema.Column.Column;
using DateTime = Pure.Primitives.DateTime.DateTime;
using Double = Pure.Primitives.Number.Double;
using Guid = Pure.Primitives.Guid.Guid;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Select;

/// <summary>
/// Selects order_id next to one literal column of every type (integer 42, decimal 19.99,
/// string hello, boolean true, date 2024-01-31, time 18:30:00, datetime
/// 2024-01-31T18:30:00Z and uuid #999) from schema_with_foreign_keys.orders.
/// </summary>
public sealed record LiteralOfEveryTypeNextToFieldQuery
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
                            new SelectItemProjectionInteger(
                                "an_integer",
                                new IntegerProjection(new LiteralInteger(42))
                            )
                        )
                    ),
                    new SelectItemProjection(
                        new SelectItemProjectionNonNullable(
                            new SelectItemProjectionDecimal(
                                "a_decimal",
                                new DecimalProjection(
                                    new LiteralAsDecimal(new LiteralDecimal(19.99m))
                                )
                            )
                        )
                    ),
                    new SelectItemProjection(
                        new SelectItemProjectionNonNullable(
                            new SelectItemProjectionString(
                                "a_string",
                                new StringProjection(new LiteralString("hello"))
                            )
                        )
                    ),
                    new SelectItemProjection(
                        new SelectItemProjectionNonNullable(
                            new SelectItemProjectionBoolean(
                                "a_boolean",
                                new BooleanProjection(new LiteralBoolean(true))
                            )
                        )
                    ),
                    new SelectItemProjection(
                        new SelectItemProjectionNonNullable(
                            new SelectItemProjectionDate(
                                "a_date",
                                new DateProjection(
                                    new LiteralDate(new DateOnly(2024, 1, 31))
                                )
                            )
                        )
                    ),
                    new SelectItemProjection(
                        new SelectItemProjectionNonNullable(
                            new SelectItemProjectionTime(
                                "a_time",
                                new TimeProjection(
                                    new LiteralTime(new TimeOnly(18, 30, 0))
                                )
                            )
                        )
                    ),
                    new SelectItemProjection(
                        new SelectItemProjectionNonNullable(
                            new SelectItemProjectionDatetime(
                                "a_datetime",
                                new DatetimeProjection(
                                    new LiteralDatetime(
                                        new DateTimeOffset(
                                            2024,
                                            1,
                                            31,
                                            18,
                                            30,
                                            0,
                                            TimeSpan.Zero
                                        )
                                    )
                                )
                            )
                        )
                    ),
                    new SelectItemProjection(
                        new SelectItemProjectionNonNullable(
                            new SelectItemProjectionUuid(
                                "a_uuid",
                                new UuidProjection(
                                    new LiteralUuid(
                                        new System.Guid(999, 0, 0, new byte[8])
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
                    new Column(new String("an_integer"), new LongColumnType()),
                    new Column(new String("a_decimal"), new DoubleColumnType()),
                    new Column(new String("a_string"), new StringColumnType()),
                    new Column(new String("a_boolean"), new BoolColumnType()),
                    new Column(new String("a_date"), new DateColumnType()),
                    new Column(new String("a_time"), new TimeColumnType()),
                    new Column(new String("a_datetime"), new DateTimeColumnType()),
                    new Column(new String("a_uuid"), new UuidColumnType()),
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
                                    new String("an_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(42))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("a_decimal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(19.99))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("a_string"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("hello"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("a_boolean"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("a_date"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(31),
                                        new UShort(1),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("a_time"), new TimeColumnType()),
                                new InvariantCell(
                                    new Time(
                                        new UShort(18),
                                        new UShort(30),
                                        new UShort(0)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("a_datetime"),
                                    new DateTimeColumnType()
                                ),
                                new InvariantCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(31),
                                            new UShort(1),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(18),
                                            new UShort(30),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("a_uuid"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "000003e7-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
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
                                    new String("an_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(42))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("a_decimal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(19.99))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("a_string"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("hello"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("a_boolean"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("a_date"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(31),
                                        new UShort(1),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("a_time"), new TimeColumnType()),
                                new InvariantCell(
                                    new Time(
                                        new UShort(18),
                                        new UShort(30),
                                        new UShort(0)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("a_datetime"),
                                    new DateTimeColumnType()
                                ),
                                new InvariantCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(31),
                                            new UShort(1),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(18),
                                            new UShort(30),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("a_uuid"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "000003e7-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
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
                                    new String("an_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(42))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("a_decimal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(19.99))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("a_string"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("hello"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("a_boolean"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("a_date"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(31),
                                        new UShort(1),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("a_time"), new TimeColumnType()),
                                new InvariantCell(
                                    new Time(
                                        new UShort(18),
                                        new UShort(30),
                                        new UShort(0)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("a_datetime"),
                                    new DateTimeColumnType()
                                ),
                                new InvariantCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(31),
                                            new UShort(1),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(18),
                                            new UShort(30),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("a_uuid"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "000003e7-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
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
                                    new String("an_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(42))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("a_decimal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(19.99))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("a_string"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("hello"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("a_boolean"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("a_date"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(31),
                                        new UShort(1),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("a_time"), new TimeColumnType()),
                                new InvariantCell(
                                    new Time(
                                        new UShort(18),
                                        new UShort(30),
                                        new UShort(0)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("a_datetime"),
                                    new DateTimeColumnType()
                                ),
                                new InvariantCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(31),
                                            new UShort(1),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(18),
                                            new UShort(30),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("a_uuid"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "000003e7-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
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
                                    new String("an_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(42))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("a_decimal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(19.99))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("a_string"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("hello"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("a_boolean"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("a_date"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(31),
                                        new UShort(1),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("a_time"), new TimeColumnType()),
                                new InvariantCell(
                                    new Time(
                                        new UShort(18),
                                        new UShort(30),
                                        new UShort(0)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("a_datetime"),
                                    new DateTimeColumnType()
                                ),
                                new InvariantCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(31),
                                            new UShort(1),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(18),
                                            new UShort(30),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("a_uuid"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "000003e7-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
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
                                    new String("an_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(42))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("a_decimal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(19.99))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("a_string"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("hello"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("a_boolean"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("a_date"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(31),
                                        new UShort(1),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("a_time"), new TimeColumnType()),
                                new InvariantCell(
                                    new Time(
                                        new UShort(18),
                                        new UShort(30),
                                        new UShort(0)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("a_datetime"),
                                    new DateTimeColumnType()
                                ),
                                new InvariantCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(31),
                                            new UShort(1),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(18),
                                            new UShort(30),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("a_uuid"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "000003e7-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
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
