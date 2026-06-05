using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace TD.WPF.Converters
{
    internal class ProfitLossToColor : IValueConverter
    {
        public ProfitLossToColor()
        {
            SetDefaultColors();
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return new SolidColorBrush(ColorForDefault);


            if (value is decimal profitLoss)
            {
                return SetColors(profitLoss);
            }
            else if (value is double d)
            {
                return SetColors((decimal)d);
            }
            else if (value is float f)
            {
                return SetColors((decimal)f);
            }
            else if (value is int i)
            {
                return SetColors((decimal)i);
            }
            else if (value is long l)
            {
                return SetColors((decimal)l);
            }
            else
            {
                return new SolidColorBrush(ColorForDefault);
            }
        }

        private object SetColors(decimal profitLoss)
        {
            if (profitLoss < 0)
            {
                return new SolidColorBrush(ColorForLoss);
            }
            else
            {
                return new SolidColorBrush(ColorForProfit);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public Color ColorForProfit { get; set; }
        public Color ColorForLoss { get; set; }
        public Color ColorForDefault { get; set; }

        private void SetDefaultColors()
        {
            ColorForProfit = Colors.LightGreen;
            ColorForLoss = Colors.LightPink;
            ColorForDefault = Colors.Transparent;
        }
    }
}
