using System.Windows;
using TD.Models;
using TD.WPF.Models;

namespace TD.WPF.Views.Trades
{
    // Codebehind for TradeImageDetailView.xaml
    public partial class TradeImageDetailView : Window
    {
        private readonly TradeImage _viewModel;
        private readonly AppSettings _appSettings = AppSettings.GetAppSettings();


        /// <summary>
        /// This constructor should not be called, but we need it for the designer to work.
        /// </summary>
        /// <remarks>This constructor creates a dummy TradeImage for the designer.</remarks>
        public TradeImageDetailView()
        {
            InitializeComponent();

            _viewModel = new TradeImage();
            DataContext = _viewModel;
        }

        /// <summary>
        /// This constructor should be used at runtime to create the view with a real TradeImage.
        /// </summary>
        /// <param name="tradeImage">The real TradeImage to be used by the view.</param>
        public TradeImageDetailView(TradeImage tradeImage)
        {
            InitializeComponent();

            _viewModel = tradeImage;
            DataContext = _viewModel;
            SourceInitialized += TradeImageDetailView_SourceInitialized;
            Deactivated += Window_Deactivated;
            Closing += Window_Closing;
        }

        private async void TradeImageDetailView_SourceInitialized(object? sender, EventArgs e)
        {
            _appSettings.TradeImageDetailViewPosition.SetWindowPositions(this);
        }

        private void Window_Deactivated(object? sender, EventArgs e)
        {
            if (!_isClosimg)
                Close();
        }

        private void Window_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            _isClosimg = true;
            _appSettings.TradeImageDetailViewPosition.GetWindowPositions(this);
            _appSettings.Save();
        }
        bool _isClosimg = false;
    }
}