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
using PureQL.CSharp.Model.Lists;
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.ProjectionExpressions;
using PureQL.CSharp.Model.RowExpressions;
using PureQL.CSharp.Model.SelectItems;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using ModelPagination = PureQL.CSharp.Model.Pagination;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Subqueries;

/// <summary>
/// Selects user_name, revenue and revenue divided by lines as average_line from
/// schema_with_foreign_keys.users aliased u inner joined with the subquery user_revenue
/// on user_id, where user_id is not in the subquery dormant, ordered by revenue
/// descending then user_name, skipping 0 rows and taking 50. shipped_lines selects
/// order_user_id and product_price times item_qty of the shipped order items,
/// user_revenue sums and counts them per user having a revenue greater than the integer
/// 20, and dormant selects the users whose last_login is before 2024-05-31T00:00:00Z.
/// </summary>
public sealed record SubqueryPipelineQuery
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
                        ).TextValue,
                        "u"
                    )
                ),
                [
                    new SelectItemProjection(
                        new SelectItemProjectionNonNullable(
                            new SelectItemProjectionString(
                                "user_name",
                                new StringProjection(
                                    new FieldString(
                                        "u",
                                        new UserNameColumn().Name.TextValue
                                    )
                                )
                            )
                        )
                    ),
                    new SelectItemProjection(
                        new SelectItemProjectionNonNullable(
                            new SelectItemProjectionDecimal(
                                "revenue",
                                new DecimalProjection(
                                    new FieldAsDecimal(
                                        new FieldDecimal("user_revenue", "revenue")
                                    )
                                )
                            )
                        )
                    ),
                    new SelectItemProjection(
                        new SelectItemProjectionNonNullable(
                            new SelectItemProjectionDecimal(
                                "average_line",
                                new DecimalProjection(
                                    new ArithmeticDecimalProjection(
                                        new DivideDecimalProjection([
                                            new DecimalProjection(
                                                new FieldAsDecimal(
                                                    new FieldDecimal(
                                                        "user_revenue",
                                                        "revenue"
                                                    )
                                                )
                                            ),
                                            new DecimalProjection(
                                                new FieldAsDecimal(
                                                    new FieldInteger(
                                                        "user_revenue",
                                                        "lines"
                                                    )
                                                )
                                            ),
                                        ])
                                    )
                                )
                            )
                        )
                    ),
                ],
                [
                    new Subquery(
                        "shipped_lines",
                        new Query(
                            new PlainQuery(
                                new From(
                                    new FromEntity(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new OrderItemsTable().Name,
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
                                                                new OrdersTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new OrderUserIdColumn()
                                                            .Name
                                                            .TextValue
                                                    )
                                                )
                                            )
                                        )
                                    ),
                                    new SelectItemProjection(
                                        new SelectItemProjectionNonNullable(
                                            new SelectItemProjectionDecimal(
                                                "line_total",
                                                new DecimalProjection(
                                                    new ArithmeticDecimalProjection(
                                                        new MultiplyDecimalProjection([
                                                            new DecimalProjection(
                                                                new FieldAsDecimal(
                                                                    new FieldDecimal(
                                                                        new JoinedString(
                                                                            new DotString(),
                                                                            [
                                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                                new ProductsTable().Name,
                                                                            ]
                                                                        ).TextValue,
                                                                        new ProductPriceColumn()
                                                                            .Name
                                                                            .TextValue
                                                                    )
                                                                )
                                                            ),
                                                            new DecimalProjection(
                                                                new FieldAsDecimal(
                                                                    new FieldDecimal(
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
                                                            ),
                                                        ])
                                                    )
                                                )
                                            )
                                        )
                                    ),
                                ],
                                [
                                    new Join(
                                        new JoinEntity(
                                            JoinType.Inner,
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new RelationalSchemaWithForeignKeys().Name,
                                                    new OrdersTable().Name,
                                                ]
                                            ).TextValue,
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
                                                                                new OrderItemsTable().Name,
                                                                            ]
                                                                        ).TextValue,
                                                                        new ItemOrderIdColumn()
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
                                                                                new OrdersTable().Name,
                                                                            ]
                                                                        ).TextValue,
                                                                        new OrderIdColumn()
                                                                            .Name
                                                                            .TextValue
                                                                    )
                                                                )
                                                            )
                                                        )
                                                    )
                                                )
                                            )
                                        )
                                    ),
                                    new Join(
                                        new JoinEntity(
                                            JoinType.Inner,
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new RelationalSchemaWithForeignKeys().Name,
                                                    new ProductsTable().Name,
                                                ]
                                            ).TextValue,
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
                                                                                new OrderItemsTable().Name,
                                                                            ]
                                                                        ).TextValue,
                                                                        new ItemProductIdColumn()
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
                                                                                new ProductsTable().Name,
                                                                            ]
                                                                        ).TextValue,
                                                                        new ProductIdColumn()
                                                                            .Name
                                                                            .TextValue
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
                                    new ComparisonRow(
                                        new EqualRow(
                                            new EqualStringRow(
                                                new StringNullableRow(
                                                    new FieldAsStringNullable(
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
                                                    new LiteralAsStringNullable(
                                                        new LiteralString("shipped")
                                                    )
                                                )
                                            )
                                        )
                                    )
                                ),
                                orderBy: null,
                                pagination: null,
                                distinct: false
                            )
                        )
                    ),
                    new Subquery(
                        "user_revenue",
                        new Query(
                            new GroupedQuery(
                                new From(new FromSubquery("shipped_lines")),
                                [
                                    new GroupKey(
                                        new GroupKeyNonNullable(
                                            new GroupKeyUuid(
                                                new UuidRow(
                                                    new FieldUuid(
                                                        "shipped_lines",
                                                        "user_id"
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
                                                "user_id",
                                                new UuidGroup(new KeyUuid(0))
                                            )
                                        )
                                    ),
                                    new SelectItemGroup(
                                        new SelectItemGroupNonNullable(
                                            new SelectItemGroupDecimal(
                                                "revenue",
                                                new DecimalGroup(
                                                    new AggregateDecimalGroup(
                                                        new SumDecimalGroup(
                                                            new DecimalNullableRow(
                                                                new FieldAsDecimalNullable(
                                                                    new FieldDecimal(
                                                                        "shipped_lines",
                                                                        "line_total"
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
                                            new SelectItemGroupInteger(
                                                "lines",
                                                new IntegerGroup(
                                                    new AggregateIntegerGroup(
                                                        new CountGroup()
                                                    )
                                                )
                                            )
                                        )
                                    ),
                                ],
                                joins: null,
                                where: null,
                                new BooleanGroup(
                                    new ComparisonGroup(
                                        new GreaterThanGroup(
                                            new GreaterThanDecimalGroup(
                                                new DecimalNullableGroup(
                                                    new AggregateDecimalNullableGroup(
                                                        new SumDecimalGroup(
                                                            new DecimalNullableRow(
                                                                new FieldAsDecimalNullable(
                                                                    new FieldDecimal(
                                                                        "shipped_lines",
                                                                        "line_total"
                                                                    )
                                                                )
                                                            )
                                                        )
                                                    )
                                                ),
                                                new DecimalNullableGroup(
                                                    new LiteralAsDecimalNullable(
                                                        new LiteralInteger(20)
                                                    )
                                                )
                                            )
                                        )
                                    )
                                ),
                                orderBy: null,
                                pagination: null,
                                distinct: false
                            )
                        )
                    ),
                    new Subquery(
                        "dormant",
                        new Query(
                            new PlainQuery(
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
                                                "id",
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
                                ],
                                joins: null,
                                new BooleanRow(
                                    new ComparisonRow(
                                        new LessThanRow(
                                            new LessThanDatetimeRow(
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
                                                                5,
                                                                31,
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
                                orderBy: null,
                                pagination: null,
                                distinct: false
                            )
                        )
                    ),
                ],
                [
                    new Join(
                        new JoinSubquery(
                            JoinType.Inner,
                            "user_revenue",
                            new BooleanRow(
                                new ComparisonRow(
                                    new EqualRow(
                                        new EqualUuidRow(
                                            new UuidNullableRow(
                                                new FieldAsUuidNullable(
                                                    new FieldUuid(
                                                        "u",
                                                        new UserIdColumn().Name.TextValue
                                                    )
                                                )
                                            ),
                                            new UuidNullableRow(
                                                new FieldAsUuidNullable(
                                                    new FieldUuid(
                                                        "user_revenue",
                                                        "user_id"
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
                    new LogicalRow(
                        new NotRow(
                            new BooleanRow(
                                new ComparisonRow(
                                    new InRow(
                                        new InUuidRow(
                                            new UuidNullableRow(
                                                new FieldAsUuidNullable(
                                                    new FieldUuid(
                                                        "u",
                                                        new UserIdColumn().Name.TextValue
                                                    )
                                                )
                                            ),
                                            new ListUuid(
                                                new ListSubqueryColumnUuid(
                                                    "dormant",
                                                    "id"
                                                )
                                            )
                                        )
                                    )
                                )
                            )
                        )
                    )
                ),
                [
                    new OrderItemProjection(
                        new ValueProjection(
                            new DecimalNullableProjection(
                                new FieldAsDecimalNullable(
                                    new FieldDecimal("user_revenue", "revenue")
                                )
                            )
                        ),
                        SortDirection.Desc
                    ),
                    new OrderItemProjection(
                        new ValueProjection(
                            new StringNullableProjection(
                                new FieldAsStringNullable(
                                    new FieldString(
                                        "u",
                                        new UserNameColumn().Name.TextValue
                                    )
                                )
                            )
                        )
                    ),
                ],
                new ModelPagination(0, 50),
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
                    new UserNameColumn(),
                    new Column(new String("revenue"), new DoubleColumnType()),
                    new Column(new String("average_line"), new DoubleColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new UserNameColumn(),
                                new InvariantCell(new String("Ann"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("revenue"), new DoubleColumnType()),
                                new InvariantCell(new Double(39.97))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("average_line"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(19.985))
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
                                new UserNameColumn(),
                                new InvariantCell(new String("Bob"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("revenue"), new DoubleColumnType()),
                                new InvariantCell(new Double(22.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("average_line"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(22.5))
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
