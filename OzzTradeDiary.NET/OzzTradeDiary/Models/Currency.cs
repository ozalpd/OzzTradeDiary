namespace TD.Models
{
    public partial class Currency
    {
        public Currency()
        {
            CurrencyTicker = string.Empty;
            Description = string.Empty;
        }
        public int Id { get; set; }
        /// <summary>
        /// Ticker or short code of the currency
        /// </summary>
        public string CurrencyTicker { get; set; }
        /// <summary>
        /// Description for the currency
        /// </summary>
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }

        /// <summary>
        /// Clones all properties in a new Currency instance,
        /// except PrimaryKey(s)
        /// </summary>
        /// <returns>New Currency instance</returns>
        public Currency Clone()
        {
            var clone = new Currency()
            {
                CurrencyTicker = CurrencyTicker,
                Description = Description,
                DisplayOrder = DisplayOrder,
                IsActive = IsActive
            };
            return clone;
        }
    }
}
