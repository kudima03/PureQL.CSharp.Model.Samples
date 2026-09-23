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
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
using Pure.RelationalSchema.Storage;
using Pure.RelationalSchema.Storage.Abstractions;
using Pure.RelationalSchema.Storage.Samples.Cells;
using Pure.RelationalSchema.Storage.Samples.SchemaDataSets;
using Pure.RelationalSchema.Storage.Samples.TableDataSets;
using PureQL.CSharp.Model.Returnings;
using PureQL.CSharp.Model.Scalars;
using Column = Pure.RelationalSchema.Column.Column;
using DateTime = Pure.Primitives.DateTime.DateTime;
using Double = Pure.Primitives.Number.Double;
using Guid = Pure.Primitives.Guid.Guid;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Select;

/// <summary>
/// Selects true as active, '2024-12-31' as release, '2024-12-31T23:59:58' as built_at,
/// 42.5 as amount, 'v2' as label, '17:30:15' as cutoff and
/// '0f8fad5b-d9cb-469f-a165-70867728950e' as marker from schema_with_foreign_keys.users.
/// </summary>
public sealed record AllSevenScalarTypesQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public Query Value =>
        new Query(
            new FromExpression(
                new JoinedString(
                    new DotString(),
                    [new RelationalSchemaWithForeignKeys().Name, new UsersTable().Name]
                ).TextValue
            ),
            [
                new SelectExpression(
                    new SingleValueReturning(
                        new BooleanReturning(new BooleanScalar(true))
                    ),
                    "active"
                ),
                new SelectExpression(
                    new SingleValueReturning(
                        new DateReturning(new DateScalar(new DateOnly(2024, 12, 31)))
                    ),
                    "release"
                ),
                new SelectExpression(
                    new SingleValueReturning(
                        new DateTimeReturning(
                            new DateTimeScalar(
                                new System.DateTime(2024, 12, 31, 23, 59, 58)
                            )
                        )
                    ),
                    "built_at"
                ),
                new SelectExpression(
                    new SingleValueReturning(new NumberReturning(new NumberScalar(42.5))),
                    "amount"
                ),
                new SelectExpression(
                    new SingleValueReturning(new StringReturning(new StringScalar("v2"))),
                    "label"
                ),
                new SelectExpression(
                    new SingleValueReturning(
                        new TimeReturning(new TimeScalar(new TimeOnly(17, 30, 15)))
                    ),
                    "cutoff"
                ),
                new SelectExpression(
                    new SingleValueReturning(
                        new UuidReturning(
                            new UuidScalar(
                                new System.Guid("0f8fad5b-d9cb-469f-a165-70867728950e")
                            )
                        )
                    ),
                    "marker"
                ),
            ]
        );

    /// <summary>
    /// The rows the query returns under SQL semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and <see cref="AuditSchemaDataSet"/>,
    /// in no particular order.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("active"), new BoolColumnType()),
                    new Column(new String("release"), new DateColumnType()),
                    new Column(new String("built_at"), new DateTimeColumnType()),
                    new Column(new String("amount"), new DoubleColumnType()),
                    new Column(new String("label"), new StringColumnType()),
                    new Column(new String("cutoff"), new TimeColumnType()),
                    new Column(new String("marker"), new UuidColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("active"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("release"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(31),
                                        new UShort(12),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("built_at"),
                                    new DateTimeColumnType()
                                ),
                                new InvariantCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(31),
                                            new UShort(12),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(23),
                                            new UShort(59),
                                            new UShort(58)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("amount"), new DoubleColumnType()),
                                new InvariantCell(new Double(42.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("label"), new StringColumnType()),
                                new InvariantCell(new String("v2"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("cutoff"), new TimeColumnType()),
                                new InvariantCell(
                                    new Time(
                                        new UShort(17),
                                        new UShort(30),
                                        new UShort(15)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("marker"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "0f8fad5b-d9cb-469f-a165-70867728950e"
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
                                new Column(new String("active"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("release"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(31),
                                        new UShort(12),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("built_at"),
                                    new DateTimeColumnType()
                                ),
                                new InvariantCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(31),
                                            new UShort(12),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(23),
                                            new UShort(59),
                                            new UShort(58)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("amount"), new DoubleColumnType()),
                                new InvariantCell(new Double(42.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("label"), new StringColumnType()),
                                new InvariantCell(new String("v2"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("cutoff"), new TimeColumnType()),
                                new InvariantCell(
                                    new Time(
                                        new UShort(17),
                                        new UShort(30),
                                        new UShort(15)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("marker"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "0f8fad5b-d9cb-469f-a165-70867728950e"
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
                                new Column(new String("active"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("release"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(31),
                                        new UShort(12),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("built_at"),
                                    new DateTimeColumnType()
                                ),
                                new InvariantCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(31),
                                            new UShort(12),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(23),
                                            new UShort(59),
                                            new UShort(58)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("amount"), new DoubleColumnType()),
                                new InvariantCell(new Double(42.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("label"), new StringColumnType()),
                                new InvariantCell(new String("v2"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("cutoff"), new TimeColumnType()),
                                new InvariantCell(
                                    new Time(
                                        new UShort(17),
                                        new UShort(30),
                                        new UShort(15)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("marker"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "0f8fad5b-d9cb-469f-a165-70867728950e"
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
                                new Column(new String("active"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("release"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(31),
                                        new UShort(12),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("built_at"),
                                    new DateTimeColumnType()
                                ),
                                new InvariantCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(31),
                                            new UShort(12),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(23),
                                            new UShort(59),
                                            new UShort(58)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("amount"), new DoubleColumnType()),
                                new InvariantCell(new Double(42.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("label"), new StringColumnType()),
                                new InvariantCell(new String("v2"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("cutoff"), new TimeColumnType()),
                                new InvariantCell(
                                    new Time(
                                        new UShort(17),
                                        new UShort(30),
                                        new UShort(15)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("marker"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "0f8fad5b-d9cb-469f-a165-70867728950e"
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
                                new Column(new String("active"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("release"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(31),
                                        new UShort(12),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("built_at"),
                                    new DateTimeColumnType()
                                ),
                                new InvariantCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(31),
                                            new UShort(12),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(23),
                                            new UShort(59),
                                            new UShort(58)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("amount"), new DoubleColumnType()),
                                new InvariantCell(new Double(42.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("label"), new StringColumnType()),
                                new InvariantCell(new String("v2"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("cutoff"), new TimeColumnType()),
                                new InvariantCell(
                                    new Time(
                                        new UShort(17),
                                        new UShort(30),
                                        new UShort(15)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("marker"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "0f8fad5b-d9cb-469f-a165-70867728950e"
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
                                new Column(new String("active"), new BoolColumnType()),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("release"), new DateColumnType()),
                                new InvariantCell(
                                    new Date(
                                        new UShort(31),
                                        new UShort(12),
                                        new UShort(2024)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("built_at"),
                                    new DateTimeColumnType()
                                ),
                                new InvariantCell(
                                    new DateTime(
                                        new Date(
                                            new UShort(31),
                                            new UShort(12),
                                            new UShort(2024)
                                        ),
                                        new Time(
                                            new UShort(23),
                                            new UShort(59),
                                            new UShort(58)
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("amount"), new DoubleColumnType()),
                                new InvariantCell(new Double(42.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("label"), new StringColumnType()),
                                new InvariantCell(new String("v2"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("cutoff"), new TimeColumnType()),
                                new InvariantCell(
                                    new Time(
                                        new UShort(17),
                                        new UShort(30),
                                        new UShort(15)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("marker"), new UuidColumnType()),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "0f8fad5b-d9cb-469f-a165-70867728950e"
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
