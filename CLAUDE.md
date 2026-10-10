# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

All `dotnet` commands must be run from the `./src` directory.

```bash
dotnet restore
dotnet tool restore
dotnet build --no-restore -warnaserror
dotnet format --verify-no-changes              # check code style (CI enforces this)
dotnet csharpier check .                       # check code style (CI enforces this)
dotnet format && dotnet csharpier format .     # auto-fix code style
dotnet test --no-build --verbosity normal --logger trx --collect:"XPlat Code Coverage"
dotnet stryker --mutation-level Complete       # mutation testing (CI enforces this)
dotnet pack --configuration Release -p:Version=<version> --output .
```

## Architecture

This is a **sample-data NuGet library**: a catalogue of named, predefined PureQL `PureQLQuery` instances used as fixtures by other repositories in the ecosystem. There is no logic here — every type is a sealed record with parameterless construction and a hard-coded query.

**Spec parity.** The catalogue mirrors the `samples/` of PureQL specification `0.1.0-preview.1.0.0` (`kudima03/PureQL-Specification`), the version `PureQL.CSharp.Model` 0.1.0-preview.12.0.0 models: one sample per specification sample, rewritten over the fixture tables of `Pure.RelationalSchema.Samples` so it can be executed, keeping the feature the specification sample demonstrates. The README's catalogue table maps every sample to its specification number.

**One sealed record per file, under `Queries/<Folder>`.** The folders group samples by what they demonstrate (`Select`, `Parameters`, `Where`, `OrderBy`, `Pagination`, `Expressions`, `Temporal`, `Nulls`, `Joins`, `Aggregates`, `GroupBy`, `Subqueries`). A sample's namespace is `PureQL.CSharp.Model.Samples.Queries.<Folder>`.

**Sample shape.** `PureQL.CSharp.Model.PureQLQuery` is a sealed class with no interface, so a sample cannot implement it. Each sample instead exposes `public PureQLQuery Value => new PureQLQuery(...);` and is used as `new FooQuery().Value`. CA1822 is switched off for `Queries/**` in `src/.editorconfig` because of this — keep `Value` an instance member.

**Documentation.** `GenerateDocumentationFile` is on, so every public type and member needs an XML doc comment — a missing one is CS1591, an error under `-warnaserror`. A sample's `<summary>` describes the query in plain English (what it selects, from where, and each clause in order); `Value` carries the one-line `Builds the query afresh on every read.`

**Naming.** A sample is named after what the query *asks*, never after what an engine is expected to answer (`HavingCountQuery`, not `…KeepsTwoUsers`). Every name ends in `Query` and is unique across all folders.

**The catalogue is deduplicated.** Before adding a sample, check that no existing sample already serializes to the same document.

**Everything is public API.** Consumers reference individual samples, so renaming a sample or changing its query is a breaking change for every repository that asserts against it.

**Determinism is the contract.** Samples must never use randomness, ambient state, time or culture-sensitive formatting. Two instances of the same sample must always serialize to the same PureQL document.

**Entity and field names are never literals.** They are derived from `Pure.RelationalSchema.Samples` metadata: `new JoinedString(new DotString(), [new RelationalSchemaWithForeignKeys().Name, new OrdersTable().Name]).TextValue` for an entity (and for a field's `source` when the entity has no alias), `new OrderTotalColumn().Name.TextValue` for a field. The only string literals in a query are aliases, subquery names and their column aliases, parameter names and value scalars. Uuid scalars use the fixtures' deterministic form `new Guid(n, 0, 0, new byte[8])`.

**Field types.** A field is declared with the PureQL type of its storage column: `DoubleColumnType` → `decimal`, and the other column types by name (`BoolColumnType` → `boolean`). It is nullable where the fixture column holds a NULL (`users.user_score`, `employees.employee_manager_id`) and where the specification makes it nullable after an outer join. No fixture column is an `integer`; integers come from `round`/`floor`/`ceiling`, `count`, `dateDiffDays`, `integerDivide`, `modulo` and literals.

**`PureQL.CSharp.Model.Pagination` must be aliased.** Inside `PureQL.CSharp.Model.Samples.Queries.*` the simple name `Pagination` binds to the `Queries.Pagination` namespace, so samples write `using ModelPagination = PureQL.CSharp.Model.Pagination;`.

**Expected results.** A sample whose query can be executed (every sample without a parameter) also has `public IStoredTableDataSet Result`: a `StoredTableDataSet` over `new Table(new EmptyString(), [columns], [])` and `Row`s of `InvariantCell`s (NULL → `EmptyCell`). It is hard-coded in the same style as the query: one column per `select` item, named by its alias and typed by its PureQL type (`integer` → `LongColumnType`, `decimal` → `DoubleColumnType`, …); a column reuses the `Pure.RelationalSchema.Samples` column when name and type match, and is otherwise `new Column(new String(<alias>), new XColumnType())`. The rows are what PostgreSQL 17 returns for the query translated to SQL over the `Pure.RelationalSchema.Storage.Samples` fixture rows, the translation spelling out the specification's semantics where SQL differs (`equal` is `IS NOT DISTINCT FROM`, NULLs sort first ascending, `sum` over no rows is 0, …), with exact `numeric` arithmetic rounded to 28 significant digits and then to `double`, and the `C` collation. The README lists every convention and every sample without a result, with the reason. **Never derive a result from a PureQL engine.** Engines are tested against these results, so a result must come from the specification's semantics. If a fixture value in `Pure.RelationalSchema.Storage.Samples` changes, recompute the affected results.

**`Guid`/`DateTime` aliases.** A file whose `Result` holds uuid or datetime cells aliases `Guid = Pure.Primitives.Guid.Guid` / `DateTime = Pure.Primitives.DateTime.DateTime`, the same as `Pure.RelationalSchema.Storage.Samples`. Its query then writes `new System.Guid(…)`; `datetime` literals are `DateTimeOffset`s and need no alias.

**Dependencies:** `PureQL.CSharp.Model` for the AST; `Pure.RelationalSchema.Samples` for the names; `Pure.Primitives` (`DotString`) and `Pure.Primitives.String.Operations` (`JoinedString`) to compose them; `Pure.RelationalSchema.Storage.Samples`, `Pure.RelationalSchema`, `Pure.RelationalSchema.Storage`, `Pure.RelationalSchema.HashCodes` and `Pure.Collections.Generic` for the results. Do **not** reference any query engine (`Pure.RelationalSchema.Storage.PureQL.Projection`). Engines consume these samples, and a reference would create a cycle.

**Multi-targeting:** net8.0, net9.0, net10.0. `IsAotCompatible` is set.

**Tests:** xUnit project targeting net10.0, mirroring the source layout one `…Tests` record per sample. Each test serializes the sample through `PureQL.CSharp.Model.Serialization` (`QueryJson`) and compares it with the expected PureQL document spelled out as a raw string literal (`ExpectedJson`). The expected document is written by hand after the specification sample and validated against `PureQL-Specification.json`, so it is an independent oracle — never regenerate it from the sample under test. A sample with a `Result` has a second test, which renders the result through `DataSetJson` (table name, columns with types, index count, cell text per row) and compares it with the expected document computed from the SQL answer.

**CI thresholds** (`.github/workflows/build-and-test.yml`): line coverage 98 (warning at 99) and mutation score 98. The repository sits at 100 % line coverage and 100 % mutation score — the JSON comparison sees every string, collection and flag a mutant could change. This repository does not use `Stryker disable` comments; a real coverage or mutation gap is fixed by writing a test, not annotated away.

**Publishing:** triggered by pushing a semver tag matching `*.*.*`. The tag name becomes the package version. Packages are published to both GitHub Packages and NuGet.org.

## Code Style

Enforced via `.editorconfig` and `dotnet format` + `csharpier` in CI:

- No `var` — always use explicit types
- No expression-bodied methods or constructors — use block bodies
- Properties and indexers use expression bodies (`=>`)
- File-scoped namespaces (`namespace Foo.Bar;`)
- No implicit object creation when the type is not apparent — `new Foo()`, not `new()`
- Private fields: `_camelCase`
- Max line length: 90 characters
- Pass `null` for an absent query clause by name (`joins: null`), so the positional clauses around it stay readable
- Do **not** override `ToString()`/`GetHashCode()` — the *Samples* convention

## Commit Messages

Do not mention Claude or AI assistance in commit messages.
