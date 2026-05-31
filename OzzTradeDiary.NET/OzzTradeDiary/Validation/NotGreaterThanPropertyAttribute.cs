using System.ComponentModel.DataAnnotations;
using TD.i18n;

namespace TD.Validation
{
    /// <summary>
    /// Validates that the decorated property's value does not exceed the value of another property
    /// on the same object. Null values on either side are treated as valid (no value to compare).
    /// </summary>
    /// <remarks>
    /// Typical use: <c>FilledQuantity</c> must not be greater than <c>OrderQuantity</c> on order entities.
    /// <example>
    /// <code>
    /// [NotGreaterThanProperty(nameof(OrderQuantity))]
    /// public decimal? FilledQuantity { get; set; }
    /// </code>
    /// </example>
    /// OzzCodeGen applies this attribute directly on the property in generated files so that
    /// <c>Validator.TryValidateObject</c> picks it up without any buddy-class workaround.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class NotGreaterThanPropertyAttribute : ValidationAttribute
    {
        /// <param name="comparisonPropertyName">
        /// Name of the property on the same object whose value must be greater than or equal to
        /// the decorated property's value.
        /// </param>
        public NotGreaterThanPropertyAttribute(string comparisonPropertyName)
        {
            ComparisonPropertyName = comparisonPropertyName;
        }

        /// <summary>Name of the upper-bound property on the same object.</summary>
        public string ComparisonPropertyName { get; }

        public override string FormatErrorMessage(string name)
        {
            string template = ErrorMessage ?? ErrorStrings.ValueMax;
            return string.Format(template, name, ComparisonPropertyName, ComparisonPropertyName);
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            var compProp = validationContext.ObjectType.GetProperty(ComparisonPropertyName);
            if (compProp == null)
                return ValidationResult.Success;

            var compValue = compProp.GetValue(validationContext.ObjectInstance);
            if (compValue == null)
                return ValidationResult.Success;

            var current = Convert.ToDecimal(value);
            var maximum = Convert.ToDecimal(compValue);

            if (current > maximum)
            {
                return new ValidationResult(
                    FormatErrorMessage(validationContext.DisplayName),
                    new[] { validationContext.MemberName! });
            }

            return ValidationResult.Success;
        }
    }
}
