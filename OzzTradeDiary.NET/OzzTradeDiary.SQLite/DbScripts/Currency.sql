CREATE TABLE IF NOT EXISTS Currencies(
    Id INTEGER PRIMARY KEY,
	CurrencyTicker TEXT Not Null, 
	Description TEXT, 
	DisplayOrder INTEGER Not Null, 
	IsActive INTEGER Not Null 
);
Create Unique Index If Not Exists idx_Currencies_CurrencyTicker on Currencies(CurrencyTicker COLLATE NOCASE);
Create Index If Not Exists idx_Currencies_DisplayOrder on Currencies(DisplayOrder );
Create Index If Not Exists idx_Currencies_IsActive on Currencies(IsActive );
