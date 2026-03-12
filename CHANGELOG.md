# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/).

## [Unreleased]

### Added
- Added application icon.

### Changed
- Renamed `TradePlan` model to `TradeImage` to better reflect its purpose as image attachments with notes for trades.
- Added enum properties in model files (`OrderType`, `TradeDirection`, and related enum usage updates).
- Added missing `OrderType` column to generated DDL scripts for `EntryOrder`, `TakeProfitOrder`, and `StopLossOrder`.
- Updated project versions of `OzzTradeDiary.WPF` and `OzzTradeDiary.SQLite` to `0.0.3` for internal change tracking.
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
