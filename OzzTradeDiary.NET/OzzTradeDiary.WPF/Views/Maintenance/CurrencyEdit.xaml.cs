using System.Windows;
using TD.Models;
using TD.WPF.ViewModels.Maintenance;

namespace TD.WPF.Views.Maintenance
{
    /// <summary>
    /// Interaction logic for CurrencyEdit.xaml
    /// </summary>
    public partial class CurrencyEdit
    {
        private CurrencyEditVM _viewModel;

        public CurrencyEdit()
        {
            // This constructor should not be called, but we need it for the designer to work.
            // We will create a dummy Currency for the designer.
            InitializeComponent();
            _viewModel = new CurrencyEditVM(new Currency());
            DataContext = _viewModel;
        }

        public CurrencyEdit(Currency currency) : base(new CurrencyEditVM(currency))
        {
            InitializeComponent();
            _viewModel = (CurrencyEditVM)DataContext;
        }

        public Currency Currency
        {
            get { return _viewModel.Currency; }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
