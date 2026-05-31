using TD.Models;

namespace TD.WPF.ViewModels.Trades
{
    public partial class StopLossOrderEditVM
    {
        /// <summary>
        /// Gets the planned risk amount for this specific stop loss order,
        /// calculated from the trade's entry price, this order's price and quantity.
        /// Recalculates when OrderPrice, OrderQuantity or Trade changes.
        /// </summary>
        public decimal? OrderRiskAmount => _stopLossOrder.OrderRiskAmount;

        partial void OnInitialized()
        {
            PropertyChanged += (_, e) =>
            {
                if (e.PropertyName is nameof(OrderPrice) or nameof(OrderQuantity) or nameof(Trade))
                {
                    RaisePropertyChanged(nameof(OrderValue));
                    RaisePropertyChanged(nameof(OrderRiskAmount));
                }
            };
        }
    }
}
