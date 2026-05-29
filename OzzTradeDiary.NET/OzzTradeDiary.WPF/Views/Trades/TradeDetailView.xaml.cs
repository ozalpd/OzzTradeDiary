using System.Windows;
using TD.Models;
using TD.WPF.Models;

namespace TD.WPF.Views.Trades
{
    // Codebehind for TradeDetailView.xaml
    public partial class TradeDetailView : Window
    {
        private readonly Trade _viewModel;
        private readonly AppSettings _appSettings = AppSettings.GetAppSettings();


        /// <summary>
        /// This constructor should not be called, but we need it for the designer to work.
        /// </summary>
        /// <remarks>This constructor creates a dummy Trade for the designer.</remarks>
        public TradeDetailView()
        {
            InitializeComponent();

            _viewModel = new Trade();
            DataContext = _viewModel;
            SourceInitialized += TradeDetailView_SourceInitialized;
        }

        /// <summary>
        /// This constructor should be used at runtime to create the view with a real Trade.
        /// </summary>
        /// <param name="trade">The real Trade to be used by the view.</param>
        public TradeDetailView(Trade trade)
        {
            InitializeComponent();

            _viewModel = trade;
            DataContext = _viewModel;
            SourceInitialized += TradeDetailView_SourceInitialized;
            Closing += Window_Closing;
        }

        private async void TradeDetailView_SourceInitialized(object? sender, EventArgs e)
        {
            OnSourceInitialized();
            _appSettings.TradeDetailViewPosition.SetWindowPositions(this);
        }
        partial void OnSourceInitialized();

        private void Window_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            _appSettings.TradeDetailViewPosition.GetWindowPositions(this);
            _appSettings.Save();
        }
    }
}