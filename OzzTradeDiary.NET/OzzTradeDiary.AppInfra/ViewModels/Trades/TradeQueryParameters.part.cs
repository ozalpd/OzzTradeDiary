using TD.Models;

namespace TD.AppInfra.ViewModels.Trades;

public partial class TradeQueryParametersVM
{
    public TradeStatusQuery? ByTradeStatus
    {
        get => Parameters.TradeStatus;
        set
        {
            Parameters.TradeStatus = value;
            RaisePropertyChanged(nameof(ByTradeStatus));
        }
    }
}

