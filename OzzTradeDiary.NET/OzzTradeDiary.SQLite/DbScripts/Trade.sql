CREATE TABLE IF NOT EXISTS Trades(
    Id INTEGER PRIMARY KEY,
	TradingAccountId INTEGER Not Null, 
	SymbolId INTEGER Not Null, 
	TradeDirection INTEGER Not Null, 
	EntryMethod INTEGER Not Null, 
	TradeStatus INTEGER Not Null, 
	Tags TEXT, 
	MarketType INTEGER Not Null, 
	EntryTime TEXT, 
	ExitTime TEXT, 
	CancellationTime TEXT, 
	PlannedPositionValue INTEGER, 
	ExecutedPositionValue INTEGER, 
	RemainingPositionValue INTEGER, 
	IsFullyClosed INTEGER Not Null, 
	PlannedEntryPrice TEXT, 
	ExecutedEntryPrice TEXT, 
	OrderQuantity TEXT, 
	FilledQuantity TEXT, 
	PlannedProfit INTEGER, 
	PlannedTP TEXT, 
	ExecutedTP TEXT, 
	PlannedSL TEXT, 
	ExecutedSL TEXT, 
	PlannedRiskAmount INTEGER, 
	PlannedRiskRewardRatio TEXT, 
	RealizedProfitLoss INTEGER, 
	NetProfitLoss INTEGER, 
	RealizedRiskAmount INTEGER, 
	TotalFeesCalculated INTEGER, 
	TotalFeesCorrected INTEGER, 
	FundingFeeTotal INTEGER, 
	SetupNotes TEXT, 
	ReviewNotes TEXT, 
	UpdatedAt TEXT Not Null 
);
Create Index If Not Exists idx_Trades_TradingAccountId on Trades(TradingAccountId, UpdatedAt DESC, EntryTime DESC);
Create Index If Not Exists idx_Trades_SymbolId on Trades(SymbolId, UpdatedAt DESC, EntryTime DESC);
Create Index If Not Exists idx_Trades_TradeDirection on Trades(TradeDirection, UpdatedAt DESC, EntryTime DESC);
Create Index If Not Exists idx_Trades_EntryMethod on Trades(EntryMethod, UpdatedAt DESC, EntryTime DESC);
Create Index If Not Exists idx_Trades_EntryTime on Trades(EntryTime DESC);
Create Index If Not Exists idx_Trades_PlannedPositionValue on Trades(PlannedPositionValue, UpdatedAt DESC, EntryTime DESC);
Create Index If Not Exists idx_Trades_ExecutedPositionValue on Trades(ExecutedPositionValue, EntryTime DESC);
Create Index If Not Exists idx_Trades_RemainingPositionValue on Trades(RemainingPositionValue, EntryTime DESC);
Create Index If Not Exists idx_Trades_IsFullyClosed on Trades(IsFullyClosed, EntryTime DESC);
Create Index If Not Exists idx_Trades_PlannedProfit on Trades(PlannedProfit, UpdatedAt DESC, EntryTime DESC);
Create Index If Not Exists idx_Trades_RealizedProfitLoss on Trades(RealizedProfitLoss, EntryTime DESC);
Create Index If Not Exists idx_Trades_UpdatedAt on Trades(UpdatedAt DESC, Id);
