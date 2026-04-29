# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/).

## [0.0.59] - 2026-04-29

### Added
- Introduce AbstractAsyncCommand/IAsyncCommand for reusable async WPF commands
- Added AreYouSureToDelete localization (EN/TR) and update resources
- Added Load[Entity]InProgress flags to AbstractDiaryVM to prevent concurrent loads

### Changed
- Refactor DeleteExchangeCommand to async, use localized prompt, and disable when related data exists or is loading
- Remove obsolete exchange delete logic from MaintenanceWindowVM
- Minor namespace and using fixes

## [0.0.58] - 2026-04-29

### Changed
- Refactored the four `AbstractDiaryVM` save methods to persist only the single entity passed by the caller instead of all loaded maintenance entities, and updated the eight maintenance create/edit commands to use the new single-entity save flow.
- Update MaintenanceWindow XAML: remove Save buttons, adjust Edit/Refresh icons and layout.
- Add AppTitle and SaveOperationFailed localization keys (EN/TR); update .resx and designer files.
- Use localized error messages in maintenance commands.

## [0.0.57] - 2026-04-29
- Replaces code-behind event handlers in MaintenanceWindow with command classes for create/edit actions on Currency,
- Exchange, and Symbol. Adds new commands under TD.WPF.Commands.Maintenance and updates IWindowDialogService for dialog creation.
- MaintenanceWindowVM and XAML now bind to these commands, improving MVVM separation and button enablement logic.
- Removes obsolete code-behind methods and applies minor namespace corrections.
- No changes to business logic or data models.

### Changed

## [0.0.56] - 2026-04-29

### Changed
- Views `CurrencyCreate`, `CurrencyEdit`, `ExchangeCreate`, `ExchangeEdit`,  `SymbolCreate`, `SymbolEdit`, and `TradingAccountEdit`,  now blocks dialog confirmation when model validation fails and initializes create-VM validation state so the OK action stays disabled until required inputs are valid.
- Moved `CreateTradingAccountCommand` and `EditTradingAccountCommand` to `Commands/Maintenance/` subfolder for better project structure.
- Added `CreateCurrencyCommand`, `EditCurrencyCommand`, `CreateExchangeCommand`, `EditExchangeCommand`, `CreateSymbolCommand`, and `EditSymbolCommand` in `Commands/Maintenance/`, wired them to `MaintenanceWindowVM` and bound in `MaintenanceWindow.xaml`.
- Removed all remaining maintenance edit/create `_Click` code-behind methods from `MaintenanceWindow.xaml.cs`; all maintenance actions are now command-driven.

## [0.0.55] - 2026-04-29

### Changed
- Added a WPF-specific `IWindowDialogService` and `WindowDialogService` to centralize maintenance edit dialog creation and owner assignment (`Owner = this`).
- Updated `MaintenanceWindow` edit handlers (Currency, Exchange, TradingAccount, Symbol) to use `WindowDialogService` instead of directly constructing edit windows in code-behind.
- Replaced `EditTradingAccounts_Click` UI wiring with `EditTradingAccountCommand` in `MaintenanceWindow`, passing the owner window as command parameter while preserving existing edit/save-refresh behavior.
- Replaced `AddTradingAccount_Click` UI wiring with `CreateTradingAccountCommand` in `MaintenanceWindow`, passing the owner window as command parameter while preserving existing create/save-refresh behavior.
- Extended `IWindowDialogService` with TradingAccount create-dialog support and updated `CreateTradingAccountCommand` to use the dialog service instead of constructing `TradingAccountCreate` directly.
- `TradingAccountCreate` now blocks dialog confirmation when model validation fails and initializes create-VM validation state so the OK action stays disabled until required inputs are valid.

## [0.0.54] - 2026-04-27

### Changed
- Moved generated lookup service classes (`*LookupService`) from `TD.WPF.Services` to `TD.AppInfra.Services` for platform-agnostic reuse across WPF and future frontends.
- Updated `WpfMvVmCodeEngine.settings`: set namespace to `TD.WPF`, added `RepoContractNamespaceName`, renamed `ServiceFolder` to `LookupFolder`, and enabled `PutLookupInInfra` so generated lookup services are placed in `TD.AppInfra`.
- Added `TD.RepositoryContracts` project reference to `OzzTradeDiary.AppInfra.csproj` to support the new lookup service location.

## [0.0.53] - 2026-04-26

### Changed
- Moved repository contract interfaces from `TD.SQLite` to a new platform-agnostic `TD.RepositoryContracts` project.
- Updated references across AppInfra, WPF, and related projects to use `TD.RepositoryContracts` namespaces.
- Adjusted code generation settings and solution structure to align with the new repository-contract project layout.
- Bumped all project versions to `0.0.53` (`OzzTradeDiary`, `OzzTradeDiary.AppInfra`, `OzzTradeDiary.RepositoryContracts`, `OzzTradeDiary.WPF`, `OzzTradeDiary.SQLite`, `OzzTradeDiary.i18n`).
- Fixed a minor typo in a `MaintenanceWindow` event handler.

### Notes
- No repository implementation or business-logic behavior changes in this release.

## [0.0.52] - 2026-04-25

### Changed
- Bumped all project versions to `0.0.52` (`OzzTradeDiary`, `OzzTradeDiary.AppInfra`, `OzzTradeDiary.WPF`, `OzzTradeDiary.SQLite`, `OzzTradeDiary.i18n`).
- Added XML documentation comments to lookup service interfaces and WPF lookup service classes for clearer API usage.
- Moved mock lookup service implementations to `TD.AppInfra.Services` in dedicated files for better shared organization.
- Removed mock lookup service classes from the WPF project after moving them to AppInfra.
- Removed unused `using` directives from `SymbolCreate.xaml.cs` and `TradingAccountCreate.xaml.cs`.
- No functional behavior changes; this release focuses on code clarity and project organization.

## [0.0.51] - 2026-04-24

### Added
- Introduced `OzzTradeDiary.AppInfra` (`TD.AppInfra`) project for shared ViewModel and command base classes targeting `net10.0` for cross-platform reuse.

### Changed
- Moved shared ViewModel base classes (`AbstractViewModel`, `AbstractDataErrorInfoVM`, `AbstractCreateEditVM`, `AbstractCollectionVM`, `AbstractCommand`, etc.) from `TD.WPF` to `TD.AppInfra` with `public` visibility.
- Updated all WPF usages to reference `TD.AppInfra` base classes instead of local `TD.WPF` implementations.
- Updated project references: `OzzTradeDiary.WPF` and `TD.Tools.SeedDemoData` now reference `OzzTradeDiary.AppInfra`.
- Updated namespaces: moved base classes to `TD.AppInfra` namespace; WPF-specific derived classes remain in `TD.WPF` namespace.
- Aligned modularization toward future MAUI frontend: shared infra/base classes are now platform-independent and available for reuse.
- Bumped all project versions to `0.0.51` (`OzzTradeDiary`, `OzzTradeDiary.AppInfra`, `OzzTradeDiary.WPF`, `OzzTradeDiary.SQLite`, `OzzTradeDiary.i18n`).

## [0.0.50] - 2026-04-22

### Changed
- Bumped all project versions to `0.0.50` (`OzzTradeDiary`, `OzzTradeDiary.WPF`, `OzzTradeDiary.SQLite`, `OzzTradeDiary.i18n`).
- Renamed the demo-data seeding tool project and all related references from `OzzTradeDiary.Tools.SeedDemoData` to `TD.Tools.SeedDemoData`, including solution and batch-script references.
- Renamed WPF design-time/default lookup services from `Empty*LookupService` to `*MockLookupService` and updated all usages.
- Disabled code generation for TradingAccount/Symbol create/edit ViewModels.
- Updated internal tracking version comments to align with `0.0.50`.

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
