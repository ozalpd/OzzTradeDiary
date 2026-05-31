using TD.Models;

namespace TD.WPF.ViewModels.Trades
{
    public partial class TakeProfitOrderEditVM
    {
        /// <summary>
        /// Gets the planned profit amount for this specific take profit order,
        /// calculated from the trade's entry price, this order's price and quantity.
        /// Recalculates when OrderPrice, OrderQuantity or Trade changes.
        /// </summary>
        public decimal? OrderProfitAmount => _takeProfitOrder.OrderProfitAmount;

        partial void OnInitialized()
        {
            PropertyChanged += (_, e) =>
            {
                if (e.PropertyName is nameof(OrderPrice) or nameof(OrderQuantity) or nameof(Trade))
                {
                    RaisePropertyChanged(nameof(OrderValue));
                    RaisePropertyChanged(nameof(OrderProfitAmount));
                }
            };
        }
    }
}
