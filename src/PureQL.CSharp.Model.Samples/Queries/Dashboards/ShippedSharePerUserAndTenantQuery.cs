using Pure.Collections.Generic;
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
using Guid = Pure.Primitives.Guid.Guid;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Dashboards;

/// <summary>
/// Selects order_tenant_id and, for each of Ann, Cara and Dan as shipped_pct_ann,
/// shipped_pct_cara and shipped_pct_dan, the decimal 0 if the count of rows whose
/// user_name is that name is the integer 0, else the decimal 100 times the count of rows
/// whose order_status is shipped and whose user_name is that name divided by that count,
/// rounded to 1 digit, from schema_with_foreign_keys.orders inner joined with
/// schema_with_foreign_keys.users on order_user_id equal to user_id, where user_active is
/// true, grouped by order_tenant_id, ordered by order_tenant_id.
/// </summary>
public sealed record ShippedSharePerUserAndTenantQuery
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
                            new GroupKeyUuid(
                                "order_tenant_id",
                                new UuidRow(
                                    new FieldUuid(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new OrdersTable().Name,
                                            ]
                                        ).TextValue,
                                        new OrderTenantIdColumn().Name.TextValue
                                    )
                                )
                            )
                        )
                    ),
                ],
                [
                    new SelectItemGroup(
                        new SelectItemGroupNonNullable(
                            new SelectItemGroupUuid(
                                "order_tenant_id",
                                new UuidGroup(new KeyUuid("order_tenant_id"))
                            )
                        )
                    ),
                    new SelectItemGroup(
                        new SelectItemGroupNonNullable(
                            new SelectItemGroupDecimal(
                                "shipped_pct_ann",
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
                                                                        new CountGroup(
                                                                            new BooleanRow(
                                                                                new ComparisonRow(
                                                                                    new EqualRow(
                                                                                        new EqualStringRow(
                                                                                            new StringNullableRow(
                                                                                                new StringRow(
                                                                                                    new FieldString(
                                                                                                        new JoinedString(
                                                                                                            new DotString(),
                                                                                                            [
                                                                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                                                                new UsersTable().Name,
                                                                                                            ]
                                                                                                        ).TextValue,
                                                                                                        new UserNameColumn()
                                                                                                            .Name
                                                                                                            .TextValue
                                                                                                    )
                                                                                                )
                                                                                            ),
                                                                                            new StringNullableRow(
                                                                                                new StringRow(
                                                                                                    new LiteralString(
                                                                                                        "Ann"
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
                                                                                                            new LogicalRow(
                                                                                                                new AndRow([
                                                                                                                    new BooleanRow(
                                                                                                                        new ComparisonRow(
                                                                                                                            new EqualRow(
                                                                                                                                new EqualStringRow(
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
                                                                                                                                                new OrderStatusColumn()
                                                                                                                                                    .Name
                                                                                                                                                    .TextValue
                                                                                                                                            )
                                                                                                                                        )
                                                                                                                                    ),
                                                                                                                                    new StringNullableRow(
                                                                                                                                        new StringRow(
                                                                                                                                            new LiteralString(
                                                                                                                                                "shipped"
                                                                                                                                            )
                                                                                                                                        )
                                                                                                                                    )
                                                                                                                                )
                                                                                                                            )
                                                                                                                        )
                                                                                                                    ),
                                                                                                                    new BooleanRow(
                                                                                                                        new ComparisonRow(
                                                                                                                            new EqualRow(
                                                                                                                                new EqualStringRow(
                                                                                                                                    new StringNullableRow(
                                                                                                                                        new StringRow(
                                                                                                                                            new FieldString(
                                                                                                                                                new JoinedString(
                                                                                                                                                    new DotString(),
                                                                                                                                                    [
                                                                                                                                                        new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                        new UsersTable().Name,
                                                                                                                                                    ]
                                                                                                                                                ).TextValue,
                                                                                                                                                new UserNameColumn()
                                                                                                                                                    .Name
                                                                                                                                                    .TextValue
                                                                                                                                            )
                                                                                                                                        )
                                                                                                                                    ),
                                                                                                                                    new StringNullableRow(
                                                                                                                                        new StringRow(
                                                                                                                                            new LiteralString(
                                                                                                                                                "Ann"
                                                                                                                                            )
                                                                                                                                        )
                                                                                                                                    )
                                                                                                                                )
                                                                                                                            )
                                                                                                                        )
                                                                                                                    ),
                                                                                                                ])
                                                                                                            )
                                                                                                        )
                                                                                                    )
                                                                                                )
                                                                                            )
                                                                                        ),
                                                                                        new DecimalGroup(
                                                                                            new IntegerGroup(
                                                                                                new AggregateIntegerGroup(
                                                                                                    new CountGroup(
                                                                                                        new BooleanRow(
                                                                                                            new ComparisonRow(
                                                                                                                new EqualRow(
                                                                                                                    new EqualStringRow(
                                                                                                                        new StringNullableRow(
                                                                                                                            new StringRow(
                                                                                                                                new FieldString(
                                                                                                                                    new JoinedString(
                                                                                                                                        new DotString(),
                                                                                                                                        [
                                                                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                            new UsersTable().Name,
                                                                                                                                        ]
                                                                                                                                    ).TextValue,
                                                                                                                                    new UserNameColumn()
                                                                                                                                        .Name
                                                                                                                                        .TextValue
                                                                                                                                )
                                                                                                                            )
                                                                                                                        ),
                                                                                                                        new StringNullableRow(
                                                                                                                            new StringRow(
                                                                                                                                new LiteralString(
                                                                                                                                    "Ann"
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
                    new SelectItemGroup(
                        new SelectItemGroupNonNullable(
                            new SelectItemGroupDecimal(
                                "shipped_pct_cara",
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
                                                                        new CountGroup(
                                                                            new BooleanRow(
                                                                                new ComparisonRow(
                                                                                    new EqualRow(
                                                                                        new EqualStringRow(
                                                                                            new StringNullableRow(
                                                                                                new StringRow(
                                                                                                    new FieldString(
                                                                                                        new JoinedString(
                                                                                                            new DotString(),
                                                                                                            [
                                                                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                                                                new UsersTable().Name,
                                                                                                            ]
                                                                                                        ).TextValue,
                                                                                                        new UserNameColumn()
                                                                                                            .Name
                                                                                                            .TextValue
                                                                                                    )
                                                                                                )
                                                                                            ),
                                                                                            new StringNullableRow(
                                                                                                new StringRow(
                                                                                                    new LiteralString(
                                                                                                        "Cara"
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
                                                                                                            new LogicalRow(
                                                                                                                new AndRow([
                                                                                                                    new BooleanRow(
                                                                                                                        new ComparisonRow(
                                                                                                                            new EqualRow(
                                                                                                                                new EqualStringRow(
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
                                                                                                                                                new OrderStatusColumn()
                                                                                                                                                    .Name
                                                                                                                                                    .TextValue
                                                                                                                                            )
                                                                                                                                        )
                                                                                                                                    ),
                                                                                                                                    new StringNullableRow(
                                                                                                                                        new StringRow(
                                                                                                                                            new LiteralString(
                                                                                                                                                "shipped"
                                                                                                                                            )
                                                                                                                                        )
                                                                                                                                    )
                                                                                                                                )
                                                                                                                            )
                                                                                                                        )
                                                                                                                    ),
                                                                                                                    new BooleanRow(
                                                                                                                        new ComparisonRow(
                                                                                                                            new EqualRow(
                                                                                                                                new EqualStringRow(
                                                                                                                                    new StringNullableRow(
                                                                                                                                        new StringRow(
                                                                                                                                            new FieldString(
                                                                                                                                                new JoinedString(
                                                                                                                                                    new DotString(),
                                                                                                                                                    [
                                                                                                                                                        new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                        new UsersTable().Name,
                                                                                                                                                    ]
                                                                                                                                                ).TextValue,
                                                                                                                                                new UserNameColumn()
                                                                                                                                                    .Name
                                                                                                                                                    .TextValue
                                                                                                                                            )
                                                                                                                                        )
                                                                                                                                    ),
                                                                                                                                    new StringNullableRow(
                                                                                                                                        new StringRow(
                                                                                                                                            new LiteralString(
                                                                                                                                                "Cara"
                                                                                                                                            )
                                                                                                                                        )
                                                                                                                                    )
                                                                                                                                )
                                                                                                                            )
                                                                                                                        )
                                                                                                                    ),
                                                                                                                ])
                                                                                                            )
                                                                                                        )
                                                                                                    )
                                                                                                )
                                                                                            )
                                                                                        ),
                                                                                        new DecimalGroup(
                                                                                            new IntegerGroup(
                                                                                                new AggregateIntegerGroup(
                                                                                                    new CountGroup(
                                                                                                        new BooleanRow(
                                                                                                            new ComparisonRow(
                                                                                                                new EqualRow(
                                                                                                                    new EqualStringRow(
                                                                                                                        new StringNullableRow(
                                                                                                                            new StringRow(
                                                                                                                                new FieldString(
                                                                                                                                    new JoinedString(
                                                                                                                                        new DotString(),
                                                                                                                                        [
                                                                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                            new UsersTable().Name,
                                                                                                                                        ]
                                                                                                                                    ).TextValue,
                                                                                                                                    new UserNameColumn()
                                                                                                                                        .Name
                                                                                                                                        .TextValue
                                                                                                                                )
                                                                                                                            )
                                                                                                                        ),
                                                                                                                        new StringNullableRow(
                                                                                                                            new StringRow(
                                                                                                                                new LiteralString(
                                                                                                                                    "Cara"
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
                    new SelectItemGroup(
                        new SelectItemGroupNonNullable(
                            new SelectItemGroupDecimal(
                                "shipped_pct_dan",
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
                                                                        new CountGroup(
                                                                            new BooleanRow(
                                                                                new ComparisonRow(
                                                                                    new EqualRow(
                                                                                        new EqualStringRow(
                                                                                            new StringNullableRow(
                                                                                                new StringRow(
                                                                                                    new FieldString(
                                                                                                        new JoinedString(
                                                                                                            new DotString(),
                                                                                                            [
                                                                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                                                                new UsersTable().Name,
                                                                                                            ]
                                                                                                        ).TextValue,
                                                                                                        new UserNameColumn()
                                                                                                            .Name
                                                                                                            .TextValue
                                                                                                    )
                                                                                                )
                                                                                            ),
                                                                                            new StringNullableRow(
                                                                                                new StringRow(
                                                                                                    new LiteralString(
                                                                                                        "Dan"
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
                                                                                                            new LogicalRow(
                                                                                                                new AndRow([
                                                                                                                    new BooleanRow(
                                                                                                                        new ComparisonRow(
                                                                                                                            new EqualRow(
                                                                                                                                new EqualStringRow(
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
                                                                                                                                                new OrderStatusColumn()
                                                                                                                                                    .Name
                                                                                                                                                    .TextValue
                                                                                                                                            )
                                                                                                                                        )
                                                                                                                                    ),
                                                                                                                                    new StringNullableRow(
                                                                                                                                        new StringRow(
                                                                                                                                            new LiteralString(
                                                                                                                                                "shipped"
                                                                                                                                            )
                                                                                                                                        )
                                                                                                                                    )
                                                                                                                                )
                                                                                                                            )
                                                                                                                        )
                                                                                                                    ),
                                                                                                                    new BooleanRow(
                                                                                                                        new ComparisonRow(
                                                                                                                            new EqualRow(
                                                                                                                                new EqualStringRow(
                                                                                                                                    new StringNullableRow(
                                                                                                                                        new StringRow(
                                                                                                                                            new FieldString(
                                                                                                                                                new JoinedString(
                                                                                                                                                    new DotString(),
                                                                                                                                                    [
                                                                                                                                                        new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                        new UsersTable().Name,
                                                                                                                                                    ]
                                                                                                                                                ).TextValue,
                                                                                                                                                new UserNameColumn()
                                                                                                                                                    .Name
                                                                                                                                                    .TextValue
                                                                                                                                            )
                                                                                                                                        )
                                                                                                                                    ),
                                                                                                                                    new StringNullableRow(
                                                                                                                                        new StringRow(
                                                                                                                                            new LiteralString(
                                                                                                                                                "Dan"
                                                                                                                                            )
                                                                                                                                        )
                                                                                                                                    )
                                                                                                                                )
                                                                                                                            )
                                                                                                                        )
                                                                                                                    ),
                                                                                                                ])
                                                                                                            )
                                                                                                        )
                                                                                                    )
                                                                                                )
                                                                                            )
                                                                                        ),
                                                                                        new DecimalGroup(
                                                                                            new IntegerGroup(
                                                                                                new AggregateIntegerGroup(
                                                                                                    new CountGroup(
                                                                                                        new BooleanRow(
                                                                                                            new ComparisonRow(
                                                                                                                new EqualRow(
                                                                                                                    new EqualStringRow(
                                                                                                                        new StringNullableRow(
                                                                                                                            new StringRow(
                                                                                                                                new FieldString(
                                                                                                                                    new JoinedString(
                                                                                                                                        new DotString(),
                                                                                                                                        [
                                                                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                            new UsersTable().Name,
                                                                                                                                        ]
                                                                                                                                    ).TextValue,
                                                                                                                                    new UserNameColumn()
                                                                                                                                        .Name
                                                                                                                                        .TextValue
                                                                                                                                )
                                                                                                                            )
                                                                                                                        ),
                                                                                                                        new StringNullableRow(
                                                                                                                            new StringRow(
                                                                                                                                new LiteralString(
                                                                                                                                    "Dan"
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
                [
                    new Join(
                        new JoinEntity(
                            JoinKind.Inner,
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue,
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
                                                        new OrderUserIdColumn()
                                                            .Name
                                                            .TextValue
                                                    )
                                                )
                                            ),
                                            new UuidNullableRow(
                                                new UuidRow(
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
                                    )
                                )
                            )
                        )
                    ),
                ],
                new BooleanRow(
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
                ),
                having: null,
                [
                    new OrderItemGroup(
                        new ExprGroup(
                            new UuidNullableGroup(
                                new UuidGroup(new KeyUuid("order_tenant_id"))
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
                    new OrderTenantIdColumn(),
                    new Column(new String("shipped_pct_ann"), new DoubleColumnType()),
                    new Column(new String("shipped_pct_cara"), new DoubleColumnType()),
                    new Column(new String("shipped_pct_dan"), new DoubleColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new OrderTenantIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000385-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("shipped_pct_ann"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(50))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("shipped_pct_cara"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("shipped_pct_dan"),
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
                                new OrderTenantIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000386-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("shipped_pct_ann"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("shipped_pct_cara"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(50))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("shipped_pct_dan"),
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
            ]
        );
}
