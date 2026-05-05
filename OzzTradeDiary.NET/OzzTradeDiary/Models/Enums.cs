using System.ComponentModel.DataAnnotations;
using TD.i18n;

namespace TD.Models
{
    public enum EntryMethod : int
    {
        Market = 10,
        Limit = 20
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

    public enum OrderType : int
    {
        [Display(ResourceType = typeof(LocalizedStrings), Name = "Market")]
        Market = 10,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "Limit")]
        Limit = 20,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "Stop")]
        Stop = 30,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "StopLimit")]
        StopLimit = 40,
        [Display(ResourceType = typeof(LocalizedStrings), Name = "TrailingStop")]
        TrailingStop = 50
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
        Long = 200,
        Short = 100
    }
}