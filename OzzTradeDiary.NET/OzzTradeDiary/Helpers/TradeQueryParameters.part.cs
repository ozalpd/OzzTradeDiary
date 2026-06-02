using TD.Models;

namespace TD.Helpers
{
    public partial class TradeQueryParameters
    {
        public TradeStatusQuery? TradeStatus { get; set; }
        public TradeDateType? TradeDateType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
