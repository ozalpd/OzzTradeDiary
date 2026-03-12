CREATE TABLE IF NOT EXISTS StopLossOrders(
    Id INTEGER PRIMARY KEY,
	TradeId INTEGER Not Null, 
	StopAll INTEGER Not Null, 
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
Create Index If Not Exists idx_StopLossOrders_TradeId on StopLossOrders(TradeId );
Create Index If Not Exists idx_StopLossOrders_OrderType on StopLossOrders(OrderType );
Create Index If Not Exists idx_StopLossOrders_ExecuteTime on StopLossOrders(ExecuteTime );
Create Index If Not Exists idx_StopLossOrders_DisplayOrder on StopLossOrders(DisplayOrder );
