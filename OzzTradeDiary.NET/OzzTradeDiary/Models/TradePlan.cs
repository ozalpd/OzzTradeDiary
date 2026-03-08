namespace TD.Models
{
    public partial class TradePlan
    {
        public int Id { get; set; }
        public int? TradeId { get; set; }

        /// <summary>
        /// Web URL or local file path of the image
        /// </summary>
        public string? ImageURL { get; set; }

        public string? Notes { get; set; }
        public System.DateTime ModifyDate { get; set; }
    
        public virtual Trade? Trade { get; set; }

        /// <summary>
        /// Clones all properties in a new TradePlan instance,
        /// except PrimaryKey(s)
        /// </summary>
        /// <returns>New TradePlan instance</returns>
        public TradePlan Clone()
        {
            var clone = new TradePlan();
            clone.TradeId = TradeId;
            clone.ImageURL = ImageURL;
            clone.Notes = Notes;
            clone.ModifyDate = ModifyDate;
    
            return clone;
        }
    }
}
