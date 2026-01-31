# OzzTradeDiary - Trading Diary Application

OzzTradeDiary is a professional trading diary application for tracking and analyzing trades across various markets including stocks, crypto, forex, and futures. Built with .NET 10 and WPF, it provides a comprehensive platform for traders to record, monitor, and review their trading performance.

## Features

- **Multi-Market Support**: Track trades across stocks, crypto, forex, futures, commodities, options, and more
- **Trade Management**: Record entry/exit prices, stop-loss, and take-profit levels
- **Order Management**: Manage entry orders and profit/loss orders with various order types
- **Configuration Management**: Application-wide settings using the AppSettings system
- **Audit Trail**: Automatic logging of all data changes for compliance and analysis
- **Code Generation Workflow**: Data models drive database schema, stored procedures, and C# code generation

## Project Structure

```
OzzTradeDiary/
├── OzzCodeGen/                       # Code generation definitions (source of truth)
│   ├── OzzTradeDiary.OzzGen          # Entity model definitions (XML)
│   ├── TSqlScriptsGen.settings       # SQL Server generation config
│   ├── ResourceGen.settings          # Localization config
│   └── Vocabulary/                   # Localization vocabulary files
│
├── DbScripts/T-SQL/                   # Generated database scripts
│   ├── _Create_OzzTradeDiary.bat      # Create development database
│   ├── _Drop_Create_OzzTradeDiary.bat # Drop and recreate database
│   └── *.sql                          # Generated schema and stored procedures
│
└── OzzTradeDiary.NET/                 # .NET Framework 4.8 Application Layer
    ├── TD.Data/                       # Data Access Layer (Entity Framework 6.5.1)
    │   ├── Enums.cs                   # Domain enumerations
    │   ├── DbModel.edmx               # EF6 EDMX (Entity Data Model)
    │   ├── DbModel.Context.tt         # T4 template for EF6 DbContext generation
    │   ├── DbModel.tt                 # T4 template for EF6 entity classes generation
    │   └── DbModel.Settings.ttinclude # T4 template settings and interface definitions
    └── TD.WPF/                        # WPF Presentation Layer
        ├── MainWindow.xaml
        ├── App.xaml
        └── Views/                     # UI views
```

## Technology Stack

- **Framework**: .NET Framework 4.8 (Windows desktop runtime)
- **Desktop UI**: Windows Presentation Foundation (WPF)
- **Data Access**: Entity Framework 6.5.1 with SQL Server and EDMX designer support
- **Code Generation**: 
  - OzzCodeGen (external tool for database and model generation)
  - T4 Templates for EF6 code generation
- **Database**: SQL Server LocalDB (`.\SqlExpress`)
- **Language Version**: C# 7.3

## Prerequisites

- .NET 10 SDK
- SQL Server LocalDB (or SQL Express)
- Visual Studio Code or Visual Studio 2022

## Getting Started

### 1. Set Up the Database

Navigate to the T-SQL scripts directory and create the development database:

```bash
cd DbScripts\T-SQL
.\_Create_OzzTradeDiary.bat
```

This will:
- Create the `OzzTradeDiary` database on `.\SqlExpress`
- Generate all schemas and tables
- Create stored procedures for all CRUD operations
- Set up audit log tables

### 2. Build the Solution

```bash
dotnet build OzzTradeDiary.slnx
```

### 3. Run the Application

```bash
dotnet run --project OzzTradeDiary.NET\TD.WPF\TD.WPF.csproj
```

## Database Management

### Creating a Fresh Development Database

```bash
cd DbScripts\T-SQL
.\_Create_OzzTradeDiary.bat
```

**Details**: Generates all development database objects including schemas, tables, stored procedures, and audit log tables. Executes SQL scripts in dependency order to ensure proper entity relationships.

### Resetting the Database (Drop & Recreate)

```bash
cd DbScripts\T-SQL
.\_Drop_Create_OzzTradeDiary.bat
```

**Details**: Safely drops all existing development database objects (if they exist), then invokes `_Create_OzzTradeDiary.bat` to recreate the database from scratch. Use this when you need a clean development environment after data model changes.

**Script Execution Order**: Schema → AppSettings → Exchange → TradingAccount → Currency → Symbol → Trade → Orders

## Core Entities

- **AppSettings** - Application-wide configuration settings (keyed by SettingType enum)
- **Exchange** - Trading exchanges (NYSE, NASDAQ, Binance, etc.)
- **TradingAccount** - User trading accounts per exchange
- **Symbol** - Tradable instruments with MarketType
- **Trade** - Core trading record with entry/exit prices and SL/TP levels
- **EntryOrder, TakeProfitOrder, StopLossOrder** - Order management

## Development Workflow

### Making Data Model Changes

1. Edit entity definitions in `OzzCodeGen/OzzTradeDiary.OzzGen`
2. Run the OzzCodeGen tool to generate:
   - SQL schema files in `DbScripts/T-SQL/`
   - Stored procedures for CRUD operations
   - C# enum and model classes
3. Review generated SQL files
4. Review generated C# files
5. Run database setup to apply schema changes:
   ```bash
   cd DbScripts\T-SQL
   .\_Drop_Create_OzzTradeDiary.bat
   ```

## Key Files

- `OzzCodeGen/OzzTradeDiary.OzzGen` - Master entity model definition
- `OzzTradeDiary.NET/TD.Data/Enums.cs` - Domain enumerations
- `DbScripts/T-SQL/_Create_OzzTradeDiary.bat` - Database creation script
- `DbScripts/T-SQL/_Drop_Create_OzzTradeDiary.bat` - Database reset script
- `.github/copilot-instructions.md` - Detailed coding guidelines and architecture

## Code Generation Tool

OzzCodeGen is an external tool that drives the code generation workflow:

- **Repository**: https://github.com/ozalpd/OzzCodeGen
- **Purpose**: Generates SQL schemas, stored procedures, C# models, and localization resources from XML entity definitions
- **Input**: `OzzTradeDiary.OzzGen` (entity definitions)
- **Outputs**:
  - Database schema files (`*.sql`) in `DbScripts/T-SQL/`
  - Stored procedure files (`*-SP.sql`) with CRUD operations
  - C# enum and model classes
  - Localization resource files

## Important Notes

- **Never edit generated files directly** - changes will be overwritten when OzzCodeGen runs
- **Database changes require regeneration** - schema files are generated, not manually maintained
- **Entity naming convention**: Entities use singular names (e.g., `Trade`), database tables use plural names (e.g., `Trades`)
- **Audit trail**: All data changes are automatically logged via stored procedures to audit tables in the `log` schema
- **Multi-language support**: The project supports Turkish and non-Turkish localization through vocabulary files

## Conventions

### Namespacing
- Root: `TD` (Trade Diary)
- Sub-namespaces: `TD.Data`, `TD.WPF`, `TD.Models`

### Financial Precision
- Prices and amounts use `decimal(18, 4)` for accuracy

### Data Access
- Uses **Entity Framework 6.5.1** (classic EF, not EF Core)
- Leverages stored procedures generated by OzzCodeGen for CRUD operations
- Database-first approach with generated SQL schemas
- **T4 Code Generation**: DbModel.edmx uses T4 templates for generating DbContext and entity classes
  - `DbModel.Context.tt` - Generates the DbContext class
  - `DbModel.tt` - Generates entity classes
  - `DbModel.Settings.ttinclude` - Configuration file for T4 templates including interface definitions, query settings, and DTO exclusions

### T4 Template Configuration

The `DbModel.Settings.ttinclude` file controls the T4 code generation behavior:
- **Interface Definitions**: Defines common interfaces like `IId`, `IDisplayOrder`, `IModifyDate`, `IIsActive`
- **Query Settings**: Default sorting properties and published property configuration
- **DTO Settings**: Specifies which properties to exclude from Data Transfer Objects
- After modifying settings, run **Transform All T4 Templates** from the Build menu in Visual Studio

## Support & Documentation

For detailed architecture, coding standards, and guidelines, see:
- `.github/copilot-instructions.md` - Comprehensive coding instructions and project conventions

## License

See LICENSE file for details.
