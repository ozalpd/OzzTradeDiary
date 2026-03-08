CREATE TABLE Symbols(
    Id INTEGER PRIMARY KEY,
	Ticker TEXT Not Null, 
	TickerFull TEXT Not Null, 
	BaseCurrency TEXT, 
	PriceCurrency TEXT Not Null, 
	Description TEXT, 
	ExchangeId INTEGER Not Null, 
	MarketType INTEGER Not Null, 
	DisplayOrder INTEGER Not Null, 
	IsActive INTEGER Not Null 
);
