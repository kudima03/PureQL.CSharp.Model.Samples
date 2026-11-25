using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
using PureQL.CSharp.Model.Lists;
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.Parameters;
using PureQL.CSharp.Model.ProjectionExpressions;

namespace PureQL.CSharp.Model.Samples.Queries.Expressions;

/// <summary>
/// Selects, from schema_with_foreign_keys.orders, 916 columns, each an operator over
/// literals, one per signature of every operator: the lifted operators with and without a
/// nullable operand, integer and decimal operands, if and coalesce for every type, equal,
/// notEqual, the four comparisons and in for every family, and every aggregate over a
/// literal selector, with and without a predicate and with a nullable selector, and the
/// window functions. Some operands are parameters, so the query has no result.
/// </summary>
public sealed record EveryOperatorSignatureInAPlainSelectQuery
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
                    "add_integer",
                    new AddIntegerProjection([
                        new LiteralInteger(7),
                        new LiteralInteger(-3),
                    ])
                ),
                new SelectItemProjection(
                    "add_integer_nullable",
                    new AddIntegerNullableProjection([
                        new LiteralInteger(7),
                        new LiteralIntegerNullable(),
                    ])
                ),
                new SelectItemProjection(
                    "add_decimal",
                    new AddDecimalProjection([
                        new LiteralInteger(7),
                        new LiteralDecimal(2.5m),
                    ])
                ),
                new SelectItemProjection(
                    "add_decimal_nullable",
                    new AddDecimalNullableProjection([
                        new LiteralDecimalNullable(),
                        new LiteralInteger(7),
                    ])
                ),
                new SelectItemProjection(
                    "subtract_integer",
                    new SubtractIntegerProjection([
                        new LiteralInteger(7),
                        new LiteralInteger(-3),
                    ])
                ),
                new SelectItemProjection(
                    "subtract_integer_nullable",
                    new SubtractIntegerNullableProjection([
                        new LiteralInteger(7),
                        new LiteralIntegerNullable(),
                    ])
                ),
                new SelectItemProjection(
                    "subtract_decimal",
                    new SubtractDecimalProjection([
                        new LiteralInteger(7),
                        new LiteralDecimal(2.5m),
                    ])
                ),
                new SelectItemProjection(
                    "subtract_decimal_nullable",
                    new SubtractDecimalNullableProjection([
                        new LiteralDecimalNullable(),
                        new LiteralInteger(7),
                    ])
                ),
                new SelectItemProjection(
                    "multiply_integer",
                    new MultiplyIntegerProjection([
                        new LiteralInteger(7),
                        new LiteralInteger(-3),
                    ])
                ),
                new SelectItemProjection(
                    "multiply_integer_nullable",
                    new MultiplyIntegerNullableProjection([
                        new LiteralInteger(7),
                        new LiteralIntegerNullable(),
                    ])
                ),
                new SelectItemProjection(
                    "multiply_decimal",
                    new MultiplyDecimalProjection([
                        new LiteralInteger(7),
                        new LiteralDecimal(2.5m),
                    ])
                ),
                new SelectItemProjection(
                    "multiply_decimal_nullable",
                    new MultiplyDecimalNullableProjection([
                        new LiteralDecimalNullable(),
                        new LiteralInteger(7),
                    ])
                ),
                new SelectItemProjection(
                    "divide_decimal",
                    new DivideDecimalProjection([
                        new LiteralInteger(7),
                        new LiteralDecimal(2.5m),
                    ])
                ),
                new SelectItemProjection(
                    "divide_decimal_nullable",
                    new DivideDecimalNullableProjection([
                        new LiteralDecimalNullable(),
                        new LiteralInteger(7),
                    ])
                ),
                new SelectItemProjection(
                    "integerDivide_integer",
                    new IntegerDivideIntegerProjection(
                        new LiteralInteger(7),
                        new LiteralInteger(-3)
                    )
                ),
                new SelectItemProjection(
                    "integerDivide_integer_nullable",
                    new IntegerDivideIntegerNullableProjection(
                        new LiteralIntegerNullable(),
                        new LiteralInteger(-3)
                    )
                ),
                new SelectItemProjection(
                    "modulo_integer",
                    new ModuloIntegerProjection(
                        new LiteralInteger(7),
                        new LiteralInteger(-3)
                    )
                ),
                new SelectItemProjection(
                    "modulo_integer_nullable",
                    new ModuloIntegerNullableProjection(
                        new LiteralIntegerNullable(),
                        new LiteralInteger(-3)
                    )
                ),
                new SelectItemProjection(
                    "modulo_decimal",
                    new ModuloDecimalProjection(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m)
                    )
                ),
                new SelectItemProjection(
                    "modulo_decimal_of_integer_and_decimal",
                    new ModuloDecimalProjection(
                        new LiteralInteger(7),
                        new LiteralDecimal(2.5m)
                    )
                ),
                new SelectItemProjection(
                    "modulo_decimal_of_decimal_and_integer",
                    new ModuloDecimalProjection(
                        new LiteralDecimal(2.5m),
                        new LiteralInteger(7)
                    )
                ),
                new SelectItemProjection(
                    "modulo_decimal_nullable",
                    new ModuloDecimalNullableProjection(
                        new LiteralDecimalNullable(),
                        new LiteralDecimal(0.1m)
                    )
                ),
                new SelectItemProjection(
                    "modulo_decimal_nullable_right",
                    new ModuloDecimalNullableProjection(
                        new LiteralDecimal(2.5m),
                        new LiteralIntegerNullable()
                    )
                ),
                new SelectItemProjection(
                    "integerDivide_integer_of_decimals",
                    new IntegerDivideIntegerProjection(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m)
                    )
                ),
                new SelectItemProjection(
                    "integerDivide_integer_of_integer_and_decimal",
                    new IntegerDivideIntegerProjection(
                        new LiteralInteger(7),
                        new LiteralDecimal(2.5m)
                    )
                ),
                new SelectItemProjection(
                    "integerDivide_integer_nullable_of_decimals",
                    new IntegerDivideIntegerNullableProjection(
                        new LiteralDecimalNullable(),
                        new LiteralDecimal(0.1m)
                    )
                ),
                new SelectItemProjection(
                    "floor_integer",
                    new FloorIntegerProjection(new LiteralDecimal(2.5m))
                ),
                new SelectItemProjection(
                    "floor_integer_nullable",
                    new FloorIntegerNullableProjection(new LiteralDecimalNullable())
                ),
                new SelectItemProjection(
                    "ceiling_integer",
                    new CeilingIntegerProjection(new LiteralDecimal(2.5m))
                ),
                new SelectItemProjection(
                    "ceiling_integer_nullable",
                    new CeilingIntegerNullableProjection(new LiteralDecimalNullable())
                ),
                new SelectItemProjection(
                    "round_integer",
                    new RoundWithoutDigitsIntegerProjection(new LiteralDecimal(2.5m))
                ),
                new SelectItemProjection(
                    "round_integer_nullable",
                    new RoundWithoutDigitsIntegerNullableProjection(
                        new LiteralDecimalNullable()
                    )
                ),
                new SelectItemProjection(
                    "round_decimal",
                    new RoundDecimalProjection(
                        new LiteralDecimal(2.5m),
                        new LiteralInteger(-3)
                    )
                ),
                new SelectItemProjection(
                    "round_decimal_nullable",
                    new RoundDecimalNullableProjection(
                        new LiteralDecimalNullable(),
                        new LiteralInteger(7)
                    )
                ),
                new SelectItemProjection(
                    "round_integer_half_even",
                    new RoundWithoutDigitsIntegerProjection(
                        new LiteralDecimal(2.5m),
                        mode: RoundingMode.HalfEven
                    )
                ),
                new SelectItemProjection(
                    "round_integer_nullable_half_even",
                    new RoundWithoutDigitsIntegerNullableProjection(
                        new LiteralDecimalNullable(),
                        mode: RoundingMode.HalfEven
                    )
                ),
                new SelectItemProjection(
                    "round_decimal_half_even",
                    new RoundDecimalProjection(
                        new LiteralDecimal(2.5m),
                        new LiteralInteger(2),
                        mode: RoundingMode.HalfEven
                    )
                ),
                new SelectItemProjection(
                    "round_decimal_nullable_half_even",
                    new RoundDecimalNullableProjection(
                        new LiteralDecimalNullable(),
                        new LiteralInteger(2),
                        mode: RoundingMode.HalfEven
                    )
                ),
                new SelectItemProjection(
                    "round_integer_of_integer_half_even",
                    new RoundWithDigitsIntegerProjection(
                        new LiteralInteger(7),
                        new LiteralInteger(-1),
                        mode: RoundingMode.HalfEven
                    )
                ),
                new SelectItemProjection(
                    "truncate_integer",
                    new TruncateWithoutDigitsIntegerProjection(new LiteralDecimal(2.5m))
                ),
                new SelectItemProjection(
                    "truncate_integer_nullable",
                    new TruncateWithoutDigitsIntegerNullableProjection(
                        new LiteralDecimalNullable()
                    )
                ),
                new SelectItemProjection(
                    "truncate_decimal",
                    new TruncateDecimalProjection(
                        new LiteralDecimal(2.5m),
                        new LiteralInteger(7)
                    )
                ),
                new SelectItemProjection(
                    "truncate_decimal_nullable",
                    new TruncateDecimalNullableProjection(
                        new LiteralDecimalNullable(),
                        new LiteralInteger(7)
                    )
                ),
                new SelectItemProjection(
                    "abs_integer",
                    new AbsIntegerProjection(new LiteralInteger(-3))
                ),
                new SelectItemProjection(
                    "abs_integer_nullable",
                    new AbsIntegerNullableProjection(new LiteralIntegerNullable())
                ),
                new SelectItemProjection(
                    "abs_decimal",
                    new AbsDecimalProjection(new LiteralDecimal(2.5m))
                ),
                new SelectItemProjection(
                    "abs_decimal_nullable",
                    new AbsDecimalNullableProjection(new LiteralDecimalNullable())
                ),
                new SelectItemProjection(
                    "sign_integer",
                    new SignIntegerProjection(new LiteralDecimal(2.5m))
                ),
                new SelectItemProjection(
                    "sign_integer_nullable",
                    new SignIntegerNullableProjection(new LiteralDecimalNullable())
                ),
                new SelectItemProjection(
                    "sign_integer_of_integer",
                    new SignIntegerProjection(new LiteralInteger(-3))
                ),
                new SelectItemProjection(
                    "power_decimal",
                    new PowerDecimalProjection(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m)
                    )
                ),
                new SelectItemProjection(
                    "power_decimal_nullable",
                    new PowerDecimalNullableProjection(
                        new LiteralDecimalNullable(),
                        new LiteralInteger(7)
                    )
                ),
                new SelectItemProjection(
                    "power_decimal_of_integers",
                    new PowerDecimalProjection(
                        new LiteralInteger(7),
                        new LiteralInteger(-3)
                    )
                ),
                new SelectItemProjection(
                    "sqrt_decimal",
                    new SqrtDecimalProjection(new LiteralDecimal(2.5m))
                ),
                new SelectItemProjection(
                    "sqrt_decimal_nullable",
                    new SqrtDecimalNullableProjection(new LiteralDecimalNullable())
                ),
                new SelectItemProjection(
                    "sqrt_decimal_of_integer",
                    new SqrtDecimalProjection(new LiteralInteger(7))
                ),
                new SelectItemProjection(
                    "exp_decimal",
                    new ExpDecimalProjection(new LiteralDecimal(2.5m))
                ),
                new SelectItemProjection(
                    "exp_decimal_nullable",
                    new ExpDecimalNullableProjection(new LiteralDecimalNullable())
                ),
                new SelectItemProjection(
                    "exp_decimal_of_integer",
                    new ExpDecimalProjection(new LiteralInteger(7))
                ),
                new SelectItemProjection(
                    "ln_decimal",
                    new LnDecimalProjection(new LiteralDecimal(2.5m))
                ),
                new SelectItemProjection(
                    "ln_decimal_nullable",
                    new LnDecimalNullableProjection(new LiteralDecimalNullable())
                ),
                new SelectItemProjection(
                    "ln_decimal_of_integer",
                    new LnDecimalProjection(new LiteralInteger(7))
                ),
                new SelectItemProjection(
                    "log10_decimal",
                    new Log10DecimalProjection(new LiteralDecimal(2.5m))
                ),
                new SelectItemProjection(
                    "log10_decimal_nullable",
                    new Log10DecimalNullableProjection(new LiteralDecimalNullable())
                ),
                new SelectItemProjection(
                    "log10_decimal_of_integer",
                    new Log10DecimalProjection(new LiteralInteger(7))
                ),
                new SelectItemProjection(
                    "log_decimal",
                    new LogDecimalProjection(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m)
                    )
                ),
                new SelectItemProjection(
                    "log_decimal_nullable",
                    new LogDecimalNullableProjection(
                        new LiteralDecimalNullable(),
                        new LiteralInteger(7)
                    )
                ),
                new SelectItemProjection(
                    "sin_decimal",
                    new SinDecimalProjection(new LiteralDecimal(0.5m))
                ),
                new SelectItemProjection(
                    "sin_decimal_of_integer",
                    new SinDecimalProjection(new LiteralInteger(1))
                ),
                new SelectItemProjection(
                    "sin_decimal_nullable",
                    new SinDecimalNullableProjection(new LiteralDecimalNullable())
                ),
                new SelectItemProjection(
                    "cos_decimal",
                    new CosDecimalProjection(new LiteralDecimal(0.5m))
                ),
                new SelectItemProjection(
                    "cos_decimal_of_integer",
                    new CosDecimalProjection(new LiteralInteger(1))
                ),
                new SelectItemProjection(
                    "cos_decimal_nullable",
                    new CosDecimalNullableProjection(new LiteralDecimalNullable())
                ),
                new SelectItemProjection(
                    "tan_decimal",
                    new TanDecimalProjection(new LiteralDecimal(0.5m))
                ),
                new SelectItemProjection(
                    "tan_decimal_of_integer",
                    new TanDecimalProjection(new LiteralInteger(1))
                ),
                new SelectItemProjection(
                    "tan_decimal_nullable",
                    new TanDecimalNullableProjection(new LiteralDecimalNullable())
                ),
                new SelectItemProjection(
                    "asin_decimal",
                    new AsinDecimalProjection(new LiteralDecimal(0.5m))
                ),
                new SelectItemProjection(
                    "asin_decimal_of_integer",
                    new AsinDecimalProjection(new LiteralInteger(1))
                ),
                new SelectItemProjection(
                    "asin_decimal_nullable",
                    new AsinDecimalNullableProjection(new LiteralDecimalNullable())
                ),
                new SelectItemProjection(
                    "acos_decimal",
                    new AcosDecimalProjection(new LiteralDecimal(0.5m))
                ),
                new SelectItemProjection(
                    "acos_decimal_of_integer",
                    new AcosDecimalProjection(new LiteralInteger(1))
                ),
                new SelectItemProjection(
                    "acos_decimal_nullable",
                    new AcosDecimalNullableProjection(new LiteralDecimalNullable())
                ),
                new SelectItemProjection(
                    "atan_decimal",
                    new AtanDecimalProjection(new LiteralDecimal(0.5m))
                ),
                new SelectItemProjection(
                    "atan_decimal_of_integer",
                    new AtanDecimalProjection(new LiteralInteger(1))
                ),
                new SelectItemProjection(
                    "atan_decimal_nullable",
                    new AtanDecimalNullableProjection(new LiteralDecimalNullable())
                ),
                new SelectItemProjection(
                    "atan2_decimal",
                    new Atan2DecimalProjection(
                        new LiteralDecimal(0.5m),
                        new LiteralDecimal(-1m)
                    )
                ),
                new SelectItemProjection(
                    "atan2_decimal_of_integers",
                    new Atan2DecimalProjection(
                        new LiteralInteger(1),
                        new LiteralInteger(-3)
                    )
                ),
                new SelectItemProjection(
                    "atan2_decimal_nullable_y",
                    new Atan2DecimalNullableProjection(
                        new LiteralDecimalNullable(),
                        new LiteralDecimal(0.5m)
                    )
                ),
                new SelectItemProjection(
                    "atan2_decimal_nullable_x",
                    new Atan2DecimalNullableProjection(
                        new LiteralDecimal(0.5m),
                        new LiteralDecimalNullable()
                    )
                ),
                new SelectItemProjection("pi_decimal", new PiProjection()),
                new SelectItemProjection(
                    "log_decimal_of_integers",
                    new LogDecimalProjection(new LiteralInteger(7), new LiteralInteger(7))
                ),
                new SelectItemProjection(
                    "concat_string",
                    new ConcatStringProjection([
                        new LiteralString("a"),
                        new LiteralString("b"),
                    ])
                ),
                new SelectItemProjection(
                    "concat_string_nullable",
                    new ConcatStringNullableProjection([
                        new LiteralString("a"),
                        new LiteralStringNullable(),
                    ])
                ),
                new SelectItemProjection(
                    "length_integer",
                    new LengthIntegerProjection(new LiteralString("abc"))
                ),
                new SelectItemProjection(
                    "length_integer_nullable",
                    new LengthIntegerNullableProjection(new LiteralStringNullable())
                ),
                new SelectItemProjection(
                    "upper_string",
                    new UpperStringProjection(new LiteralString("Stra\u00dfe"))
                ),
                new SelectItemProjection(
                    "upper_string_nullable",
                    new UpperStringNullableProjection(new LiteralStringNullable())
                ),
                new SelectItemProjection(
                    "lower_string",
                    new LowerStringProjection(new LiteralString("Stra\u00dfe"))
                ),
                new SelectItemProjection(
                    "lower_string_nullable",
                    new LowerStringNullableProjection(new LiteralStringNullable())
                ),
                new SelectItemProjection(
                    "foldCase_string",
                    new FoldCaseStringProjection(new LiteralString("a"))
                ),
                new SelectItemProjection(
                    "foldCase_string_nullable",
                    new FoldCaseStringNullableProjection(new LiteralStringNullable())
                ),
                new SelectItemProjection(
                    "normalize_string_nfc",
                    new NormalizeStringProjection(
                        new LiteralString("a"),
                        NormalizationForm.NFC
                    )
                ),
                new SelectItemProjection(
                    "normalize_string_nfd",
                    new NormalizeStringProjection(
                        new LiteralString("a"),
                        NormalizationForm.NFD
                    )
                ),
                new SelectItemProjection(
                    "normalize_string_nfkc",
                    new NormalizeStringProjection(
                        new LiteralString("a"),
                        NormalizationForm.NFKC
                    )
                ),
                new SelectItemProjection(
                    "normalize_string_nfkd",
                    new NormalizeStringProjection(
                        new LiteralString("a"),
                        NormalizationForm.NFKD
                    )
                ),
                new SelectItemProjection(
                    "normalize_string_nullable",
                    new NormalizeStringNullableProjection(
                        new LiteralStringNullable(),
                        NormalizationForm.NFC
                    )
                ),
                new SelectItemProjection(
                    "normalize_string_nullable_nfkd",
                    new NormalizeStringNullableProjection(
                        new LiteralStringNullable(),
                        NormalizationForm.NFKD
                    )
                ),
                new SelectItemProjection(
                    "removeDiacritics_string",
                    new RemoveDiacriticsStringProjection(new LiteralString("a"))
                ),
                new SelectItemProjection(
                    "removeDiacritics_string_nullable",
                    new RemoveDiacriticsStringNullableProjection(
                        new LiteralStringNullable()
                    )
                ),
                new SelectItemProjection(
                    "trim_string",
                    new TrimStringProjection(
                        new LiteralString("xax"),
                        [new LiteralString("x")]
                    )
                ),
                new SelectItemProjection(
                    "trim_string_nullable",
                    new TrimStringNullableProjection(
                        new LiteralStringNullable(),
                        [new LiteralString("x"), new LiteralString("xa")]
                    )
                ),
                new SelectItemProjection(
                    "trimStart_string",
                    new TrimStartStringProjection(
                        new LiteralString("xax"),
                        [new LiteralString("x")]
                    )
                ),
                new SelectItemProjection(
                    "trimStart_string_nullable",
                    new TrimStartStringNullableProjection(
                        new LiteralStringNullable(),
                        [new LiteralString("x"), new LiteralString("xa")]
                    )
                ),
                new SelectItemProjection(
                    "trimEnd_string",
                    new TrimEndStringProjection(
                        new LiteralString("xax"),
                        [new LiteralString("x")]
                    )
                ),
                new SelectItemProjection(
                    "trimEnd_string_nullable",
                    new TrimEndStringNullableProjection(
                        new LiteralStringNullable(),
                        [new LiteralString("x"), new LiteralString("xa")]
                    )
                ),
                new SelectItemProjection(
                    "substring_string",
                    new SubstringStringProjection(
                        new LiteralString("hello"),
                        new LiteralInteger(2)
                    )
                ),
                new SelectItemProjection(
                    "substring_string_nullable",
                    new SubstringStringNullableProjection(
                        new LiteralStringNullable(),
                        new LiteralInteger(2)
                    )
                ),
                new SelectItemProjection(
                    "substring_string_length",
                    new SubstringStringProjection(
                        new LiteralString("hello"),
                        new LiteralInteger(2),
                        length: new LiteralInteger(3)
                    )
                ),
                new SelectItemProjection(
                    "substring_string_nullable_length",
                    new SubstringStringNullableProjection(
                        new LiteralStringNullable(),
                        new LiteralInteger(2),
                        length: new LiteralInteger(0)
                    )
                ),
                new SelectItemProjection(
                    "replace_string",
                    new ReplaceStringProjection(
                        new LiteralString("a-b"),
                        new LiteralString("-"),
                        new LiteralString("b")
                    )
                ),
                new SelectItemProjection(
                    "replace_string_nullable",
                    new ReplaceStringNullableProjection(
                        new LiteralStringNullable(),
                        new LiteralString("-"),
                        new LiteralString("b")
                    )
                ),
                new SelectItemProjection(
                    "position_integer",
                    new PositionIntegerProjection(
                        new LiteralString("a-b"),
                        new LiteralString("-")
                    )
                ),
                new SelectItemProjection(
                    "position_integer_nullable",
                    new PositionIntegerNullableProjection(
                        new LiteralStringNullable(),
                        new LiteralString("-")
                    )
                ),
                new SelectItemProjection(
                    "position_integer_start",
                    new PositionIntegerProjection(
                        new LiteralString("a-b"),
                        new LiteralString("-"),
                        start: new LiteralInteger(2)
                    )
                ),
                new SelectItemProjection(
                    "position_integer_nullable_start",
                    new PositionIntegerNullableProjection(
                        new LiteralStringNullable(),
                        new LiteralString("-"),
                        start: new LiteralInteger(2)
                    )
                ),
                new SelectItemProjection(
                    "splitPart_string",
                    new SplitPartStringProjection(
                        new LiteralString("a-b"),
                        new LiteralString("-"),
                        new LiteralInteger(1)
                    )
                ),
                new SelectItemProjection(
                    "splitPart_string_nullable",
                    new SplitPartStringNullableProjection(
                        new LiteralStringNullable(),
                        new LiteralString("-"),
                        new LiteralInteger(-3)
                    )
                ),
                new SelectItemProjection(
                    "padStart_string",
                    new PadStartStringProjection(
                        new LiteralString("a"),
                        new LiteralInteger(7),
                        new LiteralString("b")
                    )
                ),
                new SelectItemProjection(
                    "padStart_string_nullable",
                    new PadStartStringNullableProjection(
                        new LiteralStringNullable(),
                        new LiteralInteger(7),
                        new LiteralString("b")
                    )
                ),
                new SelectItemProjection(
                    "padEnd_string",
                    new PadEndStringProjection(
                        new LiteralString("a"),
                        new LiteralInteger(7),
                        new LiteralString("b")
                    )
                ),
                new SelectItemProjection(
                    "padEnd_string_nullable",
                    new PadEndStringNullableProjection(
                        new LiteralStringNullable(),
                        new LiteralInteger(7),
                        new LiteralString("b")
                    )
                ),
                new SelectItemProjection(
                    "repeat_string",
                    new RepeatStringProjection(
                        new LiteralString("a"),
                        new LiteralInteger(7)
                    )
                ),
                new SelectItemProjection(
                    "repeat_string_nullable",
                    new RepeatStringNullableProjection(
                        new LiteralStringNullable(),
                        new LiteralInteger(7)
                    )
                ),
                new SelectItemProjection(
                    "reverse_string",
                    new ReverseStringProjection(new LiteralString("a"))
                ),
                new SelectItemProjection(
                    "reverse_string_nullable",
                    new ReverseStringNullableProjection(new LiteralStringNullable())
                ),
                new SelectItemProjection(
                    "concatWith_string",
                    new ConcatWithProjection(
                        [new LiteralString("a"), new LiteralString("b")],
                        new LiteralString("-")
                    )
                ),
                new SelectItemProjection(
                    "concatWith_string_null_values",
                    new ConcatWithProjection(
                        [
                            new LiteralStringNullable(),
                            new LiteralString("a"),
                            new LiteralStringNullable(),
                        ],
                        new LiteralString("-")
                    )
                ),
                new SelectItemProjection(
                    "concatWith_string_only_null_values",
                    new ConcatWithProjection(
                        [new LiteralStringNullable(), new LiteralStringNullable()],
                        new LiteralString("-")
                    )
                ),
                new SelectItemProjection(
                    "matchesRegex_boolean",
                    new MatchesRegexProjection(
                        new LiteralString("a1"),
                        new LiteralString("[0-9]")
                    )
                ),
                new SelectItemProjection(
                    "matchesRegex_boolean_nullable_operand",
                    new MatchesRegexProjection(
                        new LiteralStringNullable(),
                        new LiteralString("[0-9]")
                    )
                ),
                new SelectItemProjection(
                    "matchesRegex_boolean_parameter_pattern",
                    new MatchesRegexProjection(
                        new LiteralString("a1"),
                        new ParamString("pattern")
                    )
                ),
                new SelectItemProjection(
                    "extractRegex_string_nullable",
                    new ExtractRegexProjection(
                        new LiteralString("a1"),
                        new LiteralString("[0-9]+")
                    )
                ),
                new SelectItemProjection(
                    "extractRegex_string_nullable_group",
                    new ExtractRegexProjection(
                        new LiteralString("a1"),
                        new LiteralString("(a)([0-9])"),
                        group: new LiteralInteger(2)
                    )
                ),
                new SelectItemProjection(
                    "extractRegex_string_nullable_nullable_operand",
                    new ExtractRegexProjection(
                        new LiteralStringNullable(),
                        new LiteralString("[0-9]+")
                    )
                ),
                new SelectItemProjection(
                    "extractRegex_string_nullable_parameters",
                    new ExtractRegexProjection(
                        new LiteralString("a1"),
                        new ParamString("pattern"),
                        group: new ParamInteger("group")
                    )
                ),
                new SelectItemProjection(
                    "replaceRegex_string",
                    new ReplaceRegexStringProjection(
                        new LiteralString("a1"),
                        new LiteralString("b"),
                        new LiteralString("[0-9]")
                    )
                ),
                new SelectItemProjection(
                    "replaceRegex_string_nullable",
                    new ReplaceRegexStringNullableProjection(
                        new LiteralStringNullable(),
                        new LiteralString("b"),
                        new LiteralString("[0-9]")
                    )
                ),
                new SelectItemProjection(
                    "round_decimal_nullable_by_digits",
                    new RoundDecimalNullableProjection(
                        new LiteralDecimal(2.5m),
                        new LiteralIntegerNullable()
                    )
                ),
                new SelectItemProjection(
                    "truncate_decimal_nullable_by_digits",
                    new TruncateDecimalNullableProjection(
                        new LiteralDecimal(2.5m),
                        new LiteralIntegerNullable()
                    )
                ),
                new SelectItemProjection(
                    "round_integer_of_integer_with_digits",
                    new RoundWithDigitsIntegerProjection(
                        new LiteralInteger(7),
                        new LiteralInteger(1)
                    )
                ),
                new SelectItemProjection(
                    "round_integer_nullable_of_integer_with_digits",
                    new RoundWithDigitsIntegerNullableProjection(
                        new LiteralIntegerNullable(),
                        new LiteralInteger(-1)
                    )
                ),
                new SelectItemProjection(
                    "round_integer_nullable_of_integer_by_digits",
                    new RoundWithDigitsIntegerNullableProjection(
                        new LiteralInteger(7),
                        new LiteralIntegerNullable()
                    )
                ),
                new SelectItemProjection(
                    "truncate_integer_of_integer_with_digits",
                    new TruncateWithDigitsIntegerProjection(
                        new LiteralInteger(7),
                        new LiteralInteger(-1)
                    )
                ),
                new SelectItemProjection(
                    "truncate_integer_nullable_of_integer_with_digits",
                    new TruncateWithDigitsIntegerNullableProjection(
                        new LiteralIntegerNullable(),
                        new LiteralInteger(2)
                    )
                ),
                new SelectItemProjection(
                    "truncate_integer_nullable_of_integer_by_digits",
                    new TruncateWithDigitsIntegerNullableProjection(
                        new LiteralInteger(7),
                        new LiteralIntegerNullable()
                    )
                ),
                new SelectItemProjection(
                    "power_decimal_nullable_by_exponent",
                    new PowerDecimalNullableProjection(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimalNullable()
                    )
                ),
                new SelectItemProjection(
                    "log_decimal_nullable_by_base",
                    new LogDecimalNullableProjection(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimalNullable()
                    )
                ),
                new SelectItemProjection(
                    "substring_string_nullable_by_start",
                    new SubstringStringNullableProjection(
                        new LiteralString("abc"),
                        new LiteralIntegerNullable(),
                        length: new LiteralInteger(2)
                    )
                ),
                new SelectItemProjection(
                    "substring_string_nullable_by_length",
                    new SubstringStringNullableProjection(
                        new LiteralString("abc"),
                        new LiteralInteger(1),
                        length: new LiteralIntegerNullable()
                    )
                ),
                new SelectItemProjection(
                    "replace_string_nullable_by_pattern",
                    new ReplaceStringNullableProjection(
                        new LiteralString("abc"),
                        new LiteralStringNullable(),
                        new LiteralString("x")
                    )
                ),
                new SelectItemProjection(
                    "replace_string_nullable_by_replacement",
                    new ReplaceStringNullableProjection(
                        new LiteralString("abc"),
                        new LiteralString("b"),
                        new LiteralStringNullable()
                    )
                ),
                new SelectItemProjection(
                    "position_integer_nullable_by_pattern",
                    new PositionIntegerNullableProjection(
                        new LiteralString("abc"),
                        new LiteralStringNullable(),
                        start: new LiteralInteger(1)
                    )
                ),
                new SelectItemProjection(
                    "position_integer_nullable_by_start",
                    new PositionIntegerNullableProjection(
                        new LiteralString("abc"),
                        new LiteralString("b"),
                        start: new LiteralIntegerNullable()
                    )
                ),
                new SelectItemProjection(
                    "splitPart_string_nullable_by_delimiter",
                    new SplitPartStringNullableProjection(
                        new LiteralString("a-b"),
                        new LiteralStringNullable(),
                        new LiteralInteger(1)
                    )
                ),
                new SelectItemProjection(
                    "splitPart_string_nullable_by_index",
                    new SplitPartStringNullableProjection(
                        new LiteralString("a-b"),
                        new LiteralString("-"),
                        new LiteralIntegerNullable()
                    )
                ),
                new SelectItemProjection(
                    "padStart_string_nullable_by_length",
                    new PadStartStringNullableProjection(
                        new LiteralString("abc"),
                        new LiteralIntegerNullable(),
                        new LiteralString("0")
                    )
                ),
                new SelectItemProjection(
                    "padStart_string_nullable_by_fill",
                    new PadStartStringNullableProjection(
                        new LiteralString("abc"),
                        new LiteralInteger(7),
                        new LiteralStringNullable()
                    )
                ),
                new SelectItemProjection(
                    "padEnd_string_nullable_by_length",
                    new PadEndStringNullableProjection(
                        new LiteralString("abc"),
                        new LiteralIntegerNullable(),
                        new LiteralString("0")
                    )
                ),
                new SelectItemProjection(
                    "padEnd_string_nullable_by_fill",
                    new PadEndStringNullableProjection(
                        new LiteralString("abc"),
                        new LiteralInteger(7),
                        new LiteralStringNullable()
                    )
                ),
                new SelectItemProjection(
                    "repeat_string_nullable_by_count",
                    new RepeatStringNullableProjection(
                        new LiteralString("abc"),
                        new LiteralIntegerNullable()
                    )
                ),
                new SelectItemProjection(
                    "replaceRegex_string_nullable_by_replacement",
                    new ReplaceRegexStringNullableProjection(
                        new LiteralString("abc"),
                        new LiteralStringNullable(),
                        new LiteralString("b+")
                    )
                ),
                new SelectItemProjection(
                    "replaceRegex_string_parameter_pattern",
                    new ReplaceRegexStringProjection(
                        new LiteralString("a1"),
                        new LiteralString("b"),
                        new ParamString("pattern")
                    )
                ),
                new SelectItemProjection(
                    "contains_boolean",
                    new ContainsProjection(
                        new LiteralString("abcabc"),
                        new LiteralString("abc")
                    )
                ),
                new SelectItemProjection(
                    "contains_boolean_nullable_operand",
                    new ContainsProjection(
                        new LiteralStringNullable(),
                        new LiteralString("abc")
                    )
                ),
                new SelectItemProjection(
                    "contains_boolean_nullable_pattern",
                    new ContainsProjection(
                        new LiteralString("abcabc"),
                        new LiteralStringNullable()
                    )
                ),
                new SelectItemProjection(
                    "startsWith_boolean",
                    new StartsWithProjection(
                        new LiteralString("abcabc"),
                        new LiteralString("abc")
                    )
                ),
                new SelectItemProjection(
                    "startsWith_boolean_nullable_operand",
                    new StartsWithProjection(
                        new LiteralStringNullable(),
                        new LiteralString("abc")
                    )
                ),
                new SelectItemProjection(
                    "startsWith_boolean_nullable_pattern",
                    new StartsWithProjection(
                        new LiteralString("abcabc"),
                        new LiteralStringNullable()
                    )
                ),
                new SelectItemProjection(
                    "endsWith_boolean",
                    new EndsWithProjection(
                        new LiteralString("abcabc"),
                        new LiteralString("abc")
                    )
                ),
                new SelectItemProjection(
                    "endsWith_boolean_nullable_operand",
                    new EndsWithProjection(
                        new LiteralStringNullable(),
                        new LiteralString("abc")
                    )
                ),
                new SelectItemProjection(
                    "endsWith_boolean_nullable_pattern",
                    new EndsWithProjection(
                        new LiteralString("abcabc"),
                        new LiteralStringNullable()
                    )
                ),
                new SelectItemProjection(
                    "dateAddDays_date",
                    new DateAddDaysDateProjection(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralInteger(7)
                    )
                ),
                new SelectItemProjection(
                    "dateAddDays_date_nullable",
                    new DateAddDaysDateNullableProjection(
                        new LiteralDateNullable(),
                        new LiteralInteger(7)
                    )
                ),
                new SelectItemProjection(
                    "dateDiffDays_integer",
                    new DateDiffDaysIntegerProjection(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDate(new DateOnly(2024, 3, 1))
                    )
                ),
                new SelectItemProjection(
                    "dateDiffDays_integer_nullable",
                    new DateDiffDaysIntegerNullableProjection(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDateNullable()
                    )
                ),
                new SelectItemProjection(
                    "timeAddSeconds_time",
                    new TimeAddSecondsTimeProjection(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralDecimal(2.5m)
                    )
                ),
                new SelectItemProjection(
                    "timeAddSeconds_time_nullable",
                    new TimeAddSecondsTimeNullableProjection(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralIntegerNullable()
                    )
                ),
                new SelectItemProjection(
                    "timeDiffSeconds_decimal",
                    new TimeDiffSecondsDecimalProjection(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralTime(new TimeOnly(17, 45, 30, 500))
                    )
                ),
                new SelectItemProjection(
                    "timeDiffSeconds_decimal_nullable",
                    new TimeDiffSecondsDecimalNullableProjection(
                        new LiteralTimeNullable(),
                        new LiteralTime(new TimeOnly(8, 30, 0))
                    )
                ),
                new SelectItemProjection(
                    "datetimeAddSeconds_datetime",
                    new DatetimeAddSecondsDatetimeProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDecimal(2.5m)
                    )
                ),
                new SelectItemProjection(
                    "datetimeAddSeconds_datetime_nullable",
                    new DatetimeAddSecondsDatetimeNullableProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralIntegerNullable()
                    )
                ),
                new SelectItemProjection(
                    "datetimeDiffSeconds_decimal",
                    new DatetimeDiffSecondsDecimalProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemProjection(
                    "datetimeDiffSeconds_decimal_nullable",
                    new DatetimeDiffSecondsDecimalNullableProjection(
                        new LiteralDatetimeNullable(),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemProjection(
                    "dateAddMonths_date",
                    new DateAddMonthsDateProjection(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralInteger(1)
                    )
                ),
                new SelectItemProjection(
                    "dateAddMonths_date_nullable",
                    new DateAddMonthsDateNullableProjection(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralIntegerNullable()
                    )
                ),
                new SelectItemProjection(
                    "datetimeAddMonths_datetime",
                    new DatetimeAddMonthsDatetimeProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 18, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralInteger(1)
                    )
                ),
                new SelectItemProjection(
                    "datetimeAddMonths_datetime_nullable",
                    new DatetimeAddMonthsDatetimeNullableProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 18, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralIntegerNullable()
                    )
                ),
                new SelectItemProjection(
                    "datetimeAddMonths_datetime_in_zone",
                    new DatetimeAddMonthsDatetimeProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralInteger(1),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemProjection(
                    "datetimeAddMonths_datetime_nullable_in_zone",
                    new DatetimeAddMonthsDatetimeNullableProjection(
                        new LiteralDatetimeNullable(),
                        new LiteralInteger(1),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemProjection(
                    "datetimeAddMonths_datetime_nullable_months_in_zone",
                    new DatetimeAddMonthsDatetimeNullableProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralIntegerNullable(),
                        zone: new LiteralString("America/New_York")
                    )
                ),
                new SelectItemProjection(
                    "datetimeAddMonths_datetime_nullable_by_zone",
                    new DatetimeAddMonthsDatetimeNullableProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralInteger(-12),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemProjection(
                    "dateDiffMonths_integer",
                    new DateDiffMonthsIntegerProjection(
                        new LiteralDate(new DateOnly(2023, 2, 28)),
                        new LiteralDate(new DateOnly(2023, 1, 31))
                    )
                ),
                new SelectItemProjection(
                    "dateDiffMonths_integer_nullable",
                    new DateDiffMonthsIntegerNullableProjection(
                        new LiteralDateNullable(),
                        new LiteralDate(new DateOnly(2023, 1, 31))
                    )
                ),
                new SelectItemProjection(
                    "datetimeDiffMonths_integer",
                    new DatetimeDiffMonthsIntegerProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 2, 15, 11, 59, 59, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 15, 12, 0, 0, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemProjection(
                    "datetimeDiffMonths_integer_nullable",
                    new DatetimeDiffMonthsIntegerNullableProjection(
                        new LiteralDatetimeNullable(),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 15, 12, 0, 0, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemProjection(
                    "datetimeDiffMonths_integer_in_zone",
                    new DatetimeDiffMonthsIntegerProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemProjection(
                    "datetimeDiffMonths_integer_nullable_in_zone",
                    new DatetimeDiffMonthsIntegerNullableProjection(
                        new LiteralDatetimeNullable(),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Etc/GMT+5")
                    )
                ),
                new SelectItemProjection(
                    "datetimeDiffMonths_integer_nullable_by_zone",
                    new DatetimeDiffMonthsIntegerNullableProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemProjection(
                    "dateFromParts_date",
                    new DateFromPartsDateProjection(
                        new LiteralInteger(2024),
                        new LiteralInteger(2),
                        new LiteralInteger(29)
                    )
                ),
                new SelectItemProjection(
                    "dateFromParts_date_nullable_year",
                    new DateFromPartsDateNullableProjection(
                        new LiteralIntegerNullable(),
                        new LiteralInteger(2),
                        new LiteralInteger(29)
                    )
                ),
                new SelectItemProjection(
                    "dateFromParts_date_nullable_month",
                    new DateFromPartsDateNullableProjection(
                        new LiteralInteger(2024),
                        new LiteralIntegerNullable(),
                        new LiteralInteger(29)
                    )
                ),
                new SelectItemProjection(
                    "dateFromParts_date_nullable_day",
                    new DateFromPartsDateNullableProjection(
                        new LiteralInteger(2024),
                        new LiteralInteger(2),
                        new LiteralIntegerNullable()
                    )
                ),
                new SelectItemProjection(
                    "timeFromParts_time",
                    new TimeFromPartsTimeProjection(
                        new LiteralInteger(18),
                        new LiteralInteger(30),
                        new LiteralDecimal(15.25m)
                    )
                ),
                new SelectItemProjection(
                    "timeFromParts_time_integer_second",
                    new TimeFromPartsTimeProjection(
                        new LiteralInteger(18),
                        new LiteralInteger(30),
                        new LiteralInteger(15)
                    )
                ),
                new SelectItemProjection(
                    "timeFromParts_time_nullable_hour",
                    new TimeFromPartsTimeNullableProjection(
                        new LiteralIntegerNullable(),
                        new LiteralInteger(30),
                        new LiteralInteger(15)
                    )
                ),
                new SelectItemProjection(
                    "timeFromParts_time_nullable_minute",
                    new TimeFromPartsTimeNullableProjection(
                        new LiteralInteger(18),
                        new LiteralIntegerNullable(),
                        new LiteralInteger(15)
                    )
                ),
                new SelectItemProjection(
                    "timeFromParts_time_nullable_second",
                    new TimeFromPartsTimeNullableProjection(
                        new LiteralInteger(18),
                        new LiteralInteger(30),
                        new LiteralDecimalNullable()
                    )
                ),
                new SelectItemProjection(
                    "datetimeFromParts_datetime",
                    new DatetimeFromPartsDatetimeProjection(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralTime(new TimeOnly(8, 30, 0))
                    )
                ),
                new SelectItemProjection(
                    "datetimeFromParts_datetime_nullable_date",
                    new DatetimeFromPartsDatetimeNullableProjection(
                        new LiteralDateNullable(),
                        new LiteralTime(new TimeOnly(8, 30, 0))
                    )
                ),
                new SelectItemProjection(
                    "datetimeFromParts_datetime_nullable_time",
                    new DatetimeFromPartsDatetimeNullableProjection(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralTimeNullable()
                    )
                ),
                new SelectItemProjection(
                    "datetimeFromParts_datetime_in_zone",
                    new DatetimeFromPartsDatetimeProjection(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemProjection(
                    "datetimeFromParts_datetime_nullable_date_in_zone",
                    new DatetimeFromPartsDatetimeNullableProjection(
                        new LiteralDateNullable(),
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        zone: new LiteralString("Etc/GMT-3")
                    )
                ),
                new SelectItemProjection(
                    "datetimeFromParts_datetime_nullable_time_in_zone",
                    new DatetimeFromPartsDatetimeNullableProjection(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralTimeNullable(),
                        zone: new LiteralString("America/New_York")
                    )
                ),
                new SelectItemProjection(
                    "datetimeFromParts_datetime_nullable_by_zone",
                    new DatetimeFromPartsDatetimeNullableProjection(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemProjection(
                    "year_date",
                    new YearDateIntegerProjection(
                        new LiteralDate(new DateOnly(2024, 12, 30))
                    )
                ),
                new SelectItemProjection(
                    "year_date_nullable",
                    new YearDateIntegerNullableProjection(new LiteralDateNullable())
                ),
                new SelectItemProjection(
                    "year_datetime",
                    new YearDatetimeIntegerProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(
                                2024,
                                12,
                                30,
                                18,
                                30,
                                15,
                                250,
                                TimeSpan.Zero
                            )
                        )
                    )
                ),
                new SelectItemProjection(
                    "year_datetime_nullable",
                    new YearDatetimeIntegerNullableProjection(
                        new LiteralDatetimeNullable()
                    )
                ),
                new SelectItemProjection(
                    "year_datetime_in_zone",
                    new YearDatetimeIntegerProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemProjection(
                    "year_datetime_nullable_in_zone",
                    new YearDatetimeIntegerNullableProjection(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemProjection(
                    "year_datetime_nullable_by_zone",
                    new YearDatetimeIntegerNullableProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemProjection(
                    "quarter_date",
                    new QuarterDateIntegerProjection(
                        new LiteralDate(new DateOnly(2024, 12, 30))
                    )
                ),
                new SelectItemProjection(
                    "quarter_date_nullable",
                    new QuarterDateIntegerNullableProjection(new LiteralDateNullable())
                ),
                new SelectItemProjection(
                    "quarter_datetime",
                    new QuarterDatetimeIntegerProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(
                                2024,
                                12,
                                30,
                                18,
                                30,
                                15,
                                250,
                                TimeSpan.Zero
                            )
                        )
                    )
                ),
                new SelectItemProjection(
                    "quarter_datetime_nullable",
                    new QuarterDatetimeIntegerNullableProjection(
                        new LiteralDatetimeNullable()
                    )
                ),
                new SelectItemProjection(
                    "quarter_datetime_in_zone",
                    new QuarterDatetimeIntegerProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemProjection(
                    "quarter_datetime_nullable_in_zone",
                    new QuarterDatetimeIntegerNullableProjection(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemProjection(
                    "quarter_datetime_nullable_by_zone",
                    new QuarterDatetimeIntegerNullableProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemProjection(
                    "month_date",
                    new MonthDateIntegerProjection(
                        new LiteralDate(new DateOnly(2024, 12, 30))
                    )
                ),
                new SelectItemProjection(
                    "month_date_nullable",
                    new MonthDateIntegerNullableProjection(new LiteralDateNullable())
                ),
                new SelectItemProjection(
                    "month_datetime",
                    new MonthDatetimeIntegerProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(
                                2024,
                                12,
                                30,
                                18,
                                30,
                                15,
                                250,
                                TimeSpan.Zero
                            )
                        )
                    )
                ),
                new SelectItemProjection(
                    "month_datetime_nullable",
                    new MonthDatetimeIntegerNullableProjection(
                        new LiteralDatetimeNullable()
                    )
                ),
                new SelectItemProjection(
                    "month_datetime_in_zone",
                    new MonthDatetimeIntegerProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemProjection(
                    "month_datetime_nullable_in_zone",
                    new MonthDatetimeIntegerNullableProjection(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemProjection(
                    "month_datetime_nullable_by_zone",
                    new MonthDatetimeIntegerNullableProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemProjection(
                    "day_date",
                    new DayDateIntegerProjection(
                        new LiteralDate(new DateOnly(2024, 12, 30))
                    )
                ),
                new SelectItemProjection(
                    "day_date_nullable",
                    new DayDateIntegerNullableProjection(new LiteralDateNullable())
                ),
                new SelectItemProjection(
                    "day_datetime",
                    new DayDatetimeIntegerProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(
                                2024,
                                12,
                                30,
                                18,
                                30,
                                15,
                                250,
                                TimeSpan.Zero
                            )
                        )
                    )
                ),
                new SelectItemProjection(
                    "day_datetime_nullable",
                    new DayDatetimeIntegerNullableProjection(
                        new LiteralDatetimeNullable()
                    )
                ),
                new SelectItemProjection(
                    "day_datetime_in_zone",
                    new DayDatetimeIntegerProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemProjection(
                    "day_datetime_nullable_in_zone",
                    new DayDatetimeIntegerNullableProjection(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemProjection(
                    "day_datetime_nullable_by_zone",
                    new DayDatetimeIntegerNullableProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemProjection(
                    "dayOfWeek_date",
                    new DayOfWeekDateIntegerProjection(
                        new LiteralDate(new DateOnly(2024, 12, 30))
                    )
                ),
                new SelectItemProjection(
                    "dayOfWeek_date_nullable",
                    new DayOfWeekDateIntegerNullableProjection(new LiteralDateNullable())
                ),
                new SelectItemProjection(
                    "dayOfWeek_datetime",
                    new DayOfWeekDatetimeIntegerProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(
                                2024,
                                12,
                                30,
                                18,
                                30,
                                15,
                                250,
                                TimeSpan.Zero
                            )
                        )
                    )
                ),
                new SelectItemProjection(
                    "dayOfWeek_datetime_nullable",
                    new DayOfWeekDatetimeIntegerNullableProjection(
                        new LiteralDatetimeNullable()
                    )
                ),
                new SelectItemProjection(
                    "dayOfWeek_datetime_in_zone",
                    new DayOfWeekDatetimeIntegerProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemProjection(
                    "dayOfWeek_datetime_nullable_in_zone",
                    new DayOfWeekDatetimeIntegerNullableProjection(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemProjection(
                    "dayOfWeek_datetime_nullable_by_zone",
                    new DayOfWeekDatetimeIntegerNullableProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemProjection(
                    "dayOfYear_date",
                    new DayOfYearDateIntegerProjection(
                        new LiteralDate(new DateOnly(2024, 12, 30))
                    )
                ),
                new SelectItemProjection(
                    "dayOfYear_date_nullable",
                    new DayOfYearDateIntegerNullableProjection(new LiteralDateNullable())
                ),
                new SelectItemProjection(
                    "dayOfYear_datetime",
                    new DayOfYearDatetimeIntegerProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(
                                2024,
                                12,
                                30,
                                18,
                                30,
                                15,
                                250,
                                TimeSpan.Zero
                            )
                        )
                    )
                ),
                new SelectItemProjection(
                    "dayOfYear_datetime_nullable",
                    new DayOfYearDatetimeIntegerNullableProjection(
                        new LiteralDatetimeNullable()
                    )
                ),
                new SelectItemProjection(
                    "dayOfYear_datetime_in_zone",
                    new DayOfYearDatetimeIntegerProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemProjection(
                    "dayOfYear_datetime_nullable_in_zone",
                    new DayOfYearDatetimeIntegerNullableProjection(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemProjection(
                    "dayOfYear_datetime_nullable_by_zone",
                    new DayOfYearDatetimeIntegerNullableProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemProjection(
                    "week_date",
                    new WeekDateIntegerProjection(
                        new LiteralDate(new DateOnly(2024, 12, 30))
                    )
                ),
                new SelectItemProjection(
                    "week_date_nullable",
                    new WeekDateIntegerNullableProjection(new LiteralDateNullable())
                ),
                new SelectItemProjection(
                    "week_datetime",
                    new WeekDatetimeIntegerProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(
                                2024,
                                12,
                                30,
                                18,
                                30,
                                15,
                                250,
                                TimeSpan.Zero
                            )
                        )
                    )
                ),
                new SelectItemProjection(
                    "week_datetime_nullable",
                    new WeekDatetimeIntegerNullableProjection(
                        new LiteralDatetimeNullable()
                    )
                ),
                new SelectItemProjection(
                    "week_datetime_in_zone",
                    new WeekDatetimeIntegerProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemProjection(
                    "week_datetime_nullable_in_zone",
                    new WeekDatetimeIntegerNullableProjection(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemProjection(
                    "week_datetime_nullable_by_zone",
                    new WeekDatetimeIntegerNullableProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemProjection(
                    "weekYear_date",
                    new WeekYearDateIntegerProjection(
                        new LiteralDate(new DateOnly(2024, 12, 30))
                    )
                ),
                new SelectItemProjection(
                    "weekYear_date_nullable",
                    new WeekYearDateIntegerNullableProjection(new LiteralDateNullable())
                ),
                new SelectItemProjection(
                    "weekYear_datetime",
                    new WeekYearDatetimeIntegerProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(
                                2024,
                                12,
                                30,
                                18,
                                30,
                                15,
                                250,
                                TimeSpan.Zero
                            )
                        )
                    )
                ),
                new SelectItemProjection(
                    "weekYear_datetime_nullable",
                    new WeekYearDatetimeIntegerNullableProjection(
                        new LiteralDatetimeNullable()
                    )
                ),
                new SelectItemProjection(
                    "weekYear_datetime_in_zone",
                    new WeekYearDatetimeIntegerProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemProjection(
                    "weekYear_datetime_nullable_in_zone",
                    new WeekYearDatetimeIntegerNullableProjection(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemProjection(
                    "weekYear_datetime_nullable_by_zone",
                    new WeekYearDatetimeIntegerNullableProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemProjection(
                    "hour_time",
                    new HourTimeIntegerProjection(
                        new LiteralTime(new TimeOnly(18, 30, 15, 250))
                    )
                ),
                new SelectItemProjection(
                    "hour_time_nullable",
                    new HourTimeIntegerNullableProjection(new LiteralTimeNullable())
                ),
                new SelectItemProjection(
                    "hour_datetime",
                    new HourDatetimeIntegerProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(
                                2024,
                                12,
                                30,
                                18,
                                30,
                                15,
                                250,
                                TimeSpan.Zero
                            )
                        )
                    )
                ),
                new SelectItemProjection(
                    "hour_datetime_nullable",
                    new HourDatetimeIntegerNullableProjection(
                        new LiteralDatetimeNullable()
                    )
                ),
                new SelectItemProjection(
                    "hour_datetime_in_zone",
                    new HourDatetimeIntegerProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemProjection(
                    "hour_datetime_nullable_in_zone",
                    new HourDatetimeIntegerNullableProjection(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemProjection(
                    "hour_datetime_nullable_by_zone",
                    new HourDatetimeIntegerNullableProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemProjection(
                    "minute_time",
                    new MinuteTimeIntegerProjection(
                        new LiteralTime(new TimeOnly(18, 30, 15, 250))
                    )
                ),
                new SelectItemProjection(
                    "minute_time_nullable",
                    new MinuteTimeIntegerNullableProjection(new LiteralTimeNullable())
                ),
                new SelectItemProjection(
                    "minute_datetime",
                    new MinuteDatetimeIntegerProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(
                                2024,
                                12,
                                30,
                                18,
                                30,
                                15,
                                250,
                                TimeSpan.Zero
                            )
                        )
                    )
                ),
                new SelectItemProjection(
                    "minute_datetime_nullable",
                    new MinuteDatetimeIntegerNullableProjection(
                        new LiteralDatetimeNullable()
                    )
                ),
                new SelectItemProjection(
                    "minute_datetime_in_zone",
                    new MinuteDatetimeIntegerProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemProjection(
                    "minute_datetime_nullable_in_zone",
                    new MinuteDatetimeIntegerNullableProjection(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemProjection(
                    "minute_datetime_nullable_by_zone",
                    new MinuteDatetimeIntegerNullableProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemProjection(
                    "second_time",
                    new SecondTimeIntegerProjection(
                        new LiteralTime(new TimeOnly(18, 30, 15, 250))
                    )
                ),
                new SelectItemProjection(
                    "second_time_nullable",
                    new SecondTimeIntegerNullableProjection(new LiteralTimeNullable())
                ),
                new SelectItemProjection(
                    "second_datetime",
                    new SecondDatetimeIntegerProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(
                                2024,
                                12,
                                30,
                                18,
                                30,
                                15,
                                250,
                                TimeSpan.Zero
                            )
                        )
                    )
                ),
                new SelectItemProjection(
                    "second_datetime_nullable",
                    new SecondDatetimeIntegerNullableProjection(
                        new LiteralDatetimeNullable()
                    )
                ),
                new SelectItemProjection(
                    "second_datetime_in_zone",
                    new SecondDatetimeIntegerProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemProjection(
                    "second_datetime_nullable_in_zone",
                    new SecondDatetimeIntegerNullableProjection(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemProjection(
                    "second_datetime_nullable_by_zone",
                    new SecondDatetimeIntegerNullableProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemProjection(
                    "microsecond_time",
                    new MicrosecondTimeIntegerProjection(
                        new LiteralTime(new TimeOnly(18, 30, 15, 250))
                    )
                ),
                new SelectItemProjection(
                    "microsecond_time_nullable",
                    new MicrosecondTimeIntegerNullableProjection(
                        new LiteralTimeNullable()
                    )
                ),
                new SelectItemProjection(
                    "microsecond_datetime",
                    new MicrosecondDatetimeIntegerProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(
                                2024,
                                12,
                                30,
                                18,
                                30,
                                15,
                                250,
                                TimeSpan.Zero
                            )
                        )
                    )
                ),
                new SelectItemProjection(
                    "microsecond_datetime_nullable",
                    new MicrosecondDatetimeIntegerNullableProjection(
                        new LiteralDatetimeNullable()
                    )
                ),
                new SelectItemProjection(
                    "startOfYear_date",
                    new StartOfYearDateProjection(
                        new LiteralDate(new DateOnly(2024, 8, 15))
                    )
                ),
                new SelectItemProjection(
                    "startOfYear_date_nullable",
                    new StartOfYearDateNullableProjection(new LiteralDateNullable())
                ),
                new SelectItemProjection(
                    "startOfYear_datetime",
                    new StartOfYearDatetimeProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(
                                2024,
                                8,
                                15,
                                18,
                                30,
                                15,
                                250,
                                TimeSpan.Zero
                            )
                        )
                    )
                ),
                new SelectItemProjection(
                    "startOfYear_datetime_nullable",
                    new StartOfYearDatetimeNullableProjection(
                        new LiteralDatetimeNullable()
                    )
                ),
                new SelectItemProjection(
                    "startOfYear_datetime_in_zone",
                    new StartOfYearDatetimeProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemProjection(
                    "startOfYear_datetime_nullable_in_zone",
                    new StartOfYearDatetimeNullableProjection(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemProjection(
                    "startOfYear_datetime_nullable_by_zone",
                    new StartOfYearDatetimeNullableProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemProjection(
                    "startOfQuarter_date",
                    new StartOfQuarterDateProjection(
                        new LiteralDate(new DateOnly(2024, 8, 15))
                    )
                ),
                new SelectItemProjection(
                    "startOfQuarter_date_nullable",
                    new StartOfQuarterDateNullableProjection(new LiteralDateNullable())
                ),
                new SelectItemProjection(
                    "startOfQuarter_datetime",
                    new StartOfQuarterDatetimeProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(
                                2024,
                                8,
                                15,
                                18,
                                30,
                                15,
                                250,
                                TimeSpan.Zero
                            )
                        )
                    )
                ),
                new SelectItemProjection(
                    "startOfQuarter_datetime_nullable",
                    new StartOfQuarterDatetimeNullableProjection(
                        new LiteralDatetimeNullable()
                    )
                ),
                new SelectItemProjection(
                    "startOfQuarter_datetime_in_zone",
                    new StartOfQuarterDatetimeProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemProjection(
                    "startOfQuarter_datetime_nullable_in_zone",
                    new StartOfQuarterDatetimeNullableProjection(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemProjection(
                    "startOfQuarter_datetime_nullable_by_zone",
                    new StartOfQuarterDatetimeNullableProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemProjection(
                    "startOfMonth_date",
                    new StartOfMonthDateProjection(
                        new LiteralDate(new DateOnly(2024, 8, 15))
                    )
                ),
                new SelectItemProjection(
                    "startOfMonth_date_nullable",
                    new StartOfMonthDateNullableProjection(new LiteralDateNullable())
                ),
                new SelectItemProjection(
                    "startOfMonth_datetime",
                    new StartOfMonthDatetimeProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(
                                2024,
                                8,
                                15,
                                18,
                                30,
                                15,
                                250,
                                TimeSpan.Zero
                            )
                        )
                    )
                ),
                new SelectItemProjection(
                    "startOfMonth_datetime_nullable",
                    new StartOfMonthDatetimeNullableProjection(
                        new LiteralDatetimeNullable()
                    )
                ),
                new SelectItemProjection(
                    "startOfMonth_datetime_in_zone",
                    new StartOfMonthDatetimeProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemProjection(
                    "startOfMonth_datetime_nullable_in_zone",
                    new StartOfMonthDatetimeNullableProjection(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemProjection(
                    "startOfMonth_datetime_nullable_by_zone",
                    new StartOfMonthDatetimeNullableProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemProjection(
                    "startOfWeek_date",
                    new StartOfWeekDateProjection(
                        new LiteralDate(new DateOnly(2024, 8, 15))
                    )
                ),
                new SelectItemProjection(
                    "startOfWeek_date_nullable",
                    new StartOfWeekDateNullableProjection(new LiteralDateNullable())
                ),
                new SelectItemProjection(
                    "startOfWeek_datetime",
                    new StartOfWeekDatetimeProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(
                                2024,
                                8,
                                15,
                                18,
                                30,
                                15,
                                250,
                                TimeSpan.Zero
                            )
                        )
                    )
                ),
                new SelectItemProjection(
                    "startOfWeek_datetime_nullable",
                    new StartOfWeekDatetimeNullableProjection(
                        new LiteralDatetimeNullable()
                    )
                ),
                new SelectItemProjection(
                    "startOfWeek_datetime_in_zone",
                    new StartOfWeekDatetimeProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemProjection(
                    "startOfWeek_datetime_nullable_in_zone",
                    new StartOfWeekDatetimeNullableProjection(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemProjection(
                    "startOfWeek_datetime_nullable_by_zone",
                    new StartOfWeekDatetimeNullableProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemProjection(
                    "startOfDay_datetime",
                    new StartOfDayDatetimeProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(
                                2024,
                                8,
                                15,
                                18,
                                30,
                                15,
                                250,
                                TimeSpan.Zero
                            )
                        )
                    )
                ),
                new SelectItemProjection(
                    "startOfDay_datetime_nullable",
                    new StartOfDayDatetimeNullableProjection(
                        new LiteralDatetimeNullable()
                    )
                ),
                new SelectItemProjection(
                    "startOfDay_datetime_in_zone",
                    new StartOfDayDatetimeProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemProjection(
                    "startOfDay_datetime_nullable_in_zone",
                    new StartOfDayDatetimeNullableProjection(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemProjection(
                    "startOfDay_datetime_nullable_by_zone",
                    new StartOfDayDatetimeNullableProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemProjection(
                    "startOfHour_time",
                    new StartOfHourTimeProjection(
                        new LiteralTime(new TimeOnly(18, 30, 15, 250))
                    )
                ),
                new SelectItemProjection(
                    "startOfHour_time_nullable",
                    new StartOfHourTimeNullableProjection(new LiteralTimeNullable())
                ),
                new SelectItemProjection(
                    "startOfHour_datetime",
                    new StartOfHourDatetimeProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(
                                2024,
                                8,
                                15,
                                18,
                                30,
                                15,
                                250,
                                TimeSpan.Zero
                            )
                        )
                    )
                ),
                new SelectItemProjection(
                    "startOfHour_datetime_nullable",
                    new StartOfHourDatetimeNullableProjection(
                        new LiteralDatetimeNullable()
                    )
                ),
                new SelectItemProjection(
                    "startOfHour_datetime_in_zone",
                    new StartOfHourDatetimeProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemProjection(
                    "startOfHour_datetime_nullable_in_zone",
                    new StartOfHourDatetimeNullableProjection(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemProjection(
                    "startOfHour_datetime_nullable_by_zone",
                    new StartOfHourDatetimeNullableProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemProjection(
                    "startOfMinute_time",
                    new StartOfMinuteTimeProjection(
                        new LiteralTime(new TimeOnly(18, 30, 15, 250))
                    )
                ),
                new SelectItemProjection(
                    "startOfMinute_time_nullable",
                    new StartOfMinuteTimeNullableProjection(new LiteralTimeNullable())
                ),
                new SelectItemProjection(
                    "startOfMinute_datetime",
                    new StartOfMinuteDatetimeProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(
                                2024,
                                8,
                                15,
                                18,
                                30,
                                15,
                                250,
                                TimeSpan.Zero
                            )
                        )
                    )
                ),
                new SelectItemProjection(
                    "startOfMinute_datetime_nullable",
                    new StartOfMinuteDatetimeNullableProjection(
                        new LiteralDatetimeNullable()
                    )
                ),
                new SelectItemProjection(
                    "startOfMinute_datetime_in_zone",
                    new StartOfMinuteDatetimeProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemProjection(
                    "startOfMinute_datetime_nullable_in_zone",
                    new StartOfMinuteDatetimeNullableProjection(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemProjection(
                    "startOfMinute_datetime_nullable_by_zone",
                    new StartOfMinuteDatetimeNullableProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemProjection(
                    "startOfSecond_time",
                    new StartOfSecondTimeProjection(
                        new LiteralTime(new TimeOnly(18, 30, 15, 250))
                    )
                ),
                new SelectItemProjection(
                    "startOfSecond_time_nullable",
                    new StartOfSecondTimeNullableProjection(new LiteralTimeNullable())
                ),
                new SelectItemProjection(
                    "startOfSecond_datetime",
                    new StartOfSecondDatetimeProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(
                                2024,
                                8,
                                15,
                                18,
                                30,
                                15,
                                250,
                                TimeSpan.Zero
                            )
                        )
                    )
                ),
                new SelectItemProjection(
                    "startOfSecond_datetime_nullable",
                    new StartOfSecondDatetimeNullableProjection(
                        new LiteralDatetimeNullable()
                    )
                ),
                new SelectItemProjection(
                    "asString_string_of_integer",
                    new AsStringDecimalStringProjection(new LiteralInteger(7))
                ),
                new SelectItemProjection(
                    "asString_string_nullable_of_integer",
                    new AsStringDecimalStringNullableProjection(
                        new LiteralIntegerNullable()
                    )
                ),
                new SelectItemProjection(
                    "asString_string_of_decimal",
                    new AsStringDecimalStringProjection(new LiteralDecimal(2.5m))
                ),
                new SelectItemProjection(
                    "asString_string_nullable_of_decimal",
                    new AsStringDecimalStringNullableProjection(
                        new LiteralDecimalNullable()
                    )
                ),
                new SelectItemProjection(
                    "asString_string_of_boolean",
                    new AsStringBooleanStringProjection(new LiteralBoolean(true))
                ),
                new SelectItemProjection(
                    "asString_string_nullable_of_boolean",
                    new AsStringBooleanStringNullableProjection(
                        new LiteralBooleanNullable()
                    )
                ),
                new SelectItemProjection(
                    "asString_string_of_date",
                    new AsStringDateStringProjection(
                        new LiteralDate(new DateOnly(2024, 1, 31))
                    )
                ),
                new SelectItemProjection(
                    "asString_string_nullable_of_date",
                    new AsStringDateStringNullableProjection(new LiteralDateNullable())
                ),
                new SelectItemProjection(
                    "asString_string_of_time",
                    new AsStringTimeStringProjection(
                        new LiteralTime(new TimeOnly(8, 30, 0))
                    )
                ),
                new SelectItemProjection(
                    "asString_string_nullable_of_time",
                    new AsStringTimeStringNullableProjection(new LiteralTimeNullable())
                ),
                new SelectItemProjection(
                    "asString_string_of_datetime",
                    new AsStringDatetimeStringProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemProjection(
                    "asString_string_nullable_of_datetime",
                    new AsStringDatetimeStringNullableProjection(
                        new LiteralDatetimeNullable()
                    )
                ),
                new SelectItemProjection(
                    "asString_string_of_uuid",
                    new AsStringUuidStringProjection(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8]))
                    )
                ),
                new SelectItemProjection(
                    "asString_string_nullable_of_uuid",
                    new AsStringUuidStringNullableProjection(new LiteralUuidNullable())
                ),
                new SelectItemProjection(
                    "asDate_date",
                    new AsDateDatetimeDateProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemProjection(
                    "asDate_date_nullable",
                    new AsDateDatetimeDateNullableProjection(
                        new LiteralDatetimeNullable()
                    )
                ),
                new SelectItemProjection(
                    "asTime_time",
                    new AsTimeDatetimeTimeProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemProjection(
                    "asTime_time_nullable",
                    new AsTimeDatetimeTimeNullableProjection(
                        new LiteralDatetimeNullable()
                    )
                ),
                new SelectItemProjection(
                    "asDate_date_in_zone",
                    new AsDateDatetimeDateProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemProjection(
                    "asDate_date_nullable_in_zone",
                    new AsDateDatetimeDateNullableProjection(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemProjection(
                    "asDate_date_nullable_by_zone",
                    new AsDateDatetimeDateNullableProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemProjection(
                    "asDate_date_nullable_in_nullable_zone",
                    new AsDateDatetimeDateNullableProjection(
                        new LiteralDatetimeNullable(),
                        zone: new ParamStringNullable("zone")
                    )
                ),
                new SelectItemProjection(
                    "asTime_time_in_zone",
                    new AsTimeDatetimeTimeProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemProjection(
                    "asTime_time_nullable_in_zone",
                    new AsTimeDatetimeTimeNullableProjection(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemProjection(
                    "asTime_time_nullable_by_zone",
                    new AsTimeDatetimeTimeNullableProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemProjection(
                    "asTime_time_nullable_in_nullable_zone",
                    new AsTimeDatetimeTimeNullableProjection(
                        new LiteralDatetimeNullable(),
                        zone: new ParamStringNullable("zone")
                    )
                ),
                new SelectItemProjection(
                    "asDatetime_datetime",
                    new AsDatetimeDateDatetimeProjection(
                        new LiteralDate(new DateOnly(2024, 1, 31))
                    )
                ),
                new SelectItemProjection(
                    "asDatetime_datetime_nullable",
                    new AsDatetimeDateDatetimeNullableProjection(
                        new LiteralDateNullable()
                    )
                ),
                new SelectItemProjection(
                    "asDatetime_datetime_in_zone",
                    new AsDatetimeDateDatetimeProjection(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemProjection(
                    "asDatetime_datetime_nullable_in_zone",
                    new AsDatetimeDateDatetimeNullableProjection(
                        new LiteralDateNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemProjection(
                    "asDatetime_datetime_nullable_by_zone",
                    new AsDatetimeDateDatetimeNullableProjection(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemProjection(
                    "asInteger_integer_of_string",
                    new AsIntegerIntegerProjection(new LiteralString("7"))
                ),
                new SelectItemProjection(
                    "asInteger_integer_nullable_of_string",
                    new AsIntegerIntegerNullableProjection(new LiteralStringNullable())
                ),
                new SelectItemProjection(
                    "tryAsInteger_integer_nullable",
                    new TryAsIntegerProjection(new LiteralString("7"))
                ),
                new SelectItemProjection(
                    "tryAsInteger_integer_nullable_of_null",
                    new TryAsIntegerProjection(new LiteralStringNullable())
                ),
                new SelectItemProjection(
                    "asDecimal_decimal_of_string",
                    new AsDecimalStringDecimalProjection(new LiteralString("2.5"))
                ),
                new SelectItemProjection(
                    "asDecimal_decimal_nullable_of_string",
                    new AsDecimalStringDecimalNullableProjection(
                        new LiteralStringNullable()
                    )
                ),
                new SelectItemProjection(
                    "asDecimal_decimal_of_integer",
                    new AsDecimalIntegerDecimalProjection(new LiteralInteger(7))
                ),
                new SelectItemProjection(
                    "asDecimal_decimal_nullable_of_integer",
                    new AsDecimalIntegerDecimalNullableProjection(
                        new LiteralIntegerNullable()
                    )
                ),
                new SelectItemProjection(
                    "asNullable_integer_nullable",
                    new AsNullableIntegerNullableProjection(new LiteralInteger(7))
                ),
                new SelectItemProjection(
                    "asNullable_decimal_nullable",
                    new AsNullableDecimalNullableProjection(new LiteralDecimal(2.5m))
                ),
                new SelectItemProjection(
                    "asNullable_string_nullable",
                    new AsNullableStringNullableProjection(new LiteralString("a"))
                ),
                new SelectItemProjection(
                    "asNullable_boolean_nullable",
                    new AsNullableBooleanNullableProjection(new LiteralBoolean(true))
                ),
                new SelectItemProjection(
                    "asNullable_date_nullable",
                    new AsNullableDateNullableProjection(
                        new LiteralDate(new DateOnly(2024, 1, 31))
                    )
                ),
                new SelectItemProjection(
                    "asNullable_time_nullable",
                    new AsNullableTimeNullableProjection(
                        new LiteralTime(new TimeOnly(8, 30, 0))
                    )
                ),
                new SelectItemProjection(
                    "asNullable_datetime_nullable",
                    new AsNullableDatetimeNullableProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemProjection(
                    "asNullable_uuid_nullable",
                    new AsNullableUuidNullableProjection(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8]))
                    )
                ),
                new SelectItemProjection(
                    "tryAsDecimal_decimal_nullable",
                    new TryAsDecimalProjection(new LiteralString("2.5"))
                ),
                new SelectItemProjection(
                    "tryAsDecimal_decimal_nullable_of_null",
                    new TryAsDecimalProjection(new LiteralStringNullable())
                ),
                new SelectItemProjection(
                    "asBoolean_boolean_of_string",
                    new AsBooleanBooleanProjection(new LiteralString("true"))
                ),
                new SelectItemProjection(
                    "asBoolean_boolean_nullable_of_string",
                    new AsBooleanBooleanNullableProjection(new LiteralStringNullable())
                ),
                new SelectItemProjection(
                    "tryAsBoolean_boolean_nullable",
                    new TryAsBooleanProjection(new LiteralString("true"))
                ),
                new SelectItemProjection(
                    "tryAsBoolean_boolean_nullable_of_null",
                    new TryAsBooleanProjection(new LiteralStringNullable())
                ),
                new SelectItemProjection(
                    "asDate_date_of_string",
                    new AsDateStringDateProjection(new LiteralString("2024-01-31"))
                ),
                new SelectItemProjection(
                    "asDate_date_nullable_of_string",
                    new AsDateStringDateNullableProjection(new LiteralStringNullable())
                ),
                new SelectItemProjection(
                    "tryAsDate_date_nullable",
                    new TryAsDateProjection(new LiteralString("2024-01-31"))
                ),
                new SelectItemProjection(
                    "tryAsDate_date_nullable_of_null",
                    new TryAsDateProjection(new LiteralStringNullable())
                ),
                new SelectItemProjection(
                    "asTime_time_of_string",
                    new AsTimeStringTimeProjection(new LiteralString("08:30:00"))
                ),
                new SelectItemProjection(
                    "asTime_time_nullable_of_string",
                    new AsTimeStringTimeNullableProjection(new LiteralStringNullable())
                ),
                new SelectItemProjection(
                    "tryAsTime_time_nullable",
                    new TryAsTimeProjection(new LiteralString("08:30:00"))
                ),
                new SelectItemProjection(
                    "tryAsTime_time_nullable_of_null",
                    new TryAsTimeProjection(new LiteralStringNullable())
                ),
                new SelectItemProjection(
                    "asDatetime_datetime_of_string",
                    new AsDatetimeStringDatetimeProjection(
                        new LiteralString("2024-01-31T08:30:00Z")
                    )
                ),
                new SelectItemProjection(
                    "asDatetime_datetime_nullable_of_string",
                    new AsDatetimeStringDatetimeNullableProjection(
                        new LiteralStringNullable()
                    )
                ),
                new SelectItemProjection(
                    "tryAsDatetime_datetime_nullable",
                    new TryAsDatetimeProjection(new LiteralString("2024-01-31T08:30:00Z"))
                ),
                new SelectItemProjection(
                    "tryAsDatetime_datetime_nullable_of_null",
                    new TryAsDatetimeProjection(new LiteralStringNullable())
                ),
                new SelectItemProjection(
                    "asUuid_uuid_of_string",
                    new AsUuidUuidProjection(
                        new LiteralString("3f2a6c1e-8b4d-4e2a-9c1f-1a2b3c4d5e6f")
                    )
                ),
                new SelectItemProjection(
                    "asUuid_uuid_nullable_of_string",
                    new AsUuidUuidNullableProjection(new LiteralStringNullable())
                ),
                new SelectItemProjection(
                    "tryAsUuid_uuid_nullable",
                    new TryAsUuidProjection(
                        new LiteralString("3f2a6c1e-8b4d-4e2a-9c1f-1a2b3c4d5e6f")
                    )
                ),
                new SelectItemProjection(
                    "tryAsUuid_uuid_nullable_of_null",
                    new TryAsUuidProjection(new LiteralStringNullable())
                ),
                new SelectItemProjection(
                    "and_boolean",
                    new AndProjection([
                        new LiteralBoolean(true),
                        new LiteralBoolean(false),
                    ])
                ),
                new SelectItemProjection(
                    "or_boolean",
                    new OrProjection([
                        new LiteralBoolean(true),
                        new LiteralBoolean(false),
                    ])
                ),
                new SelectItemProjection(
                    "not_boolean",
                    new NotProjection(new LiteralBoolean(true))
                ),
                new SelectItemProjection(
                    "equal_boolean_decimal",
                    new EqualDecimalProjection(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m)
                    )
                ),
                new SelectItemProjection(
                    "equal_boolean_string",
                    new EqualStringProjection(
                        new LiteralString("a"),
                        new LiteralString("b")
                    )
                ),
                new SelectItemProjection(
                    "equal_boolean_boolean",
                    new EqualBooleanProjection(
                        new LiteralBoolean(true),
                        new LiteralBoolean(false)
                    )
                ),
                new SelectItemProjection(
                    "equal_boolean_date",
                    new EqualDateProjection(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDate(new DateOnly(2024, 3, 1))
                    )
                ),
                new SelectItemProjection(
                    "equal_boolean_time",
                    new EqualTimeProjection(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralTime(new TimeOnly(17, 45, 30, 500))
                    )
                ),
                new SelectItemProjection(
                    "equal_boolean_datetime",
                    new EqualDatetimeProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemProjection(
                    "equal_boolean_uuid",
                    new EqualUuidProjection(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralUuid(new Guid(2, 0, 0, new byte[8]))
                    )
                ),
                new SelectItemProjection(
                    "notEqual_boolean_decimal",
                    new NotEqualDecimalProjection(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m)
                    )
                ),
                new SelectItemProjection(
                    "notEqual_boolean_string",
                    new NotEqualStringProjection(
                        new LiteralString("a"),
                        new LiteralString("b")
                    )
                ),
                new SelectItemProjection(
                    "notEqual_boolean_boolean",
                    new NotEqualBooleanProjection(
                        new LiteralBoolean(true),
                        new LiteralBoolean(false)
                    )
                ),
                new SelectItemProjection(
                    "notEqual_boolean_date",
                    new NotEqualDateProjection(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDate(new DateOnly(2024, 3, 1))
                    )
                ),
                new SelectItemProjection(
                    "notEqual_boolean_time",
                    new NotEqualTimeProjection(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralTime(new TimeOnly(17, 45, 30, 500))
                    )
                ),
                new SelectItemProjection(
                    "notEqual_boolean_datetime",
                    new NotEqualDatetimeProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemProjection(
                    "notEqual_boolean_uuid",
                    new NotEqualUuidProjection(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralUuid(new Guid(2, 0, 0, new byte[8]))
                    )
                ),
                new SelectItemProjection(
                    "greaterThan_boolean_decimal",
                    new GreaterThanDecimalProjection(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m)
                    )
                ),
                new SelectItemProjection(
                    "greaterThan_boolean_string",
                    new GreaterThanStringProjection(
                        new LiteralString("a"),
                        new LiteralString("b")
                    )
                ),
                new SelectItemProjection(
                    "greaterThan_boolean_boolean",
                    new GreaterThanBooleanProjection(
                        new LiteralBoolean(true),
                        new LiteralBoolean(false)
                    )
                ),
                new SelectItemProjection(
                    "greaterThan_boolean_date",
                    new GreaterThanDateProjection(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDate(new DateOnly(2024, 3, 1))
                    )
                ),
                new SelectItemProjection(
                    "greaterThan_boolean_time",
                    new GreaterThanTimeProjection(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralTime(new TimeOnly(17, 45, 30, 500))
                    )
                ),
                new SelectItemProjection(
                    "greaterThan_boolean_datetime",
                    new GreaterThanDatetimeProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemProjection(
                    "greaterThan_boolean_uuid",
                    new GreaterThanUuidProjection(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralUuid(new Guid(2, 0, 0, new byte[8]))
                    )
                ),
                new SelectItemProjection(
                    "lessThan_boolean_decimal",
                    new LessThanDecimalProjection(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m)
                    )
                ),
                new SelectItemProjection(
                    "lessThan_boolean_string",
                    new LessThanStringProjection(
                        new LiteralString("a"),
                        new LiteralString("b")
                    )
                ),
                new SelectItemProjection(
                    "lessThan_boolean_boolean",
                    new LessThanBooleanProjection(
                        new LiteralBoolean(true),
                        new LiteralBoolean(false)
                    )
                ),
                new SelectItemProjection(
                    "lessThan_boolean_date",
                    new LessThanDateProjection(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDate(new DateOnly(2024, 3, 1))
                    )
                ),
                new SelectItemProjection(
                    "lessThan_boolean_time",
                    new LessThanTimeProjection(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralTime(new TimeOnly(17, 45, 30, 500))
                    )
                ),
                new SelectItemProjection(
                    "lessThan_boolean_datetime",
                    new LessThanDatetimeProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemProjection(
                    "lessThan_boolean_uuid",
                    new LessThanUuidProjection(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralUuid(new Guid(2, 0, 0, new byte[8]))
                    )
                ),
                new SelectItemProjection(
                    "greaterThanOrEqual_boolean_decimal",
                    new GreaterThanOrEqualDecimalProjection(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m)
                    )
                ),
                new SelectItemProjection(
                    "greaterThanOrEqual_boolean_string",
                    new GreaterThanOrEqualStringProjection(
                        new LiteralString("a"),
                        new LiteralString("b")
                    )
                ),
                new SelectItemProjection(
                    "greaterThanOrEqual_boolean_boolean",
                    new GreaterThanOrEqualBooleanProjection(
                        new LiteralBoolean(true),
                        new LiteralBoolean(false)
                    )
                ),
                new SelectItemProjection(
                    "greaterThanOrEqual_boolean_date",
                    new GreaterThanOrEqualDateProjection(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDate(new DateOnly(2024, 3, 1))
                    )
                ),
                new SelectItemProjection(
                    "greaterThanOrEqual_boolean_time",
                    new GreaterThanOrEqualTimeProjection(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralTime(new TimeOnly(17, 45, 30, 500))
                    )
                ),
                new SelectItemProjection(
                    "greaterThanOrEqual_boolean_datetime",
                    new GreaterThanOrEqualDatetimeProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemProjection(
                    "greaterThanOrEqual_boolean_uuid",
                    new GreaterThanOrEqualUuidProjection(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralUuid(new Guid(2, 0, 0, new byte[8]))
                    )
                ),
                new SelectItemProjection(
                    "lessThanOrEqual_boolean_decimal",
                    new LessThanOrEqualDecimalProjection(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m)
                    )
                ),
                new SelectItemProjection(
                    "lessThanOrEqual_boolean_string",
                    new LessThanOrEqualStringProjection(
                        new LiteralString("a"),
                        new LiteralString("b")
                    )
                ),
                new SelectItemProjection(
                    "lessThanOrEqual_boolean_boolean",
                    new LessThanOrEqualBooleanProjection(
                        new LiteralBoolean(true),
                        new LiteralBoolean(false)
                    )
                ),
                new SelectItemProjection(
                    "lessThanOrEqual_boolean_date",
                    new LessThanOrEqualDateProjection(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDate(new DateOnly(2024, 3, 1))
                    )
                ),
                new SelectItemProjection(
                    "lessThanOrEqual_boolean_time",
                    new LessThanOrEqualTimeProjection(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralTime(new TimeOnly(17, 45, 30, 500))
                    )
                ),
                new SelectItemProjection(
                    "lessThanOrEqual_boolean_datetime",
                    new LessThanOrEqualDatetimeProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemProjection(
                    "lessThanOrEqual_boolean_uuid",
                    new LessThanOrEqualUuidProjection(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralUuid(new Guid(2, 0, 0, new byte[8]))
                    )
                ),
                new SelectItemProjection(
                    "in_boolean_decimal",
                    new InDecimalProjection(
                        new LiteralDecimal(2.5m),
                        new ListLiteralDecimal([2.5m, 0.1m])
                    )
                ),
                new SelectItemProjection(
                    "in_boolean_string",
                    new InStringProjection(
                        new LiteralString("a"),
                        new ListLiteralString(["a", "b"])
                    )
                ),
                new SelectItemProjection(
                    "in_boolean_boolean",
                    new InBooleanProjection(
                        new LiteralBoolean(true),
                        new ListLiteralBoolean([true, false])
                    )
                ),
                new SelectItemProjection(
                    "in_boolean_date",
                    new InDateProjection(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new ListLiteralDate([
                            new DateOnly(2024, 1, 31),
                            new DateOnly(2024, 3, 1),
                        ])
                    )
                ),
                new SelectItemProjection(
                    "in_boolean_time",
                    new InTimeProjection(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new ListLiteralTime([
                            new TimeOnly(8, 30, 0),
                            new TimeOnly(17, 45, 30, 500),
                        ])
                    )
                ),
                new SelectItemProjection(
                    "in_boolean_datetime",
                    new InDatetimeProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new ListLiteralDatetime([
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero),
                            new DateTimeOffset(
                                2024,
                                3,
                                1,
                                17,
                                45,
                                30,
                                500,
                                TimeSpan.Zero
                            ),
                        ])
                    )
                ),
                new SelectItemProjection(
                    "in_boolean_uuid",
                    new InUuidProjection(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new ListLiteralUuid([
                            new Guid(1, 0, 0, new byte[8]),
                            new Guid(2, 0, 0, new byte[8]),
                        ])
                    )
                ),
                new SelectItemProjection(
                    "if_integer",
                    new IfIntegerProjection(
                        new LiteralInteger(7),
                        new LiteralInteger(-3),
                        new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "if_integer_nullable",
                    new IfIntegerNullableProjection(
                        new LiteralIntegerNullable(),
                        new LiteralInteger(7),
                        new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "coalesce_integer",
                    new CoalesceIntegerProjection([
                        new LiteralIntegerNullable(),
                        new LiteralInteger(7),
                    ])
                ),
                new SelectItemProjection(
                    "coalesce_integer_nullable",
                    new CoalesceIntegerNullableProjection([
                        new LiteralIntegerNullable(),
                        new LiteralIntegerNullable(),
                    ])
                ),
                new SelectItemProjection(
                    "if_decimal",
                    new IfDecimalProjection(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m),
                        new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "if_decimal_nullable",
                    new IfDecimalNullableProjection(
                        new LiteralDecimalNullable(),
                        new LiteralDecimal(2.5m),
                        new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "coalesce_decimal",
                    new CoalesceDecimalProjection([
                        new LiteralDecimalNullable(),
                        new LiteralDecimal(2.5m),
                    ])
                ),
                new SelectItemProjection(
                    "coalesce_decimal_nullable",
                    new CoalesceDecimalNullableProjection([
                        new LiteralDecimalNullable(),
                        new LiteralDecimalNullable(),
                    ])
                ),
                new SelectItemProjection(
                    "if_string",
                    new IfStringProjection(
                        new LiteralString("a"),
                        new LiteralString("b"),
                        new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "if_string_nullable",
                    new IfStringNullableProjection(
                        new LiteralStringNullable(),
                        new LiteralString("a"),
                        new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "coalesce_string",
                    new CoalesceStringProjection([
                        new LiteralStringNullable(),
                        new LiteralString("a"),
                    ])
                ),
                new SelectItemProjection(
                    "coalesce_string_nullable",
                    new CoalesceStringNullableProjection([
                        new LiteralStringNullable(),
                        new LiteralStringNullable(),
                    ])
                ),
                new SelectItemProjection(
                    "if_boolean",
                    new IfBooleanProjection(
                        new LiteralBoolean(true),
                        new LiteralBoolean(false),
                        new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "if_boolean_nullable",
                    new IfBooleanNullableProjection(
                        new LiteralBooleanNullable(),
                        new LiteralBoolean(true),
                        new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "coalesce_boolean",
                    new CoalesceBooleanProjection([
                        new LiteralBooleanNullable(),
                        new LiteralBoolean(true),
                    ])
                ),
                new SelectItemProjection(
                    "coalesce_boolean_nullable",
                    new CoalesceBooleanNullableProjection([
                        new LiteralBooleanNullable(),
                        new LiteralBooleanNullable(),
                    ])
                ),
                new SelectItemProjection(
                    "if_date",
                    new IfDateProjection(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDate(new DateOnly(2024, 3, 1)),
                        new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "if_date_nullable",
                    new IfDateNullableProjection(
                        new LiteralDateNullable(),
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "coalesce_date",
                    new CoalesceDateProjection([
                        new LiteralDateNullable(),
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                    ])
                ),
                new SelectItemProjection(
                    "coalesce_date_nullable",
                    new CoalesceDateNullableProjection([
                        new LiteralDateNullable(),
                        new LiteralDateNullable(),
                    ])
                ),
                new SelectItemProjection(
                    "if_time",
                    new IfTimeProjection(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralTime(new TimeOnly(17, 45, 30, 500)),
                        new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "if_time_nullable",
                    new IfTimeNullableProjection(
                        new LiteralTimeNullable(),
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "coalesce_time",
                    new CoalesceTimeProjection([
                        new LiteralTimeNullable(),
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                    ])
                ),
                new SelectItemProjection(
                    "coalesce_time_nullable",
                    new CoalesceTimeNullableProjection([
                        new LiteralTimeNullable(),
                        new LiteralTimeNullable(),
                    ])
                ),
                new SelectItemProjection(
                    "if_datetime",
                    new IfDatetimeProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        ),
                        new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "if_datetime_nullable",
                    new IfDatetimeNullableProjection(
                        new LiteralDatetimeNullable(),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "coalesce_datetime",
                    new CoalesceDatetimeProjection([
                        new LiteralDatetimeNullable(),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                    ])
                ),
                new SelectItemProjection(
                    "coalesce_datetime_nullable",
                    new CoalesceDatetimeNullableProjection([
                        new LiteralDatetimeNullable(),
                        new LiteralDatetimeNullable(),
                    ])
                ),
                new SelectItemProjection(
                    "if_uuid",
                    new IfUuidProjection(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralUuid(new Guid(2, 0, 0, new byte[8])),
                        new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "if_uuid_nullable",
                    new IfUuidNullableProjection(
                        new LiteralUuidNullable(),
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "coalesce_uuid",
                    new CoalesceUuidProjection([
                        new LiteralUuidNullable(),
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                    ])
                ),
                new SelectItemProjection(
                    "coalesce_uuid_nullable",
                    new CoalesceUuidNullableProjection([
                        new LiteralUuidNullable(),
                        new LiteralUuidNullable(),
                    ])
                ),
                new SelectItemProjection(
                    "nullIfEqual_integer_nullable",
                    new NullIfEqualIntegerNullableProjection(
                        new LiteralInteger(7),
                        new LiteralInteger(-3)
                    )
                ),
                new SelectItemProjection(
                    "nullIfEqual_decimal_nullable",
                    new NullIfEqualDecimalNullableProjection(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m)
                    )
                ),
                new SelectItemProjection(
                    "nullIfEqual_string_nullable",
                    new NullIfEqualStringNullableProjection(
                        new LiteralString("a"),
                        new LiteralString("b")
                    )
                ),
                new SelectItemProjection(
                    "nullIfEqual_boolean_nullable",
                    new NullIfEqualBooleanNullableProjection(
                        new LiteralBoolean(true),
                        new LiteralBoolean(false)
                    )
                ),
                new SelectItemProjection(
                    "nullIfEqual_date_nullable",
                    new NullIfEqualDateNullableProjection(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDate(new DateOnly(2024, 3, 1))
                    )
                ),
                new SelectItemProjection(
                    "nullIfEqual_time_nullable",
                    new NullIfEqualTimeNullableProjection(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralTime(new TimeOnly(17, 45, 30, 500))
                    )
                ),
                new SelectItemProjection(
                    "nullIfEqual_datetime_nullable",
                    new NullIfEqualDatetimeNullableProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemProjection(
                    "nullIfEqual_uuid_nullable",
                    new NullIfEqualUuidNullableProjection(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralUuid(new Guid(2, 0, 0, new byte[8]))
                    )
                ),
                new SelectItemProjection(
                    "nullIfEqual_integer_nullable_decimal_right",
                    new NullIfEqualIntegerNullableProjection(
                        new LiteralInteger(7),
                        new LiteralDecimal(2.5m)
                    )
                ),
                new SelectItemProjection(
                    "nullIfEqual_decimal_nullable_integer_right",
                    new NullIfEqualDecimalNullableProjection(
                        new LiteralDecimal(2.5m),
                        new LiteralInteger(7)
                    )
                ),
                new SelectItemProjection(
                    "nullIfEqual_string_nullable_null_left",
                    new NullIfEqualStringNullableProjection(
                        new LiteralStringNullable(),
                        new LiteralString("a")
                    )
                ),
                new SelectItemProjection(
                    "nullIfEqual_string_nullable_null_right",
                    new NullIfEqualStringNullableProjection(
                        new LiteralString("a"),
                        new LiteralStringNullable()
                    )
                ),
                new SelectItemProjection(
                    "greatest_integer",
                    new GreatestIntegerProjection([
                        new LiteralInteger(7),
                        new LiteralInteger(-3),
                    ])
                ),
                new SelectItemProjection(
                    "greatest_integer_nullable",
                    new GreatestIntegerNullableProjection([
                        new LiteralInteger(7),
                        new LiteralIntegerNullable(),
                        new LiteralInteger(-3),
                    ])
                ),
                new SelectItemProjection(
                    "greatest_decimal",
                    new GreatestDecimalProjection([
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m),
                    ])
                ),
                new SelectItemProjection(
                    "greatest_decimal_nullable",
                    new GreatestDecimalNullableProjection([
                        new LiteralDecimal(2.5m),
                        new LiteralDecimalNullable(),
                        new LiteralDecimal(0.1m),
                    ])
                ),
                new SelectItemProjection(
                    "greatest_string",
                    new GreatestStringProjection([
                        new LiteralString("a"),
                        new LiteralString("b"),
                    ])
                ),
                new SelectItemProjection(
                    "greatest_string_nullable",
                    new GreatestStringNullableProjection([
                        new LiteralString("a"),
                        new LiteralStringNullable(),
                        new LiteralString("b"),
                    ])
                ),
                new SelectItemProjection(
                    "greatest_boolean",
                    new GreatestBooleanProjection([
                        new LiteralBoolean(true),
                        new LiteralBoolean(false),
                    ])
                ),
                new SelectItemProjection(
                    "greatest_boolean_nullable",
                    new GreatestBooleanNullableProjection([
                        new LiteralBoolean(true),
                        new LiteralBooleanNullable(),
                        new LiteralBoolean(false),
                    ])
                ),
                new SelectItemProjection(
                    "greatest_date",
                    new GreatestDateProjection([
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDate(new DateOnly(2024, 3, 1)),
                    ])
                ),
                new SelectItemProjection(
                    "greatest_date_nullable",
                    new GreatestDateNullableProjection([
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDateNullable(),
                        new LiteralDate(new DateOnly(2024, 3, 1)),
                    ])
                ),
                new SelectItemProjection(
                    "greatest_time",
                    new GreatestTimeProjection([
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralTime(new TimeOnly(17, 45, 30, 500)),
                    ])
                ),
                new SelectItemProjection(
                    "greatest_time_nullable",
                    new GreatestTimeNullableProjection([
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralTimeNullable(),
                        new LiteralTime(new TimeOnly(17, 45, 30, 500)),
                    ])
                ),
                new SelectItemProjection(
                    "greatest_datetime",
                    new GreatestDatetimeProjection([
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        ),
                    ])
                ),
                new SelectItemProjection(
                    "greatest_datetime_nullable",
                    new GreatestDatetimeNullableProjection([
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDatetimeNullable(),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        ),
                    ])
                ),
                new SelectItemProjection(
                    "greatest_uuid",
                    new GreatestUuidProjection([
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralUuid(new Guid(2, 0, 0, new byte[8])),
                    ])
                ),
                new SelectItemProjection(
                    "greatest_uuid_nullable",
                    new GreatestUuidNullableProjection([
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralUuidNullable(),
                        new LiteralUuid(new Guid(2, 0, 0, new byte[8])),
                    ])
                ),
                new SelectItemProjection(
                    "greatest_decimal_of_integer_and_decimal",
                    new GreatestDecimalProjection([
                        new LiteralInteger(7),
                        new LiteralDecimal(2.5m),
                    ])
                ),
                new SelectItemProjection(
                    "least_integer",
                    new LeastIntegerProjection([
                        new LiteralInteger(7),
                        new LiteralInteger(-3),
                    ])
                ),
                new SelectItemProjection(
                    "least_integer_nullable",
                    new LeastIntegerNullableProjection([
                        new LiteralInteger(7),
                        new LiteralIntegerNullable(),
                        new LiteralInteger(-3),
                    ])
                ),
                new SelectItemProjection(
                    "least_decimal",
                    new LeastDecimalProjection([
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m),
                    ])
                ),
                new SelectItemProjection(
                    "least_decimal_nullable",
                    new LeastDecimalNullableProjection([
                        new LiteralDecimal(2.5m),
                        new LiteralDecimalNullable(),
                        new LiteralDecimal(0.1m),
                    ])
                ),
                new SelectItemProjection(
                    "least_string",
                    new LeastStringProjection([
                        new LiteralString("a"),
                        new LiteralString("b"),
                    ])
                ),
                new SelectItemProjection(
                    "least_string_nullable",
                    new LeastStringNullableProjection([
                        new LiteralString("a"),
                        new LiteralStringNullable(),
                        new LiteralString("b"),
                    ])
                ),
                new SelectItemProjection(
                    "least_boolean",
                    new LeastBooleanProjection([
                        new LiteralBoolean(true),
                        new LiteralBoolean(false),
                    ])
                ),
                new SelectItemProjection(
                    "least_boolean_nullable",
                    new LeastBooleanNullableProjection([
                        new LiteralBoolean(true),
                        new LiteralBooleanNullable(),
                        new LiteralBoolean(false),
                    ])
                ),
                new SelectItemProjection(
                    "least_date",
                    new LeastDateProjection([
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDate(new DateOnly(2024, 3, 1)),
                    ])
                ),
                new SelectItemProjection(
                    "least_date_nullable",
                    new LeastDateNullableProjection([
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDateNullable(),
                        new LiteralDate(new DateOnly(2024, 3, 1)),
                    ])
                ),
                new SelectItemProjection(
                    "least_time",
                    new LeastTimeProjection([
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralTime(new TimeOnly(17, 45, 30, 500)),
                    ])
                ),
                new SelectItemProjection(
                    "least_time_nullable",
                    new LeastTimeNullableProjection([
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralTimeNullable(),
                        new LiteralTime(new TimeOnly(17, 45, 30, 500)),
                    ])
                ),
                new SelectItemProjection(
                    "least_datetime",
                    new LeastDatetimeProjection([
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        ),
                    ])
                ),
                new SelectItemProjection(
                    "least_datetime_nullable",
                    new LeastDatetimeNullableProjection([
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDatetimeNullable(),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        ),
                    ])
                ),
                new SelectItemProjection(
                    "least_uuid",
                    new LeastUuidProjection([
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralUuid(new Guid(2, 0, 0, new byte[8])),
                    ])
                ),
                new SelectItemProjection(
                    "least_uuid_nullable",
                    new LeastUuidNullableProjection([
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralUuidNullable(),
                        new LiteralUuid(new Guid(2, 0, 0, new byte[8])),
                    ])
                ),
                new SelectItemProjection(
                    "least_decimal_of_integer_and_decimal",
                    new LeastDecimalProjection([
                        new LiteralInteger(7),
                        new LiteralDecimal(2.5m),
                    ])
                ),
                new SelectItemProjection("count_integer", new CountProjection()),
                new SelectItemProjection(
                    "count_integer_where",
                    new CountProjection(predicate: new LiteralBoolean(true))
                ),
                new SelectItemProjection(
                    "countDistinct_integer_of_integer",
                    new CountDistinctProjection(new LiteralInteger(7))
                ),
                new SelectItemProjection(
                    "countDistinct_integer_of_decimal",
                    new CountDistinctProjection(new LiteralDecimal(2.5m))
                ),
                new SelectItemProjection(
                    "countDistinct_integer_of_string",
                    new CountDistinctProjection(new LiteralString("a"))
                ),
                new SelectItemProjection(
                    "countDistinct_integer_of_boolean",
                    new CountDistinctProjection(new LiteralBoolean(true))
                ),
                new SelectItemProjection(
                    "countDistinct_integer_of_date",
                    new CountDistinctProjection(
                        new LiteralDate(new DateOnly(2024, 1, 31))
                    )
                ),
                new SelectItemProjection(
                    "countDistinct_integer_of_time",
                    new CountDistinctProjection(new LiteralTime(new TimeOnly(8, 30, 0)))
                ),
                new SelectItemProjection(
                    "countDistinct_integer_of_datetime",
                    new CountDistinctProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemProjection(
                    "countDistinct_integer_of_uuid",
                    new CountDistinctProjection(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8]))
                    )
                ),
                new SelectItemProjection(
                    "countDistinct_integer_of_integer_null",
                    new CountDistinctProjection(new LiteralIntegerNullable())
                ),
                new SelectItemProjection(
                    "countDistinct_integer_of_string_null",
                    new CountDistinctProjection(new LiteralStringNullable())
                ),
                new SelectItemProjection(
                    "countDistinct_integer_of_uuid_null",
                    new CountDistinctProjection(new LiteralUuidNullable())
                ),
                new SelectItemProjection(
                    "countDistinct_integer_of_decimal_where",
                    new CountDistinctProjection(
                        new LiteralDecimal(2.5m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "sum_integer_of_integer",
                    new SumIntegerProjection(new LiteralInteger(7))
                ),
                new SelectItemProjection(
                    "sum_integer_of_integer_null",
                    new SumIntegerProjection(new LiteralIntegerNullable())
                ),
                new SelectItemProjection(
                    "sum_integer_of_integer_where",
                    new SumIntegerProjection(
                        new LiteralInteger(7),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "sum_decimal_of_decimal",
                    new SumDecimalProjection(new LiteralDecimal(2.5m))
                ),
                new SelectItemProjection(
                    "sum_decimal_of_decimal_null",
                    new SumDecimalProjection(new LiteralDecimalNullable())
                ),
                new SelectItemProjection(
                    "sum_decimal_of_decimal_where",
                    new SumDecimalProjection(
                        new LiteralDecimal(2.5m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "average_decimal_of_integer",
                    new AverageDecimalProjection(new LiteralInteger(7))
                ),
                new SelectItemProjection(
                    "average_decimal_nullable_of_integer_null",
                    new AverageDecimalNullableProjection(new LiteralIntegerNullable())
                ),
                new SelectItemProjection(
                    "average_decimal_nullable_of_integer_where",
                    new AverageDecimalNullableProjection(
                        new LiteralInteger(7),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "average_decimal_of_decimal",
                    new AverageDecimalProjection(new LiteralDecimal(2.5m))
                ),
                new SelectItemProjection(
                    "average_decimal_nullable_of_decimal_null",
                    new AverageDecimalNullableProjection(new LiteralDecimalNullable())
                ),
                new SelectItemProjection(
                    "average_decimal_nullable_of_decimal_where",
                    new AverageDecimalNullableProjection(
                        new LiteralDecimal(2.5m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "average_date_of_date",
                    new AverageDateProjection(new LiteralDate(new DateOnly(2024, 1, 31)))
                ),
                new SelectItemProjection(
                    "average_date_nullable_of_date_null",
                    new AverageDateNullableProjection(new LiteralDateNullable())
                ),
                new SelectItemProjection(
                    "average_date_nullable_of_date_where",
                    new AverageDateNullableProjection(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "average_time_of_time",
                    new AverageTimeProjection(new LiteralTime(new TimeOnly(8, 30, 0)))
                ),
                new SelectItemProjection(
                    "average_time_nullable_of_time_null",
                    new AverageTimeNullableProjection(new LiteralTimeNullable())
                ),
                new SelectItemProjection(
                    "average_time_nullable_of_time_where",
                    new AverageTimeNullableProjection(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "average_datetime_of_datetime",
                    new AverageDatetimeProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemProjection(
                    "average_datetime_nullable_of_datetime_null",
                    new AverageDatetimeNullableProjection(new LiteralDatetimeNullable())
                ),
                new SelectItemProjection(
                    "average_datetime_nullable_of_datetime_where",
                    new AverageDatetimeNullableProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "min_integer_of_integer",
                    new MinIntegerProjection(new LiteralInteger(7))
                ),
                new SelectItemProjection(
                    "min_integer_nullable_of_integer_null",
                    new MinIntegerNullableProjection(new LiteralIntegerNullable())
                ),
                new SelectItemProjection(
                    "min_integer_nullable_of_integer_where",
                    new MinIntegerNullableProjection(
                        new LiteralInteger(7),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "min_decimal_of_decimal",
                    new MinDecimalProjection(new LiteralDecimal(2.5m))
                ),
                new SelectItemProjection(
                    "min_decimal_nullable_of_decimal_null",
                    new MinDecimalNullableProjection(new LiteralDecimalNullable())
                ),
                new SelectItemProjection(
                    "min_decimal_nullable_of_decimal_where",
                    new MinDecimalNullableProjection(
                        new LiteralDecimal(2.5m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "min_string_of_string",
                    new MinStringProjection(new LiteralString("a"))
                ),
                new SelectItemProjection(
                    "min_string_nullable_of_string_null",
                    new MinStringNullableProjection(new LiteralStringNullable())
                ),
                new SelectItemProjection(
                    "min_string_nullable_of_string_where",
                    new MinStringNullableProjection(
                        new LiteralString("a"),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "min_boolean_of_boolean",
                    new MinBooleanProjection(new LiteralBoolean(true))
                ),
                new SelectItemProjection(
                    "min_boolean_nullable_of_boolean_null",
                    new MinBooleanNullableProjection(new LiteralBooleanNullable())
                ),
                new SelectItemProjection(
                    "min_boolean_nullable_of_boolean_where",
                    new MinBooleanNullableProjection(
                        new LiteralBoolean(true),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "min_date_of_date",
                    new MinDateProjection(new LiteralDate(new DateOnly(2024, 1, 31)))
                ),
                new SelectItemProjection(
                    "min_date_nullable_of_date_null",
                    new MinDateNullableProjection(new LiteralDateNullable())
                ),
                new SelectItemProjection(
                    "min_date_nullable_of_date_where",
                    new MinDateNullableProjection(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "min_time_of_time",
                    new MinTimeProjection(new LiteralTime(new TimeOnly(8, 30, 0)))
                ),
                new SelectItemProjection(
                    "min_time_nullable_of_time_null",
                    new MinTimeNullableProjection(new LiteralTimeNullable())
                ),
                new SelectItemProjection(
                    "min_time_nullable_of_time_where",
                    new MinTimeNullableProjection(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "min_datetime_of_datetime",
                    new MinDatetimeProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemProjection(
                    "min_datetime_nullable_of_datetime_null",
                    new MinDatetimeNullableProjection(new LiteralDatetimeNullable())
                ),
                new SelectItemProjection(
                    "min_datetime_nullable_of_datetime_where",
                    new MinDatetimeNullableProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "min_uuid_of_uuid",
                    new MinUuidProjection(new LiteralUuid(new Guid(1, 0, 0, new byte[8])))
                ),
                new SelectItemProjection(
                    "min_uuid_nullable_of_uuid_null",
                    new MinUuidNullableProjection(new LiteralUuidNullable())
                ),
                new SelectItemProjection(
                    "min_uuid_nullable_of_uuid_where",
                    new MinUuidNullableProjection(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "max_integer_of_integer",
                    new MaxIntegerProjection(new LiteralInteger(7))
                ),
                new SelectItemProjection(
                    "max_integer_nullable_of_integer_null",
                    new MaxIntegerNullableProjection(new LiteralIntegerNullable())
                ),
                new SelectItemProjection(
                    "max_integer_nullable_of_integer_where",
                    new MaxIntegerNullableProjection(
                        new LiteralInteger(7),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "max_decimal_of_decimal",
                    new MaxDecimalProjection(new LiteralDecimal(2.5m))
                ),
                new SelectItemProjection(
                    "max_decimal_nullable_of_decimal_null",
                    new MaxDecimalNullableProjection(new LiteralDecimalNullable())
                ),
                new SelectItemProjection(
                    "max_decimal_nullable_of_decimal_where",
                    new MaxDecimalNullableProjection(
                        new LiteralDecimal(2.5m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "max_string_of_string",
                    new MaxStringProjection(new LiteralString("a"))
                ),
                new SelectItemProjection(
                    "max_string_nullable_of_string_null",
                    new MaxStringNullableProjection(new LiteralStringNullable())
                ),
                new SelectItemProjection(
                    "max_string_nullable_of_string_where",
                    new MaxStringNullableProjection(
                        new LiteralString("a"),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "max_boolean_of_boolean",
                    new MaxBooleanProjection(new LiteralBoolean(true))
                ),
                new SelectItemProjection(
                    "max_boolean_nullable_of_boolean_null",
                    new MaxBooleanNullableProjection(new LiteralBooleanNullable())
                ),
                new SelectItemProjection(
                    "max_boolean_nullable_of_boolean_where",
                    new MaxBooleanNullableProjection(
                        new LiteralBoolean(true),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "max_date_of_date",
                    new MaxDateProjection(new LiteralDate(new DateOnly(2024, 1, 31)))
                ),
                new SelectItemProjection(
                    "max_date_nullable_of_date_null",
                    new MaxDateNullableProjection(new LiteralDateNullable())
                ),
                new SelectItemProjection(
                    "max_date_nullable_of_date_where",
                    new MaxDateNullableProjection(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "max_time_of_time",
                    new MaxTimeProjection(new LiteralTime(new TimeOnly(8, 30, 0)))
                ),
                new SelectItemProjection(
                    "max_time_nullable_of_time_null",
                    new MaxTimeNullableProjection(new LiteralTimeNullable())
                ),
                new SelectItemProjection(
                    "max_time_nullable_of_time_where",
                    new MaxTimeNullableProjection(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "max_datetime_of_datetime",
                    new MaxDatetimeProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemProjection(
                    "max_datetime_nullable_of_datetime_null",
                    new MaxDatetimeNullableProjection(new LiteralDatetimeNullable())
                ),
                new SelectItemProjection(
                    "max_datetime_nullable_of_datetime_where",
                    new MaxDatetimeNullableProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "max_uuid_of_uuid",
                    new MaxUuidProjection(new LiteralUuid(new Guid(1, 0, 0, new byte[8])))
                ),
                new SelectItemProjection(
                    "max_uuid_nullable_of_uuid_null",
                    new MaxUuidNullableProjection(new LiteralUuidNullable())
                ),
                new SelectItemProjection(
                    "max_uuid_nullable_of_uuid_where",
                    new MaxUuidNullableProjection(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "first_integer_of_integer",
                    new FirstIntegerProjection(
                        new LiteralInteger(7),
                        [new OrderItemRow(new LiteralInteger(7))]
                    )
                ),
                new SelectItemProjection(
                    "first_integer_nullable_of_integer_null",
                    new FirstIntegerNullableProjection(
                        new LiteralIntegerNullable(),
                        [
                            new OrderItemRow(
                                new LiteralString("a"),
                                direction: SortDirection.Desc
                            ),
                            new OrderItemRow(new LiteralDateNullable()),
                        ]
                    )
                ),
                new SelectItemProjection(
                    "first_integer_nullable_of_integer_where",
                    new FirstIntegerNullableProjection(
                        new LiteralInteger(7),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "first_decimal_of_decimal",
                    new FirstDecimalProjection(
                        new LiteralDecimal(2.5m),
                        [new OrderItemRow(new LiteralInteger(7))]
                    )
                ),
                new SelectItemProjection(
                    "first_decimal_nullable_of_decimal_null",
                    new FirstDecimalNullableProjection(
                        new LiteralDecimalNullable(),
                        [
                            new OrderItemRow(
                                new LiteralString("a"),
                                direction: SortDirection.Desc
                            ),
                            new OrderItemRow(new LiteralDateNullable()),
                        ]
                    )
                ),
                new SelectItemProjection(
                    "first_decimal_nullable_of_decimal_where",
                    new FirstDecimalNullableProjection(
                        new LiteralDecimal(2.5m),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "first_string_of_string",
                    new FirstStringProjection(
                        new LiteralString("a"),
                        [new OrderItemRow(new LiteralInteger(7))]
                    )
                ),
                new SelectItemProjection(
                    "first_string_nullable_of_string_null",
                    new FirstStringNullableProjection(
                        new LiteralStringNullable(),
                        [
                            new OrderItemRow(
                                new LiteralString("a"),
                                direction: SortDirection.Desc
                            ),
                            new OrderItemRow(new LiteralDateNullable()),
                        ]
                    )
                ),
                new SelectItemProjection(
                    "first_string_nullable_of_string_where",
                    new FirstStringNullableProjection(
                        new LiteralString("a"),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "first_boolean_of_boolean",
                    new FirstBooleanProjection(
                        new LiteralBoolean(true),
                        [new OrderItemRow(new LiteralInteger(7))]
                    )
                ),
                new SelectItemProjection(
                    "first_boolean_nullable_of_boolean_null",
                    new FirstBooleanNullableProjection(
                        new LiteralBooleanNullable(),
                        [
                            new OrderItemRow(
                                new LiteralString("a"),
                                direction: SortDirection.Desc
                            ),
                            new OrderItemRow(new LiteralDateNullable()),
                        ]
                    )
                ),
                new SelectItemProjection(
                    "first_boolean_nullable_of_boolean_where",
                    new FirstBooleanNullableProjection(
                        new LiteralBoolean(true),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "first_date_of_date",
                    new FirstDateProjection(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        [new OrderItemRow(new LiteralInteger(7))]
                    )
                ),
                new SelectItemProjection(
                    "first_date_nullable_of_date_null",
                    new FirstDateNullableProjection(
                        new LiteralDateNullable(),
                        [
                            new OrderItemRow(
                                new LiteralString("a"),
                                direction: SortDirection.Desc
                            ),
                            new OrderItemRow(new LiteralDateNullable()),
                        ]
                    )
                ),
                new SelectItemProjection(
                    "first_date_nullable_of_date_where",
                    new FirstDateNullableProjection(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "first_time_of_time",
                    new FirstTimeProjection(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        [new OrderItemRow(new LiteralInteger(7))]
                    )
                ),
                new SelectItemProjection(
                    "first_time_nullable_of_time_null",
                    new FirstTimeNullableProjection(
                        new LiteralTimeNullable(),
                        [
                            new OrderItemRow(
                                new LiteralString("a"),
                                direction: SortDirection.Desc
                            ),
                            new OrderItemRow(new LiteralDateNullable()),
                        ]
                    )
                ),
                new SelectItemProjection(
                    "first_time_nullable_of_time_where",
                    new FirstTimeNullableProjection(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "first_datetime_of_datetime",
                    new FirstDatetimeProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        [new OrderItemRow(new LiteralInteger(7))]
                    )
                ),
                new SelectItemProjection(
                    "first_datetime_nullable_of_datetime_null",
                    new FirstDatetimeNullableProjection(
                        new LiteralDatetimeNullable(),
                        [
                            new OrderItemRow(
                                new LiteralString("a"),
                                direction: SortDirection.Desc
                            ),
                            new OrderItemRow(new LiteralDateNullable()),
                        ]
                    )
                ),
                new SelectItemProjection(
                    "first_datetime_nullable_of_datetime_where",
                    new FirstDatetimeNullableProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "first_uuid_of_uuid",
                    new FirstUuidProjection(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        [new OrderItemRow(new LiteralInteger(7))]
                    )
                ),
                new SelectItemProjection(
                    "first_uuid_nullable_of_uuid_null",
                    new FirstUuidNullableProjection(
                        new LiteralUuidNullable(),
                        [
                            new OrderItemRow(
                                new LiteralString("a"),
                                direction: SortDirection.Desc
                            ),
                            new OrderItemRow(new LiteralDateNullable()),
                        ]
                    )
                ),
                new SelectItemProjection(
                    "first_uuid_nullable_of_uuid_where",
                    new FirstUuidNullableProjection(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "last_integer_of_integer",
                    new LastIntegerProjection(
                        new LiteralInteger(7),
                        [new OrderItemRow(new LiteralInteger(7))]
                    )
                ),
                new SelectItemProjection(
                    "last_integer_nullable_of_integer_null",
                    new LastIntegerNullableProjection(
                        new LiteralIntegerNullable(),
                        [
                            new OrderItemRow(
                                new LiteralString("a"),
                                direction: SortDirection.Desc
                            ),
                            new OrderItemRow(new LiteralDateNullable()),
                        ]
                    )
                ),
                new SelectItemProjection(
                    "last_integer_nullable_of_integer_where",
                    new LastIntegerNullableProjection(
                        new LiteralInteger(7),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "last_decimal_of_decimal",
                    new LastDecimalProjection(
                        new LiteralDecimal(2.5m),
                        [new OrderItemRow(new LiteralInteger(7))]
                    )
                ),
                new SelectItemProjection(
                    "last_decimal_nullable_of_decimal_null",
                    new LastDecimalNullableProjection(
                        new LiteralDecimalNullable(),
                        [
                            new OrderItemRow(
                                new LiteralString("a"),
                                direction: SortDirection.Desc
                            ),
                            new OrderItemRow(new LiteralDateNullable()),
                        ]
                    )
                ),
                new SelectItemProjection(
                    "last_decimal_nullable_of_decimal_where",
                    new LastDecimalNullableProjection(
                        new LiteralDecimal(2.5m),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "last_string_of_string",
                    new LastStringProjection(
                        new LiteralString("a"),
                        [new OrderItemRow(new LiteralInteger(7))]
                    )
                ),
                new SelectItemProjection(
                    "last_string_nullable_of_string_null",
                    new LastStringNullableProjection(
                        new LiteralStringNullable(),
                        [
                            new OrderItemRow(
                                new LiteralString("a"),
                                direction: SortDirection.Desc
                            ),
                            new OrderItemRow(new LiteralDateNullable()),
                        ]
                    )
                ),
                new SelectItemProjection(
                    "last_string_nullable_of_string_where",
                    new LastStringNullableProjection(
                        new LiteralString("a"),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "last_boolean_of_boolean",
                    new LastBooleanProjection(
                        new LiteralBoolean(true),
                        [new OrderItemRow(new LiteralInteger(7))]
                    )
                ),
                new SelectItemProjection(
                    "last_boolean_nullable_of_boolean_null",
                    new LastBooleanNullableProjection(
                        new LiteralBooleanNullable(),
                        [
                            new OrderItemRow(
                                new LiteralString("a"),
                                direction: SortDirection.Desc
                            ),
                            new OrderItemRow(new LiteralDateNullable()),
                        ]
                    )
                ),
                new SelectItemProjection(
                    "last_boolean_nullable_of_boolean_where",
                    new LastBooleanNullableProjection(
                        new LiteralBoolean(true),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "last_date_of_date",
                    new LastDateProjection(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        [new OrderItemRow(new LiteralInteger(7))]
                    )
                ),
                new SelectItemProjection(
                    "last_date_nullable_of_date_null",
                    new LastDateNullableProjection(
                        new LiteralDateNullable(),
                        [
                            new OrderItemRow(
                                new LiteralString("a"),
                                direction: SortDirection.Desc
                            ),
                            new OrderItemRow(new LiteralDateNullable()),
                        ]
                    )
                ),
                new SelectItemProjection(
                    "last_date_nullable_of_date_where",
                    new LastDateNullableProjection(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "last_time_of_time",
                    new LastTimeProjection(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        [new OrderItemRow(new LiteralInteger(7))]
                    )
                ),
                new SelectItemProjection(
                    "last_time_nullable_of_time_null",
                    new LastTimeNullableProjection(
                        new LiteralTimeNullable(),
                        [
                            new OrderItemRow(
                                new LiteralString("a"),
                                direction: SortDirection.Desc
                            ),
                            new OrderItemRow(new LiteralDateNullable()),
                        ]
                    )
                ),
                new SelectItemProjection(
                    "last_time_nullable_of_time_where",
                    new LastTimeNullableProjection(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "last_datetime_of_datetime",
                    new LastDatetimeProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        [new OrderItemRow(new LiteralInteger(7))]
                    )
                ),
                new SelectItemProjection(
                    "last_datetime_nullable_of_datetime_null",
                    new LastDatetimeNullableProjection(
                        new LiteralDatetimeNullable(),
                        [
                            new OrderItemRow(
                                new LiteralString("a"),
                                direction: SortDirection.Desc
                            ),
                            new OrderItemRow(new LiteralDateNullable()),
                        ]
                    )
                ),
                new SelectItemProjection(
                    "last_datetime_nullable_of_datetime_where",
                    new LastDatetimeNullableProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "last_uuid_of_uuid",
                    new LastUuidProjection(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        [new OrderItemRow(new LiteralInteger(7))]
                    )
                ),
                new SelectItemProjection(
                    "last_uuid_nullable_of_uuid_null",
                    new LastUuidNullableProjection(
                        new LiteralUuidNullable(),
                        [
                            new OrderItemRow(
                                new LiteralString("a"),
                                direction: SortDirection.Desc
                            ),
                            new OrderItemRow(new LiteralDateNullable()),
                        ]
                    )
                ),
                new SelectItemProjection(
                    "last_uuid_nullable_of_uuid_where",
                    new LastUuidNullableProjection(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "median_decimal_of_integer",
                    new MedianDecimalProjection(new LiteralInteger(7))
                ),
                new SelectItemProjection(
                    "median_decimal_nullable_of_integer_null",
                    new MedianDecimalNullableProjection(new LiteralIntegerNullable())
                ),
                new SelectItemProjection(
                    "median_decimal_nullable_of_integer_where",
                    new MedianDecimalNullableProjection(
                        new LiteralInteger(7),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "median_decimal_of_decimal",
                    new MedianDecimalProjection(new LiteralDecimal(2.5m))
                ),
                new SelectItemProjection(
                    "median_decimal_nullable_of_decimal_null",
                    new MedianDecimalNullableProjection(new LiteralDecimalNullable())
                ),
                new SelectItemProjection(
                    "median_decimal_nullable_of_decimal_where",
                    new MedianDecimalNullableProjection(
                        new LiteralDecimal(2.5m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "median_date_of_date",
                    new MedianDateProjection(new LiteralDate(new DateOnly(2024, 1, 31)))
                ),
                new SelectItemProjection(
                    "median_date_nullable_of_date_null",
                    new MedianDateNullableProjection(new LiteralDateNullable())
                ),
                new SelectItemProjection(
                    "median_date_nullable_of_date_where",
                    new MedianDateNullableProjection(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "median_time_of_time",
                    new MedianTimeProjection(new LiteralTime(new TimeOnly(8, 30, 0)))
                ),
                new SelectItemProjection(
                    "median_time_nullable_of_time_null",
                    new MedianTimeNullableProjection(new LiteralTimeNullable())
                ),
                new SelectItemProjection(
                    "median_time_nullable_of_time_where",
                    new MedianTimeNullableProjection(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "median_datetime_of_datetime",
                    new MedianDatetimeProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemProjection(
                    "median_datetime_nullable_of_datetime_null",
                    new MedianDatetimeNullableProjection(new LiteralDatetimeNullable())
                ),
                new SelectItemProjection(
                    "median_datetime_nullable_of_datetime_where",
                    new MedianDatetimeNullableProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "percentileContinuous_decimal_of_integer",
                    new PercentileContinuousDecimalProjection(
                        new LiteralInteger(7),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemProjection(
                    "percentileContinuous_decimal_nullable_of_integer_null",
                    new PercentileContinuousDecimalNullableProjection(
                        new LiteralIntegerNullable(),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemProjection(
                    "percentileContinuous_decimal_nullable_of_integer_where",
                    new PercentileContinuousDecimalNullableProjection(
                        new LiteralInteger(7),
                        new LiteralDecimal(0.9m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "percentileContinuous_decimal_of_decimal",
                    new PercentileContinuousDecimalProjection(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemProjection(
                    "percentileContinuous_decimal_nullable_of_decimal_null",
                    new PercentileContinuousDecimalNullableProjection(
                        new LiteralDecimalNullable(),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemProjection(
                    "percentileContinuous_decimal_nullable_of_decimal_where",
                    new PercentileContinuousDecimalNullableProjection(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.9m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "percentileContinuous_date_of_date",
                    new PercentileContinuousDateProjection(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemProjection(
                    "percentileContinuous_date_nullable_of_date_null",
                    new PercentileContinuousDateNullableProjection(
                        new LiteralDateNullable(),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemProjection(
                    "percentileContinuous_date_nullable_of_date_where",
                    new PercentileContinuousDateNullableProjection(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDecimal(0.9m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "percentileContinuous_time_of_time",
                    new PercentileContinuousTimeProjection(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemProjection(
                    "percentileContinuous_time_nullable_of_time_null",
                    new PercentileContinuousTimeNullableProjection(
                        new LiteralTimeNullable(),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemProjection(
                    "percentileContinuous_time_nullable_of_time_where",
                    new PercentileContinuousTimeNullableProjection(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralDecimal(0.9m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "percentileContinuous_datetime_of_datetime",
                    new PercentileContinuousDatetimeProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemProjection(
                    "percentileContinuous_datetime_nullable_of_datetime_null",
                    new PercentileContinuousDatetimeNullableProjection(
                        new LiteralDatetimeNullable(),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemProjection(
                    "percentileContinuous_datetime_nullable_of_datetime_where",
                    new PercentileContinuousDatetimeNullableProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDecimal(0.9m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "percentileDiscrete_integer_of_integer",
                    new PercentileDiscreteIntegerProjection(
                        new LiteralInteger(7),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemProjection(
                    "percentileDiscrete_integer_nullable_of_integer_null",
                    new PercentileDiscreteIntegerNullableProjection(
                        new LiteralIntegerNullable(),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemProjection(
                    "percentileDiscrete_integer_nullable_of_integer_where",
                    new PercentileDiscreteIntegerNullableProjection(
                        new LiteralInteger(7),
                        new LiteralDecimal(0.9m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "percentileDiscrete_decimal_of_decimal",
                    new PercentileDiscreteDecimalProjection(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemProjection(
                    "percentileDiscrete_decimal_nullable_of_decimal_null",
                    new PercentileDiscreteDecimalNullableProjection(
                        new LiteralDecimalNullable(),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemProjection(
                    "percentileDiscrete_decimal_nullable_of_decimal_where",
                    new PercentileDiscreteDecimalNullableProjection(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.9m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "percentileDiscrete_string_of_string",
                    new PercentileDiscreteStringProjection(
                        new LiteralString("a"),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemProjection(
                    "percentileDiscrete_string_nullable_of_string_null",
                    new PercentileDiscreteStringNullableProjection(
                        new LiteralStringNullable(),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemProjection(
                    "percentileDiscrete_string_nullable_of_string_where",
                    new PercentileDiscreteStringNullableProjection(
                        new LiteralString("a"),
                        new LiteralDecimal(0.9m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "percentileDiscrete_boolean_of_boolean",
                    new PercentileDiscreteBooleanProjection(
                        new LiteralBoolean(true),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemProjection(
                    "percentileDiscrete_boolean_nullable_of_boolean_null",
                    new PercentileDiscreteBooleanNullableProjection(
                        new LiteralBooleanNullable(),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemProjection(
                    "percentileDiscrete_boolean_nullable_of_boolean_where",
                    new PercentileDiscreteBooleanNullableProjection(
                        new LiteralBoolean(true),
                        new LiteralDecimal(0.9m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "percentileDiscrete_date_of_date",
                    new PercentileDiscreteDateProjection(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemProjection(
                    "percentileDiscrete_date_nullable_of_date_null",
                    new PercentileDiscreteDateNullableProjection(
                        new LiteralDateNullable(),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemProjection(
                    "percentileDiscrete_date_nullable_of_date_where",
                    new PercentileDiscreteDateNullableProjection(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDecimal(0.9m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "percentileDiscrete_time_of_time",
                    new PercentileDiscreteTimeProjection(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemProjection(
                    "percentileDiscrete_time_nullable_of_time_null",
                    new PercentileDiscreteTimeNullableProjection(
                        new LiteralTimeNullable(),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemProjection(
                    "percentileDiscrete_time_nullable_of_time_where",
                    new PercentileDiscreteTimeNullableProjection(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralDecimal(0.9m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "percentileDiscrete_datetime_of_datetime",
                    new PercentileDiscreteDatetimeProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemProjection(
                    "percentileDiscrete_datetime_nullable_of_datetime_null",
                    new PercentileDiscreteDatetimeNullableProjection(
                        new LiteralDatetimeNullable(),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemProjection(
                    "percentileDiscrete_datetime_nullable_of_datetime_where",
                    new PercentileDiscreteDatetimeNullableProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDecimal(0.9m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "percentileDiscrete_uuid_of_uuid",
                    new PercentileDiscreteUuidProjection(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemProjection(
                    "percentileDiscrete_uuid_nullable_of_uuid_null",
                    new PercentileDiscreteUuidNullableProjection(
                        new LiteralUuidNullable(),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemProjection(
                    "percentileDiscrete_uuid_nullable_of_uuid_where",
                    new PercentileDiscreteUuidNullableProjection(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralDecimal(0.9m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "mode_integer_of_integer",
                    new ModeIntegerProjection(new LiteralInteger(7))
                ),
                new SelectItemProjection(
                    "mode_integer_nullable_of_integer_null",
                    new ModeIntegerNullableProjection(new LiteralIntegerNullable())
                ),
                new SelectItemProjection(
                    "mode_integer_nullable_of_integer_where",
                    new ModeIntegerNullableProjection(
                        new LiteralInteger(7),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "mode_decimal_of_decimal",
                    new ModeDecimalProjection(new LiteralDecimal(2.5m))
                ),
                new SelectItemProjection(
                    "mode_decimal_nullable_of_decimal_null",
                    new ModeDecimalNullableProjection(new LiteralDecimalNullable())
                ),
                new SelectItemProjection(
                    "mode_decimal_nullable_of_decimal_where",
                    new ModeDecimalNullableProjection(
                        new LiteralDecimal(2.5m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "mode_string_of_string",
                    new ModeStringProjection(new LiteralString("a"))
                ),
                new SelectItemProjection(
                    "mode_string_nullable_of_string_null",
                    new ModeStringNullableProjection(new LiteralStringNullable())
                ),
                new SelectItemProjection(
                    "mode_string_nullable_of_string_where",
                    new ModeStringNullableProjection(
                        new LiteralString("a"),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "mode_boolean_of_boolean",
                    new ModeBooleanProjection(new LiteralBoolean(true))
                ),
                new SelectItemProjection(
                    "mode_boolean_nullable_of_boolean_null",
                    new ModeBooleanNullableProjection(new LiteralBooleanNullable())
                ),
                new SelectItemProjection(
                    "mode_boolean_nullable_of_boolean_where",
                    new ModeBooleanNullableProjection(
                        new LiteralBoolean(true),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "mode_date_of_date",
                    new ModeDateProjection(new LiteralDate(new DateOnly(2024, 1, 31)))
                ),
                new SelectItemProjection(
                    "mode_date_nullable_of_date_null",
                    new ModeDateNullableProjection(new LiteralDateNullable())
                ),
                new SelectItemProjection(
                    "mode_date_nullable_of_date_where",
                    new ModeDateNullableProjection(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "mode_time_of_time",
                    new ModeTimeProjection(new LiteralTime(new TimeOnly(8, 30, 0)))
                ),
                new SelectItemProjection(
                    "mode_time_nullable_of_time_null",
                    new ModeTimeNullableProjection(new LiteralTimeNullable())
                ),
                new SelectItemProjection(
                    "mode_time_nullable_of_time_where",
                    new ModeTimeNullableProjection(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "mode_datetime_of_datetime",
                    new ModeDatetimeProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemProjection(
                    "mode_datetime_nullable_of_datetime_null",
                    new ModeDatetimeNullableProjection(new LiteralDatetimeNullable())
                ),
                new SelectItemProjection(
                    "mode_datetime_nullable_of_datetime_where",
                    new ModeDatetimeNullableProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "mode_uuid_of_uuid",
                    new ModeUuidProjection(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8]))
                    )
                ),
                new SelectItemProjection(
                    "mode_uuid_nullable_of_uuid_null",
                    new ModeUuidNullableProjection(new LiteralUuidNullable())
                ),
                new SelectItemProjection(
                    "mode_uuid_nullable_of_uuid_where",
                    new ModeUuidNullableProjection(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "variancePopulation_decimal_of_integer",
                    new VariancePopulationDecimalProjection(new LiteralInteger(7))
                ),
                new SelectItemProjection(
                    "variancePopulation_decimal_nullable_of_integer_null",
                    new VariancePopulationDecimalNullableProjection(
                        new LiteralIntegerNullable()
                    )
                ),
                new SelectItemProjection(
                    "variancePopulation_decimal_nullable_of_integer_where",
                    new VariancePopulationDecimalNullableProjection(
                        new LiteralInteger(7),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "variancePopulation_decimal_of_decimal",
                    new VariancePopulationDecimalProjection(new LiteralDecimal(2.5m))
                ),
                new SelectItemProjection(
                    "variancePopulation_decimal_nullable_of_decimal_null",
                    new VariancePopulationDecimalNullableProjection(
                        new LiteralDecimalNullable()
                    )
                ),
                new SelectItemProjection(
                    "variancePopulation_decimal_nullable_of_decimal_where",
                    new VariancePopulationDecimalNullableProjection(
                        new LiteralDecimal(2.5m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "standardDeviationPopulation_decimal_of_integer",
                    new StandardDeviationPopulationDecimalProjection(
                        new LiteralInteger(7)
                    )
                ),
                new SelectItemProjection(
                    "standardDeviationPopulation_decimal_nullable_of_integer_null",
                    new StandardDeviationPopulationDecimalNullableProjection(
                        new LiteralIntegerNullable()
                    )
                ),
                new SelectItemProjection(
                    "standardDeviationPopulation_decimal_nullable_of_integer_where",
                    new StandardDeviationPopulationDecimalNullableProjection(
                        new LiteralInteger(7),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "standardDeviationPopulation_decimal_of_decimal",
                    new StandardDeviationPopulationDecimalProjection(
                        new LiteralDecimal(2.5m)
                    )
                ),
                new SelectItemProjection(
                    "standardDeviationPopulation_decimal_nullable_of_decimal_null",
                    new StandardDeviationPopulationDecimalNullableProjection(
                        new LiteralDecimalNullable()
                    )
                ),
                new SelectItemProjection(
                    "standardDeviationPopulation_decimal_nullable_of_decimal_where",
                    new StandardDeviationPopulationDecimalNullableProjection(
                        new LiteralDecimal(2.5m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "varianceSample_decimal_nullable_of_integer",
                    new VarianceSampleProjection(new LiteralInteger(7))
                ),
                new SelectItemProjection(
                    "varianceSample_decimal_nullable_of_decimal",
                    new VarianceSampleProjection(new LiteralDecimal(2.5m))
                ),
                new SelectItemProjection(
                    "varianceSample_decimal_nullable_of_decimal_null",
                    new VarianceSampleProjection(new LiteralDecimalNullable())
                ),
                new SelectItemProjection(
                    "varianceSample_decimal_nullable_of_decimal_where",
                    new VarianceSampleProjection(
                        new LiteralDecimal(2.5m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "standardDeviationSample_decimal_nullable_of_integer",
                    new StandardDeviationSampleProjection(new LiteralInteger(7))
                ),
                new SelectItemProjection(
                    "standardDeviationSample_decimal_nullable_of_decimal",
                    new StandardDeviationSampleProjection(new LiteralDecimal(2.5m))
                ),
                new SelectItemProjection(
                    "standardDeviationSample_decimal_nullable_of_decimal_null",
                    new StandardDeviationSampleProjection(new LiteralDecimalNullable())
                ),
                new SelectItemProjection(
                    "standardDeviationSample_decimal_nullable_of_decimal_where",
                    new StandardDeviationSampleProjection(
                        new LiteralDecimal(2.5m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "concatAll_string_of_string",
                    new ConcatAllProjection(
                        new LiteralString("a"),
                        new LiteralString(","),
                        [new OrderItemRow(new LiteralInteger(7))]
                    )
                ),
                new SelectItemProjection(
                    "concatAll_string_of_string_null",
                    new ConcatAllProjection(
                        new LiteralStringNullable(),
                        new LiteralString(","),
                        [
                            new OrderItemRow(
                                new LiteralString("a"),
                                direction: SortDirection.Desc
                            ),
                            new OrderItemRow(new LiteralDateNullable()),
                        ]
                    )
                ),
                new SelectItemProjection(
                    "concatAll_string_of_string_where",
                    new ConcatAllProjection(
                        new LiteralString("a"),
                        new LiteralString(","),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "concatAll_string_of_string_distinct",
                    new ConcatAllProjection(
                        new LiteralString("a"),
                        new LiteralString(""),
                        [new OrderItemRow(new LiteralInteger(7))],
                        distinct: true
                    )
                ),
                new SelectItemProjection(
                    "concatAll_string_parameter_separator",
                    new ConcatAllProjection(
                        new LiteralString("a"),
                        new ParamString("separator"),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true),
                        distinct: true
                    )
                ),
                new SelectItemProjection(
                    "percentileContinuous_parameter_fraction",
                    new PercentileContinuousDecimalProjection(
                        new LiteralDecimal(2.5m),
                        new ParamDecimal("fraction")
                    )
                ),
                new SelectItemProjection(
                    "percentileDiscrete_fraction_zero",
                    new PercentileDiscreteIntegerProjection(
                        new LiteralInteger(7),
                        new LiteralDecimal(0m)
                    )
                ),
                new SelectItemProjection(
                    "percentileDiscrete_fraction_one",
                    new PercentileDiscreteIntegerProjection(
                        new LiteralInteger(7),
                        new LiteralDecimal(1m)
                    )
                ),
                new SelectItemProjection(
                    "any_boolean_where",
                    new AnyProjection(new LiteralBoolean(true))
                ),
                new SelectItemProjection(
                    "all_boolean_where",
                    new AllProjection(new LiteralBoolean(true))
                ),
                new SelectItemProjection(
                    "rowNumber_integer",
                    new RowNumberProjection(
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))])
                    )
                ),
                new SelectItemProjection(
                    "rowNumber_integer_partitioned",
                    new RowNumberProjection(
                        new OrderedWindow(
                            [
                                new OrderItemRow(
                                    new LiteralInteger(7),
                                    direction: SortDirection.Desc
                                ),
                                new OrderItemRow(new LiteralDateNullable()),
                            ],
                            partitionBy:
                            [
                                new LiteralString("a"),
                                new LiteralDateNullable(),
                            ]
                        )
                    )
                ),
                new SelectItemProjection(
                    "rank_integer",
                    new RankProjection(
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))])
                    )
                ),
                new SelectItemProjection(
                    "denseRank_integer",
                    new DenseRankProjection(
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))])
                    )
                ),
                new SelectItemProjection(
                    "percentRank_decimal",
                    new PercentRankProjection(
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))])
                    )
                ),
                new SelectItemProjection(
                    "cumulativeDistribution_decimal",
                    new CumulativeDistributionProjection(
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))])
                    )
                ),
                new SelectItemProjection(
                    "ntile_integer",
                    new NtileProjection(
                        new LiteralInteger(4),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))])
                    )
                ),
                new SelectItemProjection(
                    "ntile_integer_one_bucket",
                    new NtileProjection(
                        new LiteralInteger(1),
                        new OrderedWindow(
                            [
                                new OrderItemRow(
                                    new LiteralInteger(7),
                                    direction: SortDirection.Desc
                                ),
                                new OrderItemRow(new LiteralDateNullable()),
                            ],
                            partitionBy:
                            [
                                new LiteralString("a"),
                                new LiteralDateNullable(),
                            ]
                        )
                    )
                ),
                new SelectItemProjection(
                    "ntile_integer_parameter_buckets",
                    new NtileProjection(
                        new ParamInteger("buckets"),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))])
                    )
                ),
                new SelectItemProjection(
                    "previous_integer_with_default",
                    new PreviousIntegerProjection(
                        new LiteralInteger(7),
                        new LiteralInteger(-3),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))])
                    )
                ),
                new SelectItemProjection(
                    "previous_integer_nullable",
                    new PreviousIntegerNullableProjection(
                        new LiteralInteger(7),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))])
                    )
                ),
                new SelectItemProjection(
                    "previous_integer_nullable_of_null",
                    new PreviousIntegerNullableProjection(
                        new LiteralIntegerNullable(),
                        new OrderedWindow(
                            [
                                new OrderItemRow(
                                    new LiteralInteger(7),
                                    direction: SortDirection.Desc
                                ),
                                new OrderItemRow(new LiteralDateNullable()),
                            ],
                            partitionBy:
                            [
                                new LiteralString("a"),
                                new LiteralDateNullable(),
                            ]
                        )
                    )
                ),
                new SelectItemProjection(
                    "previous_integer_nullable_of_null_with_default",
                    new PreviousIntegerNullableProjection(
                        new LiteralIntegerNullable(),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))]),
                        @default: new LiteralInteger(-3)
                    )
                ),
                new SelectItemProjection(
                    "previous_decimal_with_default",
                    new PreviousDecimalProjection(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))])
                    )
                ),
                new SelectItemProjection(
                    "previous_decimal_nullable",
                    new PreviousDecimalNullableProjection(
                        new LiteralDecimal(2.5m),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))])
                    )
                ),
                new SelectItemProjection(
                    "previous_decimal_nullable_of_null",
                    new PreviousDecimalNullableProjection(
                        new LiteralDecimalNullable(),
                        new OrderedWindow(
                            [
                                new OrderItemRow(
                                    new LiteralInteger(7),
                                    direction: SortDirection.Desc
                                ),
                                new OrderItemRow(new LiteralDateNullable()),
                            ],
                            partitionBy:
                            [
                                new LiteralString("a"),
                                new LiteralDateNullable(),
                            ]
                        )
                    )
                ),
                new SelectItemProjection(
                    "previous_decimal_nullable_of_null_with_default",
                    new PreviousDecimalNullableProjection(
                        new LiteralDecimalNullable(),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))]),
                        @default: new LiteralDecimal(0.1m)
                    )
                ),
                new SelectItemProjection(
                    "previous_string_with_default",
                    new PreviousStringProjection(
                        new LiteralString("a"),
                        new LiteralString("b"),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))])
                    )
                ),
                new SelectItemProjection(
                    "previous_string_nullable",
                    new PreviousStringNullableProjection(
                        new LiteralString("a"),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))])
                    )
                ),
                new SelectItemProjection(
                    "previous_string_nullable_of_null",
                    new PreviousStringNullableProjection(
                        new LiteralStringNullable(),
                        new OrderedWindow(
                            [
                                new OrderItemRow(
                                    new LiteralInteger(7),
                                    direction: SortDirection.Desc
                                ),
                                new OrderItemRow(new LiteralDateNullable()),
                            ],
                            partitionBy:
                            [
                                new LiteralString("a"),
                                new LiteralDateNullable(),
                            ]
                        )
                    )
                ),
                new SelectItemProjection(
                    "previous_string_nullable_of_null_with_default",
                    new PreviousStringNullableProjection(
                        new LiteralStringNullable(),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))]),
                        @default: new LiteralString("b")
                    )
                ),
                new SelectItemProjection(
                    "previous_boolean_with_default",
                    new PreviousBooleanProjection(
                        new LiteralBoolean(true),
                        new LiteralBoolean(false),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))])
                    )
                ),
                new SelectItemProjection(
                    "previous_boolean_nullable",
                    new PreviousBooleanNullableProjection(
                        new LiteralBoolean(true),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))])
                    )
                ),
                new SelectItemProjection(
                    "previous_boolean_nullable_of_null",
                    new PreviousBooleanNullableProjection(
                        new LiteralBooleanNullable(),
                        new OrderedWindow(
                            [
                                new OrderItemRow(
                                    new LiteralInteger(7),
                                    direction: SortDirection.Desc
                                ),
                                new OrderItemRow(new LiteralDateNullable()),
                            ],
                            partitionBy:
                            [
                                new LiteralString("a"),
                                new LiteralDateNullable(),
                            ]
                        )
                    )
                ),
                new SelectItemProjection(
                    "previous_boolean_nullable_of_null_with_default",
                    new PreviousBooleanNullableProjection(
                        new LiteralBooleanNullable(),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))]),
                        @default: new LiteralBoolean(false)
                    )
                ),
                new SelectItemProjection(
                    "previous_date_with_default",
                    new PreviousDateProjection(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDate(new DateOnly(2024, 3, 1)),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))])
                    )
                ),
                new SelectItemProjection(
                    "previous_date_nullable",
                    new PreviousDateNullableProjection(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))])
                    )
                ),
                new SelectItemProjection(
                    "previous_date_nullable_of_null",
                    new PreviousDateNullableProjection(
                        new LiteralDateNullable(),
                        new OrderedWindow(
                            [
                                new OrderItemRow(
                                    new LiteralInteger(7),
                                    direction: SortDirection.Desc
                                ),
                                new OrderItemRow(new LiteralDateNullable()),
                            ],
                            partitionBy:
                            [
                                new LiteralString("a"),
                                new LiteralDateNullable(),
                            ]
                        )
                    )
                ),
                new SelectItemProjection(
                    "previous_date_nullable_of_null_with_default",
                    new PreviousDateNullableProjection(
                        new LiteralDateNullable(),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))]),
                        @default: new LiteralDate(new DateOnly(2024, 3, 1))
                    )
                ),
                new SelectItemProjection(
                    "previous_time_with_default",
                    new PreviousTimeProjection(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralTime(new TimeOnly(17, 45, 30, 500)),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))])
                    )
                ),
                new SelectItemProjection(
                    "previous_time_nullable",
                    new PreviousTimeNullableProjection(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))])
                    )
                ),
                new SelectItemProjection(
                    "previous_time_nullable_of_null",
                    new PreviousTimeNullableProjection(
                        new LiteralTimeNullable(),
                        new OrderedWindow(
                            [
                                new OrderItemRow(
                                    new LiteralInteger(7),
                                    direction: SortDirection.Desc
                                ),
                                new OrderItemRow(new LiteralDateNullable()),
                            ],
                            partitionBy:
                            [
                                new LiteralString("a"),
                                new LiteralDateNullable(),
                            ]
                        )
                    )
                ),
                new SelectItemProjection(
                    "previous_time_nullable_of_null_with_default",
                    new PreviousTimeNullableProjection(
                        new LiteralTimeNullable(),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))]),
                        @default: new LiteralTime(new TimeOnly(17, 45, 30, 500))
                    )
                ),
                new SelectItemProjection(
                    "previous_datetime_with_default",
                    new PreviousDatetimeProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        ),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))])
                    )
                ),
                new SelectItemProjection(
                    "previous_datetime_nullable",
                    new PreviousDatetimeNullableProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))])
                    )
                ),
                new SelectItemProjection(
                    "previous_datetime_nullable_of_null",
                    new PreviousDatetimeNullableProjection(
                        new LiteralDatetimeNullable(),
                        new OrderedWindow(
                            [
                                new OrderItemRow(
                                    new LiteralInteger(7),
                                    direction: SortDirection.Desc
                                ),
                                new OrderItemRow(new LiteralDateNullable()),
                            ],
                            partitionBy:
                            [
                                new LiteralString("a"),
                                new LiteralDateNullable(),
                            ]
                        )
                    )
                ),
                new SelectItemProjection(
                    "previous_datetime_nullable_of_null_with_default",
                    new PreviousDatetimeNullableProjection(
                        new LiteralDatetimeNullable(),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))]),
                        @default: new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemProjection(
                    "previous_uuid_with_default",
                    new PreviousUuidProjection(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralUuid(new Guid(2, 0, 0, new byte[8])),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))])
                    )
                ),
                new SelectItemProjection(
                    "previous_uuid_nullable",
                    new PreviousUuidNullableProjection(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))])
                    )
                ),
                new SelectItemProjection(
                    "previous_uuid_nullable_of_null",
                    new PreviousUuidNullableProjection(
                        new LiteralUuidNullable(),
                        new OrderedWindow(
                            [
                                new OrderItemRow(
                                    new LiteralInteger(7),
                                    direction: SortDirection.Desc
                                ),
                                new OrderItemRow(new LiteralDateNullable()),
                            ],
                            partitionBy:
                            [
                                new LiteralString("a"),
                                new LiteralDateNullable(),
                            ]
                        )
                    )
                ),
                new SelectItemProjection(
                    "previous_uuid_nullable_of_null_with_default",
                    new PreviousUuidNullableProjection(
                        new LiteralUuidNullable(),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))]),
                        @default: new LiteralUuid(new Guid(2, 0, 0, new byte[8]))
                    )
                ),
                new SelectItemProjection(
                    "previous_decimal_with_integer_default",
                    new PreviousDecimalProjection(
                        new LiteralDecimal(2.5m),
                        new LiteralInteger(7),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))])
                    )
                ),
                new SelectItemProjection(
                    "previous_decimal_nullable_with_integer_default",
                    new PreviousDecimalNullableProjection(
                        new LiteralDecimalNullable(),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))]),
                        @default: new LiteralInteger(7)
                    )
                ),
                new SelectItemProjection(
                    "previous_integer_nullable_offset",
                    new PreviousIntegerNullableProjection(
                        new LiteralInteger(7),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))]),
                        offset: new LiteralInteger(2)
                    )
                ),
                new SelectItemProjection(
                    "previous_integer_parameter_offset",
                    new PreviousIntegerProjection(
                        new LiteralInteger(7),
                        new LiteralInteger(-3),
                        new OrderedWindow(
                            [
                                new OrderItemRow(
                                    new LiteralInteger(7),
                                    direction: SortDirection.Desc
                                ),
                                new OrderItemRow(new LiteralDateNullable()),
                            ],
                            partitionBy:
                            [
                                new LiteralString("a"),
                                new LiteralDateNullable(),
                            ]
                        ),
                        offset: new ParamInteger("offset")
                    )
                ),
                new SelectItemProjection(
                    "next_integer_with_default",
                    new NextIntegerProjection(
                        new LiteralInteger(7),
                        new LiteralInteger(-3),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))])
                    )
                ),
                new SelectItemProjection(
                    "next_integer_nullable",
                    new NextIntegerNullableProjection(
                        new LiteralInteger(7),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))])
                    )
                ),
                new SelectItemProjection(
                    "next_integer_nullable_of_null",
                    new NextIntegerNullableProjection(
                        new LiteralIntegerNullable(),
                        new OrderedWindow(
                            [
                                new OrderItemRow(
                                    new LiteralInteger(7),
                                    direction: SortDirection.Desc
                                ),
                                new OrderItemRow(new LiteralDateNullable()),
                            ],
                            partitionBy:
                            [
                                new LiteralString("a"),
                                new LiteralDateNullable(),
                            ]
                        )
                    )
                ),
                new SelectItemProjection(
                    "next_integer_nullable_of_null_with_default",
                    new NextIntegerNullableProjection(
                        new LiteralIntegerNullable(),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))]),
                        @default: new LiteralInteger(-3)
                    )
                ),
                new SelectItemProjection(
                    "next_decimal_with_default",
                    new NextDecimalProjection(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))])
                    )
                ),
                new SelectItemProjection(
                    "next_decimal_nullable",
                    new NextDecimalNullableProjection(
                        new LiteralDecimal(2.5m),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))])
                    )
                ),
                new SelectItemProjection(
                    "next_decimal_nullable_of_null",
                    new NextDecimalNullableProjection(
                        new LiteralDecimalNullable(),
                        new OrderedWindow(
                            [
                                new OrderItemRow(
                                    new LiteralInteger(7),
                                    direction: SortDirection.Desc
                                ),
                                new OrderItemRow(new LiteralDateNullable()),
                            ],
                            partitionBy:
                            [
                                new LiteralString("a"),
                                new LiteralDateNullable(),
                            ]
                        )
                    )
                ),
                new SelectItemProjection(
                    "next_decimal_nullable_of_null_with_default",
                    new NextDecimalNullableProjection(
                        new LiteralDecimalNullable(),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))]),
                        @default: new LiteralDecimal(0.1m)
                    )
                ),
                new SelectItemProjection(
                    "next_string_with_default",
                    new NextStringProjection(
                        new LiteralString("a"),
                        new LiteralString("b"),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))])
                    )
                ),
                new SelectItemProjection(
                    "next_string_nullable",
                    new NextStringNullableProjection(
                        new LiteralString("a"),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))])
                    )
                ),
                new SelectItemProjection(
                    "next_string_nullable_of_null",
                    new NextStringNullableProjection(
                        new LiteralStringNullable(),
                        new OrderedWindow(
                            [
                                new OrderItemRow(
                                    new LiteralInteger(7),
                                    direction: SortDirection.Desc
                                ),
                                new OrderItemRow(new LiteralDateNullable()),
                            ],
                            partitionBy:
                            [
                                new LiteralString("a"),
                                new LiteralDateNullable(),
                            ]
                        )
                    )
                ),
                new SelectItemProjection(
                    "next_string_nullable_of_null_with_default",
                    new NextStringNullableProjection(
                        new LiteralStringNullable(),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))]),
                        @default: new LiteralString("b")
                    )
                ),
                new SelectItemProjection(
                    "next_boolean_with_default",
                    new NextBooleanProjection(
                        new LiteralBoolean(true),
                        new LiteralBoolean(false),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))])
                    )
                ),
                new SelectItemProjection(
                    "next_boolean_nullable",
                    new NextBooleanNullableProjection(
                        new LiteralBoolean(true),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))])
                    )
                ),
                new SelectItemProjection(
                    "next_boolean_nullable_of_null",
                    new NextBooleanNullableProjection(
                        new LiteralBooleanNullable(),
                        new OrderedWindow(
                            [
                                new OrderItemRow(
                                    new LiteralInteger(7),
                                    direction: SortDirection.Desc
                                ),
                                new OrderItemRow(new LiteralDateNullable()),
                            ],
                            partitionBy:
                            [
                                new LiteralString("a"),
                                new LiteralDateNullable(),
                            ]
                        )
                    )
                ),
                new SelectItemProjection(
                    "next_boolean_nullable_of_null_with_default",
                    new NextBooleanNullableProjection(
                        new LiteralBooleanNullable(),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))]),
                        @default: new LiteralBoolean(false)
                    )
                ),
                new SelectItemProjection(
                    "next_date_with_default",
                    new NextDateProjection(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDate(new DateOnly(2024, 3, 1)),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))])
                    )
                ),
                new SelectItemProjection(
                    "next_date_nullable",
                    new NextDateNullableProjection(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))])
                    )
                ),
                new SelectItemProjection(
                    "next_date_nullable_of_null",
                    new NextDateNullableProjection(
                        new LiteralDateNullable(),
                        new OrderedWindow(
                            [
                                new OrderItemRow(
                                    new LiteralInteger(7),
                                    direction: SortDirection.Desc
                                ),
                                new OrderItemRow(new LiteralDateNullable()),
                            ],
                            partitionBy:
                            [
                                new LiteralString("a"),
                                new LiteralDateNullable(),
                            ]
                        )
                    )
                ),
                new SelectItemProjection(
                    "next_date_nullable_of_null_with_default",
                    new NextDateNullableProjection(
                        new LiteralDateNullable(),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))]),
                        @default: new LiteralDate(new DateOnly(2024, 3, 1))
                    )
                ),
                new SelectItemProjection(
                    "next_time_with_default",
                    new NextTimeProjection(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralTime(new TimeOnly(17, 45, 30, 500)),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))])
                    )
                ),
                new SelectItemProjection(
                    "next_time_nullable",
                    new NextTimeNullableProjection(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))])
                    )
                ),
                new SelectItemProjection(
                    "next_time_nullable_of_null",
                    new NextTimeNullableProjection(
                        new LiteralTimeNullable(),
                        new OrderedWindow(
                            [
                                new OrderItemRow(
                                    new LiteralInteger(7),
                                    direction: SortDirection.Desc
                                ),
                                new OrderItemRow(new LiteralDateNullable()),
                            ],
                            partitionBy:
                            [
                                new LiteralString("a"),
                                new LiteralDateNullable(),
                            ]
                        )
                    )
                ),
                new SelectItemProjection(
                    "next_time_nullable_of_null_with_default",
                    new NextTimeNullableProjection(
                        new LiteralTimeNullable(),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))]),
                        @default: new LiteralTime(new TimeOnly(17, 45, 30, 500))
                    )
                ),
                new SelectItemProjection(
                    "next_datetime_with_default",
                    new NextDatetimeProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        ),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))])
                    )
                ),
                new SelectItemProjection(
                    "next_datetime_nullable",
                    new NextDatetimeNullableProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))])
                    )
                ),
                new SelectItemProjection(
                    "next_datetime_nullable_of_null",
                    new NextDatetimeNullableProjection(
                        new LiteralDatetimeNullable(),
                        new OrderedWindow(
                            [
                                new OrderItemRow(
                                    new LiteralInteger(7),
                                    direction: SortDirection.Desc
                                ),
                                new OrderItemRow(new LiteralDateNullable()),
                            ],
                            partitionBy:
                            [
                                new LiteralString("a"),
                                new LiteralDateNullable(),
                            ]
                        )
                    )
                ),
                new SelectItemProjection(
                    "next_datetime_nullable_of_null_with_default",
                    new NextDatetimeNullableProjection(
                        new LiteralDatetimeNullable(),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))]),
                        @default: new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemProjection(
                    "next_uuid_with_default",
                    new NextUuidProjection(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralUuid(new Guid(2, 0, 0, new byte[8])),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))])
                    )
                ),
                new SelectItemProjection(
                    "next_uuid_nullable",
                    new NextUuidNullableProjection(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))])
                    )
                ),
                new SelectItemProjection(
                    "next_uuid_nullable_of_null",
                    new NextUuidNullableProjection(
                        new LiteralUuidNullable(),
                        new OrderedWindow(
                            [
                                new OrderItemRow(
                                    new LiteralInteger(7),
                                    direction: SortDirection.Desc
                                ),
                                new OrderItemRow(new LiteralDateNullable()),
                            ],
                            partitionBy:
                            [
                                new LiteralString("a"),
                                new LiteralDateNullable(),
                            ]
                        )
                    )
                ),
                new SelectItemProjection(
                    "next_uuid_nullable_of_null_with_default",
                    new NextUuidNullableProjection(
                        new LiteralUuidNullable(),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))]),
                        @default: new LiteralUuid(new Guid(2, 0, 0, new byte[8]))
                    )
                ),
                new SelectItemProjection(
                    "next_decimal_with_integer_default",
                    new NextDecimalProjection(
                        new LiteralDecimal(2.5m),
                        new LiteralInteger(7),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))])
                    )
                ),
                new SelectItemProjection(
                    "next_decimal_nullable_with_integer_default",
                    new NextDecimalNullableProjection(
                        new LiteralDecimalNullable(),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))]),
                        @default: new LiteralInteger(7)
                    )
                ),
                new SelectItemProjection(
                    "next_integer_nullable_offset",
                    new NextIntegerNullableProjection(
                        new LiteralInteger(7),
                        new OrderedWindow([new OrderItemRow(new LiteralInteger(7))]),
                        offset: new LiteralInteger(2)
                    )
                ),
                new SelectItemProjection(
                    "next_integer_parameter_offset",
                    new NextIntegerProjection(
                        new LiteralInteger(7),
                        new LiteralInteger(-3),
                        new OrderedWindow(
                            [
                                new OrderItemRow(
                                    new LiteralInteger(7),
                                    direction: SortDirection.Desc
                                ),
                                new OrderItemRow(new LiteralDateNullable()),
                            ],
                            partitionBy:
                            [
                                new LiteralString("a"),
                                new LiteralDateNullable(),
                            ]
                        ),
                        offset: new ParamInteger("offset")
                    )
                ),
                new SelectItemProjection(
                    "count_integer_over_partition",
                    new CountProjection(
                        over: new WindowWithoutFrameWithoutRange([new LiteralString("a")])
                    )
                ),
                new SelectItemProjection(
                    "count_integer_over_frame_where",
                    new CountProjection(
                        predicate: new LiteralBoolean(true),
                        over: new WindowWithFrame(
                            [new OrderItemRow(new LiteralInteger(7))],
                            new Frame(new All())
                        )
                    )
                ),
                new SelectItemProjection(
                    "countDistinct_integer_over_partition",
                    new CountDistinctProjection(
                        new LiteralString("a"),
                        over: new WindowWithoutFrameWithoutRange([new LiteralInteger(1)])
                    )
                ),
                new SelectItemProjection(
                    "countDistinct_integer_of_null_over_frame_where",
                    new CountDistinctProjection(
                        new LiteralDateNullable(),
                        predicate: new LiteralBoolean(true),
                        over: new WindowWithFrame(
                            [new OrderItemRow(new LiteralInteger(1))],
                            new Frame(2)
                        )
                    )
                ),
                new SelectItemProjection(
                    "sum_integer_over_partition",
                    new SumIntegerProjection(
                        new LiteralInteger(7),
                        over: new WindowWithoutFrameWithoutRange([new LiteralString("a")])
                    )
                ),
                new SelectItemProjection(
                    "sum_decimal_of_null_over_frame",
                    new SumDecimalProjection(
                        new LiteralDecimalNullable(),
                        over: new WindowWithFrame(
                            [new OrderItemRow(new LiteralInteger(7))],
                            new Frame(new All())
                        )
                    )
                ),
                new SelectItemProjection(
                    "average_decimal_of_integer_over_frame",
                    new AverageDecimalProjection(
                        new LiteralInteger(7),
                        over: new WindowWithFrame(
                            [new OrderItemRow(new LiteralInteger(7))],
                            new Frame(new All())
                        )
                    )
                ),
                new SelectItemProjection(
                    "average_decimal_nullable_over_frame_where",
                    new AverageDecimalNullableProjection(
                        new LiteralDecimal(2.5m),
                        predicate: new LiteralBoolean(true),
                        over: new WindowWithFrame(
                            [new OrderItemRow(new LiteralInteger(7))],
                            new Frame(new All())
                        )
                    )
                ),
                new SelectItemProjection(
                    "min_date_over_partition",
                    new MinDateProjection(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        over: new WindowWithoutFrameWithoutRange([new LiteralString("a")])
                    )
                ),
                new SelectItemProjection(
                    "max_string_nullable_of_null_over_frame",
                    new MaxStringNullableProjection(
                        new LiteralStringNullable(),
                        over: new WindowWithFrame(
                            [new OrderItemRow(new LiteralInteger(7))],
                            new Frame(new All())
                        )
                    )
                ),
                new SelectItemProjection(
                    "first_uuid_over_frame",
                    new FirstUuidProjection(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        [new OrderItemRow(new LiteralInteger(7))],
                        over: new WindowWithFrame(
                            [new OrderItemRow(new LiteralInteger(7))],
                            new Frame(new All())
                        )
                    )
                ),
                new SelectItemProjection(
                    "last_time_nullable_over_partition_where",
                    new LastTimeNullableProjection(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true),
                        over: new WindowWithoutFrameWithoutRange([new LiteralString("a")])
                    )
                ),
                new SelectItemProjection(
                    "median_decimal_over_frame",
                    new MedianDecimalProjection(
                        new LiteralDecimal(2.5m),
                        over: new WindowWithFrame(
                            [new OrderItemRow(new LiteralInteger(7))],
                            new Frame(2)
                        )
                    )
                ),
                new SelectItemProjection(
                    "percentileContinuous_datetime_over_partition",
                    new PercentileContinuousDatetimeProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDecimal(0.9m),
                        over: new WindowWithoutFrameWithoutRange([new LiteralString("a")])
                    )
                ),
                new SelectItemProjection(
                    "percentileDiscrete_integer_over_frame",
                    new PercentileDiscreteIntegerProjection(
                        new LiteralInteger(7),
                        new LiteralDecimal(0.5m),
                        over: new WindowWithFrame(
                            [new OrderItemRow(new LiteralInteger(7))],
                            new Frame(1, 1),
                            partitionBy: [new LiteralString("a")]
                        )
                    )
                ),
                new SelectItemProjection(
                    "mode_boolean_over_partition",
                    new ModeBooleanProjection(
                        new LiteralBoolean(true),
                        over: new WindowWithoutFrameWithoutRange([new LiteralString("a")])
                    )
                ),
                new SelectItemProjection(
                    "variancePopulation_decimal_over_frame",
                    new VariancePopulationDecimalProjection(
                        new LiteralDecimal(2.5m),
                        over: new WindowWithFrame(
                            [new OrderItemRow(new LiteralInteger(7))],
                            new Frame(new All())
                        )
                    )
                ),
                new SelectItemProjection(
                    "standardDeviationPopulation_decimal_over_partition",
                    new StandardDeviationPopulationDecimalProjection(
                        new LiteralDecimal(2.5m),
                        over: new WindowWithoutFrameWithoutRange([new LiteralString("a")])
                    )
                ),
                new SelectItemProjection(
                    "varianceSample_decimal_nullable_over_frame",
                    new VarianceSampleProjection(
                        new LiteralDecimal(2.5m),
                        over: new WindowWithFrame(
                            [new OrderItemRow(new LiteralInteger(7))],
                            new Frame(new All())
                        )
                    )
                ),
                new SelectItemProjection(
                    "standardDeviationSample_decimal_nullable_over_partition",
                    new StandardDeviationSampleProjection(
                        new LiteralDecimal(2.5m),
                        over: new WindowWithoutFrameWithoutRange([new LiteralString("a")])
                    )
                ),
                new SelectItemProjection(
                    "covariancePopulation_decimal",
                    new CovariancePopulationDecimalProjection(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m)
                    )
                ),
                new SelectItemProjection(
                    "covariancePopulation_decimal_of_integers",
                    new CovariancePopulationDecimalProjection(
                        new LiteralInteger(7),
                        new LiteralInteger(-3)
                    )
                ),
                new SelectItemProjection(
                    "covariancePopulation_decimal_nullable_left",
                    new CovariancePopulationDecimalNullableProjection(
                        new LiteralDecimalNullable(),
                        new LiteralDecimal(2.5m)
                    )
                ),
                new SelectItemProjection(
                    "covariancePopulation_decimal_nullable_right",
                    new CovariancePopulationDecimalNullableProjection(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimalNullable()
                    )
                ),
                new SelectItemProjection(
                    "covariancePopulation_decimal_nullable_where",
                    new CovariancePopulationDecimalNullableProjection(
                        new LiteralDecimal(2.5m),
                        new LiteralInteger(7),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "covarianceSample_decimal_nullable",
                    new CovarianceSampleProjection(
                        new LiteralDecimal(2.5m),
                        new LiteralInteger(7)
                    )
                ),
                new SelectItemProjection(
                    "covarianceSample_decimal_nullable_of_nulls_where",
                    new CovarianceSampleProjection(
                        new LiteralDecimalNullable(),
                        new LiteralIntegerNullable(),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "correlation_decimal_nullable",
                    new CorrelationProjection(
                        new LiteralDecimal(2.5m),
                        new LiteralInteger(7)
                    )
                ),
                new SelectItemProjection(
                    "correlation_decimal_nullable_of_nulls_where",
                    new CorrelationProjection(
                        new LiteralDecimalNullable(),
                        new LiteralDecimalNullable(),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "regressionSlope_decimal_nullable",
                    new RegressionSlopeProjection(
                        new LiteralDecimal(2.5m),
                        new LiteralInteger(7)
                    )
                ),
                new SelectItemProjection(
                    "regressionSlope_decimal_nullable_of_integers",
                    new RegressionSlopeProjection(
                        new LiteralInteger(7),
                        new LiteralInteger(-3)
                    )
                ),
                new SelectItemProjection(
                    "regressionSlope_decimal_nullable_of_nulls_where",
                    new RegressionSlopeProjection(
                        new LiteralDecimalNullable(),
                        new LiteralIntegerNullable(),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "regressionIntercept_decimal_nullable",
                    new RegressionInterceptProjection(
                        new LiteralDecimal(2.5m),
                        new LiteralInteger(7)
                    )
                ),
                new SelectItemProjection(
                    "regressionIntercept_decimal_nullable_of_integers",
                    new RegressionInterceptProjection(
                        new LiteralInteger(7),
                        new LiteralInteger(-3)
                    )
                ),
                new SelectItemProjection(
                    "regressionIntercept_decimal_nullable_of_nulls_where",
                    new RegressionInterceptProjection(
                        new LiteralDecimalNullable(),
                        new LiteralIntegerNullable(),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "regressionRSquared_decimal_nullable",
                    new RegressionRSquaredProjection(
                        new LiteralDecimal(2.5m),
                        new LiteralInteger(7)
                    )
                ),
                new SelectItemProjection(
                    "regressionRSquared_decimal_nullable_of_integers",
                    new RegressionRSquaredProjection(
                        new LiteralInteger(7),
                        new LiteralInteger(-3)
                    )
                ),
                new SelectItemProjection(
                    "regressionRSquared_decimal_nullable_of_nulls_where",
                    new RegressionRSquaredProjection(
                        new LiteralDecimalNullable(),
                        new LiteralIntegerNullable(),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "concatAll_string_over_frame",
                    new ConcatAllProjection(
                        new LiteralString("a"),
                        new LiteralString(","),
                        [new OrderItemRow(new LiteralInteger(7))],
                        over: new WindowWithFrame(
                            [new OrderItemRow(new LiteralInteger(7))],
                            new Frame(new All())
                        )
                    )
                ),
                new SelectItemProjection(
                    "any_boolean_over_partition",
                    new AnyProjection(
                        new LiteralBoolean(true),
                        over: new WindowWithoutFrameWithoutRange([new LiteralString("a")])
                    )
                ),
                new SelectItemProjection(
                    "all_boolean_over_frame",
                    new AllProjection(
                        new LiteralBoolean(true),
                        over: new WindowWithFrame(
                            [new OrderItemRow(new LiteralInteger(7))],
                            new Frame(following: new All())
                        )
                    )
                ),
            ]
        );
}
