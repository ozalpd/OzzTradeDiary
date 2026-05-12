using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Resources;

namespace TD.Extensions
{
    public static class EnumExtension
    {
        /// <summary>
        ///     A generic extension method that aids in reflecting 
        ///     and retrieving any attribute that is applied to an `Enum`.
        /// </summary>
        public static TAttribute? GetAttribute<TAttribute>(this Enum enumValue)
                where TAttribute : Attribute
        {
            var attribute = enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<TAttribute>();

            return attribute;
        }

        public static string GetDisplayValue(this Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());

            DisplayAttribute[] descriptionAttributes = Array.Empty<DisplayAttribute>();
            if (fieldInfo != null)
                descriptionAttributes = fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false) as DisplayAttribute[]
                                     ?? Array.Empty<DisplayAttribute>();

            if (descriptionAttributes.Length > 0 && descriptionAttributes[0].ResourceType != null)
                return LookupResource(descriptionAttributes[0].ResourceType, descriptionAttributes[0].Name ?? string.Empty);

            return (descriptionAttributes.Length > 0) ? descriptionAttributes[0].Name ?? value.ToString() : value.ToString();
        }

        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        private static string LookupResource(Type resourceManagerProvider, string resourceKey)
        {
            foreach (PropertyInfo staticProperty in resourceManagerProvider.GetProperties(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (staticProperty.PropertyType == typeof(ResourceManager))
                {
                    var resourceManager = (ResourceManager?)staticProperty.GetValue(null, null);
                    return resourceManager?.GetString(resourceKey) ?? resourceKey;
                }
            }

            return resourceKey; // Fallback with the key name
        }
    }

    /// <summary>
    /// Represents a strongly typed enumeration value and its associated display text for use in UI selection or display
    /// scenarios.
    /// </summary>
    /// <remarks>This class is commonly used to bind enumeration values to user interface elements, such as
    /// dropdown lists or combo boxes, where both the underlying value and a user-friendly display string are
    /// needed.</remarks>
    /// <typeparam name="T">The enumeration type represented by this item.</typeparam>
    public class EnumValueItem<T> where T : Enum
    {
        public required T Value { get; set; }
        public string DisplayValue { get; set; } = string.Empty;
    }
}
