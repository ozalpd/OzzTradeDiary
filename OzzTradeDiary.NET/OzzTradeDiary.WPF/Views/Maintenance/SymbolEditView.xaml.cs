using System.Windows;
using TD.Models;
using TD.WPF.ViewModels.Maintenance;

namespace TD.WPF.Views.Maintenance
{
    /// <summary>
    /// Interaction logic for SymbolEditView.xaml
    /// </summary>
    public partial class SymbolEditView
    {
        private SymbolEditVM _viewModel;

        public SymbolEditView()
        {
            // This constructor should not be called, but we need it for the designer to work.
            // We will create a dummy Symbol for the designer.
            InitializeComponent();
            _viewModel = new SymbolEditVM(new Symbol());
            _isDirty = _viewModel;
            DataContext = _viewModel;
        }

        public SymbolEditView(Symbol Symbol) : base(new SymbolEditVM(Symbol))
        {
            InitializeComponent();
            //_viewModel = new SymbolEditVM(Symbol);
            _viewModel = (SymbolEditVM)DataContext;
        }

        public Symbol Symbol
        {
            get { return _viewModel.Symbol; }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_viewModel.ValidateModel())
                return;

            DialogResult = true;
        }
    }
}
