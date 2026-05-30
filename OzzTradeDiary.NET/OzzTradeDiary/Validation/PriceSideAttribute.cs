using System.ComponentModel.DataAnnotations;
using TD.Models;

namespace TD.Validation
{
    /// <summary>
    /// Validates that a planned price level (TP or SL) is on the correct side of the entry price,
    /// relative to trade direction. Only enforced when <see cref="Trade.TradeStatus"/> is
    /// <see cref="TradeStatus.Planned"/> or <see cref="TradeStatus.Pending"/>; passes silently for
    /// Active and Closed trades because price levels can be freely adjusted once a position is open.
    /// </summary>
    /// <remarks>
    /// Apply to <c>OrderPrice</c> on order entities that hold a <c>Trade</c> navigation property.
    /// The attribute reaches through that property to read <see cref="Trade.TradeDirection"/>,
    /// <see cref="Trade.TradeStatus"/>, and <see cref="Trade.PlannedEntryPrice"/> without requiring
    /// those fields to be duplicated on the order entity.
    /// <example>
    /// <code>
    /// // TakeProfitOrder — must be above entry for Long, below for Short
    /// [PriceSide(PriceSide.Above)]
    /// public decimal OrderPrice { get; set; }
    ///
    /// // StopLossOrder — must be below entry for Long, above for Short
    /// [PriceSide(PriceSide.Below)]
    /// public decimal OrderPrice { get; set; }
    /// </code>
    /// </example>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class PriceSideAttribute : ValidationAttribute
    {
        /// <param name="side">
        /// The required side relative to entry price for a <see cref="TradeDirection.Long"/> position.
        /// Automatically inverted for <see cref="TradeDirection.Short"/> positions.
        /// </param>
        public PriceSideAttribute(PriceSide side)
        {
            Side = side;
        }

        /// <summary>The required side for a Long position.</summary>
        public PriceSide Side { get; }

        /// <summary>
        /// Name of the property on the validated object that exposes the parent <see cref="Trade"/>.
        /// Defaults to <c>"Trade"</c>.
        /// </summary>
        public string TradeProperty { get; set; } = "Trade";

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            if (validationContext.ObjectInstance is not Trade trade)
            {
                // Reach through the navigation property to the parent Trade
                var tradeProp = validationContext.ObjectType.GetProperty(TradeProperty);
                if (tradeProp == null)
                    return ValidationResult.Success;

                if (tradeProp.GetValue(validationContext.ObjectInstance) is not Trade trade2)
                    return ValidationResult.Success;

                trade = trade2;
            }

            // Only enforce during the planning phase; once a position is open price levels move freely
            if (trade.TradeStatus != TradeStatus.Planned && trade.TradeStatus != TradeStatus.Pending)
                return ValidationResult.Success;

            // Cannot validate without a known entry price
            if (trade.PlannedEntryPrice is not > 0)
                return ValidationResult.Success;

            var price = Convert.ToDecimal(value);
            var entryPrice = trade.PlannedEntryPrice!.Value;

            bool isDirectionSet = trade.TradeDirection == TradeDirection.Long || trade.TradeDirection == TradeDirection.Short;
            if (!isDirectionSet)
            {
                string message = "Trade direction must be set to Long or Short before setting price.";
                return new ValidationResult(message, new[] { validationContext.MemberName! });
            }

            // Long → Side is taken as-is. Short → Side is inverted.
            bool mustBeAbove = (trade.TradeDirection == TradeDirection.Long) == (Side == PriceSide.Above);
            bool isValid = mustBeAbove ? price > entryPrice : price < entryPrice;

            if (!isValid)
            {
                string sideLabel = mustBeAbove ? "above" : "below";
                string message = string.Format(
                    ErrorMessage ?? $"The {{0}} field must be {sideLabel} the entry price ({entryPrice}).",
                    validationContext.DisplayName);

                return new ValidationResult(message, new[] { validationContext.MemberName! });
            }

            return ValidationResult.Success;
        }
    }
}
