namespace TD.Models
{
    public partial class Exchange
    {
        public Exchange()
        {
            Symbols = new HashSet<Symbol>();
            TradingAccounts = new HashSet<TradingAccount>();
            ExchangeName = string.Empty;
            ExchangeCode = string.Empty;
        }

        public int Id { get; set; }
        public string ExchangeName { get; set; }
        public string ExchangeCode { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }


        public virtual ICollection<Symbol> Symbols { get; set; }

        public virtual ICollection<TradingAccount> TradingAccounts { get; set; }
        /// <summary>
        /// Clones all properties in a new Exchange instance,
        /// except PrimaryKey(s)
        /// </summary>
        /// <returns>New Exchange instance</returns>
        public Exchange Clone()
        {
            var clone = new Exchange();
            clone.ExchangeName = ExchangeName;
            clone.ExchangeCode = ExchangeCode;
            clone.DisplayOrder = DisplayOrder;
            clone.IsActive = IsActive;

            return clone;
        }
    }
}
