using System.Globalization;
using Microsoft.Data.Sqlite;

namespace TD.SQLite;

/// <summary>
/// SQLite-based implementation of IDatabaseMetadataRepository.
/// </summary>
public class SqliteDatabaseMetadataRepository : IDatabaseMetadataRepository
{
    private readonly string _connectionString;

    /// <summary>
    /// Initializes a new instance with the specified database path.
    /// </summary>
    /// <param name="databasePath">The full path to the SQLite database file.</param>
    public SqliteDatabaseMetadataRepository(string databasePath)
    {
        _connectionString = $"Data Source={databasePath}";
        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var metadataCommand = connection.CreateCommand();
        metadataCommand.CommandText = @"
                CREATE TABLE IF NOT EXISTS DatabaseMetadata (
                    Id INTEGER PRIMARY KEY CHECK (Id = 1),
                    LastUpdateUtc TEXT NOT NULL
                );

                INSERT INTO DatabaseMetadata (Id, LastUpdateUtc)
                VALUES (1, @lastUpdateUtc)
                ON CONFLICT(Id) DO NOTHING;";
        metadataCommand.Parameters.AddWithValue("@lastUpdateUtc", string.Empty);
        metadataCommand.ExecuteNonQuery();

        var readMetadataCommand = connection.CreateCommand();
        readMetadataCommand.CommandText = "SELECT LastUpdateUtc FROM DatabaseMetadata WHERE Id = 1";
        var lastUpdateUtc = readMetadataCommand.ExecuteScalar() as string;
        LastUpdateTime = ToLocalDateTime(lastUpdateUtc);
    }

    /// <summary>
    /// Gets the last data update time in local time.
    /// </summary>
    public DateTime? LastUpdateTime { get; private set; }

    /// <summary>
    /// Gets the last database update time in UTC.
    /// </summary>
    /// <returns>The last update time, or null if never updated.</returns>
    public async Task<DateTime?> GetLastUpdateTimeUtcAsync()
    {
        using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT LastUpdateUtc FROM DatabaseMetadata WHERE Id = 1";
        
        var result = await command.ExecuteScalarAsync() as string;
        
        if (string.IsNullOrWhiteSpace(result))
        {
            return null;
        }

        if (DateTime.TryParse(result, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal, out var utcTime))
        {
            return utcTime;
        }

        return null;
    }

    public async Task SaveLastUpdateUtcAsync(SqliteConnection connection)
    {
        var nowUtc = DateTimeOffset.UtcNow;

        var metadataCommand = connection.CreateCommand();
        metadataCommand.CommandText = @"
                UPDATE DatabaseMetadata
                SET LastUpdateUtc = @lastUpdateUtc
                WHERE Id = 1";
        metadataCommand.Parameters.AddWithValue("@lastUpdateUtc", nowUtc.ToString("O"));

        await metadataCommand.ExecuteNonQueryAsync();
        LastUpdateTime = nowUtc.ToLocalTime().DateTime;
    }

    /// <summary>
    /// Gets the last database update time in local time.
    /// </summary>
    /// <returns>The last update time in local time, or null if never updated.</returns>
    public async Task<DateTime?> GetLastUpdateTimeAsync()
    {
        var utcTime = await GetLastUpdateTimeUtcAsync();
        return utcTime?.ToLocalTime();
    }

    private static DateTime? ToLocalDateTime(string? utcValue)
    {
        DateTime? dateTime = null;
        if (DateTimeOffset.TryParse(utcValue, out var parsedUtc))
            dateTime = parsedUtc.ToLocalTime().DateTime;

        return dateTime;
    }
}
