using System.ComponentModel.DataAnnotations;
using TD.i18n;

namespace TD.Models
{
    public partial class TakeProfitOrder : IValidatableObject
    {
        /// <summary>
        /// Gets or sets the filled value for the trade, representing the total value of the filled quantity at the
        /// filled price.
        /// </summary>
        /// <remarks>This property is typically calculated as the product of FilledQuantity and
        /// FilledPrice. The setter exists to support data binding scenarios and does not affect the calculated
        /// value.</remarks>
        [Display(ResourceType = typeof(LocalizedStrings), Name = "FilledValue")]
        public decimal? FilledValue
        {
            get => FilledQuantity * FilledPrice;
            //This is a calculated property, so we don't want to set it directly. However, we need to have a setter to satisfy the requirements of the data binding in the UI and database.
            //The setter will simply store the value in a private field, but it won't be used in any calculations.
            set { _filledValue = value; }
        }
        decimal? _filledValue;

        /// <summary>
        /// Gets or sets the calculated order value for the trade.
        /// </summary>
        /// <remarks>The order value is computed as the product of the order quantity and order price. The
        /// setter exists to support data binding scenarios but does not affect the calculation of the value.</remarks>
        [Display(ResourceType = typeof(LocalizedStrings), Name = "OrderValue")]
        public decimal? OrderValue
        {
            get => OrderQuantity * OrderPrice;
            //This is a calculated property, so we don't want to set it directly. However, we need to have a setter to satisfy the requirements of the data binding in the UI and database.
            //The setter will simply store the value in a private field, but it won't be used in any calculations.
            set { _orderValue = value; }
        }
        decimal? _orderValue;

        /// <summary>
        /// Gets or sets the calculated fees for the trade.
        /// </summary>
        /// <remarks>The fees are computed based on the trade details. The setter exists to support data binding scenarios but does not affect the calculation of the fees.</remarks>
        public decimal? FeesCalculated => CalculateFees();

        private decimal? CalculateFees()
        {
            var account = Trade?.TradingAccount;
            // Implement the logic to calculate fees based on trade details
            return null; // Placeholder
        }


        /// <summary>
        /// Gets the planned profit amount for this specific take profit Filled,
        /// calculated from the trade's entry price, this Filled's price and quantity.
        /// </summary>
        public decimal? FilledProfitAmount
        {
            get
            {
                if (Trade == null || FilledPrice <= 0)
                    return null;

                var entryPrice = Trade.ExecutedEntryPrice ?? Trade.PlannedEntryPrice;
                var quantity = FilledQuantity;

                if (entryPrice == null || quantity == null || quantity <= 0)
                    return null;

                return Trade.TradeDirection == TradeDirection.Long
                    ? (FilledPrice - entryPrice.Value) * quantity.Value
                    : (entryPrice.Value - FilledPrice) * quantity.Value;
            }
        }

        public bool IsCancelled => CancellationTime.HasValue;

        /// <summary>
        /// Gets the planned profit amount for this specific take profit order,
        /// calculated from the trade's entry price, this order's price and quantity.
        /// </summary>
        public decimal? OrderProfitAmount
        {
            get
            {
                if (Trade == null || OrderPrice <= 0)
                    return null;

                var entryPrice = Trade.ExecutedEntryPrice ?? Trade.PlannedEntryPrice;
                var quantity = OrderQuantity;

                if (entryPrice == null || quantity == null || quantity <= 0)
                    return null;

                return Trade.TradeDirection == TradeDirection.Long
                    ? (OrderPrice - entryPrice.Value) * quantity.Value
                    : (entryPrice.Value - OrderPrice) * quantity.Value;
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (FilledQuantity.HasValue)
            {
                if (FilledPrice == null || FilledPrice <= 0)
                {
                    yield return new ValidationResult(
                        ErrorStrings.FilledPriceWhenFilledQuantityHasValue,
                        new[] { nameof(FilledPrice) });
                }

                var filledQ = FilledQuantity.Value;
                var remExceptThis = Trade.GetRemainingOpenQuantity(filledQ) ?? 0;
                if (filledQ > remExceptThis)
                    yield return new ValidationResult(
                        string.Format(ErrorStrings.FilledQuantityExceedsRemaining, remExceptThis),
                        new[] { nameof(FilledQuantity) });
            }

            if (FilledPrice.HasValue && (FilledQuantity == null || FilledQuantity <= 0))
            {
                yield return new ValidationResult(
                    ErrorStrings.FilledQuantityWhenFilledPriceHasValue,
                    new[] { nameof(FilledQuantity) });
            }
        }
    }
}
