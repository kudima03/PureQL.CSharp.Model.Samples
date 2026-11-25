using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
using PureQL.CSharp.Model.GroupExpressions;
using PureQL.CSharp.Model.Lists;
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.Parameters;
using PureQL.CSharp.Model.RowExpressions;

namespace PureQL.CSharp.Model.Samples.Queries.GroupBy;

/// <summary>
/// Groups schema_with_foreign_keys.orders by 555 keys and selects the number of rows of
/// each group as n. Every key is an operator over literals, one per signature of every
/// operator: the lifted operators with and without a nullable operand, integer and
/// decimal operands, if and coalesce for every type, equal, notEqual, the four
/// comparisons and in for every family. Some operands are parameters, so the query has no
/// result.
/// </summary>
public sealed record EveryOperatorSignatureAsGroupKeysQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainGroupedQueryWithoutPagination(
            from: new FromEntity(
                new JoinedString(
                    new DotString(),
                    [new RelationalSchemaWithForeignKeys().Name, new OrdersTable().Name]
                ).TextValue
            ),
            groupBy:
            [
                new GroupKey(
                    "add_integer",
                    new AddIntegerRow([new LiteralInteger(7), new LiteralInteger(-3)])
                ),
                new GroupKey(
                    "add_integer_nullable",
                    new AddIntegerNullableRow([
                        new LiteralInteger(7),
                        new LiteralIntegerNullable(),
                    ])
                ),
                new GroupKey(
                    "add_decimal",
                    new AddDecimalRow([new LiteralInteger(7), new LiteralDecimal(2.5m)])
                ),
                new GroupKey(
                    "add_decimal_nullable",
                    new AddDecimalNullableRow([
                        new LiteralDecimalNullable(),
                        new LiteralInteger(7),
                    ])
                ),
                new GroupKey(
                    "subtract_integer",
                    new SubtractIntegerRow([
                        new LiteralInteger(7),
                        new LiteralInteger(-3),
                    ])
                ),
                new GroupKey(
                    "subtract_integer_nullable",
                    new SubtractIntegerNullableRow([
                        new LiteralInteger(7),
                        new LiteralIntegerNullable(),
                    ])
                ),
                new GroupKey(
                    "subtract_decimal",
                    new SubtractDecimalRow([
                        new LiteralInteger(7),
                        new LiteralDecimal(2.5m),
                    ])
                ),
                new GroupKey(
                    "subtract_decimal_nullable",
                    new SubtractDecimalNullableRow([
                        new LiteralDecimalNullable(),
                        new LiteralInteger(7),
                    ])
                ),
                new GroupKey(
                    "multiply_integer",
                    new MultiplyIntegerRow([
                        new LiteralInteger(7),
                        new LiteralInteger(-3),
                    ])
                ),
                new GroupKey(
                    "multiply_integer_nullable",
                    new MultiplyIntegerNullableRow([
                        new LiteralInteger(7),
                        new LiteralIntegerNullable(),
                    ])
                ),
                new GroupKey(
                    "multiply_decimal",
                    new MultiplyDecimalRow([
                        new LiteralInteger(7),
                        new LiteralDecimal(2.5m),
                    ])
                ),
                new GroupKey(
                    "multiply_decimal_nullable",
                    new MultiplyDecimalNullableRow([
                        new LiteralDecimalNullable(),
                        new LiteralInteger(7),
                    ])
                ),
                new GroupKey(
                    "divide_decimal",
                    new DivideDecimalRow([
                        new LiteralInteger(7),
                        new LiteralDecimal(2.5m),
                    ])
                ),
                new GroupKey(
                    "divide_decimal_nullable",
                    new DivideDecimalNullableRow([
                        new LiteralDecimalNullable(),
                        new LiteralInteger(7),
                    ])
                ),
                new GroupKey(
                    "integerDivide_integer",
                    new IntegerDivideIntegerRow(
                        new LiteralInteger(7),
                        new LiteralInteger(-3)
                    )
                ),
                new GroupKey(
                    "integerDivide_integer_nullable",
                    new IntegerDivideIntegerNullableRow(
                        new LiteralIntegerNullable(),
                        new LiteralInteger(-3)
                    )
                ),
                new GroupKey(
                    "modulo_integer",
                    new ModuloIntegerRow(new LiteralInteger(7), new LiteralInteger(-3))
                ),
                new GroupKey(
                    "modulo_integer_nullable",
                    new ModuloIntegerNullableRow(
                        new LiteralIntegerNullable(),
                        new LiteralInteger(-3)
                    )
                ),
                new GroupKey(
                    "modulo_decimal",
                    new ModuloDecimalRow(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m)
                    )
                ),
                new GroupKey(
                    "modulo_decimal_of_integer_and_decimal",
                    new ModuloDecimalRow(new LiteralInteger(7), new LiteralDecimal(2.5m))
                ),
                new GroupKey(
                    "modulo_decimal_of_decimal_and_integer",
                    new ModuloDecimalRow(new LiteralDecimal(2.5m), new LiteralInteger(7))
                ),
                new GroupKey(
                    "modulo_decimal_nullable",
                    new ModuloDecimalNullableRow(
                        new LiteralDecimalNullable(),
                        new LiteralDecimal(0.1m)
                    )
                ),
                new GroupKey(
                    "modulo_decimal_nullable_right",
                    new ModuloDecimalNullableRow(
                        new LiteralDecimal(2.5m),
                        new LiteralIntegerNullable()
                    )
                ),
                new GroupKey(
                    "integerDivide_integer_of_decimals",
                    new IntegerDivideIntegerRow(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m)
                    )
                ),
                new GroupKey(
                    "integerDivide_integer_of_integer_and_decimal",
                    new IntegerDivideIntegerRow(
                        new LiteralInteger(7),
                        new LiteralDecimal(2.5m)
                    )
                ),
                new GroupKey(
                    "integerDivide_integer_nullable_of_decimals",
                    new IntegerDivideIntegerNullableRow(
                        new LiteralDecimalNullable(),
                        new LiteralDecimal(0.1m)
                    )
                ),
                new GroupKey(
                    "floor_integer",
                    new FloorIntegerRow(new LiteralDecimal(2.5m))
                ),
                new GroupKey(
                    "floor_integer_nullable",
                    new FloorIntegerNullableRow(new LiteralDecimalNullable())
                ),
                new GroupKey(
                    "ceiling_integer",
                    new CeilingIntegerRow(new LiteralDecimal(2.5m))
                ),
                new GroupKey(
                    "ceiling_integer_nullable",
                    new CeilingIntegerNullableRow(new LiteralDecimalNullable())
                ),
                new GroupKey(
                    "round_integer",
                    new RoundWithoutDigitsIntegerRow(new LiteralDecimal(2.5m))
                ),
                new GroupKey(
                    "round_integer_nullable",
                    new RoundWithoutDigitsIntegerNullableRow(new LiteralDecimalNullable())
                ),
                new GroupKey(
                    "round_decimal",
                    new RoundDecimalRow(new LiteralDecimal(2.5m), new LiteralInteger(-3))
                ),
                new GroupKey(
                    "round_decimal_nullable",
                    new RoundDecimalNullableRow(
                        new LiteralDecimalNullable(),
                        new LiteralInteger(7)
                    )
                ),
                new GroupKey(
                    "round_integer_half_even",
                    new RoundWithoutDigitsIntegerRow(
                        new LiteralDecimal(2.5m),
                        mode: RoundingMode.HalfEven
                    )
                ),
                new GroupKey(
                    "round_integer_nullable_half_even",
                    new RoundWithoutDigitsIntegerNullableRow(
                        new LiteralDecimalNullable(),
                        mode: RoundingMode.HalfEven
                    )
                ),
                new GroupKey(
                    "round_decimal_half_even",
                    new RoundDecimalRow(
                        new LiteralDecimal(2.5m),
                        new LiteralInteger(2),
                        mode: RoundingMode.HalfEven
                    )
                ),
                new GroupKey(
                    "round_decimal_nullable_half_even",
                    new RoundDecimalNullableRow(
                        new LiteralDecimalNullable(),
                        new LiteralInteger(2),
                        mode: RoundingMode.HalfEven
                    )
                ),
                new GroupKey(
                    "round_integer_of_integer_half_even",
                    new RoundWithDigitsIntegerRow(
                        new LiteralInteger(7),
                        new LiteralInteger(-1),
                        mode: RoundingMode.HalfEven
                    )
                ),
                new GroupKey(
                    "truncate_integer",
                    new TruncateWithoutDigitsIntegerRow(new LiteralDecimal(2.5m))
                ),
                new GroupKey(
                    "truncate_integer_nullable",
                    new TruncateWithoutDigitsIntegerNullableRow(
                        new LiteralDecimalNullable()
                    )
                ),
                new GroupKey(
                    "truncate_decimal",
                    new TruncateDecimalRow(
                        new LiteralDecimal(2.5m),
                        new LiteralInteger(7)
                    )
                ),
                new GroupKey(
                    "truncate_decimal_nullable",
                    new TruncateDecimalNullableRow(
                        new LiteralDecimalNullable(),
                        new LiteralInteger(7)
                    )
                ),
                new GroupKey("abs_integer", new AbsIntegerRow(new LiteralInteger(-3))),
                new GroupKey(
                    "abs_integer_nullable",
                    new AbsIntegerNullableRow(new LiteralIntegerNullable())
                ),
                new GroupKey("abs_decimal", new AbsDecimalRow(new LiteralDecimal(2.5m))),
                new GroupKey(
                    "abs_decimal_nullable",
                    new AbsDecimalNullableRow(new LiteralDecimalNullable())
                ),
                new GroupKey(
                    "sign_integer",
                    new SignIntegerRow(new LiteralDecimal(2.5m))
                ),
                new GroupKey(
                    "sign_integer_nullable",
                    new SignIntegerNullableRow(new LiteralDecimalNullable())
                ),
                new GroupKey(
                    "sign_integer_of_integer",
                    new SignIntegerRow(new LiteralInteger(-3))
                ),
                new GroupKey(
                    "power_decimal",
                    new PowerDecimalRow(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m)
                    )
                ),
                new GroupKey(
                    "power_decimal_nullable",
                    new PowerDecimalNullableRow(
                        new LiteralDecimalNullable(),
                        new LiteralInteger(7)
                    )
                ),
                new GroupKey(
                    "power_decimal_of_integers",
                    new PowerDecimalRow(new LiteralInteger(7), new LiteralInteger(-3))
                ),
                new GroupKey(
                    "sqrt_decimal",
                    new SqrtDecimalRow(new LiteralDecimal(2.5m))
                ),
                new GroupKey(
                    "sqrt_decimal_nullable",
                    new SqrtDecimalNullableRow(new LiteralDecimalNullable())
                ),
                new GroupKey(
                    "sqrt_decimal_of_integer",
                    new SqrtDecimalRow(new LiteralInteger(7))
                ),
                new GroupKey("exp_decimal", new ExpDecimalRow(new LiteralDecimal(2.5m))),
                new GroupKey(
                    "exp_decimal_nullable",
                    new ExpDecimalNullableRow(new LiteralDecimalNullable())
                ),
                new GroupKey(
                    "exp_decimal_of_integer",
                    new ExpDecimalRow(new LiteralInteger(7))
                ),
                new GroupKey("ln_decimal", new LnDecimalRow(new LiteralDecimal(2.5m))),
                new GroupKey(
                    "ln_decimal_nullable",
                    new LnDecimalNullableRow(new LiteralDecimalNullable())
                ),
                new GroupKey(
                    "ln_decimal_of_integer",
                    new LnDecimalRow(new LiteralInteger(7))
                ),
                new GroupKey(
                    "log10_decimal",
                    new Log10DecimalRow(new LiteralDecimal(2.5m))
                ),
                new GroupKey(
                    "log10_decimal_nullable",
                    new Log10DecimalNullableRow(new LiteralDecimalNullable())
                ),
                new GroupKey(
                    "log10_decimal_of_integer",
                    new Log10DecimalRow(new LiteralInteger(7))
                ),
                new GroupKey(
                    "log_decimal",
                    new LogDecimalRow(new LiteralDecimal(2.5m), new LiteralDecimal(0.1m))
                ),
                new GroupKey(
                    "log_decimal_nullable",
                    new LogDecimalNullableRow(
                        new LiteralDecimalNullable(),
                        new LiteralInteger(7)
                    )
                ),
                new GroupKey("sin_decimal", new SinDecimalRow(new LiteralDecimal(0.5m))),
                new GroupKey(
                    "sin_decimal_of_integer",
                    new SinDecimalRow(new LiteralInteger(1))
                ),
                new GroupKey(
                    "sin_decimal_nullable",
                    new SinDecimalNullableRow(new LiteralDecimalNullable())
                ),
                new GroupKey("cos_decimal", new CosDecimalRow(new LiteralDecimal(0.5m))),
                new GroupKey(
                    "cos_decimal_of_integer",
                    new CosDecimalRow(new LiteralInteger(1))
                ),
                new GroupKey(
                    "cos_decimal_nullable",
                    new CosDecimalNullableRow(new LiteralDecimalNullable())
                ),
                new GroupKey("tan_decimal", new TanDecimalRow(new LiteralDecimal(0.5m))),
                new GroupKey(
                    "tan_decimal_of_integer",
                    new TanDecimalRow(new LiteralInteger(1))
                ),
                new GroupKey(
                    "tan_decimal_nullable",
                    new TanDecimalNullableRow(new LiteralDecimalNullable())
                ),
                new GroupKey(
                    "asin_decimal",
                    new AsinDecimalRow(new LiteralDecimal(0.5m))
                ),
                new GroupKey(
                    "asin_decimal_of_integer",
                    new AsinDecimalRow(new LiteralInteger(1))
                ),
                new GroupKey(
                    "asin_decimal_nullable",
                    new AsinDecimalNullableRow(new LiteralDecimalNullable())
                ),
                new GroupKey(
                    "acos_decimal",
                    new AcosDecimalRow(new LiteralDecimal(0.5m))
                ),
                new GroupKey(
                    "acos_decimal_of_integer",
                    new AcosDecimalRow(new LiteralInteger(1))
                ),
                new GroupKey(
                    "acos_decimal_nullable",
                    new AcosDecimalNullableRow(new LiteralDecimalNullable())
                ),
                new GroupKey(
                    "atan_decimal",
                    new AtanDecimalRow(new LiteralDecimal(0.5m))
                ),
                new GroupKey(
                    "atan_decimal_of_integer",
                    new AtanDecimalRow(new LiteralInteger(1))
                ),
                new GroupKey(
                    "atan_decimal_nullable",
                    new AtanDecimalNullableRow(new LiteralDecimalNullable())
                ),
                new GroupKey(
                    "atan2_decimal",
                    new Atan2DecimalRow(new LiteralDecimal(0.5m), new LiteralDecimal(-1m))
                ),
                new GroupKey(
                    "atan2_decimal_of_integers",
                    new Atan2DecimalRow(new LiteralInteger(1), new LiteralInteger(-3))
                ),
                new GroupKey(
                    "atan2_decimal_nullable_y",
                    new Atan2DecimalNullableRow(
                        new LiteralDecimalNullable(),
                        new LiteralDecimal(0.5m)
                    )
                ),
                new GroupKey(
                    "atan2_decimal_nullable_x",
                    new Atan2DecimalNullableRow(
                        new LiteralDecimal(0.5m),
                        new LiteralDecimalNullable()
                    )
                ),
                new GroupKey("pi_decimal", new PiRow()),
                new GroupKey(
                    "log_decimal_of_integers",
                    new LogDecimalRow(new LiteralInteger(7), new LiteralInteger(7))
                ),
                new GroupKey(
                    "concat_string",
                    new ConcatStringRow([new LiteralString("a"), new LiteralString("b")])
                ),
                new GroupKey(
                    "concat_string_nullable",
                    new ConcatStringNullableRow([
                        new LiteralString("a"),
                        new LiteralStringNullable(),
                    ])
                ),
                new GroupKey(
                    "length_integer",
                    new LengthIntegerRow(new LiteralString("abc"))
                ),
                new GroupKey(
                    "length_integer_nullable",
                    new LengthIntegerNullableRow(new LiteralStringNullable())
                ),
                new GroupKey(
                    "upper_string",
                    new UpperStringRow(new LiteralString("Stra\u00dfe"))
                ),
                new GroupKey(
                    "upper_string_nullable",
                    new UpperStringNullableRow(new LiteralStringNullable())
                ),
                new GroupKey(
                    "lower_string",
                    new LowerStringRow(new LiteralString("Stra\u00dfe"))
                ),
                new GroupKey(
                    "lower_string_nullable",
                    new LowerStringNullableRow(new LiteralStringNullable())
                ),
                new GroupKey(
                    "foldCase_string",
                    new FoldCaseStringRow(new LiteralString("a"))
                ),
                new GroupKey(
                    "foldCase_string_nullable",
                    new FoldCaseStringNullableRow(new LiteralStringNullable())
                ),
                new GroupKey(
                    "normalize_string_nfc",
                    new NormalizeStringRow(new LiteralString("a"), NormalizationForm.NFC)
                ),
                new GroupKey(
                    "normalize_string_nfd",
                    new NormalizeStringRow(new LiteralString("a"), NormalizationForm.NFD)
                ),
                new GroupKey(
                    "normalize_string_nfkc",
                    new NormalizeStringRow(new LiteralString("a"), NormalizationForm.NFKC)
                ),
                new GroupKey(
                    "normalize_string_nfkd",
                    new NormalizeStringRow(new LiteralString("a"), NormalizationForm.NFKD)
                ),
                new GroupKey(
                    "normalize_string_nullable",
                    new NormalizeStringNullableRow(
                        new LiteralStringNullable(),
                        NormalizationForm.NFC
                    )
                ),
                new GroupKey(
                    "normalize_string_nullable_nfkd",
                    new NormalizeStringNullableRow(
                        new LiteralStringNullable(),
                        NormalizationForm.NFKD
                    )
                ),
                new GroupKey(
                    "removeDiacritics_string",
                    new RemoveDiacriticsStringRow(new LiteralString("a"))
                ),
                new GroupKey(
                    "removeDiacritics_string_nullable",
                    new RemoveDiacriticsStringNullableRow(new LiteralStringNullable())
                ),
                new GroupKey(
                    "trim_string",
                    new TrimStringRow(new LiteralString("xax"), [new LiteralString("x")])
                ),
                new GroupKey(
                    "trim_string_nullable",
                    new TrimStringNullableRow(
                        new LiteralStringNullable(),
                        [new LiteralString("x"), new LiteralString("xa")]
                    )
                ),
                new GroupKey(
                    "trimStart_string",
                    new TrimStartStringRow(
                        new LiteralString("xax"),
                        [new LiteralString("x")]
                    )
                ),
                new GroupKey(
                    "trimStart_string_nullable",
                    new TrimStartStringNullableRow(
                        new LiteralStringNullable(),
                        [new LiteralString("x"), new LiteralString("xa")]
                    )
                ),
                new GroupKey(
                    "trimEnd_string",
                    new TrimEndStringRow(
                        new LiteralString("xax"),
                        [new LiteralString("x")]
                    )
                ),
                new GroupKey(
                    "trimEnd_string_nullable",
                    new TrimEndStringNullableRow(
                        new LiteralStringNullable(),
                        [new LiteralString("x"), new LiteralString("xa")]
                    )
                ),
                new GroupKey(
                    "substring_string",
                    new SubstringStringRow(
                        new LiteralString("hello"),
                        new LiteralInteger(2)
                    )
                ),
                new GroupKey(
                    "substring_string_nullable",
                    new SubstringStringNullableRow(
                        new LiteralStringNullable(),
                        new LiteralInteger(2)
                    )
                ),
                new GroupKey(
                    "substring_string_length",
                    new SubstringStringRow(
                        new LiteralString("hello"),
                        new LiteralInteger(2),
                        length: new LiteralInteger(3)
                    )
                ),
                new GroupKey(
                    "substring_string_nullable_length",
                    new SubstringStringNullableRow(
                        new LiteralStringNullable(),
                        new LiteralInteger(2),
                        length: new LiteralInteger(0)
                    )
                ),
                new GroupKey(
                    "replace_string",
                    new ReplaceStringRow(
                        new LiteralString("a-b"),
                        new LiteralString("-"),
                        new LiteralString("b")
                    )
                ),
                new GroupKey(
                    "replace_string_nullable",
                    new ReplaceStringNullableRow(
                        new LiteralStringNullable(),
                        new LiteralString("-"),
                        new LiteralString("b")
                    )
                ),
                new GroupKey(
                    "position_integer",
                    new PositionIntegerRow(
                        new LiteralString("a-b"),
                        new LiteralString("-")
                    )
                ),
                new GroupKey(
                    "position_integer_nullable",
                    new PositionIntegerNullableRow(
                        new LiteralStringNullable(),
                        new LiteralString("-")
                    )
                ),
                new GroupKey(
                    "position_integer_start",
                    new PositionIntegerRow(
                        new LiteralString("a-b"),
                        new LiteralString("-"),
                        start: new LiteralInteger(2)
                    )
                ),
                new GroupKey(
                    "position_integer_nullable_start",
                    new PositionIntegerNullableRow(
                        new LiteralStringNullable(),
                        new LiteralString("-"),
                        start: new LiteralInteger(2)
                    )
                ),
                new GroupKey(
                    "splitPart_string",
                    new SplitPartStringRow(
                        new LiteralString("a-b"),
                        new LiteralString("-"),
                        new LiteralInteger(1)
                    )
                ),
                new GroupKey(
                    "splitPart_string_nullable",
                    new SplitPartStringNullableRow(
                        new LiteralStringNullable(),
                        new LiteralString("-"),
                        new LiteralInteger(-3)
                    )
                ),
                new GroupKey(
                    "padStart_string",
                    new PadStartStringRow(
                        new LiteralString("a"),
                        new LiteralInteger(7),
                        new LiteralString("b")
                    )
                ),
                new GroupKey(
                    "padStart_string_nullable",
                    new PadStartStringNullableRow(
                        new LiteralStringNullable(),
                        new LiteralInteger(7),
                        new LiteralString("b")
                    )
                ),
                new GroupKey(
                    "padEnd_string",
                    new PadEndStringRow(
                        new LiteralString("a"),
                        new LiteralInteger(7),
                        new LiteralString("b")
                    )
                ),
                new GroupKey(
                    "padEnd_string_nullable",
                    new PadEndStringNullableRow(
                        new LiteralStringNullable(),
                        new LiteralInteger(7),
                        new LiteralString("b")
                    )
                ),
                new GroupKey(
                    "repeat_string",
                    new RepeatStringRow(new LiteralString("a"), new LiteralInteger(7))
                ),
                new GroupKey(
                    "repeat_string_nullable",
                    new RepeatStringNullableRow(
                        new LiteralStringNullable(),
                        new LiteralInteger(7)
                    )
                ),
                new GroupKey(
                    "reverse_string",
                    new ReverseStringRow(new LiteralString("a"))
                ),
                new GroupKey(
                    "reverse_string_nullable",
                    new ReverseStringNullableRow(new LiteralStringNullable())
                ),
                new GroupKey(
                    "concatWith_string",
                    new ConcatWithRow(
                        [new LiteralString("a"), new LiteralString("b")],
                        new LiteralString("-")
                    )
                ),
                new GroupKey(
                    "concatWith_string_null_values",
                    new ConcatWithRow(
                        [
                            new LiteralStringNullable(),
                            new LiteralString("a"),
                            new LiteralStringNullable(),
                        ],
                        new LiteralString("-")
                    )
                ),
                new GroupKey(
                    "concatWith_string_only_null_values",
                    new ConcatWithRow(
                        [new LiteralStringNullable(), new LiteralStringNullable()],
                        new LiteralString("-")
                    )
                ),
                new GroupKey(
                    "matchesRegex_boolean",
                    new MatchesRegexRow(
                        new LiteralString("a1"),
                        new LiteralString("[0-9]")
                    )
                ),
                new GroupKey(
                    "matchesRegex_boolean_nullable_operand",
                    new MatchesRegexRow(
                        new LiteralStringNullable(),
                        new LiteralString("[0-9]")
                    )
                ),
                new GroupKey(
                    "matchesRegex_boolean_parameter_pattern",
                    new MatchesRegexRow(
                        new LiteralString("a1"),
                        new ParamString("pattern")
                    )
                ),
                new GroupKey(
                    "extractRegex_string_nullable",
                    new ExtractRegexRow(
                        new LiteralString("a1"),
                        new LiteralString("[0-9]+")
                    )
                ),
                new GroupKey(
                    "extractRegex_string_nullable_group",
                    new ExtractRegexRow(
                        new LiteralString("a1"),
                        new LiteralString("(a)([0-9])"),
                        group: new LiteralInteger(2)
                    )
                ),
                new GroupKey(
                    "extractRegex_string_nullable_nullable_operand",
                    new ExtractRegexRow(
                        new LiteralStringNullable(),
                        new LiteralString("[0-9]+")
                    )
                ),
                new GroupKey(
                    "extractRegex_string_nullable_parameters",
                    new ExtractRegexRow(
                        new LiteralString("a1"),
                        new ParamString("pattern"),
                        group: new ParamInteger("group")
                    )
                ),
                new GroupKey(
                    "replaceRegex_string",
                    new ReplaceRegexStringRow(
                        new LiteralString("a1"),
                        new LiteralString("b"),
                        new LiteralString("[0-9]")
                    )
                ),
                new GroupKey(
                    "replaceRegex_string_nullable",
                    new ReplaceRegexStringNullableRow(
                        new LiteralStringNullable(),
                        new LiteralString("b"),
                        new LiteralString("[0-9]")
                    )
                ),
                new GroupKey(
                    "round_decimal_nullable_by_digits",
                    new RoundDecimalNullableRow(
                        new LiteralDecimal(2.5m),
                        new LiteralIntegerNullable()
                    )
                ),
                new GroupKey(
                    "truncate_decimal_nullable_by_digits",
                    new TruncateDecimalNullableRow(
                        new LiteralDecimal(2.5m),
                        new LiteralIntegerNullable()
                    )
                ),
                new GroupKey(
                    "round_integer_of_integer_with_digits",
                    new RoundWithDigitsIntegerRow(
                        new LiteralInteger(7),
                        new LiteralInteger(1)
                    )
                ),
                new GroupKey(
                    "round_integer_nullable_of_integer_with_digits",
                    new RoundWithDigitsIntegerNullableRow(
                        new LiteralIntegerNullable(),
                        new LiteralInteger(-1)
                    )
                ),
                new GroupKey(
                    "round_integer_nullable_of_integer_by_digits",
                    new RoundWithDigitsIntegerNullableRow(
                        new LiteralInteger(7),
                        new LiteralIntegerNullable()
                    )
                ),
                new GroupKey(
                    "truncate_integer_of_integer_with_digits",
                    new TruncateWithDigitsIntegerRow(
                        new LiteralInteger(7),
                        new LiteralInteger(-1)
                    )
                ),
                new GroupKey(
                    "truncate_integer_nullable_of_integer_with_digits",
                    new TruncateWithDigitsIntegerNullableRow(
                        new LiteralIntegerNullable(),
                        new LiteralInteger(2)
                    )
                ),
                new GroupKey(
                    "truncate_integer_nullable_of_integer_by_digits",
                    new TruncateWithDigitsIntegerNullableRow(
                        new LiteralInteger(7),
                        new LiteralIntegerNullable()
                    )
                ),
                new GroupKey(
                    "power_decimal_nullable_by_exponent",
                    new PowerDecimalNullableRow(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimalNullable()
                    )
                ),
                new GroupKey(
                    "log_decimal_nullable_by_base",
                    new LogDecimalNullableRow(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimalNullable()
                    )
                ),
                new GroupKey(
                    "substring_string_nullable_by_start",
                    new SubstringStringNullableRow(
                        new LiteralString("abc"),
                        new LiteralIntegerNullable(),
                        length: new LiteralInteger(2)
                    )
                ),
                new GroupKey(
                    "substring_string_nullable_by_length",
                    new SubstringStringNullableRow(
                        new LiteralString("abc"),
                        new LiteralInteger(1),
                        length: new LiteralIntegerNullable()
                    )
                ),
                new GroupKey(
                    "replace_string_nullable_by_pattern",
                    new ReplaceStringNullableRow(
                        new LiteralString("abc"),
                        new LiteralStringNullable(),
                        new LiteralString("x")
                    )
                ),
                new GroupKey(
                    "replace_string_nullable_by_replacement",
                    new ReplaceStringNullableRow(
                        new LiteralString("abc"),
                        new LiteralString("b"),
                        new LiteralStringNullable()
                    )
                ),
                new GroupKey(
                    "position_integer_nullable_by_pattern",
                    new PositionIntegerNullableRow(
                        new LiteralString("abc"),
                        new LiteralStringNullable(),
                        start: new LiteralInteger(1)
                    )
                ),
                new GroupKey(
                    "position_integer_nullable_by_start",
                    new PositionIntegerNullableRow(
                        new LiteralString("abc"),
                        new LiteralString("b"),
                        start: new LiteralIntegerNullable()
                    )
                ),
                new GroupKey(
                    "splitPart_string_nullable_by_delimiter",
                    new SplitPartStringNullableRow(
                        new LiteralString("a-b"),
                        new LiteralStringNullable(),
                        new LiteralInteger(1)
                    )
                ),
                new GroupKey(
                    "splitPart_string_nullable_by_index",
                    new SplitPartStringNullableRow(
                        new LiteralString("a-b"),
                        new LiteralString("-"),
                        new LiteralIntegerNullable()
                    )
                ),
                new GroupKey(
                    "padStart_string_nullable_by_length",
                    new PadStartStringNullableRow(
                        new LiteralString("abc"),
                        new LiteralIntegerNullable(),
                        new LiteralString("0")
                    )
                ),
                new GroupKey(
                    "padStart_string_nullable_by_fill",
                    new PadStartStringNullableRow(
                        new LiteralString("abc"),
                        new LiteralInteger(7),
                        new LiteralStringNullable()
                    )
                ),
                new GroupKey(
                    "padEnd_string_nullable_by_length",
                    new PadEndStringNullableRow(
                        new LiteralString("abc"),
                        new LiteralIntegerNullable(),
                        new LiteralString("0")
                    )
                ),
                new GroupKey(
                    "padEnd_string_nullable_by_fill",
                    new PadEndStringNullableRow(
                        new LiteralString("abc"),
                        new LiteralInteger(7),
                        new LiteralStringNullable()
                    )
                ),
                new GroupKey(
                    "repeat_string_nullable_by_count",
                    new RepeatStringNullableRow(
                        new LiteralString("abc"),
                        new LiteralIntegerNullable()
                    )
                ),
                new GroupKey(
                    "replaceRegex_string_nullable_by_replacement",
                    new ReplaceRegexStringNullableRow(
                        new LiteralString("abc"),
                        new LiteralStringNullable(),
                        new LiteralString("b+")
                    )
                ),
                new GroupKey(
                    "replaceRegex_string_parameter_pattern",
                    new ReplaceRegexStringRow(
                        new LiteralString("a1"),
                        new LiteralString("b"),
                        new ParamString("pattern")
                    )
                ),
                new GroupKey(
                    "contains_boolean",
                    new ContainsRow(new LiteralString("abcabc"), new LiteralString("abc"))
                ),
                new GroupKey(
                    "contains_boolean_nullable_operand",
                    new ContainsRow(new LiteralStringNullable(), new LiteralString("abc"))
                ),
                new GroupKey(
                    "contains_boolean_nullable_pattern",
                    new ContainsRow(
                        new LiteralString("abcabc"),
                        new LiteralStringNullable()
                    )
                ),
                new GroupKey(
                    "startsWith_boolean",
                    new StartsWithRow(
                        new LiteralString("abcabc"),
                        new LiteralString("abc")
                    )
                ),
                new GroupKey(
                    "startsWith_boolean_nullable_operand",
                    new StartsWithRow(
                        new LiteralStringNullable(),
                        new LiteralString("abc")
                    )
                ),
                new GroupKey(
                    "startsWith_boolean_nullable_pattern",
                    new StartsWithRow(
                        new LiteralString("abcabc"),
                        new LiteralStringNullable()
                    )
                ),
                new GroupKey(
                    "endsWith_boolean",
                    new EndsWithRow(new LiteralString("abcabc"), new LiteralString("abc"))
                ),
                new GroupKey(
                    "endsWith_boolean_nullable_operand",
                    new EndsWithRow(new LiteralStringNullable(), new LiteralString("abc"))
                ),
                new GroupKey(
                    "endsWith_boolean_nullable_pattern",
                    new EndsWithRow(
                        new LiteralString("abcabc"),
                        new LiteralStringNullable()
                    )
                ),
                new GroupKey(
                    "dateAddDays_date",
                    new DateAddDaysDateRow(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralInteger(7)
                    )
                ),
                new GroupKey(
                    "dateAddDays_date_nullable",
                    new DateAddDaysDateNullableRow(
                        new LiteralDateNullable(),
                        new LiteralInteger(7)
                    )
                ),
                new GroupKey(
                    "dateDiffDays_integer",
                    new DateDiffDaysIntegerRow(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDate(new DateOnly(2024, 3, 1))
                    )
                ),
                new GroupKey(
                    "dateDiffDays_integer_nullable",
                    new DateDiffDaysIntegerNullableRow(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDateNullable()
                    )
                ),
                new GroupKey(
                    "timeAddSeconds_time",
                    new TimeAddSecondsTimeRow(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralDecimal(2.5m)
                    )
                ),
                new GroupKey(
                    "timeAddSeconds_time_nullable",
                    new TimeAddSecondsTimeNullableRow(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralIntegerNullable()
                    )
                ),
                new GroupKey(
                    "timeDiffSeconds_decimal",
                    new TimeDiffSecondsDecimalRow(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralTime(new TimeOnly(17, 45, 30, 500))
                    )
                ),
                new GroupKey(
                    "timeDiffSeconds_decimal_nullable",
                    new TimeDiffSecondsDecimalNullableRow(
                        new LiteralTimeNullable(),
                        new LiteralTime(new TimeOnly(8, 30, 0))
                    )
                ),
                new GroupKey(
                    "datetimeAddSeconds_datetime",
                    new DatetimeAddSecondsDatetimeRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDecimal(2.5m)
                    )
                ),
                new GroupKey(
                    "datetimeAddSeconds_datetime_nullable",
                    new DatetimeAddSecondsDatetimeNullableRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralIntegerNullable()
                    )
                ),
                new GroupKey(
                    "datetimeDiffSeconds_decimal",
                    new DatetimeDiffSecondsDecimalRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        )
                    )
                ),
                new GroupKey(
                    "datetimeDiffSeconds_decimal_nullable",
                    new DatetimeDiffSecondsDecimalNullableRow(
                        new LiteralDatetimeNullable(),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        )
                    )
                ),
                new GroupKey(
                    "dateAddMonths_date",
                    new DateAddMonthsDateRow(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralInteger(1)
                    )
                ),
                new GroupKey(
                    "dateAddMonths_date_nullable",
                    new DateAddMonthsDateNullableRow(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralIntegerNullable()
                    )
                ),
                new GroupKey(
                    "datetimeAddMonths_datetime",
                    new DatetimeAddMonthsDatetimeRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 18, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralInteger(1)
                    )
                ),
                new GroupKey(
                    "datetimeAddMonths_datetime_nullable",
                    new DatetimeAddMonthsDatetimeNullableRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 18, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralIntegerNullable()
                    )
                ),
                new GroupKey(
                    "datetimeAddMonths_datetime_in_zone",
                    new DatetimeAddMonthsDatetimeRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralInteger(1),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new GroupKey(
                    "datetimeAddMonths_datetime_nullable_in_zone",
                    new DatetimeAddMonthsDatetimeNullableRow(
                        new LiteralDatetimeNullable(),
                        new LiteralInteger(1),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new GroupKey(
                    "datetimeAddMonths_datetime_nullable_months_in_zone",
                    new DatetimeAddMonthsDatetimeNullableRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralIntegerNullable(),
                        zone: new LiteralString("America/New_York")
                    )
                ),
                new GroupKey(
                    "datetimeAddMonths_datetime_nullable_by_zone",
                    new DatetimeAddMonthsDatetimeNullableRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralInteger(-12),
                        zone: new LiteralStringNullable()
                    )
                ),
                new GroupKey(
                    "dateDiffMonths_integer",
                    new DateDiffMonthsIntegerRow(
                        new LiteralDate(new DateOnly(2023, 2, 28)),
                        new LiteralDate(new DateOnly(2023, 1, 31))
                    )
                ),
                new GroupKey(
                    "dateDiffMonths_integer_nullable",
                    new DateDiffMonthsIntegerNullableRow(
                        new LiteralDateNullable(),
                        new LiteralDate(new DateOnly(2023, 1, 31))
                    )
                ),
                new GroupKey(
                    "datetimeDiffMonths_integer",
                    new DatetimeDiffMonthsIntegerRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 2, 15, 11, 59, 59, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 15, 12, 0, 0, TimeSpan.Zero)
                        )
                    )
                ),
                new GroupKey(
                    "datetimeDiffMonths_integer_nullable",
                    new DatetimeDiffMonthsIntegerNullableRow(
                        new LiteralDatetimeNullable(),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 15, 12, 0, 0, TimeSpan.Zero)
                        )
                    )
                ),
                new GroupKey(
                    "datetimeDiffMonths_integer_in_zone",
                    new DatetimeDiffMonthsIntegerRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new GroupKey(
                    "datetimeDiffMonths_integer_nullable_in_zone",
                    new DatetimeDiffMonthsIntegerNullableRow(
                        new LiteralDatetimeNullable(),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Etc/GMT+5")
                    )
                ),
                new GroupKey(
                    "datetimeDiffMonths_integer_nullable_by_zone",
                    new DatetimeDiffMonthsIntegerNullableRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new GroupKey(
                    "dateFromParts_date",
                    new DateFromPartsDateRow(
                        new LiteralInteger(2024),
                        new LiteralInteger(2),
                        new LiteralInteger(29)
                    )
                ),
                new GroupKey(
                    "dateFromParts_date_nullable_year",
                    new DateFromPartsDateNullableRow(
                        new LiteralIntegerNullable(),
                        new LiteralInteger(2),
                        new LiteralInteger(29)
                    )
                ),
                new GroupKey(
                    "dateFromParts_date_nullable_month",
                    new DateFromPartsDateNullableRow(
                        new LiteralInteger(2024),
                        new LiteralIntegerNullable(),
                        new LiteralInteger(29)
                    )
                ),
                new GroupKey(
                    "dateFromParts_date_nullable_day",
                    new DateFromPartsDateNullableRow(
                        new LiteralInteger(2024),
                        new LiteralInteger(2),
                        new LiteralIntegerNullable()
                    )
                ),
                new GroupKey(
                    "timeFromParts_time",
                    new TimeFromPartsTimeRow(
                        new LiteralInteger(18),
                        new LiteralInteger(30),
                        new LiteralDecimal(15.25m)
                    )
                ),
                new GroupKey(
                    "timeFromParts_time_integer_second",
                    new TimeFromPartsTimeRow(
                        new LiteralInteger(18),
                        new LiteralInteger(30),
                        new LiteralInteger(15)
                    )
                ),
                new GroupKey(
                    "timeFromParts_time_nullable_hour",
                    new TimeFromPartsTimeNullableRow(
                        new LiteralIntegerNullable(),
                        new LiteralInteger(30),
                        new LiteralInteger(15)
                    )
                ),
                new GroupKey(
                    "timeFromParts_time_nullable_minute",
                    new TimeFromPartsTimeNullableRow(
                        new LiteralInteger(18),
                        new LiteralIntegerNullable(),
                        new LiteralInteger(15)
                    )
                ),
                new GroupKey(
                    "timeFromParts_time_nullable_second",
                    new TimeFromPartsTimeNullableRow(
                        new LiteralInteger(18),
                        new LiteralInteger(30),
                        new LiteralDecimalNullable()
                    )
                ),
                new GroupKey(
                    "datetimeFromParts_datetime",
                    new DatetimeFromPartsDatetimeRow(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralTime(new TimeOnly(8, 30, 0))
                    )
                ),
                new GroupKey(
                    "datetimeFromParts_datetime_nullable_date",
                    new DatetimeFromPartsDatetimeNullableRow(
                        new LiteralDateNullable(),
                        new LiteralTime(new TimeOnly(8, 30, 0))
                    )
                ),
                new GroupKey(
                    "datetimeFromParts_datetime_nullable_time",
                    new DatetimeFromPartsDatetimeNullableRow(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralTimeNullable()
                    )
                ),
                new GroupKey(
                    "datetimeFromParts_datetime_in_zone",
                    new DatetimeFromPartsDatetimeRow(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new GroupKey(
                    "datetimeFromParts_datetime_nullable_date_in_zone",
                    new DatetimeFromPartsDatetimeNullableRow(
                        new LiteralDateNullable(),
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        zone: new LiteralString("Etc/GMT-3")
                    )
                ),
                new GroupKey(
                    "datetimeFromParts_datetime_nullable_time_in_zone",
                    new DatetimeFromPartsDatetimeNullableRow(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralTimeNullable(),
                        zone: new LiteralString("America/New_York")
                    )
                ),
                new GroupKey(
                    "datetimeFromParts_datetime_nullable_by_zone",
                    new DatetimeFromPartsDatetimeNullableRow(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        zone: new LiteralStringNullable()
                    )
                ),
                new GroupKey(
                    "year_date",
                    new YearDateIntegerRow(new LiteralDate(new DateOnly(2024, 12, 30)))
                ),
                new GroupKey(
                    "year_date_nullable",
                    new YearDateIntegerNullableRow(new LiteralDateNullable())
                ),
                new GroupKey(
                    "year_datetime",
                    new YearDatetimeIntegerRow(
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
                new GroupKey(
                    "year_datetime_nullable",
                    new YearDatetimeIntegerNullableRow(new LiteralDatetimeNullable())
                ),
                new GroupKey(
                    "year_datetime_in_zone",
                    new YearDatetimeIntegerRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new GroupKey(
                    "year_datetime_nullable_in_zone",
                    new YearDatetimeIntegerNullableRow(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new GroupKey(
                    "year_datetime_nullable_by_zone",
                    new YearDatetimeIntegerNullableRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new GroupKey(
                    "quarter_date",
                    new QuarterDateIntegerRow(new LiteralDate(new DateOnly(2024, 12, 30)))
                ),
                new GroupKey(
                    "quarter_date_nullable",
                    new QuarterDateIntegerNullableRow(new LiteralDateNullable())
                ),
                new GroupKey(
                    "quarter_datetime",
                    new QuarterDatetimeIntegerRow(
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
                new GroupKey(
                    "quarter_datetime_nullable",
                    new QuarterDatetimeIntegerNullableRow(new LiteralDatetimeNullable())
                ),
                new GroupKey(
                    "quarter_datetime_in_zone",
                    new QuarterDatetimeIntegerRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new GroupKey(
                    "quarter_datetime_nullable_in_zone",
                    new QuarterDatetimeIntegerNullableRow(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new GroupKey(
                    "quarter_datetime_nullable_by_zone",
                    new QuarterDatetimeIntegerNullableRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new GroupKey(
                    "month_date",
                    new MonthDateIntegerRow(new LiteralDate(new DateOnly(2024, 12, 30)))
                ),
                new GroupKey(
                    "month_date_nullable",
                    new MonthDateIntegerNullableRow(new LiteralDateNullable())
                ),
                new GroupKey(
                    "month_datetime",
                    new MonthDatetimeIntegerRow(
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
                new GroupKey(
                    "month_datetime_nullable",
                    new MonthDatetimeIntegerNullableRow(new LiteralDatetimeNullable())
                ),
                new GroupKey(
                    "month_datetime_in_zone",
                    new MonthDatetimeIntegerRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new GroupKey(
                    "month_datetime_nullable_in_zone",
                    new MonthDatetimeIntegerNullableRow(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new GroupKey(
                    "month_datetime_nullable_by_zone",
                    new MonthDatetimeIntegerNullableRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new GroupKey(
                    "day_date",
                    new DayDateIntegerRow(new LiteralDate(new DateOnly(2024, 12, 30)))
                ),
                new GroupKey(
                    "day_date_nullable",
                    new DayDateIntegerNullableRow(new LiteralDateNullable())
                ),
                new GroupKey(
                    "day_datetime",
                    new DayDatetimeIntegerRow(
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
                new GroupKey(
                    "day_datetime_nullable",
                    new DayDatetimeIntegerNullableRow(new LiteralDatetimeNullable())
                ),
                new GroupKey(
                    "day_datetime_in_zone",
                    new DayDatetimeIntegerRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new GroupKey(
                    "day_datetime_nullable_in_zone",
                    new DayDatetimeIntegerNullableRow(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new GroupKey(
                    "day_datetime_nullable_by_zone",
                    new DayDatetimeIntegerNullableRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new GroupKey(
                    "dayOfWeek_date",
                    new DayOfWeekDateIntegerRow(
                        new LiteralDate(new DateOnly(2024, 12, 30))
                    )
                ),
                new GroupKey(
                    "dayOfWeek_date_nullable",
                    new DayOfWeekDateIntegerNullableRow(new LiteralDateNullable())
                ),
                new GroupKey(
                    "dayOfWeek_datetime",
                    new DayOfWeekDatetimeIntegerRow(
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
                new GroupKey(
                    "dayOfWeek_datetime_nullable",
                    new DayOfWeekDatetimeIntegerNullableRow(new LiteralDatetimeNullable())
                ),
                new GroupKey(
                    "dayOfWeek_datetime_in_zone",
                    new DayOfWeekDatetimeIntegerRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new GroupKey(
                    "dayOfWeek_datetime_nullable_in_zone",
                    new DayOfWeekDatetimeIntegerNullableRow(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new GroupKey(
                    "dayOfWeek_datetime_nullable_by_zone",
                    new DayOfWeekDatetimeIntegerNullableRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new GroupKey(
                    "dayOfYear_date",
                    new DayOfYearDateIntegerRow(
                        new LiteralDate(new DateOnly(2024, 12, 30))
                    )
                ),
                new GroupKey(
                    "dayOfYear_date_nullable",
                    new DayOfYearDateIntegerNullableRow(new LiteralDateNullable())
                ),
                new GroupKey(
                    "dayOfYear_datetime",
                    new DayOfYearDatetimeIntegerRow(
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
                new GroupKey(
                    "dayOfYear_datetime_nullable",
                    new DayOfYearDatetimeIntegerNullableRow(new LiteralDatetimeNullable())
                ),
                new GroupKey(
                    "dayOfYear_datetime_in_zone",
                    new DayOfYearDatetimeIntegerRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new GroupKey(
                    "dayOfYear_datetime_nullable_in_zone",
                    new DayOfYearDatetimeIntegerNullableRow(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new GroupKey(
                    "dayOfYear_datetime_nullable_by_zone",
                    new DayOfYearDatetimeIntegerNullableRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new GroupKey(
                    "week_date",
                    new WeekDateIntegerRow(new LiteralDate(new DateOnly(2024, 12, 30)))
                ),
                new GroupKey(
                    "week_date_nullable",
                    new WeekDateIntegerNullableRow(new LiteralDateNullable())
                ),
                new GroupKey(
                    "week_datetime",
                    new WeekDatetimeIntegerRow(
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
                new GroupKey(
                    "week_datetime_nullable",
                    new WeekDatetimeIntegerNullableRow(new LiteralDatetimeNullable())
                ),
                new GroupKey(
                    "week_datetime_in_zone",
                    new WeekDatetimeIntegerRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new GroupKey(
                    "week_datetime_nullable_in_zone",
                    new WeekDatetimeIntegerNullableRow(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new GroupKey(
                    "week_datetime_nullable_by_zone",
                    new WeekDatetimeIntegerNullableRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new GroupKey(
                    "weekYear_date",
                    new WeekYearDateIntegerRow(
                        new LiteralDate(new DateOnly(2024, 12, 30))
                    )
                ),
                new GroupKey(
                    "weekYear_date_nullable",
                    new WeekYearDateIntegerNullableRow(new LiteralDateNullable())
                ),
                new GroupKey(
                    "weekYear_datetime",
                    new WeekYearDatetimeIntegerRow(
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
                new GroupKey(
                    "weekYear_datetime_nullable",
                    new WeekYearDatetimeIntegerNullableRow(new LiteralDatetimeNullable())
                ),
                new GroupKey(
                    "weekYear_datetime_in_zone",
                    new WeekYearDatetimeIntegerRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new GroupKey(
                    "weekYear_datetime_nullable_in_zone",
                    new WeekYearDatetimeIntegerNullableRow(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new GroupKey(
                    "weekYear_datetime_nullable_by_zone",
                    new WeekYearDatetimeIntegerNullableRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new GroupKey(
                    "hour_time",
                    new HourTimeIntegerRow(new LiteralTime(new TimeOnly(18, 30, 15, 250)))
                ),
                new GroupKey(
                    "hour_time_nullable",
                    new HourTimeIntegerNullableRow(new LiteralTimeNullable())
                ),
                new GroupKey(
                    "hour_datetime",
                    new HourDatetimeIntegerRow(
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
                new GroupKey(
                    "hour_datetime_nullable",
                    new HourDatetimeIntegerNullableRow(new LiteralDatetimeNullable())
                ),
                new GroupKey(
                    "hour_datetime_in_zone",
                    new HourDatetimeIntegerRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new GroupKey(
                    "hour_datetime_nullable_in_zone",
                    new HourDatetimeIntegerNullableRow(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new GroupKey(
                    "hour_datetime_nullable_by_zone",
                    new HourDatetimeIntegerNullableRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new GroupKey(
                    "minute_time",
                    new MinuteTimeIntegerRow(
                        new LiteralTime(new TimeOnly(18, 30, 15, 250))
                    )
                ),
                new GroupKey(
                    "minute_time_nullable",
                    new MinuteTimeIntegerNullableRow(new LiteralTimeNullable())
                ),
                new GroupKey(
                    "minute_datetime",
                    new MinuteDatetimeIntegerRow(
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
                new GroupKey(
                    "minute_datetime_nullable",
                    new MinuteDatetimeIntegerNullableRow(new LiteralDatetimeNullable())
                ),
                new GroupKey(
                    "minute_datetime_in_zone",
                    new MinuteDatetimeIntegerRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new GroupKey(
                    "minute_datetime_nullable_in_zone",
                    new MinuteDatetimeIntegerNullableRow(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new GroupKey(
                    "minute_datetime_nullable_by_zone",
                    new MinuteDatetimeIntegerNullableRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new GroupKey(
                    "second_time",
                    new SecondTimeIntegerRow(
                        new LiteralTime(new TimeOnly(18, 30, 15, 250))
                    )
                ),
                new GroupKey(
                    "second_time_nullable",
                    new SecondTimeIntegerNullableRow(new LiteralTimeNullable())
                ),
                new GroupKey(
                    "second_datetime",
                    new SecondDatetimeIntegerRow(
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
                new GroupKey(
                    "second_datetime_nullable",
                    new SecondDatetimeIntegerNullableRow(new LiteralDatetimeNullable())
                ),
                new GroupKey(
                    "second_datetime_in_zone",
                    new SecondDatetimeIntegerRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new GroupKey(
                    "second_datetime_nullable_in_zone",
                    new SecondDatetimeIntegerNullableRow(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new GroupKey(
                    "second_datetime_nullable_by_zone",
                    new SecondDatetimeIntegerNullableRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new GroupKey(
                    "microsecond_time",
                    new MicrosecondTimeIntegerRow(
                        new LiteralTime(new TimeOnly(18, 30, 15, 250))
                    )
                ),
                new GroupKey(
                    "microsecond_time_nullable",
                    new MicrosecondTimeIntegerNullableRow(new LiteralTimeNullable())
                ),
                new GroupKey(
                    "microsecond_datetime",
                    new MicrosecondDatetimeIntegerRow(
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
                new GroupKey(
                    "microsecond_datetime_nullable",
                    new MicrosecondDatetimeIntegerNullableRow(
                        new LiteralDatetimeNullable()
                    )
                ),
                new GroupKey(
                    "startOfYear_date",
                    new StartOfYearDateRow(new LiteralDate(new DateOnly(2024, 8, 15)))
                ),
                new GroupKey(
                    "startOfYear_date_nullable",
                    new StartOfYearDateNullableRow(new LiteralDateNullable())
                ),
                new GroupKey(
                    "startOfYear_datetime",
                    new StartOfYearDatetimeRow(
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
                new GroupKey(
                    "startOfYear_datetime_nullable",
                    new StartOfYearDatetimeNullableRow(new LiteralDatetimeNullable())
                ),
                new GroupKey(
                    "startOfYear_datetime_in_zone",
                    new StartOfYearDatetimeRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new GroupKey(
                    "startOfYear_datetime_nullable_in_zone",
                    new StartOfYearDatetimeNullableRow(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new GroupKey(
                    "startOfYear_datetime_nullable_by_zone",
                    new StartOfYearDatetimeNullableRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new GroupKey(
                    "startOfQuarter_date",
                    new StartOfQuarterDateRow(new LiteralDate(new DateOnly(2024, 8, 15)))
                ),
                new GroupKey(
                    "startOfQuarter_date_nullable",
                    new StartOfQuarterDateNullableRow(new LiteralDateNullable())
                ),
                new GroupKey(
                    "startOfQuarter_datetime",
                    new StartOfQuarterDatetimeRow(
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
                new GroupKey(
                    "startOfQuarter_datetime_nullable",
                    new StartOfQuarterDatetimeNullableRow(new LiteralDatetimeNullable())
                ),
                new GroupKey(
                    "startOfQuarter_datetime_in_zone",
                    new StartOfQuarterDatetimeRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new GroupKey(
                    "startOfQuarter_datetime_nullable_in_zone",
                    new StartOfQuarterDatetimeNullableRow(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new GroupKey(
                    "startOfQuarter_datetime_nullable_by_zone",
                    new StartOfQuarterDatetimeNullableRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new GroupKey(
                    "startOfMonth_date",
                    new StartOfMonthDateRow(new LiteralDate(new DateOnly(2024, 8, 15)))
                ),
                new GroupKey(
                    "startOfMonth_date_nullable",
                    new StartOfMonthDateNullableRow(new LiteralDateNullable())
                ),
                new GroupKey(
                    "startOfMonth_datetime",
                    new StartOfMonthDatetimeRow(
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
                new GroupKey(
                    "startOfMonth_datetime_nullable",
                    new StartOfMonthDatetimeNullableRow(new LiteralDatetimeNullable())
                ),
                new GroupKey(
                    "startOfMonth_datetime_in_zone",
                    new StartOfMonthDatetimeRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new GroupKey(
                    "startOfMonth_datetime_nullable_in_zone",
                    new StartOfMonthDatetimeNullableRow(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new GroupKey(
                    "startOfMonth_datetime_nullable_by_zone",
                    new StartOfMonthDatetimeNullableRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new GroupKey(
                    "startOfWeek_date",
                    new StartOfWeekDateRow(new LiteralDate(new DateOnly(2024, 8, 15)))
                ),
                new GroupKey(
                    "startOfWeek_date_nullable",
                    new StartOfWeekDateNullableRow(new LiteralDateNullable())
                ),
                new GroupKey(
                    "startOfWeek_datetime",
                    new StartOfWeekDatetimeRow(
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
                new GroupKey(
                    "startOfWeek_datetime_nullable",
                    new StartOfWeekDatetimeNullableRow(new LiteralDatetimeNullable())
                ),
                new GroupKey(
                    "startOfWeek_datetime_in_zone",
                    new StartOfWeekDatetimeRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new GroupKey(
                    "startOfWeek_datetime_nullable_in_zone",
                    new StartOfWeekDatetimeNullableRow(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new GroupKey(
                    "startOfWeek_datetime_nullable_by_zone",
                    new StartOfWeekDatetimeNullableRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new GroupKey(
                    "startOfDay_datetime",
                    new StartOfDayDatetimeRow(
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
                new GroupKey(
                    "startOfDay_datetime_nullable",
                    new StartOfDayDatetimeNullableRow(new LiteralDatetimeNullable())
                ),
                new GroupKey(
                    "startOfDay_datetime_in_zone",
                    new StartOfDayDatetimeRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new GroupKey(
                    "startOfDay_datetime_nullable_in_zone",
                    new StartOfDayDatetimeNullableRow(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new GroupKey(
                    "startOfDay_datetime_nullable_by_zone",
                    new StartOfDayDatetimeNullableRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new GroupKey(
                    "startOfHour_time",
                    new StartOfHourTimeRow(new LiteralTime(new TimeOnly(18, 30, 15, 250)))
                ),
                new GroupKey(
                    "startOfHour_time_nullable",
                    new StartOfHourTimeNullableRow(new LiteralTimeNullable())
                ),
                new GroupKey(
                    "startOfHour_datetime",
                    new StartOfHourDatetimeRow(
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
                new GroupKey(
                    "startOfHour_datetime_nullable",
                    new StartOfHourDatetimeNullableRow(new LiteralDatetimeNullable())
                ),
                new GroupKey(
                    "startOfHour_datetime_in_zone",
                    new StartOfHourDatetimeRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new GroupKey(
                    "startOfHour_datetime_nullable_in_zone",
                    new StartOfHourDatetimeNullableRow(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new GroupKey(
                    "startOfHour_datetime_nullable_by_zone",
                    new StartOfHourDatetimeNullableRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new GroupKey(
                    "startOfMinute_time",
                    new StartOfMinuteTimeRow(
                        new LiteralTime(new TimeOnly(18, 30, 15, 250))
                    )
                ),
                new GroupKey(
                    "startOfMinute_time_nullable",
                    new StartOfMinuteTimeNullableRow(new LiteralTimeNullable())
                ),
                new GroupKey(
                    "startOfMinute_datetime",
                    new StartOfMinuteDatetimeRow(
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
                new GroupKey(
                    "startOfMinute_datetime_nullable",
                    new StartOfMinuteDatetimeNullableRow(new LiteralDatetimeNullable())
                ),
                new GroupKey(
                    "startOfMinute_datetime_in_zone",
                    new StartOfMinuteDatetimeRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new GroupKey(
                    "startOfMinute_datetime_nullable_in_zone",
                    new StartOfMinuteDatetimeNullableRow(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new GroupKey(
                    "startOfMinute_datetime_nullable_by_zone",
                    new StartOfMinuteDatetimeNullableRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new GroupKey(
                    "startOfSecond_time",
                    new StartOfSecondTimeRow(
                        new LiteralTime(new TimeOnly(18, 30, 15, 250))
                    )
                ),
                new GroupKey(
                    "startOfSecond_time_nullable",
                    new StartOfSecondTimeNullableRow(new LiteralTimeNullable())
                ),
                new GroupKey(
                    "startOfSecond_datetime",
                    new StartOfSecondDatetimeRow(
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
                new GroupKey(
                    "startOfSecond_datetime_nullable",
                    new StartOfSecondDatetimeNullableRow(new LiteralDatetimeNullable())
                ),
                new GroupKey(
                    "asString_string_of_integer",
                    new AsStringDecimalStringRow(new LiteralInteger(7))
                ),
                new GroupKey(
                    "asString_string_nullable_of_integer",
                    new AsStringDecimalStringNullableRow(new LiteralIntegerNullable())
                ),
                new GroupKey(
                    "asString_string_of_decimal",
                    new AsStringDecimalStringRow(new LiteralDecimal(2.5m))
                ),
                new GroupKey(
                    "asString_string_nullable_of_decimal",
                    new AsStringDecimalStringNullableRow(new LiteralDecimalNullable())
                ),
                new GroupKey(
                    "asString_string_of_boolean",
                    new AsStringBooleanStringRow(new LiteralBoolean(true))
                ),
                new GroupKey(
                    "asString_string_nullable_of_boolean",
                    new AsStringBooleanStringNullableRow(new LiteralBooleanNullable())
                ),
                new GroupKey(
                    "asString_string_of_date",
                    new AsStringDateStringRow(new LiteralDate(new DateOnly(2024, 1, 31)))
                ),
                new GroupKey(
                    "asString_string_nullable_of_date",
                    new AsStringDateStringNullableRow(new LiteralDateNullable())
                ),
                new GroupKey(
                    "asString_string_of_time",
                    new AsStringTimeStringRow(new LiteralTime(new TimeOnly(8, 30, 0)))
                ),
                new GroupKey(
                    "asString_string_nullable_of_time",
                    new AsStringTimeStringNullableRow(new LiteralTimeNullable())
                ),
                new GroupKey(
                    "asString_string_of_datetime",
                    new AsStringDatetimeStringRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        )
                    )
                ),
                new GroupKey(
                    "asString_string_nullable_of_datetime",
                    new AsStringDatetimeStringNullableRow(new LiteralDatetimeNullable())
                ),
                new GroupKey(
                    "asString_string_of_uuid",
                    new AsStringUuidStringRow(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8]))
                    )
                ),
                new GroupKey(
                    "asString_string_nullable_of_uuid",
                    new AsStringUuidStringNullableRow(new LiteralUuidNullable())
                ),
                new GroupKey(
                    "asDate_date",
                    new AsDateDatetimeDateRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        )
                    )
                ),
                new GroupKey(
                    "asDate_date_nullable",
                    new AsDateDatetimeDateNullableRow(new LiteralDatetimeNullable())
                ),
                new GroupKey(
                    "asTime_time",
                    new AsTimeDatetimeTimeRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        )
                    )
                ),
                new GroupKey(
                    "asTime_time_nullable",
                    new AsTimeDatetimeTimeNullableRow(new LiteralDatetimeNullable())
                ),
                new GroupKey(
                    "asDate_date_in_zone",
                    new AsDateDatetimeDateRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new GroupKey(
                    "asDate_date_nullable_in_zone",
                    new AsDateDatetimeDateNullableRow(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new GroupKey(
                    "asDate_date_nullable_by_zone",
                    new AsDateDatetimeDateNullableRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new GroupKey(
                    "asDate_date_nullable_in_nullable_zone",
                    new AsDateDatetimeDateNullableRow(
                        new LiteralDatetimeNullable(),
                        zone: new ParamStringNullable("zone")
                    )
                ),
                new GroupKey(
                    "asTime_time_in_zone",
                    new AsTimeDatetimeTimeRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new GroupKey(
                    "asTime_time_nullable_in_zone",
                    new AsTimeDatetimeTimeNullableRow(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new GroupKey(
                    "asTime_time_nullable_by_zone",
                    new AsTimeDatetimeTimeNullableRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new GroupKey(
                    "asTime_time_nullable_in_nullable_zone",
                    new AsTimeDatetimeTimeNullableRow(
                        new LiteralDatetimeNullable(),
                        zone: new ParamStringNullable("zone")
                    )
                ),
                new GroupKey(
                    "asDatetime_datetime",
                    new AsDatetimeDateDatetimeRow(
                        new LiteralDate(new DateOnly(2024, 1, 31))
                    )
                ),
                new GroupKey(
                    "asDatetime_datetime_nullable",
                    new AsDatetimeDateDatetimeNullableRow(new LiteralDateNullable())
                ),
                new GroupKey(
                    "asDatetime_datetime_in_zone",
                    new AsDatetimeDateDatetimeRow(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new GroupKey(
                    "asDatetime_datetime_nullable_in_zone",
                    new AsDatetimeDateDatetimeNullableRow(
                        new LiteralDateNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new GroupKey(
                    "asDatetime_datetime_nullable_by_zone",
                    new AsDatetimeDateDatetimeNullableRow(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        zone: new LiteralStringNullable()
                    )
                ),
                new GroupKey(
                    "asInteger_integer_of_string",
                    new AsIntegerIntegerRow(new LiteralString("7"))
                ),
                new GroupKey(
                    "asInteger_integer_nullable_of_string",
                    new AsIntegerIntegerNullableRow(new LiteralStringNullable())
                ),
                new GroupKey(
                    "tryAsInteger_integer_nullable",
                    new TryAsIntegerRow(new LiteralString("7"))
                ),
                new GroupKey(
                    "tryAsInteger_integer_nullable_of_null",
                    new TryAsIntegerRow(new LiteralStringNullable())
                ),
                new GroupKey(
                    "asDecimal_decimal_of_string",
                    new AsDecimalStringDecimalRow(new LiteralString("2.5"))
                ),
                new GroupKey(
                    "asDecimal_decimal_nullable_of_string",
                    new AsDecimalStringDecimalNullableRow(new LiteralStringNullable())
                ),
                new GroupKey(
                    "asDecimal_decimal_of_integer",
                    new AsDecimalIntegerDecimalRow(new LiteralInteger(7))
                ),
                new GroupKey(
                    "asDecimal_decimal_nullable_of_integer",
                    new AsDecimalIntegerDecimalNullableRow(new LiteralIntegerNullable())
                ),
                new GroupKey(
                    "asNullable_integer_nullable",
                    new AsNullableIntegerNullableRow(new LiteralInteger(7))
                ),
                new GroupKey(
                    "asNullable_decimal_nullable",
                    new AsNullableDecimalNullableRow(new LiteralDecimal(2.5m))
                ),
                new GroupKey(
                    "asNullable_string_nullable",
                    new AsNullableStringNullableRow(new LiteralString("a"))
                ),
                new GroupKey(
                    "asNullable_boolean_nullable",
                    new AsNullableBooleanNullableRow(new LiteralBoolean(true))
                ),
                new GroupKey(
                    "asNullable_date_nullable",
                    new AsNullableDateNullableRow(
                        new LiteralDate(new DateOnly(2024, 1, 31))
                    )
                ),
                new GroupKey(
                    "asNullable_time_nullable",
                    new AsNullableTimeNullableRow(new LiteralTime(new TimeOnly(8, 30, 0)))
                ),
                new GroupKey(
                    "asNullable_datetime_nullable",
                    new AsNullableDatetimeNullableRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        )
                    )
                ),
                new GroupKey(
                    "asNullable_uuid_nullable",
                    new AsNullableUuidNullableRow(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8]))
                    )
                ),
                new GroupKey(
                    "tryAsDecimal_decimal_nullable",
                    new TryAsDecimalRow(new LiteralString("2.5"))
                ),
                new GroupKey(
                    "tryAsDecimal_decimal_nullable_of_null",
                    new TryAsDecimalRow(new LiteralStringNullable())
                ),
                new GroupKey(
                    "asBoolean_boolean_of_string",
                    new AsBooleanBooleanRow(new LiteralString("true"))
                ),
                new GroupKey(
                    "asBoolean_boolean_nullable_of_string",
                    new AsBooleanBooleanNullableRow(new LiteralStringNullable())
                ),
                new GroupKey(
                    "tryAsBoolean_boolean_nullable",
                    new TryAsBooleanRow(new LiteralString("true"))
                ),
                new GroupKey(
                    "tryAsBoolean_boolean_nullable_of_null",
                    new TryAsBooleanRow(new LiteralStringNullable())
                ),
                new GroupKey(
                    "asDate_date_of_string",
                    new AsDateStringDateRow(new LiteralString("2024-01-31"))
                ),
                new GroupKey(
                    "asDate_date_nullable_of_string",
                    new AsDateStringDateNullableRow(new LiteralStringNullable())
                ),
                new GroupKey(
                    "tryAsDate_date_nullable",
                    new TryAsDateRow(new LiteralString("2024-01-31"))
                ),
                new GroupKey(
                    "tryAsDate_date_nullable_of_null",
                    new TryAsDateRow(new LiteralStringNullable())
                ),
                new GroupKey(
                    "asTime_time_of_string",
                    new AsTimeStringTimeRow(new LiteralString("08:30:00"))
                ),
                new GroupKey(
                    "asTime_time_nullable_of_string",
                    new AsTimeStringTimeNullableRow(new LiteralStringNullable())
                ),
                new GroupKey(
                    "tryAsTime_time_nullable",
                    new TryAsTimeRow(new LiteralString("08:30:00"))
                ),
                new GroupKey(
                    "tryAsTime_time_nullable_of_null",
                    new TryAsTimeRow(new LiteralStringNullable())
                ),
                new GroupKey(
                    "asDatetime_datetime_of_string",
                    new AsDatetimeStringDatetimeRow(
                        new LiteralString("2024-01-31T08:30:00Z")
                    )
                ),
                new GroupKey(
                    "asDatetime_datetime_nullable_of_string",
                    new AsDatetimeStringDatetimeNullableRow(new LiteralStringNullable())
                ),
                new GroupKey(
                    "tryAsDatetime_datetime_nullable",
                    new TryAsDatetimeRow(new LiteralString("2024-01-31T08:30:00Z"))
                ),
                new GroupKey(
                    "tryAsDatetime_datetime_nullable_of_null",
                    new TryAsDatetimeRow(new LiteralStringNullable())
                ),
                new GroupKey(
                    "asUuid_uuid_of_string",
                    new AsUuidUuidRow(
                        new LiteralString("3f2a6c1e-8b4d-4e2a-9c1f-1a2b3c4d5e6f")
                    )
                ),
                new GroupKey(
                    "asUuid_uuid_nullable_of_string",
                    new AsUuidUuidNullableRow(new LiteralStringNullable())
                ),
                new GroupKey(
                    "tryAsUuid_uuid_nullable",
                    new TryAsUuidRow(
                        new LiteralString("3f2a6c1e-8b4d-4e2a-9c1f-1a2b3c4d5e6f")
                    )
                ),
                new GroupKey(
                    "tryAsUuid_uuid_nullable_of_null",
                    new TryAsUuidRow(new LiteralStringNullable())
                ),
                new GroupKey(
                    "and_boolean",
                    new AndRow([new LiteralBoolean(true), new LiteralBoolean(false)])
                ),
                new GroupKey(
                    "or_boolean",
                    new OrRow([new LiteralBoolean(true), new LiteralBoolean(false)])
                ),
                new GroupKey("not_boolean", new NotRow(new LiteralBoolean(true))),
                new GroupKey(
                    "equal_boolean_decimal",
                    new EqualDecimalRow(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m)
                    )
                ),
                new GroupKey(
                    "equal_boolean_string",
                    new EqualStringRow(new LiteralString("a"), new LiteralString("b"))
                ),
                new GroupKey(
                    "equal_boolean_boolean",
                    new EqualBooleanRow(
                        new LiteralBoolean(true),
                        new LiteralBoolean(false)
                    )
                ),
                new GroupKey(
                    "equal_boolean_date",
                    new EqualDateRow(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDate(new DateOnly(2024, 3, 1))
                    )
                ),
                new GroupKey(
                    "equal_boolean_time",
                    new EqualTimeRow(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralTime(new TimeOnly(17, 45, 30, 500))
                    )
                ),
                new GroupKey(
                    "equal_boolean_datetime",
                    new EqualDatetimeRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        )
                    )
                ),
                new GroupKey(
                    "equal_boolean_uuid",
                    new EqualUuidRow(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralUuid(new Guid(2, 0, 0, new byte[8]))
                    )
                ),
                new GroupKey(
                    "notEqual_boolean_decimal",
                    new NotEqualDecimalRow(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m)
                    )
                ),
                new GroupKey(
                    "notEqual_boolean_string",
                    new NotEqualStringRow(new LiteralString("a"), new LiteralString("b"))
                ),
                new GroupKey(
                    "notEqual_boolean_boolean",
                    new NotEqualBooleanRow(
                        new LiteralBoolean(true),
                        new LiteralBoolean(false)
                    )
                ),
                new GroupKey(
                    "notEqual_boolean_date",
                    new NotEqualDateRow(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDate(new DateOnly(2024, 3, 1))
                    )
                ),
                new GroupKey(
                    "notEqual_boolean_time",
                    new NotEqualTimeRow(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralTime(new TimeOnly(17, 45, 30, 500))
                    )
                ),
                new GroupKey(
                    "notEqual_boolean_datetime",
                    new NotEqualDatetimeRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        )
                    )
                ),
                new GroupKey(
                    "notEqual_boolean_uuid",
                    new NotEqualUuidRow(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralUuid(new Guid(2, 0, 0, new byte[8]))
                    )
                ),
                new GroupKey(
                    "greaterThan_boolean_decimal",
                    new GreaterThanDecimalRow(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m)
                    )
                ),
                new GroupKey(
                    "greaterThan_boolean_string",
                    new GreaterThanStringRow(
                        new LiteralString("a"),
                        new LiteralString("b")
                    )
                ),
                new GroupKey(
                    "greaterThan_boolean_boolean",
                    new GreaterThanBooleanRow(
                        new LiteralBoolean(true),
                        new LiteralBoolean(false)
                    )
                ),
                new GroupKey(
                    "greaterThan_boolean_date",
                    new GreaterThanDateRow(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDate(new DateOnly(2024, 3, 1))
                    )
                ),
                new GroupKey(
                    "greaterThan_boolean_time",
                    new GreaterThanTimeRow(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralTime(new TimeOnly(17, 45, 30, 500))
                    )
                ),
                new GroupKey(
                    "greaterThan_boolean_datetime",
                    new GreaterThanDatetimeRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        )
                    )
                ),
                new GroupKey(
                    "greaterThan_boolean_uuid",
                    new GreaterThanUuidRow(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralUuid(new Guid(2, 0, 0, new byte[8]))
                    )
                ),
                new GroupKey(
                    "lessThan_boolean_decimal",
                    new LessThanDecimalRow(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m)
                    )
                ),
                new GroupKey(
                    "lessThan_boolean_string",
                    new LessThanStringRow(new LiteralString("a"), new LiteralString("b"))
                ),
                new GroupKey(
                    "lessThan_boolean_boolean",
                    new LessThanBooleanRow(
                        new LiteralBoolean(true),
                        new LiteralBoolean(false)
                    )
                ),
                new GroupKey(
                    "lessThan_boolean_date",
                    new LessThanDateRow(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDate(new DateOnly(2024, 3, 1))
                    )
                ),
                new GroupKey(
                    "lessThan_boolean_time",
                    new LessThanTimeRow(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralTime(new TimeOnly(17, 45, 30, 500))
                    )
                ),
                new GroupKey(
                    "lessThan_boolean_datetime",
                    new LessThanDatetimeRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        )
                    )
                ),
                new GroupKey(
                    "lessThan_boolean_uuid",
                    new LessThanUuidRow(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralUuid(new Guid(2, 0, 0, new byte[8]))
                    )
                ),
                new GroupKey(
                    "greaterThanOrEqual_boolean_decimal",
                    new GreaterThanOrEqualDecimalRow(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m)
                    )
                ),
                new GroupKey(
                    "greaterThanOrEqual_boolean_string",
                    new GreaterThanOrEqualStringRow(
                        new LiteralString("a"),
                        new LiteralString("b")
                    )
                ),
                new GroupKey(
                    "greaterThanOrEqual_boolean_boolean",
                    new GreaterThanOrEqualBooleanRow(
                        new LiteralBoolean(true),
                        new LiteralBoolean(false)
                    )
                ),
                new GroupKey(
                    "greaterThanOrEqual_boolean_date",
                    new GreaterThanOrEqualDateRow(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDate(new DateOnly(2024, 3, 1))
                    )
                ),
                new GroupKey(
                    "greaterThanOrEqual_boolean_time",
                    new GreaterThanOrEqualTimeRow(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralTime(new TimeOnly(17, 45, 30, 500))
                    )
                ),
                new GroupKey(
                    "greaterThanOrEqual_boolean_datetime",
                    new GreaterThanOrEqualDatetimeRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        )
                    )
                ),
                new GroupKey(
                    "greaterThanOrEqual_boolean_uuid",
                    new GreaterThanOrEqualUuidRow(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralUuid(new Guid(2, 0, 0, new byte[8]))
                    )
                ),
                new GroupKey(
                    "lessThanOrEqual_boolean_decimal",
                    new LessThanOrEqualDecimalRow(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m)
                    )
                ),
                new GroupKey(
                    "lessThanOrEqual_boolean_string",
                    new LessThanOrEqualStringRow(
                        new LiteralString("a"),
                        new LiteralString("b")
                    )
                ),
                new GroupKey(
                    "lessThanOrEqual_boolean_boolean",
                    new LessThanOrEqualBooleanRow(
                        new LiteralBoolean(true),
                        new LiteralBoolean(false)
                    )
                ),
                new GroupKey(
                    "lessThanOrEqual_boolean_date",
                    new LessThanOrEqualDateRow(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDate(new DateOnly(2024, 3, 1))
                    )
                ),
                new GroupKey(
                    "lessThanOrEqual_boolean_time",
                    new LessThanOrEqualTimeRow(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralTime(new TimeOnly(17, 45, 30, 500))
                    )
                ),
                new GroupKey(
                    "lessThanOrEqual_boolean_datetime",
                    new LessThanOrEqualDatetimeRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        )
                    )
                ),
                new GroupKey(
                    "lessThanOrEqual_boolean_uuid",
                    new LessThanOrEqualUuidRow(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralUuid(new Guid(2, 0, 0, new byte[8]))
                    )
                ),
                new GroupKey(
                    "in_boolean_decimal",
                    new InDecimalRow(
                        new LiteralDecimal(2.5m),
                        new ListLiteralDecimal([2.5m, 0.1m])
                    )
                ),
                new GroupKey(
                    "in_boolean_string",
                    new InStringRow(
                        new LiteralString("a"),
                        new ListLiteralString(["a", "b"])
                    )
                ),
                new GroupKey(
                    "in_boolean_boolean",
                    new InBooleanRow(
                        new LiteralBoolean(true),
                        new ListLiteralBoolean([true, false])
                    )
                ),
                new GroupKey(
                    "in_boolean_date",
                    new InDateRow(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new ListLiteralDate([
                            new DateOnly(2024, 1, 31),
                            new DateOnly(2024, 3, 1),
                        ])
                    )
                ),
                new GroupKey(
                    "in_boolean_time",
                    new InTimeRow(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new ListLiteralTime([
                            new TimeOnly(8, 30, 0),
                            new TimeOnly(17, 45, 30, 500),
                        ])
                    )
                ),
                new GroupKey(
                    "in_boolean_datetime",
                    new InDatetimeRow(
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
                new GroupKey(
                    "in_boolean_uuid",
                    new InUuidRow(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new ListLiteralUuid([
                            new Guid(1, 0, 0, new byte[8]),
                            new Guid(2, 0, 0, new byte[8]),
                        ])
                    )
                ),
                new GroupKey(
                    "if_integer",
                    new IfIntegerRow(
                        new LiteralInteger(7),
                        new LiteralInteger(-3),
                        new LiteralBoolean(true)
                    )
                ),
                new GroupKey(
                    "if_integer_nullable",
                    new IfIntegerNullableRow(
                        new LiteralIntegerNullable(),
                        new LiteralInteger(7),
                        new LiteralBoolean(true)
                    )
                ),
                new GroupKey(
                    "coalesce_integer",
                    new CoalesceIntegerRow([
                        new LiteralIntegerNullable(),
                        new LiteralInteger(7),
                    ])
                ),
                new GroupKey(
                    "coalesce_integer_nullable",
                    new CoalesceIntegerNullableRow([
                        new LiteralIntegerNullable(),
                        new LiteralIntegerNullable(),
                    ])
                ),
                new GroupKey(
                    "if_decimal",
                    new IfDecimalRow(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m),
                        new LiteralBoolean(true)
                    )
                ),
                new GroupKey(
                    "if_decimal_nullable",
                    new IfDecimalNullableRow(
                        new LiteralDecimalNullable(),
                        new LiteralDecimal(2.5m),
                        new LiteralBoolean(true)
                    )
                ),
                new GroupKey(
                    "coalesce_decimal",
                    new CoalesceDecimalRow([
                        new LiteralDecimalNullable(),
                        new LiteralDecimal(2.5m),
                    ])
                ),
                new GroupKey(
                    "coalesce_decimal_nullable",
                    new CoalesceDecimalNullableRow([
                        new LiteralDecimalNullable(),
                        new LiteralDecimalNullable(),
                    ])
                ),
                new GroupKey(
                    "if_string",
                    new IfStringRow(
                        new LiteralString("a"),
                        new LiteralString("b"),
                        new LiteralBoolean(true)
                    )
                ),
                new GroupKey(
                    "if_string_nullable",
                    new IfStringNullableRow(
                        new LiteralStringNullable(),
                        new LiteralString("a"),
                        new LiteralBoolean(true)
                    )
                ),
                new GroupKey(
                    "coalesce_string",
                    new CoalesceStringRow([
                        new LiteralStringNullable(),
                        new LiteralString("a"),
                    ])
                ),
                new GroupKey(
                    "coalesce_string_nullable",
                    new CoalesceStringNullableRow([
                        new LiteralStringNullable(),
                        new LiteralStringNullable(),
                    ])
                ),
                new GroupKey(
                    "if_boolean",
                    new IfBooleanRow(
                        new LiteralBoolean(true),
                        new LiteralBoolean(false),
                        new LiteralBoolean(true)
                    )
                ),
                new GroupKey(
                    "if_boolean_nullable",
                    new IfBooleanNullableRow(
                        new LiteralBooleanNullable(),
                        new LiteralBoolean(true),
                        new LiteralBoolean(true)
                    )
                ),
                new GroupKey(
                    "coalesce_boolean",
                    new CoalesceBooleanRow([
                        new LiteralBooleanNullable(),
                        new LiteralBoolean(true),
                    ])
                ),
                new GroupKey(
                    "coalesce_boolean_nullable",
                    new CoalesceBooleanNullableRow([
                        new LiteralBooleanNullable(),
                        new LiteralBooleanNullable(),
                    ])
                ),
                new GroupKey(
                    "if_date",
                    new IfDateRow(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDate(new DateOnly(2024, 3, 1)),
                        new LiteralBoolean(true)
                    )
                ),
                new GroupKey(
                    "if_date_nullable",
                    new IfDateNullableRow(
                        new LiteralDateNullable(),
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralBoolean(true)
                    )
                ),
                new GroupKey(
                    "coalesce_date",
                    new CoalesceDateRow([
                        new LiteralDateNullable(),
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                    ])
                ),
                new GroupKey(
                    "coalesce_date_nullable",
                    new CoalesceDateNullableRow([
                        new LiteralDateNullable(),
                        new LiteralDateNullable(),
                    ])
                ),
                new GroupKey(
                    "if_time",
                    new IfTimeRow(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralTime(new TimeOnly(17, 45, 30, 500)),
                        new LiteralBoolean(true)
                    )
                ),
                new GroupKey(
                    "if_time_nullable",
                    new IfTimeNullableRow(
                        new LiteralTimeNullable(),
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralBoolean(true)
                    )
                ),
                new GroupKey(
                    "coalesce_time",
                    new CoalesceTimeRow([
                        new LiteralTimeNullable(),
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                    ])
                ),
                new GroupKey(
                    "coalesce_time_nullable",
                    new CoalesceTimeNullableRow([
                        new LiteralTimeNullable(),
                        new LiteralTimeNullable(),
                    ])
                ),
                new GroupKey(
                    "if_datetime",
                    new IfDatetimeRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        ),
                        new LiteralBoolean(true)
                    )
                ),
                new GroupKey(
                    "if_datetime_nullable",
                    new IfDatetimeNullableRow(
                        new LiteralDatetimeNullable(),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralBoolean(true)
                    )
                ),
                new GroupKey(
                    "coalesce_datetime",
                    new CoalesceDatetimeRow([
                        new LiteralDatetimeNullable(),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                    ])
                ),
                new GroupKey(
                    "coalesce_datetime_nullable",
                    new CoalesceDatetimeNullableRow([
                        new LiteralDatetimeNullable(),
                        new LiteralDatetimeNullable(),
                    ])
                ),
                new GroupKey(
                    "if_uuid",
                    new IfUuidRow(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralUuid(new Guid(2, 0, 0, new byte[8])),
                        new LiteralBoolean(true)
                    )
                ),
                new GroupKey(
                    "if_uuid_nullable",
                    new IfUuidNullableRow(
                        new LiteralUuidNullable(),
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralBoolean(true)
                    )
                ),
                new GroupKey(
                    "coalesce_uuid",
                    new CoalesceUuidRow([
                        new LiteralUuidNullable(),
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                    ])
                ),
                new GroupKey(
                    "coalesce_uuid_nullable",
                    new CoalesceUuidNullableRow([
                        new LiteralUuidNullable(),
                        new LiteralUuidNullable(),
                    ])
                ),
                new GroupKey(
                    "nullIfEqual_integer_nullable",
                    new NullIfEqualIntegerNullableRow(
                        new LiteralInteger(7),
                        new LiteralInteger(-3)
                    )
                ),
                new GroupKey(
                    "nullIfEqual_decimal_nullable",
                    new NullIfEqualDecimalNullableRow(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m)
                    )
                ),
                new GroupKey(
                    "nullIfEqual_string_nullable",
                    new NullIfEqualStringNullableRow(
                        new LiteralString("a"),
                        new LiteralString("b")
                    )
                ),
                new GroupKey(
                    "nullIfEqual_boolean_nullable",
                    new NullIfEqualBooleanNullableRow(
                        new LiteralBoolean(true),
                        new LiteralBoolean(false)
                    )
                ),
                new GroupKey(
                    "nullIfEqual_date_nullable",
                    new NullIfEqualDateNullableRow(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDate(new DateOnly(2024, 3, 1))
                    )
                ),
                new GroupKey(
                    "nullIfEqual_time_nullable",
                    new NullIfEqualTimeNullableRow(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralTime(new TimeOnly(17, 45, 30, 500))
                    )
                ),
                new GroupKey(
                    "nullIfEqual_datetime_nullable",
                    new NullIfEqualDatetimeNullableRow(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        )
                    )
                ),
                new GroupKey(
                    "nullIfEqual_uuid_nullable",
                    new NullIfEqualUuidNullableRow(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralUuid(new Guid(2, 0, 0, new byte[8]))
                    )
                ),
                new GroupKey(
                    "nullIfEqual_integer_nullable_decimal_right",
                    new NullIfEqualIntegerNullableRow(
                        new LiteralInteger(7),
                        new LiteralDecimal(2.5m)
                    )
                ),
                new GroupKey(
                    "nullIfEqual_decimal_nullable_integer_right",
                    new NullIfEqualDecimalNullableRow(
                        new LiteralDecimal(2.5m),
                        new LiteralInteger(7)
                    )
                ),
                new GroupKey(
                    "nullIfEqual_string_nullable_null_left",
                    new NullIfEqualStringNullableRow(
                        new LiteralStringNullable(),
                        new LiteralString("a")
                    )
                ),
                new GroupKey(
                    "nullIfEqual_string_nullable_null_right",
                    new NullIfEqualStringNullableRow(
                        new LiteralString("a"),
                        new LiteralStringNullable()
                    )
                ),
                new GroupKey(
                    "greatest_integer",
                    new GreatestIntegerRow([
                        new LiteralInteger(7),
                        new LiteralInteger(-3),
                    ])
                ),
                new GroupKey(
                    "greatest_integer_nullable",
                    new GreatestIntegerNullableRow([
                        new LiteralInteger(7),
                        new LiteralIntegerNullable(),
                        new LiteralInteger(-3),
                    ])
                ),
                new GroupKey(
                    "greatest_decimal",
                    new GreatestDecimalRow([
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m),
                    ])
                ),
                new GroupKey(
                    "greatest_decimal_nullable",
                    new GreatestDecimalNullableRow([
                        new LiteralDecimal(2.5m),
                        new LiteralDecimalNullable(),
                        new LiteralDecimal(0.1m),
                    ])
                ),
                new GroupKey(
                    "greatest_string",
                    new GreatestStringRow([
                        new LiteralString("a"),
                        new LiteralString("b"),
                    ])
                ),
                new GroupKey(
                    "greatest_string_nullable",
                    new GreatestStringNullableRow([
                        new LiteralString("a"),
                        new LiteralStringNullable(),
                        new LiteralString("b"),
                    ])
                ),
                new GroupKey(
                    "greatest_boolean",
                    new GreatestBooleanRow([
                        new LiteralBoolean(true),
                        new LiteralBoolean(false),
                    ])
                ),
                new GroupKey(
                    "greatest_boolean_nullable",
                    new GreatestBooleanNullableRow([
                        new LiteralBoolean(true),
                        new LiteralBooleanNullable(),
                        new LiteralBoolean(false),
                    ])
                ),
                new GroupKey(
                    "greatest_date",
                    new GreatestDateRow([
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDate(new DateOnly(2024, 3, 1)),
                    ])
                ),
                new GroupKey(
                    "greatest_date_nullable",
                    new GreatestDateNullableRow([
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDateNullable(),
                        new LiteralDate(new DateOnly(2024, 3, 1)),
                    ])
                ),
                new GroupKey(
                    "greatest_time",
                    new GreatestTimeRow([
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralTime(new TimeOnly(17, 45, 30, 500)),
                    ])
                ),
                new GroupKey(
                    "greatest_time_nullable",
                    new GreatestTimeNullableRow([
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralTimeNullable(),
                        new LiteralTime(new TimeOnly(17, 45, 30, 500)),
                    ])
                ),
                new GroupKey(
                    "greatest_datetime",
                    new GreatestDatetimeRow([
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        ),
                    ])
                ),
                new GroupKey(
                    "greatest_datetime_nullable",
                    new GreatestDatetimeNullableRow([
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDatetimeNullable(),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        ),
                    ])
                ),
                new GroupKey(
                    "greatest_uuid",
                    new GreatestUuidRow([
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralUuid(new Guid(2, 0, 0, new byte[8])),
                    ])
                ),
                new GroupKey(
                    "greatest_uuid_nullable",
                    new GreatestUuidNullableRow([
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralUuidNullable(),
                        new LiteralUuid(new Guid(2, 0, 0, new byte[8])),
                    ])
                ),
                new GroupKey(
                    "greatest_decimal_of_integer_and_decimal",
                    new GreatestDecimalRow([
                        new LiteralInteger(7),
                        new LiteralDecimal(2.5m),
                    ])
                ),
                new GroupKey(
                    "least_integer",
                    new LeastIntegerRow([new LiteralInteger(7), new LiteralInteger(-3)])
                ),
                new GroupKey(
                    "least_integer_nullable",
                    new LeastIntegerNullableRow([
                        new LiteralInteger(7),
                        new LiteralIntegerNullable(),
                        new LiteralInteger(-3),
                    ])
                ),
                new GroupKey(
                    "least_decimal",
                    new LeastDecimalRow([
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m),
                    ])
                ),
                new GroupKey(
                    "least_decimal_nullable",
                    new LeastDecimalNullableRow([
                        new LiteralDecimal(2.5m),
                        new LiteralDecimalNullable(),
                        new LiteralDecimal(0.1m),
                    ])
                ),
                new GroupKey(
                    "least_string",
                    new LeastStringRow([new LiteralString("a"), new LiteralString("b")])
                ),
                new GroupKey(
                    "least_string_nullable",
                    new LeastStringNullableRow([
                        new LiteralString("a"),
                        new LiteralStringNullable(),
                        new LiteralString("b"),
                    ])
                ),
                new GroupKey(
                    "least_boolean",
                    new LeastBooleanRow([
                        new LiteralBoolean(true),
                        new LiteralBoolean(false),
                    ])
                ),
                new GroupKey(
                    "least_boolean_nullable",
                    new LeastBooleanNullableRow([
                        new LiteralBoolean(true),
                        new LiteralBooleanNullable(),
                        new LiteralBoolean(false),
                    ])
                ),
                new GroupKey(
                    "least_date",
                    new LeastDateRow([
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDate(new DateOnly(2024, 3, 1)),
                    ])
                ),
                new GroupKey(
                    "least_date_nullable",
                    new LeastDateNullableRow([
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDateNullable(),
                        new LiteralDate(new DateOnly(2024, 3, 1)),
                    ])
                ),
                new GroupKey(
                    "least_time",
                    new LeastTimeRow([
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralTime(new TimeOnly(17, 45, 30, 500)),
                    ])
                ),
                new GroupKey(
                    "least_time_nullable",
                    new LeastTimeNullableRow([
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralTimeNullable(),
                        new LiteralTime(new TimeOnly(17, 45, 30, 500)),
                    ])
                ),
                new GroupKey(
                    "least_datetime",
                    new LeastDatetimeRow([
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        ),
                    ])
                ),
                new GroupKey(
                    "least_datetime_nullable",
                    new LeastDatetimeNullableRow([
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDatetimeNullable(),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        ),
                    ])
                ),
                new GroupKey(
                    "least_uuid",
                    new LeastUuidRow([
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralUuid(new Guid(2, 0, 0, new byte[8])),
                    ])
                ),
                new GroupKey(
                    "least_uuid_nullable",
                    new LeastUuidNullableRow([
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralUuidNullable(),
                        new LiteralUuid(new Guid(2, 0, 0, new byte[8])),
                    ])
                ),
                new GroupKey(
                    "least_decimal_of_integer_and_decimal",
                    new LeastDecimalRow([new LiteralInteger(7), new LiteralDecimal(2.5m)])
                ),
            ],
            select: [new SelectItemGroup("n", new CountGroup())]
        );
}
