namespace TD.Extensions
{
    public static class Numeric
    {
        /// <summary>
        /// Rounds a decimal value to the nearest multiple of a specified quantum.
        /// </summary>
        /// <param name="value">The decimal value to round.</param>
        /// <param name="quantum">The quantum (step size) to round to.</param>
        /// <returns>The value rounded to the nearest multiple of <paramref name="quantum"/>.</returns>
        public static decimal RoundToQuantum(this decimal value, decimal quantum)
        {
            return Math.Round(value / quantum) * quantum;
        }

        /// <summary>
        /// Rounds a decimal value to a precision level determined by its magnitude, with smaller values receiving more
        /// decimal places.
        /// </summary>
        /// <param name="value">The decimal value to round.</param>
        /// <returns>The rounded value with 1-6 decimal places depending on magnitude: 1 place for values > 10000, 2 for > 1000,
        /// 3 for > 100, 4 for > 10, 5 for > 1, and 6 for values ≤ 1.</returns>
        public static decimal RoundToQuantum(this decimal value)
        {
            if (value > 10000)
            {
                value = Math.Round(value, 1);
            }
            else if (value > 1000)
            {
                value = Math.Round(value, 2);
            }
            else if (value > 100)
            {
                value = Math.Round(value, 3);
            }
            else if (value > 10)
            {
                value = Math.Round(value, 4);
            }
            else if (value > 1)
            {
                value = Math.Round(value, 5);
            }
            else
            {
                value = Math.Round(value, 6);
            }

            return value;
        }
    }
}
