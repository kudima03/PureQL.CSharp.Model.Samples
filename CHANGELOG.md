# Changelog

All notable changes to `PureQL.CSharp.Model.Samples` are documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [0.1.0-preview.0.1.0] - 2026-09-23

### Added

- The full query sample catalogue, covering select, where (scalar and per-row), joins, group by, aggregates, order by, pagination, combined-clause, type & null-semantics, parameter, and error-path queries.
- A plain-English XML summary for every sample, describing what it selects, from where, and each clause in order.
- Expected PostgreSQL 17 result rows for every sample whose query can be answered in SQL, for use as a serialization oracle in downstream tests.
- A README describing the catalogue's conventions and listing every included sample.
