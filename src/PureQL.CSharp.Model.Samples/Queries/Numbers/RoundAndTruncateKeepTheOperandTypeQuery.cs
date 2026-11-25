using Pure.Collections.Generic;
using Pure.Primitives.Number;
using Pure.Primitives.String;
using Pure.RelationalSchema.Abstractions.Column;
using Pure.RelationalSchema.ColumnType;
using Pure.RelationalSchema.HashCodes;
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
/// Selects from an inline rows subquery lines holding the id, quantity, returned_quantity
/// and unit_price of five lines, without a filter or an order, round and truncate side by
/// side where an integer stays an integer and a decimal a decimal: integer_to_tens
/// (quantity rounded to tens) and decimal_to_tens (unit_price), integer_to_two_places and
/// decimal_to_two_places (round with 2 digits), integer_truncated and decimal_truncated
/// (truncate to hundreds), widened_first (asDecimal of the quantity rounded to tens),
/// of_an_integer_sum (the sum of quantity and returned_quantity rounded to tens),
/// of_a_mixed_sum (the sum of quantity and unit_price), of_a_quotient (quantity divided
/// by returned_quantity rounded to 0 digits), of_an_integer_quotient (quantity
/// integer-divided by 3 rounded to tens), of_a_literal_integer (2750 rounded to
/// hundreds), of_a_literal_decimal (0.15 rounded to 1 digit), of_a_literal_truncated
/// (1299 truncated to hundreds), half_even (quantity rounded to tens with mode halfEven),
/// nullable_operand (a null integer rounded to tens), nullable_digits (quantity truncated
/// with asNullable of -1 digits), nullable_decimal (a null decimal rounded to 2 digits),
/// rounded_twice (the quantity rounded to tens, then to hundreds) and
/// without_digits_of_a_decimal (unit_price rounded without digits, an integer).
/// </summary>
public sealed record RoundAndTruncateKeepTheOperandTypeQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithoutPagination(
            subqueries:
            [
                new SubqueryRows(
                    "lines",
                    [
                        [
                            new RowItem(
                                "id",
                                new LiteralUuid(new Guid(831, 0, 0, new byte[8]))
                            ),
                            new RowItem("quantity", new LiteralInteger(4)),
                            new RowItem("returned_quantity", new LiteralInteger(1)),
                            new RowItem("unit_price", new LiteralDecimal(9.99m)),
                        ],
                        [
                            new RowItem(
                                "id",
                                new LiteralUuid(new Guid(832, 0, 0, new byte[8]))
                            ),
                            new RowItem("quantity", new LiteralInteger(25)),
                            new RowItem("returned_quantity", new LiteralInteger(2)),
                            new RowItem("unit_price", new LiteralDecimal(19.99m)),
                        ],
                        [
                            new RowItem(
                                "id",
                                new LiteralUuid(new Guid(833, 0, 0, new byte[8]))
                            ),
                            new RowItem("quantity", new LiteralInteger(15)),
                            new RowItem("returned_quantity", new LiteralInteger(3)),
                            new RowItem("unit_price", new LiteralDecimal(4.5m)),
                        ],
                        [
                            new RowItem(
                                "id",
                                new LiteralUuid(new Guid(834, 0, 0, new byte[8]))
                            ),
                            new RowItem("quantity", new LiteralInteger(149)),
                            new RowItem("returned_quantity", new LiteralInteger(9)),
                            new RowItem("unit_price", new LiteralDecimal(250m)),
                        ],
                        [
                            new RowItem(
                                "id",
                                new LiteralUuid(new Guid(835, 0, 0, new byte[8]))
                            ),
                            new RowItem("quantity", new LiteralInteger(260)),
                            new RowItem("returned_quantity", new LiteralInteger(5)),
                            new RowItem("unit_price", new LiteralDecimal(1234.5678m)),
                        ],
                    ]
                ),
            ],
            from: new FromSubquery("lines"),
            select:
            [
                new SelectItemProjection(
                    "integer_to_tens",
                    new RoundWithDigitsIntegerProjection(
                        new FieldInteger("lines", "quantity"),
                        new LiteralInteger(-1)
                    )
                ),
                new SelectItemProjection(
                    "decimal_to_tens",
                    new RoundDecimalProjection(
                        new FieldDecimal("lines", "unit_price"),
                        new LiteralInteger(-1)
                    )
                ),
                new SelectItemProjection(
                    "integer_to_two_places",
                    new RoundWithDigitsIntegerProjection(
                        new FieldInteger("lines", "quantity"),
                        new LiteralInteger(2)
                    )
                ),
                new SelectItemProjection(
                    "decimal_to_two_places",
                    new RoundDecimalProjection(
                        new FieldDecimal("lines", "unit_price"),
                        new LiteralInteger(2)
                    )
                ),
                new SelectItemProjection(
                    "integer_truncated",
                    new TruncateWithDigitsIntegerProjection(
                        new FieldInteger("lines", "quantity"),
                        new LiteralInteger(-2)
                    )
                ),
                new SelectItemProjection(
                    "decimal_truncated",
                    new TruncateDecimalProjection(
                        new FieldDecimal("lines", "unit_price"),
                        new LiteralInteger(-2)
                    )
                ),
                new SelectItemProjection(
                    "widened_first",
                    new RoundDecimalProjection(
                        new AsDecimalIntegerDecimalProjection(
                            new FieldInteger("lines", "quantity")
                        ),
                        new LiteralInteger(-1)
                    )
                ),
                new SelectItemProjection(
                    "of_an_integer_sum",
                    new RoundWithDigitsIntegerProjection(
                        new AddIntegerProjection([
                            new FieldInteger("lines", "quantity"),
                            new FieldInteger("lines", "returned_quantity"),
                        ]),
                        new LiteralInteger(-1)
                    )
                ),
                new SelectItemProjection(
                    "of_a_mixed_sum",
                    new RoundDecimalProjection(
                        new AddDecimalProjection([
                            new FieldInteger("lines", "quantity"),
                            new FieldDecimal("lines", "unit_price"),
                        ]),
                        new LiteralInteger(-1)
                    )
                ),
                new SelectItemProjection(
                    "of_a_quotient",
                    new RoundDecimalProjection(
                        new DivideDecimalProjection([
                            new FieldInteger("lines", "quantity"),
                            new FieldInteger("lines", "returned_quantity"),
                        ]),
                        new LiteralInteger(0)
                    )
                ),
                new SelectItemProjection(
                    "of_an_integer_quotient",
                    new RoundWithDigitsIntegerProjection(
                        new IntegerDivideIntegerProjection(
                            new FieldInteger("lines", "quantity"),
                            new LiteralInteger(3)
                        ),
                        new LiteralInteger(-1)
                    )
                ),
                new SelectItemProjection(
                    "of_a_literal_integer",
                    new RoundWithDigitsIntegerProjection(
                        new LiteralInteger(2750),
                        new LiteralInteger(-2)
                    )
                ),
                new SelectItemProjection(
                    "of_a_literal_decimal",
                    new RoundDecimalProjection(
                        new LiteralDecimal(0.15m),
                        new LiteralInteger(1)
                    )
                ),
                new SelectItemProjection(
                    "of_a_literal_truncated",
                    new TruncateWithDigitsIntegerProjection(
                        new LiteralInteger(1299),
                        new LiteralInteger(-2)
                    )
                ),
                new SelectItemProjection(
                    "half_even",
                    new RoundWithDigitsIntegerProjection(
                        new FieldInteger("lines", "quantity"),
                        new LiteralInteger(-1),
                        mode: RoundingMode.HalfEven
                    )
                ),
                new SelectItemProjection(
                    "nullable_operand",
                    new RoundWithDigitsIntegerNullableProjection(
                        new LiteralIntegerNullable(),
                        new LiteralInteger(-1)
                    )
                ),
                new SelectItemProjection(
                    "nullable_digits",
                    new TruncateWithDigitsIntegerNullableProjection(
                        new FieldInteger("lines", "quantity"),
                        new AsNullableIntegerNullableProjection(new LiteralInteger(-1))
                    )
                ),
                new SelectItemProjection(
                    "nullable_decimal",
                    new RoundDecimalNullableProjection(
                        new LiteralDecimalNullable(),
                        new LiteralInteger(2)
                    )
                ),
                new SelectItemProjection(
                    "rounded_twice",
                    new RoundWithDigitsIntegerProjection(
                        new RoundWithDigitsIntegerProjection(
                            new FieldInteger("lines", "quantity"),
                            new LiteralInteger(-1)
                        ),
                        new LiteralInteger(-2)
                    )
                ),
                new SelectItemProjection(
                    "without_digits_of_a_decimal",
                    new RoundWithoutDigitsIntegerProjection(
                        new FieldDecimal("lines", "unit_price")
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
                    new Column(new String("integer_to_tens"), new LongColumnType()),
                    new Column(new String("decimal_to_tens"), new DoubleColumnType()),
                    new Column(new String("integer_to_two_places"), new LongColumnType()),
                    new Column(
                        new String("decimal_to_two_places"),
                        new DoubleColumnType()
                    ),
                    new Column(new String("integer_truncated"), new LongColumnType()),
                    new Column(new String("decimal_truncated"), new DoubleColumnType()),
                    new Column(new String("widened_first"), new DoubleColumnType()),
                    new Column(new String("of_an_integer_sum"), new LongColumnType()),
                    new Column(new String("of_a_mixed_sum"), new DoubleColumnType()),
                    new Column(new String("of_a_quotient"), new DoubleColumnType()),
                    new Column(
                        new String("of_an_integer_quotient"),
                        new LongColumnType()
                    ),
                    new Column(new String("of_a_literal_integer"), new LongColumnType()),
                    new Column(
                        new String("of_a_literal_decimal"),
                        new DoubleColumnType()
                    ),
                    new Column(
                        new String("of_a_literal_truncated"),
                        new LongColumnType()
                    ),
                    new Column(new String("half_even"), new LongColumnType()),
                    new Column(new String("nullable_operand"), new LongColumnType()),
                    new Column(new String("nullable_digits"), new LongColumnType()),
                    new Column(new String("nullable_decimal"), new DoubleColumnType()),
                    new Column(new String("rounded_twice"), new LongColumnType()),
                    new Column(
                        new String("without_digits_of_a_decimal"),
                        new LongColumnType()
                    ),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("integer_to_tens"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_to_tens"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(10.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("integer_to_two_places"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(4))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_to_two_places"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(9.99))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("integer_truncated"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_truncated"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("widened_first"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_an_integer_sum"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(10))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_mixed_sum"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(10.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_quotient"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(4.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_an_integer_quotient"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_literal_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2800))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_literal_decimal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_literal_truncated"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1200))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("half_even"), new LongColumnType()),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_operand"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_digits"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_decimal"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rounded_twice"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("without_digits_of_a_decimal"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(10))
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
                                    new String("integer_to_tens"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(30))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_to_tens"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(20.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("integer_to_two_places"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(25))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_to_two_places"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(19.99))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("integer_truncated"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_truncated"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("widened_first"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(30.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_an_integer_sum"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(30))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_mixed_sum"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(40.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_quotient"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(13.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_an_integer_quotient"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(10))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_literal_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2800))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_literal_decimal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_literal_truncated"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1200))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("half_even"), new LongColumnType()),
                                new InvariantCell(new Long(20))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_operand"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_digits"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(20))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_decimal"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rounded_twice"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("without_digits_of_a_decimal"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(20))
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
                                    new String("integer_to_tens"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(20))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_to_tens"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("integer_to_two_places"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(15))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_to_two_places"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(4.5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("integer_truncated"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_truncated"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("widened_first"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(20.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_an_integer_sum"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(20))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_mixed_sum"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(20.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_quotient"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(5.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_an_integer_quotient"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(10))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_literal_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2800))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_literal_decimal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_literal_truncated"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1200))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("half_even"), new LongColumnType()),
                                new InvariantCell(new Long(20))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_operand"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_digits"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(10))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_decimal"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rounded_twice"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("without_digits_of_a_decimal"),
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
                                new Column(
                                    new String("integer_to_tens"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(150))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_to_tens"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(250.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("integer_to_two_places"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(149))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_to_two_places"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(250.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("integer_truncated"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(100))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_truncated"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(200.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("widened_first"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(150.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_an_integer_sum"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(160))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_mixed_sum"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(400.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_quotient"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(17.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_an_integer_quotient"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(50))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_literal_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2800))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_literal_decimal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_literal_truncated"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1200))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("half_even"), new LongColumnType()),
                                new InvariantCell(new Long(150))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_operand"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_digits"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(140))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_decimal"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rounded_twice"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(200))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("without_digits_of_a_decimal"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(250))
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
                                    new String("integer_to_tens"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(260))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_to_tens"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1230.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("integer_to_two_places"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(260))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_to_two_places"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1234.57))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("integer_truncated"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(200))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("decimal_truncated"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1200.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("widened_first"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(260.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_an_integer_sum"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(270))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_mixed_sum"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(1490.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_quotient"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(52.0))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_an_integer_quotient"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(90))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_literal_integer"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(2800))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_literal_decimal"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(0.2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("of_a_literal_truncated"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1200))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("half_even"), new LongColumnType()),
                                new InvariantCell(new Long(260))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_operand"),
                                    new LongColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_digits"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(260))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("nullable_decimal"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("rounded_twice"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(300))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("without_digits_of_a_decimal"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1235))
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
