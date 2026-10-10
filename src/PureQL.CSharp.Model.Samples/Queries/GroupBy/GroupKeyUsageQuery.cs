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
using Guid = Pure.Primitives.Guid.Guid;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.GroupBy;

/// <summary>
/// Selects order_user_id, the buyer's user_name or else unknown as buyer, the days since
/// signup times 24 as hours_since_signup and the count of rows as orders from
/// schema_with_foreign_keys.orders left joined with schema_with_foreign_keys.users on
/// order_user_id equal to user_id and user_active, grouped by order_user_id, the nullable
/// user_name and the nullable days from signup_date to placed_on, having a known
/// user_name and at most 1600 days, ordered by those days then by the count descending.
/// </summary>
public sealed record GroupKeyUsageQuery
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
                                new UuidRow(
                                    new FieldUuid(
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
                                "order_user_id"
                            )
                        )
                    ),
                    new GroupKey(
                        new GroupKeyNullable(
                            new GroupKeyStringNullable(
                                new StringNullableRow(
                                    new FieldAsStringNullable(
                                        new FieldStringNullable(
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new RelationalSchemaWithForeignKeys().Name,
                                                    new UsersTable().Name,
                                                ]
                                            ).TextValue,
                                            new UserNameColumn().Name.TextValue
                                        )
                                    )
                                ),
                                "buyer"
                            )
                        )
                    ),
                    new GroupKey(
                        new GroupKeyNullable(
                            new GroupKeyIntegerNullable(
                                new IntegerNullableRow(
                                    new DateDiffDaysIntegerNullableRow(
                                        new DateNullableRow(
                                            new FieldAsDateNullable(
                                                new FieldDate(
                                                    new JoinedString(
                                                        new DotString(),
                                                        [
                                                            new RelationalSchemaWithForeignKeys().Name,
                                                            new OrdersTable().Name,
                                                        ]
                                                    ).TextValue,
                                                    new PlacedOnColumn().Name.TextValue
                                                )
                                            )
                                        ),
                                        new DateNullableRow(
                                            new FieldAsDateNullable(
                                                new FieldDateNullable(
                                                    new JoinedString(
                                                        new DotString(),
                                                        [
                                                            new RelationalSchemaWithForeignKeys().Name,
                                                            new UsersTable().Name,
                                                        ]
                                                    ).TextValue,
                                                    new SignupDateColumn().Name.TextValue
                                                )
                                            )
                                        )
                                    )
                                ),
                                "days_since_signup"
                            )
                        )
                    ),
                ],
                [
                    new SelectItemGroup(
                        new SelectItemGroupNonNullable(
                            new SelectItemGroupUuid(
                                "order_user_id",
                                new UuidGroup(new KeyUuid(0))
                            )
                        )
                    ),
                    new SelectItemGroup(
                        new SelectItemGroupNonNullable(
                            new SelectItemGroupString(
                                "buyer",
                                new StringGroup(
                                    new ConditionalStringGroup(
                                        new CoalesceStringGroup([
                                            new StringNullableGroup(
                                                new KeyAsStringNullable(
                                                    new KeyStringNullable(1)
                                                )
                                            ),
                                            new StringNullableGroup(
                                                new LiteralAsStringNullable(
                                                    new LiteralString("unknown")
                                                )
                                            ),
                                        ])
                                    )
                                )
                            )
                        )
                    ),
                    new SelectItemGroup(
                        new SelectItemGroupNullable(
                            new SelectItemGroupIntegerNullable(
                                "hours_since_signup",
                                new IntegerNullableGroup(
                                    new ArithmeticIntegerNullableGroup(
                                        new MultiplyIntegerNullableGroup([
                                            new IntegerNullableGroup(
                                                new KeyAsIntegerNullable(
                                                    new KeyIntegerNullable(2)
                                                )
                                            ),
                                            new IntegerNullableGroup(
                                                new LiteralAsIntegerNullable(
                                                    new LiteralInteger(24)
                                                )
                                            ),
                                        ])
                                    )
                                )
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
                ],
                subqueries: null,
                [
                    new Join(
                        new JoinEntity(
                            JoinType.Left,
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue,
                            new BooleanRow(
                                new LogicalRow(
                                    new AndRow([
                                        new BooleanRow(
                                            new ComparisonRow(
                                                new EqualRow(
                                                    new EqualUuidRow(
                                                        new UuidNullableRow(
                                                            new FieldAsUuidNullable(
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
                                                            new FieldAsUuidNullable(
                                                                new FieldUuid(
                                                                    new JoinedString(
                                                                        new DotString(),
                                                                        [
                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                            new UsersTable().Name,
                                                                        ]
                                                                    ).TextValue,
                                                                    new UserIdColumn()
                                                                        .Name
                                                                        .TextValue
                                                                )
                                                            )
                                                        )
                                                    )
                                                )
                                            )
                                        ),
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
                                    ])
                                )
                            )
                        )
                    ),
                ],
                where: null,
                new BooleanGroup(
                    new LogicalGroup(
                        new AndGroup([
                            new BooleanGroup(
                                new ComparisonGroup(
                                    new NotEqualGroup(
                                        new NotEqualStringGroup(
                                            new StringNullableGroup(
                                                new KeyAsStringNullable(
                                                    new KeyStringNullable(1)
                                                )
                                            ),
                                            new StringNullableGroup(
                                                new LiteralAsStringNullable(
                                                    new LiteralStringNullable()
                                                )
                                            )
                                        )
                                    )
                                )
                            ),
                            new BooleanGroup(
                                new ComparisonGroup(
                                    new LessThanOrEqualGroup(
                                        new LessThanOrEqualDecimalGroup(
                                            new DecimalNullableGroup(
                                                new KeyAsDecimalNullable(
                                                    new KeyIntegerNullable(2)
                                                )
                                            ),
                                            new DecimalNullableGroup(
                                                new LiteralAsDecimalNullable(
                                                    new LiteralInteger(1600)
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
                    new OrderItemGroup(
                        new ValueGroup(
                            new DecimalNullableGroup(
                                new KeyAsDecimalNullable(new KeyIntegerNullable(2))
                            )
                        )
                    ),
                    new OrderItemGroup(
                        new ValueGroup(
                            new DecimalNullableGroup(
                                new AggregateDecimalNullableGroup(new CountGroup())
                            )
                        ),
                        SortDirection.Desc
                    ),
                ],
                pagination: null,
                distinct: false
            )
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and
    /// <see cref="AuditSchemaDataSet"/>, in this order.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new OrderUserIdColumn(),
                    new Column(new String("buyer"), new StringColumnType()),
                    new Column(new String("hours_since_signup"), new LongColumnType()),
                    new Column(new String("orders"), new LongColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new OrderUserIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000004-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("buyer"), new StringColumnType()),
                                new InvariantCell(new String("Dan"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("hours_since_signup"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(13896))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(1))
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
                                new OrderUserIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000001-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("buyer"), new StringColumnType()),
                                new InvariantCell(new String("Ann"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("hours_since_signup"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(38376))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(1))
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
                                new OrderUserIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000001-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("buyer"), new StringColumnType()),
                                new InvariantCell(new String("Ann"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("hours_since_signup"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(38400))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(1))
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
