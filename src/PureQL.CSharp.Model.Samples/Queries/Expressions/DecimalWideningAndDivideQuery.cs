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
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Expressions;

/// <summary>
/// Selects item_id, product_price times the rounded item_qty as line_total, the rounded
/// item_qty divided by the integer 2 as half_quantity and product_price times 1.2 as
/// price_with_tax from schema_with_foreign_keys.order_items inner joined with
/// schema_with_foreign_keys.products on item_product_id equal to product_id.
/// </summary>
public sealed record DecimalWideningAndDivideQuery
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
                                new OrderItemsTable().Name,
                            ]
                        ).TextValue
                    )
                ),
                [
                    new SelectItemProjection(
                        new SelectItemProjectionNonNullable(
                            new SelectItemProjectionUuid(
                                "item_id",
                                new UuidProjection(
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
                                                new RoundingDecimalProjection(
                                                    new RoundIntegerProjection(
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
                                                        )
                                                    )
                                                )
                                            ),
                                        ])
                                    )
                                )
                            )
                        )
                    ),
                    new SelectItemProjection(
                        new SelectItemProjectionNonNullable(
                            new SelectItemProjectionDecimal(
                                "half_quantity",
                                new DecimalProjection(
                                    new ArithmeticDecimalProjection(
                                        new DivideDecimalProjection([
                                            new DecimalProjection(
                                                new RoundingDecimalProjection(
                                                    new RoundIntegerProjection(
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
                                                        )
                                                    )
                                                )
                                            ),
                                            new DecimalProjection(
                                                new LiteralAsDecimal(
                                                    new LiteralInteger(2)
                                                )
                                            ),
                                        ])
                                    )
                                )
                            )
                        )
                    ),
                    new SelectItemProjection(
                        new SelectItemProjectionNonNullable(
                            new SelectItemProjectionDecimal(
                                "price_with_tax",
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
                                                new LiteralAsDecimal(
                                                    new LiteralDecimal(1.2m)
                                                )
                                            ),
                                        ])
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
                    new ItemIdColumn(),
                    new Column(new String("line_total"), new DoubleColumnType()),
                    new Column(new String("half_quantity"), new DoubleColumnType()),
                    new Column(new String("price_with_tax"), new DoubleColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new ItemIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "0000012d-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("line_total"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(19.98))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("half_quantity"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("price_with_tax"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(11.988))
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
                                new ItemIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "0000012e-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("line_total"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(19.99))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("half_quantity"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("price_with_tax"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(23.988))
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
                                new ItemIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "0000012f-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("line_total"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(22.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("half_quantity"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(2.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("price_with_tax"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(5.4))
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
                                new ItemIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000130-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("line_total"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(29.97))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("half_quantity"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("price_with_tax"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(11.988))
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
