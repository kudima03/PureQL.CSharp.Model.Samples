using Pure.Collections.Generic;
using Pure.Primitives.Bool;
using Pure.Primitives.Date;
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
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Nulls;

/// <summary>
/// Selects from schema_with_foreign_keys.users, each user's name with a combining acute
/// accent normalized to NFC (normalize with the form NFC), the name with a precomposed
/// accent normalized to NFKD, nullable because it is null for Bob (nullIfEqual), the
/// age in whole years on 2024-06-30 as dateDiffDays over 365.25 rounded to an integer
/// with the mode halfEven, 2.345 rounded to 2 digits with halfEven, whether the address
/// (the name, an at sign and example.org for an active user and mail.net otherwise)
/// matches the regular expression ^[A-C][a-z]*@example[.]org$, its domain (extractRegex
/// with the group 1), the names of all users joined by a comma and a space in the order
/// of the names (concatAll with a separator), the percentileDiscrete of signup_date
/// with the fraction 0.75, the ntile of 4 buckets and the previous signup_date with the
/// offset 2 (the signup_date itself as the default) over signup_date and user_id,
/// ordered by the name normalized to NFKC and user_id. Every form, mode, pattern,
/// group, separator, fraction, bucket count and offset is a literal that is never null
/// and never makes a result nullable.
/// </summary>
public sealed record ConstantOperandsNeverNullableQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithoutPagination(
            from: new FromEntity(
                new JoinedString(
                    new DotString(),
                    [new RelationalSchemaWithForeignKeys().Name, new UsersTable().Name]
                ).TextValue
            ),
            select:
            [
                new SelectItemProjection(
                    "name_nfc",
                    new NormalizeStringProjection(
                        new ConcatStringProjection([
                            new FieldString(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new UsersTable().Name,
                                    ]
                                ).TextValue,
                                new UserNameColumn().Name.TextValue
                            ),
                            new LiteralString("e\u0301"),
                        ]),
                        NormalizationForm.NFC
                    )
                ),
                new SelectItemProjection(
                    "middle_nfkd",
                    new NormalizeStringNullableProjection(
                        new NullIfEqualStringNullableProjection(
                            new ConcatStringProjection([
                                new FieldString(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new UsersTable().Name,
                                        ]
                                    ).TextValue,
                                    new UserNameColumn().Name.TextValue
                                ),
                                new LiteralString("\u00e9"),
                            ]),
                            new LiteralString("Bob\u00e9")
                        ),
                        NormalizationForm.NFKD
                    )
                ),
                new SelectItemProjection(
                    "whole_age",
                    new RoundWithoutDigitsIntegerProjection(
                        new DivideDecimalProjection([
                            new DateDiffDaysIntegerProjection(
                                new LiteralDate(new DateOnly(2024, 6, 30)),
                                new FieldDate(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new UsersTable().Name,
                                        ]
                                    ).TextValue,
                                    new SignupDateColumn().Name.TextValue
                                )
                            ),
                            new LiteralDecimal(365.25m),
                        ]),
                        RoundingMode.HalfEven
                    )
                ),
                new SelectItemProjection(
                    "rate",
                    new RoundDecimalProjection(
                        new LiteralDecimal(2.345m),
                        new LiteralInteger(2),
                        RoundingMode.HalfEven
                    )
                ),
                new SelectItemProjection(
                    "has_domain",
                    new MatchesRegexProjection(
                        new ConcatStringProjection([
                            new FieldString(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new UsersTable().Name,
                                    ]
                                ).TextValue,
                                new UserNameColumn().Name.TextValue
                            ),
                            new LiteralString("@"),
                            new IfStringProjection(
                                new LiteralString("example.org"),
                                new LiteralString("mail.net"),
                                new FieldBoolean(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new UsersTable().Name,
                                        ]
                                    ).TextValue,
                                    new UserActiveColumn().Name.TextValue
                                )
                            ),
                        ]),
                        new RegexPattern(new LiteralString("^[A-C][a-z]*@example[.]org$"))
                    )
                ),
                new SelectItemProjection(
                    "domain",
                    new ExtractRegexProjection(
                        new ConcatStringProjection([
                            new FieldString(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new UsersTable().Name,
                                    ]
                                ).TextValue,
                                new UserNameColumn().Name.TextValue
                            ),
                            new LiteralString("@"),
                            new IfStringProjection(
                                new LiteralString("example.org"),
                                new LiteralString("mail.net"),
                                new FieldBoolean(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new UsersTable().Name,
                                        ]
                                    ).TextValue,
                                    new UserActiveColumn().Name.TextValue
                                )
                            ),
                        ]),
                        new RegexPattern(new LiteralString("@(.+)$")),
                        new RegexGroup(new LiteralInteger(1))
                    )
                ),
                new SelectItemProjection(
                    "all_names",
                    new ConcatAllProjection(
                        new FieldString(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue,
                            new UserNameColumn().Name.TextValue
                        ),
                        new Separator(new LiteralString(", ")),
                        [
                            new OrderItemRow(
                                new FieldString(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new UsersTable().Name,
                                        ]
                                    ).TextValue,
                                    new UserNameColumn().Name.TextValue
                                )
                            ),
                        ]
                    )
                ),
                new SelectItemProjection(
                    "signup_quantile",
                    new PercentileDiscreteDateProjection(
                        new FieldDate(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue,
                            new SignupDateColumn().Name.TextValue
                        ),
                        new Fraction(new LiteralDecimal(0.75m))
                    )
                ),
                new SelectItemProjection(
                    "bucket",
                    new NtileProjection(
                        new Buckets(new LiteralInteger(4)),
                        new OrderedWindow([
                            new OrderItemRow(
                                new FieldDate(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new UsersTable().Name,
                                        ]
                                    ).TextValue,
                                    new SignupDateColumn().Name.TextValue
                                )
                            ),
                            new OrderItemRow(
                                new FieldUuid(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new UsersTable().Name,
                                        ]
                                    ).TextValue,
                                    new UserIdColumn().Name.TextValue
                                )
                            ),
                        ])
                    )
                ),
                new SelectItemProjection(
                    "earlier_signup",
                    new PreviousDateProjection(
                        new FieldDate(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue,
                            new SignupDateColumn().Name.TextValue
                        ),
                        new FieldDate(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue,
                            new SignupDateColumn().Name.TextValue
                        ),
                        new OrderedWindow([
                            new OrderItemRow(
                                new FieldDate(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new UsersTable().Name,
                                        ]
                                    ).TextValue,
                                    new SignupDateColumn().Name.TextValue
                                )
                            ),
                            new OrderItemRow(
                                new FieldUuid(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new UsersTable().Name,
                                        ]
                                    ).TextValue,
                                    new UserIdColumn().Name.TextValue
                                )
                            ),
                        ]),
                        new Offset(new LiteralInteger(2))
                    )
                ),
            ],
            orderBy:
            [
                new OrderItemProjectionExpression(
                    new NormalizeStringProjection(
                        new FieldString(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new UsersTable().Name,
                                ]
                            ).TextValue,
                            new UserNameColumn().Name.TextValue
                        ),
                        NormalizationForm.NFKC
                    )
                ),
                new OrderItemProjectionExpression(
                    new FieldUuid(
                        new JoinedString(
                            new DotString(),
                            [
                                new RelationalSchemaWithForeignKeys().Name,
                                new UsersTable().Name,
                            ]
                        ).TextValue,
                        new UserIdColumn().Name.TextValue
                    )
                ),
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over <see
    /// cref="SchemaDataSetWithForeignKeys"/> and <see cref="AuditSchemaDataSet"/>, in
    /// this order. The normalizations are PostgreSQL's, checked with Python
    /// unicodedata, and the halfEven rounding of 2.345 is computed exactly with Python
    /// decimal.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(new String("name_nfc"), new StringColumnType()),
                    new Column(new String("middle_nfkd"), new StringColumnType()),
                    new Column(new String("whole_age"), new LongColumnType()),
                    new Column(new String("rate"), new DoubleColumnType()),
                    new Column(new String("has_domain"), new BoolColumnType()),
                    new Column(new String("domain"), new StringColumnType()),
                    new Column(new String("all_names"), new StringColumnType()),
                    new Column(new String("signup_quantile"), new DateColumnType()),
                    new Column(new String("bucket"), new LongColumnType()),
                    new Column(new String("earlier_signup"), new DateColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("name_nfc"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Ann\u00e9"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("middle_nfkd"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Anne\u0301"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("whole_age"), new LongColumnType()),
                                new InvariantCell(new Long(4))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("rate"), new DoubleColumnType()),
                                new InvariantCell(new Double(2.34))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("has_domain"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("domain"), new StringColumnType()),
                                new InvariantCell(new String("example.org"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("all_names"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(
                                    new String("Ann, Bob, Cara, Dan, Eve, Fay")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("signup_quantile"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(5),
                                        new UShort(11),
                                        new UShort(2022)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("bucket"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("earlier_signup"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(15),
                                        new UShort(1),
                                        new UShort(2020)
                                    )
                                )
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
                                    new String("name_nfc"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Bob\u00e9"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("middle_nfkd"),
                                    new StringColumnType()
                                ),
                                new EmptyCell()
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("whole_age"), new LongColumnType()),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("rate"), new DoubleColumnType()),
                                new InvariantCell(new Double(2.34))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("has_domain"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("domain"), new StringColumnType()),
                                new InvariantCell(new String("mail.net"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("all_names"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(
                                    new String("Ann, Bob, Cara, Dan, Eve, Fay")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("signup_quantile"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(5),
                                        new UShort(11),
                                        new UShort(2022)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("bucket"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("earlier_signup"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(15),
                                        new UShort(1),
                                        new UShort(2020)
                                    )
                                )
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
                                    new String("name_nfc"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Cara\u00e9"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("middle_nfkd"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Carae\u0301"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("whole_age"), new LongColumnType()),
                                new InvariantCell(new Long(5))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("rate"), new DoubleColumnType()),
                                new InvariantCell(new Double(2.34))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("has_domain"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("domain"), new StringColumnType()),
                                new InvariantCell(new String("example.org"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("all_names"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(
                                    new String("Ann, Bob, Cara, Dan, Eve, Fay")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("signup_quantile"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(5),
                                        new UShort(11),
                                        new UShort(2022)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("bucket"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("earlier_signup"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(10),
                                        new UShort(7),
                                        new UShort(2019)
                                    )
                                )
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
                                    new String("name_nfc"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Dan\u00e9"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("middle_nfkd"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Dane\u0301"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("whole_age"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("rate"), new DoubleColumnType()),
                                new InvariantCell(new Double(2.34))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("has_domain"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("domain"), new StringColumnType()),
                                new InvariantCell(new String("example.org"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("all_names"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(
                                    new String("Ann, Bob, Cara, Dan, Eve, Fay")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("signup_quantile"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(5),
                                        new UShort(11),
                                        new UShort(2022)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("bucket"), new LongColumnType()),
                                new InvariantCell(new Long(3))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("earlier_signup"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(15),
                                        new UShort(1),
                                        new UShort(2020)
                                    )
                                )
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
                                    new String("name_nfc"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Eve\u00e9"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("middle_nfkd"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Evee\u0301"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("whole_age"), new LongColumnType()),
                                new InvariantCell(new Long(1))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("rate"), new DoubleColumnType()),
                                new InvariantCell(new Double(2.34))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("has_domain"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("domain"), new StringColumnType()),
                                new InvariantCell(new String("mail.net"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("all_names"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(
                                    new String("Ann, Bob, Cara, Dan, Eve, Fay")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("signup_quantile"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(5),
                                        new UShort(11),
                                        new UShort(2022)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("bucket"), new LongColumnType()),
                                new InvariantCell(new Long(4))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("earlier_signup"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(20),
                                        new UShort(3),
                                        new UShort(2021)
                                    )
                                )
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
                                    new String("name_nfc"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Fay\u00e9"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("middle_nfkd"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(new String("Faye\u0301"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("whole_age"), new LongColumnType()),
                                new InvariantCell(new Long(4))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("rate"), new DoubleColumnType()),
                                new InvariantCell(new Double(2.34))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("has_domain"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("domain"), new StringColumnType()),
                                new InvariantCell(new String("example.org"))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("all_names"),
                                    new StringColumnType()
                                ),
                                new InvariantCell(
                                    new String("Ann, Bob, Cara, Dan, Eve, Fay")
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("signup_quantile"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(5),
                                        new UShort(11),
                                        new UShort(2022)
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("bucket"), new LongColumnType()),
                                new InvariantCell(new Long(2))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("earlier_signup"),
                                    new DateColumnType()
                                ),
                                new InvariantCell(
                                    new Date(
                                        new UShort(10),
                                        new UShort(7),
                                        new UShort(2019)
                                    )
                                )
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
