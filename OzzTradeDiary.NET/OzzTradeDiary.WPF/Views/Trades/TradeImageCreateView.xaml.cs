using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TD.Models;
using TD.WPF.ViewModels.Trades;

namespace TD.WPF.Views.Trades
{
    // Codebehind for TradeImageCreateView.xaml
    public partial class TradeImageCreateView : Window
    {
        private readonly TradeImageCreateVM _viewModel;
        private Trade? _preselectedTrade;

        public TradeImage TradeImage => _viewModel.TradeImage;

        /// <summary>
        /// This constructor should not be called, but we need it for the designer to work.
        /// </summary>
        /// <remarks>This constructor creates a dummy TradeImage for the designer.</remarks>
        public TradeImageCreateView() : this(null)
        {

        }

        /// <summary>
        /// This constructor should be used at runtime to create the view with a real TradeImage.
        /// </summary>
        /// <param name="tradeImage">The real TradeImage to be used by the view.</param>
        public TradeImageCreateView(Trade? preselectedTrade)
        {
            InitializeComponent();

            _viewModel = new TradeImageCreateVM();
            DataContext = _viewModel;
            _preselectedTrade = preselectedTrade;
            SourceInitialized += TradeImageCreateView_SourceInitialized;
        }

        private async void TradeImageCreateView_SourceInitialized(object? sender, EventArgs e)
        {
            if (_preselectedTrade != null)
            {
                _viewModel.Trade = _preselectedTrade;
                _viewModel.TradeId = _preselectedTrade.Id;
            }
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