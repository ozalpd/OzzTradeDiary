namespace TD.WPF.ViewModels.Trades
{
    public partial class TradeEditVM
    {
        partial void OnInitialized()
        {
            PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(OrderQuantity) || e.PropertyName == nameof(PlannedEntryPrice))
            {
                if (!_calculatingOrderQuantity)
                    RaisePropertyChanged(nameof(PlannedPositionValue));
            }

            if (e.PropertyName == nameof(FilledQuantity) || e.PropertyName == nameof(ExecutedEntryPrice))
            {
                if (!_calculatingFilledQuantity)
                    RaisePropertyChanged(nameof(ExecutedPositionValue));
            }
        }

        public void SetQuantity(decimal positionValue, bool planned = true)
        {
            Trade.SetQuantity(positionValue, planned);
            if (planned)
            {
                RaisePropertyChanged(nameof(OrderQuantity));
            }
            else
            {
                RaisePropertyChanged(nameof(FilledQuantity));
            }
        }

        public void SetQuantity(string positionValue, bool planned = true)
        {
            decimal posValue = 0;
            if (decimal.TryParse(positionValue, out posValue))
            {
                _calculatingFilledQuantity = !planned;
                _calculatingOrderQuantity = planned;
                SetQuantity(posValue, planned);
                _calculatingFilledQuantity = false;
                _calculatingOrderQuantity = false;
            }
        }
        bool _calculatingFilledQuantity = false;
        bool _calculatingOrderQuantity = false;
    }
}
