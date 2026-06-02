using TD.Models;

namespace TD.Helpers;

/// <summary>
/// Provides helper methods for common date/time calculations.
/// </summary>
public static class DateHelper
{
    public static (DateTime? Start, DateTime? End) GetMonthPeriod(this DatePeriod datePeriod)
    {
        switch (datePeriod)
        {
            case DatePeriod.AllDates:
                return (null, null);

            case DatePeriod.ThisWeek:
                return (ThisWeekStart(), DateTime.Today);

            case DatePeriod.PreviousWeek:
                return (PreviousWeekStart(), PreviousWeekEnd());

            case DatePeriod.ThisMonth:
                return (ThisMonthStarts(), DateTime.Today);

            case DatePeriod.PreviousMonth:
                return (PreviousMonthStart(), PreviousMonthEnd());

            case DatePeriod.ThisQuarter:
                return (ThisQuarterStarts(), ThisQuarterEnds());

            case DatePeriod.PreviousQuarter:
                return (PreviousQuarterStarts(), PreviousQuarterEnds());

            case DatePeriod.ThisHalfYear:
                return (ThisHalfYearStarts(), ThisHalfYearEnds());

            case DatePeriod.PreviousHalfYear:
                return (PreviousHalfYearStarts(), PreviousHalfYearEnds());

            case DatePeriod.ThisYear:
                return (ThisYearStarts(), ThisYearEnds());

            case DatePeriod.PreviousYear:
                return (PreviousYearStarts(), PreviousYearEnds());

            default:
                break;
        }

        return (null, null);
    }



    /// <summary>
    /// Returns the date of the most recent Monday (or today if today is Monday).
    /// </summary>
    public static DateTime ThisWeekStart(DayOfWeek firstDay = DayOfWeek.Monday)
    {
        var today = DateTime.Today;
        int diff = (7 + (today.DayOfWeek - firstDay)) % 7;
        return today.AddDays(-diff);
    }

    /// <summary>
    /// Returns the beginning date of the previous week.
    /// </summary>
    public static DateTime PreviousWeekStart(DayOfWeek firstDay = DayOfWeek.Monday)
        => ThisWeekStart(firstDay).AddDays(-7);

    /// <summary>
    /// Returns the end date (inclusive) of the previous week.
    /// </summary>
    public static DateTime PreviousWeekEnd(DayOfWeek firstDay = DayOfWeek.Monday)
        => ThisWeekStart(firstDay).AddDays(-1);

    public static DateTime ThisMonthStarts()
        => new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

    public static DateTime ThisMonthEnds()
    {
        var today = DateTime.Today;
        var nextMonth = today.AddMonths(1);
        return new DateTime(nextMonth.Year, nextMonth.Month, 1).AddDays(-1);
    }

    public static DateTime PreviousMonthEnd() => ThisMonthStarts().AddDays(-1);

    public static DateTime PreviousMonthStart()
    {
        var today = DateTime.Today;
        var previousMonth = today.AddMonths(-1);
        return new DateTime(previousMonth.Year, previousMonth.Month, 1);
    }

    public static DateTime ThisQuarterStarts()
    {
        var today = DateTime.Today;
        int currentQuarter = (today.Month - 1) / 3 + 1;
        return new DateTime(today.Year, (currentQuarter - 1) * 3 + 1, 1);
    }

    public static DateTime ThisQuarterEnds()
    {
        var today = DateTime.Today;
        int currentQuarter = (today.Month - 1) / 3 + 1;
        int nextQuarter = currentQuarter == 4 ? 1 : currentQuarter + 1;
        var nextQuarterStart = new DateTime(today.Year, nextQuarter * 3 - 2, 1);
        return nextQuarterStart.AddDays(-1);
    }

    public static DateTime PreviousQuarterStarts()
    {
        var today = DateTime.Today;
        int currentQuarter = (today.Month - 1) / 3 + 1;
        int prevQuarter = currentQuarter == 1 ? 4 : currentQuarter - 1;
        return new DateTime(today.Year, (prevQuarter - 1) * 3 + 1, 1);
    }

    public static DateTime PreviousQuarterEnds() => ThisQuarterStarts().AddDays(-1);


    public static DateTime ThisHalfYearStarts()
    {
        var today = DateTime.Today;
        int currentHalf = (today.Month - 1) / 6 + 1;

        return currentHalf == 1
             ? new DateTime(today.Year, 1, 1)
             : new DateTime(today.Year, 7, 1);
    }

    public static DateTime ThisHalfYearEnds()
    {
        var today = DateTime.Today;
        int currentHalf = (today.Month - 1) / 6 + 1;

        return currentHalf == 1
             ? new DateTime(today.Year, 6, 30)
             : new DateTime(today.Year, 12, 31);
    }

    public static DateTime PreviousHalfYearStarts()
    {
        var today = DateTime.Today;
        int currentHalf = (today.Month - 1) / 6 + 1;

        return currentHalf == 1
             ? new DateTime(today.Year - 1, 7, 1)
             : new DateTime(today.Year, 1, 1);
    }

    public static DateTime PreviousHalfYearEnds()
    {
        var today = DateTime.Today;
        int currentHalf = (today.Month - 1) / 6 + 1;

        return currentHalf == 1
             ? new DateTime(today.Year - 1, 12, 31)
             : new DateTime(today.Year, 6, 30);
    }

    public static DateTime ThisYearStarts() => new DateTime(DateTime.Today.Year, 1, 1);

    public static DateTime ThisYearEnds() => new DateTime(DateTime.Today.Year, 12, 31);

    public static DateTime PreviousYearStarts() => new DateTime(DateTime.Today.Year - 1, 1, 1);

    public static DateTime PreviousYearEnds() => new DateTime(DateTime.Today.Year - 1, 12, 31);
}