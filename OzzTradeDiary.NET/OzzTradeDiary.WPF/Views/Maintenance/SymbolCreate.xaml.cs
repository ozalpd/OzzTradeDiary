using System.Windows;
using TD.AppInfra.Services;
using TD.Models;
using TD.WPF.ViewModels.Maintenance;

namespace TD.WPF.Views.Maintenance
{
    /// <summary>
    /// Interaction logic for CreateSymbol.xaml
    /// </summary>
    public partial class SymbolCreate : Window
    {
        private readonly SymbolCreateVM _viewModel;
        private Exchange? _selectedExchange;

        public Symbol Symbol => _viewModel.Symbol;

        public SymbolCreate() : this(new ExchangeMockLookupService(), new CurrencyMockLookupService(), null)
        {
        }

        internal SymbolCreate(IExchangeLookupService exchangeLookupService, ICurrencyLookupService currencyLookupService, Exchange? preselectedExchange)
        {
            InitializeComponent();

            _viewModel = new SymbolCreateVM(currencyLookupService, exchangeLookupService);
            DataContext = _viewModel;
            _selectedExchange = preselectedExchange;
            SourceInitialized += CreateSymbol_SourceInitialized;
        }

        private async void CreateSymbol_SourceInitialized(object? sender, EventArgs e)
        {
            try
            {
                await _viewModel.LoadExchangesAsync();
                if (_selectedExchange != null)
                    _viewModel.ExchangeId = _selectedExchange.Id;
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
            if (!_viewModel.ValidateModel())
                return;

            DialogResult = true;
        }

        private void MarketTypeComboBox_DropDownClosed(object sender, EventArgs e)
        {
            TickerTextBox.Focus();
        }
    }
}
