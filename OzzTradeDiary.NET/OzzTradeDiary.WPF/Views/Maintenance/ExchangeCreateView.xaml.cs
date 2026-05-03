using System.Windows;
using TD.AppInfra.Services;
using TD.Models;
using TD.WPF.ViewModels.Maintenance;

namespace TD.WPF.Views.Maintenance
{
    /// <summary>
    /// Interaction logic for ExchangeCreateView.xaml
    /// </summary>
    public partial class ExchangeCreateView : Window
    {
        private readonly ExchangeCreateVM _viewModel;

        public Exchange Exchange => _viewModel.Exchange;

        public ExchangeCreateView() : this(new CurrencyMockLookupService()) { }

        public ExchangeCreateView(ICurrencyLookupService currencyLookupService)
        {
            InitializeComponent();

            _viewModel = new ExchangeCreateVM(currencyLookupService);
            DataContext = _viewModel;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_viewModel.ValidateModel())
                return;

            DialogResult = true;
        }
    }
}
