using Microsoft.Data.Sqlite;

namespace TD.SQLite;

/// <summary>
/// Repository interface for accessing database metadata.
/// </summary>
public interface IDbMetadataRepository
{
    /// <summary>
    /// Gets the last database update time in UTC.
    /// </summary>
    /// <returns>The last update time, or null if never updated.</returns>
    Task<DateTime?> GetLastUpdateTimeUtcAsync();

    /// <summary>
    /// Gets the last database update time in local time.
    /// </summary>
    /// <returns>The last update time in local time, or null if never updated.</returns>
    Task<DateTime?> GetLastUpdateTimeAsync();

    /// <summary>
    /// Saves the current UTC time as the last database update time.
    /// </summary>
    /// <param name="connection">The SQLite connection to use for the update.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SaveLastUpdateUtcAsync(SqliteConnection connection);
}
