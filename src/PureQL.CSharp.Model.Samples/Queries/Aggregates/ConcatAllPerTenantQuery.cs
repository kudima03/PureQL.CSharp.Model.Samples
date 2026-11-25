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
using PureQL.CSharp.Model.Keys;
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.RowExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using Guid = Pure.Primitives.Guid.Guid;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Aggregates;

/// <summary>
/// Selects, per item_tenant_id of schema_with_foreign_keys.order_items inner joined to
/// schema_with_foreign_keys.products on item_product_id = product_id and to
/// schema_with_foreign_keys.orders on item_order_id = order_id, the count of lines, the
/// product_name of every line joined by ", " in order of product_name and item_id, the
/// distinct order_status values joined by ", ", the names of the products in stock (an if
/// that is null otherwise, so the others are skipped) joined by " | ", and the names of
/// the lines of more than one unit (a predicate) by item_qty descending, ordered by
/// item_tenant_id.
/// </summary>
public sealed record ConcatAllPerTenantQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainGroupedQueryWithoutPagination(
            from: new FromEntity(
                new JoinedString(
                    new DotString(),
                    [
                        new RelationalSchemaWithForeignKeys().Name,
                        new OrderItemsTable().Name,
                    ]
                ).TextValue
            ),
            joins:
            [
                new JoinEntity(
                    JoinKind.Inner,
                    new JoinedString(
                        new DotString(),
                        [
                            new RelationalSchemaWithForeignKeys().Name,
                            new ProductsTable().Name,
                        ]
                    ).TextValue,
                    new EqualUuidRow(
                        new FieldUuid(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrderItemsTable().Name,
                                ]
                            ).TextValue,
                            new ItemProductIdColumn().Name.TextValue
                        ),
                        new FieldUuid(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new ProductsTable().Name,
                                ]
                            ).TextValue,
                            new ProductIdColumn().Name.TextValue
                        )
                    )
                ),
                new JoinEntity(
                    JoinKind.Inner,
                    new JoinedString(
                        new DotString(),
                        [
                            new RelationalSchemaWithForeignKeys().Name,
                            new OrdersTable().Name,
                        ]
                    ).TextValue,
                    new EqualUuidRow(
                        new FieldUuid(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrderItemsTable().Name,
                                ]
                            ).TextValue,
                            new ItemOrderIdColumn().Name.TextValue
                        ),
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
                ),
            ],
            groupBy:
            [
                new GroupKey(
                    "item_tenant_id",
                    new FieldUuid(
                        new JoinedString(
                            new DotString(),
                            [
                                new RelationalSchemaWithForeignKeys().Name,
                                new OrderItemsTable().Name,
                            ]
                        ).TextValue,
                        new ItemTenantIdColumn().Name.TextValue
                    )
                ),
            ],
            select:
            [
                new SelectItemGroup("item_tenant_id", new KeyUuid("item_tenant_id")),
                new SelectItemGroup("lines", new CountGroup()),
                new SelectItemGroup(
                    "product_names",
                    new ConcatAllGroup(
                        new FieldString(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new ProductsTable().Name,
                                ]
                            ).TextValue,
                            new ProductNameColumn().Name.TextValue
                        ),
                        new LiteralString(", "),
                        [
                            new OrderItemRow(
                                new FieldString(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new ProductsTable().Name,
                                        ]
                                    ).TextValue,
                                    new ProductNameColumn().Name.TextValue
                                )
                            ),
                            new OrderItemRow(
                                new FieldUuid(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new OrderItemsTable().Name,
                                        ]
                                    ).TextValue,
                                    new ItemIdColumn().Name.TextValue
                                )
                            ),
                        ]
                    )
                ),
                new SelectItemGroup(
                    "order_statuses",
                    new ConcatAllGroup(
                        new FieldString(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrdersTable().Name,
                                ]
                            ).TextValue,
                            new OrderStatusColumn().Name.TextValue
                        ),
                        new LiteralString(", "),
                        [
                            new OrderItemRow(
                                new FieldString(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new OrdersTable().Name,
                                        ]
                                    ).TextValue,
                                    new OrderStatusColumn().Name.TextValue
                                )
                            ),
                        ],
                        distinct: true
                    )
                ),
                new SelectItemGroup(
                    "in_stock_names",
                    new ConcatAllGroup(
                        new IfStringNullableRow(
                            new FieldString(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new ProductsTable().Name,
                                    ]
                                ).TextValue,
                                new ProductNameColumn().Name.TextValue
                            ),
                            new LiteralStringNullable(),
                            new FieldBoolean(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new ProductsTable().Name,
                                    ]
                                ).TextValue,
                                new ProductInStockColumn().Name.TextValue
                            )
                        ),
                        new LiteralString(" | "),
                        [
                            new OrderItemRow(
                                new FieldString(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new ProductsTable().Name,
                                        ]
                                    ).TextValue,
                                    new ProductNameColumn().Name.TextValue
                                )
                            ),
                            new OrderItemRow(
                                new FieldUuid(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new OrderItemsTable().Name,
                                        ]
                                    ).TextValue,
                                    new ItemIdColumn().Name.TextValue
                                )
                            ),
                        ]
                    )
                ),
                new SelectItemGroup(
                    "lines_above_one_unit",
                    new ConcatAllGroup(
                        new FieldString(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new ProductsTable().Name,
                                ]
                            ).TextValue,
                            new ProductNameColumn().Name.TextValue
                        ),
                        new LiteralString(", "),
                        [
                            new OrderItemRow(
                                new FieldDecimal(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new OrderItemsTable().Name,
                                        ]
                                    ).TextValue,
                                    new ItemQtyColumn().Name.TextValue
                                ),
                                SortDirection.Desc
                            ),
                            new OrderItemRow(
                                new FieldUuid(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new OrderItemsTable().Name,
                                        ]
                                    ).TextValue,
                                    new ItemIdColumn().Name.TextValue
                                )
                            ),
                        ],
                        new GreaterThanDecimalRow(
                            new FieldDecimal(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new OrderItemsTable().Name,
                                    ]
                                ).TextValue,
                                new ItemQtyColumn().Name.TextValue
                            ),
                            new LiteralInteger(1)
                        )
                    )
                ),
            ],
            orderBy: [new OrderItemGroupExpression(new KeyUuid("item_tenant_id"))]
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
                    new ItemTenantIdColumn(),
                    new Column(new String("lines"), new LongColumnType()),
                    new Column(new String("product_names"), new StringColumnType()),
                    new Column(new String("order_statuses"), new StringColumnType()),
                    new Column(new String("in_stock_names"), new StringColumnType()),
                    new Column(
                        new String("lines_above_one_unit"),
                        new StringColumnType()
                    ),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new ItemTenantIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000385-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("lines"), new LongColumnType()),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("product_names"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Gadget, Gizmo, Widget"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("order_statuses"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("shipped"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("in_stock_names"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Gizmo | Widget"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lines_above_one_unit"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Gizmo, Widget"))
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
                                new ItemTenantIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000386-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("lines"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("product_names"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Widget"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("order_statuses"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("shipped"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("in_stock_names"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Widget"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lines_above_one_unit"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Widget"))
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
