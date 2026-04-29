using System.Windows;
using TD.AppInfra.Services;
using TD.Models;
using TD.WPF.ViewModels.Maintenance;

namespace TD.WPF.Views.Maintenance
{
    /// <summary>
    /// Interaction logic for CreateTradingAccount.xaml
    /// </summary>
    public partial class TradingAccountCreate : Window
    {
        private readonly TradingAccountCreateVM _viewModel;
        private Exchange? _selectedExchange;

        public TradingAccount TradingAccount => _viewModel.TradingAccount;

        public TradingAccountCreate() : this(new ExchangeMockLookupService(), null)
        {
        }

        internal TradingAccountCreate(IExchangeLookupService exchangeLookupService, Exchange? selectedExchange)
        {
            InitializeComponent();

            _viewModel = new TradingAccountCreateVM(exchangeLookupService);
            DataContext = _viewModel;
            _selectedExchange = selectedExchange;
            SourceInitialized += CreateTradingAccount_SourceInitialized;
        }

        private async void CreateTradingAccount_SourceInitialized(object? sender, EventArgs e)
        {
            try
            {
                await _viewModel.LoadExchangesAsync();
                if (_selectedExchange != null)
                {
                    _viewModel.ExchangeId = _selectedExchange.Id;
                    _viewModel.Title= $"{_selectedExchange.ExchangeName} Account";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Load exchanges failed.\n{ex.Message}", "Ozz Trade Diary", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_viewModel.ValidateModel())
                return;

            DialogResult = true;
        }
    }
}
