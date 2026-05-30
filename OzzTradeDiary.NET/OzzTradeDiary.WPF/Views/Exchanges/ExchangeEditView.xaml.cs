using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TD.Models;
using TD.WPF.ViewModels.Exchanges;

namespace TD.WPF.Views.Exchanges
{
    // Codebehind for ExchangeEditView.xaml
    public partial class ExchangeEditView : AbstractEditView
    {
        private readonly ExchangeEditVM _viewModel;

        public Exchange Exchange => _viewModel.Exchange;

        /// <summary>
        /// This constructor should not be called, but we need it for the designer to work.
        /// </summary>
        /// <remarks>This constructor creates a dummy Exchange for the designer.</remarks>
        public ExchangeEditView()
        {
            InitializeComponent();

            _viewModel = new ExchangeEditVM(new Exchange());
            _isDirty = _viewModel;
            DataContext = _viewModel;
            SourceInitialized += ExchangeEditView_SourceInitialized;
        }

        /// <summary>
        /// This constructor should be used at runtime to create the view with a real Exchange.
        /// </summary>
        /// <param name="exchange">The real Exchange to be used by the view.</param>
        public ExchangeEditView(Exchange exchange) : base(new ExchangeEditVM(exchange))
        {
            InitializeComponent();
            _viewModel = (ExchangeEditVM)DataContext;
        }

        private async void ExchangeEditView_SourceInitialized(object? sender, EventArgs e)
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