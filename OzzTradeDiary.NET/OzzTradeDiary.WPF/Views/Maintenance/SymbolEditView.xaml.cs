using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TD.Models;
using TD.WPF.ViewModels.Maintenance;

namespace TD.WPF.Views.Maintenance
{
    // Codebehind for SymbolEditView.xaml
    public partial class SymbolEditView : AbstractEditView
    {
        private readonly SymbolEditVM _viewModel;

        public Symbol Symbol => _viewModel.Symbol;

        /// <summary>
        /// This constructor should not be called, but we need it for the designer to work.
        /// </summary>
        /// <remarks>This constructor creates a dummy Symbol for the designer.</remarks>
        public SymbolEditView()
        {
            InitializeComponent();

            _viewModel = new SymbolEditVM(new Symbol());
            _isDirty = _viewModel;
            DataContext = _viewModel;
            SourceInitialized += SymbolEditView_SourceInitialized;
        }

        /// <summary>
        /// This constructor should be used at runtime to create the view with a real Symbol.
        /// </summary>
        /// <param name="symbol">The real Symbol to be used by the view.</param>
        public SymbolEditView(Symbol symbol) : base(new SymbolEditVM(symbol))
        {
            InitializeComponent();
            _viewModel = (SymbolEditVM)DataContext;
        }

        private async void SymbolEditView_SourceInitialized(object? sender, EventArgs e)
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