using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TD.WPF.Views.Trades
{
    public partial class TradeCreateView
    {
        partial void OnSourceInitialized()
        {
            _ = _viewModel.SetAllSymbolsAsync();
            PlannedPositionValueTextBox.PreviewTextInput += CalculatedTextBox_TextInput;
            PlannedEntryPriceTextBox.PreviewTextInput += CalculatedTextBox_TextInput;
            _viewModel.PropertyChanged += OnPropertyChanged;

            SetPositionValueReadOnly();
        }

        private void OnPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.PlannedEntryPrice))
            {
                SetPositionValueReadOnly();
            }
        }

        private void CalculatedTextBox_TextInput(object sender, TextCompositionEventArgs e)
        {
            if (sender is not TextBox tb)
                return;

            if (tb == PlannedPositionValueTextBox)
                _viewModel.SetOrderQuantity(tb.Text + e.Text);

            if (tb == PlannedEntryPriceTextBox)
            {
                if (_viewModel.PlannedEntryPrice > 0 && e.Text == "\t")
                {
                    PlannedPositionValueTextBox.Focus();
                }

                if (_viewModel.PlannedEntryPrice > 0)
                    return;

                if (decimal.TryParse(tb.Text, out var price) && price > 0)
                {
                    istyping = true;
                    _viewModel.PlannedEntryPrice = price;
                    istyping = false;
                }
            }
        }
        bool istyping = false;

        public void SetPositionValueReadOnly()
        {
            if (_viewModel.PlannedEntryPrice > 0)
            {
                PlannedPositionValueTextBox.Style = (Style)FindResource("ValidationTextBoxStyle");
                if (!istyping)
                    PlannedPositionValueTextBox.Focus();
            }
            else
            {
                PlannedPositionValueTextBox.Style = (Style)FindResource("ReadOnlyTextBoxStyle");
            }
        }
    }
}
