using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TD.Models;
using TD.WPF.ViewModels.Trades;

namespace TD.WPF.Views.Trades
{
    // Codebehind for TradeEditView.xaml
    public partial class TradeEditView : AbstractEditView
    {
        private readonly TradeEditVM _viewModel;

        public Trade Trade => _viewModel.Trade;

        /// <summary>
        /// This constructor should not be called, but we need it for the designer to work.
        /// </summary>
        /// <remarks>This constructor creates a dummy Trade for the designer.</remarks>
        public TradeEditView()
        {
            InitializeComponent();

            _viewModel = new TradeEditVM(new Trade());
            _isDirty = _viewModel;
            DataContext = _viewModel;
            SourceInitialized += TradeEditView_SourceInitialized;
        }

        /// <summary>
        /// This constructor should be used at runtime to create the view with a real Trade.
        /// </summary>
        /// <param name="trade">The real Trade to be used by the view.</param>
        public TradeEditView(Trade trade) : base(new TradeEditVM(trade))
        {
            InitializeComponent();
            _viewModel = (TradeEditVM)DataContext;
        }

        private async void TradeEditView_SourceInitialized(object? sender, EventArgs e)
        {
            OnSourceInitialized();
        }
        partial void OnSourceInitialized();

        private void DecimalTextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (sender is not TextBox tb)
                return;
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                var pasteText = (string)e.DataObject.GetData(typeof(string));
                var proposed = tb.Text.Remove(tb.SelectionStart, tb.SelectionLength).Insert(tb.SelectionStart, pasteText);
                var decimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
                if (!Regex.IsMatch(proposed, @"^-?\d*(" + Regex.Escape(decimalSeparator) + @"\d*)?$"))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private void DecimalTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (sender is not TextBox tb)
                return;

            var decimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            var proposed = tb.Text.Remove(tb.SelectionStart, tb.SelectionLength).Insert(tb.SelectionStart, e.Text);
            e.Handled = !Regex.IsMatch(proposed, @"^-?\d*(" + Regex.Escape(decimalSeparator) + @"\d*)?$");
        }

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