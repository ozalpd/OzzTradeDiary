namespace TD.Models
{
    public partial class Trade
    {
        /// <summary>
        /// Gets or sets the executed position value, representing the product of the executed entry price and the
        /// filled quantity for the trade.
        /// </summary>
        /// <remarks>This property is calculated based on the current values of ExecutedEntry and
        /// FilledQuantity. Setting this property does not affect the underlying calculation; the setter exists to
        /// support data binding scenarios.</remarks>
        public decimal? ExecutedPositionValue
        {
            get { return ExecutedEntry * FilledQuantity; }
            //This is a calculated property, so we don't want to set it directly. However, we need to have a setter to satisfy the requirements of the data binding in the UI and database.
            //The setter will simply store the value in a private field, but it won't be used in any calculations.
            set { _filledValue = value; }
        }

        /// <summary>
        /// Gets or sets the planned position value for the trade, calculated as the product of the planned entry price
        /// and the order quantity.
        /// </summary>
        /// <remarks>This property is a calculated value based on other trade details and is not intended
        /// to be set directly in most scenarios. The setter exists primarily to support data binding requirements in UI
        /// and database operations, but setting this property does not affect the underlying calculation.</remarks>
        public decimal? PlannedPositionValue
        {
            get { return PlannedEntry * OrderQuantity; }
            //This is a calculated property, so we don't want to set it directly. However, we need to have a setter to satisfy the requirements of the data binding in the UI and database.
            //The setter will simply store the value in a private field, but it won't be used in any calculations.
            set { _orderValue = value; }
        }

        decimal? _orderValue;
        decimal? _filledValue;
    }
}
