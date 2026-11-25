using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
using PureQL.CSharp.Model.Lists;
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.Parameters;
using PureQL.CSharp.Model.TotalExpressions;

namespace PureQL.CSharp.Model.Samples.Queries.Aggregates;

/// <summary>
/// Aggregates schema_with_foreign_keys.orders into one row of 811 columns, each an
/// operator over literals, one per signature of every operator: the lifted operators with
/// and without a nullable operand, integer and decimal operands, if and coalesce for
/// every type, equal, notEqual, the four comparisons and in for every family, and every
/// aggregate over a literal selector, with and without a predicate and with a nullable
/// selector. Some operands are parameters, so the query has no result.
/// </summary>
public sealed record EveryOperatorSignatureInATotalQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainTotalQuery(
            from: new FromEntity(
                new JoinedString(
                    new DotString(),
                    [new RelationalSchemaWithForeignKeys().Name, new OrdersTable().Name]
                ).TextValue
            ),
            aggregate:
            [
                new SelectItemTotal(
                    "add_integer",
                    new AddIntegerTotal([new LiteralInteger(7), new LiteralInteger(-3)])
                ),
                new SelectItemTotal(
                    "add_integer_nullable",
                    new AddIntegerNullableTotal([
                        new LiteralInteger(7),
                        new LiteralIntegerNullable(),
                    ])
                ),
                new SelectItemTotal(
                    "add_decimal",
                    new AddDecimalTotal([new LiteralInteger(7), new LiteralDecimal(2.5m)])
                ),
                new SelectItemTotal(
                    "add_decimal_nullable",
                    new AddDecimalNullableTotal([
                        new LiteralDecimalNullable(),
                        new LiteralInteger(7),
                    ])
                ),
                new SelectItemTotal(
                    "subtract_integer",
                    new SubtractIntegerTotal([
                        new LiteralInteger(7),
                        new LiteralInteger(-3),
                    ])
                ),
                new SelectItemTotal(
                    "subtract_integer_nullable",
                    new SubtractIntegerNullableTotal([
                        new LiteralInteger(7),
                        new LiteralIntegerNullable(),
                    ])
                ),
                new SelectItemTotal(
                    "subtract_decimal",
                    new SubtractDecimalTotal([
                        new LiteralInteger(7),
                        new LiteralDecimal(2.5m),
                    ])
                ),
                new SelectItemTotal(
                    "subtract_decimal_nullable",
                    new SubtractDecimalNullableTotal([
                        new LiteralDecimalNullable(),
                        new LiteralInteger(7),
                    ])
                ),
                new SelectItemTotal(
                    "multiply_integer",
                    new MultiplyIntegerTotal([
                        new LiteralInteger(7),
                        new LiteralInteger(-3),
                    ])
                ),
                new SelectItemTotal(
                    "multiply_integer_nullable",
                    new MultiplyIntegerNullableTotal([
                        new LiteralInteger(7),
                        new LiteralIntegerNullable(),
                    ])
                ),
                new SelectItemTotal(
                    "multiply_decimal",
                    new MultiplyDecimalTotal([
                        new LiteralInteger(7),
                        new LiteralDecimal(2.5m),
                    ])
                ),
                new SelectItemTotal(
                    "multiply_decimal_nullable",
                    new MultiplyDecimalNullableTotal([
                        new LiteralDecimalNullable(),
                        new LiteralInteger(7),
                    ])
                ),
                new SelectItemTotal(
                    "divide_decimal",
                    new DivideDecimalTotal([
                        new LiteralInteger(7),
                        new LiteralDecimal(2.5m),
                    ])
                ),
                new SelectItemTotal(
                    "divide_decimal_nullable",
                    new DivideDecimalNullableTotal([
                        new LiteralDecimalNullable(),
                        new LiteralInteger(7),
                    ])
                ),
                new SelectItemTotal(
                    "integerDivide_integer",
                    new IntegerDivideIntegerTotal(
                        new LiteralInteger(7),
                        new LiteralInteger(-3)
                    )
                ),
                new SelectItemTotal(
                    "integerDivide_integer_nullable",
                    new IntegerDivideIntegerNullableTotal(
                        new LiteralIntegerNullable(),
                        new LiteralInteger(-3)
                    )
                ),
                new SelectItemTotal(
                    "modulo_integer",
                    new ModuloIntegerTotal(new LiteralInteger(7), new LiteralInteger(-3))
                ),
                new SelectItemTotal(
                    "modulo_integer_nullable",
                    new ModuloIntegerNullableTotal(
                        new LiteralIntegerNullable(),
                        new LiteralInteger(-3)
                    )
                ),
                new SelectItemTotal(
                    "modulo_decimal",
                    new ModuloDecimalTotal(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m)
                    )
                ),
                new SelectItemTotal(
                    "modulo_decimal_of_integer_and_decimal",
                    new ModuloDecimalTotal(
                        new LiteralInteger(7),
                        new LiteralDecimal(2.5m)
                    )
                ),
                new SelectItemTotal(
                    "modulo_decimal_of_decimal_and_integer",
                    new ModuloDecimalTotal(
                        new LiteralDecimal(2.5m),
                        new LiteralInteger(7)
                    )
                ),
                new SelectItemTotal(
                    "modulo_decimal_nullable",
                    new ModuloDecimalNullableTotal(
                        new LiteralDecimalNullable(),
                        new LiteralDecimal(0.1m)
                    )
                ),
                new SelectItemTotal(
                    "modulo_decimal_nullable_right",
                    new ModuloDecimalNullableTotal(
                        new LiteralDecimal(2.5m),
                        new LiteralIntegerNullable()
                    )
                ),
                new SelectItemTotal(
                    "integerDivide_integer_of_decimals",
                    new IntegerDivideIntegerTotal(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m)
                    )
                ),
                new SelectItemTotal(
                    "integerDivide_integer_of_integer_and_decimal",
                    new IntegerDivideIntegerTotal(
                        new LiteralInteger(7),
                        new LiteralDecimal(2.5m)
                    )
                ),
                new SelectItemTotal(
                    "integerDivide_integer_nullable_of_decimals",
                    new IntegerDivideIntegerNullableTotal(
                        new LiteralDecimalNullable(),
                        new LiteralDecimal(0.1m)
                    )
                ),
                new SelectItemTotal(
                    "floor_integer",
                    new FloorIntegerTotal(new LiteralDecimal(2.5m))
                ),
                new SelectItemTotal(
                    "floor_integer_nullable",
                    new FloorIntegerNullableTotal(new LiteralDecimalNullable())
                ),
                new SelectItemTotal(
                    "ceiling_integer",
                    new CeilingIntegerTotal(new LiteralDecimal(2.5m))
                ),
                new SelectItemTotal(
                    "ceiling_integer_nullable",
                    new CeilingIntegerNullableTotal(new LiteralDecimalNullable())
                ),
                new SelectItemTotal(
                    "round_integer",
                    new RoundWithoutDigitsIntegerTotal(new LiteralDecimal(2.5m))
                ),
                new SelectItemTotal(
                    "round_integer_nullable",
                    new RoundWithoutDigitsIntegerNullableTotal(
                        new LiteralDecimalNullable()
                    )
                ),
                new SelectItemTotal(
                    "round_decimal",
                    new RoundDecimalTotal(
                        new LiteralDecimal(2.5m),
                        new LiteralInteger(-3)
                    )
                ),
                new SelectItemTotal(
                    "round_decimal_nullable",
                    new RoundDecimalNullableTotal(
                        new LiteralDecimalNullable(),
                        new LiteralInteger(7)
                    )
                ),
                new SelectItemTotal(
                    "round_integer_half_even",
                    new RoundWithoutDigitsIntegerTotal(
                        new LiteralDecimal(2.5m),
                        mode: RoundingMode.HalfEven
                    )
                ),
                new SelectItemTotal(
                    "round_integer_nullable_half_even",
                    new RoundWithoutDigitsIntegerNullableTotal(
                        new LiteralDecimalNullable(),
                        mode: RoundingMode.HalfEven
                    )
                ),
                new SelectItemTotal(
                    "round_decimal_half_even",
                    new RoundDecimalTotal(
                        new LiteralDecimal(2.5m),
                        new LiteralInteger(2),
                        mode: RoundingMode.HalfEven
                    )
                ),
                new SelectItemTotal(
                    "round_decimal_nullable_half_even",
                    new RoundDecimalNullableTotal(
                        new LiteralDecimalNullable(),
                        new LiteralInteger(2),
                        mode: RoundingMode.HalfEven
                    )
                ),
                new SelectItemTotal(
                    "round_integer_of_integer_half_even",
                    new RoundWithDigitsIntegerTotal(
                        new LiteralInteger(7),
                        new LiteralInteger(-1),
                        mode: RoundingMode.HalfEven
                    )
                ),
                new SelectItemTotal(
                    "truncate_integer",
                    new TruncateWithoutDigitsIntegerTotal(new LiteralDecimal(2.5m))
                ),
                new SelectItemTotal(
                    "truncate_integer_nullable",
                    new TruncateWithoutDigitsIntegerNullableTotal(
                        new LiteralDecimalNullable()
                    )
                ),
                new SelectItemTotal(
                    "truncate_decimal",
                    new TruncateDecimalTotal(
                        new LiteralDecimal(2.5m),
                        new LiteralInteger(7)
                    )
                ),
                new SelectItemTotal(
                    "truncate_decimal_nullable",
                    new TruncateDecimalNullableTotal(
                        new LiteralDecimalNullable(),
                        new LiteralInteger(7)
                    )
                ),
                new SelectItemTotal(
                    "abs_integer",
                    new AbsIntegerTotal(new LiteralInteger(-3))
                ),
                new SelectItemTotal(
                    "abs_integer_nullable",
                    new AbsIntegerNullableTotal(new LiteralIntegerNullable())
                ),
                new SelectItemTotal(
                    "abs_decimal",
                    new AbsDecimalTotal(new LiteralDecimal(2.5m))
                ),
                new SelectItemTotal(
                    "abs_decimal_nullable",
                    new AbsDecimalNullableTotal(new LiteralDecimalNullable())
                ),
                new SelectItemTotal(
                    "sign_integer",
                    new SignIntegerTotal(new LiteralDecimal(2.5m))
                ),
                new SelectItemTotal(
                    "sign_integer_nullable",
                    new SignIntegerNullableTotal(new LiteralDecimalNullable())
                ),
                new SelectItemTotal(
                    "sign_integer_of_integer",
                    new SignIntegerTotal(new LiteralInteger(-3))
                ),
                new SelectItemTotal(
                    "power_decimal",
                    new PowerDecimalTotal(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m)
                    )
                ),
                new SelectItemTotal(
                    "power_decimal_nullable",
                    new PowerDecimalNullableTotal(
                        new LiteralDecimalNullable(),
                        new LiteralInteger(7)
                    )
                ),
                new SelectItemTotal(
                    "power_decimal_of_integers",
                    new PowerDecimalTotal(new LiteralInteger(7), new LiteralInteger(-3))
                ),
                new SelectItemTotal(
                    "sqrt_decimal",
                    new SqrtDecimalTotal(new LiteralDecimal(2.5m))
                ),
                new SelectItemTotal(
                    "sqrt_decimal_nullable",
                    new SqrtDecimalNullableTotal(new LiteralDecimalNullable())
                ),
                new SelectItemTotal(
                    "sqrt_decimal_of_integer",
                    new SqrtDecimalTotal(new LiteralInteger(7))
                ),
                new SelectItemTotal(
                    "exp_decimal",
                    new ExpDecimalTotal(new LiteralDecimal(2.5m))
                ),
                new SelectItemTotal(
                    "exp_decimal_nullable",
                    new ExpDecimalNullableTotal(new LiteralDecimalNullable())
                ),
                new SelectItemTotal(
                    "exp_decimal_of_integer",
                    new ExpDecimalTotal(new LiteralInteger(7))
                ),
                new SelectItemTotal(
                    "ln_decimal",
                    new LnDecimalTotal(new LiteralDecimal(2.5m))
                ),
                new SelectItemTotal(
                    "ln_decimal_nullable",
                    new LnDecimalNullableTotal(new LiteralDecimalNullable())
                ),
                new SelectItemTotal(
                    "ln_decimal_of_integer",
                    new LnDecimalTotal(new LiteralInteger(7))
                ),
                new SelectItemTotal(
                    "log10_decimal",
                    new Log10DecimalTotal(new LiteralDecimal(2.5m))
                ),
                new SelectItemTotal(
                    "log10_decimal_nullable",
                    new Log10DecimalNullableTotal(new LiteralDecimalNullable())
                ),
                new SelectItemTotal(
                    "log10_decimal_of_integer",
                    new Log10DecimalTotal(new LiteralInteger(7))
                ),
                new SelectItemTotal(
                    "log_decimal",
                    new LogDecimalTotal(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m)
                    )
                ),
                new SelectItemTotal(
                    "log_decimal_nullable",
                    new LogDecimalNullableTotal(
                        new LiteralDecimalNullable(),
                        new LiteralInteger(7)
                    )
                ),
                new SelectItemTotal(
                    "sin_decimal",
                    new SinDecimalTotal(new LiteralDecimal(0.5m))
                ),
                new SelectItemTotal(
                    "sin_decimal_of_integer",
                    new SinDecimalTotal(new LiteralInteger(1))
                ),
                new SelectItemTotal(
                    "sin_decimal_nullable",
                    new SinDecimalNullableTotal(new LiteralDecimalNullable())
                ),
                new SelectItemTotal(
                    "cos_decimal",
                    new CosDecimalTotal(new LiteralDecimal(0.5m))
                ),
                new SelectItemTotal(
                    "cos_decimal_of_integer",
                    new CosDecimalTotal(new LiteralInteger(1))
                ),
                new SelectItemTotal(
                    "cos_decimal_nullable",
                    new CosDecimalNullableTotal(new LiteralDecimalNullable())
                ),
                new SelectItemTotal(
                    "tan_decimal",
                    new TanDecimalTotal(new LiteralDecimal(0.5m))
                ),
                new SelectItemTotal(
                    "tan_decimal_of_integer",
                    new TanDecimalTotal(new LiteralInteger(1))
                ),
                new SelectItemTotal(
                    "tan_decimal_nullable",
                    new TanDecimalNullableTotal(new LiteralDecimalNullable())
                ),
                new SelectItemTotal(
                    "asin_decimal",
                    new AsinDecimalTotal(new LiteralDecimal(0.5m))
                ),
                new SelectItemTotal(
                    "asin_decimal_of_integer",
                    new AsinDecimalTotal(new LiteralInteger(1))
                ),
                new SelectItemTotal(
                    "asin_decimal_nullable",
                    new AsinDecimalNullableTotal(new LiteralDecimalNullable())
                ),
                new SelectItemTotal(
                    "acos_decimal",
                    new AcosDecimalTotal(new LiteralDecimal(0.5m))
                ),
                new SelectItemTotal(
                    "acos_decimal_of_integer",
                    new AcosDecimalTotal(new LiteralInteger(1))
                ),
                new SelectItemTotal(
                    "acos_decimal_nullable",
                    new AcosDecimalNullableTotal(new LiteralDecimalNullable())
                ),
                new SelectItemTotal(
                    "atan_decimal",
                    new AtanDecimalTotal(new LiteralDecimal(0.5m))
                ),
                new SelectItemTotal(
                    "atan_decimal_of_integer",
                    new AtanDecimalTotal(new LiteralInteger(1))
                ),
                new SelectItemTotal(
                    "atan_decimal_nullable",
                    new AtanDecimalNullableTotal(new LiteralDecimalNullable())
                ),
                new SelectItemTotal(
                    "atan2_decimal",
                    new Atan2DecimalTotal(
                        new LiteralDecimal(0.5m),
                        new LiteralDecimal(-1m)
                    )
                ),
                new SelectItemTotal(
                    "atan2_decimal_of_integers",
                    new Atan2DecimalTotal(new LiteralInteger(1), new LiteralInteger(-3))
                ),
                new SelectItemTotal(
                    "atan2_decimal_nullable_y",
                    new Atan2DecimalNullableTotal(
                        new LiteralDecimalNullable(),
                        new LiteralDecimal(0.5m)
                    )
                ),
                new SelectItemTotal(
                    "atan2_decimal_nullable_x",
                    new Atan2DecimalNullableTotal(
                        new LiteralDecimal(0.5m),
                        new LiteralDecimalNullable()
                    )
                ),
                new SelectItemTotal("pi_decimal", new PiTotal()),
                new SelectItemTotal(
                    "log_decimal_of_integers",
                    new LogDecimalTotal(new LiteralInteger(7), new LiteralInteger(7))
                ),
                new SelectItemTotal(
                    "concat_string",
                    new ConcatStringTotal([
                        new LiteralString("a"),
                        new LiteralString("b"),
                    ])
                ),
                new SelectItemTotal(
                    "concat_string_nullable",
                    new ConcatStringNullableTotal([
                        new LiteralString("a"),
                        new LiteralStringNullable(),
                    ])
                ),
                new SelectItemTotal(
                    "length_integer",
                    new LengthIntegerTotal(new LiteralString("abc"))
                ),
                new SelectItemTotal(
                    "length_integer_nullable",
                    new LengthIntegerNullableTotal(new LiteralStringNullable())
                ),
                new SelectItemTotal(
                    "upper_string",
                    new UpperStringTotal(new LiteralString("Stra\u00dfe"))
                ),
                new SelectItemTotal(
                    "upper_string_nullable",
                    new UpperStringNullableTotal(new LiteralStringNullable())
                ),
                new SelectItemTotal(
                    "lower_string",
                    new LowerStringTotal(new LiteralString("Stra\u00dfe"))
                ),
                new SelectItemTotal(
                    "lower_string_nullable",
                    new LowerStringNullableTotal(new LiteralStringNullable())
                ),
                new SelectItemTotal(
                    "foldCase_string",
                    new FoldCaseStringTotal(new LiteralString("a"))
                ),
                new SelectItemTotal(
                    "foldCase_string_nullable",
                    new FoldCaseStringNullableTotal(new LiteralStringNullable())
                ),
                new SelectItemTotal(
                    "normalize_string_nfc",
                    new NormalizeStringTotal(
                        new LiteralString("a"),
                        NormalizationForm.NFC
                    )
                ),
                new SelectItemTotal(
                    "normalize_string_nfd",
                    new NormalizeStringTotal(
                        new LiteralString("a"),
                        NormalizationForm.NFD
                    )
                ),
                new SelectItemTotal(
                    "normalize_string_nfkc",
                    new NormalizeStringTotal(
                        new LiteralString("a"),
                        NormalizationForm.NFKC
                    )
                ),
                new SelectItemTotal(
                    "normalize_string_nfkd",
                    new NormalizeStringTotal(
                        new LiteralString("a"),
                        NormalizationForm.NFKD
                    )
                ),
                new SelectItemTotal(
                    "normalize_string_nullable",
                    new NormalizeStringNullableTotal(
                        new LiteralStringNullable(),
                        NormalizationForm.NFC
                    )
                ),
                new SelectItemTotal(
                    "normalize_string_nullable_nfkd",
                    new NormalizeStringNullableTotal(
                        new LiteralStringNullable(),
                        NormalizationForm.NFKD
                    )
                ),
                new SelectItemTotal(
                    "removeDiacritics_string",
                    new RemoveDiacriticsStringTotal(new LiteralString("a"))
                ),
                new SelectItemTotal(
                    "removeDiacritics_string_nullable",
                    new RemoveDiacriticsStringNullableTotal(new LiteralStringNullable())
                ),
                new SelectItemTotal(
                    "trim_string",
                    new TrimStringTotal(
                        new LiteralString("xax"),
                        [new LiteralString("x")]
                    )
                ),
                new SelectItemTotal(
                    "trim_string_nullable",
                    new TrimStringNullableTotal(
                        new LiteralStringNullable(),
                        [new LiteralString("x"), new LiteralString("xa")]
                    )
                ),
                new SelectItemTotal(
                    "trimStart_string",
                    new TrimStartStringTotal(
                        new LiteralString("xax"),
                        [new LiteralString("x")]
                    )
                ),
                new SelectItemTotal(
                    "trimStart_string_nullable",
                    new TrimStartStringNullableTotal(
                        new LiteralStringNullable(),
                        [new LiteralString("x"), new LiteralString("xa")]
                    )
                ),
                new SelectItemTotal(
                    "trimEnd_string",
                    new TrimEndStringTotal(
                        new LiteralString("xax"),
                        [new LiteralString("x")]
                    )
                ),
                new SelectItemTotal(
                    "trimEnd_string_nullable",
                    new TrimEndStringNullableTotal(
                        new LiteralStringNullable(),
                        [new LiteralString("x"), new LiteralString("xa")]
                    )
                ),
                new SelectItemTotal(
                    "substring_string",
                    new SubstringStringTotal(
                        new LiteralString("hello"),
                        new LiteralInteger(2)
                    )
                ),
                new SelectItemTotal(
                    "substring_string_nullable",
                    new SubstringStringNullableTotal(
                        new LiteralStringNullable(),
                        new LiteralInteger(2)
                    )
                ),
                new SelectItemTotal(
                    "substring_string_length",
                    new SubstringStringTotal(
                        new LiteralString("hello"),
                        new LiteralInteger(2),
                        length: new LiteralInteger(3)
                    )
                ),
                new SelectItemTotal(
                    "substring_string_nullable_length",
                    new SubstringStringNullableTotal(
                        new LiteralStringNullable(),
                        new LiteralInteger(2),
                        length: new LiteralInteger(0)
                    )
                ),
                new SelectItemTotal(
                    "replace_string",
                    new ReplaceStringTotal(
                        new LiteralString("a-b"),
                        new LiteralString("-"),
                        new LiteralString("b")
                    )
                ),
                new SelectItemTotal(
                    "replace_string_nullable",
                    new ReplaceStringNullableTotal(
                        new LiteralStringNullable(),
                        new LiteralString("-"),
                        new LiteralString("b")
                    )
                ),
                new SelectItemTotal(
                    "position_integer",
                    new PositionIntegerTotal(
                        new LiteralString("a-b"),
                        new LiteralString("-")
                    )
                ),
                new SelectItemTotal(
                    "position_integer_nullable",
                    new PositionIntegerNullableTotal(
                        new LiteralStringNullable(),
                        new LiteralString("-")
                    )
                ),
                new SelectItemTotal(
                    "position_integer_start",
                    new PositionIntegerTotal(
                        new LiteralString("a-b"),
                        new LiteralString("-"),
                        start: new LiteralInteger(2)
                    )
                ),
                new SelectItemTotal(
                    "position_integer_nullable_start",
                    new PositionIntegerNullableTotal(
                        new LiteralStringNullable(),
                        new LiteralString("-"),
                        start: new LiteralInteger(2)
                    )
                ),
                new SelectItemTotal(
                    "splitPart_string",
                    new SplitPartStringTotal(
                        new LiteralString("a-b"),
                        new LiteralString("-"),
                        new LiteralInteger(1)
                    )
                ),
                new SelectItemTotal(
                    "splitPart_string_nullable",
                    new SplitPartStringNullableTotal(
                        new LiteralStringNullable(),
                        new LiteralString("-"),
                        new LiteralInteger(-3)
                    )
                ),
                new SelectItemTotal(
                    "padStart_string",
                    new PadStartStringTotal(
                        new LiteralString("a"),
                        new LiteralInteger(7),
                        new LiteralString("b")
                    )
                ),
                new SelectItemTotal(
                    "padStart_string_nullable",
                    new PadStartStringNullableTotal(
                        new LiteralStringNullable(),
                        new LiteralInteger(7),
                        new LiteralString("b")
                    )
                ),
                new SelectItemTotal(
                    "padEnd_string",
                    new PadEndStringTotal(
                        new LiteralString("a"),
                        new LiteralInteger(7),
                        new LiteralString("b")
                    )
                ),
                new SelectItemTotal(
                    "padEnd_string_nullable",
                    new PadEndStringNullableTotal(
                        new LiteralStringNullable(),
                        new LiteralInteger(7),
                        new LiteralString("b")
                    )
                ),
                new SelectItemTotal(
                    "repeat_string",
                    new RepeatStringTotal(new LiteralString("a"), new LiteralInteger(7))
                ),
                new SelectItemTotal(
                    "repeat_string_nullable",
                    new RepeatStringNullableTotal(
                        new LiteralStringNullable(),
                        new LiteralInteger(7)
                    )
                ),
                new SelectItemTotal(
                    "reverse_string",
                    new ReverseStringTotal(new LiteralString("a"))
                ),
                new SelectItemTotal(
                    "reverse_string_nullable",
                    new ReverseStringNullableTotal(new LiteralStringNullable())
                ),
                new SelectItemTotal(
                    "concatWith_string",
                    new ConcatWithTotal(
                        [new LiteralString("a"), new LiteralString("b")],
                        new LiteralString("-")
                    )
                ),
                new SelectItemTotal(
                    "concatWith_string_null_values",
                    new ConcatWithTotal(
                        [
                            new LiteralStringNullable(),
                            new LiteralString("a"),
                            new LiteralStringNullable(),
                        ],
                        new LiteralString("-")
                    )
                ),
                new SelectItemTotal(
                    "concatWith_string_only_null_values",
                    new ConcatWithTotal(
                        [new LiteralStringNullable(), new LiteralStringNullable()],
                        new LiteralString("-")
                    )
                ),
                new SelectItemTotal(
                    "matchesRegex_boolean",
                    new MatchesRegexTotal(
                        new LiteralString("a1"),
                        new LiteralString("[0-9]")
                    )
                ),
                new SelectItemTotal(
                    "matchesRegex_boolean_nullable_operand",
                    new MatchesRegexTotal(
                        new LiteralStringNullable(),
                        new LiteralString("[0-9]")
                    )
                ),
                new SelectItemTotal(
                    "matchesRegex_boolean_parameter_pattern",
                    new MatchesRegexTotal(
                        new LiteralString("a1"),
                        new ParamString("pattern")
                    )
                ),
                new SelectItemTotal(
                    "extractRegex_string_nullable",
                    new ExtractRegexTotal(
                        new LiteralString("a1"),
                        new LiteralString("[0-9]+")
                    )
                ),
                new SelectItemTotal(
                    "extractRegex_string_nullable_group",
                    new ExtractRegexTotal(
                        new LiteralString("a1"),
                        new LiteralString("(a)([0-9])"),
                        group: new LiteralInteger(2)
                    )
                ),
                new SelectItemTotal(
                    "extractRegex_string_nullable_nullable_operand",
                    new ExtractRegexTotal(
                        new LiteralStringNullable(),
                        new LiteralString("[0-9]+")
                    )
                ),
                new SelectItemTotal(
                    "extractRegex_string_nullable_parameters",
                    new ExtractRegexTotal(
                        new LiteralString("a1"),
                        new ParamString("pattern"),
                        group: new ParamInteger("group")
                    )
                ),
                new SelectItemTotal(
                    "replaceRegex_string",
                    new ReplaceRegexStringTotal(
                        new LiteralString("a1"),
                        new LiteralString("b"),
                        new LiteralString("[0-9]")
                    )
                ),
                new SelectItemTotal(
                    "replaceRegex_string_nullable",
                    new ReplaceRegexStringNullableTotal(
                        new LiteralStringNullable(),
                        new LiteralString("b"),
                        new LiteralString("[0-9]")
                    )
                ),
                new SelectItemTotal(
                    "round_decimal_nullable_by_digits",
                    new RoundDecimalNullableTotal(
                        new LiteralDecimal(2.5m),
                        new LiteralIntegerNullable()
                    )
                ),
                new SelectItemTotal(
                    "truncate_decimal_nullable_by_digits",
                    new TruncateDecimalNullableTotal(
                        new LiteralDecimal(2.5m),
                        new LiteralIntegerNullable()
                    )
                ),
                new SelectItemTotal(
                    "round_integer_of_integer_with_digits",
                    new RoundWithDigitsIntegerTotal(
                        new LiteralInteger(7),
                        new LiteralInteger(1)
                    )
                ),
                new SelectItemTotal(
                    "round_integer_nullable_of_integer_with_digits",
                    new RoundWithDigitsIntegerNullableTotal(
                        new LiteralIntegerNullable(),
                        new LiteralInteger(-1)
                    )
                ),
                new SelectItemTotal(
                    "round_integer_nullable_of_integer_by_digits",
                    new RoundWithDigitsIntegerNullableTotal(
                        new LiteralInteger(7),
                        new LiteralIntegerNullable()
                    )
                ),
                new SelectItemTotal(
                    "truncate_integer_of_integer_with_digits",
                    new TruncateWithDigitsIntegerTotal(
                        new LiteralInteger(7),
                        new LiteralInteger(-1)
                    )
                ),
                new SelectItemTotal(
                    "truncate_integer_nullable_of_integer_with_digits",
                    new TruncateWithDigitsIntegerNullableTotal(
                        new LiteralIntegerNullable(),
                        new LiteralInteger(2)
                    )
                ),
                new SelectItemTotal(
                    "truncate_integer_nullable_of_integer_by_digits",
                    new TruncateWithDigitsIntegerNullableTotal(
                        new LiteralInteger(7),
                        new LiteralIntegerNullable()
                    )
                ),
                new SelectItemTotal(
                    "power_decimal_nullable_by_exponent",
                    new PowerDecimalNullableTotal(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimalNullable()
                    )
                ),
                new SelectItemTotal(
                    "log_decimal_nullable_by_base",
                    new LogDecimalNullableTotal(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimalNullable()
                    )
                ),
                new SelectItemTotal(
                    "substring_string_nullable_by_start",
                    new SubstringStringNullableTotal(
                        new LiteralString("abc"),
                        new LiteralIntegerNullable(),
                        length: new LiteralInteger(2)
                    )
                ),
                new SelectItemTotal(
                    "substring_string_nullable_by_length",
                    new SubstringStringNullableTotal(
                        new LiteralString("abc"),
                        new LiteralInteger(1),
                        length: new LiteralIntegerNullable()
                    )
                ),
                new SelectItemTotal(
                    "replace_string_nullable_by_pattern",
                    new ReplaceStringNullableTotal(
                        new LiteralString("abc"),
                        new LiteralStringNullable(),
                        new LiteralString("x")
                    )
                ),
                new SelectItemTotal(
                    "replace_string_nullable_by_replacement",
                    new ReplaceStringNullableTotal(
                        new LiteralString("abc"),
                        new LiteralString("b"),
                        new LiteralStringNullable()
                    )
                ),
                new SelectItemTotal(
                    "position_integer_nullable_by_pattern",
                    new PositionIntegerNullableTotal(
                        new LiteralString("abc"),
                        new LiteralStringNullable(),
                        start: new LiteralInteger(1)
                    )
                ),
                new SelectItemTotal(
                    "position_integer_nullable_by_start",
                    new PositionIntegerNullableTotal(
                        new LiteralString("abc"),
                        new LiteralString("b"),
                        start: new LiteralIntegerNullable()
                    )
                ),
                new SelectItemTotal(
                    "splitPart_string_nullable_by_delimiter",
                    new SplitPartStringNullableTotal(
                        new LiteralString("a-b"),
                        new LiteralStringNullable(),
                        new LiteralInteger(1)
                    )
                ),
                new SelectItemTotal(
                    "splitPart_string_nullable_by_index",
                    new SplitPartStringNullableTotal(
                        new LiteralString("a-b"),
                        new LiteralString("-"),
                        new LiteralIntegerNullable()
                    )
                ),
                new SelectItemTotal(
                    "padStart_string_nullable_by_length",
                    new PadStartStringNullableTotal(
                        new LiteralString("abc"),
                        new LiteralIntegerNullable(),
                        new LiteralString("0")
                    )
                ),
                new SelectItemTotal(
                    "padStart_string_nullable_by_fill",
                    new PadStartStringNullableTotal(
                        new LiteralString("abc"),
                        new LiteralInteger(7),
                        new LiteralStringNullable()
                    )
                ),
                new SelectItemTotal(
                    "padEnd_string_nullable_by_length",
                    new PadEndStringNullableTotal(
                        new LiteralString("abc"),
                        new LiteralIntegerNullable(),
                        new LiteralString("0")
                    )
                ),
                new SelectItemTotal(
                    "padEnd_string_nullable_by_fill",
                    new PadEndStringNullableTotal(
                        new LiteralString("abc"),
                        new LiteralInteger(7),
                        new LiteralStringNullable()
                    )
                ),
                new SelectItemTotal(
                    "repeat_string_nullable_by_count",
                    new RepeatStringNullableTotal(
                        new LiteralString("abc"),
                        new LiteralIntegerNullable()
                    )
                ),
                new SelectItemTotal(
                    "replaceRegex_string_nullable_by_replacement",
                    new ReplaceRegexStringNullableTotal(
                        new LiteralString("abc"),
                        new LiteralStringNullable(),
                        new LiteralString("b+")
                    )
                ),
                new SelectItemTotal(
                    "replaceRegex_string_parameter_pattern",
                    new ReplaceRegexStringTotal(
                        new LiteralString("a1"),
                        new LiteralString("b"),
                        new ParamString("pattern")
                    )
                ),
                new SelectItemTotal(
                    "contains_boolean",
                    new ContainsTotal(
                        new LiteralString("abcabc"),
                        new LiteralString("abc")
                    )
                ),
                new SelectItemTotal(
                    "contains_boolean_nullable_operand",
                    new ContainsTotal(
                        new LiteralStringNullable(),
                        new LiteralString("abc")
                    )
                ),
                new SelectItemTotal(
                    "contains_boolean_nullable_pattern",
                    new ContainsTotal(
                        new LiteralString("abcabc"),
                        new LiteralStringNullable()
                    )
                ),
                new SelectItemTotal(
                    "startsWith_boolean",
                    new StartsWithTotal(
                        new LiteralString("abcabc"),
                        new LiteralString("abc")
                    )
                ),
                new SelectItemTotal(
                    "startsWith_boolean_nullable_operand",
                    new StartsWithTotal(
                        new LiteralStringNullable(),
                        new LiteralString("abc")
                    )
                ),
                new SelectItemTotal(
                    "startsWith_boolean_nullable_pattern",
                    new StartsWithTotal(
                        new LiteralString("abcabc"),
                        new LiteralStringNullable()
                    )
                ),
                new SelectItemTotal(
                    "endsWith_boolean",
                    new EndsWithTotal(
                        new LiteralString("abcabc"),
                        new LiteralString("abc")
                    )
                ),
                new SelectItemTotal(
                    "endsWith_boolean_nullable_operand",
                    new EndsWithTotal(
                        new LiteralStringNullable(),
                        new LiteralString("abc")
                    )
                ),
                new SelectItemTotal(
                    "endsWith_boolean_nullable_pattern",
                    new EndsWithTotal(
                        new LiteralString("abcabc"),
                        new LiteralStringNullable()
                    )
                ),
                new SelectItemTotal(
                    "dateAddDays_date",
                    new DateAddDaysDateTotal(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralInteger(7)
                    )
                ),
                new SelectItemTotal(
                    "dateAddDays_date_nullable",
                    new DateAddDaysDateNullableTotal(
                        new LiteralDateNullable(),
                        new LiteralInteger(7)
                    )
                ),
                new SelectItemTotal(
                    "dateDiffDays_integer",
                    new DateDiffDaysIntegerTotal(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDate(new DateOnly(2024, 3, 1))
                    )
                ),
                new SelectItemTotal(
                    "dateDiffDays_integer_nullable",
                    new DateDiffDaysIntegerNullableTotal(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDateNullable()
                    )
                ),
                new SelectItemTotal(
                    "timeAddSeconds_time",
                    new TimeAddSecondsTimeTotal(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralDecimal(2.5m)
                    )
                ),
                new SelectItemTotal(
                    "timeAddSeconds_time_nullable",
                    new TimeAddSecondsTimeNullableTotal(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralIntegerNullable()
                    )
                ),
                new SelectItemTotal(
                    "timeDiffSeconds_decimal",
                    new TimeDiffSecondsDecimalTotal(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralTime(new TimeOnly(17, 45, 30, 500))
                    )
                ),
                new SelectItemTotal(
                    "timeDiffSeconds_decimal_nullable",
                    new TimeDiffSecondsDecimalNullableTotal(
                        new LiteralTimeNullable(),
                        new LiteralTime(new TimeOnly(8, 30, 0))
                    )
                ),
                new SelectItemTotal(
                    "datetimeAddSeconds_datetime",
                    new DatetimeAddSecondsDatetimeTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDecimal(2.5m)
                    )
                ),
                new SelectItemTotal(
                    "datetimeAddSeconds_datetime_nullable",
                    new DatetimeAddSecondsDatetimeNullableTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralIntegerNullable()
                    )
                ),
                new SelectItemTotal(
                    "datetimeDiffSeconds_decimal",
                    new DatetimeDiffSecondsDecimalTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemTotal(
                    "datetimeDiffSeconds_decimal_nullable",
                    new DatetimeDiffSecondsDecimalNullableTotal(
                        new LiteralDatetimeNullable(),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemTotal(
                    "dateAddMonths_date",
                    new DateAddMonthsDateTotal(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralInteger(1)
                    )
                ),
                new SelectItemTotal(
                    "dateAddMonths_date_nullable",
                    new DateAddMonthsDateNullableTotal(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralIntegerNullable()
                    )
                ),
                new SelectItemTotal(
                    "datetimeAddMonths_datetime",
                    new DatetimeAddMonthsDatetimeTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 18, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralInteger(1)
                    )
                ),
                new SelectItemTotal(
                    "datetimeAddMonths_datetime_nullable",
                    new DatetimeAddMonthsDatetimeNullableTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 18, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralIntegerNullable()
                    )
                ),
                new SelectItemTotal(
                    "datetimeAddMonths_datetime_in_zone",
                    new DatetimeAddMonthsDatetimeTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralInteger(1),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemTotal(
                    "datetimeAddMonths_datetime_nullable_in_zone",
                    new DatetimeAddMonthsDatetimeNullableTotal(
                        new LiteralDatetimeNullable(),
                        new LiteralInteger(1),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemTotal(
                    "datetimeAddMonths_datetime_nullable_months_in_zone",
                    new DatetimeAddMonthsDatetimeNullableTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralIntegerNullable(),
                        zone: new LiteralString("America/New_York")
                    )
                ),
                new SelectItemTotal(
                    "datetimeAddMonths_datetime_nullable_by_zone",
                    new DatetimeAddMonthsDatetimeNullableTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralInteger(-12),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemTotal(
                    "dateDiffMonths_integer",
                    new DateDiffMonthsIntegerTotal(
                        new LiteralDate(new DateOnly(2023, 2, 28)),
                        new LiteralDate(new DateOnly(2023, 1, 31))
                    )
                ),
                new SelectItemTotal(
                    "dateDiffMonths_integer_nullable",
                    new DateDiffMonthsIntegerNullableTotal(
                        new LiteralDateNullable(),
                        new LiteralDate(new DateOnly(2023, 1, 31))
                    )
                ),
                new SelectItemTotal(
                    "datetimeDiffMonths_integer",
                    new DatetimeDiffMonthsIntegerTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 2, 15, 11, 59, 59, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 15, 12, 0, 0, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemTotal(
                    "datetimeDiffMonths_integer_nullable",
                    new DatetimeDiffMonthsIntegerNullableTotal(
                        new LiteralDatetimeNullable(),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 15, 12, 0, 0, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemTotal(
                    "datetimeDiffMonths_integer_in_zone",
                    new DatetimeDiffMonthsIntegerTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemTotal(
                    "datetimeDiffMonths_integer_nullable_in_zone",
                    new DatetimeDiffMonthsIntegerNullableTotal(
                        new LiteralDatetimeNullable(),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Etc/GMT+5")
                    )
                ),
                new SelectItemTotal(
                    "datetimeDiffMonths_integer_nullable_by_zone",
                    new DatetimeDiffMonthsIntegerNullableTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemTotal(
                    "dateFromParts_date",
                    new DateFromPartsDateTotal(
                        new LiteralInteger(2024),
                        new LiteralInteger(2),
                        new LiteralInteger(29)
                    )
                ),
                new SelectItemTotal(
                    "dateFromParts_date_nullable_year",
                    new DateFromPartsDateNullableTotal(
                        new LiteralIntegerNullable(),
                        new LiteralInteger(2),
                        new LiteralInteger(29)
                    )
                ),
                new SelectItemTotal(
                    "dateFromParts_date_nullable_month",
                    new DateFromPartsDateNullableTotal(
                        new LiteralInteger(2024),
                        new LiteralIntegerNullable(),
                        new LiteralInteger(29)
                    )
                ),
                new SelectItemTotal(
                    "dateFromParts_date_nullable_day",
                    new DateFromPartsDateNullableTotal(
                        new LiteralInteger(2024),
                        new LiteralInteger(2),
                        new LiteralIntegerNullable()
                    )
                ),
                new SelectItemTotal(
                    "timeFromParts_time",
                    new TimeFromPartsTimeTotal(
                        new LiteralInteger(18),
                        new LiteralInteger(30),
                        new LiteralDecimal(15.25m)
                    )
                ),
                new SelectItemTotal(
                    "timeFromParts_time_integer_second",
                    new TimeFromPartsTimeTotal(
                        new LiteralInteger(18),
                        new LiteralInteger(30),
                        new LiteralInteger(15)
                    )
                ),
                new SelectItemTotal(
                    "timeFromParts_time_nullable_hour",
                    new TimeFromPartsTimeNullableTotal(
                        new LiteralIntegerNullable(),
                        new LiteralInteger(30),
                        new LiteralInteger(15)
                    )
                ),
                new SelectItemTotal(
                    "timeFromParts_time_nullable_minute",
                    new TimeFromPartsTimeNullableTotal(
                        new LiteralInteger(18),
                        new LiteralIntegerNullable(),
                        new LiteralInteger(15)
                    )
                ),
                new SelectItemTotal(
                    "timeFromParts_time_nullable_second",
                    new TimeFromPartsTimeNullableTotal(
                        new LiteralInteger(18),
                        new LiteralInteger(30),
                        new LiteralDecimalNullable()
                    )
                ),
                new SelectItemTotal(
                    "datetimeFromParts_datetime",
                    new DatetimeFromPartsDatetimeTotal(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralTime(new TimeOnly(8, 30, 0))
                    )
                ),
                new SelectItemTotal(
                    "datetimeFromParts_datetime_nullable_date",
                    new DatetimeFromPartsDatetimeNullableTotal(
                        new LiteralDateNullable(),
                        new LiteralTime(new TimeOnly(8, 30, 0))
                    )
                ),
                new SelectItemTotal(
                    "datetimeFromParts_datetime_nullable_time",
                    new DatetimeFromPartsDatetimeNullableTotal(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralTimeNullable()
                    )
                ),
                new SelectItemTotal(
                    "datetimeFromParts_datetime_in_zone",
                    new DatetimeFromPartsDatetimeTotal(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemTotal(
                    "datetimeFromParts_datetime_nullable_date_in_zone",
                    new DatetimeFromPartsDatetimeNullableTotal(
                        new LiteralDateNullable(),
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        zone: new LiteralString("Etc/GMT-3")
                    )
                ),
                new SelectItemTotal(
                    "datetimeFromParts_datetime_nullable_time_in_zone",
                    new DatetimeFromPartsDatetimeNullableTotal(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralTimeNullable(),
                        zone: new LiteralString("America/New_York")
                    )
                ),
                new SelectItemTotal(
                    "datetimeFromParts_datetime_nullable_by_zone",
                    new DatetimeFromPartsDatetimeNullableTotal(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemTotal(
                    "year_date",
                    new YearDateIntegerTotal(new LiteralDate(new DateOnly(2024, 12, 30)))
                ),
                new SelectItemTotal(
                    "year_date_nullable",
                    new YearDateIntegerNullableTotal(new LiteralDateNullable())
                ),
                new SelectItemTotal(
                    "year_datetime",
                    new YearDatetimeIntegerTotal(
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
                new SelectItemTotal(
                    "year_datetime_nullable",
                    new YearDatetimeIntegerNullableTotal(new LiteralDatetimeNullable())
                ),
                new SelectItemTotal(
                    "year_datetime_in_zone",
                    new YearDatetimeIntegerTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemTotal(
                    "year_datetime_nullable_in_zone",
                    new YearDatetimeIntegerNullableTotal(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemTotal(
                    "year_datetime_nullable_by_zone",
                    new YearDatetimeIntegerNullableTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemTotal(
                    "quarter_date",
                    new QuarterDateIntegerTotal(
                        new LiteralDate(new DateOnly(2024, 12, 30))
                    )
                ),
                new SelectItemTotal(
                    "quarter_date_nullable",
                    new QuarterDateIntegerNullableTotal(new LiteralDateNullable())
                ),
                new SelectItemTotal(
                    "quarter_datetime",
                    new QuarterDatetimeIntegerTotal(
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
                new SelectItemTotal(
                    "quarter_datetime_nullable",
                    new QuarterDatetimeIntegerNullableTotal(new LiteralDatetimeNullable())
                ),
                new SelectItemTotal(
                    "quarter_datetime_in_zone",
                    new QuarterDatetimeIntegerTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemTotal(
                    "quarter_datetime_nullable_in_zone",
                    new QuarterDatetimeIntegerNullableTotal(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemTotal(
                    "quarter_datetime_nullable_by_zone",
                    new QuarterDatetimeIntegerNullableTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemTotal(
                    "month_date",
                    new MonthDateIntegerTotal(new LiteralDate(new DateOnly(2024, 12, 30)))
                ),
                new SelectItemTotal(
                    "month_date_nullable",
                    new MonthDateIntegerNullableTotal(new LiteralDateNullable())
                ),
                new SelectItemTotal(
                    "month_datetime",
                    new MonthDatetimeIntegerTotal(
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
                new SelectItemTotal(
                    "month_datetime_nullable",
                    new MonthDatetimeIntegerNullableTotal(new LiteralDatetimeNullable())
                ),
                new SelectItemTotal(
                    "month_datetime_in_zone",
                    new MonthDatetimeIntegerTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemTotal(
                    "month_datetime_nullable_in_zone",
                    new MonthDatetimeIntegerNullableTotal(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemTotal(
                    "month_datetime_nullable_by_zone",
                    new MonthDatetimeIntegerNullableTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemTotal(
                    "day_date",
                    new DayDateIntegerTotal(new LiteralDate(new DateOnly(2024, 12, 30)))
                ),
                new SelectItemTotal(
                    "day_date_nullable",
                    new DayDateIntegerNullableTotal(new LiteralDateNullable())
                ),
                new SelectItemTotal(
                    "day_datetime",
                    new DayDatetimeIntegerTotal(
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
                new SelectItemTotal(
                    "day_datetime_nullable",
                    new DayDatetimeIntegerNullableTotal(new LiteralDatetimeNullable())
                ),
                new SelectItemTotal(
                    "day_datetime_in_zone",
                    new DayDatetimeIntegerTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemTotal(
                    "day_datetime_nullable_in_zone",
                    new DayDatetimeIntegerNullableTotal(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemTotal(
                    "day_datetime_nullable_by_zone",
                    new DayDatetimeIntegerNullableTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemTotal(
                    "dayOfWeek_date",
                    new DayOfWeekDateIntegerTotal(
                        new LiteralDate(new DateOnly(2024, 12, 30))
                    )
                ),
                new SelectItemTotal(
                    "dayOfWeek_date_nullable",
                    new DayOfWeekDateIntegerNullableTotal(new LiteralDateNullable())
                ),
                new SelectItemTotal(
                    "dayOfWeek_datetime",
                    new DayOfWeekDatetimeIntegerTotal(
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
                new SelectItemTotal(
                    "dayOfWeek_datetime_nullable",
                    new DayOfWeekDatetimeIntegerNullableTotal(
                        new LiteralDatetimeNullable()
                    )
                ),
                new SelectItemTotal(
                    "dayOfWeek_datetime_in_zone",
                    new DayOfWeekDatetimeIntegerTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemTotal(
                    "dayOfWeek_datetime_nullable_in_zone",
                    new DayOfWeekDatetimeIntegerNullableTotal(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemTotal(
                    "dayOfWeek_datetime_nullable_by_zone",
                    new DayOfWeekDatetimeIntegerNullableTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemTotal(
                    "dayOfYear_date",
                    new DayOfYearDateIntegerTotal(
                        new LiteralDate(new DateOnly(2024, 12, 30))
                    )
                ),
                new SelectItemTotal(
                    "dayOfYear_date_nullable",
                    new DayOfYearDateIntegerNullableTotal(new LiteralDateNullable())
                ),
                new SelectItemTotal(
                    "dayOfYear_datetime",
                    new DayOfYearDatetimeIntegerTotal(
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
                new SelectItemTotal(
                    "dayOfYear_datetime_nullable",
                    new DayOfYearDatetimeIntegerNullableTotal(
                        new LiteralDatetimeNullable()
                    )
                ),
                new SelectItemTotal(
                    "dayOfYear_datetime_in_zone",
                    new DayOfYearDatetimeIntegerTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemTotal(
                    "dayOfYear_datetime_nullable_in_zone",
                    new DayOfYearDatetimeIntegerNullableTotal(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemTotal(
                    "dayOfYear_datetime_nullable_by_zone",
                    new DayOfYearDatetimeIntegerNullableTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemTotal(
                    "week_date",
                    new WeekDateIntegerTotal(new LiteralDate(new DateOnly(2024, 12, 30)))
                ),
                new SelectItemTotal(
                    "week_date_nullable",
                    new WeekDateIntegerNullableTotal(new LiteralDateNullable())
                ),
                new SelectItemTotal(
                    "week_datetime",
                    new WeekDatetimeIntegerTotal(
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
                new SelectItemTotal(
                    "week_datetime_nullable",
                    new WeekDatetimeIntegerNullableTotal(new LiteralDatetimeNullable())
                ),
                new SelectItemTotal(
                    "week_datetime_in_zone",
                    new WeekDatetimeIntegerTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemTotal(
                    "week_datetime_nullable_in_zone",
                    new WeekDatetimeIntegerNullableTotal(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemTotal(
                    "week_datetime_nullable_by_zone",
                    new WeekDatetimeIntegerNullableTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemTotal(
                    "weekYear_date",
                    new WeekYearDateIntegerTotal(
                        new LiteralDate(new DateOnly(2024, 12, 30))
                    )
                ),
                new SelectItemTotal(
                    "weekYear_date_nullable",
                    new WeekYearDateIntegerNullableTotal(new LiteralDateNullable())
                ),
                new SelectItemTotal(
                    "weekYear_datetime",
                    new WeekYearDatetimeIntegerTotal(
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
                new SelectItemTotal(
                    "weekYear_datetime_nullable",
                    new WeekYearDatetimeIntegerNullableTotal(
                        new LiteralDatetimeNullable()
                    )
                ),
                new SelectItemTotal(
                    "weekYear_datetime_in_zone",
                    new WeekYearDatetimeIntegerTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemTotal(
                    "weekYear_datetime_nullable_in_zone",
                    new WeekYearDatetimeIntegerNullableTotal(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemTotal(
                    "weekYear_datetime_nullable_by_zone",
                    new WeekYearDatetimeIntegerNullableTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemTotal(
                    "hour_time",
                    new HourTimeIntegerTotal(
                        new LiteralTime(new TimeOnly(18, 30, 15, 250))
                    )
                ),
                new SelectItemTotal(
                    "hour_time_nullable",
                    new HourTimeIntegerNullableTotal(new LiteralTimeNullable())
                ),
                new SelectItemTotal(
                    "hour_datetime",
                    new HourDatetimeIntegerTotal(
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
                new SelectItemTotal(
                    "hour_datetime_nullable",
                    new HourDatetimeIntegerNullableTotal(new LiteralDatetimeNullable())
                ),
                new SelectItemTotal(
                    "hour_datetime_in_zone",
                    new HourDatetimeIntegerTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemTotal(
                    "hour_datetime_nullable_in_zone",
                    new HourDatetimeIntegerNullableTotal(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemTotal(
                    "hour_datetime_nullable_by_zone",
                    new HourDatetimeIntegerNullableTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemTotal(
                    "minute_time",
                    new MinuteTimeIntegerTotal(
                        new LiteralTime(new TimeOnly(18, 30, 15, 250))
                    )
                ),
                new SelectItemTotal(
                    "minute_time_nullable",
                    new MinuteTimeIntegerNullableTotal(new LiteralTimeNullable())
                ),
                new SelectItemTotal(
                    "minute_datetime",
                    new MinuteDatetimeIntegerTotal(
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
                new SelectItemTotal(
                    "minute_datetime_nullable",
                    new MinuteDatetimeIntegerNullableTotal(new LiteralDatetimeNullable())
                ),
                new SelectItemTotal(
                    "minute_datetime_in_zone",
                    new MinuteDatetimeIntegerTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemTotal(
                    "minute_datetime_nullable_in_zone",
                    new MinuteDatetimeIntegerNullableTotal(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemTotal(
                    "minute_datetime_nullable_by_zone",
                    new MinuteDatetimeIntegerNullableTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemTotal(
                    "second_time",
                    new SecondTimeIntegerTotal(
                        new LiteralTime(new TimeOnly(18, 30, 15, 250))
                    )
                ),
                new SelectItemTotal(
                    "second_time_nullable",
                    new SecondTimeIntegerNullableTotal(new LiteralTimeNullable())
                ),
                new SelectItemTotal(
                    "second_datetime",
                    new SecondDatetimeIntegerTotal(
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
                new SelectItemTotal(
                    "second_datetime_nullable",
                    new SecondDatetimeIntegerNullableTotal(new LiteralDatetimeNullable())
                ),
                new SelectItemTotal(
                    "second_datetime_in_zone",
                    new SecondDatetimeIntegerTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemTotal(
                    "second_datetime_nullable_in_zone",
                    new SecondDatetimeIntegerNullableTotal(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemTotal(
                    "second_datetime_nullable_by_zone",
                    new SecondDatetimeIntegerNullableTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemTotal(
                    "microsecond_time",
                    new MicrosecondTimeIntegerTotal(
                        new LiteralTime(new TimeOnly(18, 30, 15, 250))
                    )
                ),
                new SelectItemTotal(
                    "microsecond_time_nullable",
                    new MicrosecondTimeIntegerNullableTotal(new LiteralTimeNullable())
                ),
                new SelectItemTotal(
                    "microsecond_datetime",
                    new MicrosecondDatetimeIntegerTotal(
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
                new SelectItemTotal(
                    "microsecond_datetime_nullable",
                    new MicrosecondDatetimeIntegerNullableTotal(
                        new LiteralDatetimeNullable()
                    )
                ),
                new SelectItemTotal(
                    "startOfYear_date",
                    new StartOfYearDateTotal(new LiteralDate(new DateOnly(2024, 8, 15)))
                ),
                new SelectItemTotal(
                    "startOfYear_date_nullable",
                    new StartOfYearDateNullableTotal(new LiteralDateNullable())
                ),
                new SelectItemTotal(
                    "startOfYear_datetime",
                    new StartOfYearDatetimeTotal(
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
                new SelectItemTotal(
                    "startOfYear_datetime_nullable",
                    new StartOfYearDatetimeNullableTotal(new LiteralDatetimeNullable())
                ),
                new SelectItemTotal(
                    "startOfYear_datetime_in_zone",
                    new StartOfYearDatetimeTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemTotal(
                    "startOfYear_datetime_nullable_in_zone",
                    new StartOfYearDatetimeNullableTotal(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemTotal(
                    "startOfYear_datetime_nullable_by_zone",
                    new StartOfYearDatetimeNullableTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemTotal(
                    "startOfQuarter_date",
                    new StartOfQuarterDateTotal(
                        new LiteralDate(new DateOnly(2024, 8, 15))
                    )
                ),
                new SelectItemTotal(
                    "startOfQuarter_date_nullable",
                    new StartOfQuarterDateNullableTotal(new LiteralDateNullable())
                ),
                new SelectItemTotal(
                    "startOfQuarter_datetime",
                    new StartOfQuarterDatetimeTotal(
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
                new SelectItemTotal(
                    "startOfQuarter_datetime_nullable",
                    new StartOfQuarterDatetimeNullableTotal(new LiteralDatetimeNullable())
                ),
                new SelectItemTotal(
                    "startOfQuarter_datetime_in_zone",
                    new StartOfQuarterDatetimeTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemTotal(
                    "startOfQuarter_datetime_nullable_in_zone",
                    new StartOfQuarterDatetimeNullableTotal(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemTotal(
                    "startOfQuarter_datetime_nullable_by_zone",
                    new StartOfQuarterDatetimeNullableTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemTotal(
                    "startOfMonth_date",
                    new StartOfMonthDateTotal(new LiteralDate(new DateOnly(2024, 8, 15)))
                ),
                new SelectItemTotal(
                    "startOfMonth_date_nullable",
                    new StartOfMonthDateNullableTotal(new LiteralDateNullable())
                ),
                new SelectItemTotal(
                    "startOfMonth_datetime",
                    new StartOfMonthDatetimeTotal(
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
                new SelectItemTotal(
                    "startOfMonth_datetime_nullable",
                    new StartOfMonthDatetimeNullableTotal(new LiteralDatetimeNullable())
                ),
                new SelectItemTotal(
                    "startOfMonth_datetime_in_zone",
                    new StartOfMonthDatetimeTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemTotal(
                    "startOfMonth_datetime_nullable_in_zone",
                    new StartOfMonthDatetimeNullableTotal(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemTotal(
                    "startOfMonth_datetime_nullable_by_zone",
                    new StartOfMonthDatetimeNullableTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemTotal(
                    "startOfWeek_date",
                    new StartOfWeekDateTotal(new LiteralDate(new DateOnly(2024, 8, 15)))
                ),
                new SelectItemTotal(
                    "startOfWeek_date_nullable",
                    new StartOfWeekDateNullableTotal(new LiteralDateNullable())
                ),
                new SelectItemTotal(
                    "startOfWeek_datetime",
                    new StartOfWeekDatetimeTotal(
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
                new SelectItemTotal(
                    "startOfWeek_datetime_nullable",
                    new StartOfWeekDatetimeNullableTotal(new LiteralDatetimeNullable())
                ),
                new SelectItemTotal(
                    "startOfWeek_datetime_in_zone",
                    new StartOfWeekDatetimeTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemTotal(
                    "startOfWeek_datetime_nullable_in_zone",
                    new StartOfWeekDatetimeNullableTotal(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemTotal(
                    "startOfWeek_datetime_nullable_by_zone",
                    new StartOfWeekDatetimeNullableTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemTotal(
                    "startOfDay_datetime",
                    new StartOfDayDatetimeTotal(
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
                new SelectItemTotal(
                    "startOfDay_datetime_nullable",
                    new StartOfDayDatetimeNullableTotal(new LiteralDatetimeNullable())
                ),
                new SelectItemTotal(
                    "startOfDay_datetime_in_zone",
                    new StartOfDayDatetimeTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemTotal(
                    "startOfDay_datetime_nullable_in_zone",
                    new StartOfDayDatetimeNullableTotal(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemTotal(
                    "startOfDay_datetime_nullable_by_zone",
                    new StartOfDayDatetimeNullableTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemTotal(
                    "startOfHour_time",
                    new StartOfHourTimeTotal(
                        new LiteralTime(new TimeOnly(18, 30, 15, 250))
                    )
                ),
                new SelectItemTotal(
                    "startOfHour_time_nullable",
                    new StartOfHourTimeNullableTotal(new LiteralTimeNullable())
                ),
                new SelectItemTotal(
                    "startOfHour_datetime",
                    new StartOfHourDatetimeTotal(
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
                new SelectItemTotal(
                    "startOfHour_datetime_nullable",
                    new StartOfHourDatetimeNullableTotal(new LiteralDatetimeNullable())
                ),
                new SelectItemTotal(
                    "startOfHour_datetime_in_zone",
                    new StartOfHourDatetimeTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemTotal(
                    "startOfHour_datetime_nullable_in_zone",
                    new StartOfHourDatetimeNullableTotal(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemTotal(
                    "startOfHour_datetime_nullable_by_zone",
                    new StartOfHourDatetimeNullableTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemTotal(
                    "startOfMinute_time",
                    new StartOfMinuteTimeTotal(
                        new LiteralTime(new TimeOnly(18, 30, 15, 250))
                    )
                ),
                new SelectItemTotal(
                    "startOfMinute_time_nullable",
                    new StartOfMinuteTimeNullableTotal(new LiteralTimeNullable())
                ),
                new SelectItemTotal(
                    "startOfMinute_datetime",
                    new StartOfMinuteDatetimeTotal(
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
                new SelectItemTotal(
                    "startOfMinute_datetime_nullable",
                    new StartOfMinuteDatetimeNullableTotal(new LiteralDatetimeNullable())
                ),
                new SelectItemTotal(
                    "startOfMinute_datetime_in_zone",
                    new StartOfMinuteDatetimeTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemTotal(
                    "startOfMinute_datetime_nullable_in_zone",
                    new StartOfMinuteDatetimeNullableTotal(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemTotal(
                    "startOfMinute_datetime_nullable_by_zone",
                    new StartOfMinuteDatetimeNullableTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemTotal(
                    "startOfSecond_time",
                    new StartOfSecondTimeTotal(
                        new LiteralTime(new TimeOnly(18, 30, 15, 250))
                    )
                ),
                new SelectItemTotal(
                    "startOfSecond_time_nullable",
                    new StartOfSecondTimeNullableTotal(new LiteralTimeNullable())
                ),
                new SelectItemTotal(
                    "startOfSecond_datetime",
                    new StartOfSecondDatetimeTotal(
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
                new SelectItemTotal(
                    "startOfSecond_datetime_nullable",
                    new StartOfSecondDatetimeNullableTotal(new LiteralDatetimeNullable())
                ),
                new SelectItemTotal(
                    "asString_string_of_integer",
                    new AsStringDecimalStringTotal(new LiteralInteger(7))
                ),
                new SelectItemTotal(
                    "asString_string_nullable_of_integer",
                    new AsStringDecimalStringNullableTotal(new LiteralIntegerNullable())
                ),
                new SelectItemTotal(
                    "asString_string_of_decimal",
                    new AsStringDecimalStringTotal(new LiteralDecimal(2.5m))
                ),
                new SelectItemTotal(
                    "asString_string_nullable_of_decimal",
                    new AsStringDecimalStringNullableTotal(new LiteralDecimalNullable())
                ),
                new SelectItemTotal(
                    "asString_string_of_boolean",
                    new AsStringBooleanStringTotal(new LiteralBoolean(true))
                ),
                new SelectItemTotal(
                    "asString_string_nullable_of_boolean",
                    new AsStringBooleanStringNullableTotal(new LiteralBooleanNullable())
                ),
                new SelectItemTotal(
                    "asString_string_of_date",
                    new AsStringDateStringTotal(
                        new LiteralDate(new DateOnly(2024, 1, 31))
                    )
                ),
                new SelectItemTotal(
                    "asString_string_nullable_of_date",
                    new AsStringDateStringNullableTotal(new LiteralDateNullable())
                ),
                new SelectItemTotal(
                    "asString_string_of_time",
                    new AsStringTimeStringTotal(new LiteralTime(new TimeOnly(8, 30, 0)))
                ),
                new SelectItemTotal(
                    "asString_string_nullable_of_time",
                    new AsStringTimeStringNullableTotal(new LiteralTimeNullable())
                ),
                new SelectItemTotal(
                    "asString_string_of_datetime",
                    new AsStringDatetimeStringTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemTotal(
                    "asString_string_nullable_of_datetime",
                    new AsStringDatetimeStringNullableTotal(new LiteralDatetimeNullable())
                ),
                new SelectItemTotal(
                    "asString_string_of_uuid",
                    new AsStringUuidStringTotal(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8]))
                    )
                ),
                new SelectItemTotal(
                    "asString_string_nullable_of_uuid",
                    new AsStringUuidStringNullableTotal(new LiteralUuidNullable())
                ),
                new SelectItemTotal(
                    "asDate_date",
                    new AsDateDatetimeDateTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemTotal(
                    "asDate_date_nullable",
                    new AsDateDatetimeDateNullableTotal(new LiteralDatetimeNullable())
                ),
                new SelectItemTotal(
                    "asTime_time",
                    new AsTimeDatetimeTimeTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemTotal(
                    "asTime_time_nullable",
                    new AsTimeDatetimeTimeNullableTotal(new LiteralDatetimeNullable())
                ),
                new SelectItemTotal(
                    "asDate_date_in_zone",
                    new AsDateDatetimeDateTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemTotal(
                    "asDate_date_nullable_in_zone",
                    new AsDateDatetimeDateNullableTotal(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemTotal(
                    "asDate_date_nullable_by_zone",
                    new AsDateDatetimeDateNullableTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemTotal(
                    "asDate_date_nullable_in_nullable_zone",
                    new AsDateDatetimeDateNullableTotal(
                        new LiteralDatetimeNullable(),
                        zone: new ParamStringNullable("zone")
                    )
                ),
                new SelectItemTotal(
                    "asTime_time_in_zone",
                    new AsTimeDatetimeTimeTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemTotal(
                    "asTime_time_nullable_in_zone",
                    new AsTimeDatetimeTimeNullableTotal(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemTotal(
                    "asTime_time_nullable_by_zone",
                    new AsTimeDatetimeTimeNullableTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemTotal(
                    "asTime_time_nullable_in_nullable_zone",
                    new AsTimeDatetimeTimeNullableTotal(
                        new LiteralDatetimeNullable(),
                        zone: new ParamStringNullable("zone")
                    )
                ),
                new SelectItemTotal(
                    "asDatetime_datetime",
                    new AsDatetimeDateDatetimeTotal(
                        new LiteralDate(new DateOnly(2024, 1, 31))
                    )
                ),
                new SelectItemTotal(
                    "asDatetime_datetime_nullable",
                    new AsDatetimeDateDatetimeNullableTotal(new LiteralDateNullable())
                ),
                new SelectItemTotal(
                    "asDatetime_datetime_in_zone",
                    new AsDatetimeDateDatetimeTotal(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemTotal(
                    "asDatetime_datetime_nullable_in_zone",
                    new AsDatetimeDateDatetimeNullableTotal(
                        new LiteralDateNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemTotal(
                    "asDatetime_datetime_nullable_by_zone",
                    new AsDatetimeDateDatetimeNullableTotal(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemTotal(
                    "asInteger_integer_of_string",
                    new AsIntegerIntegerTotal(new LiteralString("7"))
                ),
                new SelectItemTotal(
                    "asInteger_integer_nullable_of_string",
                    new AsIntegerIntegerNullableTotal(new LiteralStringNullable())
                ),
                new SelectItemTotal(
                    "tryAsInteger_integer_nullable",
                    new TryAsIntegerTotal(new LiteralString("7"))
                ),
                new SelectItemTotal(
                    "tryAsInteger_integer_nullable_of_null",
                    new TryAsIntegerTotal(new LiteralStringNullable())
                ),
                new SelectItemTotal(
                    "asDecimal_decimal_of_string",
                    new AsDecimalStringDecimalTotal(new LiteralString("2.5"))
                ),
                new SelectItemTotal(
                    "asDecimal_decimal_nullable_of_string",
                    new AsDecimalStringDecimalNullableTotal(new LiteralStringNullable())
                ),
                new SelectItemTotal(
                    "asDecimal_decimal_of_integer",
                    new AsDecimalIntegerDecimalTotal(new LiteralInteger(7))
                ),
                new SelectItemTotal(
                    "asDecimal_decimal_nullable_of_integer",
                    new AsDecimalIntegerDecimalNullableTotal(new LiteralIntegerNullable())
                ),
                new SelectItemTotal(
                    "asNullable_integer_nullable",
                    new AsNullableIntegerNullableTotal(new LiteralInteger(7))
                ),
                new SelectItemTotal(
                    "asNullable_decimal_nullable",
                    new AsNullableDecimalNullableTotal(new LiteralDecimal(2.5m))
                ),
                new SelectItemTotal(
                    "asNullable_string_nullable",
                    new AsNullableStringNullableTotal(new LiteralString("a"))
                ),
                new SelectItemTotal(
                    "asNullable_boolean_nullable",
                    new AsNullableBooleanNullableTotal(new LiteralBoolean(true))
                ),
                new SelectItemTotal(
                    "asNullable_date_nullable",
                    new AsNullableDateNullableTotal(
                        new LiteralDate(new DateOnly(2024, 1, 31))
                    )
                ),
                new SelectItemTotal(
                    "asNullable_time_nullable",
                    new AsNullableTimeNullableTotal(
                        new LiteralTime(new TimeOnly(8, 30, 0))
                    )
                ),
                new SelectItemTotal(
                    "asNullable_datetime_nullable",
                    new AsNullableDatetimeNullableTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemTotal(
                    "asNullable_uuid_nullable",
                    new AsNullableUuidNullableTotal(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8]))
                    )
                ),
                new SelectItemTotal(
                    "tryAsDecimal_decimal_nullable",
                    new TryAsDecimalTotal(new LiteralString("2.5"))
                ),
                new SelectItemTotal(
                    "tryAsDecimal_decimal_nullable_of_null",
                    new TryAsDecimalTotal(new LiteralStringNullable())
                ),
                new SelectItemTotal(
                    "asBoolean_boolean_of_string",
                    new AsBooleanBooleanTotal(new LiteralString("true"))
                ),
                new SelectItemTotal(
                    "asBoolean_boolean_nullable_of_string",
                    new AsBooleanBooleanNullableTotal(new LiteralStringNullable())
                ),
                new SelectItemTotal(
                    "tryAsBoolean_boolean_nullable",
                    new TryAsBooleanTotal(new LiteralString("true"))
                ),
                new SelectItemTotal(
                    "tryAsBoolean_boolean_nullable_of_null",
                    new TryAsBooleanTotal(new LiteralStringNullable())
                ),
                new SelectItemTotal(
                    "asDate_date_of_string",
                    new AsDateStringDateTotal(new LiteralString("2024-01-31"))
                ),
                new SelectItemTotal(
                    "asDate_date_nullable_of_string",
                    new AsDateStringDateNullableTotal(new LiteralStringNullable())
                ),
                new SelectItemTotal(
                    "tryAsDate_date_nullable",
                    new TryAsDateTotal(new LiteralString("2024-01-31"))
                ),
                new SelectItemTotal(
                    "tryAsDate_date_nullable_of_null",
                    new TryAsDateTotal(new LiteralStringNullable())
                ),
                new SelectItemTotal(
                    "asTime_time_of_string",
                    new AsTimeStringTimeTotal(new LiteralString("08:30:00"))
                ),
                new SelectItemTotal(
                    "asTime_time_nullable_of_string",
                    new AsTimeStringTimeNullableTotal(new LiteralStringNullable())
                ),
                new SelectItemTotal(
                    "tryAsTime_time_nullable",
                    new TryAsTimeTotal(new LiteralString("08:30:00"))
                ),
                new SelectItemTotal(
                    "tryAsTime_time_nullable_of_null",
                    new TryAsTimeTotal(new LiteralStringNullable())
                ),
                new SelectItemTotal(
                    "asDatetime_datetime_of_string",
                    new AsDatetimeStringDatetimeTotal(
                        new LiteralString("2024-01-31T08:30:00Z")
                    )
                ),
                new SelectItemTotal(
                    "asDatetime_datetime_nullable_of_string",
                    new AsDatetimeStringDatetimeNullableTotal(new LiteralStringNullable())
                ),
                new SelectItemTotal(
                    "tryAsDatetime_datetime_nullable",
                    new TryAsDatetimeTotal(new LiteralString("2024-01-31T08:30:00Z"))
                ),
                new SelectItemTotal(
                    "tryAsDatetime_datetime_nullable_of_null",
                    new TryAsDatetimeTotal(new LiteralStringNullable())
                ),
                new SelectItemTotal(
                    "asUuid_uuid_of_string",
                    new AsUuidUuidTotal(
                        new LiteralString("3f2a6c1e-8b4d-4e2a-9c1f-1a2b3c4d5e6f")
                    )
                ),
                new SelectItemTotal(
                    "asUuid_uuid_nullable_of_string",
                    new AsUuidUuidNullableTotal(new LiteralStringNullable())
                ),
                new SelectItemTotal(
                    "tryAsUuid_uuid_nullable",
                    new TryAsUuidTotal(
                        new LiteralString("3f2a6c1e-8b4d-4e2a-9c1f-1a2b3c4d5e6f")
                    )
                ),
                new SelectItemTotal(
                    "tryAsUuid_uuid_nullable_of_null",
                    new TryAsUuidTotal(new LiteralStringNullable())
                ),
                new SelectItemTotal(
                    "and_boolean",
                    new AndTotal([new LiteralBoolean(true), new LiteralBoolean(false)])
                ),
                new SelectItemTotal(
                    "or_boolean",
                    new OrTotal([new LiteralBoolean(true), new LiteralBoolean(false)])
                ),
                new SelectItemTotal(
                    "not_boolean",
                    new NotTotal(new LiteralBoolean(true))
                ),
                new SelectItemTotal(
                    "equal_boolean_decimal",
                    new EqualDecimalTotal(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m)
                    )
                ),
                new SelectItemTotal(
                    "equal_boolean_string",
                    new EqualStringTotal(new LiteralString("a"), new LiteralString("b"))
                ),
                new SelectItemTotal(
                    "equal_boolean_boolean",
                    new EqualBooleanTotal(
                        new LiteralBoolean(true),
                        new LiteralBoolean(false)
                    )
                ),
                new SelectItemTotal(
                    "equal_boolean_date",
                    new EqualDateTotal(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDate(new DateOnly(2024, 3, 1))
                    )
                ),
                new SelectItemTotal(
                    "equal_boolean_time",
                    new EqualTimeTotal(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralTime(new TimeOnly(17, 45, 30, 500))
                    )
                ),
                new SelectItemTotal(
                    "equal_boolean_datetime",
                    new EqualDatetimeTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemTotal(
                    "equal_boolean_uuid",
                    new EqualUuidTotal(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralUuid(new Guid(2, 0, 0, new byte[8]))
                    )
                ),
                new SelectItemTotal(
                    "notEqual_boolean_decimal",
                    new NotEqualDecimalTotal(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m)
                    )
                ),
                new SelectItemTotal(
                    "notEqual_boolean_string",
                    new NotEqualStringTotal(
                        new LiteralString("a"),
                        new LiteralString("b")
                    )
                ),
                new SelectItemTotal(
                    "notEqual_boolean_boolean",
                    new NotEqualBooleanTotal(
                        new LiteralBoolean(true),
                        new LiteralBoolean(false)
                    )
                ),
                new SelectItemTotal(
                    "notEqual_boolean_date",
                    new NotEqualDateTotal(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDate(new DateOnly(2024, 3, 1))
                    )
                ),
                new SelectItemTotal(
                    "notEqual_boolean_time",
                    new NotEqualTimeTotal(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralTime(new TimeOnly(17, 45, 30, 500))
                    )
                ),
                new SelectItemTotal(
                    "notEqual_boolean_datetime",
                    new NotEqualDatetimeTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemTotal(
                    "notEqual_boolean_uuid",
                    new NotEqualUuidTotal(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralUuid(new Guid(2, 0, 0, new byte[8]))
                    )
                ),
                new SelectItemTotal(
                    "greaterThan_boolean_decimal",
                    new GreaterThanDecimalTotal(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m)
                    )
                ),
                new SelectItemTotal(
                    "greaterThan_boolean_string",
                    new GreaterThanStringTotal(
                        new LiteralString("a"),
                        new LiteralString("b")
                    )
                ),
                new SelectItemTotal(
                    "greaterThan_boolean_boolean",
                    new GreaterThanBooleanTotal(
                        new LiteralBoolean(true),
                        new LiteralBoolean(false)
                    )
                ),
                new SelectItemTotal(
                    "greaterThan_boolean_date",
                    new GreaterThanDateTotal(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDate(new DateOnly(2024, 3, 1))
                    )
                ),
                new SelectItemTotal(
                    "greaterThan_boolean_time",
                    new GreaterThanTimeTotal(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralTime(new TimeOnly(17, 45, 30, 500))
                    )
                ),
                new SelectItemTotal(
                    "greaterThan_boolean_datetime",
                    new GreaterThanDatetimeTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemTotal(
                    "greaterThan_boolean_uuid",
                    new GreaterThanUuidTotal(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralUuid(new Guid(2, 0, 0, new byte[8]))
                    )
                ),
                new SelectItemTotal(
                    "lessThan_boolean_decimal",
                    new LessThanDecimalTotal(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m)
                    )
                ),
                new SelectItemTotal(
                    "lessThan_boolean_string",
                    new LessThanStringTotal(
                        new LiteralString("a"),
                        new LiteralString("b")
                    )
                ),
                new SelectItemTotal(
                    "lessThan_boolean_boolean",
                    new LessThanBooleanTotal(
                        new LiteralBoolean(true),
                        new LiteralBoolean(false)
                    )
                ),
                new SelectItemTotal(
                    "lessThan_boolean_date",
                    new LessThanDateTotal(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDate(new DateOnly(2024, 3, 1))
                    )
                ),
                new SelectItemTotal(
                    "lessThan_boolean_time",
                    new LessThanTimeTotal(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralTime(new TimeOnly(17, 45, 30, 500))
                    )
                ),
                new SelectItemTotal(
                    "lessThan_boolean_datetime",
                    new LessThanDatetimeTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemTotal(
                    "lessThan_boolean_uuid",
                    new LessThanUuidTotal(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralUuid(new Guid(2, 0, 0, new byte[8]))
                    )
                ),
                new SelectItemTotal(
                    "greaterThanOrEqual_boolean_decimal",
                    new GreaterThanOrEqualDecimalTotal(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m)
                    )
                ),
                new SelectItemTotal(
                    "greaterThanOrEqual_boolean_string",
                    new GreaterThanOrEqualStringTotal(
                        new LiteralString("a"),
                        new LiteralString("b")
                    )
                ),
                new SelectItemTotal(
                    "greaterThanOrEqual_boolean_boolean",
                    new GreaterThanOrEqualBooleanTotal(
                        new LiteralBoolean(true),
                        new LiteralBoolean(false)
                    )
                ),
                new SelectItemTotal(
                    "greaterThanOrEqual_boolean_date",
                    new GreaterThanOrEqualDateTotal(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDate(new DateOnly(2024, 3, 1))
                    )
                ),
                new SelectItemTotal(
                    "greaterThanOrEqual_boolean_time",
                    new GreaterThanOrEqualTimeTotal(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralTime(new TimeOnly(17, 45, 30, 500))
                    )
                ),
                new SelectItemTotal(
                    "greaterThanOrEqual_boolean_datetime",
                    new GreaterThanOrEqualDatetimeTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemTotal(
                    "greaterThanOrEqual_boolean_uuid",
                    new GreaterThanOrEqualUuidTotal(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralUuid(new Guid(2, 0, 0, new byte[8]))
                    )
                ),
                new SelectItemTotal(
                    "lessThanOrEqual_boolean_decimal",
                    new LessThanOrEqualDecimalTotal(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m)
                    )
                ),
                new SelectItemTotal(
                    "lessThanOrEqual_boolean_string",
                    new LessThanOrEqualStringTotal(
                        new LiteralString("a"),
                        new LiteralString("b")
                    )
                ),
                new SelectItemTotal(
                    "lessThanOrEqual_boolean_boolean",
                    new LessThanOrEqualBooleanTotal(
                        new LiteralBoolean(true),
                        new LiteralBoolean(false)
                    )
                ),
                new SelectItemTotal(
                    "lessThanOrEqual_boolean_date",
                    new LessThanOrEqualDateTotal(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDate(new DateOnly(2024, 3, 1))
                    )
                ),
                new SelectItemTotal(
                    "lessThanOrEqual_boolean_time",
                    new LessThanOrEqualTimeTotal(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralTime(new TimeOnly(17, 45, 30, 500))
                    )
                ),
                new SelectItemTotal(
                    "lessThanOrEqual_boolean_datetime",
                    new LessThanOrEqualDatetimeTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemTotal(
                    "lessThanOrEqual_boolean_uuid",
                    new LessThanOrEqualUuidTotal(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralUuid(new Guid(2, 0, 0, new byte[8]))
                    )
                ),
                new SelectItemTotal(
                    "in_boolean_decimal",
                    new InDecimalTotal(
                        new LiteralDecimal(2.5m),
                        new ListLiteralDecimal([2.5m, 0.1m])
                    )
                ),
                new SelectItemTotal(
                    "in_boolean_string",
                    new InStringTotal(
                        new LiteralString("a"),
                        new ListLiteralString(["a", "b"])
                    )
                ),
                new SelectItemTotal(
                    "in_boolean_boolean",
                    new InBooleanTotal(
                        new LiteralBoolean(true),
                        new ListLiteralBoolean([true, false])
                    )
                ),
                new SelectItemTotal(
                    "in_boolean_date",
                    new InDateTotal(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new ListLiteralDate([
                            new DateOnly(2024, 1, 31),
                            new DateOnly(2024, 3, 1),
                        ])
                    )
                ),
                new SelectItemTotal(
                    "in_boolean_time",
                    new InTimeTotal(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new ListLiteralTime([
                            new TimeOnly(8, 30, 0),
                            new TimeOnly(17, 45, 30, 500),
                        ])
                    )
                ),
                new SelectItemTotal(
                    "in_boolean_datetime",
                    new InDatetimeTotal(
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
                new SelectItemTotal(
                    "in_boolean_uuid",
                    new InUuidTotal(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new ListLiteralUuid([
                            new Guid(1, 0, 0, new byte[8]),
                            new Guid(2, 0, 0, new byte[8]),
                        ])
                    )
                ),
                new SelectItemTotal(
                    "if_integer",
                    new IfIntegerTotal(
                        new LiteralInteger(7),
                        new LiteralInteger(-3),
                        new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "if_integer_nullable",
                    new IfIntegerNullableTotal(
                        new LiteralIntegerNullable(),
                        new LiteralInteger(7),
                        new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "coalesce_integer",
                    new CoalesceIntegerTotal([
                        new LiteralIntegerNullable(),
                        new LiteralInteger(7),
                    ])
                ),
                new SelectItemTotal(
                    "coalesce_integer_nullable",
                    new CoalesceIntegerNullableTotal([
                        new LiteralIntegerNullable(),
                        new LiteralIntegerNullable(),
                    ])
                ),
                new SelectItemTotal(
                    "if_decimal",
                    new IfDecimalTotal(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m),
                        new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "if_decimal_nullable",
                    new IfDecimalNullableTotal(
                        new LiteralDecimalNullable(),
                        new LiteralDecimal(2.5m),
                        new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "coalesce_decimal",
                    new CoalesceDecimalTotal([
                        new LiteralDecimalNullable(),
                        new LiteralDecimal(2.5m),
                    ])
                ),
                new SelectItemTotal(
                    "coalesce_decimal_nullable",
                    new CoalesceDecimalNullableTotal([
                        new LiteralDecimalNullable(),
                        new LiteralDecimalNullable(),
                    ])
                ),
                new SelectItemTotal(
                    "if_string",
                    new IfStringTotal(
                        new LiteralString("a"),
                        new LiteralString("b"),
                        new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "if_string_nullable",
                    new IfStringNullableTotal(
                        new LiteralStringNullable(),
                        new LiteralString("a"),
                        new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "coalesce_string",
                    new CoalesceStringTotal([
                        new LiteralStringNullable(),
                        new LiteralString("a"),
                    ])
                ),
                new SelectItemTotal(
                    "coalesce_string_nullable",
                    new CoalesceStringNullableTotal([
                        new LiteralStringNullable(),
                        new LiteralStringNullable(),
                    ])
                ),
                new SelectItemTotal(
                    "if_boolean",
                    new IfBooleanTotal(
                        new LiteralBoolean(true),
                        new LiteralBoolean(false),
                        new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "if_boolean_nullable",
                    new IfBooleanNullableTotal(
                        new LiteralBooleanNullable(),
                        new LiteralBoolean(true),
                        new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "coalesce_boolean",
                    new CoalesceBooleanTotal([
                        new LiteralBooleanNullable(),
                        new LiteralBoolean(true),
                    ])
                ),
                new SelectItemTotal(
                    "coalesce_boolean_nullable",
                    new CoalesceBooleanNullableTotal([
                        new LiteralBooleanNullable(),
                        new LiteralBooleanNullable(),
                    ])
                ),
                new SelectItemTotal(
                    "if_date",
                    new IfDateTotal(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDate(new DateOnly(2024, 3, 1)),
                        new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "if_date_nullable",
                    new IfDateNullableTotal(
                        new LiteralDateNullable(),
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "coalesce_date",
                    new CoalesceDateTotal([
                        new LiteralDateNullable(),
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                    ])
                ),
                new SelectItemTotal(
                    "coalesce_date_nullable",
                    new CoalesceDateNullableTotal([
                        new LiteralDateNullable(),
                        new LiteralDateNullable(),
                    ])
                ),
                new SelectItemTotal(
                    "if_time",
                    new IfTimeTotal(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralTime(new TimeOnly(17, 45, 30, 500)),
                        new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "if_time_nullable",
                    new IfTimeNullableTotal(
                        new LiteralTimeNullable(),
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "coalesce_time",
                    new CoalesceTimeTotal([
                        new LiteralTimeNullable(),
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                    ])
                ),
                new SelectItemTotal(
                    "coalesce_time_nullable",
                    new CoalesceTimeNullableTotal([
                        new LiteralTimeNullable(),
                        new LiteralTimeNullable(),
                    ])
                ),
                new SelectItemTotal(
                    "if_datetime",
                    new IfDatetimeTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        ),
                        new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "if_datetime_nullable",
                    new IfDatetimeNullableTotal(
                        new LiteralDatetimeNullable(),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "coalesce_datetime",
                    new CoalesceDatetimeTotal([
                        new LiteralDatetimeNullable(),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                    ])
                ),
                new SelectItemTotal(
                    "coalesce_datetime_nullable",
                    new CoalesceDatetimeNullableTotal([
                        new LiteralDatetimeNullable(),
                        new LiteralDatetimeNullable(),
                    ])
                ),
                new SelectItemTotal(
                    "if_uuid",
                    new IfUuidTotal(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralUuid(new Guid(2, 0, 0, new byte[8])),
                        new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "if_uuid_nullable",
                    new IfUuidNullableTotal(
                        new LiteralUuidNullable(),
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "coalesce_uuid",
                    new CoalesceUuidTotal([
                        new LiteralUuidNullable(),
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                    ])
                ),
                new SelectItemTotal(
                    "coalesce_uuid_nullable",
                    new CoalesceUuidNullableTotal([
                        new LiteralUuidNullable(),
                        new LiteralUuidNullable(),
                    ])
                ),
                new SelectItemTotal(
                    "nullIfEqual_integer_nullable",
                    new NullIfEqualIntegerNullableTotal(
                        new LiteralInteger(7),
                        new LiteralInteger(-3)
                    )
                ),
                new SelectItemTotal(
                    "nullIfEqual_decimal_nullable",
                    new NullIfEqualDecimalNullableTotal(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m)
                    )
                ),
                new SelectItemTotal(
                    "nullIfEqual_string_nullable",
                    new NullIfEqualStringNullableTotal(
                        new LiteralString("a"),
                        new LiteralString("b")
                    )
                ),
                new SelectItemTotal(
                    "nullIfEqual_boolean_nullable",
                    new NullIfEqualBooleanNullableTotal(
                        new LiteralBoolean(true),
                        new LiteralBoolean(false)
                    )
                ),
                new SelectItemTotal(
                    "nullIfEqual_date_nullable",
                    new NullIfEqualDateNullableTotal(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDate(new DateOnly(2024, 3, 1))
                    )
                ),
                new SelectItemTotal(
                    "nullIfEqual_time_nullable",
                    new NullIfEqualTimeNullableTotal(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralTime(new TimeOnly(17, 45, 30, 500))
                    )
                ),
                new SelectItemTotal(
                    "nullIfEqual_datetime_nullable",
                    new NullIfEqualDatetimeNullableTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemTotal(
                    "nullIfEqual_uuid_nullable",
                    new NullIfEqualUuidNullableTotal(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralUuid(new Guid(2, 0, 0, new byte[8]))
                    )
                ),
                new SelectItemTotal(
                    "nullIfEqual_integer_nullable_decimal_right",
                    new NullIfEqualIntegerNullableTotal(
                        new LiteralInteger(7),
                        new LiteralDecimal(2.5m)
                    )
                ),
                new SelectItemTotal(
                    "nullIfEqual_decimal_nullable_integer_right",
                    new NullIfEqualDecimalNullableTotal(
                        new LiteralDecimal(2.5m),
                        new LiteralInteger(7)
                    )
                ),
                new SelectItemTotal(
                    "nullIfEqual_string_nullable_null_left",
                    new NullIfEqualStringNullableTotal(
                        new LiteralStringNullable(),
                        new LiteralString("a")
                    )
                ),
                new SelectItemTotal(
                    "nullIfEqual_string_nullable_null_right",
                    new NullIfEqualStringNullableTotal(
                        new LiteralString("a"),
                        new LiteralStringNullable()
                    )
                ),
                new SelectItemTotal(
                    "greatest_integer",
                    new GreatestIntegerTotal([
                        new LiteralInteger(7),
                        new LiteralInteger(-3),
                    ])
                ),
                new SelectItemTotal(
                    "greatest_integer_nullable",
                    new GreatestIntegerNullableTotal([
                        new LiteralInteger(7),
                        new LiteralIntegerNullable(),
                        new LiteralInteger(-3),
                    ])
                ),
                new SelectItemTotal(
                    "greatest_decimal",
                    new GreatestDecimalTotal([
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m),
                    ])
                ),
                new SelectItemTotal(
                    "greatest_decimal_nullable",
                    new GreatestDecimalNullableTotal([
                        new LiteralDecimal(2.5m),
                        new LiteralDecimalNullable(),
                        new LiteralDecimal(0.1m),
                    ])
                ),
                new SelectItemTotal(
                    "greatest_string",
                    new GreatestStringTotal([
                        new LiteralString("a"),
                        new LiteralString("b"),
                    ])
                ),
                new SelectItemTotal(
                    "greatest_string_nullable",
                    new GreatestStringNullableTotal([
                        new LiteralString("a"),
                        new LiteralStringNullable(),
                        new LiteralString("b"),
                    ])
                ),
                new SelectItemTotal(
                    "greatest_boolean",
                    new GreatestBooleanTotal([
                        new LiteralBoolean(true),
                        new LiteralBoolean(false),
                    ])
                ),
                new SelectItemTotal(
                    "greatest_boolean_nullable",
                    new GreatestBooleanNullableTotal([
                        new LiteralBoolean(true),
                        new LiteralBooleanNullable(),
                        new LiteralBoolean(false),
                    ])
                ),
                new SelectItemTotal(
                    "greatest_date",
                    new GreatestDateTotal([
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDate(new DateOnly(2024, 3, 1)),
                    ])
                ),
                new SelectItemTotal(
                    "greatest_date_nullable",
                    new GreatestDateNullableTotal([
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDateNullable(),
                        new LiteralDate(new DateOnly(2024, 3, 1)),
                    ])
                ),
                new SelectItemTotal(
                    "greatest_time",
                    new GreatestTimeTotal([
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralTime(new TimeOnly(17, 45, 30, 500)),
                    ])
                ),
                new SelectItemTotal(
                    "greatest_time_nullable",
                    new GreatestTimeNullableTotal([
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralTimeNullable(),
                        new LiteralTime(new TimeOnly(17, 45, 30, 500)),
                    ])
                ),
                new SelectItemTotal(
                    "greatest_datetime",
                    new GreatestDatetimeTotal([
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        ),
                    ])
                ),
                new SelectItemTotal(
                    "greatest_datetime_nullable",
                    new GreatestDatetimeNullableTotal([
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDatetimeNullable(),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        ),
                    ])
                ),
                new SelectItemTotal(
                    "greatest_uuid",
                    new GreatestUuidTotal([
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralUuid(new Guid(2, 0, 0, new byte[8])),
                    ])
                ),
                new SelectItemTotal(
                    "greatest_uuid_nullable",
                    new GreatestUuidNullableTotal([
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralUuidNullable(),
                        new LiteralUuid(new Guid(2, 0, 0, new byte[8])),
                    ])
                ),
                new SelectItemTotal(
                    "greatest_decimal_of_integer_and_decimal",
                    new GreatestDecimalTotal([
                        new LiteralInteger(7),
                        new LiteralDecimal(2.5m),
                    ])
                ),
                new SelectItemTotal(
                    "least_integer",
                    new LeastIntegerTotal([new LiteralInteger(7), new LiteralInteger(-3)])
                ),
                new SelectItemTotal(
                    "least_integer_nullable",
                    new LeastIntegerNullableTotal([
                        new LiteralInteger(7),
                        new LiteralIntegerNullable(),
                        new LiteralInteger(-3),
                    ])
                ),
                new SelectItemTotal(
                    "least_decimal",
                    new LeastDecimalTotal([
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m),
                    ])
                ),
                new SelectItemTotal(
                    "least_decimal_nullable",
                    new LeastDecimalNullableTotal([
                        new LiteralDecimal(2.5m),
                        new LiteralDecimalNullable(),
                        new LiteralDecimal(0.1m),
                    ])
                ),
                new SelectItemTotal(
                    "least_string",
                    new LeastStringTotal([new LiteralString("a"), new LiteralString("b")])
                ),
                new SelectItemTotal(
                    "least_string_nullable",
                    new LeastStringNullableTotal([
                        new LiteralString("a"),
                        new LiteralStringNullable(),
                        new LiteralString("b"),
                    ])
                ),
                new SelectItemTotal(
                    "least_boolean",
                    new LeastBooleanTotal([
                        new LiteralBoolean(true),
                        new LiteralBoolean(false),
                    ])
                ),
                new SelectItemTotal(
                    "least_boolean_nullable",
                    new LeastBooleanNullableTotal([
                        new LiteralBoolean(true),
                        new LiteralBooleanNullable(),
                        new LiteralBoolean(false),
                    ])
                ),
                new SelectItemTotal(
                    "least_date",
                    new LeastDateTotal([
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDate(new DateOnly(2024, 3, 1)),
                    ])
                ),
                new SelectItemTotal(
                    "least_date_nullable",
                    new LeastDateNullableTotal([
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDateNullable(),
                        new LiteralDate(new DateOnly(2024, 3, 1)),
                    ])
                ),
                new SelectItemTotal(
                    "least_time",
                    new LeastTimeTotal([
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralTime(new TimeOnly(17, 45, 30, 500)),
                    ])
                ),
                new SelectItemTotal(
                    "least_time_nullable",
                    new LeastTimeNullableTotal([
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralTimeNullable(),
                        new LiteralTime(new TimeOnly(17, 45, 30, 500)),
                    ])
                ),
                new SelectItemTotal(
                    "least_datetime",
                    new LeastDatetimeTotal([
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        ),
                    ])
                ),
                new SelectItemTotal(
                    "least_datetime_nullable",
                    new LeastDatetimeNullableTotal([
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDatetimeNullable(),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        ),
                    ])
                ),
                new SelectItemTotal(
                    "least_uuid",
                    new LeastUuidTotal([
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralUuid(new Guid(2, 0, 0, new byte[8])),
                    ])
                ),
                new SelectItemTotal(
                    "least_uuid_nullable",
                    new LeastUuidNullableTotal([
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralUuidNullable(),
                        new LiteralUuid(new Guid(2, 0, 0, new byte[8])),
                    ])
                ),
                new SelectItemTotal(
                    "least_decimal_of_integer_and_decimal",
                    new LeastDecimalTotal([
                        new LiteralInteger(7),
                        new LiteralDecimal(2.5m),
                    ])
                ),
                new SelectItemTotal("count_integer", new CountTotal()),
                new SelectItemTotal(
                    "count_integer_where",
                    new CountTotal(predicate: new LiteralBoolean(true))
                ),
                new SelectItemTotal(
                    "countDistinct_integer_of_integer",
                    new CountDistinctTotal(new LiteralInteger(7))
                ),
                new SelectItemTotal(
                    "countDistinct_integer_of_decimal",
                    new CountDistinctTotal(new LiteralDecimal(2.5m))
                ),
                new SelectItemTotal(
                    "countDistinct_integer_of_string",
                    new CountDistinctTotal(new LiteralString("a"))
                ),
                new SelectItemTotal(
                    "countDistinct_integer_of_boolean",
                    new CountDistinctTotal(new LiteralBoolean(true))
                ),
                new SelectItemTotal(
                    "countDistinct_integer_of_date",
                    new CountDistinctTotal(new LiteralDate(new DateOnly(2024, 1, 31)))
                ),
                new SelectItemTotal(
                    "countDistinct_integer_of_time",
                    new CountDistinctTotal(new LiteralTime(new TimeOnly(8, 30, 0)))
                ),
                new SelectItemTotal(
                    "countDistinct_integer_of_datetime",
                    new CountDistinctTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemTotal(
                    "countDistinct_integer_of_uuid",
                    new CountDistinctTotal(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8]))
                    )
                ),
                new SelectItemTotal(
                    "countDistinct_integer_of_integer_null",
                    new CountDistinctTotal(new LiteralIntegerNullable())
                ),
                new SelectItemTotal(
                    "countDistinct_integer_of_string_null",
                    new CountDistinctTotal(new LiteralStringNullable())
                ),
                new SelectItemTotal(
                    "countDistinct_integer_of_uuid_null",
                    new CountDistinctTotal(new LiteralUuidNullable())
                ),
                new SelectItemTotal(
                    "countDistinct_integer_of_decimal_where",
                    new CountDistinctTotal(
                        new LiteralDecimal(2.5m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "sum_integer_of_integer",
                    new SumIntegerTotal(new LiteralInteger(7))
                ),
                new SelectItemTotal(
                    "sum_integer_of_integer_null",
                    new SumIntegerTotal(new LiteralIntegerNullable())
                ),
                new SelectItemTotal(
                    "sum_integer_of_integer_where",
                    new SumIntegerTotal(
                        new LiteralInteger(7),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "sum_decimal_of_decimal",
                    new SumDecimalTotal(new LiteralDecimal(2.5m))
                ),
                new SelectItemTotal(
                    "sum_decimal_of_decimal_null",
                    new SumDecimalTotal(new LiteralDecimalNullable())
                ),
                new SelectItemTotal(
                    "sum_decimal_of_decimal_where",
                    new SumDecimalTotal(
                        new LiteralDecimal(2.5m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "average_decimal_nullable_of_integer",
                    new AverageDecimalNullableTotal(new LiteralInteger(7))
                ),
                new SelectItemTotal(
                    "average_decimal_nullable_of_integer_null",
                    new AverageDecimalNullableTotal(new LiteralIntegerNullable())
                ),
                new SelectItemTotal(
                    "average_decimal_nullable_of_integer_where",
                    new AverageDecimalNullableTotal(
                        new LiteralInteger(7),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "average_decimal_nullable_of_decimal",
                    new AverageDecimalNullableTotal(new LiteralDecimal(2.5m))
                ),
                new SelectItemTotal(
                    "average_decimal_nullable_of_decimal_null",
                    new AverageDecimalNullableTotal(new LiteralDecimalNullable())
                ),
                new SelectItemTotal(
                    "average_decimal_nullable_of_decimal_where",
                    new AverageDecimalNullableTotal(
                        new LiteralDecimal(2.5m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "average_date_nullable_of_date",
                    new AverageDateNullableTotal(
                        new LiteralDate(new DateOnly(2024, 1, 31))
                    )
                ),
                new SelectItemTotal(
                    "average_date_nullable_of_date_null",
                    new AverageDateNullableTotal(new LiteralDateNullable())
                ),
                new SelectItemTotal(
                    "average_date_nullable_of_date_where",
                    new AverageDateNullableTotal(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "average_time_nullable_of_time",
                    new AverageTimeNullableTotal(new LiteralTime(new TimeOnly(8, 30, 0)))
                ),
                new SelectItemTotal(
                    "average_time_nullable_of_time_null",
                    new AverageTimeNullableTotal(new LiteralTimeNullable())
                ),
                new SelectItemTotal(
                    "average_time_nullable_of_time_where",
                    new AverageTimeNullableTotal(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "average_datetime_nullable_of_datetime",
                    new AverageDatetimeNullableTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemTotal(
                    "average_datetime_nullable_of_datetime_null",
                    new AverageDatetimeNullableTotal(new LiteralDatetimeNullable())
                ),
                new SelectItemTotal(
                    "average_datetime_nullable_of_datetime_where",
                    new AverageDatetimeNullableTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "min_integer_nullable_of_integer",
                    new MinIntegerNullableTotal(new LiteralInteger(7))
                ),
                new SelectItemTotal(
                    "min_integer_nullable_of_integer_null",
                    new MinIntegerNullableTotal(new LiteralIntegerNullable())
                ),
                new SelectItemTotal(
                    "min_integer_nullable_of_integer_where",
                    new MinIntegerNullableTotal(
                        new LiteralInteger(7),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "min_decimal_nullable_of_decimal",
                    new MinDecimalNullableTotal(new LiteralDecimal(2.5m))
                ),
                new SelectItemTotal(
                    "min_decimal_nullable_of_decimal_null",
                    new MinDecimalNullableTotal(new LiteralDecimalNullable())
                ),
                new SelectItemTotal(
                    "min_decimal_nullable_of_decimal_where",
                    new MinDecimalNullableTotal(
                        new LiteralDecimal(2.5m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "min_string_nullable_of_string",
                    new MinStringNullableTotal(new LiteralString("a"))
                ),
                new SelectItemTotal(
                    "min_string_nullable_of_string_null",
                    new MinStringNullableTotal(new LiteralStringNullable())
                ),
                new SelectItemTotal(
                    "min_string_nullable_of_string_where",
                    new MinStringNullableTotal(
                        new LiteralString("a"),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "min_boolean_nullable_of_boolean",
                    new MinBooleanNullableTotal(new LiteralBoolean(true))
                ),
                new SelectItemTotal(
                    "min_boolean_nullable_of_boolean_null",
                    new MinBooleanNullableTotal(new LiteralBooleanNullable())
                ),
                new SelectItemTotal(
                    "min_boolean_nullable_of_boolean_where",
                    new MinBooleanNullableTotal(
                        new LiteralBoolean(true),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "min_date_nullable_of_date",
                    new MinDateNullableTotal(new LiteralDate(new DateOnly(2024, 1, 31)))
                ),
                new SelectItemTotal(
                    "min_date_nullable_of_date_null",
                    new MinDateNullableTotal(new LiteralDateNullable())
                ),
                new SelectItemTotal(
                    "min_date_nullable_of_date_where",
                    new MinDateNullableTotal(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "min_time_nullable_of_time",
                    new MinTimeNullableTotal(new LiteralTime(new TimeOnly(8, 30, 0)))
                ),
                new SelectItemTotal(
                    "min_time_nullable_of_time_null",
                    new MinTimeNullableTotal(new LiteralTimeNullable())
                ),
                new SelectItemTotal(
                    "min_time_nullable_of_time_where",
                    new MinTimeNullableTotal(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "min_datetime_nullable_of_datetime",
                    new MinDatetimeNullableTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemTotal(
                    "min_datetime_nullable_of_datetime_null",
                    new MinDatetimeNullableTotal(new LiteralDatetimeNullable())
                ),
                new SelectItemTotal(
                    "min_datetime_nullable_of_datetime_where",
                    new MinDatetimeNullableTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "min_uuid_nullable_of_uuid",
                    new MinUuidNullableTotal(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8]))
                    )
                ),
                new SelectItemTotal(
                    "min_uuid_nullable_of_uuid_null",
                    new MinUuidNullableTotal(new LiteralUuidNullable())
                ),
                new SelectItemTotal(
                    "min_uuid_nullable_of_uuid_where",
                    new MinUuidNullableTotal(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "max_integer_nullable_of_integer",
                    new MaxIntegerNullableTotal(new LiteralInteger(7))
                ),
                new SelectItemTotal(
                    "max_integer_nullable_of_integer_null",
                    new MaxIntegerNullableTotal(new LiteralIntegerNullable())
                ),
                new SelectItemTotal(
                    "max_integer_nullable_of_integer_where",
                    new MaxIntegerNullableTotal(
                        new LiteralInteger(7),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "max_decimal_nullable_of_decimal",
                    new MaxDecimalNullableTotal(new LiteralDecimal(2.5m))
                ),
                new SelectItemTotal(
                    "max_decimal_nullable_of_decimal_null",
                    new MaxDecimalNullableTotal(new LiteralDecimalNullable())
                ),
                new SelectItemTotal(
                    "max_decimal_nullable_of_decimal_where",
                    new MaxDecimalNullableTotal(
                        new LiteralDecimal(2.5m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "max_string_nullable_of_string",
                    new MaxStringNullableTotal(new LiteralString("a"))
                ),
                new SelectItemTotal(
                    "max_string_nullable_of_string_null",
                    new MaxStringNullableTotal(new LiteralStringNullable())
                ),
                new SelectItemTotal(
                    "max_string_nullable_of_string_where",
                    new MaxStringNullableTotal(
                        new LiteralString("a"),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "max_boolean_nullable_of_boolean",
                    new MaxBooleanNullableTotal(new LiteralBoolean(true))
                ),
                new SelectItemTotal(
                    "max_boolean_nullable_of_boolean_null",
                    new MaxBooleanNullableTotal(new LiteralBooleanNullable())
                ),
                new SelectItemTotal(
                    "max_boolean_nullable_of_boolean_where",
                    new MaxBooleanNullableTotal(
                        new LiteralBoolean(true),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "max_date_nullable_of_date",
                    new MaxDateNullableTotal(new LiteralDate(new DateOnly(2024, 1, 31)))
                ),
                new SelectItemTotal(
                    "max_date_nullable_of_date_null",
                    new MaxDateNullableTotal(new LiteralDateNullable())
                ),
                new SelectItemTotal(
                    "max_date_nullable_of_date_where",
                    new MaxDateNullableTotal(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "max_time_nullable_of_time",
                    new MaxTimeNullableTotal(new LiteralTime(new TimeOnly(8, 30, 0)))
                ),
                new SelectItemTotal(
                    "max_time_nullable_of_time_null",
                    new MaxTimeNullableTotal(new LiteralTimeNullable())
                ),
                new SelectItemTotal(
                    "max_time_nullable_of_time_where",
                    new MaxTimeNullableTotal(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "max_datetime_nullable_of_datetime",
                    new MaxDatetimeNullableTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemTotal(
                    "max_datetime_nullable_of_datetime_null",
                    new MaxDatetimeNullableTotal(new LiteralDatetimeNullable())
                ),
                new SelectItemTotal(
                    "max_datetime_nullable_of_datetime_where",
                    new MaxDatetimeNullableTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "max_uuid_nullable_of_uuid",
                    new MaxUuidNullableTotal(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8]))
                    )
                ),
                new SelectItemTotal(
                    "max_uuid_nullable_of_uuid_null",
                    new MaxUuidNullableTotal(new LiteralUuidNullable())
                ),
                new SelectItemTotal(
                    "max_uuid_nullable_of_uuid_where",
                    new MaxUuidNullableTotal(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "first_integer_nullable_of_integer",
                    new FirstIntegerNullableTotal(
                        new LiteralInteger(7),
                        [new OrderItemRow(new LiteralInteger(7))]
                    )
                ),
                new SelectItemTotal(
                    "first_integer_nullable_of_integer_null",
                    new FirstIntegerNullableTotal(
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
                new SelectItemTotal(
                    "first_integer_nullable_of_integer_where",
                    new FirstIntegerNullableTotal(
                        new LiteralInteger(7),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "first_decimal_nullable_of_decimal",
                    new FirstDecimalNullableTotal(
                        new LiteralDecimal(2.5m),
                        [new OrderItemRow(new LiteralInteger(7))]
                    )
                ),
                new SelectItemTotal(
                    "first_decimal_nullable_of_decimal_null",
                    new FirstDecimalNullableTotal(
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
                new SelectItemTotal(
                    "first_decimal_nullable_of_decimal_where",
                    new FirstDecimalNullableTotal(
                        new LiteralDecimal(2.5m),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "first_string_nullable_of_string",
                    new FirstStringNullableTotal(
                        new LiteralString("a"),
                        [new OrderItemRow(new LiteralInteger(7))]
                    )
                ),
                new SelectItemTotal(
                    "first_string_nullable_of_string_null",
                    new FirstStringNullableTotal(
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
                new SelectItemTotal(
                    "first_string_nullable_of_string_where",
                    new FirstStringNullableTotal(
                        new LiteralString("a"),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "first_boolean_nullable_of_boolean",
                    new FirstBooleanNullableTotal(
                        new LiteralBoolean(true),
                        [new OrderItemRow(new LiteralInteger(7))]
                    )
                ),
                new SelectItemTotal(
                    "first_boolean_nullable_of_boolean_null",
                    new FirstBooleanNullableTotal(
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
                new SelectItemTotal(
                    "first_boolean_nullable_of_boolean_where",
                    new FirstBooleanNullableTotal(
                        new LiteralBoolean(true),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "first_date_nullable_of_date",
                    new FirstDateNullableTotal(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        [new OrderItemRow(new LiteralInteger(7))]
                    )
                ),
                new SelectItemTotal(
                    "first_date_nullable_of_date_null",
                    new FirstDateNullableTotal(
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
                new SelectItemTotal(
                    "first_date_nullable_of_date_where",
                    new FirstDateNullableTotal(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "first_time_nullable_of_time",
                    new FirstTimeNullableTotal(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        [new OrderItemRow(new LiteralInteger(7))]
                    )
                ),
                new SelectItemTotal(
                    "first_time_nullable_of_time_null",
                    new FirstTimeNullableTotal(
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
                new SelectItemTotal(
                    "first_time_nullable_of_time_where",
                    new FirstTimeNullableTotal(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "first_datetime_nullable_of_datetime",
                    new FirstDatetimeNullableTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        [new OrderItemRow(new LiteralInteger(7))]
                    )
                ),
                new SelectItemTotal(
                    "first_datetime_nullable_of_datetime_null",
                    new FirstDatetimeNullableTotal(
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
                new SelectItemTotal(
                    "first_datetime_nullable_of_datetime_where",
                    new FirstDatetimeNullableTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "first_uuid_nullable_of_uuid",
                    new FirstUuidNullableTotal(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        [new OrderItemRow(new LiteralInteger(7))]
                    )
                ),
                new SelectItemTotal(
                    "first_uuid_nullable_of_uuid_null",
                    new FirstUuidNullableTotal(
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
                new SelectItemTotal(
                    "first_uuid_nullable_of_uuid_where",
                    new FirstUuidNullableTotal(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "last_integer_nullable_of_integer",
                    new LastIntegerNullableTotal(
                        new LiteralInteger(7),
                        [new OrderItemRow(new LiteralInteger(7))]
                    )
                ),
                new SelectItemTotal(
                    "last_integer_nullable_of_integer_null",
                    new LastIntegerNullableTotal(
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
                new SelectItemTotal(
                    "last_integer_nullable_of_integer_where",
                    new LastIntegerNullableTotal(
                        new LiteralInteger(7),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "last_decimal_nullable_of_decimal",
                    new LastDecimalNullableTotal(
                        new LiteralDecimal(2.5m),
                        [new OrderItemRow(new LiteralInteger(7))]
                    )
                ),
                new SelectItemTotal(
                    "last_decimal_nullable_of_decimal_null",
                    new LastDecimalNullableTotal(
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
                new SelectItemTotal(
                    "last_decimal_nullable_of_decimal_where",
                    new LastDecimalNullableTotal(
                        new LiteralDecimal(2.5m),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "last_string_nullable_of_string",
                    new LastStringNullableTotal(
                        new LiteralString("a"),
                        [new OrderItemRow(new LiteralInteger(7))]
                    )
                ),
                new SelectItemTotal(
                    "last_string_nullable_of_string_null",
                    new LastStringNullableTotal(
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
                new SelectItemTotal(
                    "last_string_nullable_of_string_where",
                    new LastStringNullableTotal(
                        new LiteralString("a"),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "last_boolean_nullable_of_boolean",
                    new LastBooleanNullableTotal(
                        new LiteralBoolean(true),
                        [new OrderItemRow(new LiteralInteger(7))]
                    )
                ),
                new SelectItemTotal(
                    "last_boolean_nullable_of_boolean_null",
                    new LastBooleanNullableTotal(
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
                new SelectItemTotal(
                    "last_boolean_nullable_of_boolean_where",
                    new LastBooleanNullableTotal(
                        new LiteralBoolean(true),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "last_date_nullable_of_date",
                    new LastDateNullableTotal(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        [new OrderItemRow(new LiteralInteger(7))]
                    )
                ),
                new SelectItemTotal(
                    "last_date_nullable_of_date_null",
                    new LastDateNullableTotal(
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
                new SelectItemTotal(
                    "last_date_nullable_of_date_where",
                    new LastDateNullableTotal(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "last_time_nullable_of_time",
                    new LastTimeNullableTotal(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        [new OrderItemRow(new LiteralInteger(7))]
                    )
                ),
                new SelectItemTotal(
                    "last_time_nullable_of_time_null",
                    new LastTimeNullableTotal(
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
                new SelectItemTotal(
                    "last_time_nullable_of_time_where",
                    new LastTimeNullableTotal(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "last_datetime_nullable_of_datetime",
                    new LastDatetimeNullableTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        [new OrderItemRow(new LiteralInteger(7))]
                    )
                ),
                new SelectItemTotal(
                    "last_datetime_nullable_of_datetime_null",
                    new LastDatetimeNullableTotal(
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
                new SelectItemTotal(
                    "last_datetime_nullable_of_datetime_where",
                    new LastDatetimeNullableTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "last_uuid_nullable_of_uuid",
                    new LastUuidNullableTotal(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        [new OrderItemRow(new LiteralInteger(7))]
                    )
                ),
                new SelectItemTotal(
                    "last_uuid_nullable_of_uuid_null",
                    new LastUuidNullableTotal(
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
                new SelectItemTotal(
                    "last_uuid_nullable_of_uuid_where",
                    new LastUuidNullableTotal(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "median_decimal_nullable_of_integer",
                    new MedianDecimalNullableTotal(new LiteralInteger(7))
                ),
                new SelectItemTotal(
                    "median_decimal_nullable_of_integer_null",
                    new MedianDecimalNullableTotal(new LiteralIntegerNullable())
                ),
                new SelectItemTotal(
                    "median_decimal_nullable_of_integer_where",
                    new MedianDecimalNullableTotal(
                        new LiteralInteger(7),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "median_decimal_nullable_of_decimal",
                    new MedianDecimalNullableTotal(new LiteralDecimal(2.5m))
                ),
                new SelectItemTotal(
                    "median_decimal_nullable_of_decimal_null",
                    new MedianDecimalNullableTotal(new LiteralDecimalNullable())
                ),
                new SelectItemTotal(
                    "median_decimal_nullable_of_decimal_where",
                    new MedianDecimalNullableTotal(
                        new LiteralDecimal(2.5m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "median_date_nullable_of_date",
                    new MedianDateNullableTotal(
                        new LiteralDate(new DateOnly(2024, 1, 31))
                    )
                ),
                new SelectItemTotal(
                    "median_date_nullable_of_date_null",
                    new MedianDateNullableTotal(new LiteralDateNullable())
                ),
                new SelectItemTotal(
                    "median_date_nullable_of_date_where",
                    new MedianDateNullableTotal(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "median_time_nullable_of_time",
                    new MedianTimeNullableTotal(new LiteralTime(new TimeOnly(8, 30, 0)))
                ),
                new SelectItemTotal(
                    "median_time_nullable_of_time_null",
                    new MedianTimeNullableTotal(new LiteralTimeNullable())
                ),
                new SelectItemTotal(
                    "median_time_nullable_of_time_where",
                    new MedianTimeNullableTotal(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "median_datetime_nullable_of_datetime",
                    new MedianDatetimeNullableTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemTotal(
                    "median_datetime_nullable_of_datetime_null",
                    new MedianDatetimeNullableTotal(new LiteralDatetimeNullable())
                ),
                new SelectItemTotal(
                    "median_datetime_nullable_of_datetime_where",
                    new MedianDatetimeNullableTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "percentileContinuous_decimal_nullable_of_integer",
                    new PercentileContinuousDecimalNullableTotal(
                        new LiteralInteger(7),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemTotal(
                    "percentileContinuous_decimal_nullable_of_integer_null",
                    new PercentileContinuousDecimalNullableTotal(
                        new LiteralIntegerNullable(),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemTotal(
                    "percentileContinuous_decimal_nullable_of_integer_where",
                    new PercentileContinuousDecimalNullableTotal(
                        new LiteralInteger(7),
                        new LiteralDecimal(0.9m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "percentileContinuous_decimal_nullable_of_decimal",
                    new PercentileContinuousDecimalNullableTotal(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemTotal(
                    "percentileContinuous_decimal_nullable_of_decimal_null",
                    new PercentileContinuousDecimalNullableTotal(
                        new LiteralDecimalNullable(),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemTotal(
                    "percentileContinuous_decimal_nullable_of_decimal_where",
                    new PercentileContinuousDecimalNullableTotal(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.9m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "percentileContinuous_date_nullable_of_date",
                    new PercentileContinuousDateNullableTotal(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemTotal(
                    "percentileContinuous_date_nullable_of_date_null",
                    new PercentileContinuousDateNullableTotal(
                        new LiteralDateNullable(),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemTotal(
                    "percentileContinuous_date_nullable_of_date_where",
                    new PercentileContinuousDateNullableTotal(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDecimal(0.9m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "percentileContinuous_time_nullable_of_time",
                    new PercentileContinuousTimeNullableTotal(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemTotal(
                    "percentileContinuous_time_nullable_of_time_null",
                    new PercentileContinuousTimeNullableTotal(
                        new LiteralTimeNullable(),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemTotal(
                    "percentileContinuous_time_nullable_of_time_where",
                    new PercentileContinuousTimeNullableTotal(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralDecimal(0.9m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "percentileContinuous_datetime_nullable_of_datetime",
                    new PercentileContinuousDatetimeNullableTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemTotal(
                    "percentileContinuous_datetime_nullable_of_datetime_null",
                    new PercentileContinuousDatetimeNullableTotal(
                        new LiteralDatetimeNullable(),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemTotal(
                    "percentileContinuous_datetime_nullable_of_datetime_where",
                    new PercentileContinuousDatetimeNullableTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDecimal(0.9m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "percentileDiscrete_integer_nullable_of_integer",
                    new PercentileDiscreteIntegerNullableTotal(
                        new LiteralInteger(7),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemTotal(
                    "percentileDiscrete_integer_nullable_of_integer_null",
                    new PercentileDiscreteIntegerNullableTotal(
                        new LiteralIntegerNullable(),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemTotal(
                    "percentileDiscrete_integer_nullable_of_integer_where",
                    new PercentileDiscreteIntegerNullableTotal(
                        new LiteralInteger(7),
                        new LiteralDecimal(0.9m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "percentileDiscrete_decimal_nullable_of_decimal",
                    new PercentileDiscreteDecimalNullableTotal(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemTotal(
                    "percentileDiscrete_decimal_nullable_of_decimal_null",
                    new PercentileDiscreteDecimalNullableTotal(
                        new LiteralDecimalNullable(),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemTotal(
                    "percentileDiscrete_decimal_nullable_of_decimal_where",
                    new PercentileDiscreteDecimalNullableTotal(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.9m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "percentileDiscrete_string_nullable_of_string",
                    new PercentileDiscreteStringNullableTotal(
                        new LiteralString("a"),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemTotal(
                    "percentileDiscrete_string_nullable_of_string_null",
                    new PercentileDiscreteStringNullableTotal(
                        new LiteralStringNullable(),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemTotal(
                    "percentileDiscrete_string_nullable_of_string_where",
                    new PercentileDiscreteStringNullableTotal(
                        new LiteralString("a"),
                        new LiteralDecimal(0.9m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "percentileDiscrete_boolean_nullable_of_boolean",
                    new PercentileDiscreteBooleanNullableTotal(
                        new LiteralBoolean(true),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemTotal(
                    "percentileDiscrete_boolean_nullable_of_boolean_null",
                    new PercentileDiscreteBooleanNullableTotal(
                        new LiteralBooleanNullable(),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemTotal(
                    "percentileDiscrete_boolean_nullable_of_boolean_where",
                    new PercentileDiscreteBooleanNullableTotal(
                        new LiteralBoolean(true),
                        new LiteralDecimal(0.9m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "percentileDiscrete_date_nullable_of_date",
                    new PercentileDiscreteDateNullableTotal(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemTotal(
                    "percentileDiscrete_date_nullable_of_date_null",
                    new PercentileDiscreteDateNullableTotal(
                        new LiteralDateNullable(),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemTotal(
                    "percentileDiscrete_date_nullable_of_date_where",
                    new PercentileDiscreteDateNullableTotal(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDecimal(0.9m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "percentileDiscrete_time_nullable_of_time",
                    new PercentileDiscreteTimeNullableTotal(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemTotal(
                    "percentileDiscrete_time_nullable_of_time_null",
                    new PercentileDiscreteTimeNullableTotal(
                        new LiteralTimeNullable(),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemTotal(
                    "percentileDiscrete_time_nullable_of_time_where",
                    new PercentileDiscreteTimeNullableTotal(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralDecimal(0.9m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "percentileDiscrete_datetime_nullable_of_datetime",
                    new PercentileDiscreteDatetimeNullableTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemTotal(
                    "percentileDiscrete_datetime_nullable_of_datetime_null",
                    new PercentileDiscreteDatetimeNullableTotal(
                        new LiteralDatetimeNullable(),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemTotal(
                    "percentileDiscrete_datetime_nullable_of_datetime_where",
                    new PercentileDiscreteDatetimeNullableTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDecimal(0.9m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "percentileDiscrete_uuid_nullable_of_uuid",
                    new PercentileDiscreteUuidNullableTotal(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemTotal(
                    "percentileDiscrete_uuid_nullable_of_uuid_null",
                    new PercentileDiscreteUuidNullableTotal(
                        new LiteralUuidNullable(),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemTotal(
                    "percentileDiscrete_uuid_nullable_of_uuid_where",
                    new PercentileDiscreteUuidNullableTotal(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralDecimal(0.9m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "mode_integer_nullable_of_integer",
                    new ModeIntegerNullableTotal(new LiteralInteger(7))
                ),
                new SelectItemTotal(
                    "mode_integer_nullable_of_integer_null",
                    new ModeIntegerNullableTotal(new LiteralIntegerNullable())
                ),
                new SelectItemTotal(
                    "mode_integer_nullable_of_integer_where",
                    new ModeIntegerNullableTotal(
                        new LiteralInteger(7),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "mode_decimal_nullable_of_decimal",
                    new ModeDecimalNullableTotal(new LiteralDecimal(2.5m))
                ),
                new SelectItemTotal(
                    "mode_decimal_nullable_of_decimal_null",
                    new ModeDecimalNullableTotal(new LiteralDecimalNullable())
                ),
                new SelectItemTotal(
                    "mode_decimal_nullable_of_decimal_where",
                    new ModeDecimalNullableTotal(
                        new LiteralDecimal(2.5m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "mode_string_nullable_of_string",
                    new ModeStringNullableTotal(new LiteralString("a"))
                ),
                new SelectItemTotal(
                    "mode_string_nullable_of_string_null",
                    new ModeStringNullableTotal(new LiteralStringNullable())
                ),
                new SelectItemTotal(
                    "mode_string_nullable_of_string_where",
                    new ModeStringNullableTotal(
                        new LiteralString("a"),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "mode_boolean_nullable_of_boolean",
                    new ModeBooleanNullableTotal(new LiteralBoolean(true))
                ),
                new SelectItemTotal(
                    "mode_boolean_nullable_of_boolean_null",
                    new ModeBooleanNullableTotal(new LiteralBooleanNullable())
                ),
                new SelectItemTotal(
                    "mode_boolean_nullable_of_boolean_where",
                    new ModeBooleanNullableTotal(
                        new LiteralBoolean(true),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "mode_date_nullable_of_date",
                    new ModeDateNullableTotal(new LiteralDate(new DateOnly(2024, 1, 31)))
                ),
                new SelectItemTotal(
                    "mode_date_nullable_of_date_null",
                    new ModeDateNullableTotal(new LiteralDateNullable())
                ),
                new SelectItemTotal(
                    "mode_date_nullable_of_date_where",
                    new ModeDateNullableTotal(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "mode_time_nullable_of_time",
                    new ModeTimeNullableTotal(new LiteralTime(new TimeOnly(8, 30, 0)))
                ),
                new SelectItemTotal(
                    "mode_time_nullable_of_time_null",
                    new ModeTimeNullableTotal(new LiteralTimeNullable())
                ),
                new SelectItemTotal(
                    "mode_time_nullable_of_time_where",
                    new ModeTimeNullableTotal(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "mode_datetime_nullable_of_datetime",
                    new ModeDatetimeNullableTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemTotal(
                    "mode_datetime_nullable_of_datetime_null",
                    new ModeDatetimeNullableTotal(new LiteralDatetimeNullable())
                ),
                new SelectItemTotal(
                    "mode_datetime_nullable_of_datetime_where",
                    new ModeDatetimeNullableTotal(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "mode_uuid_nullable_of_uuid",
                    new ModeUuidNullableTotal(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8]))
                    )
                ),
                new SelectItemTotal(
                    "mode_uuid_nullable_of_uuid_null",
                    new ModeUuidNullableTotal(new LiteralUuidNullable())
                ),
                new SelectItemTotal(
                    "mode_uuid_nullable_of_uuid_where",
                    new ModeUuidNullableTotal(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "variancePopulation_decimal_nullable_of_integer",
                    new VariancePopulationTotal(new LiteralInteger(7))
                ),
                new SelectItemTotal(
                    "variancePopulation_decimal_nullable_of_integer_null",
                    new VariancePopulationTotal(new LiteralIntegerNullable())
                ),
                new SelectItemTotal(
                    "variancePopulation_decimal_nullable_of_integer_where",
                    new VariancePopulationTotal(
                        new LiteralInteger(7),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "variancePopulation_decimal_nullable_of_decimal",
                    new VariancePopulationTotal(new LiteralDecimal(2.5m))
                ),
                new SelectItemTotal(
                    "variancePopulation_decimal_nullable_of_decimal_null",
                    new VariancePopulationTotal(new LiteralDecimalNullable())
                ),
                new SelectItemTotal(
                    "variancePopulation_decimal_nullable_of_decimal_where",
                    new VariancePopulationTotal(
                        new LiteralDecimal(2.5m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "standardDeviationPopulation_decimal_nullable_of_integer",
                    new StandardDeviationPopulationTotal(new LiteralInteger(7))
                ),
                new SelectItemTotal(
                    "standardDeviationPopulation_decimal_nullable_of_integer_null",
                    new StandardDeviationPopulationTotal(new LiteralIntegerNullable())
                ),
                new SelectItemTotal(
                    "standardDeviationPopulation_decimal_nullable_of_integer_where",
                    new StandardDeviationPopulationTotal(
                        new LiteralInteger(7),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "standardDeviationPopulation_decimal_nullable_of_decimal",
                    new StandardDeviationPopulationTotal(new LiteralDecimal(2.5m))
                ),
                new SelectItemTotal(
                    "standardDeviationPopulation_decimal_nullable_of_decimal_null",
                    new StandardDeviationPopulationTotal(new LiteralDecimalNullable())
                ),
                new SelectItemTotal(
                    "standardDeviationPopulation_decimal_nullable_of_decimal_where",
                    new StandardDeviationPopulationTotal(
                        new LiteralDecimal(2.5m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "varianceSample_decimal_nullable_of_integer",
                    new VarianceSampleTotal(new LiteralInteger(7))
                ),
                new SelectItemTotal(
                    "varianceSample_decimal_nullable_of_decimal",
                    new VarianceSampleTotal(new LiteralDecimal(2.5m))
                ),
                new SelectItemTotal(
                    "varianceSample_decimal_nullable_of_decimal_null",
                    new VarianceSampleTotal(new LiteralDecimalNullable())
                ),
                new SelectItemTotal(
                    "varianceSample_decimal_nullable_of_decimal_where",
                    new VarianceSampleTotal(
                        new LiteralDecimal(2.5m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "standardDeviationSample_decimal_nullable_of_integer",
                    new StandardDeviationSampleTotal(new LiteralInteger(7))
                ),
                new SelectItemTotal(
                    "standardDeviationSample_decimal_nullable_of_decimal",
                    new StandardDeviationSampleTotal(new LiteralDecimal(2.5m))
                ),
                new SelectItemTotal(
                    "standardDeviationSample_decimal_nullable_of_decimal_null",
                    new StandardDeviationSampleTotal(new LiteralDecimalNullable())
                ),
                new SelectItemTotal(
                    "standardDeviationSample_decimal_nullable_of_decimal_where",
                    new StandardDeviationSampleTotal(
                        new LiteralDecimal(2.5m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "covariancePopulation_decimal_nullable",
                    new CovariancePopulationTotal(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m)
                    )
                ),
                new SelectItemTotal(
                    "covariancePopulation_decimal_nullable_left",
                    new CovariancePopulationTotal(
                        new LiteralDecimalNullable(),
                        new LiteralDecimal(2.5m)
                    )
                ),
                new SelectItemTotal(
                    "covariancePopulation_decimal_nullable_right",
                    new CovariancePopulationTotal(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimalNullable()
                    )
                ),
                new SelectItemTotal(
                    "covariancePopulation_decimal_nullable_where",
                    new CovariancePopulationTotal(
                        new LiteralDecimal(2.5m),
                        new LiteralInteger(7),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "covarianceSample_decimal_nullable",
                    new CovarianceSampleTotal(
                        new LiteralDecimal(2.5m),
                        new LiteralInteger(7)
                    )
                ),
                new SelectItemTotal(
                    "covarianceSample_decimal_nullable_of_nulls_where",
                    new CovarianceSampleTotal(
                        new LiteralDecimalNullable(),
                        new LiteralIntegerNullable(),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "correlation_decimal_nullable",
                    new CorrelationTotal(new LiteralDecimal(2.5m), new LiteralInteger(7))
                ),
                new SelectItemTotal(
                    "correlation_decimal_nullable_of_nulls_where",
                    new CorrelationTotal(
                        new LiteralDecimalNullable(),
                        new LiteralDecimalNullable(),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "regressionSlope_decimal_nullable",
                    new RegressionSlopeTotal(
                        new LiteralDecimal(2.5m),
                        new LiteralInteger(7)
                    )
                ),
                new SelectItemTotal(
                    "regressionSlope_decimal_nullable_of_integers",
                    new RegressionSlopeTotal(
                        new LiteralInteger(7),
                        new LiteralInteger(-3)
                    )
                ),
                new SelectItemTotal(
                    "regressionSlope_decimal_nullable_of_nulls_where",
                    new RegressionSlopeTotal(
                        new LiteralDecimalNullable(),
                        new LiteralIntegerNullable(),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "regressionIntercept_decimal_nullable",
                    new RegressionInterceptTotal(
                        new LiteralDecimal(2.5m),
                        new LiteralInteger(7)
                    )
                ),
                new SelectItemTotal(
                    "regressionIntercept_decimal_nullable_of_integers",
                    new RegressionInterceptTotal(
                        new LiteralInteger(7),
                        new LiteralInteger(-3)
                    )
                ),
                new SelectItemTotal(
                    "regressionIntercept_decimal_nullable_of_nulls_where",
                    new RegressionInterceptTotal(
                        new LiteralDecimalNullable(),
                        new LiteralIntegerNullable(),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "regressionRSquared_decimal_nullable",
                    new RegressionRSquaredTotal(
                        new LiteralDecimal(2.5m),
                        new LiteralInteger(7)
                    )
                ),
                new SelectItemTotal(
                    "regressionRSquared_decimal_nullable_of_integers",
                    new RegressionRSquaredTotal(
                        new LiteralInteger(7),
                        new LiteralInteger(-3)
                    )
                ),
                new SelectItemTotal(
                    "regressionRSquared_decimal_nullable_of_nulls_where",
                    new RegressionRSquaredTotal(
                        new LiteralDecimalNullable(),
                        new LiteralIntegerNullable(),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "concatAll_string_of_string",
                    new ConcatAllTotal(
                        new LiteralString("a"),
                        new LiteralString(","),
                        [new OrderItemRow(new LiteralInteger(7))]
                    )
                ),
                new SelectItemTotal(
                    "concatAll_string_of_string_null",
                    new ConcatAllTotal(
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
                new SelectItemTotal(
                    "concatAll_string_of_string_where",
                    new ConcatAllTotal(
                        new LiteralString("a"),
                        new LiteralString(","),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemTotal(
                    "concatAll_string_of_string_distinct",
                    new ConcatAllTotal(
                        new LiteralString("a"),
                        new LiteralString(""),
                        [new OrderItemRow(new LiteralInteger(7))],
                        distinct: true
                    )
                ),
                new SelectItemTotal(
                    "concatAll_string_parameter_separator",
                    new ConcatAllTotal(
                        new LiteralString("a"),
                        new ParamString("separator"),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true),
                        distinct: true
                    )
                ),
                new SelectItemTotal(
                    "percentileContinuous_parameter_fraction",
                    new PercentileContinuousDecimalNullableTotal(
                        new LiteralDecimal(2.5m),
                        new ParamDecimal("fraction")
                    )
                ),
                new SelectItemTotal(
                    "percentileDiscrete_fraction_zero",
                    new PercentileDiscreteIntegerNullableTotal(
                        new LiteralInteger(7),
                        new LiteralDecimal(0m)
                    )
                ),
                new SelectItemTotal(
                    "percentileDiscrete_fraction_one",
                    new PercentileDiscreteIntegerNullableTotal(
                        new LiteralInteger(7),
                        new LiteralDecimal(1m)
                    )
                ),
                new SelectItemTotal(
                    "any_boolean_where",
                    new AnyTotal(new LiteralBoolean(true))
                ),
                new SelectItemTotal(
                    "all_boolean_where",
                    new AllTotal(new LiteralBoolean(true))
                ),
            ]
        );
}
