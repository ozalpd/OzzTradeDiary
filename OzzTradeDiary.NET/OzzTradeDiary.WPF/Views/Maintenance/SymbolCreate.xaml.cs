using System.Windows;
using TD.AppInfra.Services;
using TD.Models;
using TD.WPF.Services;
using TD.WPF.ViewModels.Maintenance;

namespace TD.WPF.Views.Maintenance
{
    /// <summary>
    /// Interaction logic for CreateSymbol.xaml
    /// </summary>
    public partial class SymbolCreate : Window
    {
        private readonly SymbolCreateVM _viewModel;

        public Symbol Symbol => _viewModel.Symbol;

        public SymbolCreate() : this(new ExchangeMockLookupService(), new CurrencyMockLookupService())
        {
        }

        internal SymbolCreate(IExchangeLookupService exchangeLookupService, ICurrencyLookupService currencyLookupService)
        {
            InitializeComponent();

            _viewModel = new SymbolCreateVM(exchangeLookupService, currencyLookupService);
            DataContext = _viewModel;
            SourceInitialized += CreateSymbol_SourceInitialized;
        }

        private async void CreateSymbol_SourceInitialized(object? sender, EventArgs e)
        {
            try
            {
                await _viewModel.LoadExchangesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Load exchanges failed.\n{ex.Message}", "Ozz Trade Diary", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            try
            {
                await _viewModel.LoadCurrenciesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Load currencies failed.\n{ex.Message}", "Ozz Trade Diary", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void MarketTypeComboBox_DropDownClosed(object sender, EventArgs e)
        {
            TickerTextBox.Focus();
        }
    }
}
