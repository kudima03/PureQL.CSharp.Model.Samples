# PureQL.CSharp.Model.Samples

Named, predefined PureQL queries — one sealed record per query — written against the stored data sets of `Pure.RelationalSchema.Storage.Samples`.

[![.NET build & test](https://github.com/kudima03/PureQL.CSharp.Model.Samples/actions/workflows/build-and-test.yml/badge.svg?branch=main)](https://github.com/kudima03/PureQL.CSharp.Model.Samples/actions/workflows/build-and-test.yml)
[![Build and Deploy](https://github.com/kudima03/PureQL.CSharp.Model.Samples/actions/workflows/publish-nuget.yml/badge.svg?branch=main)](https://github.com/kudima03/PureQL.CSharp.Model.Samples/actions/workflows/publish-nuget.yml)
[![NuGet](https://img.shields.io/nuget/v/PureQL.CSharp.Model.Samples)](https://www.nuget.org/packages/PureQL.CSharp.Model.Samples)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

## Overview

`PureQL.CSharp.Model.Samples` provides a fixed, deterministic catalogue of `PureQL.CSharp.Model` `Query` instances. Every query reads from the query-grade fixtures of [`Pure.RelationalSchema.Storage.Samples`](https://github.com/kudima03/Pure.RelationalSchema.Storage.Samples) — six users, six orders, four products, four order items, four employees and four logins across the `schema_with_foreign_keys` and `audit` schemas — plus a handful of its shape-family data sets. Nothing is random, nothing is generated at run time, nothing reads the clock or the current culture.

The catalogue is ported from the [`Pure.RelationalSchema.Storage.PureQL.Projection`](https://github.com/kudima03/Pure.RelationalSchema.Storage.PureQL.Projection) test suite: each distinct query that suite executes against those fixtures appears here exactly once, under the name of what it asks rather than of what an engine is expected to answer. A query engine's tests can take a sample instead of rebuilding the AST inline, and a serializer, validator or pretty-printer gets the same wide corpus of real queries for free.

Entity and field names are never spelled as literals. They are derived from the `Pure.RelationalSchema.Samples` metadata the stored data sets are built from:

```csharp
new NumberField(
    new JoinedString(
        new DotString(),
        [new RelationalSchemaWithForeignKeys().Name, new OrdersTable().Name]
    ).TextValue,
    new OrderTotalColumn().Name.TextValue
)
// entity "schema_with_foreign_keys.orders", field "order_total"
```

so a rename in the schema fixtures carries through to every query that references it.

## Sample shape

`PureQL.CSharp.Model.Query` is a sealed record with no interface over it, so a sample cannot *be* a `Query` the way `UsersTable` is an `ITable`. Instead each sample is a sealed record with a parameterless constructor that exposes its query through `Value`:

```csharp
public sealed record SkipAndTakeQuery
{
    public Query Value => new Query(...);
}
```

`Value` builds a fresh `Query` on every read. Two instances of the same sample always produce the same PureQL document.

## Catalogue

`namespace PureQL.CSharp.Model.Samples.Queries.<Folder>`

The folders mirror the clause-oriented layout of the Projection test suite.

| Folder | Samples | Exercises |
|---|---|---|
| `Select` | 59 | Column projection and aliases, `DISTINCT` over every column type and over joins, scalar and literal-arithmetic columns, wide projections, and select lists an engine has to reject |
| `Where/Scalar` | 44 | Single-value predicates over constants: equality and range comparison for every value type, `and`/`or`/`not` up to five levels deep, De Morgan pairs, and arithmetic inside a comparison |
| `Where/Each` | 138 | Per-row (`each*`) predicates: equality and comparison for every value type, per-row arithmetic and date/time/datetime shifts and differences, literal-array operands, whole-array equality, nested boolean trees, and predicates over joined columns |
| `Where` | 9 | Filters over joined columns, and filters that match nothing, on their own and followed by `GROUP BY`, `HAVING` and pagination |
| `Joins` | 70 | Inner, left, right and full joins on key, composite, non-equi, negated, constant and uuid-literal conditions; cross-schema and chained joins; `FROM` aliases; and joins followed by every later clause |
| `GroupBy` | 84 | Grouping by every key type and by composite and joined keys, `HAVING` over every aggregate and comparison operator, nested `HAVING` trees, whole-set `HAVING`, and scalars and mixed projections in group mode |
| `Aggregates` | 60 | `count`, `sum`, `average`, `min` and `max` over every column type, per group and over the whole set, over per-row arithmetic and temporal expressions, over nullable columns, and across joins |
| `OrderBy` | 32 | Sorting by every column type in both directions, multi-key and mixed-direction orderings, ties, joined columns with padded rows, aggregates and aliases, and null fields |
| `Pagination` | 13 | `skip`/`take` windows: in range, past the end, beyond `int.MaxValue`, zero and negative values, and after `DISTINCT`, `GROUP BY` and joins |
| `Combined` | 40 | Several clauses at once — join, where, group by, having, order by, distinct and pagination — including every clause together with five-level predicate trees |
| `Types` | 39 | One column of each type, calendar and numeric extremes, uuid casing, and NULL semantics: nullable operands in comparisons, arithmetic and aggregates, three-valued `not`, NULL group keys and left-join padding |

### Data sets

A sample's `from` entity names the schema it reads from. The stored data sets that hold those schemas are:

| Entity prefix | Stored data set |
|---|---|
| `schema_with_foreign_keys.` | `SchemaDataSetWithForeignKeys` |
| `audit.` | `AuditSchemaDataSet` |
| `schema_without_foreign_keys.` | `SchemaDataSetWithoutRows`, `UuidCasingSchemaDataSet` |
| `schema_with_indexes.` | `SchemaDataSetWithAmbiguousIds` |
| `single_table_schema.` | `SingleTableSchemaDataSet` |

The Projection suite runs every query-family sample against `[new SchemaDataSetWithForeignKeys(), new AuditSchemaDataSet()]`.

### Well-formed is not the same as valid

Some samples are deliberately unanswerable — an entity no data set holds, a field the resolved table does not carry, a division by zero, a parameter with no binding. They are still well-formed `Query` values that serialize and round-trip; what they exercise is an engine's failure path.

## Dependencies

- [`PureQL.CSharp.Model` 0.1.0-preview.11.0.1](https://github.com/kudima03/PureQL.CSharp.Model/tree/0.1.0-preview.11.0.1) — the query AST every sample builds
- [`Pure.RelationalSchema.Samples` 0.1.0-preview.1.0.0](https://github.com/kudima03/Pure.RelationalSchema.Samples/tree/0.1.0-preview.1.0.0) — the schema, table and column fixtures entity and field names are derived from
- [`Pure.Primitives` 3.6.5](https://github.com/kudima03/Pure.Primitives/tree/3.6.5) — `DotString`, the `schema.table` separator
- [`Pure.Primitives.String.Operations` 1.5.1](https://github.com/kudima03/Pure.Primitives.String.Operations/tree/1.5.1) — `JoinedString`, which composes the entity name

`Pure.RelationalSchema.Storage.Samples` is not referenced: the queries only need the names, not the rows. It is what a consumer runs them against.

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
using PureQL.CSharp.Model.Samples.Queries.Pagination;

Query query = new SkipAndTakeQuery().Value;

// query.From.Entity == "schema_with_foreign_keys.orders"
// query.Pagination  == new Pagination(2, 2)
```

Executed against the matching stored data sets:

```csharp
using Pure.RelationalSchema.Storage.Abstractions;
using Pure.RelationalSchema.Storage.PureQL.Projection;
using Pure.RelationalSchema.Storage.Samples.SchemaDataSets;
using PureQL.CSharp.Model.Samples.Queries.Pagination;

IStoredTableDataSet result = new PureQLProjection(
    [new SchemaDataSetWithForeignKeys(), new AuditSchemaDataSet()],
    new SkipAndTakeQuery().Value
);
```

Serialized to a PureQL document:

```csharp
using System.Text.Json;
using System.Text.Json.Serialization;
using PureQL.CSharp.Model.Samples.Queries.Pagination;
using PureQL.CSharp.Model.Serialization;

JsonSerializerOptions options = new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
};

foreach (JsonConverter converter in new PureQLConverters())
{
    options.Converters.Add(converter);
}

string json = JsonSerializer.Serialize(new SkipAndTakeQuery().Value, options);
```
