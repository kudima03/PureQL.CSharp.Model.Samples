using Pure.Collections.Generic;
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
using PureQL.CSharp.Model.SelectItems;
using Column = Pure.RelationalSchema.Column.Column;
using Double = Pure.Primitives.Number.Double;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Nulls;

/// <summary>
/// Selects user_score or else user_age as score_or_age, and user_score or else a typed
/// null decimal as score_or_null, from schema_with_foreign_keys.users.
/// </summary>
public sealed record CoalesceQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new PureQLQuery(
            new MainPlainQuery(
                new From(
                    new FromEntity(
                        new JoinedString(
                            new DotString(),
                            [
                                new RelationalSchemaWithForeignKeys().Name,
                                new UsersTable().Name,
                            ]
                        ).TextValue
                    )
                ),
                [
                    new SelectItemProjection(
                        new SelectItemProjectionNonNullable(
                            new SelectItemProjectionDecimal(
                                "score_or_age",
                                new DecimalProjection(
                                    new ConditionalDecimalProjection(
                                        new CoalesceDecimalProjection([
                                            new DecimalNullableProjection(
                                                new FieldAsDecimalNullable(
                                                    new FieldDecimalNullable(
                                                        new JoinedString(
                                                            new DotString(),
                                                            [
                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                new UsersTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new UserScoreColumn()
                                                            .Name
                                                            .TextValue
                                                    )
                                                )
                                            ),
                                            new DecimalNullableProjection(
                                                new FieldAsDecimalNullable(
                                                    new FieldDecimal(
                                                        new JoinedString(
                                                            new DotString(),
                                                            [
                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                new UsersTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new UserAgeColumn().Name.TextValue
                                                    )
                                                )
                                            ),
                                        ])
                                    )
                                )
                            )
                        )
                    ),
                    new SelectItemProjection(
                        new SelectItemProjectionNullable(
                            new SelectItemProjectionDecimalNullable(
                                "score_or_null",
                                new DecimalNullableProjection(
                                    new ConditionalDecimalNullableProjection(
                                        new CoalesceDecimalNullableProjection([
                                            new DecimalNullableProjection(
                                                new FieldAsDecimalNullable(
                                                    new FieldDecimalNullable(
                                                        new JoinedString(
                                                            new DotString(),
                                                            [
                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                new UsersTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new UserScoreColumn()
                                                            .Name
                                                            .TextValue
                                                    )
                                                )
                                            ),
                                            new DecimalNullableProjection(
                                                new LiteralAsDecimalNullable(
                                                    new LiteralDecimalNullable()
                                                )
                                            ),
                                        ])
                                    )
                                )
                            )
                        )
                    ),
                ]
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
                    new Column(new String("score_or_age"), new DoubleColumnType()),
                    new Column(new String("score_or_null"), new DoubleColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("score_or_age"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(30))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("score_or_null"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(30))
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
                                    new String("score_or_age"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(25))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("score_or_null"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
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
                                    new String("score_or_age"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(30))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("score_or_null"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(30))
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
                                    new String("score_or_age"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(42))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("score_or_null"),
                                    new DoubleColumnType()
                                ),
                                new EmptyCell()
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
                                    new String("score_or_age"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(10))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("score_or_null"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(10))
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
                                    new String("score_or_age"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(28))
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(
                                    new String("score_or_null"),
                                    new DoubleColumnType()
                                ),
                                new InvariantCell(new Double(28))
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
