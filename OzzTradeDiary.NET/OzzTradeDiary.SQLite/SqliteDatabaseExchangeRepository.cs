using Microsoft.Data.Sqlite;
using TD.Models;

namespace TD.SQLite;

/// <summary>
/// SQLite-based repository for exchange CRUD operations.
/// </summary>
public class SqliteDatabaseExchangeRepository : AbstractDatabaseRepository, IDatabaseExchangeRepository
{
    private readonly string _connectionString;
    private readonly SqliteDatabaseMetadataRepository _metadataRepository;

    public SqliteDatabaseExchangeRepository(string databasePath, SqliteDatabaseMetadataRepository? metadataRepository = null)
    {
        _connectionString = $"Data Source={databasePath}";
        _metadataRepository = metadataRepository ?? new SqliteDatabaseMetadataRepository(databasePath);
        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        SqliteDbScriptInitializer.ExecuteScript(connection, "Exchange.sql");
        SqliteDbScriptInitializer.SeedIfEmpty(connection, "Exchanges", "Exchanges-Data.sql");
    }
    private readonly string _selectStatement = "SELECT Id, ExchangeName, ExchangeCode, DefaultCurrency, DisplayOrder, IsActive FROM Exchanges";

    public async Task<IReadOnlyList<Exchange>> GetAllAsync(bool? isActive = null)
    {
        var result = new List<Exchange>();

        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        await using var command = connection.CreateCommand();
        command.CommandText = _selectStatement;

        if (isActive.HasValue)
        {
            command.CommandText += " WHERE IsActive = @isActive";
            command.Parameters.AddWithValue("@isActive", isActive.Value ? 1 : 0);
        }

        command.CommandText += " ORDER BY DisplayOrder, ExchangeName";

        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            result.Add(MapExchange(reader));
        }

        return result;
    }

    public async Task<Exchange?> GetByExchangeCodeAsync(string exchangeCode)
    {
        if (string.IsNullOrWhiteSpace(exchangeCode))
            return null;

        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        await using var command = connection.CreateCommand();
        command.CommandText = $@"{_selectStatement}
            WHERE ExchangeCode = @exchangeCode";
        command.Parameters.AddWithValue("@exchangeCode", exchangeCode);

        await using var reader = await command.ExecuteReaderAsync();
        if (!await reader.ReadAsync())
            return null;

        return MapExchange(reader);
    }

    public async Task<Exchange?> GetByIdAsync(int id)
    {
        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        await using var command = connection.CreateCommand();
        command.CommandText = $@"{_selectStatement}
            WHERE Id = @id";
        command.Parameters.AddWithValue("@id", id);

        await using var reader = await command.ExecuteReaderAsync();
        if (!await reader.ReadAsync())
            return null;

        return MapExchange(reader);
    }

    public async Task<int> CreateAsync(Exchange exchange)
    {
        ArgumentNullException.ThrowIfNull(exchange);
        ValidateOrThrow(exchange);

        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        var existingExchange = await GetByExchangeCodeAsync(exchange.ExchangeCode);
        if (existingExchange != null)
        {
            exchange.Id = existingExchange.Id;
            await UpdateAsync(exchange);
            return existingExchange.Id;
        }

        await using var command = connection.CreateCommand();
        command.CommandText = @"
            INSERT INTO Exchanges (ExchangeName, ExchangeCode, DefaultCurrency, DisplayOrder, IsActive)
            VALUES (@exchangeName, @exchangeCode, @defaultCurrency, @displayOrder, @isActive);
            SELECT last_insert_rowid();";

        command.Parameters.AddWithValue("@exchangeName", exchange.ExchangeName);
        command.Parameters.AddWithValue("@exchangeCode", exchange.ExchangeCode);
        AddNullableTextParameter(command, "@defaultCurrency", exchange.DefaultCurrency);
        command.Parameters.AddWithValue("@displayOrder", exchange.DisplayOrder);
        command.Parameters.AddWithValue("@isActive", exchange.IsActive ? 1 : 0);

        var id = Convert.ToInt32((long)(await command.ExecuteScalarAsync() ?? 0));

        await _metadataRepository.SaveLastUpdateUtcAsync(connection);

        return id;
    }

    public async Task<bool> UpdateAsync(Exchange exchange)
    {
        ArgumentNullException.ThrowIfNull(exchange);
        ValidateOrThrow(exchange);

        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        var existingExchange = await GetByExchangeCodeAsync(exchange.ExchangeCode);
        if (existingExchange != null && existingExchange.Id != exchange.Id)
            throw new InvalidOperationException($"A different exchange with the same code already exists: {exchange.ExchangeCode}");

        bool noChanges = existingExchange != null
                      && existingExchange.ExchangeName.Equals(exchange.ExchangeName)
                      && existingExchange.DefaultCurrency == exchange.DefaultCurrency
                      && existingExchange.DisplayOrder == exchange.DisplayOrder
                      && existingExchange.IsActive == exchange.IsActive;

        if (noChanges)
            return false;

        await using var command = connection.CreateCommand();
        // ExchangeCode is not updated to avoid complications with existing references,
        // so only ExchangeName, DisplayOrder and IsActive are updated
        command.CommandText = @"
            UPDATE Exchanges
            SET ExchangeName = @exchangeName,
                DefaultCurrency = @defaultCurrency,
                DisplayOrder = @displayOrder,
                IsActive = @isActive
            WHERE Id = @id";

        command.Parameters.AddWithValue("@id", exchange.Id);
        command.Parameters.AddWithValue("@exchangeName", exchange.ExchangeName);
        AddNullableTextParameter(command, "@defaultCurrency", exchange.DefaultCurrency);
        command.Parameters.AddWithValue("@displayOrder", exchange.DisplayOrder);
        command.Parameters.AddWithValue("@isActive", exchange.IsActive ? 1 : 0);

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
        command.CommandText = "DELETE FROM Exchanges WHERE Id = @id";
        command.Parameters.AddWithValue("@id", id);

        var affectedRows = await command.ExecuteNonQueryAsync();
        if (affectedRows > 0)
            await _metadataRepository.SaveLastUpdateUtcAsync(connection);

        return affectedRows > 0;
    }

    private static Exchange MapExchange(SqliteDataReader reader)
    {
        return new Exchange
        {
            Id = reader.GetInt32(0),
            ExchangeName = reader.GetString(1),
            ExchangeCode = reader.GetString(2),
            DefaultCurrency = reader.IsDBNull(3) ? null : reader.GetString(3),
            DisplayOrder = reader.GetInt32(4),
            IsActive = reader.GetInt64(5) == 1
        };
    }
}
