namespace TD.Models
{
    public partial class EntryOrder
    {
        public EntryOrder()
        {
            Trade = new Trade();
        }

        public int Id { get; set; }
        public int TradeId { get; set; }
        public OrderType OrderType { get; set; }
        public Nullable<System.DateTime> ExecuteTime { get; set; }
        /// <summary>
        /// Planned Entry Price
        /// </summary>
        public decimal OrderPrice { get; set; }
        /// <summary>
        /// Executed Entry Price
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

        public virtual Trade Trade { get; set; }

        /// <summary>
        /// Clones all properties in a new EntryOrder instance,
        /// except PrimaryKey(s)
        /// </summary>
        /// <returns>New EntryOrder instance</returns>
        public EntryOrder Clone()
        {
            var clone = new EntryOrder();
            clone.TradeId = TradeId;
            clone.OrderType = OrderType;
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
