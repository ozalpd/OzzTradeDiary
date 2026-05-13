using System.Windows;
using TD.Models;
using TD.WPF.ViewModels.Maintenance;

namespace TD.WPF.Views.Maintenance
{
    // Codebehind for CurrencyEditView.xaml
    public partial class CurrencyEditView : AbstractEditView
    {
        private readonly CurrencyEditVM _viewModel;

        public Currency Currency => _viewModel.Currency;

        /// <summary>
        /// This constructor should not be called, but we need it for the designer to work.
        /// </summary>
        /// <remarks>This constructor creates a dummy Currency for the designer.</remarks>
        public CurrencyEditView()
        {
            InitializeComponent();

            _viewModel = new CurrencyEditVM(new Currency());
            _isDirty = _viewModel;
            DataContext = _viewModel;
            SourceInitialized += CurrencyEditView_SourceInitialized;
        }

        /// <summary>
        /// This constructor should be used at runtime to create the view with a real Currency.
        /// </summary>
        /// <param name="currency">The real Currency to be used by the view.</param>
        public CurrencyEditView(Currency currency) : base(new CurrencyEditVM(currency))
        {
            InitializeComponent();
            _viewModel = (CurrencyEditVM)DataContext;
        }

        private async void CurrencyEditView_SourceInitialized(object? sender, EventArgs e)
        {
            OnSourceInitialized();
        }
        partial void OnSourceInitialized();

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_viewModel.ValidateModel())
                return;

            DialogResult = true;
        }
    }
}