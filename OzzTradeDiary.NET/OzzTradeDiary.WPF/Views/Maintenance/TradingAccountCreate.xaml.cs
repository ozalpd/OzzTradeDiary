using System;
using System.Windows;
using TD.Models;
using TD.WPF.Services;
using TD.WPF.ViewModels.Maintenance;

namespace TD.WPF.Views.Maintenance
{
    /// <summary>
    /// Interaction logic for CreateTradingAccount.xaml
    /// </summary>
    public partial class TradingAccountCreate : Window
    {
        private readonly TradingAccountCreateVM _viewModel;

        public TradingAccount TradingAccount => _viewModel.TradingAccount;

        public TradingAccountCreate() : this(new ExchangeMockLookupService())
        {
        }

        internal TradingAccountCreate(IExchangeLookupService exchangeLookupService)
        {
            InitializeComponent();

            _viewModel = new TradingAccountCreateVM(exchangeLookupService);
            DataContext = _viewModel;
            SourceInitialized += CreateTradingAccount_SourceInitialized;
        }

        private async void CreateTradingAccount_SourceInitialized(object? sender, EventArgs e)
        {
            try
            {
                await _viewModel.LoadExchangesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Load exchanges failed.\n{ex.Message}", "Ozz Trade Diary", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
