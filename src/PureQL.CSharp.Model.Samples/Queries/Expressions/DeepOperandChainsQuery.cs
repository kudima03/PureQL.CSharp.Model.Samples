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
using PureQL.CSharp.Model.Lists;
using PureQL.CSharp.Model.Literals;
using PureQL.CSharp.Model.ProjectionExpressions;
using PureQL.CSharp.Model.RowExpressions;
using Column = Pure.RelationalSchema.Column.Column;
using Guid = Pure.Primitives.Guid.Guid;
using String = Pure.Primitives.String.String;
using Table = Pure.RelationalSchema.Table.Table;

namespace PureQL.CSharp.Model.Samples.Queries.Expressions;

/// <summary>
/// Selects order_id and points from schema_with_foreign_keys.users left joined with
/// schema_with_foreign_keys.orders on order_user_id equal to user_id, where two
/// conditions hold, each nested 30 levels deep, ordered by a coalesce of typed nulls
/// nested 30 levels deep and then by order_id. The first condition is that a date is
/// greater than 2020-06-01: the placed_on of the order, coalesced after an if that gives
/// a typed null date when its condition is true and the signup_date otherwise, where the
/// condition of the innermost if is user_active and that of every other if the comparison
/// of the level below. The second is that a name is in the list literal [Ann, Bob, x]:
/// the user_name chosen by an if against the string x, with user_active as the innermost
/// condition and the level below as every other. points is the integer 1 when
/// user_active, else the floor of user_score, an if chained through else 30 levels deep.
/// </summary>
public sealed record DeepOperandChainsQuery
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
            joins:
            [
                new JoinEntity(
                    JoinKind.Left,
                    new JoinedString(
                        new DotString(),
                        [
                            new RelationalSchemaWithForeignKeys().Name,
                            new OrdersTable().Name,
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
            where: new AndRow([
                new GreaterThanDateRow(
                    new CoalesceDateNullableRow([
                        new IfDateNullableRow(
                            new LiteralDateNullable(),
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
                            new GreaterThanDateRow(
                                new CoalesceDateNullableRow([
                                    new IfDateNullableRow(
                                        new LiteralDateNullable(),
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
                                        new GreaterThanDateRow(
                                            new CoalesceDateNullableRow([
                                                new IfDateNullableRow(
                                                    new LiteralDateNullable(),
                                                    new FieldDate(
                                                        new JoinedString(
                                                            new DotString(),
                                                            [
                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                new UsersTable().Name,
                                                            ]
                                                        ).TextValue,
                                                        new SignupDateColumn()
                                                            .Name
                                                            .TextValue
                                                    ),
                                                    new GreaterThanDateRow(
                                                        new CoalesceDateNullableRow([
                                                            new IfDateNullableRow(
                                                                new LiteralDateNullable(),
                                                                new FieldDate(
                                                                    new JoinedString(
                                                                        new DotString(),
                                                                        [
                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                            new UsersTable().Name,
                                                                        ]
                                                                    ).TextValue,
                                                                    new SignupDateColumn()
                                                                        .Name
                                                                        .TextValue
                                                                ),
                                                                new GreaterThanDateRow(
                                                                    new CoalesceDateNullableRow([
                                                                        new IfDateNullableRow(
                                                                            new LiteralDateNullable(),
                                                                            new FieldDate(
                                                                                new JoinedString(
                                                                                    new DotString(),
                                                                                    [
                                                                                        new RelationalSchemaWithForeignKeys().Name,
                                                                                        new UsersTable().Name,
                                                                                    ]
                                                                                ).TextValue,
                                                                                new SignupDateColumn()
                                                                                    .Name
                                                                                    .TextValue
                                                                            ),
                                                                            new GreaterThanDateRow(
                                                                                new CoalesceDateNullableRow([
                                                                                    new IfDateNullableRow(
                                                                                        new LiteralDateNullable(),
                                                                                        new FieldDate(
                                                                                            new JoinedString(
                                                                                                new DotString(),
                                                                                                [
                                                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                                                    new UsersTable().Name,
                                                                                                ]
                                                                                            ).TextValue,
                                                                                            new SignupDateColumn()
                                                                                                .Name
                                                                                                .TextValue
                                                                                        ),
                                                                                        new GreaterThanDateRow(
                                                                                            new CoalesceDateNullableRow([
                                                                                                new IfDateNullableRow(
                                                                                                    new LiteralDateNullable(),
                                                                                                    new FieldDate(
                                                                                                        new JoinedString(
                                                                                                            new DotString(),
                                                                                                            [
                                                                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                                                                new UsersTable().Name,
                                                                                                            ]
                                                                                                        ).TextValue,
                                                                                                        new SignupDateColumn()
                                                                                                            .Name
                                                                                                            .TextValue
                                                                                                    ),
                                                                                                    new GreaterThanDateRow(
                                                                                                        new CoalesceDateNullableRow([
                                                                                                            new IfDateNullableRow(
                                                                                                                new LiteralDateNullable(),
                                                                                                                new FieldDate(
                                                                                                                    new JoinedString(
                                                                                                                        new DotString(),
                                                                                                                        [
                                                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                                                            new UsersTable().Name,
                                                                                                                        ]
                                                                                                                    ).TextValue,
                                                                                                                    new SignupDateColumn()
                                                                                                                        .Name
                                                                                                                        .TextValue
                                                                                                                ),
                                                                                                                new GreaterThanDateRow(
                                                                                                                    new CoalesceDateNullableRow([
                                                                                                                        new IfDateNullableRow(
                                                                                                                            new LiteralDateNullable(),
                                                                                                                            new FieldDate(
                                                                                                                                new JoinedString(
                                                                                                                                    new DotString(),
                                                                                                                                    [
                                                                                                                                        new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                        new UsersTable().Name,
                                                                                                                                    ]
                                                                                                                                ).TextValue,
                                                                                                                                new SignupDateColumn()
                                                                                                                                    .Name
                                                                                                                                    .TextValue
                                                                                                                            ),
                                                                                                                            new GreaterThanDateRow(
                                                                                                                                new CoalesceDateNullableRow([
                                                                                                                                    new IfDateNullableRow(
                                                                                                                                        new LiteralDateNullable(),
                                                                                                                                        new FieldDate(
                                                                                                                                            new JoinedString(
                                                                                                                                                new DotString(),
                                                                                                                                                [
                                                                                                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                    new UsersTable().Name,
                                                                                                                                                ]
                                                                                                                                            ).TextValue,
                                                                                                                                            new SignupDateColumn()
                                                                                                                                                .Name
                                                                                                                                                .TextValue
                                                                                                                                        ),
                                                                                                                                        new GreaterThanDateRow(
                                                                                                                                            new CoalesceDateNullableRow([
                                                                                                                                                new IfDateNullableRow(
                                                                                                                                                    new LiteralDateNullable(),
                                                                                                                                                    new FieldDate(
                                                                                                                                                        new JoinedString(
                                                                                                                                                            new DotString(),
                                                                                                                                                            [
                                                                                                                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                new UsersTable().Name,
                                                                                                                                                            ]
                                                                                                                                                        ).TextValue,
                                                                                                                                                        new SignupDateColumn()
                                                                                                                                                            .Name
                                                                                                                                                            .TextValue
                                                                                                                                                    ),
                                                                                                                                                    new GreaterThanDateRow(
                                                                                                                                                        new CoalesceDateNullableRow([
                                                                                                                                                            new IfDateNullableRow(
                                                                                                                                                                new LiteralDateNullable(),
                                                                                                                                                                new FieldDate(
                                                                                                                                                                    new JoinedString(
                                                                                                                                                                        new DotString(),
                                                                                                                                                                        [
                                                                                                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                            new UsersTable().Name,
                                                                                                                                                                        ]
                                                                                                                                                                    ).TextValue,
                                                                                                                                                                    new SignupDateColumn()
                                                                                                                                                                        .Name
                                                                                                                                                                        .TextValue
                                                                                                                                                                ),
                                                                                                                                                                new GreaterThanDateRow(
                                                                                                                                                                    new CoalesceDateNullableRow([
                                                                                                                                                                        new IfDateNullableRow(
                                                                                                                                                                            new LiteralDateNullable(),
                                                                                                                                                                            new FieldDate(
                                                                                                                                                                                new JoinedString(
                                                                                                                                                                                    new DotString(),
                                                                                                                                                                                    [
                                                                                                                                                                                        new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                        new UsersTable().Name,
                                                                                                                                                                                    ]
                                                                                                                                                                                ).TextValue,
                                                                                                                                                                                new SignupDateColumn()
                                                                                                                                                                                    .Name
                                                                                                                                                                                    .TextValue
                                                                                                                                                                            ),
                                                                                                                                                                            new GreaterThanDateRow(
                                                                                                                                                                                new CoalesceDateNullableRow([
                                                                                                                                                                                    new IfDateNullableRow(
                                                                                                                                                                                        new LiteralDateNullable(),
                                                                                                                                                                                        new FieldDate(
                                                                                                                                                                                            new JoinedString(
                                                                                                                                                                                                new DotString(),
                                                                                                                                                                                                [
                                                                                                                                                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                    new UsersTable().Name,
                                                                                                                                                                                                ]
                                                                                                                                                                                            ).TextValue,
                                                                                                                                                                                            new SignupDateColumn()
                                                                                                                                                                                                .Name
                                                                                                                                                                                                .TextValue
                                                                                                                                                                                        ),
                                                                                                                                                                                        new GreaterThanDateRow(
                                                                                                                                                                                            new CoalesceDateNullableRow([
                                                                                                                                                                                                new IfDateNullableRow(
                                                                                                                                                                                                    new LiteralDateNullable(),
                                                                                                                                                                                                    new FieldDate(
                                                                                                                                                                                                        new JoinedString(
                                                                                                                                                                                                            new DotString(),
                                                                                                                                                                                                            [
                                                                                                                                                                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                new UsersTable().Name,
                                                                                                                                                                                                            ]
                                                                                                                                                                                                        ).TextValue,
                                                                                                                                                                                                        new SignupDateColumn()
                                                                                                                                                                                                            .Name
                                                                                                                                                                                                            .TextValue
                                                                                                                                                                                                    ),
                                                                                                                                                                                                    new GreaterThanDateRow(
                                                                                                                                                                                                        new CoalesceDateNullableRow([
                                                                                                                                                                                                            new IfDateNullableRow(
                                                                                                                                                                                                                new LiteralDateNullable(),
                                                                                                                                                                                                                new FieldDate(
                                                                                                                                                                                                                    new JoinedString(
                                                                                                                                                                                                                        new DotString(),
                                                                                                                                                                                                                        [
                                                                                                                                                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                            new UsersTable().Name,
                                                                                                                                                                                                                        ]
                                                                                                                                                                                                                    ).TextValue,
                                                                                                                                                                                                                    new SignupDateColumn()
                                                                                                                                                                                                                        .Name
                                                                                                                                                                                                                        .TextValue
                                                                                                                                                                                                                ),
                                                                                                                                                                                                                new GreaterThanDateRow(
                                                                                                                                                                                                                    new CoalesceDateNullableRow([
                                                                                                                                                                                                                        new IfDateNullableRow(
                                                                                                                                                                                                                            new LiteralDateNullable(),
                                                                                                                                                                                                                            new FieldDate(
                                                                                                                                                                                                                                new JoinedString(
                                                                                                                                                                                                                                    new DotString(),
                                                                                                                                                                                                                                    [
                                                                                                                                                                                                                                        new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                                        new UsersTable().Name,
                                                                                                                                                                                                                                    ]
                                                                                                                                                                                                                                ).TextValue,
                                                                                                                                                                                                                                new SignupDateColumn()
                                                                                                                                                                                                                                    .Name
                                                                                                                                                                                                                                    .TextValue
                                                                                                                                                                                                                            ),
                                                                                                                                                                                                                            new GreaterThanDateRow(
                                                                                                                                                                                                                                new CoalesceDateNullableRow([
                                                                                                                                                                                                                                    new IfDateNullableRow(
                                                                                                                                                                                                                                        new LiteralDateNullable(),
                                                                                                                                                                                                                                        new FieldDate(
                                                                                                                                                                                                                                            new JoinedString(
                                                                                                                                                                                                                                                new DotString(),
                                                                                                                                                                                                                                                [
                                                                                                                                                                                                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                                                    new UsersTable().Name,
                                                                                                                                                                                                                                                ]
                                                                                                                                                                                                                                            ).TextValue,
                                                                                                                                                                                                                                            new SignupDateColumn()
                                                                                                                                                                                                                                                .Name
                                                                                                                                                                                                                                                .TextValue
                                                                                                                                                                                                                                        ),
                                                                                                                                                                                                                                        new GreaterThanDateRow(
                                                                                                                                                                                                                                            new CoalesceDateNullableRow([
                                                                                                                                                                                                                                                new IfDateNullableRow(
                                                                                                                                                                                                                                                    new LiteralDateNullable(),
                                                                                                                                                                                                                                                    new FieldDate(
                                                                                                                                                                                                                                                        new JoinedString(
                                                                                                                                                                                                                                                            new DotString(),
                                                                                                                                                                                                                                                            [
                                                                                                                                                                                                                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                                                                new UsersTable().Name,
                                                                                                                                                                                                                                                            ]
                                                                                                                                                                                                                                                        ).TextValue,
                                                                                                                                                                                                                                                        new SignupDateColumn()
                                                                                                                                                                                                                                                            .Name
                                                                                                                                                                                                                                                            .TextValue
                                                                                                                                                                                                                                                    ),
                                                                                                                                                                                                                                                    new GreaterThanDateRow(
                                                                                                                                                                                                                                                        new CoalesceDateNullableRow([
                                                                                                                                                                                                                                                            new IfDateNullableRow(
                                                                                                                                                                                                                                                                new LiteralDateNullable(),
                                                                                                                                                                                                                                                                new FieldDate(
                                                                                                                                                                                                                                                                    new JoinedString(
                                                                                                                                                                                                                                                                        new DotString(),
                                                                                                                                                                                                                                                                        [
                                                                                                                                                                                                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                                                                            new UsersTable().Name,
                                                                                                                                                                                                                                                                        ]
                                                                                                                                                                                                                                                                    ).TextValue,
                                                                                                                                                                                                                                                                    new SignupDateColumn()
                                                                                                                                                                                                                                                                        .Name
                                                                                                                                                                                                                                                                        .TextValue
                                                                                                                                                                                                                                                                ),
                                                                                                                                                                                                                                                                new GreaterThanDateRow(
                                                                                                                                                                                                                                                                    new CoalesceDateNullableRow([
                                                                                                                                                                                                                                                                        new IfDateNullableRow(
                                                                                                                                                                                                                                                                            new LiteralDateNullable(),
                                                                                                                                                                                                                                                                            new FieldDate(
                                                                                                                                                                                                                                                                                new JoinedString(
                                                                                                                                                                                                                                                                                    new DotString(),
                                                                                                                                                                                                                                                                                    [
                                                                                                                                                                                                                                                                                        new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                                                                                        new UsersTable().Name,
                                                                                                                                                                                                                                                                                    ]
                                                                                                                                                                                                                                                                                ).TextValue,
                                                                                                                                                                                                                                                                                new SignupDateColumn()
                                                                                                                                                                                                                                                                                    .Name
                                                                                                                                                                                                                                                                                    .TextValue
                                                                                                                                                                                                                                                                            ),
                                                                                                                                                                                                                                                                            new GreaterThanDateRow(
                                                                                                                                                                                                                                                                                new CoalesceDateNullableRow([
                                                                                                                                                                                                                                                                                    new IfDateNullableRow(
                                                                                                                                                                                                                                                                                        new LiteralDateNullable(),
                                                                                                                                                                                                                                                                                        new FieldDate(
                                                                                                                                                                                                                                                                                            new JoinedString(
                                                                                                                                                                                                                                                                                                new DotString(),
                                                                                                                                                                                                                                                                                                [
                                                                                                                                                                                                                                                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                                                                                                    new UsersTable().Name,
                                                                                                                                                                                                                                                                                                ]
                                                                                                                                                                                                                                                                                            ).TextValue,
                                                                                                                                                                                                                                                                                            new SignupDateColumn()
                                                                                                                                                                                                                                                                                                .Name
                                                                                                                                                                                                                                                                                                .TextValue
                                                                                                                                                                                                                                                                                        ),
                                                                                                                                                                                                                                                                                        new GreaterThanDateRow(
                                                                                                                                                                                                                                                                                            new CoalesceDateNullableRow([
                                                                                                                                                                                                                                                                                                new IfDateNullableRow(
                                                                                                                                                                                                                                                                                                    new LiteralDateNullable(),
                                                                                                                                                                                                                                                                                                    new FieldDate(
                                                                                                                                                                                                                                                                                                        new JoinedString(
                                                                                                                                                                                                                                                                                                            new DotString(),
                                                                                                                                                                                                                                                                                                            [
                                                                                                                                                                                                                                                                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                                                                                                                new UsersTable().Name,
                                                                                                                                                                                                                                                                                                            ]
                                                                                                                                                                                                                                                                                                        ).TextValue,
                                                                                                                                                                                                                                                                                                        new SignupDateColumn()
                                                                                                                                                                                                                                                                                                            .Name
                                                                                                                                                                                                                                                                                                            .TextValue
                                                                                                                                                                                                                                                                                                    ),
                                                                                                                                                                                                                                                                                                    new GreaterThanDateRow(
                                                                                                                                                                                                                                                                                                        new CoalesceDateNullableRow([
                                                                                                                                                                                                                                                                                                            new IfDateNullableRow(
                                                                                                                                                                                                                                                                                                                new LiteralDateNullable(),
                                                                                                                                                                                                                                                                                                                new FieldDate(
                                                                                                                                                                                                                                                                                                                    new JoinedString(
                                                                                                                                                                                                                                                                                                                        new DotString(),
                                                                                                                                                                                                                                                                                                                        [
                                                                                                                                                                                                                                                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                                                                                                                            new UsersTable().Name,
                                                                                                                                                                                                                                                                                                                        ]
                                                                                                                                                                                                                                                                                                                    ).TextValue,
                                                                                                                                                                                                                                                                                                                    new SignupDateColumn()
                                                                                                                                                                                                                                                                                                                        .Name
                                                                                                                                                                                                                                                                                                                        .TextValue
                                                                                                                                                                                                                                                                                                                ),
                                                                                                                                                                                                                                                                                                                new GreaterThanDateRow(
                                                                                                                                                                                                                                                                                                                    new CoalesceDateNullableRow([
                                                                                                                                                                                                                                                                                                                        new IfDateNullableRow(
                                                                                                                                                                                                                                                                                                                            new LiteralDateNullable(),
                                                                                                                                                                                                                                                                                                                            new FieldDate(
                                                                                                                                                                                                                                                                                                                                new JoinedString(
                                                                                                                                                                                                                                                                                                                                    new DotString(),
                                                                                                                                                                                                                                                                                                                                    [
                                                                                                                                                                                                                                                                                                                                        new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                                                                                                                                        new UsersTable().Name,
                                                                                                                                                                                                                                                                                                                                    ]
                                                                                                                                                                                                                                                                                                                                ).TextValue,
                                                                                                                                                                                                                                                                                                                                new SignupDateColumn()
                                                                                                                                                                                                                                                                                                                                    .Name
                                                                                                                                                                                                                                                                                                                                    .TextValue
                                                                                                                                                                                                                                                                                                                            ),
                                                                                                                                                                                                                                                                                                                            new GreaterThanDateRow(
                                                                                                                                                                                                                                                                                                                                new CoalesceDateNullableRow([
                                                                                                                                                                                                                                                                                                                                    new IfDateNullableRow(
                                                                                                                                                                                                                                                                                                                                        new LiteralDateNullable(),
                                                                                                                                                                                                                                                                                                                                        new FieldDate(
                                                                                                                                                                                                                                                                                                                                            new JoinedString(
                                                                                                                                                                                                                                                                                                                                                new DotString(),
                                                                                                                                                                                                                                                                                                                                                [
                                                                                                                                                                                                                                                                                                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                                                                                                                                                    new UsersTable().Name,
                                                                                                                                                                                                                                                                                                                                                ]
                                                                                                                                                                                                                                                                                                                                            ).TextValue,
                                                                                                                                                                                                                                                                                                                                            new SignupDateColumn()
                                                                                                                                                                                                                                                                                                                                                .Name
                                                                                                                                                                                                                                                                                                                                                .TextValue
                                                                                                                                                                                                                                                                                                                                        ),
                                                                                                                                                                                                                                                                                                                                        new GreaterThanDateRow(
                                                                                                                                                                                                                                                                                                                                            new CoalesceDateNullableRow([
                                                                                                                                                                                                                                                                                                                                                new IfDateNullableRow(
                                                                                                                                                                                                                                                                                                                                                    new LiteralDateNullable(),
                                                                                                                                                                                                                                                                                                                                                    new FieldDate(
                                                                                                                                                                                                                                                                                                                                                        new JoinedString(
                                                                                                                                                                                                                                                                                                                                                            new DotString(),
                                                                                                                                                                                                                                                                                                                                                            [
                                                                                                                                                                                                                                                                                                                                                                new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                                                                                                                                                                new UsersTable().Name,
                                                                                                                                                                                                                                                                                                                                                            ]
                                                                                                                                                                                                                                                                                                                                                        ).TextValue,
                                                                                                                                                                                                                                                                                                                                                        new SignupDateColumn()
                                                                                                                                                                                                                                                                                                                                                            .Name
                                                                                                                                                                                                                                                                                                                                                            .TextValue
                                                                                                                                                                                                                                                                                                                                                    ),
                                                                                                                                                                                                                                                                                                                                                    new GreaterThanDateRow(
                                                                                                                                                                                                                                                                                                                                                        new CoalesceDateNullableRow([
                                                                                                                                                                                                                                                                                                                                                            new IfDateNullableRow(
                                                                                                                                                                                                                                                                                                                                                                new LiteralDateNullable(),
                                                                                                                                                                                                                                                                                                                                                                new FieldDate(
                                                                                                                                                                                                                                                                                                                                                                    new JoinedString(
                                                                                                                                                                                                                                                                                                                                                                        new DotString(),
                                                                                                                                                                                                                                                                                                                                                                        [
                                                                                                                                                                                                                                                                                                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                                                                                                                                                                            new UsersTable().Name,
                                                                                                                                                                                                                                                                                                                                                                        ]
                                                                                                                                                                                                                                                                                                                                                                    ).TextValue,
                                                                                                                                                                                                                                                                                                                                                                    new SignupDateColumn()
                                                                                                                                                                                                                                                                                                                                                                        .Name
                                                                                                                                                                                                                                                                                                                                                                        .TextValue
                                                                                                                                                                                                                                                                                                                                                                ),
                                                                                                                                                                                                                                                                                                                                                                new GreaterThanDateRow(
                                                                                                                                                                                                                                                                                                                                                                    new CoalesceDateNullableRow([
                                                                                                                                                                                                                                                                                                                                                                        new IfDateNullableRow(
                                                                                                                                                                                                                                                                                                                                                                            new LiteralDateNullable(),
                                                                                                                                                                                                                                                                                                                                                                            new FieldDate(
                                                                                                                                                                                                                                                                                                                                                                                new JoinedString(
                                                                                                                                                                                                                                                                                                                                                                                    new DotString(),
                                                                                                                                                                                                                                                                                                                                                                                    [
                                                                                                                                                                                                                                                                                                                                                                                        new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                                                                                                                                                                                        new UsersTable().Name,
                                                                                                                                                                                                                                                                                                                                                                                    ]
                                                                                                                                                                                                                                                                                                                                                                                ).TextValue,
                                                                                                                                                                                                                                                                                                                                                                                new SignupDateColumn()
                                                                                                                                                                                                                                                                                                                                                                                    .Name
                                                                                                                                                                                                                                                                                                                                                                                    .TextValue
                                                                                                                                                                                                                                                                                                                                                                            ),
                                                                                                                                                                                                                                                                                                                                                                            new GreaterThanDateRow(
                                                                                                                                                                                                                                                                                                                                                                                new CoalesceDateNullableRow([
                                                                                                                                                                                                                                                                                                                                                                                    new IfDateNullableRow(
                                                                                                                                                                                                                                                                                                                                                                                        new LiteralDateNullable(),
                                                                                                                                                                                                                                                                                                                                                                                        new FieldDate(
                                                                                                                                                                                                                                                                                                                                                                                            new JoinedString(
                                                                                                                                                                                                                                                                                                                                                                                                new DotString(),
                                                                                                                                                                                                                                                                                                                                                                                                [
                                                                                                                                                                                                                                                                                                                                                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                                                                                                                                                                                                    new UsersTable().Name,
                                                                                                                                                                                                                                                                                                                                                                                                ]
                                                                                                                                                                                                                                                                                                                                                                                            ).TextValue,
                                                                                                                                                                                                                                                                                                                                                                                            new SignupDateColumn()
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
                                                                                                                                                                                                                                                                                                                                                                                    new FieldDateNullable(
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
                                                                                                                                                                                                                                                                                                                                                                                    ),
                                                                                                                                                                                                                                                                                                                                                                                ]),
                                                                                                                                                                                                                                                                                                                                                                                new LiteralDate(
                                                                                                                                                                                                                                                                                                                                                                                    new DateOnly(
                                                                                                                                                                                                                                                                                                                                                                                        2020,
                                                                                                                                                                                                                                                                                                                                                                                        6,
                                                                                                                                                                                                                                                                                                                                                                                        1
                                                                                                                                                                                                                                                                                                                                                                                    )
                                                                                                                                                                                                                                                                                                                                                                                )
                                                                                                                                                                                                                                                                                                                                                                            )
                                                                                                                                                                                                                                                                                                                                                                        ),
                                                                                                                                                                                                                                                                                                                                                                        new FieldDateNullable(
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
                                                                                                                                                                                                                                                                                                                                                                        ),
                                                                                                                                                                                                                                                                                                                                                                    ]),
                                                                                                                                                                                                                                                                                                                                                                    new LiteralDate(
                                                                                                                                                                                                                                                                                                                                                                        new DateOnly(
                                                                                                                                                                                                                                                                                                                                                                            2020,
                                                                                                                                                                                                                                                                                                                                                                            6,
                                                                                                                                                                                                                                                                                                                                                                            1
                                                                                                                                                                                                                                                                                                                                                                        )
                                                                                                                                                                                                                                                                                                                                                                    )
                                                                                                                                                                                                                                                                                                                                                                )
                                                                                                                                                                                                                                                                                                                                                            ),
                                                                                                                                                                                                                                                                                                                                                            new FieldDateNullable(
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
                                                                                                                                                                                                                                                                                                                                                            ),
                                                                                                                                                                                                                                                                                                                                                        ]),
                                                                                                                                                                                                                                                                                                                                                        new LiteralDate(
                                                                                                                                                                                                                                                                                                                                                            new DateOnly(
                                                                                                                                                                                                                                                                                                                                                                2020,
                                                                                                                                                                                                                                                                                                                                                                6,
                                                                                                                                                                                                                                                                                                                                                                1
                                                                                                                                                                                                                                                                                                                                                            )
                                                                                                                                                                                                                                                                                                                                                        )
                                                                                                                                                                                                                                                                                                                                                    )
                                                                                                                                                                                                                                                                                                                                                ),
                                                                                                                                                                                                                                                                                                                                                new FieldDateNullable(
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
                                                                                                                                                                                                                                                                                                                                                ),
                                                                                                                                                                                                                                                                                                                                            ]),
                                                                                                                                                                                                                                                                                                                                            new LiteralDate(
                                                                                                                                                                                                                                                                                                                                                new DateOnly(
                                                                                                                                                                                                                                                                                                                                                    2020,
                                                                                                                                                                                                                                                                                                                                                    6,
                                                                                                                                                                                                                                                                                                                                                    1
                                                                                                                                                                                                                                                                                                                                                )
                                                                                                                                                                                                                                                                                                                                            )
                                                                                                                                                                                                                                                                                                                                        )
                                                                                                                                                                                                                                                                                                                                    ),
                                                                                                                                                                                                                                                                                                                                    new FieldDateNullable(
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
                                                                                                                                                                                                                                                                                                                                    ),
                                                                                                                                                                                                                                                                                                                                ]),
                                                                                                                                                                                                                                                                                                                                new LiteralDate(
                                                                                                                                                                                                                                                                                                                                    new DateOnly(
                                                                                                                                                                                                                                                                                                                                        2020,
                                                                                                                                                                                                                                                                                                                                        6,
                                                                                                                                                                                                                                                                                                                                        1
                                                                                                                                                                                                                                                                                                                                    )
                                                                                                                                                                                                                                                                                                                                )
                                                                                                                                                                                                                                                                                                                            )
                                                                                                                                                                                                                                                                                                                        ),
                                                                                                                                                                                                                                                                                                                        new FieldDateNullable(
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
                                                                                                                                                                                                                                                                                                                        ),
                                                                                                                                                                                                                                                                                                                    ]),
                                                                                                                                                                                                                                                                                                                    new LiteralDate(
                                                                                                                                                                                                                                                                                                                        new DateOnly(
                                                                                                                                                                                                                                                                                                                            2020,
                                                                                                                                                                                                                                                                                                                            6,
                                                                                                                                                                                                                                                                                                                            1
                                                                                                                                                                                                                                                                                                                        )
                                                                                                                                                                                                                                                                                                                    )
                                                                                                                                                                                                                                                                                                                )
                                                                                                                                                                                                                                                                                                            ),
                                                                                                                                                                                                                                                                                                            new FieldDateNullable(
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
                                                                                                                                                                                                                                                                                                            ),
                                                                                                                                                                                                                                                                                                        ]),
                                                                                                                                                                                                                                                                                                        new LiteralDate(
                                                                                                                                                                                                                                                                                                            new DateOnly(
                                                                                                                                                                                                                                                                                                                2020,
                                                                                                                                                                                                                                                                                                                6,
                                                                                                                                                                                                                                                                                                                1
                                                                                                                                                                                                                                                                                                            )
                                                                                                                                                                                                                                                                                                        )
                                                                                                                                                                                                                                                                                                    )
                                                                                                                                                                                                                                                                                                ),
                                                                                                                                                                                                                                                                                                new FieldDateNullable(
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
                                                                                                                                                                                                                                                                                                ),
                                                                                                                                                                                                                                                                                            ]),
                                                                                                                                                                                                                                                                                            new LiteralDate(
                                                                                                                                                                                                                                                                                                new DateOnly(
                                                                                                                                                                                                                                                                                                    2020,
                                                                                                                                                                                                                                                                                                    6,
                                                                                                                                                                                                                                                                                                    1
                                                                                                                                                                                                                                                                                                )
                                                                                                                                                                                                                                                                                            )
                                                                                                                                                                                                                                                                                        )
                                                                                                                                                                                                                                                                                    ),
                                                                                                                                                                                                                                                                                    new FieldDateNullable(
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
                                                                                                                                                                                                                                                                                    ),
                                                                                                                                                                                                                                                                                ]),
                                                                                                                                                                                                                                                                                new LiteralDate(
                                                                                                                                                                                                                                                                                    new DateOnly(
                                                                                                                                                                                                                                                                                        2020,
                                                                                                                                                                                                                                                                                        6,
                                                                                                                                                                                                                                                                                        1
                                                                                                                                                                                                                                                                                    )
                                                                                                                                                                                                                                                                                )
                                                                                                                                                                                                                                                                            )
                                                                                                                                                                                                                                                                        ),
                                                                                                                                                                                                                                                                        new FieldDateNullable(
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
                                                                                                                                                                                                                                                                        ),
                                                                                                                                                                                                                                                                    ]),
                                                                                                                                                                                                                                                                    new LiteralDate(
                                                                                                                                                                                                                                                                        new DateOnly(
                                                                                                                                                                                                                                                                            2020,
                                                                                                                                                                                                                                                                            6,
                                                                                                                                                                                                                                                                            1
                                                                                                                                                                                                                                                                        )
                                                                                                                                                                                                                                                                    )
                                                                                                                                                                                                                                                                )
                                                                                                                                                                                                                                                            ),
                                                                                                                                                                                                                                                            new FieldDateNullable(
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
                                                                                                                                                                                                                                                            ),
                                                                                                                                                                                                                                                        ]),
                                                                                                                                                                                                                                                        new LiteralDate(
                                                                                                                                                                                                                                                            new DateOnly(
                                                                                                                                                                                                                                                                2020,
                                                                                                                                                                                                                                                                6,
                                                                                                                                                                                                                                                                1
                                                                                                                                                                                                                                                            )
                                                                                                                                                                                                                                                        )
                                                                                                                                                                                                                                                    )
                                                                                                                                                                                                                                                ),
                                                                                                                                                                                                                                                new FieldDateNullable(
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
                                                                                                                                                                                                                                                ),
                                                                                                                                                                                                                                            ]),
                                                                                                                                                                                                                                            new LiteralDate(
                                                                                                                                                                                                                                                new DateOnly(
                                                                                                                                                                                                                                                    2020,
                                                                                                                                                                                                                                                    6,
                                                                                                                                                                                                                                                    1
                                                                                                                                                                                                                                                )
                                                                                                                                                                                                                                            )
                                                                                                                                                                                                                                        )
                                                                                                                                                                                                                                    ),
                                                                                                                                                                                                                                    new FieldDateNullable(
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
                                                                                                                                                                                                                                    ),
                                                                                                                                                                                                                                ]),
                                                                                                                                                                                                                                new LiteralDate(
                                                                                                                                                                                                                                    new DateOnly(
                                                                                                                                                                                                                                        2020,
                                                                                                                                                                                                                                        6,
                                                                                                                                                                                                                                        1
                                                                                                                                                                                                                                    )
                                                                                                                                                                                                                                )
                                                                                                                                                                                                                            )
                                                                                                                                                                                                                        ),
                                                                                                                                                                                                                        new FieldDateNullable(
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
                                                                                                                                                                                                                        ),
                                                                                                                                                                                                                    ]),
                                                                                                                                                                                                                    new LiteralDate(
                                                                                                                                                                                                                        new DateOnly(
                                                                                                                                                                                                                            2020,
                                                                                                                                                                                                                            6,
                                                                                                                                                                                                                            1
                                                                                                                                                                                                                        )
                                                                                                                                                                                                                    )
                                                                                                                                                                                                                )
                                                                                                                                                                                                            ),
                                                                                                                                                                                                            new FieldDateNullable(
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
                                                                                                                                                                                                            ),
                                                                                                                                                                                                        ]),
                                                                                                                                                                                                        new LiteralDate(
                                                                                                                                                                                                            new DateOnly(
                                                                                                                                                                                                                2020,
                                                                                                                                                                                                                6,
                                                                                                                                                                                                                1
                                                                                                                                                                                                            )
                                                                                                                                                                                                        )
                                                                                                                                                                                                    )
                                                                                                                                                                                                ),
                                                                                                                                                                                                new FieldDateNullable(
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
                                                                                                                                                                                                ),
                                                                                                                                                                                            ]),
                                                                                                                                                                                            new LiteralDate(
                                                                                                                                                                                                new DateOnly(
                                                                                                                                                                                                    2020,
                                                                                                                                                                                                    6,
                                                                                                                                                                                                    1
                                                                                                                                                                                                )
                                                                                                                                                                                            )
                                                                                                                                                                                        )
                                                                                                                                                                                    ),
                                                                                                                                                                                    new FieldDateNullable(
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
                                                                                                                                                                                    ),
                                                                                                                                                                                ]),
                                                                                                                                                                                new LiteralDate(
                                                                                                                                                                                    new DateOnly(
                                                                                                                                                                                        2020,
                                                                                                                                                                                        6,
                                                                                                                                                                                        1
                                                                                                                                                                                    )
                                                                                                                                                                                )
                                                                                                                                                                            )
                                                                                                                                                                        ),
                                                                                                                                                                        new FieldDateNullable(
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
                                                                                                                                                                        ),
                                                                                                                                                                    ]),
                                                                                                                                                                    new LiteralDate(
                                                                                                                                                                        new DateOnly(
                                                                                                                                                                            2020,
                                                                                                                                                                            6,
                                                                                                                                                                            1
                                                                                                                                                                        )
                                                                                                                                                                    )
                                                                                                                                                                )
                                                                                                                                                            ),
                                                                                                                                                            new FieldDateNullable(
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
                                                                                                                                                            ),
                                                                                                                                                        ]),
                                                                                                                                                        new LiteralDate(
                                                                                                                                                            new DateOnly(
                                                                                                                                                                2020,
                                                                                                                                                                6,
                                                                                                                                                                1
                                                                                                                                                            )
                                                                                                                                                        )
                                                                                                                                                    )
                                                                                                                                                ),
                                                                                                                                                new FieldDateNullable(
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
                                                                                                                                                ),
                                                                                                                                            ]),
                                                                                                                                            new LiteralDate(
                                                                                                                                                new DateOnly(
                                                                                                                                                    2020,
                                                                                                                                                    6,
                                                                                                                                                    1
                                                                                                                                                )
                                                                                                                                            )
                                                                                                                                        )
                                                                                                                                    ),
                                                                                                                                    new FieldDateNullable(
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
                                                                                                                                    ),
                                                                                                                                ]),
                                                                                                                                new LiteralDate(
                                                                                                                                    new DateOnly(
                                                                                                                                        2020,
                                                                                                                                        6,
                                                                                                                                        1
                                                                                                                                    )
                                                                                                                                )
                                                                                                                            )
                                                                                                                        ),
                                                                                                                        new FieldDateNullable(
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
                                                                                                                        ),
                                                                                                                    ]),
                                                                                                                    new LiteralDate(
                                                                                                                        new DateOnly(
                                                                                                                            2020,
                                                                                                                            6,
                                                                                                                            1
                                                                                                                        )
                                                                                                                    )
                                                                                                                )
                                                                                                            ),
                                                                                                            new FieldDateNullable(
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
                                                                                                            ),
                                                                                                        ]),
                                                                                                        new LiteralDate(
                                                                                                            new DateOnly(
                                                                                                                2020,
                                                                                                                6,
                                                                                                                1
                                                                                                            )
                                                                                                        )
                                                                                                    )
                                                                                                ),
                                                                                                new FieldDateNullable(
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
                                                                                                ),
                                                                                            ]),
                                                                                            new LiteralDate(
                                                                                                new DateOnly(
                                                                                                    2020,
                                                                                                    6,
                                                                                                    1
                                                                                                )
                                                                                            )
                                                                                        )
                                                                                    ),
                                                                                    new FieldDateNullable(
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
                                                                                    ),
                                                                                ]),
                                                                                new LiteralDate(
                                                                                    new DateOnly(
                                                                                        2020,
                                                                                        6,
                                                                                        1
                                                                                    )
                                                                                )
                                                                            )
                                                                        ),
                                                                        new FieldDateNullable(
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
                                                                        ),
                                                                    ]),
                                                                    new LiteralDate(
                                                                        new DateOnly(
                                                                            2020,
                                                                            6,
                                                                            1
                                                                        )
                                                                    )
                                                                )
                                                            ),
                                                            new FieldDateNullable(
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
                                                            ),
                                                        ]),
                                                        new LiteralDate(
                                                            new DateOnly(2020, 6, 1)
                                                        )
                                                    )
                                                ),
                                                new FieldDateNullable(
                                                    new JoinedString(
                                                        new DotString(),
                                                        [
                                                            new RelationalSchemaWithForeignKeys().Name,
                                                            new OrdersTable().Name,
                                                        ]
                                                    ).TextValue,
                                                    new PlacedOnColumn().Name.TextValue
                                                ),
                                            ]),
                                            new LiteralDate(new DateOnly(2020, 6, 1))
                                        )
                                    ),
                                    new FieldDateNullable(
                                        new JoinedString(
                                            new DotString(),
                                            [
                                                new RelationalSchemaWithForeignKeys().Name,
                                                new OrdersTable().Name,
                                            ]
                                        ).TextValue,
                                        new PlacedOnColumn().Name.TextValue
                                    ),
                                ]),
                                new LiteralDate(new DateOnly(2020, 6, 1))
                            )
                        ),
                        new FieldDateNullable(
                            new JoinedString(
                                new DotString(),
                                [
                                    new RelationalSchemaWithForeignKeys().Name,
                                    new OrdersTable().Name,
                                ]
                            ).TextValue,
                            new PlacedOnColumn().Name.TextValue
                        ),
                    ]),
                    new LiteralDate(new DateOnly(2020, 6, 1))
                ),
                new InStringRow(
                    new IfStringRow(
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
                        new LiteralString("x"),
                        new InStringRow(
                            new IfStringRow(
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
                                new LiteralString("x"),
                                new InStringRow(
                                    new IfStringRow(
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
                                        new LiteralString("x"),
                                        new InStringRow(
                                            new IfStringRow(
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
                                                new LiteralString("x"),
                                                new InStringRow(
                                                    new IfStringRow(
                                                        new FieldString(
                                                            new JoinedString(
                                                                new DotString(),
                                                                [
                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                    new UsersTable().Name,
                                                                ]
                                                            ).TextValue,
                                                            new UserNameColumn()
                                                                .Name
                                                                .TextValue
                                                        ),
                                                        new LiteralString("x"),
                                                        new InStringRow(
                                                            new IfStringRow(
                                                                new FieldString(
                                                                    new JoinedString(
                                                                        new DotString(),
                                                                        [
                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                            new UsersTable().Name,
                                                                        ]
                                                                    ).TextValue,
                                                                    new UserNameColumn()
                                                                        .Name
                                                                        .TextValue
                                                                ),
                                                                new LiteralString("x"),
                                                                new InStringRow(
                                                                    new IfStringRow(
                                                                        new FieldString(
                                                                            new JoinedString(
                                                                                new DotString(),
                                                                                [
                                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                                    new UsersTable().Name,
                                                                                ]
                                                                            ).TextValue,
                                                                            new UserNameColumn()
                                                                                .Name
                                                                                .TextValue
                                                                        ),
                                                                        new LiteralString(
                                                                            "x"
                                                                        ),
                                                                        new InStringRow(
                                                                            new IfStringRow(
                                                                                new FieldString(
                                                                                    new JoinedString(
                                                                                        new DotString(),
                                                                                        [
                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                            new UsersTable().Name,
                                                                                        ]
                                                                                    ).TextValue,
                                                                                    new UserNameColumn()
                                                                                        .Name
                                                                                        .TextValue
                                                                                ),
                                                                                new LiteralString(
                                                                                    "x"
                                                                                ),
                                                                                new InStringRow(
                                                                                    new IfStringRow(
                                                                                        new FieldString(
                                                                                            new JoinedString(
                                                                                                new DotString(),
                                                                                                [
                                                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                                                    new UsersTable().Name,
                                                                                                ]
                                                                                            ).TextValue,
                                                                                            new UserNameColumn()
                                                                                                .Name
                                                                                                .TextValue
                                                                                        ),
                                                                                        new LiteralString(
                                                                                            "x"
                                                                                        ),
                                                                                        new InStringRow(
                                                                                            new IfStringRow(
                                                                                                new FieldString(
                                                                                                    new JoinedString(
                                                                                                        new DotString(),
                                                                                                        [
                                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                                            new UsersTable().Name,
                                                                                                        ]
                                                                                                    ).TextValue,
                                                                                                    new UserNameColumn()
                                                                                                        .Name
                                                                                                        .TextValue
                                                                                                ),
                                                                                                new LiteralString(
                                                                                                    "x"
                                                                                                ),
                                                                                                new InStringRow(
                                                                                                    new IfStringRow(
                                                                                                        new FieldString(
                                                                                                            new JoinedString(
                                                                                                                new DotString(),
                                                                                                                [
                                                                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                                                                    new UsersTable().Name,
                                                                                                                ]
                                                                                                            ).TextValue,
                                                                                                            new UserNameColumn()
                                                                                                                .Name
                                                                                                                .TextValue
                                                                                                        ),
                                                                                                        new LiteralString(
                                                                                                            "x"
                                                                                                        ),
                                                                                                        new InStringRow(
                                                                                                            new IfStringRow(
                                                                                                                new FieldString(
                                                                                                                    new JoinedString(
                                                                                                                        new DotString(),
                                                                                                                        [
                                                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                                                            new UsersTable().Name,
                                                                                                                        ]
                                                                                                                    ).TextValue,
                                                                                                                    new UserNameColumn()
                                                                                                                        .Name
                                                                                                                        .TextValue
                                                                                                                ),
                                                                                                                new LiteralString(
                                                                                                                    "x"
                                                                                                                ),
                                                                                                                new InStringRow(
                                                                                                                    new IfStringRow(
                                                                                                                        new FieldString(
                                                                                                                            new JoinedString(
                                                                                                                                new DotString(),
                                                                                                                                [
                                                                                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                    new UsersTable().Name,
                                                                                                                                ]
                                                                                                                            ).TextValue,
                                                                                                                            new UserNameColumn()
                                                                                                                                .Name
                                                                                                                                .TextValue
                                                                                                                        ),
                                                                                                                        new LiteralString(
                                                                                                                            "x"
                                                                                                                        ),
                                                                                                                        new InStringRow(
                                                                                                                            new IfStringRow(
                                                                                                                                new FieldString(
                                                                                                                                    new JoinedString(
                                                                                                                                        new DotString(),
                                                                                                                                        [
                                                                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                            new UsersTable().Name,
                                                                                                                                        ]
                                                                                                                                    ).TextValue,
                                                                                                                                    new UserNameColumn()
                                                                                                                                        .Name
                                                                                                                                        .TextValue
                                                                                                                                ),
                                                                                                                                new LiteralString(
                                                                                                                                    "x"
                                                                                                                                ),
                                                                                                                                new InStringRow(
                                                                                                                                    new IfStringRow(
                                                                                                                                        new FieldString(
                                                                                                                                            new JoinedString(
                                                                                                                                                new DotString(),
                                                                                                                                                [
                                                                                                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                    new UsersTable().Name,
                                                                                                                                                ]
                                                                                                                                            ).TextValue,
                                                                                                                                            new UserNameColumn()
                                                                                                                                                .Name
                                                                                                                                                .TextValue
                                                                                                                                        ),
                                                                                                                                        new LiteralString(
                                                                                                                                            "x"
                                                                                                                                        ),
                                                                                                                                        new InStringRow(
                                                                                                                                            new IfStringRow(
                                                                                                                                                new FieldString(
                                                                                                                                                    new JoinedString(
                                                                                                                                                        new DotString(),
                                                                                                                                                        [
                                                                                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                            new UsersTable().Name,
                                                                                                                                                        ]
                                                                                                                                                    ).TextValue,
                                                                                                                                                    new UserNameColumn()
                                                                                                                                                        .Name
                                                                                                                                                        .TextValue
                                                                                                                                                ),
                                                                                                                                                new LiteralString(
                                                                                                                                                    "x"
                                                                                                                                                ),
                                                                                                                                                new InStringRow(
                                                                                                                                                    new IfStringRow(
                                                                                                                                                        new FieldString(
                                                                                                                                                            new JoinedString(
                                                                                                                                                                new DotString(),
                                                                                                                                                                [
                                                                                                                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                    new UsersTable().Name,
                                                                                                                                                                ]
                                                                                                                                                            ).TextValue,
                                                                                                                                                            new UserNameColumn()
                                                                                                                                                                .Name
                                                                                                                                                                .TextValue
                                                                                                                                                        ),
                                                                                                                                                        new LiteralString(
                                                                                                                                                            "x"
                                                                                                                                                        ),
                                                                                                                                                        new InStringRow(
                                                                                                                                                            new IfStringRow(
                                                                                                                                                                new FieldString(
                                                                                                                                                                    new JoinedString(
                                                                                                                                                                        new DotString(),
                                                                                                                                                                        [
                                                                                                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                            new UsersTable().Name,
                                                                                                                                                                        ]
                                                                                                                                                                    ).TextValue,
                                                                                                                                                                    new UserNameColumn()
                                                                                                                                                                        .Name
                                                                                                                                                                        .TextValue
                                                                                                                                                                ),
                                                                                                                                                                new LiteralString(
                                                                                                                                                                    "x"
                                                                                                                                                                ),
                                                                                                                                                                new InStringRow(
                                                                                                                                                                    new IfStringRow(
                                                                                                                                                                        new FieldString(
                                                                                                                                                                            new JoinedString(
                                                                                                                                                                                new DotString(),
                                                                                                                                                                                [
                                                                                                                                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                    new UsersTable().Name,
                                                                                                                                                                                ]
                                                                                                                                                                            ).TextValue,
                                                                                                                                                                            new UserNameColumn()
                                                                                                                                                                                .Name
                                                                                                                                                                                .TextValue
                                                                                                                                                                        ),
                                                                                                                                                                        new LiteralString(
                                                                                                                                                                            "x"
                                                                                                                                                                        ),
                                                                                                                                                                        new InStringRow(
                                                                                                                                                                            new IfStringRow(
                                                                                                                                                                                new FieldString(
                                                                                                                                                                                    new JoinedString(
                                                                                                                                                                                        new DotString(),
                                                                                                                                                                                        [
                                                                                                                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                            new UsersTable().Name,
                                                                                                                                                                                        ]
                                                                                                                                                                                    ).TextValue,
                                                                                                                                                                                    new UserNameColumn()
                                                                                                                                                                                        .Name
                                                                                                                                                                                        .TextValue
                                                                                                                                                                                ),
                                                                                                                                                                                new LiteralString(
                                                                                                                                                                                    "x"
                                                                                                                                                                                ),
                                                                                                                                                                                new InStringRow(
                                                                                                                                                                                    new IfStringRow(
                                                                                                                                                                                        new FieldString(
                                                                                                                                                                                            new JoinedString(
                                                                                                                                                                                                new DotString(),
                                                                                                                                                                                                [
                                                                                                                                                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                    new UsersTable().Name,
                                                                                                                                                                                                ]
                                                                                                                                                                                            ).TextValue,
                                                                                                                                                                                            new UserNameColumn()
                                                                                                                                                                                                .Name
                                                                                                                                                                                                .TextValue
                                                                                                                                                                                        ),
                                                                                                                                                                                        new LiteralString(
                                                                                                                                                                                            "x"
                                                                                                                                                                                        ),
                                                                                                                                                                                        new InStringRow(
                                                                                                                                                                                            new IfStringRow(
                                                                                                                                                                                                new FieldString(
                                                                                                                                                                                                    new JoinedString(
                                                                                                                                                                                                        new DotString(),
                                                                                                                                                                                                        [
                                                                                                                                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                            new UsersTable().Name,
                                                                                                                                                                                                        ]
                                                                                                                                                                                                    ).TextValue,
                                                                                                                                                                                                    new UserNameColumn()
                                                                                                                                                                                                        .Name
                                                                                                                                                                                                        .TextValue
                                                                                                                                                                                                ),
                                                                                                                                                                                                new LiteralString(
                                                                                                                                                                                                    "x"
                                                                                                                                                                                                ),
                                                                                                                                                                                                new InStringRow(
                                                                                                                                                                                                    new IfStringRow(
                                                                                                                                                                                                        new FieldString(
                                                                                                                                                                                                            new JoinedString(
                                                                                                                                                                                                                new DotString(),
                                                                                                                                                                                                                [
                                                                                                                                                                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                    new UsersTable().Name,
                                                                                                                                                                                                                ]
                                                                                                                                                                                                            ).TextValue,
                                                                                                                                                                                                            new UserNameColumn()
                                                                                                                                                                                                                .Name
                                                                                                                                                                                                                .TextValue
                                                                                                                                                                                                        ),
                                                                                                                                                                                                        new LiteralString(
                                                                                                                                                                                                            "x"
                                                                                                                                                                                                        ),
                                                                                                                                                                                                        new InStringRow(
                                                                                                                                                                                                            new IfStringRow(
                                                                                                                                                                                                                new FieldString(
                                                                                                                                                                                                                    new JoinedString(
                                                                                                                                                                                                                        new DotString(),
                                                                                                                                                                                                                        [
                                                                                                                                                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                            new UsersTable().Name,
                                                                                                                                                                                                                        ]
                                                                                                                                                                                                                    ).TextValue,
                                                                                                                                                                                                                    new UserNameColumn()
                                                                                                                                                                                                                        .Name
                                                                                                                                                                                                                        .TextValue
                                                                                                                                                                                                                ),
                                                                                                                                                                                                                new LiteralString(
                                                                                                                                                                                                                    "x"
                                                                                                                                                                                                                ),
                                                                                                                                                                                                                new InStringRow(
                                                                                                                                                                                                                    new IfStringRow(
                                                                                                                                                                                                                        new FieldString(
                                                                                                                                                                                                                            new JoinedString(
                                                                                                                                                                                                                                new DotString(),
                                                                                                                                                                                                                                [
                                                                                                                                                                                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                                    new UsersTable().Name,
                                                                                                                                                                                                                                ]
                                                                                                                                                                                                                            ).TextValue,
                                                                                                                                                                                                                            new UserNameColumn()
                                                                                                                                                                                                                                .Name
                                                                                                                                                                                                                                .TextValue
                                                                                                                                                                                                                        ),
                                                                                                                                                                                                                        new LiteralString(
                                                                                                                                                                                                                            "x"
                                                                                                                                                                                                                        ),
                                                                                                                                                                                                                        new InStringRow(
                                                                                                                                                                                                                            new IfStringRow(
                                                                                                                                                                                                                                new FieldString(
                                                                                                                                                                                                                                    new JoinedString(
                                                                                                                                                                                                                                        new DotString(),
                                                                                                                                                                                                                                        [
                                                                                                                                                                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                                            new UsersTable().Name,
                                                                                                                                                                                                                                        ]
                                                                                                                                                                                                                                    ).TextValue,
                                                                                                                                                                                                                                    new UserNameColumn()
                                                                                                                                                                                                                                        .Name
                                                                                                                                                                                                                                        .TextValue
                                                                                                                                                                                                                                ),
                                                                                                                                                                                                                                new LiteralString(
                                                                                                                                                                                                                                    "x"
                                                                                                                                                                                                                                ),
                                                                                                                                                                                                                                new InStringRow(
                                                                                                                                                                                                                                    new IfStringRow(
                                                                                                                                                                                                                                        new FieldString(
                                                                                                                                                                                                                                            new JoinedString(
                                                                                                                                                                                                                                                new DotString(),
                                                                                                                                                                                                                                                [
                                                                                                                                                                                                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                                                    new UsersTable().Name,
                                                                                                                                                                                                                                                ]
                                                                                                                                                                                                                                            ).TextValue,
                                                                                                                                                                                                                                            new UserNameColumn()
                                                                                                                                                                                                                                                .Name
                                                                                                                                                                                                                                                .TextValue
                                                                                                                                                                                                                                        ),
                                                                                                                                                                                                                                        new LiteralString(
                                                                                                                                                                                                                                            "x"
                                                                                                                                                                                                                                        ),
                                                                                                                                                                                                                                        new InStringRow(
                                                                                                                                                                                                                                            new IfStringRow(
                                                                                                                                                                                                                                                new FieldString(
                                                                                                                                                                                                                                                    new JoinedString(
                                                                                                                                                                                                                                                        new DotString(),
                                                                                                                                                                                                                                                        [
                                                                                                                                                                                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                                                            new UsersTable().Name,
                                                                                                                                                                                                                                                        ]
                                                                                                                                                                                                                                                    ).TextValue,
                                                                                                                                                                                                                                                    new UserNameColumn()
                                                                                                                                                                                                                                                        .Name
                                                                                                                                                                                                                                                        .TextValue
                                                                                                                                                                                                                                                ),
                                                                                                                                                                                                                                                new LiteralString(
                                                                                                                                                                                                                                                    "x"
                                                                                                                                                                                                                                                ),
                                                                                                                                                                                                                                                new InStringRow(
                                                                                                                                                                                                                                                    new IfStringRow(
                                                                                                                                                                                                                                                        new FieldString(
                                                                                                                                                                                                                                                            new JoinedString(
                                                                                                                                                                                                                                                                new DotString(),
                                                                                                                                                                                                                                                                [
                                                                                                                                                                                                                                                                    new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                                                                    new UsersTable().Name,
                                                                                                                                                                                                                                                                ]
                                                                                                                                                                                                                                                            ).TextValue,
                                                                                                                                                                                                                                                            new UserNameColumn()
                                                                                                                                                                                                                                                                .Name
                                                                                                                                                                                                                                                                .TextValue
                                                                                                                                                                                                                                                        ),
                                                                                                                                                                                                                                                        new LiteralString(
                                                                                                                                                                                                                                                            "x"
                                                                                                                                                                                                                                                        ),
                                                                                                                                                                                                                                                        new InStringRow(
                                                                                                                                                                                                                                                            new IfStringRow(
                                                                                                                                                                                                                                                                new FieldString(
                                                                                                                                                                                                                                                                    new JoinedString(
                                                                                                                                                                                                                                                                        new DotString(),
                                                                                                                                                                                                                                                                        [
                                                                                                                                                                                                                                                                            new RelationalSchemaWithForeignKeys().Name,
                                                                                                                                                                                                                                                                            new UsersTable().Name,
                                                                                                                                                                                                                                                                        ]
                                                                                                                                                                                                                                                                    ).TextValue,
                                                                                                                                                                                                                                                                    new UserNameColumn()
                                                                                                                                                                                                                                                                        .Name
                                                                                                                                                                                                                                                                        .TextValue
                                                                                                                                                                                                                                                                ),
                                                                                                                                                                                                                                                                new LiteralString(
                                                                                                                                                                                                                                                                    "x"
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
                                                                                                                                                                                                                                                            new ListLiteralString([
                                                                                                                                                                                                                                                                "Ann",
                                                                                                                                                                                                                                                                "Bob",
                                                                                                                                                                                                                                                                "x",
                                                                                                                                                                                                                                                            ])
                                                                                                                                                                                                                                                        )
                                                                                                                                                                                                                                                    ),
                                                                                                                                                                                                                                                    new ListLiteralString([
                                                                                                                                                                                                                                                        "Ann",
                                                                                                                                                                                                                                                        "Bob",
                                                                                                                                                                                                                                                        "x",
                                                                                                                                                                                                                                                    ])
                                                                                                                                                                                                                                                )
                                                                                                                                                                                                                                            ),
                                                                                                                                                                                                                                            new ListLiteralString([
                                                                                                                                                                                                                                                "Ann",
                                                                                                                                                                                                                                                "Bob",
                                                                                                                                                                                                                                                "x",
                                                                                                                                                                                                                                            ])
                                                                                                                                                                                                                                        )
                                                                                                                                                                                                                                    ),
                                                                                                                                                                                                                                    new ListLiteralString([
                                                                                                                                                                                                                                        "Ann",
                                                                                                                                                                                                                                        "Bob",
                                                                                                                                                                                                                                        "x",
                                                                                                                                                                                                                                    ])
                                                                                                                                                                                                                                )
                                                                                                                                                                                                                            ),
                                                                                                                                                                                                                            new ListLiteralString([
                                                                                                                                                                                                                                "Ann",
                                                                                                                                                                                                                                "Bob",
                                                                                                                                                                                                                                "x",
                                                                                                                                                                                                                            ])
                                                                                                                                                                                                                        )
                                                                                                                                                                                                                    ),
                                                                                                                                                                                                                    new ListLiteralString([
                                                                                                                                                                                                                        "Ann",
                                                                                                                                                                                                                        "Bob",
                                                                                                                                                                                                                        "x",
                                                                                                                                                                                                                    ])
                                                                                                                                                                                                                )
                                                                                                                                                                                                            ),
                                                                                                                                                                                                            new ListLiteralString([
                                                                                                                                                                                                                "Ann",
                                                                                                                                                                                                                "Bob",
                                                                                                                                                                                                                "x",
                                                                                                                                                                                                            ])
                                                                                                                                                                                                        )
                                                                                                                                                                                                    ),
                                                                                                                                                                                                    new ListLiteralString([
                                                                                                                                                                                                        "Ann",
                                                                                                                                                                                                        "Bob",
                                                                                                                                                                                                        "x",
                                                                                                                                                                                                    ])
                                                                                                                                                                                                )
                                                                                                                                                                                            ),
                                                                                                                                                                                            new ListLiteralString([
                                                                                                                                                                                                "Ann",
                                                                                                                                                                                                "Bob",
                                                                                                                                                                                                "x",
                                                                                                                                                                                            ])
                                                                                                                                                                                        )
                                                                                                                                                                                    ),
                                                                                                                                                                                    new ListLiteralString([
                                                                                                                                                                                        "Ann",
                                                                                                                                                                                        "Bob",
                                                                                                                                                                                        "x",
                                                                                                                                                                                    ])
                                                                                                                                                                                )
                                                                                                                                                                            ),
                                                                                                                                                                            new ListLiteralString([
                                                                                                                                                                                "Ann",
                                                                                                                                                                                "Bob",
                                                                                                                                                                                "x",
                                                                                                                                                                            ])
                                                                                                                                                                        )
                                                                                                                                                                    ),
                                                                                                                                                                    new ListLiteralString([
                                                                                                                                                                        "Ann",
                                                                                                                                                                        "Bob",
                                                                                                                                                                        "x",
                                                                                                                                                                    ])
                                                                                                                                                                )
                                                                                                                                                            ),
                                                                                                                                                            new ListLiteralString([
                                                                                                                                                                "Ann",
                                                                                                                                                                "Bob",
                                                                                                                                                                "x",
                                                                                                                                                            ])
                                                                                                                                                        )
                                                                                                                                                    ),
                                                                                                                                                    new ListLiteralString([
                                                                                                                                                        "Ann",
                                                                                                                                                        "Bob",
                                                                                                                                                        "x",
                                                                                                                                                    ])
                                                                                                                                                )
                                                                                                                                            ),
                                                                                                                                            new ListLiteralString([
                                                                                                                                                "Ann",
                                                                                                                                                "Bob",
                                                                                                                                                "x",
                                                                                                                                            ])
                                                                                                                                        )
                                                                                                                                    ),
                                                                                                                                    new ListLiteralString([
                                                                                                                                        "Ann",
                                                                                                                                        "Bob",
                                                                                                                                        "x",
                                                                                                                                    ])
                                                                                                                                )
                                                                                                                            ),
                                                                                                                            new ListLiteralString([
                                                                                                                                "Ann",
                                                                                                                                "Bob",
                                                                                                                                "x",
                                                                                                                            ])
                                                                                                                        )
                                                                                                                    ),
                                                                                                                    new ListLiteralString([
                                                                                                                        "Ann",
                                                                                                                        "Bob",
                                                                                                                        "x",
                                                                                                                    ])
                                                                                                                )
                                                                                                            ),
                                                                                                            new ListLiteralString([
                                                                                                                "Ann",
                                                                                                                "Bob",
                                                                                                                "x",
                                                                                                            ])
                                                                                                        )
                                                                                                    ),
                                                                                                    new ListLiteralString([
                                                                                                        "Ann",
                                                                                                        "Bob",
                                                                                                        "x",
                                                                                                    ])
                                                                                                )
                                                                                            ),
                                                                                            new ListLiteralString([
                                                                                                "Ann",
                                                                                                "Bob",
                                                                                                "x",
                                                                                            ])
                                                                                        )
                                                                                    ),
                                                                                    new ListLiteralString([
                                                                                        "Ann",
                                                                                        "Bob",
                                                                                        "x",
                                                                                    ])
                                                                                )
                                                                            ),
                                                                            new ListLiteralString([
                                                                                "Ann",
                                                                                "Bob",
                                                                                "x",
                                                                            ])
                                                                        )
                                                                    ),
                                                                    new ListLiteralString([
                                                                        "Ann",
                                                                        "Bob",
                                                                        "x",
                                                                    ])
                                                                )
                                                            ),
                                                            new ListLiteralString([
                                                                "Ann",
                                                                "Bob",
                                                                "x",
                                                            ])
                                                        )
                                                    ),
                                                    new ListLiteralString([
                                                        "Ann",
                                                        "Bob",
                                                        "x",
                                                    ])
                                                )
                                            ),
                                            new ListLiteralString(["Ann", "Bob", "x"])
                                        )
                                    ),
                                    new ListLiteralString(["Ann", "Bob", "x"])
                                )
                            ),
                            new ListLiteralString(["Ann", "Bob", "x"])
                        )
                    ),
                    new ListLiteralString(["Ann", "Bob", "x"])
                ),
            ]),
            select:
            [
                new SelectItemProjection(
                    "order_id",
                    new FieldUuidNullable(
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
                    "points",
                    new IfIntegerNullableProjection(
                        new LiteralInteger(1),
                        new IfIntegerNullableProjection(
                            new LiteralInteger(1),
                            new IfIntegerNullableProjection(
                                new LiteralInteger(1),
                                new IfIntegerNullableProjection(
                                    new LiteralInteger(1),
                                    new IfIntegerNullableProjection(
                                        new LiteralInteger(1),
                                        new IfIntegerNullableProjection(
                                            new LiteralInteger(1),
                                            new IfIntegerNullableProjection(
                                                new LiteralInteger(1),
                                                new IfIntegerNullableProjection(
                                                    new LiteralInteger(1),
                                                    new IfIntegerNullableProjection(
                                                        new LiteralInteger(1),
                                                        new IfIntegerNullableProjection(
                                                            new LiteralInteger(1),
                                                            new IfIntegerNullableProjection(
                                                                new LiteralInteger(1),
                                                                new IfIntegerNullableProjection(
                                                                    new LiteralInteger(1),
                                                                    new IfIntegerNullableProjection(
                                                                        new LiteralInteger(
                                                                            1
                                                                        ),
                                                                        new IfIntegerNullableProjection(
                                                                            new LiteralInteger(
                                                                                1
                                                                            ),
                                                                            new IfIntegerNullableProjection(
                                                                                new LiteralInteger(
                                                                                    1
                                                                                ),
                                                                                new IfIntegerNullableProjection(
                                                                                    new LiteralInteger(
                                                                                        1
                                                                                    ),
                                                                                    new IfIntegerNullableProjection(
                                                                                        new LiteralInteger(
                                                                                            1
                                                                                        ),
                                                                                        new IfIntegerNullableProjection(
                                                                                            new LiteralInteger(
                                                                                                1
                                                                                            ),
                                                                                            new IfIntegerNullableProjection(
                                                                                                new LiteralInteger(
                                                                                                    1
                                                                                                ),
                                                                                                new IfIntegerNullableProjection(
                                                                                                    new LiteralInteger(
                                                                                                        1
                                                                                                    ),
                                                                                                    new IfIntegerNullableProjection(
                                                                                                        new LiteralInteger(
                                                                                                            1
                                                                                                        ),
                                                                                                        new IfIntegerNullableProjection(
                                                                                                            new LiteralInteger(
                                                                                                                1
                                                                                                            ),
                                                                                                            new IfIntegerNullableProjection(
                                                                                                                new LiteralInteger(
                                                                                                                    1
                                                                                                                ),
                                                                                                                new IfIntegerNullableProjection(
                                                                                                                    new LiteralInteger(
                                                                                                                        1
                                                                                                                    ),
                                                                                                                    new IfIntegerNullableProjection(
                                                                                                                        new LiteralInteger(
                                                                                                                            1
                                                                                                                        ),
                                                                                                                        new IfIntegerNullableProjection(
                                                                                                                            new LiteralInteger(
                                                                                                                                1
                                                                                                                            ),
                                                                                                                            new IfIntegerNullableProjection(
                                                                                                                                new LiteralInteger(
                                                                                                                                    1
                                                                                                                                ),
                                                                                                                                new IfIntegerNullableProjection(
                                                                                                                                    new LiteralInteger(
                                                                                                                                        1
                                                                                                                                    ),
                                                                                                                                    new IfIntegerNullableProjection(
                                                                                                                                        new LiteralInteger(
                                                                                                                                            1
                                                                                                                                        ),
                                                                                                                                        new IfIntegerNullableProjection(
                                                                                                                                            new LiteralInteger(
                                                                                                                                                1
                                                                                                                                            ),
                                                                                                                                            new FloorIntegerNullableProjection(
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
                    )
                ),
            ],
            orderBy:
            [
                new OrderItemProjectionExpression(
                    new CoalesceDecimalNullableProjection([
                        new CoalesceDecimalNullableProjection([
                            new CoalesceDecimalNullableProjection([
                                new CoalesceDecimalNullableProjection([
                                    new CoalesceDecimalNullableProjection([
                                        new CoalesceDecimalNullableProjection([
                                            new CoalesceDecimalNullableProjection([
                                                new CoalesceDecimalNullableProjection([
                                                    new CoalesceDecimalNullableProjection([
                                                        new CoalesceDecimalNullableProjection([
                                                            new CoalesceDecimalNullableProjection([
                                                                new CoalesceDecimalNullableProjection([
                                                                    new CoalesceDecimalNullableProjection([
                                                                        new CoalesceDecimalNullableProjection([
                                                                            new CoalesceDecimalNullableProjection([
                                                                                new CoalesceDecimalNullableProjection([
                                                                                    new CoalesceDecimalNullableProjection([
                                                                                        new CoalesceDecimalNullableProjection([
                                                                                            new CoalesceDecimalNullableProjection([
                                                                                                new CoalesceDecimalNullableProjection([
                                                                                                    new CoalesceDecimalNullableProjection([
                                                                                                        new CoalesceDecimalNullableProjection([
                                                                                                            new CoalesceDecimalNullableProjection([
                                                                                                                new CoalesceDecimalNullableProjection([
                                                                                                                    new CoalesceDecimalNullableProjection([
                                                                                                                        new CoalesceDecimalNullableProjection([
                                                                                                                            new CoalesceDecimalNullableProjection([
                                                                                                                                new CoalesceDecimalNullableProjection([
                                                                                                                                    new CoalesceDecimalNullableProjection([
                                                                                                                                        new CoalesceDecimalNullableProjection([
                                                                                                                                            new LiteralIntegerNullable(),
                                                                                                                                            new LiteralDecimalNullable(),
                                                                                                                                        ]),
                                                                                                                                        new LiteralDecimalNullable(),
                                                                                                                                    ]),
                                                                                                                                    new LiteralDecimalNullable(),
                                                                                                                                ]),
                                                                                                                                new LiteralDecimalNullable(),
                                                                                                                            ]),
                                                                                                                            new LiteralDecimalNullable(),
                                                                                                                        ]),
                                                                                                                        new LiteralDecimalNullable(),
                                                                                                                    ]),
                                                                                                                    new LiteralDecimalNullable(),
                                                                                                                ]),
                                                                                                                new LiteralDecimalNullable(),
                                                                                                            ]),
                                                                                                            new LiteralDecimalNullable(),
                                                                                                        ]),
                                                                                                        new LiteralDecimalNullable(),
                                                                                                    ]),
                                                                                                    new LiteralDecimalNullable(),
                                                                                                ]),
                                                                                                new LiteralDecimalNullable(),
                                                                                            ]),
                                                                                            new LiteralDecimalNullable(),
                                                                                        ]),
                                                                                        new LiteralDecimalNullable(),
                                                                                    ]),
                                                                                    new LiteralDecimalNullable(),
                                                                                ]),
                                                                                new LiteralDecimalNullable(),
                                                                            ]),
                                                                            new LiteralDecimalNullable(),
                                                                        ]),
                                                                        new LiteralDecimalNullable(),
                                                                    ]),
                                                                    new LiteralDecimalNullable(),
                                                                ]),
                                                                new LiteralDecimalNullable(),
                                                            ]),
                                                            new LiteralDecimalNullable(),
                                                        ]),
                                                        new LiteralDecimalNullable(),
                                                    ]),
                                                    new LiteralDecimalNullable(),
                                                ]),
                                                new LiteralDecimalNullable(),
                                            ]),
                                            new LiteralDecimalNullable(),
                                        ]),
                                        new LiteralDecimalNullable(),
                                    ]),
                                    new LiteralDecimalNullable(),
                                ]),
                                new LiteralDecimalNullable(),
                            ]),
                            new LiteralDecimalNullable(),
                        ]),
                        new LiteralDecimalNullable(),
                    ])
                ),
                new OrderItemProjectionExpression(
                    new FieldUuidNullable(
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
    /// <see cref="AuditSchemaDataSet"/>, in this order.
    /// </summary>
    public IStoredTableDataSet Result =>
        new StoredTableDataSet(
            new Table(
                new EmptyString(),
                [
                    new OrderIdColumn(),
                    new Column(new String("points"), new LongColumnType()),
                ],
                []
            ),
            [
                new Row(
                    new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                        [
                            new KeyValuePair<IColumn, ICell>(
                                new OrderIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000065-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("points"), new LongColumnType()),
                                new InvariantCell(new Long(1))
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
                                new OrderIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000066-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("points"), new LongColumnType()),
                                new InvariantCell(new Long(1))
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
                                new OrderIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000067-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("points"), new LongColumnType()),
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
                                new OrderIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000068-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("points"), new LongColumnType()),
                                new InvariantCell(new Long(1))
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
                                new OrderIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "00000069-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("points"), new LongColumnType()),
                                new InvariantCell(new Long(1))
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
                                new OrderIdColumn(),
                                new InvariantCell(
                                    new Guid(
                                        new System.Guid(
                                            "0000006a-0000-0000-0000-000000000000"
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<IColumn, ICell>(
                                new Column(new String("points"), new LongColumnType()),
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
