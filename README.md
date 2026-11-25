# PureQL.CSharp.Model.Samples

Named, predefined PureQL queries — one sealed record per query — written against the stored data sets of `Pure.RelationalSchema.Storage.Samples`.

[![.NET build & test](https://github.com/kudima03/PureQL.CSharp.Model.Samples/actions/workflows/build-and-test.yml/badge.svg?branch=main)](https://github.com/kudima03/PureQL.CSharp.Model.Samples/actions/workflows/build-and-test.yml)
[![Build and Deploy](https://github.com/kudima03/PureQL.CSharp.Model.Samples/actions/workflows/publish-nuget.yml/badge.svg?branch=main)](https://github.com/kudima03/PureQL.CSharp.Model.Samples/actions/workflows/publish-nuget.yml)
[![NuGet](https://img.shields.io/nuget/v/PureQL.CSharp.Model.Samples)](https://www.nuget.org/packages/PureQL.CSharp.Model.Samples)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

## Overview

`PureQL.CSharp.Model.Samples` provides a fixed, deterministic catalogue of `PureQL.CSharp.Model` `PureQLQuery` instances for PureQL specification [`0.1.0-preview.3.0.0`](https://github.com/kudima03/PureQL-Specification/tree/0.1.0-preview.3.0.0) and the samples its `main` branch has added since, at [`75f1d7c`](https://github.com/kudima03/PureQL-Specification/tree/75f1d7c). Every query reads from the query-grade fixtures of [`Pure.RelationalSchema.Storage.Samples`](https://github.com/kudima03/Pure.RelationalSchema.Storage.Samples): six users, six orders, four products, four order items and four employees in the `schema_with_foreign_keys` schema, and four logins in the `audit` schema. Nothing is random, nothing is generated at run time, nothing reads the clock or the current culture.

The catalogue mirrors the specification's [`samples/valid`](https://github.com/kudima03/PureQL-Specification/tree/75f1d7c/samples/valid): each of its 154 `.json` samples and 232 of its 234 `.jsonc` tests appears here once (386 samples; the two left out are listed under "Samples not mirrored"), rewritten over the fixture tables so that it can be executed. A sample keeps the feature its specification counterpart demonstrates and is named after what the query asks, not after what an engine is expected to answer. Where the fixtures lack the data a specification sample reads, the sample builds it from fixture columns, literals and inline `rows`: the employee hierarchy (`employee_manager_id`) stands in for referrals, a tenant for a user with several orders, `if` over the tenant for a user's time zone, inline rows for cash flows and imported text, and text with accents, `ß` or full-width letters is a literal joined to a fixture name. Outer joins make the nullable fields the fixtures do not store.

Samples 126 to 154 of the specification are the PureQL queries [Projector](https://github.com/Elegant-soft/Projector) stores for its dashboards (`GET api/v1/queries`), and the `Dashboards` folder holds them: 29 samples, one per stored query, rewritten in the same way. They combine many features in one query, as a real dashboard does: filtered aggregates side by side, `if`-guarded ratios, computed date buckets, self-joins through aliases, and chains of grouped subqueries joined back to an entity. Each keeps the shape its Projector query demonstrates over the fixture tables. Order items stand in for order lines, users for customers, employees (through `employee_user_id`) for sales reps and managers, tenants for channels, and `user_score` for satisfaction. A query engine's tests can take a sample instead of rebuilding the AST inline, and a serializer, validator or pretty-printer gets a corpus of real queries for free.

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

367 of the 386 samples also carry a `Result`: the table the query returns, hard-coded in the same style as the query itself.

```csharp
IStoredTableDataSet expected = new GroupByCountQuery().Result;
```

The result is an `IStoredTableDataSet` (`StoredTableDataSet` from `Pure.RelationalSchema.Storage.Samples`). Its table schema has an empty name, no indexes and one column per `select` item, named by the item's alias and typed by its PureQL type: `integer` → `LongColumnType`, `decimal` → `DoubleColumnType`, `string` → `StringColumnType`, `boolean` → `BoolColumnType`, `date` → `DateColumnType`, `time` → `TimeColumnType`, `datetime` → `DateTimeColumnType`, `uuid` → `UuidColumnType`. When a column's name and type match a `Pure.RelationalSchema.Samples` column, that column is used as is (`new OrderTotalColumn()`). Rows are `Row`s of `InvariantCell`s over the typed `Pure.Primitives` values, NULL being `EmptyCell`; a `datetime` cell is a `UtcDateTimeCell`, the same text ending in `Z`. `Pure.Primitives` times and datetimes hold whole seconds, so a `time` or `datetime` value with a fractional second is an `InvariantCell` of its text, spelled as the specification spells it (`09:00:01.5`, `2024-06-01T10:00:00.25Z`), in a column of its PureQL type. The property's summary names the stored data sets the result is computed over.

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

19 samples have no `Result`, because they hold a parameter and the model has no way to bind one. Each keeps its parameter because the specification sample it mirrors is about one:

`Parameters.ParametersInWhereAndSelectQuery`, `Where.DecimalRangesAgainstLiteralsParameterAndFieldQuery`, `Where.InListParameterAndNotInListLiteralQuery`, `Where.OptionalListFiltersQuery`, `Pagination.FilterOrderAndPageQuery`, `GroupBy.HavingBooleanLogicQuery`, `Where.OptionalFiltersSwitchedOffByNullQuery`, `Pagination.PagedOrderHistoryByParametersQuery`, `GroupBy.EveryOperatorSignatureAsGroupKeysQuery`, `Expressions.EveryOperatorSignatureInAPlainSelectQuery`, `GroupBy.EveryOperatorSignatureInAGroupedSelectQuery`, `Aggregates.EveryOperatorSignatureInATotalQuery`, `Parameters.OrdersWithTaxFromAParameterRowQuery`, `Subqueries.InlineRowsOfEveryCellTypeQuery`, `Pagination.PageOfOrdersFromParametersQuery`, `Subqueries.PaginationFormsInSubqueriesQuery`, `Parameters.ListParametersThatMayBeEmptyQuery`, `Subqueries.SetsThatAreEmptyAtExecutionQuery`, `Where.ListLiteralsInEveryClauseQuery`

The four operator-signature samples (`GroupBy.EveryOperatorSignatureAsGroupKeysQuery`, `Expressions.EveryOperatorSignatureInAPlainSelectQuery`, `GroupBy.EveryOperatorSignatureInAGroupedSelectQuery`, `Aggregates.EveryOperatorSignatureInATotalQuery`, specification 184 to 187) keep their parameters because a parameter operand is a signature of its own.

Every other parameter of the specification samples is replaced by a literal (a date standing for today, a zone, a frame size, an `offset`, a `maxDepth`, a regular expression), and so is every parameter of the Projector queries, which filter by a date or a name rather than demonstrate parameters.

## Samples not mirrored

Two `.jsonc` tests of the specification are not mirrored, because what they demonstrate is the spelling of entity and field names, and a sample's entity and field names come from the `Pure.RelationalSchema.Samples` fixtures, which have none of those spellings:

- 166 `names_with_inner_spaces`: names with spaces inside them
- 193 `names_that_are_not_identifiers`: names that are not identifiers

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
| 018 | `Pagination` | `UsersAfterACursorQuery` | Keyset paging: ordering comparisons of a uuid cursor (`greaterThan`), a boolean (`greaterThanOrEqual` against true) and a time (`lessThanOrEqual`), ordered by a `select` column and cut by `take` |
| 019 | `Expressions` | `IntegerArithmeticQuery` | `add` / `subtract` / `multiply` staying `integer` |
| 020 | `Expressions` | `DecimalWideningAndDivideQuery` | `integer` widening to `decimal`; `divide` |
| 021 | `Numbers` | `OperatorsOverAListQuery` | `multiply`, `subtract`, `divide` and `add` over three values, one step at a time; a division in two steps next to one by a product; `sum` and `average` over every row |
| 022 | `Expressions` | `RoundingAndIntegerDivisionQuery` | `floor` / `ceiling` / `round`, `round` with digits, `integerDivide`, `modulo` |
| 023 | `Numbers` | `WholeNumbersQuery` | `round` / `floor` / `ceiling` / `truncate` of decimals, `round` of an `integer` to tens with `digits`, `integerDivide` |
| 024 | `Numbers` | `AgeBandsQuery` | Rounding an `integer` keeps an `integer`: age bands from `truncate` with `digits` as the group key, the end of each band, a count to the nearest ten |
| 025 | `Numbers` | `DecimalRemaindersQuery` | `integerDivide` and `modulo` of decimals: a fractional part, a multiple of ten cents, whole fifties and the rest, seconds split into hours and a remainder |
| 026 | `Numbers` | `RoundHalfToEvenQuery` | `round` with `"mode": "halfEven"`: a tax under both rules and where they differ, a whole price, a line total in tens |
| 027 | `Numbers` | `AbsSignAndTruncateQuery` | `abs` / `sign` / `truncate`: `abs` keeping `integer`, `sign` lifted over a nullable field, `truncate` with and without `digits`, the largest deviations in `where` and `orderBy` |
| 028 | `Numbers` | `GreatestAndLeastQuery` | `greatest` / `least`: a minimum charge, a clamp, results lifted over nullable fields or defaulted with `coalesce`, an `integer` result, the latest activity in `where` |
| 029 | `Numbers` | `PowersAndLogarithmsQuery` | `power` / `sqrt` / `exp` / `ln` / `log10` / `log`: compound growth, a growth rate from a ratio guarded by `nullIfEqual`, the digits before the point as an `integer`, a base-2 logarithm, large orders in `where` |
| 030 | `Numbers` | `HourAsAnAngleQuery` | `sin`, `cos`, `atan2` and `pi`: the hour of an order as an angle and as a point on a circle, and the angle back in degrees |
| 031 | `Expressions` | `ConcatQuery` | `concat` |
| 032 | `Strings` | `StringLengthQuery` | `length` in code points: as a column, lifted over a nullable field, in `where` and `orderBy` |
| 033 | `Strings` | `UpperLowerQuery` | `upper` / `lower`: columns, lifted over a nullable field, a full case mapping (ß to SS), and a `where` that ignores case |
| 034 | `Strings` | `TrimQuery` | `trim` / `trimStart` / `trimEnd`: one or several `removables`, a nullable field, and a `where` that skips blank names |
| 035 | `Strings` | `SubstringQuery` | `substring`: a prefix, the rest of a string, a `length` over a nullable field, and a prefix filter in `where` |
| 036 | `Strings` | `StringMatchingQuery` | `contains` / `startsWith` / `endsWith`: a search that ignores case in `where`, and matches over nullable strings as non-null `boolean` columns |
| 037 | `Strings` | `FoldCaseQuery` | `foldCase`: a search, a prefix match and a regular expression that ignore case, an equality over a nullable field, folded keys as columns, and a sort without regard to case |
| 038 | `Strings` | `NormalizeQuery` | `normalize`: a match on a key that ignores case and width (`foldCase`, then `"NFKC"`), a column in `"NFC"`, a check that a value is stored composed, the length of the `"NFD"` form, lifted over a nullable field |
| 039 | `Strings` | `RemoveDiacriticsQuery` | `removeDiacritics`: a search that ignores case and accents, a column without accents, a check for accents, a key that also ignores ligatures, and a sort without regard to either |
| 040 | `Strings` | `ReplacePositionSplitPartQuery` | `replace` / `position` / `splitPart`: parts of a text with a negative `index`, the position of a separator and of the next one, a masked name, letters removed from a nullable field, a `position` filter in `where` |
| 041 | `Strings` | `PaddingAndJoiningQuery` | `padStart` / `padEnd` / `repeat` / `reverse` / `concatWith`: names joined while skipping null ones, a nullable number padded with zeros, a dotted column, and a `concatWith` filter in `where` |
| 042 | `Strings` | `RegularExpressionsQuery` | `matchesRegex` / `extractRegex` / `replaceRegex`: a well-formed name in `where`, a capture group, a match that is null when absent, a nullable field, runs of white space collapsed |
| 043 | `Expressions` | `IfColumnQuery` | `if` as a column, with an `integer` branch widened to `decimal` |
| 044 | `OrderBy` | `OrderByComputedExpressionQuery` | Ordering by a computed expression |
| 045 | `OrderBy` | `OrderBySelectColumnQuery` | `orderBy` naming `select` columns: a filtered `sum`, a ratio of aggregates and the key, each written once, paged with `take` |
| 046 | `Temporal` | `DateMathQuery` | `dateAddDays` / `dateDiffDays` as columns and in `where` |
| 047 | `Temporal` | `AddMonthsQuery` | `dateAddMonths` / `datetimeAddMonths`: a trial end, years as 12 months, a nullable field after a left join, the last 48 months in `where` |
| 048 | `Temporal` | `AmountsNamedByTheirUnitQuery` | The five operators that add, `{ operand, days }`, `{ operand, months }` and `{ operand, seconds }`: a due date, a year later, a month before, a reminder from hours, a shift end wrapping past midnight, a nullable amount |
| 049 | `Temporal` | `MonthsBetweenQuery` | `dateDiffMonths` / `datetimeDiffMonths`: whole years of membership, months since a nullable shipped order, months since the last login with the time of day counting, members of two years or more in `where` |
| 050 | `Temporal` | `TimeAndDatetimeMathQuery` | `timeAddSeconds` wrapping past midnight, `datetimeDiffSeconds` |
| 051 | `Temporal` | `DatetimeUtcLiteralsQuery` | `datetime` literals in UTC with fractional seconds |
| 052 | `TimeZones` | `LocalBusinessDayQuery` | `asDate` and `asTime` with a `zone`: revenue by the local business day in Asia/Tokyo, orders after 18:00 local time, and orders whose UTC day is another day |
| 053 | `Temporal` | `DatePartsQuery` | Parts of a `date`, a `datetime` and a `time` as `integer` columns, `week` / `weekYear` of a last day of December, lifted over a nullable field, weekend sign-ups in `where` |
| 054 | `TimeZones` | `LocalWeekdayAndHourQuery` | Parts with a `zone`: orders by the local `dayOfWeek` and `hour` in America/New_York, with weekend and business-hour flags |
| 055 | `TimeZones` | `LocalMonthsQuery` | `startOf…` with a `zone`: revenue by local month since the start of the local year, the local days with orders, and the revenue of the current local week |
| 056 | `TimeZones` | `LocalTimesAsInstantsQuery` | `asDatetime` and `datetimeFromParts` with a `zone` computed per user: the orders within local days, the 18:00 local cutoff as an instant, and whether an order ships today |
| 057 | `TimeZones` | `MonthsInALocalCalendarQuery` | `datetimeAddMonths` and `datetimeDiffMonths` with a `zone` from a subquery column: whole months and years of membership, the next renewal and anniversary at the same local time |
| 058 | `TimeZones` | `OperatorsWithAndWithoutAZoneQuery` | Which operators take a `zone`: `second`, `startOfMinute`, `asDate` and `datetimeAddMonths` read the user's zone (from a subquery), while `microsecond`, `startOfSecond` and `asString` do not, `dateAddMonths` / `dateDiffMonths` move a local `date` |
| 059 | `Temporal` | `DatesFromPartsQuery` | `dateFromParts`, `timeFromParts` and `datetimeFromParts`: the start of a fiscal year, the middle of a month, a cut-off time, and an instant from a `date` and a `time`, lifted over a nullable one |
| 060 | `Conversions` | `ConversionsQuery` | `asString` / `asDate` / `asTime` / `asDatetime`: a label from a date and a decimal, a nullable date as text, the day and time of a `datetime`, midnight of a `date`, a `datetime` filtered by its day |
| 061 | `Conversions` | `ReadingTextQuery` | `as…` of a string and `tryAs…`: text read as numbers, `null` on other text, a default through `coalesce` after `trim`, a nullable string, a date sent as text in `where` |
| 062 | `Conversions` | `ExplicitWideningQuery` | `asDecimal` of an `integer` and `asNullable` where no position widens: a `decimal` column, `previous` with a `decimal` default, `nullIfEqual` keeping the widened type |
| 063 | `Nulls` | `NullableColumnQuery` | A nullable field in a nullable column |
| 064 | `Nulls` | `TypedNullLiteralsQuery` | Typed null literals, `integer?` widened to `decimal?` |
| 065 | `Nulls` | `CoalesceQuery` | `coalesce`, non-null and nullable |
| 066 | `Nulls` | `NullIfEqualQuery` | `nullIfEqual`: a sentinel read as `null`, a division guarded against a zero divisor, its default through `coalesce`, the same guard in `where` |
| 067 | `Nulls` | `NullChecksQuery` | `equal` / `notEqual` against a typed null |
| 068 | `Nulls` | `NullableBooleanConditionsQuery` | A nullable boolean through `equal(…, false)` and `coalesce` |
| 069 | `Nulls` | `ComparingNullableValuesQuery` | Ordering comparisons of two nullable values: `greaterThanOrEqual` of two `null`s is `true` where `greaterThan` is `false`, and a bound that is a typed null |
| 070 | `Nulls` | `LiftedOperatorsQuery` | Lifted `add`, `dateAddDays` and `multiply` |
| 071 | `Nulls` | `LiftedOverEveryOperandQuery` | Operators lifted over the operands named by their role: a nullable `exponent`, `digits`, `count`, `pattern` and `length` |
| 072 | `Joins` | `InnerJoinQuery` | Inner join on a key |
| 073 | `Joins` | `LeftJoinNullableFieldsQuery` | Left join: nullable fields, typed null, `coalesce` |
| 074 | `Joins` | `SelfJoinThroughAliasQuery` | Self-join through a join alias |
| 075 | `Joins` | `JoinOnNullableKeyQuery` | Join on a key nullable on both sides |
| 076 | `Joins` | `RightJoinQuery` | Right join: the left side becomes nullable |
| 077 | `Joins` | `FullJoinQuery` | Full join: both sides coalesced |
| 078 | `Joins` | `MultipleJoinsQuery` | Several joins of different kinds |
| 079 | `Joins` | `OuterJoinChainQuery` | Chained left joins; `null` equals `null` in the second `on` |
| 080 | `Joins` | `RightJoinAfterLeftJoinsQuery` | A left join with an extra `notEqual`, a left join through an alias reading the now nullable field, then a right join that makes every earlier field nullable |
| 081 | `Aggregates` | `AggregatesOverAllRowsQuery` | A total query: `count` / `sum` / `min` / `max` / `average` / `any` / `all` over all rows |
| 082 | `Aggregates` | `AggregateWithPredicateNextToFieldsQuery` | Filtered aggregates next to row columns |
| 083 | `Aggregates` | `ShareOfTotalQuery` | A row value divided by an aggregate |
| 084 | `GroupBy` | `GroupByCountQuery` | One key and `count` |
| 085 | `GroupBy` | `GroupByMultipleKeysQuery` | Two keys |
| 086 | `GroupBy` | `ComputedGroupKeyQuery` | A computed key, selected and ordered by |
| 087 | `GroupBy` | `GroupByMonthQuery` | `startOfMonth` of a `date` as the group key, ordered by it, `startOfYear` in `where`, `startOfHour` over `max` |
| 088 | `GroupBy` | `HavingCountQuery` | `having` on an aggregate |
| 089 | `GroupBy` | `HavingBooleanLogicQuery` | `having` with `and` / `or` / `not` and a parameter |
| 090 | `GroupBy` | `ConditionalAggregatesQuery` | Aggregates with predicates, `sum` over `if`, `any` / `all` |
| 091 | `Aggregates` | `CountDistinctPerStatusQuery` | `countDistinct` per status: with a `predicate`, over a nullable selector, next to `count`, over `"all"` |
| 092 | `Aggregates` | `FirstAndLastPerUserQuery` | `first` / `last` with their own `orderBy`: a `null` value kept, a `predicate` that skips one, a nullable `last` |
| 093 | `Aggregates` | `StatisticsPerStatusQuery` | `median`, `percentileContinuous` / `percentileDiscrete`, `mode` of a `time`, `median` of a `date`, a nullable `median` with a `predicate`, population and sample deviations |
| 094 | `Aggregates` | `TemporalAveragesPerUserQuery` | `average`, `median` and `percentileContinuous` of a `date`, a `datetime` and a `time`, a mean halfway between two days going to the later one, a nullable `average` |
| 095 | `Aggregates` | `CovarianceAndCorrelationPerTenantQuery` | `correlation`, `covariancePopulation` and `covarianceSample` of price and quantity, a `correlation` with a `predicate`, the covariance of a column with itself |
| 096 | `Aggregates` | `DailyRevenueTrendQuery` | `regressionSlope` / `regressionIntercept` / `regressionRSquared` over a grouped subquery, and a forecast from the line |
| 097 | `Aggregates` | `ConcatAllPerTenantQuery` | `concatAll` with its own `orderBy`, `distinct`, a nullable selector whose nulls are skipped, and a `predicate` |
| 098 | `GroupBy` | `GroupKeyUsageQuery` | Plain, nullable and computed keys in `select`, `having` and `orderBy` |
| 099 | `GroupBy` | `NullableGroupKeyAndAggregatesQuery` | A nullable key; `average` of `datetime` |
| 100 | `GroupBy` | `DateMathEverywhereQuery` | Date math per row and over aggregates |
| 101 | `GroupBy` | `IntegerAndDecimalInGroupsQuery` | `integer` / `decimal` rules in a grouped query |
| 102 | `GroupBy` | `GroupedRevenueQuery` | `count` over all rows, `having` and `orderBy` |
| 103 | `Subqueries` | `FromSubqueryQuery` | Reading from a subquery |
| 104 | `Subqueries` | `InSubqueryColumnQuery` | `in` over a subquery column |
| 105 | `Subqueries` | `SubqueryChainQuery` | A subquery reading another, joined by the main query |
| 106 | `Subqueries` | `LeftJoinSubqueryQuery` | Left join to a subquery |
| 107 | `Subqueries` | `SubqueryPipelineQuery` | Three subqueries feeding a joined, filtered, ordered, paged query |
| 108 | `SetOperations` | `ActivityTimelineQuery` | `unionAll` as a subquery: orders, logins and sign-ups as one timeline, a column made nullable by one input, ordered and paged by the main query |
| 109 | `SetOperations` | `UsersToContactQuery` | A combined main query: an `intersect` inside a `union` inside an `except` |
| 110 | `Subqueries` | `InlineStatusRowsQuery` | Inline `rows` with a `null` cell that makes a column nullable, left joined to orders to label and sort them |
| 111 | `Windows` | `PartitionTotalsQuery` | Aggregates over a partition: the user's total, count and largest order, a share guarded with `nullIfEqual`, a second partition key, a total over all rows without `over` |
| 112 | `Windows` | `RunningTotalsQuery` | Frames from the start of the partition: a running total and count, a filtered running total, the largest and average so far, the total still to come |
| 113 | `Windows` | `MovingAveragesQuery` | Frames of a number of rows: an average of the last rows, a row with its neighbours, the rows after, a comparison with a moving average |
| 114 | `Windows` | `RangeFramesQuery` | Ranges of values: `{ days }`, `{ months }`, `"all"`, `{ seconds }` on both sides, and a `decimal` range |
| 115 | `Windows` | `RankingQuery` | `rowNumber`, `rank` and `denseRank` within a user and over all rows, `percentRank`, `cumulativeDistribution`, quartiles with `ntile` |
| 116 | `Windows` | `PreviousAndNextOrderQuery` | `previous` / `next`: the change with and without a `default`, days since the previous order, an `offset`, a first-order flag, a nullable operand with a default |
| 117 | `Windows` | `TopOrdersPerTenantQuery` | The largest orders of each tenant: `rowNumber` in a subquery, filtered by the query that reads it |
| 118 | `Windows` | `RunningTotalOfDailySumsQuery` | Windows over grouped rows: cumulative and week-to-date totals of daily sums, a three-day average, the change from the previous day, a share and a rank in the week |
| 119 | `Windows` | `RowExpressionsInWindowsQuery` | Row expressions in windows with a zone computed per row: partitions by a local month, a range over the local day, `previous` / `next` of a computed date with a computed default |
| 120 | `Recursive` | `DescendantsQuery` | A recursive subquery walking down: everyone below an employee, with the depth as a computed column and a `maxDepth` |
| 121 | `Recursive` | `AncestorsQuery` | A recursive subquery walking up: `next` reads the subquery in `from` and joins each row's manager, up to the top of the chain |
| 122 | `Recursive` | `TreeInReadingOrderQuery` | A tree in reading order: a path extended with `concat`, a label indented with `repeat`, sorted by the path |
| 123 | `Recursive` | `TreeRollUpQuery` | Amounts rolled up a tree: every employee paired with everyone below by a recursive subquery, joined to orders and grouped by the ancestor |
| 124 | `Recursive` | `CalendarOfDaysQuery` | A row for each day between two dates, seeded from inline rows and stopped by `where`, left joined to the orders of each day |
| 125 | `Recursive` | `RateOfReturnByIterationQuery` | A grouped `next`: the internal rate of return of each investment by Newton's method, stopped in `having`, the last step read with `distinctBy` |

### Dashboards

`namespace PureQL.CSharp.Model.Samples.Queries.Dashboards`, specification samples 126 to 154, one per query Projector stores, by its id:

| Spec | Projector query | Sample | Exercises |
|---|---|---|---|
| 126 | `bd6f078a-0a03-4561-a6d7-83c06345a6f5` | `RecentOrdersPerActiveUserQuery` | `and` of a boolean field and a `datetime` range; ordered by `count`, then the key |
| 127 | `2fac3de8-6ed5-406d-bf90-055674de7d4e` | `BilledWithFeePerEmployeeQuery` | A join chain, a date range, `sum` of `add`, ordered by it |
| 128 | `2fb00633-3184-4faf-acae-699795e9b3f6` | `RevenuePerUserNameQuery` | A key from a joined entity, ordered by an aggregate descending |
| 129 | `934b5726-9aa7-48de-81da-d2430d2f9403` | `ItemValueShareOfOrderTotalPerProductQuery` | A ratio of two `sum`s |
| 130 | `47412e00-e311-49ea-b053-ac6195acb437` | `ItemValuePerUserAndProductQuery` | A `concat` group key, a `greaterThan` filter |
| 131 | `b51b0f7f-907d-4058-b1ba-d6a403e990bf` | `OrderStageExcludingUserQuery` | `and` of `in` and `notEqual`; a nested `if` key over `equal` |
| 132 | `528d489c-032a-488a-96a0-fab37bd097df` | `RevenuePerTeamLeadPerPeriodQuery` | The same entity joined twice through aliases; a computed period key |
| 133 | `6a03aeba-fa02-4b56-91c9-5a4a5daab537` | `LoginStatsPerTenantQuery` | `average` of `datetimeDiffSeconds`, a coalesced nullable `average`, a guarded share |
| 134 | `0b617d43-461a-408c-a432-15aef9fcbb60` | `UserFunnelStageQuery` | A left joined subquery; a nested `if` key over `not`, `coalesce` and `lessThan` |
| 135 | `fc47ae13-4219-44c4-aed3-90cc9b6d6445` | `DailyRevenuePerTenantQuery` | A `sum` with a predicate per tenant next to the total |
| 136 | `89fcdced-5d53-43b0-b2ee-eb8197a9d091` | `OrderTotalsPerStatusAndTenantQuery` | Filtered `sum`s, the total, `average` of `dateDiffDays`, `count` |
| 137 | `8b1efefc-0a15-4b46-a451-21dc561c5da8` | `PlacedVersusShippedPerDayQuery` | Reading from a subquery left joined to another; `coalesce` and `subtract` |
| 138 | `dfc68e29-087b-44f3-b744-b96527b93910` | `WeeklyUnitsPerProductQuery` | A computed week key, `dateAddDays` over the key, filtered `sum`s |
| 139 | `cc714d72-0023-4ca6-a7bb-8f336d347671` | `RepeatBuyerOrderAndItemProfileQuery` | Two grouped subqueries inner joined, filtered on a subquery column |
| 140 | `d363779f-fa97-4f23-bcbe-57ed8752a372` | `ScoreByProductPerOrderDayQuery` | An `integer` computed key; `average`s with `equal` / `notEqual` predicates |
| 141 | `b21432b3-e413-40e6-98ea-117cc50f5bce` | `DirectReportsPerManagerQuery` | `notEqual` on a nullable field; `min` of `time` and `date` |
| 142 | `dfddb067-ef61-48a8-9945-f22b8583c4d8` | `DailyInStockRevenueShareQuery` | `subtract`, `divide` and `multiply` over aggregates |
| 143 | `409f5283-7423-4d18-99a2-e1e8ff0be846` | `LineFlagsPerUserAndStatusQuery` | Two `string` keys; `count`s with `and` / `not` / `lessThan` / boolean predicates |
| 144 | `be439994-1e21-4ce5-a46b-416302754f08` | `ItemRevenueToOrderTotalPerEmployeeQuery` | An entity joined to two grouped subqueries, an `in`-list filter |
| 145 | `c87841bc-4433-4e7c-bcb8-91d62949bc44` | `RevenuePerEmployeeAndAgeQuery` | Four group keys; an aggregate divided by a `decimal` key |
| 146 | `5579e839-e1dd-44c9-bbd7-2a536b82db0f` | `InStockItemValueVersusOrderedPerUserQuery` | A joined subquery filtered by `and(equal, in)`; a variance percentage |
| 147 | `e40e9165-3e2d-4686-b410-ec3c8cebd34c` | `OrdersPerWeekdayQuery` | A six-level `if` key over `modulo` of `dateDiffDays`; a guarded share |
| 148 | `bb020cef-6f52-4db0-a66c-4059f01f8690` | `ShippedSharePerUserAndTenantQuery` | `if`-guarded ratios of filtered `count`s |
| 149 | `e14b189d-90cc-4acb-b1e4-dc61998cbb03` | `UnbilledOrderAgingQuery` | `dateDiffDays` from a literal, `if` buckets over it, ordered by two expressions; paged |
| 150 | `6e0f2c5a-21ca-4eca-bf38-542e1e6a3d93` | `ProductSalesPerformanceQuery` | A guarded percentage, `average` of `datetimeDiffSeconds` and `max` of `dateDiffDays` in a subquery; ratios of its columns |
| 151 | `59f44177-b983-46a8-901f-b9136f5c50c9` | `UserSalesSummaryQuery` | One inner and two left joined subqueries; a guard comparing `coalesce` with `integer` 0 |
| 152 | `ba64a130-7551-4c81-89fe-20e6c9267303` | `UserAccountOverviewQuery` | Five subqueries, one reading another; inner and left joins; `max`; paged |
| 153 | `61b93c09-1e41-4d0b-9d4f-756d507cfbd5` | `EmployeeScorecardQuery` | A `from` alias self-joined through a join alias; three subqueries with nested `and` ranges |
| 154 | `e2d37261-7318-4abc-809b-41d66a47a20f` | `LineRestockSuggestionQuery` | A left join on two keys; arithmetic in `if` branches; paged |

### Specification tests

Specification samples 155 to 388 are its `.jsonc` tests: edge cases, every operator in every clause, and real-world shapes. Each is one PureQL document, mirrored here like a `.json` sample. 166 and 193 are not mirrored (see "Samples not mirrored").

| Spec | Folder | Sample | Exercises |
|---|---|---|---|
| 155 | `Expressions` | `DeeplyNestedIfAndEqualQuery` | `equal(if(equal(if(...))))` nested 30 levels, the innermost condition a boolean field of an inner joined entity |
| 156 | `Select` | `LiteralEdgeValuesOfEveryTypeQuery` | Literals at the edges of each format: the largest and smallest 64-bit integers, decimals of 28 digits / 28 places / without a fraction, the first and last day, a leap day, microseconds, UTC datetimes with a fraction, a uuid of hex letters |
| 157 | `Where` | `InOverEveryKindOfListQuery` | `in` over a string, boolean, decimal, integer and datetime list literal, a typed null operand, and a nullable subquery column |
| 158 | `Pagination` | `PageOfOneFromAliasedSubqueryQuery` | A `from` subquery through an alias, a boolean as `where`, `take` 1 alone, `orderBy` keys of a boolean, a uuid, a typed null and `count` |
| 159 | `Expressions` | `OperatorTypingAtItsEdgesQuery` | Operator typing at its edges: `round` with negative and computed digits, `dateDiffDays` of a nullable date, three-operand `add`, typed null comparisons, `integer` against `decimal`, `if` over uuid, `coalesce` with the non-null operand last, `coalesce(integer?, decimal)` |
| 160 | `Expressions` | `OperandsReadFromDeclaredTypesQuery` | Operands of `equal` / `in` / comparisons and `orderBy` keys that are `if`, `coalesce`, `round`, `count` and aggregates over typed nulls and `integer` next to `decimal`: the family is read from the declared type |
| 161 | `Aggregates` | `AggregateEdgesOverAllRowsQuery` | Aggregates over all rows in a plain query: `count` with and without a predicate, `sum` of a nullable selector and of a literal, `average` of a `datetime`, `all`, and an `if` mixing `count` with a field |
| 162 | `GroupBy` | `GroupedQueryAtItsEdgesQuery` | A grouped query at its edges: nullable, date, boolean, literal and coalesced keys, a key in `having`, aggregates over `"all"` in `having`, `select` and `orderBy`, keys combined with aggregates through `equal` / `in` / date math / `concat` |
| 163 | `Subqueries` | `TopSpendersThroughChainedSubqueriesQuery` | A grouped main query over subqueries: a grouped subquery with `having`, `orderBy`, `distinctBy` and `pagination` (take 2), a later subquery filtering with `in` over its user_id column, and the first subquery joined twice (inner, then left) under the aliases `a` and `b`, with `max` of a nullable column |
| 164 | `Expressions` | `DeepOperandChainsQuery` | Operators whose result type follows an operand nested 30 levels deep: `greaterThan(coalesce(if(greaterThan(...))))` over dates, `in(if(in(...)))` over strings, an `integer?` column of `if` chained through `else`, an `orderBy` key of nested `coalesce` over typed nulls, left joined fields nullable |
| 165 | `Expressions` | `DeepNumericNestingQuery` | `equal(if(equal(if(...))))` nested 30 levels deep over integers, the innermost `if` on a boolean field |
| 167 | `Where` | `OptionalFiltersSwitchedOffByNullQuery` | Nullable scalar parameters, each filter `or(equal(param, null), condition)` so a parameter bound to `null` switches it off, ordered and paged |
| 168 | `GroupBy` | `TopCustomersInPeriodQuery` | Two group keys, a `datetime` range in `where`, `having` on a `sum`, groups ordered by the aggregate descending then by a key, `average` over a group, paged with `take` |
| 169 | `Joins` | `UsersWithoutOrdersQuery` | An anti-join through a left join: an orders field compared with a typed null `uuid?` in `where`, ordered by a `datetime` |
| 170 | `Subqueries` | `FirstOrderWithinThreeDaysQuery` | A grouped subquery of each user's first order (`min` of a `datetime`) joined to users, `datetimeDiffSeconds` against `multiply(3, 86400)` in `where`, and the hours rounded to one digit |
| 171 | `GroupBy` | `OrderSizeBucketsQuery` | A computed `if` chain as the group key, a share of all orders as `count` divided by `count` over `"all"` and rounded to 3 digits, buckets ordered by their smallest order |
| 172 | `Expressions` | `LineTotalsAfterCouponDiscountQuery` | Mixed integer and decimal arithmetic per row: a kept quantity from `subtract` over `round` and `integerDivide`, a line total, and a discounted total rounded to 2 digits with `coalesce` turning the nullable discount of a left-joined inline-rows coupon back into a decimal |
| 173 | `Aggregates` | `OrderKpisInOneRowQuery` | A total query: `count` with and without a predicate, a share guarded against a zero count by `if`, `any`, and `max` of a datetime in one row |
| 174 | `Subqueries` | `OrdersAboveUserAverageQuery` | A grouped subquery of one average per user joined back to the orders: orders at least 20% above their user's average, ordered by the ratio of the two |
| 175 | `Temporal` | `ShiftWindowsInBusinessHoursQuery` | `time` literals compared in `where`, `timeAddSeconds` with seconds composed by `multiply`, and `timeDiffSeconds` divided by 3600 into hours |
| 176 | `Joins` | `ManagerChainDepthQuery` | Two left self-joins of one entity under aliases, the second `on` reading a nullable field of the first alias, nullable names, and a nested `if` over typed-null checks counting the chain depth |
| 177 | `GroupBy` | `CouponEffectivenessPerCouponQuery` | A group key made non-null by `coalesce` after a left join, `count` with a predicate, `sum` over `if`, `average`, and `orderBy` an aggregate that is not selected |
| 178 | `Temporal` | `TrialsEndingWithinThreeDaysQuery` | A nullable date lifted through `dateAddDays` and `dateDiffDays` in `where`, `select` and `orderBy`: users without a trial drop out of a comparison with null on their own |
| 179 | `Pagination` | `PagedOrderHistoryByParametersQuery` | `pagination` whose `skip` and `take` are integer parameters, a uuid parameter in `where`, and an id tie-breaker keeping the order total |
| 180 | `GroupBy` | `DefaultOverInGroupsAndTotalsQuery` | Default `over`: a total-query subquery (`count`, nullable `min`) next to a grouped query whose `count` / `min` / `average` run over each group, `over: "all"` only for the total a share divides by, `having` on `count`, ordered by an aggregate |
| 181 | `GroupBy` | `DeclaredOperatorTypesInGroupsQuery` | Declared types in a grouped query: `average` of an `integer` (decimal), `min` over the group and over all rows (non-null), `coalesce` and `if` mixing `integer` with `decimal` (nullable), `round` with `digits` over an `integer`, `equal` of an `integer` and a `decimal` |
| 182 | `Aggregates` | `TotalQueryJoinedOnTrueQuery` | A total query as a subquery (`count`, nullable `max`, `any`) joined on `true` so its one row sits next to every order, `coalesce` of its nullable column, `min` over all rows of a plain query |
| 183 | `Subqueries` | `DistinctBySubqueryLeftJoinedQuery` | `distinctBy` in a subquery whose `orderBy` picks each user's latest order without pagination, left joined to users so its columns are nullable |
| 184 | `GroupBy` | `EveryOperatorSignatureAsGroupKeysQuery` | Every operator in every signature as 555 `groupBy` keys (row context) over literals: lifted operators with and without a nullable operand, `integer` and `decimal` operands, `if` / `coalesce` for every type, `equal` / `notEqual` / the four comparisons / `in` for every family |
| 185 | `Expressions` | `EveryOperatorSignatureInAPlainSelectQuery` | Every operator in every signature as 916 columns of a plain `select` (projection context) over literals, with every aggregate and window function, with and without a predicate and with a nullable selector |
| 186 | `GroupBy` | `EveryOperatorSignatureInAGroupedSelectQuery` | Every operator in every signature as 1069 columns of a grouped `select` (group context) over literals, with every aggregate with and without a predicate, with a nullable selector and over all rows, next to a `uuid` field as the group key |
| 187 | `Aggregates` | `EveryOperatorSignatureInATotalQuery` | Every operator in every signature as 811 columns of a total query (total context) over literals, with every aggregate with and without a predicate and with a nullable selector |
| 188 | `Where` | `BooleanOperatorsNestedThirtyLevelsDeepQuery` | `not` / `or` / `and` nested thirty levels deep in `where`, the innermost `and` holding a decimal comparison so the filter keeps some orders |
| 189 | `Nulls` | `CoalesceNestedThirtyLevelsDeepQuery` | `coalesce` of a typed null and a `coalesce` nested thirty levels deep, the innermost operand the non-null `order_status`, so every level is a non-null string |
| 190 | `Numbers` | `RoundOfAddNestedThirtyLevelsDeepQuery` | `round` (negative digits) of `add` nested thirty levels deep over decimals, the innermost operand `order_total` |
| 191 | `Expressions` | `IfNestedThirtyLevelsOverCountsQuery` | `if` nested thirty levels deep through `else`, a `count` aggregate in every condition of a plain select, thresholds chosen so level 21 is the first to hold |
| 192 | `Select` | `LiteralsAtTheEdgesOfEveryFormatQuery` | Literals at the edges of each format: leap days, first and last instants and microseconds, nil and largest uuid, 28-digit and 28-place decimals, a zero integer, and awkward strings |
| 194 | `Nulls` | `NullIfEqualInEveryClauseOfAGroupedQuery` | `nullIfEqual` in `join.on`, `where`, group keys, `having`, `select`, `orderBy` and inside an aggregate's selector and predicate of a grouped query; `average` skips the sentinel turned null |
| 195 | `Aggregates` | `RatiosGuardedAgainstZeroDivisorsPerProductQuery` | `divide`, `integerDivide` and `modulo` by `nullIfEqual(..., 0)` over integer sums per product (null instead of a failure), `coalesce` to a default, integer operands keeping `nullIfEqual` an integer, a sum `over` all groups |
| 196 | `Nulls` | `BlankValuesOfEveryTypeAsNullQuery` | `nullIfEqual` for every type (string, decimal, boolean, date, time, datetime, uuid, field-vs-field), `trim` then `nullIfEqual`, `coalesce` falling back to the next value, and the same in `where` |
| 197 | `Aggregates` | `RatiosInATotalQuery` | A total query over no rows: `divide` by `nullIfEqual` of `count` and 0, a filtered `count`, `coalesce` of a nullable ratio |
| 198 | `Aggregates` | `LatestOrderNextToEachOrderQuery` | `first` and `last` in a plain query, run over all rows and repeated on every row: compared, differenced, divided and with a predicate (nullable), under a page |
| 199 | `Aggregates` | `FirstAndLastInATotalQuery` | `first` and `last` in a total query (always nullable), with a nullable selector sorted null first, a predicate, and `dateDiffDays` of two of them |
| 200 | `Aggregates` | `FirstAndLastPerStatusOverAllRowsQuery` | `first` and `last` per group and with `over: all`, in `having` and `orderBy`, with computed sort keys (a nullable day difference descending, an `if`) |
| 201 | `Subqueries` | `LatestOrderPerUserFromASubqueryQuery` | A grouped subquery of `last` values left joined to an entity: every column of it nullable, with `coalesce` fallbacks |
| 202 | `GroupBy` | `PriceDeviationPerProductQuery` | `abs`, `sign` and `truncate` over aggregates: in `having`, `select` and `orderBy`, inside a selector, integer sums staying integers, `truncate` with and without digits |
| 203 | `Numbers` | `NumericFunctionsInEveryClauseQuery` | `abs`, `sign` and `truncate` in `join.on`, `where`, group keys (negative digits), `having`, an aggregate's selector and predicate, and `orderBy` |
| 204 | `Numbers` | `NumericFunctionCornersQuery` | Corners of `abs`, `sign` and `truncate`: literals, nullable operands, an integer operand with digits, negative and computed digits, `round`, `floor` and `truncate` side by side |
| 205 | `Numbers` | `AbsSignAndTruncateInATotalQuery` | `abs` / `sign` / `truncate` in a total query: non-null over filtered `sum` and `count`, nullable over `average` and `min`; `truncate` with and without `digits` |
| 206 | `Numbers` | `OrdersActiveInAPeriodQuery` | `greatest` / `least` clipping a date range to a period in `where`, `select` and `orderBy`; a discount capped by the total and a payable floored at zero; `coalesce` before `least` over a nullable operand, and a lifted `least` over it |
| 207 | `Numbers` | `GreatestAndLeastOfEveryTypeQuery` | `greatest` / `least` of every type (decimal, integer, nullable integer, string, boolean, date, time, datetime, uuid, nullable uuid), three operands and the middle of three from nested calls |
| 208 | `GroupBy` | `GreatestAndLeastOverAggregatesQuery` | `greatest` / `least` in a grouped query: over aggregates in `having`, `select` and `orderBy`, over `all` rows, inside an aggregate's selector and predicate, and as a `groupBy` key |
| 209 | `Numbers` | `GreatestAndLeastInATotalQuery` | `greatest` / `least` in a total query: non-null over `count` and `sum`, nullable over `min` and `max`, until `coalesce` gives a default |
| 210 | `Numbers` | `StandardDeviationByHandQuery` | A spread by hand in a total query: variance from `average` of `power` less `power` of the `average`, floored with `greatest`, `sqrt`, a coefficient guarded by `nullIfEqual`, a geometric mean as `exp` of an `average` of `ln` with a predicate, and `log` to base 2 of a count |
| 211 | `GroupBy` | `GrowthPerUserQuery` | A compound growth rate per group: `last` over `first` guarded by `nullIfEqual`, to the `power` of one over the months between two dates; `power`, `ln`, `log10` and `sqrt` over aggregates in `having`, `select` and `orderBy`, over `all` rows and inside a selector and a predicate |
| 212 | `Numbers` | `PowersAndLogarithmsCornersQuery` | Corners of `power`, `sqrt`, `exp`, `ln`, `log10` and `log`: integer operands giving a decimal that `round` turns into an integer, negative, fractional, computed and literal exponents, a nullable operand, nested calls, and bases that are literal, computed or below 1 |
| 213 | `Numbers` | `PowersAndLogarithmsInEveryClauseQuery` | `power` / `sqrt` / `exp` / `ln` / `log10` / `log` in every clause: `power` in `join.on` against a credit limit stood in by `user_age` times 2000, `sqrt` in `where`, `floor` of `log10` and of a base-2 `log` of `greatest(order_total, 1)` as group keys, `exp` of the `average` of `ln` in `having`, `power(10, key)` and an `average` of `sqrt` in `select`, ordered by the keys |
| 214 | `Conversions` | `OrdersPerDayOfADatetimeQuery` | A datetime grouped by its date: `asDate` and `asString` as `groupBy` keys, `asTime` of `max` / `min` in `having` and `select`, a title built with `concat` over `asString` of a date, an integer and a decimal, `asDatetime` of the key, a conversion inside a `count` predicate, `length` of `asString` of a decimal |
| 215 | `Conversions` | `AsStringOfEveryTypeQuery` | `asString` of every type that is not a string: fields, literals and computed values, non-null and lifted over a nullable operand (nullable `integer`, `boolean`, `date`, `datetime` and `uuid` through a left-joined inline subquery), and its text used by `length`, `substring`, `concat` and `coalesce` |
| 216 | `Conversions` | `DatesCombinedWithDatetimesQuery` | A date against a datetime through `asDate` and `asDatetime`: in `join.on`, in `where` against a nullable `date`, in `orderBy`; a date and a time put together into an instant with `datetimeAddSeconds`, `datetimeDiffSeconds`, and a nullable datetime truncated to midnight through its date |
| 217 | `Conversions` | `ConversionsInATotalQuery` | Conversions in a total query: `asString` of `count`, `sum` and `any`, `asDate` and `asDatetime` over `max` and `min` (nullable there), `coalesce` giving a label, a `count` predicate over `asTime` |
| 218 | `Conversions` | `ReadingTextOfEveryTypeQuery` | `as<Type>` and `tryAs<Type>` of a string for every type that is not one: non-null over a non-null string and lifted over a nullable one, `tryAs` always nullable, the text `asString` gives read back, text built with `concat` or cut with `substring` read like any other, `asDate` of a datetime and of a string |
| 219 | `Conversions` | `UnparsedPostalCodesPerCountryQuery` | `tryAs` in a grouped query: a nullable group key, predicates that count the text that is not a number, a nullable selector of `min` / `max` / `sum`, `having` and `orderBy`, and lifted arithmetic over the parsed key |
| 220 | `Conversions` | `TextKeysJoinedToTypedKeysQuery` | A key stored as text joined to a typed key: `tryAsUuid` and `tryAsDecimal` in `join.on` (text that is not a literal gives null and matches nothing), `tryAsUuid` / `tryAsDecimal` in `select` and `orderBy`, and `asInteger` of a text in `where` |
| 221 | `Conversions` | `ReadingTextInATotalQuery` | tryAsInteger inside a total query's predicates and selectors (a count, a max, an average, an all over a nullable mobile): unreadable text is skipped as null, and asInteger and asDatetime of text literals next to the aggregates |
| 222 | `Strings` | `UsersPerEmailDomainQuery` | replace / position / splitPart in a grouped query: a group key cut from an email and a second key cut from the first, position in where and in predicates, replace and position inside selectors, replace and length over group keys |
| 223 | `Joins` | `JoinOnNormalizedSkuQuery` | replace (after lower) in join.on against the text of a uuid, position in where, splitPart in select and orderBy, also over a uuid written as text |
| 224 | `Strings` | `ReplacePositionSplitPartCornersQuery` | Corners of replace / position / splitPart: an empty pattern and delimiter, a pattern from a field, a computed or literal start, a negative index, nullable operands, a literal operand, parts of a date written as text |
| 225 | `Strings` | `ReplacePositionSplitPartInATotalQuery` | replace / position / splitPart in a total query: inside predicates and selectors and over aggregates, where min is nullable and replace over it is lifted, and replace over the text of a count |
| 226 | `Strings` | `FixedWidthReportLinesQuery` | padStart / padEnd / repeat / reverse / concatWith in a grouped query: a padded group key, report lines of fixed width with a bar over count, concatWith over aggregates and in a selector, reverse in a predicate, a padded count as a sort key |
| 227 | `Strings` | `OrderLabelsOverJoinsQuery` | concatWith over a left join skipping a null code next to a lifted padEnd, a decimal written with two places through truncate / round / splitPart / padEnd, and padStart in where |
| 228 | `Strings` | `PaddingAndJoiningCornersQuery` | Corners of padStart / padEnd / repeat / reverse / concatWith: a multi-code-point and an empty fill, literal, computed and negative lengths, a count of zero, nullable operands, only null values, five values, an empty separator, nested concatWith |
| 229 | `Strings` | `PaddingAndJoiningInATotalQuery` | padStart, padEnd, repeat, reverse and concatWith in a total query over count, min and max of users |
| 230 | `Strings` | `RegularExpressionDialectQuery` | every construct of the matchesRegex dialect (31 literal patterns) over an inline rows subquery of texts joined to users |
| 231 | `Strings` | `UsersPerEmailDomainByRegexQuery` | extractRegex, matchesRegex and replaceRegex in a grouped query: as group keys, in where, having, predicates and selectors, and over group keys in select |
| 232 | `Strings` | `CodesMatchedByRegexQuery` | regular expressions over joined sources: replaceRegex on both sides of join.on, negated match in where, capture groups, tryAsInteger over an extracted number, coalesce over a missing match, a replacement taken from a field |
| 233 | `Strings` | `RegularExpressionsInATotalQuery` | matchesRegex, extractRegex and replaceRegex inside a total query (aggregate), over count, min, max and every |
| 234 | `Aggregates` | `ScoresAgainstTheDistributionQuery` | statistical totals repeated on every row of a plain query (average, population standard deviation, median, continuous percentile, mode) for a z-score, control limit and comparisons |
| 235 | `Aggregates` | `StatisticsForEveryTypeQuery` | median, percentiles, mode, variance and standard deviation in a total query over every type they take (decimal, string, boolean, date, time, datetime, uuid) |
| 236 | `Aggregates` | `PriceStatisticsPerProductQuery` | grouped median, mode, continuous and discrete percentiles, deviations and variance, with predicates and a having over a standard deviation |
| 237 | `Aggregates` | `StatisticsFromAGroupedSubqueryQuery` | `median`, `standardDeviationSample` and `mode` as columns of a grouped subquery, left joined to users: nullable after the join, a median in `coalesce` and a filter in `where`, a `mode` defaulted with `coalesce`, ordered by the nullable median descending |
| 238 | `Aggregates` | `ConcatAllInATotalQuery` | `concatAll` in a total query is non-null: `distinct`, an `orderBy` by other expressions, a `predicate`, a computed selector, and the result read through `notEqual`, `nullIfEqual` with `coalesce`, and `length` |
| 239 | `Aggregates` | `ConcatAllRepeatedOnEveryRowQuery` | `concatAll` in a plain query runs over all rows and repeats on every row: distinct values, a `startsWith` over it, a timeline built with `concatWith` and joined with a line break |
| 240 | `Aggregates` | `ConcatAllNamesPerCountryQuery` | `concatAll` in a grouped query: in `having` and `orderBy`, descending, `distinct`, a nullable selector whose nulls are skipped, a computed selector with an empty separator, `over` `"all"`, a `predicate`, and inside `concat` |
| 241 | `Subqueries` | `ConcatAllNamesPerOrderFromASubqueryQuery` | `concatAll` as a column of a grouped subquery over a join: nullable after a left join, `contains` on it false for the orders without lines, defaulted with `coalesce` |
| 242 | `SetOperations` | `GroupsWithATotalsRowQuery` | `unionAll` of a grouped query and a total query with the same columns as a subquery, a totals row sorted last by an `equal` in `orderBy`, a division guarded with `nullIfEqual` |
| 243 | `SetOperations` | `UnionColumnTypesQuery` | `union` columns matched by alias in any order and typed to the least type accepting every input: `integer` and `decimal` give `decimal`, a column nullable in one input is nullable, duplicates removed, read with `notEqual` against a typed null |
| 244 | `SetOperations` | `CombinedQueriesInASubqueryChainQuery` | Combined queries among the subqueries: a `union` of inputs paged or deduplicated on their own, a grouped subquery reading it, an `except` over an entity and that subquery, joined and tested with `in` |
| 245 | `SetOperations` | `UsersCommonToSpendersActiveAndNamedQuery` | A combined (intersect) main query with a subquery of its own, an input reading the subquery and an input that is itself a unionAll; the threshold parameter replaced by a literal. |
| 246 | `SetOperations` | `RowCountsOfEverySetOperationQuery` | unionAll, union, intersect and except over the same two inputs as subqueries, their row counts put together by a unionAll of four total queries. |
| 247 | `Subqueries` | `OrdersPerListedStatusQuery` | Inline rows as the from source of a grouped query, left joined to orders; a column with a null in one row is nullable. |
| 248 | `Parameters` | `OrdersWithTaxFromAParameterRowQuery` | A single inline row of parameters and a literal inner joined on true next to every row; a nullable parameter gives a nullable column. |
| 249 | `Subqueries` | `InlineRowsOfEveryCellTypeQuery` | Inline rows with cells of every type as literals, typed nulls and parameters, in any column order; a column takes the least type accepting every row (integer and decimal give decimal, a null makes it nullable). |
| 250 | `Subqueries` | `MonthsWithoutOrdersFilledWithZeroQuery` | Months without orders filled with zero by a unionAll of a grouped subquery and the missing months, joined to inline rows of months and quarters. |
| 251 | `Joins` | `QuadrantsOfTwoInlineListsQuery` | Two inline-row subqueries inner joined on true (a cross product) and aggregated into one row of joined strings in a given order. |
| 252 | `Windows` | `EveryAggregateOverWindowsQuery` | Every aggregate (count, sum, average, median, percentiles, variance, standard deviation, min, max, first, last, mode, distinct join, any, every) over a whole-partition window and over a framed running window. |
| 253 | `Windows` | `FrameOfEveryFormQuery` | Every form of a `frame`: a number of rows, `"all"` or both on either side, a frame of 1000 rows, without a partition, ordered backwards, and next to a window that is the whole partition |
| 254 | `Windows` | `WindowsInEveryPositionQuery` | Window functions as operands of other operators, in `orderBy` next to `distinctBy` and `pagination`, in a paged subquery and in the inputs of a set operation |
| 255 | `Windows` | `WindowsOverNullableFieldsQuery` | Windows over a left join: aggregates of a nullable selector or with a predicate are nullable, `count` and `sum` never are, `previous` of a nullable operand with a default, nullable fields as partition and sort keys |
| 256 | `Windows` | `WindowsKeyedByRowExpressionsQuery` | Partition and sort keys of a window that are row expressions: a computed month, an `if`, a literal, a `coalesce` of a nullable field, a nullable partition key, several keys in both directions, `ntile` |
| 257 | `Windows` | `StreaksAndFirstValuesQuery` | Gaps between orders with `previous`, the end of a sequence with `next`, and `first`, `last`, `concatAll` and `all` over a window with their own `orderBy` |
| 258 | `Recursive` | `RecursiveSeedsOfEveryKindQuery` | The seed of a recursive subquery as a grouped query with `having`, a `unionAll`, a paged query and a total query; a nullable grouped key and total aggregate with `next` returning a non-null value; a series stopping before `maxDepth` |
| 259 | `Recursive` | `RecursiveNextWithJoinsAndAliasesQuery` | A `next` query reading the subquery under an alias, joining entities with `inner` and `left`, filtering with `in` over an earlier subquery, computing columns; the result read twice under aliases and by a later subquery |
| 260 | `Recursive` | `WalkThatStopsAtACycleQuery` | A walk over edges given as inline `rows` containing a cycle: `next` skips a node already on the path with `contains`, so the walk ends before `maxDepth` |
| 261 | `Recursive` | `TwoRecursiveSubqueriesQuery` | Two recursive subqueries, the second reading the first in its seed and in an `in` list of its next (`not in`), `maxDepth`, combined by `unionAll` with a side column |
| 262 | `Recursive` | `RecursiveColumnTypesQuery` | A recursive subquery whose seed fixes every column type (typed nulls, a decimal, a nullable datetime from a left join) and whose next returns non-null and integer values those columns accept |
| 263 | `Recursive` | `RecursiveSubqueryReadersQuery` | A recursive subquery read by a grouped subquery, a total subquery, `in`, two joins, `rowNumber`, `count` over all rows, with `distinctBy` and `pagination` |
| 264 | `GroupBy` | `GroupingWithoutCaseQuery` | `foldCase` as group keys, over a nullable field too; in `having`, in a `min` selector, `concatAll` and over a key |
| 265 | `Joins` | `MatchingWithoutCaseOverJoinsQuery` | `foldCase` on both sides of `equal` in `join.on` (a self join through an alias), in an `in` list, and lifted over the nullable fields of a left join |
| 266 | `Strings` | `FoldCaseCornersQuery` | Corners of `foldCase`: twice, over `upper`, a literal, a null string and a `concat`, inside `length`, `position`, `replace`, `splitPart` and `if`, as a partition and sort key of windows |
| 267 | `Aggregates` | `FoldCaseInATotalQuery` | `foldCase` in a total query: in `where`, predicates and selectors, and lifted over `min`, which is nullable there |
| 268 | `GroupBy` | `GroupingByNormalizedKeysQuery` | `normalize` as group keys, over `foldCase` and a nullable field (`NFKC`, `NFC`); in a selector, a predicate and over a key (`NFD`) |
| 269 | `Strings` | `NormalizeMatchingOverJoinsQuery` | `normalize` on both sides of `equal` in `join.on`, as the operand of `in`, and lifted over the nullable field of a left join |
| 270 | `Strings` | `NormalizeCornersQuery` | Corners of `normalize`: each of the four forms, applied twice, composed after decomposing, over a literal, a typed null and a `concat`, with `foldCase` in both orders, and inside `length`, `contains`, `substring`, `matchesRegex` and window keys |
| 271 | `Strings` | `NormalizeInATotalQuery` | `normalize` in a total query: in `where`, in predicates and selectors, and lifted over `min`, which is nullable there |
| 272 | `GroupBy` | `GroupingWithoutAccentsQuery` | `removeDiacritics` as a group key, over `foldCase` and over a nullable field, so that spellings with and without accents fall in one group; in a selector, a predicate and over a key |
| 273 | `Strings` | `RemoveDiacriticsMatchingOverJoinsQuery` | `removeDiacritics` on both sides of `equal` in `join.on`, as the operand of `in`, and lifted over the nullable field of a left join |
| 274 | `Strings` | `RemoveDiacriticsCornersQuery` | Corners of `removeDiacritics`: applied twice, over a literal, a typed null, a `concat` and a decomposed string, with `foldCase` in both orders, and inside `length`, `startsWith`, `matchesRegex`, `substring` and window keys |
| 275 | `Strings` | `RemoveDiacriticsInATotalQuery` | `removeDiacritics` in a total query: in `where`, in predicates and selectors, and lifted over `min`, which is nullable there |
| 276 | `Aggregates` | `TemporalMeansInATotalQuery` | `average`, `median` and `percentileContinuous` of a `date`, a `datetime` and a `time` in a total query, where each is nullable; a mean exactly halfway between two days or microseconds is the later one |
| 277 | `Windows` | `TemporalMeansOverWindowsQuery` | average and median of dates, times and datetimes over a partition and over frames (so far, last two rows), rounded halfway to the later value |
| 278 | `Aggregates` | `TemporalMeansWithPredicatesQuery` | grouped average, median and continuous percentile of dates, datetimes and times with predicates, over all, in having and orderBy |
| 279 | `Aggregates` | `TemporalMeansOfComputedValuesQuery` | grouped average and median of computed dates, times and datetimes (asDate, asTime, dateAddDays, startOfWeek, datetimeAddSeconds) over a join |
| 280 | `Nulls` | `InclusiveBoundsThatMayBeNullQuery` | greaterThanOrEqual and lessThanOrEqual over nullable date and uuid operands in where and select: two nulls are equal, with one null both a comparison and its opposite are false |
| 281 | `Nulls` | `OrderingComparisonsOfNullsQuery` | all four ordering comparisons of two typed nulls and of a null with a value, for each of the eight types |
| 282 | `Nulls` | `InclusiveComparisonsInJoinsAndGroupsQuery` | inclusive comparisons of nullable operands in join.on, having, a count predicate and over a nullable max and a nullable group key |
| 283 | `Aggregates` | `InclusiveComparisonsInATotalQuery` | counts at or after, after and equal to a nullable bound (the first the sum of the others), all/any with inclusive comparisons and a max compared with a nullable total |
| 284 | `Pagination` | `FirstPageWithoutSkipQuery` | a first page: take alone, no skip, after a descending orderBy |
| 285 | `Pagination` | `OrdersAfterTheFirstFourQuery` | skip without take: rows after the first four of an ordered query |
| 286 | `Pagination` | `PageOfOrdersFromParametersQuery` | a page whose take and skip are parameters |
| 287 | `Subqueries` | `PaginationFormsInSubqueriesQuery` | take alone, skip alone and a parameter in subqueries, plain and grouped, and after distinctBy in a main query |
| 288 | `Select` | `OneValueListLiteralsOfEveryTypeQuery` | a list literal and a scalar literal of one value of every type in select |
| 289 | `Parameters` | `ListParametersThatMayBeEmptyQuery` | a list parameter of every type behind a flag, which may be empty |
| 290 | `Subqueries` | `SetsThatAreEmptyAtExecutionQuery` | in over a subquery column that may be empty, also one filtered by a list parameter |
| 291 | `Where` | `ListLiteralsInEveryClauseQuery` | list literals of one, two and three values in join.on, where, group key, having, predicate, select and orderBy next to list parameters |
| 292 | `Numbers` | `RoundingDecimalResultsQuery` | rounding of decimal results as the specification defines it |
| 293 | `Numbers` | `RoundingIntegersToTensAndHundredsQuery` | round and truncate of an integer with digits (-1, -2, computed digits, positive digits, nullable, in a sum), staying an integer |
| 294 | `GroupBy` | `RoundedAverageAndQuotientPerTenantQuery` | Rounding in a grouped query: round of an average, floor/ceiling of sums and quotients, truncate with digits of a sum, in having and orderBy |
| 295 | `Numbers` | `IntegersUsedAsTheyAreQuery` | An integer used as it is: in subtract, integerDivide, modulo, count, sum, abs, a comparison and asDecimal |
| 296 | `Recursive` | `WidenedSeedColumnsOfARecursionQuery` | A recursive seed declaring its columns as next fills them: asNullable of a uuid, datetime and string, asDecimal of a nullable integer and of an integer |
| 297 | `Conversions` | `WideningInGroupKeysAndAggregatesQuery` | asDecimal and asNullable as group keys, in having, select and orderBy over count and sums of integers, and as the left operand of nullIfEqual |
| 298 | `Nulls` | `AsNullableOfEveryTypeQuery` | asNullable of a non-null value of every type, of a literal, an expression and a widened integer, in notEqual, coalesce and next |
| 299 | `Conversions` | `AsDecimalOfIntegersAndStringsInATotalQuery` | asDecimal of an integer next to asDecimal of a string in a total query: lifted over a nullable integer, tryAsDecimal, asNullable of a count |
| 300 | `Numbers` | `DecimalRemainderCornersQuery` | modulo and integerDivide over decimals: fractional part, negative on either side, nullIfEqual guard, integer operands, mixed operands, and the identity left = right * integerDivide + modulo |
| 301 | `Numbers` | `DecimalRemaindersInAGroupedQuery` | `integerDivide` and `modulo` of decimals as group keys, in `having`, over aggregates and inside a selector and a predicate |
| 302 | `Numbers` | `DecimalRemaindersInATotalQuery` | `integerDivide` and `modulo` of decimals in `where` and over the aggregates of a total query, lifted over a nullable average |
| 303 | `Temporal` | `DurationsInPartsQuery` | A duration in seconds split into days, hours, minutes and seconds with `integerDivide` and `modulo` of a decimal, as a label, a running sum and a sort key |
| 304 | `Numbers` | `RoundHalfToEvenCornersQuery` | `round` with `halfEven`: to a whole number, to cents, tens and hundreds, with computed digits, over a nullable, nested, for cash rounding and next to the default rule |
| 305 | `Numbers` | `RoundHalfToEvenInAGroupedQuery` | `round` with `halfEven` as a group key, in `having`, `select` and `orderBy`, over a `sum` and inside a selector: tax rounded once against per line |
| 306 | `Numbers` | `RoundHalfToEvenInATotalQuery` | `round` with `halfEven` in `where` and over the aggregates of a total query, lifted over those that are nullable there |
| 307 | `Windows` | `RoundHalfToEvenOverWindowsQuery` | `round` with `halfEven` over a running total and a share, as the operand of `previous` and as a sort key of a window and of the query |
| 308 | `Nulls` | `NullableOperandsByRoleQuery` | Each operand named by its role (`digits`, `exponent`, `base`, `start`, `length`, `pattern`, `replacement`, `delimiter`, `index`, `fill`, `count`) nullable, one at a time, making the result nullable |
| 309 | `Nulls` | `AbsentAndNullableOperandsQuery` | round, truncate, substring and position with the operand that may be absent (digits, length, start) left out, written and nullable: absent does not make the result nullable, a nullable one that is written does |
| 310 | `Nulls` | `NullableRoleOperandsInAGroupedQuery` | Nullable role operands (substring length, power exponent, repeat count, round digits) in a group key, having and select of a grouped query, as aggregates over a nullable field next to non-null ones |
| 311 | `Nulls` | `NullableRoleOperandsInATotalQuery` | Role operands (log base, repeat count, replace pattern, splitPart index) that are nullable aggregates of a total query, next to count, which stays non-null |
| 312 | `Temporal` | `AmountsOfEveryKindQuery` | dateAddDays, dateAddMonths, datetimeAddMonths, timeAddSeconds and datetimeAddSeconds, each by a literal, a negative, another literal, a nullable value and a computed amount, named by its unit |
| 313 | `GroupBy` | `AddedAmountsInAGroupedQuery` | The operators that add in group keys, having, select, an aggregate predicate and orderBy of a grouped query, over keys and aggregates, with count as an amount |
| 314 | `Aggregates` | `AddedAmountsInATotalQuery` | The operators that add in where and over the aggregates of a total query, lifted over the nullable ones, with count as an amount and inside a selector |
| 315 | `Temporal` | `AddingAndTakingTheDifferenceQuery` | Adding an amount and taking the difference gives it back (days, months, seconds), timeAddSeconds wrapping a day, adding months then days, previous over an added date, a nullable added date in a difference |
| 316 | `Temporal` | `PartsAndBackQuery` | dateFromParts, timeFromParts and datetimeFromParts as the inverse of the parts, midnight, the first of the month, a nullable part and a time with a fraction of a second |
| 317 | `GroupBy` | `FromPartsInAGroupedQuery` | dateFromParts / timeFromParts / datetimeFromParts in a grouped query: a month and an hour built from parts as group keys, in having over an aggregate year, in select over keys and aggregates, in a count predicate and in orderBy |
| 318 | `Aggregates` | `FromPartsInATotalQuery` | dateFromParts / timeFromParts / datetimeFromParts in where and over the aggregates of a total query, nullable there, next to a non-null fixed instant |
| 319 | `Windows` | `FromPartsOverWindowsQuery` | An instant built from a date and a time as a sort key, the operand of previous and a datetimeDiffSeconds operand, a month built from parts as a partition key, lifted over a nullable date after a self-join |
| 320 | `Aggregates` | `PairStatisticsInATotalQuery` | covariancePopulation / covarianceSample / correlation in a total query, all nullable there: a predicate, r-squared by power, a slope by divide and nullIfEqual, a nullable operand |
| 321 | `Windows` | `PairStatisticsOverWindowsQuery` | The statistics of two columns over all rows, a partition and a frame of ten rows: the population covariance non-null there, the sample covariance and the correlation nullable, and a predicate |
| 322 | `GroupBy` | `PairStatisticsInAGroupedQuery` | correlation in having, select and orderBy of a grouped query: over the group and over all rows, over computed values, covariance with its operands swapped, an absolute correlation compared |
| 323 | `Aggregates` | `PairStatisticsOperandKindsQuery` | The operands of the pair statistics: decimals, integers, a nullable left or right, a predicate, literals; the population covariance is nullable exactly when an operand or the predicate is |
| 324 | `Numbers` | `GreatCircleDistanceQuery` | sin, cos, asin, sqrt, power, atan2 and pi: the haversine distance and the bearing from a point to stores written as inline rows, in where, select and orderBy |
| 325 | `Numbers` | `TrigonometricIdentitiesQuery` | `sin` / `cos` / `tan` and `asin` / `atan` / `acos` / `atan2` side by side: sin squared plus cos squared, asin of a sine and atan of a tangent (x back only within a quarter turn), half pi, quarter pi and pi from constants, the sine of pi, which is not zero, degrees and radians through pi, trigonometry lifted over a nullable field after a left join, an area from pi |
| 326 | `GroupBy` | `CircularMeanHourPerUserQuery` | Trigonometry in a grouped query: the mean hour as an angle from `atan2` of sums of sines and cosines, back to an hour with `modulo`, a concentration, an average sine, a filtered `count`, with the sums in `having`, `select` and `orderBy` |
| 327 | `Aggregates` | `TrigonometryOverAggregatesInATotalQuery` | Trigonometry in `where` and over the aggregates of a total query: `atan2` of sums, `cos` of a nullable `min`, `atan` of a nullable `average`, `pi` next to `count`, a `sum` of `tan` |
| 328 | `Windows` | `RangeDistancesOverNumbersQuery` | Every form of a range distance over an `integer` and a `decimal` key: a number, a decimal literal, a 28-place decimal, `"all"` on either side, a computed and a descending key and a partition |
| 329 | `Windows` | `RangeDistancesOverDatesQuery` | Every form of a range distance over a `date` and a `datetime` key: days, months and seconds, fractional seconds, `"all"`, a nullable key and a computed month key |
| 330 | `Windows` | `EveryAggregateOverARangeQuery` | Every kind of aggregate over a range (`count`, `sum`, `average`, `min`, `max`, `median`, `first`, `concatAll`, filtered `average`, `any`, `varianceSample`, `correlation`, a guarded share), typed as over all rows, and a rows frame beside it |
| 331 | `Windows` | `LastTwelveMonthsPerUserQuery` | Monthly sums in a subquery and a range of 11 months over the month, which counts months without rows where 11 rows back would not, plus a year-to-date partition |
| 332 | `Windows` | `SortingBySelectWindowColumnsQuery` | A plain query sorted by its own window columns: a product, a running total, a `rowNumber`, a share and a label are written once in `select` and named in `orderBy` next to a field |
| 333 | `OrderBy` | `GroupedSortByColumnsAndExpressionsQuery` | A grouped query ordered by columns and an expression (the average order) in both directions and by a nullable column, with the group count kept and the page skipping the first group |
| 334 | `Subqueries` | `SortedSubqueriesDistinctAndPagedQuery` | Subqueries whose orderBy names columns, serving distinctBy and pagination, read by a main query that does the same |
| 335 | `SetOperations` | `PagedSetOperationInputsQuery` | unionAll inputs each paged in the order of a column, and the combined rows sorted by a startOfMinute column computed by the query that reads them |
| 336 | `Aggregates` | `CountDistinctInATotalQuery` | countDistinct in a total query: over a nullable selector, with a predicate, over a computed month, a ratio guarded with nullIfEqual and compared with count |
| 337 | `Windows` | `CountDistinctOverWindowsQuery` | countDistinct over all rows, a partition, a frame of rows, a range of days and a running frame with a predicate |
| 338 | `GroupBy` | `CountDistinctInAGroupedQuery` | countDistinct in having, select and orderBy of a grouped query: over a uuid, a decimal and an integer, over all rows, compared with 1 and with count |
| 339 | `Aggregates` | `CountDistinctSelectorKindsQuery` | countDistinct of every selector kind: each type nullable or not, a computed value, a folded string, a condition, a literal, a null, a uuid literal and a predicate |
| 340 | `Aggregates` | `RegressionInATotalQuery` | regressionSlope / regressionIntercept / regressionRSquared in a total query: a predicate, the slope the other way round and by hand, the squared correlation, a predicted value, a nullable operand |
| 341 | `Windows` | `RegressionOverWindowsQuery` | regressionSlope, regressionIntercept and regressionRSquared over all rows, a partition, a 11-row frame and a 1-day range, and a value compared with the trend its rolling line predicts |
| 342 | `Aggregates` | `RegressionInAGroupedQuery` | The regression aggregates in having, select and orderBy of a grouped query, over the group, over all (over: all) and over computed values |
| 343 | `Aggregates` | `RegressionOperandKindsQuery` | Regression operands of decimals, integers, a nullable y or x, a predicate, literals, nulls and a constant x, always decimal? |
| 344 | `TimeZones` | `LocalDatesInEveryClauseQuery` | asDate and asTime with a per-user zone in join.on, where, select, a window partition, an aggregate selector and orderBy, next to the UTC day |
| 345 | `TimeZones` | `ZoneExpressionKindsQuery` | The zone of asDate and asTime as a literal, a computed if, a coalesce, a nullable and a null zone, and a nullable or null datetime |
| 346 | `TimeZones` | `ZoneNameFormsQuery` | Literal zone names of every form: areas, cities, three parts, hyphens, underscores, mixed case, and Etc/GMT+12 to Etc/GMT-14 |
| 347 | `TimeZones` | `LocalDatesGroupedAndTotalledQuery` | Grouping by the local day of a per-user zone with local times in having and aggregates, and a total subquery over one local day, joined on true |
| 348 | `TimeZones` | `LocalPartsInEveryClauseQuery` | year, hour, dayOfWeek, week, weekYear, month, minute, second and dayOfYear with a per-user zone in join.on, where, select, a window partition, an aggregate selector and orderBy |
| 349 | `TimeZones` | `LocalPartKindsQuery` | Every part with a `zone` (year to second, plus quarter, dayOfWeek, dayOfYear, week, weekYear): a literal zone, another literal zone, a zone column, a computed zone (coalesce of a nullable zone column and the zone column), a nullable zone column and a null zone (nullable part), a nullable datetime operand, a datetime literal in Asia/Kathmandu; `microsecond` and the parts of a `date` and a `time` take no zone |
| 350 | `TimeZones` | `LocalPartsInConditionsQuery` | Parts with a zone column in `where`, a boolean and a greeting `if`: the local hour at an instant, whether an active user can be called now (hour, weekday), signed up in the current local year, and a birthday on the user's own local day, ordered by id |
| 351 | `TimeZones` | `LocalPartsInGroupedAndTotalQueriesQuery` | Parts with a zone in a grouped query (a datetime key in `having` and `select`, the part of a `max`, inside `countDistinct` and `max`) and in a total query (a `count` predicate, the year of a nullable `min`, a nullable `mode`, ISO weeks as weekYear * 100 + week), joined on true |
| 352 | `TimeZones` | `LocalPeriodStartsInEveryClauseQuery` | `startOf…` with the zone of a subquery column in `join.on`, `where`, `select`, a window's partition, the sort key of a range window (518400 seconds) and an aggregate's selector, the UTC day start next to the local one, ordered by the local day start descending |
| 353 | `TimeZones` | `LocalPeriodStartKindsQuery` | Every `startOf…` that takes a zone (year to minute) in a literal zone, another literal zone, a zone column, a computed zone, a nullable zone column, a null zone, a nullable datetime and a datetime literal; `startOfSecond` and the starts of a `date` and of a `time` take no zone; asDate of a local start and a nested start |
| 354 | `TimeZones` | `LocalDayBucketsQuery` | Rows grouped by the start of the local day in a subquery, then read as instants: the local date of each bucket (asDate with a zone), totals by local month and running totals by local week over windows partitioned by startOfMonth / startOfWeek, and the previous bucket |
| 355 | `TimeZones` | `LocalPeriodStartsInGroupedAndTotalQueriesQuery` | `startOf…` with a zone in a grouped query (a datetime key in `having` against the start of the local month, startOfHour of the key, startOfWeek of a `max`, inside `countDistinct` and `max`) and in a total query (where against the start of the local quarter, the start of the month of a nullable `min`, a nullable `mode`), joined on true |
| 356 | `TimeZones` | `LocalInstantsInEveryClauseQuery` | `asDatetime` of a date and `datetimeFromParts` with the zone of a subquery column in `join.on`, `where`, `select`, an aggregate's predicate and selector, a window's partition and `orderBy`; UTC midnight next to the local one |
| 357 | `TimeZones` | `DatetimeZoneKindsQuery` | `asDatetime` and `datetimeFromParts` with a `zone` as a literal, a fixed offset (Etc/GMT-3), a column-like per-user zone, a `coalesce`, and null / nullable zones, dates and times (nullable exactly when an operand or the zone is); without a zone or of a string they read UTC |
| 358 | `TimeZones` | `LocalInstantRoundTripsQuery` | Instants built from the local date and time of other instants with a per-user `zone`: start of the local day, the same instant again, local noon, the same local time tomorrow and next month, the length of a local day and the offset from UTC |
| 359 | `TimeZones` | `LocalInstantsInGroupedAndTotalQueriesQuery` | `asDatetime` and `datetimeFromParts` with a `zone` in a grouped subquery (of a key and of an aggregate) and in a total subquery over one local day (Pacific/Pago_Pago), joined on `true` |
| 360 | `TimeZones` | `LocalMonthsInEveryClauseQuery` | `datetimeAddMonths` and `datetimeDiffMonths` with a per-user `zone` in `join.on`, `where`, `select`, a window's partition, an aggregate's selector and predicate, and `orderBy`, next to the UTC results |
| 361 | `TimeZones` | `LocalMonthKindsQuery` | The `zone` of `datetimeAddMonths` and `datetimeDiffMonths` as a literal, a fixed offset, a per-user zone, a `coalesce` and null / nullable zones, operands and months; `dateAddMonths` of a date takes no zone |
| 362 | `TimeZones` | `LocalMonthIdentitiesQuery` | Adding months in a zone and counting them back, the swapped difference, the same result built from parts, the start of the next local month and of the previous quarter, and an age in whole years |
| 363 | `TimeZones` | `LocalMonthsInGroupedAndTotalQueriesQuery` | The month operators with a `zone` in a grouped subquery (of a key and of aggregates) and in a total subquery over twelve local months, joined on `true` |
| 364 | `Recursive` | `BisectionOfARateQuery` | A rate solved by bisection: each step of a grouped recursive subquery aggregates the cash flows at the middle of an interval and keeps the half with the root; `where` limits the steps and `having` stops at a tolerance |
| 365 | `Recursive` | `PathCountsPerDepthQuery` | A grouped recursive next reading the subquery in a join: a user reached along several paths is one row of the next level, with the number of paths |
| 366 | `Recursive` | `HierarchyWalkWithAggregatedStepsQuery` | A grouped next with two joins, a where, a computed key, a having of count and any, and sum, a share over all rows of the step, countDistinct, max, all and concatAll columns |
| 367 | `Recursive` | `GroupedNextOverTotalAndCombinedSeedsQuery` | Recursive subqueries seeded by a total query and a combined query, each with a grouped next, beside one with a plain next; maxDepth as a literal |
| 368 | `Numbers` | `RoundedIntegersAsIntegerOperandsQuery` | round and truncate of an integer with digits in integer positions (days, months, year, repeat count, pad length, modulo, sum, abs, nullIfEqual, if, ntile sort key, previous, integer list), widened with asDecimal |
| 369 | `Numbers` | `RoundAndTruncateKeepTheOperandTypeQuery` | round and truncate with digits: an integer stays an integer and a decimal a decimal, negative and positive digits, halfEven, nullable operands; without digits round takes a decimal |
| 370 | `GroupBy` | `RoundedIntegersAcrossGroupedClausesQuery` | An integer band as a group key and rounded integers in having, select and orderBy of a grouped query: of a sum, count, over all rows, as selector of sum, max and countDistinct; average stays decimal |
| 371 | `Subqueries` | `RoundedIntegersFromSubqueriesQuery` | Rounded integers as columns of a grouped, a total and a recursive subquery, read as integers by the main query: join on an integer band, over a running sum and a row number |
| 372 | `Windows` | `ComputedKeysSelectorsAndDefaultsInWindowsQuery` | Computed partition keys (if, foldCase, startOfMonth in a per-row zone), sort keys (coalesce), range keys (dateDiffDays), selector and predicate of sum, operand and default of previous/next, and first with a datetimeFromParts selector |
| 373 | `Recursive` | `ContextsOfRecursiveStepsQuery` | A recursive next without groupBy computing row expressions from both sources, and one with groupBy reading keys and aggregates over the group and over all rows of the step, in select and having. |
| 374 | `Temporal` | `PartsLiftedOverEveryPartQuery` | dateFromParts, timeFromParts and datetimeFromParts lifted over every part, the zone included: nullable exactly when a part is. |
| 375 | `Nulls` | `ConstantOperandsNeverNullableQuery` | A form, mode, regular expression, group, separator, fraction, buckets and offset are never null and never make a result nullable. |
| 376 | `Windows` | `WindowFunctionsByKindQuery` | An aggregate window takes a frame, a range or a partition alone; the ranking functions and next take an order and neither a frame nor a range. |
| 377 | `Numbers` | `OperatorsOverAListInEveryContextQuery` | List operators over several operands in row, projection, group and total contexts. |
| 378 | `Numbers` | `LongOperandListsQuery` | Operators over long operand lists. |
| 379 | `Aggregates` | `AggregatesOverExactValuesQuery` | Aggregates computed from exact values and rounded once. |
| 380 | `Numbers` | `NestedAndFlatOperatorsQuery` | A list operator next to the nested form it stands for, one division by a product in place of two, and a rounding written out with round. |
| 381 | `Temporal` | `ZoneFreeOperatorsInEveryClauseQuery` | microsecond, startOfSecond, dateAddMonths and dateDiffMonths without a zone in where, group keys, an aggregate selector and predicate, having, and the partition and order keys of a window in a subquery; a nullable date makes the month arithmetic nullable |
| 382 | `TimeZones` | `MonthArithmeticOfLocalDatesQuery` | Month arithmetic on a local date read once by asDate (dateAddMonths, dateDiffMonths) next to datetimeAddMonths and datetimeDiffMonths with a zone; a nullable datetime or nullable zone makes every step nullable |
| 383 | `Conversions` | `AsStringOfOperatorResultsQuery` | asString of what operators yield: a date group key, count, sum, max of a datetime, a nullable max of a date, and the microsecond, startOfSecond and a decimal quotient inside a concatAll selector |
| 384 | `Joins` | `FullJoinThenLeftJoinOnNullableKeysQuery` | A full join makes both sides nullable after it, never in its own on; the next left join reads the nullable key in its own on |
| 385 | `Subqueries` | `SubqueryColumnsAfterALeftJoinQuery` | Subquery columns made nullable by an outer join |
| 386 | `Joins` | `SelfJoinsAfterALeftJoinQuery` | Self joins of one entity under aliases after a left join, reading nullable keys in the next on |
| 387 | `Where` | `ComparisonsOfEveryTypeInEveryClauseQuery` | Comparisons of every type in every clause |
| 388 | `Pagination` | `PagesAfterACursorInSubqueriesQuery` | Pages after a cursor inside subqueries |

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

// The deepest samples nest far beyond the default depth of 64.
JsonSerializerOptions options = new JsonSerializerOptions { MaxDepth = 512 };

foreach (JsonConverter converter in new PureQLConverters())
{
    options.Converters.Add(converter);
}

string json = JsonSerializer.Serialize(new GroupByCountQuery().Value, options);
```
