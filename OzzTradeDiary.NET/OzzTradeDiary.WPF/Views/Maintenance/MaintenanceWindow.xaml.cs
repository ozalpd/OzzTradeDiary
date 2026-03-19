using System.Windows;
using TD.Models;
using TD.WPF.Extensions;
using TD.WPF.Models;
using TD.WPF.ViewModels.Maintenance;

namespace TD.WPF.Views.Maintenance
{
    /// <summary>
    /// Interaction logic for MaintenanceWindow.xaml
    /// </summary>
    public partial class MaintenanceWindow : Window
    {
        private MaintenanceWindowVM _viewModel;
        private readonly AppSettings _appSettings = AppSettings.GetAppSettings();

        public MaintenanceWindow()
        {
            InitializeComponent();
            this.SetIconFromGeometryResource("gear-wide-connected", "#0044D7", "#E8FFFFFF", 48);
            SourceInitialized += Window_SourceInitialized;
            Closing += Window_Closing;
        }


        private async void Window_SourceInitialized(object? sender, EventArgs e)
        {
            _viewModel = new MaintenanceWindowVM();
            DataContext = _viewModel;
            _appSettings.MaintenanceWindowPosition.SetWindowPositions(this);
            await ExecuteUiActionAsync(_viewModel.LoadAllAsync, "Load data");
        }

        private void Window_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            _appSettings.MaintenanceWindowPosition.GetWindowPositions(this);
        }

        private async void AddCurrency_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Currencies.Add(new Currency { IsActive = true });
            await Task.CompletedTask;
        }

        private async void SaveCurrencies_Click(object sender, RoutedEventArgs e)
        {
            await ExecuteUiActionAsync(_viewModel.SaveCurrenciesAsync, "Save currencies");
            await ExecuteUiActionAsync(_viewModel.LoadCurrenciesAsync, "Refresh currencies");
        }

        private async void RefreshCurrencies_Click(object sender, RoutedEventArgs e)
        {
            await ExecuteUiActionAsync(_viewModel.LoadCurrenciesAsync, "Refresh currencies");
        }

        private async void AddExchange_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Exchanges.Add(new Exchange { IsActive = true });
            await Task.CompletedTask;
        }

        private async void SaveExchanges_Click(object sender, RoutedEventArgs e)
        {
            await ExecuteUiActionAsync(_viewModel.SaveExchangesAsync, "Save exchanges");
            await ExecuteUiActionAsync(_viewModel.LoadExchangesAsync, "Refresh exchanges");
        }

        private async void RefreshExchanges_Click(object sender, RoutedEventArgs e)
        {
            await ExecuteUiActionAsync(_viewModel.LoadExchangesAsync, "Refresh exchanges");
        }

        private async void AddTradingAccount_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new TradingAccountCreate { Owner = this };
            if (dialog.ShowDialog() == true)
            {
                _viewModel.TradingAccounts.Add(dialog.TradingAccount);
                await ExecuteUiActionAsync(_viewModel.SaveTradingAccountsAsync, "Save trading accounts");
                await ExecuteUiActionAsync(_viewModel.LoadTradingAccountsAsync, "Refresh trading accounts");
            }
        }

        private async void SaveTradingAccounts_Click(object sender, RoutedEventArgs e)
        {
            await ExecuteUiActionAsync(_viewModel.SaveTradingAccountsAsync, "Save trading accounts");
            await ExecuteUiActionAsync(_viewModel.LoadTradingAccountsAsync, "Refresh trading accounts");
        }

        private async void RefreshTradingAccounts_Click(object sender, RoutedEventArgs e)
        {
            await ExecuteUiActionAsync(_viewModel.LoadTradingAccountsAsync, "Refresh trading accounts");
        }

        private async void AddSymbol_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Symbols.Add(new Symbol { IsActive = true, MarketType = MarketType.Unspecified });
            await Task.CompletedTask;
        }

        private async void SaveSymbols_Click(object sender, RoutedEventArgs e)
        {
            await ExecuteUiActionAsync(_viewModel.SaveSymbolsAsync, "Save symbols");
            await ExecuteUiActionAsync(_viewModel.LoadSymbolsAsync, "Refresh symbols");
        }

        private async void RefreshSymbols_Click(object sender, RoutedEventArgs e)
        {
            await ExecuteUiActionAsync(_viewModel.LoadSymbolsAsync, "Refresh symbols");
        }

        private async Task ExecuteUiActionAsync(Func<Task> action, string operationName)
        {
            try
            {
                await action();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{operationName} failed.\n{ex.Message}", "Ozz Trade Diary", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
