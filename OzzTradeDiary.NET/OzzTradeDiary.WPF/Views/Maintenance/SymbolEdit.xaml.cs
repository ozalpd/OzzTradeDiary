using System.Windows;
using TD.Models;
using TD.WPF.ViewModels.Maintenance;

namespace TD.WPF.Views.Maintenance
{
    /// <summary>
    /// Interaction logic for SymbolEdit.xaml
    /// </summary>
    public partial class SymbolEdit
    {
        private SymbolEditVM _viewModel;

        public SymbolEdit()
        {
            // This constructor should not be called, but we need it for the designer to work.
            // We will create a dummy Symbol for the designer.
            InitializeComponent();
            _viewModel = new SymbolEditVM(new Symbol());
            _isDirty = _viewModel;
            DataContext = _viewModel;
        }

        public SymbolEdit(Symbol Symbol) : base(new SymbolEditVM(Symbol))
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
            DialogResult = true;
        }
    }
}
