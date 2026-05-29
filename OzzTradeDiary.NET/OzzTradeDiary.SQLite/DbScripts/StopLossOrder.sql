CREATE TABLE IF NOT EXISTS StopLossOrders(
    Id INTEGER PRIMARY KEY,
	TradeId INTEGER Not Null, 
	OrderType INTEGER Not Null, 
	FilledTime INTEGER, 
	OrderPrice TEXT Not Null, 
	FilledPrice TEXT, 
	OrderQuantity TEXT, 
	FilledQuantity TEXT, 
	OrderValue INTEGER, 
	FilledValue INTEGER, 
	Notes TEXT, 
	UpdatedAt TEXT Not Null 
);
Create Index If Not Exists idx_StopLossOrders_FilledTime on StopLossOrders(FilledTime);
Create Index If Not Exists idx_StopLossOrders_UpdatedAt on StopLossOrders(UpdatedAt DESC, Id);
