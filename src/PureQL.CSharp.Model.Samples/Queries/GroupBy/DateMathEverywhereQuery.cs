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
using PureQL.CSharp.Model.GroupExpressions;
using PureQL.CSharp.Model.GroupKeys;
using PureQL.CSharp.Model.Keys;
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.RowExpressions;
using PureQL.CSharp.Model.SelectItems;
using Column = Pure.RelationalSchema.Column.Column;
using Guid = Pure.Primitives.Guid.Guid;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.GroupBy;

/// <summary>
/// Selects order_user_id and the days between the min and max placed_on as
/// active_span_days from schema_with_foreign_keys.orders, where placed_on is at most 4
/// days after 2024-06-01, grouped by order_user_id, having a max of placed_on at or after
/// 2024-06-30 minus 27 days.
/// </summary>
public sealed record DateMathEverywhereQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new PureQLQuery(
            new MainGroupedQuery(
                new From(
                    new FromEntity(
                        new JoinedString(
                            new DotString(),
                            [
                                new RelationalSchemaWithForeignKeys().Name,
                                new OrdersTable().Name,
                            ]
                        ).TextValue
                    )
                ),
                [
                    new GroupKey(
                        new GroupKeyNonNullable(
                            new GroupKeyUuid(
                                new UuidRow(
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
                                )
                            )
                        )
                    ),
                ],
                [
                    new SelectItemGroup(
                        new SelectItemGroupNonNullable(
                            new SelectItemGroupUuid(
                                "order_user_id",
                                new UuidGroup(new KeyUuid(0))
                            )
                        )
                    ),
                    new SelectItemGroup(
                        new SelectItemGroupNonNullable(
                            new SelectItemGroupInteger(
                                "active_span_days",
                                new IntegerGroup(
                                    new DateDiffDaysIntegerGroup(
                                        new DateGroup(
                                            new AggregateDateGroup(
                                                new MaxDateGroup(
                                                    new DateRow(
                                                        new FieldDate(
                                                            new JoinedString(
                                                                new DotString(),
                                                                [
                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                    new OrdersTable().Name,
                                                                ]
                                                            ).TextValue,
                                                            new PlacedOnColumn()
                                                                .Name
                                                                .TextValue
                                                        )
                                                    )
                                                )
                                            )
                                        ),
                                        new DateGroup(
                                            new AggregateDateGroup(
                                                new MinDateGroup(
                                                    new DateRow(
                                                        new FieldDate(
                                                            new JoinedString(
                                                                new DotString(),
                                                                [
                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                    new OrdersTable().Name,
                                                                ]
                                                            ).TextValue,
                                                            new PlacedOnColumn()
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
                        )
                    ),
                ],
                subqueries: null,
                joins: null,
                new BooleanRow(
                    new ComparisonRow(
                        new LessThanOrEqualRow(
                            new LessThanOrEqualDecimalRow(
                                new DecimalNullableRow(
                                    new DifferenceDecimalNullableRow(
                                        new DateDiffDaysIntegerNullableRow(
                                            new DateNullableRow(
                                                new FieldAsDateNullable(
                                                    new FieldDate(
                                                        new JoinedString(
                                                            new DotString(),
                                                            [
                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                new OrdersTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new PlacedOnColumn()
                                                            .Name
                                                            .TextValue
                                                    )
                                                )
                                            ),
                                            new DateNullableRow(
                                                new LiteralAsDateNullable(
                                                    new LiteralDate(
                                                        new DateOnly(2024, 6, 1)
                                                    )
                                                )
                                            )
                                        )
                                    )
                                ),
                                new DecimalNullableRow(
                                    new LiteralAsDecimalNullable(new LiteralInteger(4))
                                )
                            )
                        )
                    )
                ),
                new BooleanGroup(
                    new ComparisonGroup(
                        new GreaterThanOrEqualGroup(
                            new GreaterThanOrEqualDateGroup(
                                new DateNullableGroup(
                                    new AggregateDateNullableGroup(
                                        new MaxDateNullableGroup(
                                            new DateNullableRow(
                                                new FieldAsDateNullable(
                                                    new FieldDate(
                                                        new JoinedString(
                                                            new DotString(),
                                                            [
                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                new OrdersTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new PlacedOnColumn()
                                                            .Name
                                                            .TextValue
                                                    )
                                                )
                                            )
                                        )
                                    )
                                ),
                                new DateNullableGroup(
                                    new DateAddDaysDateNullableGroup(
                                        new DateNullableGroup(
                                            new LiteralAsDateNullable(
                                                new LiteralDate(new DateOnly(2024, 6, 30))
                                            )
                                        ),
                                        new IntegerNullableGroup(
                                            new LiteralAsIntegerNullable(
                                                new LiteralInteger(-27)
                                            )
                                        )
                                    )
                                )
                            )
                        )
                    )
                ),
                orderBy: null,
                pagination: null,
                distinct: false
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
                    new OrderUserIdColumn(),
                    new Column(new String("active_span_days"), new LongColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new OrderUserIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000002-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("active_span_days"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(0))
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
                                new OrderUserIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000003-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("active_span_days"),
                                    new LongColumnType()
                                ),
                                new InvariantCell(new Long(1))
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
