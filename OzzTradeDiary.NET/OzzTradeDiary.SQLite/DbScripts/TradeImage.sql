CREATE TABLE TradeImages(
    Id INTEGER PRIMARY KEY,
	TradeId INTEGER, 
	ImageURL TEXT Not Null, 
	Notes TEXT, 
	ModifyDate INTEGER Not Null 
);
