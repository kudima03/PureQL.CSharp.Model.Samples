using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Samples.Columns;
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
using PureQL.CSharp.Model.Aggregates;
using PureQL.CSharp.Model.Aggregates.Numeric;
using PureQL.CSharp.Model.ArrayReturnings;
using PureQL.CSharp.Model.BooleanOperations;
using PureQL.CSharp.Model.Comparisons;
using PureQL.CSharp.Model.EachBooleanOperations;
using PureQL.CSharp.Model.EachComparisons;
using PureQL.CSharp.Model.EachEqualities;
using PureQL.CSharp.Model.Fields;
using PureQL.CSharp.Model.Returnings;
using PureQL.CSharp.Model.Scalars;

namespace PureQL.CSharp.Model.Samples.Queries.Combined;

public sealed record FiveLevelEachTreeWithHavingQuery
{
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
                        new UuidArrayReturning(
                            new UuidField(
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
                ),
                new SelectExpression(
                    new SingleValueReturning(
                        new NumberReturning(
                            new NumberAggregate(
                                new SumNumber(
                                    new NumberArrayReturning(
                                        new NumberField(
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new RelationalSchemaWithForeignKeys().Name,
                                                    new OrderItemsTable().Name,
                                                ]
                                            ).TextValue,
                                            new ItemQtyColumn().Name.TextValue
                                        )
                                    )
                                )
                            )
                        )
                    ),
                    "qtySum"
                ),
                new SelectExpression(
                    new SingleValueReturning(
                        new NumberReturning(
                            new Count(
                                new ArrayReturning(
                                    new UuidArrayReturning(
                                        new UuidField(
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new RelationalSchemaWithForeignKeys().Name,
                                                    new OrderItemsTable().Name,
                                                ]
                                            ).TextValue,
                                            new ItemIdColumn().Name.TextValue
                                        )
                                    )
                                )
                            )
                        )
                    ),
                    "itemCount"
                ),
            ],
            new BooleanArrayReturning(
                new EachAndOperator([
                    new BooleanArrayReturning(
                        new EachOrOperator([
                            new BooleanArrayReturning(
                                new EachNotOperator(
                                    new BooleanArrayReturning(
                                        new EachAndOperator([
                                            new BooleanArrayReturning(
                                                new EachComparison(
                                                    new EachNumberComparison(
                                                        EachComparisonOperator.EachGreaterThan,
                                                        new NumberArrayReturning(
                                                            new NumberField(
                                                                new JoinedString(
                                                                    new DotString(),
                                                                    [
                                                                        new RelationalSchemaWithForeignKeys().Name,
                                                                        new OrdersTable().Name,
                                                                    ]
                                                                ).TextValue,
                                                                new OrderTotalColumn()
                                                                    .Name
                                                                    .TextValue
                                                            )
                                                        ),
                                                        new NumberReturning(
                                                            new NumberScalar(100)
                                                        )
                                                    )
                                                )
                                            ),
                                            new BooleanArrayReturning(
                                                new EachEquality(
                                                    new EachStringEquality(
                                                        new StringArrayReturning(
                                                            new StringField(
                                                                new JoinedString(
                                                                    new DotString(),
                                                                    [
                                                                        new RelationalSchemaWithForeignKeys().Name,
                                                                        new OrdersTable().Name,
                                                                    ]
                                                                ).TextValue,
                                                                new OrderStatusColumn()
                                                                    .Name
                                                                    .TextValue
                                                            )
                                                        ),
                                                        new StringReturning(
                                                            new StringScalar("pending")
                                                        )
                                                    )
                                                )
                                            ),
                                        ])
                                    )
                                )
                            ),
                            new BooleanArrayReturning(
                                new EachComparison(
                                    new EachNumberComparison(
                                        EachComparisonOperator.EachGreaterThanOrEqual,
                                        new NumberArrayReturning(
                                            new NumberField(
                                                new JoinedString(
                                                    new DotString(),
                                                    [
                                                        new RelationalSchemaWithForeignKeys().Name,
                                                        new OrdersTable().Name,
                                                    ]
                                                ).TextValue,
                                                new OrderTotalColumn().Name.TextValue
                                            )
                                        ),
                                        new NumberReturning(new NumberScalar(300))
                                    )
                                )
                            ),
                        ])
                    ),
                    new BooleanArrayReturning(
                        new EachOrOperator([
                            new BooleanArrayReturning(
                                new EachNotOperator(
                                    new BooleanArrayReturning(
                                        new EachEquality(
                                            new EachStringEquality(
                                                new StringArrayReturning(
                                                    new StringField(
                                                        new JoinedString(
                                                            new DotString(),
                                                            [
                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                new OrdersTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new OrderStatusColumn()
                                                            .Name
                                                            .TextValue
                                                    )
                                                ),
                                                new StringReturning(
                                                    new StringScalar("cancelled")
                                                )
                                            )
                                        )
                                    )
                                )
                            ),
                            new BooleanArrayReturning(
                                new EachAndOperator([
                                    new BooleanArrayReturning(
                                        new EachComparison(
                                            new EachNumberComparison(
                                                EachComparisonOperator.EachLessThan,
                                                new NumberArrayReturning(
                                                    new NumberField(
                                                        new JoinedString(
                                                            new DotString(),
                                                            [
                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                new OrdersTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new OrderTotalColumn()
                                                            .Name
                                                            .TextValue
                                                    )
                                                ),
                                                new NumberReturning(new NumberScalar(100))
                                            )
                                        )
                                    ),
                                    new BooleanArrayReturning(
                                        new EachEquality(
                                            new EachStringEquality(
                                                new StringArrayReturning(
                                                    new StringField(
                                                        new JoinedString(
                                                            new DotString(),
                                                            [
                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                new OrdersTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new OrderStatusColumn()
                                                            .Name
                                                            .TextValue
                                                    )
                                                ),
                                                new StringReturning(
                                                    new StringScalar("shipped")
                                                )
                                            )
                                        )
                                    ),
                                ])
                            ),
                        ])
                    ),
                ])
            ),
            [
                new Join(
                    JoinType.Inner,
                    new JoinedString(
                        new DotString(),
                        [
                            new RelationalSchemaWithForeignKeys().Name,
                            new UsersTable().Name,
                        ]
                    ).TextValue,
                    new BooleanArrayReturning(
                        new EachEquality(
                            new EachUuidEquality(
                                new UuidArrayReturning(
                                    new UuidField(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new OrdersTable().Name,
                                            ]
                                        ).TextValue,
                                        new OrderUserIdColumn().Name.TextValue
                                    )
                                ),
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
                        )
                    )
                ),
                new Join(
                    JoinType.Inner,
                    new JoinedString(
                        new DotString(),
                        [
                            new RelationalSchemaWithForeignKeys().Name,
                            new OrderItemsTable().Name,
                        ]
                    ).TextValue,
                    new BooleanArrayReturning(
                        new EachEquality(
                            new EachUuidEquality(
                                new UuidArrayReturning(
                                    new UuidField(
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
                                new UuidArrayReturning(
                                    new UuidField(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new OrderItemsTable().Name,
                                            ]
                                        ).TextValue,
                                        new ItemOrderIdColumn().Name.TextValue
                                    )
                                )
                            )
                        )
                    )
                ),
            ],
            [
                new Field(
                    new UuidField(
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
            ],
            new BooleanReturning(
                new BooleanOperator(
                    new AndOperator([
                        new BooleanReturning(
                            new BooleanOperator(
                                new OrOperator([
                                    new BooleanReturning(
                                        new Comparison(
                                            new NumberComparison(
                                                ComparisonOperator.GreaterThanOrEqual,
                                                new NumberReturning(
                                                    new Count(
                                                        new ArrayReturning(
                                                            new UuidArrayReturning(
                                                                new UuidField(
                                                                    new JoinedString(
                                                                        new DotString(),
                                                                        [
                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                            new OrderItemsTable().Name,
                                                                        ]
                                                                    ).TextValue,
                                                                    new ItemIdColumn()
                                                                        .Name
                                                                        .TextValue
                                                                )
                                                            )
                                                        )
                                                    )
                                                ),
                                                new NumberReturning(new NumberScalar(2))
                                            )
                                        )
                                    ),
                                    new BooleanReturning(
                                        new Comparison(
                                            new NumberComparison(
                                                ComparisonOperator.GreaterThanOrEqual,
                                                new NumberReturning(
                                                    new NumberAggregate(
                                                        new SumNumber(
                                                            new NumberArrayReturning(
                                                                new NumberField(
                                                                    new JoinedString(
                                                                        new DotString(),
                                                                        [
                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                            new OrderItemsTable().Name,
                                                                        ]
                                                                    ).TextValue,
                                                                    new ItemQtyColumn()
                                                                        .Name
                                                                        .TextValue
                                                                )
                                                            )
                                                        )
                                                    )
                                                ),
                                                new NumberReturning(new NumberScalar(5))
                                            )
                                        )
                                    ),
                                ])
                            )
                        ),
                        new BooleanReturning(
                            new BooleanOperator(
                                new NotOperator(
                                    new BooleanReturning(
                                        new Comparison(
                                            new NumberComparison(
                                                ComparisonOperator.GreaterThanOrEqual,
                                                new NumberReturning(
                                                    new Count(
                                                        new ArrayReturning(
                                                            new UuidArrayReturning(
                                                                new UuidField(
                                                                    new JoinedString(
                                                                        new DotString(),
                                                                        [
                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                            new OrderItemsTable().Name,
                                                                        ]
                                                                    ).TextValue,
                                                                    new ItemIdColumn()
                                                                        .Name
                                                                        .TextValue
                                                                )
                                                            )
                                                        )
                                                    )
                                                ),
                                                new NumberReturning(new NumberScalar(100))
                                            )
                                        )
                                    )
                                )
                            )
                        ),
                    ])
                )
            ),
            [
                new OrderByItem(
                    new Field(
                        new UuidField(
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
                ),
            ],
            pagination: null,
            true
        );
}
