using System.ComponentModel.DataAnnotations;
using TD.Extensions;
using TD.i18n;

namespace TD.Models
{
    public partial class Trade
    {
        /// <summary>
        /// Calculates planned and executed entry prices, take-profit levels, stop-loss levels, and quantities by
        /// aggregating data from associated entry, take-profit, and stop-loss orders.
        /// </summary>
        /// <remarks>Must be called after navigation collections (EntryOrders, TakeProfitOrders,
        /// StopLossOrders) are loaded to ensure accurate calculations. Prices are computed as weighted averages and are
        /// not rounded to preserve precision for assets with very small unit prices.</remarks>
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

        //----------------------------------------------------------------------------------
        // Below properties are not persisted, but are calculated for display purposes
        // based on executed prices and filled quantity. Some of them's names are starts
        // with Rounded their main purpose is to provide rounded values for display in the
        // UI, but they are not rounded in calculations to preserve precision for assets
        // with very small unit prices.
        //----------------------------------------------------------------------------------


        public bool IsActiveOrWaiting => IsWaiting || TradeStatus == TradeStatus.Active;

        public bool IsMissedOrCancelled => TradeStatus <= TradeStatus.Missed;

        public bool IsWaiting => TradeStatus == TradeStatus.Planned || TradeStatus == TradeStatus.Pending;

        public bool IsWinning => RealizedProfitLoss.HasValue && RealizedProfitLoss.Value > 0;

        /// <summary>
        /// Gets the realized R multiple for the trade — how many units of planned risk were actually made or lost.
        /// </summary>
        /// <remarks>A value of +2.0 means the trade returned twice the planned risk amount (2R win).
        /// A value of -1.0 means the trade lost exactly the planned risk amount (1R loss).
        /// Returns null if PlannedRiskAmount is null or zero.</remarks>
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
        }

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
