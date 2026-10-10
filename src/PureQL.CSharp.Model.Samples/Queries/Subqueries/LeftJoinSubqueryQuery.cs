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
using PureQL.CSharp.Model.ProjectionExpressions;
using PureQL.CSharp.Model.RowExpressions;
using PureQL.CSharp.Model.SelectItems;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Subqueries;

/// <summary>
/// Selects user_name, the subquery's orders or else the integer 0 as orders and the
/// nullable subquery total from schema_with_foreign_keys.users left joined with the
/// subquery user_totals on user_id. The subquery sums order_total and counts the shipped
/// orders of schema_with_foreign_keys.orders per order_user_id.
/// </summary>
public sealed record LeftJoinSubqueryQuery
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
                            new SelectItemProjectionString(
                                "user_name",
                                new StringProjection(
                                    new FieldString(
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
                            )
                        )
                    ),
                    new SelectItemProjection(
                        new SelectItemProjectionNonNullable(
                            new SelectItemProjectionInteger(
                                "orders",
                                new IntegerProjection(
                                    new ConditionalIntegerProjection(
                                        new CoalesceIntegerProjection([
                                            new IntegerNullableProjection(
                                                new FieldAsIntegerNullable(
                                                    new FieldIntegerNullable(
                                                        "user_totals",
                                                        "orders"
                                                    )
                                                )
                                            ),
                                            new IntegerNullableProjection(
                                                new LiteralAsIntegerNullable(
                                                    new LiteralInteger(0)
                                                )
                                            ),
                                        ])
                                    )
                                )
                            )
                        )
                    ),
                    new SelectItemProjection(
                        new SelectItemProjectionNullable(
                            new SelectItemProjectionDecimalNullable(
                                "total",
                                new DecimalNullableProjection(
                                    new FieldAsDecimalNullable(
                                        new FieldDecimalNullable("user_totals", "total")
                                    )
                                )
                            )
                        )
                    ),
                ],
                [
                    new Subquery(
                        "user_totals",
                        new Query(
                            new GroupedQuery(
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
                                                        new OrderUserIdColumn()
                                                            .Name
                                                            .TextValue
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
                                                "total",
                                                new DecimalGroup(
                                                    new AggregateDecimalGroup(
                                                        new SumDecimalGroup(
                                                            new DecimalNullableRow(
                                                                new FieldAsDecimalNullable(
                                                                    new FieldDecimal(
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
                                                "orders",
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
                                having: null,
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
                            JoinType.Left,
                            "user_totals",
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
                                                                new UsersTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new UserIdColumn().Name.TextValue
                                                    )
                                                )
                                            ),
                                            new UuidNullableRow(
                                                new FieldAsUuidNullable(
                                                    new FieldUuid(
                                                        "user_totals",
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
                where: null,
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
                    new UserNameColumn(),
                    new Column(new String("orders"), new LongColumnType()),
                    new Column(new String("total"), new DoubleColumnType()),
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
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("total"), new DoubleColumnType()),
                                new InvariantCell(new Double(100.5))
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
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("total"), new DoubleColumnType()),
                                new InvariantCell(new Double(200))
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
                                new InvariantCell(new String("Cara"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("total"), new DoubleColumnType()),
                                new InvariantCell(new Double(300))
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
                                new InvariantCell(new String("Dan"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("total"), new DoubleColumnType()),
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
                                new UserNameColumn(),
                                new InvariantCell(new String("Eve"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("total"), new DoubleColumnType()),
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
                                new UserNameColumn(),
                                new InvariantCell(new String("Fay"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("orders"), new LongColumnType()),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("total"), new DoubleColumnType()),
                                new EmptyCell()
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
