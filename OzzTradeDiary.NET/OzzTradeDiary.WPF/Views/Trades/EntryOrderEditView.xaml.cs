using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TD.Models;
using TD.WPF.ViewModels.Trades;

namespace TD.WPF.Views.Trades
{
    // Codebehind for EntryOrderEditView.xaml
    public partial class EntryOrderEditView : AbstractEditView
    {
        private readonly EntryOrderEditVM _viewModel;

        public EntryOrder EntryOrder => _viewModel.EntryOrder;

        /// <summary>
        /// This constructor should not be called, but we need it for the designer to work.
        /// </summary>
        /// <remarks>This constructor creates a dummy EntryOrder for the designer.</remarks>
        public EntryOrderEditView()
        {
            InitializeComponent();

            _viewModel = new EntryOrderEditVM(new EntryOrder());
            _isDirty = _viewModel;
            DataContext = _viewModel;
            SourceInitialized += EntryOrderEditView_SourceInitialized;
        }

        /// <summary>
        /// This constructor should be used at runtime to create the view with a real EntryOrder.
        /// </summary>
        /// <param name="entryOrder">The real EntryOrder to be used by the view.</param>
        public EntryOrderEditView(EntryOrder entryOrder) : base(new EntryOrderEditVM(entryOrder))
        {
            InitializeComponent();
            _viewModel = (EntryOrderEditVM)DataContext;
        }

        private async void EntryOrderEditView_SourceInitialized(object? sender, EventArgs e)
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