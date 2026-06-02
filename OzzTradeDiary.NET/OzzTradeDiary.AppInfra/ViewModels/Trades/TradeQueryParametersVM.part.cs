using TD.Models;

namespace TD.AppInfra.ViewModels.Trades;

public partial class TradeQueryParametersVM
{
    public DateTime? ByEndDate
    {
        get => Parameters.EndDate;
        set
        {
            Parameters.EndDate = value;
            RaisePropertyChanged(nameof(ByEndDate));
        }
    }

    public DateTime? ByStartDate
    {
        get => Parameters.StartDate;
        set
        {
            Parameters.StartDate = value;
            RaisePropertyChanged(nameof(ByStartDate));
        }
    }

    public TradeDateType? ByTradeDateType
    {
        get => Parameters.TradeDateType;
        set
        {
            Parameters.TradeDateType = value;
            RaisePropertyChanged(nameof(TradeDateType));
            SetTradeDates();
        }
    }

    public TradeStatusQuery? ByTradeStatus
    {
        get => Parameters.TradeStatus;
        set
        {
            Parameters.TradeStatus = value;
            RaisePropertyChanged(nameof(ByTradeStatus));
        }
    }

    private void SetTradeDates()
    {
        if (ByTradeDateType == null)
            return;

        var tradeDateType = ByTradeDateType.Value;

        EntryTimeMin = tradeDateType == TradeDateType.EntryTime ? ByStartDate : null;
        EntryTimeMax = tradeDateType == TradeDateType.EntryTime ? ByEndDate : null;

        ExitTimeMin = tradeDateType == TradeDateType.ExitTime ? ByStartDate : null;
        ExitTimeMax = tradeDateType == TradeDateType.ExitTime ? ByEndDate : null;

        CancellationTimeMin = tradeDateType == TradeDateType.CancellationTime ? ByStartDate : null;
        CancellationTimeMax = tradeDateType == TradeDateType.CancellationTime ? ByEndDate : null;

        UpdatedAtMin = tradeDateType == TradeDateType.UpdateTime ? ByStartDate : null;
        UpdatedAtMax = tradeDateType == TradeDateType.UpdateTime ? ByEndDate : null;
    }

    partial void OnReset()
    {
        ByEndDate = null;
        ByStartDate = null;
        ByTradeDateType = null;
        ByTradeStatus = null;
    }
}

