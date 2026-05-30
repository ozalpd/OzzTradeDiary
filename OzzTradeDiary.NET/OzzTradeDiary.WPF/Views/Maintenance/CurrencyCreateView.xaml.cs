using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TD.Models;
using TD.WPF.ViewModels.Maintenance;

namespace TD.WPF.Views.Maintenance
{
    // Codebehind for CurrencyCreateView.xaml
    public partial class CurrencyCreateView : Window
    {
        private readonly CurrencyCreateVM _viewModel;

        public Currency Currency => _viewModel.Currency;

        public CurrencyCreateView()
        {
            InitializeComponent();

            _viewModel = new CurrencyCreateVM();
            DataContext = _viewModel;
            SourceInitialized += CurrencyCreateView_SourceInitialized;
        }

        private async void CurrencyCreateView_SourceInitialized(object? sender, EventArgs e)
        {
            OnSourceInitialized();
        }
        partial void OnSourceInitialized();

        private void IntegerTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (sender is not TextBox tb)
                return;

            e.Handled = !char.IsDigit(e.Text, 0);
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_viewModel.ValidateModel())
                return;

            DialogResult = true;
        }
    }
}