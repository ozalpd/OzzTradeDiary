CREATE TABLE TradingAccounts(
    Id INTEGER PRIMARY KEY,
	Title TEXT Not Null, 
	AccountCode TEXT, 
	ExchangeId INTEGER Not Null, 
	Notes TEXT, 
	DisplayOrder INTEGER Not Null, 
	IsActive INTEGER Not Null 
);
