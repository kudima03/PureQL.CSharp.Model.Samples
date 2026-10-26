# PureQL.CSharp.Model.Samples

Named, predefined PureQL queries — one sealed record per query — written against the stored data sets of `Pure.RelationalSchema.Storage.Samples`.

[![.NET build & test](https://github.com/kudima03/PureQL.CSharp.Model.Samples/actions/workflows/build-and-test.yml/badge.svg?branch=main)](https://github.com/kudima03/PureQL.CSharp.Model.Samples/actions/workflows/build-and-test.yml)
[![Build and Deploy](https://github.com/kudima03/PureQL.CSharp.Model.Samples/actions/workflows/publish-nuget.yml/badge.svg?branch=main)](https://github.com/kudima03/PureQL.CSharp.Model.Samples/actions/workflows/publish-nuget.yml)
[![NuGet](https://img.shields.io/nuget/v/PureQL.CSharp.Model.Samples)](https://www.nuget.org/packages/PureQL.CSharp.Model.Samples)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

## Overview

`PureQL.CSharp.Model.Samples` provides a fixed, deterministic catalogue of `PureQL.CSharp.Model` `PureQLQuery` instances for PureQL specification [`0.1.0-preview.2.0.1`](https://github.com/kudima03/PureQL-Specification/tree/0.1.0-preview.2.0.1). Every query reads from the query-grade fixtures of [`Pure.RelationalSchema.Storage.Samples`](https://github.com/kudima03/Pure.RelationalSchema.Storage.Samples): six users, six orders, four products, four order items and four employees in the `schema_with_foreign_keys` schema, and four logins in the `audit` schema. Nothing is random, nothing is generated at run time, nothing reads the clock or the current culture.

The catalogue mirrors the specification's [`samples/`](https://github.com/kudima03/PureQL-Specification/tree/0.1.0-preview.2.0.1/samples): each of its 57 samples appears here once, rewritten over the fixture tables so that it can be executed. A sample keeps the feature its specification counterpart demonstrates and is named after what the query asks, not after what an engine is expected to answer.

Next to them, the `Dashboards` folder holds 29 samples rewritten from the PureQL queries [Projector](https://github.com/Elegant-soft/Projector) stores for its dashboards (`GET api/v1/queries`): one per stored query, in the same way. They combine many features in one query, as a real dashboard does: filtered aggregates side by side, `if`-guarded ratios, computed date buckets, self-joins through aliases, and chains of grouped subqueries joined back to an entity. Each keeps the shape its Projector query demonstrates over the fixture tables. Order items stand in for order lines, users for customers, employees (through `employee_user_id`) for sales reps and managers, tenants for channels, and `user_score` for satisfaction. A query engine's tests can take a sample instead of rebuilding the AST inline, and a serializer, validator or pretty-printer gets a corpus of real queries for free.

Entity and field names are never spelled as literals. They are derived from the `Pure.RelationalSchema.Samples` metadata the stored data sets are built from:

```csharp
new FieldDecimal(
    new JoinedString(
        new DotString(),
        [new RelationalSchemaWithForeignKeys().Name, new OrdersTable().Name]
    ).TextValue,
    new OrderTotalColumn().Name.TextValue
)
// source "schema_with_foreign_keys.orders", field "order_total"
```

so a rename in the schema fixtures carries through to every query that references it.

## Sample shape

`PureQL.CSharp.Model.PureQLQuery` is a sealed class with no interface over it, so a sample cannot *be* a query the way `UsersTable` is an `ITable`. Instead each sample is a sealed record with a parameterless constructor that exposes its query through `Value`:

```csharp
public sealed record FilterOrderAndPageQuery
{
    public PureQLQuery Value => new PureQLQuery(new MainPlainQuery(...));
}
```

`Value` builds a fresh `PureQLQuery` on every read. Two instances of the same sample always produce the same PureQL document.

## Field types

The fixtures declare storage column types, not PureQL types, so every field is declared with the PureQL type of its column:

| Column type | PureQL type |
|---|---|
| `DoubleColumnType` (`order_total`, `product_price`, `item_qty`, `user_age`, `user_score`) | `decimal` |
| `StringColumnType` | `string` |
| `BoolColumnType` | `boolean` |
| `DateColumnType` | `date` |
| `TimeColumnType` | `time` |
| `DateTimeColumnType` | `datetime` |
| `UuidColumnType` | `uuid` |

A field is nullable where the fixture column holds a NULL (`users.user_score`, `employees.employee_manager_id`) and where the specification makes it nullable after an outer join. No fixture column is an `integer`, so the samples get their integers from `round`, `floor` and `ceiling`, `count`, `dateDiffDays`, `integerDivide`, `modulo` and integer literals.

## Expected results

81 of the 86 samples also carry a `Result`: the table the query returns, hard-coded in the same style as the query itself.

```csharp
IStoredTableDataSet expected = new GroupByCountQuery().Result;
```

The result is an `IStoredTableDataSet` (`StoredTableDataSet` from `Pure.RelationalSchema.Storage.Samples`). Its table schema has an empty name, no indexes and one column per `select` item, named by the item's alias and typed by its PureQL type: `integer` → `LongColumnType`, `decimal` → `DoubleColumnType`, `string` → `StringColumnType`, `boolean` → `BoolColumnType`, `date` → `DateColumnType`, `time` → `TimeColumnType`, `datetime` → `DateTimeColumnType`, `uuid` → `UuidColumnType`. When a column's name and type match a `Pure.RelationalSchema.Samples` column, that column is used as is (`new OrderTotalColumn()`). Rows are `Row`s of `InvariantCell`s over the typed `Pure.Primitives` values, NULL being `EmptyCell`. The property's summary names the stored data sets the result is computed over.

### How the results were computed

The results come from the semantics of the specification, not from any PureQL engine, so an engine can be tested against them. Every query was translated to SQL and run on PostgreSQL 17 over exactly the fixture rows of `Pure.RelationalSchema.Storage.Samples`. Where SQL and the specification disagree, the translation spells the specification's rule out:

- **Equality:** `equal` and `notEqual` are `IS NOT DISTINCT FROM` and `IS DISTINCT FROM`, so `null` equals `null` in every clause, `join.on` included. An ordering comparison against `null` is `false`.
- **Numbers:** `decimal` is exact (`numeric`). Each `decimal` answer is rounded half away from zero to 28 significant digits, as the specification rounds `divide` and `average`, and then to the nearest `double`. An engine computing in `double` should compare with a tolerance. `round` rounds half away from zero, `integerDivide` truncates toward zero and `modulo` takes the sign of its left operand.
- **Strings** compare and sort by code point (the `C` collation).
- **Aggregates:** `count` and `sum` over no rows are `0`, `any` is `false` and `all` is `true`. `min`, `max` and `average` skip NULLs. An aggregate next to fields in a plain query is computed over all rows and repeated on every row. A total query (`aggregate`) returns exactly one row.
- **`distinctBy`:** of the rows equal on the listed columns, compared with `equal`, the first in `orderBy` order is kept (`row_number()` over a partition by those columns, ordered as `orderBy`).
- **Sorting:** NULLs sort first ascending and last descending. Uuids sort by their bytes read as unsigned big-endian, and booleans `false` before `true`.
- **Dates and times:** `dateDiffDays` and the `…DiffSeconds` operators are `left − right`, and `timeAddSeconds` wraps around midnight.
- **Datetimes:** the fixture stores datetimes without an offset, and they are read as UTC. `datetime` literals are in UTC, as the specification requires, and `datetime` cells are written in UTC.

**Row order:** when the query has an `orderBy`, rows are in that order. No result has rows tied on every sort key. Without an `orderBy`, the rows are a multiset in no particular order. Each result was also checked against a copy of the fixtures loaded in reverse row order, and no result depends on physical row order.

### Samples without a result

5 samples have no `Result`, because they hold a parameter and the model has no way to bind one. Each keeps its parameter because the specification sample it mirrors is about one:

`Parameters.ParametersInWhereAndSelectQuery`, `Where.DecimalRangesAgainstLiteralsParameterAndFieldQuery`, `Where.InListParameterAndNotInListLiteralQuery`, `Pagination.FilterOrderAndPageQuery`, `GroupBy.HavingBooleanLogicQuery`

Every other parameter of the specification samples is replaced by a literal, and so is every parameter of the Projector queries, which filter by a date or a name rather than demonstrate parameters.

## Catalogue

`namespace PureQL.CSharp.Model.Samples.Queries.<Folder>`

| Spec | Folder | Sample | Exercises |
|---|---|---|---|
| 01 | `Select` | `UserNameColumnQuery` | One field from one entity |
| 02 | `Select` | `FieldsThroughFromAliasQuery` | Fields referenced through a `from` alias |
| 03 | `Select` | `LiteralOfEveryTypeNextToFieldQuery` | A literal of every type as a column |
| 04 | `Parameters` | `ParametersInWhereAndSelectQuery` | Parameters in `where` and as a column |
| 05 | `Where` | `WhereStatusEqualsLiteralQuery` | `equal` with a string literal |
| 06 | `Where` | `WhereBooleanFieldQuery` | A boolean field as the condition |
| 07 | `Where` | `WhereNotEqualAndNotQuery` | `notEqual` and `not` |
| 08 | `Where` | `WhereNestedAndOrQuery` | Nested `and` / `or` |
| 09 | `Where` | `DecimalRangesAgainstLiteralsParameterAndFieldQuery` | `decimal` ranges against literals, a parameter and a nullable field |
| 10 | `Where` | `StringDateAndTimeRangesQuery` | Ranges on `string`, `date` and `time` |
| 11 | `Where` | `InListParameterAndNotInListLiteralQuery` | `in` a list parameter and a list literal |
| 12 | `OrderBy` | `OrderByMultipleKeysQuery` | Several keys and directions |
| 13 | `Select` | `DistinctOrderStatusQuery` | `distinctBy` listing every column |
| 14 | `Select` | `LatestOrderPerUserQuery` | `distinctBy` keeping the first row per key in `orderBy` order |
| 15 | `Pagination` | `FilterOrderAndPageQuery` | Filter, order and page |
| 16 | `Expressions` | `IntegerArithmeticQuery` | `add` / `subtract` / `multiply` staying `integer` |
| 17 | `Expressions` | `DecimalWideningAndDivideQuery` | `integer` widening to `decimal`; `divide` |
| 18 | `Expressions` | `RoundingAndIntegerDivisionQuery` | `floor` / `ceiling` / `round`, `round` with digits, `integerDivide`, `modulo` |
| 19 | `Expressions` | `ConcatQuery` | `concat` |
| 20 | `Expressions` | `IfColumnQuery` | `if` as a column, with an `integer` branch widened to `decimal` |
| 21 | `OrderBy` | `OrderByComputedExpressionQuery` | Ordering by a computed expression |
| 22 | `Temporal` | `DateMathQuery` | `dateAddDays` / `dateDiffDays` as columns and in `where` |
| 23 | `Temporal` | `TimeAndDatetimeMathQuery` | `timeAddSeconds` wrapping past midnight, `datetimeDiffSeconds` |
| 24 | `Temporal` | `DatetimeUtcLiteralsQuery` | `datetime` literals in UTC with fractional seconds |
| 25 | `Nulls` | `NullableColumnQuery` | A nullable field in a nullable column |
| 26 | `Nulls` | `TypedNullLiteralsQuery` | Typed null literals, `integer?` widened to `decimal?` |
| 27 | `Nulls` | `CoalesceQuery` | `coalesce`, non-null and nullable |
| 28 | `Nulls` | `NullChecksQuery` | `equal` / `notEqual` against a typed null |
| 29 | `Nulls` | `NullableBooleanConditionsQuery` | A nullable boolean through `equal(…, false)` and `coalesce` |
| 30 | `Nulls` | `LiftedOperatorsQuery` | Lifted `add`, `dateAddDays` and `multiply` |
| 31 | `Joins` | `InnerJoinQuery` | Inner join on a key |
| 32 | `Joins` | `LeftJoinNullableFieldsQuery` | Left join: nullable fields, typed null, `coalesce` |
| 33 | `Joins` | `SelfJoinThroughAliasQuery` | Self-join through a join alias |
| 34 | `Joins` | `JoinOnNullableKeyQuery` | Join on a key nullable on both sides |
| 35 | `Joins` | `RightJoinQuery` | Right join: the left side becomes nullable |
| 36 | `Joins` | `FullJoinQuery` | Full join: both sides coalesced |
| 37 | `Joins` | `MultipleJoinsQuery` | Several joins of different kinds |
| 38 | `Joins` | `OuterJoinChainQuery` | Chained left joins; `null` equals `null` in the second `on` |
| 39 | `Aggregates` | `AggregatesOverAllRowsQuery` | A total query: `count` / `sum` / `min` / `max` / `average` / `any` / `all` over all rows |
| 40 | `Aggregates` | `AggregateWithPredicateNextToFieldsQuery` | Filtered aggregates next to row columns |
| 41 | `Aggregates` | `ShareOfTotalQuery` | A row value divided by an aggregate |
| 42 | `GroupBy` | `GroupByCountQuery` | One key and `count` |
| 43 | `GroupBy` | `GroupByMultipleKeysQuery` | Two keys |
| 44 | `GroupBy` | `ComputedGroupKeyQuery` | A computed key, selected and ordered by |
| 45 | `GroupBy` | `HavingCountQuery` | `having` on an aggregate |
| 46 | `GroupBy` | `HavingBooleanLogicQuery` | `having` with `and` / `or` / `not` and a parameter |
| 47 | `GroupBy` | `ConditionalAggregatesQuery` | Aggregates with predicates, `sum` over `if`, `any` / `all` |
| 48 | `GroupBy` | `GroupKeyUsageQuery` | Plain, nullable and computed keys in `select`, `having` and `orderBy` |
| 49 | `GroupBy` | `NullableGroupKeyAndAggregatesQuery` | A nullable key; `average` of `datetime` |
| 50 | `GroupBy` | `DateMathEverywhereQuery` | Date math per row and over aggregates |
| 51 | `GroupBy` | `IntegerAndDecimalInGroupsQuery` | `integer` / `decimal` rules in a grouped query |
| 52 | `GroupBy` | `GroupedRevenueQuery` | `count` over all rows, `having` and `orderBy` |
| 53 | `Subqueries` | `FromSubqueryQuery` | Reading from a subquery |
| 54 | `Subqueries` | `InSubqueryColumnQuery` | `in` over a subquery column |
| 55 | `Subqueries` | `SubqueryChainQuery` | A subquery reading another, joined by the main query |
| 56 | `Subqueries` | `LeftJoinSubqueryQuery` | Left join to a subquery |
| 57 | `Subqueries` | `SubqueryPipelineQuery` | Three subqueries feeding a joined, filtered, ordered, paged query |

### Dashboards

`namespace PureQL.CSharp.Model.Samples.Queries.Dashboards`, one sample per query Projector stores, by its id:

| Projector query | Sample | Exercises |
|---|---|---|
| `fc47ae13-4219-44c4-aed3-90cc9b6d6445` | `DailyRevenuePerTenantQuery` | A `sum` with a predicate per tenant next to the total |
| `dfddb067-ef61-48a8-9945-f22b8583c4d8` | `DailyInStockRevenueShareQuery` | `subtract`, `divide` and `multiply` over aggregates |
| `bb020cef-6f52-4db0-a66c-4059f01f8690` | `ShippedSharePerUserAndTenantQuery` | `if`-guarded ratios of filtered `count`s |
| `dfc68e29-087b-44f3-b744-b96527b93910` | `WeeklyUnitsPerProductQuery` | A computed week key, `dateAddDays` over the key, filtered `sum`s |
| `6a03aeba-fa02-4b56-91c9-5a4a5daab537` | `LoginStatsPerTenantQuery` | `average` of `datetimeDiffSeconds`, a coalesced nullable `average`, a guarded share |
| `cc714d72-0023-4ca6-a7bb-8f336d347671` | `RepeatBuyerOrderAndItemProfileQuery` | Two grouped subqueries inner joined, filtered on a subquery column |
| `d363779f-fa97-4f23-bcbe-57ed8752a372` | `ScoreByProductPerOrderDayQuery` | An `integer` computed key; `average`s with `equal` / `notEqual` predicates |
| `be439994-1e21-4ce5-a46b-416302754f08` | `ItemRevenueToOrderTotalPerEmployeeQuery` | An entity joined to two grouped subqueries, an `in`-list filter |
| `c87841bc-4433-4e7c-bcb8-91d62949bc44` | `RevenuePerEmployeeAndAgeQuery` | Four group keys; an aggregate divided by a `decimal` key |
| `8b1efefc-0a15-4b46-a451-21dc561c5da8` | `PlacedVersusShippedPerDayQuery` | Reading from a subquery left joined to another; `coalesce` and `subtract` |
| `89fcdced-5d53-43b0-b2ee-eb8197a9d091` | `OrderTotalsPerStatusAndTenantQuery` | Filtered `sum`s, the total, `average` of `dateDiffDays`, `count` |
| `5579e839-e1dd-44c9-bbd7-2a536b82db0f` | `InStockItemValueVersusOrderedPerUserQuery` | A joined subquery filtered by `and(equal, in)`; a variance percentage |
| `528d489c-032a-488a-96a0-fab37bd097df` | `RevenuePerTeamLeadPerPeriodQuery` | The same entity joined twice through aliases; a computed period key |
| `2fb00633-3184-4faf-acae-699795e9b3f6` | `RevenuePerUserNameQuery` | A key from a joined entity, ordered by an aggregate descending |
| `bd6f078a-0a03-4561-a6d7-83c06345a6f5` | `RecentOrdersPerActiveUserQuery` | `and` of a boolean field and a `datetime` range; ordered by `count`, then the key |
| `0b617d43-461a-408c-a432-15aef9fcbb60` | `UserFunnelStageQuery` | A left joined subquery; a nested `if` key over `not`, `coalesce` and `lessThan` |
| `b51b0f7f-907d-4058-b1ba-d6a403e990bf` | `OrderStageExcludingUserQuery` | `and` of `in` and `notEqual`; a nested `if` key over `equal` |
| `47412e00-e311-49ea-b053-ac6195acb437` | `ItemValuePerUserAndProductQuery` | A `concat` group key, a `greaterThan` filter |
| `e40e9165-3e2d-4686-b410-ec3c8cebd34c` | `OrdersPerWeekdayQuery` | A six-level `if` key over `modulo` of `dateDiffDays`; a guarded share |
| `934b5726-9aa7-48de-81da-d2430d2f9403` | `ItemValueShareOfOrderTotalPerProductQuery` | A ratio of two `sum`s |
| `2fac3de8-6ed5-406d-bf90-055674de7d4e` | `BilledWithFeePerEmployeeQuery` | A join chain, a date range, `sum` of `add`, ordered by it |
| `ba64a130-7551-4c81-89fe-20e6c9267303` | `UserAccountOverviewQuery` | Five subqueries, one reading another; inner and left joins; `max`; paged |
| `e2d37261-7318-4abc-809b-41d66a47a20f` | `LineRestockSuggestionQuery` | A left join on two keys; arithmetic in `if` branches; paged |
| `61b93c09-1e41-4d0b-9d4f-756d507cfbd5` | `EmployeeScorecardQuery` | A `from` alias self-joined through a join alias; three subqueries with nested `and` ranges |
| `e14b189d-90cc-4acb-b1e4-dc61998cbb03` | `UnbilledOrderAgingQuery` | `dateDiffDays` from a literal, `if` buckets over it, ordered by two expressions; paged |
| `59f44177-b983-46a8-901f-b9136f5c50c9` | `UserSalesSummaryQuery` | One inner and two left joined subqueries; a guard comparing `coalesce` with `integer` 0 |
| `b21432b3-e413-40e6-98ea-117cc50f5bce` | `DirectReportsPerManagerQuery` | `notEqual` on a nullable field; `min` of `time` and `date` |
| `6e0f2c5a-21ca-4eca-bf38-542e1e6a3d93` | `ProductSalesPerformanceQuery` | A guarded percentage, `average` of `datetimeDiffSeconds` and `max` of `dateDiffDays` in a subquery; ratios of its columns |
| `409f5283-7423-4d18-99a2-e1e8ff0be846` | `LineFlagsPerUserAndStatusQuery` | Two `string` keys; `count`s with `and` / `not` / `lessThan` / boolean predicates |

### Data sets

A sample's entities name the schema they read from. The stored data sets that hold those schemas are:

| Entity prefix | Stored data set |
|---|---|
| `schema_with_foreign_keys.` | `SchemaDataSetWithForeignKeys` |
| `audit.` | `AuditSchemaDataSet` |

Every result is computed over `[new SchemaDataSetWithForeignKeys(), new AuditSchemaDataSet()]`.

## Dependencies

- [`PureQL.CSharp.Model` 0.1.0-preview.13.0.0](https://github.com/kudima03/PureQL.CSharp.Model/tree/0.1.0-preview.13.0.0): the query AST every sample builds
- [`Pure.RelationalSchema.Samples` 0.1.0-preview.1.0.0](https://github.com/kudima03/Pure.RelationalSchema.Samples/tree/0.1.0-preview.2.0.1): the schema, table and column fixtures that entity, field and result column names come from
- [`Pure.RelationalSchema.Storage.Samples` 0.1.0-preview.1.0.0](https://github.com/kudima03/Pure.RelationalSchema.Storage.Samples/tree/0.1.0-preview.2.0.1): the fixture rows the results are computed over, and `StoredTableDataSet`, `InvariantCell` and `EmptyCell`, which the results are built from
- [`Pure.RelationalSchema` 2.0.3](https://github.com/kudima03/Pure.RelationalSchema/tree/2.0.3): `Table`, `Column` and the column types of a result's schema
- [`Pure.RelationalSchema.Storage` 0.1.0-preview.8.0.0](https://github.com/kudima03/Pure.RelationalSchema.Storage/tree/0.1.0-preview.8.0.0): `Row`
- [`Pure.RelationalSchema.HashCodes` 3.3.0](https://github.com/kudima03/Pure.RelationalSchema.HashCodes/tree/3.3.0): `ColumnHash`, which keys a row's cells
- [`Pure.Collections.Generic` 0.1.0-preview.3.0.0](https://github.com/kudima03/Pure.Collections.Generic/tree/0.1.0-preview.3.0.0): the hash-keyed dictionary behind `IRow.Cells`
- [`Pure.Primitives` 3.6.5](https://github.com/kudima03/Pure.Primitives/tree/3.6.5): `DotString`, the `schema.table` separator, and the typed values a result's cells wrap
- [`Pure.Primitives.String.Operations` 1.5.1](https://github.com/kudima03/Pure.Primitives.String.Operations/tree/1.5.1): `JoinedString`, which composes the entity name

No PureQL engine is referenced. Engines are what these samples test.

## Target Frameworks

- .NET 8
- .NET 9
- .NET 10

## Installation

```bash
dotnet add package PureQL.CSharp.Model.Samples
```

## Usage

```csharp
using PureQL.CSharp.Model;
using PureQL.CSharp.Model.Samples.Queries.GroupBy;

PureQLQuery query = new GroupByCountQuery().Value;
```

Executed against the matching stored data sets, and compared with the expected rows:

```csharp
using Pure.RelationalSchema.Storage.Abstractions;
using Pure.RelationalSchema.Storage.Samples.SchemaDataSets;
using PureQL.CSharp.Model.Samples.Queries.GroupBy;

IStoredTableDataSet expected = new GroupByCountQuery().Result;
// run new GroupByCountQuery().Value through an engine over
// [new SchemaDataSetWithForeignKeys(), new AuditSchemaDataSet()]
// and compare its rows with expected
```

Serialized to a PureQL document:

```csharp
using System.Text.Json;
using System.Text.Json.Serialization;
using PureQL.CSharp.Model.Samples.Queries.GroupBy;
using PureQL.CSharp.Model.Serialization;

JsonSerializerOptions options = new JsonSerializerOptions { MaxDepth = 256 };

foreach (JsonConverter converter in new PureQLConverters())
{
    options.Converters.Add(converter);
}

string json = JsonSerializer.Serialize(new GroupByCountQuery().Value, options);
```
