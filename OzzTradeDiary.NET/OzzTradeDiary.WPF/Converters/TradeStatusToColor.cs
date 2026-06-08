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
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF8C0"));

                case TradeStatus.Pending:
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D9E8F0"));

                case TradeStatus.Active:
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BFDDFF"));

                case TradeStatus.Closed:
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A6CEFF"));

                case TradeStatus.Missed:
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E0D0FF"));

                case TradeStatus.Cancelled:
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#DCDAFF"));

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
