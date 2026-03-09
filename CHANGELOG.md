# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/).

## [Unreleased]

### Added
- Added application icon.

### Changed
- Renamed `TradePlan` model to `TradeImage` to better reflect its purpose as image attachments with notes for trades.

### Documentation
- Updated `README.md` and `.github/copilot-instructions.md` for unreleased status guidance.
- Documented that `OzzTradeDiary.SQLite/DbScripts/` contains one generated DDL file per model in `OzzTradeDiary/Models`.

## [0.0.2] - 2026-03-08

### Added
- DPI-aware multi-monitor window positioning.

### Technical
- Layered MVVM architecture split into `TD`, `TD.SQLite`, and `TD.WPF` projects.
- SQLite data access implemented with `Microsoft.Data.Sqlite`.
- Code generation setup for SQLite DDL and localization resources via `OzzCodeGen`.
