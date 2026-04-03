CREATE TABLE IF NOT EXISTS TradeImages(
    Id INTEGER PRIMARY KEY,
	TradeId INTEGER, 
	ImageURL TEXT Not Null, 
	Notes TEXT, 
	UpdatedAt TEXT Not Null 
);
Create Index If Not Exists idx_TradeImages_TradeId on TradeImages(TradeId );
Create Index If Not Exists idx_TradeImages_UpdatedAt on TradeImages(UpdatedAt  DESC);
