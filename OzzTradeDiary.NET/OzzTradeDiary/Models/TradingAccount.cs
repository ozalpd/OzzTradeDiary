namespace TD.Models
{
    public partial class TradingAccount
    {
        public TradingAccount()
        {
            Title = string.Empty;
            AccountCode = string.Empty;
            Trades = new HashSet<Trade>();
            Exchance = new Exchange();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string AccountCode { get; set; }
        /// <summary>
        /// The Id value of exchange record that related to the account.
        /// </summary>
        public int ExchangeId { get; set; }
        public string? Notes { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }

        /// <summary>
        /// Exchange of the account
        /// </summary>
        public Exchange Exchance { get; set; }

        public virtual ICollection<Trade> Trades { get; set; }

        /// <summary>
        /// Clones all properties in a new TradingAccount instance,
        /// except PrimaryKey(s)
        /// </summary>
        /// <returns>New TradingAccount instance</returns>
        public TradingAccount Clone()
        {
            var clone = new TradingAccount();
            clone.Title = Title;
            clone.AccountCode = AccountCode;
            clone.ExchangeId = ExchangeId;
            clone.Notes = Notes;
            clone.DisplayOrder = DisplayOrder;
            clone.IsActive = IsActive;

            return clone;
        }
    }
}
