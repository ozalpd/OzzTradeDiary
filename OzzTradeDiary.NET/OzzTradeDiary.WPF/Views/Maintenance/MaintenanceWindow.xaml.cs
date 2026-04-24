using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TD.AppInfra.Services;
using TD.WPF.Extensions;
using TD.WPF.Models;
using TD.WPF.Services;
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
        private IExchangeLookupService _exchangeLookupService = new ExchangeMockLookupService();
        private ICurrencyLookupService _currencyLookupService = new CurrencyMockLookupService();

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
            _exchangeLookupService = new ExchangeLookupService(_viewModel.ExchangeRepository);
            _currencyLookupService = new CurrencyLookupService(_viewModel.CurrencyRepository);
            DataContext = _viewModel;
            _appSettings.MaintenanceWindowPosition.SetWindowPositions(this);
            await ExecuteUiActionAsync(_viewModel.LoadAllAsync, "Load data");
        }

        private void Window_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            _appSettings.MaintenanceWindowPosition.GetWindowPositions(this);
        }

        private void FilterByExchangeCombo_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateFilterByExchangePlaceholderVisibility();
        }

        private void FilterByExchangeCombo_DropDownOpened(object sender, EventArgs e)
        {
            FilterByExchangeTextBlock.Visibility = Visibility.Collapsed;
        }

        private void FilterByExchangeCombo_DropDownClosed(object sender, EventArgs e)
        {
            UpdateFilterByExchangePlaceholderVisibility();
        }

        private void FilterByExchangeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateFilterByExchangePlaceholderVisibility();
        }

        private void FilterByExchangeTextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FilterByExchangeTextBlock.Visibility = Visibility.Collapsed;
            FilterByExchangeCombo.IsDropDownOpen = true;
            e.Handled = true;
        }

        private void UpdateFilterByExchangePlaceholderVisibility()
        {
            FilterByExchangeTextBlock.Visibility = FilterByExchangeCombo.SelectedItem is null
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        private async void AddCurrency_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new CurrencyCreate { Owner = this };
            if (dialog.ShowDialog() == true)
            {
                _viewModel.Currencies.Add(dialog.Currency);
                await ExecuteUiActionAsync(_viewModel.SaveCurrenciesAsync, "Save currencies");
                await ExecuteUiActionAsync(_viewModel.LoadCurrenciesAsync, "Refresh currencies");
            }
        }

        private async void EditCurrencys_Click(object sender, RoutedEventArgs e)
        {

            if (_viewModel.SelectedCurrency == null)
                return;

            var dialog = new CurrencyEdit(_viewModel.SelectedCurrency)
            {
                Owner = this
            };

            int id = _viewModel.SelectedCurrency.Id;
            if (dialog.ShowDialog() == true && dialog.IsDirty)
            {
                _viewModel.Currencies.Add(dialog.Currency);
                await ExecuteUiActionAsync(_viewModel.SaveCurrenciesAsync, "Save currencies");
                await ExecuteUiActionAsync(_viewModel.LoadCurrenciesAsync, "Refresh currencies");
            }
            else if (dialog.IsDirty)
            {
                await ExecuteUiActionAsync(_viewModel.LoadCurrenciesAsync, "Refresh currencies");
            }

            _viewModel.SelectedCurrency = _viewModel.Currencies.FirstOrDefault(x => x.Id == id);
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
            var dialog = new ExchangeCreate { Owner = this };
            if (dialog.ShowDialog() == true)
            {
                _viewModel.Exchanges.Add(dialog.Exchange);
                await ExecuteUiActionAsync(_viewModel.SaveExchangesAsync, "Save exchanges");
                await ExecuteUiActionAsync(_viewModel.LoadExchangesAsync, "Refresh exchanges");
            }
        }

        private async void EditExchanges_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedExchange == null)
                return;
            var dialog = new ExchangeEdit(_viewModel.SelectedExchange)
            {
                Owner = this
            };

            int id = _viewModel.SelectedExchange.Id;
            if (dialog.ShowDialog() == true && dialog.IsDirty)
            {
                _viewModel.Exchanges.Add(dialog.Exchange);
                await ExecuteUiActionAsync(_viewModel.SaveExchangesAsync, "Save exchanges");
                await ExecuteUiActionAsync(_viewModel.LoadExchangesAsync, "Refresh exchanges");
            }
            else if (dialog.IsDirty)
            {
                await ExecuteUiActionAsync(_viewModel.LoadExchangesAsync, "Refresh exchanges");
            }

            _viewModel.SelectedExchange = _viewModel.Exchanges.FirstOrDefault(x => x.Id == id);
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
            var dialog = new TradingAccountCreate(_exchangeLookupService) { Owner = this };
            if (dialog.ShowDialog() == true)
            {
                _viewModel.TradingAccounts.Add(dialog.TradingAccount);
                await ExecuteUiActionAsync(_viewModel.SaveTradingAccountsAsync, "Save trading accounts");
                await ExecuteUiActionAsync(_viewModel.LoadTradingAccountsAsync, "Refresh trading accounts");
            }
        }

        private async void EditTradingAccounts_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedTradingAccount == null)
                return;

            var dialog = new TradingAccountEdit(_viewModel.SelectedTradingAccount)
            {
                Owner = this
            };

            int id = _viewModel.SelectedTradingAccount.Id;
            if (dialog.ShowDialog() == true && dialog.IsDirty)
            {
                await ExecuteUiActionAsync(_viewModel.SaveTradingAccountsAsync, "Save trading accounts");
                await ExecuteUiActionAsync(_viewModel.LoadTradingAccountsAsync, "Refresh trading accounts");
            }
            else if (dialog.IsDirty)
            {
                await ExecuteUiActionAsync(_viewModel.LoadCurrenciesAsync, "Refresh currencies");
            }

            _viewModel.SelectedTradingAccount = _viewModel.TradingAccounts.FirstOrDefault(x => x.Id == id);
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
            var dialog = new SymbolCreate(_exchangeLookupService, _currencyLookupService) { Owner = this };
            if (dialog.ShowDialog() == true)
            {
                _viewModel.Symbols.Add(dialog.Symbol);
                await ExecuteUiActionAsync(_viewModel.SaveSymbolsAsync, "Save symbols");
                await ExecuteUiActionAsync(_viewModel.LoadSymbolsAsync, "Refresh symbols");
            }
        }

        private async void EditSymbols_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedSymbol == null)
                return;

            var dialog = new SymbolEdit(_viewModel.SelectedSymbol)
            {
                Owner = this
            };

            int id = _viewModel.SelectedSymbol.Id;
            if (dialog.ShowDialog() == true && dialog.IsDirty)
            {
                await ExecuteUiActionAsync(_viewModel.SaveSymbolsAsync, "Save symbols");
                await ExecuteUiActionAsync(_viewModel.LoadSymbolsAsync, "Refresh symbols");
            }
            else if (dialog.IsDirty)
            {
                await ExecuteUiActionAsync(_viewModel.LoadSymbolsAsync, "Refresh symbols");
            }

            _viewModel.SelectedSymbol = _viewModel.Symbols.FirstOrDefault(x => x.Id == id);
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
