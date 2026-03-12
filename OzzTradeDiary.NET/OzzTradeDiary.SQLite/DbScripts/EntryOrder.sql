CREATE TABLE IF NOT EXISTS EntryOrders(
    Id INTEGER PRIMARY KEY,
	TradeId INTEGER Not Null, 
	OrderType INTEGER Not Null, 
	ExecuteTime INTEGER, 
	OrderPrice REAL Not Null, 
	FilledPrice REAL, 
	OrderQuantity REAL, 
	FilledQuantity REAL, 
	OrderAmount REAL, 
	FilledAmount REAL, 
	DisplayOrder INTEGER Not Null 
);
Create Index If Not Exists idx_EntryOrders_TradeId on EntryOrders(TradeId );
Create Index If Not Exists idx_EntryOrders_OrderType on EntryOrders(OrderType );
Create Index If Not Exists idx_EntryOrders_ExecuteTime on EntryOrders(ExecuteTime );
Create Index If Not Exists idx_EntryOrders_DisplayOrder on EntryOrders(DisplayOrder );
