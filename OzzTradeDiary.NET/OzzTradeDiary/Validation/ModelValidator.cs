using System.ComponentModel.DataAnnotations;

namespace TD.Validation;

public static class ModelValidator
{
    public static IReadOnlyDictionary<string, IReadOnlyList<string>> Validate(object model)
    {
        ArgumentNullException.ThrowIfNull(model);

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        Validator.TryValidateObject(model, context, results, validateAllProperties: true);

        return GroupResults(results);
    }

    public static IReadOnlyList<string> ValidateProperty(object model, string propertyName)
    {
        ArgumentNullException.ThrowIfNull(model);
        ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);

        var context = new ValidationContext(model)
        {
            MemberName = propertyName
        };

        var results = new List<ValidationResult>();
        var value = model.GetType().GetProperty(propertyName)?.GetValue(model);

        Validator.TryValidateProperty(value, context, results);

        return results
            .Select(r => r.ErrorMessage)
            .Where(m => !string.IsNullOrWhiteSpace(m))
            .Cast<string>()
            .Distinct()
            .ToArray();
    }

    private static IReadOnlyDictionary<string, IReadOnlyList<string>> GroupResults(IEnumerable<ValidationResult> results)
    {
        var map = new Dictionary<string, List<string>>(StringComparer.Ordinal);

        foreach (var result in results)
        {
            var message = result.ErrorMessage;
            if (string.IsNullOrWhiteSpace(message))
                continue;

            var members = result.MemberNames?.Any() == true
                ? result.MemberNames
                : new[] { string.Empty };

            foreach (var member in members)
            {
                if (!map.TryGetValue(member, out var list))
                {
                    list = new List<string>();
                    map[member] = list;
                }

                if (!list.Contains(message))
                    list.Add(message);
            }
        }

        return map.ToDictionary(k => k.Key, v => (IReadOnlyList<string>)v.Value.AsReadOnly(), StringComparer.Ordinal);
    }
}