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
            get { return ExecutedEntryPrice * FilledQuantity; }
            //This is a calculated property, so we don't want to set it directly. However, we need to have a setter to satisfy the requirements of the data binding in the UI and database.
            //The setter will simply store the value in a private field, but it won't be used in any calculations.
            set { _filledValue = value; }
        }
        decimal? _orderValue;

        /// <summary>
        /// Gets or sets the planned position value for the trade, calculated as the product of the planned entry price
        /// and the order quantity.
        /// </summary>
        /// <remarks>This property is a calculated value based on other trade details and is not intended
        /// to be set directly in most scenarios. The setter exists primarily to support data binding requirements in UI
        /// and database operations, but setting this property does not affect the underlying calculation.</remarks>
        public decimal? PlannedPositionValue
        {
            get { return PlannedEntryPrice * OrderQuantity; }
            //This is a calculated property, so we don't want to set it directly. However, we need to have a setter to satisfy the requirements of the data binding in the UI and database.
            //The setter will simply store the value in a private field, but it won't be used in any calculations.
            set { _orderValue = value; }
        }
        decimal? _filledValue;

        /// <summary>
        /// Gets or sets the planned profit or loss for the trade based on the planned entry price, take profit price,
        /// and order quantity.
        /// </summary>
        /// <remarks>For long positions, the planned profit or loss is calculated as (PlannedTP -
        /// PlannedEntryPrice) multiplied by OrderQuantity. For short positions, it is calculated as (PlannedEntryPrice
        /// - PlannedTP) multiplied by OrderQuantity. If either PlannedEntryPrice or PlannedTP is not specified, the
        /// value is null. The setter exists to support data binding scenarios but does not affect the calculated
        /// value.</remarks>
        public decimal? PlannedProfitLoss
        {
            get
            {
                if (PlannedEntryPrice.HasValue && PlannedTP.HasValue)
                {
                    // For long positions, profit is made when the price goes up, and loss is made when the price goes down.
                    // For short positions, profit is made when the price goes down, and loss is made when the price goes up.
                    if (TradeDirection == TradeDirection.Long)
                    {
                        return (PlannedTP.Value - PlannedEntryPrice.Value) * (OrderQuantity ?? 0);
                    }
                    else // TradeDirection.Short
                    {
                        return (PlannedEntryPrice.Value - PlannedTP.Value) * (OrderQuantity ?? 0);
                    }
                }
                return null;
            }
            //This is a calculated property, so we don't want to set it directly. However, we need to have a setter to satisfy the requirements of the data binding in the UI and database.
            //The setter will simply store the value in a private field, but it won't be used in any calculations.
            set { _plannedProfitLoss = value; }
        }
        decimal? _plannedProfitLoss;

        /// <summary>
        /// Gets or sets the realized profit or loss for the trade based on executed entry and exit prices.
        /// </summary>
        /// <remarks>The realized profit or loss is calculated using the executed entry price and either
        /// the executed take profit or stop loss price, depending on which is available. For long positions, profit is
        /// realized when the exit price is higher than the entry price; for short positions, when the exit price is
        /// lower. If neither executed take profit nor stop loss is available, the value is null. The setter exists to
        /// support data binding scenarios and does not affect the calculated value.</remarks>
        public decimal? RealizedProfitLoss
        {
            get
            {
                if (ExecutedEntryPrice.HasValue && (ExecutedTP.HasValue || ExecutedSL.HasValue))
                {
                    decimal profitLossIfTP = ExecutedTP.HasValue ? (ExecutedTP.Value - ExecutedEntryPrice.Value) * (FilledQuantity ?? 0) : 0;
                    decimal profitLossIfSL = ExecutedSL.HasValue ? (ExecutedSL.Value - ExecutedEntryPrice.Value) * (FilledQuantity ?? 0) : 0;

                    // For long positions, profit is made when the price goes up, and loss is made when the price goes down.
                    // For short positions, profit is made when the price goes down, and loss is made when the price goes up.
                    if (TradeDirection == TradeDirection.Long)
                    {
                        return profitLossIfTP > 0 ? profitLossIfTP : profitLossIfSL;
                    }
                    else // TradeDirection.Short
                    {
                        return profitLossIfTP < 0 ? profitLossIfTP : profitLossIfSL;
                    }
                }
                return null;
            }
            //This is a calculated property, so we don't want to set it directly. However, we need to have a setter to satisfy the requirements of the data binding in the UI and database.
            //The setter will simply store the value in a private field, but it won't be used in any calculations.
            set { _realizedProfitLoss = value; }
        }
        decimal? _realizedProfitLoss;

        /// <summary>
        /// Gets or sets the planned risk amount for the trade, calculated based on the planned entry price, 
        /// planned stop loss price, and order quantity.
        /// </summary> <remarks>This property is a calculated value that represents the potential loss if the trade hits the stop loss 
        /// level. For long positions, it calculates the risk as the amount lost if the price goes down to the stop loss level. 
        /// For short positions, it calculates the risk as the amount lost if the price goes up to the stop loss level. 
        /// The setter exists primarily to support data binding requirements in UI and database operations, 
        /// but setting this property does not affect the underlying calculation.</remarks>      
        public decimal? PlannedRiskAmount
        {
            get
            {
                if (PlannedEntryPrice.HasValue && PlannedSL.HasValue)
                {
                    // For long positions, risk is the amount lost if the price goes down to the stop loss level.
                    // For short positions, risk is the amount lost if the price goes up to the stop loss level.
                    if (TradeDirection == TradeDirection.Long)
                    {
                        return (PlannedEntryPrice.Value - PlannedSL.Value) * (OrderQuantity ?? 0);
                    }
                    else // TradeDirection.Short
                    {
                        return (PlannedSL.Value - PlannedEntryPrice.Value) * (OrderQuantity ?? 0);
                    }
                }
                return null;
            }
            //This is a calculated property, so we don't want to set it directly. However, we need to have a setter to satisfy the requirements of the data binding in the UI and database.
            //The setter will simply store the value in a private field, but it won't be used in any calculations.
            set { _plannedRiskAmount = value; }
        }
        decimal? _plannedRiskAmount;

    }
}
