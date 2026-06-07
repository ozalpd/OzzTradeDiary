using System.IO;
using System.Text.Json;

//This file contains properties that are specific to the WPF application, such as window positions and backup settings.
//It is separate from the core AppSettings class to avoid dependencies on WPF-specific types in the core library.
namespace TD.WPF.Models;

public partial class AppSettings
{
    public AppSettings() { }


    /// <summary>
    /// Gets or sets a value indicating whether automatic backups are enabled.
    /// </summary>
    /// <remarks>When set to <see langword="true"/>, the system performs backups automatically at
    /// scheduled intervals. Ensure that backup settings are properly configured to prevent data loss.</remarks>
    public bool AutoBackupEnabled { get; set; }

    /// <summary>
    /// Gets or sets the interval, in minutes, at which automatic backups are performed. The minimum allowed value
    /// is 10 minutes.
    /// </summary>
    /// <remarks>If a value less than 10 is specified, the interval is automatically adjusted to 10
    /// minutes to ensure backups occur at a reasonable frequency and to help prevent data loss.</remarks>
    public uint AutoBackupIntervalMinutes
    {
        get
        {
            if (backupInterval < 10)
            {
                backupInterval = 10;
            }
            return backupInterval;
        }

        set => backupInterval = value;
    }
    uint backupInterval = 10;

    /// <summary>
    /// Gets or sets the path to the folder where backups are stored. If the folder does not exist, it is created
    /// automatically.
    /// </summary>
    /// <remarks>The backup folder path can be set to a custom location. If not set, the default
    /// backup folder path is used. Ensure that the specified path has the necessary permissions for creating
    /// directories.</remarks>
    public string BackupFolder
    {
        get
        {
            if (string.IsNullOrWhiteSpace(_backupDir))
            {
                _backupDir = GetDefaultBackupFolderPath();
            }

            if (!string.IsNullOrWhiteSpace(_backupDir) && !Directory.Exists(_backupDir))
            {
                Directory.CreateDirectory(_backupDir);
            }
            return _backupDir;
        }
        set => _backupDir = value;
    }
    string _backupDir = string.Empty;

    private static string GetDefaultBackupFolderPath() => Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
        "OzzTradeDiary",
        "BackUp");


    /// <summary>
    /// Gets or sets the time of the last backup in Coordinated Universal Time (UTC).
    /// </summary>
    /// <remarks>This property is null if no backup has been performed.</remarks>
    public DateTime? LastBackupTimeUtc { get; set; }

    /// <summary>
    /// Number of most recent backups to keep
    /// </summary>
    public uint MaxBackupFiles
    {
        get
        {
            if (backupFilesToKeep < 10)
            {
                backupFilesToKeep = 10;
            }
            return backupFilesToKeep;
        }

        set => backupFilesToKeep = value;
    }
    uint backupFilesToKeep = 100;

    /// <summary>
    /// Gets or sets the position and size of the main application window.
    /// </summary>
    public WindowPosition MainWindowPosition { get; set; } = new WindowPosition();

    /// <summary>
    /// Gets or sets the position and size of the maintenance window.
    /// </summary>
    public WindowPosition MaintenanceWindowPosition { get; set; } = new WindowPosition();

    public WindowPosition TradeDetailViewPosition { get; set; } = new WindowPosition();

    public WindowPosition TradeImageDetailViewPosition { get; set; } = new WindowPosition();

    /// <summary>
    /// Gets or sets the BCP-47 culture name used for the application UI (e.g. <c>"en-US"</c>, <c>"tr-TR"</c>).
    /// </summary>
    /// <remarks>When empty, the operating system's current culture is used.</remarks>
    public string UiCulture { get; set; } = string.Empty;


    public void Save()
    {
        var settingsFilePath = GetSettingsFilePath();
        var settingsJson = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(settingsFilePath, settingsJson);
    }
}
