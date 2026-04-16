using System.ComponentModel.DataAnnotations;
using TD.i18n;

namespace TD.Models
{
    public partial class TakeProfitOrder
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
    }
}
