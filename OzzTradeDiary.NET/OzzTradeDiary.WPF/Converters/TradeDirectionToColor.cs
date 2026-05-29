using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using TD.Models;

namespace TD.WPF.Converters
{
    internal class TradeDirectionToColor : IValueConverter
    {
        public TradeDirectionToColor()
        {
            SetDefaultColors();
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value is not TradeDirection tradeDirection)
            {
                return new SolidColorBrush(ColorForDefault);
            }
            switch (tradeDirection)
            {
                case TradeDirection.Long:
                    return new SolidColorBrush(ColorForLong);
                case TradeDirection.Short:
                    return new SolidColorBrush(ColorForShort);
                default:
                    return new SolidColorBrush(ColorForDefault);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public Color ColorForLong { get; set; }
        public Color ColorForShort { get; set; }
        public Color ColorForDefault { get; set; }

        private void SetDefaultColors()
        {
            ColorForLong = Colors.LightGreen;
            ColorForShort = Colors.LightPink;
            ColorForDefault = Colors.Transparent;
        }
    }
}
