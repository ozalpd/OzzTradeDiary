using System.Collections.ObjectModel;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TD.AppInfra.DesignTime;
using TD.AppInfra.Services;
using TD.Models;
using TD.WPF.ViewModels.Trades;

namespace TD.WPF.Views.Trades
{
    // Codebehind for TradeCreateView.xaml
    public partial class TradeCreateView : Window
    {
        private readonly TradeCreateVM _viewModel;
        private TradingAccount? _preselectedTradingAccount;

        public Trade Trade => _viewModel.Trade;

        /// <summary>
        /// This constructor should not be called, but we need it for the designer to work.
        /// </summary>
        /// <remarks>This constructor creates a dummy Trade for the designer.</remarks>
        public TradeCreateView() : this(new SymbolMockLookupService(), new TradingAccountMockLookupService(), null)
        {

        }

        /// <summary>
        /// This constructor should be used at runtime to create the view with a real Trade.
        /// </summary>
        /// <param name="trade">The real Trade to be used by the view.</param>
        public TradeCreateView(ISymbolLookupService symbolLookupService, ITradingAccountLookupService tradingAccountLookupService,
                                                                      TradingAccount? preselectedTradingAccount)
        {
            InitializeComponent();

            _viewModel = new TradeCreateVM(symbolLookupService, tradingAccountLookupService);
            DataContext = _viewModel;
            _preselectedTradingAccount = preselectedTradingAccount;
            SourceInitialized += TradeCreateView_SourceInitialized;
        }

        private async void TradeCreateView_SourceInitialized(object? sender, EventArgs e)
        {
            try
            {
                await _viewModel.LoadSymbolsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Load exchanges failed.\n{ex.Message}", "Ozz Trade Diary", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            try
            {
                await _viewModel.LoadTradingAccountsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Load exchanges failed.\n{ex.Message}", "Ozz Trade Diary", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (_preselectedTradingAccount != null)
            {
                _viewModel.TradingAccountId = _preselectedTradingAccount.Id;
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