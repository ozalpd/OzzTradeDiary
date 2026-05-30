CREATE TABLE IF NOT EXISTS TakeProfitOrders(
    Id INTEGER PRIMARY KEY,
	TradeId INTEGER Not Null, 
	OrderType INTEGER Not Null, 
	OrderPrice TEXT Not Null, 
	FilledTime INTEGER, 
	FilledPrice TEXT, 
	OrderQuantity TEXT, 
	FilledQuantity TEXT, 
	OrderValue INTEGER, 
	FilledValue INTEGER, 
	Notes TEXT, 
	UpdatedAt TEXT Not Null 
);
Create Index If Not Exists idx_TakeProfitOrders_FilledTime on TakeProfitOrders(FilledTime);
Create Index If Not Exists idx_TakeProfitOrders_UpdatedAt on TakeProfitOrders(UpdatedAt DESC, Id);
