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
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.ProjectionExpressions;
using PureQL.CSharp.Model.RowExpressions;
using PureQL.CSharp.Model.SelectItems;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using Guid = Pure.Primitives.Guid.Guid;
using ModelPagination = PureQL.CSharp.Model.Pagination;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Subqueries;

/// <summary>
/// Selects id and total from the subquery recent_orders, which selects order_id as id and
/// order_total as total from schema_with_foreign_keys.orders where placed_at is at or
/// after 2024-06-03T00:00:00Z, ordered by total descending, skipping 0 rows and taking 2.
/// </summary>
public sealed record FromSubqueryQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new PureQLQuery(
            new MainPlainQuery(
                new From(new FromSubquery("recent_orders")),
                [
                    new SelectItemProjection(
                        new SelectItemProjectionNonNullable(
                            new SelectItemProjectionUuid(
                                "id",
                                new UuidProjection(new FieldUuid("recent_orders", "id"))
                            )
                        )
                    ),
                    new SelectItemProjection(
                        new SelectItemProjectionNonNullable(
                            new SelectItemProjectionDecimal(
                                "total",
                                new DecimalProjection(
                                    new FieldAsDecimal(
                                        new FieldDecimal("recent_orders", "total")
                                    )
                                )
                            )
                        )
                    ),
                ],
                [
                    new Subquery(
                        "recent_orders",
                        new Query(
                            new PlainQuery(
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
                                                                new OrdersTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new OrderIdColumn().Name.TextValue
                                                    )
                                                )
                                            )
                                        )
                                    ),
                                    new SelectItemProjection(
                                        new SelectItemProjectionNonNullable(
                                            new SelectItemProjectionDecimal(
                                                "total",
                                                new DecimalProjection(
                                                    new FieldAsDecimal(
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
                                    ),
                                ],
                                joins: null,
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
                                                                    new OrdersTable().Name,
                                                                ]
                                                            ).TextValue,
                                                            new PlacedAtColumn()
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
                                                                3,
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
                joins: null,
                where: null,
                [
                    new OrderItemProjection(
                        new ValueProjection(
                            new DecimalNullableProjection(
                                new FieldAsDecimalNullable(
                                    new FieldDecimal("recent_orders", "total")
                                )
                            )
                        ),
                        SortDirection.Desc
                    ),
                ],
                new ModelPagination(0, 2),
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
                [new IdColumn(), new Column(new String("total"), new DoubleColumnType())],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new IdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000069-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
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
                                new IdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000067-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
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
            ]
        );
}
