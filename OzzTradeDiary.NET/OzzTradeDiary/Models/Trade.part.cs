using System.ComponentModel.DataAnnotations;
using System.Transactions;
using TD.Extensions;
using TD.i18n;

namespace TD.Models
{
    public partial class Trade : IValidatableObject
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
        /// A read-only computed label that uniquely identifies this trade in lists and grids.
        /// Combines symbol ticker, direction, and entry date (falling back to trade Id when no
        /// entry time has been recorded yet). Never entered by the user.
        /// Example: "BTCUSDT · Long · 2025-01-15" or "BTCUSDT · Short · #42"
        /// </summary>
        public string Label
        {
            get
            {
                var direction = TradeDirection.GetDisplayValue();
                var status = TradeStatus.GetDisplayValue();
                var datePart = EntryTime.HasValue
                    ? EntryTime.Value.ToString("yyyy-MM-dd")
                    : $"#{Id}";
                return $"{Symbol.TickerFull} · {status} · {direction} · {datePart}";
            }
        }

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
        }

        /// <summary>
        /// Market value of the position that remains open after accounting for filled take-profit and stop-loss orders.
        /// </summary>
        /// <remarks>Calculated as (FilledQuantity - exited quantity) × ExecutedEntryPrice. Returns null
        /// if ExecutedEntryPrice or FilledQuantity is null. Returns 0 if the entire position has been exited. This
        /// calculated value is persisted to enable query operations.</remarks>
        /// <summary>
        /// Market value of the position that remains open after accounting for filled take-profit and stop-loss orders.
        /// </summary>
        /// <remarks>
        /// Calculated as (FilledQuantity - exited quantity) × ExecutedEntryPrice. Returns <c>null</c>
        /// if ExecutedEntryPrice or FilledQuantity is null. Returns 0 if the entire position has been exited.
        /// This calculated value is persisted to enable query operations.
        /// See also <see cref="RemainingOpenQuantity"/> for the quantity-only counterpart.
        /// </remarks>
        [Display(ResourceType = typeof(LocalizedStrings), Name = "RemainingPositionValue")]
        public decimal? RemainingOpenPositionValue
        {
            get
            {
                if (!ExecutedEntryPrice.HasValue || !FilledQuantity.HasValue)
                    return null;

                // Sum quantities already exited via TP and SL fills
                decimal exitedQty = TakeProfitOrders.Where(o => !o.IsCancelled)
                                                    .Sum(o => o.FilledQuantity ?? 0)
                                  + StopLossOrders.Where(o => !o.IsCancelled)
                                                  .Sum(o => o.FilledQuantity ?? 0);

                decimal remainingQty = FilledQuantity.Value - exitedQty;
                return remainingQty > 0 ? remainingQty * ExecutedEntryPrice.Value : 0;
            }
        }

        /// <summary>
        /// Filled quantity that is still open — i.e. not yet covered by any filled take-profit or stop-loss order.
        /// </summary>
        /// <remarks>
        /// Calculated as FilledQuantity minus the sum of FilledQuantity across all TP and SL orders.
        /// Returns <c>null</c> when FilledQuantity is not set. Returns 0 when the entire position has been exited.
        /// See also <see cref="RemainingOpenPositionValue"/> for the monetary counterpart,
        /// and <see cref="UnallocatedTPQuantity"/> / <see cref="UnallocatedSLQuantity"/> for plan-coverage gaps.
        /// </remarks>
        [Display(ResourceType = typeof(LocalizedStrings), Name = "RemainingQuantity")]
        public decimal? RemainingOpenQuantity => GetRemainingOpenQuantity();

        public decimal? GetRemainingOpenQuantity(decimal exception = 0)
        {
            if (!FilledQuantity.HasValue)
                return null;
            // Sum quantities already exited via TP and SL fills
            decimal exitedQty = TakeProfitOrders.Where(o => !o.IsCancelled)
                                                .Sum(o => o.FilledQuantity ?? 0)
                              + StopLossOrders.Where(o => !o.IsCancelled)
                                              .Sum(o => o.FilledQuantity ?? 0);
            decimal remainingQty = FilledQuantity.Value - (exitedQty - exception);
            return remainingQty > 0 ? remainingQty : 0;
        }

        /// <summary>
        /// Portion of the planned order quantity not yet covered by any take-profit order.
        /// </summary>
        /// <remarks>
        /// Calculated as OrderQuantity minus the sum of OrderQuantity across all TakeProfitOrders.
        /// Returns <c>null</c> when OrderQuantity is not set. A positive value means there is still
        /// planned quantity that has no corresponding TP exit order assigned.
        /// </remarks>
        [Display(ResourceType = typeof(LocalizedStrings), Name = "UnallocatedTPQuantity")]
        public decimal? UnallocatedTPQuantity
        {
            get
            {
                if (!OrderQuantity.HasValue)
                    return null;
                decimal allocated = TakeProfitOrders.Where(o => !o.IsCancelled)
                                                    .Sum(o => o.OrderQuantity ?? 0);
                decimal unallocated = OrderQuantity.Value - allocated;
                return unallocated > 0 ? unallocated : 0;
            }
        }

        /// <summary>
        /// Portion of the planned order quantity not yet covered by any stop-loss order.
        /// </summary>
        /// <remarks>
        /// Calculated as OrderQuantity minus the sum of OrderQuantity across all StopLossOrders.
        /// Returns <c>null</c> when OrderQuantity is not set. A positive value means there is still
        /// planned quantity that has no corresponding SL exit order assigned.
        /// </remarks>
        [Display(ResourceType = typeof(LocalizedStrings), Name = "UnallocatedSLQuantity")]
        public decimal? UnallocatedSLQuantity
        {
            get
            {
                if (!OrderQuantity.HasValue)
                    return null;
                decimal allocated = StopLossOrders.Where(o => !o.IsCancelled)
                                                  .Sum(o => o.OrderQuantity ?? 0);
                decimal unallocated = OrderQuantity.Value - allocated;
                return unallocated > 0 ? unallocated : 0;
            }
        }

        [Display(ResourceType = typeof(LocalizedStrings), Name = "UnallocatedSLQuantity")]
        public string RoundedUnallocatedSLQuantity => UnallocatedSLQuantity.ToRoundedString();

        [Display(ResourceType = typeof(LocalizedStrings), Name = "UnallocatedTPQuantity")]
        public string RoundedUnallocatedTPQuantity => UnallocatedTPQuantity.ToRoundedString();

        [Display(ResourceType = typeof(LocalizedStrings), Name = "RemainingQuantity")]
        public string RoundedRemainingOpenQuantity => RemainingOpenQuantity.ToRoundedString();

        [Display(ResourceType = typeof(LocalizedStrings), Name = "RemainingPositionValue")]
        public string RoundedRemainingOpenPositionValue => RemainingOpenPositionValue.ToRoundedString();

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


        /// <inheritdoc/>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Sum of TakeProfitOrder.OrderQuantity must not exceed Trade.OrderQuantity
            if (OrderQuantity > 0 && TakeProfitOrders?.Count > 0)
            {
                var tpSum = TakeProfitOrders
                    .Where(o => o.OrderQuantity.HasValue && !o.IsCancelled)
                    .Sum(o => o.OrderQuantity!.Value);

                if (tpSum > OrderQuantity!.Value)
                    yield return new ValidationResult(
                        string.Format(ErrorStrings.ValueMax,
                            LocalizedStrings.TakeProfitOrders,
                            LocalizedStrings.OrderQuantity,
                            LocalizedStrings.OrderQuantity),
                        new[] { nameof(TakeProfitOrders) });
            }

            // Sum of StopLossOrder.OrderQuantity must not exceed Trade.OrderQuantity
            if (OrderQuantity > 0 && StopLossOrders?.Count > 0)
            {
                var slSum = StopLossOrders
                    .Where(o => o.OrderQuantity.HasValue && !o.IsCancelled)
                    .Sum(o => o.OrderQuantity!.Value);

                if (slSum > OrderQuantity!.Value)
                    yield return new ValidationResult(
                        string.Format(ErrorStrings.ValueMax,
                            LocalizedStrings.StopLossOrders,
                            LocalizedStrings.OrderQuantity,
                            LocalizedStrings.OrderQuantity),
                        new[] { nameof(StopLossOrders) });
            }

            if (EntryTime.HasValue && ExitTime.HasValue && EntryTime > ExitTime)
            {
                yield return new ValidationResult(
                    string.Format(ErrorStrings.DateLessThan,
                        LocalizedStrings.EntryTime, "",
                        LocalizedStrings.ExitTime),
                    new[] { nameof(EntryTime) });
                yield return new ValidationResult(
                    string.Format(ErrorStrings.DateGreaterThan,
                        LocalizedStrings.ExitTime,
                        LocalizedStrings.EntryTime),
                    new[] { nameof(ExitTime) });
            }

            if (ExitTime.HasValue && !EntryTime.HasValue)
            {
                yield return new ValidationResult(
                                    string.Format(ErrorStrings.RequiredWhenOtherFilled, LocalizedStrings.EntryTime, LocalizedStrings.ExitTime),
                                    new[] { nameof(EntryTime) });
            }

            if (TradeStatus >= TradeStatus.Active && !EntryTime.HasValue)
            {
                yield return new ValidationResult(
                    string.Format(ErrorStrings.RequiredWhenOtherSelected, LocalizedStrings.EntryTime, TradeStatus.GetDisplayValue(), LocalizedStrings.TradeStatus),
                    new[] { nameof(EntryTime), nameof(TradeStatus) });
            }

            if (TradeStatus < TradeStatus.Active && EntryTime.HasValue)
            {
                yield return new ValidationResult(string.Format(ErrorStrings.MinSelectionWhenOtherFilled,
                                                                LocalizedStrings.EntryTime, LocalizedStrings.TradeStatus, TransactionStatus.Active.GetDisplayValue()),
                                                   new[] { nameof(TradeStatus) });
            }
        }
    }
}