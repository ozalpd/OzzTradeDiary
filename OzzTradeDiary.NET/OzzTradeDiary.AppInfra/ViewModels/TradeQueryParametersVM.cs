using TD.Helpers;
using TD.Models;

namespace TD.AppInfra.ViewModels
{
    public class TradeQueryParametersVM : AbstractViewModel
    {
        public TradeQueryParametersVM()
        {
            Parameters = new TradeQueryParameters();
        }

        public TradeQueryParameters Parameters { get; }

        public int? ByTradingAccountId
        {
            get => Parameters.TradingAccountId;
            set
            {
                Parameters.TradingAccountId = value;
                RaisePropertyChanged(nameof(ByTradingAccountId));
            }
        }

        public int? BySymbolId
        {
            get => Parameters.SymbolId;
            set
            {
                Parameters.SymbolId = value;
                RaisePropertyChanged(nameof(BySymbolId));
            }
        }

        public TradeDirection? ByDirection
        {
            get => Parameters.TradeDirection;
            set
            {
                Parameters.TradeDirection = value;
                RaisePropertyChanged(nameof(ByDirection));
            }
        }

        public bool? IsFullyClosed
        {
            get => Parameters.IsFullyClosed;
            set
            {
                Parameters.IsFullyClosed = value;
                RaisePropertyChanged(nameof(IsFullyClosed));
                RaisePropertyChanged(nameof(UpdatedAtFilterEnabled));
                if (value != false)
                {
                    UpdatedAtMin = null;
                    UpdatedAtMax = null;
                }
            }
        }

        /// <summary>
        /// True when <see cref="IsFullyClosed"/> is <c>false</c>, enabling the UpdatedAt date range filter.
        /// </summary>
        public bool UpdatedAtFilterEnabled => IsFullyClosed == false;

        public DateTime? EntryTimeMin
        {
            get => Parameters.EntryTimeMin;
            set
            {
                Parameters.EntryTimeMin = value;
                RaisePropertyChanged(nameof(EntryTimeMin));
            }
        }

        public DateTime? EntryTimeMax
        {
            get => Parameters.EntryTimeMax;
            set
            {
                Parameters.EntryTimeMax = value;
                RaisePropertyChanged(nameof(EntryTimeMax));
            }
        }

        public DateTime? UpdatedAtMin
        {
            get => Parameters.UpdatedAtMin;
            set
            {
                Parameters.UpdatedAtMin = value;
                RaisePropertyChanged(nameof(UpdatedAtMin));
            }
        }

        public DateTime? UpdatedAtMax
        {
            get => Parameters.UpdatedAtMax;
            set
            {
                Parameters.UpdatedAtMax = value;
                RaisePropertyChanged(nameof(UpdatedAtMax));
            }
        }

        public decimal? RealizedProfitLossMin
        {
            get => Parameters.RealizedProfitLossMin;
            set
            {
                Parameters.RealizedProfitLossMin = value;
                RaisePropertyChanged(nameof(RealizedProfitLossMin));
            }
        }

        public decimal? RealizedProfitLossMax
        {
            get => Parameters.RealizedProfitLossMax;
            set
            {
                Parameters.RealizedProfitLossMax = value;
                RaisePropertyChanged(nameof(RealizedProfitLossMax));
            }
        }

        public int Page
        {
            get => Parameters.Page;
            set
            {
                Parameters.Page = value;
                RaisePropertyChanged(nameof(Page));
            }
        }

        public int PageCount => Parameters.PageCount;

        public int PageSize
        {
            get => Parameters.PageSize;
            set
            {
                Parameters.PageSize = value;
                RaisePropertyChanged(nameof(PageSize));
            }
        }

        public long TotalCount => Parameters.TotalCount;

        public void Reset()
        {
            ByTradingAccountId = null;
            BySymbolId = null;
            ByDirection = null;
            IsFullyClosed = null;
            EntryTimeMin = null;
            EntryTimeMax = null;
            RealizedProfitLossMin = null;
            RealizedProfitLossMax = null;
        }
    }
}
