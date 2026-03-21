using Microsoft.Data.Sqlite;
using System.ComponentModel.DataAnnotations;
using TD.Validation;

namespace TD.SQLite
{
    public abstract class AbstractDatabaseRepository
    {

        protected static void AddNullableTextParameter(SqliteCommand command, string parameterName, string? value)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = parameterName;
            parameter.SqliteType = SqliteType.Text;
            parameter.Value = value is null ? DBNull.Value : value;
            command.Parameters.Add(parameter);
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
