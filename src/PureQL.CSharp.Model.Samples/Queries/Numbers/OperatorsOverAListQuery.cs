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
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.ProjectionExpressions;
using PureQL.CSharp.Model.RowExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using Guid = Pure.Primitives.Guid.Guid;
using ModelPagination = PureQL.CSharp.Model.Pagination;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Numbers;

/// <summary>
/// Selects from schema_with_foreign_keys.order_items inner joined to
/// schema_with_foreign_keys.products on item_product_id, where the product of
/// product_price, item_qty and 0.2 is greater than 3.997: item_id, that product as tax,
/// the rounded item_qty minus 1 minus 1, product_price divided by 3 and then by 2,
/// product_price divided once by 3 times 2, the rounded item_qty plus 1 plus 0.5, the sum
/// of the rounded item_qty and the average product_price over all rows, ordered by the
/// tax column descending and item_id, with a take of 50.
/// </summary>
public sealed record OperatorsOverAListQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithPagination(
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
            ],
            where: new GreaterThanDecimalRow(
                new MultiplyDecimalRow([
                    new FieldDecimal(
                        new JoinedString(
                            new DotString(),
                            [
                                new RelationalSchemaWithForeignKeys().Name,
                                new ProductsTable().Name,
                            ]
                        ).TextValue,
                        new ProductPriceColumn().Name.TextValue
                    ),
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
                    new LiteralDecimal(0.2m),
                ]),
                new LiteralDecimal(3.997m)
            ),
            select:
            [
                new SelectItemProjection(
                    "item_id",
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
                new SelectItemProjection(
                    "tax",
                    new MultiplyDecimalProjection([
                        new FieldDecimal(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new ProductsTable().Name,
                                ]
                            ).TextValue,
                            new ProductPriceColumn().Name.TextValue
                        ),
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
                        new LiteralDecimal(0.2m),
                    ])
                ),
                new SelectItemProjection(
                    "kept_units",
                    new SubtractIntegerProjection([
                        new RoundWithoutDigitsIntegerProjection(
                            new FieldDecimal(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new OrderItemsTable().Name,
                                    ]
                                ).TextValue,
                                new ItemQtyColumn().Name.TextValue
                            )
                        ),
                        new LiteralInteger(1),
                        new LiteralInteger(1),
                    ])
                ),
                new SelectItemProjection(
                    "share_in_steps",
                    new DivideDecimalProjection([
                        new FieldDecimal(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new ProductsTable().Name,
                                ]
                            ).TextValue,
                            new ProductPriceColumn().Name.TextValue
                        ),
                        new LiteralInteger(3),
                        new LiteralInteger(2),
                    ])
                ),
                new SelectItemProjection(
                    "share_divided_once",
                    new DivideDecimalProjection([
                        new FieldDecimal(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new ProductsTable().Name,
                                ]
                            ).TextValue,
                            new ProductPriceColumn().Name.TextValue
                        ),
                        new MultiplyIntegerProjection([
                            new LiteralInteger(3),
                            new LiteralInteger(2),
                        ]),
                    ])
                ),
                new SelectItemProjection(
                    "units_and_a_half",
                    new AddDecimalProjection([
                        new RoundWithoutDigitsIntegerProjection(
                            new FieldDecimal(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new OrderItemsTable().Name,
                                    ]
                                ).TextValue,
                                new ItemQtyColumn().Name.TextValue
                            )
                        ),
                        new LiteralInteger(1),
                        new LiteralDecimal(0.5m),
                    ])
                ),
                new SelectItemProjection(
                    "all_units",
                    new SumIntegerProjection(
                        new RoundWithoutDigitsIntegerRow(
                            new FieldDecimal(
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
                ),
                new SelectItemProjection(
                    "average_price",
                    new AverageDecimalProjection(
                        new FieldDecimal(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new ProductsTable().Name,
                                ]
                            ).TextValue,
                            new ProductPriceColumn().Name.TextValue
                        )
                    )
                ),
            ],
            orderBy:
            [
                new OrderItemProjectionColumn("tax", SortDirection.Desc),
                new OrderItemProjectionExpression(
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
            pagination: new ModelPagination(take: 50)
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and
    /// <see cref="AuditSchemaDataSet"/>, in this order.
    /// Each step of a division over three values is rounded on its own, as the
    /// specification folds them.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new ItemIdColumn(),
                    new Column(new String("tax"), new DoubleColumnType()),
                    new Column(new String("kept_units"), new LongColumnType()),
                    new Column(new String("share_in_steps"), new DoubleColumnType()),
                    new Column(new String("share_divided_once"), new DoubleColumnType()),
                    new Column(new String("units_and_a_half"), new DoubleColumnType()),
                    new Column(new String("all_units"), new LongColumnType()),
                    new Column(new String("average_price"), new DoubleColumnType()),
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
                                            "00000130-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("tax"), new DoubleColumnType()),
                                new InvariantCell(new Double(5.994))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("kept_units"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("share_in_steps"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1.665))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("share_divided_once"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1.665))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("units_and_a_half"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(4.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("all_units"), new LongColumnType()),
                                new InvariantCell(new Long(9))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("average_price"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(11.493333333333334))
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
                                new Column(new String("tax"), new DoubleColumnType()),
                                new InvariantCell(new Double(4.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("kept_units"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("share_in_steps"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.75))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("share_divided_once"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.75))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("units_and_a_half"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(6.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("all_units"), new LongColumnType()),
                                new InvariantCell(new Long(9))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("average_price"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(11.493333333333334))
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
                                new Column(new String("tax"), new DoubleColumnType()),
                                new InvariantCell(new Double(3.998))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("kept_units"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(-1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("share_in_steps"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(3.3316666666666666))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("share_divided_once"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(3.3316666666666666))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("units_and_a_half"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(2.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("all_units"), new LongColumnType()),
                                new InvariantCell(new Long(9))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("average_price"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(11.493333333333334))
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
