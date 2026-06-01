using System.ComponentModel.DataAnnotations;
using TD.i18n;

namespace TD.Models
{
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
        [Display(ResourceType = typeof(LocalizedStrings), Name = "Crypto")]
        Crypto = 80,
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

    public enum TradeDirection : int
    {
        [Display(ResourceType = typeof(LocalizedStrings), Name = "Long")]
        Long = 200,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "Short")]
        Short = 100
    }

    public enum TradeStatus
    {
        [Display(ResourceType = typeof(LocalizedStrings), Name = "Planned")]
        Planned = 10,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "Pending")]
        Pending = 20,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "Active")]
        Active = 30,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "Closed")]
        Closed = 40,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "Missed")]
        Missed = -10,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "Cancelled")]
        Cancelled = -20
    }

    public enum TradeStatusQuery
    {
        [Display(ResourceType = typeof(LocalizedStrings), Name = "All", Order = 0)]
        All = 0,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "Waiting", Order = 300)]
        Waiting = 1010,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "ActiveOrWaiting", Order = 100)]
        ActiveOrWaiting = 1020,
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