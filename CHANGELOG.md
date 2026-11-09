# Changelog

All notable changes to `PureQL.CSharp.Model.Samples` are documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Changed

- **Breaking:** migrated the catalogue to `PureQL.CSharp.Model` 0.1.0-preview.14.0.0, the model rewritten with C# 15 unions on .NET 11. `Value` is still a `PureQLQuery`, now a union of the query variants: each sample builds the variant its clauses select (`MainPlainQueryWithPagination`, `MainGroupedQueryWithoutPagination`, `MainTotalQuery`, …) from the model's records, every operator the record of its exact declared type in its context (`AddIntegerNullableRow`, `SumDecimalGroup`) and every leaf the record of its type (`FieldDecimal`, `LiteralInteger`, `KeyUuid`). No sample is renamed, every sample serializes to the same PureQL document as before, and expected results are unchanged.
- **Breaking:** targets .NET 11 only, as `PureQL.CSharp.Model` does; the SDK is pinned by `global.json`.
- Tests serialize through `PureQL.CSharp.Model.Serialization` 0.1.0-preview.6.0.0, which writes properties in the specification's order; expected documents are compared with the properties of every object in canonical order, and their content is unchanged.

### Removed

- Package validation, until the first release on the union-based model becomes the baseline.

## [0.1.0-preview.2.3.1] - 2026-11-02

### Fixed

- **Breaking:** expected results write a `datetime` cell in UTC ending in `Z`, the one spelling the specification gives a `datetime` result, instead of the stored fixture text without a designator (#56). 24 cells change in `Aggregates.AggregatesOverAllRowsQuery`, `Dashboards.UserAccountOverviewQuery`, `GroupBy.NullableGroupKeyAndAggregatesQuery`, `Select.FieldsThroughFromAliasQuery`, `Select.LatestOrderPerUserQuery` and `Select.LiteralOfEveryTypeNextToFieldQuery`. No query and no public API changes.

### Changed

- Tests serialize through `PureQL.CSharp.Model.Serialization` 0.1.0-preview.5.1.0 (#54).

## [0.1.0-preview.2.3.0] - 2026-10-26

### Added

- A `Dashboards` folder of 29 samples, one per PureQL query stored by Projector, rewritten over the `schema_with_foreign_keys` fixtures with every parameter replaced by a literal (#51). Every one carries an expected result computed on PostgreSQL 17, and the README maps each to its Projector query id.

## [0.1.0-preview.2.2.0] - 2026-10-23

### Changed

- Updated `Pure.Primitives.String.Operations` from 1.5.1 to 1.7.0 (#43), `Pure.RelationalSchema` from 2.0.3 to 2.1.0 (#44), `Pure.RelationalSchema.Samples` from 0.1.0-preview.1.0.0 to 0.1.0-preview.1.1.0 and `Pure.RelationalSchema.Storage.Samples` from 0.1.0-preview.1.0.0 to 0.1.0-preview.1.2.0 (#46). No change to the public API, and expected results are unchanged.
- Tests serialize through `PureQL.CSharp.Model.Serialization` 0.1.0-preview.5.0.1 (#48).

## [0.1.0-preview.2.1.0] - 2026-10-22

### Changed

- Updated `Pure.Primitives` from 3.6.5 to 3.7.0 (#40). No change to the public API.

## [0.1.0-preview.2.0.0] - 2026-10-14

### Added

- `Select.LatestOrderPerUserQuery`, mirroring specification sample 14: `distinctBy` keeps the first order per user. Its result is computed on PostgreSQL 17.

### Changed

- **Breaking:** migrated the catalogue to PureQL specification 0.1.0-preview.2.0.1, which `PureQL.CSharp.Model` 0.1.0-preview.13.0.0 models and `PureQL.CSharp.Model.Serialization` 0.1.0-preview.5.0.0 writes (#33). Every operator declares its exact type, so the samples use the exact unions in place of the leaf unions, `distinctBy` in place of `distinct`, aliased group keys, `JoinKind` in place of `JoinType`, and `Expr*` in place of `ValueProjection`/`ValueGroup`.
- **Breaking:** `Temporal.DatetimeLiteralsWithOffsetsQuery` is renamed `Temporal.DatetimeUtcLiteralsQuery` and writes its literals in UTC, as offsets are no longer accepted.
- **Breaking:** `Aggregates.AggregatesOverAllRowsQuery` is now a total query (`aggregate`).
- Expected results are unchanged: the migration keeps every query's meaning.

## [0.1.0-preview.1.0.1] - 2026-10-10

### Fixed

- **Breaking:** `GroupBy.GroupedRevenueQuery.Result` now computes `share_of_all_lines` as the specification defines it. `count` with `over: "all"` counts the 4 rows left by `where`, not the 3 left by `having`, so the shares are 0.5 and 0.25 instead of 0.6666666666666666 and 0.3333333333333333 (#28).

## [0.1.0-preview.1.0.0] - 2026-10-10

### Changed

- **Breaking:** rebuilt the catalogue for `PureQL.CSharp.Model` 0.1.0-preview.12.0.0, which models PureQL specification 0.1.0-preview.1.0.0. A sample's `Value` is now a `PureQLQuery`, and every previous sample is removed.
- The catalogue now mirrors the specification's 56 samples, one sample each, rewritten over the `schema_with_foreign_keys` and `audit` fixtures and grouped in `Select`, `Parameters`, `Where`, `OrderBy`, `Pagination`, `Expressions`, `Temporal`, `Nulls`, `Joins`, `Aggregates`, `GroupBy` and `Subqueries`.
- Expected results follow the specification's semantics (`null` equals `null`, NULLs sort first ascending, `sum` over no rows is 0, …). `integer` columns are `LongColumnType`, and `decimal` answers are rounded to 28 significant digits before `double`. 51 samples carry a result, and the 5 that hold a parameter do not.
- Tests serialize through `PureQL.CSharp.Model.Serialization` 0.1.0-preview.4.0.0.

## [0.1.0-preview.0.1.0] - 2026-09-23

### Added

- The full query sample catalogue, covering select, where (scalar and per-row), joins, group by, aggregates, order by, pagination, combined-clause, type & null-semantics, parameter, and error-path queries.
- A plain-English XML summary for every sample, describing what it selects, from where, and each clause in order.
- Expected PostgreSQL 17 result rows for every sample whose query can be answered in SQL, for use as a serialization oracle in downstream tests.
- A README describing the catalogue's conventions and listing every included sample.
