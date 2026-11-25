using Pure.Collections.Generic;
using Pure.Primitives.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Abstractions.Column;
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
using PureQL.CSharp.Model.RowExpressions;
using Guid = Pure.Primitives.Guid.Guid;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Expressions;

/// <summary>
/// Selects item_id from schema_with_foreign_keys.order_items inner joined with
/// schema_with_foreign_keys.products on item_product_id equal to product_id, where an
/// integer equals the integer 0. The integer is an if nested 30 levels deep: the
/// innermost if returns the floor of item_qty when product_in_stock and the integer 0
/// otherwise, and every other if returns the floor of item_qty when the if below equals
/// the integer 0 and the integer 0 otherwise.
/// </summary>
public sealed record DeepNumericNestingQuery
{
    /// <summary>Builds the query afresh on every read.</summary>
    public PureQLQuery Value =>
        new MainPlainQueryWithoutPagination(
            from: new FromEntity(
                new JoinedString(
                    new DotString(),
                    [
                        new RelationalSchemaWithForeignKeys().Name,
                        new OrderItemsTable().Name,
                    ]
                ).TextValue
            ),
            joins:
            [
                new JoinEntity(
                    JoinKind.Inner,
                    new JoinedString(
                        new DotString(),
                        [
                            new RelationalSchemaWithForeignKeys().Name,
                            new ProductsTable().Name,
                        ]
                    ).TextValue,
                    new EqualUuidRow(
                        new FieldUuid(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrderItemsTable().Name,
                                ]
                            ).TextValue,
                            new ItemProductIdColumn().Name.TextValue
                        ),
                        new FieldUuid(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new ProductsTable().Name,
                                ]
                            ).TextValue,
                            new ProductIdColumn().Name.TextValue
                        )
                    )
                ),
            ],
            where: new EqualDecimalRow(
                new IfIntegerRow(
                    new FloorIntegerRow(
                        new FieldDecimal(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrderItemsTable().Name,
                                ]
                            ).TextValue,
                            new ItemQtyColumn().Name.TextValue
                        )
                    ),
                    new LiteralInteger(0),
                    new EqualDecimalRow(
                        new IfIntegerRow(
                            new FloorIntegerRow(
                                new FieldDecimal(
                                    new JoinedString(
                                        new DotString(),
                                        [
                                            new RelationalSchemaWithForeignKeys().Name,
                                            new OrderItemsTable().Name,
                                        ]
                                    ).TextValue,
                                    new ItemQtyColumn().Name.TextValue
                                )
                            ),
                            new LiteralInteger(0),
                            new EqualDecimalRow(
                                new IfIntegerRow(
                                    new FloorIntegerRow(
                                        new FieldDecimal(
                                            new JoinedString(
                                                new DotString(),
                                                [
                                                    new RelationalSchemaWithForeignKeys().Name,
                                                    new OrderItemsTable().Name,
                                                ]
                                            ).TextValue,
                                            new ItemQtyColumn().Name.TextValue
                                        )
                                    ),
                                    new LiteralInteger(0),
                                    new EqualDecimalRow(
                                        new IfIntegerRow(
                                            new FloorIntegerRow(
                                                new FieldDecimal(
                                                    new JoinedString(
                                                        new DotString(),
                                                        [
                                                            new RelationalSchemaWithForeignKeys().Name,
                                                            new OrderItemsTable().Name,
                                                        ]
                                                    ).TextValue,
                                                    new ItemQtyColumn().Name.TextValue
                                                )
                                            ),
                                            new LiteralInteger(0),
                                            new EqualDecimalRow(
                                                new IfIntegerRow(
                                                    new FloorIntegerRow(
                                                        new FieldDecimal(
                                                            new JoinedString(
                                                                new DotString(),
                                                                [
                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                    new OrderItemsTable().Name,
                                                                ]
                                                            ).TextValue,
                                                            new ItemQtyColumn()
                                                                .Name
                                                                .TextValue
                                                        )
                                                    ),
                                                    new LiteralInteger(0),
                                                    new EqualDecimalRow(
                                                        new IfIntegerRow(
                                                            new FloorIntegerRow(
                                                                new FieldDecimal(
                                                                    new JoinedString(
                                                                        new DotString(),
                                                                        [
                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                            new OrderItemsTable().Name,
                                                                        ]
                                                                    ).TextValue,
                                                                    new ItemQtyColumn()
                                                                        .Name
                                                                        .TextValue
                                                                )
                                                            ),
                                                            new LiteralInteger(0),
                                                            new EqualDecimalRow(
                                                                new IfIntegerRow(
                                                                    new FloorIntegerRow(
                                                                        new FieldDecimal(
                                                                            new JoinedString(
                                                                                new DotString(),
                                                                                [
                                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                                    new OrderItemsTable().Name,
                                                                                ]
                                                                            ).TextValue,
                                                                            new ItemQtyColumn()
                                                                                .Name
                                                                                .TextValue
                                                                        )
                                                                    ),
                                                                    new LiteralInteger(0),
                                                                    new EqualDecimalRow(
                                                                        new IfIntegerRow(
                                                                            new FloorIntegerRow(
                                                                                new FieldDecimal(
                                                                                    new JoinedString(
                                                                                        new DotString(),
                                                                                        [
                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                            new OrderItemsTable().Name,
                                                                                        ]
                                                                                    ).TextValue,
                                                                                    new ItemQtyColumn()
                                                                                        .Name
                                                                                        .TextValue
                                                                                )
                                                                            ),
                                                                            new LiteralInteger(
                                                                                0
                                                                            ),
                                                                            new EqualDecimalRow(
                                                                                new IfIntegerRow(
                                                                                    new FloorIntegerRow(
                                                                                        new FieldDecimal(
                                                                                            new JoinedString(
                                                                                                new DotString(),
                                                                                                [
                                                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                                                    new OrderItemsTable().Name,
                                                                                                ]
                                                                                            ).TextValue,
                                                                                            new ItemQtyColumn()
                                                                                                .Name
                                                                                                .TextValue
                                                                                        )
                                                                                    ),
                                                                                    new LiteralInteger(
                                                                                        0
                                                                                    ),
                                                                                    new EqualDecimalRow(
                                                                                        new IfIntegerRow(
                                                                                            new FloorIntegerRow(
                                                                                                new FieldDecimal(
                                                                                                    new JoinedString(
                                                                                                        new DotString(),
                                                                                                        [
                                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                                            new OrderItemsTable().Name,
                                                                                                        ]
                                                                                                    ).TextValue,
                                                                                                    new ItemQtyColumn()
                                                                                                        .Name
                                                                                                        .TextValue
                                                                                                )
                                                                                            ),
                                                                                            new LiteralInteger(
                                                                                                0
                                                                                            ),
                                                                                            new EqualDecimalRow(
                                                                                                new IfIntegerRow(
                                                                                                    new FloorIntegerRow(
                                                                                                        new FieldDecimal(
                                                                                                            new JoinedString(
                                                                                                                new DotString(),
                                                                                                                [
                                                                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                                                                    new OrderItemsTable().Name,
                                                                                                                ]
                                                                                                            ).TextValue,
                                                                                                            new ItemQtyColumn()
                                                                                                                .Name
                                                                                                                .TextValue
                                                                                                        )
                                                                                                    ),
                                                                                                    new LiteralInteger(
                                                                                                        0
                                                                                                    ),
                                                                                                    new EqualDecimalRow(
                                                                                                        new IfIntegerRow(
                                                                                                            new FloorIntegerRow(
                                                                                                                new FieldDecimal(
                                                                                                                    new JoinedString(
                                                                                                                        new DotString(),
                                                                                                                        [
                                                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                                                            new OrderItemsTable().Name,
                                                                                                                        ]
                                                                                                                    ).TextValue,
                                                                                                                    new ItemQtyColumn()
                                                                                                                        .Name
                                                                                                                        .TextValue
                                                                                                                )
                                                                                                            ),
                                                                                                            new LiteralInteger(
                                                                                                                0
                                                                                                            ),
                                                                                                            new EqualDecimalRow(
                                                                                                                new IfIntegerRow(
                                                                                                                    new FloorIntegerRow(
                                                                                                                        new FieldDecimal(
                                                                                                                            new JoinedString(
                                                                                                                                new DotString(),
                                                                                                                                [
                                                                                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                    new OrderItemsTable().Name,
                                                                                                                                ]
                                                                                                                            ).TextValue,
                                                                                                                            new ItemQtyColumn()
                                                                                                                                .Name
                                                                                                                                .TextValue
                                                                                                                        )
                                                                                                                    ),
                                                                                                                    new LiteralInteger(
                                                                                                                        0
                                                                                                                    ),
                                                                                                                    new EqualDecimalRow(
                                                                                                                        new IfIntegerRow(
                                                                                                                            new FloorIntegerRow(
                                                                                                                                new FieldDecimal(
                                                                                                                                    new JoinedString(
                                                                                                                                        new DotString(),
                                                                                                                                        [
                                                                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                            new OrderItemsTable().Name,
                                                                                                                                        ]
                                                                                                                                    ).TextValue,
                                                                                                                                    new ItemQtyColumn()
                                                                                                                                        .Name
                                                                                                                                        .TextValue
                                                                                                                                )
                                                                                                                            ),
                                                                                                                            new LiteralInteger(
                                                                                                                                0
                                                                                                                            ),
                                                                                                                            new EqualDecimalRow(
                                                                                                                                new IfIntegerRow(
                                                                                                                                    new FloorIntegerRow(
                                                                                                                                        new FieldDecimal(
                                                                                                                                            new JoinedString(
                                                                                                                                                new DotString(),
                                                                                                                                                [
                                                                                                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                    new OrderItemsTable().Name,
                                                                                                                                                ]
                                                                                                                                            ).TextValue,
                                                                                                                                            new ItemQtyColumn()
                                                                                                                                                .Name
                                                                                                                                                .TextValue
                                                                                                                                        )
                                                                                                                                    ),
                                                                                                                                    new LiteralInteger(
                                                                                                                                        0
                                                                                                                                    ),
                                                                                                                                    new EqualDecimalRow(
                                                                                                                                        new IfIntegerRow(
                                                                                                                                            new FloorIntegerRow(
                                                                                                                                                new FieldDecimal(
                                                                                                                                                    new JoinedString(
                                                                                                                                                        new DotString(),
                                                                                                                                                        [
                                                                                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                            new OrderItemsTable().Name,
                                                                                                                                                        ]
                                                                                                                                                    ).TextValue,
                                                                                                                                                    new ItemQtyColumn()
                                                                                                                                                        .Name
                                                                                                                                                        .TextValue
                                                                                                                                                )
                                                                                                                                            ),
                                                                                                                                            new LiteralInteger(
                                                                                                                                                0
                                                                                                                                            ),
                                                                                                                                            new EqualDecimalRow(
                                                                                                                                                new IfIntegerRow(
                                                                                                                                                    new FloorIntegerRow(
                                                                                                                                                        new FieldDecimal(
                                                                                                                                                            new JoinedString(
                                                                                                                                                                new DotString(),
                                                                                                                                                                [
                                                                                                                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                    new OrderItemsTable().Name,
                                                                                                                                                                ]
                                                                                                                                                            ).TextValue,
                                                                                                                                                            new ItemQtyColumn()
                                                                                                                                                                .Name
                                                                                                                                                                .TextValue
                                                                                                                                                        )
                                                                                                                                                    ),
                                                                                                                                                    new LiteralInteger(
                                                                                                                                                        0
                                                                                                                                                    ),
                                                                                                                                                    new EqualDecimalRow(
                                                                                                                                                        new IfIntegerRow(
                                                                                                                                                            new FloorIntegerRow(
                                                                                                                                                                new FieldDecimal(
                                                                                                                                                                    new JoinedString(
                                                                                                                                                                        new DotString(),
                                                                                                                                                                        [
                                                                                                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                            new OrderItemsTable().Name,
                                                                                                                                                                        ]
                                                                                                                                                                    ).TextValue,
                                                                                                                                                                    new ItemQtyColumn()
                                                                                                                                                                        .Name
                                                                                                                                                                        .TextValue
                                                                                                                                                                )
                                                                                                                                                            ),
                                                                                                                                                            new LiteralInteger(
                                                                                                                                                                0
                                                                                                                                                            ),
                                                                                                                                                            new EqualDecimalRow(
                                                                                                                                                                new IfIntegerRow(
                                                                                                                                                                    new FloorIntegerRow(
                                                                                                                                                                        new FieldDecimal(
                                                                                                                                                                            new JoinedString(
                                                                                                                                                                                new DotString(),
                                                                                                                                                                                [
                                                                                                                                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                    new OrderItemsTable().Name,
                                                                                                                                                                                ]
                                                                                                                                                                            ).TextValue,
                                                                                                                                                                            new ItemQtyColumn()
                                                                                                                                                                                .Name
                                                                                                                                                                                .TextValue
                                                                                                                                                                        )
                                                                                                                                                                    ),
                                                                                                                                                                    new LiteralInteger(
                                                                                                                                                                        0
                                                                                                                                                                    ),
                                                                                                                                                                    new EqualDecimalRow(
                                                                                                                                                                        new IfIntegerRow(
                                                                                                                                                                            new FloorIntegerRow(
                                                                                                                                                                                new FieldDecimal(
                                                                                                                                                                                    new JoinedString(
                                                                                                                                                                                        new DotString(),
                                                                                                                                                                                        [
                                                                                                                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                            new OrderItemsTable().Name,
                                                                                                                                                                                        ]
                                                                                                                                                                                    ).TextValue,
                                                                                                                                                                                    new ItemQtyColumn()
                                                                                                                                                                                        .Name
                                                                                                                                                                                        .TextValue
                                                                                                                                                                                )
                                                                                                                                                                            ),
                                                                                                                                                                            new LiteralInteger(
                                                                                                                                                                                0
                                                                                                                                                                            ),
                                                                                                                                                                            new EqualDecimalRow(
                                                                                                                                                                                new IfIntegerRow(
                                                                                                                                                                                    new FloorIntegerRow(
                                                                                                                                                                                        new FieldDecimal(
                                                                                                                                                                                            new JoinedString(
                                                                                                                                                                                                new DotString(),
                                                                                                                                                                                                [
                                                                                                                                                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                    new OrderItemsTable().Name,
                                                                                                                                                                                                ]
                                                                                                                                                                                            ).TextValue,
                                                                                                                                                                                            new ItemQtyColumn()
                                                                                                                                                                                                .Name
                                                                                                                                                                                                .TextValue
                                                                                                                                                                                        )
                                                                                                                                                                                    ),
                                                                                                                                                                                    new LiteralInteger(
                                                                                                                                                                                        0
                                                                                                                                                                                    ),
                                                                                                                                                                                    new EqualDecimalRow(
                                                                                                                                                                                        new IfIntegerRow(
                                                                                                                                                                                            new FloorIntegerRow(
                                                                                                                                                                                                new FieldDecimal(
                                                                                                                                                                                                    new JoinedString(
                                                                                                                                                                                                        new DotString(),
                                                                                                                                                                                                        [
                                                                                                                                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                            new OrderItemsTable().Name,
                                                                                                                                                                                                        ]
                                                                                                                                                                                                    ).TextValue,
                                                                                                                                                                                                    new ItemQtyColumn()
                                                                                                                                                                                                        .Name
                                                                                                                                                                                                        .TextValue
                                                                                                                                                                                                )
                                                                                                                                                                                            ),
                                                                                                                                                                                            new LiteralInteger(
                                                                                                                                                                                                0
                                                                                                                                                                                            ),
                                                                                                                                                                                            new EqualDecimalRow(
                                                                                                                                                                                                new IfIntegerRow(
                                                                                                                                                                                                    new FloorIntegerRow(
                                                                                                                                                                                                        new FieldDecimal(
                                                                                                                                                                                                            new JoinedString(
                                                                                                                                                                                                                new DotString(),
                                                                                                                                                                                                                [
                                                                                                                                                                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                    new OrderItemsTable().Name,
                                                                                                                                                                                                                ]
                                                                                                                                                                                                            ).TextValue,
                                                                                                                                                                                                            new ItemQtyColumn()
                                                                                                                                                                                                                .Name
                                                                                                                                                                                                                .TextValue
                                                                                                                                                                                                        )
                                                                                                                                                                                                    ),
                                                                                                                                                                                                    new LiteralInteger(
                                                                                                                                                                                                        0
                                                                                                                                                                                                    ),
                                                                                                                                                                                                    new EqualDecimalRow(
                                                                                                                                                                                                        new IfIntegerRow(
                                                                                                                                                                                                            new FloorIntegerRow(
                                                                                                                                                                                                                new FieldDecimal(
                                                                                                                                                                                                                    new JoinedString(
                                                                                                                                                                                                                        new DotString(),
                                                                                                                                                                                                                        [
                                                                                                                                                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                            new OrderItemsTable().Name,
                                                                                                                                                                                                                        ]
                                                                                                                                                                                                                    ).TextValue,
                                                                                                                                                                                                                    new ItemQtyColumn()
                                                                                                                                                                                                                        .Name
                                                                                                                                                                                                                        .TextValue
                                                                                                                                                                                                                )
                                                                                                                                                                                                            ),
                                                                                                                                                                                                            new LiteralInteger(
                                                                                                                                                                                                                0
                                                                                                                                                                                                            ),
                                                                                                                                                                                                            new EqualDecimalRow(
                                                                                                                                                                                                                new IfIntegerRow(
                                                                                                                                                                                                                    new FloorIntegerRow(
                                                                                                                                                                                                                        new FieldDecimal(
                                                                                                                                                                                                                            new JoinedString(
                                                                                                                                                                                                                                new DotString(),
                                                                                                                                                                                                                                [
                                                                                                                                                                                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                                    new OrderItemsTable().Name,
                                                                                                                                                                                                                                ]
                                                                                                                                                                                                                            ).TextValue,
                                                                                                                                                                                                                            new ItemQtyColumn()
                                                                                                                                                                                                                                .Name
                                                                                                                                                                                                                                .TextValue
                                                                                                                                                                                                                        )
                                                                                                                                                                                                                    ),
                                                                                                                                                                                                                    new LiteralInteger(
                                                                                                                                                                                                                        0
                                                                                                                                                                                                                    ),
                                                                                                                                                                                                                    new EqualDecimalRow(
                                                                                                                                                                                                                        new IfIntegerRow(
                                                                                                                                                                                                                            new FloorIntegerRow(
                                                                                                                                                                                                                                new FieldDecimal(
                                                                                                                                                                                                                                    new JoinedString(
                                                                                                                                                                                                                                        new DotString(),
                                                                                                                                                                                                                                        [
                                                                                                                                                                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                                            new OrderItemsTable().Name,
                                                                                                                                                                                                                                        ]
                                                                                                                                                                                                                                    ).TextValue,
                                                                                                                                                                                                                                    new ItemQtyColumn()
                                                                                                                                                                                                                                        .Name
                                                                                                                                                                                                                                        .TextValue
                                                                                                                                                                                                                                )
                                                                                                                                                                                                                            ),
                                                                                                                                                                                                                            new LiteralInteger(
                                                                                                                                                                                                                                0
                                                                                                                                                                                                                            ),
                                                                                                                                                                                                                            new EqualDecimalRow(
                                                                                                                                                                                                                                new IfIntegerRow(
                                                                                                                                                                                                                                    new FloorIntegerRow(
                                                                                                                                                                                                                                        new FieldDecimal(
                                                                                                                                                                                                                                            new JoinedString(
                                                                                                                                                                                                                                                new DotString(),
                                                                                                                                                                                                                                                [
                                                                                                                                                                                                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                                                    new OrderItemsTable().Name,
                                                                                                                                                                                                                                                ]
                                                                                                                                                                                                                                            ).TextValue,
                                                                                                                                                                                                                                            new ItemQtyColumn()
                                                                                                                                                                                                                                                .Name
                                                                                                                                                                                                                                                .TextValue
                                                                                                                                                                                                                                        )
                                                                                                                                                                                                                                    ),
                                                                                                                                                                                                                                    new LiteralInteger(
                                                                                                                                                                                                                                        0
                                                                                                                                                                                                                                    ),
                                                                                                                                                                                                                                    new EqualDecimalRow(
                                                                                                                                                                                                                                        new IfIntegerRow(
                                                                                                                                                                                                                                            new FloorIntegerRow(
                                                                                                                                                                                                                                                new FieldDecimal(
                                                                                                                                                                                                                                                    new JoinedString(
                                                                                                                                                                                                                                                        new DotString(),
                                                                                                                                                                                                                                                        [
                                                                                                                                                                                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                                                            new OrderItemsTable().Name,
                                                                                                                                                                                                                                                        ]
                                                                                                                                                                                                                                                    ).TextValue,
                                                                                                                                                                                                                                                    new ItemQtyColumn()
                                                                                                                                                                                                                                                        .Name
                                                                                                                                                                                                                                                        .TextValue
                                                                                                                                                                                                                                                )
                                                                                                                                                                                                                                            ),
                                                                                                                                                                                                                                            new LiteralInteger(
                                                                                                                                                                                                                                                0
                                                                                                                                                                                                                                            ),
                                                                                                                                                                                                                                            new EqualDecimalRow(
                                                                                                                                                                                                                                                new IfIntegerRow(
                                                                                                                                                                                                                                                    new FloorIntegerRow(
                                                                                                                                                                                                                                                        new FieldDecimal(
                                                                                                                                                                                                                                                            new JoinedString(
                                                                                                                                                                                                                                                                new DotString(),
                                                                                                                                                                                                                                                                [
                                                                                                                                                                                                                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                                                                    new OrderItemsTable().Name,
                                                                                                                                                                                                                                                                ]
                                                                                                                                                                                                                                                            ).TextValue,
                                                                                                                                                                                                                                                            new ItemQtyColumn()
                                                                                                                                                                                                                                                                .Name
                                                                                                                                                                                                                                                                .TextValue
                                                                                                                                                                                                                                                        )
                                                                                                                                                                                                                                                    ),
                                                                                                                                                                                                                                                    new LiteralInteger(
                                                                                                                                                                                                                                                        0
                                                                                                                                                                                                                                                    ),
                                                                                                                                                                                                                                                    new EqualDecimalRow(
                                                                                                                                                                                                                                                        new IfIntegerRow(
                                                                                                                                                                                                                                                            new FloorIntegerRow(
                                                                                                                                                                                                                                                                new FieldDecimal(
                                                                                                                                                                                                                                                                    new JoinedString(
                                                                                                                                                                                                                                                                        new DotString(),
                                                                                                                                                                                                                                                                        [
                                                                                                                                                                                                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                                                                            new OrderItemsTable().Name,
                                                                                                                                                                                                                                                                        ]
                                                                                                                                                                                                                                                                    ).TextValue,
                                                                                                                                                                                                                                                                    new ItemQtyColumn()
                                                                                                                                                                                                                                                                        .Name
                                                                                                                                                                                                                                                                        .TextValue
                                                                                                                                                                                                                                                                )
                                                                                                                                                                                                                                                            ),
                                                                                                                                                                                                                                                            new LiteralInteger(
                                                                                                                                                                                                                                                                0
                                                                                                                                                                                                                                                            ),
                                                                                                                                                                                                                                                            new FieldBoolean(
                                                                                                                                                                                                                                                                new JoinedString(
                                                                                                                                                                                                                                                                    new DotString(),
                                                                                                                                                                                                                                                                    [
                                                                                                                                                                                                                                                                        new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                                                                        new ProductsTable().Name,
                                                                                                                                                                                                                                                                    ]
                                                                                                                                                                                                                                                                ).TextValue,
                                                                                                                                                                                                                                                                new ProductInStockColumn()
                                                                                                                                                                                                                                                                    .Name
                                                                                                                                                                                                                                                                    .TextValue
                                                                                                                                                                                                                                                            )
                                                                                                                                                                                                                                                        ),
                                                                                                                                                                                                                                                        new LiteralInteger(
                                                                                                                                                                                                                                                            0
                                                                                                                                                                                                                                                        )
                                                                                                                                                                                                                                                    )
                                                                                                                                                                                                                                                ),
                                                                                                                                                                                                                                                new LiteralInteger(
                                                                                                                                                                                                                                                    0
                                                                                                                                                                                                                                                )
                                                                                                                                                                                                                                            )
                                                                                                                                                                                                                                        ),
                                                                                                                                                                                                                                        new LiteralInteger(
                                                                                                                                                                                                                                            0
                                                                                                                                                                                                                                        )
                                                                                                                                                                                                                                    )
                                                                                                                                                                                                                                ),
                                                                                                                                                                                                                                new LiteralInteger(
                                                                                                                                                                                                                                    0
                                                                                                                                                                                                                                )
                                                                                                                                                                                                                            )
                                                                                                                                                                                                                        ),
                                                                                                                                                                                                                        new LiteralInteger(
                                                                                                                                                                                                                            0
                                                                                                                                                                                                                        )
                                                                                                                                                                                                                    )
                                                                                                                                                                                                                ),
                                                                                                                                                                                                                new LiteralInteger(
                                                                                                                                                                                                                    0
                                                                                                                                                                                                                )
                                                                                                                                                                                                            )
                                                                                                                                                                                                        ),
                                                                                                                                                                                                        new LiteralInteger(
                                                                                                                                                                                                            0
                                                                                                                                                                                                        )
                                                                                                                                                                                                    )
                                                                                                                                                                                                ),
                                                                                                                                                                                                new LiteralInteger(
                                                                                                                                                                                                    0
                                                                                                                                                                                                )
                                                                                                                                                                                            )
                                                                                                                                                                                        ),
                                                                                                                                                                                        new LiteralInteger(
                                                                                                                                                                                            0
                                                                                                                                                                                        )
                                                                                                                                                                                    )
                                                                                                                                                                                ),
                                                                                                                                                                                new LiteralInteger(
                                                                                                                                                                                    0
                                                                                                                                                                                )
                                                                                                                                                                            )
                                                                                                                                                                        ),
                                                                                                                                                                        new LiteralInteger(
                                                                                                                                                                            0
                                                                                                                                                                        )
                                                                                                                                                                    )
                                                                                                                                                                ),
                                                                                                                                                                new LiteralInteger(
                                                                                                                                                                    0
                                                                                                                                                                )
                                                                                                                                                            )
                                                                                                                                                        ),
                                                                                                                                                        new LiteralInteger(
                                                                                                                                                            0
                                                                                                                                                        )
                                                                                                                                                    )
                                                                                                                                                ),
                                                                                                                                                new LiteralInteger(
                                                                                                                                                    0
                                                                                                                                                )
                                                                                                                                            )
                                                                                                                                        ),
                                                                                                                                        new LiteralInteger(
                                                                                                                                            0
                                                                                                                                        )
                                                                                                                                    )
                                                                                                                                ),
                                                                                                                                new LiteralInteger(
                                                                                                                                    0
                                                                                                                                )
                                                                                                                            )
                                                                                                                        ),
                                                                                                                        new LiteralInteger(
                                                                                                                            0
                                                                                                                        )
                                                                                                                    )
                                                                                                                ),
                                                                                                                new LiteralInteger(
                                                                                                                    0
                                                                                                                )
                                                                                                            )
                                                                                                        ),
                                                                                                        new LiteralInteger(
                                                                                                            0
                                                                                                        )
                                                                                                    )
                                                                                                ),
                                                                                                new LiteralInteger(
                                                                                                    0
                                                                                                )
                                                                                            )
                                                                                        ),
                                                                                        new LiteralInteger(
                                                                                            0
                                                                                        )
                                                                                    )
                                                                                ),
                                                                                new LiteralInteger(
                                                                                    0
                                                                                )
                                                                            )
                                                                        ),
                                                                        new LiteralInteger(
                                                                            0
                                                                        )
                                                                    )
                                                                ),
                                                                new LiteralInteger(0)
                                                            )
                                                        ),
                                                        new LiteralInteger(0)
                                                    )
                                                ),
                                                new LiteralInteger(0)
                                            )
                                        ),
                                        new LiteralInteger(0)
                                    )
                                ),
                                new LiteralInteger(0)
                            )
                        ),
                        new LiteralInteger(0)
                    )
                ),
                new LiteralInteger(0)
            ),
            select:
            [
                new SelectItemProjection(
                    "item_id",
                    new FieldUuid(
                        new JoinedString(
                            new DotString(),
                            [
                                new RelationalSchemaWithForeignKeys().Name,
                                new OrderItemsTable().Name,
                            ]
                        ).TextValue,
                        new ItemIdColumn().Name.TextValue
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
            new Table(new EmptyString(), [new ItemIdColumn()], []),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new ItemIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "0000012d-0000-0000-0000-000000000000"
                                        )
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
                                new ItemIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "0000012f-0000-0000-0000-000000000000"
                                        )
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
                                new ItemIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000130-0000-0000-0000-000000000000"
                                        )
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
