CREATE TABLE IF NOT EXISTS Exchanges(
    Id INTEGER PRIMARY KEY,
	ExchangeName TEXT Not Null, 
	ExchangeCode TEXT Not Null, 
	DefaultCurrency TEXT, 
	HasAnySymbol INTEGER Not Null, 
	DisplayOrder INTEGER Not Null, 
	IsActive INTEGER Not Null 
);
Create Index If Not Exists idx_Exchanges_ExchangeName on Exchanges(ExchangeName COLLATE NOCASE);
Create Unique Index If Not Exists idx_Exchanges_ExchangeCode on Exchanges(ExchangeCode COLLATE NOCASE);
Create Index If Not Exists idx_Exchanges_DisplayOrder on Exchanges(DisplayOrder );
Create Index If Not Exists idx_Exchanges_IsActive on Exchanges(IsActive );
