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
using PureQL.CSharp.Model.RowExpressions;
using PureQL.CSharp.Model.TotalExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Conversions;

/// <summary>
/// Aggregates schema_with_foreign_keys.order_items, with the quantity standing for an
/// integer column as item_qty times the integer 10 rounded to an integer, into one row:
/// the sum of the quantity widened with asDecimal as units, the count widened with
/// asDecimal as lines, the max of the quantity widened with asDecimal as largest, the
/// string 12.5 read with asDecimal as text_as_decimal, the string 42 read with asInteger
/// and widened with asDecimal as text_as_integer_as_decimal, the string 7.25 widened with
/// asNullable and read with tryAsDecimal as tried_text, a null string read with
/// tryAsDecimal as tried_missing_text, a null integer widened with asDecimal as
/// nullable_integer, the sum of the quantity widened with asDecimal and rounded to an
/// integer as rounded_again, and the count widened with asNullable as optional_lines.
/// </summary>
public sealed record AsDecimalOfIntegersAndStringsInATotalQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainTotalQuery(
            from: new FromEntity(
                new JoinedString(
                    new DotString(),
                    [
                        new RelationalSchemaWithForeignKeys().Name,
                        new OrderItemsTable().Name,
                    ]
                ).TextValue
            ),
            aggregate:
            [
                new SelectItemTotal(
                    "units",
                    new AsDecimalIntegerDecimalTotal(
                        new SumIntegerTotal(
                            new RoundWithoutDigitsIntegerRow(
                                new MultiplyDecimalRow([
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
                                    new LiteralInteger(10),
                                ])
                            )
                        )
                    )
                ),
                new SelectItemTotal(
                    "lines",
                    new AsDecimalIntegerDecimalTotal(new CountTotal())
                ),
                new SelectItemTotal(
                    "largest",
                    new AsDecimalIntegerDecimalNullableTotal(
                        new MaxIntegerNullableTotal(
                            new RoundWithoutDigitsIntegerRow(
                                new MultiplyDecimalRow([
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
                                    new LiteralInteger(10),
                                ])
                            )
                        )
                    )
                ),
                new SelectItemTotal(
                    "text_as_decimal",
                    new AsDecimalStringDecimalTotal(new LiteralString("12.5"))
                ),
                new SelectItemTotal(
                    "text_as_integer_as_decimal",
                    new AsDecimalIntegerDecimalTotal(
                        new AsIntegerIntegerTotal(new LiteralString("42"))
                    )
                ),
                new SelectItemTotal(
                    "tried_text",
                    new TryAsDecimalTotal(
                        new AsNullableStringNullableTotal(new LiteralString("7.25"))
                    )
                ),
                new SelectItemTotal(
                    "tried_missing_text",
                    new TryAsDecimalTotal(new LiteralStringNullable())
                ),
                new SelectItemTotal(
                    "nullable_integer",
                    new AsDecimalIntegerDecimalNullableTotal(new LiteralIntegerNullable())
                ),
                new SelectItemTotal(
                    "rounded_again",
                    new RoundWithoutDigitsIntegerTotal(
                        new AsDecimalIntegerDecimalTotal(
                            new SumIntegerTotal(
                                new RoundWithoutDigitsIntegerRow(
                                    new MultiplyDecimalRow([
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
                                        new LiteralInteger(10),
                                    ])
                                )
                            )
                        )
                    )
                ),
                new SelectItemTotal(
                    "optional_lines",
                    new AsNullableIntegerNullableTotal(new CountTotal())
                ),
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
                    new Column(new String("units"), new DoubleColumnType()),
                    new Column(new String("lines"), new DoubleColumnType()),
                    new Column(new String("largest"), new DoubleColumnType()),
                    new Column(new String("text_as_decimal"), new DoubleColumnType()),
                    new Column(
                        new String("text_as_integer_as_decimal"),
                        new DoubleColumnType()
                    ),
                    new Column(new String("tried_text"), new DoubleColumnType()),
                    new Column(new String("tried_missing_text"), new DoubleColumnType()),
                    new Column(new String("nullable_integer"), new DoubleColumnType()),
                    new Column(new String("rounded_again"), new LongColumnType()),
                    new Column(new String("optional_lines"), new LongColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("units"), new DoubleColumnType()),
                                new InvariantCell(new Double(110.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("lines"), new DoubleColumnType()),
                                new InvariantCell(new Double(4.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("largest"), new DoubleColumnType()),
                                new InvariantCell(new Double(50.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("text_as_decimal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(12.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("text_as_integer_as_decimal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(42.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("tried_text"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(7.25))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("tried_missing_text"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_integer"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rounded_again"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(110))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("optional_lines"),
                                    new LongColumnType()
                                ),
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
