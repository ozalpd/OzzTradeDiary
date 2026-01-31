# OzzTradeDiary AI Coding Instructions

## Project Overview
OzzTradeDiary is a trading diary application for tracking trades across various markets (stocks, crypto, forex, futures). It uses a **code-generation workflow** where entity models defined in XML drive the generation of database schemas, stored procedures, and C# code.

## Architecture

### Three-Tier Structure
1. **OzzCodeGen/** - Code generation definitions (source of truth for data model)
2. **DbScripts/T-SQL/** - Generated SQL Server database schema and stored procedures
3. **OzzTradeDiary.NET/** - .NET Framework 4.8 application layer
   - **TD.Data** - Data access layer (Entity Framework 6.5.1 with T4 code generation)
   - **TD.WPF** - WPF presentation layer

### Code Generation Workflow (Critical)
**DO NOT manually edit generated files.** All data model changes must be made in XML definitions:

- **Entity models**: Edit `OzzCodeGen/OzzTradeDiary.OzzGen` XML file
- **SQL generation config**: `OzzCodeGen/TSqlScriptsGen.settings`
- **Localization config**: `OzzCodeGen/ResourceGen.settings`
- **Vocabulary files**: `OzzCodeGen/Vocabulary/vocabulary.*.xml`

Generated outputs include:
- SQL schema files in `DbScripts/T-SQL/` (e.g., `Trade.sql`, `Symbol.sql`)
- SQL stored procedures (e.g., `Trades-SP.sql`, `EntryOrders-SP.sql`)
- C# enums and models (target folder: `../GeneratedCodes` per OzzGen config)

## Domain Model

### Core Entities (from OzzTradeDiary.OzzGen)
- **AppSettings** - Application-wide configuration settings (keyed by SettingType enum)
- **Exchange** - Trading exchanges (NYSE, NASDAQ, Binance, etc.)
- **TradingAccount** - User trading accounts per exchange
- **Symbol** - Tradable instruments with MarketType (Stock, Crypto, Forex, etc.)
- **Trade** - Core trading record with entry/exit prices and SL/TP levels
- **EntryOrder**, **TakeProfitOrder**, **StopLossOrder** - Order management

### Base Interfaces Pattern
Entity definitions use abstract base types for common behavior:
- `IPrimaryKey` - All entities have integer `Id` (store-generated)
- `IModifyDate` - Tracks last modification with `ModifyDate` timestamp
- `IDisplayOrder` - Entities with `DisplayOrder` for sorting
- `IIsActive` - Soft-delete pattern with `IsActive` boolean
- `ITradeOrder` - Common order fields (OrderPrice, FilledPrice, OrderQuantity, etc.)

### Enums (from TD.Data/Enums.cs)
```csharp
EntryMethod: Market=10, Limit=20
MarketType: Unspecified=0, Stock=20, Fund=30, Futures=40, Forex=50, Option=60, Commodity=70, Crypto=80, CryptoPerpetual=90, Index=100
OrderType: Market=10, Limit=20, Stop=30, StopLimit=40, TrailingStop=50
SettingType: String=1010, Email=1020, StringArray=1101, Boolean=2000, Integer=2010, IntegerArray=2011, Decimal=2021, DecimalArray=2022, Date=3010, DateArray=3011, DateTime=3020, DateTimeArray=3021
TradeDirection: Long=200, Short=100
```

## Database Conventions

### SQL Server LocalDB
- **Server**: `.\SqlExpress`
- **Database**: `OzzTradeDiary`
- **Schemas**: `dbo` (main tables), `log` (audit log tables)

### Audit Logging Pattern
Every main table has a corresponding log table in the `log` schema:
- Example: `dbo.Trades` → `log.TradesLog`
- Log tables include `LogStatus` and `LogStatusDescription` fields
- Stored procedures automatically log inserts/updates/deletes

### Stored Procedure Naming
- Insert: `[dbo].[EntityName_Insert]` (returns new ID)
- Update: `[dbo].[EntityName_Update]`
- Delete: `[dbo].[EntityName_Delete]` (logs to audit table first)
- Log Insert: `[log].[EntityNameLog_Insert]`

### Database Setup Commands
```bash
# Create database from scratch
cd DbScripts\T-SQL
.\_Create_OzzTradeDiary.bat

# Drop and recreate (development)
.\_Drop_Create_OzzTradeDiary.bat
```

**Batch File Details:**
- **`_Create_OzzTradeDiary.bat`** - Generates all development database objects (schemas, tables, stored procedures, and audit log tables). Executes SQL scripts in dependency order to ensure proper entity relationships.
- **`_Drop_Create_OzzTradeDiary.bat`** - Safely drops all development database objects first (if they exist), then invokes `_Create_OzzTradeDiary.bat` to recreate the database from scratch. Use this when you need a clean development environment after data model changes.

Scripts execute in order: Schema → AppSettings → Exchange → TradingAccount → Currency → Symbol → Trade → Orders

## Development Workflow

### Building the Application
```bash
# Build solution (VS Code or command line)
dotnet build OzzTradeDiary.slnx

# Run WPF application
dotnet run --project OzzTradeDiary.NET\TD.WPF\TD.WPF.csproj
```

### Making Data Model Changes
1. Edit entity definitions in `OzzCodeGen/OzzTradeDiary.OzzGen`
2. Run code generation tool (see OzzCodeGen section below)
3. Review generated SQL files in `DbScripts/T-SQL/`
4. Review generated C# files in target folder
5. Run `_Drop_Create_OzzTradeDiary.bat` to apply schema changes

### OzzCodeGen Tool
The code generation is powered by **OzzCodeGen**, an external tool not included in this repository:
- **Repository**: https://github.com/ozalpd/OzzCodeGen
- **Purpose**: Generates SQL schemas, stored procedures, C# models, and localization resources from XML entity definitions
- **Configuration Files**:
  - `OzzTradeDiary.OzzGen` - Entity model definitions (enums, entities, properties)
  - `TSqlScriptsGen.settings` - SQL Server generation settings (table names, columns, stored procedures)
  - `ResourceGen.settings` - Localization resource generation settings
  - `SqliteScriptsGen.settings` - SQLite generation settings (if needed)

When OzzCodeGen runs, it reads the XML entity definitions and generates:
- Database schema files (`*.sql`) in `DbScripts/T-SQL/`
- Stored procedure files (`*-SP.sql`) with CRUD operations
- C# enum and model classes (configured to output to `../GeneratedCodes`)
- Localization resource files for multi-language support

## Project Conventions

### Namespacing
- Root namespace: `TD` (Trade Diary)
- Sub-namespaces: `TD.Data`, `TD.WPF`, `TD.Models`
- Localization: `OTD.i8n` (per ResourceGen.settings)

### Entity & Table Naming
- **Entities**: Use singular names (e.g., `Trade`, `Symbol`, `Exchange`) since each entity represents a single record from a table
- **Database Tables**: Plural names (e.g., `Trades`, `Symbols`, `Exchanges`)
- This convention applies throughout the codebase and generated files

### Decimal Precision
Prices and amounts use `decimal(18, 4)` for financial accuracy

### Foreign Keys
Entity relationships enforced via SQL constraints:
- Example: `Trade.TradingAccountId` → `FK_Trade_TradingAccountId` → `TradingAccounts(Id)`
- Example: `Trade.SymbolId` → `FK_Trade_SymbolId` → `Symbols(Id)`

### Target Framework
- **.NET Framework 4.8** (Windows desktop runtime, compatible with EF6 EDMX designer)
- **Entity Framework 6.5.1** (classic EF, not EF Core)
- **WPF** for Windows desktop UI
- **C# Language Version**: 7.3

### Entity Framework 6 Usage
This project uses **Entity Framework 6.5.1** (the classic version, not EF Core):
- **Data Layer**: Located in `TD.Data` project
- **Database-First Approach**: Works with generated SQL schemas and stored procedures
- **T4 Code Generation Strategy**: DbModel.edmx uses T4 templates instead of default code generation
  - **DbModel.Context.tt** - Generates the DbContext class with configurable abstract context option
  - **DbModel.tt** - Generates entity classes with support for interfaces and DTOs
  - **DbModel.Settings.ttinclude** - Central configuration file for T4 templates containing:
    - Interface definitions (`IId`, `IDisplayOrder`, `IModifyDate`, `IIsActive`)
    - Query settings (default sorting properties, publish property name)
    - DTO exclusion settings (properties to exclude from Data Transfer Objects)
    - Utility methods for interface generation
- **Why EF6 over EF Core**: EF6 was chosen specifically for its superior support of stored procedures, which are all generated by OzzCodeGen for CRUD operations
- **Key Difference**: EF6 is the older Object-Relational Mapper that runs on .NET Framework and .NET (whereas EF Core is the modern cross-platform rewrite)
- **Important**: When working with data access, use EF6 patterns and APIs, not EF Core patterns
  - Use `System.Data.Entity` namespace, not `Microsoft.EntityFrameworkCore`
  - DbContext configuration differs from EF Core
  - Migration strategy differs - this project uses SQL scripts instead of EF migrations
- **Integration**: EF6 entities map to tables created by OzzCodeGen-generated SQL scripts and use the generated stored procedures for data operations
- **T4 Template Modifications**: After changing settings in `DbModel.Settings.ttinclude`, run **Transform All T4 Templates** from the Build menu in Visual Studio to regenerate code

## Key Files Reference

- [OzzCodeGen/OzzTradeDiary.OzzGen](OzzCodeGen/OzzTradeDiary.OzzGen) - Master entity model definition
- [DbScripts/T-SQL/Trade.sql](DbScripts/T-SQL/Trade.sql) - Example of generated schema with log table
- [DbScripts/T-SQL/Trades-SP.sql](DbScripts/T-SQL/Trades-SP.sql) - Example of generated CRUD stored procedures
- [OzzTradeDiary.NET/TD.Data/Enums.cs](OzzTradeDiary.NET/TD.Data/Enums.cs) - Domain enumerations
- [OzzTradeDiary.slnx](OzzTradeDiary.slnx) - Solution file (modern .slnx format)

## Important Notes

- **Never edit generated files directly** - changes will be overwritten
- **Database changes require regeneration** - schema files are not manually maintained
- **Multi-language support** - vocabulary files support Turkish and non-Turkish localization
- **Audit trail** - all data changes automatically logged via stored procedures

## Suggested Additions

- **Generated files safety**: list generated output directories (e.g., `DbScripts/T-SQL`, `OzzTradeDiary.NET/GeneratedCodes`) and explicitly forbid direct edits.
- **TD.WPF UI pattern**: document the preferred MVVM approach (e.g., `INotifyPropertyChanged`, commands, view-model naming).
- **Data access expectations**: call out EF6 stored-procedure usage for writes and avoid EF Core patterns or ad-hoc LINQ writes.
- **Error handling & logging**: specify the logging framework/location and error handling conventions.
- **Testing conventions**: specify test project locations, mocking strategy, and test naming patterns.
- **Formatting & style**: note any nullable, async, or naming conventions.
- **Schema change checklist**: edit XML → run generator → review SQL/C# → rebuild DB.
- **Target framework**: remind that projects target `net10.0` and any language version constraints.
