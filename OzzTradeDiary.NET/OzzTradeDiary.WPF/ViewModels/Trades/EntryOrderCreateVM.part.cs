using System;
using System.Collections.Generic;
using System.Text;

namespace TD.WPF.ViewModels.Trades
{
    public partial class EntryOrderCreateVM
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
        }
    }
}
