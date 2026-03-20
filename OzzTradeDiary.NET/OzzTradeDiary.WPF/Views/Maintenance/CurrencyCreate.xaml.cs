using System.Windows;
using TD.Models;
using TD.WPF.ViewModels.Maintenance;

namespace TD.WPF.Views.Maintenance
{
    /// <summary>
    /// Interaction logic for CreateCurrency.xaml
    /// </summary>
    public partial class CurrencyCreate : Window
    {
        private readonly CurrencyCreateVM _viewModel;

        public Currency Currency => _viewModel.Currency;

        public CurrencyCreate()
        {
            InitializeComponent();

            _viewModel = new CurrencyCreateVM();
            DataContext = _viewModel;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
