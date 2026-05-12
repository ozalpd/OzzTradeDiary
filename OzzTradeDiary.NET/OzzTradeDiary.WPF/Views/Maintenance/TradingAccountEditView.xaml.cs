using System.Windows;
using TD.Models;
using TD.WPF.ViewModels.Maintenance;

namespace TD.WPF.Views.Maintenance
{
    // Codebehind for TradingAccountEditView.xaml
    public partial class TradingAccountEditView : AbstractEditView
    {
        private readonly TradingAccountEditVM _viewModel;

        public TradingAccount TradingAccount => _viewModel.TradingAccount;

        /// <summary>
        /// This constructor should not be called, but we need it for the designer to work.
        /// </summary>
        /// <remarks>This constructor creates a dummy TradingAccount for the designer.</remarks>
        public TradingAccountEditView()
        {
            InitializeComponent();

            _viewModel = new TradingAccountEditVM(new TradingAccount());
            _isDirty = _viewModel;
            DataContext = _viewModel;
        }

        /// <summary>
        /// This constructor should be used at runtime to create the view with a real TradingAccount.
        /// </summary>
        /// <param name="tradingAccount">The real TradingAccount to be used by the view.</param>
        public TradingAccountEditView(TradingAccount tradingAccount) : base(new TradingAccountEditVM(tradingAccount))
        {
            InitializeComponent();
            _viewModel = (TradingAccountEditVM)DataContext;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_viewModel.ValidateModel())
                return;

            DialogResult = true;
        }
    }
}