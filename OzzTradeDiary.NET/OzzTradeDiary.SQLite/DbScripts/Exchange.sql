CREATE TABLE IF NOT EXISTS Exchanges(
    Id INTEGER PRIMARY KEY,
	ExchangeName TEXT Not Null, 
	ExchangeCode TEXT Not Null, 
	CountryCode TEXT Not Null, 
	DefaultCurrencyId INTEGER Not Null, 
	Timezone TEXT Not Null, 
	IsAlwaysOpen INTEGER Not Null, 
	HasAnySymbol INTEGER Not Null, 
	DisplayOrder INTEGER Not Null, 
	IsActive INTEGER Not Null 
);
Create Index If Not Exists idx_Exchanges_ExchangeName on Exchanges(ExchangeName COLLATE NOCASE, DisplayOrder, IsActive);
Create Unique Index If Not Exists idx_Exchanges_ExchangeCode on Exchanges(ExchangeCode COLLATE NOCASE, DisplayOrder, IsActive);
Create Index If Not Exists idx_Exchanges_DisplayOrder on Exchanges(DisplayOrder);
Create Index If Not Exists idx_Exchanges_IsActive on Exchanges(IsActive);
