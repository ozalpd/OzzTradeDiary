using Microsoft.Data.Sqlite;
using System.ComponentModel.DataAnnotations;
using TD.Validation;

namespace TD.SQLite
{
    public abstract class AbstractDatabaseRepository<T> where T : class
    {
        protected AbstractDatabaseRepository(string databasePath, string tableName)
        {
            _metadataRepository = MetadataRepository.GetInstance(databasePath);
            _connectionString = _metadataRepository.ConnectionString;
            _tableName = tableName;
        }

        protected readonly string _connectionString;
        protected readonly MetadataRepository _metadataRepository;
        protected readonly string _tableName;

        protected static void AddNullableTextParameter(SqliteCommand command, string parameterName, string? value)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = parameterName;
            parameter.SqliteType = SqliteType.Text;
            parameter.Value = value is null ? DBNull.Value : value;
            command.Parameters.Add(parameter);
        }

        protected static void ExecuteScript(SqliteConnection connection, string scriptFileName)
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
        private static readonly string ScriptsDirectory = Path.Combine(AppContext.BaseDirectory, "DbScripts");


        protected long GetRecordCount()
        {
            using var connection = GetOpenConnection();
            using var countCommand = connection.CreateCommand();
            countCommand.CommandText = $"SELECT COUNT(1) FROM {_tableName}";

            _cachedRecordCount = Convert.ToInt64(countCommand.ExecuteScalar());
            return _cachedRecordCount.Value;
        }

        protected void ClearRecordCountCache()
        {
            _cachedRecordCount = null;
        }

        public long RecordCount
        {
            get
            {
                if (!_cachedRecordCount.HasValue)
                {
                    _cachedRecordCount = GetRecordCount();
                }
                return _cachedRecordCount.Value;
            }
        }
        private long? _cachedRecordCount;

        protected SqliteConnection GetOpenConnection()
        {
            var connection = new SqliteConnection(_connectionString);
            connection.Open();
            return connection;
        }

        protected async Task<SqliteConnection> GetOpenConnectionAsync()
        {
            var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();
            return connection;
        }

        protected async Task ExecuteInTransactionAsync(Func<SqliteConnection, SqliteTransaction, Task> operation)
        {
            ArgumentNullException.ThrowIfNull(operation);

            await using var connection = await GetOpenConnectionAsync();
            using var transaction = connection.BeginTransaction();

            try
            {
                await operation(connection, transaction);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        protected async Task<TResult> ExecuteInTransactionAsync<TResult>(Func<SqliteConnection, SqliteTransaction, Task<TResult>> operation)
        {
            ArgumentNullException.ThrowIfNull(operation);

            await using var connection = await GetOpenConnectionAsync();
            using var transaction = connection.BeginTransaction();

            try
            {
                var result = await operation(connection, transaction);
                transaction.Commit();
                return result;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        protected void SeedIfEmpty(SqliteConnection connection, string seedScriptFileName)
        {
            if (RecordCount > 0)
                return;

            ExecuteScript(connection, seedScriptFileName);
            ClearRecordCountCache();
        }

        protected static void ValidateOrThrow(object model)
        {
            var errors = ModelValidator.Validate(model);
            if (errors.Count > 0)
            {
                var message = string.Join(Environment.NewLine, errors.SelectMany(e => e.Value));
                throw new ValidationException(message);
            }
        }
    }
}
