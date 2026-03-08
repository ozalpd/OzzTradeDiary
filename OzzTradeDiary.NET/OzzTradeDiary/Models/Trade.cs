namespace TD.Models
{
    public partial class Trade
    {
        public Trade()
        {
            EntryOrders = new HashSet<EntryOrder>();
            StopLossOrders = new HashSet<StopLossOrder>();
            TakeProfitOrders = new HashSet<TakeProfitOrder>();
            TradePlans = new HashSet<TradePlan>();
            Symbol = new Symbol();
            TradingAccount = new TradingAccount();
        }

        public int Id { get; set; }
        public int TradingAccountId { get; set; }
        public int SymbolId { get; set; }
        public Nullable<System.DateTime> EntryTime { get; set; }
        public int EntryMethod { get; set; }
        public int TradeDirection { get; set; }
        /// <summary>
        /// Planned Entry Price, calculated from EntryOrders
        /// </summary>
        public Nullable<decimal> PlannedEntry { get; set; }
        /// <summary>
        /// Executed Entry Price, calculated from EntryOrders
        /// </summary>
        public Nullable<decimal> ExecutedEntry { get; set; }
        /// <summary>
        /// Planned Take Profit Price, calculated from TakeProfitOrders
        /// </summary>
        public Nullable<decimal> PlannedTP { get; set; }
        /// <summary>
        /// Executed Take Profit Price, calculated from TakeProfitOrders
        /// </summary>
        public Nullable<decimal> ExecutedTP { get; set; }
        /// <summary>
        /// Planned Stop Loss Price, calculated from StopLossOrders
        /// </summary>
        public Nullable<decimal> PlannedSL { get; set; }
        /// <summary>
        /// Executed Stop Loss Price, calculated from StopLossOrders
        /// </summary>
        public Nullable<decimal> ExecutedSL { get; set; }

        public System.DateTime ModifyDate { get; set; }


        public virtual ICollection<EntryOrder> EntryOrders { get; set; }

        public virtual ICollection<StopLossOrder> StopLossOrders { get; set; }
        public virtual Symbol Symbol { get; set; }
        public virtual ICollection<TakeProfitOrder> TakeProfitOrders { get; set; }
        public virtual ICollection<TradePlan> TradePlans { get; set; }
        public virtual TradingAccount TradingAccount { get; set; }

        /// <summary>
        /// Clones all properties in a new Trade instance,
        /// except PrimaryKey(s)
        /// </summary>
        /// <returns>New Trade instance</returns>
        public Trade Clone()
        {
            var clone = new Trade();
            clone.TradingAccountId = TradingAccountId;
            clone.SymbolId = SymbolId;
            clone.EntryTime = EntryTime;
            clone.EntryMethod = EntryMethod;
            clone.TradeDirection = TradeDirection;
            clone.PlannedEntry = PlannedEntry;
            clone.ExecutedEntry = ExecutedEntry;
            clone.PlannedTP = PlannedTP;
            clone.ExecutedTP = ExecutedTP;
            clone.PlannedSL = PlannedSL;
            clone.ExecutedSL = ExecutedSL;
            clone.ModifyDate = ModifyDate;

            return clone;
        }
    }
}
