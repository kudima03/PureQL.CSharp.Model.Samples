using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Samples.Columns;
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
using PureQL.CSharp.Model.Fields;
using PureQL.CSharp.Model.GroupExpressions;
using PureQL.CSharp.Model.Lists;
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.Parameters;

namespace PureQL.CSharp.Model.Samples.Queries.GroupBy;

/// <summary>
/// Groups schema_with_foreign_keys.orders by user_id and selects, as 1069 columns, an
/// operator over literals for every signature of every operator: the lifted operators
/// with and without a nullable operand, integer and decimal operands, if and coalesce for
/// every type, equal, notEqual, the four comparisons and in for every family, and every
/// aggregate over a literal selector, with and without a predicate and with a nullable
/// selector, also over all rows. Some operands are parameters, so the query has no
/// result.
/// </summary>
public sealed record EveryOperatorSignatureInAGroupedSelectQuery
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
                    "user_id",
                    new FieldUuid(
                        new JoinedString(
                            new DotString(),
                            [
                                new RelationalSchemaWithForeignKeys().Name,
                                new OrdersTable().Name,
                            ]
                        ).TextValue,
                        new OrderUserIdColumn().Name.TextValue
                    )
                ),
            ],
            select:
            [
                new SelectItemGroup(
                    "add_integer",
                    new AddIntegerGroup([new LiteralInteger(7), new LiteralInteger(-3)])
                ),
                new SelectItemGroup(
                    "add_integer_nullable",
                    new AddIntegerNullableGroup([
                        new LiteralInteger(7),
                        new LiteralIntegerNullable(),
                    ])
                ),
                new SelectItemGroup(
                    "add_decimal",
                    new AddDecimalGroup([new LiteralInteger(7), new LiteralDecimal(2.5m)])
                ),
                new SelectItemGroup(
                    "add_decimal_nullable",
                    new AddDecimalNullableGroup([
                        new LiteralDecimalNullable(),
                        new LiteralInteger(7),
                    ])
                ),
                new SelectItemGroup(
                    "subtract_integer",
                    new SubtractIntegerGroup([
                        new LiteralInteger(7),
                        new LiteralInteger(-3),
                    ])
                ),
                new SelectItemGroup(
                    "subtract_integer_nullable",
                    new SubtractIntegerNullableGroup([
                        new LiteralInteger(7),
                        new LiteralIntegerNullable(),
                    ])
                ),
                new SelectItemGroup(
                    "subtract_decimal",
                    new SubtractDecimalGroup([
                        new LiteralInteger(7),
                        new LiteralDecimal(2.5m),
                    ])
                ),
                new SelectItemGroup(
                    "subtract_decimal_nullable",
                    new SubtractDecimalNullableGroup([
                        new LiteralDecimalNullable(),
                        new LiteralInteger(7),
                    ])
                ),
                new SelectItemGroup(
                    "multiply_integer",
                    new MultiplyIntegerGroup([
                        new LiteralInteger(7),
                        new LiteralInteger(-3),
                    ])
                ),
                new SelectItemGroup(
                    "multiply_integer_nullable",
                    new MultiplyIntegerNullableGroup([
                        new LiteralInteger(7),
                        new LiteralIntegerNullable(),
                    ])
                ),
                new SelectItemGroup(
                    "multiply_decimal",
                    new MultiplyDecimalGroup([
                        new LiteralInteger(7),
                        new LiteralDecimal(2.5m),
                    ])
                ),
                new SelectItemGroup(
                    "multiply_decimal_nullable",
                    new MultiplyDecimalNullableGroup([
                        new LiteralDecimalNullable(),
                        new LiteralInteger(7),
                    ])
                ),
                new SelectItemGroup(
                    "divide_decimal",
                    new DivideDecimalGroup([
                        new LiteralInteger(7),
                        new LiteralDecimal(2.5m),
                    ])
                ),
                new SelectItemGroup(
                    "divide_decimal_nullable",
                    new DivideDecimalNullableGroup([
                        new LiteralDecimalNullable(),
                        new LiteralInteger(7),
                    ])
                ),
                new SelectItemGroup(
                    "integerDivide_integer",
                    new IntegerDivideIntegerGroup(
                        new LiteralInteger(7),
                        new LiteralInteger(-3)
                    )
                ),
                new SelectItemGroup(
                    "integerDivide_integer_nullable",
                    new IntegerDivideIntegerNullableGroup(
                        new LiteralIntegerNullable(),
                        new LiteralInteger(-3)
                    )
                ),
                new SelectItemGroup(
                    "modulo_integer",
                    new ModuloIntegerGroup(new LiteralInteger(7), new LiteralInteger(-3))
                ),
                new SelectItemGroup(
                    "modulo_integer_nullable",
                    new ModuloIntegerNullableGroup(
                        new LiteralIntegerNullable(),
                        new LiteralInteger(-3)
                    )
                ),
                new SelectItemGroup(
                    "modulo_decimal",
                    new ModuloDecimalGroup(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m)
                    )
                ),
                new SelectItemGroup(
                    "modulo_decimal_of_integer_and_decimal",
                    new ModuloDecimalGroup(
                        new LiteralInteger(7),
                        new LiteralDecimal(2.5m)
                    )
                ),
                new SelectItemGroup(
                    "modulo_decimal_of_decimal_and_integer",
                    new ModuloDecimalGroup(
                        new LiteralDecimal(2.5m),
                        new LiteralInteger(7)
                    )
                ),
                new SelectItemGroup(
                    "modulo_decimal_nullable",
                    new ModuloDecimalNullableGroup(
                        new LiteralDecimalNullable(),
                        new LiteralDecimal(0.1m)
                    )
                ),
                new SelectItemGroup(
                    "modulo_decimal_nullable_right",
                    new ModuloDecimalNullableGroup(
                        new LiteralDecimal(2.5m),
                        new LiteralIntegerNullable()
                    )
                ),
                new SelectItemGroup(
                    "integerDivide_integer_of_decimals",
                    new IntegerDivideIntegerGroup(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m)
                    )
                ),
                new SelectItemGroup(
                    "integerDivide_integer_of_integer_and_decimal",
                    new IntegerDivideIntegerGroup(
                        new LiteralInteger(7),
                        new LiteralDecimal(2.5m)
                    )
                ),
                new SelectItemGroup(
                    "integerDivide_integer_nullable_of_decimals",
                    new IntegerDivideIntegerNullableGroup(
                        new LiteralDecimalNullable(),
                        new LiteralDecimal(0.1m)
                    )
                ),
                new SelectItemGroup(
                    "floor_integer",
                    new FloorIntegerGroup(new LiteralDecimal(2.5m))
                ),
                new SelectItemGroup(
                    "floor_integer_nullable",
                    new FloorIntegerNullableGroup(new LiteralDecimalNullable())
                ),
                new SelectItemGroup(
                    "ceiling_integer",
                    new CeilingIntegerGroup(new LiteralDecimal(2.5m))
                ),
                new SelectItemGroup(
                    "ceiling_integer_nullable",
                    new CeilingIntegerNullableGroup(new LiteralDecimalNullable())
                ),
                new SelectItemGroup(
                    "round_integer",
                    new RoundWithoutDigitsIntegerGroup(new LiteralDecimal(2.5m))
                ),
                new SelectItemGroup(
                    "round_integer_nullable",
                    new RoundWithoutDigitsIntegerNullableGroup(
                        new LiteralDecimalNullable()
                    )
                ),
                new SelectItemGroup(
                    "round_decimal",
                    new RoundDecimalGroup(
                        new LiteralDecimal(2.5m),
                        new LiteralInteger(-3)
                    )
                ),
                new SelectItemGroup(
                    "round_decimal_nullable",
                    new RoundDecimalNullableGroup(
                        new LiteralDecimalNullable(),
                        new LiteralInteger(7)
                    )
                ),
                new SelectItemGroup(
                    "round_integer_half_even",
                    new RoundWithoutDigitsIntegerGroup(
                        new LiteralDecimal(2.5m),
                        mode: RoundingMode.HalfEven
                    )
                ),
                new SelectItemGroup(
                    "round_integer_nullable_half_even",
                    new RoundWithoutDigitsIntegerNullableGroup(
                        new LiteralDecimalNullable(),
                        mode: RoundingMode.HalfEven
                    )
                ),
                new SelectItemGroup(
                    "round_decimal_half_even",
                    new RoundDecimalGroup(
                        new LiteralDecimal(2.5m),
                        new LiteralInteger(2),
                        mode: RoundingMode.HalfEven
                    )
                ),
                new SelectItemGroup(
                    "round_decimal_nullable_half_even",
                    new RoundDecimalNullableGroup(
                        new LiteralDecimalNullable(),
                        new LiteralInteger(2),
                        mode: RoundingMode.HalfEven
                    )
                ),
                new SelectItemGroup(
                    "round_integer_of_integer_half_even",
                    new RoundWithDigitsIntegerGroup(
                        new LiteralInteger(7),
                        new LiteralInteger(-1),
                        mode: RoundingMode.HalfEven
                    )
                ),
                new SelectItemGroup(
                    "truncate_integer",
                    new TruncateWithoutDigitsIntegerGroup(new LiteralDecimal(2.5m))
                ),
                new SelectItemGroup(
                    "truncate_integer_nullable",
                    new TruncateWithoutDigitsIntegerNullableGroup(
                        new LiteralDecimalNullable()
                    )
                ),
                new SelectItemGroup(
                    "truncate_decimal",
                    new TruncateDecimalGroup(
                        new LiteralDecimal(2.5m),
                        new LiteralInteger(7)
                    )
                ),
                new SelectItemGroup(
                    "truncate_decimal_nullable",
                    new TruncateDecimalNullableGroup(
                        new LiteralDecimalNullable(),
                        new LiteralInteger(7)
                    )
                ),
                new SelectItemGroup(
                    "abs_integer",
                    new AbsIntegerGroup(new LiteralInteger(-3))
                ),
                new SelectItemGroup(
                    "abs_integer_nullable",
                    new AbsIntegerNullableGroup(new LiteralIntegerNullable())
                ),
                new SelectItemGroup(
                    "abs_decimal",
                    new AbsDecimalGroup(new LiteralDecimal(2.5m))
                ),
                new SelectItemGroup(
                    "abs_decimal_nullable",
                    new AbsDecimalNullableGroup(new LiteralDecimalNullable())
                ),
                new SelectItemGroup(
                    "sign_integer",
                    new SignIntegerGroup(new LiteralDecimal(2.5m))
                ),
                new SelectItemGroup(
                    "sign_integer_nullable",
                    new SignIntegerNullableGroup(new LiteralDecimalNullable())
                ),
                new SelectItemGroup(
                    "sign_integer_of_integer",
                    new SignIntegerGroup(new LiteralInteger(-3))
                ),
                new SelectItemGroup(
                    "power_decimal",
                    new PowerDecimalGroup(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m)
                    )
                ),
                new SelectItemGroup(
                    "power_decimal_nullable",
                    new PowerDecimalNullableGroup(
                        new LiteralDecimalNullable(),
                        new LiteralInteger(7)
                    )
                ),
                new SelectItemGroup(
                    "power_decimal_of_integers",
                    new PowerDecimalGroup(new LiteralInteger(7), new LiteralInteger(-3))
                ),
                new SelectItemGroup(
                    "sqrt_decimal",
                    new SqrtDecimalGroup(new LiteralDecimal(2.5m))
                ),
                new SelectItemGroup(
                    "sqrt_decimal_nullable",
                    new SqrtDecimalNullableGroup(new LiteralDecimalNullable())
                ),
                new SelectItemGroup(
                    "sqrt_decimal_of_integer",
                    new SqrtDecimalGroup(new LiteralInteger(7))
                ),
                new SelectItemGroup(
                    "exp_decimal",
                    new ExpDecimalGroup(new LiteralDecimal(2.5m))
                ),
                new SelectItemGroup(
                    "exp_decimal_nullable",
                    new ExpDecimalNullableGroup(new LiteralDecimalNullable())
                ),
                new SelectItemGroup(
                    "exp_decimal_of_integer",
                    new ExpDecimalGroup(new LiteralInteger(7))
                ),
                new SelectItemGroup(
                    "ln_decimal",
                    new LnDecimalGroup(new LiteralDecimal(2.5m))
                ),
                new SelectItemGroup(
                    "ln_decimal_nullable",
                    new LnDecimalNullableGroup(new LiteralDecimalNullable())
                ),
                new SelectItemGroup(
                    "ln_decimal_of_integer",
                    new LnDecimalGroup(new LiteralInteger(7))
                ),
                new SelectItemGroup(
                    "log10_decimal",
                    new Log10DecimalGroup(new LiteralDecimal(2.5m))
                ),
                new SelectItemGroup(
                    "log10_decimal_nullable",
                    new Log10DecimalNullableGroup(new LiteralDecimalNullable())
                ),
                new SelectItemGroup(
                    "log10_decimal_of_integer",
                    new Log10DecimalGroup(new LiteralInteger(7))
                ),
                new SelectItemGroup(
                    "log_decimal",
                    new LogDecimalGroup(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m)
                    )
                ),
                new SelectItemGroup(
                    "log_decimal_nullable",
                    new LogDecimalNullableGroup(
                        new LiteralDecimalNullable(),
                        new LiteralInteger(7)
                    )
                ),
                new SelectItemGroup(
                    "sin_decimal",
                    new SinDecimalGroup(new LiteralDecimal(0.5m))
                ),
                new SelectItemGroup(
                    "sin_decimal_of_integer",
                    new SinDecimalGroup(new LiteralInteger(1))
                ),
                new SelectItemGroup(
                    "sin_decimal_nullable",
                    new SinDecimalNullableGroup(new LiteralDecimalNullable())
                ),
                new SelectItemGroup(
                    "cos_decimal",
                    new CosDecimalGroup(new LiteralDecimal(0.5m))
                ),
                new SelectItemGroup(
                    "cos_decimal_of_integer",
                    new CosDecimalGroup(new LiteralInteger(1))
                ),
                new SelectItemGroup(
                    "cos_decimal_nullable",
                    new CosDecimalNullableGroup(new LiteralDecimalNullable())
                ),
                new SelectItemGroup(
                    "tan_decimal",
                    new TanDecimalGroup(new LiteralDecimal(0.5m))
                ),
                new SelectItemGroup(
                    "tan_decimal_of_integer",
                    new TanDecimalGroup(new LiteralInteger(1))
                ),
                new SelectItemGroup(
                    "tan_decimal_nullable",
                    new TanDecimalNullableGroup(new LiteralDecimalNullable())
                ),
                new SelectItemGroup(
                    "asin_decimal",
                    new AsinDecimalGroup(new LiteralDecimal(0.5m))
                ),
                new SelectItemGroup(
                    "asin_decimal_of_integer",
                    new AsinDecimalGroup(new LiteralInteger(1))
                ),
                new SelectItemGroup(
                    "asin_decimal_nullable",
                    new AsinDecimalNullableGroup(new LiteralDecimalNullable())
                ),
                new SelectItemGroup(
                    "acos_decimal",
                    new AcosDecimalGroup(new LiteralDecimal(0.5m))
                ),
                new SelectItemGroup(
                    "acos_decimal_of_integer",
                    new AcosDecimalGroup(new LiteralInteger(1))
                ),
                new SelectItemGroup(
                    "acos_decimal_nullable",
                    new AcosDecimalNullableGroup(new LiteralDecimalNullable())
                ),
                new SelectItemGroup(
                    "atan_decimal",
                    new AtanDecimalGroup(new LiteralDecimal(0.5m))
                ),
                new SelectItemGroup(
                    "atan_decimal_of_integer",
                    new AtanDecimalGroup(new LiteralInteger(1))
                ),
                new SelectItemGroup(
                    "atan_decimal_nullable",
                    new AtanDecimalNullableGroup(new LiteralDecimalNullable())
                ),
                new SelectItemGroup(
                    "atan2_decimal",
                    new Atan2DecimalGroup(
                        new LiteralDecimal(0.5m),
                        new LiteralDecimal(-1m)
                    )
                ),
                new SelectItemGroup(
                    "atan2_decimal_of_integers",
                    new Atan2DecimalGroup(new LiteralInteger(1), new LiteralInteger(-3))
                ),
                new SelectItemGroup(
                    "atan2_decimal_nullable_y",
                    new Atan2DecimalNullableGroup(
                        new LiteralDecimalNullable(),
                        new LiteralDecimal(0.5m)
                    )
                ),
                new SelectItemGroup(
                    "atan2_decimal_nullable_x",
                    new Atan2DecimalNullableGroup(
                        new LiteralDecimal(0.5m),
                        new LiteralDecimalNullable()
                    )
                ),
                new SelectItemGroup("pi_decimal", new PiGroup()),
                new SelectItemGroup(
                    "log_decimal_of_integers",
                    new LogDecimalGroup(new LiteralInteger(7), new LiteralInteger(7))
                ),
                new SelectItemGroup(
                    "concat_string",
                    new ConcatStringGroup([
                        new LiteralString("a"),
                        new LiteralString("b"),
                    ])
                ),
                new SelectItemGroup(
                    "concat_string_nullable",
                    new ConcatStringNullableGroup([
                        new LiteralString("a"),
                        new LiteralStringNullable(),
                    ])
                ),
                new SelectItemGroup(
                    "length_integer",
                    new LengthIntegerGroup(new LiteralString("abc"))
                ),
                new SelectItemGroup(
                    "length_integer_nullable",
                    new LengthIntegerNullableGroup(new LiteralStringNullable())
                ),
                new SelectItemGroup(
                    "upper_string",
                    new UpperStringGroup(new LiteralString("Stra\u00dfe"))
                ),
                new SelectItemGroup(
                    "upper_string_nullable",
                    new UpperStringNullableGroup(new LiteralStringNullable())
                ),
                new SelectItemGroup(
                    "lower_string",
                    new LowerStringGroup(new LiteralString("Stra\u00dfe"))
                ),
                new SelectItemGroup(
                    "lower_string_nullable",
                    new LowerStringNullableGroup(new LiteralStringNullable())
                ),
                new SelectItemGroup(
                    "foldCase_string",
                    new FoldCaseStringGroup(new LiteralString("a"))
                ),
                new SelectItemGroup(
                    "foldCase_string_nullable",
                    new FoldCaseStringNullableGroup(new LiteralStringNullable())
                ),
                new SelectItemGroup(
                    "normalize_string_nfc",
                    new NormalizeStringGroup(
                        new LiteralString("a"),
                        NormalizationForm.NFC
                    )
                ),
                new SelectItemGroup(
                    "normalize_string_nfd",
                    new NormalizeStringGroup(
                        new LiteralString("a"),
                        NormalizationForm.NFD
                    )
                ),
                new SelectItemGroup(
                    "normalize_string_nfkc",
                    new NormalizeStringGroup(
                        new LiteralString("a"),
                        NormalizationForm.NFKC
                    )
                ),
                new SelectItemGroup(
                    "normalize_string_nfkd",
                    new NormalizeStringGroup(
                        new LiteralString("a"),
                        NormalizationForm.NFKD
                    )
                ),
                new SelectItemGroup(
                    "normalize_string_nullable",
                    new NormalizeStringNullableGroup(
                        new LiteralStringNullable(),
                        NormalizationForm.NFC
                    )
                ),
                new SelectItemGroup(
                    "normalize_string_nullable_nfkd",
                    new NormalizeStringNullableGroup(
                        new LiteralStringNullable(),
                        NormalizationForm.NFKD
                    )
                ),
                new SelectItemGroup(
                    "removeDiacritics_string",
                    new RemoveDiacriticsStringGroup(new LiteralString("a"))
                ),
                new SelectItemGroup(
                    "removeDiacritics_string_nullable",
                    new RemoveDiacriticsStringNullableGroup(new LiteralStringNullable())
                ),
                new SelectItemGroup(
                    "trim_string",
                    new TrimStringGroup(
                        new LiteralString("xax"),
                        [new LiteralString("x")]
                    )
                ),
                new SelectItemGroup(
                    "trim_string_nullable",
                    new TrimStringNullableGroup(
                        new LiteralStringNullable(),
                        [new LiteralString("x"), new LiteralString("xa")]
                    )
                ),
                new SelectItemGroup(
                    "trimStart_string",
                    new TrimStartStringGroup(
                        new LiteralString("xax"),
                        [new LiteralString("x")]
                    )
                ),
                new SelectItemGroup(
                    "trimStart_string_nullable",
                    new TrimStartStringNullableGroup(
                        new LiteralStringNullable(),
                        [new LiteralString("x"), new LiteralString("xa")]
                    )
                ),
                new SelectItemGroup(
                    "trimEnd_string",
                    new TrimEndStringGroup(
                        new LiteralString("xax"),
                        [new LiteralString("x")]
                    )
                ),
                new SelectItemGroup(
                    "trimEnd_string_nullable",
                    new TrimEndStringNullableGroup(
                        new LiteralStringNullable(),
                        [new LiteralString("x"), new LiteralString("xa")]
                    )
                ),
                new SelectItemGroup(
                    "substring_string",
                    new SubstringStringGroup(
                        new LiteralString("hello"),
                        new LiteralInteger(2)
                    )
                ),
                new SelectItemGroup(
                    "substring_string_nullable",
                    new SubstringStringNullableGroup(
                        new LiteralStringNullable(),
                        new LiteralInteger(2)
                    )
                ),
                new SelectItemGroup(
                    "substring_string_length",
                    new SubstringStringGroup(
                        new LiteralString("hello"),
                        new LiteralInteger(2),
                        length: new LiteralInteger(3)
                    )
                ),
                new SelectItemGroup(
                    "substring_string_nullable_length",
                    new SubstringStringNullableGroup(
                        new LiteralStringNullable(),
                        new LiteralInteger(2),
                        length: new LiteralInteger(0)
                    )
                ),
                new SelectItemGroup(
                    "replace_string",
                    new ReplaceStringGroup(
                        new LiteralString("a-b"),
                        new LiteralString("-"),
                        new LiteralString("b")
                    )
                ),
                new SelectItemGroup(
                    "replace_string_nullable",
                    new ReplaceStringNullableGroup(
                        new LiteralStringNullable(),
                        new LiteralString("-"),
                        new LiteralString("b")
                    )
                ),
                new SelectItemGroup(
                    "position_integer",
                    new PositionIntegerGroup(
                        new LiteralString("a-b"),
                        new LiteralString("-")
                    )
                ),
                new SelectItemGroup(
                    "position_integer_nullable",
                    new PositionIntegerNullableGroup(
                        new LiteralStringNullable(),
                        new LiteralString("-")
                    )
                ),
                new SelectItemGroup(
                    "position_integer_start",
                    new PositionIntegerGroup(
                        new LiteralString("a-b"),
                        new LiteralString("-"),
                        start: new LiteralInteger(2)
                    )
                ),
                new SelectItemGroup(
                    "position_integer_nullable_start",
                    new PositionIntegerNullableGroup(
                        new LiteralStringNullable(),
                        new LiteralString("-"),
                        start: new LiteralInteger(2)
                    )
                ),
                new SelectItemGroup(
                    "splitPart_string",
                    new SplitPartStringGroup(
                        new LiteralString("a-b"),
                        new LiteralString("-"),
                        new LiteralInteger(1)
                    )
                ),
                new SelectItemGroup(
                    "splitPart_string_nullable",
                    new SplitPartStringNullableGroup(
                        new LiteralStringNullable(),
                        new LiteralString("-"),
                        new LiteralInteger(-3)
                    )
                ),
                new SelectItemGroup(
                    "padStart_string",
                    new PadStartStringGroup(
                        new LiteralString("a"),
                        new LiteralInteger(7),
                        new LiteralString("b")
                    )
                ),
                new SelectItemGroup(
                    "padStart_string_nullable",
                    new PadStartStringNullableGroup(
                        new LiteralStringNullable(),
                        new LiteralInteger(7),
                        new LiteralString("b")
                    )
                ),
                new SelectItemGroup(
                    "padEnd_string",
                    new PadEndStringGroup(
                        new LiteralString("a"),
                        new LiteralInteger(7),
                        new LiteralString("b")
                    )
                ),
                new SelectItemGroup(
                    "padEnd_string_nullable",
                    new PadEndStringNullableGroup(
                        new LiteralStringNullable(),
                        new LiteralInteger(7),
                        new LiteralString("b")
                    )
                ),
                new SelectItemGroup(
                    "repeat_string",
                    new RepeatStringGroup(new LiteralString("a"), new LiteralInteger(7))
                ),
                new SelectItemGroup(
                    "repeat_string_nullable",
                    new RepeatStringNullableGroup(
                        new LiteralStringNullable(),
                        new LiteralInteger(7)
                    )
                ),
                new SelectItemGroup(
                    "reverse_string",
                    new ReverseStringGroup(new LiteralString("a"))
                ),
                new SelectItemGroup(
                    "reverse_string_nullable",
                    new ReverseStringNullableGroup(new LiteralStringNullable())
                ),
                new SelectItemGroup(
                    "concatWith_string",
                    new ConcatWithGroup(
                        [new LiteralString("a"), new LiteralString("b")],
                        new LiteralString("-")
                    )
                ),
                new SelectItemGroup(
                    "concatWith_string_null_values",
                    new ConcatWithGroup(
                        [
                            new LiteralStringNullable(),
                            new LiteralString("a"),
                            new LiteralStringNullable(),
                        ],
                        new LiteralString("-")
                    )
                ),
                new SelectItemGroup(
                    "concatWith_string_only_null_values",
                    new ConcatWithGroup(
                        [new LiteralStringNullable(), new LiteralStringNullable()],
                        new LiteralString("-")
                    )
                ),
                new SelectItemGroup(
                    "matchesRegex_boolean",
                    new MatchesRegexGroup(
                        new LiteralString("a1"),
                        new LiteralString("[0-9]")
                    )
                ),
                new SelectItemGroup(
                    "matchesRegex_boolean_nullable_operand",
                    new MatchesRegexGroup(
                        new LiteralStringNullable(),
                        new LiteralString("[0-9]")
                    )
                ),
                new SelectItemGroup(
                    "matchesRegex_boolean_parameter_pattern",
                    new MatchesRegexGroup(
                        new LiteralString("a1"),
                        new ParamString("pattern")
                    )
                ),
                new SelectItemGroup(
                    "extractRegex_string_nullable",
                    new ExtractRegexGroup(
                        new LiteralString("a1"),
                        new LiteralString("[0-9]+")
                    )
                ),
                new SelectItemGroup(
                    "extractRegex_string_nullable_group",
                    new ExtractRegexGroup(
                        new LiteralString("a1"),
                        new LiteralString("(a)([0-9])"),
                        group: new LiteralInteger(2)
                    )
                ),
                new SelectItemGroup(
                    "extractRegex_string_nullable_nullable_operand",
                    new ExtractRegexGroup(
                        new LiteralStringNullable(),
                        new LiteralString("[0-9]+")
                    )
                ),
                new SelectItemGroup(
                    "extractRegex_string_nullable_parameters",
                    new ExtractRegexGroup(
                        new LiteralString("a1"),
                        new ParamString("pattern"),
                        group: new ParamInteger("group")
                    )
                ),
                new SelectItemGroup(
                    "replaceRegex_string",
                    new ReplaceRegexStringGroup(
                        new LiteralString("a1"),
                        new LiteralString("b"),
                        new LiteralString("[0-9]")
                    )
                ),
                new SelectItemGroup(
                    "replaceRegex_string_nullable",
                    new ReplaceRegexStringNullableGroup(
                        new LiteralStringNullable(),
                        new LiteralString("b"),
                        new LiteralString("[0-9]")
                    )
                ),
                new SelectItemGroup(
                    "round_decimal_nullable_by_digits",
                    new RoundDecimalNullableGroup(
                        new LiteralDecimal(2.5m),
                        new LiteralIntegerNullable()
                    )
                ),
                new SelectItemGroup(
                    "truncate_decimal_nullable_by_digits",
                    new TruncateDecimalNullableGroup(
                        new LiteralDecimal(2.5m),
                        new LiteralIntegerNullable()
                    )
                ),
                new SelectItemGroup(
                    "round_integer_of_integer_with_digits",
                    new RoundWithDigitsIntegerGroup(
                        new LiteralInteger(7),
                        new LiteralInteger(1)
                    )
                ),
                new SelectItemGroup(
                    "round_integer_nullable_of_integer_with_digits",
                    new RoundWithDigitsIntegerNullableGroup(
                        new LiteralIntegerNullable(),
                        new LiteralInteger(-1)
                    )
                ),
                new SelectItemGroup(
                    "round_integer_nullable_of_integer_by_digits",
                    new RoundWithDigitsIntegerNullableGroup(
                        new LiteralInteger(7),
                        new LiteralIntegerNullable()
                    )
                ),
                new SelectItemGroup(
                    "truncate_integer_of_integer_with_digits",
                    new TruncateWithDigitsIntegerGroup(
                        new LiteralInteger(7),
                        new LiteralInteger(-1)
                    )
                ),
                new SelectItemGroup(
                    "truncate_integer_nullable_of_integer_with_digits",
                    new TruncateWithDigitsIntegerNullableGroup(
                        new LiteralIntegerNullable(),
                        new LiteralInteger(2)
                    )
                ),
                new SelectItemGroup(
                    "truncate_integer_nullable_of_integer_by_digits",
                    new TruncateWithDigitsIntegerNullableGroup(
                        new LiteralInteger(7),
                        new LiteralIntegerNullable()
                    )
                ),
                new SelectItemGroup(
                    "power_decimal_nullable_by_exponent",
                    new PowerDecimalNullableGroup(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimalNullable()
                    )
                ),
                new SelectItemGroup(
                    "log_decimal_nullable_by_base",
                    new LogDecimalNullableGroup(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimalNullable()
                    )
                ),
                new SelectItemGroup(
                    "substring_string_nullable_by_start",
                    new SubstringStringNullableGroup(
                        new LiteralString("abc"),
                        new LiteralIntegerNullable(),
                        length: new LiteralInteger(2)
                    )
                ),
                new SelectItemGroup(
                    "substring_string_nullable_by_length",
                    new SubstringStringNullableGroup(
                        new LiteralString("abc"),
                        new LiteralInteger(1),
                        length: new LiteralIntegerNullable()
                    )
                ),
                new SelectItemGroup(
                    "replace_string_nullable_by_pattern",
                    new ReplaceStringNullableGroup(
                        new LiteralString("abc"),
                        new LiteralStringNullable(),
                        new LiteralString("x")
                    )
                ),
                new SelectItemGroup(
                    "replace_string_nullable_by_replacement",
                    new ReplaceStringNullableGroup(
                        new LiteralString("abc"),
                        new LiteralString("b"),
                        new LiteralStringNullable()
                    )
                ),
                new SelectItemGroup(
                    "position_integer_nullable_by_pattern",
                    new PositionIntegerNullableGroup(
                        new LiteralString("abc"),
                        new LiteralStringNullable(),
                        start: new LiteralInteger(1)
                    )
                ),
                new SelectItemGroup(
                    "position_integer_nullable_by_start",
                    new PositionIntegerNullableGroup(
                        new LiteralString("abc"),
                        new LiteralString("b"),
                        start: new LiteralIntegerNullable()
                    )
                ),
                new SelectItemGroup(
                    "splitPart_string_nullable_by_delimiter",
                    new SplitPartStringNullableGroup(
                        new LiteralString("a-b"),
                        new LiteralStringNullable(),
                        new LiteralInteger(1)
                    )
                ),
                new SelectItemGroup(
                    "splitPart_string_nullable_by_index",
                    new SplitPartStringNullableGroup(
                        new LiteralString("a-b"),
                        new LiteralString("-"),
                        new LiteralIntegerNullable()
                    )
                ),
                new SelectItemGroup(
                    "padStart_string_nullable_by_length",
                    new PadStartStringNullableGroup(
                        new LiteralString("abc"),
                        new LiteralIntegerNullable(),
                        new LiteralString("0")
                    )
                ),
                new SelectItemGroup(
                    "padStart_string_nullable_by_fill",
                    new PadStartStringNullableGroup(
                        new LiteralString("abc"),
                        new LiteralInteger(7),
                        new LiteralStringNullable()
                    )
                ),
                new SelectItemGroup(
                    "padEnd_string_nullable_by_length",
                    new PadEndStringNullableGroup(
                        new LiteralString("abc"),
                        new LiteralIntegerNullable(),
                        new LiteralString("0")
                    )
                ),
                new SelectItemGroup(
                    "padEnd_string_nullable_by_fill",
                    new PadEndStringNullableGroup(
                        new LiteralString("abc"),
                        new LiteralInteger(7),
                        new LiteralStringNullable()
                    )
                ),
                new SelectItemGroup(
                    "repeat_string_nullable_by_count",
                    new RepeatStringNullableGroup(
                        new LiteralString("abc"),
                        new LiteralIntegerNullable()
                    )
                ),
                new SelectItemGroup(
                    "replaceRegex_string_nullable_by_replacement",
                    new ReplaceRegexStringNullableGroup(
                        new LiteralString("abc"),
                        new LiteralStringNullable(),
                        new LiteralString("b+")
                    )
                ),
                new SelectItemGroup(
                    "replaceRegex_string_parameter_pattern",
                    new ReplaceRegexStringGroup(
                        new LiteralString("a1"),
                        new LiteralString("b"),
                        new ParamString("pattern")
                    )
                ),
                new SelectItemGroup(
                    "contains_boolean",
                    new ContainsGroup(
                        new LiteralString("abcabc"),
                        new LiteralString("abc")
                    )
                ),
                new SelectItemGroup(
                    "contains_boolean_nullable_operand",
                    new ContainsGroup(
                        new LiteralStringNullable(),
                        new LiteralString("abc")
                    )
                ),
                new SelectItemGroup(
                    "contains_boolean_nullable_pattern",
                    new ContainsGroup(
                        new LiteralString("abcabc"),
                        new LiteralStringNullable()
                    )
                ),
                new SelectItemGroup(
                    "startsWith_boolean",
                    new StartsWithGroup(
                        new LiteralString("abcabc"),
                        new LiteralString("abc")
                    )
                ),
                new SelectItemGroup(
                    "startsWith_boolean_nullable_operand",
                    new StartsWithGroup(
                        new LiteralStringNullable(),
                        new LiteralString("abc")
                    )
                ),
                new SelectItemGroup(
                    "startsWith_boolean_nullable_pattern",
                    new StartsWithGroup(
                        new LiteralString("abcabc"),
                        new LiteralStringNullable()
                    )
                ),
                new SelectItemGroup(
                    "endsWith_boolean",
                    new EndsWithGroup(
                        new LiteralString("abcabc"),
                        new LiteralString("abc")
                    )
                ),
                new SelectItemGroup(
                    "endsWith_boolean_nullable_operand",
                    new EndsWithGroup(
                        new LiteralStringNullable(),
                        new LiteralString("abc")
                    )
                ),
                new SelectItemGroup(
                    "endsWith_boolean_nullable_pattern",
                    new EndsWithGroup(
                        new LiteralString("abcabc"),
                        new LiteralStringNullable()
                    )
                ),
                new SelectItemGroup(
                    "dateAddDays_date",
                    new DateAddDaysDateGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralInteger(7)
                    )
                ),
                new SelectItemGroup(
                    "dateAddDays_date_nullable",
                    new DateAddDaysDateNullableGroup(
                        new LiteralDateNullable(),
                        new LiteralInteger(7)
                    )
                ),
                new SelectItemGroup(
                    "dateDiffDays_integer",
                    new DateDiffDaysIntegerGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDate(new DateOnly(2024, 3, 1))
                    )
                ),
                new SelectItemGroup(
                    "dateDiffDays_integer_nullable",
                    new DateDiffDaysIntegerNullableGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDateNullable()
                    )
                ),
                new SelectItemGroup(
                    "timeAddSeconds_time",
                    new TimeAddSecondsTimeGroup(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralDecimal(2.5m)
                    )
                ),
                new SelectItemGroup(
                    "timeAddSeconds_time_nullable",
                    new TimeAddSecondsTimeNullableGroup(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralIntegerNullable()
                    )
                ),
                new SelectItemGroup(
                    "timeDiffSeconds_decimal",
                    new TimeDiffSecondsDecimalGroup(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralTime(new TimeOnly(17, 45, 30, 500))
                    )
                ),
                new SelectItemGroup(
                    "timeDiffSeconds_decimal_nullable",
                    new TimeDiffSecondsDecimalNullableGroup(
                        new LiteralTimeNullable(),
                        new LiteralTime(new TimeOnly(8, 30, 0))
                    )
                ),
                new SelectItemGroup(
                    "datetimeAddSeconds_datetime",
                    new DatetimeAddSecondsDatetimeGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDecimal(2.5m)
                    )
                ),
                new SelectItemGroup(
                    "datetimeAddSeconds_datetime_nullable",
                    new DatetimeAddSecondsDatetimeNullableGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralIntegerNullable()
                    )
                ),
                new SelectItemGroup(
                    "datetimeDiffSeconds_decimal",
                    new DatetimeDiffSecondsDecimalGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemGroup(
                    "datetimeDiffSeconds_decimal_nullable",
                    new DatetimeDiffSecondsDecimalNullableGroup(
                        new LiteralDatetimeNullable(),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemGroup(
                    "dateAddMonths_date",
                    new DateAddMonthsDateGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralInteger(1)
                    )
                ),
                new SelectItemGroup(
                    "dateAddMonths_date_nullable",
                    new DateAddMonthsDateNullableGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralIntegerNullable()
                    )
                ),
                new SelectItemGroup(
                    "datetimeAddMonths_datetime",
                    new DatetimeAddMonthsDatetimeGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 18, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralInteger(1)
                    )
                ),
                new SelectItemGroup(
                    "datetimeAddMonths_datetime_nullable",
                    new DatetimeAddMonthsDatetimeNullableGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 18, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralIntegerNullable()
                    )
                ),
                new SelectItemGroup(
                    "datetimeAddMonths_datetime_in_zone",
                    new DatetimeAddMonthsDatetimeGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralInteger(1),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemGroup(
                    "datetimeAddMonths_datetime_nullable_in_zone",
                    new DatetimeAddMonthsDatetimeNullableGroup(
                        new LiteralDatetimeNullable(),
                        new LiteralInteger(1),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemGroup(
                    "datetimeAddMonths_datetime_nullable_months_in_zone",
                    new DatetimeAddMonthsDatetimeNullableGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralIntegerNullable(),
                        zone: new LiteralString("America/New_York")
                    )
                ),
                new SelectItemGroup(
                    "datetimeAddMonths_datetime_nullable_by_zone",
                    new DatetimeAddMonthsDatetimeNullableGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralInteger(-12),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemGroup(
                    "dateDiffMonths_integer",
                    new DateDiffMonthsIntegerGroup(
                        new LiteralDate(new DateOnly(2023, 2, 28)),
                        new LiteralDate(new DateOnly(2023, 1, 31))
                    )
                ),
                new SelectItemGroup(
                    "dateDiffMonths_integer_nullable",
                    new DateDiffMonthsIntegerNullableGroup(
                        new LiteralDateNullable(),
                        new LiteralDate(new DateOnly(2023, 1, 31))
                    )
                ),
                new SelectItemGroup(
                    "datetimeDiffMonths_integer",
                    new DatetimeDiffMonthsIntegerGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 2, 15, 11, 59, 59, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 15, 12, 0, 0, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemGroup(
                    "datetimeDiffMonths_integer_nullable",
                    new DatetimeDiffMonthsIntegerNullableGroup(
                        new LiteralDatetimeNullable(),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 15, 12, 0, 0, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemGroup(
                    "datetimeDiffMonths_integer_in_zone",
                    new DatetimeDiffMonthsIntegerGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemGroup(
                    "datetimeDiffMonths_integer_nullable_in_zone",
                    new DatetimeDiffMonthsIntegerNullableGroup(
                        new LiteralDatetimeNullable(),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Etc/GMT+5")
                    )
                ),
                new SelectItemGroup(
                    "datetimeDiffMonths_integer_nullable_by_zone",
                    new DatetimeDiffMonthsIntegerNullableGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemGroup(
                    "dateFromParts_date",
                    new DateFromPartsDateGroup(
                        new LiteralInteger(2024),
                        new LiteralInteger(2),
                        new LiteralInteger(29)
                    )
                ),
                new SelectItemGroup(
                    "dateFromParts_date_nullable_year",
                    new DateFromPartsDateNullableGroup(
                        new LiteralIntegerNullable(),
                        new LiteralInteger(2),
                        new LiteralInteger(29)
                    )
                ),
                new SelectItemGroup(
                    "dateFromParts_date_nullable_month",
                    new DateFromPartsDateNullableGroup(
                        new LiteralInteger(2024),
                        new LiteralIntegerNullable(),
                        new LiteralInteger(29)
                    )
                ),
                new SelectItemGroup(
                    "dateFromParts_date_nullable_day",
                    new DateFromPartsDateNullableGroup(
                        new LiteralInteger(2024),
                        new LiteralInteger(2),
                        new LiteralIntegerNullable()
                    )
                ),
                new SelectItemGroup(
                    "timeFromParts_time",
                    new TimeFromPartsTimeGroup(
                        new LiteralInteger(18),
                        new LiteralInteger(30),
                        new LiteralDecimal(15.25m)
                    )
                ),
                new SelectItemGroup(
                    "timeFromParts_time_integer_second",
                    new TimeFromPartsTimeGroup(
                        new LiteralInteger(18),
                        new LiteralInteger(30),
                        new LiteralInteger(15)
                    )
                ),
                new SelectItemGroup(
                    "timeFromParts_time_nullable_hour",
                    new TimeFromPartsTimeNullableGroup(
                        new LiteralIntegerNullable(),
                        new LiteralInteger(30),
                        new LiteralInteger(15)
                    )
                ),
                new SelectItemGroup(
                    "timeFromParts_time_nullable_minute",
                    new TimeFromPartsTimeNullableGroup(
                        new LiteralInteger(18),
                        new LiteralIntegerNullable(),
                        new LiteralInteger(15)
                    )
                ),
                new SelectItemGroup(
                    "timeFromParts_time_nullable_second",
                    new TimeFromPartsTimeNullableGroup(
                        new LiteralInteger(18),
                        new LiteralInteger(30),
                        new LiteralDecimalNullable()
                    )
                ),
                new SelectItemGroup(
                    "datetimeFromParts_datetime",
                    new DatetimeFromPartsDatetimeGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralTime(new TimeOnly(8, 30, 0))
                    )
                ),
                new SelectItemGroup(
                    "datetimeFromParts_datetime_nullable_date",
                    new DatetimeFromPartsDatetimeNullableGroup(
                        new LiteralDateNullable(),
                        new LiteralTime(new TimeOnly(8, 30, 0))
                    )
                ),
                new SelectItemGroup(
                    "datetimeFromParts_datetime_nullable_time",
                    new DatetimeFromPartsDatetimeNullableGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralTimeNullable()
                    )
                ),
                new SelectItemGroup(
                    "datetimeFromParts_datetime_in_zone",
                    new DatetimeFromPartsDatetimeGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemGroup(
                    "datetimeFromParts_datetime_nullable_date_in_zone",
                    new DatetimeFromPartsDatetimeNullableGroup(
                        new LiteralDateNullable(),
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        zone: new LiteralString("Etc/GMT-3")
                    )
                ),
                new SelectItemGroup(
                    "datetimeFromParts_datetime_nullable_time_in_zone",
                    new DatetimeFromPartsDatetimeNullableGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralTimeNullable(),
                        zone: new LiteralString("America/New_York")
                    )
                ),
                new SelectItemGroup(
                    "datetimeFromParts_datetime_nullable_by_zone",
                    new DatetimeFromPartsDatetimeNullableGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemGroup(
                    "year_date",
                    new YearDateIntegerGroup(new LiteralDate(new DateOnly(2024, 12, 30)))
                ),
                new SelectItemGroup(
                    "year_date_nullable",
                    new YearDateIntegerNullableGroup(new LiteralDateNullable())
                ),
                new SelectItemGroup(
                    "year_datetime",
                    new YearDatetimeIntegerGroup(
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
                new SelectItemGroup(
                    "year_datetime_nullable",
                    new YearDatetimeIntegerNullableGroup(new LiteralDatetimeNullable())
                ),
                new SelectItemGroup(
                    "year_datetime_in_zone",
                    new YearDatetimeIntegerGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemGroup(
                    "year_datetime_nullable_in_zone",
                    new YearDatetimeIntegerNullableGroup(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemGroup(
                    "year_datetime_nullable_by_zone",
                    new YearDatetimeIntegerNullableGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemGroup(
                    "quarter_date",
                    new QuarterDateIntegerGroup(
                        new LiteralDate(new DateOnly(2024, 12, 30))
                    )
                ),
                new SelectItemGroup(
                    "quarter_date_nullable",
                    new QuarterDateIntegerNullableGroup(new LiteralDateNullable())
                ),
                new SelectItemGroup(
                    "quarter_datetime",
                    new QuarterDatetimeIntegerGroup(
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
                new SelectItemGroup(
                    "quarter_datetime_nullable",
                    new QuarterDatetimeIntegerNullableGroup(new LiteralDatetimeNullable())
                ),
                new SelectItemGroup(
                    "quarter_datetime_in_zone",
                    new QuarterDatetimeIntegerGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemGroup(
                    "quarter_datetime_nullable_in_zone",
                    new QuarterDatetimeIntegerNullableGroup(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemGroup(
                    "quarter_datetime_nullable_by_zone",
                    new QuarterDatetimeIntegerNullableGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemGroup(
                    "month_date",
                    new MonthDateIntegerGroup(new LiteralDate(new DateOnly(2024, 12, 30)))
                ),
                new SelectItemGroup(
                    "month_date_nullable",
                    new MonthDateIntegerNullableGroup(new LiteralDateNullable())
                ),
                new SelectItemGroup(
                    "month_datetime",
                    new MonthDatetimeIntegerGroup(
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
                new SelectItemGroup(
                    "month_datetime_nullable",
                    new MonthDatetimeIntegerNullableGroup(new LiteralDatetimeNullable())
                ),
                new SelectItemGroup(
                    "month_datetime_in_zone",
                    new MonthDatetimeIntegerGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemGroup(
                    "month_datetime_nullable_in_zone",
                    new MonthDatetimeIntegerNullableGroup(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemGroup(
                    "month_datetime_nullable_by_zone",
                    new MonthDatetimeIntegerNullableGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemGroup(
                    "day_date",
                    new DayDateIntegerGroup(new LiteralDate(new DateOnly(2024, 12, 30)))
                ),
                new SelectItemGroup(
                    "day_date_nullable",
                    new DayDateIntegerNullableGroup(new LiteralDateNullable())
                ),
                new SelectItemGroup(
                    "day_datetime",
                    new DayDatetimeIntegerGroup(
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
                new SelectItemGroup(
                    "day_datetime_nullable",
                    new DayDatetimeIntegerNullableGroup(new LiteralDatetimeNullable())
                ),
                new SelectItemGroup(
                    "day_datetime_in_zone",
                    new DayDatetimeIntegerGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemGroup(
                    "day_datetime_nullable_in_zone",
                    new DayDatetimeIntegerNullableGroup(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemGroup(
                    "day_datetime_nullable_by_zone",
                    new DayDatetimeIntegerNullableGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemGroup(
                    "dayOfWeek_date",
                    new DayOfWeekDateIntegerGroup(
                        new LiteralDate(new DateOnly(2024, 12, 30))
                    )
                ),
                new SelectItemGroup(
                    "dayOfWeek_date_nullable",
                    new DayOfWeekDateIntegerNullableGroup(new LiteralDateNullable())
                ),
                new SelectItemGroup(
                    "dayOfWeek_datetime",
                    new DayOfWeekDatetimeIntegerGroup(
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
                new SelectItemGroup(
                    "dayOfWeek_datetime_nullable",
                    new DayOfWeekDatetimeIntegerNullableGroup(
                        new LiteralDatetimeNullable()
                    )
                ),
                new SelectItemGroup(
                    "dayOfWeek_datetime_in_zone",
                    new DayOfWeekDatetimeIntegerGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemGroup(
                    "dayOfWeek_datetime_nullable_in_zone",
                    new DayOfWeekDatetimeIntegerNullableGroup(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemGroup(
                    "dayOfWeek_datetime_nullable_by_zone",
                    new DayOfWeekDatetimeIntegerNullableGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemGroup(
                    "dayOfYear_date",
                    new DayOfYearDateIntegerGroup(
                        new LiteralDate(new DateOnly(2024, 12, 30))
                    )
                ),
                new SelectItemGroup(
                    "dayOfYear_date_nullable",
                    new DayOfYearDateIntegerNullableGroup(new LiteralDateNullable())
                ),
                new SelectItemGroup(
                    "dayOfYear_datetime",
                    new DayOfYearDatetimeIntegerGroup(
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
                new SelectItemGroup(
                    "dayOfYear_datetime_nullable",
                    new DayOfYearDatetimeIntegerNullableGroup(
                        new LiteralDatetimeNullable()
                    )
                ),
                new SelectItemGroup(
                    "dayOfYear_datetime_in_zone",
                    new DayOfYearDatetimeIntegerGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemGroup(
                    "dayOfYear_datetime_nullable_in_zone",
                    new DayOfYearDatetimeIntegerNullableGroup(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemGroup(
                    "dayOfYear_datetime_nullable_by_zone",
                    new DayOfYearDatetimeIntegerNullableGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemGroup(
                    "week_date",
                    new WeekDateIntegerGroup(new LiteralDate(new DateOnly(2024, 12, 30)))
                ),
                new SelectItemGroup(
                    "week_date_nullable",
                    new WeekDateIntegerNullableGroup(new LiteralDateNullable())
                ),
                new SelectItemGroup(
                    "week_datetime",
                    new WeekDatetimeIntegerGroup(
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
                new SelectItemGroup(
                    "week_datetime_nullable",
                    new WeekDatetimeIntegerNullableGroup(new LiteralDatetimeNullable())
                ),
                new SelectItemGroup(
                    "week_datetime_in_zone",
                    new WeekDatetimeIntegerGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemGroup(
                    "week_datetime_nullable_in_zone",
                    new WeekDatetimeIntegerNullableGroup(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemGroup(
                    "week_datetime_nullable_by_zone",
                    new WeekDatetimeIntegerNullableGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemGroup(
                    "weekYear_date",
                    new WeekYearDateIntegerGroup(
                        new LiteralDate(new DateOnly(2024, 12, 30))
                    )
                ),
                new SelectItemGroup(
                    "weekYear_date_nullable",
                    new WeekYearDateIntegerNullableGroup(new LiteralDateNullable())
                ),
                new SelectItemGroup(
                    "weekYear_datetime",
                    new WeekYearDatetimeIntegerGroup(
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
                new SelectItemGroup(
                    "weekYear_datetime_nullable",
                    new WeekYearDatetimeIntegerNullableGroup(
                        new LiteralDatetimeNullable()
                    )
                ),
                new SelectItemGroup(
                    "weekYear_datetime_in_zone",
                    new WeekYearDatetimeIntegerGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemGroup(
                    "weekYear_datetime_nullable_in_zone",
                    new WeekYearDatetimeIntegerNullableGroup(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemGroup(
                    "weekYear_datetime_nullable_by_zone",
                    new WeekYearDatetimeIntegerNullableGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemGroup(
                    "hour_time",
                    new HourTimeIntegerGroup(
                        new LiteralTime(new TimeOnly(18, 30, 15, 250))
                    )
                ),
                new SelectItemGroup(
                    "hour_time_nullable",
                    new HourTimeIntegerNullableGroup(new LiteralTimeNullable())
                ),
                new SelectItemGroup(
                    "hour_datetime",
                    new HourDatetimeIntegerGroup(
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
                new SelectItemGroup(
                    "hour_datetime_nullable",
                    new HourDatetimeIntegerNullableGroup(new LiteralDatetimeNullable())
                ),
                new SelectItemGroup(
                    "hour_datetime_in_zone",
                    new HourDatetimeIntegerGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemGroup(
                    "hour_datetime_nullable_in_zone",
                    new HourDatetimeIntegerNullableGroup(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemGroup(
                    "hour_datetime_nullable_by_zone",
                    new HourDatetimeIntegerNullableGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemGroup(
                    "minute_time",
                    new MinuteTimeIntegerGroup(
                        new LiteralTime(new TimeOnly(18, 30, 15, 250))
                    )
                ),
                new SelectItemGroup(
                    "minute_time_nullable",
                    new MinuteTimeIntegerNullableGroup(new LiteralTimeNullable())
                ),
                new SelectItemGroup(
                    "minute_datetime",
                    new MinuteDatetimeIntegerGroup(
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
                new SelectItemGroup(
                    "minute_datetime_nullable",
                    new MinuteDatetimeIntegerNullableGroup(new LiteralDatetimeNullable())
                ),
                new SelectItemGroup(
                    "minute_datetime_in_zone",
                    new MinuteDatetimeIntegerGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemGroup(
                    "minute_datetime_nullable_in_zone",
                    new MinuteDatetimeIntegerNullableGroup(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemGroup(
                    "minute_datetime_nullable_by_zone",
                    new MinuteDatetimeIntegerNullableGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemGroup(
                    "second_time",
                    new SecondTimeIntegerGroup(
                        new LiteralTime(new TimeOnly(18, 30, 15, 250))
                    )
                ),
                new SelectItemGroup(
                    "second_time_nullable",
                    new SecondTimeIntegerNullableGroup(new LiteralTimeNullable())
                ),
                new SelectItemGroup(
                    "second_datetime",
                    new SecondDatetimeIntegerGroup(
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
                new SelectItemGroup(
                    "second_datetime_nullable",
                    new SecondDatetimeIntegerNullableGroup(new LiteralDatetimeNullable())
                ),
                new SelectItemGroup(
                    "second_datetime_in_zone",
                    new SecondDatetimeIntegerGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemGroup(
                    "second_datetime_nullable_in_zone",
                    new SecondDatetimeIntegerNullableGroup(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemGroup(
                    "second_datetime_nullable_by_zone",
                    new SecondDatetimeIntegerNullableGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemGroup(
                    "microsecond_time",
                    new MicrosecondTimeIntegerGroup(
                        new LiteralTime(new TimeOnly(18, 30, 15, 250))
                    )
                ),
                new SelectItemGroup(
                    "microsecond_time_nullable",
                    new MicrosecondTimeIntegerNullableGroup(new LiteralTimeNullable())
                ),
                new SelectItemGroup(
                    "microsecond_datetime",
                    new MicrosecondDatetimeIntegerGroup(
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
                new SelectItemGroup(
                    "microsecond_datetime_nullable",
                    new MicrosecondDatetimeIntegerNullableGroup(
                        new LiteralDatetimeNullable()
                    )
                ),
                new SelectItemGroup(
                    "startOfYear_date",
                    new StartOfYearDateGroup(new LiteralDate(new DateOnly(2024, 8, 15)))
                ),
                new SelectItemGroup(
                    "startOfYear_date_nullable",
                    new StartOfYearDateNullableGroup(new LiteralDateNullable())
                ),
                new SelectItemGroup(
                    "startOfYear_datetime",
                    new StartOfYearDatetimeGroup(
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
                new SelectItemGroup(
                    "startOfYear_datetime_nullable",
                    new StartOfYearDatetimeNullableGroup(new LiteralDatetimeNullable())
                ),
                new SelectItemGroup(
                    "startOfYear_datetime_in_zone",
                    new StartOfYearDatetimeGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemGroup(
                    "startOfYear_datetime_nullable_in_zone",
                    new StartOfYearDatetimeNullableGroup(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemGroup(
                    "startOfYear_datetime_nullable_by_zone",
                    new StartOfYearDatetimeNullableGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemGroup(
                    "startOfQuarter_date",
                    new StartOfQuarterDateGroup(
                        new LiteralDate(new DateOnly(2024, 8, 15))
                    )
                ),
                new SelectItemGroup(
                    "startOfQuarter_date_nullable",
                    new StartOfQuarterDateNullableGroup(new LiteralDateNullable())
                ),
                new SelectItemGroup(
                    "startOfQuarter_datetime",
                    new StartOfQuarterDatetimeGroup(
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
                new SelectItemGroup(
                    "startOfQuarter_datetime_nullable",
                    new StartOfQuarterDatetimeNullableGroup(new LiteralDatetimeNullable())
                ),
                new SelectItemGroup(
                    "startOfQuarter_datetime_in_zone",
                    new StartOfQuarterDatetimeGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemGroup(
                    "startOfQuarter_datetime_nullable_in_zone",
                    new StartOfQuarterDatetimeNullableGroup(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemGroup(
                    "startOfQuarter_datetime_nullable_by_zone",
                    new StartOfQuarterDatetimeNullableGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemGroup(
                    "startOfMonth_date",
                    new StartOfMonthDateGroup(new LiteralDate(new DateOnly(2024, 8, 15)))
                ),
                new SelectItemGroup(
                    "startOfMonth_date_nullable",
                    new StartOfMonthDateNullableGroup(new LiteralDateNullable())
                ),
                new SelectItemGroup(
                    "startOfMonth_datetime",
                    new StartOfMonthDatetimeGroup(
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
                new SelectItemGroup(
                    "startOfMonth_datetime_nullable",
                    new StartOfMonthDatetimeNullableGroup(new LiteralDatetimeNullable())
                ),
                new SelectItemGroup(
                    "startOfMonth_datetime_in_zone",
                    new StartOfMonthDatetimeGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemGroup(
                    "startOfMonth_datetime_nullable_in_zone",
                    new StartOfMonthDatetimeNullableGroup(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemGroup(
                    "startOfMonth_datetime_nullable_by_zone",
                    new StartOfMonthDatetimeNullableGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemGroup(
                    "startOfWeek_date",
                    new StartOfWeekDateGroup(new LiteralDate(new DateOnly(2024, 8, 15)))
                ),
                new SelectItemGroup(
                    "startOfWeek_date_nullable",
                    new StartOfWeekDateNullableGroup(new LiteralDateNullable())
                ),
                new SelectItemGroup(
                    "startOfWeek_datetime",
                    new StartOfWeekDatetimeGroup(
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
                new SelectItemGroup(
                    "startOfWeek_datetime_nullable",
                    new StartOfWeekDatetimeNullableGroup(new LiteralDatetimeNullable())
                ),
                new SelectItemGroup(
                    "startOfWeek_datetime_in_zone",
                    new StartOfWeekDatetimeGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemGroup(
                    "startOfWeek_datetime_nullable_in_zone",
                    new StartOfWeekDatetimeNullableGroup(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemGroup(
                    "startOfWeek_datetime_nullable_by_zone",
                    new StartOfWeekDatetimeNullableGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemGroup(
                    "startOfDay_datetime",
                    new StartOfDayDatetimeGroup(
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
                new SelectItemGroup(
                    "startOfDay_datetime_nullable",
                    new StartOfDayDatetimeNullableGroup(new LiteralDatetimeNullable())
                ),
                new SelectItemGroup(
                    "startOfDay_datetime_in_zone",
                    new StartOfDayDatetimeGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemGroup(
                    "startOfDay_datetime_nullable_in_zone",
                    new StartOfDayDatetimeNullableGroup(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemGroup(
                    "startOfDay_datetime_nullable_by_zone",
                    new StartOfDayDatetimeNullableGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemGroup(
                    "startOfHour_time",
                    new StartOfHourTimeGroup(
                        new LiteralTime(new TimeOnly(18, 30, 15, 250))
                    )
                ),
                new SelectItemGroup(
                    "startOfHour_time_nullable",
                    new StartOfHourTimeNullableGroup(new LiteralTimeNullable())
                ),
                new SelectItemGroup(
                    "startOfHour_datetime",
                    new StartOfHourDatetimeGroup(
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
                new SelectItemGroup(
                    "startOfHour_datetime_nullable",
                    new StartOfHourDatetimeNullableGroup(new LiteralDatetimeNullable())
                ),
                new SelectItemGroup(
                    "startOfHour_datetime_in_zone",
                    new StartOfHourDatetimeGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemGroup(
                    "startOfHour_datetime_nullable_in_zone",
                    new StartOfHourDatetimeNullableGroup(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemGroup(
                    "startOfHour_datetime_nullable_by_zone",
                    new StartOfHourDatetimeNullableGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemGroup(
                    "startOfMinute_time",
                    new StartOfMinuteTimeGroup(
                        new LiteralTime(new TimeOnly(18, 30, 15, 250))
                    )
                ),
                new SelectItemGroup(
                    "startOfMinute_time_nullable",
                    new StartOfMinuteTimeNullableGroup(new LiteralTimeNullable())
                ),
                new SelectItemGroup(
                    "startOfMinute_datetime",
                    new StartOfMinuteDatetimeGroup(
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
                new SelectItemGroup(
                    "startOfMinute_datetime_nullable",
                    new StartOfMinuteDatetimeNullableGroup(new LiteralDatetimeNullable())
                ),
                new SelectItemGroup(
                    "startOfMinute_datetime_in_zone",
                    new StartOfMinuteDatetimeGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemGroup(
                    "startOfMinute_datetime_nullable_in_zone",
                    new StartOfMinuteDatetimeNullableGroup(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemGroup(
                    "startOfMinute_datetime_nullable_by_zone",
                    new StartOfMinuteDatetimeNullableGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemGroup(
                    "startOfSecond_time",
                    new StartOfSecondTimeGroup(
                        new LiteralTime(new TimeOnly(18, 30, 15, 250))
                    )
                ),
                new SelectItemGroup(
                    "startOfSecond_time_nullable",
                    new StartOfSecondTimeNullableGroup(new LiteralTimeNullable())
                ),
                new SelectItemGroup(
                    "startOfSecond_datetime",
                    new StartOfSecondDatetimeGroup(
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
                new SelectItemGroup(
                    "startOfSecond_datetime_nullable",
                    new StartOfSecondDatetimeNullableGroup(new LiteralDatetimeNullable())
                ),
                new SelectItemGroup(
                    "asString_string_of_integer",
                    new AsStringDecimalStringGroup(new LiteralInteger(7))
                ),
                new SelectItemGroup(
                    "asString_string_nullable_of_integer",
                    new AsStringDecimalStringNullableGroup(new LiteralIntegerNullable())
                ),
                new SelectItemGroup(
                    "asString_string_of_decimal",
                    new AsStringDecimalStringGroup(new LiteralDecimal(2.5m))
                ),
                new SelectItemGroup(
                    "asString_string_nullable_of_decimal",
                    new AsStringDecimalStringNullableGroup(new LiteralDecimalNullable())
                ),
                new SelectItemGroup(
                    "asString_string_of_boolean",
                    new AsStringBooleanStringGroup(new LiteralBoolean(true))
                ),
                new SelectItemGroup(
                    "asString_string_nullable_of_boolean",
                    new AsStringBooleanStringNullableGroup(new LiteralBooleanNullable())
                ),
                new SelectItemGroup(
                    "asString_string_of_date",
                    new AsStringDateStringGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31))
                    )
                ),
                new SelectItemGroup(
                    "asString_string_nullable_of_date",
                    new AsStringDateStringNullableGroup(new LiteralDateNullable())
                ),
                new SelectItemGroup(
                    "asString_string_of_time",
                    new AsStringTimeStringGroup(new LiteralTime(new TimeOnly(8, 30, 0)))
                ),
                new SelectItemGroup(
                    "asString_string_nullable_of_time",
                    new AsStringTimeStringNullableGroup(new LiteralTimeNullable())
                ),
                new SelectItemGroup(
                    "asString_string_of_datetime",
                    new AsStringDatetimeStringGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemGroup(
                    "asString_string_nullable_of_datetime",
                    new AsStringDatetimeStringNullableGroup(new LiteralDatetimeNullable())
                ),
                new SelectItemGroup(
                    "asString_string_of_uuid",
                    new AsStringUuidStringGroup(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8]))
                    )
                ),
                new SelectItemGroup(
                    "asString_string_nullable_of_uuid",
                    new AsStringUuidStringNullableGroup(new LiteralUuidNullable())
                ),
                new SelectItemGroup(
                    "asDate_date",
                    new AsDateDatetimeDateGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemGroup(
                    "asDate_date_nullable",
                    new AsDateDatetimeDateNullableGroup(new LiteralDatetimeNullable())
                ),
                new SelectItemGroup(
                    "asTime_time",
                    new AsTimeDatetimeTimeGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemGroup(
                    "asTime_time_nullable",
                    new AsTimeDatetimeTimeNullableGroup(new LiteralDatetimeNullable())
                ),
                new SelectItemGroup(
                    "asDate_date_in_zone",
                    new AsDateDatetimeDateGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemGroup(
                    "asDate_date_nullable_in_zone",
                    new AsDateDatetimeDateNullableGroup(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemGroup(
                    "asDate_date_nullable_by_zone",
                    new AsDateDatetimeDateNullableGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemGroup(
                    "asDate_date_nullable_in_nullable_zone",
                    new AsDateDatetimeDateNullableGroup(
                        new LiteralDatetimeNullable(),
                        zone: new ParamStringNullable("zone")
                    )
                ),
                new SelectItemGroup(
                    "asTime_time_in_zone",
                    new AsTimeDatetimeTimeGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemGroup(
                    "asTime_time_nullable_in_zone",
                    new AsTimeDatetimeTimeNullableGroup(
                        new LiteralDatetimeNullable(),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemGroup(
                    "asTime_time_nullable_by_zone",
                    new AsTimeDatetimeTimeNullableGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemGroup(
                    "asTime_time_nullable_in_nullable_zone",
                    new AsTimeDatetimeTimeNullableGroup(
                        new LiteralDatetimeNullable(),
                        zone: new ParamStringNullable("zone")
                    )
                ),
                new SelectItemGroup(
                    "asDatetime_datetime",
                    new AsDatetimeDateDatetimeGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31))
                    )
                ),
                new SelectItemGroup(
                    "asDatetime_datetime_nullable",
                    new AsDatetimeDateDatetimeNullableGroup(new LiteralDateNullable())
                ),
                new SelectItemGroup(
                    "asDatetime_datetime_in_zone",
                    new AsDatetimeDateDatetimeGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        zone: new LiteralString("Europe/Moscow")
                    )
                ),
                new SelectItemGroup(
                    "asDatetime_datetime_nullable_in_zone",
                    new AsDatetimeDateDatetimeNullableGroup(
                        new LiteralDateNullable(),
                        zone: new LiteralString("Asia/Kolkata")
                    )
                ),
                new SelectItemGroup(
                    "asDatetime_datetime_nullable_by_zone",
                    new AsDatetimeDateDatetimeNullableGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        zone: new LiteralStringNullable()
                    )
                ),
                new SelectItemGroup(
                    "asInteger_integer_of_string",
                    new AsIntegerIntegerGroup(new LiteralString("7"))
                ),
                new SelectItemGroup(
                    "asInteger_integer_nullable_of_string",
                    new AsIntegerIntegerNullableGroup(new LiteralStringNullable())
                ),
                new SelectItemGroup(
                    "tryAsInteger_integer_nullable",
                    new TryAsIntegerGroup(new LiteralString("7"))
                ),
                new SelectItemGroup(
                    "tryAsInteger_integer_nullable_of_null",
                    new TryAsIntegerGroup(new LiteralStringNullable())
                ),
                new SelectItemGroup(
                    "asDecimal_decimal_of_string",
                    new AsDecimalStringDecimalGroup(new LiteralString("2.5"))
                ),
                new SelectItemGroup(
                    "asDecimal_decimal_nullable_of_string",
                    new AsDecimalStringDecimalNullableGroup(new LiteralStringNullable())
                ),
                new SelectItemGroup(
                    "asDecimal_decimal_of_integer",
                    new AsDecimalIntegerDecimalGroup(new LiteralInteger(7))
                ),
                new SelectItemGroup(
                    "asDecimal_decimal_nullable_of_integer",
                    new AsDecimalIntegerDecimalNullableGroup(new LiteralIntegerNullable())
                ),
                new SelectItemGroup(
                    "asNullable_integer_nullable",
                    new AsNullableIntegerNullableGroup(new LiteralInteger(7))
                ),
                new SelectItemGroup(
                    "asNullable_decimal_nullable",
                    new AsNullableDecimalNullableGroup(new LiteralDecimal(2.5m))
                ),
                new SelectItemGroup(
                    "asNullable_string_nullable",
                    new AsNullableStringNullableGroup(new LiteralString("a"))
                ),
                new SelectItemGroup(
                    "asNullable_boolean_nullable",
                    new AsNullableBooleanNullableGroup(new LiteralBoolean(true))
                ),
                new SelectItemGroup(
                    "asNullable_date_nullable",
                    new AsNullableDateNullableGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31))
                    )
                ),
                new SelectItemGroup(
                    "asNullable_time_nullable",
                    new AsNullableTimeNullableGroup(
                        new LiteralTime(new TimeOnly(8, 30, 0))
                    )
                ),
                new SelectItemGroup(
                    "asNullable_datetime_nullable",
                    new AsNullableDatetimeNullableGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemGroup(
                    "asNullable_uuid_nullable",
                    new AsNullableUuidNullableGroup(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8]))
                    )
                ),
                new SelectItemGroup(
                    "tryAsDecimal_decimal_nullable",
                    new TryAsDecimalGroup(new LiteralString("2.5"))
                ),
                new SelectItemGroup(
                    "tryAsDecimal_decimal_nullable_of_null",
                    new TryAsDecimalGroup(new LiteralStringNullable())
                ),
                new SelectItemGroup(
                    "asBoolean_boolean_of_string",
                    new AsBooleanBooleanGroup(new LiteralString("true"))
                ),
                new SelectItemGroup(
                    "asBoolean_boolean_nullable_of_string",
                    new AsBooleanBooleanNullableGroup(new LiteralStringNullable())
                ),
                new SelectItemGroup(
                    "tryAsBoolean_boolean_nullable",
                    new TryAsBooleanGroup(new LiteralString("true"))
                ),
                new SelectItemGroup(
                    "tryAsBoolean_boolean_nullable_of_null",
                    new TryAsBooleanGroup(new LiteralStringNullable())
                ),
                new SelectItemGroup(
                    "asDate_date_of_string",
                    new AsDateStringDateGroup(new LiteralString("2024-01-31"))
                ),
                new SelectItemGroup(
                    "asDate_date_nullable_of_string",
                    new AsDateStringDateNullableGroup(new LiteralStringNullable())
                ),
                new SelectItemGroup(
                    "tryAsDate_date_nullable",
                    new TryAsDateGroup(new LiteralString("2024-01-31"))
                ),
                new SelectItemGroup(
                    "tryAsDate_date_nullable_of_null",
                    new TryAsDateGroup(new LiteralStringNullable())
                ),
                new SelectItemGroup(
                    "asTime_time_of_string",
                    new AsTimeStringTimeGroup(new LiteralString("08:30:00"))
                ),
                new SelectItemGroup(
                    "asTime_time_nullable_of_string",
                    new AsTimeStringTimeNullableGroup(new LiteralStringNullable())
                ),
                new SelectItemGroup(
                    "tryAsTime_time_nullable",
                    new TryAsTimeGroup(new LiteralString("08:30:00"))
                ),
                new SelectItemGroup(
                    "tryAsTime_time_nullable_of_null",
                    new TryAsTimeGroup(new LiteralStringNullable())
                ),
                new SelectItemGroup(
                    "asDatetime_datetime_of_string",
                    new AsDatetimeStringDatetimeGroup(
                        new LiteralString("2024-01-31T08:30:00Z")
                    )
                ),
                new SelectItemGroup(
                    "asDatetime_datetime_nullable_of_string",
                    new AsDatetimeStringDatetimeNullableGroup(new LiteralStringNullable())
                ),
                new SelectItemGroup(
                    "tryAsDatetime_datetime_nullable",
                    new TryAsDatetimeGroup(new LiteralString("2024-01-31T08:30:00Z"))
                ),
                new SelectItemGroup(
                    "tryAsDatetime_datetime_nullable_of_null",
                    new TryAsDatetimeGroup(new LiteralStringNullable())
                ),
                new SelectItemGroup(
                    "asUuid_uuid_of_string",
                    new AsUuidUuidGroup(
                        new LiteralString("3f2a6c1e-8b4d-4e2a-9c1f-1a2b3c4d5e6f")
                    )
                ),
                new SelectItemGroup(
                    "asUuid_uuid_nullable_of_string",
                    new AsUuidUuidNullableGroup(new LiteralStringNullable())
                ),
                new SelectItemGroup(
                    "tryAsUuid_uuid_nullable",
                    new TryAsUuidGroup(
                        new LiteralString("3f2a6c1e-8b4d-4e2a-9c1f-1a2b3c4d5e6f")
                    )
                ),
                new SelectItemGroup(
                    "tryAsUuid_uuid_nullable_of_null",
                    new TryAsUuidGroup(new LiteralStringNullable())
                ),
                new SelectItemGroup(
                    "and_boolean",
                    new AndGroup([new LiteralBoolean(true), new LiteralBoolean(false)])
                ),
                new SelectItemGroup(
                    "or_boolean",
                    new OrGroup([new LiteralBoolean(true), new LiteralBoolean(false)])
                ),
                new SelectItemGroup(
                    "not_boolean",
                    new NotGroup(new LiteralBoolean(true))
                ),
                new SelectItemGroup(
                    "equal_boolean_decimal",
                    new EqualDecimalGroup(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m)
                    )
                ),
                new SelectItemGroup(
                    "equal_boolean_string",
                    new EqualStringGroup(new LiteralString("a"), new LiteralString("b"))
                ),
                new SelectItemGroup(
                    "equal_boolean_boolean",
                    new EqualBooleanGroup(
                        new LiteralBoolean(true),
                        new LiteralBoolean(false)
                    )
                ),
                new SelectItemGroup(
                    "equal_boolean_date",
                    new EqualDateGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDate(new DateOnly(2024, 3, 1))
                    )
                ),
                new SelectItemGroup(
                    "equal_boolean_time",
                    new EqualTimeGroup(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralTime(new TimeOnly(17, 45, 30, 500))
                    )
                ),
                new SelectItemGroup(
                    "equal_boolean_datetime",
                    new EqualDatetimeGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemGroup(
                    "equal_boolean_uuid",
                    new EqualUuidGroup(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralUuid(new Guid(2, 0, 0, new byte[8]))
                    )
                ),
                new SelectItemGroup(
                    "notEqual_boolean_decimal",
                    new NotEqualDecimalGroup(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m)
                    )
                ),
                new SelectItemGroup(
                    "notEqual_boolean_string",
                    new NotEqualStringGroup(
                        new LiteralString("a"),
                        new LiteralString("b")
                    )
                ),
                new SelectItemGroup(
                    "notEqual_boolean_boolean",
                    new NotEqualBooleanGroup(
                        new LiteralBoolean(true),
                        new LiteralBoolean(false)
                    )
                ),
                new SelectItemGroup(
                    "notEqual_boolean_date",
                    new NotEqualDateGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDate(new DateOnly(2024, 3, 1))
                    )
                ),
                new SelectItemGroup(
                    "notEqual_boolean_time",
                    new NotEqualTimeGroup(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralTime(new TimeOnly(17, 45, 30, 500))
                    )
                ),
                new SelectItemGroup(
                    "notEqual_boolean_datetime",
                    new NotEqualDatetimeGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemGroup(
                    "notEqual_boolean_uuid",
                    new NotEqualUuidGroup(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralUuid(new Guid(2, 0, 0, new byte[8]))
                    )
                ),
                new SelectItemGroup(
                    "greaterThan_boolean_decimal",
                    new GreaterThanDecimalGroup(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m)
                    )
                ),
                new SelectItemGroup(
                    "greaterThan_boolean_string",
                    new GreaterThanStringGroup(
                        new LiteralString("a"),
                        new LiteralString("b")
                    )
                ),
                new SelectItemGroup(
                    "greaterThan_boolean_boolean",
                    new GreaterThanBooleanGroup(
                        new LiteralBoolean(true),
                        new LiteralBoolean(false)
                    )
                ),
                new SelectItemGroup(
                    "greaterThan_boolean_date",
                    new GreaterThanDateGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDate(new DateOnly(2024, 3, 1))
                    )
                ),
                new SelectItemGroup(
                    "greaterThan_boolean_time",
                    new GreaterThanTimeGroup(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralTime(new TimeOnly(17, 45, 30, 500))
                    )
                ),
                new SelectItemGroup(
                    "greaterThan_boolean_datetime",
                    new GreaterThanDatetimeGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemGroup(
                    "greaterThan_boolean_uuid",
                    new GreaterThanUuidGroup(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralUuid(new Guid(2, 0, 0, new byte[8]))
                    )
                ),
                new SelectItemGroup(
                    "lessThan_boolean_decimal",
                    new LessThanDecimalGroup(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m)
                    )
                ),
                new SelectItemGroup(
                    "lessThan_boolean_string",
                    new LessThanStringGroup(
                        new LiteralString("a"),
                        new LiteralString("b")
                    )
                ),
                new SelectItemGroup(
                    "lessThan_boolean_boolean",
                    new LessThanBooleanGroup(
                        new LiteralBoolean(true),
                        new LiteralBoolean(false)
                    )
                ),
                new SelectItemGroup(
                    "lessThan_boolean_date",
                    new LessThanDateGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDate(new DateOnly(2024, 3, 1))
                    )
                ),
                new SelectItemGroup(
                    "lessThan_boolean_time",
                    new LessThanTimeGroup(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralTime(new TimeOnly(17, 45, 30, 500))
                    )
                ),
                new SelectItemGroup(
                    "lessThan_boolean_datetime",
                    new LessThanDatetimeGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemGroup(
                    "lessThan_boolean_uuid",
                    new LessThanUuidGroup(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralUuid(new Guid(2, 0, 0, new byte[8]))
                    )
                ),
                new SelectItemGroup(
                    "greaterThanOrEqual_boolean_decimal",
                    new GreaterThanOrEqualDecimalGroup(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m)
                    )
                ),
                new SelectItemGroup(
                    "greaterThanOrEqual_boolean_string",
                    new GreaterThanOrEqualStringGroup(
                        new LiteralString("a"),
                        new LiteralString("b")
                    )
                ),
                new SelectItemGroup(
                    "greaterThanOrEqual_boolean_boolean",
                    new GreaterThanOrEqualBooleanGroup(
                        new LiteralBoolean(true),
                        new LiteralBoolean(false)
                    )
                ),
                new SelectItemGroup(
                    "greaterThanOrEqual_boolean_date",
                    new GreaterThanOrEqualDateGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDate(new DateOnly(2024, 3, 1))
                    )
                ),
                new SelectItemGroup(
                    "greaterThanOrEqual_boolean_time",
                    new GreaterThanOrEqualTimeGroup(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralTime(new TimeOnly(17, 45, 30, 500))
                    )
                ),
                new SelectItemGroup(
                    "greaterThanOrEqual_boolean_datetime",
                    new GreaterThanOrEqualDatetimeGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemGroup(
                    "greaterThanOrEqual_boolean_uuid",
                    new GreaterThanOrEqualUuidGroup(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralUuid(new Guid(2, 0, 0, new byte[8]))
                    )
                ),
                new SelectItemGroup(
                    "lessThanOrEqual_boolean_decimal",
                    new LessThanOrEqualDecimalGroup(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m)
                    )
                ),
                new SelectItemGroup(
                    "lessThanOrEqual_boolean_string",
                    new LessThanOrEqualStringGroup(
                        new LiteralString("a"),
                        new LiteralString("b")
                    )
                ),
                new SelectItemGroup(
                    "lessThanOrEqual_boolean_boolean",
                    new LessThanOrEqualBooleanGroup(
                        new LiteralBoolean(true),
                        new LiteralBoolean(false)
                    )
                ),
                new SelectItemGroup(
                    "lessThanOrEqual_boolean_date",
                    new LessThanOrEqualDateGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDate(new DateOnly(2024, 3, 1))
                    )
                ),
                new SelectItemGroup(
                    "lessThanOrEqual_boolean_time",
                    new LessThanOrEqualTimeGroup(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralTime(new TimeOnly(17, 45, 30, 500))
                    )
                ),
                new SelectItemGroup(
                    "lessThanOrEqual_boolean_datetime",
                    new LessThanOrEqualDatetimeGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemGroup(
                    "lessThanOrEqual_boolean_uuid",
                    new LessThanOrEqualUuidGroup(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralUuid(new Guid(2, 0, 0, new byte[8]))
                    )
                ),
                new SelectItemGroup(
                    "in_boolean_decimal",
                    new InDecimalGroup(
                        new LiteralDecimal(2.5m),
                        new ListLiteralDecimal([2.5m, 0.1m])
                    )
                ),
                new SelectItemGroup(
                    "in_boolean_string",
                    new InStringGroup(
                        new LiteralString("a"),
                        new ListLiteralString(["a", "b"])
                    )
                ),
                new SelectItemGroup(
                    "in_boolean_boolean",
                    new InBooleanGroup(
                        new LiteralBoolean(true),
                        new ListLiteralBoolean([true, false])
                    )
                ),
                new SelectItemGroup(
                    "in_boolean_date",
                    new InDateGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new ListLiteralDate([
                            new DateOnly(2024, 1, 31),
                            new DateOnly(2024, 3, 1),
                        ])
                    )
                ),
                new SelectItemGroup(
                    "in_boolean_time",
                    new InTimeGroup(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new ListLiteralTime([
                            new TimeOnly(8, 30, 0),
                            new TimeOnly(17, 45, 30, 500),
                        ])
                    )
                ),
                new SelectItemGroup(
                    "in_boolean_datetime",
                    new InDatetimeGroup(
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
                new SelectItemGroup(
                    "in_boolean_uuid",
                    new InUuidGroup(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new ListLiteralUuid([
                            new Guid(1, 0, 0, new byte[8]),
                            new Guid(2, 0, 0, new byte[8]),
                        ])
                    )
                ),
                new SelectItemGroup(
                    "if_integer",
                    new IfIntegerGroup(
                        new LiteralInteger(7),
                        new LiteralInteger(-3),
                        new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "if_integer_nullable",
                    new IfIntegerNullableGroup(
                        new LiteralIntegerNullable(),
                        new LiteralInteger(7),
                        new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "coalesce_integer",
                    new CoalesceIntegerGroup([
                        new LiteralIntegerNullable(),
                        new LiteralInteger(7),
                    ])
                ),
                new SelectItemGroup(
                    "coalesce_integer_nullable",
                    new CoalesceIntegerNullableGroup([
                        new LiteralIntegerNullable(),
                        new LiteralIntegerNullable(),
                    ])
                ),
                new SelectItemGroup(
                    "if_decimal",
                    new IfDecimalGroup(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m),
                        new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "if_decimal_nullable",
                    new IfDecimalNullableGroup(
                        new LiteralDecimalNullable(),
                        new LiteralDecimal(2.5m),
                        new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "coalesce_decimal",
                    new CoalesceDecimalGroup([
                        new LiteralDecimalNullable(),
                        new LiteralDecimal(2.5m),
                    ])
                ),
                new SelectItemGroup(
                    "coalesce_decimal_nullable",
                    new CoalesceDecimalNullableGroup([
                        new LiteralDecimalNullable(),
                        new LiteralDecimalNullable(),
                    ])
                ),
                new SelectItemGroup(
                    "if_string",
                    new IfStringGroup(
                        new LiteralString("a"),
                        new LiteralString("b"),
                        new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "if_string_nullable",
                    new IfStringNullableGroup(
                        new LiteralStringNullable(),
                        new LiteralString("a"),
                        new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "coalesce_string",
                    new CoalesceStringGroup([
                        new LiteralStringNullable(),
                        new LiteralString("a"),
                    ])
                ),
                new SelectItemGroup(
                    "coalesce_string_nullable",
                    new CoalesceStringNullableGroup([
                        new LiteralStringNullable(),
                        new LiteralStringNullable(),
                    ])
                ),
                new SelectItemGroup(
                    "if_boolean",
                    new IfBooleanGroup(
                        new LiteralBoolean(true),
                        new LiteralBoolean(false),
                        new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "if_boolean_nullable",
                    new IfBooleanNullableGroup(
                        new LiteralBooleanNullable(),
                        new LiteralBoolean(true),
                        new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "coalesce_boolean",
                    new CoalesceBooleanGroup([
                        new LiteralBooleanNullable(),
                        new LiteralBoolean(true),
                    ])
                ),
                new SelectItemGroup(
                    "coalesce_boolean_nullable",
                    new CoalesceBooleanNullableGroup([
                        new LiteralBooleanNullable(),
                        new LiteralBooleanNullable(),
                    ])
                ),
                new SelectItemGroup(
                    "if_date",
                    new IfDateGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDate(new DateOnly(2024, 3, 1)),
                        new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "if_date_nullable",
                    new IfDateNullableGroup(
                        new LiteralDateNullable(),
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "coalesce_date",
                    new CoalesceDateGroup([
                        new LiteralDateNullable(),
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                    ])
                ),
                new SelectItemGroup(
                    "coalesce_date_nullable",
                    new CoalesceDateNullableGroup([
                        new LiteralDateNullable(),
                        new LiteralDateNullable(),
                    ])
                ),
                new SelectItemGroup(
                    "if_time",
                    new IfTimeGroup(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralTime(new TimeOnly(17, 45, 30, 500)),
                        new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "if_time_nullable",
                    new IfTimeNullableGroup(
                        new LiteralTimeNullable(),
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "coalesce_time",
                    new CoalesceTimeGroup([
                        new LiteralTimeNullable(),
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                    ])
                ),
                new SelectItemGroup(
                    "coalesce_time_nullable",
                    new CoalesceTimeNullableGroup([
                        new LiteralTimeNullable(),
                        new LiteralTimeNullable(),
                    ])
                ),
                new SelectItemGroup(
                    "if_datetime",
                    new IfDatetimeGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        ),
                        new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "if_datetime_nullable",
                    new IfDatetimeNullableGroup(
                        new LiteralDatetimeNullable(),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "coalesce_datetime",
                    new CoalesceDatetimeGroup([
                        new LiteralDatetimeNullable(),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                    ])
                ),
                new SelectItemGroup(
                    "coalesce_datetime_nullable",
                    new CoalesceDatetimeNullableGroup([
                        new LiteralDatetimeNullable(),
                        new LiteralDatetimeNullable(),
                    ])
                ),
                new SelectItemGroup(
                    "if_uuid",
                    new IfUuidGroup(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralUuid(new Guid(2, 0, 0, new byte[8])),
                        new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "if_uuid_nullable",
                    new IfUuidNullableGroup(
                        new LiteralUuidNullable(),
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "coalesce_uuid",
                    new CoalesceUuidGroup([
                        new LiteralUuidNullable(),
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                    ])
                ),
                new SelectItemGroup(
                    "coalesce_uuid_nullable",
                    new CoalesceUuidNullableGroup([
                        new LiteralUuidNullable(),
                        new LiteralUuidNullable(),
                    ])
                ),
                new SelectItemGroup(
                    "nullIfEqual_integer_nullable",
                    new NullIfEqualIntegerNullableGroup(
                        new LiteralInteger(7),
                        new LiteralInteger(-3)
                    )
                ),
                new SelectItemGroup(
                    "nullIfEqual_decimal_nullable",
                    new NullIfEqualDecimalNullableGroup(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m)
                    )
                ),
                new SelectItemGroup(
                    "nullIfEqual_string_nullable",
                    new NullIfEqualStringNullableGroup(
                        new LiteralString("a"),
                        new LiteralString("b")
                    )
                ),
                new SelectItemGroup(
                    "nullIfEqual_boolean_nullable",
                    new NullIfEqualBooleanNullableGroup(
                        new LiteralBoolean(true),
                        new LiteralBoolean(false)
                    )
                ),
                new SelectItemGroup(
                    "nullIfEqual_date_nullable",
                    new NullIfEqualDateNullableGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDate(new DateOnly(2024, 3, 1))
                    )
                ),
                new SelectItemGroup(
                    "nullIfEqual_time_nullable",
                    new NullIfEqualTimeNullableGroup(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralTime(new TimeOnly(17, 45, 30, 500))
                    )
                ),
                new SelectItemGroup(
                    "nullIfEqual_datetime_nullable",
                    new NullIfEqualDatetimeNullableGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemGroup(
                    "nullIfEqual_uuid_nullable",
                    new NullIfEqualUuidNullableGroup(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralUuid(new Guid(2, 0, 0, new byte[8]))
                    )
                ),
                new SelectItemGroup(
                    "nullIfEqual_integer_nullable_decimal_right",
                    new NullIfEqualIntegerNullableGroup(
                        new LiteralInteger(7),
                        new LiteralDecimal(2.5m)
                    )
                ),
                new SelectItemGroup(
                    "nullIfEqual_decimal_nullable_integer_right",
                    new NullIfEqualDecimalNullableGroup(
                        new LiteralDecimal(2.5m),
                        new LiteralInteger(7)
                    )
                ),
                new SelectItemGroup(
                    "nullIfEqual_string_nullable_null_left",
                    new NullIfEqualStringNullableGroup(
                        new LiteralStringNullable(),
                        new LiteralString("a")
                    )
                ),
                new SelectItemGroup(
                    "nullIfEqual_string_nullable_null_right",
                    new NullIfEqualStringNullableGroup(
                        new LiteralString("a"),
                        new LiteralStringNullable()
                    )
                ),
                new SelectItemGroup(
                    "greatest_integer",
                    new GreatestIntegerGroup([
                        new LiteralInteger(7),
                        new LiteralInteger(-3),
                    ])
                ),
                new SelectItemGroup(
                    "greatest_integer_nullable",
                    new GreatestIntegerNullableGroup([
                        new LiteralInteger(7),
                        new LiteralIntegerNullable(),
                        new LiteralInteger(-3),
                    ])
                ),
                new SelectItemGroup(
                    "greatest_decimal",
                    new GreatestDecimalGroup([
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m),
                    ])
                ),
                new SelectItemGroup(
                    "greatest_decimal_nullable",
                    new GreatestDecimalNullableGroup([
                        new LiteralDecimal(2.5m),
                        new LiteralDecimalNullable(),
                        new LiteralDecimal(0.1m),
                    ])
                ),
                new SelectItemGroup(
                    "greatest_string",
                    new GreatestStringGroup([
                        new LiteralString("a"),
                        new LiteralString("b"),
                    ])
                ),
                new SelectItemGroup(
                    "greatest_string_nullable",
                    new GreatestStringNullableGroup([
                        new LiteralString("a"),
                        new LiteralStringNullable(),
                        new LiteralString("b"),
                    ])
                ),
                new SelectItemGroup(
                    "greatest_boolean",
                    new GreatestBooleanGroup([
                        new LiteralBoolean(true),
                        new LiteralBoolean(false),
                    ])
                ),
                new SelectItemGroup(
                    "greatest_boolean_nullable",
                    new GreatestBooleanNullableGroup([
                        new LiteralBoolean(true),
                        new LiteralBooleanNullable(),
                        new LiteralBoolean(false),
                    ])
                ),
                new SelectItemGroup(
                    "greatest_date",
                    new GreatestDateGroup([
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDate(new DateOnly(2024, 3, 1)),
                    ])
                ),
                new SelectItemGroup(
                    "greatest_date_nullable",
                    new GreatestDateNullableGroup([
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDateNullable(),
                        new LiteralDate(new DateOnly(2024, 3, 1)),
                    ])
                ),
                new SelectItemGroup(
                    "greatest_time",
                    new GreatestTimeGroup([
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralTime(new TimeOnly(17, 45, 30, 500)),
                    ])
                ),
                new SelectItemGroup(
                    "greatest_time_nullable",
                    new GreatestTimeNullableGroup([
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralTimeNullable(),
                        new LiteralTime(new TimeOnly(17, 45, 30, 500)),
                    ])
                ),
                new SelectItemGroup(
                    "greatest_datetime",
                    new GreatestDatetimeGroup([
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        ),
                    ])
                ),
                new SelectItemGroup(
                    "greatest_datetime_nullable",
                    new GreatestDatetimeNullableGroup([
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDatetimeNullable(),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        ),
                    ])
                ),
                new SelectItemGroup(
                    "greatest_uuid",
                    new GreatestUuidGroup([
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralUuid(new Guid(2, 0, 0, new byte[8])),
                    ])
                ),
                new SelectItemGroup(
                    "greatest_uuid_nullable",
                    new GreatestUuidNullableGroup([
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralUuidNullable(),
                        new LiteralUuid(new Guid(2, 0, 0, new byte[8])),
                    ])
                ),
                new SelectItemGroup(
                    "greatest_decimal_of_integer_and_decimal",
                    new GreatestDecimalGroup([
                        new LiteralInteger(7),
                        new LiteralDecimal(2.5m),
                    ])
                ),
                new SelectItemGroup(
                    "least_integer",
                    new LeastIntegerGroup([new LiteralInteger(7), new LiteralInteger(-3)])
                ),
                new SelectItemGroup(
                    "least_integer_nullable",
                    new LeastIntegerNullableGroup([
                        new LiteralInteger(7),
                        new LiteralIntegerNullable(),
                        new LiteralInteger(-3),
                    ])
                ),
                new SelectItemGroup(
                    "least_decimal",
                    new LeastDecimalGroup([
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m),
                    ])
                ),
                new SelectItemGroup(
                    "least_decimal_nullable",
                    new LeastDecimalNullableGroup([
                        new LiteralDecimal(2.5m),
                        new LiteralDecimalNullable(),
                        new LiteralDecimal(0.1m),
                    ])
                ),
                new SelectItemGroup(
                    "least_string",
                    new LeastStringGroup([new LiteralString("a"), new LiteralString("b")])
                ),
                new SelectItemGroup(
                    "least_string_nullable",
                    new LeastStringNullableGroup([
                        new LiteralString("a"),
                        new LiteralStringNullable(),
                        new LiteralString("b"),
                    ])
                ),
                new SelectItemGroup(
                    "least_boolean",
                    new LeastBooleanGroup([
                        new LiteralBoolean(true),
                        new LiteralBoolean(false),
                    ])
                ),
                new SelectItemGroup(
                    "least_boolean_nullable",
                    new LeastBooleanNullableGroup([
                        new LiteralBoolean(true),
                        new LiteralBooleanNullable(),
                        new LiteralBoolean(false),
                    ])
                ),
                new SelectItemGroup(
                    "least_date",
                    new LeastDateGroup([
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDate(new DateOnly(2024, 3, 1)),
                    ])
                ),
                new SelectItemGroup(
                    "least_date_nullable",
                    new LeastDateNullableGroup([
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDateNullable(),
                        new LiteralDate(new DateOnly(2024, 3, 1)),
                    ])
                ),
                new SelectItemGroup(
                    "least_time",
                    new LeastTimeGroup([
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralTime(new TimeOnly(17, 45, 30, 500)),
                    ])
                ),
                new SelectItemGroup(
                    "least_time_nullable",
                    new LeastTimeNullableGroup([
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralTimeNullable(),
                        new LiteralTime(new TimeOnly(17, 45, 30, 500)),
                    ])
                ),
                new SelectItemGroup(
                    "least_datetime",
                    new LeastDatetimeGroup([
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        ),
                    ])
                ),
                new SelectItemGroup(
                    "least_datetime_nullable",
                    new LeastDatetimeNullableGroup([
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDatetimeNullable(),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 3, 1, 17, 45, 30, 500, TimeSpan.Zero)
                        ),
                    ])
                ),
                new SelectItemGroup(
                    "least_uuid",
                    new LeastUuidGroup([
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralUuid(new Guid(2, 0, 0, new byte[8])),
                    ])
                ),
                new SelectItemGroup(
                    "least_uuid_nullable",
                    new LeastUuidNullableGroup([
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralUuidNullable(),
                        new LiteralUuid(new Guid(2, 0, 0, new byte[8])),
                    ])
                ),
                new SelectItemGroup(
                    "least_decimal_of_integer_and_decimal",
                    new LeastDecimalGroup([
                        new LiteralInteger(7),
                        new LiteralDecimal(2.5m),
                    ])
                ),
                new SelectItemGroup("count_integer", new CountGroup()),
                new SelectItemGroup(
                    "count_integer_where",
                    new CountGroup(predicate: new LiteralBoolean(true))
                ),
                new SelectItemGroup(
                    "countDistinct_integer_of_integer",
                    new CountDistinctGroup(new LiteralInteger(7))
                ),
                new SelectItemGroup(
                    "countDistinct_integer_of_decimal",
                    new CountDistinctGroup(new LiteralDecimal(2.5m))
                ),
                new SelectItemGroup(
                    "countDistinct_integer_of_string",
                    new CountDistinctGroup(new LiteralString("a"))
                ),
                new SelectItemGroup(
                    "countDistinct_integer_of_boolean",
                    new CountDistinctGroup(new LiteralBoolean(true))
                ),
                new SelectItemGroup(
                    "countDistinct_integer_of_date",
                    new CountDistinctGroup(new LiteralDate(new DateOnly(2024, 1, 31)))
                ),
                new SelectItemGroup(
                    "countDistinct_integer_of_time",
                    new CountDistinctGroup(new LiteralTime(new TimeOnly(8, 30, 0)))
                ),
                new SelectItemGroup(
                    "countDistinct_integer_of_datetime",
                    new CountDistinctGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemGroup(
                    "countDistinct_integer_of_uuid",
                    new CountDistinctGroup(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8]))
                    )
                ),
                new SelectItemGroup(
                    "countDistinct_integer_of_integer_null",
                    new CountDistinctGroup(new LiteralIntegerNullable())
                ),
                new SelectItemGroup(
                    "countDistinct_integer_of_string_null",
                    new CountDistinctGroup(new LiteralStringNullable())
                ),
                new SelectItemGroup(
                    "countDistinct_integer_of_uuid_null",
                    new CountDistinctGroup(new LiteralUuidNullable())
                ),
                new SelectItemGroup(
                    "countDistinct_integer_of_decimal_where",
                    new CountDistinctGroup(
                        new LiteralDecimal(2.5m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "sum_integer_of_integer",
                    new SumIntegerGroup(new LiteralInteger(7))
                ),
                new SelectItemGroup(
                    "sum_integer_of_integer_null",
                    new SumIntegerGroup(new LiteralIntegerNullable())
                ),
                new SelectItemGroup(
                    "sum_integer_of_integer_where",
                    new SumIntegerGroup(
                        new LiteralInteger(7),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "sum_decimal_of_decimal",
                    new SumDecimalGroup(new LiteralDecimal(2.5m))
                ),
                new SelectItemGroup(
                    "sum_decimal_of_decimal_null",
                    new SumDecimalGroup(new LiteralDecimalNullable())
                ),
                new SelectItemGroup(
                    "sum_decimal_of_decimal_where",
                    new SumDecimalGroup(
                        new LiteralDecimal(2.5m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "average_decimal_of_integer",
                    new AverageDecimalGroup(new LiteralInteger(7))
                ),
                new SelectItemGroup(
                    "average_decimal_nullable_of_integer_null",
                    new AverageDecimalNullableGroup(new LiteralIntegerNullable())
                ),
                new SelectItemGroup(
                    "average_decimal_nullable_of_integer_where",
                    new AverageDecimalNullableGroup(
                        new LiteralInteger(7),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "average_decimal_of_decimal",
                    new AverageDecimalGroup(new LiteralDecimal(2.5m))
                ),
                new SelectItemGroup(
                    "average_decimal_nullable_of_decimal_null",
                    new AverageDecimalNullableGroup(new LiteralDecimalNullable())
                ),
                new SelectItemGroup(
                    "average_decimal_nullable_of_decimal_where",
                    new AverageDecimalNullableGroup(
                        new LiteralDecimal(2.5m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "average_date_of_date",
                    new AverageDateGroup(new LiteralDate(new DateOnly(2024, 1, 31)))
                ),
                new SelectItemGroup(
                    "average_date_nullable_of_date_null",
                    new AverageDateNullableGroup(new LiteralDateNullable())
                ),
                new SelectItemGroup(
                    "average_date_nullable_of_date_where",
                    new AverageDateNullableGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "average_time_of_time",
                    new AverageTimeGroup(new LiteralTime(new TimeOnly(8, 30, 0)))
                ),
                new SelectItemGroup(
                    "average_time_nullable_of_time_null",
                    new AverageTimeNullableGroup(new LiteralTimeNullable())
                ),
                new SelectItemGroup(
                    "average_time_nullable_of_time_where",
                    new AverageTimeNullableGroup(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "average_datetime_of_datetime",
                    new AverageDatetimeGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemGroup(
                    "average_datetime_nullable_of_datetime_null",
                    new AverageDatetimeNullableGroup(new LiteralDatetimeNullable())
                ),
                new SelectItemGroup(
                    "average_datetime_nullable_of_datetime_where",
                    new AverageDatetimeNullableGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "min_integer_of_integer",
                    new MinIntegerGroup(new LiteralInteger(7))
                ),
                new SelectItemGroup(
                    "min_integer_nullable_of_integer_null",
                    new MinIntegerNullableGroup(new LiteralIntegerNullable())
                ),
                new SelectItemGroup(
                    "min_integer_nullable_of_integer_where",
                    new MinIntegerNullableGroup(
                        new LiteralInteger(7),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "min_decimal_of_decimal",
                    new MinDecimalGroup(new LiteralDecimal(2.5m))
                ),
                new SelectItemGroup(
                    "min_decimal_nullable_of_decimal_null",
                    new MinDecimalNullableGroup(new LiteralDecimalNullable())
                ),
                new SelectItemGroup(
                    "min_decimal_nullable_of_decimal_where",
                    new MinDecimalNullableGroup(
                        new LiteralDecimal(2.5m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "min_string_of_string",
                    new MinStringGroup(new LiteralString("a"))
                ),
                new SelectItemGroup(
                    "min_string_nullable_of_string_null",
                    new MinStringNullableGroup(new LiteralStringNullable())
                ),
                new SelectItemGroup(
                    "min_string_nullable_of_string_where",
                    new MinStringNullableGroup(
                        new LiteralString("a"),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "min_boolean_of_boolean",
                    new MinBooleanGroup(new LiteralBoolean(true))
                ),
                new SelectItemGroup(
                    "min_boolean_nullable_of_boolean_null",
                    new MinBooleanNullableGroup(new LiteralBooleanNullable())
                ),
                new SelectItemGroup(
                    "min_boolean_nullable_of_boolean_where",
                    new MinBooleanNullableGroup(
                        new LiteralBoolean(true),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "min_date_of_date",
                    new MinDateGroup(new LiteralDate(new DateOnly(2024, 1, 31)))
                ),
                new SelectItemGroup(
                    "min_date_nullable_of_date_null",
                    new MinDateNullableGroup(new LiteralDateNullable())
                ),
                new SelectItemGroup(
                    "min_date_nullable_of_date_where",
                    new MinDateNullableGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "min_time_of_time",
                    new MinTimeGroup(new LiteralTime(new TimeOnly(8, 30, 0)))
                ),
                new SelectItemGroup(
                    "min_time_nullable_of_time_null",
                    new MinTimeNullableGroup(new LiteralTimeNullable())
                ),
                new SelectItemGroup(
                    "min_time_nullable_of_time_where",
                    new MinTimeNullableGroup(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "min_datetime_of_datetime",
                    new MinDatetimeGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemGroup(
                    "min_datetime_nullable_of_datetime_null",
                    new MinDatetimeNullableGroup(new LiteralDatetimeNullable())
                ),
                new SelectItemGroup(
                    "min_datetime_nullable_of_datetime_where",
                    new MinDatetimeNullableGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "min_uuid_of_uuid",
                    new MinUuidGroup(new LiteralUuid(new Guid(1, 0, 0, new byte[8])))
                ),
                new SelectItemGroup(
                    "min_uuid_nullable_of_uuid_null",
                    new MinUuidNullableGroup(new LiteralUuidNullable())
                ),
                new SelectItemGroup(
                    "min_uuid_nullable_of_uuid_where",
                    new MinUuidNullableGroup(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "max_integer_of_integer",
                    new MaxIntegerGroup(new LiteralInteger(7))
                ),
                new SelectItemGroup(
                    "max_integer_nullable_of_integer_null",
                    new MaxIntegerNullableGroup(new LiteralIntegerNullable())
                ),
                new SelectItemGroup(
                    "max_integer_nullable_of_integer_where",
                    new MaxIntegerNullableGroup(
                        new LiteralInteger(7),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "max_decimal_of_decimal",
                    new MaxDecimalGroup(new LiteralDecimal(2.5m))
                ),
                new SelectItemGroup(
                    "max_decimal_nullable_of_decimal_null",
                    new MaxDecimalNullableGroup(new LiteralDecimalNullable())
                ),
                new SelectItemGroup(
                    "max_decimal_nullable_of_decimal_where",
                    new MaxDecimalNullableGroup(
                        new LiteralDecimal(2.5m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "max_string_of_string",
                    new MaxStringGroup(new LiteralString("a"))
                ),
                new SelectItemGroup(
                    "max_string_nullable_of_string_null",
                    new MaxStringNullableGroup(new LiteralStringNullable())
                ),
                new SelectItemGroup(
                    "max_string_nullable_of_string_where",
                    new MaxStringNullableGroup(
                        new LiteralString("a"),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "max_boolean_of_boolean",
                    new MaxBooleanGroup(new LiteralBoolean(true))
                ),
                new SelectItemGroup(
                    "max_boolean_nullable_of_boolean_null",
                    new MaxBooleanNullableGroup(new LiteralBooleanNullable())
                ),
                new SelectItemGroup(
                    "max_boolean_nullable_of_boolean_where",
                    new MaxBooleanNullableGroup(
                        new LiteralBoolean(true),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "max_date_of_date",
                    new MaxDateGroup(new LiteralDate(new DateOnly(2024, 1, 31)))
                ),
                new SelectItemGroup(
                    "max_date_nullable_of_date_null",
                    new MaxDateNullableGroup(new LiteralDateNullable())
                ),
                new SelectItemGroup(
                    "max_date_nullable_of_date_where",
                    new MaxDateNullableGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "max_time_of_time",
                    new MaxTimeGroup(new LiteralTime(new TimeOnly(8, 30, 0)))
                ),
                new SelectItemGroup(
                    "max_time_nullable_of_time_null",
                    new MaxTimeNullableGroup(new LiteralTimeNullable())
                ),
                new SelectItemGroup(
                    "max_time_nullable_of_time_where",
                    new MaxTimeNullableGroup(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "max_datetime_of_datetime",
                    new MaxDatetimeGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemGroup(
                    "max_datetime_nullable_of_datetime_null",
                    new MaxDatetimeNullableGroup(new LiteralDatetimeNullable())
                ),
                new SelectItemGroup(
                    "max_datetime_nullable_of_datetime_where",
                    new MaxDatetimeNullableGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "max_uuid_of_uuid",
                    new MaxUuidGroup(new LiteralUuid(new Guid(1, 0, 0, new byte[8])))
                ),
                new SelectItemGroup(
                    "max_uuid_nullable_of_uuid_null",
                    new MaxUuidNullableGroup(new LiteralUuidNullable())
                ),
                new SelectItemGroup(
                    "max_uuid_nullable_of_uuid_where",
                    new MaxUuidNullableGroup(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "first_integer_of_integer",
                    new FirstIntegerGroup(
                        new LiteralInteger(7),
                        [new OrderItemRow(new LiteralInteger(7))]
                    )
                ),
                new SelectItemGroup(
                    "first_integer_nullable_of_integer_null",
                    new FirstIntegerNullableGroup(
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
                new SelectItemGroup(
                    "first_integer_nullable_of_integer_where",
                    new FirstIntegerNullableGroup(
                        new LiteralInteger(7),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "first_decimal_of_decimal",
                    new FirstDecimalGroup(
                        new LiteralDecimal(2.5m),
                        [new OrderItemRow(new LiteralInteger(7))]
                    )
                ),
                new SelectItemGroup(
                    "first_decimal_nullable_of_decimal_null",
                    new FirstDecimalNullableGroup(
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
                new SelectItemGroup(
                    "first_decimal_nullable_of_decimal_where",
                    new FirstDecimalNullableGroup(
                        new LiteralDecimal(2.5m),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "first_string_of_string",
                    new FirstStringGroup(
                        new LiteralString("a"),
                        [new OrderItemRow(new LiteralInteger(7))]
                    )
                ),
                new SelectItemGroup(
                    "first_string_nullable_of_string_null",
                    new FirstStringNullableGroup(
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
                new SelectItemGroup(
                    "first_string_nullable_of_string_where",
                    new FirstStringNullableGroup(
                        new LiteralString("a"),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "first_boolean_of_boolean",
                    new FirstBooleanGroup(
                        new LiteralBoolean(true),
                        [new OrderItemRow(new LiteralInteger(7))]
                    )
                ),
                new SelectItemGroup(
                    "first_boolean_nullable_of_boolean_null",
                    new FirstBooleanNullableGroup(
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
                new SelectItemGroup(
                    "first_boolean_nullable_of_boolean_where",
                    new FirstBooleanNullableGroup(
                        new LiteralBoolean(true),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "first_date_of_date",
                    new FirstDateGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        [new OrderItemRow(new LiteralInteger(7))]
                    )
                ),
                new SelectItemGroup(
                    "first_date_nullable_of_date_null",
                    new FirstDateNullableGroup(
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
                new SelectItemGroup(
                    "first_date_nullable_of_date_where",
                    new FirstDateNullableGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "first_time_of_time",
                    new FirstTimeGroup(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        [new OrderItemRow(new LiteralInteger(7))]
                    )
                ),
                new SelectItemGroup(
                    "first_time_nullable_of_time_null",
                    new FirstTimeNullableGroup(
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
                new SelectItemGroup(
                    "first_time_nullable_of_time_where",
                    new FirstTimeNullableGroup(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "first_datetime_of_datetime",
                    new FirstDatetimeGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        [new OrderItemRow(new LiteralInteger(7))]
                    )
                ),
                new SelectItemGroup(
                    "first_datetime_nullable_of_datetime_null",
                    new FirstDatetimeNullableGroup(
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
                new SelectItemGroup(
                    "first_datetime_nullable_of_datetime_where",
                    new FirstDatetimeNullableGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "first_uuid_of_uuid",
                    new FirstUuidGroup(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        [new OrderItemRow(new LiteralInteger(7))]
                    )
                ),
                new SelectItemGroup(
                    "first_uuid_nullable_of_uuid_null",
                    new FirstUuidNullableGroup(
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
                new SelectItemGroup(
                    "first_uuid_nullable_of_uuid_where",
                    new FirstUuidNullableGroup(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "last_integer_of_integer",
                    new LastIntegerGroup(
                        new LiteralInteger(7),
                        [new OrderItemRow(new LiteralInteger(7))]
                    )
                ),
                new SelectItemGroup(
                    "last_integer_nullable_of_integer_null",
                    new LastIntegerNullableGroup(
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
                new SelectItemGroup(
                    "last_integer_nullable_of_integer_where",
                    new LastIntegerNullableGroup(
                        new LiteralInteger(7),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "last_decimal_of_decimal",
                    new LastDecimalGroup(
                        new LiteralDecimal(2.5m),
                        [new OrderItemRow(new LiteralInteger(7))]
                    )
                ),
                new SelectItemGroup(
                    "last_decimal_nullable_of_decimal_null",
                    new LastDecimalNullableGroup(
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
                new SelectItemGroup(
                    "last_decimal_nullable_of_decimal_where",
                    new LastDecimalNullableGroup(
                        new LiteralDecimal(2.5m),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "last_string_of_string",
                    new LastStringGroup(
                        new LiteralString("a"),
                        [new OrderItemRow(new LiteralInteger(7))]
                    )
                ),
                new SelectItemGroup(
                    "last_string_nullable_of_string_null",
                    new LastStringNullableGroup(
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
                new SelectItemGroup(
                    "last_string_nullable_of_string_where",
                    new LastStringNullableGroup(
                        new LiteralString("a"),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "last_boolean_of_boolean",
                    new LastBooleanGroup(
                        new LiteralBoolean(true),
                        [new OrderItemRow(new LiteralInteger(7))]
                    )
                ),
                new SelectItemGroup(
                    "last_boolean_nullable_of_boolean_null",
                    new LastBooleanNullableGroup(
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
                new SelectItemGroup(
                    "last_boolean_nullable_of_boolean_where",
                    new LastBooleanNullableGroup(
                        new LiteralBoolean(true),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "last_date_of_date",
                    new LastDateGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        [new OrderItemRow(new LiteralInteger(7))]
                    )
                ),
                new SelectItemGroup(
                    "last_date_nullable_of_date_null",
                    new LastDateNullableGroup(
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
                new SelectItemGroup(
                    "last_date_nullable_of_date_where",
                    new LastDateNullableGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "last_time_of_time",
                    new LastTimeGroup(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        [new OrderItemRow(new LiteralInteger(7))]
                    )
                ),
                new SelectItemGroup(
                    "last_time_nullable_of_time_null",
                    new LastTimeNullableGroup(
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
                new SelectItemGroup(
                    "last_time_nullable_of_time_where",
                    new LastTimeNullableGroup(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "last_datetime_of_datetime",
                    new LastDatetimeGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        [new OrderItemRow(new LiteralInteger(7))]
                    )
                ),
                new SelectItemGroup(
                    "last_datetime_nullable_of_datetime_null",
                    new LastDatetimeNullableGroup(
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
                new SelectItemGroup(
                    "last_datetime_nullable_of_datetime_where",
                    new LastDatetimeNullableGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "last_uuid_of_uuid",
                    new LastUuidGroup(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        [new OrderItemRow(new LiteralInteger(7))]
                    )
                ),
                new SelectItemGroup(
                    "last_uuid_nullable_of_uuid_null",
                    new LastUuidNullableGroup(
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
                new SelectItemGroup(
                    "last_uuid_nullable_of_uuid_where",
                    new LastUuidNullableGroup(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "median_decimal_of_integer",
                    new MedianDecimalGroup(new LiteralInteger(7))
                ),
                new SelectItemGroup(
                    "median_decimal_nullable_of_integer_null",
                    new MedianDecimalNullableGroup(new LiteralIntegerNullable())
                ),
                new SelectItemGroup(
                    "median_decimal_nullable_of_integer_where",
                    new MedianDecimalNullableGroup(
                        new LiteralInteger(7),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "median_decimal_of_decimal",
                    new MedianDecimalGroup(new LiteralDecimal(2.5m))
                ),
                new SelectItemGroup(
                    "median_decimal_nullable_of_decimal_null",
                    new MedianDecimalNullableGroup(new LiteralDecimalNullable())
                ),
                new SelectItemGroup(
                    "median_decimal_nullable_of_decimal_where",
                    new MedianDecimalNullableGroup(
                        new LiteralDecimal(2.5m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "median_date_of_date",
                    new MedianDateGroup(new LiteralDate(new DateOnly(2024, 1, 31)))
                ),
                new SelectItemGroup(
                    "median_date_nullable_of_date_null",
                    new MedianDateNullableGroup(new LiteralDateNullable())
                ),
                new SelectItemGroup(
                    "median_date_nullable_of_date_where",
                    new MedianDateNullableGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "median_time_of_time",
                    new MedianTimeGroup(new LiteralTime(new TimeOnly(8, 30, 0)))
                ),
                new SelectItemGroup(
                    "median_time_nullable_of_time_null",
                    new MedianTimeNullableGroup(new LiteralTimeNullable())
                ),
                new SelectItemGroup(
                    "median_time_nullable_of_time_where",
                    new MedianTimeNullableGroup(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "median_datetime_of_datetime",
                    new MedianDatetimeGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemGroup(
                    "median_datetime_nullable_of_datetime_null",
                    new MedianDatetimeNullableGroup(new LiteralDatetimeNullable())
                ),
                new SelectItemGroup(
                    "median_datetime_nullable_of_datetime_where",
                    new MedianDatetimeNullableGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "percentileContinuous_decimal_of_integer",
                    new PercentileContinuousDecimalGroup(
                        new LiteralInteger(7),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemGroup(
                    "percentileContinuous_decimal_nullable_of_integer_null",
                    new PercentileContinuousDecimalNullableGroup(
                        new LiteralIntegerNullable(),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemGroup(
                    "percentileContinuous_decimal_nullable_of_integer_where",
                    new PercentileContinuousDecimalNullableGroup(
                        new LiteralInteger(7),
                        new LiteralDecimal(0.9m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "percentileContinuous_decimal_of_decimal",
                    new PercentileContinuousDecimalGroup(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemGroup(
                    "percentileContinuous_decimal_nullable_of_decimal_null",
                    new PercentileContinuousDecimalNullableGroup(
                        new LiteralDecimalNullable(),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemGroup(
                    "percentileContinuous_decimal_nullable_of_decimal_where",
                    new PercentileContinuousDecimalNullableGroup(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.9m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "percentileContinuous_date_of_date",
                    new PercentileContinuousDateGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemGroup(
                    "percentileContinuous_date_nullable_of_date_null",
                    new PercentileContinuousDateNullableGroup(
                        new LiteralDateNullable(),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemGroup(
                    "percentileContinuous_date_nullable_of_date_where",
                    new PercentileContinuousDateNullableGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDecimal(0.9m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "percentileContinuous_time_of_time",
                    new PercentileContinuousTimeGroup(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemGroup(
                    "percentileContinuous_time_nullable_of_time_null",
                    new PercentileContinuousTimeNullableGroup(
                        new LiteralTimeNullable(),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemGroup(
                    "percentileContinuous_time_nullable_of_time_where",
                    new PercentileContinuousTimeNullableGroup(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralDecimal(0.9m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "percentileContinuous_datetime_of_datetime",
                    new PercentileContinuousDatetimeGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemGroup(
                    "percentileContinuous_datetime_nullable_of_datetime_null",
                    new PercentileContinuousDatetimeNullableGroup(
                        new LiteralDatetimeNullable(),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemGroup(
                    "percentileContinuous_datetime_nullable_of_datetime_where",
                    new PercentileContinuousDatetimeNullableGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDecimal(0.9m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "percentileDiscrete_integer_of_integer",
                    new PercentileDiscreteIntegerGroup(
                        new LiteralInteger(7),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemGroup(
                    "percentileDiscrete_integer_nullable_of_integer_null",
                    new PercentileDiscreteIntegerNullableGroup(
                        new LiteralIntegerNullable(),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemGroup(
                    "percentileDiscrete_integer_nullable_of_integer_where",
                    new PercentileDiscreteIntegerNullableGroup(
                        new LiteralInteger(7),
                        new LiteralDecimal(0.9m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "percentileDiscrete_decimal_of_decimal",
                    new PercentileDiscreteDecimalGroup(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemGroup(
                    "percentileDiscrete_decimal_nullable_of_decimal_null",
                    new PercentileDiscreteDecimalNullableGroup(
                        new LiteralDecimalNullable(),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemGroup(
                    "percentileDiscrete_decimal_nullable_of_decimal_where",
                    new PercentileDiscreteDecimalNullableGroup(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.9m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "percentileDiscrete_string_of_string",
                    new PercentileDiscreteStringGroup(
                        new LiteralString("a"),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemGroup(
                    "percentileDiscrete_string_nullable_of_string_null",
                    new PercentileDiscreteStringNullableGroup(
                        new LiteralStringNullable(),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemGroup(
                    "percentileDiscrete_string_nullable_of_string_where",
                    new PercentileDiscreteStringNullableGroup(
                        new LiteralString("a"),
                        new LiteralDecimal(0.9m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "percentileDiscrete_boolean_of_boolean",
                    new PercentileDiscreteBooleanGroup(
                        new LiteralBoolean(true),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemGroup(
                    "percentileDiscrete_boolean_nullable_of_boolean_null",
                    new PercentileDiscreteBooleanNullableGroup(
                        new LiteralBooleanNullable(),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemGroup(
                    "percentileDiscrete_boolean_nullable_of_boolean_where",
                    new PercentileDiscreteBooleanNullableGroup(
                        new LiteralBoolean(true),
                        new LiteralDecimal(0.9m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "percentileDiscrete_date_of_date",
                    new PercentileDiscreteDateGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemGroup(
                    "percentileDiscrete_date_nullable_of_date_null",
                    new PercentileDiscreteDateNullableGroup(
                        new LiteralDateNullable(),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemGroup(
                    "percentileDiscrete_date_nullable_of_date_where",
                    new PercentileDiscreteDateNullableGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDecimal(0.9m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "percentileDiscrete_time_of_time",
                    new PercentileDiscreteTimeGroup(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemGroup(
                    "percentileDiscrete_time_nullable_of_time_null",
                    new PercentileDiscreteTimeNullableGroup(
                        new LiteralTimeNullable(),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemGroup(
                    "percentileDiscrete_time_nullable_of_time_where",
                    new PercentileDiscreteTimeNullableGroup(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralDecimal(0.9m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "percentileDiscrete_datetime_of_datetime",
                    new PercentileDiscreteDatetimeGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemGroup(
                    "percentileDiscrete_datetime_nullable_of_datetime_null",
                    new PercentileDiscreteDatetimeNullableGroup(
                        new LiteralDatetimeNullable(),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemGroup(
                    "percentileDiscrete_datetime_nullable_of_datetime_where",
                    new PercentileDiscreteDatetimeNullableGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDecimal(0.9m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "percentileDiscrete_uuid_of_uuid",
                    new PercentileDiscreteUuidGroup(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemGroup(
                    "percentileDiscrete_uuid_nullable_of_uuid_null",
                    new PercentileDiscreteUuidNullableGroup(
                        new LiteralUuidNullable(),
                        new LiteralDecimal(0.9m)
                    )
                ),
                new SelectItemGroup(
                    "percentileDiscrete_uuid_nullable_of_uuid_where",
                    new PercentileDiscreteUuidNullableGroup(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralDecimal(0.9m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "mode_integer_of_integer",
                    new ModeIntegerGroup(new LiteralInteger(7))
                ),
                new SelectItemGroup(
                    "mode_integer_nullable_of_integer_null",
                    new ModeIntegerNullableGroup(new LiteralIntegerNullable())
                ),
                new SelectItemGroup(
                    "mode_integer_nullable_of_integer_where",
                    new ModeIntegerNullableGroup(
                        new LiteralInteger(7),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "mode_decimal_of_decimal",
                    new ModeDecimalGroup(new LiteralDecimal(2.5m))
                ),
                new SelectItemGroup(
                    "mode_decimal_nullable_of_decimal_null",
                    new ModeDecimalNullableGroup(new LiteralDecimalNullable())
                ),
                new SelectItemGroup(
                    "mode_decimal_nullable_of_decimal_where",
                    new ModeDecimalNullableGroup(
                        new LiteralDecimal(2.5m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "mode_string_of_string",
                    new ModeStringGroup(new LiteralString("a"))
                ),
                new SelectItemGroup(
                    "mode_string_nullable_of_string_null",
                    new ModeStringNullableGroup(new LiteralStringNullable())
                ),
                new SelectItemGroup(
                    "mode_string_nullable_of_string_where",
                    new ModeStringNullableGroup(
                        new LiteralString("a"),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "mode_boolean_of_boolean",
                    new ModeBooleanGroup(new LiteralBoolean(true))
                ),
                new SelectItemGroup(
                    "mode_boolean_nullable_of_boolean_null",
                    new ModeBooleanNullableGroup(new LiteralBooleanNullable())
                ),
                new SelectItemGroup(
                    "mode_boolean_nullable_of_boolean_where",
                    new ModeBooleanNullableGroup(
                        new LiteralBoolean(true),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "mode_date_of_date",
                    new ModeDateGroup(new LiteralDate(new DateOnly(2024, 1, 31)))
                ),
                new SelectItemGroup(
                    "mode_date_nullable_of_date_null",
                    new ModeDateNullableGroup(new LiteralDateNullable())
                ),
                new SelectItemGroup(
                    "mode_date_nullable_of_date_where",
                    new ModeDateNullableGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "mode_time_of_time",
                    new ModeTimeGroup(new LiteralTime(new TimeOnly(8, 30, 0)))
                ),
                new SelectItemGroup(
                    "mode_time_nullable_of_time_null",
                    new ModeTimeNullableGroup(new LiteralTimeNullable())
                ),
                new SelectItemGroup(
                    "mode_time_nullable_of_time_where",
                    new ModeTimeNullableGroup(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "mode_datetime_of_datetime",
                    new ModeDatetimeGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemGroup(
                    "mode_datetime_nullable_of_datetime_null",
                    new ModeDatetimeNullableGroup(new LiteralDatetimeNullable())
                ),
                new SelectItemGroup(
                    "mode_datetime_nullable_of_datetime_where",
                    new ModeDatetimeNullableGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "mode_uuid_of_uuid",
                    new ModeUuidGroup(new LiteralUuid(new Guid(1, 0, 0, new byte[8])))
                ),
                new SelectItemGroup(
                    "mode_uuid_nullable_of_uuid_null",
                    new ModeUuidNullableGroup(new LiteralUuidNullable())
                ),
                new SelectItemGroup(
                    "mode_uuid_nullable_of_uuid_where",
                    new ModeUuidNullableGroup(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "variancePopulation_decimal_of_integer",
                    new VariancePopulationDecimalGroup(new LiteralInteger(7))
                ),
                new SelectItemGroup(
                    "variancePopulation_decimal_nullable_of_integer_null",
                    new VariancePopulationDecimalNullableGroup(
                        new LiteralIntegerNullable()
                    )
                ),
                new SelectItemGroup(
                    "variancePopulation_decimal_nullable_of_integer_where",
                    new VariancePopulationDecimalNullableGroup(
                        new LiteralInteger(7),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "variancePopulation_decimal_of_decimal",
                    new VariancePopulationDecimalGroup(new LiteralDecimal(2.5m))
                ),
                new SelectItemGroup(
                    "variancePopulation_decimal_nullable_of_decimal_null",
                    new VariancePopulationDecimalNullableGroup(
                        new LiteralDecimalNullable()
                    )
                ),
                new SelectItemGroup(
                    "variancePopulation_decimal_nullable_of_decimal_where",
                    new VariancePopulationDecimalNullableGroup(
                        new LiteralDecimal(2.5m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "standardDeviationPopulation_decimal_of_integer",
                    new StandardDeviationPopulationDecimalGroup(new LiteralInteger(7))
                ),
                new SelectItemGroup(
                    "standardDeviationPopulation_decimal_nullable_of_integer_null",
                    new StandardDeviationPopulationDecimalNullableGroup(
                        new LiteralIntegerNullable()
                    )
                ),
                new SelectItemGroup(
                    "standardDeviationPopulation_decimal_nullable_of_integer_where",
                    new StandardDeviationPopulationDecimalNullableGroup(
                        new LiteralInteger(7),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "standardDeviationPopulation_decimal_of_decimal",
                    new StandardDeviationPopulationDecimalGroup(new LiteralDecimal(2.5m))
                ),
                new SelectItemGroup(
                    "standardDeviationPopulation_decimal_nullable_of_decimal_null",
                    new StandardDeviationPopulationDecimalNullableGroup(
                        new LiteralDecimalNullable()
                    )
                ),
                new SelectItemGroup(
                    "standardDeviationPopulation_decimal_nullable_of_decimal_where",
                    new StandardDeviationPopulationDecimalNullableGroup(
                        new LiteralDecimal(2.5m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "varianceSample_decimal_nullable_of_integer",
                    new VarianceSampleGroup(new LiteralInteger(7))
                ),
                new SelectItemGroup(
                    "varianceSample_decimal_nullable_of_decimal",
                    new VarianceSampleGroup(new LiteralDecimal(2.5m))
                ),
                new SelectItemGroup(
                    "varianceSample_decimal_nullable_of_decimal_null",
                    new VarianceSampleGroup(new LiteralDecimalNullable())
                ),
                new SelectItemGroup(
                    "varianceSample_decimal_nullable_of_decimal_where",
                    new VarianceSampleGroup(
                        new LiteralDecimal(2.5m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "standardDeviationSample_decimal_nullable_of_integer",
                    new StandardDeviationSampleGroup(new LiteralInteger(7))
                ),
                new SelectItemGroup(
                    "standardDeviationSample_decimal_nullable_of_decimal",
                    new StandardDeviationSampleGroup(new LiteralDecimal(2.5m))
                ),
                new SelectItemGroup(
                    "standardDeviationSample_decimal_nullable_of_decimal_null",
                    new StandardDeviationSampleGroup(new LiteralDecimalNullable())
                ),
                new SelectItemGroup(
                    "standardDeviationSample_decimal_nullable_of_decimal_where",
                    new StandardDeviationSampleGroup(
                        new LiteralDecimal(2.5m),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "covariancePopulation_decimal",
                    new CovariancePopulationDecimalGroup(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m)
                    )
                ),
                new SelectItemGroup(
                    "covariancePopulation_decimal_of_integers",
                    new CovariancePopulationDecimalGroup(
                        new LiteralInteger(7),
                        new LiteralInteger(-3)
                    )
                ),
                new SelectItemGroup(
                    "covariancePopulation_decimal_nullable_left",
                    new CovariancePopulationDecimalNullableGroup(
                        new LiteralDecimalNullable(),
                        new LiteralDecimal(2.5m)
                    )
                ),
                new SelectItemGroup(
                    "covariancePopulation_decimal_nullable_right",
                    new CovariancePopulationDecimalNullableGroup(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimalNullable()
                    )
                ),
                new SelectItemGroup(
                    "covariancePopulation_decimal_nullable_where",
                    new CovariancePopulationDecimalNullableGroup(
                        new LiteralDecimal(2.5m),
                        new LiteralInteger(7),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "covarianceSample_decimal_nullable",
                    new CovarianceSampleGroup(
                        new LiteralDecimal(2.5m),
                        new LiteralInteger(7)
                    )
                ),
                new SelectItemGroup(
                    "covarianceSample_decimal_nullable_of_nulls_where",
                    new CovarianceSampleGroup(
                        new LiteralDecimalNullable(),
                        new LiteralIntegerNullable(),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "correlation_decimal_nullable",
                    new CorrelationGroup(new LiteralDecimal(2.5m), new LiteralInteger(7))
                ),
                new SelectItemGroup(
                    "correlation_decimal_nullable_of_nulls_where",
                    new CorrelationGroup(
                        new LiteralDecimalNullable(),
                        new LiteralDecimalNullable(),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "regressionSlope_decimal_nullable",
                    new RegressionSlopeGroup(
                        new LiteralDecimal(2.5m),
                        new LiteralInteger(7)
                    )
                ),
                new SelectItemGroup(
                    "regressionSlope_decimal_nullable_of_integers",
                    new RegressionSlopeGroup(
                        new LiteralInteger(7),
                        new LiteralInteger(-3)
                    )
                ),
                new SelectItemGroup(
                    "regressionSlope_decimal_nullable_of_nulls_where",
                    new RegressionSlopeGroup(
                        new LiteralDecimalNullable(),
                        new LiteralIntegerNullable(),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "regressionIntercept_decimal_nullable",
                    new RegressionInterceptGroup(
                        new LiteralDecimal(2.5m),
                        new LiteralInteger(7)
                    )
                ),
                new SelectItemGroup(
                    "regressionIntercept_decimal_nullable_of_integers",
                    new RegressionInterceptGroup(
                        new LiteralInteger(7),
                        new LiteralInteger(-3)
                    )
                ),
                new SelectItemGroup(
                    "regressionIntercept_decimal_nullable_of_nulls_where",
                    new RegressionInterceptGroup(
                        new LiteralDecimalNullable(),
                        new LiteralIntegerNullable(),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "regressionRSquared_decimal_nullable",
                    new RegressionRSquaredGroup(
                        new LiteralDecimal(2.5m),
                        new LiteralInteger(7)
                    )
                ),
                new SelectItemGroup(
                    "regressionRSquared_decimal_nullable_of_integers",
                    new RegressionRSquaredGroup(
                        new LiteralInteger(7),
                        new LiteralInteger(-3)
                    )
                ),
                new SelectItemGroup(
                    "regressionRSquared_decimal_nullable_of_nulls_where",
                    new RegressionRSquaredGroup(
                        new LiteralDecimalNullable(),
                        new LiteralIntegerNullable(),
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "concatAll_string_of_string",
                    new ConcatAllGroup(
                        new LiteralString("a"),
                        new LiteralString(","),
                        [new OrderItemRow(new LiteralInteger(7))]
                    )
                ),
                new SelectItemGroup(
                    "concatAll_string_of_string_null",
                    new ConcatAllGroup(
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
                new SelectItemGroup(
                    "concatAll_string_of_string_where",
                    new ConcatAllGroup(
                        new LiteralString("a"),
                        new LiteralString(","),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true)
                    )
                ),
                new SelectItemGroup(
                    "concatAll_string_of_string_distinct",
                    new ConcatAllGroup(
                        new LiteralString("a"),
                        new LiteralString(""),
                        [new OrderItemRow(new LiteralInteger(7))],
                        distinct: true
                    )
                ),
                new SelectItemGroup(
                    "concatAll_string_parameter_separator",
                    new ConcatAllGroup(
                        new LiteralString("a"),
                        new ParamString("separator"),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true),
                        distinct: true
                    )
                ),
                new SelectItemGroup(
                    "percentileContinuous_parameter_fraction",
                    new PercentileContinuousDecimalGroup(
                        new LiteralDecimal(2.5m),
                        new ParamDecimal("fraction")
                    )
                ),
                new SelectItemGroup(
                    "percentileDiscrete_fraction_zero",
                    new PercentileDiscreteIntegerGroup(
                        new LiteralInteger(7),
                        new LiteralDecimal(0m)
                    )
                ),
                new SelectItemGroup(
                    "percentileDiscrete_fraction_one",
                    new PercentileDiscreteIntegerGroup(
                        new LiteralInteger(7),
                        new LiteralDecimal(1m)
                    )
                ),
                new SelectItemGroup(
                    "any_boolean_where",
                    new AnyGroup(new LiteralBoolean(true))
                ),
                new SelectItemGroup(
                    "all_boolean_where",
                    new AllGroup(new LiteralBoolean(true))
                ),
                new SelectItemGroup(
                    "count_integer_over_all",
                    new CountGroup(over: AggregateOver.All)
                ),
                new SelectItemGroup(
                    "count_integer_where_over_all",
                    new CountGroup(
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "countDistinct_integer_of_integer_over_all",
                    new CountDistinctGroup(new LiteralInteger(7), over: AggregateOver.All)
                ),
                new SelectItemGroup(
                    "countDistinct_integer_of_decimal_over_all",
                    new CountDistinctGroup(
                        new LiteralDecimal(2.5m),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "countDistinct_integer_of_string_over_all",
                    new CountDistinctGroup(
                        new LiteralString("a"),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "countDistinct_integer_of_boolean_over_all",
                    new CountDistinctGroup(
                        new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "countDistinct_integer_of_date_over_all",
                    new CountDistinctGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "countDistinct_integer_of_time_over_all",
                    new CountDistinctGroup(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "countDistinct_integer_of_datetime_over_all",
                    new CountDistinctGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "countDistinct_integer_of_uuid_over_all",
                    new CountDistinctGroup(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "countDistinct_integer_of_integer_null_over_all",
                    new CountDistinctGroup(
                        new LiteralIntegerNullable(),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "countDistinct_integer_of_string_null_over_all",
                    new CountDistinctGroup(
                        new LiteralStringNullable(),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "countDistinct_integer_of_uuid_null_over_all",
                    new CountDistinctGroup(
                        new LiteralUuidNullable(),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "countDistinct_integer_of_decimal_where_over_all",
                    new CountDistinctGroup(
                        new LiteralDecimal(2.5m),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "sum_integer_of_integer_over_all",
                    new SumIntegerGroup(new LiteralInteger(7), over: AggregateOver.All)
                ),
                new SelectItemGroup(
                    "sum_integer_of_integer_null_over_all",
                    new SumIntegerGroup(
                        new LiteralIntegerNullable(),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "sum_integer_of_integer_where_over_all",
                    new SumIntegerGroup(
                        new LiteralInteger(7),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "sum_decimal_of_decimal_over_all",
                    new SumDecimalGroup(new LiteralDecimal(2.5m), over: AggregateOver.All)
                ),
                new SelectItemGroup(
                    "sum_decimal_of_decimal_null_over_all",
                    new SumDecimalGroup(
                        new LiteralDecimalNullable(),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "sum_decimal_of_decimal_where_over_all",
                    new SumDecimalGroup(
                        new LiteralDecimal(2.5m),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "average_decimal_of_integer_over_all",
                    new AverageDecimalGroup(
                        new LiteralInteger(7),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "average_decimal_nullable_of_integer_null_over_all",
                    new AverageDecimalNullableGroup(
                        new LiteralIntegerNullable(),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "average_decimal_nullable_of_integer_where_over_all",
                    new AverageDecimalNullableGroup(
                        new LiteralInteger(7),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "average_decimal_of_decimal_over_all",
                    new AverageDecimalGroup(
                        new LiteralDecimal(2.5m),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "average_decimal_nullable_of_decimal_null_over_all",
                    new AverageDecimalNullableGroup(
                        new LiteralDecimalNullable(),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "average_decimal_nullable_of_decimal_where_over_all",
                    new AverageDecimalNullableGroup(
                        new LiteralDecimal(2.5m),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "average_date_of_date_over_all",
                    new AverageDateGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "average_date_nullable_of_date_null_over_all",
                    new AverageDateNullableGroup(
                        new LiteralDateNullable(),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "average_date_nullable_of_date_where_over_all",
                    new AverageDateNullableGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "average_time_of_time_over_all",
                    new AverageTimeGroup(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "average_time_nullable_of_time_null_over_all",
                    new AverageTimeNullableGroup(
                        new LiteralTimeNullable(),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "average_time_nullable_of_time_where_over_all",
                    new AverageTimeNullableGroup(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "average_datetime_of_datetime_over_all",
                    new AverageDatetimeGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "average_datetime_nullable_of_datetime_null_over_all",
                    new AverageDatetimeNullableGroup(
                        new LiteralDatetimeNullable(),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "average_datetime_nullable_of_datetime_where_over_all",
                    new AverageDatetimeNullableGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "min_integer_of_integer_over_all",
                    new MinIntegerGroup(new LiteralInteger(7), over: AggregateOver.All)
                ),
                new SelectItemGroup(
                    "min_integer_nullable_of_integer_null_over_all",
                    new MinIntegerNullableGroup(
                        new LiteralIntegerNullable(),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "min_integer_nullable_of_integer_where_over_all",
                    new MinIntegerNullableGroup(
                        new LiteralInteger(7),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "min_decimal_of_decimal_over_all",
                    new MinDecimalGroup(new LiteralDecimal(2.5m), over: AggregateOver.All)
                ),
                new SelectItemGroup(
                    "min_decimal_nullable_of_decimal_null_over_all",
                    new MinDecimalNullableGroup(
                        new LiteralDecimalNullable(),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "min_decimal_nullable_of_decimal_where_over_all",
                    new MinDecimalNullableGroup(
                        new LiteralDecimal(2.5m),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "min_string_of_string_over_all",
                    new MinStringGroup(new LiteralString("a"), over: AggregateOver.All)
                ),
                new SelectItemGroup(
                    "min_string_nullable_of_string_null_over_all",
                    new MinStringNullableGroup(
                        new LiteralStringNullable(),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "min_string_nullable_of_string_where_over_all",
                    new MinStringNullableGroup(
                        new LiteralString("a"),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "min_boolean_of_boolean_over_all",
                    new MinBooleanGroup(new LiteralBoolean(true), over: AggregateOver.All)
                ),
                new SelectItemGroup(
                    "min_boolean_nullable_of_boolean_null_over_all",
                    new MinBooleanNullableGroup(
                        new LiteralBooleanNullable(),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "min_boolean_nullable_of_boolean_where_over_all",
                    new MinBooleanNullableGroup(
                        new LiteralBoolean(true),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "min_date_of_date_over_all",
                    new MinDateGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "min_date_nullable_of_date_null_over_all",
                    new MinDateNullableGroup(
                        new LiteralDateNullable(),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "min_date_nullable_of_date_where_over_all",
                    new MinDateNullableGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "min_time_of_time_over_all",
                    new MinTimeGroup(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "min_time_nullable_of_time_null_over_all",
                    new MinTimeNullableGroup(
                        new LiteralTimeNullable(),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "min_time_nullable_of_time_where_over_all",
                    new MinTimeNullableGroup(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "min_datetime_of_datetime_over_all",
                    new MinDatetimeGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "min_datetime_nullable_of_datetime_null_over_all",
                    new MinDatetimeNullableGroup(
                        new LiteralDatetimeNullable(),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "min_datetime_nullable_of_datetime_where_over_all",
                    new MinDatetimeNullableGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "min_uuid_of_uuid_over_all",
                    new MinUuidGroup(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "min_uuid_nullable_of_uuid_null_over_all",
                    new MinUuidNullableGroup(
                        new LiteralUuidNullable(),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "min_uuid_nullable_of_uuid_where_over_all",
                    new MinUuidNullableGroup(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "max_integer_of_integer_over_all",
                    new MaxIntegerGroup(new LiteralInteger(7), over: AggregateOver.All)
                ),
                new SelectItemGroup(
                    "max_integer_nullable_of_integer_null_over_all",
                    new MaxIntegerNullableGroup(
                        new LiteralIntegerNullable(),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "max_integer_nullable_of_integer_where_over_all",
                    new MaxIntegerNullableGroup(
                        new LiteralInteger(7),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "max_decimal_of_decimal_over_all",
                    new MaxDecimalGroup(new LiteralDecimal(2.5m), over: AggregateOver.All)
                ),
                new SelectItemGroup(
                    "max_decimal_nullable_of_decimal_null_over_all",
                    new MaxDecimalNullableGroup(
                        new LiteralDecimalNullable(),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "max_decimal_nullable_of_decimal_where_over_all",
                    new MaxDecimalNullableGroup(
                        new LiteralDecimal(2.5m),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "max_string_of_string_over_all",
                    new MaxStringGroup(new LiteralString("a"), over: AggregateOver.All)
                ),
                new SelectItemGroup(
                    "max_string_nullable_of_string_null_over_all",
                    new MaxStringNullableGroup(
                        new LiteralStringNullable(),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "max_string_nullable_of_string_where_over_all",
                    new MaxStringNullableGroup(
                        new LiteralString("a"),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "max_boolean_of_boolean_over_all",
                    new MaxBooleanGroup(new LiteralBoolean(true), over: AggregateOver.All)
                ),
                new SelectItemGroup(
                    "max_boolean_nullable_of_boolean_null_over_all",
                    new MaxBooleanNullableGroup(
                        new LiteralBooleanNullable(),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "max_boolean_nullable_of_boolean_where_over_all",
                    new MaxBooleanNullableGroup(
                        new LiteralBoolean(true),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "max_date_of_date_over_all",
                    new MaxDateGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "max_date_nullable_of_date_null_over_all",
                    new MaxDateNullableGroup(
                        new LiteralDateNullable(),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "max_date_nullable_of_date_where_over_all",
                    new MaxDateNullableGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "max_time_of_time_over_all",
                    new MaxTimeGroup(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "max_time_nullable_of_time_null_over_all",
                    new MaxTimeNullableGroup(
                        new LiteralTimeNullable(),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "max_time_nullable_of_time_where_over_all",
                    new MaxTimeNullableGroup(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "max_datetime_of_datetime_over_all",
                    new MaxDatetimeGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "max_datetime_nullable_of_datetime_null_over_all",
                    new MaxDatetimeNullableGroup(
                        new LiteralDatetimeNullable(),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "max_datetime_nullable_of_datetime_where_over_all",
                    new MaxDatetimeNullableGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "max_uuid_of_uuid_over_all",
                    new MaxUuidGroup(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "max_uuid_nullable_of_uuid_null_over_all",
                    new MaxUuidNullableGroup(
                        new LiteralUuidNullable(),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "max_uuid_nullable_of_uuid_where_over_all",
                    new MaxUuidNullableGroup(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "first_integer_of_integer_over_all",
                    new FirstIntegerGroup(
                        new LiteralInteger(7),
                        [new OrderItemRow(new LiteralInteger(7))],
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "first_integer_nullable_of_integer_null_over_all",
                    new FirstIntegerNullableGroup(
                        new LiteralIntegerNullable(),
                        [
                            new OrderItemRow(
                                new LiteralString("a"),
                                direction: SortDirection.Desc
                            ),
                            new OrderItemRow(new LiteralDateNullable()),
                        ],
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "first_integer_nullable_of_integer_where_over_all",
                    new FirstIntegerNullableGroup(
                        new LiteralInteger(7),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "first_decimal_of_decimal_over_all",
                    new FirstDecimalGroup(
                        new LiteralDecimal(2.5m),
                        [new OrderItemRow(new LiteralInteger(7))],
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "first_decimal_nullable_of_decimal_null_over_all",
                    new FirstDecimalNullableGroup(
                        new LiteralDecimalNullable(),
                        [
                            new OrderItemRow(
                                new LiteralString("a"),
                                direction: SortDirection.Desc
                            ),
                            new OrderItemRow(new LiteralDateNullable()),
                        ],
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "first_decimal_nullable_of_decimal_where_over_all",
                    new FirstDecimalNullableGroup(
                        new LiteralDecimal(2.5m),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "first_string_of_string_over_all",
                    new FirstStringGroup(
                        new LiteralString("a"),
                        [new OrderItemRow(new LiteralInteger(7))],
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "first_string_nullable_of_string_null_over_all",
                    new FirstStringNullableGroup(
                        new LiteralStringNullable(),
                        [
                            new OrderItemRow(
                                new LiteralString("a"),
                                direction: SortDirection.Desc
                            ),
                            new OrderItemRow(new LiteralDateNullable()),
                        ],
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "first_string_nullable_of_string_where_over_all",
                    new FirstStringNullableGroup(
                        new LiteralString("a"),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "first_boolean_of_boolean_over_all",
                    new FirstBooleanGroup(
                        new LiteralBoolean(true),
                        [new OrderItemRow(new LiteralInteger(7))],
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "first_boolean_nullable_of_boolean_null_over_all",
                    new FirstBooleanNullableGroup(
                        new LiteralBooleanNullable(),
                        [
                            new OrderItemRow(
                                new LiteralString("a"),
                                direction: SortDirection.Desc
                            ),
                            new OrderItemRow(new LiteralDateNullable()),
                        ],
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "first_boolean_nullable_of_boolean_where_over_all",
                    new FirstBooleanNullableGroup(
                        new LiteralBoolean(true),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "first_date_of_date_over_all",
                    new FirstDateGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        [new OrderItemRow(new LiteralInteger(7))],
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "first_date_nullable_of_date_null_over_all",
                    new FirstDateNullableGroup(
                        new LiteralDateNullable(),
                        [
                            new OrderItemRow(
                                new LiteralString("a"),
                                direction: SortDirection.Desc
                            ),
                            new OrderItemRow(new LiteralDateNullable()),
                        ],
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "first_date_nullable_of_date_where_over_all",
                    new FirstDateNullableGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "first_time_of_time_over_all",
                    new FirstTimeGroup(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        [new OrderItemRow(new LiteralInteger(7))],
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "first_time_nullable_of_time_null_over_all",
                    new FirstTimeNullableGroup(
                        new LiteralTimeNullable(),
                        [
                            new OrderItemRow(
                                new LiteralString("a"),
                                direction: SortDirection.Desc
                            ),
                            new OrderItemRow(new LiteralDateNullable()),
                        ],
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "first_time_nullable_of_time_where_over_all",
                    new FirstTimeNullableGroup(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "first_datetime_of_datetime_over_all",
                    new FirstDatetimeGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        [new OrderItemRow(new LiteralInteger(7))],
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "first_datetime_nullable_of_datetime_null_over_all",
                    new FirstDatetimeNullableGroup(
                        new LiteralDatetimeNullable(),
                        [
                            new OrderItemRow(
                                new LiteralString("a"),
                                direction: SortDirection.Desc
                            ),
                            new OrderItemRow(new LiteralDateNullable()),
                        ],
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "first_datetime_nullable_of_datetime_where_over_all",
                    new FirstDatetimeNullableGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "first_uuid_of_uuid_over_all",
                    new FirstUuidGroup(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        [new OrderItemRow(new LiteralInteger(7))],
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "first_uuid_nullable_of_uuid_null_over_all",
                    new FirstUuidNullableGroup(
                        new LiteralUuidNullable(),
                        [
                            new OrderItemRow(
                                new LiteralString("a"),
                                direction: SortDirection.Desc
                            ),
                            new OrderItemRow(new LiteralDateNullable()),
                        ],
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "first_uuid_nullable_of_uuid_where_over_all",
                    new FirstUuidNullableGroup(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "last_integer_of_integer_over_all",
                    new LastIntegerGroup(
                        new LiteralInteger(7),
                        [new OrderItemRow(new LiteralInteger(7))],
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "last_integer_nullable_of_integer_null_over_all",
                    new LastIntegerNullableGroup(
                        new LiteralIntegerNullable(),
                        [
                            new OrderItemRow(
                                new LiteralString("a"),
                                direction: SortDirection.Desc
                            ),
                            new OrderItemRow(new LiteralDateNullable()),
                        ],
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "last_integer_nullable_of_integer_where_over_all",
                    new LastIntegerNullableGroup(
                        new LiteralInteger(7),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "last_decimal_of_decimal_over_all",
                    new LastDecimalGroup(
                        new LiteralDecimal(2.5m),
                        [new OrderItemRow(new LiteralInteger(7))],
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "last_decimal_nullable_of_decimal_null_over_all",
                    new LastDecimalNullableGroup(
                        new LiteralDecimalNullable(),
                        [
                            new OrderItemRow(
                                new LiteralString("a"),
                                direction: SortDirection.Desc
                            ),
                            new OrderItemRow(new LiteralDateNullable()),
                        ],
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "last_decimal_nullable_of_decimal_where_over_all",
                    new LastDecimalNullableGroup(
                        new LiteralDecimal(2.5m),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "last_string_of_string_over_all",
                    new LastStringGroup(
                        new LiteralString("a"),
                        [new OrderItemRow(new LiteralInteger(7))],
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "last_string_nullable_of_string_null_over_all",
                    new LastStringNullableGroup(
                        new LiteralStringNullable(),
                        [
                            new OrderItemRow(
                                new LiteralString("a"),
                                direction: SortDirection.Desc
                            ),
                            new OrderItemRow(new LiteralDateNullable()),
                        ],
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "last_string_nullable_of_string_where_over_all",
                    new LastStringNullableGroup(
                        new LiteralString("a"),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "last_boolean_of_boolean_over_all",
                    new LastBooleanGroup(
                        new LiteralBoolean(true),
                        [new OrderItemRow(new LiteralInteger(7))],
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "last_boolean_nullable_of_boolean_null_over_all",
                    new LastBooleanNullableGroup(
                        new LiteralBooleanNullable(),
                        [
                            new OrderItemRow(
                                new LiteralString("a"),
                                direction: SortDirection.Desc
                            ),
                            new OrderItemRow(new LiteralDateNullable()),
                        ],
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "last_boolean_nullable_of_boolean_where_over_all",
                    new LastBooleanNullableGroup(
                        new LiteralBoolean(true),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "last_date_of_date_over_all",
                    new LastDateGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        [new OrderItemRow(new LiteralInteger(7))],
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "last_date_nullable_of_date_null_over_all",
                    new LastDateNullableGroup(
                        new LiteralDateNullable(),
                        [
                            new OrderItemRow(
                                new LiteralString("a"),
                                direction: SortDirection.Desc
                            ),
                            new OrderItemRow(new LiteralDateNullable()),
                        ],
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "last_date_nullable_of_date_where_over_all",
                    new LastDateNullableGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "last_time_of_time_over_all",
                    new LastTimeGroup(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        [new OrderItemRow(new LiteralInteger(7))],
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "last_time_nullable_of_time_null_over_all",
                    new LastTimeNullableGroup(
                        new LiteralTimeNullable(),
                        [
                            new OrderItemRow(
                                new LiteralString("a"),
                                direction: SortDirection.Desc
                            ),
                            new OrderItemRow(new LiteralDateNullable()),
                        ],
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "last_time_nullable_of_time_where_over_all",
                    new LastTimeNullableGroup(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "last_datetime_of_datetime_over_all",
                    new LastDatetimeGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        [new OrderItemRow(new LiteralInteger(7))],
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "last_datetime_nullable_of_datetime_null_over_all",
                    new LastDatetimeNullableGroup(
                        new LiteralDatetimeNullable(),
                        [
                            new OrderItemRow(
                                new LiteralString("a"),
                                direction: SortDirection.Desc
                            ),
                            new OrderItemRow(new LiteralDateNullable()),
                        ],
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "last_datetime_nullable_of_datetime_where_over_all",
                    new LastDatetimeNullableGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "last_uuid_of_uuid_over_all",
                    new LastUuidGroup(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        [new OrderItemRow(new LiteralInteger(7))],
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "last_uuid_nullable_of_uuid_null_over_all",
                    new LastUuidNullableGroup(
                        new LiteralUuidNullable(),
                        [
                            new OrderItemRow(
                                new LiteralString("a"),
                                direction: SortDirection.Desc
                            ),
                            new OrderItemRow(new LiteralDateNullable()),
                        ],
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "last_uuid_nullable_of_uuid_where_over_all",
                    new LastUuidNullableGroup(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "median_decimal_of_integer_over_all",
                    new MedianDecimalGroup(new LiteralInteger(7), over: AggregateOver.All)
                ),
                new SelectItemGroup(
                    "median_decimal_nullable_of_integer_null_over_all",
                    new MedianDecimalNullableGroup(
                        new LiteralIntegerNullable(),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "median_decimal_nullable_of_integer_where_over_all",
                    new MedianDecimalNullableGroup(
                        new LiteralInteger(7),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "median_decimal_of_decimal_over_all",
                    new MedianDecimalGroup(
                        new LiteralDecimal(2.5m),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "median_decimal_nullable_of_decimal_null_over_all",
                    new MedianDecimalNullableGroup(
                        new LiteralDecimalNullable(),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "median_decimal_nullable_of_decimal_where_over_all",
                    new MedianDecimalNullableGroup(
                        new LiteralDecimal(2.5m),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "median_date_of_date_over_all",
                    new MedianDateGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "median_date_nullable_of_date_null_over_all",
                    new MedianDateNullableGroup(
                        new LiteralDateNullable(),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "median_date_nullable_of_date_where_over_all",
                    new MedianDateNullableGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "median_time_of_time_over_all",
                    new MedianTimeGroup(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "median_time_nullable_of_time_null_over_all",
                    new MedianTimeNullableGroup(
                        new LiteralTimeNullable(),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "median_time_nullable_of_time_where_over_all",
                    new MedianTimeNullableGroup(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "median_datetime_of_datetime_over_all",
                    new MedianDatetimeGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "median_datetime_nullable_of_datetime_null_over_all",
                    new MedianDatetimeNullableGroup(
                        new LiteralDatetimeNullable(),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "median_datetime_nullable_of_datetime_where_over_all",
                    new MedianDatetimeNullableGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "percentileContinuous_decimal_of_integer_over_all",
                    new PercentileContinuousDecimalGroup(
                        new LiteralInteger(7),
                        new LiteralDecimal(0.9m),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "percentileContinuous_decimal_nullable_of_integer_null_over_all",
                    new PercentileContinuousDecimalNullableGroup(
                        new LiteralIntegerNullable(),
                        new LiteralDecimal(0.9m),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "percentileContinuous_decimal_nullable_of_integer_where_over_all",
                    new PercentileContinuousDecimalNullableGroup(
                        new LiteralInteger(7),
                        new LiteralDecimal(0.9m),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "percentileContinuous_decimal_of_decimal_over_all",
                    new PercentileContinuousDecimalGroup(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.9m),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "percentileContinuous_decimal_nullable_of_decimal_null_over_all",
                    new PercentileContinuousDecimalNullableGroup(
                        new LiteralDecimalNullable(),
                        new LiteralDecimal(0.9m),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "percentileContinuous_decimal_nullable_of_decimal_where_over_all",
                    new PercentileContinuousDecimalNullableGroup(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.9m),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "percentileContinuous_date_of_date_over_all",
                    new PercentileContinuousDateGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDecimal(0.9m),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "percentileContinuous_date_nullable_of_date_null_over_all",
                    new PercentileContinuousDateNullableGroup(
                        new LiteralDateNullable(),
                        new LiteralDecimal(0.9m),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "percentileContinuous_date_nullable_of_date_where_over_all",
                    new PercentileContinuousDateNullableGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDecimal(0.9m),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "percentileContinuous_time_of_time_over_all",
                    new PercentileContinuousTimeGroup(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralDecimal(0.9m),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "percentileContinuous_time_nullable_of_time_null_over_all",
                    new PercentileContinuousTimeNullableGroup(
                        new LiteralTimeNullable(),
                        new LiteralDecimal(0.9m),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "percentileContinuous_time_nullable_of_time_where_over_all",
                    new PercentileContinuousTimeNullableGroup(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralDecimal(0.9m),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "percentileContinuous_datetime_of_datetime_over_all",
                    new PercentileContinuousDatetimeGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDecimal(0.9m),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "percentileContinuous_datetime_nullable_of_datetime_null_over_all",
                    new PercentileContinuousDatetimeNullableGroup(
                        new LiteralDatetimeNullable(),
                        new LiteralDecimal(0.9m),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "percentileContinuous_datetime_nullable_of_datetime_where_over_all",
                    new PercentileContinuousDatetimeNullableGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDecimal(0.9m),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "percentileDiscrete_integer_of_integer_over_all",
                    new PercentileDiscreteIntegerGroup(
                        new LiteralInteger(7),
                        new LiteralDecimal(0.9m),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "percentileDiscrete_integer_nullable_of_integer_null_over_all",
                    new PercentileDiscreteIntegerNullableGroup(
                        new LiteralIntegerNullable(),
                        new LiteralDecimal(0.9m),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "percentileDiscrete_integer_nullable_of_integer_where_over_all",
                    new PercentileDiscreteIntegerNullableGroup(
                        new LiteralInteger(7),
                        new LiteralDecimal(0.9m),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "percentileDiscrete_decimal_of_decimal_over_all",
                    new PercentileDiscreteDecimalGroup(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.9m),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "percentileDiscrete_decimal_nullable_of_decimal_null_over_all",
                    new PercentileDiscreteDecimalNullableGroup(
                        new LiteralDecimalNullable(),
                        new LiteralDecimal(0.9m),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "percentileDiscrete_decimal_nullable_of_decimal_where_over_all",
                    new PercentileDiscreteDecimalNullableGroup(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.9m),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "percentileDiscrete_string_of_string_over_all",
                    new PercentileDiscreteStringGroup(
                        new LiteralString("a"),
                        new LiteralDecimal(0.9m),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "percentileDiscrete_string_nullable_of_string_null_over_all",
                    new PercentileDiscreteStringNullableGroup(
                        new LiteralStringNullable(),
                        new LiteralDecimal(0.9m),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "percentileDiscrete_string_nullable_of_string_where_over_all",
                    new PercentileDiscreteStringNullableGroup(
                        new LiteralString("a"),
                        new LiteralDecimal(0.9m),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "percentileDiscrete_boolean_of_boolean_over_all",
                    new PercentileDiscreteBooleanGroup(
                        new LiteralBoolean(true),
                        new LiteralDecimal(0.9m),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "percentileDiscrete_boolean_nullable_of_boolean_null_over_all",
                    new PercentileDiscreteBooleanNullableGroup(
                        new LiteralBooleanNullable(),
                        new LiteralDecimal(0.9m),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "percentileDiscrete_boolean_nullable_of_boolean_where_over_all",
                    new PercentileDiscreteBooleanNullableGroup(
                        new LiteralBoolean(true),
                        new LiteralDecimal(0.9m),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "percentileDiscrete_date_of_date_over_all",
                    new PercentileDiscreteDateGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDecimal(0.9m),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "percentileDiscrete_date_nullable_of_date_null_over_all",
                    new PercentileDiscreteDateNullableGroup(
                        new LiteralDateNullable(),
                        new LiteralDecimal(0.9m),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "percentileDiscrete_date_nullable_of_date_where_over_all",
                    new PercentileDiscreteDateNullableGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDecimal(0.9m),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "percentileDiscrete_time_of_time_over_all",
                    new PercentileDiscreteTimeGroup(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralDecimal(0.9m),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "percentileDiscrete_time_nullable_of_time_null_over_all",
                    new PercentileDiscreteTimeNullableGroup(
                        new LiteralTimeNullable(),
                        new LiteralDecimal(0.9m),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "percentileDiscrete_time_nullable_of_time_where_over_all",
                    new PercentileDiscreteTimeNullableGroup(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralDecimal(0.9m),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "percentileDiscrete_datetime_of_datetime_over_all",
                    new PercentileDiscreteDatetimeGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDecimal(0.9m),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "percentileDiscrete_datetime_nullable_of_datetime_null_over_all",
                    new PercentileDiscreteDatetimeNullableGroup(
                        new LiteralDatetimeNullable(),
                        new LiteralDecimal(0.9m),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "percentileDiscrete_datetime_nullable_of_datetime_where_over_all",
                    new PercentileDiscreteDatetimeNullableGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDecimal(0.9m),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "percentileDiscrete_uuid_of_uuid_over_all",
                    new PercentileDiscreteUuidGroup(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralDecimal(0.9m),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "percentileDiscrete_uuid_nullable_of_uuid_null_over_all",
                    new PercentileDiscreteUuidNullableGroup(
                        new LiteralUuidNullable(),
                        new LiteralDecimal(0.9m),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "percentileDiscrete_uuid_nullable_of_uuid_where_over_all",
                    new PercentileDiscreteUuidNullableGroup(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        new LiteralDecimal(0.9m),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "mode_integer_of_integer_over_all",
                    new ModeIntegerGroup(new LiteralInteger(7), over: AggregateOver.All)
                ),
                new SelectItemGroup(
                    "mode_integer_nullable_of_integer_null_over_all",
                    new ModeIntegerNullableGroup(
                        new LiteralIntegerNullable(),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "mode_integer_nullable_of_integer_where_over_all",
                    new ModeIntegerNullableGroup(
                        new LiteralInteger(7),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "mode_decimal_of_decimal_over_all",
                    new ModeDecimalGroup(
                        new LiteralDecimal(2.5m),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "mode_decimal_nullable_of_decimal_null_over_all",
                    new ModeDecimalNullableGroup(
                        new LiteralDecimalNullable(),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "mode_decimal_nullable_of_decimal_where_over_all",
                    new ModeDecimalNullableGroup(
                        new LiteralDecimal(2.5m),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "mode_string_of_string_over_all",
                    new ModeStringGroup(new LiteralString("a"), over: AggregateOver.All)
                ),
                new SelectItemGroup(
                    "mode_string_nullable_of_string_null_over_all",
                    new ModeStringNullableGroup(
                        new LiteralStringNullable(),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "mode_string_nullable_of_string_where_over_all",
                    new ModeStringNullableGroup(
                        new LiteralString("a"),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "mode_boolean_of_boolean_over_all",
                    new ModeBooleanGroup(
                        new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "mode_boolean_nullable_of_boolean_null_over_all",
                    new ModeBooleanNullableGroup(
                        new LiteralBooleanNullable(),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "mode_boolean_nullable_of_boolean_where_over_all",
                    new ModeBooleanNullableGroup(
                        new LiteralBoolean(true),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "mode_date_of_date_over_all",
                    new ModeDateGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "mode_date_nullable_of_date_null_over_all",
                    new ModeDateNullableGroup(
                        new LiteralDateNullable(),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "mode_date_nullable_of_date_where_over_all",
                    new ModeDateNullableGroup(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "mode_time_of_time_over_all",
                    new ModeTimeGroup(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "mode_time_nullable_of_time_null_over_all",
                    new ModeTimeNullableGroup(
                        new LiteralTimeNullable(),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "mode_time_nullable_of_time_where_over_all",
                    new ModeTimeNullableGroup(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "mode_datetime_of_datetime_over_all",
                    new ModeDatetimeGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "mode_datetime_nullable_of_datetime_null_over_all",
                    new ModeDatetimeNullableGroup(
                        new LiteralDatetimeNullable(),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "mode_datetime_nullable_of_datetime_where_over_all",
                    new ModeDatetimeNullableGroup(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "mode_uuid_of_uuid_over_all",
                    new ModeUuidGroup(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "mode_uuid_nullable_of_uuid_null_over_all",
                    new ModeUuidNullableGroup(
                        new LiteralUuidNullable(),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "mode_uuid_nullable_of_uuid_where_over_all",
                    new ModeUuidNullableGroup(
                        new LiteralUuid(new Guid(1, 0, 0, new byte[8])),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "variancePopulation_decimal_of_integer_over_all",
                    new VariancePopulationDecimalGroup(
                        new LiteralInteger(7),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "variancePopulation_decimal_nullable_of_integer_null_over_all",
                    new VariancePopulationDecimalNullableGroup(
                        new LiteralIntegerNullable(),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "variancePopulation_decimal_nullable_of_integer_where_over_all",
                    new VariancePopulationDecimalNullableGroup(
                        new LiteralInteger(7),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "variancePopulation_decimal_of_decimal_over_all",
                    new VariancePopulationDecimalGroup(
                        new LiteralDecimal(2.5m),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "variancePopulation_decimal_nullable_of_decimal_null_over_all",
                    new VariancePopulationDecimalNullableGroup(
                        new LiteralDecimalNullable(),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "variancePopulation_decimal_nullable_of_decimal_where_over_all",
                    new VariancePopulationDecimalNullableGroup(
                        new LiteralDecimal(2.5m),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "standardDeviationPopulation_decimal_of_integer_over_all",
                    new StandardDeviationPopulationDecimalGroup(
                        new LiteralInteger(7),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "standardDeviationPopulation_decimal_nullable_of_integer_null_over_all",
                    new StandardDeviationPopulationDecimalNullableGroup(
                        new LiteralIntegerNullable(),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "standardDeviationPopulation_decimal_nullable_of_integer_where_over_all",
                    new StandardDeviationPopulationDecimalNullableGroup(
                        new LiteralInteger(7),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "standardDeviationPopulation_decimal_of_decimal_over_all",
                    new StandardDeviationPopulationDecimalGroup(
                        new LiteralDecimal(2.5m),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "standardDeviationPopulation_decimal_nullable_of_decimal_null_over_all",
                    new StandardDeviationPopulationDecimalNullableGroup(
                        new LiteralDecimalNullable(),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "standardDeviationPopulation_decimal_nullable_of_decimal_where_over_all",
                    new StandardDeviationPopulationDecimalNullableGroup(
                        new LiteralDecimal(2.5m),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "varianceSample_decimal_nullable_of_integer_over_all",
                    new VarianceSampleGroup(
                        new LiteralInteger(7),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "varianceSample_decimal_nullable_of_decimal_over_all",
                    new VarianceSampleGroup(
                        new LiteralDecimal(2.5m),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "varianceSample_decimal_nullable_of_decimal_null_over_all",
                    new VarianceSampleGroup(
                        new LiteralDecimalNullable(),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "varianceSample_decimal_nullable_of_decimal_where_over_all",
                    new VarianceSampleGroup(
                        new LiteralDecimal(2.5m),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "standardDeviationSample_decimal_nullable_of_integer_over_all",
                    new StandardDeviationSampleGroup(
                        new LiteralInteger(7),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "standardDeviationSample_decimal_nullable_of_decimal_over_all",
                    new StandardDeviationSampleGroup(
                        new LiteralDecimal(2.5m),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "standardDeviationSample_decimal_nullable_of_decimal_null_over_all",
                    new StandardDeviationSampleGroup(
                        new LiteralDecimalNullable(),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "standardDeviationSample_decimal_nullable_of_decimal_where_over_all",
                    new StandardDeviationSampleGroup(
                        new LiteralDecimal(2.5m),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "covariancePopulation_decimal_over_all",
                    new CovariancePopulationDecimalGroup(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimal(0.1m),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "covariancePopulation_decimal_of_integers_over_all",
                    new CovariancePopulationDecimalGroup(
                        new LiteralInteger(7),
                        new LiteralInteger(-3),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "covariancePopulation_decimal_nullable_left_over_all",
                    new CovariancePopulationDecimalNullableGroup(
                        new LiteralDecimalNullable(),
                        new LiteralDecimal(2.5m),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "covariancePopulation_decimal_nullable_right_over_all",
                    new CovariancePopulationDecimalNullableGroup(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimalNullable(),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "covariancePopulation_decimal_nullable_where_over_all",
                    new CovariancePopulationDecimalNullableGroup(
                        new LiteralDecimal(2.5m),
                        new LiteralInteger(7),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "covarianceSample_decimal_nullable_over_all",
                    new CovarianceSampleGroup(
                        new LiteralDecimal(2.5m),
                        new LiteralInteger(7),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "covarianceSample_decimal_nullable_of_nulls_where_over_all",
                    new CovarianceSampleGroup(
                        new LiteralDecimalNullable(),
                        new LiteralIntegerNullable(),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "correlation_decimal_nullable_over_all",
                    new CorrelationGroup(
                        new LiteralDecimal(2.5m),
                        new LiteralInteger(7),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "correlation_decimal_nullable_of_nulls_where_over_all",
                    new CorrelationGroup(
                        new LiteralDecimalNullable(),
                        new LiteralDecimalNullable(),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "regressionSlope_decimal_nullable_over_all",
                    new RegressionSlopeGroup(
                        new LiteralDecimal(2.5m),
                        new LiteralInteger(7),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "regressionSlope_decimal_nullable_of_integers_over_all",
                    new RegressionSlopeGroup(
                        new LiteralInteger(7),
                        new LiteralInteger(-3),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "regressionSlope_decimal_nullable_of_nulls_where_over_all",
                    new RegressionSlopeGroup(
                        new LiteralDecimalNullable(),
                        new LiteralIntegerNullable(),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "regressionIntercept_decimal_nullable_over_all",
                    new RegressionInterceptGroup(
                        new LiteralDecimal(2.5m),
                        new LiteralInteger(7),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "regressionIntercept_decimal_nullable_of_integers_over_all",
                    new RegressionInterceptGroup(
                        new LiteralInteger(7),
                        new LiteralInteger(-3),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "regressionIntercept_decimal_nullable_of_nulls_where_over_all",
                    new RegressionInterceptGroup(
                        new LiteralDecimalNullable(),
                        new LiteralIntegerNullable(),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "regressionRSquared_decimal_nullable_over_all",
                    new RegressionRSquaredGroup(
                        new LiteralDecimal(2.5m),
                        new LiteralInteger(7),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "regressionRSquared_decimal_nullable_of_integers_over_all",
                    new RegressionRSquaredGroup(
                        new LiteralInteger(7),
                        new LiteralInteger(-3),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "regressionRSquared_decimal_nullable_of_nulls_where_over_all",
                    new RegressionRSquaredGroup(
                        new LiteralDecimalNullable(),
                        new LiteralIntegerNullable(),
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "concatAll_string_of_string_over_all",
                    new ConcatAllGroup(
                        new LiteralString("a"),
                        new LiteralString(","),
                        [new OrderItemRow(new LiteralInteger(7))],
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "concatAll_string_of_string_null_over_all",
                    new ConcatAllGroup(
                        new LiteralStringNullable(),
                        new LiteralString(","),
                        [
                            new OrderItemRow(
                                new LiteralString("a"),
                                direction: SortDirection.Desc
                            ),
                            new OrderItemRow(new LiteralDateNullable()),
                        ],
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "concatAll_string_of_string_where_over_all",
                    new ConcatAllGroup(
                        new LiteralString("a"),
                        new LiteralString(","),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "concatAll_string_of_string_distinct_over_all",
                    new ConcatAllGroup(
                        new LiteralString("a"),
                        new LiteralString(""),
                        [new OrderItemRow(new LiteralInteger(7))],
                        over: AggregateOver.All,
                        distinct: true
                    )
                ),
                new SelectItemGroup(
                    "concatAll_string_parameter_separator_over_all",
                    new ConcatAllGroup(
                        new LiteralString("a"),
                        new ParamString("separator"),
                        [new OrderItemRow(new LiteralInteger(7))],
                        predicate: new LiteralBoolean(true),
                        over: AggregateOver.All,
                        distinct: true
                    )
                ),
                new SelectItemGroup(
                    "percentileContinuous_parameter_fraction_over_all",
                    new PercentileContinuousDecimalGroup(
                        new LiteralDecimal(2.5m),
                        new ParamDecimal("fraction"),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "percentileDiscrete_fraction_zero_over_all",
                    new PercentileDiscreteIntegerGroup(
                        new LiteralInteger(7),
                        new LiteralDecimal(0m),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "percentileDiscrete_fraction_one_over_all",
                    new PercentileDiscreteIntegerGroup(
                        new LiteralInteger(7),
                        new LiteralDecimal(1m),
                        over: AggregateOver.All
                    )
                ),
                new SelectItemGroup(
                    "any_boolean_where_over_all",
                    new AnyGroup(new LiteralBoolean(true), over: AggregateOver.All)
                ),
                new SelectItemGroup(
                    "all_boolean_where_over_all",
                    new AllGroup(new LiteralBoolean(true), over: AggregateOver.All)
                ),
            ]
        );
}
