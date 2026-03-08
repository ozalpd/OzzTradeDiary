namespace TD.Models
{
    public partial class StopLossOrder
    {
        public int Id { get; set; }
        public int TradeId { get; set; }
        /// <summary>
        /// Indicates that all remaining quantity of the trade was or will be closed
        /// </summary>
        public bool StopAll { get; set; }
        public Nullable<System.DateTime> ExecuteTime { get; set; }
        /// <summary>
        /// Planned Stop Loss Price
        /// </summary>
        public decimal OrderPrice { get; set; }
        /// <summary>
        /// Executed Stop Loss Price
        /// </summary>
        public Nullable<decimal> FilledPrice { get; set; }
        /// <summary>
        /// Planned contract quantity of order
        /// </summary>
        public Nullable<decimal> OrderQuantity { get; set; }
        /// <summary>
        /// Realized contract quantity of order
        /// </summary>
        public Nullable<decimal> FilledQuantity { get; set; }
        /// <summary>
        /// Planned amount in currency, like $100
        /// </summary>
        public Nullable<decimal> OrderAmount { get; set; }
        /// <summary>
        /// Filled contract amount in currency, like $100
        /// </summary>
        public Nullable<decimal> FilledAmount { get; set; }
        public int DisplayOrder { get; set; }
    
        public virtual Trade? Trade { get; set; }
        /// <summary>
        /// Clones all properties in a new StopLossOrder instance,
        /// except PrimaryKey(s)
        /// </summary>
        /// <returns>New StopLossOrder instance</returns>
        public StopLossOrder Clone()
        {
            var clone = new StopLossOrder();
            clone.TradeId = TradeId;
            clone.StopAll = StopAll;
            clone.ExecuteTime = ExecuteTime;
            clone.OrderPrice = OrderPrice;
            clone.FilledPrice = FilledPrice;
            clone.OrderQuantity = OrderQuantity;
            clone.FilledQuantity = FilledQuantity;
            clone.OrderAmount = OrderAmount;
            clone.FilledAmount = FilledAmount;
            clone.DisplayOrder = DisplayOrder;
    
            return clone;
        }
    }
}
