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
using PureQL.CSharp.Model.EachArithmetics;
using PureQL.CSharp.Model.EachBooleanOperations;
using PureQL.CSharp.Model.EachComparisons;
using PureQL.CSharp.Model.Fields;
using PureQL.CSharp.Model.Returnings;
using PureQL.CSharp.Model.Scalars;
using Guid = Pure.Primitives.Guid.Guid;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Where.Each;

/// <summary>
/// Selects user_id from schema_with_foreign_keys.users, keeping the rows where (user_age
/// plus 5) is greater than 30 and (user_age times 2) is less than 100.
/// </summary>
public sealed record EachAndOfShiftedAgeAboveThresholdAndDoubledAgeBelowThresholdQuery
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
                new EachAndOperator([
                    new BooleanArrayReturning(
                        new EachComparison(
                            new EachNumberComparison(
                                EachComparisonOperator.EachGreaterThan,
                                new NumberArrayReturning(
                                    new EachArithmetic(
                                        new EachAdd([
                                            new NumberArrayReturning(
                                                new NumberField(
                                                    new JoinedString(
                                                        new DotString(),
                                                        [
                                                            new RelationalSchemaWithForeignKeys().Name,
                                                            new UsersTable().Name,
                                                        ]
                                                    ).TextValue,
                                                    new UserAgeColumn().Name.TextValue
                                                )
                                            ),
                                            new NumberReturning(new NumberScalar(5)),
                                        ])
                                    )
                                ),
                                new NumberReturning(new NumberScalar(30))
                            )
                        )
                    ),
                    new BooleanArrayReturning(
                        new EachComparison(
                            new EachNumberComparison(
                                EachComparisonOperator.EachLessThan,
                                new NumberArrayReturning(
                                    new EachArithmetic(
                                        new EachMultiply([
                                            new NumberArrayReturning(
                                                new NumberField(
                                                    new JoinedString(
                                                        new DotString(),
                                                        [
                                                            new RelationalSchemaWithForeignKeys().Name,
                                                            new UsersTable().Name,
                                                        ]
                                                    ).TextValue,
                                                    new UserAgeColumn().Name.TextValue
                                                )
                                            ),
                                            new NumberReturning(new NumberScalar(2)),
                                        ])
                                    )
                                ),
                                new NumberReturning(new NumberScalar(100))
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
                                            "00000003-0000-0000-0000-000000000000"
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
