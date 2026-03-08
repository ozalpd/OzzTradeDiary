CREATE TABLE Trades(
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
	ModifyDate INTEGER Not Null 
);
