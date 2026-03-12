namespace TD.Models
{
    public partial class Symbol
    {
        public Symbol()
        {
            Trades = new HashSet<Trade>();
            Ticker = string.Empty;
            TickerFull = string.Empty;
            BaseCurrency = string.Empty;
            PriceCurrency = string.Empty;
            Description = string.Empty;
        }
    
        public int Id { get; set; }
        /// <summary>
        /// Symbol name without exchange prefix, e.g. 'BTCUSDT'
        /// </summary>
        public string Ticker { get; set; }
        /// <summary>
        /// Symbol name with exchange prefix, e.g. 'BYBIT:BTCUSD.P'
        /// </summary>
        public string TickerFull { get; set; }
        /// <summary>
        /// Contains a string (CurrencyTicker) that representing the symbol's base currency if the instrument is a Crypto pair or a Forex pair or a derivative based on such a pair.
        /// For example, this property holds "GBP" for "GBPJPY", "BTC" for "BTCUSDT" and "" for "NASDAQ:MSFT".
        /// </summary>
        public string BaseCurrency { get; set; }
        /// <summary>
        /// Contains a string (CurrencyTicker) that representing currency of the symbol's price.
        /// For example, this property holds "JPY" for "GBPJPY", "USDT" for "BTCUSDT" and "USD" for "NASDAQ:MSFT".
        /// </summary>
        public string PriceCurrency { get; set; }
        /// <summary>
        /// Description of the symbol
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The Id value of exchange record that related to the symbol
        /// </summary>
        public int ExchangeId { get; set; }
        public MarketType MarketType { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
    
        /// <summary>
        /// Exchange of the symbol
        /// </summary>
        public virtual Exchange? Exchange { get; set; }
       
        public virtual ICollection<Trade> Trades { get; set; }
        /// <summary>
        /// Clones all properties in a new Symbol instance,
        /// except PrimaryKey(s)
        /// </summary>
        /// <returns>New Symbol instance</returns>
        public Symbol Clone()
        {
            var clone = new Symbol();
            clone.Ticker = Ticker;
            clone.TickerFull = TickerFull;
            clone.BaseCurrency = BaseCurrency;
            clone.PriceCurrency = PriceCurrency;
            clone.Description = Description;
            clone.ExchangeId = ExchangeId;
            clone.MarketType = MarketType;
            clone.DisplayOrder = DisplayOrder;
            clone.IsActive = IsActive;
    
            return clone;
        }
    }
}
