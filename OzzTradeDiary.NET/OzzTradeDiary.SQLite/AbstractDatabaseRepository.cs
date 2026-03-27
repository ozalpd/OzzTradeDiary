using Microsoft.Data.Sqlite;
using System.ComponentModel.DataAnnotations;
using TD.Validation;

namespace TD.SQLite
{
    public abstract class AbstractDatabaseRepository<T> where T : class
    {
        protected AbstractDatabaseRepository(string databasePath, string tableName, MetadataRepository? metadataRepository = null)
        {
            _connectionString = $"Data Source={databasePath}";
            _metadataRepository = metadataRepository ?? new MetadataRepository(databasePath);
            _tableName = tableName;
        }

        protected readonly string _connectionString;
        protected readonly MetadataRepository? _metadataRepository;
        protected readonly string _tableName;

        protected static void AddNullableTextParameter(SqliteCommand command, string parameterName, string? value)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = parameterName;
            parameter.SqliteType = SqliteType.Text;
            parameter.Value = value is null ? DBNull.Value : value;
            command.Parameters.Add(parameter);
        }

        protected long GetRecordCount()
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            using var countCommand = connection.CreateCommand();
            countCommand.CommandText = $"SELECT COUNT(1) FROM {_tableName}";

            _cachedRecordCount = Convert.ToInt64(countCommand.ExecuteScalar());
            return _cachedRecordCount.Value;
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

        protected void SeedIfEmpty(string seedScriptFileName)
        {
            if (RecordCount > 0)
                return;

            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            DbScriptInitializer.ExecuteScript(connection, seedScriptFileName);
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
