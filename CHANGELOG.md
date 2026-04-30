# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/).

## [0.0.60] - 2026-04-29

### Added
- Introduce ISymbolCreationContext and implement in AbstractDiaryVM

### Changed
- Refactor MaintenanceWindowVM to construct and pass lookup services explicitly
- Add support for preselecting Exchange in symbol/account dialogs
- Update dialog service, commands, and constructors to accept Exchange
- Pre-populate exchange selection and suggest default account title
- Icon buttons in MaintenanceWindow now use dynamic styles for enabled/disabled/hover states, improving visual feedback.
- Error dialogs now use localized app title.
- Show inner exception details in error dialogs for better diagnostics
- Renamed all maintenance command classes to follow the {Entity}{Action}Command pattern for clarity and consistency.
- Added PreselectedExchange property to SymbolCreateCommand and TradingAccountCreateCommand to support exchange preselection in dialogs.
- Updated MaintenanceWindowVM and MaintenanceWindow.xaml to use new command names and properties.
- Performed related cleanup and refactoring.

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

## [0.0.29] - 2026-04-11

### Changed
- In `DEBUG` builds, default data paths now resolve under the repository `SampleData` folder (git-ignored) instead of user profile app-data folders, so development/testing no longer touches end-user database and settings files.
- In non-debug builds, data paths continue to use user profile app-data folders.
- Bumped all project versions to `0.0.29` (`OzzTradeDiary`, `OzzTradeDiary.WPF`, `OzzTradeDiary.SQLite`, `OzzTradeDiary.i18n`).

## [0.0.28] - 2026-04-05

### Changed
- Repository classes were refactored with improved input validation: `GetByIdAsync` methods now guard against invalid (non-positive) IDs and return `null` early.
- Added new query methods for filtering by exchange and active status across relevant repositories.
- `TradeImageRepository` now supports querying by `TradeId` and orders all results consistently.
- `TradingAccountRepository` and `SymbolRepository` now support querying by `ExchangeId` and orders all results consistently.
- Repository interfaces were updated to reflect the new query methods.
- Redundant code removed and validation logic cleaned up across repository implementations.
- Bumped all project versions to `0.0.28` (`OzzTradeDiary`, `OzzTradeDiary.WPF`, `OzzTradeDiary.SQLite`, `OzzTradeDiary.i18n`).

## [0.0.27] - 2026-04-04

### Changed
- Repository lookup methods now accept nullable arguments (`int?`, `string?`) for `GetByIdAsync` / `GetBy*Async` and return `null` when arguments are `null`.
- `TradingAccountRepository` was regenerated and refactored; `IDbTradingAccountRepository` was replaced by the co-located `ITradingAccountRepository` interface and all usages were updated.
- `AbstractDiaryVM` updated to use `ITradingAccountRepository`.
- `TradingAccountRepository` duplicate handling and change detection were improved with descriptive exceptions and no-op update avoidance when data is unchanged.
- `SymbolRepository` and `TradingAccountRepository` now use partial extensibility hooks (`OnCreated`, `OnUpdated`) consistently.
- Added `SymbolRepository.part.cs` to update `Exchange.HasAnySymbol` when a new `Symbol` is created.
- `ClearRecordCountCache()` now runs before event hooks in `CreateAsync` methods.
- Repository interfaces were moved into repository class files (`I<Entity>Repository`), and generated repositories are now intended to be extended only via partial files.
- General repository cleanup for consistency and null-safety across generated implementations.
- Bumped all project versions to `0.0.27` (`OzzTradeDiary`, `OzzTradeDiary.WPF`, `OzzTradeDiary.SQLite`, `OzzTradeDiary.i18n`).

## [0.0.26] - 2026-04-04

### Changed
- `IDbSymbolRepository` was replaced by the co-located `ISymbolRepository` interface defined within `SymbolRepository.cs`; the old `IDbSymbolRepository` file was removed and all references were updated.
- `SymbolRepository` was regenerated with improved CRUD logic, code clarity, null handling, and parameterization.
- `AbstractDiaryVM` updated to use `ISymbolRepository`.
- New property settings for Symbol entity relationships added to `CsSqliteRepositoryEngine.settings`.
- Microsoft.Data.Sqlite upgraded to `10.0.5` in `OzzTradeDiary.SQLite` and `OzzTradeDiary.WPF` projects.
- Bumped all project versions to `0.0.26` (`OzzTradeDiary`, `OzzTradeDiary.WPF`, `OzzTradeDiary.SQLite`, `OzzTradeDiary.i18n`).

## [0.0.25] - 2026-04-03

### Changed
- `IDbCurrencyRepository` was replaced by the co-located `ICurrencyRepository` interface defined within `CurrencyRepository.cs`; the old `IDbCurrencyRepository` file was removed and all references were updated.
- `CurrencyRepository` was marked as `partial` and refactored with extensibility hooks (`OnCreated`, `OnUpdated`) for consistency with the generated repository pattern.
- Related ViewModel property types adjusted to use `ICurrencyRepository`.
- Code reformatted and reorganized for clarity; core CRUD logic is unchanged.
- Bumped all project versions to `0.0.25` (`OzzTradeDiary`, `OzzTradeDiary.WPF`, `OzzTradeDiary.SQLite`, `OzzTradeDiary.i18n`).

## [0.0.24] - 2026-04-03

### Added
- `SingleColumnUpdate` property added to codegen settings for all entities; set to `true` for `HasAnySymbol` in Exchanges to support targeted single-column update generation.
- `partial` extension hooks `OnCreated` and `OnUpdated` added to `ExchangeRepository` and `TradeImageRepository` for extensibility without modifying generated code.
- `DeleteAsync` added to `TradeImageRepository`.
- `UpdateHasAnySymbolAsync` method added to `ExchangeRepository`.

### Changed
- `IDbExchangeRepository` was replaced by the co-located `IExchangeRepository` interface; all usages in `SymbolRepository`, `TradingAccountRepository`, and related ViewModels were updated.
- `ExchangeRepository` was refactored: added `DeleteAsync`, partial `OnCreated`/`OnUpdated` methods, and `UpdateHasAnySymbolAsync`.
- `SymbolRepository` and `TradingAccountRepository` updated to use `IExchangeRepository`.
- `ExchangeRepository` property type updated in relevant ViewModels.
- Bumped all project versions to `0.0.24` (`OzzTradeDiary`, `OzzTradeDiary.WPF`, `OzzTradeDiary.SQLite`, `OzzTradeDiary.i18n`).

## [0.0.23] - 2026-04-03

### Added
- Added `ICreatedAt` and `IUpdatedAt` base entity interfaces for improved inheritance and timestamp tracking.

### Changed
- `IModifyDate` interface was renamed to `IUpdatedAt`, and the `ModifyDate` property was renamed to `UpdatedAt` across all models, repositories, SQL scripts, generator settings, and localization files for consistency.
- `TradeImageRepository` was updated with async `CreateAsync` and `UpdateAsync` methods using the `UpdatedAt` property.
- SQL scripts and codegen settings were updated to use `UpdatedAt` instead of `ModifyDate`.
- Standardized naming and improved consistency for modification timestamp tracking throughout the project.
- Bumped all project versions to `0.0.23` (`OzzTradeDiary`, `OzzTradeDiary.WPF`, `OzzTradeDiary.SQLite`, `OzzTradeDiary.i18n`).

## [0.0.22] - 2026-04-02

### Added
- `TradeImageRepository` with improved structure and documentation.
- SQLite repository code generation settings for the regenerated repository layer.

### Changed
- `ExchangeRepository` was regenerated with improved structure and documentation.
- `MetadataRepository` and its interface were refactored, and date columns now use `TEXT` instead of numeric ticks in SQLite scripts.
- `.OzzGen` and generator settings were updated for display members, code generation, and schema alignment.
- `ModifyDate` columns in SQL scripts now use `TEXT`.
- Date and time handling were improved throughout the repository and model layers.
- Bumped all project versions to `0.0.22` (`OzzTradeDiary`, `OzzTradeDiary.WPF`, `OzzTradeDiary.SQLite`, `OzzTradeDiary.i18n`).

## [0.0.21] - 2026-03-30

### Changed
- OzzCodeGen code generation naming was updated: `CsModelClassCodeEngine.settings` now uses `CSharpModelClassCodeEngine` as the root element, and `OzzTradeDiary.OzzGen` now uses `CS_Model_Class_Generator` instead of `Model_Class_Generator`.
- Bumped all project versions to `0.0.21` (`OzzTradeDiary`, `OzzTradeDiary.WPF`, `OzzTradeDiary.SQLite`, `OzzTradeDiary.i18n`).

## [0.0.20] - 2026-03-28

### Changed
- Refactored SQL script execution (`ExecuteScript`) into `AbstractDatabaseRepository` and removed `DbScriptInitializer`; all DDL and seed script execution now lives in the repository base class.
- Added search and exchange-based filtering to the Symbols tab in `MaintenanceWindow`, including UI placeholders and filtered collections.
- Updated English and Turkish localization for new filter terms.
- Bumped all project versions to `0.0.20` (`OzzTradeDiary`, `OzzTradeDiary.WPF`, `OzzTradeDiary.SQLite`, `OzzTradeDiary.i18n`).

## [0.0.19] - 2026-03-28

### Changed
- Repository classes now use the shared `GetOpenConnection()` / `GetOpenConnectionAsync()` helpers from `AbstractDatabaseRepository` to centralize and standardize SQLite connection management.
- `ClearRecordCountCache()` is now used consistently and the cache is cleared after create, delete, and seed operations.
- Added async transaction helpers to `AbstractDatabaseRepository` for future multi-step repository operations.
- `_metadataRepository` was made non-nullable and SQL select statements were moved to readonly fields for clarity.
- Bumped all project versions to `0.0.19` (`OzzTradeDiary`, `OzzTradeDiary.WPF`, `OzzTradeDiary.SQLite`, `OzzTradeDiary.i18n`).

## [0.0.18] - 2026-03-28

### Changed
- `MetadataRepository` now uses a singleton pattern with a private constructor and static `GetInstance()` method so metadata access and the database connection string come from a single source of truth.
- Repository classes no longer accept an optional `MetadataRepository` parameter and instead use the singleton internally.
- WPF ViewModels and services no longer instantiate `MetadataRepository` directly.
- Bumped all project versions to `0.0.18` (`OzzTradeDiary`, `OzzTradeDiary.WPF`, `OzzTradeDiary.SQLite`, `OzzTradeDiary.i18n`).

## [0.0.17] - 2026-03-27

### Changed
- SQLite repository and interface names were simplified for readability, e.g. `SqliteDatabaseSymbolRepository` → `SymbolRepository` and `IDatabaseTradingAccountRepository` → `IDbTradingAccountRepository`.
- Bumped all project versions to `0.0.17` (`OzzTradeDiary`, `OzzTradeDiary.WPF`, `OzzTradeDiary.SQLite`, `OzzTradeDiary.i18n`).

## [0.0.16] - 2026-03-27

### Added
- `HasAnySymbol` on `Exchange` and the related schema, seed data, repositories, localization, codegen, and maintenance UI.
- BIST symbols in `Symbols-Data.sql`.

### Changed
- Order models now use the `OrderType` enum instead of ad hoc order-type handling.
- String validation and generated resource metadata now use `MaxStringLength` and `MinStringLength` where applicable.
- Repositories were refactored to use the shared generic base class, with minor repository and `ModelValidator` cleanup.
- `MaintenanceWindow` was updated for `HasAnySymbol` and symbol grid behavior.
- Turkish translations and `OzzCodeGen` settings/vocabulary were improved to keep generated localization resources aligned.
- Bumped all project versions to `0.0.16` (`OzzTradeDiary`, `OzzTradeDiary.WPF`, `OzzTradeDiary.SQLite`, `OzzTradeDiary.i18n`).

## [0.0.15] - 2026-03-22

### Added
- `UiCulture` property (`string`) on `AppSettings` — stores a BCP-47 culture name (e.g. `"en-US"`, `"tr-TR"`); when empty the operating system culture is used.

### Changed
- `App` startup replaced `StartupUri` with a `Startup` event handler (`OnStartup`); `UiCulture` is applied to `Thread.CurrentThread.CurrentUICulture` and `CurrentCulture` before `MainWindow` is created, so all resource lookups and formatting reflect the chosen culture from the first frame.
- `MainWindow` menu item headers are now bound to `ActionStrings` and `LocalizedStrings` via `{x:Static i18n:*}` — all top-level menus and most sub-items are localized; `_Exit` and `_Add Trade` remain hardcoded pending dedicated resource keys.
- Remaining nullable reference warnings in `EnumExtension.cs` resolved: `DisplayAttribute.Name` null-coalesced at both call sites, dead null guard on `descriptionAttributes` removed, `GetValue` cast changed to `ResourceManager?`, and `GetString` return null-coalesced with key as fallback.
- Bumped all project versions to `0.0.15` (`OzzTradeDiary`, `OzzTradeDiary.WPF`, `OzzTradeDiary.SQLite`, `OzzTradeDiary.i18n`).

## [0.0.14] - 2026-03-21

### Changed
- `TextExtensions` was moved from the WPF project into shared project `OzzTradeDiary` for platform-agnostic reuse.
- SQLite insert/update handling was fixed in repository code paths to persist `null` values correctly instead of failing or writing incorrect placeholder text.
- `MaintenanceWindow` edit flows were polished.
- Bumped all project versions to `0.0.14` (`OzzTradeDiary`, `OzzTradeDiary.WPF`, `OzzTradeDiary.SQLite`, `OzzTradeDiary.i18n`).

## [0.0.13] - 2026-03-21

### Added
- `SymbolCreate` and `SymbolEdit` views with corresponding `SymbolCreateVM` and `SymbolEditVM` for modal create/edit flows in maintenance.
- `DefaultCurrency` property on the `Exchange` model and in the generated `Exchange.sql` DDL.
- `EnumExtension` static helper for working with enum values and localized `Display` attributes.

### Changed
- `EnumExtension` was moved from the WPF project into shared project `OzzTradeDiary` and now lives under namespace `TD.Extensions` for platform-agnostic reuse.
- `IIsDirty` was moved into `AbstractEditVM.cs` and the standalone `IIsDirty.cs` file was removed.
- `SymbolCreate` market type ComboBox now shows localized display text from `MarketType` enum `Display` attributes (via `EnumExtension.GetDisplayValue`) instead of blank entries.
- `SqliteDatabaseExchangeRepository` now stores `Exchange.DefaultCurrency` as SQL `NULL` and no longer throws when creating or updating an exchange with a `null` default currency.
- Bumped all project versions to `0.0.13` (`OzzTradeDiary`, `OzzTradeDiary.WPF`, `OzzTradeDiary.SQLite`, `OzzTradeDiary.i18n`).


## [0.0.12] - 2026-03-21

### Added
- `AbstractEditView` base window in `TD.WPF.Views` with shared close-guard logic for unsaved dialog changes via `IIsDirty`.

### Changed
- `TradingAccountEdit`, `ExchangeEdit`, and `CurrencyEdit` now inherit from `AbstractEditView` for consistent unsaved-changes confirmation (`Yes/No/Cancel`) on close.
- Bumped all project versions to `0.0.12` (`OzzTradeDiary`, `OzzTradeDiary.WPF`, `OzzTradeDiary.SQLite`, `OzzTradeDiary.i18n`).

## [0.0.11] - 2026-03-20

### Added
- `CurrencyCreate` and `CurrencyEdit` views with corresponding `CurrencyCreateVM` and `CurrencyEditVM` for modal create/edit flows in maintenance.
- `ExchangeCreate` and `ExchangeEdit` views with corresponding `ExchangeCreateVM` and `ExchangeEditVM` for modal create/edit flows in maintenance.

### Changed
- `MaintenanceWindow` currency and exchange actions now use the new create/edit dialogs and view models, with save + refresh flow after successful dialog completion.
- Bumped all project versions to `0.0.11` (`OzzTradeDiary`, `OzzTradeDiary.WPF`, `OzzTradeDiary.SQLite`, `OzzTradeDiary.i18n`).

## [0.0.10] - 2026-03-19

### Added
- `TradingAccountEdit` view and `TradingAccountEditVM` for editing existing trading accounts via a modal dialog from `MaintenanceWindow`.
- `ReadOnlyTextBoxStyle` shared style in `Styles.xaml` — read-only `TextBox` with slightly darker background (`#EDEDED`) to visually indicate non-editable fields.
- `EditTradingAccounts_Click` handler in `MaintenanceWindow` opens `TradingAccountEdit` dialog for the selected trading account; saves and refreshes on OK.
- Edit button (pencil icon) in `MaintenanceWindow` TradingAccount toolbar, disabled when `SelectedTradingAccount` is `null` via `DataTrigger`.

### Changed
- Bumped all project versions to `0.0.10` (`OzzTradeDiary`, `OzzTradeDiary.WPF`, `OzzTradeDiary.SQLite`, `OzzTradeDiary.i18n`).

## [0.0.9] - 2026-03-19

### Changed
- Bumped all project versions to `0.0.9` (`OzzTradeDiary`, `OzzTradeDiary.WPF`, `OzzTradeDiary.SQLite`, `OzzTradeDiary.i18n`).
- Renamed `CreateTradingAccountVM` to `TradingAccountCreateVM` and `CreateTradingAccount` view to `TradingAccountCreate` to follow entity-first, verb-last naming convention.
- Documented naming convention: View/ViewModel names use entity-first, verb-last order (e.g., `TradingAccountCreate`, `TradingAccountCreateVM`) for Solution Explorer grouping; method names remain verb-first (e.g., `CreateTradingAccount()`, `DeleteExchange()`).

## [0.0.8] - 2026-03-19

### Added
- `DeleteExchangeCommand` bound in `MaintenanceWindow` Exchange toolbar — deletes `SelectedExchange` with `Yes/No` confirmation message box; command is disabled when no exchange is selected or when any `Symbol`/`TradingAccount` references the selected exchange (`ExchangeId`).

### Changed
- Bumped all project versions to `0.0.8` (`OzzTradeDiary`, `OzzTradeDiary.WPF`, `OzzTradeDiary.SQLite`, `OzzTradeDiary.i18n`).
- Removed `AccountCode` property from `TradingAccount` model and its generated DDL script.
- `CreateTradingAccount` dialog action buttons now use icon buttons (Bootstrap Icons) instead of plain text buttons.
- `CreateTradingAccount` dialog now displays `ExchangeCode` instead of `ExchangeId`.
- `MaintenanceWindow` TradingAccounts DataGrid now displays `Exchange.ExchangeCode` instead of `ExchangeId`.
- `SqliteDatabaseTradingAccountRepository` now supports injecting an exchange repository and maps `TradingAccount.Exchange` from database exchange records when loading trading accounts (`GetAllAsync`, `GetByIdAsync`).
- `SqliteDatabaseSymbolRepository` now supports injecting an exchange repository and maps `Symbol.Exchange` from database exchange records when loading symbols (`GetAllAsync`, `GetByTickerFullAsync`, `GetByIdAsync`).

## [0.0.6] - 2026-03-19

### Added
- `AbstractDatabaseRepository` abstract base class in `TD.SQLite` — provides `ValidateOrThrow(object model)` helper; all repositories except `SqliteDatabaseMetadataRepository` inherit from it; `ValidateOrThrow` is called in `CreateAsync` and `UpdateAsync` after the null guard and throws `ValidationException` with all DataAnnotations error messages joined by newlines.
- `CreateTradingAccount` dialog: `AddTradingAccount_Click` in `MaintenanceWindow` now opens it as a modal dialog centered on the owner window; on OK the new account is added and saved; Cancel or close discards changes.
- `CreateTradingAccountVM` live validation: per-property `ValidateProperty` calls on `Title`, `AccountCode`, `ExchangeId`, and `DisplayOrder`; `IsValid` property (`!HasErrors`) exposed for OK button `IsEnabled` binding; `ValidateModel` called in constructor so OK starts disabled.
- `ValidationTextBoxStyle` and `ValidationComboBoxStyle` shared styles in `Styles.xaml` — display inline red error text below the control, red border, and tooltip on validation failure without disabling the control.
- Validation errors displayed inline in `CreateTradingAccount` dialog for `Title`, `AccountCode`, and `Exchange` fields.
- `Title` TextBox auto-focused when `CreateTradingAccount` dialog opens via `FocusManager.FocusedElement`.
- `AbstractDiaryVM` base class in `TD.WPF.ViewModels` — consolidates repository initialization and CRUD operations (Currency, Exchange, TradingAccount, Symbol) shared across ViewModels.
- `AppVersion` static class in `TD.WPF.Models` — reads product name, version, copyright, and description from assembly attributes at runtime.
- `MaintenanceWindow` Add, Save (with auto-refresh), and Refresh CRUD operations for Currency, Exchange, TradingAccount, and Symbol entities via code-behind event handlers.
- `MainWindow` title bar now displays the application version number via `AppVersion.Version`.
- Added generated symbol seed script `OzzTradeDiary.SQLite/DbScripts/Symbols-Data.sql` and wired symbol repository initialization to seed `Symbols` via `SeedIfEmpty`.
- Added shared `ModelValidator` in `TD.Validation` for DataAnnotations-based model validation reusable across WPF, MAUI, and ASP.NET.
- Added `OzzTradeDiary.i18n` (`net10.0`) localization project with generated resource sets: `ActionStrings`, `CommonStrings`, `ErrorStrings`, `LocalizedStrings`, and `MessageStrings` (including `tr` culture variants).
- Implemented `CreateTradingAccount` window form bindings to `CreateTradingAccountVM`, including an `Exchanges` combo box (`ItemsSource=Exchanges`) with two-way selection bound to `ExchangeId`.
- Added `OK` and `Cancel` action buttons to `CreateTradingAccount` dialog.

### Changed
- Bumped all project versions to `0.0.6` (`OzzTradeDiary`, `OzzTradeDiary.WPF`, `OzzTradeDiary.SQLite`, `OzzTradeDiary.i18n`).
- `CreateTradingAccount` dialog property label `TextBlock` elements now bind to `{x:Static i18n:LocalizedStrings.PropertyName}` instead of hardcoded strings.
- Model DataAnnotations now use `TD.i18n` resources for localized display names and validation messages.
- Moved `MainWindow` into `Views/` subfolder; namespace updated to `TD.WPF.Views`.
- Moved `MaintenanceWindow` and its view-model into `Views/Maintenance/` and `ViewModels/Maintenance/` subfolders; namespaces updated to `TD.WPF.Views.Maintenance` and `TD.WPF.ViewModels.Maintenance`.
- Localization/resource generation guidance updated: resources are generated by `OzzCodeGen` via `OzzCodeGen/ResourceGen.settings` + `OzzCodeGen/Vocabulary`; `LocalizedStrings.resx`, model classes, and database schema are sourced from `OzzTradeDiary.OzzGen` generation outputs.

## [0.0.5] - 2026-03-12
### Added
- Added `IDatabaseSymbolRepository` and `SqliteDatabaseSymbolRepository` in `TD.SQLite`.
- Added `_Maintenance` menu to `MainWindow` with `ShowMaintenanceCommand` — opens `MaintenanceWindow` and brings it to front if already open.
- Added `_Help` menu to `MainWindow` with `ShowAboutCommand` that displays `AboutDialog` as a modal dialog.
- Added `AboutDialog` with product name, version, description, copyright, and GitHub hyperlink.
- `AboutDialog` auto-closes on deactivation to prevent blocking `MainWindow` when another window is focused.
- `AboutDialog` loads the highest resolution frame available from the `.ico` file.
- Added info-circle-fill icon to the About menu item.
- Added `WindowExtensions.SetIconFromGeometryResource()` extension method for rendering Bootstrap Icon geometry as a window title bar icon.
- `MaintenanceWindow` title bar icon set to `gear-wide-connected` via `SetIconFromGeometryResource()`.

### Changed
- Enforced `Symbol.TickerFull` as a unique immutable key in repository behavior (cannot be changed after insert).
- `Symbol` update flow now only updates mutable fields (`Description`, `DisplayOrder`, `IsActive`) to protect identifier/classification fields after creation.

### Technical
- Implemented repositories now include `Currency`, `Exchange`, `TradingAccount`, and `Symbol`.

## [0.0.4] - 2026-03-12
### Added
- Added application icon.

### Changed
- Renamed `TradePlan` model to `TradeImage` to better reflect its purpose as image attachments with notes for trades.
- Added enum properties in model files (`OrderType`, `TradeDirection`, and related enum usage updates).
- Added missing `OrderType` column to generated DDL scripts for `EntryOrder`, `TakeProfitOrder`, and `StopLossOrder`.
- Updated project versions of `OzzTradeDiary.WPF` and `OzzTradeDiary.SQLite` to `0.0.4` for internal change tracking (`2026-03-12`).
- Updated DDL generation to use idempotent table creation (`CREATE TABLE IF NOT EXISTS`).

### Technical
- Data layer implementation is still in progress.
- Implemented repositories: `Currency`, `Exchange`, and `TradingAccount`. Remaining model repositories will be added.
- Model naming is singular, related table naming is plural.
- Each model has a generated DDL file in `OzzTradeDiary.SQLite/DbScripts` named `<ModelName>.sql`.
- Some models have generated seed files named `<PluralTableName>-Data.sql` in `OzzTradeDiary.SQLite/DbScripts`.
- Each CUD function updates metadata timestamp via `SaveLastUpdateUtcAsync` in `SqliteDatabaseMetadataRepository`.
- Confirmed `DbScripts` SQL files are generated via `OzzCodeGen` (not manually edited).

### Documentation
- Updated `README.md` and `.github/copilot-instructions.md` for unreleased status guidance.
- Documented SQLite model/table/script conventions and repository coverage status.

## [0.0.2] - 2026-03-08

### Added
- DPI-aware multi-monitor window positioning.

### Technical
- Layered MVVM architecture split into `TD`, `TD.SQLite`, and `TD.WPF` projects.
- SQLite data access implemented with `Microsoft.Data.Sqlite`.
- Code generation setup for SQLite DDL and localization resources via `OzzCodeGen`.
