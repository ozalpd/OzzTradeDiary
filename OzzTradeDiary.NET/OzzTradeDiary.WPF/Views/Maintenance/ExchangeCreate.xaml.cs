using System.Windows;
using TD.Models;
using TD.WPF.ViewModels.Maintenance;

namespace TD.WPF.Views.Maintenance
{
    /// <summary>
    /// Interaction logic for CreateExchange.xaml
    /// </summary>
    public partial class ExchangeCreate : Window
    {
        private readonly ExchangeCreateVM _viewModel;

        public Exchange Exchange => _viewModel.Exchange;

        public ExchangeCreate()
        {
            InitializeComponent();

            _viewModel = new ExchangeCreateVM();
            DataContext = _viewModel;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
