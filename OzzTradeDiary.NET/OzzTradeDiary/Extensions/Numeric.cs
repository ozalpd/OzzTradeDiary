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
        /// <returns>The rounded value with 1-6 decimal places depending on magnitude: 1 place for values > 20000, 2 for > 2000,
        /// 3 for > 200, 4 for > 20, 5 for > 2, and 6 for values ≤ 1.</returns>
        public static decimal RoundToQuantum(this decimal value)
        {
            if (value > 200000)
            {
                value = Math.Round(value, 0);
            }
            else if (value > 20000)
            {
                value = Math.Round(value, 1);
            }
            else if (value > 2000)
            {
                value = Math.Round(value, 2);
            }
            else if (value > 200)
            {
                value = Math.Round(value, 3);
            }
            else if (value > 20)
            {
                value = Math.Round(value, 4);
            }
            else if (value > 2)
            {
                value = Math.Round(value, 5);
            }
            else
            {
                value = Math.Round(value, 6);
            }

            return value;
        }

        public static string ToRoundedString(this decimal value)
        {
            string s;
            if (value > 200000)
            {
                s = value.RoundToQuantum().ToString("N0");
            }
            else if (value > 20000)
            {
                s = value.RoundToQuantum().ToString("N1");
            }
            else if (value > 2000)
            {
                s = value.RoundToQuantum().ToString("N2");
            }
            else if (value > 200)
            {
                s = value.RoundToQuantum().ToString("N3");
            }
            else if (value > 20)
            {
                s = value.RoundToQuantum().ToString("N4");
            }
            else if (value > 2)
            {
                s = value.RoundToQuantum().ToString("N5");
            }
            else
            {
                s = value.RoundToQuantum().ToString("N6");
            }
            return s;
        }

        public static string ToRoundedString(this decimal? value)
        {
            if (value.HasValue)
            {
                return value.Value.ToRoundedString();
            }
            return string.Empty;
        }
    }
}