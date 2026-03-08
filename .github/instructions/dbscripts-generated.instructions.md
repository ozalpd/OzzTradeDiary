---
applyTo: "**/DbScripts/**"
description: "Use when: viewing or referencing SQLite DDL scripts in the DbScripts folder"
---

# Generated SQL Scripts — Do Not Edit

Files in `OzzTradeDiary.SQLite/DbScripts/` are **auto-generated** by OzzCodeGen from `OzzCodeGen/SqliteScriptsGen.settings`.

## Rules

- **Never edit these files manually** — changes will be overwritten on next generation
- To modify schema: update `OzzCodeGen/SqliteScriptsGen.settings` and regenerate
- To add a new table: add a new `EntitySetting` block in the settings file

## Schema Conventions (for reference)

- Primary key: `Id INTEGER PRIMARY KEY`
- Strings: `TEXT`
- Numbers: `INTEGER` (ints, enums, bools) or `REAL` (decimals)
- Dates: `INTEGER` (stored as ticks), **not** ISO 8601 text
- Foreign keys: `{Entity}Id INTEGER Not Null`
- Nullable columns marked with `Null`, required with `Not Null`
