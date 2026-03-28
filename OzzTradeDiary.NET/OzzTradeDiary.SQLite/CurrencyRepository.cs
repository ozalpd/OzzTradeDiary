using Microsoft.Data.Sqlite;
using TD.Models;

namespace TD.SQLite;

/// <summary>
/// SQLite-based repository for currency CRUD operations.
/// </summary>
public class CurrencyRepository : AbstractDatabaseRepository<Currency>, IDbCurrencyRepository
{
    public CurrencyRepository(string databasePath) : base(databasePath, "Currencies")
    {
        _selectStatement = $"SELECT Id, CurrencyTicker, Description, DisplayOrder, IsActive FROM {_tableName}";
        InitializeDatabase();
    }
    private readonly string _selectStatement;

    private void InitializeDatabase()
    {
        using var connection = GetOpenConnection();
        DbScriptInitializer.ExecuteScript(connection, "Currency.sql");
        SeedIfEmpty("Currencies-Data.sql");
    }

    public async Task<IReadOnlyList<Currency>> GetAllAsync(bool? isActive = null)
    {
        var result = new List<Currency>();

        await using var connection = await GetOpenConnectionAsync();
        await using var command = connection.CreateCommand();
        command.CommandText = _selectStatement;

        if (isActive.HasValue)
        {
            command.CommandText += " WHERE IsActive = @isActive";
            command.Parameters.AddWithValue("@isActive", isActive.Value ? 1 : 0);
        }

        command.CommandText += " ORDER BY DisplayOrder, CurrencyTicker";

        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            result.Add(MapCurrency(reader));
        }

        return result;
    }

    public async Task<Currency?> GetByCurrencyTickerAsync(string currencyTicker)
    {
        if (string.IsNullOrWhiteSpace(currencyTicker))
            return null;
        await using var connection = await GetOpenConnectionAsync();
        await using var command = connection.CreateCommand();
        command.CommandText = $"{_selectStatement} WHERE CurrencyTicker = @currencyTicker";
        command.Parameters.AddWithValue("@currencyTicker", currencyTicker);
        await using var reader = await command.ExecuteReaderAsync();
        if (!await reader.ReadAsync())
            return null;
        return MapCurrency(reader);
    }

    public async Task<Currency?> GetByIdAsync(int id)
    {
        await using var connection = await GetOpenConnectionAsync();
        await using var command = connection.CreateCommand();
        command.CommandText = $"{_selectStatement} WHERE Id = @id";
        command.Parameters.AddWithValue("@id", id);

        await using var reader = await command.ExecuteReaderAsync();
        if (!await reader.ReadAsync())
            return null;

        return MapCurrency(reader);
    }

    public async Task<int> CreateAsync(Currency currency)
    {
        ArgumentNullException.ThrowIfNull(currency);
        ValidateOrThrow(currency);

        await using var connection = await GetOpenConnectionAsync();
        var existingCurrency = await GetByCurrencyTickerAsync(currency.CurrencyTicker);
        if (existingCurrency != null)
        {
            currency.Id = existingCurrency.Id;
            await UpdateAsync(currency);
            return existingCurrency.Id;
        }

        await using var command = connection.CreateCommand();
        command.CommandText = @"
            INSERT INTO Currencies (CurrencyTicker, Description, DisplayOrder, IsActive)
            VALUES (@currencyTicker, @description, @displayOrder, @isActive);
            SELECT last_insert_rowid();";

        command.Parameters.AddWithValue("@currencyTicker", currency.CurrencyTicker);
        AddNullableTextParameter(command, "@description", currency.Description);
        command.Parameters.AddWithValue("@displayOrder", currency.DisplayOrder);
        command.Parameters.AddWithValue("@isActive", currency.IsActive ? 1 : 0);

        var id = Convert.ToInt32((long)(await command.ExecuteScalarAsync() ?? 0));

        await _metadataRepository.SaveLastUpdateUtcAsync(connection);
        ClearRecordCountCache();

        return id;
    }

    public async Task<bool> UpdateAsync(Currency currency)
    {
        ArgumentNullException.ThrowIfNull(currency);
        ValidateOrThrow(currency);

        await using var connection = await GetOpenConnectionAsync();
        var existingCurrency = await GetByCurrencyTickerAsync(currency.CurrencyTicker);
        if (existingCurrency != null && existingCurrency.Id != currency.Id)
            throw new InvalidOperationException($"A different currency with the same ticker already exists: {currency.CurrencyTicker}");

        bool noChanges = existingCurrency != null
                      && (existingCurrency.Description ?? string.Empty).Equals(currency.Description ?? string.Empty)
                      && existingCurrency.DisplayOrder == currency.DisplayOrder
                      && existingCurrency.IsActive == currency.IsActive;

        if (noChanges)
            return false;

        await using var command = connection.CreateCommand();
        //CurrencyTicker is not updated to avoid complications with existing references in trades,
        //so only Description, DisplayOrder and IsActive are updated
        command.CommandText = @"
            UPDATE Currencies
            SET Description = @description,
                DisplayOrder = @displayOrder,
                IsActive = @isActive
            WHERE Id = @id";

        command.Parameters.AddWithValue("@id", currency.Id);
        AddNullableTextParameter(command, "@description", currency.Description);
        command.Parameters.AddWithValue("@displayOrder", currency.DisplayOrder);
        command.Parameters.AddWithValue("@isActive", currency.IsActive ? 1 : 0);

        var affectedRows = await command.ExecuteNonQueryAsync();
        if (affectedRows > 0)
            await _metadataRepository.SaveLastUpdateUtcAsync(connection);

        return affectedRows > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        await using var connection = await GetOpenConnectionAsync();
        await using var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM Currencies WHERE Id = @id";
        command.Parameters.AddWithValue("@id", id);

        var affectedRows = await command.ExecuteNonQueryAsync();
        if (affectedRows > 0)
        {
            await _metadataRepository.SaveLastUpdateUtcAsync(connection);
            ClearRecordCountCache();
        }

        return affectedRows > 0;
    }

    private static Currency MapCurrency(SqliteDataReader reader)
    {
        return new Currency
        {
            Id = reader.GetInt32(0),
            CurrencyTicker = reader.GetString(1),
            Description = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
            DisplayOrder = reader.GetInt32(3),
            IsActive = reader.GetInt64(4) == 1
        };
    }
}
