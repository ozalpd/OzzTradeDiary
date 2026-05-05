CREATE TABLE IF NOT EXISTS Holidays(
    Id INTEGER PRIMARY KEY,
	HolidayName TEXT Not Null, 
	ExchangeId INTEGER Not Null, 
	Month INTEGER, 
	Day INTEGER, 
	HolidayDate TEXT, 
	IsHalfDay INTEGER Not Null 
);
Create Index If Not Exists idx_Holidays_ExchangeId on Holidays(ExchangeId);
