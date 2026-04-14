namespace TD.Helpers
{
    public partial class TradeQueryParameters
    {
        public decimal? ExecutedPositionValueMax { get; set; }
        public decimal? ExecutedPositionValueMin { get; set; }
        public decimal? PlannedPositionValueMax { get; set; }
        public decimal? PlannedPositionValueMin { get; set; }

        partial void OnHasAnySearchCriteria()
        {
            _hasAnySearchCriteria = _hasAnySearchCriteria || ExecutedPositionValueMin.HasValue || ExecutedPositionValueMax.HasValue;
        }
    }
}
