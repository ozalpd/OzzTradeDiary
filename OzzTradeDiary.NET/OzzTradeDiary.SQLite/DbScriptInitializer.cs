using Microsoft.Data.Sqlite;

namespace TD.SQLite;

internal static class DbScriptInitializer
{
    private static readonly string ScriptsDirectory = Path.Combine(AppContext.BaseDirectory, "DbScripts");

    public static void ExecuteScript(SqliteConnection connection, string scriptFileName)
    {
        var scriptPath = Path.Combine(ScriptsDirectory, scriptFileName);
        if (!File.Exists(scriptPath))
            throw new FileNotFoundException($"SQL script file not found: {scriptPath}", scriptPath);

        var sql = File.ReadAllText(scriptPath);
        if (string.IsNullOrWhiteSpace(sql))
            return;

        using var command = connection.CreateCommand();
        command.CommandText = sql;
        command.ExecuteNonQuery();
    }

    public static void SeedIfEmpty(SqliteConnection connection, string tableName, string seedScriptFileName)
    {
        using var countCommand = connection.CreateCommand();
        countCommand.CommandText = $"SELECT COUNT(1) FROM {tableName}";

        var count = Convert.ToInt64(countCommand.ExecuteScalar());
        if (count > 0)
            return;

        ExecuteScript(connection, seedScriptFileName);
    }
}
