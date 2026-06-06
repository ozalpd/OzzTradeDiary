using System.Collections.ObjectModel;
using TD.Extensions;
using TD.Models;
using TD.WPF.Commands.Trades;

namespace TD.WPF.ViewModels.Trades
{
    public partial class TradeHistoryVM
    {
        public ObservableCollection<TradeImage> TradeImages { get; }
        public TradeImageCreateCommand TradeImageCreateCommand { get; }
        public TradeImageDeleteCommand TradeImageDeleteCommand { get; }
        public TradeImageDetailCommand TradeImageDetailCommand { get; }
        public TradeImageEditCommand TradeImageEditCommand { get; }

        /// <summary>
        /// Gets the collection of available TradeImageCategory enum members for selection or display.
        /// </summary>
        public IEnumerable<EnumValueItem<TradeImageCategory>> TradeImageCategoryValues { get; }

        public TradeImage? SelectedTradeImage
        {
            get { return _selectedTradeImage; }
            set
            {
                _selectedTradeImage = value;
                RaisePropertyChanged(nameof(SelectedTradeImage));
                RaiseTradeImageCmdCanExecute();
            }
        }
        TradeImage? _selectedTradeImage;

        /// <summary>
        /// Removes <paramref name="TradeImage"/> from <see cref="Trade.TradeImages"/> of
        /// <see cref="SelectedTrade"/>, then persists <em>all</em> remaining entry orders
        /// in a single repository call (<see cref="ITradeRepository.SaveTradeImagesAsync"/>).
        /// </summary>
        /// <remarks>
        /// The save call persists the entire <see cref="Trade.TradeImages"/> collection after
        /// removal, so any other in-memory modifications to that collection are also committed.
        /// Returns <c>false</c> without saving if <see cref="SelectedTrade"/> is <c>null</c> or
        /// <paramref name="TradeImage"/> has not been persisted yet (<c>Id == 0</c>).
        /// </remarks>
        /// <param name="TradeImage">The entry order to delete.</param>
        /// <returns><c>true</c> if the order was found and removed; otherwise <c>false</c>.</returns>
        public async Task<bool> DeleteTradeImageAsync(TradeImage TradeImage)
        {
            if (SelectedTrade == null)
                return false;

            ArgumentNullException.ThrowIfNull(TradeImage, nameof(TradeImage));
            bool isDeleted = false;
            if (TradeImage.Id > 0)
            {
                isDeleted = SelectedTrade.TradeImages.Remove(TradeImage);
                await TradeRepository.SaveTradeImagesAsync(SelectedTrade);
                RefreshTrades();
            }

            return isDeleted;
        }

        private void ReplaceTradeImages()
        {
            TradeImages.Clear();
            if (SelectedTrade != null)
            {
                ReplaceCollection(TradeImages, SelectedTrade.TradeImages);
            }
            RaiseTradeImageCmdCanExecute();
        }

        private void RaiseTradeImageCmdCanExecute()
        {
            TradeImageCreateCommand.RaiseCanExecuteChanged();
            TradeImageDeleteCommand.RaiseCanExecuteChanged();
            TradeImageEditCommand.RaiseCanExecuteChanged();
            TradeImageDetailCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Adds or updates <paramref name="TradeImage"/> within <see cref="Trade.TradeImages"/> of
        /// <see cref="SelectedTrade"/>, then persists <em>all</em> entry orders of that trade
        /// in a single repository call (<see cref="ITradeRepository.SaveTradeImagesAsync"/>).
        /// </summary>
        /// <remarks>
        /// Despite its singular name, this method always saves the entire
        /// <see cref="Trade.TradeImages"/> collection — not just the supplied order.
        /// If <paramref name="TradeImage"/> is new (<c>Id == 0</c>) it is appended to the collection
        /// before saving; if it already exists it is updated in place.
        /// </remarks>
        /// <param name="TradeImage">The entry order to add or update.</param>
        public async Task SaveTradeImageAsync(TradeImage TradeImage)
        {
            if (SelectedTrade == null)
                return;
            if (TradeImage.Id == 0)
            {
                SelectedTrade.TradeImages.Add(TradeImage);
                TradeImage.Trade = SelectedTrade;
            }
            await TradeRepository.SaveTradeImagesAsync(SelectedTrade);
            RefreshTrades();
        }
    }
}
