using Microsoft.Data.Sqlite;
using TD.Models;

namespace TD.SQLite;

/// <summary>
/// SQLite-based repository for symbol CRUD operations.
/// </summary>
public class SqliteDatabaseSymbolRepository : IDatabaseSymbolRepository
{
    private readonly string _connectionString;
    private readonly SqliteDatabaseMetadataRepository _metadataRepository;

    public SqliteDatabaseSymbolRepository(string databasePath, SqliteDatabaseMetadataRepository? metadataRepository = null)
    {
        _connectionString = $"Data Source={databasePath}";
        _metadataRepository = metadataRepository ?? new SqliteDatabaseMetadataRepository(databasePath);
        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        SqliteDbScriptInitializer.ExecuteScript(connection, "Symbol.sql");
        SqliteDbScriptInitializer.SeedIfEmpty(connection, "Symbols", "Symbols-Data.sql");
    }

    public async Task<IReadOnlyList<Symbol>> GetAllAsync(bool? isActive = null)
    {
        var result = new List<Symbol>();

        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        await using var command = connection.CreateCommand();
        command.CommandText = @"
            SELECT Id, Ticker, TickerFull, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive
            FROM Symbols";

        if (isActive.HasValue)
        {
            command.CommandText += " WHERE IsActive = @isActive";
            command.Parameters.AddWithValue("@isActive", isActive.Value ? 1 : 0);
        }

        command.CommandText += " ORDER BY DisplayOrder, Ticker";

        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            result.Add(MapSymbol(reader));
        }

        return result;
    }

    public async Task<Symbol?> GetByTickerFullAsync(string tickerFull)
    {
        if (string.IsNullOrWhiteSpace(tickerFull))
            return null;

        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        await using var command = connection.CreateCommand();
        command.CommandText = @"
            SELECT Id, Ticker, TickerFull, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive
            FROM Symbols
            WHERE TickerFull = @tickerFull";
        command.Parameters.AddWithValue("@tickerFull", tickerFull);

        await using var reader = await command.ExecuteReaderAsync();
        if (!await reader.ReadAsync())
            return null;

        return MapSymbol(reader);
    }

    public async Task<Symbol?> GetByIdAsync(int id)
    {
        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        await using var command = connection.CreateCommand();
        command.CommandText = @"
            SELECT Id, Ticker, TickerFull, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive
            FROM Symbols
            WHERE Id = @id";
        command.Parameters.AddWithValue("@id", id);

        await using var reader = await command.ExecuteReaderAsync();
        if (!await reader.ReadAsync())
            return null;

        return MapSymbol(reader);
    }

    public async Task<int> CreateAsync(Symbol symbol)
    {
        ArgumentNullException.ThrowIfNull(symbol);

        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        var existingSymbol = await GetByTickerFullAsync(symbol.TickerFull);
        if (existingSymbol != null)
        {
            symbol.Id = existingSymbol.Id;
            await UpdateAsync(symbol);
            return existingSymbol.Id;
        }

        await using var command = connection.CreateCommand();
        command.CommandText = @"
            INSERT INTO Symbols (Ticker, TickerFull, BaseCurrency, PriceCurrency, Description, ExchangeId, MarketType, DisplayOrder, IsActive)
            VALUES (@ticker, @tickerFull, @baseCurrency, @priceCurrency, @description, @exchangeId, @marketType, @displayOrder, @isActive);
            SELECT last_insert_rowid();";

        command.Parameters.AddWithValue("@ticker", symbol.Ticker);
        command.Parameters.AddWithValue("@tickerFull", symbol.TickerFull);
        command.Parameters.AddWithValue("@baseCurrency", (object?)symbol.BaseCurrency ?? DBNull.Value);
        command.Parameters.AddWithValue("@priceCurrency", symbol.PriceCurrency);
        command.Parameters.AddWithValue("@description", (object?)symbol.Description ?? DBNull.Value);
        command.Parameters.AddWithValue("@exchangeId", symbol.ExchangeId);
        command.Parameters.AddWithValue("@marketType", (int)symbol.MarketType);
        command.Parameters.AddWithValue("@displayOrder", symbol.DisplayOrder);
        command.Parameters.AddWithValue("@isActive", symbol.IsActive ? 1 : 0);

        var id = Convert.ToInt32((long)(await command.ExecuteScalarAsync() ?? 0));

        await _metadataRepository.SaveLastUpdateUtcAsync(connection);

        return id;
    }

    public async Task<bool> UpdateAsync(Symbol symbol)
    {
        ArgumentNullException.ThrowIfNull(symbol);

        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        var existingSymbolById = await GetByIdAsync(symbol.Id);
        if (existingSymbolById is null)
            return false;

        if (!string.Equals(existingSymbolById.TickerFull, symbol.TickerFull, StringComparison.Ordinal))
            throw new InvalidOperationException($"TickerFull cannot be changed after creation. Existing: {existingSymbolById.TickerFull}, Requested: {symbol.TickerFull}");

        var existingSymbolByTickerFull = await GetByTickerFullAsync(symbol.TickerFull);
        if (existingSymbolByTickerFull != null && existingSymbolByTickerFull.Id != symbol.Id)
            throw new InvalidOperationException($"A different symbol with the same TickerFull already exists: {symbol.TickerFull}");

        bool noChanges = string.Equals(existingSymbolById.Description, symbol.Description, StringComparison.Ordinal)
                      && existingSymbolById.DisplayOrder == symbol.DisplayOrder
                      && existingSymbolById.IsActive == symbol.IsActive;

        if (noChanges)
            return false;

        await using var command = connection.CreateCommand();
        // TickerFull, Ticker, BaseCurrency, ExchangeId, MarketType, and PriceCurrency are immutable after creation, so they are not included in the update statement
        command.CommandText = @"
            UPDATE Symbols
            SET Description = @description,
                DisplayOrder = @displayOrder,
                IsActive = @isActive
            WHERE Id = @id";

        command.Parameters.AddWithValue("@id", symbol.Id);
        command.Parameters.AddWithValue("@description", (object?)symbol.Description ?? DBNull.Value);
        command.Parameters.AddWithValue("@displayOrder", symbol.DisplayOrder);
        command.Parameters.AddWithValue("@isActive", symbol.IsActive ? 1 : 0);

        var affectedRows = await command.ExecuteNonQueryAsync();
        if (affectedRows > 0)
            await _metadataRepository.SaveLastUpdateUtcAsync(connection);

        return affectedRows > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        await using var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM Symbols WHERE Id = @id";
        command.Parameters.AddWithValue("@id", id);

        var affectedRows = await command.ExecuteNonQueryAsync();
        if (affectedRows > 0)
            await _metadataRepository.SaveLastUpdateUtcAsync(connection);

        return affectedRows > 0;
    }

    private static Symbol MapSymbol(SqliteDataReader reader)
    {
        return new Symbol
        {
            Id = reader.GetInt32(0),
            Ticker = reader.GetString(1),
            TickerFull = reader.GetString(2),
            BaseCurrency = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
            PriceCurrency = reader.GetString(4),
            Description = reader.IsDBNull(5) ? string.Empty : reader.GetString(5),
            ExchangeId = reader.GetInt32(6),
            MarketType = (MarketType)reader.GetInt32(7),
            DisplayOrder = reader.GetInt32(8),
            IsActive = reader.GetInt64(9) == 1
        };
    }
}
