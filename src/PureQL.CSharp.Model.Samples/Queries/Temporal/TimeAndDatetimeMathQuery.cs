using Pure.Collections.Generic;
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
using PureQL.CSharp.Model.RowExpressions;
using PureQL.CSharp.Model.SelectItems;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using Guid = Pure.Primitives.Guid.Guid;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Temporal;

/// <summary>
/// Selects user_id, shift_start plus 16 times 3600 seconds as shift_end and the seconds
/// from 2024-06-01T00:00:00Z to last_login as seconds_since_june from
/// schema_with_foreign_keys.users, where last_login is at or after 2024-06-01T00:00:00Z
/// and shift_start is before 10:00:00.
/// </summary>
public sealed record TimeAndDatetimeMathQuery
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
                                new UsersTable().Name,
                            ]
                        ).TextValue
                    )
                ),
                [
                    new SelectItemProjection(
                        new SelectItemProjectionNonNullable(
                            new SelectItemProjectionUuid(
                                "user_id",
                                new UuidProjection(
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
                            )
                        )
                    ),
                    new SelectItemProjection(
                        new SelectItemProjectionNonNullable(
                            new SelectItemProjectionTime(
                                "shift_end",
                                new TimeProjection(
                                    new TimeAddSecondsTimeProjection(
                                        new TimeProjection(
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
                                        ),
                                        new DecimalProjection(
                                            new ArithmeticDecimalProjection(
                                                new MultiplyDecimalProjection([
                                                    new DecimalProjection(
                                                        new LiteralAsDecimal(
                                                            new LiteralInteger(16)
                                                        )
                                                    ),
                                                    new DecimalProjection(
                                                        new LiteralAsDecimal(
                                                            new LiteralInteger(3600)
                                                        )
                                                    ),
                                                ])
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
                                "seconds_since_june",
                                new DecimalProjection(
                                    new DifferenceDecimalProjection(
                                        new DatetimeDiffSecondsDecimalProjection(
                                            new DatetimeProjection(
                                                new FieldDatetime(
                                                    new JoinedString(
                                                        new DotString(),
                                                        [
                                                            new RelationalSchemaWithForeignKeys().Name,
                                                            new UsersTable().Name,
                                                        ]
                                                    ).TextValue,
                                                    new LastLoginColumn().Name.TextValue
                                                )
                                            ),
                                            new DatetimeProjection(
                                                new LiteralDatetime(
                                                    new DateTimeOffset(
                                                        2024,
                                                        6,
                                                        1,
                                                        0,
                                                        0,
                                                        0,
                                                        TimeSpan.Zero
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
                    new LogicalRow(
                        new AndRow([
                            new BooleanRow(
                                new ComparisonRow(
                                    new GreaterThanOrEqualRow(
                                        new GreaterThanOrEqualDatetimeRow(
                                            new DatetimeNullableRow(
                                                new FieldAsDatetimeNullable(
                                                    new FieldDatetime(
                                                        new JoinedString(
                                                            new DotString(),
                                                            [
                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                new UsersTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new LastLoginColumn()
                                                            .Name
                                                            .TextValue
                                                    )
                                                )
                                            ),
                                            new DatetimeNullableRow(
                                                new LiteralAsDatetimeNullable(
                                                    new LiteralDatetime(
                                                        new DateTimeOffset(
                                                            2024,
                                                            6,
                                                            1,
                                                            0,
                                                            0,
                                                            0,
                                                            TimeSpan.Zero
                                                        )
                                                    )
                                                )
                                            )
                                        )
                                    )
                                )
                            ),
                            new BooleanRow(
                                new ComparisonRow(
                                    new LessThanRow(
                                        new LessThanTimeRow(
                                            new TimeNullableRow(
                                                new FieldAsTimeNullable(
                                                    new FieldTime(
                                                        new JoinedString(
                                                            new DotString(),
                                                            [
                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                new UsersTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new ShiftStartColumn()
                                                            .Name
                                                            .TextValue
                                                    )
                                                )
                                            ),
                                            new TimeNullableRow(
                                                new LiteralAsTimeNullable(
                                                    new LiteralTime(
                                                        new TimeOnly(10, 0, 0)
                                                    )
                                                )
                                            )
                                        )
                                    )
                                )
                            ),
                        ])
                    )
                ),
                orderBy: null,
                pagination: null,
                distinct: false
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
                    new UserIdColumn(),
                    new Column(new String("shift_end"), new TimeColumnType()),
                    new Column(new String("seconds_since_june"), new DoubleColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new UserIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000001-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("shift_end"), new TimeColumnType()),
                                new InvariantCell(
                                    new Time(new UShort(1), new UShort(0), new UShort(0))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("seconds_since_june"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(30600))
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
                                new UserIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000005-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("shift_end"), new TimeColumnType()),
                                new InvariantCell(
                                    new Time(new UShort(0), new UShort(0), new UShort(0))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("seconds_since_june"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(284700))
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
                                new UserIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000006-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("shift_end"), new TimeColumnType()),
                                new InvariantCell(
                                    new Time(new UShort(1), new UShort(0), new UShort(0))
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("seconds_since_june"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(30600))
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
