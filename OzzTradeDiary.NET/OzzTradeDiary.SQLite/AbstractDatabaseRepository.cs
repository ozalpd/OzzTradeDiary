using System.ComponentModel.DataAnnotations;
using TD.Models;
using TD.Validation;

namespace TD.SQLite
{
    public abstract class AbstractDatabaseRepository
    {

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
