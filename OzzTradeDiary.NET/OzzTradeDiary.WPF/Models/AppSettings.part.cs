using System.IO;
using System.Text.Json;

namespace TD.WPF.Models;

public partial class AppSettings
{
    private static AppSettings? _instance;
    private static readonly object _syncRoot = new();

    /// <summary>
    /// Gets or sets the file path to the database used by the application. If the path is not set, a default path
    /// is generated.
    /// </summary>
    /// <remarks>The database path is automatically created if it does not exist when accessed. The
    /// default path is located in the application's default database folder and is named 'trades.db'.</remarks>
    public string DatabasePath
    {
        get
        {
            if (string.IsNullOrWhiteSpace(_dbPath))
            {
                _dbPath = Path.Combine(GetDefaultDatabaseFolderPath(), "trades.db");
            }

            var dbDirPath = Path.GetDirectoryName(_dbPath);
            if (!string.IsNullOrWhiteSpace(dbDirPath) && !Directory.Exists(dbDirPath))
            {
                Directory.CreateDirectory(dbDirPath);
            }

            return _dbPath;
        }

        set => _dbPath = value;
    }
    string _dbPath = string.Empty;

    private static string GetDefaultDatabaseFolderPath()
    {
#if DEBUG
        var sampleDataPath = TryGetDebugSampleDataFolderPath();
        if (!string.IsNullOrWhiteSpace(sampleDataPath))
        {
            return sampleDataPath;
        }
#endif

        string dbFolderPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "OzzTradeDiary");
        return dbFolderPath;
    }

    private static string? TryGetDebugSampleDataFolderPath()
    {
        var current = new DirectoryInfo(AppContext.BaseDirectory);
        while (current is not null)
        {
            var hasGitFolder = Directory.Exists(Path.Combine(current.FullName, ".git"));
            var hasSolution = File.Exists(Path.Combine(current.FullName, "OzzTradeDiary.slnx"));

            if (hasGitFolder || hasSolution)
            {
                return Path.Combine(current.FullName, "SampleData");
            }

            current = current.Parent;
        }

        return null;
    }

    public string GetDatabaseFolderPath() => Path.GetDirectoryName(DatabasePath) ?? string.Empty;

    public static AppSettings GetAppSettings()
    {
        if (_instance is not null)
        {
            return _instance;
        }

        lock (_syncRoot)
        {
            if (_instance is not null)
            {
                return _instance;
            }

            var settingsFilePath = GetSettingsFilePath();
            if (File.Exists(settingsFilePath))
            {
                var settingsJson = File.ReadAllText(settingsFilePath);
                if (!string.IsNullOrWhiteSpace(settingsJson))
                {
                    try
                    {
                        _instance = JsonSerializer.Deserialize<AppSettings>(settingsJson);
                    }
                    catch (JsonException)
                    {
                    }
                    catch (NotSupportedException)
                    {
                    }
                }
            }

            _instance ??= new AppSettings();
            return _instance;
        }
    }

    private static string GetSettingsFilePath()
    {
#if DEBUG
        var sampleDataPath = TryGetDebugSampleDataFolderPath();
        if (!string.IsNullOrWhiteSpace(sampleDataPath))
        {
            Directory.CreateDirectory(sampleDataPath);
            return Path.Combine(sampleDataPath, settingsFileName);
        }
#endif
        var settingsFolder = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "OzzTradeDiary");
        Directory.CreateDirectory(settingsFolder);

        return Path.Combine(settingsFolder, settingsFileName);
    }
    private static string settingsFileName = "tdsettings.json";

}

