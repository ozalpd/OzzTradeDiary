namespace TD.Models
{
    public partial class Trade
    {
        public decimal? ExecutedPositionValue => ExecutedEntry * FilledQuantity;
        public decimal? PlannedPositionValue => PlannedEntry * OrderQuantity;

    }
}
