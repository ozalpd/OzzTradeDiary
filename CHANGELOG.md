# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/).

## [0.0.33] - 2026-04-12

### Added
- Added `Exchange.Symbols` and `Exchange.TradingAccounts` navigation collections and async loading support in `ExchangeRepository`.
- Added `OrderQuantity` and `FilledQuantity` fields to `Trade`, including repository mapping and schema updates.

### Changed
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
