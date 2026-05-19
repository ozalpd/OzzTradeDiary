using System.Collections.ObjectModel;
using System.Windows;
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

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_viewModel.ValidateModel())
                return;

            DialogResult = true;
        }
    }
}