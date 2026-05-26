using System.Windows;
using TD.Models;
using TD.WPF.ViewModels.Trades;

namespace TD.WPF.Views.Trades
{
    // Codebehind for TradeDetailView.xaml
    public partial class TradeDetailView : Window
    {
        private readonly Trade _viewModel;


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
        }

        private async void TradeDetailView_SourceInitialized(object? sender, EventArgs e)
        {
            OnSourceInitialized();
        }
        partial void OnSourceInitialized();
    }
}