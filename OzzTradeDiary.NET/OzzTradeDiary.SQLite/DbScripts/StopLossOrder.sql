CREATE TABLE StopLossOrders(
    Id INTEGER PRIMARY KEY,
	TradeId INTEGER Not Null, 
	StopAll INTEGER Not Null, 
	ExecuteTime INTEGER, 
	OrderPrice REAL Not Null, 
	FilledPrice REAL, 
	OrderQuantity REAL, 
	FilledQuantity REAL, 
	OrderAmount REAL, 
	FilledAmount REAL, 
	DisplayOrder INTEGER Not Null 
);
