using System.Collections.ObjectModel;
using System.Windows;
using TD.AppInfra.DesignTime;
using TD.AppInfra.Services;
using TD.Models;
using TD.WPF.ViewModels.Maintenance;

namespace TD.WPF.Views.Maintenance
{
    // Codebehind for SymbolCreateView.xaml
    public partial class SymbolCreateView : Window
    {
        private readonly SymbolCreateVM _viewModel;
        private Exchange? _preselectedExchange;

        public Symbol Symbol => _viewModel.Symbol;

        /// <summary>
        /// This constructor should not be called, but we need it for the designer to work.
        /// </summary>
        /// <remarks>This constructor creates a dummy Symbol for the designer.</remarks>
        public SymbolCreateView() : this(new CurrencyMockLookupService(), new ExchangeMockLookupService(), null)
        {

        }

        /// <summary>
        /// This constructor should be used at runtime to create the view with a real Symbol.
        /// </summary>
        /// <param name="symbol">The real Symbol to be used by the view.</param>
        public SymbolCreateView(ICurrencyLookupService currencyLookupService, IExchangeLookupService exchangeLookupService,
                                                                         Exchange? preselectedExchange)
        {
            InitializeComponent();

            _viewModel = new SymbolCreateVM(currencyLookupService, exchangeLookupService);
            DataContext = _viewModel;
            _preselectedExchange = preselectedExchange;
            SourceInitialized += SymbolCreateView_SourceInitialized;
        }

        private async void SymbolCreateView_SourceInitialized(object? sender, EventArgs e)
        {
            try
            {
                await _viewModel.LoadCurrenciesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Load exchanges failed.\n{ex.Message}", "Ozz Trade Diary", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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