using System.Windows;
using TD.Models;
using TD.WPF.ViewModels.Maintenance;

namespace TD.WPF.Views.Maintenance
{
    /// <summary>
    /// Interaction logic for TradingAccountEditView.xaml
    /// </summary>
    public partial class TradingAccountEditView
    {
        private TradingAccountEditVM _viewModel;

        public TradingAccountEditView()
        {
            // This constructor should not be called, but we need it for the designer to work.
            // We will create a dummy TradingAccount for the designer.
            InitializeComponent();
            _viewModel = new TradingAccountEditVM(new TradingAccount());
            _isDirty = _viewModel;
            DataContext = _viewModel;
        }

        public TradingAccountEditView(TradingAccount tradingAccount) : base(new TradingAccountEditVM(tradingAccount))
        {
            InitializeComponent();
            //_viewModel = new TradingAccountEditVM(tradingAccount);
            _viewModel = (TradingAccountEditVM)DataContext;
        }

        public TradingAccount TradingAccount
        {
            get { return _viewModel.TradingAccount; }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_viewModel.ValidateModel())
                return;

            DialogResult = true;
        }
    }
}
