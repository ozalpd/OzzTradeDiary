CREATE TABLE IF NOT EXISTS Sessions(
    Id INTEGER PRIMARY KEY,
	ExchangeId INTEGER Not Null, 
	SessionType INTEGER Not Null, 
	DayOfWeek INTEGER, 
	LocalOpen TEXT Not Null, 
	LocalClose TEXT Not Null 
);
