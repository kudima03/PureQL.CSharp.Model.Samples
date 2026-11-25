using Pure.Collections.Generic;
using Pure.Primitives.Bool;
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
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Numbers;

/// <summary>
/// Selects from schema_with_foreign_keys.order_items inner joined with
/// schema_with_foreign_keys.products on item_product_id equal to product_id, with the
/// quantity standing for an integer column as item_qty times the integer 3 rounded to an
/// integer, the product_price modulo the integer 1 as fractional_part, the integer 0
/// minus product_price modulo the integer 2 as of_a_negative, product_price modulo the
/// decimal -2 as by_a_negative, product_price modulo the decimal 0.25 as by_a_step,
/// product_price modulo the decimal 0 null if equal to the integer 0 as guarded, the
/// quantity modulo the integer 12 as integers_stay_integer, the quantity modulo the
/// decimal 2.5 as integer_by_decimal, product_price modulo the quantity as
/// decimal_by_integer_field, a null decimal modulo the integer 1 as nullable,
/// product_price integer-divided by the decimal 0.25 as steps, the quantity
/// integer-divided by the decimal 2.5 as steps_of_an_integer, product_price
/// integer-divided by the decimal 0 null if equal to the integer 0 as guarded_steps,
/// product_price equal to the decimal 0.25 times steps plus product_price modulo the
/// decimal 0.25 as identity_holds, product_price integer-divided by the integer 1 as
/// whole_part, that equal to product_price truncated as same_as_truncate, and
/// product_price modulo the integer 10 modulo the decimal 0.25 as nested.
/// </summary>
public sealed record DecimalRemainderCornersQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithoutPagination(
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
            select:
            [
                new SelectItemProjection(
                    "fractional_part",
                    new ModuloDecimalProjection(
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
                        new LiteralInteger(1)
                    )
                ),
                new SelectItemProjection(
                    "of_a_negative",
                    new ModuloDecimalProjection(
                        new SubtractDecimalProjection([
                            new LiteralInteger(0),
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
                        ]),
                        new LiteralInteger(2)
                    )
                ),
                new SelectItemProjection(
                    "by_a_negative",
                    new ModuloDecimalProjection(
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
                        new LiteralDecimal(-2m)
                    )
                ),
                new SelectItemProjection(
                    "by_a_step",
                    new ModuloDecimalProjection(
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
                        new LiteralDecimal(0.25m)
                    )
                ),
                new SelectItemProjection(
                    "guarded",
                    new ModuloDecimalNullableProjection(
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
                        new NullIfEqualDecimalNullableProjection(
                            new LiteralDecimal(0m),
                            new LiteralInteger(0)
                        )
                    )
                ),
                new SelectItemProjection(
                    "integers_stay_integer",
                    new ModuloIntegerProjection(
                        new RoundWithoutDigitsIntegerProjection(
                            new MultiplyDecimalProjection([
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
                                new LiteralInteger(3),
                            ])
                        ),
                        new LiteralInteger(12)
                    )
                ),
                new SelectItemProjection(
                    "integer_by_decimal",
                    new ModuloDecimalProjection(
                        new RoundWithoutDigitsIntegerProjection(
                            new MultiplyDecimalProjection([
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
                                new LiteralInteger(3),
                            ])
                        ),
                        new LiteralDecimal(2.5m)
                    )
                ),
                new SelectItemProjection(
                    "decimal_by_integer_field",
                    new ModuloDecimalProjection(
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
                        new RoundWithoutDigitsIntegerProjection(
                            new MultiplyDecimalProjection([
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
                                new LiteralInteger(3),
                            ])
                        )
                    )
                ),
                new SelectItemProjection(
                    "nullable",
                    new ModuloDecimalNullableProjection(
                        new LiteralDecimalNullable(),
                        new LiteralInteger(1)
                    )
                ),
                new SelectItemProjection(
                    "steps",
                    new IntegerDivideIntegerProjection(
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
                        new LiteralDecimal(0.25m)
                    )
                ),
                new SelectItemProjection(
                    "steps_of_an_integer",
                    new IntegerDivideIntegerProjection(
                        new RoundWithoutDigitsIntegerProjection(
                            new MultiplyDecimalProjection([
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
                                new LiteralInteger(3),
                            ])
                        ),
                        new LiteralDecimal(2.5m)
                    )
                ),
                new SelectItemProjection(
                    "guarded_steps",
                    new IntegerDivideIntegerNullableProjection(
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
                        new NullIfEqualDecimalNullableProjection(
                            new LiteralDecimal(0m),
                            new LiteralInteger(0)
                        )
                    )
                ),
                new SelectItemProjection(
                    "identity_holds",
                    new EqualDecimalProjection(
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
                        new AddDecimalProjection([
                            new MultiplyDecimalProjection([
                                new LiteralDecimal(0.25m),
                                new IntegerDivideIntegerProjection(
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
                                    new LiteralDecimal(0.25m)
                                ),
                            ]),
                            new ModuloDecimalProjection(
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
                                new LiteralDecimal(0.25m)
                            ),
                        ])
                    )
                ),
                new SelectItemProjection(
                    "whole_part",
                    new IntegerDivideIntegerProjection(
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
                        new LiteralInteger(1)
                    )
                ),
                new SelectItemProjection(
                    "same_as_truncate",
                    new EqualDecimalProjection(
                        new IntegerDivideIntegerProjection(
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
                            new LiteralInteger(1)
                        ),
                        new TruncateWithoutDigitsIntegerProjection(
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
                    )
                ),
                new SelectItemProjection(
                    "nested",
                    new ModuloDecimalProjection(
                        new ModuloDecimalProjection(
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
                            new LiteralInteger(10)
                        ),
                        new LiteralDecimal(0.25m)
                    )
                ),
            ]
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
                    new Column(new String("fractional_part"), new DoubleColumnType()),
                    new Column(new String("of_a_negative"), new DoubleColumnType()),
                    new Column(new String("by_a_negative"), new DoubleColumnType()),
                    new Column(new String("by_a_step"), new DoubleColumnType()),
                    new Column(new String("guarded"), new DoubleColumnType()),
                    new Column(new String("integers_stay_integer"), new LongColumnType()),
                    new Column(new String("integer_by_decimal"), new DoubleColumnType()),
                    new Column(
                        new String("decimal_by_integer_field"),
                        new DoubleColumnType()
                    ),
                    new Column(new String("nullable"), new DoubleColumnType()),
                    new Column(new String("steps"), new LongColumnType()),
                    new Column(new String("steps_of_an_integer"), new LongColumnType()),
                    new Column(new String("guarded_steps"), new LongColumnType()),
                    new Column(new String("identity_holds"), new BoolColumnType()),
                    new Column(new String("whole_part"), new LongColumnType()),
                    new Column(new String("same_as_truncate"), new BoolColumnType()),
                    new Column(new String("nested"), new DoubleColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("fractional_part"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.99))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_negative"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-1.99))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("by_a_negative"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1.99))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("by_a_step"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.24))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("guarded"), new DoubleColumnType()),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("integers_stay_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(6))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("integer_by_decimal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_by_integer_field"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(3.99))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("steps"), new LongColumnType()),
                                new InvariantCell(new Long(39))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("steps_of_an_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("guarded_steps"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("identity_holds"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("whole_part"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(9))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("same_as_truncate"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("nested"), new DoubleColumnType()),
                                new InvariantCell(new Double(0.24))
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
                                    new String("fractional_part"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.99))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_negative"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-1.99))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("by_a_negative"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1.99))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("by_a_step"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.24))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("guarded"), new DoubleColumnType()),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("integers_stay_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("integer_by_decimal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_by_integer_field"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1.99))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("steps"), new LongColumnType()),
                                new InvariantCell(new Long(79))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("steps_of_an_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("guarded_steps"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("identity_holds"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("whole_part"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(19))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("same_as_truncate"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("nested"), new DoubleColumnType()),
                                new InvariantCell(new Double(0.24))
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
                                    new String("fractional_part"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_negative"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-0.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("by_a_negative"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("by_a_step"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("guarded"), new DoubleColumnType()),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("integers_stay_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("integer_by_decimal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_by_integer_field"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(4.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("steps"), new LongColumnType()),
                                new InvariantCell(new Long(18))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("steps_of_an_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(6))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("guarded_steps"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("identity_holds"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("whole_part"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(4))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("same_as_truncate"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("nested"), new DoubleColumnType()),
                                new InvariantCell(new Double(0.0))
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
                                    new String("fractional_part"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.99))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_negative"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(-1.99))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("by_a_negative"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1.99))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("by_a_step"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.24))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("guarded"), new DoubleColumnType()),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("integers_stay_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(9))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("integer_by_decimal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_by_integer_field"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.99))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("steps"), new LongColumnType()),
                                new InvariantCell(new Long(39))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("steps_of_an_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("guarded_steps"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("identity_holds"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("whole_part"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(9))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("same_as_truncate"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("nested"), new DoubleColumnType()),
                                new InvariantCell(new Double(0.24))
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
