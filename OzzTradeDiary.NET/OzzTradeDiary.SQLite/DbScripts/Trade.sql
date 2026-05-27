CREATE TABLE IF NOT EXISTS Trades(
    Id INTEGER PRIMARY KEY,
	TradingAccountId INTEGER Not Null, 
	SymbolId INTEGER Not Null, 
	EntryMethod INTEGER Not Null, 
	TradeDirection INTEGER Not Null, 
	EntryTime TEXT, 
	ExitTime TEXT, 
	IsFullyClosed INTEGER Not Null, 
	RealizedR TEXT, 
	PlannedEntryPrice TEXT, 
	ExecutedEntryPrice TEXT, 
	OrderQuantity TEXT, 
	FilledQuantity TEXT, 
	PlannedPositionValue INTEGER, 
	ExecutedPositionValue INTEGER, 
	RemainingPositionValue INTEGER, 
	PlannedProfit INTEGER, 
	RealizedProfitLoss INTEGER, 
	PlannedTP TEXT, 
	ExecutedTP TEXT, 
	PlannedRiskAmount INTEGER, 
	RealizedRiskAmount INTEGER, 
	PlannedSL TEXT, 
	ExecutedSL TEXT, 
	PlannedRiskRewardRatio TEXT, 
	SetupNotes TEXT, 
	ReviewNotes TEXT, 
	UpdatedAt TEXT Not Null 
);
Create Index If Not Exists idx_Trades_TradingAccountId on Trades(TradingAccountId, UpdatedAt DESC, EntryTime DESC);
Create Index If Not Exists idx_Trades_SymbolId on Trades(SymbolId, UpdatedAt DESC, EntryTime DESC);
Create Index If Not Exists idx_Trades_EntryMethod on Trades(EntryMethod);
Create Index If Not Exists idx_Trades_TradeDirection on Trades(TradeDirection, UpdatedAt DESC, EntryTime DESC);
Create Index If Not Exists idx_Trades_EntryTime on Trades(EntryTime DESC);
Create Index If Not Exists idx_Trades_IsFullyClosed on Trades(IsFullyClosed, EntryTime DESC);
Create Index If Not Exists idx_Trades_PlannedPositionValue on Trades(PlannedPositionValue, UpdatedAt DESC, EntryTime DESC);
Create Index If Not Exists idx_Trades_ExecutedPositionValue on Trades(ExecutedPositionValue, EntryTime DESC);
Create Index If Not Exists idx_Trades_RemainingPositionValue on Trades(RemainingPositionValue, EntryTime DESC);
Create Index If Not Exists idx_Trades_PlannedProfit on Trades(PlannedProfit, UpdatedAt DESC, EntryTime DESC);
Create Index If Not Exists idx_Trades_RealizedProfitLoss on Trades(RealizedProfitLoss, EntryTime DESC);
Create Index If Not Exists idx_Trades_UpdatedAt on Trades(UpdatedAt DESC, Id);
