using Microsoft.Data.Sqlite;
using TD.Models;

namespace TD.SQLite;

/// <summary>
/// SQLite-based repository for trading account CRUD operations.
/// </summary>
public class SqliteDatabaseTradingAccountRepository : IDatabaseTradingAccountRepository
{
    private readonly string _connectionString;
    private readonly SqliteDatabaseMetadataRepository _metadataRepository;

    public SqliteDatabaseTradingAccountRepository(string databasePath, SqliteDatabaseMetadataRepository? metadataRepository = null)
    {
        _connectionString = $"Data Source={databasePath}";
        _metadataRepository = metadataRepository ?? new SqliteDatabaseMetadataRepository(databasePath);
        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        SqliteDbScriptInitializer.ExecuteScript(connection, "TradingAccount.sql");
    }

    public async Task<IReadOnlyList<TradingAccount>> GetAllAsync(bool? isActive = null)
    {
        var result = new List<TradingAccount>();

        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        await using var command = connection.CreateCommand();
        command.CommandText = @"
            SELECT Id, Title, AccountCode, ExchangeId, Notes, DisplayOrder, IsActive
            FROM TradingAccounts";

        if (isActive.HasValue)
        {
            command.CommandText += " WHERE IsActive = @isActive";
            command.Parameters.AddWithValue("@isActive", isActive.Value ? 1 : 0);
        }

        command.CommandText += " ORDER BY DisplayOrder, Title";

        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            result.Add(MapTradingAccount(reader));
        }

        return result;
    }

    public async Task<TradingAccount?> GetByIdAsync(int id)
    {
        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        await using var command = connection.CreateCommand();
        command.CommandText = @"
            SELECT Id, Title, AccountCode, ExchangeId, Notes, DisplayOrder, IsActive
            FROM TradingAccounts
            WHERE Id = @id";
        command.Parameters.AddWithValue("@id", id);

        await using var reader = await command.ExecuteReaderAsync();
        if (!await reader.ReadAsync())
            return null;

        return MapTradingAccount(reader);
    }

    public async Task<int> CreateAsync(TradingAccount tradingAccount)
    {
        ArgumentNullException.ThrowIfNull(tradingAccount);

        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        await using var command = connection.CreateCommand();
        command.CommandText = @"
            INSERT INTO TradingAccounts (Title, AccountCode, ExchangeId, Notes, DisplayOrder, IsActive)
            VALUES (@title, @accountCode, @exchangeId, @notes, @displayOrder, @isActive);
            SELECT last_insert_rowid();";

        command.Parameters.AddWithValue("@title", tradingAccount.Title);
        command.Parameters.AddWithValue("@accountCode", (object?)tradingAccount.AccountCode ?? DBNull.Value);
        command.Parameters.AddWithValue("@exchangeId", tradingAccount.ExchangeId);
        command.Parameters.AddWithValue("@notes", (object?)tradingAccount.Notes ?? DBNull.Value);
        command.Parameters.AddWithValue("@displayOrder", tradingAccount.DisplayOrder);
        command.Parameters.AddWithValue("@isActive", tradingAccount.IsActive ? 1 : 0);

        var id = Convert.ToInt32((long)(await command.ExecuteScalarAsync() ?? 0));

        await _metadataRepository.SaveLastUpdateUtcAsync(connection);

        return id;
    }

    public async Task<bool> UpdateAsync(TradingAccount tradingAccount)
    {
        ArgumentNullException.ThrowIfNull(tradingAccount);

        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        await using var command = connection.CreateCommand();
        command.CommandText = @"
            UPDATE TradingAccounts
            SET Title = @title,
                AccountCode = @accountCode,
                ExchangeId = @exchangeId,
                Notes = @notes,
                DisplayOrder = @displayOrder,
                IsActive = @isActive
            WHERE Id = @id";

        command.Parameters.AddWithValue("@id", tradingAccount.Id);
        command.Parameters.AddWithValue("@title", tradingAccount.Title);
        command.Parameters.AddWithValue("@accountCode", (object?)tradingAccount.AccountCode ?? DBNull.Value);
        command.Parameters.AddWithValue("@exchangeId", tradingAccount.ExchangeId);
        command.Parameters.AddWithValue("@notes", (object?)tradingAccount.Notes ?? DBNull.Value);
        command.Parameters.AddWithValue("@displayOrder", tradingAccount.DisplayOrder);
        command.Parameters.AddWithValue("@isActive", tradingAccount.IsActive ? 1 : 0);

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
        command.CommandText = "DELETE FROM TradingAccounts WHERE Id = @id";
        command.Parameters.AddWithValue("@id", id);

        var affectedRows = await command.ExecuteNonQueryAsync();
        if (affectedRows > 0)
            await _metadataRepository.SaveLastUpdateUtcAsync(connection);

        return affectedRows > 0;
    }

    private static TradingAccount MapTradingAccount(SqliteDataReader reader)
    {
        return new TradingAccount
        {
            Id = reader.GetInt32(0),
            Title = reader.GetString(1),
            AccountCode = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
            ExchangeId = reader.GetInt32(3),
            Notes = reader.IsDBNull(4) ? null : reader.GetString(4),
            DisplayOrder = reader.GetInt32(5),
            IsActive = reader.GetInt64(6) == 1
        };
    }
}
