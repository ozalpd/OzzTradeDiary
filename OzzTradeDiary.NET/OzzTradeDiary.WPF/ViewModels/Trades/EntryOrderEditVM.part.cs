namespace TD.WPF.ViewModels.Trades
{
    public partial class EntryOrderEditVM
    {
        partial void OnInitialized()
        {
            PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName is nameof(OrderPrice) or nameof(OrderQuantity) or nameof(Trade))
            {
                RaisePropertyChanged(nameof(OrderValue));
            }

            if (e.PropertyName is nameof(FilledPrice) or nameof(FilledQuantity) or nameof(Trade))
            {
                RaisePropertyChanged(nameof(FilledValue));
            }
        }
    }
}
