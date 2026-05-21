using TD.Models;

namespace TD.WPF.ViewModels.Trades
{
    public partial class TradeListVM
    {
        protected override void OnSelectedItemChanged(Trade? oldSelectedItem)
        {
            if (SelectedItem == null)
                return;

            _ = LoadNavigationCollectionsAsync(SelectedItem);
        }

        public async Task LoadNavigationCollectionsAsync(Trade trade)
        {
            if (trade == null)
                return;

            await TradeRepository.LoadNavigationCollectionsAsync(trade);
        }
    }
}
