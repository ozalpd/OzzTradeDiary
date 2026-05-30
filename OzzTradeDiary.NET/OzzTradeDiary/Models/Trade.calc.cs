using System.ComponentModel.DataAnnotations;
using TD.i18n;
using TD.Validation;

//----------------------------------------------------------------------------------
// Custom calculated properties for Trade, these properties are also persisted for
// use in SQL queries. They have dummy setters to satisfy data binding requirements
// in the UI and database, but the actual values are calculated based on other
// properties and are not set directly.
//----------------------------------------------------------------------------------
namespace TD.Models;

public partial class Trade
{
    /// <summary>
    /// Gets or sets the executed position value, representing the product of the executed entry price and the
    /// filled quantity for the trade.
    /// </summary>
    /// <remarks>This property is calculated based on the current values of ExecutedEntryPrice and
    /// FilledQuantity. Setting this property does not affect the underlying calculation; the setter exists to
    /// support data binding scenarios.</remarks>
    [Display(ResourceType = typeof(LocalizedStrings), Name = "ExecutedPositionValue")]
    public decimal? ExecutedPositionValue
    {
        get
        {
            if (ExecutedEntryPrice.HasValue && FilledQuantity.HasValue)
                return Math.Round(ExecutedEntryPrice.Value * FilledQuantity.Value, 4);

            return null;
        }
        //This is a calculated property, so we don't want to set it directly. However, we need to have a setter to satisfy the requirements of the data binding in the UI and database.
        //The setter will simply store the value in a private field, but it won't be used in any calculations.
        set { _filledValue = value; }
    }
    decimal? _filledValue;

    /// <summary>
    /// Gets or sets a value indicating whether the trade position is fully closed.
    /// </summary>
    /// <remarks>This is a calculated property based on <see cref="ExitTime"/>, <see cref="FilledQuantity"/>, and <see
    /// cref="OrderQuantity"/>. The setter is provided for data binding and persistence compatibility but does not affect
    /// calculations.</remarks>
    [Display(ResourceType = typeof(LocalizedStrings), Name = "IsFullyClosed")]
    public bool IsFullyClosed
    {
        get
        {
            // A trade is considered fully closed if it has an exit time and the filled quantity is equal to the order quantity.
            return ExitTime.HasValue && FilledQuantity.HasValue && OrderQuantity.HasValue && FilledQuantity.Value >= OrderQuantity.Value;
        }
        //This is a calculated property, so we don't want to set it directly. However, we need to have a setter to satisfy the requirements of the data binding in the UI and database.
        //The setter will simply store the value in a private field, but it won't be used in any calculations.
        set { _isFullyClosed = value; }
    }
    bool _isFullyClosed;

    /// <summary>
    /// Gets or sets the net profit or loss after deducting all trading fees and funding fees from the realized
    /// profit/loss.
    /// </summary>
    /// <remarks>This is a calculated property based on <see cref="RealizedProfitLoss"/>, <see cref="FundingFeeTotal"/>,
    /// and <see cref="TotalFeesCorrected"/> or <see cref="TotalFeesCalculated"/>. The setter is provided for
    /// data binding and persistence compatibility but does not affect calculations.</remarks>
    [Display(ResourceType = typeof(LocalizedStrings), Name = "NetProfitLoss")]
    public decimal? NetProfitLoss
    {
        get { return RealizedProfitLoss - (FundingFeeTotal ?? 0) - (TotalFeesCorrected ?? TotalFeesCalculated ?? 0); }
        set { _netProfitLoss = value; }
    }
    decimal? _netProfitLoss;

    /// <summary>
    /// Gets or sets the planned position value for the trade, calculated as the product of the planned entry price
    /// and the order quantity.
    /// </summary>
    /// <remarks>This property is a calculated value based on other trade details and is not intended
    /// to be set directly in most scenarios. The setter exists primarily to support data binding requirements in UI
    /// and database operations, but setting this property does not affect the underlying calculation.</remarks>
    [GreaterThan(0)]
    [Display(ResourceType = typeof(LocalizedStrings), Name = "PlannedPositionValue")]
    public decimal? PlannedPositionValue
    {
        get
        {
            if (PlannedEntryPrice > 0 && OrderQuantity > 0)
                return PlannedEntryPrice.Value * OrderQuantity.Value;

            return null;
        }
        //This is a calculated property, so we don't want to set it directly. However, we need to have a setter to satisfy the requirements of the data binding in the UI and database.
        //The setter will simply store the value in a private field, but it won't be used in any calculations.
        set { _orderValue = value; }
    }
    decimal? _orderValue;

    /// <summary>
    /// Gets or sets the planned profit or loss for the trade based on the planned entry price, take profit price,
    /// and order quantity.
    /// </summary>
    /// <remarks>For long positions, the planned profit or loss is calculated as (PlannedTP -
    /// PlannedEntryPrice) multiplied by OrderQuantity. For short positions, it is calculated as (PlannedEntryPrice
    /// - PlannedTP) multiplied by OrderQuantity. If either PlannedEntryPrice or PlannedTP is not specified, the
    /// value is null. The setter exists to support data binding scenarios but does not affect the calculated
    /// value.</remarks>
    [Display(ResourceType = typeof(LocalizedStrings), Name = "PlannedProfit")]
    public decimal? PlannedProfit
    {
        get
        {
            if (PlannedEntryPrice.HasValue && PlannedTP.HasValue)
            {
                if (!OrderQuantity.HasValue)
                    return null;

                // Long profits when price rises, Short profits when price falls.
                // directionMultiplier normalises both into: positive = profit, negative = loss.
                decimal directionMultiplier = TradeDirection == TradeDirection.Long ? 1m : -1m;
                return Math.Round((PlannedTP.Value - PlannedEntryPrice.Value) * directionMultiplier * OrderQuantity.Value, 4);
            }
            return null;
        }
        //This is a calculated property, so we don't want to set it directly. However, we need to have a setter to satisfy the requirements of the data binding in the UI and database.
        //The setter will simply store the value in a private field, but it won't be used in any calculations.
        set { _plannedProfit = value; }
    }
    decimal? _plannedProfit;

    /// <summary>
    /// Gets or sets the planned risk amount for the trade, calculated based on the planned entry price, 
    /// planned stop loss price, and order quantity.
    /// </summary> <remarks>This property is a calculated value that represents the potential loss if the trade hits the stop loss 
    /// level. For long positions, it calculates the risk as the amount lost if the price goes down to the stop loss level. 
    /// For short positions, it calculates the risk as the amount lost if the price goes up to the stop loss level. 
    /// The setter exists primarily to support data binding requirements in UI and database operations, 
    /// but setting this property does not affect the underlying calculation.</remarks>      
    [Display(ResourceType = typeof(LocalizedStrings), Name = "PlannedRiskAmount")]
    public decimal? PlannedRiskAmount
    {
        get
        {
            if (PlannedEntryPrice.HasValue && PlannedSL.HasValue && OrderQuantity.HasValue)
            {
                decimal riskPerUnit = TradeDirection == TradeDirection.Long
                    ? PlannedEntryPrice.Value - PlannedSL.Value   // Long: SL is below entry
                    : PlannedSL.Value - PlannedEntryPrice.Value;  // Short: SL is above entry

                // Return null for invalid SL placement (SL on wrong side of entry)
                if (riskPerUnit <= 0)
                    return null;

                return Math.Round(riskPerUnit * OrderQuantity.Value, 4);
            }
            return null;
        }
        //This is a calculated property, so we don't want to set it directly. However, we need to have a setter to satisfy the requirements of the data binding in the UI and database.
        //The setter will simply store the value in a private field, but it won't be used in any calculations.
        set { _plannedRiskAmount = value; }
    }
    decimal? _plannedRiskAmount;

    /// <summary>
    /// Gets the planned risk/reward ratio for the trade, calculated as PlannedProfit divided by PlannedRiskAmount.
    /// </summary>
    /// <remarks>A ratio of 2.0 means the planned reward is twice the planned risk (2:1 R/R).
    /// Returns null if PlannedRiskAmount is null or zero.</remarks>
    [Display(ResourceType = typeof(LocalizedStrings), Name = "PlannedRiskRewardRatio")]
    public decimal? PlannedRiskRewardRatio
    {
        get
        {
            if (PlannedRiskAmount.HasValue && PlannedRiskAmount != 0)
            {
                return PlannedProfit / PlannedRiskAmount;
            }
            return null;
        }
        //This is a calculated property, so we don't want to set it directly. However, we need to have a setter to satisfy the requirements of the data binding in the UI and database.
        set { _plannedRiskRewardRatio = value; }
    }
    decimal? _plannedRiskRewardRatio;

    /// <summary>
    /// Market value of the position that remains open after accounting for filled take-profit and stop-loss orders.
    /// </summary>
    /// <remarks>Calculated as (FilledQuantity - exited quantity) × ExecutedEntryPrice. Returns null
    /// if ExecutedEntryPrice or FilledQuantity is null. Returns 0 if the entire position has been exited. This
    /// calculated value is persisted to enable query operations.</remarks>
    public decimal? RemainingPositionValue
    {
        get
        {
            if (!ExecutedEntryPrice.HasValue || !FilledQuantity.HasValue)
                return null;

            // Sum quantities already exited via TP and SL fills
            decimal exitedQty = TakeProfitOrders.Sum(o => o.FilledQuantity ?? 0)
                              + StopLossOrders.Sum(o => o.FilledQuantity ?? 0);

            decimal remainingQty = FilledQuantity.Value - exitedQty;
            return remainingQty > 0 ? remainingQty * ExecutedEntryPrice.Value : 0;
        }
        set { _remainingPositionValue = value; }
    }
    decimal? _remainingPositionValue;

    /// <summary>
    /// Gets or sets the realized profit or loss for the trade based on executed entry and exit prices.
    /// </summary>
    /// <remarks>The realized profit or loss is calculated using the executed entry price and either
    /// the executed take profit or stop loss price, depending on which is available. For long positions, profit is
    /// realized when the exit price is higher than the entry price; for short positions, when the exit price is
    /// lower. If neither executed take profit nor stop loss is available, the value is null. The setter exists to
    /// support data binding scenarios and does not affect the calculated value.</remarks>
    [Display(ResourceType = typeof(LocalizedStrings), Name = "RealizedProfitLoss")]
    public decimal? RealizedProfitLoss
    {
        get
        {
            if (ExecutedEntryPrice.HasValue && FilledQuantity.HasValue && (ExecutedTP.HasValue || ExecutedSL.HasValue))
            {
                // Prefer ExecutedTP as the exit price; fall back to ExecutedSL.
                decimal exitPrice = ExecutedTP ?? ExecutedSL!.Value;

                // Long profits when price rises, Short profits when price falls.
                // directionMultiplier normalises both into: positive = profit, negative = loss.
                decimal directionMultiplier = TradeDirection == TradeDirection.Long ? 1m : -1m;
                return Math.Round((exitPrice - ExecutedEntryPrice.Value) * directionMultiplier * FilledQuantity.Value, 4);
            }
            return null;
        }
        //This is a calculated property, so we don't want to set it directly. However, we need to have a setter to satisfy the requirements of the data binding in the UI and database.
        //The setter will simply store the value in a private field, but it won't be used in any calculations.
        set { _realizedProfitLoss = value; }
    }
    decimal? _realizedProfitLoss;

    /// <summary>
    /// Realized risk amount based on executed entry price and executed exit price (TP or SL), representing the actual amount won or lost on the trade.
    /// </summary>
    [Display(ResourceType = typeof(LocalizedStrings), Name = "RealizedRiskAmount")]
    public decimal? RealizedRiskAmount
    {
        get
        {
            if (ExecutedEntryPrice.HasValue && (ExecutedTP.HasValue || ExecutedSL.HasValue))
            {
                decimal riskIfTP = ExecutedTP.HasValue ? (ExecutedTP.Value - ExecutedEntryPrice.Value) * (FilledQuantity ?? 0) : 0;
                decimal riskIfSL = ExecutedSL.HasValue ? (ExecutedSL.Value - ExecutedEntryPrice.Value) * (FilledQuantity ?? 0) : 0;
                // For long positions, risk is the amount lost if the price goes down, and for short positions, risk is the amount lost if the price goes up.
                if (TradeDirection == TradeDirection.Long)
                {
                    return riskIfTP < 0 ? riskIfTP : riskIfSL;
                }
                else // TradeDirection.Short
                {
                    return riskIfTP > 0 ? riskIfTP : riskIfSL;
                }
            }
            return null;

        }
        //This is a calculated property, so we don't want to set it directly. However, we need to have a setter to satisfy the requirements of the data binding in the UI and database.
        set { _realizedRiskAmount = value; }
    }
    decimal? _realizedRiskAmount;

    [Display(ResourceType = typeof(LocalizedStrings), Name = "TotalFeesCalculated")]
    public decimal? TotalFeesCalculated
    {
        get
        {
            if (TotalFeesCorrected.HasValue)
            {
                return TotalFeesCorrected;
            }
            else
            {
                decimal totalFees = 0;
                if (EntryOrders != null)
                {
                    foreach (var order in EntryOrders)
                    {
                        totalFees += order.FeesCalculated ?? 0;
                    }
                }
                if (TakeProfitOrders != null)
                {
                    foreach (var order in TakeProfitOrders)
                    {
                        totalFees += order.FeesCalculated ?? 0;
                    }
                }
                if (StopLossOrders != null)
                {
                    foreach (var order in StopLossOrders)
                    {
                        totalFees += order.FeesCalculated ?? 0;
                    }
                }
                return totalFees;
            }
        }
        set { _totalFeesCalculated = value; }
    }
    decimal? _totalFeesCalculated;
}
