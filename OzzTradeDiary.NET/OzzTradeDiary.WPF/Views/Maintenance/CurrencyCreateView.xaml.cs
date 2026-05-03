using System.Windows;
using TD.Models;
using TD.WPF.ViewModels.Maintenance;

namespace TD.WPF.Views.Maintenance
{
    /// <summary>
    /// Interaction logic for CurrencyCreateView.xaml
    /// </summary>
    public partial class CurrencyCreateView : Window
    {
        private readonly CurrencyCreateVM _viewModel;

        public Currency Currency => _viewModel.Currency;

        public CurrencyCreateView()
        {
            InitializeComponent();

            _viewModel = new CurrencyCreateVM();
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
