CREATE TABLE IF NOT EXISTS Trades(
    Id INTEGER PRIMARY KEY,
	TradingAccountId INTEGER Not Null, 
	SymbolId INTEGER Not Null, 
	EntryTime INTEGER, 
	EntryMethod INTEGER Not Null, 
	TradeDirection INTEGER Not Null, 
	OrderQuantity TEXT, 
	PlannedEntry TEXT, 
	PlannedPositionValue INTEGER, 
	FilledQuantity TEXT, 
	ExecutedEntry TEXT, 
	ExecutedPositionValue INTEGER, 
	PlannedTP TEXT, 
	ExecutedTP TEXT, 
	PlannedSL TEXT, 
	ExecutedSL TEXT, 
	UpdatedAt TEXT Not Null 
);
Create Index If Not Exists idx_Trades_TradingAccountId on Trades(TradingAccountId, UpdatedAt DESC);
Create Index If Not Exists idx_Trades_SymbolId on Trades(SymbolId, UpdatedAt DESC);
Create Index If Not Exists idx_Trades_EntryTime on Trades(EntryTime DESC);
Create Index If Not Exists idx_Trades_EntryMethod on Trades(EntryMethod);
Create Index If Not Exists idx_Trades_TradeDirection on Trades(TradeDirection);
Create Index If Not Exists idx_Trades_PlannedPositionValue on Trades(PlannedPositionValue, UpdatedAt DESC);
Create Index If Not Exists idx_Trades_ExecutedPositionValue on Trades(ExecutedPositionValue, EntryTime DESC);
Create Index If Not Exists idx_Trades_UpdatedAt on Trades(UpdatedAt DESC, Id);
