using System.Collections.ObjectModel;
using System.Windows;
using TD.AppInfra.Services;
using TD.Models;
using TD.WPF.ViewModels.Maintenance;

namespace TD.WPF.Views.Maintenance
{
    // Codebehind for TradingAccountCreateView.xaml
    public partial class TradingAccountCreateView : Window
    {
        private readonly TradingAccountCreateVM _viewModel;
        private Exchange? _preselectedExchange;

        public TradingAccount TradingAccount => _viewModel.TradingAccount;

        /// <summary>
        /// This constructor should not be called, but we need it for the designer to work.
        /// </summary>
        /// <remarks>This constructor creates a dummy TradingAccount for the designer.</remarks>
        public TradingAccountCreateView() : this(new ExchangeMockLookupService(), null)
        {

        }

        /// <summary>
        /// This constructor should be used at runtime to create the view with a real TradingAccount.
        /// </summary>
        /// <param name="tradingAccount">The real TradingAccount to be used by the view.</param>
        public TradingAccountCreateView(IExchangeLookupService exchangeLookupService, Exchange? preselectedExchange)
        {
            InitializeComponent();

            _viewModel = new TradingAccountCreateVM(exchangeLookupService);
            DataContext = _viewModel;
            _preselectedExchange = preselectedExchange;
            SourceInitialized += TradingAccountCreateView_SourceInitialized;
        }

        private async void TradingAccountCreateView_SourceInitialized(object? sender, EventArgs e)
        {
            try
            {
                await _viewModel.LoadExchangesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Load exchanges failed.\n{ex.Message}", "Ozz Trade Diary", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (_preselectedExchange != null)
            {
                _viewModel.ExchangeId = _preselectedExchange.Id;
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