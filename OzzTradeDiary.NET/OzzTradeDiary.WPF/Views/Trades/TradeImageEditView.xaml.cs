using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TD.Models;
using TD.WPF.ViewModels.Trades;

namespace TD.WPF.Views.Trades
{
    // Codebehind for TradeImageEditView.xaml
    public partial class TradeImageEditView : AbstractEditView
    {
        private readonly TradeImageEditVM _viewModel;

        public TradeImage TradeImage => _viewModel.TradeImage;

        /// <summary>
        /// This constructor should not be called, but we need it for the designer to work.
        /// </summary>
        /// <remarks>This constructor creates a dummy TradeImage for the designer.</remarks>
        public TradeImageEditView()
        {
            InitializeComponent();

            _viewModel = new TradeImageEditVM(new TradeImage());
            _isDirty = _viewModel;
            DataContext = _viewModel;
            SourceInitialized += TradeImageEditView_SourceInitialized;
        }

        /// <summary>
        /// This constructor should be used at runtime to create the view with a real TradeImage.
        /// </summary>
        /// <param name="tradeImage">The real TradeImage to be used by the view.</param>
        public TradeImageEditView(TradeImage tradeImage) : base(new TradeImageEditVM(tradeImage))
        {
            InitializeComponent();
            _viewModel = (TradeImageEditVM)DataContext;
        }

        private async void TradeImageEditView_SourceInitialized(object? sender, EventArgs e)
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