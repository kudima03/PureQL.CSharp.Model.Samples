using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Samples.Columns;
using Pure.RelationalSchema.Samples.Schemas;
using Pure.RelationalSchema.Samples.Tables;
using PureQL.CSharp.Model.Fields;
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.Parameters;
using PureQL.CSharp.Model.ProjectionExpressions;
using PureQL.CSharp.Model.RowExpressions;

namespace PureQL.CSharp.Model.Samples.Queries.Parameters;

/// <summary>
/// Selects order_id as id, order_total times one plus the rate as total_with_tax, the
/// currency and the note from schema_with_foreign_keys.orders inner joined on true to a
/// subquery settings of one inline row (the decimal parameter tax_rate as rate, the date
/// parameter as_of, the nullable string parameter note, and the literal EUR as currency),
/// keeping the orders whose placed_on is on or before as_of.
/// </summary>
public sealed record OrdersWithTaxFromAParameterRowQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithoutPagination(
            subqueries:
            [
                new SubqueryRows(
                    "settings",
                    [
                        [
                            new RowItem("rate", new ParamDecimal("tax_rate")),
                            new RowItem("as_of", new ParamDate("as_of")),
                            new RowItem("note", new ParamStringNullable("note")),
                            new RowItem("currency", new LiteralString("EUR")),
                        ],
                    ]
                ),
            ],
            from: new FromEntity(
                new JoinedString(
                    new DotString(),
                    [new RelationalSchemaWithForeignKeys().Name, new OrdersTable().Name]
                ).TextValue
            ),
            joins:
            [
                new JoinSubquery(JoinKind.Inner, "settings", new LiteralBoolean(true)),
            ],
            where: new LessThanOrEqualDateRow(
                new FieldDate(
                    new JoinedString(
                        new DotString(),
                        [
                            new RelationalSchemaWithForeignKeys().Name,
                            new OrdersTable().Name,
                        ]
                    ).TextValue,
                    new PlacedOnColumn().Name.TextValue
                ),
                new FieldDate("settings", "as_of")
            ),
            select:
            [
                new SelectItemProjection(
                    "id",
                    new FieldUuid(
                        new JoinedString(
                            new DotString(),
                            [
                                new RelationalSchemaWithForeignKeys().Name,
                                new OrdersTable().Name,
                            ]
                        ).TextValue,
                        new OrderIdColumn().Name.TextValue
                    )
                ),
                new SelectItemProjection(
                    "total_with_tax",
                    new MultiplyDecimalProjection([
                        new FieldDecimal(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrdersTable().Name,
                                ]
                            ).TextValue,
                            new OrderTotalColumn().Name.TextValue
                        ),
                        new AddDecimalProjection([
                            new LiteralInteger(1),
                            new FieldDecimal("settings", "rate"),
                        ]),
                    ])
                ),
                new SelectItemProjection(
                    "currency",
                    new FieldString("settings", "currency")
                ),
                new SelectItemProjection(
                    "note",
                    new FieldStringNullable("settings", "note")
                ),
            ]
        );
}
