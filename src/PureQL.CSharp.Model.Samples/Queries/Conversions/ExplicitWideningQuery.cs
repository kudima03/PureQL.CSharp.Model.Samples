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
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Conversions;

/// <summary>
/// Selects from a subquery lines (item_id, item_order_id and item_qty rounded to an
/// integer quantity from schema_with_foreign_keys.order_items): item_id, asDecimal of
/// quantity, previous of asDecimal of quantity within item_order_id ordered by item_id
/// with the decimal default 0.5, nullIfEqual of asDecimal of quantity and 1, and
/// asNullable of item_order_id and of quantity, ordered by item_id.
/// </summary>
public sealed record ExplicitWideningQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithoutPagination(
            subqueries:
            [
                new SubqueryQuery(
                    "lines",
                    new PlainQueryWithoutPaginationWithoutOrderBy(
                        from: new FromEntity(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrderItemsTable().Name,
                                ]
                            ).TextValue
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
                                "item_order_id",
                                new FieldUuid(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new OrderItemsTable().Name,
                                        ]
                                    ).TextValue,
                                    new ItemOrderIdColumn().Name.TextValue
                                )
                            ),
                            new SelectItemProjection(
                                "quantity",
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
                                )
                            ),
                        ]
                    )
                ),
            ],
            from: new FromSubquery("lines"),
            select:
            [
                new SelectItemProjection("item_id", new FieldUuid("lines", "item_id")),
                new SelectItemProjection(
                    "quantity",
                    new AsDecimalIntegerDecimalProjection(
                        new FieldInteger("lines", "quantity")
                    )
                ),
                new SelectItemProjection(
                    "previous_quantity_or_half",
                    new PreviousDecimalProjection(
                        new AsDecimalIntegerDecimalRow(
                            new FieldInteger("lines", "quantity")
                        ),
                        new LiteralDecimal(0.5m),
                        new OrderedWindow(
                            [new OrderItemRow(new FieldUuid("lines", "item_id"))],
                            [new FieldUuid("lines", "item_order_id")]
                        )
                    )
                ),
                new SelectItemProjection(
                    "quantity_unless_one",
                    new NullIfEqualDecimalNullableProjection(
                        new AsDecimalIntegerDecimalProjection(
                            new FieldInteger("lines", "quantity")
                        ),
                        new LiteralDecimal(1m)
                    )
                ),
                new SelectItemProjection(
                    "optional_order_id",
                    new AsNullableUuidNullableProjection(
                        new FieldUuid("lines", "item_order_id")
                    )
                ),
                new SelectItemProjection(
                    "optional_quantity",
                    new AsNullableIntegerNullableProjection(
                        new FieldInteger("lines", "quantity")
                    )
                ),
            ],
            orderBy:
            [
                new OrderItemProjectionExpression(new FieldUuid("lines", "item_id")),
            ]
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
                    new ItemIdColumn(),
                    new Column(new String("quantity"), new DoubleColumnType()),
                    new Column(
                        new String("previous_quantity_or_half"),
                        new DoubleColumnType()
                    ),
                    new Column(new String("quantity_unless_one"), new DoubleColumnType()),
                    new Column(new String("optional_order_id"), new UuidColumnType()),
                    new Column(new String("optional_quantity"), new LongColumnType()),
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
                                    new String("quantity"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("previous_quantity_or_half"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("quantity_unless_one"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("optional_order_id"),
                                    new UuidColumnType()
                                ),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000065-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("optional_quantity"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
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
                                    new String("quantity"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("previous_quantity_or_half"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("quantity_unless_one"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("optional_order_id"),
                                    new UuidColumnType()
                                ),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000065-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("optional_quantity"),
                                    new LongColumnType()
                                ),
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
                                    new String("quantity"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("previous_quantity_or_half"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("quantity_unless_one"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("optional_order_id"),
                                    new UuidColumnType()
                                ),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000067-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("optional_quantity"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(5))
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
                                    new String("quantity"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("previous_quantity_or_half"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("quantity_unless_one"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("optional_order_id"),
                                    new UuidColumnType()
                                ),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000069-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("optional_quantity"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(3))
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
