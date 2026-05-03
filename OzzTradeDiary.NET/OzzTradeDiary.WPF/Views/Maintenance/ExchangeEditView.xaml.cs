using System.Windows;
using TD.Models;
using TD.WPF.ViewModels.Maintenance;

namespace TD.WPF.Views.Maintenance
{
    /// <summary>
    /// Interaction logic for ExchangeEditView.xaml
    /// </summary>
    public partial class ExchangeEditView
    {
        private ExchangeEditVM _viewModel;

        public ExchangeEditView()
        {
            // This constructor should not be called, but we need it for the designer to work.
            // We will create a dummy Exchange for the designer.
            InitializeComponent();
            _viewModel = new ExchangeEditVM(new Exchange());
            DataContext = _viewModel;
        }

        public ExchangeEditView(Exchange exchange) : base(new ExchangeEditVM(exchange))
        {
            InitializeComponent();
            _viewModel = (ExchangeEditVM)DataContext;
        }

        public Exchange Exchange
        {
            get { return _viewModel.Exchange; }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_viewModel.ValidateModel())
                return;

            DialogResult = true;
        }
    }
}
