# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/).

## [Unreleased]

### Added
- `AbstractDiaryVM` base class in `TD.WPF.ViewModels` — consolidates repository initialization and CRUD operations (Currency, Exchange, TradingAccount, Symbol) shared across ViewModels.
- `AppVersion` static class in `TD.WPF.Models` — reads product name, version, copyright, and description from assembly attributes at runtime.
- `MaintenanceWindow` Add, Save (with auto-refresh), and Refresh CRUD operations for Currency, Exchange, TradingAccount, and Symbol entities via code-behind event handlers.
- `MainWindow` title bar now displays the application version number via `AppVersion.Version`.

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
