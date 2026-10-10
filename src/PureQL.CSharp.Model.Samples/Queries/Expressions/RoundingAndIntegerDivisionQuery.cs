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
using PureQL.CSharp.Model.SelectItems;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Expressions;

/// <summary>
/// Selects product_price floored, ceiled and rounded, product_price rounded to 1 digit,
/// and the rounded product_price integer-divided by 6 and modulo 6 from
/// schema_with_foreign_keys.products.
/// </summary>
public sealed record RoundingAndIntegerDivisionQuery
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
                                new ProductsTable().Name,
                            ]
                        ).TextValue
                    )
                ),
                [
                    new SelectItemProjection(
                        new SelectItemProjectionNonNullable(
                            new SelectItemProjectionInteger(
                                "price_floor",
                                new IntegerProjection(
                                    new RoundingIntegerProjection(
                                        new FloorIntegerProjection(
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
                                            )
                                        )
                                    )
                                )
                            )
                        )
                    ),
                    new SelectItemProjection(
                        new SelectItemProjectionNonNullable(
                            new SelectItemProjectionInteger(
                                "price_ceiling",
                                new IntegerProjection(
                                    new RoundingIntegerProjection(
                                        new CeilingIntegerProjection(
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
                                            )
                                        )
                                    )
                                )
                            )
                        )
                    ),
                    new SelectItemProjection(
                        new SelectItemProjectionNonNullable(
                            new SelectItemProjectionInteger(
                                "price_rounded",
                                new IntegerProjection(
                                    new RoundingIntegerProjection(
                                        new RoundIntegerProjection(
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
                                            )
                                        )
                                    )
                                )
                            )
                        )
                    ),
                    new SelectItemProjection(
                        new SelectItemProjectionNonNullable(
                            new SelectItemProjectionDecimal(
                                "price_tenths",
                                new DecimalProjection(
                                    new RoundingDecimalProjection(
                                        new RoundDecimalDigitsProjection(
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
                                            new IntegerProjection(new LiteralInteger(1))
                                        )
                                    )
                                )
                            )
                        )
                    ),
                    new SelectItemProjection(
                        new SelectItemProjectionNonNullable(
                            new SelectItemProjectionInteger(
                                "packs",
                                new IntegerProjection(
                                    new ArithmeticIntegerProjection(
                                        new IntegerDivideIntegerProjection(
                                            new IntegerProjection(
                                                new RoundingIntegerProjection(
                                                    new RoundIntegerProjection(
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
                                                        )
                                                    )
                                                )
                                            ),
                                            new IntegerProjection(new LiteralInteger(6))
                                        )
                                    )
                                )
                            )
                        )
                    ),
                    new SelectItemProjection(
                        new SelectItemProjectionNonNullable(
                            new SelectItemProjectionInteger(
                                "loose",
                                new IntegerProjection(
                                    new ArithmeticIntegerProjection(
                                        new ModuloIntegerProjection(
                                            new IntegerProjection(
                                                new RoundingIntegerProjection(
                                                    new RoundIntegerProjection(
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
                                                        )
                                                    )
                                                )
                                            ),
                                            new IntegerProjection(new LiteralInteger(6))
                                        )
                                    )
                                )
                            )
                        )
                    ),
                ]
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
                    new Column(new String("price_floor"), new LongColumnType()),
                    new Column(new String("price_ceiling"), new LongColumnType()),
                    new Column(new String("price_rounded"), new LongColumnType()),
                    new Column(new String("price_tenths"), new DoubleColumnType()),
                    new Column(new String("packs"), new LongColumnType()),
                    new Column(new String("loose"), new LongColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("price_floor"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(9))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("price_ceiling"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(10))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("price_rounded"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(10))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("price_tenths"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(10))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("packs"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("loose"), new LongColumnType()),
                                new InvariantCell(new Long(4))
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
                                    new String("price_floor"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(19))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("price_ceiling"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(20))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("price_rounded"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(20))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("price_tenths"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(20))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("packs"), new LongColumnType()),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("loose"), new LongColumnType()),
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
                                new Column(
                                    new String("price_floor"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(4))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("price_ceiling"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("price_rounded"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("price_tenths"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(4.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("packs"), new LongColumnType()),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("loose"), new LongColumnType()),
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
                                new Column(
                                    new String("price_floor"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(250))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("price_ceiling"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(250))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("price_rounded"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(250))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("price_tenths"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(250))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("packs"), new LongColumnType()),
                                new InvariantCell(new Long(41))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("loose"), new LongColumnType()),
                                new InvariantCell(new Long(4))
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
