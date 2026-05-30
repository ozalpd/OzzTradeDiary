using System.ComponentModel.DataAnnotations;
using TD.i18n;

namespace TD.Validation
{
    /// <summary>
    /// Validates that a selection has been made — the value must not be <c>0</c> (the default unselected state).
    /// Use on enum properties and foreign-key int properties instead of <see cref="RangeAttribute"/> to get a
    /// clear "required" error message rather than a confusing numeric-range message.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class RequiredSelectionAttribute : ValidationAttribute
    {
        public RequiredSelectionAttribute()
            : base(() => ErrorStrings.RequiredEnum)
        {
        }

        public override bool IsValid(object? value)
        {
            if (value == null)
                return false;

            return Convert.ToInt32(value) != 0;
        }
    }
}
