using System.Collections.ObjectModel;
using System.Windows;
using TD.AppInfra.Services;
using TD.Models;
using TD.WPF.ViewModels.Exchanges;

namespace TD.WPF.Views.Exchanges
{
    // Codebehind for ExchangeCreateView.xaml
    public partial class ExchangeCreateView : Window
    {
        private readonly ExchangeCreateVM _viewModel;

        public Exchange Exchange => _viewModel.Exchange;

        /// <summary>
        /// This constructor should not be called, but we need it for the designer to work.
        /// </summary>
        /// <remarks>This constructor creates a dummy Exchange for the designer.</remarks>
        public ExchangeCreateView() : this(new CurrencyMockLookupService())
        {

        }

        /// <summary>
        /// This constructor should be used at runtime to create the view with a real Exchange.
        /// </summary>
        /// <param name="exchange">The real Exchange to be used by the view.</param>
        public ExchangeCreateView(ICurrencyLookupService currencyLookupService)
        {
            InitializeComponent();

            _viewModel = new ExchangeCreateVM(currencyLookupService);
            DataContext = _viewModel;
            SourceInitialized += ExchangeCreateView_SourceInitialized;
        }

        private async void ExchangeCreateView_SourceInitialized(object? sender, EventArgs e)
        {
            try
            {
                await _viewModel.LoadCurrenciesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Load exchanges failed.\n{ex.Message}", "Ozz Trade Diary", MessageBoxButton.OK, MessageBoxImage.Error);
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