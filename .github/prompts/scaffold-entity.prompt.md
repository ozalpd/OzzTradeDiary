---
description: "Scaffold a new entity end-to-end: model, SQLite CodeGen settings, repository, and ViewModel"
mode: "agent"
---

# Scaffold New Entity

Create all layers for a new entity in the OzzTradeDiary project.

## Input

- **Entity name**: ${input:entityName:Entity class name (PascalCase, e.g. Broker)}
- **Properties**: ${input:properties:Comma-separated properties with types (e.g. Name:string, Code:string, IsActive:bool, DisplayOrder:int)}

## Steps

### 1. Model class (`OzzTradeDiary.NET/OzzTradeDiary/Models/{entityName}.cs`)

Follow the pattern in `Currency.cs`:
- Namespace: `TD.Models`
- `partial class` with parameterless constructor initializing strings to `string.Empty`
- `int Id` primary key property
- XML doc comments on each property
- `Clone()` method copying all properties except `Id`

### 2. SQLite CodeGen settings (`OzzCodeGen/SqliteScriptsGen.settings`)

Add a new `<EntitySetting>` block in the existing settings file:
- Map C# types to SQLite: `string` → `TEXT`, `int` → `INTEGER`, `decimal` → `REAL`, `bool` → `INTEGER`, `DateTime` → `INTEGER`
- Set `Nullable` appropriately (strings usually `Not Null`, optional fields `Null`)
- Include `IPrimaryKey` as base entity
- Add `IDisplayOrder` if the entity has `DisplayOrder`
- Add `IIsActive` if the entity has `IsActive`

### 3. Repository (`OzzTradeDiary.NET/OzzTradeDiary.SQLite/{entityName}Repository.cs`)

Follow the pattern in `SqliteDatabaseMetadataRepository.cs`:
- Namespace: `TD.SQLite`
- Constructor accepts `string databasePath`, builds connection string
- Use `Microsoft.Data.Sqlite` with parameterized queries
- Implement CRUD: `GetAllAsync()`, `GetByIdAsync(int id)`, `InsertAsync()`, `UpdateAsync()`, `DeleteAsync(int id)`
- Use `async/await` throughout

### 4. ViewModel (`OzzTradeDiary.NET/OzzTradeDiary.WPF/ViewModels/{entityName}CollectionVM.cs`)

Follow the pattern in `AbstractCollectionVM<T>`:
- Namespace: `TD.WPF.ViewModels`
- Extend `AbstractCollectionVM<{entityName}>`
- Implement `OnSearchStringChanged()` with string filtering
- Implement `OnSelectedItemChanging()` and `OnSelectedItemChanged()`

After creating all files, build the solution with `dotnet build OzzTradeDiary.slnx` to verify no compile errors.
