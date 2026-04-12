# Ozz Trade Diary

A Windows desktop trade journaling application for tracking trades across multiple markets — crypto, stocks, forex, futures, options, commodities, and more.

> **Status**: Pre-release development (no public release yet)
> 
> **Internal tracking versions**: `OzzTradeDiary` `0.0.31`, `OzzTradeDiary.WPF` `0.0.31`, `OzzTradeDiary.SQLite` `0.0.31`, `OzzTradeDiary.i18n` `0.0.31`

## Changelog

See [`CHANGELOG.md`](CHANGELOG.md) for release history.

## Third-Party Assets

- This project uses icon paths derived from **Bootstrap Icons**.
- Source: `https://icons.getbootstrap.com`
- Project repository: `https://github.com/twbs/icons`
- Version: `v1.13.1`
- License: `MIT`
- SPDX License Identifier: `MIT`

## Features

- DPI-aware multi-monitor window positioning
- `AppSettings` uses a debug-safe default data location: in `DEBUG` builds database and related app data resolve under repo-root `SampleData` (git-ignored), while release builds continue to use user app-data folders
- `Exchange.HasAnySymbol` is carried through the model, schema, repositories, seed data, localization, codegen, and maintenance UI for exchange/symbol linkage
- `AppSettings.UiCulture` stores the user's preferred BCP-47 culture name (e.g. `"en-US"`, `"tr-TR"`); applied to `CurrentUICulture` and `CurrentCulture` in `App.OnStartup` before `MainWindow` is created — when empty the OS culture is used
- `MainWindow` menu items are fully localized via `{x:Static i18n:ActionStrings.*}` and `{x:Static i18n:LocalizedStrings.*}`
- Repository class names in `TD.SQLite` use concise readable names such as `SymbolRepository` and `ExchangeRepository`; generated repository interfaces are co-located and use `I<Entity>Repository` naming (for example `ITradingAccountRepository`, `ISymbolRepository`)
- Repository lookup APIs use nullable parameters (`int?`, `string?`) for `GetByIdAsync`/`GetBy*Async` and return `null` for `null` inputs for safer call paths
- Generated repositories now support `OnLoaded` partial hooks in addition to create/update hooks for post-load extensibility
- `Trade` navigation properties can be generated with `AutoLoad=true`, allowing related collections to be populated automatically after entity load
- `TradeRepository.OnLoaded` now populates `TradeImages`, `EntryOrders`, `TakeProfitOrders`, and `StopLossOrders`
- `TradeRepository` now uses injected order and image repositories to load related collections
- `MetadataRepository` uses a singleton `GetInstance()` pattern so all repositories and WPF services share the same metadata access and database connection string source of truth
- Repository base classes use shared `GetOpenConnection()` / `GetOpenConnectionAsync()` helpers, with `ClearRecordCountCache()` used after create/delete/seed operations
- Date and time values are stored as `TEXT` in SQLite scripts, with `UpdatedAt` columns aligned to text-based handling
- Domain enums wired in models (including `OrderType` and `TradeDirection`)
- String validation and generated resources now use `MaxStringLength` / `MinStringLength` metadata where applicable
- `TD.Extensions.EnumExtension` shared helper in `OzzTradeDiary` for building enum value collections and reading localized `Display` attribute text for UI bindings across WPF and future platform frontends
- Shared text helpers were moved from the WPF app into `OzzTradeDiary` so they can be reused by platform-agnostic and future UI projects
- Generated SQLite schema includes `OrderType` for `EntryOrder`, `TakeProfitOrder`, and `StopLossOrder`
- Repositories implemented: `Currency`, `Exchange`, `TradingAccount`, `Symbol`, `Trade`, `TradeImage`, `EntryOrder`, `StopLossOrder`, `TakeProfitOrder`
- Added full CRUD repository support and entity mapping for `Trade`, `EntryOrder`, `StopLossOrder`, and `TakeProfitOrder`
- Generated SQL index definitions were updated for better query performance, especially in `Trades` and `TradeImages`
- Repository implementations now share a generic base class, with minor `ModelValidator` cleanup and repository refactoring
- SQLite insert/update repository paths now handle nullable values more reliably, persisting SQL `NULL` where appropriate
- `Exchange` includes nullable `DefaultCurrency`, persisted in SQLite as SQL `NULL` when not set
- `AbstractDatabaseRepository` base class provides shared `ValidateOrThrow` helper called in `CreateAsync`/`UpdateAsync` — throws `ValidationException` with all DataAnnotations error messages
- `AbstractDatabaseRepository` centralizes SQLite connection handling, record-count cache invalidation, SQL script execution, and async transaction helpers for future multi-step repository operations
- `AbstractDiaryVM` base ViewModel consolidates repository initialization and CRUD operations shared across ViewModels
- `AbstractEditVM` now contains the `IIsDirty` contract, and the standalone `IIsDirty.cs` file has been removed
- `ModelValidator` shared utility in `TD.Validation` for DataAnnotations-based model validation reusable by WPF, MAUI, and ASP.NET
- Dedicated localization project `OzzTradeDiary.i18n` with generated resources: `ActionStrings`, `CommonStrings`, `ErrorStrings`, `LocalizedStrings`, `MessageStrings` (`default` + `tr`)
- Turkish translations and localization code generation vocabulary were improved so generated resources stay aligned
- Model classes apply localized DataAnnotations using `TD.i18n` resources for display names and validation messages
- Maintenance window accessible from menu, with singleton window management (bring-to-front if already open)
- Maintenance window provides Add, Edit, Save, Refresh, and Delete (Exchange) CRUD operations for Currency, Exchange, TradingAccount, and Symbol
- `MaintenanceWindow` now includes `HasAnySymbol` exchange handling and updated symbol grid behavior
- `MaintenanceWindow` Symbols tab supports search and exchange-based filtering with localized placeholder text
- `MaintenanceWindow` edit flows have been polished for a more consistent maintenance experience
- `CurrencyCreate`/`CurrencyEdit`, `ExchangeCreate`/`ExchangeEdit`, and `SymbolCreate`/`SymbolEdit` dialogs with dedicated view models integrated into maintenance flows
- `SymbolCreate` market type ComboBox shows localized display text from enum `Display` attributes instead of blank items
- `AbstractEditView` base window provides shared unsaved-changes confirmation behavior for edit dialogs
- `TradingAccountEdit`, `ExchangeEdit`, and `CurrencyEdit` inherit from `AbstractEditView` for consistent `Yes/No/Cancel` close handling when there are pending changes
- `DeleteExchangeCommand` with `CanExecute` safety checks (disabled when exchange is referenced by Symbols or TradingAccounts) and `Yes/No` confirmation before deletion
- `TradingAccountCreate` dialog (renamed from `CreateTradingAccount`) and `TradingAccountCreateVM` (renamed from `CreateTradingAccountVM`) with icon-based action buttons, live per-field validation, inline error display, and OK button enabled only when all required fields are valid
- `TradingAccountCreate` and maintenance grids display `ExchangeCode` instead of numeric `ExchangeId`
- `AccountCode` property removed from `TradingAccount` model and generated DDL
- Shared validation styles `ValidationTextBoxStyle` and `ValidationComboBoxStyle` for consistent inline error display across forms
- `ReadOnlyTextBoxStyle` for non-editable fields with darker background cue; read-only bindings use `Mode=OneWay`
- `TradingAccountEdit` dialog for editing mutable fields (`DisplayOrder`, `IsActive`, `Notes`) of existing trading accounts; immutable fields (`Title`, `ExchangeCode`) displayed as read-only
- `TradeImage` repository added with improved structure and documentation
- Generator execution order in `OzzTradeDiary.OzzGen` is kept aligned with repository and schema dependencies for generated hooks and auto-loading behavior
- About dialog with auto-close on deactivation, high-resolution icon rendering, and GitHub link
- Bootstrap Icons rendered as window title bar icons via `WindowExtensions`
- Application version displayed in `MainWindow` title bar via `AppVersion`

## Planned

- Remaining model repository classes in `OzzTradeDiary.SQLite`
- Functional data layer repositories and query logic
- Multi-market support: Stock, Forex, Crypto, Futures, Options, Commodities, and more
- Trade tracking with entry/exit orders, stop-loss, and take-profit levels
- Long and short trade directions
- Multiple order types: Market, Limit, Stop, StopLimit, TrailingStop
- Trade images with chart attachments and notes
- Multi-account and multi-exchange support
- Automatic SQLite database backup with ZIP archiving

## Prerequisites

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- Windows 10 (build 19041) or later

## Getting Started

```bash
# Clone the repository
git clone https://github.com/your-username/OzzTradeDiary.git
cd OzzTradeDiary

# Build
dotnet build OzzTradeDiary.slnx

# Run
dotnet run --project OzzTradeDiary.NET/OzzTradeDiary.WPF/OzzTradeDiary.WPF.csproj
```

## Project Structure

View and ViewModel files use **entity-first, verb-last** naming (e.g., `TradingAccountCreate.xaml`, `TradingAccountCreateVM.cs`) so that files for the same entity sort together in Solution Explorer. Method names remain **verb-first** (e.g., `CreateTradingAccount()`, `DeleteExchange()`).

```
OzzTradeDiary.slnx
├── OzzTradeDiary.NET/
│   ├── OzzTradeDiary/            # Core models, shared helpers, validation (TD / TD.Extensions / TD.Validation) — platform-independent
│   │   ├── Models/               # Trade, TradeImage, Symbol, Exchange, Currency, Orders, etc.
│   │   ├── Extensions/           # Shared enum/text helpers reusable outside WPF
│   │   └── Validation/           # Shared DataAnnotations-based validation (TD.Validation.ModelValidator)
│   ├── OzzTradeDiary.i18n/       # Localization resources (TD.i18n namespace) — platform-independent, generated by OzzCodeGen/OzzGen
│   │   ├── *Strings.resx         # Base resources (Action/Common/Error/Localized/Message)
│   │   └── *Strings.tr.resx      # Turkish resources
│   ├── OzzTradeDiary.SQLite/     # Data access (TD.SQLite namespace) — platform-independent
│   │   ├── DbScripts/            # Generated SQL DDL scripts (one per model in OzzTradeDiary/Models)
│   │   └── Repositories/         # Data access repositories (one per entity)
│   └── OzzTradeDiary.WPF/        # Desktop UI (TD.WPF namespace) — Windows only
│       ├── Commands/             # ICommand implementations
│       ├── Extensions/           # WPF-specific helpers
│       ├── Models/               # App settings, versioning, window positioning
│       ├── Resources/            # XAML styles, Bootstrap Icons
│       ├── Services/             # Database backup, auto-backup
│       ├── ViewModels/           # MVVM view models
│       │   └── Maintenance/      # ViewModels for maintenance screens (TD.WPF.ViewModels.Maintenance)
│       └── Views/                # XAML views
│           └── Maintenance/      # Maintenance window and related views (TD.WPF.Views.Maintenance)
└── OzzCodeGen/                   # Code generation settings
    ├── SqliteScriptsGen.settings # SQLite DDL generation config
    ├── ResourceGen.settings      # Localization resource generation config
    └── Vocabulary/               # English/Turkish translation files
```

## Architecture

Four-project MVVM architecture layered for platform portability:

| Project | Target | Role |
|---------|--------|------|
| **OzzTradeDiary** | `net10.0` | Core data models, shared helpers such as `TD.Extensions.EnumExtension` and shared text extensions, and shared validation utilities (for example `TD.Validation.ModelValidator`) with no platform dependencies |
| **OzzTradeDiary.i18n** | `net10.0` | Shared localization resources (`*.resx`/designer classes) generated from OzzCodeGen/OzzGen inputs |
| **OzzTradeDiary.SQLite** | `net10.0` | Data access layer — raw SQLite via Microsoft.Data.Sqlite, repository pattern |
| **OzzTradeDiary.WPF** | `net10.0-windows10.0.19041.0` | WPF desktop frontend — views, view models, commands, services |

Core models, localization resources, and data access are platform-independent, enabling future frontends (MAUI, web, etc.) to reuse them.
Model classes localize display metadata and validation messages through DataAnnotations that reference `TD.i18n` resource types.
Shared enum and text helper logic lives in `TD.Extensions` so UI projects such as WPF and future MAUI frontends can reuse the same behavior.

## SQLite Repository and Script Conventions

- Model names are singular; related table names are plural.
- Each model has a generated DDL file in `OzzTradeDiary.SQLite/DbScripts` with the same model name and `.sql` suffix.
- Some models have generated seed files named `<PluralTableName>-Data.sql` in `OzzTradeDiary.SQLite/DbScripts`.
- DDL and seed files are generated by `OzzCodeGen`; do not edit manually.
- Each repository CUD operation updates metadata timestamp via `SaveLastUpdateUtcAsync` in `SqliteDatabaseMetadataRepository`.
- `MetadataRepository` is shared via a singleton `GetInstance()` method; repositories use it internally and WPF ViewModels/services do not instantiate it directly.
- When adding a generated seed file `<PluralTableName>-Data.sql`, the corresponding repository `InitializeDatabase()` should call `SqliteDbScriptInitializer.SeedIfEmpty(connection, "<PluralTableName>", "<PluralTableName>-Data.sql")`.
- Generated repositories can expose `OnLoaded` partial hooks for post-load behavior, especially when navigation properties are configured with `AutoLoad=true`.
- `TradeRepository` may use injected related repositories to populate `Trade` navigation collections during `OnLoaded`.
- Immutable repository keys:
  - `Currency.CurrencyTicker`
  - `Exchange.ExchangeCode`
  - `Symbol.TickerFull`
- Nullable text fields such as `Exchange.DefaultCurrency` should be stored as SQL `NULL`, not the literal string `"null"`.

## Database Initialization and Seed Data

At repository startup, initialization executes generated DDL scripts and applies baseline seed scripts only when target tables are empty.

- Seeding is idempotent and uses `SqliteDbScriptInitializer.SeedIfEmpty(...)`.
- Current generated seed scripts include:
  - `OzzTradeDiary.SQLite/DbScripts/Currencies-Data.sql`
  - `OzzTradeDiary.SQLite/DbScripts/Exchanges-Data.sql`
  - `OzzTradeDiary.SQLite/DbScripts/Symbols-Data.sql`

## Dependencies

| Package | Version | Project |
|---------|---------|---------|
| Microsoft.Data.Sqlite | 10.0.5 | OzzTradeDiary.SQLite, OzzTradeDiary.WPF |

## Code Generation

SQLite DDL scripts and localization resources are generated by **OzzCodeGen** — a separate code generation tool. Configuration lives in `OzzCodeGen/`:

- `SqliteScriptsGen.settings` — maps C# models to SQLite CREATE TABLE scripts in `DbScripts/`
- `SqliteRepositoryGen.settings` — generates SQLite repository classes and interfaces
- `CsModelClassCodeEngine.settings` — uses `CSharpModelClassCodeEngine` as the root element for generated C# model class settings
- `ResourceGen.settings` — generates localization resources from vocabulary XML files
- `Vocabulary/` — bilingual term definitions (English/Turkish)
- `LocalizedStrings.resx` is sourced from `OzzTradeDiary.OzzGen`
- Model classes and database schema generation are sourced from `OzzTradeDiary.OzzGen` (generator name: `CS_Model_Class_Generator`)
- Generated model metadata includes DataAnnotations that use `TD.i18n` resources for UI display names and validation messages
- Generated repository interfaces are co-located inside repository class files (`I<Entity>Repository`)
- Generated repository hooks and auto-loading behavior depend on generator settings and execution order in `OzzTradeDiary.OzzGen`
- Custom repository behavior should be added in companion partial files (for example `SymbolRepository.part.cs` or `TradeRepository.part.cs`)

> **Do not edit generated files manually** (`DbScripts/*.sql`, generated `.resx`, generated `.Designer.cs`, and generated repository/model/schema outputs) — they are overwritten on regeneration.

## License

[MIT License](LICENSE)
