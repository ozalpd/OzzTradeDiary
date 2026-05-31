# OzzTradeDiary.WPF — Development Roadmap

This file is an AI-readable planning document. It tracks implemented features and
prioritized upcoming work. Update status markers as features are completed.

## Status Markers
- ✅ Done
- 🔴 Cycle 1 — Blocking (do first)
- 🟠 Cycle 2 — Usability
- 🟡 Cycle 3 — Analytics
- 🟢 Cycle 4 — Import/Export
- ⚪ Cycle 5 — Polish

---

## ✅ Implemented

- Maintenance CRUD: Currency, Exchange, Symbol, TradingAccount (full create/edit/delete with views)
- Trade CRUD: create, edit, delete dialogs with full field coverage
- Trade list: master pane with paging, date-range + account + symbol filters
- Trade detail pane: inline in MainWindow — all trade properties, order grids with add/edit/delete buttons
- TradeDetailView: standalone popup window with full trade + orders + notes
- Order CRUD: EntryOrder, StopLossOrder, TakeProfitOrder — full create/edit/delete
- Order calculated display props: `OrderRiskAmount` (SL VMs), `OrderProfitAmount` (TP VMs)
- Converters: TradeStatusToColor, TradeDirectionToColor, BoolToColor
- Database backup service: AutoBackupHelper, DatabaseBackupService (logic only, no UI trigger yet)
- About dialog
- App infrastructure: AppSettings, AppVersion, WindowPosition, composition root in App.OnStartup

---

## 🔴 Cycle 1 — Core Workflow Gaps (blocking daily use)

### 1. TradeImage CRUD
- ViewModel, commands, and view for adding/removing images (URLs + local paths + notes per image)
- `TradeImage` repository is implemented; zero UI exists
- Images are a core journaling feature — attach chart screenshots to trades

### 2. TradeStatus + TradeDirection + MarketType filters in toolbar
- Filter bar only has date range + account + symbol
- Add ComboBox filters for TradeStatus, TradeDirection, MarketType to the filter toolbar
- All three have corresponding `*Values` collections already in TradeListVM

### 3. P&L color coding in trade list
- `NetProfitLoss` and `RealizedProfitLoss` columns in the trade DataGrid need color coding
- Positive = green, negative = red, null = neutral
- Use a converter or CellStyle with DataTriggers

### 4. Fee auto-calculation wiring
- `Trade.TotalFeesCalculated` is persisted but never automatically populated
- `CalculateFromOrders()` in `Trade.calc.cs` should sum `FeeCalculated` from all EntryOrders + TakeProfitOrders + StopLossOrders
- `FeeCalculated` on order entities is a calculated-only property: `FilledValue * rate` (maker/taker from TradingAccount)
- `NetProfitLoss` must be computed after `TotalFeesCalculated` is set

### 5. Notes field in order create/edit views
- `Notes` property exists on `EntryOrder`, `StopLossOrder`, `TakeProfitOrder`
- Missing from all three create/edit XAML forms — data entered is silently dropped
- Add a `TextBox` bound to `Notes` in each order create/edit view

---

## 🟠 Cycle 2 — Usability & Completeness

### 6. Tags filter in toolbar
- `Trade.Tags` is a persisted free-text field (255 chars)
- Add a text search filter to the filter toolbar bound to `QueryVM.ByTags` or similar
- `TradeQueryParameters` may need a `Tags` search property added

### 7. Trade detail pane tabs
- The detail pane is getting tall and hard to scan
- Split into tabs: **Details** | **Orders** | **Images** | **Notes**
- Keep the trade header (account, symbol, status, direction) always visible above the tabs

### 8. TotalFeesCorrected + FundingFeeTotal entry in TradeEditView
- Both fields are currently display-only in the detail pane
- Add editable fields to `TradeEditView` so users can enter corrected fees and funding fees
- `TotalFeesCorrected` overrides `TotalFeesCalculated` in `EffectiveFees` when set

### 9. CancellationTime auto-sync in TradeEditView
- When `TradeStatus` is set to `Cancelled`, auto-fill `CancellationTime` with current UTC time
- When `CancellationTime` is cleared, reset `TradeStatus` away from `Cancelled`
- Bidirectional sync; implement in `TradeEditVM.part.cs`

### 10. MakerFeeRate / TakerFeeRate in TradingAccount create/edit view
- `MakerFeeRate` and `TakerFeeRate` are on the `TradingAccount` model
- Missing from `TradingAccountCreateView` and `TradingAccountEditView` XAML forms
- Required for fee auto-calculation (Cycle 1 item 4) to work end-to-end

---

## 🟡 Cycle 3 — Analytics & Reporting

### 11. Summary view
- `ShowSummaryCommand` is in the menu but not implemented (command is null / no-op)
- Show: total trades, win rate, average R, average NetProfitLoss, grouped by account / symbol / period
- New `SummaryWindow` + `SummaryWindowVM`; open from `ShowSummaryCommand`

### 12. RealizedR display
- `RealizedR` is calculated and persisted on `Trade`
- Add a column to the trade list DataGrid
- Show in trade detail pane alongside `PlannedRiskRewardRatio`

### 13. PlannedRiskRewardRatio visual indicator
- Add color or icon to the R:R column in the trade list: < 1.0 = red, 1.0–1.9 = yellow, ≥ 2.0 = green
- Use a CellStyle with DataTriggers or a converter

---

## 🟢 Cycle 4 — Import / Export

### 14. CSV export
- File → Export menu item exists but command is not implemented
- Export the currently filtered trade list to CSV
- Include all visible columns; let user choose file path via SaveFileDialog

### 15. ByBit CSV import
- File → Import menu item exists but command is not implemented
- Map ByBit trade history export format to Trade + Order entities
- Show a preview/mapping dialog before committing

### 16. Database backup UI
- `DatabaseBackupService` is fully implemented with ZIP + timestamp
- No UI exposes it — add a menu item or toolbar button to trigger manual backup
- Show last backup timestamp somewhere (status bar or About dialog)

---

## ⚪ Cycle 5 — Polish & Low Priority

### 17. TradeStatus color fix — Missed / Cancelled
- Current pink tones (#FFCEE3, #FFB0D2) for Missed/Cancelled are misleading (reads as "loss")
- Change to neutral tones: Missed → soft lavender (#E8E0F0), Cancelled → light grey (#D8D8D8)
- Edit `TradeStatusToColor.cs`

### 18. App settings dialog
- `AppSettings` + `UiCulture` exist with no UI to change them
- New `SettingsDialog` with: UI language/culture picker, default page size, database path override
- Add menu item under File or new Settings menu

### 19. TradeDetail popup improvements
- Add print/export button to `TradeDetailView`
- Add keyboard shortcut (e.g. `F4` or `Ctrl+D`) to open it from the main window

### 20. Keyboard shortcuts
- `Ctrl+N` — create trade
- `Delete` — delete selected trade (with confirmation)
- `Enter` — open trade detail
- `F5` — reload / apply filters
- Add via `InputBindings` on `MainWindow`

### 21. Charts view
- `ShowChartsCommand` in menu but not implemented
- Equity curve, win/loss pie, P&L by symbol bar chart
- Low priority until Summary view (Cycle 3) is stable
