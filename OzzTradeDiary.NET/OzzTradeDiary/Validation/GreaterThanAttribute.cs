using System.ComponentModel.DataAnnotations;
using TD.i18n;

namespace TD.Validation
{
    public class GreaterThanAttribute : ValidationAttribute
    {
        private readonly decimal _minvalue;
        private static readonly string defaultErrorMessageString = "The {0} field must be greater than {1}.";

        public GreaterThanAttribute(decimal minvalue, string? customErrMessage = null)
        {
            _minvalue = minvalue;
            ErrorMessage = !string.IsNullOrEmpty(customErrMessage)
                         ? customErrMessage
                         : ErrorStrings.ValueGreaterThan;
        }

        public GreaterThanAttribute(int minvalue, string? customErrMessage = null) : this((decimal)minvalue, customErrMessage) { }

        public GreaterThanAttribute(double minvalue, string? customErrMessage = null) : this((decimal)minvalue, customErrMessage) { }


        public override string FormatErrorMessage(string name)
        {
            string messageString;
            try
            {
                messageString = string.Format(ErrorMessageString, name, _minvalue);
            }
            catch (Exception)
            {
                messageString = string.Format(defaultErrorMessageString, name, _minvalue);
            }

            return messageString;
        }

        public override bool IsValid(object? value)
        {
            if (value == null)
                return true;

            switch (Type.GetTypeCode(value.GetType()))
            {
                case TypeCode.Byte:
                    return (Byte)value > MinValue;

                case TypeCode.SByte:
                    return (SByte)value > MinValue;

                case TypeCode.UInt16:
                    return (UInt16)value > MinValue;

                case TypeCode.UInt32:
                    return (UInt32)value > MinValue;

                case TypeCode.UInt64:
                    return (UInt64)value > MinValue;

                case TypeCode.Int16:
                    return (Int16)value > MinValue;

                case TypeCode.Int32:
                    return (Int32)value > MinValue;

                case TypeCode.Int64:
                    return (Int64)value > MinValue;

                case TypeCode.Decimal:
                    return (decimal)value > MinValue;

                case TypeCode.Double:
                    return (double)value > (double)MinValue;

                case TypeCode.Single:
                    return (Single)value > (Single)MinValue;
            }

            return false;
        }

        [Required]
        protected decimal MinValue => _minvalue;
    }
}
