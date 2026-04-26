using TD.RepositoryContracts;
using TD.WPF.Commands;
using TD.WPF.ViewModels.Trades;

namespace TD.WPF.ViewModels
{
    internal class MainWindowVM : AbstractDiaryVM
    {
        public MainWindowVM(ITradeRepository tradeRepository)
        {
            TradeRepository = tradeRepository ?? throw new ArgumentNullException(nameof(tradeRepository));

            TradeHistory = new TradeHistoryVM();
            ShowMaintenanceCommand = new ShowMaintenanceCommand();
            ShowAboutCommand = new ShowAboutCommand();
            ExitCommand = new ExitCommand();
        }

        public ITradeRepository TradeRepository { get; }
        public TradeHistoryVM TradeHistory { get; }
        public ShowMaintenanceCommand ShowMaintenanceCommand { get; }
        public ShowAboutCommand ShowAboutCommand { get; }
        public ExitCommand ExitCommand { get; }
    }
}
