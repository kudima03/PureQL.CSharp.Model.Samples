using Pure.Collections.Generic;
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
using PureQL.CSharp.Model.GroupExpressions;
using PureQL.CSharp.Model.Keys;
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.ProjectionExpressions;
using PureQL.CSharp.Model.RowExpressions;
using PureQL.CSharp.Model.Samples.Cells;
using Column = Pure.RelationalSchema.Column.Column;
using DateTime = Pure.Primitives.DateTime.DateTime;
using Double = Pure.Primitives.Number.Double;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.TimeZones;

/// <summary>
/// Selects from a subquery local_days, which groups schema_with_foreign_keys.users by the
/// start of the local day of last_login in Asia/Tokyo, and gives each day start the sum
/// of user_age and the count of users, the day start, its local date, the total, the
/// total over its local month (a window partitioned by the start of the local month), the
/// running total in its local week (a window partitioned by the start of the local week,
/// ordered by the day start, from the first row to the current one) and the previous day
/// start, ordered by the day_start column.
/// </summary>
public sealed record LocalDayBucketsQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithoutPagination(
            subqueries:
            [
                new SubqueryQuery(
                    "local_days",
                    new GroupedQueryWithoutPaginationWithoutOrderBy(
                        from: new FromEntity(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue
                        ),
                        groupBy:
                        [
                            new GroupKey(
                                "day_start",
                                new StartOfDayDatetimeRow(
                                    new FieldDatetime(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new UsersTable().Name,
                                            ]
                                        ).TextValue,
                                        new LastLoginColumn().Name.TextValue
                                    ),
                                    zone: new LiteralString("Asia/Tokyo")
                                )
                            ),
                        ],
                        select:
                        [
                            new SelectItemGroup(
                                "day_start",
                                new KeyDatetime("day_start")
                            ),
                            new SelectItemGroup(
                                "total_age",
                                new SumDecimalGroup(
                                    new FieldDecimal(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new UsersTable().Name,
                                            ]
                                        ).TextValue,
                                        new UserAgeColumn().Name.TextValue
                                    )
                                )
                            ),
                            new SelectItemGroup("users", new CountGroup()),
                        ]
                    )
                ),
            ],
            from: new FromSubquery("local_days"),
            select:
            [
                new SelectItemProjection(
                    "day_start",
                    new FieldDatetime("local_days", "day_start")
                ),
                new SelectItemProjection(
                    "local_day",
                    new AsDateDatetimeDateProjection(
                        new FieldDatetime("local_days", "day_start"),
                        zone: new LiteralString("Asia/Tokyo")
                    )
                ),
                new SelectItemProjection(
                    "total_age",
                    new FieldDecimal("local_days", "total_age")
                ),
                new SelectItemProjection(
                    "total_age_of_the_local_month",
                    new SumDecimalProjection(
                        new FieldDecimal("local_days", "total_age"),
                        over: new WindowWithoutFrameWithoutRange([
                            new StartOfMonthDatetimeRow(
                                new FieldDatetime("local_days", "day_start"),
                                zone: new LiteralString("Asia/Tokyo")
                            ),
                        ])
                    )
                ),
                new SelectItemProjection(
                    "running_in_the_local_week",
                    new SumDecimalProjection(
                        new FieldDecimal("local_days", "total_age"),
                        over: new WindowWithFrame(
                            [
                                new OrderItemRow(
                                    new FieldDatetime("local_days", "day_start")
                                ),
                            ],
                            new Frame(preceding: new All()),
                            partitionBy:
                            [
                                new StartOfWeekDatetimeRow(
                                    new FieldDatetime("local_days", "day_start"),
                                    zone: new LiteralString("Asia/Tokyo")
                                ),
                            ]
                        )
                    )
                ),
                new SelectItemProjection(
                    "previous_day_start",
                    new PreviousDatetimeNullableProjection(
                        new FieldDatetime("local_days", "day_start"),
                        over: new OrderedWindow([
                            new OrderItemRow(
                                new FieldDatetime("local_days", "day_start")
                            ),
                        ])
                    )
                ),
            ],
            orderBy: [new OrderItemProjectionColumn("day_start")]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and
    /// <see cref="AuditSchemaDataSet"/>, in this order.
    /// Local times are read with the tz database of PostgreSQL 17 and checked with
    /// Python zoneinfo; no value falls in a skipped or repeated local hour.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("day_start"), new DateTimeColumnType()),
                    new Column(new String("local_day"), new DateColumnType()),
                    new Column(new String("total_age"), new DoubleColumnType()),
                    new Column(
                        new String("total_age_of_the_local_month"),
                        new DoubleColumnType()
                    ),
                    new Column(
                        new String("running_in_the_local_week"),
                        new DoubleColumnType()
                    ),
                    new Column(
                        new String("previous_day_start"),
                        new DateTimeColumnType()
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
                                    new String("day_start"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(29),
                                            new UShort(5),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(15),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("local_day"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(30),
                                        new UShort(5),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("total_age"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(30.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("total_age_of_the_local_month"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(30.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("running_in_the_local_week"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(30.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("previous_day_start"),
                                    new DateTimeColumnType()
                                ),
                                new EmptyCell()
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
                                new Column(
                                    new String("day_start"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(31),
                                            new UShort(5),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(15),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("local_day"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(1),
                                        new UShort(6),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("total_age"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(58.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("total_age_of_the_local_month"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(150.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("running_in_the_local_week"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(88.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("previous_day_start"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(29),
                                            new UShort(5),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(15),
                                            new UShort(0),
                                            new UShort(0)
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
                                new Column(
                                    new String("day_start"),
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
                                            new UShort(15),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("local_day"), new DateColumnType()),
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
                                    new String("total_age"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(25.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("total_age_of_the_local_month"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(150.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("running_in_the_local_week"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(113.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("previous_day_start"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(31),
                                            new UShort(5),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(15),
                                            new UShort(0),
                                            new UShort(0)
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
                                new Column(
                                    new String("day_start"),
                                    new DateTimeColumnType()
                                ),
                                new UtcDateTimeCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(3),
                                            new UShort(6),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(15),
                                            new UShort(0),
                                            new UShort(0)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("local_day"), new DateColumnType()),
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
                                    new String("total_age"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(67.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("total_age_of_the_local_month"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(150.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("running_in_the_local_week"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(67.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("previous_day_start"),
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
                                            new UShort(15),
                                            new UShort(0),
                                            new UShort(0)
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
