using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using TD.Models;

namespace TD.WPF.Converters
{
    internal class TradeStatusToColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value is not TradeStatus tradeStatus)
            {
                return new SolidColorBrush(Colors.Transparent);
            }

            switch (tradeStatus)
            {
                case TradeStatus.Planned:
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF9D2"));

                case TradeStatus.Pending:
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFEBCC"));

                case TradeStatus.Active:
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BFDDF0"));

                case TradeStatus.Closed:
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8CC0EB"));

                case TradeStatus.Missed:
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFCEE3"));

                case TradeStatus.Cancelled:
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFB0D2"));

                default:
                    return new SolidColorBrush(Colors.Transparent);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
