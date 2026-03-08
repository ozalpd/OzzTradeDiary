CREATE TABLE Currencies(
    Id INTEGER PRIMARY KEY,
	CurrencyTicker TEXT Not Null, 
	Description TEXT, 
	DisplayOrder INTEGER Not Null, 
	IsActive INTEGER Not Null 
);
