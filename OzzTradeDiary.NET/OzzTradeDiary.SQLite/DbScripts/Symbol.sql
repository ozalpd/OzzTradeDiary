CREATE TABLE IF NOT EXISTS Symbols(
    Id INTEGER PRIMARY KEY,
	Ticker TEXT Not Null, 
	TickerFull TEXT Not Null, 
	BaseCurrency TEXT, 
	PriceCurrencyId INTEGER Not Null, 
	Description TEXT, 
	ExchangeId INTEGER Not Null, 
	MarketType INTEGER Not Null, 
	DisplayOrder INTEGER Not Null, 
	IsActive INTEGER Not Null 
);
Create Index If Not Exists idx_Symbols_Ticker on Symbols(Ticker COLLATE NOCASE);
Create Unique Index If Not Exists idx_Symbols_TickerFull on Symbols(TickerFull COLLATE NOCASE);
Create Index If Not Exists idx_Symbols_ExchangeId on Symbols(ExchangeId);
Create Index If Not Exists idx_Symbols_MarketType on Symbols(MarketType);
Create Index If Not Exists idx_Symbols_DisplayOrder on Symbols(DisplayOrder);
Create Index If Not Exists idx_Symbols_IsActive on Symbols(IsActive);
