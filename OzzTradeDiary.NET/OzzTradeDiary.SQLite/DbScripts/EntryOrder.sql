CREATE TABLE IF NOT EXISTS EntryOrders(
    Id INTEGER PRIMARY KEY,
	TradeId INTEGER Not Null, 
	OrderType INTEGER Not Null, 
	OrderPrice TEXT Not Null, 
	FilledPrice TEXT, 
	OrderQuantity TEXT, 
	FilledQuantity TEXT, 
	OrderValue INTEGER, 
	FilledValue INTEGER, 
	FilledTime INTEGER, 
	Notes TEXT, 
	UpdatedAt TEXT Not Null 
);
Create Index If Not Exists idx_EntryOrders_FilledTime on EntryOrders(FilledTime);
Create Index If Not Exists idx_EntryOrders_UpdatedAt on EntryOrders(UpdatedAt DESC, Id);
