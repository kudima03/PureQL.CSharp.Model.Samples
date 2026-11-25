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
using PureQL.CSharp.Model.RowExpressions;
using Guid = Pure.Primitives.Guid.Guid;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Expressions;

/// <summary>
/// Selects order_id as id from schema_with_foreign_keys.orders inner joined to
/// schema_with_foreign_keys.users on order_user_id equal to user_id, where order_id
/// equals an if nested 30 levels deep: each level is an if of order_id, or order_user_id
/// otherwise, whose condition is the equal of the level below with order_id, and the
/// innermost condition is user_active.
/// </summary>
public sealed record DeeplyNestedIfAndEqualQuery
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
            joins:
            [
                new JoinEntity(
                    JoinKind.Inner,
                    new JoinedString(
                        new DotString(),
                        [
                            new RelationalSchemaWithForeignKeys().Name,
                            new UsersTable().Name,
                        ]
                    ).TextValue,
                    new EqualUuidRow(
                        new FieldUuid(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrdersTable().Name,
                                ]
                            ).TextValue,
                            new OrderUserIdColumn().Name.TextValue
                        ),
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
                    )
                ),
            ],
            where: new EqualUuidRow(
                new IfUuidRow(
                    new FieldUuid(
                        new JoinedString(
                            new DotString(),
                            [
                                new RelationalSchemaWithForeignKeys().Name,
                                new OrdersTable().Name,
                            ]
                        ).TextValue,
                        new OrderIdColumn().Name.TextValue
                    ),
                    new FieldUuid(
                        new JoinedString(
                            new DotString(),
                            [
                                new RelationalSchemaWithForeignKeys().Name,
                                new OrdersTable().Name,
                            ]
                        ).TextValue,
                        new OrderUserIdColumn().Name.TextValue
                    ),
                    new EqualUuidRow(
                        new IfUuidRow(
                            new FieldUuid(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new OrdersTable().Name,
                                    ]
                                ).TextValue,
                                new OrderIdColumn().Name.TextValue
                            ),
                            new FieldUuid(
                                new JoinedString(
                                    new DotString(),
                                    [
                                        new RelationalSchemaWithForeignKeys().Name,
                                        new OrdersTable().Name,
                                    ]
                                ).TextValue,
                                new OrderUserIdColumn().Name.TextValue
                            ),
                            new EqualUuidRow(
                                new IfUuidRow(
                                    new FieldUuid(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new OrdersTable().Name,
                                            ]
                                        ).TextValue,
                                        new OrderIdColumn().Name.TextValue
                                    ),
                                    new FieldUuid(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new OrdersTable().Name,
                                            ]
                                        ).TextValue,
                                        new OrderUserIdColumn().Name.TextValue
                                    ),
                                    new EqualUuidRow(
                                        new IfUuidRow(
                                            new FieldUuid(
                                                new JoinedString(
                                                    new DotString(),
                                                    [
                                                        new RelationalSchemaWithForeignKeys().Name,
                                                        new OrdersTable().Name,
                                                    ]
                                                ).TextValue,
                                                new OrderIdColumn().Name.TextValue
                                            ),
                                            new FieldUuid(
                                                new JoinedString(
                                                    new DotString(),
                                                    [
                                                        new RelationalSchemaWithForeignKeys().Name,
                                                        new OrdersTable().Name,
                                                    ]
                                                ).TextValue,
                                                new OrderUserIdColumn().Name.TextValue
                                            ),
                                            new EqualUuidRow(
                                                new IfUuidRow(
                                                    new FieldUuid(
                                                        new JoinedString(
                                                            new DotString(),
                                                            [
                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                new OrdersTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new OrderIdColumn().Name.TextValue
                                                    ),
                                                    new FieldUuid(
                                                        new JoinedString(
                                                            new DotString(),
                                                            [
                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                new OrdersTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new OrderUserIdColumn()
                                                            .Name
                                                            .TextValue
                                                    ),
                                                    new EqualUuidRow(
                                                        new IfUuidRow(
                                                            new FieldUuid(
                                                                new JoinedString(
                                                                    new DotString(),
                                                                    [
                                                                        new RelationalSchemaWithForeignKeys().Name,
                                                                        new OrdersTable().Name,
                                                                    ]
                                                                ).TextValue,
                                                                new OrderIdColumn()
                                                                    .Name
                                                                    .TextValue
                                                            ),
                                                            new FieldUuid(
                                                                new JoinedString(
                                                                    new DotString(),
                                                                    [
                                                                        new RelationalSchemaWithForeignKeys().Name,
                                                                        new OrdersTable().Name,
                                                                    ]
                                                                ).TextValue,
                                                                new OrderUserIdColumn()
                                                                    .Name
                                                                    .TextValue
                                                            ),
                                                            new EqualUuidRow(
                                                                new IfUuidRow(
                                                                    new FieldUuid(
                                                                        new JoinedString(
                                                                            new DotString(),
                                                                            [
                                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                                new OrdersTable().Name,
                                                                            ]
                                                                        ).TextValue,
                                                                        new OrderIdColumn()
                                                                            .Name
                                                                            .TextValue
                                                                    ),
                                                                    new FieldUuid(
                                                                        new JoinedString(
                                                                            new DotString(),
                                                                            [
                                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                                new OrdersTable().Name,
                                                                            ]
                                                                        ).TextValue,
                                                                        new OrderUserIdColumn()
                                                                            .Name
                                                                            .TextValue
                                                                    ),
                                                                    new EqualUuidRow(
                                                                        new IfUuidRow(
                                                                            new FieldUuid(
                                                                                new JoinedString(
                                                                                    new DotString(),
                                                                                    [
                                                                                        new RelationalSchemaWithForeignKeys().Name,
                                                                                        new OrdersTable().Name,
                                                                                    ]
                                                                                ).TextValue,
                                                                                new OrderIdColumn()
                                                                                    .Name
                                                                                    .TextValue
                                                                            ),
                                                                            new FieldUuid(
                                                                                new JoinedString(
                                                                                    new DotString(),
                                                                                    [
                                                                                        new RelationalSchemaWithForeignKeys().Name,
                                                                                        new OrdersTable().Name,
                                                                                    ]
                                                                                ).TextValue,
                                                                                new OrderUserIdColumn()
                                                                                    .Name
                                                                                    .TextValue
                                                                            ),
                                                                            new EqualUuidRow(
                                                                                new IfUuidRow(
                                                                                    new FieldUuid(
                                                                                        new JoinedString(
                                                                                            new DotString(),
                                                                                            [
                                                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                                                new OrdersTable().Name,
                                                                                            ]
                                                                                        ).TextValue,
                                                                                        new OrderIdColumn()
                                                                                            .Name
                                                                                            .TextValue
                                                                                    ),
                                                                                    new FieldUuid(
                                                                                        new JoinedString(
                                                                                            new DotString(),
                                                                                            [
                                                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                                                new OrdersTable().Name,
                                                                                            ]
                                                                                        ).TextValue,
                                                                                        new OrderUserIdColumn()
                                                                                            .Name
                                                                                            .TextValue
                                                                                    ),
                                                                                    new EqualUuidRow(
                                                                                        new IfUuidRow(
                                                                                            new FieldUuid(
                                                                                                new JoinedString(
                                                                                                    new DotString(),
                                                                                                    [
                                                                                                        new RelationalSchemaWithForeignKeys().Name,
                                                                                                        new OrdersTable().Name,
                                                                                                    ]
                                                                                                ).TextValue,
                                                                                                new OrderIdColumn()
                                                                                                    .Name
                                                                                                    .TextValue
                                                                                            ),
                                                                                            new FieldUuid(
                                                                                                new JoinedString(
                                                                                                    new DotString(),
                                                                                                    [
                                                                                                        new RelationalSchemaWithForeignKeys().Name,
                                                                                                        new OrdersTable().Name,
                                                                                                    ]
                                                                                                ).TextValue,
                                                                                                new OrderUserIdColumn()
                                                                                                    .Name
                                                                                                    .TextValue
                                                                                            ),
                                                                                            new EqualUuidRow(
                                                                                                new IfUuidRow(
                                                                                                    new FieldUuid(
                                                                                                        new JoinedString(
                                                                                                            new DotString(),
                                                                                                            [
                                                                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                                                                new OrdersTable().Name,
                                                                                                            ]
                                                                                                        ).TextValue,
                                                                                                        new OrderIdColumn()
                                                                                                            .Name
                                                                                                            .TextValue
                                                                                                    ),
                                                                                                    new FieldUuid(
                                                                                                        new JoinedString(
                                                                                                            new DotString(),
                                                                                                            [
                                                                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                                                                new OrdersTable().Name,
                                                                                                            ]
                                                                                                        ).TextValue,
                                                                                                        new OrderUserIdColumn()
                                                                                                            .Name
                                                                                                            .TextValue
                                                                                                    ),
                                                                                                    new EqualUuidRow(
                                                                                                        new IfUuidRow(
                                                                                                            new FieldUuid(
                                                                                                                new JoinedString(
                                                                                                                    new DotString(),
                                                                                                                    [
                                                                                                                        new RelationalSchemaWithForeignKeys().Name,
                                                                                                                        new OrdersTable().Name,
                                                                                                                    ]
                                                                                                                ).TextValue,
                                                                                                                new OrderIdColumn()
                                                                                                                    .Name
                                                                                                                    .TextValue
                                                                                                            ),
                                                                                                            new FieldUuid(
                                                                                                                new JoinedString(
                                                                                                                    new DotString(),
                                                                                                                    [
                                                                                                                        new RelationalSchemaWithForeignKeys().Name,
                                                                                                                        new OrdersTable().Name,
                                                                                                                    ]
                                                                                                                ).TextValue,
                                                                                                                new OrderUserIdColumn()
                                                                                                                    .Name
                                                                                                                    .TextValue
                                                                                                            ),
                                                                                                            new EqualUuidRow(
                                                                                                                new IfUuidRow(
                                                                                                                    new FieldUuid(
                                                                                                                        new JoinedString(
                                                                                                                            new DotString(),
                                                                                                                            [
                                                                                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                new OrdersTable().Name,
                                                                                                                            ]
                                                                                                                        ).TextValue,
                                                                                                                        new OrderIdColumn()
                                                                                                                            .Name
                                                                                                                            .TextValue
                                                                                                                    ),
                                                                                                                    new FieldUuid(
                                                                                                                        new JoinedString(
                                                                                                                            new DotString(),
                                                                                                                            [
                                                                                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                new OrdersTable().Name,
                                                                                                                            ]
                                                                                                                        ).TextValue,
                                                                                                                        new OrderUserIdColumn()
                                                                                                                            .Name
                                                                                                                            .TextValue
                                                                                                                    ),
                                                                                                                    new EqualUuidRow(
                                                                                                                        new IfUuidRow(
                                                                                                                            new FieldUuid(
                                                                                                                                new JoinedString(
                                                                                                                                    new DotString(),
                                                                                                                                    [
                                                                                                                                        new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                        new OrdersTable().Name,
                                                                                                                                    ]
                                                                                                                                ).TextValue,
                                                                                                                                new OrderIdColumn()
                                                                                                                                    .Name
                                                                                                                                    .TextValue
                                                                                                                            ),
                                                                                                                            new FieldUuid(
                                                                                                                                new JoinedString(
                                                                                                                                    new DotString(),
                                                                                                                                    [
                                                                                                                                        new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                        new OrdersTable().Name,
                                                                                                                                    ]
                                                                                                                                ).TextValue,
                                                                                                                                new OrderUserIdColumn()
                                                                                                                                    .Name
                                                                                                                                    .TextValue
                                                                                                                            ),
                                                                                                                            new EqualUuidRow(
                                                                                                                                new IfUuidRow(
                                                                                                                                    new FieldUuid(
                                                                                                                                        new JoinedString(
                                                                                                                                            new DotString(),
                                                                                                                                            [
                                                                                                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                new OrdersTable().Name,
                                                                                                                                            ]
                                                                                                                                        ).TextValue,
                                                                                                                                        new OrderIdColumn()
                                                                                                                                            .Name
                                                                                                                                            .TextValue
                                                                                                                                    ),
                                                                                                                                    new FieldUuid(
                                                                                                                                        new JoinedString(
                                                                                                                                            new DotString(),
                                                                                                                                            [
                                                                                                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                new OrdersTable().Name,
                                                                                                                                            ]
                                                                                                                                        ).TextValue,
                                                                                                                                        new OrderUserIdColumn()
                                                                                                                                            .Name
                                                                                                                                            .TextValue
                                                                                                                                    ),
                                                                                                                                    new EqualUuidRow(
                                                                                                                                        new IfUuidRow(
                                                                                                                                            new FieldUuid(
                                                                                                                                                new JoinedString(
                                                                                                                                                    new DotString(),
                                                                                                                                                    [
                                                                                                                                                        new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                        new OrdersTable().Name,
                                                                                                                                                    ]
                                                                                                                                                ).TextValue,
                                                                                                                                                new OrderIdColumn()
                                                                                                                                                    .Name
                                                                                                                                                    .TextValue
                                                                                                                                            ),
                                                                                                                                            new FieldUuid(
                                                                                                                                                new JoinedString(
                                                                                                                                                    new DotString(),
                                                                                                                                                    [
                                                                                                                                                        new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                        new OrdersTable().Name,
                                                                                                                                                    ]
                                                                                                                                                ).TextValue,
                                                                                                                                                new OrderUserIdColumn()
                                                                                                                                                    .Name
                                                                                                                                                    .TextValue
                                                                                                                                            ),
                                                                                                                                            new EqualUuidRow(
                                                                                                                                                new IfUuidRow(
                                                                                                                                                    new FieldUuid(
                                                                                                                                                        new JoinedString(
                                                                                                                                                            new DotString(),
                                                                                                                                                            [
                                                                                                                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                new OrdersTable().Name,
                                                                                                                                                            ]
                                                                                                                                                        ).TextValue,
                                                                                                                                                        new OrderIdColumn()
                                                                                                                                                            .Name
                                                                                                                                                            .TextValue
                                                                                                                                                    ),
                                                                                                                                                    new FieldUuid(
                                                                                                                                                        new JoinedString(
                                                                                                                                                            new DotString(),
                                                                                                                                                            [
                                                                                                                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                new OrdersTable().Name,
                                                                                                                                                            ]
                                                                                                                                                        ).TextValue,
                                                                                                                                                        new OrderUserIdColumn()
                                                                                                                                                            .Name
                                                                                                                                                            .TextValue
                                                                                                                                                    ),
                                                                                                                                                    new EqualUuidRow(
                                                                                                                                                        new IfUuidRow(
                                                                                                                                                            new FieldUuid(
                                                                                                                                                                new JoinedString(
                                                                                                                                                                    new DotString(),
                                                                                                                                                                    [
                                                                                                                                                                        new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                        new OrdersTable().Name,
                                                                                                                                                                    ]
                                                                                                                                                                ).TextValue,
                                                                                                                                                                new OrderIdColumn()
                                                                                                                                                                    .Name
                                                                                                                                                                    .TextValue
                                                                                                                                                            ),
                                                                                                                                                            new FieldUuid(
                                                                                                                                                                new JoinedString(
                                                                                                                                                                    new DotString(),
                                                                                                                                                                    [
                                                                                                                                                                        new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                        new OrdersTable().Name,
                                                                                                                                                                    ]
                                                                                                                                                                ).TextValue,
                                                                                                                                                                new OrderUserIdColumn()
                                                                                                                                                                    .Name
                                                                                                                                                                    .TextValue
                                                                                                                                                            ),
                                                                                                                                                            new EqualUuidRow(
                                                                                                                                                                new IfUuidRow(
                                                                                                                                                                    new FieldUuid(
                                                                                                                                                                        new JoinedString(
                                                                                                                                                                            new DotString(),
                                                                                                                                                                            [
                                                                                                                                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                new OrdersTable().Name,
                                                                                                                                                                            ]
                                                                                                                                                                        ).TextValue,
                                                                                                                                                                        new OrderIdColumn()
                                                                                                                                                                            .Name
                                                                                                                                                                            .TextValue
                                                                                                                                                                    ),
                                                                                                                                                                    new FieldUuid(
                                                                                                                                                                        new JoinedString(
                                                                                                                                                                            new DotString(),
                                                                                                                                                                            [
                                                                                                                                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                new OrdersTable().Name,
                                                                                                                                                                            ]
                                                                                                                                                                        ).TextValue,
                                                                                                                                                                        new OrderUserIdColumn()
                                                                                                                                                                            .Name
                                                                                                                                                                            .TextValue
                                                                                                                                                                    ),
                                                                                                                                                                    new EqualUuidRow(
                                                                                                                                                                        new IfUuidRow(
                                                                                                                                                                            new FieldUuid(
                                                                                                                                                                                new JoinedString(
                                                                                                                                                                                    new DotString(),
                                                                                                                                                                                    [
                                                                                                                                                                                        new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                        new OrdersTable().Name,
                                                                                                                                                                                    ]
                                                                                                                                                                                ).TextValue,
                                                                                                                                                                                new OrderIdColumn()
                                                                                                                                                                                    .Name
                                                                                                                                                                                    .TextValue
                                                                                                                                                                            ),
                                                                                                                                                                            new FieldUuid(
                                                                                                                                                                                new JoinedString(
                                                                                                                                                                                    new DotString(),
                                                                                                                                                                                    [
                                                                                                                                                                                        new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                        new OrdersTable().Name,
                                                                                                                                                                                    ]
                                                                                                                                                                                ).TextValue,
                                                                                                                                                                                new OrderUserIdColumn()
                                                                                                                                                                                    .Name
                                                                                                                                                                                    .TextValue
                                                                                                                                                                            ),
                                                                                                                                                                            new EqualUuidRow(
                                                                                                                                                                                new IfUuidRow(
                                                                                                                                                                                    new FieldUuid(
                                                                                                                                                                                        new JoinedString(
                                                                                                                                                                                            new DotString(),
                                                                                                                                                                                            [
                                                                                                                                                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                new OrdersTable().Name,
                                                                                                                                                                                            ]
                                                                                                                                                                                        ).TextValue,
                                                                                                                                                                                        new OrderIdColumn()
                                                                                                                                                                                            .Name
                                                                                                                                                                                            .TextValue
                                                                                                                                                                                    ),
                                                                                                                                                                                    new FieldUuid(
                                                                                                                                                                                        new JoinedString(
                                                                                                                                                                                            new DotString(),
                                                                                                                                                                                            [
                                                                                                                                                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                new OrdersTable().Name,
                                                                                                                                                                                            ]
                                                                                                                                                                                        ).TextValue,
                                                                                                                                                                                        new OrderUserIdColumn()
                                                                                                                                                                                            .Name
                                                                                                                                                                                            .TextValue
                                                                                                                                                                                    ),
                                                                                                                                                                                    new EqualUuidRow(
                                                                                                                                                                                        new IfUuidRow(
                                                                                                                                                                                            new FieldUuid(
                                                                                                                                                                                                new JoinedString(
                                                                                                                                                                                                    new DotString(),
                                                                                                                                                                                                    [
                                                                                                                                                                                                        new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                        new OrdersTable().Name,
                                                                                                                                                                                                    ]
                                                                                                                                                                                                ).TextValue,
                                                                                                                                                                                                new OrderIdColumn()
                                                                                                                                                                                                    .Name
                                                                                                                                                                                                    .TextValue
                                                                                                                                                                                            ),
                                                                                                                                                                                            new FieldUuid(
                                                                                                                                                                                                new JoinedString(
                                                                                                                                                                                                    new DotString(),
                                                                                                                                                                                                    [
                                                                                                                                                                                                        new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                        new OrdersTable().Name,
                                                                                                                                                                                                    ]
                                                                                                                                                                                                ).TextValue,
                                                                                                                                                                                                new OrderUserIdColumn()
                                                                                                                                                                                                    .Name
                                                                                                                                                                                                    .TextValue
                                                                                                                                                                                            ),
                                                                                                                                                                                            new EqualUuidRow(
                                                                                                                                                                                                new IfUuidRow(
                                                                                                                                                                                                    new FieldUuid(
                                                                                                                                                                                                        new JoinedString(
                                                                                                                                                                                                            new DotString(),
                                                                                                                                                                                                            [
                                                                                                                                                                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                new OrdersTable().Name,
                                                                                                                                                                                                            ]
                                                                                                                                                                                                        ).TextValue,
                                                                                                                                                                                                        new OrderIdColumn()
                                                                                                                                                                                                            .Name
                                                                                                                                                                                                            .TextValue
                                                                                                                                                                                                    ),
                                                                                                                                                                                                    new FieldUuid(
                                                                                                                                                                                                        new JoinedString(
                                                                                                                                                                                                            new DotString(),
                                                                                                                                                                                                            [
                                                                                                                                                                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                new OrdersTable().Name,
                                                                                                                                                                                                            ]
                                                                                                                                                                                                        ).TextValue,
                                                                                                                                                                                                        new OrderUserIdColumn()
                                                                                                                                                                                                            .Name
                                                                                                                                                                                                            .TextValue
                                                                                                                                                                                                    ),
                                                                                                                                                                                                    new EqualUuidRow(
                                                                                                                                                                                                        new IfUuidRow(
                                                                                                                                                                                                            new FieldUuid(
                                                                                                                                                                                                                new JoinedString(
                                                                                                                                                                                                                    new DotString(),
                                                                                                                                                                                                                    [
                                                                                                                                                                                                                        new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                        new OrdersTable().Name,
                                                                                                                                                                                                                    ]
                                                                                                                                                                                                                ).TextValue,
                                                                                                                                                                                                                new OrderIdColumn()
                                                                                                                                                                                                                    .Name
                                                                                                                                                                                                                    .TextValue
                                                                                                                                                                                                            ),
                                                                                                                                                                                                            new FieldUuid(
                                                                                                                                                                                                                new JoinedString(
                                                                                                                                                                                                                    new DotString(),
                                                                                                                                                                                                                    [
                                                                                                                                                                                                                        new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                        new OrdersTable().Name,
                                                                                                                                                                                                                    ]
                                                                                                                                                                                                                ).TextValue,
                                                                                                                                                                                                                new OrderUserIdColumn()
                                                                                                                                                                                                                    .Name
                                                                                                                                                                                                                    .TextValue
                                                                                                                                                                                                            ),
                                                                                                                                                                                                            new EqualUuidRow(
                                                                                                                                                                                                                new IfUuidRow(
                                                                                                                                                                                                                    new FieldUuid(
                                                                                                                                                                                                                        new JoinedString(
                                                                                                                                                                                                                            new DotString(),
                                                                                                                                                                                                                            [
                                                                                                                                                                                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                                new OrdersTable().Name,
                                                                                                                                                                                                                            ]
                                                                                                                                                                                                                        ).TextValue,
                                                                                                                                                                                                                        new OrderIdColumn()
                                                                                                                                                                                                                            .Name
                                                                                                                                                                                                                            .TextValue
                                                                                                                                                                                                                    ),
                                                                                                                                                                                                                    new FieldUuid(
                                                                                                                                                                                                                        new JoinedString(
                                                                                                                                                                                                                            new DotString(),
                                                                                                                                                                                                                            [
                                                                                                                                                                                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                                new OrdersTable().Name,
                                                                                                                                                                                                                            ]
                                                                                                                                                                                                                        ).TextValue,
                                                                                                                                                                                                                        new OrderUserIdColumn()
                                                                                                                                                                                                                            .Name
                                                                                                                                                                                                                            .TextValue
                                                                                                                                                                                                                    ),
                                                                                                                                                                                                                    new EqualUuidRow(
                                                                                                                                                                                                                        new IfUuidRow(
                                                                                                                                                                                                                            new FieldUuid(
                                                                                                                                                                                                                                new JoinedString(
                                                                                                                                                                                                                                    new DotString(),
                                                                                                                                                                                                                                    [
                                                                                                                                                                                                                                        new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                                        new OrdersTable().Name,
                                                                                                                                                                                                                                    ]
                                                                                                                                                                                                                                ).TextValue,
                                                                                                                                                                                                                                new OrderIdColumn()
                                                                                                                                                                                                                                    .Name
                                                                                                                                                                                                                                    .TextValue
                                                                                                                                                                                                                            ),
                                                                                                                                                                                                                            new FieldUuid(
                                                                                                                                                                                                                                new JoinedString(
                                                                                                                                                                                                                                    new DotString(),
                                                                                                                                                                                                                                    [
                                                                                                                                                                                                                                        new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                                        new OrdersTable().Name,
                                                                                                                                                                                                                                    ]
                                                                                                                                                                                                                                ).TextValue,
                                                                                                                                                                                                                                new OrderUserIdColumn()
                                                                                                                                                                                                                                    .Name
                                                                                                                                                                                                                                    .TextValue
                                                                                                                                                                                                                            ),
                                                                                                                                                                                                                            new EqualUuidRow(
                                                                                                                                                                                                                                new IfUuidRow(
                                                                                                                                                                                                                                    new FieldUuid(
                                                                                                                                                                                                                                        new JoinedString(
                                                                                                                                                                                                                                            new DotString(),
                                                                                                                                                                                                                                            [
                                                                                                                                                                                                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                                                new OrdersTable().Name,
                                                                                                                                                                                                                                            ]
                                                                                                                                                                                                                                        ).TextValue,
                                                                                                                                                                                                                                        new OrderIdColumn()
                                                                                                                                                                                                                                            .Name
                                                                                                                                                                                                                                            .TextValue
                                                                                                                                                                                                                                    ),
                                                                                                                                                                                                                                    new FieldUuid(
                                                                                                                                                                                                                                        new JoinedString(
                                                                                                                                                                                                                                            new DotString(),
                                                                                                                                                                                                                                            [
                                                                                                                                                                                                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                                                new OrdersTable().Name,
                                                                                                                                                                                                                                            ]
                                                                                                                                                                                                                                        ).TextValue,
                                                                                                                                                                                                                                        new OrderUserIdColumn()
                                                                                                                                                                                                                                            .Name
                                                                                                                                                                                                                                            .TextValue
                                                                                                                                                                                                                                    ),
                                                                                                                                                                                                                                    new EqualUuidRow(
                                                                                                                                                                                                                                        new IfUuidRow(
                                                                                                                                                                                                                                            new FieldUuid(
                                                                                                                                                                                                                                                new JoinedString(
                                                                                                                                                                                                                                                    new DotString(),
                                                                                                                                                                                                                                                    [
                                                                                                                                                                                                                                                        new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                                                        new OrdersTable().Name,
                                                                                                                                                                                                                                                    ]
                                                                                                                                                                                                                                                ).TextValue,
                                                                                                                                                                                                                                                new OrderIdColumn()
                                                                                                                                                                                                                                                    .Name
                                                                                                                                                                                                                                                    .TextValue
                                                                                                                                                                                                                                            ),
                                                                                                                                                                                                                                            new FieldUuid(
                                                                                                                                                                                                                                                new JoinedString(
                                                                                                                                                                                                                                                    new DotString(),
                                                                                                                                                                                                                                                    [
                                                                                                                                                                                                                                                        new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                                                        new OrdersTable().Name,
                                                                                                                                                                                                                                                    ]
                                                                                                                                                                                                                                                ).TextValue,
                                                                                                                                                                                                                                                new OrderUserIdColumn()
                                                                                                                                                                                                                                                    .Name
                                                                                                                                                                                                                                                    .TextValue
                                                                                                                                                                                                                                            ),
                                                                                                                                                                                                                                            new EqualUuidRow(
                                                                                                                                                                                                                                                new IfUuidRow(
                                                                                                                                                                                                                                                    new FieldUuid(
                                                                                                                                                                                                                                                        new JoinedString(
                                                                                                                                                                                                                                                            new DotString(),
                                                                                                                                                                                                                                                            [
                                                                                                                                                                                                                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                                                                new OrdersTable().Name,
                                                                                                                                                                                                                                                            ]
                                                                                                                                                                                                                                                        ).TextValue,
                                                                                                                                                                                                                                                        new OrderIdColumn()
                                                                                                                                                                                                                                                            .Name
                                                                                                                                                                                                                                                            .TextValue
                                                                                                                                                                                                                                                    ),
                                                                                                                                                                                                                                                    new FieldUuid(
                                                                                                                                                                                                                                                        new JoinedString(
                                                                                                                                                                                                                                                            new DotString(),
                                                                                                                                                                                                                                                            [
                                                                                                                                                                                                                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                                                                new OrdersTable().Name,
                                                                                                                                                                                                                                                            ]
                                                                                                                                                                                                                                                        ).TextValue,
                                                                                                                                                                                                                                                        new OrderUserIdColumn()
                                                                                                                                                                                                                                                            .Name
                                                                                                                                                                                                                                                            .TextValue
                                                                                                                                                                                                                                                    ),
                                                                                                                                                                                                                                                    new EqualUuidRow(
                                                                                                                                                                                                                                                        new IfUuidRow(
                                                                                                                                                                                                                                                            new FieldUuid(
                                                                                                                                                                                                                                                                new JoinedString(
                                                                                                                                                                                                                                                                    new DotString(),
                                                                                                                                                                                                                                                                    [
                                                                                                                                                                                                                                                                        new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                                                                        new OrdersTable().Name,
                                                                                                                                                                                                                                                                    ]
                                                                                                                                                                                                                                                                ).TextValue,
                                                                                                                                                                                                                                                                new OrderIdColumn()
                                                                                                                                                                                                                                                                    .Name
                                                                                                                                                                                                                                                                    .TextValue
                                                                                                                                                                                                                                                            ),
                                                                                                                                                                                                                                                            new FieldUuid(
                                                                                                                                                                                                                                                                new JoinedString(
                                                                                                                                                                                                                                                                    new DotString(),
                                                                                                                                                                                                                                                                    [
                                                                                                                                                                                                                                                                        new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                                                                        new OrdersTable().Name,
                                                                                                                                                                                                                                                                    ]
                                                                                                                                                                                                                                                                ).TextValue,
                                                                                                                                                                                                                                                                new OrderUserIdColumn()
                                                                                                                                                                                                                                                                    .Name
                                                                                                                                                                                                                                                                    .TextValue
                                                                                                                                                                                                                                                            ),
                                                                                                                                                                                                                                                            new FieldBoolean(
                                                                                                                                                                                                                                                                new JoinedString(
                                                                                                                                                                                                                                                                    new DotString(),
                                                                                                                                                                                                                                                                    [
                                                                                                                                                                                                                                                                        new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                                                                        new UsersTable().Name,
                                                                                                                                                                                                                                                                    ]
                                                                                                                                                                                                                                                                ).TextValue,
                                                                                                                                                                                                                                                                new UserActiveColumn()
                                                                                                                                                                                                                                                                    .Name
                                                                                                                                                                                                                                                                    .TextValue
                                                                                                                                                                                                                                                            )
                                                                                                                                                                                                                                                        ),
                                                                                                                                                                                                                                                        new FieldUuid(
                                                                                                                                                                                                                                                            new JoinedString(
                                                                                                                                                                                                                                                                new DotString(),
                                                                                                                                                                                                                                                                [
                                                                                                                                                                                                                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                                                                    new OrdersTable().Name,
                                                                                                                                                                                                                                                                ]
                                                                                                                                                                                                                                                            ).TextValue,
                                                                                                                                                                                                                                                            new OrderIdColumn()
                                                                                                                                                                                                                                                                .Name
                                                                                                                                                                                                                                                                .TextValue
                                                                                                                                                                                                                                                        )
                                                                                                                                                                                                                                                    )
                                                                                                                                                                                                                                                ),
                                                                                                                                                                                                                                                new FieldUuid(
                                                                                                                                                                                                                                                    new JoinedString(
                                                                                                                                                                                                                                                        new DotString(),
                                                                                                                                                                                                                                                        [
                                                                                                                                                                                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                                                            new OrdersTable().Name,
                                                                                                                                                                                                                                                        ]
                                                                                                                                                                                                                                                    ).TextValue,
                                                                                                                                                                                                                                                    new OrderIdColumn()
                                                                                                                                                                                                                                                        .Name
                                                                                                                                                                                                                                                        .TextValue
                                                                                                                                                                                                                                                )
                                                                                                                                                                                                                                            )
                                                                                                                                                                                                                                        ),
                                                                                                                                                                                                                                        new FieldUuid(
                                                                                                                                                                                                                                            new JoinedString(
                                                                                                                                                                                                                                                new DotString(),
                                                                                                                                                                                                                                                [
                                                                                                                                                                                                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                                                    new OrdersTable().Name,
                                                                                                                                                                                                                                                ]
                                                                                                                                                                                                                                            ).TextValue,
                                                                                                                                                                                                                                            new OrderIdColumn()
                                                                                                                                                                                                                                                .Name
                                                                                                                                                                                                                                                .TextValue
                                                                                                                                                                                                                                        )
                                                                                                                                                                                                                                    )
                                                                                                                                                                                                                                ),
                                                                                                                                                                                                                                new FieldUuid(
                                                                                                                                                                                                                                    new JoinedString(
                                                                                                                                                                                                                                        new DotString(),
                                                                                                                                                                                                                                        [
                                                                                                                                                                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                                            new OrdersTable().Name,
                                                                                                                                                                                                                                        ]
                                                                                                                                                                                                                                    ).TextValue,
                                                                                                                                                                                                                                    new OrderIdColumn()
                                                                                                                                                                                                                                        .Name
                                                                                                                                                                                                                                        .TextValue
                                                                                                                                                                                                                                )
                                                                                                                                                                                                                            )
                                                                                                                                                                                                                        ),
                                                                                                                                                                                                                        new FieldUuid(
                                                                                                                                                                                                                            new JoinedString(
                                                                                                                                                                                                                                new DotString(),
                                                                                                                                                                                                                                [
                                                                                                                                                                                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                                    new OrdersTable().Name,
                                                                                                                                                                                                                                ]
                                                                                                                                                                                                                            ).TextValue,
                                                                                                                                                                                                                            new OrderIdColumn()
                                                                                                                                                                                                                                .Name
                                                                                                                                                                                                                                .TextValue
                                                                                                                                                                                                                        )
                                                                                                                                                                                                                    )
                                                                                                                                                                                                                ),
                                                                                                                                                                                                                new FieldUuid(
                                                                                                                                                                                                                    new JoinedString(
                                                                                                                                                                                                                        new DotString(),
                                                                                                                                                                                                                        [
                                                                                                                                                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                            new OrdersTable().Name,
                                                                                                                                                                                                                        ]
                                                                                                                                                                                                                    ).TextValue,
                                                                                                                                                                                                                    new OrderIdColumn()
                                                                                                                                                                                                                        .Name
                                                                                                                                                                                                                        .TextValue
                                                                                                                                                                                                                )
                                                                                                                                                                                                            )
                                                                                                                                                                                                        ),
                                                                                                                                                                                                        new FieldUuid(
                                                                                                                                                                                                            new JoinedString(
                                                                                                                                                                                                                new DotString(),
                                                                                                                                                                                                                [
                                                                                                                                                                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                    new OrdersTable().Name,
                                                                                                                                                                                                                ]
                                                                                                                                                                                                            ).TextValue,
                                                                                                                                                                                                            new OrderIdColumn()
                                                                                                                                                                                                                .Name
                                                                                                                                                                                                                .TextValue
                                                                                                                                                                                                        )
                                                                                                                                                                                                    )
                                                                                                                                                                                                ),
                                                                                                                                                                                                new FieldUuid(
                                                                                                                                                                                                    new JoinedString(
                                                                                                                                                                                                        new DotString(),
                                                                                                                                                                                                        [
                                                                                                                                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                            new OrdersTable().Name,
                                                                                                                                                                                                        ]
                                                                                                                                                                                                    ).TextValue,
                                                                                                                                                                                                    new OrderIdColumn()
                                                                                                                                                                                                        .Name
                                                                                                                                                                                                        .TextValue
                                                                                                                                                                                                )
                                                                                                                                                                                            )
                                                                                                                                                                                        ),
                                                                                                                                                                                        new FieldUuid(
                                                                                                                                                                                            new JoinedString(
                                                                                                                                                                                                new DotString(),
                                                                                                                                                                                                [
                                                                                                                                                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                    new OrdersTable().Name,
                                                                                                                                                                                                ]
                                                                                                                                                                                            ).TextValue,
                                                                                                                                                                                            new OrderIdColumn()
                                                                                                                                                                                                .Name
                                                                                                                                                                                                .TextValue
                                                                                                                                                                                        )
                                                                                                                                                                                    )
                                                                                                                                                                                ),
                                                                                                                                                                                new FieldUuid(
                                                                                                                                                                                    new JoinedString(
                                                                                                                                                                                        new DotString(),
                                                                                                                                                                                        [
                                                                                                                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                            new OrdersTable().Name,
                                                                                                                                                                                        ]
                                                                                                                                                                                    ).TextValue,
                                                                                                                                                                                    new OrderIdColumn()
                                                                                                                                                                                        .Name
                                                                                                                                                                                        .TextValue
                                                                                                                                                                                )
                                                                                                                                                                            )
                                                                                                                                                                        ),
                                                                                                                                                                        new FieldUuid(
                                                                                                                                                                            new JoinedString(
                                                                                                                                                                                new DotString(),
                                                                                                                                                                                [
                                                                                                                                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                    new OrdersTable().Name,
                                                                                                                                                                                ]
                                                                                                                                                                            ).TextValue,
                                                                                                                                                                            new OrderIdColumn()
                                                                                                                                                                                .Name
                                                                                                                                                                                .TextValue
                                                                                                                                                                        )
                                                                                                                                                                    )
                                                                                                                                                                ),
                                                                                                                                                                new FieldUuid(
                                                                                                                                                                    new JoinedString(
                                                                                                                                                                        new DotString(),
                                                                                                                                                                        [
                                                                                                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                            new OrdersTable().Name,
                                                                                                                                                                        ]
                                                                                                                                                                    ).TextValue,
                                                                                                                                                                    new OrderIdColumn()
                                                                                                                                                                        .Name
                                                                                                                                                                        .TextValue
                                                                                                                                                                )
                                                                                                                                                            )
                                                                                                                                                        ),
                                                                                                                                                        new FieldUuid(
                                                                                                                                                            new JoinedString(
                                                                                                                                                                new DotString(),
                                                                                                                                                                [
                                                                                                                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                    new OrdersTable().Name,
                                                                                                                                                                ]
                                                                                                                                                            ).TextValue,
                                                                                                                                                            new OrderIdColumn()
                                                                                                                                                                .Name
                                                                                                                                                                .TextValue
                                                                                                                                                        )
                                                                                                                                                    )
                                                                                                                                                ),
                                                                                                                                                new FieldUuid(
                                                                                                                                                    new JoinedString(
                                                                                                                                                        new DotString(),
                                                                                                                                                        [
                                                                                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                            new OrdersTable().Name,
                                                                                                                                                        ]
                                                                                                                                                    ).TextValue,
                                                                                                                                                    new OrderIdColumn()
                                                                                                                                                        .Name
                                                                                                                                                        .TextValue
                                                                                                                                                )
                                                                                                                                            )
                                                                                                                                        ),
                                                                                                                                        new FieldUuid(
                                                                                                                                            new JoinedString(
                                                                                                                                                new DotString(),
                                                                                                                                                [
                                                                                                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                    new OrdersTable().Name,
                                                                                                                                                ]
                                                                                                                                            ).TextValue,
                                                                                                                                            new OrderIdColumn()
                                                                                                                                                .Name
                                                                                                                                                .TextValue
                                                                                                                                        )
                                                                                                                                    )
                                                                                                                                ),
                                                                                                                                new FieldUuid(
                                                                                                                                    new JoinedString(
                                                                                                                                        new DotString(),
                                                                                                                                        [
                                                                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                            new OrdersTable().Name,
                                                                                                                                        ]
                                                                                                                                    ).TextValue,
                                                                                                                                    new OrderIdColumn()
                                                                                                                                        .Name
                                                                                                                                        .TextValue
                                                                                                                                )
                                                                                                                            )
                                                                                                                        ),
                                                                                                                        new FieldUuid(
                                                                                                                            new JoinedString(
                                                                                                                                new DotString(),
                                                                                                                                [
                                                                                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                    new OrdersTable().Name,
                                                                                                                                ]
                                                                                                                            ).TextValue,
                                                                                                                            new OrderIdColumn()
                                                                                                                                .Name
                                                                                                                                .TextValue
                                                                                                                        )
                                                                                                                    )
                                                                                                                ),
                                                                                                                new FieldUuid(
                                                                                                                    new JoinedString(
                                                                                                                        new DotString(),
                                                                                                                        [
                                                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                                                            new OrdersTable().Name,
                                                                                                                        ]
                                                                                                                    ).TextValue,
                                                                                                                    new OrderIdColumn()
                                                                                                                        .Name
                                                                                                                        .TextValue
                                                                                                                )
                                                                                                            )
                                                                                                        ),
                                                                                                        new FieldUuid(
                                                                                                            new JoinedString(
                                                                                                                new DotString(),
                                                                                                                [
                                                                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                                                                    new OrdersTable().Name,
                                                                                                                ]
                                                                                                            ).TextValue,
                                                                                                            new OrderIdColumn()
                                                                                                                .Name
                                                                                                                .TextValue
                                                                                                        )
                                                                                                    )
                                                                                                ),
                                                                                                new FieldUuid(
                                                                                                    new JoinedString(
                                                                                                        new DotString(),
                                                                                                        [
                                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                                            new OrdersTable().Name,
                                                                                                        ]
                                                                                                    ).TextValue,
                                                                                                    new OrderIdColumn()
                                                                                                        .Name
                                                                                                        .TextValue
                                                                                                )
                                                                                            )
                                                                                        ),
                                                                                        new FieldUuid(
                                                                                            new JoinedString(
                                                                                                new DotString(),
                                                                                                [
                                                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                                                    new OrdersTable().Name,
                                                                                                ]
                                                                                            ).TextValue,
                                                                                            new OrderIdColumn()
                                                                                                .Name
                                                                                                .TextValue
                                                                                        )
                                                                                    )
                                                                                ),
                                                                                new FieldUuid(
                                                                                    new JoinedString(
                                                                                        new DotString(),
                                                                                        [
                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                            new OrdersTable().Name,
                                                                                        ]
                                                                                    ).TextValue,
                                                                                    new OrderIdColumn()
                                                                                        .Name
                                                                                        .TextValue
                                                                                )
                                                                            )
                                                                        ),
                                                                        new FieldUuid(
                                                                            new JoinedString(
                                                                                new DotString(),
                                                                                [
                                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                                    new OrdersTable().Name,
                                                                                ]
                                                                            ).TextValue,
                                                                            new OrderIdColumn()
                                                                                .Name
                                                                                .TextValue
                                                                        )
                                                                    )
                                                                ),
                                                                new FieldUuid(
                                                                    new JoinedString(
                                                                        new DotString(),
                                                                        [
                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                            new OrdersTable().Name,
                                                                        ]
                                                                    ).TextValue,
                                                                    new OrderIdColumn()
                                                                        .Name
                                                                        .TextValue
                                                                )
                                                            )
                                                        ),
                                                        new FieldUuid(
                                                            new JoinedString(
                                                                new DotString(),
                                                                [
                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                    new OrdersTable().Name,
                                                                ]
                                                            ).TextValue,
                                                            new OrderIdColumn()
                                                                .Name
                                                                .TextValue
                                                        )
                                                    )
                                                ),
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
                                            )
                                        ),
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
                                    )
                                ),
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
                            )
                        ),
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
                    )
                ),
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
            ]
        );

    /// <summary>
    /// The rows the query returns under the PureQL specification's semantics over
    /// <see cref="SchemaDataSetWithForeignKeys"/> and
    /// <see cref="AuditSchemaDataSet"/>, in no particular order.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(new EmptyString(), [new IdColumn()], []),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new IdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000065-0000-0000-0000-000000000000"
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
                                new IdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000066-0000-0000-0000-000000000000"
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
                                new IdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000068-0000-0000-0000-000000000000"
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
                                new IdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000069-0000-0000-0000-000000000000"
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
                                new IdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "0000006a-0000-0000-0000-000000000000"
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
