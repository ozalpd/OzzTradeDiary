using System.ComponentModel.DataAnnotations;
using TD.i18n;

namespace TD.Models
{
    public enum DatePeriod
    {
        [Display(ResourceType = typeof(LocalizedStrings), Name = "AllDates", Order = 0)]
        AllDates = 0,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "ThisWeek", Order = 10)]
        ThisWeek = 10,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "PreviousWeek", Order = 20)]
        PreviousWeek = 20,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "ThisMonth", Order = 30)]
        ThisMonth = 30,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "PreviousMonth", Order = 40)]
        PreviousMonth = 40,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "ThisQuarter", Order = 50)]
        ThisQuarter = 50,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "PreviousQuarter", Order = 60)]
        PreviousQuarter = 60,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "ThisHalfYear", Order = 70)]
        ThisHalfYear = 70,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "PreviousHalfYear", Order = 80)]
        PreviousHalfYear = 80,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "ThisYear", Order = 90)]
        ThisYear = 90,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "PreviousYear", Order = 100)]
        PreviousYear = 100
    }

    public enum EntryOrderType : int
    {
        [Display(ResourceType = typeof(LocalizedStrings), Name = "Market")]
        Market = 10,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "Limit")]
        Limit = 20,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "StopMarket")]
        StopMarket = 40,   // stop-market entry
        [Display(ResourceType = typeof(LocalizedStrings), Name = "StopLimit")]
        StopLimit = 50
    }

    public enum ExitOrderType : int
    {
        [Display(ResourceType = typeof(LocalizedStrings), Name = "Market")]
        Market = 10,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "Limit")]
        Limit = 20,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "TrailingStop")]
        TrailingStop = 30,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "Stop")]
        Stop = 40,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "StopLimit")]
        StopLimit = 50,
    }

    public enum TradeImageCategory : int
    {
        [Display(ResourceType = typeof(LocalizedStrings), Name = "Setup", Order = 10)]
        Setup = 10,  // Chart showing the trade setup / entry signal
        [Display(ResourceType = typeof(LocalizedStrings), Name = "Entry", Order = 20)]
        Entry = 20,  // Screenshot at entry execution
        [Display(ResourceType = typeof(LocalizedStrings), Name = "Exit", Order = 30)]
        Exit = 30,  // Screenshot at exit execution
        [Display(ResourceType = typeof(LocalizedStrings), Name = "Review", Order = 40)]
        Review = 40,  // Post-trade analysis / journaling
        [Display(ResourceType = typeof(LocalizedStrings), Name = "Other", Order = 50)]
        Other = 50,  // Uncategorized
    }

    public enum MarketType : int
    {
        [Display(ResourceType = typeof(LocalizedStrings), Name = "Unspecified")]
        Unspecified = 0,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "Stock")]
        Stock = 20,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "Fund")]
        Fund = 30,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "Futures")]
        Futures = 40,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "Forex")]
        Forex = 50,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "Option")]
        Option = 60,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "Commodity")]
        Commodity = 70,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "CryptoSpot")]
        CryptoSpot = 80,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "CryptoPerpetual")]
        CryptoPerpetual = 90,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "Index")]
        Index = 100
    }

    /// <summary>
    /// Specifies which side of the entry price a planned price level must be on for a Long position.
    /// The rule is automatically inverted for Short positions.
    /// </summary>
    public enum PriceSide
    {
        /// <summary>Price must be above the entry price (e.g. Take Profit for Long).</summary>
        Above,
        /// <summary>Price must be below the entry price (e.g. Stop Loss for Long).</summary>
        Below
    }

    public enum ReportType
    {
        Balance = 10,
        Performance = 20,
        NetResult = 30,
        TradeHistory = 40
    }

    public enum SessionType : int
    {
        [Display(ResourceType = typeof(LocalizedStrings), Name = "Break")]
        Break = 0,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "Regular")]
        Regular = 1,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "PreMarket")]
        PreMarket = 2,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "AfterHours")]
        AfterHours = 3,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "SundayOpen")]
        SundayOpen = 5,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "OpeningAuction")]
        OpeningAuction = 6,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "ClosingAuction")]
        ClosingAuction = 7,

        // CME-specific
        [Display(ResourceType = typeof(LocalizedStrings), Name = "RegularTradingHours")]
        RegularTradingHours = 8,      // RTH
        [Display(ResourceType = typeof(LocalizedStrings), Name = "ElectronicTradingHours")]
        ElectronicTradingHours = 9    // ETH / Globex
    }

    public enum SettingType : int
    {
        String = 1010,
        Email = 1020,
        StringArray = 1101,
        Boolean = 2000,
        Integer = 2010,
        IntegerArray = 2011,
        Decimal = 2021,
        DecimalArray = 2022,
        Date = 3010,
        DateArray = 3011,
        DateTime = 3020,
        DateTimeArray = 3021
    }

    public enum TradeDateType : int
    {
        [Display(ResourceType = typeof(LocalizedStrings), Name = "EntryTime", Order = 10)]
        EntryTime = 10,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "ExitTime", Order = 20)]
        ExitTime = 20,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "CancellationTime", Order = 30)]
        CancellationTime = 30,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "UpdateTime", Order = 40)]
        UpdateTime = 40
    }

    public enum TradeDirection : int
    {
        [Display(ResourceType = typeof(LocalizedStrings), Name = "Long")]
        Long = 200,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "Short")]
        Short = 100
    }

    public enum TradeStatus
    {
        [Display(ResourceType = typeof(LocalizedStrings), Name = "Planned", Order = 10)]
        Planned = 10,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "Pending", Order = 20)]
        Pending = 20,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "Active", Order = 30)]
        Active = 30,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "Closed", Order = 40)]
        Closed = 40,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "Missed", Order = 50)]
        Missed = -10,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "Cancelled", Order = 60)]
        Cancelled = -20
    }

    public enum TradeStatusQuery
    {
        [Display(ResourceType = typeof(LocalizedStrings), Name = "All", Order = 0)]
        All = 0,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "ActiveOrWaiting", Order = 100)]
        ActiveOrWaiting = 1010,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "Waiting", Order = 300)]
        Waiting = 1020,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "MissedOrCancelled", Order = 700)]
        MissedOrCancelled = 1030,

        [Display(ResourceType = typeof(LocalizedStrings), Name = "Planned", Order = 400)]
        Planned = 10,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "Pending", Order = 500)]
        Pending = 20,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "Active", Order = 200)]
        Active = 30,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "Closed", Order = 600)]
        Closed = 40,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "Missed", Order = 800)]
        Missed = -10,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "Cancelled", Order = 900)]
        Cancelled = -20
    }
}