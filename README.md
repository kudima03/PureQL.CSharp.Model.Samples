# PureQL.CSharp.Model.Samples

Named, predefined PureQL queries — one sealed record per query — written against the stored data sets of `Pure.RelationalSchema.Storage.Samples`.

[![.NET build & test](https://github.com/kudima03/PureQL.CSharp.Model.Samples/actions/workflows/build-and-test.yml/badge.svg?branch=main)](https://github.com/kudima03/PureQL.CSharp.Model.Samples/actions/workflows/build-and-test.yml)
[![Build and Deploy](https://github.com/kudima03/PureQL.CSharp.Model.Samples/actions/workflows/publish-nuget.yml/badge.svg?branch=main)](https://github.com/kudima03/PureQL.CSharp.Model.Samples/actions/workflows/publish-nuget.yml)
[![NuGet](https://img.shields.io/nuget/v/PureQL.CSharp.Model.Samples)](https://www.nuget.org/packages/PureQL.CSharp.Model.Samples)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

## Overview

`PureQL.CSharp.Model.Samples` provides a fixed, deterministic catalogue of `PureQL.CSharp.Model` `PureQLQuery` instances for PureQL specification [`0.1.0-preview.3.0.0`](https://github.com/kudima03/PureQL-Specification/tree/0.1.0-preview.3.0.0). Every query reads from the query-grade fixtures of [`Pure.RelationalSchema.Storage.Samples`](https://github.com/kudima03/Pure.RelationalSchema.Storage.Samples): six users, six orders, four products, four order items and four employees in the `schema_with_foreign_keys` schema, and four logins in the `audit` schema. Nothing is random, nothing is generated at run time, nothing reads the clock or the current culture.

The catalogue mirrors the specification's [`samples/valid`](https://github.com/kudima03/PureQL-Specification/tree/0.1.0-preview.3.0.0/samples/valid): each of its 151 samples (the `.json` files, not the `.jsonc` tests) appears here once, rewritten over the fixture tables so that it can be executed. A sample keeps the feature its specification counterpart demonstrates and is named after what the query asks, not after what an engine is expected to answer. Where the fixtures lack the data a specification sample reads, the sample builds it from fixture columns, literals and inline `rows`: the employee hierarchy (`employee_manager_id`) stands in for referrals, a tenant for a user with several orders, `if` over the tenant for a user's time zone, inline rows for cash flows and imported text, and text with accents, `ß` or full-width letters is a literal joined to a fixture name. Outer joins make the nullable fields the fixtures do not store.

Samples 123 to 151 of the specification are the PureQL queries [Projector](https://github.com/Elegant-soft/Projector) stores for its dashboards (`GET api/v1/queries`), and the `Dashboards` folder holds them: 29 samples, one per stored query, rewritten in the same way. They combine many features in one query, as a real dashboard does: filtered aggregates side by side, `if`-guarded ratios, computed date buckets, self-joins through aliases, and chains of grouped subqueries joined back to an entity. Each keeps the shape its Projector query demonstrates over the fixture tables. Order items stand in for order lines, users for customers, employees (through `employee_user_id`) for sales reps and managers, tenants for channels, and `user_score` for satisfaction. A query engine's tests can take a sample instead of rebuilding the AST inline, and a serializer, validator or pretty-printer gets a corpus of real queries for free.

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

`PureQL.CSharp.Model.PureQLQuery` is a C# 15 `union` of the main query variants (`MainPlainQueryWithoutPagination`, `MainPlainQueryWithPagination`, `MainGroupedQueryWithoutPagination`, `MainGroupedQueryWithPagination`, `MainTotalQuery`, and the combined `MainCombinedQueryUnionAll`, `MainCombinedQueryUnion`, `MainCombinedQueryIntersect`, `MainCombinedQueryExcept`), so a sample cannot *be* a query the way `UsersTable` is an `ITable`. Instead each sample is a sealed record with a parameterless constructor that exposes its query through `Value`:

```csharp
public sealed record FilterOrderAndPageQuery
{
    public PureQLQuery Value =>
        new MainPlainQueryWithPagination(
            from: new FromEntity(...),
            where: new AndRow([...]),
            select: [new SelectItemProjection("order_id", new FieldUuid(...)), ...],
            orderBy: [...],
            pagination: new Pagination(take: 20)
        );
}
```

`Value` builds a fresh `PureQLQuery` on every read. Two instances of the same sample always produce the same PureQL document. Every sample's `Value` is a `PureQLQuery`, whatever its variant, as engines and the serializer take one; `Value.Value` is the variant record:

```csharp
PureQLQuery query = new FilterOrderAndPageQuery().Value;

if (query.Value is MainPlainQueryWithPagination page)
{
    // page.Pagination, page.OrderBy, ...
}
```

The query is built from the model's records, each of which converts implicitly to every union that lists it. Every operator is the record of its exact declared type in its context (`AddIntegerNullableRow` in `where`, `SumDecimalGroup` in a grouped `select`), and every leaf the record of its type (`FieldDecimal`, `LiteralInteger`, `KeyUuid`, `LiteralStringNullable()` for a typed null).

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

A `decimal` column is read through a `FieldDecimal`, a nullable one through a `FieldDecimalNullable`, and so on for every type. A field is nullable where the fixture column holds a NULL (`users.user_score`, `employees.employee_manager_id`) and where the specification makes it nullable after an outer join. No fixture column is an `integer`, so the samples get their integers from `round`, `floor` and `ceiling`, `count`, `dateDiffDays`, `integerDivide`, `modulo` and integer literals.

## Expected results

145 of the 151 samples also carry a `Result`: the table the query returns, hard-coded in the same style as the query itself.

```csharp
IStoredTableDataSet expected = new GroupByCountQuery().Result;
```

The result is an `IStoredTableDataSet` (`StoredTableDataSet` from `Pure.RelationalSchema.Storage.Samples`). Its table schema has an empty name, no indexes and one column per `select` item, named by the item's alias and typed by its PureQL type: `integer` → `LongColumnType`, `decimal` → `DoubleColumnType`, `string` → `StringColumnType`, `boolean` → `BoolColumnType`, `date` → `DateColumnType`, `time` → `TimeColumnType`, `datetime` → `DateTimeColumnType`, `uuid` → `UuidColumnType`. When a column's name and type match a `Pure.RelationalSchema.Samples` column, that column is used as is (`new OrderTotalColumn()`). Rows are `Row`s of `InvariantCell`s over the typed `Pure.Primitives` values, NULL being `EmptyCell`; a `datetime` cell is a `UtcDateTimeCell`, the same text ending in `Z`. The property's summary names the stored data sets the result is computed over.

### How the results were computed

The results come from the semantics of the specification, not from any PureQL engine, so an engine can be tested against them. Every query was translated to SQL and run on PostgreSQL 17 over exactly the fixture rows of `Pure.RelationalSchema.Storage.Samples`. Where SQL and the specification disagree, the translation spells the specification's rule out:

- **Equality:** `equal` and `notEqual` are `IS NOT DISTINCT FROM` and `IS DISTINCT FROM`, so `null` equals `null` in every clause, `join.on` included. An ordering comparison against `null` is `false`.
- **Numbers:** `decimal` is exact (`numeric`). Each `decimal` answer is rounded half away from zero to the decimal precision, the most decimal places that fit in 28 digits and at most 28, as the specification rounds every `decimal` result, and then to the nearest `double`. An engine computing in `double` should compare with a tolerance. `round` rounds half away from zero unless its `mode` is `halfEven`, `integerDivide` truncates toward zero and `modulo` takes the sign of its left operand. An operator over more than two values is a left fold, each step rounded on its own; an aggregate is computed from exact values and rounded once.
- **Exact functions:** `power`, `sqrt`, `exp`, `ln`, `log10` and `log` are computed in `numeric` at 50 decimal places and checked against Python `decimal`; trigonometry, which PostgreSQL has only in `double`, is computed with Python `decimal` at 120 digits and written into the SQL. So are the full case mappings of `upper` / `lower`, `foldCase` (`str.casefold`) and `removeDiacritics` (`unicodedata`), which PostgreSQL 17 does not provide; `normalize` is PostgreSQL's, checked with `unicodedata`. Each such result says so in its summary.
- **Strings** compare and sort by code point (the `C` collation).
- **Aggregates:** `count`, `countDistinct` and `sum` over no rows are `0`, `concatAll` is the empty string, `any` is `false` and `all` is `true`. Every aggregate but `first` and `last` skips NULLs. An aggregate next to fields in a plain query is computed over all rows and repeated on every row. A total query (`aggregate`) returns exactly one row.
- **Statistics:** percentiles and medians are interpolated in exact `numeric` at position `fraction × (n − 1)` (PostgreSQL's `percentile_cont` is `double`), `mode` takes the least of the most frequent values, variances and covariances come from exact sums and the standard deviations are `sqrt` of the exact variance. A mean of dates, times or datetimes is rounded to the day or the microsecond, a value exactly halfway going to the later one; every such result lands on a whole second.
- **`distinctBy`:** of the rows equal on the listed columns, compared with `equal`, the first in `orderBy` order is kept (`row_number()` over a partition by those columns, ordered as `orderBy`).
- **Sorting:** NULLs sort first ascending and last descending. Uuids sort by their bytes read as unsigned big-endian, and booleans `false` before `true`.
- **Dates and times:** `dateDiffDays` and the `…DiffSeconds` operators are `left − right`, and `timeAddSeconds` wraps around midnight. Adding months clamps the day to the end of the month, and `dateDiffMonths` / `datetimeDiffMonths` are translated as its inverse: the largest `n` with `right` plus `n` months not after `left`.
- **Time zones:** local dates and times are read with the tz database of the PostgreSQL 17 image (`AT TIME ZONE`) and checked with Python `zoneinfo`, over 2024 dates of zones whose rules are stable (Asia/Tokyo, America/New_York, Pacific/Pago_Pago, Pacific/Auckland). No value falls in a skipped or repeated local hour, where PostgreSQL and the specification's "first instant at or after" rule could differ.
- **Windows** are SQL window functions with the frame written out (`ROWS` or `RANGE`, the current row always inside); `percentRank` and `cumulativeDistribution` are exact divisions. No result depends on the order of peers.
- **Set operations, inline rows and recursion:** `UNION ALL`, `UNION`, `INTERSECT` and `EXCEPT`; `VALUES`; `WITH RECURSIVE`. A grouped recursive step, which PostgreSQL does not allow in a recursive term, aggregates in a `LATERAL` subquery, checked against exact decimal arithmetic rounded as the specification rounds.
- **Datetimes:** the fixture stores datetimes without an offset, and they are read as UTC. `datetime` literals are in UTC, as the specification requires, and `datetime` cells are written in UTC with `Z`, the one spelling the specification gives a `datetime` result (`2024-06-06T15:00:00Z`).

**Row order:** when the query has an `orderBy`, rows are in that order. No result has rows tied on every sort key. Without an `orderBy`, the rows are a multiset in no particular order. Each result was also checked against a copy of the fixtures loaded in reverse row order, and no result depends on physical row order.

### Samples without a result

6 samples have no `Result`, because they hold a parameter and the model has no way to bind one. Each keeps its parameter because the specification sample it mirrors is about one:

`Parameters.ParametersInWhereAndSelectQuery`, `Where.DecimalRangesAgainstLiteralsParameterAndFieldQuery`, `Where.InListParameterAndNotInListLiteralQuery`, `Where.OptionalListFiltersQuery`, `Pagination.FilterOrderAndPageQuery`, `GroupBy.HavingBooleanLogicQuery`

Every other parameter of the specification samples is replaced by a literal (a date standing for today, a zone, a frame size, an `offset`, a `maxDepth`, a regular expression), and so is every parameter of the Projector queries, which filter by a date or a name rather than demonstrate parameters.

## Catalogue

`namespace PureQL.CSharp.Model.Samples.Queries.<Folder>`

| Spec | Folder | Sample | Exercises |
|---|---|---|---|
| 001 | `Select` | `UserNameColumnQuery` | One field from one entity |
| 002 | `Select` | `FieldsThroughFromAliasQuery` | Fields referenced through a `from` alias |
| 003 | `Select` | `LiteralOfEveryTypeNextToFieldQuery` | A literal of every type as a column |
| 004 | `Parameters` | `ParametersInWhereAndSelectQuery` | Parameters in `where` and as a column |
| 005 | `Where` | `WhereStatusEqualsLiteralQuery` | `equal` with a string literal |
| 006 | `Where` | `WhereBooleanFieldQuery` | A boolean field as the condition |
| 007 | `Where` | `WhereNotEqualAndNotQuery` | `notEqual` and `not` |
| 008 | `Where` | `WhereNestedAndOrQuery` | Nested `and` / `or` |
| 009 | `Where` | `DecimalRangesAgainstLiteralsParameterAndFieldQuery` | `decimal` ranges against literals, a parameter and a nullable field |
| 010 | `Where` | `StringDateAndTimeRangesQuery` | Ranges on `string`, `date` and `time` |
| 011 | `Where` | `InListParameterAndNotInListLiteralQuery` | `in` a list parameter and a list literal |
| 012 | `Where` | `OptionalListFiltersQuery` | List parameters behind `boolean` parameters that switch each filter off, next to list literals of one and two values |
| 013 | `OrderBy` | `OrderByMultipleKeysQuery` | Several keys and directions |
| 014 | `Select` | `DistinctOrderStatusQuery` | `distinctBy` listing every column |
| 015 | `Select` | `LatestOrderPerUserQuery` | `distinctBy` keeping the first row per key in `orderBy` order |
| 016 | `Pagination` | `FilterOrderAndPageQuery` | Filter, order and page |
| 017 | `Pagination` | `OrdersAfterTheLatestQuery` | `pagination` with `skip` alone: every order of a tenant but the latest |
| 018 | `Expressions` | `IntegerArithmeticQuery` | `add` / `subtract` / `multiply` staying `integer` |
| 019 | `Expressions` | `DecimalWideningAndDivideQuery` | `integer` widening to `decimal`; `divide` |
| 020 | `Numbers` | `OperatorsOverAListQuery` | `multiply`, `subtract`, `divide` and `add` over three values, one step at a time; a division in two steps next to one by a product; `sum` and `average` over every row |
| 021 | `Expressions` | `RoundingAndIntegerDivisionQuery` | `floor` / `ceiling` / `round`, `round` with digits, `integerDivide`, `modulo` |
| 022 | `Numbers` | `WholeNumbersQuery` | `round` / `floor` / `ceiling` / `truncate` of decimals, `round` of an `integer` to tens with `digits`, `integerDivide` |
| 023 | `Numbers` | `AgeBandsQuery` | Rounding an `integer` keeps an `integer`: age bands from `truncate` with `digits` as the group key, the end of each band, a count to the nearest ten |
| 024 | `Numbers` | `DecimalRemaindersQuery` | `integerDivide` and `modulo` of decimals: a fractional part, a multiple of ten cents, whole fifties and the rest, seconds split into hours and a remainder |
| 025 | `Numbers` | `RoundHalfToEvenQuery` | `round` with `"mode": "halfEven"`: a tax under both rules and where they differ, a whole price, a line total in tens |
| 026 | `Numbers` | `AbsSignAndTruncateQuery` | `abs` / `sign` / `truncate`: `abs` keeping `integer`, `sign` lifted over a nullable field, `truncate` with and without `digits`, the largest deviations in `where` and `orderBy` |
| 027 | `Numbers` | `GreatestAndLeastQuery` | `greatest` / `least`: a minimum charge, a clamp, results lifted over nullable fields or defaulted with `coalesce`, an `integer` result, the latest activity in `where` |
| 028 | `Numbers` | `PowersAndLogarithmsQuery` | `power` / `sqrt` / `exp` / `ln` / `log10` / `log`: compound growth, a growth rate from a ratio guarded by `nullIfEqual`, the digits before the point as an `integer`, a base-2 logarithm, large orders in `where` |
| 029 | `Numbers` | `HourAsAnAngleQuery` | `sin`, `cos`, `atan2` and `pi`: the hour of an order as an angle and as a point on a circle, and the angle back in degrees |
| 030 | `Expressions` | `ConcatQuery` | `concat` |
| 031 | `Strings` | `StringLengthQuery` | `length` in code points: as a column, lifted over a nullable field, in `where` and `orderBy` |
| 032 | `Strings` | `UpperLowerQuery` | `upper` / `lower`: columns, lifted over a nullable field, a full case mapping (ß to SS), and a `where` that ignores case |
| 033 | `Strings` | `TrimQuery` | `trim` / `trimStart` / `trimEnd`: one or several `removables`, a nullable field, and a `where` that skips blank names |
| 034 | `Strings` | `SubstringQuery` | `substring`: a prefix, the rest of a string, a `length` over a nullable field, and a prefix filter in `where` |
| 035 | `Strings` | `StringMatchingQuery` | `contains` / `startsWith` / `endsWith`: a search that ignores case in `where`, and matches over nullable strings as non-null `boolean` columns |
| 036 | `Strings` | `FoldCaseQuery` | `foldCase`: a search, a prefix match and a regular expression that ignore case, an equality over a nullable field, folded keys as columns, and a sort without regard to case |
| 037 | `Strings` | `NormalizeQuery` | `normalize`: a match on a key that ignores case and width (`foldCase`, then `"NFKC"`), a column in `"NFC"`, a check that a value is stored composed, the length of the `"NFD"` form, lifted over a nullable field |
| 038 | `Strings` | `RemoveDiacriticsQuery` | `removeDiacritics`: a search that ignores case and accents, a column without accents, a check for accents, a key that also ignores ligatures, and a sort without regard to either |
| 039 | `Strings` | `ReplacePositionSplitPartQuery` | `replace` / `position` / `splitPart`: parts of a text with a negative `index`, the position of a separator and of the next one, a masked name, letters removed from a nullable field, a `position` filter in `where` |
| 040 | `Strings` | `PaddingAndJoiningQuery` | `padStart` / `padEnd` / `repeat` / `reverse` / `concatWith`: names joined while skipping null ones, a nullable number padded with zeros, a dotted column, and a `concatWith` filter in `where` |
| 041 | `Strings` | `RegularExpressionsQuery` | `matchesRegex` / `extractRegex` / `replaceRegex`: a well-formed name in `where`, a capture group, a match that is null when absent, a nullable field, runs of white space collapsed |
| 042 | `Expressions` | `IfColumnQuery` | `if` as a column, with an `integer` branch widened to `decimal` |
| 043 | `OrderBy` | `OrderByComputedExpressionQuery` | Ordering by a computed expression |
| 044 | `OrderBy` | `OrderBySelectColumnQuery` | `orderBy` naming `select` columns: a filtered `sum`, a ratio of aggregates and the key, each written once, paged with `take` |
| 045 | `Temporal` | `DateMathQuery` | `dateAddDays` / `dateDiffDays` as columns and in `where` |
| 046 | `Temporal` | `AddMonthsQuery` | `dateAddMonths` / `datetimeAddMonths`: a trial end, years as 12 months, a nullable field after a left join, the last 48 months in `where` |
| 047 | `Temporal` | `AmountsNamedByTheirUnitQuery` | The five operators that add, `{ operand, days }`, `{ operand, months }` and `{ operand, seconds }`: a due date, a year later, a month before, a reminder from hours, a shift end wrapping past midnight, a nullable amount |
| 048 | `Temporal` | `MonthsBetweenQuery` | `dateDiffMonths` / `datetimeDiffMonths`: whole years of membership, months since a nullable shipped order, months since the last login with the time of day counting, members of two years or more in `where` |
| 049 | `Temporal` | `TimeAndDatetimeMathQuery` | `timeAddSeconds` wrapping past midnight, `datetimeDiffSeconds` |
| 050 | `Temporal` | `DatetimeUtcLiteralsQuery` | `datetime` literals in UTC with fractional seconds |
| 051 | `TimeZones` | `LocalBusinessDayQuery` | `asDate` and `asTime` with a `zone`: revenue by the local business day in Asia/Tokyo, orders after 18:00 local time, and orders whose UTC day is another day |
| 052 | `Temporal` | `DatePartsQuery` | Parts of a `date`, a `datetime` and a `time` as `integer` columns, `week` / `weekYear` of a last day of December, lifted over a nullable field, weekend sign-ups in `where` |
| 053 | `TimeZones` | `LocalWeekdayAndHourQuery` | Parts with a `zone`: orders by the local `dayOfWeek` and `hour` in America/New_York, with weekend and business-hour flags |
| 054 | `TimeZones` | `LocalMonthsQuery` | `startOf…` with a `zone`: revenue by local month since the start of the local year, the local days with orders, and the revenue of the current local week |
| 055 | `TimeZones` | `LocalTimesAsInstantsQuery` | `asDatetime` and `datetimeFromParts` with a `zone` computed per user: the orders within local days, the 18:00 local cutoff as an instant, and whether an order ships today |
| 056 | `TimeZones` | `MonthsInALocalCalendarQuery` | `datetimeAddMonths` and `datetimeDiffMonths` with a `zone` from a subquery column: whole months and years of membership, the next renewal and anniversary at the same local time |
| 057 | `Temporal` | `DatesFromPartsQuery` | `dateFromParts`, `timeFromParts` and `datetimeFromParts`: the start of a fiscal year, the middle of a month, a cut-off time, and an instant from a `date` and a `time`, lifted over a nullable one |
| 058 | `Conversions` | `ConversionsQuery` | `asString` / `asDate` / `asTime` / `asDatetime`: a label from a date and a decimal, a nullable date as text, the day and time of a `datetime`, midnight of a `date`, a `datetime` filtered by its day |
| 059 | `Conversions` | `ReadingTextQuery` | `as…` of a string and `tryAs…`: text read as numbers, `null` on other text, a default through `coalesce` after `trim`, a nullable string, a date sent as text in `where` |
| 060 | `Conversions` | `ExplicitWideningQuery` | `asDecimal` of an `integer` and `asNullable` where no position widens: a `decimal` column, `previous` with a `decimal` default, `nullIfEqual` keeping the widened type |
| 061 | `Nulls` | `NullableColumnQuery` | A nullable field in a nullable column |
| 062 | `Nulls` | `TypedNullLiteralsQuery` | Typed null literals, `integer?` widened to `decimal?` |
| 063 | `Nulls` | `CoalesceQuery` | `coalesce`, non-null and nullable |
| 064 | `Nulls` | `NullIfEqualQuery` | `nullIfEqual`: a sentinel read as `null`, a division guarded against a zero divisor, its default through `coalesce`, the same guard in `where` |
| 065 | `Nulls` | `NullChecksQuery` | `equal` / `notEqual` against a typed null |
| 066 | `Nulls` | `NullableBooleanConditionsQuery` | A nullable boolean through `equal(…, false)` and `coalesce` |
| 067 | `Nulls` | `ComparingNullableValuesQuery` | Ordering comparisons of two nullable values: `greaterThanOrEqual` of two `null`s is `true` where `greaterThan` is `false`, and a bound that is a typed null |
| 068 | `Nulls` | `LiftedOperatorsQuery` | Lifted `add`, `dateAddDays` and `multiply` |
| 069 | `Nulls` | `LiftedOverEveryOperandQuery` | Operators lifted over the operands named by their role: a nullable `exponent`, `digits`, `count`, `pattern` and `length` |
| 070 | `Joins` | `InnerJoinQuery` | Inner join on a key |
| 071 | `Joins` | `LeftJoinNullableFieldsQuery` | Left join: nullable fields, typed null, `coalesce` |
| 072 | `Joins` | `SelfJoinThroughAliasQuery` | Self-join through a join alias |
| 073 | `Joins` | `JoinOnNullableKeyQuery` | Join on a key nullable on both sides |
| 074 | `Joins` | `RightJoinQuery` | Right join: the left side becomes nullable |
| 075 | `Joins` | `FullJoinQuery` | Full join: both sides coalesced |
| 076 | `Joins` | `MultipleJoinsQuery` | Several joins of different kinds |
| 077 | `Joins` | `OuterJoinChainQuery` | Chained left joins; `null` equals `null` in the second `on` |
| 078 | `Aggregates` | `AggregatesOverAllRowsQuery` | A total query: `count` / `sum` / `min` / `max` / `average` / `any` / `all` over all rows |
| 079 | `Aggregates` | `AggregateWithPredicateNextToFieldsQuery` | Filtered aggregates next to row columns |
| 080 | `Aggregates` | `ShareOfTotalQuery` | A row value divided by an aggregate |
| 081 | `GroupBy` | `GroupByCountQuery` | One key and `count` |
| 082 | `GroupBy` | `GroupByMultipleKeysQuery` | Two keys |
| 083 | `GroupBy` | `ComputedGroupKeyQuery` | A computed key, selected and ordered by |
| 084 | `GroupBy` | `GroupByMonthQuery` | `startOfMonth` of a `date` as the group key, ordered by it, `startOfYear` in `where`, `startOfHour` over `max` |
| 085 | `GroupBy` | `HavingCountQuery` | `having` on an aggregate |
| 086 | `GroupBy` | `HavingBooleanLogicQuery` | `having` with `and` / `or` / `not` and a parameter |
| 087 | `GroupBy` | `ConditionalAggregatesQuery` | Aggregates with predicates, `sum` over `if`, `any` / `all` |
| 088 | `Aggregates` | `CountDistinctPerStatusQuery` | `countDistinct` per status: with a `predicate`, over a nullable selector, next to `count`, over `"all"` |
| 089 | `Aggregates` | `FirstAndLastPerUserQuery` | `first` / `last` with their own `orderBy`: a `null` value kept, a `predicate` that skips one, a nullable `last` |
| 090 | `Aggregates` | `StatisticsPerStatusQuery` | `median`, `percentileContinuous` / `percentileDiscrete`, `mode` of a `time`, `median` of a `date`, a nullable `median` with a `predicate`, population and sample deviations |
| 091 | `Aggregates` | `TemporalAveragesPerUserQuery` | `average`, `median` and `percentileContinuous` of a `date`, a `datetime` and a `time`, a mean halfway between two days going to the later one, a nullable `average` |
| 092 | `Aggregates` | `CovarianceAndCorrelationPerTenantQuery` | `correlation`, `covariancePopulation` and `covarianceSample` of price and quantity, a `correlation` with a `predicate`, the covariance of a column with itself |
| 093 | `Aggregates` | `DailyRevenueTrendQuery` | `regressionSlope` / `regressionIntercept` / `regressionRSquared` over a grouped subquery, and a forecast from the line |
| 094 | `Aggregates` | `ConcatAllPerTenantQuery` | `concatAll` with its own `orderBy`, `distinct`, a nullable selector whose nulls are skipped, and a `predicate` |
| 095 | `GroupBy` | `GroupKeyUsageQuery` | Plain, nullable and computed keys in `select`, `having` and `orderBy` |
| 096 | `GroupBy` | `NullableGroupKeyAndAggregatesQuery` | A nullable key; `average` of `datetime` |
| 097 | `GroupBy` | `DateMathEverywhereQuery` | Date math per row and over aggregates |
| 098 | `GroupBy` | `IntegerAndDecimalInGroupsQuery` | `integer` / `decimal` rules in a grouped query |
| 099 | `GroupBy` | `GroupedRevenueQuery` | `count` over all rows, `having` and `orderBy` |
| 100 | `Subqueries` | `FromSubqueryQuery` | Reading from a subquery |
| 101 | `Subqueries` | `InSubqueryColumnQuery` | `in` over a subquery column |
| 102 | `Subqueries` | `SubqueryChainQuery` | A subquery reading another, joined by the main query |
| 103 | `Subqueries` | `LeftJoinSubqueryQuery` | Left join to a subquery |
| 104 | `Subqueries` | `SubqueryPipelineQuery` | Three subqueries feeding a joined, filtered, ordered, paged query |
| 105 | `SetOperations` | `ActivityTimelineQuery` | `unionAll` as a subquery: orders, logins and sign-ups as one timeline, a column made nullable by one input, ordered and paged by the main query |
| 106 | `SetOperations` | `UsersToContactQuery` | A combined main query: an `intersect` inside a `union` inside an `except` |
| 107 | `Subqueries` | `InlineStatusRowsQuery` | Inline `rows` with a `null` cell that makes a column nullable, left joined to orders to label and sort them |
| 108 | `Windows` | `PartitionTotalsQuery` | Aggregates over a partition: the user's total, count and largest order, a share guarded with `nullIfEqual`, a second partition key, a total over all rows without `over` |
| 109 | `Windows` | `RunningTotalsQuery` | Frames from the start of the partition: a running total and count, a filtered running total, the largest and average so far, the total still to come |
| 110 | `Windows` | `MovingAveragesQuery` | Frames of a number of rows: an average of the last rows, a row with its neighbours, the rows after, a comparison with a moving average |
| 111 | `Windows` | `RangeFramesQuery` | Ranges of values: `{ days }`, `{ months }`, `"all"`, `{ seconds }` on both sides, and a `decimal` range |
| 112 | `Windows` | `RankingQuery` | `rowNumber`, `rank` and `denseRank` within a user and over all rows, `percentRank`, `cumulativeDistribution`, quartiles with `ntile` |
| 113 | `Windows` | `PreviousAndNextOrderQuery` | `previous` / `next`: the change with and without a `default`, days since the previous order, an `offset`, a first-order flag, a nullable operand with a default |
| 114 | `Windows` | `TopOrdersPerTenantQuery` | The largest orders of each tenant: `rowNumber` in a subquery, filtered by the query that reads it |
| 115 | `Windows` | `RunningTotalOfDailySumsQuery` | Windows over grouped rows: cumulative and week-to-date totals of daily sums, a three-day average, the change from the previous day, a share and a rank in the week |
| 116 | `Windows` | `RowExpressionsInWindowsQuery` | Row expressions in windows with a zone computed per row: partitions by a local month, a range over the local day, `previous` / `next` of a computed date with a computed default |
| 117 | `Recursive` | `DescendantsQuery` | A recursive subquery walking down: everyone below an employee, with the depth as a computed column and a `maxDepth` |
| 118 | `Recursive` | `AncestorsQuery` | A recursive subquery walking up: `next` reads the subquery in `from` and joins each row's manager, up to the top of the chain |
| 119 | `Recursive` | `TreeInReadingOrderQuery` | A tree in reading order: a path extended with `concat`, a label indented with `repeat`, sorted by the path |
| 120 | `Recursive` | `TreeRollUpQuery` | Amounts rolled up a tree: every employee paired with everyone below by a recursive subquery, joined to orders and grouped by the ancestor |
| 121 | `Recursive` | `CalendarOfDaysQuery` | A row for each day between two dates, seeded from inline rows and stopped by `where`, left joined to the orders of each day |
| 122 | `Recursive` | `RateOfReturnByIterationQuery` | A grouped `next`: the internal rate of return of each investment by Newton's method, stopped in `having`, the last step read with `distinctBy` |

### Dashboards

`namespace PureQL.CSharp.Model.Samples.Queries.Dashboards`, specification samples 123 to 151, one per query Projector stores, by its id:

| Spec | Projector query | Sample | Exercises |
|---|---|---|---|
| 123 | `bd6f078a-0a03-4561-a6d7-83c06345a6f5` | `RecentOrdersPerActiveUserQuery` | `and` of a boolean field and a `datetime` range; ordered by `count`, then the key |
| 124 | `2fac3de8-6ed5-406d-bf90-055674de7d4e` | `BilledWithFeePerEmployeeQuery` | A join chain, a date range, `sum` of `add`, ordered by it |
| 125 | `2fb00633-3184-4faf-acae-699795e9b3f6` | `RevenuePerUserNameQuery` | A key from a joined entity, ordered by an aggregate descending |
| 126 | `934b5726-9aa7-48de-81da-d2430d2f9403` | `ItemValueShareOfOrderTotalPerProductQuery` | A ratio of two `sum`s |
| 127 | `47412e00-e311-49ea-b053-ac6195acb437` | `ItemValuePerUserAndProductQuery` | A `concat` group key, a `greaterThan` filter |
| 128 | `b51b0f7f-907d-4058-b1ba-d6a403e990bf` | `OrderStageExcludingUserQuery` | `and` of `in` and `notEqual`; a nested `if` key over `equal` |
| 129 | `528d489c-032a-488a-96a0-fab37bd097df` | `RevenuePerTeamLeadPerPeriodQuery` | The same entity joined twice through aliases; a computed period key |
| 130 | `6a03aeba-fa02-4b56-91c9-5a4a5daab537` | `LoginStatsPerTenantQuery` | `average` of `datetimeDiffSeconds`, a coalesced nullable `average`, a guarded share |
| 131 | `0b617d43-461a-408c-a432-15aef9fcbb60` | `UserFunnelStageQuery` | A left joined subquery; a nested `if` key over `not`, `coalesce` and `lessThan` |
| 132 | `fc47ae13-4219-44c4-aed3-90cc9b6d6445` | `DailyRevenuePerTenantQuery` | A `sum` with a predicate per tenant next to the total |
| 133 | `89fcdced-5d53-43b0-b2ee-eb8197a9d091` | `OrderTotalsPerStatusAndTenantQuery` | Filtered `sum`s, the total, `average` of `dateDiffDays`, `count` |
| 134 | `8b1efefc-0a15-4b46-a451-21dc561c5da8` | `PlacedVersusShippedPerDayQuery` | Reading from a subquery left joined to another; `coalesce` and `subtract` |
| 135 | `dfc68e29-087b-44f3-b744-b96527b93910` | `WeeklyUnitsPerProductQuery` | A computed week key, `dateAddDays` over the key, filtered `sum`s |
| 136 | `cc714d72-0023-4ca6-a7bb-8f336d347671` | `RepeatBuyerOrderAndItemProfileQuery` | Two grouped subqueries inner joined, filtered on a subquery column |
| 137 | `d363779f-fa97-4f23-bcbe-57ed8752a372` | `ScoreByProductPerOrderDayQuery` | An `integer` computed key; `average`s with `equal` / `notEqual` predicates |
| 138 | `b21432b3-e413-40e6-98ea-117cc50f5bce` | `DirectReportsPerManagerQuery` | `notEqual` on a nullable field; `min` of `time` and `date` |
| 139 | `dfddb067-ef61-48a8-9945-f22b8583c4d8` | `DailyInStockRevenueShareQuery` | `subtract`, `divide` and `multiply` over aggregates |
| 140 | `409f5283-7423-4d18-99a2-e1e8ff0be846` | `LineFlagsPerUserAndStatusQuery` | Two `string` keys; `count`s with `and` / `not` / `lessThan` / boolean predicates |
| 141 | `be439994-1e21-4ce5-a46b-416302754f08` | `ItemRevenueToOrderTotalPerEmployeeQuery` | An entity joined to two grouped subqueries, an `in`-list filter |
| 142 | `c87841bc-4433-4e7c-bcb8-91d62949bc44` | `RevenuePerEmployeeAndAgeQuery` | Four group keys; an aggregate divided by a `decimal` key |
| 143 | `5579e839-e1dd-44c9-bbd7-2a536b82db0f` | `InStockItemValueVersusOrderedPerUserQuery` | A joined subquery filtered by `and(equal, in)`; a variance percentage |
| 144 | `e40e9165-3e2d-4686-b410-ec3c8cebd34c` | `OrdersPerWeekdayQuery` | A six-level `if` key over `modulo` of `dateDiffDays`; a guarded share |
| 145 | `bb020cef-6f52-4db0-a66c-4059f01f8690` | `ShippedSharePerUserAndTenantQuery` | `if`-guarded ratios of filtered `count`s |
| 146 | `e14b189d-90cc-4acb-b1e4-dc61998cbb03` | `UnbilledOrderAgingQuery` | `dateDiffDays` from a literal, `if` buckets over it, ordered by two expressions; paged |
| 147 | `6e0f2c5a-21ca-4eca-bf38-542e1e6a3d93` | `ProductSalesPerformanceQuery` | A guarded percentage, `average` of `datetimeDiffSeconds` and `max` of `dateDiffDays` in a subquery; ratios of its columns |
| 148 | `59f44177-b983-46a8-901f-b9136f5c50c9` | `UserSalesSummaryQuery` | One inner and two left joined subqueries; a guard comparing `coalesce` with `integer` 0 |
| 149 | `ba64a130-7551-4c81-89fe-20e6c9267303` | `UserAccountOverviewQuery` | Five subqueries, one reading another; inner and left joins; `max`; paged |
| 150 | `61b93c09-1e41-4d0b-9d4f-756d507cfbd5` | `EmployeeScorecardQuery` | A `from` alias self-joined through a join alias; three subqueries with nested `and` ranges |
| 151 | `e2d37261-7318-4abc-809b-41d66a47a20f` | `LineRestockSuggestionQuery` | A left join on two keys; arithmetic in `if` branches; paged |

### Data sets

A sample's entities name the schema they read from. The stored data sets that hold those schemas are:

| Entity prefix | Stored data set |
|---|---|
| `schema_with_foreign_keys.` | `SchemaDataSetWithForeignKeys` |
| `audit.` | `AuditSchemaDataSet` |

Every result is computed over `[new SchemaDataSetWithForeignKeys(), new AuditSchemaDataSet()]`.

## Dependencies

- [`PureQL.CSharp.Model` 0.1.0-preview.15.0.0](https://github.com/kudima03/PureQL.CSharp.Model/tree/0.1.0-preview.15.0.0): the query AST every sample builds
- [`Pure.RelationalSchema.Samples` 0.1.0-preview.1.2.0](https://github.com/kudima03/Pure.RelationalSchema.Samples/tree/0.1.0-preview.2.0.1): the schema, table and column fixtures that entity, field and result column names come from
- [`Pure.RelationalSchema.Storage.Samples` 0.1.0-preview.1.3.0](https://github.com/kudima03/Pure.RelationalSchema.Storage.Samples/tree/0.1.0-preview.2.0.1): the fixture rows the results are computed over, and `StoredTableDataSet`, `InvariantCell` and `EmptyCell`, which the results are built from
- [`Pure.RelationalSchema` 2.2.0](https://github.com/kudima03/Pure.RelationalSchema/tree/2.2.0): `Table`, `Column` and the column types of a result's schema
- [`Pure.RelationalSchema.Storage` 0.1.0-preview.8.1.0](https://github.com/kudima03/Pure.RelationalSchema.Storage/tree/0.1.0-preview.8.1.0): `Row`
- [`Pure.RelationalSchema.HashCodes` 3.4.0](https://github.com/kudima03/Pure.RelationalSchema.HashCodes/tree/3.4.0): `ColumnHash`, which keys a row's cells
- [`Pure.Collections.Generic` 0.1.0-preview.3.1.0](https://github.com/kudima03/Pure.Collections.Generic/tree/0.1.0-preview.3.1.0): the hash-keyed dictionary behind `IRow.Cells`
- [`Pure.Primitives` 3.8.0](https://github.com/kudima03/Pure.Primitives/tree/3.8.0): `DotString`, the `schema.table` separator, and the typed values a result's cells wrap
- [`Pure.Primitives.String.Operations` 1.8.0](https://github.com/kudima03/Pure.Primitives.String.Operations/tree/1.8.0): `JoinedString`, which composes the entity name

No PureQL engine is referenced. Engines are what these samples test.

## Target Frameworks

- .NET 11

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
