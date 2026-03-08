# Project Guidelines

## Overview

Early-stage development (v0.0.2).

## Architecture

**MVVM pattern** with three projects, layered for platform portability:

| Project | Namespace | Role | Platform |
|---------|-----------|------|----------|
| `OzzTradeDiary` | `TD` | Core data models (plain C# classes) | Platform-independent |
| `OzzTradeDiary.SQLite` | `TD.SQLite` | Data access — raw SQLite via Microsoft.Data.Sqlite | Platform-independent |
| `OzzTradeDiary.WPF` | `TD.WPF` | Desktop UI — views, view models, commands, services | Windows only |

**Data flow**: Models (`TD`) → SQLite repositories (`TD.SQLite`) → Services (`TD.WPF`) → ViewModels → XAML Views

**No ORM** — uses Microsoft.Data.Sqlite directly. SQL DDL scripts live in `OzzTradeDiary.SQLite/DbScripts/` and are code-generated from `OzzCodeGen/SqliteScriptsGen.settings`.

**Platform strategy**: Core models (`TD`) and data access (`TD.SQLite`) target `net10.0` (cross-platform). Only the WPF project targets `net10.0-windows10.0.19041.0`. Keep business logic and data access out of the WPF project so future platform frontends (MAUI, web, etc.) can reuse them.

## Build and Test

- **Framework**: .NET 10.0, C# with nullable reference types and implicit usings
- **Build**: `dotnet build OzzTradeDiary.slnx`
- **Run**: `dotnet run --project OzzTradeDiary.NET/OzzTradeDiary.WPF/OzzTradeDiary.WPF.csproj`

## Code Style

### Naming

- **Namespaces**: Short — `TD`, `TD.WPF`, `TD.SQLite`
- **Classes/Properties**: PascalCase
- **Private fields**: `_camelCase`
- **ViewModel suffix**: `VM` (e.g., `AbstractCollectionVM<T>`, `AbstractDataErrorInfoVM`)

### Models (TD namespace)

- Every model has an `int Id` primary key
- Include a `Clone()` method that copies all properties except PKs/navigation properties
- Navigation properties use `virtual` collections
- Use XML documentation comments on properties

### ViewModels (TD.WPF namespace)

- Inherit from `AbstractViewModel` (provides `INotifyPropertyChanged` via `RaisePropertyChanged()`)
- Validation: extend `AbstractDataErrorInfoVM` (implements `INotifyDataErrorInfo`)
- Collections: extend `AbstractCollectionVM<T>` (provides `ObservableCollection<T>`, filtering, selection)
- Commands: extend `AbstractCommand` (implements `ICommand`)

### SQLite (TD.SQLite namespace)

- Repository pattern — one repository per entity
- DDL scripts in `DbScripts/` folder are **generated** by OzzCodeGen — do not edit manually
- SQLite types: `INTEGER` (ints, dates as ticks), `REAL` (decimals), `TEXT` (strings)
- Dates stored as ticks (`INTEGER`), not ISO 8601 text

## Conventions

- **Singleton pattern** for `AppSettings` (lazy-loaded)
- **Localization**: Bilingual (English/Turkish) — planned; vocabulary XML files scaffolded in `OzzCodeGen/Vocabulary/` but not yet implemented
- **Code generation**: OzzCodeGen generates SQLite DDL scripts and localization resources — settings files in `OzzCodeGen/` define mappings
- **Backup**: SQLite backup via `BackupDatabase` API → ZIP archives with timestamps
- **Window state**: DPI-aware multi-monitor positioning via WinAPI (`WindowPosition`)
- **Database path**: Default is `{AppData}/taxpayers.db`

## Key Enums

- `MarketType`: Stock, Fund, Futures, Forex, Option, Commodity, Crypto, CryptoPerpetual, Index
- `TradeDirection`: Long (200), Short (100)
- `EntryMethod`: Market, Limit
- `OrderType`: Market, Limit, Stop, StopLimit, TrailingStop
