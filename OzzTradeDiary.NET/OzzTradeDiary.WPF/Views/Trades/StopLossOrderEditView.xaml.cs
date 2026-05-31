using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TD.Models;
using TD.WPF.ViewModels.Trades;

namespace TD.WPF.Views.Trades
{
    // Codebehind for StopLossOrderEditView.xaml
    public partial class StopLossOrderEditView : AbstractEditView
    {
        private readonly StopLossOrderEditVM _viewModel;

        public StopLossOrder StopLossOrder => _viewModel.StopLossOrder;

        /// <summary>
        /// This constructor should not be called, but we need it for the designer to work.
        /// </summary>
        /// <remarks>This constructor creates a dummy StopLossOrder for the designer.</remarks>
        public StopLossOrderEditView()
        {
            InitializeComponent();

            _viewModel = new StopLossOrderEditVM(new StopLossOrder());
            _isDirty = _viewModel;
            DataContext = _viewModel;
            SourceInitialized += StopLossOrderEditView_SourceInitialized;
        }

        /// <summary>
        /// This constructor should be used at runtime to create the view with a real StopLossOrder.
        /// </summary>
        /// <param name="stopLossOrder">The real StopLossOrder to be used by the view.</param>
        public StopLossOrderEditView(StopLossOrder stopLossOrder) : base(new StopLossOrderEditVM(stopLossOrder))
        {
            InitializeComponent();
            _viewModel = (StopLossOrderEditVM)DataContext;
        }

        private async void StopLossOrderEditView_SourceInitialized(object? sender, EventArgs e)
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

        private void SetFilledPriceButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.FilledPrice = _viewModel.OrderPrice;
        }

        private void SetFilledQuantityButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.FilledQuantity = _viewModel.OrderQuantity;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_viewModel.ValidateModel())
                return;

            DialogResult = true;
        }
    }
}