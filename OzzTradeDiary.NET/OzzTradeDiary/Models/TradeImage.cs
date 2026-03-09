namespace TD.Models
{
    public partial class TradeImage
    {
        public int Id { get; set; }
        public int? TradeId { get; set; }

        /// <summary>
        /// Web URL or local file path of the image (typically a screenshot)
        /// </summary>
        public string? ImageURL { get; set; }

        public string? Notes { get; set; }
        public System.DateTime ModifyDate { get; set; }
    
        public virtual Trade? Trade { get; set; }

        /// <summary>
        /// Clones all properties in a new TradeImage instance,
        /// except PrimaryKey(s)
        /// </summary>
        /// <returns>New TradeImage instance</returns>
        public TradeImage Clone()
        {
            var clone = new TradeImage();
            clone.TradeId = TradeId;
            clone.ImageURL = ImageURL;
            clone.Notes = Notes;
            clone.ModifyDate = ModifyDate;
    
            return clone;
        }
    }
}
