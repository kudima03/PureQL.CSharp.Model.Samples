using Pure.Collections.Generic;
using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Abstractions.Column;
using Pure.RelationalSchema.HashCodes;
using Pure.RelationalSchema.Samples.Columns;
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
using Pure.RelationalSchema.Storage;
using Pure.RelationalSchema.Storage.Abstractions;
using Pure.RelationalSchema.Storage.Samples.Cells;
using Pure.RelationalSchema.Storage.Samples.SchemaDataSets;
using Pure.RelationalSchema.Storage.Samples.TableDataSets;
using PureQL.CSharp.Model.ArrayReturnings;
using PureQL.CSharp.Model.EachBooleanOperations;
using PureQL.CSharp.Model.EachComparisons;
using PureQL.CSharp.Model.EachDateTimeArithmetics;
using PureQL.CSharp.Model.EachEqualities;
using PureQL.CSharp.Model.Fields;
using PureQL.CSharp.Model.Returnings;
using PureQL.CSharp.Model.Scalars;
using Guid = Pure.Primitives.Guid.Guid;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Where.Each;

/// <summary>
/// Selects user_id from schema_with_foreign_keys.users, keeping the rows where last_login
/// plus 3600 seconds equals '2024-06-01T09:30:00' or the seconds between last_login and
/// '2024-06-02T00:00:00' is greater than 0.
/// </summary>
public sealed record EachOrOfDateTimeAddSecondsEqualsTargetAndDateTimeDiffSecondsAboveZeroQuery
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
                    new ArrayReturning(
                        new UuidArrayReturning(
                            new UuidField(
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
                ),
            ],
            new BooleanArrayReturning(
                new EachOrOperator([
                    new BooleanArrayReturning(
                        new EachEquality(
                            new EachDateTimeEquality(
                                new DateTimeArrayReturning(
                                    new EachDateTimeAddSeconds(
                                        new DateTimeArrayReturning(
                                            new DateTimeField(
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
                                        new NumberReturning(new NumberScalar(3600))
                                    )
                                ),
                                new DateTimeReturning(
                                    new DateTimeScalar(new DateTime(2024, 6, 1, 9, 30, 0))
                                )
                            )
                        )
                    ),
                    new BooleanArrayReturning(
                        new EachComparison(
                            new EachNumberComparison(
                                EachComparisonOperator.EachGreaterThan,
                                new NumberArrayReturning(
                                    new EachDateTimeDiffSeconds(
                                        new DateTimeArrayReturning(
                                            new DateTimeField(
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
                                        new DateTimeReturning(
                                            new DateTimeScalar(
                                                new DateTime(2024, 6, 2, 0, 0, 0)
                                            )
                                        )
                                    )
                                ),
                                new NumberReturning(new NumberScalar(0))
                            )
                        )
                    ),
                ])
            ),
            join: null,
            groupBy: null,
            having: null,
            orderBy: null,
            pagination: null
        );

    /// <summary>
    /// The rows the query returns under SQL semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and <see cref="AuditSchemaDataSet"/>,
    /// in no particular order.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(new EmptyString(), [new UserIdColumn()], []),
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
                                            "00000002-0000-0000-0000-000000000000"
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
                                new UserIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000004-0000-0000-0000-000000000000"
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
                                new UserIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000005-0000-0000-0000-000000000000"
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
                                new UserIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000006-0000-0000-0000-000000000000"
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
