CREATE TABLE TradePlans(
    Id INTEGER PRIMARY KEY,
	TradeId INTEGER Not Null, 
	ImageURL TEXT Not Null, 
	Notes TEXT, 
	ModifyDate INTEGER Not Null 
);
