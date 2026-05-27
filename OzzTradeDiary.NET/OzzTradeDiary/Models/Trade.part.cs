using System.ComponentModel.DataAnnotations;
using TD.Extensions;
using TD.i18n;

namespace TD.Models
{
    public partial class Trade
    {
        public void CalculateFromOrders()
        {
            decimal totalExecutedValue = 0;
            decimal totalPlannedValue = 0;
            decimal totalFilledQuantity = 0;
            decimal totalOrderQuantity = 0;
            foreach (var entry in EntryOrders)
            {
                if (entry.FilledPrice.HasValue && entry.FilledQuantity.HasValue)
                {
                    totalExecutedValue += entry.FilledPrice.Value * entry.FilledQuantity.Value;
                    totalFilledQuantity += entry.FilledQuantity.Value;
                }

                if (entry.OrderPrice > 0 && entry.OrderQuantity.HasValue)
                {
                    totalPlannedValue += entry.OrderPrice * entry.OrderQuantity.Value;
                    totalOrderQuantity += entry.OrderQuantity.Value;
                }
            }

            if (totalFilledQuantity > 0)
            {
                //Some prices are less than a tenth of a cent, so we don't round prices here
                ExecutedEntryPrice = totalExecutedValue / totalFilledQuantity;
                FilledQuantity = totalFilledQuantity;
            }
            else
            {
                ExecutedEntryPrice = null;
                FilledQuantity = null;
            }

            if (totalOrderQuantity > 0)
            {
                //Some prices are less than a tenth of a cent, so we don't round prices here
                PlannedEntryPrice = totalPlannedValue / totalOrderQuantity;
                OrderQuantity = totalOrderQuantity;
            }
            else
            {
                PlannedEntryPrice = null;
                OrderQuantity = null;
            }


            totalExecutedValue = 0;
            totalPlannedValue = 0;
            totalFilledQuantity = 0;
            totalOrderQuantity = 0;
            foreach (var tpOrder in TakeProfitOrders)
            {
                if (tpOrder.FilledPrice.HasValue && tpOrder.FilledQuantity.HasValue)
                {
                    totalExecutedValue += tpOrder.FilledPrice.Value * tpOrder.FilledQuantity.Value;
                    totalFilledQuantity += tpOrder.FilledQuantity.Value;
                }

                if (tpOrder.OrderPrice > 0 && tpOrder.OrderQuantity.HasValue)
                {
                    totalPlannedValue += tpOrder.OrderPrice * tpOrder.OrderQuantity.Value;
                    totalOrderQuantity += tpOrder.OrderQuantity.Value;
                }
            }

            if (totalFilledQuantity > 0)
            {
                ExecutedTP = totalExecutedValue / totalFilledQuantity;
            }
            else
            {
                ExecutedTP = null;
            }

            if (totalOrderQuantity > 0)
            {
                PlannedTP = totalPlannedValue / totalOrderQuantity;
            }
            else
            {
                PlannedTP = null;
            }


            totalExecutedValue = 0;
            totalPlannedValue = 0;
            totalFilledQuantity = 0;
            totalOrderQuantity = 0;
            foreach (var slOrder in StopLossOrders)
            {
                if (slOrder.FilledPrice.HasValue && slOrder.FilledQuantity.HasValue)
                {
                    totalExecutedValue += slOrder.FilledPrice.Value * slOrder.FilledQuantity.Value;
                    totalFilledQuantity += slOrder.FilledQuantity.Value;
                }

                if (slOrder.OrderPrice > 0 && slOrder.OrderQuantity.HasValue)
                {
                    totalPlannedValue += slOrder.OrderPrice * slOrder.OrderQuantity.Value;
                    totalOrderQuantity += slOrder.OrderQuantity.Value;
                }
            }

            if (totalFilledQuantity > 0)
            {
                ExecutedSL = totalExecutedValue / totalFilledQuantity;
            }
            else
            {
                ExecutedSL = null;
            }

            if (totalOrderQuantity > 0)
            {
                PlannedSL = totalPlannedValue / totalOrderQuantity;
            }
            else
            {
                PlannedSL = null;
            }
        }


        /// <summary>
        /// Gets or sets the executed position value, representing the product of the executed entry price and the
        /// filled quantity for the trade.
        /// </summary>
        /// <remarks>This property is calculated based on the current values of ExecutedEntry and
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
        decimal? _orderValue;


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
        /// Gets or sets the planned position value for the trade, calculated as the product of the planned entry price
        /// and the order quantity.
        /// </summary>
        /// <remarks>This property is a calculated value based on other trade details and is not intended
        /// to be set directly in most scenarios. The setter exists primarily to support data binding requirements in UI
        /// and database operations, but setting this property does not affect the underlying calculation.</remarks>
        [Display(ResourceType = typeof(LocalizedStrings), Name = "PlannedPositionValue")]
        public decimal? PlannedPositionValue
        {
            get
            {
                if (PlannedEntryPrice.HasValue && OrderQuantity.HasValue)
                    return Math.Round(PlannedEntryPrice.Value * OrderQuantity.Value, 4);

                return null;
            }
            //This is a calculated property, so we don't want to set it directly. However, we need to have a setter to satisfy the requirements of the data binding in the UI and database.
            //The setter will simply store the value in a private field, but it won't be used in any calculations.
            set { _orderValue = value; }
        }
        decimal? _filledValue;

        /// <summary>
        /// Calculates and sets the order or filled quantity by dividing the position value by the entry price.
        /// </summary>
        /// <param name="positionValue">The total position value to convert to quantity.</param>
        /// <param name="planned">If <c>true</c>, sets <see cref="OrderQuantity"/> using <see cref="PlannedEntryPrice"/>; otherwise, sets <see
        /// cref="FilledQuantity"/> using <see cref="ExecutedEntryPrice"/>.</param>
        public void SetQuantity(decimal positionValue, bool planned = true)
        {
            if (planned && PlannedEntryPrice.HasValue)
            {
                OrderQuantity = (positionValue / PlannedEntryPrice.Value).RoundToQuantum();
            }
            else if (ExecutedEntryPrice.HasValue)
            {
                FilledQuantity = (positionValue / ExecutedEntryPrice.Value).RoundToQuantum();
            }
        }

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
        /// Gets the planned risk/reward ratio for the trade, calculated as PlannedProfitLoss divided by PlannedRiskAmount.
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
        /// Gets the realized R multiple for the trade — how many units of planned risk were actually made or lost.
        /// </summary>
        /// <remarks>A value of +2.0 means the trade returned twice the planned risk amount (2R win).
        /// A value of -1.0 means the trade lost exactly the planned risk amount (1R loss).
        /// Returns null if PlannedRiskAmount is null or zero.</remarks>
        [Display(ResourceType = typeof(LocalizedStrings), Name = "RealizedR")]
        public decimal? RealizedR
        {
            get
            {
                if (PlannedRiskAmount.HasValue && RealizedProfitLoss.HasValue && PlannedRiskAmount != 0)
                {
                    return Math.Round(RealizedProfitLoss.Value / PlannedRiskAmount.Value, 4);
                }
                return null;
            }
            //This is a calculated property, so we don't want to set it directly. However, we need to have a setter to satisfy the requirements of the data binding in the UI and database.
            set { _realizedR = value; }
        }
        decimal? _realizedR;

        [Display(ResourceType = typeof(LocalizedStrings), Name = "FilledQuantity")]
        public string RoundedFilledQuantity => FilledQuantity.ToRoundedString();

        [Display(ResourceType = typeof(LocalizedStrings), Name = "OrderQuantity")]
        public string RoundedOrderQuantity => OrderQuantity.ToRoundedString();

        [Display(ResourceType = typeof(LocalizedStrings), Name = "PlannedEntryPrice")]
        public string RoundedPlannedEntryPrice => PlannedEntryPrice.ToRoundedString();

        [Display(ResourceType = typeof(LocalizedStrings), Name = "ExecutedEntryPrice")]
        public string RoundedExecutedEntryPrice => ExecutedEntryPrice.ToRoundedString();

        [Display(ResourceType = typeof(LocalizedStrings), Name = "PlannedSL")]
        public string RoundedPlannedSL => PlannedSL.ToRoundedString();

        [Display(ResourceType = typeof(LocalizedStrings), Name = "ExecutedSL")]
        public string RoundedExecutedSL => ExecutedSL.ToRoundedString();

        [Display(ResourceType = typeof(LocalizedStrings), Name = "PlannedTP")]
        public string RoundedPlannedTP => PlannedTP.ToRoundedString();

        [Display(ResourceType = typeof(LocalizedStrings), Name = "ExecutedTP")]
        public string RoundedExecutedTP => ExecutedTP.ToRoundedString();
    }
}
