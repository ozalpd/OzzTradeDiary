using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TD.Models;
using TD.WPF.ViewModels.Trades;

namespace TD.WPF.Views.Trades
{
    // Codebehind for TakeProfitOrderCreateView.xaml
    public partial class TakeProfitOrderCreateView : Window
    {
        private readonly TakeProfitOrderCreateVM _viewModel;
        private Trade? _preselectedTrade;

        public TakeProfitOrder TakeProfitOrder => _viewModel.TakeProfitOrder;

        /// <summary>
        /// This constructor should not be called, but we need it for the designer to work.
        /// </summary>
        /// <remarks>This constructor creates a dummy TakeProfitOrder for the designer.</remarks>
        public TakeProfitOrderCreateView() : this(null)
        {

        }

        /// <summary>
        /// This constructor should be used at runtime to create the view with a real TakeProfitOrder.
        /// </summary>
        /// <param name="takeProfitOrder">The real TakeProfitOrder to be used by the view.</param>
        public TakeProfitOrderCreateView(Trade? preselectedTrade)
        {
            InitializeComponent();

            _viewModel = new TakeProfitOrderCreateVM();
            DataContext = _viewModel;
            _preselectedTrade = preselectedTrade;
            SourceInitialized += TakeProfitOrderCreateView_SourceInitialized;
        }

        private async void TakeProfitOrderCreateView_SourceInitialized(object? sender, EventArgs e)
        {
            if (_preselectedTrade != null)
            {
                _viewModel.Trade = _preselectedTrade;
                _viewModel.TradeId = _preselectedTrade.Id;
            }
            OnSourceInitialized();
        }
        partial void OnSourceInitialized();

        private void DecimalTextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (sender is not TextBox tb)
                return;
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                var pasteText = (string)e.DataObject.GetData(typeof(string));
                var proposed = tb.Text.Remove(tb.SelectionStart, tb.SelectionLength).Insert(tb.SelectionStart, pasteText);
                var decimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
                if (!Regex.IsMatch(proposed, @"^-?\d*(" + Regex.Escape(decimalSeparator) + @"\d*)?$"))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private void DecimalTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (sender is not TextBox tb)
                return;

            var decimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            var proposed = tb.Text.Remove(tb.SelectionStart, tb.SelectionLength).Insert(tb.SelectionStart, e.Text);
            e.Handled = !Regex.IsMatch(proposed, @"^-?\d*(" + Regex.Escape(decimalSeparator) + @"\d*)?$");
        }

        private void IntegerTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (sender is not TextBox tb)
                return;

            e.Handled = !char.IsDigit(e.Text, 0);
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_viewModel.ValidateModel())
                return;

            DialogResult = true;
        }
    }
}