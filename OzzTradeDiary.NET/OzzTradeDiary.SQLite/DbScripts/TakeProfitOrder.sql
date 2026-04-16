CREATE TABLE IF NOT EXISTS TakeProfitOrders(
    Id INTEGER PRIMARY KEY,
	TradeId INTEGER Not Null, 
	OrderType INTEGER Not Null, 
	ExecuteTime INTEGER, 
	OrderPrice TEXT Not Null, 
	FilledPrice TEXT, 
	OrderQuantity TEXT, 
	FilledQuantity TEXT, 
	OrderValue INTEGER, 
	FilledValue INTEGER, 
	DisplayOrder INTEGER Not Null 
);
Create Index If Not Exists idx_TakeProfitOrders_TradeId on TakeProfitOrders(TradeId);
Create Index If Not Exists idx_TakeProfitOrders_OrderType on TakeProfitOrders(OrderType);
Create Index If Not Exists idx_TakeProfitOrders_ExecuteTime on TakeProfitOrders(ExecuteTime);
Create Index If Not Exists idx_TakeProfitOrders_DisplayOrder on TakeProfitOrders(DisplayOrder);
