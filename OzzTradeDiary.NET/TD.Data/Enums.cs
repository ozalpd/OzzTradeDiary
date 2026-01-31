namespace TD.Data;

public enum EntryMethod : int
{
    Market = 10,
    Limit = 20
}

public enum MarketType : int
{
    Unspecified = 0,
    Stock = 20,
    Fund = 30,
    Futures = 40,
    Forex = 50,
    Option = 60,
    Commodity = 70,
    Crypto = 80,
    CryptoPerpetual = 90,
    Index = 100
}

public enum OrderType : int
{
    Market = 10,
    Limit = 20,
    Stop = 30,
    StopLimit = 40,
    TrailingStop = 50
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