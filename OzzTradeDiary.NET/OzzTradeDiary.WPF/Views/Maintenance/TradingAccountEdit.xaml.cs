using System.Windows;
using TD.Models;
using TD.WPF.ViewModels.Maintenance;

namespace TD.WPF.Views.Maintenance
{
    /// <summary>
    /// Interaction logic for TradingAccountEdit.xaml
    /// </summary>
    public partial class TradingAccountEdit : Window
    {
        private TradingAccountEditVM _viewModel;

        public TradingAccountEdit()
        {
            // This constructor should not be called, but we need it for the designer to work.
            // We will create a dummy TradingAccount for the designer.
            InitializeComponent();
            _viewModel = new TradingAccountEditVM(new TradingAccount());
            DataContext = _viewModel;
        }

        public TradingAccountEdit(TradingAccount tradingAccount)
        {
            InitializeComponent();
            _viewModel = new TradingAccountEditVM(tradingAccount);
            DataContext = _viewModel;
        }

        public TradingAccount TradingAccount
        {
            get { return _viewModel.TradingAccount; }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
