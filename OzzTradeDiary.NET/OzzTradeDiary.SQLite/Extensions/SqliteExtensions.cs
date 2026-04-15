using Microsoft.Data.Sqlite;

namespace TD.SQLite.Extensions
{
    public static class SqliteExtensions
    {
        public static void AddNullableParameter(this SqliteCommand command, string parameterName, long? value)
        {
            var parameter = command.GetParameter(parameterName, SqliteType.Integer);
            parameter.Value = value is null ? DBNull.Value : value;
            command.Parameters.Add(parameter);
        }

        public static void AddNullableParameter(this SqliteCommand command, string parameterName, int? value)
        {
            AddNullableParameter(command, parameterName, (long?)value);
        }

        public static void AddNullableParameter(this SqliteCommand command, string parameterName, double? value)
        {
            var parameter = command.GetParameter(parameterName, SqliteType.Real);
            parameter.Value = value is null ? DBNull.Value : value;
            command.Parameters.Add(parameter);
        }

        public static void AddNullableParameter(this SqliteCommand command, string parameterName, string? value)
        {
            var parameter = command.GetParameter(parameterName, SqliteType.Text);
            parameter.Value = value is null ? DBNull.Value : value;
            command.Parameters.Add(parameter);
        }

        public static SqliteParameter GetParameter(this SqliteCommand command, string parameterName, SqliteType paramType)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = parameterName;
            parameter.SqliteType = paramType;

            return parameter;
        }
    }
}
