# Changelog

All notable changes to `PureQL.CSharp.Model.Samples` are documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

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
