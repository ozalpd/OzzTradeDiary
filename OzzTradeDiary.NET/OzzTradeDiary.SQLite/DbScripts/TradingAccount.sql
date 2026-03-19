CREATE TABLE IF NOT EXISTS TradingAccounts(
    Id INTEGER PRIMARY KEY,
	Title TEXT Not Null, 
	ExchangeId INTEGER Not Null, 
	Notes TEXT, 
	DisplayOrder INTEGER Not Null, 
	IsActive INTEGER Not Null 
);
Create Index If Not Exists idx_TradingAccounts_Title on TradingAccounts(Title COLLATE NOCASE);
Create Index If Not Exists idx_TradingAccounts_ExchangeId on TradingAccounts(ExchangeId );
Create Index If Not Exists idx_TradingAccounts_DisplayOrder on TradingAccounts(DisplayOrder );
Create Index If Not Exists idx_TradingAccounts_IsActive on TradingAccounts(IsActive );
