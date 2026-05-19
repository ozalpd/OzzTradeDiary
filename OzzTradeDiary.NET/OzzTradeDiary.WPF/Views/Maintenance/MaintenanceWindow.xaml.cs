using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TD.AppInfra.DesignTime;
using TD.i18n;
using TD.RepositoryContracts;
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
        private readonly IWindowDialogService _windowDialogService = new WindowDialogService();
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IExchangeRepository _exchangeRepository;
        private readonly ISymbolRepository _symbolRepository;
        private readonly ITradingAccountRepository _tradingAccountRepository;

        /// <summary>
        /// Parameterless constructor for the XAML designer.
        /// </summary>
        public MaintenanceWindow()
            : this(new CurrencyMockRepository(), new ExchangeMockRepository(),
                   new SymbolMockRepository(), new TradingAccountMockRepository())
        {
        }

        internal MaintenanceWindow(ICurrencyRepository currencyRepository,
                                   IExchangeRepository exchangeRepository,
                                   ISymbolRepository symbolRepository,
                                   ITradingAccountRepository tradingAccountRepository)
        {
            _currencyRepository = currencyRepository;
            _exchangeRepository = exchangeRepository;
            _symbolRepository = symbolRepository;
            _tradingAccountRepository = tradingAccountRepository;
            InitializeComponent();
            this.SetIconFromGeometryResource("gear-wide-connected", "#0044D7", "#E8FFFFFF", 48);
            SourceInitialized += Window_SourceInitialized;
            Closing += Window_Closing;
        }


        private async void Window_SourceInitialized(object? sender, EventArgs e)
        {
            _viewModel = new MaintenanceWindowVM(_windowDialogService,
                                                 _currencyRepository, _exchangeRepository,
                                                 _symbolRepository, _tradingAccountRepository);
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

        private async void RefreshCurrencies_Click(object sender, RoutedEventArgs e)
        {
            await ExecuteUiActionAsync(_viewModel.LoadCurrenciesAsync, "Refresh currencies");
        }

        private async void RefreshExchanges_Click(object sender, RoutedEventArgs e)
        {
            await ExecuteUiActionAsync(_viewModel.LoadExchangesAsync, "Refresh exchanges");
        }

        private async void RefreshTradingAccounts_Click(object sender, RoutedEventArgs e)
        {
            await ExecuteUiActionAsync(_viewModel.LoadTradingAccountsAsync, "Refresh trading accounts");
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
                MessageBox.Show($"{operationName} failed.\n{ex.Message}", CommonStrings.AppTitle, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
