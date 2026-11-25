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
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Numbers;

/// <summary>
/// Selects from schema_with_foreign_keys.orders order_total as rounded_total: order_total
/// plus the decimal 2.5, rounded to -1 digits, plus 2.5, rounded to -1 digits, and so on,
/// round of add nested thirty levels deep.
/// </summary>
public sealed record RoundOfAddNestedThirtyLevelsDeepQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithoutPagination(
            from: new FromEntity(
                new JoinedString(
                    new DotString(),
                    [new RelationalSchemaWithForeignKeys().Name, new OrdersTable().Name]
                ).TextValue
            ),
            select:
            [
                new SelectItemProjection(
                    "rounded_total",
                    new RoundDecimalProjection(
                        new AddDecimalProjection([
                            new RoundDecimalProjection(
                                new AddDecimalProjection([
                                    new RoundDecimalProjection(
                                        new AddDecimalProjection([
                                            new RoundDecimalProjection(
                                                new AddDecimalProjection([
                                                    new RoundDecimalProjection(
                                                        new AddDecimalProjection([
                                                            new RoundDecimalProjection(
                                                                new AddDecimalProjection([
                                                                    new RoundDecimalProjection(
                                                                        new AddDecimalProjection([
                                                                            new RoundDecimalProjection(
                                                                                new AddDecimalProjection([
                                                                                    new RoundDecimalProjection(
                                                                                        new AddDecimalProjection([
                                                                                            new RoundDecimalProjection(
                                                                                                new AddDecimalProjection([
                                                                                                    new RoundDecimalProjection(
                                                                                                        new AddDecimalProjection([
                                                                                                            new RoundDecimalProjection(
                                                                                                                new AddDecimalProjection([
                                                                                                                    new RoundDecimalProjection(
                                                                                                                        new AddDecimalProjection([
                                                                                                                            new RoundDecimalProjection(
                                                                                                                                new AddDecimalProjection([
                                                                                                                                    new RoundDecimalProjection(
                                                                                                                                        new AddDecimalProjection([
                                                                                                                                            new RoundDecimalProjection(
                                                                                                                                                new AddDecimalProjection([
                                                                                                                                                    new RoundDecimalProjection(
                                                                                                                                                        new AddDecimalProjection([
                                                                                                                                                            new RoundDecimalProjection(
                                                                                                                                                                new AddDecimalProjection([
                                                                                                                                                                    new RoundDecimalProjection(
                                                                                                                                                                        new AddDecimalProjection([
                                                                                                                                                                            new RoundDecimalProjection(
                                                                                                                                                                                new AddDecimalProjection([
                                                                                                                                                                                    new RoundDecimalProjection(
                                                                                                                                                                                        new AddDecimalProjection([
                                                                                                                                                                                            new RoundDecimalProjection(
                                                                                                                                                                                                new AddDecimalProjection([
                                                                                                                                                                                                    new RoundDecimalProjection(
                                                                                                                                                                                                        new AddDecimalProjection([
                                                                                                                                                                                                            new RoundDecimalProjection(
                                                                                                                                                                                                                new AddDecimalProjection([
                                                                                                                                                                                                                    new RoundDecimalProjection(
                                                                                                                                                                                                                        new AddDecimalProjection([
                                                                                                                                                                                                                            new RoundDecimalProjection(
                                                                                                                                                                                                                                new AddDecimalProjection([
                                                                                                                                                                                                                                    new RoundDecimalProjection(
                                                                                                                                                                                                                                        new AddDecimalProjection([
                                                                                                                                                                                                                                            new RoundDecimalProjection(
                                                                                                                                                                                                                                                new AddDecimalProjection([
                                                                                                                                                                                                                                                    new RoundDecimalProjection(
                                                                                                                                                                                                                                                        new AddDecimalProjection([
                                                                                                                                                                                                                                                            new RoundDecimalProjection(
                                                                                                                                                                                                                                                                new AddDecimalProjection([
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
                                                                                                                                                                                                                                                                    ),
                                                                                                                                                                                                                                                                    new LiteralDecimal(
                                                                                                                                                                                                                                                                        2.5m
                                                                                                                                                                                                                                                                    ),
                                                                                                                                                                                                                                                                ]),
                                                                                                                                                                                                                                                                new LiteralInteger(
                                                                                                                                                                                                                                                                    -1
                                                                                                                                                                                                                                                                )
                                                                                                                                                                                                                                                            ),
                                                                                                                                                                                                                                                            new LiteralDecimal(
                                                                                                                                                                                                                                                                2.5m
                                                                                                                                                                                                                                                            ),
                                                                                                                                                                                                                                                        ]),
                                                                                                                                                                                                                                                        new LiteralInteger(
                                                                                                                                                                                                                                                            -1
                                                                                                                                                                                                                                                        )
                                                                                                                                                                                                                                                    ),
                                                                                                                                                                                                                                                    new LiteralDecimal(
                                                                                                                                                                                                                                                        2.5m
                                                                                                                                                                                                                                                    ),
                                                                                                                                                                                                                                                ]),
                                                                                                                                                                                                                                                new LiteralInteger(
                                                                                                                                                                                                                                                    -1
                                                                                                                                                                                                                                                )
                                                                                                                                                                                                                                            ),
                                                                                                                                                                                                                                            new LiteralDecimal(
                                                                                                                                                                                                                                                2.5m
                                                                                                                                                                                                                                            ),
                                                                                                                                                                                                                                        ]),
                                                                                                                                                                                                                                        new LiteralInteger(
                                                                                                                                                                                                                                            -1
                                                                                                                                                                                                                                        )
                                                                                                                                                                                                                                    ),
                                                                                                                                                                                                                                    new LiteralDecimal(
                                                                                                                                                                                                                                        2.5m
                                                                                                                                                                                                                                    ),
                                                                                                                                                                                                                                ]),
                                                                                                                                                                                                                                new LiteralInteger(
                                                                                                                                                                                                                                    -1
                                                                                                                                                                                                                                )
                                                                                                                                                                                                                            ),
                                                                                                                                                                                                                            new LiteralDecimal(
                                                                                                                                                                                                                                2.5m
                                                                                                                                                                                                                            ),
                                                                                                                                                                                                                        ]),
                                                                                                                                                                                                                        new LiteralInteger(
                                                                                                                                                                                                                            -1
                                                                                                                                                                                                                        )
                                                                                                                                                                                                                    ),
                                                                                                                                                                                                                    new LiteralDecimal(
                                                                                                                                                                                                                        2.5m
                                                                                                                                                                                                                    ),
                                                                                                                                                                                                                ]),
                                                                                                                                                                                                                new LiteralInteger(
                                                                                                                                                                                                                    -1
                                                                                                                                                                                                                )
                                                                                                                                                                                                            ),
                                                                                                                                                                                                            new LiteralDecimal(
                                                                                                                                                                                                                2.5m
                                                                                                                                                                                                            ),
                                                                                                                                                                                                        ]),
                                                                                                                                                                                                        new LiteralInteger(
                                                                                                                                                                                                            -1
                                                                                                                                                                                                        )
                                                                                                                                                                                                    ),
                                                                                                                                                                                                    new LiteralDecimal(
                                                                                                                                                                                                        2.5m
                                                                                                                                                                                                    ),
                                                                                                                                                                                                ]),
                                                                                                                                                                                                new LiteralInteger(
                                                                                                                                                                                                    -1
                                                                                                                                                                                                )
                                                                                                                                                                                            ),
                                                                                                                                                                                            new LiteralDecimal(
                                                                                                                                                                                                2.5m
                                                                                                                                                                                            ),
                                                                                                                                                                                        ]),
                                                                                                                                                                                        new LiteralInteger(
                                                                                                                                                                                            -1
                                                                                                                                                                                        )
                                                                                                                                                                                    ),
                                                                                                                                                                                    new LiteralDecimal(
                                                                                                                                                                                        2.5m
                                                                                                                                                                                    ),
                                                                                                                                                                                ]),
                                                                                                                                                                                new LiteralInteger(
                                                                                                                                                                                    -1
                                                                                                                                                                                )
                                                                                                                                                                            ),
                                                                                                                                                                            new LiteralDecimal(
                                                                                                                                                                                2.5m
                                                                                                                                                                            ),
                                                                                                                                                                        ]),
                                                                                                                                                                        new LiteralInteger(
                                                                                                                                                                            -1
                                                                                                                                                                        )
                                                                                                                                                                    ),
                                                                                                                                                                    new LiteralDecimal(
                                                                                                                                                                        2.5m
                                                                                                                                                                    ),
                                                                                                                                                                ]),
                                                                                                                                                                new LiteralInteger(
                                                                                                                                                                    -1
                                                                                                                                                                )
                                                                                                                                                            ),
                                                                                                                                                            new LiteralDecimal(
                                                                                                                                                                2.5m
                                                                                                                                                            ),
                                                                                                                                                        ]),
                                                                                                                                                        new LiteralInteger(
                                                                                                                                                            -1
                                                                                                                                                        )
                                                                                                                                                    ),
                                                                                                                                                    new LiteralDecimal(
                                                                                                                                                        2.5m
                                                                                                                                                    ),
                                                                                                                                                ]),
                                                                                                                                                new LiteralInteger(
                                                                                                                                                    -1
                                                                                                                                                )
                                                                                                                                            ),
                                                                                                                                            new LiteralDecimal(
                                                                                                                                                2.5m
                                                                                                                                            ),
                                                                                                                                        ]),
                                                                                                                                        new LiteralInteger(
                                                                                                                                            -1
                                                                                                                                        )
                                                                                                                                    ),
                                                                                                                                    new LiteralDecimal(
                                                                                                                                        2.5m
                                                                                                                                    ),
                                                                                                                                ]),
                                                                                                                                new LiteralInteger(
                                                                                                                                    -1
                                                                                                                                )
                                                                                                                            ),
                                                                                                                            new LiteralDecimal(
                                                                                                                                2.5m
                                                                                                                            ),
                                                                                                                        ]),
                                                                                                                        new LiteralInteger(
                                                                                                                            -1
                                                                                                                        )
                                                                                                                    ),
                                                                                                                    new LiteralDecimal(
                                                                                                                        2.5m
                                                                                                                    ),
                                                                                                                ]),
                                                                                                                new LiteralInteger(
                                                                                                                    -1
                                                                                                                )
                                                                                                            ),
                                                                                                            new LiteralDecimal(
                                                                                                                2.5m
                                                                                                            ),
                                                                                                        ]),
                                                                                                        new LiteralInteger(
                                                                                                            -1
                                                                                                        )
                                                                                                    ),
                                                                                                    new LiteralDecimal(
                                                                                                        2.5m
                                                                                                    ),
                                                                                                ]),
                                                                                                new LiteralInteger(
                                                                                                    -1
                                                                                                )
                                                                                            ),
                                                                                            new LiteralDecimal(
                                                                                                2.5m
                                                                                            ),
                                                                                        ]),
                                                                                        new LiteralInteger(
                                                                                            -1
                                                                                        )
                                                                                    ),
                                                                                    new LiteralDecimal(
                                                                                        2.5m
                                                                                    ),
                                                                                ]),
                                                                                new LiteralInteger(
                                                                                    -1
                                                                                )
                                                                            ),
                                                                            new LiteralDecimal(
                                                                                2.5m
                                                                            ),
                                                                        ]),
                                                                        new LiteralInteger(
                                                                            -1
                                                                        )
                                                                    ),
                                                                    new LiteralDecimal(
                                                                        2.5m
                                                                    ),
                                                                ]),
                                                                new LiteralInteger(-1)
                                                            ),
                                                            new LiteralDecimal(2.5m),
                                                        ]),
                                                        new LiteralInteger(-1)
                                                    ),
                                                    new LiteralDecimal(2.5m),
                                                ]),
                                                new LiteralInteger(-1)
                                            ),
                                            new LiteralDecimal(2.5m),
                                        ]),
                                        new LiteralInteger(-1)
                                    ),
                                    new LiteralDecimal(2.5m),
                                ]),
                                new LiteralInteger(-1)
                            ),
                            new LiteralDecimal(2.5m),
                        ]),
                        new LiteralInteger(-1)
                    )
                ),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over <see
    /// cref="SchemaDataSetWithForeignKeys"/> and <see cref="AuditSchemaDataSet"/>, in no
    /// particular order.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [new Column(new String("rounded_total"), new DoubleColumnType())],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rounded_total"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(100))
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
                                new Column(
                                    new String("rounded_total"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(50))
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
                                new Column(
                                    new String("rounded_total"),
                                    new DoubleColumnType()
                                ),
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
                                new Column(
                                    new String("rounded_total"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(80))
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
                                new Column(
                                    new String("rounded_total"),
                                    new DoubleColumnType()
                                ),
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
                                new Column(
                                    new String("rounded_total"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(100))
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
