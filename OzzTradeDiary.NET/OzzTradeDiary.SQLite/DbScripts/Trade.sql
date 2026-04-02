CREATE TABLE IF NOT EXISTS Trades(
    Id INTEGER PRIMARY KEY,
	TradingAccountId INTEGER Not Null, 
	SymbolId INTEGER Not Null, 
	EntryTime INTEGER, 
	EntryMethod INTEGER Not Null, 
	TradeDirection INTEGER Not Null, 
	PlannedEntry REAL, 
	ExecutedEntry REAL, 
	PlannedTP REAL, 
	ExecutedTP REAL, 
	PlannedSL REAL, 
	ExecutedSL REAL, 
	ModifyDate TEXT Not Null 
);
Create Index If Not Exists idx_Trades_TradingAccountId on Trades(TradingAccountId );
Create Index If Not Exists idx_Trades_SymbolId on Trades(SymbolId );
Create Index If Not Exists idx_Trades_EntryTime on Trades(EntryTime  DESC);
Create Index If Not Exists idx_Trades_EntryMethod on Trades(EntryMethod );
Create Index If Not Exists idx_Trades_TradeDirection on Trades(TradeDirection );
Create Index If Not Exists idx_Trades_ModifyDate on Trades(ModifyDate  DESC);
