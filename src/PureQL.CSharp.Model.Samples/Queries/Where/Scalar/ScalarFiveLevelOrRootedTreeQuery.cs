using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Samples.Columns;
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
using Pure.RelationalSchema.Storage.Abstractions;
using Pure.RelationalSchema.Storage.Samples.SchemaDataSets;
using Pure.RelationalSchema.Storage.Samples.TableDataSets;
using PureQL.CSharp.Model.ArrayReturnings;
using PureQL.CSharp.Model.BooleanOperations;
using PureQL.CSharp.Model.Fields;
using PureQL.CSharp.Model.Returnings;
using PureQL.CSharp.Model.Scalars;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Where.Scalar;

/// <summary>
/// Selects order_status from schema_with_foreign_keys.orders, filtered by (not (false or
/// false) and false) or (not (true) and (false or false)).
/// </summary>
public sealed record ScalarFiveLevelOrRootedTreeQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public Query Value =>
        new Query(
            new FromExpression(
                new JoinedString(
                    new DotString(),
                    [new RelationalSchemaWithForeignKeys().Name, new OrdersTable().Name]
                ).TextValue
            ),
            [
                new SelectExpression(
                    new ArrayReturning(
                        new StringArrayReturning(
                            new StringField(
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
                    )
                ),
            ],
            new BooleanReturning(
                new BooleanOperator(
                    new OrOperator([
                        new BooleanReturning(
                            new BooleanOperator(
                                new AndOperator([
                                    new BooleanReturning(
                                        new BooleanOperator(
                                            new NotOperator(
                                                new BooleanReturning(
                                                    new BooleanOperator(
                                                        new OrOperator([
                                                            new BooleanReturning(
                                                                new BooleanScalar(false)
                                                            ),
                                                            new BooleanReturning(
                                                                new BooleanScalar(false)
                                                            ),
                                                        ])
                                                    )
                                                )
                                            )
                                        )
                                    ),
                                    new BooleanReturning(new BooleanScalar(false)),
                                ])
                            )
                        ),
                        new BooleanReturning(
                            new BooleanOperator(
                                new AndOperator([
                                    new BooleanReturning(
                                        new BooleanOperator(
                                            new NotOperator(
                                                new BooleanReturning(
                                                    new BooleanScalar(true)
                                                )
                                            )
                                        )
                                    ),
                                    new BooleanReturning(
                                        new BooleanOperator(
                                            new OrOperator([
                                                new BooleanReturning(
                                                    new BooleanScalar(false)
                                                ),
                                                new BooleanReturning(
                                                    new BooleanScalar(false)
                                                ),
                                            ])
                                        )
                                    ),
                                ])
                            )
                        ),
                    ])
                )
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
            new Table(new EmptyString(), [new OrderStatusColumn()], []),
            []
        );
}
