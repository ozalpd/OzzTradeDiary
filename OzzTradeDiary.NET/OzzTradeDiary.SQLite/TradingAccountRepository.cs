using Microsoft.Data.Sqlite;
using TD.Models;

namespace TD.SQLite;

/// <summary>
/// SQLite-based repository for trading account CRUD operations.
/// </summary>
public class TradingAccountRepository : AbstractDatabaseRepository<TradingAccount>, IDbTradingAccountRepository
{
    public TradingAccountRepository(string databasePath,
                                    IExchangeRepository? exchangeRepository = null) : base(databasePath, "TradingAccounts")
    {
        _selectStatement = $"SELECT {string.Join(", ", ColumnNames)} FROM {_tableName}";
        _exchangeRepository = exchangeRepository ?? new ExchangeRepository(databasePath);
        InitializeDatabase();
    }
    private readonly string _selectStatement;
    private readonly IExchangeRepository _exchangeRepository;

    private void InitializeDatabase()
    {
        using var connection = GetOpenConnection();
        ExecuteScript(connection, "TradingAccount.sql");
    }

    public async Task<IReadOnlyList<TradingAccount>> GetAllAsync(bool? isActive = null)
    {
        var result = new List<TradingAccount>();
        var exchangesById = (await _exchangeRepository.GetAllAsync()).ToDictionary(item => item.Id);

        await using var connection = await GetOpenConnectionAsync();
        await using var command = connection.CreateCommand();
        command.CommandText = _selectStatement;

        if (isActive.HasValue)
        {
            command.CommandText += " WHERE IsActive = @isActive";
            command.Parameters.AddWithValue("@isActive", isActive.Value ? 1 : 0);
        }

        command.CommandText += " ORDER BY DisplayOrder, Title";

        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            var tradingAccount = MapTradingAccount(reader);
            if (exchangesById.TryGetValue(tradingAccount.ExchangeId, out var exchange))
                tradingAccount.Exchange = exchange;

            result.Add(tradingAccount);
        }

        return result;
    }

    public async Task<TradingAccount?> GetByIdAsync(int id)
    {
        await using var connection = await GetOpenConnectionAsync();
        await using var command = connection.CreateCommand();
        command.CommandText = $"{_selectStatement} WHERE Id = @id";
        command.Parameters.AddWithValue("@id", id);

        await using var reader = await command.ExecuteReaderAsync();
        if (!await reader.ReadAsync())
            return null;

        var tradingAccount = MapTradingAccount(reader);
        await PopulateExchangeAsync(tradingAccount);

        return tradingAccount;
    }

    public async Task<TradingAccount?> GetByTitleAsync(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            return null;

        await using var connection = await GetOpenConnectionAsync();
        await using var command = connection.CreateCommand();
        command.CommandText = $"{_selectStatement} WHERE Title = @title";
        command.Parameters.AddWithValue("@title", title);

        await using var reader = await command.ExecuteReaderAsync();
        if (!await reader.ReadAsync())
            return null;

        var tradingAccount = MapTradingAccount(reader);
        await PopulateExchangeAsync(tradingAccount);

        return tradingAccount;
    }

    public async Task<int> CreateAsync(TradingAccount tradingAccount)
    {
        ArgumentNullException.ThrowIfNull(tradingAccount);
        ValidateOrThrow(tradingAccount);

        await using var connection = await GetOpenConnectionAsync();
        var existingTradingAccount = await GetByTitleAsync(tradingAccount.Title);
        if (existingTradingAccount != null)
        {
            tradingAccount.Id = existingTradingAccount.Id;
            await UpdateAsync(tradingAccount);
            return existingTradingAccount.Id;
        }

        await using var command = connection.CreateCommand();
        command.CommandText = $@"INSERT INTO {_tableName} ({string.Join(", ", ColumnNames[1..])})
            VALUES (@title, @exchangeId, @notes, @displayOrder, @isActive);
            SELECT last_insert_rowid();";

        command.Parameters.AddWithValue("@title", tradingAccount.Title);
        command.Parameters.AddWithValue("@exchangeId", tradingAccount.ExchangeId);
        AddNullableTextParameter(command, "@notes", tradingAccount.Notes);
        command.Parameters.AddWithValue("@displayOrder", tradingAccount.DisplayOrder);
        command.Parameters.AddWithValue("@isActive", tradingAccount.IsActive ? 1 : 0);

        var id = Convert.ToInt32((long)(await command.ExecuteScalarAsync() ?? 0));

        await _metadataRepository.SaveLastUpdateUtcAsync(connection);
        ClearRecordCountCache();

        return id;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        await using var connection = await GetOpenConnectionAsync();
        await using var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM TradingAccounts WHERE Id = @id";
        command.Parameters.AddWithValue("@id", id);

        var affectedRows = await command.ExecuteNonQueryAsync();
        if (affectedRows > 0)
        {
            await _metadataRepository.SaveLastUpdateUtcAsync(connection);
            ClearRecordCountCache();
        }

        return affectedRows > 0;
    }

    public async Task<bool> UpdateAsync(TradingAccount tradingAccount)
    {
        ArgumentNullException.ThrowIfNull(tradingAccount);
        ValidateOrThrow(tradingAccount);

        await using var connection = await GetOpenConnectionAsync();
        var existingTradingAccount = await GetByTitleAsync(tradingAccount.Title);
        if (existingTradingAccount != null && existingTradingAccount.Id != tradingAccount.Id)
            throw new InvalidOperationException($"A different trading account with the same title already exists: {tradingAccount.Title}");

        bool noChanges = existingTradingAccount != null
                      && existingTradingAccount.Title.Equals(tradingAccount.Title)
                      && existingTradingAccount.ExchangeId == tradingAccount.ExchangeId
                      && string.Equals(existingTradingAccount.Notes, tradingAccount.Notes, StringComparison.Ordinal)
                      && existingTradingAccount.DisplayOrder == tradingAccount.DisplayOrder
                      && existingTradingAccount.IsActive == tradingAccount.IsActive;

        if (noChanges)
            return false;

        await using var command = connection.CreateCommand();
        // Title and ExchangeId are not updated to avoid complications with existing references,
        // so only Notes, DisplayOrder and IsActive are updated
        command.CommandText = @"
            UPDATE TradingAccounts
            SET Notes = @notes,
                DisplayOrder = @displayOrder,
                IsActive = @isActive
            WHERE Id = @id";

        command.Parameters.AddWithValue("@id", tradingAccount.Id);
        AddNullableTextParameter(command, "@notes", tradingAccount.Notes);
        command.Parameters.AddWithValue("@displayOrder", tradingAccount.DisplayOrder);
        command.Parameters.AddWithValue("@isActive", tradingAccount.IsActive ? 1 : 0);

        var affectedRows = await command.ExecuteNonQueryAsync();
        if (affectedRows > 0)
            await _metadataRepository.SaveLastUpdateUtcAsync(connection);

        return affectedRows > 0;
    }

    private async Task PopulateExchangeAsync(TradingAccount tradingAccount)
    {
        tradingAccount.Exchange = await _exchangeRepository.GetByIdAsync(tradingAccount.ExchangeId)
                                ?? new Exchange { Id = tradingAccount.ExchangeId };
    }


    private static TradingAccount MapTradingAccount(SqliteDataReader reader)
    {
        var tradingAccount = new TradingAccount
        {
            Id = reader.GetInt32(ColNrs.Id),
            Title = reader.GetString(ColNrs.Title),
            ExchangeId = reader.GetInt32(ColNrs.ExchangeId),
            Notes = reader.IsDBNull(ColNrs.Notes) ? null : reader.GetString(ColNrs.Notes),
            DisplayOrder = reader.GetInt32(ColNrs.DisplayOrder),
            IsActive = reader.GetInt64(ColNrs.IsActive) == 1

        };

        return tradingAccount;
    }

    public readonly struct ColNrs
    {
        public readonly static int Id = 0;
        public readonly static int Title = 1;
        public readonly static int ExchangeId = 2;
        public readonly static int Notes = 3;
        public readonly static int DisplayOrder = 4;
        public readonly static int IsActive = 5;
    }

    public readonly string[] ColumnNames = new[] {
            "Id",
            "Title",
            "ExchangeId",
            "Notes",
            "DisplayOrder",
            "IsActive"
        };
}
