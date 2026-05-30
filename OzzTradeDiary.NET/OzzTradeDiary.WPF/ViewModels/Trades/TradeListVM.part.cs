using TD.Models;

namespace TD.WPF.ViewModels.Trades
{
    public partial class TradeListVM
    {
        protected override void OnSelectedItemChanged(Trade? oldSelectedItem)
        {
            if (SelectedItem != null)
                _ = LoadNavigationCollectionsAsync(SelectedItem);
        }

        public virtual async Task LoadNavigationCollectionsAsync(Trade trade)
        {
            if (trade != null)
                await TradeRepository.LoadNavigationCollectionsAsync(trade);
        }
    }
}
