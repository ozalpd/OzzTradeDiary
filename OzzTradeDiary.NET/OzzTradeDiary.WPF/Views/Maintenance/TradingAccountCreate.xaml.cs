using System;
using System.Windows;
using TD.Models;
using TD.WPF.ViewModels.Maintenance;

namespace TD.WPF.Views.Maintenance
{
    /// <summary>
    /// Interaction logic for CreateTradingAccount.xaml
    /// </summary>
    public partial class CreateTradingAccount : Window
    {
        private readonly CreateTradingAccountVM _viewModel;

        public TradingAccount TradingAccount => _viewModel.TradingAccount;

        public CreateTradingAccount()
        {
            InitializeComponent();

            _viewModel = new CreateTradingAccountVM();
            DataContext = _viewModel;
            SourceInitialized += CreateTradingAccount_SourceInitialized;
        }

        private async void CreateTradingAccount_SourceInitialized(object? sender, EventArgs e)
        {
            try
            {
                await _viewModel.LoadExchangesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Load exchanges failed.\n{ex.Message}", "Ozz Trade Diary", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
