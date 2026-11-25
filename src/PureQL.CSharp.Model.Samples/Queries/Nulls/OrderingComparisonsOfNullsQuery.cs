using Pure.Collections.Generic;
using Pure.Primitives.Bool;
using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Abstractions.Column;
using Pure.RelationalSchema.ColumnType;
using Pure.RelationalSchema.HashCodes;
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
using Pure.RelationalSchema.Storage;
using Pure.RelationalSchema.Storage.Abstractions;
using Pure.RelationalSchema.Storage.Samples.Cells;
using Pure.RelationalSchema.Storage.Samples.TableDataSets;
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.ProjectionExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Nulls;

/// <summary>
/// Selects, for each of integer, decimal, string, boolean, date, time, datetime and uuid,
/// the four ordering comparisons (at least, at most, greater than, less than) of two typed
/// nulls, and the at-least of a typed null and a value and the at-most of a value and a
/// typed null, from schema_with_foreign_keys.orders. Every column is a non-null boolean.
/// </summary>
public sealed record OrderingComparisonsOfNullsQuery
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
                    "greaterThanOrEqual_integer",
                    new GreaterThanOrEqualDecimalProjection(
                        new LiteralIntegerNullable(),
                        new LiteralIntegerNullable()
                    )
                ),
                new SelectItemProjection(
                    "lessThanOrEqual_integer",
                    new LessThanOrEqualDecimalProjection(
                        new LiteralIntegerNullable(),
                        new LiteralIntegerNullable()
                    )
                ),
                new SelectItemProjection(
                    "greaterThan_integer",
                    new GreaterThanDecimalProjection(
                        new LiteralIntegerNullable(),
                        new LiteralIntegerNullable()
                    )
                ),
                new SelectItemProjection(
                    "lessThan_integer",
                    new LessThanDecimalProjection(
                        new LiteralIntegerNullable(),
                        new LiteralIntegerNullable()
                    )
                ),
                new SelectItemProjection(
                    "greaterThanOrEqual_integer_null_left",
                    new GreaterThanOrEqualDecimalProjection(
                        new LiteralIntegerNullable(),
                        new LiteralInteger(7)
                    )
                ),
                new SelectItemProjection(
                    "lessThanOrEqual_integer_null_right",
                    new LessThanOrEqualDecimalProjection(
                        new LiteralInteger(7),
                        new LiteralIntegerNullable()
                    )
                ),
                new SelectItemProjection(
                    "greaterThanOrEqual_decimal",
                    new GreaterThanOrEqualDecimalProjection(
                        new LiteralDecimalNullable(),
                        new LiteralDecimalNullable()
                    )
                ),
                new SelectItemProjection(
                    "lessThanOrEqual_decimal",
                    new LessThanOrEqualDecimalProjection(
                        new LiteralDecimalNullable(),
                        new LiteralDecimalNullable()
                    )
                ),
                new SelectItemProjection(
                    "greaterThan_decimal",
                    new GreaterThanDecimalProjection(
                        new LiteralDecimalNullable(),
                        new LiteralDecimalNullable()
                    )
                ),
                new SelectItemProjection(
                    "lessThan_decimal",
                    new LessThanDecimalProjection(
                        new LiteralDecimalNullable(),
                        new LiteralDecimalNullable()
                    )
                ),
                new SelectItemProjection(
                    "greaterThanOrEqual_decimal_null_left",
                    new GreaterThanOrEqualDecimalProjection(
                        new LiteralDecimalNullable(),
                        new LiteralDecimal(2.5m)
                    )
                ),
                new SelectItemProjection(
                    "lessThanOrEqual_decimal_null_right",
                    new LessThanOrEqualDecimalProjection(
                        new LiteralDecimal(2.5m),
                        new LiteralDecimalNullable()
                    )
                ),
                new SelectItemProjection(
                    "greaterThanOrEqual_string",
                    new GreaterThanOrEqualStringProjection(
                        new LiteralStringNullable(),
                        new LiteralStringNullable()
                    )
                ),
                new SelectItemProjection(
                    "lessThanOrEqual_string",
                    new LessThanOrEqualStringProjection(
                        new LiteralStringNullable(),
                        new LiteralStringNullable()
                    )
                ),
                new SelectItemProjection(
                    "greaterThan_string",
                    new GreaterThanStringProjection(
                        new LiteralStringNullable(),
                        new LiteralStringNullable()
                    )
                ),
                new SelectItemProjection(
                    "lessThan_string",
                    new LessThanStringProjection(
                        new LiteralStringNullable(),
                        new LiteralStringNullable()
                    )
                ),
                new SelectItemProjection(
                    "greaterThanOrEqual_string_null_left",
                    new GreaterThanOrEqualStringProjection(
                        new LiteralStringNullable(),
                        new LiteralString("a")
                    )
                ),
                new SelectItemProjection(
                    "lessThanOrEqual_string_null_right",
                    new LessThanOrEqualStringProjection(
                        new LiteralString("a"),
                        new LiteralStringNullable()
                    )
                ),
                new SelectItemProjection(
                    "greaterThanOrEqual_boolean",
                    new GreaterThanOrEqualBooleanProjection(
                        new LiteralBooleanNullable(),
                        new LiteralBooleanNullable()
                    )
                ),
                new SelectItemProjection(
                    "lessThanOrEqual_boolean",
                    new LessThanOrEqualBooleanProjection(
                        new LiteralBooleanNullable(),
                        new LiteralBooleanNullable()
                    )
                ),
                new SelectItemProjection(
                    "greaterThan_boolean",
                    new GreaterThanBooleanProjection(
                        new LiteralBooleanNullable(),
                        new LiteralBooleanNullable()
                    )
                ),
                new SelectItemProjection(
                    "lessThan_boolean",
                    new LessThanBooleanProjection(
                        new LiteralBooleanNullable(),
                        new LiteralBooleanNullable()
                    )
                ),
                new SelectItemProjection(
                    "greaterThanOrEqual_boolean_null_left",
                    new GreaterThanOrEqualBooleanProjection(
                        new LiteralBooleanNullable(),
                        new LiteralBoolean(true)
                    )
                ),
                new SelectItemProjection(
                    "lessThanOrEqual_boolean_null_right",
                    new LessThanOrEqualBooleanProjection(
                        new LiteralBoolean(true),
                        new LiteralBooleanNullable()
                    )
                ),
                new SelectItemProjection(
                    "greaterThanOrEqual_date",
                    new GreaterThanOrEqualDateProjection(
                        new LiteralDateNullable(),
                        new LiteralDateNullable()
                    )
                ),
                new SelectItemProjection(
                    "lessThanOrEqual_date",
                    new LessThanOrEqualDateProjection(
                        new LiteralDateNullable(),
                        new LiteralDateNullable()
                    )
                ),
                new SelectItemProjection(
                    "greaterThan_date",
                    new GreaterThanDateProjection(
                        new LiteralDateNullable(),
                        new LiteralDateNullable()
                    )
                ),
                new SelectItemProjection(
                    "lessThan_date",
                    new LessThanDateProjection(
                        new LiteralDateNullable(),
                        new LiteralDateNullable()
                    )
                ),
                new SelectItemProjection(
                    "greaterThanOrEqual_date_null_left",
                    new GreaterThanOrEqualDateProjection(
                        new LiteralDateNullable(),
                        new LiteralDate(new DateOnly(2024, 1, 31))
                    )
                ),
                new SelectItemProjection(
                    "lessThanOrEqual_date_null_right",
                    new LessThanOrEqualDateProjection(
                        new LiteralDate(new DateOnly(2024, 1, 31)),
                        new LiteralDateNullable()
                    )
                ),
                new SelectItemProjection(
                    "greaterThanOrEqual_time",
                    new GreaterThanOrEqualTimeProjection(
                        new LiteralTimeNullable(),
                        new LiteralTimeNullable()
                    )
                ),
                new SelectItemProjection(
                    "lessThanOrEqual_time",
                    new LessThanOrEqualTimeProjection(
                        new LiteralTimeNullable(),
                        new LiteralTimeNullable()
                    )
                ),
                new SelectItemProjection(
                    "greaterThan_time",
                    new GreaterThanTimeProjection(
                        new LiteralTimeNullable(),
                        new LiteralTimeNullable()
                    )
                ),
                new SelectItemProjection(
                    "lessThan_time",
                    new LessThanTimeProjection(
                        new LiteralTimeNullable(),
                        new LiteralTimeNullable()
                    )
                ),
                new SelectItemProjection(
                    "greaterThanOrEqual_time_null_left",
                    new GreaterThanOrEqualTimeProjection(
                        new LiteralTimeNullable(),
                        new LiteralTime(new TimeOnly(8, 30, 0))
                    )
                ),
                new SelectItemProjection(
                    "lessThanOrEqual_time_null_right",
                    new LessThanOrEqualTimeProjection(
                        new LiteralTime(new TimeOnly(8, 30, 0)),
                        new LiteralTimeNullable()
                    )
                ),
                new SelectItemProjection(
                    "greaterThanOrEqual_datetime",
                    new GreaterThanOrEqualDatetimeProjection(
                        new LiteralDatetimeNullable(),
                        new LiteralDatetimeNullable()
                    )
                ),
                new SelectItemProjection(
                    "lessThanOrEqual_datetime",
                    new LessThanOrEqualDatetimeProjection(
                        new LiteralDatetimeNullable(),
                        new LiteralDatetimeNullable()
                    )
                ),
                new SelectItemProjection(
                    "greaterThan_datetime",
                    new GreaterThanDatetimeProjection(
                        new LiteralDatetimeNullable(),
                        new LiteralDatetimeNullable()
                    )
                ),
                new SelectItemProjection(
                    "lessThan_datetime",
                    new LessThanDatetimeProjection(
                        new LiteralDatetimeNullable(),
                        new LiteralDatetimeNullable()
                    )
                ),
                new SelectItemProjection(
                    "greaterThanOrEqual_datetime_null_left",
                    new GreaterThanOrEqualDatetimeProjection(
                        new LiteralDatetimeNullable(),
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        )
                    )
                ),
                new SelectItemProjection(
                    "lessThanOrEqual_datetime_null_right",
                    new LessThanOrEqualDatetimeProjection(
                        new LiteralDatetime(
                            new DateTimeOffset(2024, 1, 31, 8, 30, 0, TimeSpan.Zero)
                        ),
                        new LiteralDatetimeNullable()
                    )
                ),
                new SelectItemProjection(
                    "greaterThanOrEqual_uuid",
                    new GreaterThanOrEqualUuidProjection(
                        new LiteralUuidNullable(),
                        new LiteralUuidNullable()
                    )
                ),
                new SelectItemProjection(
                    "lessThanOrEqual_uuid",
                    new LessThanOrEqualUuidProjection(
                        new LiteralUuidNullable(),
                        new LiteralUuidNullable()
                    )
                ),
                new SelectItemProjection(
                    "greaterThan_uuid",
                    new GreaterThanUuidProjection(
                        new LiteralUuidNullable(),
                        new LiteralUuidNullable()
                    )
                ),
                new SelectItemProjection(
                    "lessThan_uuid",
                    new LessThanUuidProjection(
                        new LiteralUuidNullable(),
                        new LiteralUuidNullable()
                    )
                ),
                new SelectItemProjection(
                    "greaterThanOrEqual_uuid_null_left",
                    new GreaterThanOrEqualUuidProjection(
                        new LiteralUuidNullable(),
                        new LiteralUuid(new Guid(7, 0, 0, new byte[8]))
                    )
                ),
                new SelectItemProjection(
                    "lessThanOrEqual_uuid_null_right",
                    new LessThanOrEqualUuidProjection(
                        new LiteralUuid(new Guid(7, 0, 0, new byte[8])),
                        new LiteralUuidNullable()
                    )
                ),
            ]
        );

    /// <summary>
    /// The table computed over the fixture rows, one identical row per order, in no particular
    /// order. Of two nulls the inclusive comparisons are true and the strict ones false; with
    /// exactly one null every comparison is false.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new Column(
                        new String("greaterThanOrEqual_integer"),
                        new BoolColumnType()
                    ),
                    new Column(
                        new String("lessThanOrEqual_integer"),
                        new BoolColumnType()
                    ),
                    new Column(new String("greaterThan_integer"), new BoolColumnType()),
                    new Column(new String("lessThan_integer"), new BoolColumnType()),
                    new Column(
                        new String("greaterThanOrEqual_integer_null_left"),
                        new BoolColumnType()
                    ),
                    new Column(
                        new String("lessThanOrEqual_integer_null_right"),
                        new BoolColumnType()
                    ),
                    new Column(
                        new String("greaterThanOrEqual_decimal"),
                        new BoolColumnType()
                    ),
                    new Column(
                        new String("lessThanOrEqual_decimal"),
                        new BoolColumnType()
                    ),
                    new Column(new String("greaterThan_decimal"), new BoolColumnType()),
                    new Column(new String("lessThan_decimal"), new BoolColumnType()),
                    new Column(
                        new String("greaterThanOrEqual_decimal_null_left"),
                        new BoolColumnType()
                    ),
                    new Column(
                        new String("lessThanOrEqual_decimal_null_right"),
                        new BoolColumnType()
                    ),
                    new Column(
                        new String("greaterThanOrEqual_string"),
                        new BoolColumnType()
                    ),
                    new Column(
                        new String("lessThanOrEqual_string"),
                        new BoolColumnType()
                    ),
                    new Column(new String("greaterThan_string"), new BoolColumnType()),
                    new Column(new String("lessThan_string"), new BoolColumnType()),
                    new Column(
                        new String("greaterThanOrEqual_string_null_left"),
                        new BoolColumnType()
                    ),
                    new Column(
                        new String("lessThanOrEqual_string_null_right"),
                        new BoolColumnType()
                    ),
                    new Column(
                        new String("greaterThanOrEqual_boolean"),
                        new BoolColumnType()
                    ),
                    new Column(
                        new String("lessThanOrEqual_boolean"),
                        new BoolColumnType()
                    ),
                    new Column(new String("greaterThan_boolean"), new BoolColumnType()),
                    new Column(new String("lessThan_boolean"), new BoolColumnType()),
                    new Column(
                        new String("greaterThanOrEqual_boolean_null_left"),
                        new BoolColumnType()
                    ),
                    new Column(
                        new String("lessThanOrEqual_boolean_null_right"),
                        new BoolColumnType()
                    ),
                    new Column(
                        new String("greaterThanOrEqual_date"),
                        new BoolColumnType()
                    ),
                    new Column(new String("lessThanOrEqual_date"), new BoolColumnType()),
                    new Column(new String("greaterThan_date"), new BoolColumnType()),
                    new Column(new String("lessThan_date"), new BoolColumnType()),
                    new Column(
                        new String("greaterThanOrEqual_date_null_left"),
                        new BoolColumnType()
                    ),
                    new Column(
                        new String("lessThanOrEqual_date_null_right"),
                        new BoolColumnType()
                    ),
                    new Column(
                        new String("greaterThanOrEqual_time"),
                        new BoolColumnType()
                    ),
                    new Column(new String("lessThanOrEqual_time"), new BoolColumnType()),
                    new Column(new String("greaterThan_time"), new BoolColumnType()),
                    new Column(new String("lessThan_time"), new BoolColumnType()),
                    new Column(
                        new String("greaterThanOrEqual_time_null_left"),
                        new BoolColumnType()
                    ),
                    new Column(
                        new String("lessThanOrEqual_time_null_right"),
                        new BoolColumnType()
                    ),
                    new Column(
                        new String("greaterThanOrEqual_datetime"),
                        new BoolColumnType()
                    ),
                    new Column(
                        new String("lessThanOrEqual_datetime"),
                        new BoolColumnType()
                    ),
                    new Column(new String("greaterThan_datetime"), new BoolColumnType()),
                    new Column(new String("lessThan_datetime"), new BoolColumnType()),
                    new Column(
                        new String("greaterThanOrEqual_datetime_null_left"),
                        new BoolColumnType()
                    ),
                    new Column(
                        new String("lessThanOrEqual_datetime_null_right"),
                        new BoolColumnType()
                    ),
                    new Column(
                        new String("greaterThanOrEqual_uuid"),
                        new BoolColumnType()
                    ),
                    new Column(new String("lessThanOrEqual_uuid"), new BoolColumnType()),
                    new Column(new String("greaterThan_uuid"), new BoolColumnType()),
                    new Column(new String("lessThan_uuid"), new BoolColumnType()),
                    new Column(
                        new String("greaterThanOrEqual_uuid_null_left"),
                        new BoolColumnType()
                    ),
                    new Column(
                        new String("lessThanOrEqual_uuid_null_right"),
                        new BoolColumnType()
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
                                    new String("greaterThanOrEqual_integer"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_integer"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThan_integer"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThan_integer"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_integer_null_left"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_integer_null_right"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_decimal"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_decimal"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThan_decimal"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThan_decimal"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_decimal_null_left"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_decimal_null_right"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_string"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_string"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThan_string"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThan_string"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_string_null_left"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_string_null_right"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_boolean"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_boolean"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThan_boolean"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThan_boolean"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_boolean_null_left"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_boolean_null_right"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_date"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_date"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThan_date"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThan_date"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_date_null_left"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_date_null_right"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_time"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_time"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThan_time"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThan_time"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_time_null_left"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_time_null_right"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_datetime"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_datetime"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThan_datetime"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThan_datetime"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_datetime_null_left"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_datetime_null_right"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_uuid"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_uuid"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThan_uuid"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThan_uuid"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_uuid_null_left"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_uuid_null_right"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
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
                                    new String("greaterThanOrEqual_integer"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_integer"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThan_integer"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThan_integer"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_integer_null_left"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_integer_null_right"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_decimal"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_decimal"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThan_decimal"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThan_decimal"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_decimal_null_left"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_decimal_null_right"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_string"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_string"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThan_string"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThan_string"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_string_null_left"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_string_null_right"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_boolean"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_boolean"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThan_boolean"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThan_boolean"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_boolean_null_left"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_boolean_null_right"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_date"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_date"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThan_date"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThan_date"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_date_null_left"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_date_null_right"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_time"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_time"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThan_time"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThan_time"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_time_null_left"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_time_null_right"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_datetime"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_datetime"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThan_datetime"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThan_datetime"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_datetime_null_left"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_datetime_null_right"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_uuid"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_uuid"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThan_uuid"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThan_uuid"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_uuid_null_left"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_uuid_null_right"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
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
                                    new String("greaterThanOrEqual_integer"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_integer"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThan_integer"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThan_integer"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_integer_null_left"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_integer_null_right"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_decimal"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_decimal"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThan_decimal"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThan_decimal"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_decimal_null_left"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_decimal_null_right"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_string"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_string"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThan_string"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThan_string"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_string_null_left"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_string_null_right"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_boolean"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_boolean"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThan_boolean"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThan_boolean"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_boolean_null_left"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_boolean_null_right"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_date"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_date"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThan_date"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThan_date"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_date_null_left"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_date_null_right"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_time"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_time"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThan_time"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThan_time"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_time_null_left"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_time_null_right"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_datetime"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_datetime"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThan_datetime"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThan_datetime"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_datetime_null_left"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_datetime_null_right"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_uuid"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_uuid"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThan_uuid"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThan_uuid"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_uuid_null_left"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_uuid_null_right"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
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
                                    new String("greaterThanOrEqual_integer"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_integer"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThan_integer"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThan_integer"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_integer_null_left"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_integer_null_right"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_decimal"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_decimal"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThan_decimal"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThan_decimal"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_decimal_null_left"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_decimal_null_right"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_string"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_string"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThan_string"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThan_string"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_string_null_left"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_string_null_right"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_boolean"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_boolean"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThan_boolean"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThan_boolean"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_boolean_null_left"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_boolean_null_right"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_date"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_date"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThan_date"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThan_date"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_date_null_left"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_date_null_right"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_time"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_time"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThan_time"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThan_time"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_time_null_left"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_time_null_right"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_datetime"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_datetime"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThan_datetime"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThan_datetime"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_datetime_null_left"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_datetime_null_right"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_uuid"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_uuid"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThan_uuid"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThan_uuid"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_uuid_null_left"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_uuid_null_right"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
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
                                    new String("greaterThanOrEqual_integer"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_integer"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThan_integer"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThan_integer"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_integer_null_left"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_integer_null_right"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_decimal"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_decimal"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThan_decimal"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThan_decimal"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_decimal_null_left"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_decimal_null_right"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_string"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_string"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThan_string"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThan_string"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_string_null_left"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_string_null_right"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_boolean"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_boolean"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThan_boolean"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThan_boolean"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_boolean_null_left"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_boolean_null_right"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_date"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_date"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThan_date"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThan_date"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_date_null_left"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_date_null_right"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_time"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_time"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThan_time"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThan_time"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_time_null_left"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_time_null_right"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_datetime"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_datetime"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThan_datetime"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThan_datetime"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_datetime_null_left"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_datetime_null_right"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_uuid"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_uuid"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThan_uuid"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThan_uuid"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_uuid_null_left"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_uuid_null_right"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
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
                                    new String("greaterThanOrEqual_integer"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_integer"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThan_integer"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThan_integer"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_integer_null_left"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_integer_null_right"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_decimal"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_decimal"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThan_decimal"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThan_decimal"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_decimal_null_left"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_decimal_null_right"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_string"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_string"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThan_string"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThan_string"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_string_null_left"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_string_null_right"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_boolean"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_boolean"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThan_boolean"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThan_boolean"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_boolean_null_left"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_boolean_null_right"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_date"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_date"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThan_date"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThan_date"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_date_null_left"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_date_null_right"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_time"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_time"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThan_time"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThan_time"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_time_null_left"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_time_null_right"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_datetime"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_datetime"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThan_datetime"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThan_datetime"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_datetime_null_left"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_datetime_null_right"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_uuid"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_uuid"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new True())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThan_uuid"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThan_uuid"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("greaterThanOrEqual_uuid_null_left"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("lessThanOrEqual_uuid_null_right"),
                                    new BoolColumnType()
                                ),
                                new InvariantCell(new False())
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
