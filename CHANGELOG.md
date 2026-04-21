# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/).

## [0.0.49] - 2026-04-21

### Changed
- Refactored generated lookup service contracts to use `Get<Entity>sAsync(bool isActive)` signatures and marked the interfaces as `partial` for future extensibility.
- Regenerated lookup service implementations with the new method signatures, updated code-generated file headers, and generated design-time mock implementations.
- Updated maintenance create ViewModels to use the regenerated lookup service APIs instead of the previous active-only method names.
- Regenerated abstract/base WPF ViewModel files with standardized codegen headers, formatting cleanups, and improved generated output consistency.
- Moved the `IIsDirty` contract inside `AbstractCreateEditVM` so create/edit dialog dirty-state behavior stays co-located with the shared base ViewModel.
- Refactored and reordered the Currency tab in `MaintenanceWindow.xaml` for a cleaner maintenance UI layout.
- Updated OzzCodeGen/MVVM settings to improve generated namespace structure, folder structure, and lookup service generation.
- Bumped all project versions to `0.0.49` (`OzzTradeDiary`, `OzzTradeDiary.WPF`, `OzzTradeDiary.SQLite`, `OzzTradeDiary.i18n`).

### Removed
- Removed redundant hand-written design-time lookup service stubs that are now replaced by regenerated design-time mock classes.

## [0.0.48] - 2026-04-20

### Added
- Added WPF lookup services `IExchangeLookupService`/`ExchangeLookupService` and `ICurrencyLookupService`/`CurrencyLookupService` to provide active exchange/currency collections for create dialogs.
- Added `ISymbolLookupService`/`SymbolLookupService` and `ITradingAccountLookupService`/`TradingAccountLookupService` for active symbol and trading-account lookup collections.
- Added empty lookup service implementations for design-time/default constructor scenarios.

### Changed
- `SymbolCreateVM` and `TradingAccountCreateVM` now load lookup data through injected lookup services instead of direct repository dependencies.
- Maintenance create-dialog composition now passes lookup-service instances when opening Symbol and TradingAccount create windows.
- Moved `ITradeRepository` composition to `App.OnStartup` and passed it through `MainWindow` into `MainWindowVM` constructor injection.

## [0.0.47] - 2026-04-20

### Added
- Introduced `WpfMvVmCodeEngine.settings` for WPF View/ViewModel/Command generation and updated `OzzTradeDiary.OzzGen` pipeline ordering to include the new MVVM generation stage.

### Changed
- Refactored core WPF ViewModel base classes to generated, `public`, and extensible implementations for cleaner reuse across feature ViewModels.
- Replaced `AbstractEditVM` with `AbstractCreateEditVM` and updated maintenance ViewModels to inherit from the new base class.
- Improved ViewModel property validation and null handling for create/edit flows.
- Made `AbstractEditView` abstract and simplified dirty-tracking behavior for edit dialogs.
- Updated code generation and repository settings to remove redundant fields and keep generated outputs aligned.
- Minor style and documentation cleanups across MVVM/WPF-related generated and companion code.
- Bumped all project versions to `0.0.47` (`OzzTradeDiary`, `OzzTradeDiary.WPF`, `OzzTradeDiary.SQLite`, `OzzTradeDiary.i18n`).

## [0.0.46] - 2026-04-18

### Added
- Added `GetDecimalFromText(ordinal)` to `TD.SQLite.Extensions.SqliteExtensions` for reading `TEXT`-stored decimals, replacing the obsolete `GetNullableDecimal`.
- Added `GetDecimalFromInteger(ordinal, scale)` to `TD.SQLite.Extensions.SqliteExtensions` for reading scaled-integer decimals; pairs with `AddDecimalToIntegerParameter` using the same scale.

### Changed
- Renamed `GetNullableDecimal` → `GetDecimalFromText` across all repository decimal field mappings for consistency with the write-side naming convention (`AddDecimalToTextParameter` / `AddDecimalToIntegerParameter`).
- Updated all repository decimal read mappings to use `GetDecimalFromText` or `GetDecimalFromInteger` as appropriate.
- Minor formatting improvements across repository files.
- Bumped all project versions to `0.0.46` (`OzzTradeDiary`, `OzzTradeDiary.WPF`, `OzzTradeDiary.SQLite`, `OzzTradeDiary.i18n`).

## [0.0.45] - 2026-04-18

### Added
- Added min/max range filters for position value, P/L, and risk to `TradeQueryParameters`: `PlannedPositionValueMin/Max`, `ExecutedPositionValueMin/Max`, `PlannedProfitLossMin/Max`, `RealizedProfitLossMin/Max`, and `PlannedRiskAmountMin/Max`.
- `SeedDemoData` tool now supports `--daysago` argument for flexible seeding across custom date ranges; batch scripts updated accordingly.

### Changed
- Implemented full-featured `GetPagedAsync` in `TradeRepository` and `SymbolRepository` with comprehensive filtering support; moved paging logic from partial files into main generated classes for cohesion.
- Refactored all repository parameter handling: replaced `AddWithValue` with type-safe `SqliteExtensions` helpers (`AddParameter`, `AddDateTimeToTextParameter`, `AddDecimalToTextParameter`, `AddDecimalToIntegerParameter`).
- Updated code generation settings to support new `TradeQueryParameters` search fields and paging support; adjusted codegen engine ordering for proper dependency flow.
- `TradeQueryParameters.HasAnySearchCriteria()` and copy constructor updated to include all new position-value, P/L, and risk filter properties.
- Removed obsolete `TradeQueryParameters.part.cs` file (functionality consolidated into generated `TradeQueryParameters.cs`).
- Minor repository code cleanups and consistent formatting across all repository implementations.
- Bumped all project versions to `0.0.45` (`OzzTradeDiary`, `OzzTradeDiary.WPF`, `OzzTradeDiary.SQLite`, `OzzTradeDiary.i18n`).

## [0.0.44] - 2026-04-18

### Changed
- Lowered crypto symbol `DisplayOrder` values in generated `Symbols-Data.sql` to improve UI grouping in symbol/account views.
- Refactored `OzzTradeDiary.Tools.SeedDemoData` seeding flow to generate more realistic crypto demo data.
- Enhanced demo symbol setup with `ADAUSD`, unique per-symbol display ordering, and realistic price generation backed by a crypto price dictionary.
- Demo seeding now creates two exchanges and two trading accounts, then distributes trades across a wider date range.
- Refactored `SeedTrades` to support flexible account/exchange/day-range/suffix targeting for richer reusable seeding scenarios.

## [0.0.43] - 2026-04-17

### Added
- Added `IsFullyClosed`, `PlannedProfitLoss`, `RealizedProfitLoss`, and `PlannedRiskAmount` fields to the `Trade` model as persisted domain fields.
- Renamed `PlannedEntry` → `PlannedEntryPrice` and `ExecutedEntry` → `ExecutedEntryPrice` in the `Trade` model for clearer domain intent.

### Changed
- Updated code generation settings, schema DDL, and indexes so the new and renamed `Trade` fields are aligned across model, repository mapping, and generated schema.
- Updated `TradeRepository` insert/update/select mappings and column ordering to reflect the renamed entry-price fields and new P/L, risk, and closed fields.
- Updated calculated position-value properties and any derived logic that referenced the old `PlannedEntry`/`ExecutedEntry` names.
- Updated localization resources and vocabulary inputs for `IsFullyClosed`, `PlannedEntryPrice`, `ExecutedEntryPrice`, `PlannedProfitLoss`, `RealizedProfitLoss`, and `PlannedRiskAmount`.
- Updated demo data seeding to populate the new and renamed trade fields.
- Updated default trade ordering in the repository to align with the new domain contract.
- Bumped all project versions to `0.0.43` (`OzzTradeDiary`, `OzzTradeDiary.WPF`, `OzzTradeDiary.SQLite`, `OzzTradeDiary.i18n`).

## [0.0.42] - 2026-04-17

### Added
- Added persisted `PlannedPositionValue` and `ExecutedPositionValue` fields to the `Trade` domain/model contract.
- Added generated SQLite schema support for `Trade.PlannedPositionValue` and `Trade.ExecutedPositionValue`, including index coverage for position-value querying.

### Changed
- Updated code generation settings and outputs so `Trade` model, schema, and repository mappings stay aligned for explicit position-value storage.
- Updated `TradeRepository` insert/update/select mapping and column ordering to include persisted planned/executed position value fields.
- Refactored repository parameterization to use `TD.SQLite.Extensions.SqliteExtensions` `SqliteCommand` extension helpers for safer typed parameter handling.
- Updated demo data seeding to populate `PlannedPositionValue` and `ExecutedPositionValue`.
- Bumped all project versions to `0.0.42` (`OzzTradeDiary`, `OzzTradeDiary.WPF`, `OzzTradeDiary.SQLite`, `OzzTradeDiary.i18n`).

## [0.0.41] - 2026-04-16

### Added
- Added calculated partial `OrderValue` / `FilledValue` properties for `EntryOrder`, `StopLossOrder`, and `TakeProfitOrder` so order value fields are derived instead of persisted.
- Added explicit SQLite decimal parameter helpers in `TD.SQLite.Extensions.SqliteExtensions`: `AddDecimalToIntegerParameter` and `AddDecimalToTextParameter`.

### Changed
- Removed persisted `OrderAmount` / `FilledAmount` fields from models and replaced them with calculated `OrderValue` / `FilledValue` partials for order entities.
- Excluded `OrderValue` / `FilledValue` from code-generated persisted fields so schema and repository generation no longer treat them as stored columns.
- Replaced `AddNullableParameter(decimal?)` usage with explicit decimal storage helpers to make integer-scaled and text-based decimal persistence intentional.
- Updated code generation engine order to preserve the correct dependency flow during regeneration.
- Bumped all project versions to `0.0.41` (`OzzTradeDiary`, `OzzTradeDiary.WPF`, `OzzTradeDiary.SQLite`, `OzzTradeDiary.i18n`).

## [0.0.40] - 2026-04-16

### Added
- Added code generation metadata for SQLite column names and decimal scales so generated schema and repository mappings can stay aligned.
- Expanded `TD.SQLite.Extensions.SqliteExtensions` with decimal-to-integer scaling helpers and nullable parameter support for precision-safe SQLite persistence.

### Changed
- Renamed `OrderAmount` and `FilledAmount` to `OrderValue` and `FilledValue` across models, repositories, localization resources, vocabulary files, and code generation settings.
- Updated SQLite schema so `OrderValue` and `FilledValue` are stored as scaled `INTEGER` values (scale 4), while price and quantity fields are stored as `TEXT` for improved precision.
- Refactored repository implementations and code generation to use the new column names, storage types, and decimal scaling behavior.
- Updated localization resources and vocabulary inputs to reflect the new value-based naming.
- Bumped all project versions to `0.0.40` (`OzzTradeDiary`, `OzzTradeDiary.WPF`, `OzzTradeDiary.SQLite`, `OzzTradeDiary.i18n`).

## [0.0.39] - 2026-04-15

### Added
- Introduced `SqliteExtensions` in `TD.SQLite.Extensions` to provide type-safe nullable parameter handling for SQLite commands, replacing the previous `AddNullableTextParameter` method with more flexible `AddNullableParameter` extension methods that support various data types.

### Changed
- Refactored nullable SQLite parameter handling by replacing `AddNullableTextParameter` with `AddNullableParameter` extension methods in `TD.SQLite.Extensions`.
- Expanded `SqliteExtensions` with type-safe nullable parameter support for repository insert/update command parameterization.
- Updated all repository usages to the new nullable parameter extensions.
- Bumped all project versions to `0.0.39` (`OzzTradeDiary`, `OzzTradeDiary.WPF`, `OzzTradeDiary.SQLite`, `OzzTradeDiary.i18n`).

## [0.0.38] - 2026-04-14

### Changed
- `TradeRepository.GetPagedAsync` now applies `TradeQueryParameters` range filters for `EntryTime`, `PlannedEntry`, `ExecutedEntry`, and `UpdatedAt` in addition to existing account/symbol/direction filtering.
- Added calculated range filters for position value in `TradeRepository.GetPagedAsync`:
  - planned position value: `(PlannedEntry * OrderQuantity)` via `PlannedPositionValueMin` / `PlannedPositionValueMax`
  - executed position value: `(ExecutedEntry * FilledQuantity)` via `ExecutedPositionValueMin` / `ExecutedPositionValueMax`
- `TradeRepository.GetPagedAsync` continues to support range filters for `EntryTime` and `UpdatedAt`, plus account/symbol/direction filtering.

## [0.0.37] - 2026-04-14

### Added
- Introduced `TradeQueryParameters` in `TD.Helpers` for typed trade filtering.

### Changed
- Refactored `QueryParameters`: moved from `TD.Validation` to `TD.Helpers`, and added XML documentation plus a copy constructor.
- Enhanced `CsModelClassCodeEngine.settings` to support per-entity query parameter generation and search-parameter marking.
- Bumped all project versions to `0.0.37` (`OzzTradeDiary`, `OzzTradeDiary.WPF`, `OzzTradeDiary.SQLite`, `OzzTradeDiary.i18n`).

## [0.0.36] - 2026-04-13

### Added
- Introduced `QueryParameters` in `TD.Validation` to support shared pagination/search query input patterns.
- Added `OnInitialized` partial method hooks to generated repository constructors for extensibility.

### Changed
- Updated generated code headers in model and validator files for improved clarity.
- Enabled query parameter class generation in codegen settings.
- Reordered generators in `OzzTradeDiary.OzzGen` to keep generated outputs aligned.
- Bumped all project versions to `0.0.36` (`OzzTradeDiary`, `OzzTradeDiary.WPF`, `OzzTradeDiary.SQLite`, `OzzTradeDiary.i18n`).

## [0.0.35] - 2026-04-13

### Changed
- Repository interfaces were refactored to `partial` so contracts can be extended across files without modifying generated source.
- Added new partial interface methods for navigation-loading and update flows in `ExchangeRepository`, `SymbolRepository`, and `TradeRepository` contracts.
- `ExchangeRepository` now invokes additional partial extensibility hooks (`OnLoaded`, `OnCreated`, `OnUpdated`) for cleaner separation of concerns in custom partial implementations.
- `ExchangeRepository` formatting and internal consistency were improved.
- No breaking changes; these updates focus on extensibility and maintainability.
- Bumped all project versions to `0.0.35` (`OzzTradeDiary`, `OzzTradeDiary.WPF`, `OzzTradeDiary.SQLite`, `OzzTradeDiary.i18n`).

## [0.0.34] - 2026-04-13

### Changed
- `ExchangeRepository` now requires `SymbolRepository` and `TradingAccountRepository` via constructor injection and no longer initializes those dependencies internally.
- `TradingAccountRepository` ownership/lifetime is now managed at the composition root.
- `OzzTradeDiary.Tools.SeedDemoData` and project documentation were updated to align with the new dependency-injection pattern.
- Minor formatting and changelog cleanup updates.
- Bumped all project versions to `0.0.34` (`OzzTradeDiary`, `OzzTradeDiary.WPF`, `OzzTradeDiary.SQLite`, `OzzTradeDiary.i18n`).

## [0.0.33] - 2026-04-13

### Added
- Added `Exchange.Symbols` and `Exchange.TradingAccounts` navigation collections and async loading support in `ExchangeRepository`.
- Added `OrderQuantity` and `FilledQuantity` fields to `Trade`, including repository mapping and schema updates.

### Changed
- `ExchangeRepository` constructor now accepts `SymbolRepository` and `TradingAccountRepository` dependencies explicitly to support navigation loading with clearer dependency wiring.
- `TradingAccountRepository` initialization responsibility was removed from `ExchangeRepository` initialization flow and is now managed externally through constructor dependency injection.
- Demo data seeding was improved with navigation-collection usage, realistic quantity generation, and weighted trade direction.
- Updated generator/resource/repository settings to include the new exchange navigation collections and trade quantity fields.
- Minor repository cleanup and seeding batch script improvements.
- Bumped all project versions to `0.0.33` (`OzzTradeDiary`, `OzzTradeDiary.WPF`, `OzzTradeDiary.SQLite`, `OzzTradeDiary.i18n`).

## [0.0.32] - 2026-04-12

### Added
- Added `OzzTradeDiary.Tools.SeedDemoData`, a small console tool for seeding demo data into the debug `SampleData\trades.db` SQLite database. Supports `--reset`, `--db <path>`, and defaults to the repo-root `SampleData` path.
- Added `Scripts/SeedDemoData.bat` as a convenience launcher for the demo-data seeding tool.
- Added `AppSettings.part.cs` to hold shared debug `SampleData` path resolution logic.

### Changed
- Demo seeding was expanded to generate data for a list of popular crypto symbols instead of a single hardcoded symbol.
- `SeedDemoData` now creates symbols dynamically from ticker strings and generates randomized trade values and multiple images per trade.
- Existing-demo checks were removed from the seeding flow so demo datasets are reseeded from scratch when run with reset.
- Workspace/script flow was updated so `Scripts/SeedDemoData.bat` resets the debug database before seeding.
- Bumped all project versions to `0.0.32` (`OzzTradeDiary`, `OzzTradeDiary.WPF`, `OzzTradeDiary.SQLite`, `OzzTradeDiary.i18n`).

## [0.0.31] - 2026-04-12

### Changed
- Enabled auto-loading for `Trade` navigation properties in generator settings by setting `AutoLoad=true` where applicable.
- Added generated `GetByIdAsync` lookup support and `OnLoaded` partial extensibility hooks to repositories.
- `TradeRepository` now implements `OnLoaded` to populate related navigation collections for `TradeImages`, `EntryOrders`, `TakeProfitOrders`, and `StopLossOrders`.
- `TradeRepository` now uses injected related repositories for order and image loading.
- Adjusted generator ordering in `OzzTradeDiary.OzzGen` to keep generated outputs aligned with the new repository behavior.
- Bumped all project versions to `0.0.31` (`OzzTradeDiary`, `OzzTradeDiary.WPF`, `OzzTradeDiary.SQLite`, `OzzTradeDiary.i18n`).

## [0.0.30] - 2026-04-12

### Added
- Added full CRUD SQLite repositories for `Trade`, `EntryOrder`, `StopLossOrder`, and `TakeProfitOrder`, including entity mapping support.

### Changed
- Updated generated SQL index definitions to improve query performance, with focus on `Trades` and `TradeImages` access patterns.
- Data access structure in `TD.SQLite` was expanded and aligned for improved maintainability and efficiency.
- Bumped all project versions to `0.0.30` (`OzzTradeDiary`, `OzzTradeDiary.WPF`, `OzzTradeDiary.SQLite`, `OzzTradeDiary.i18n`).

## [0.0.29] - 2026-04-05
