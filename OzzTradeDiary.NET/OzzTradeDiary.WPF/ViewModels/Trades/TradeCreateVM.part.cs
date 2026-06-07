using TD.Extensions;
using TD.Models;
using static TD.Extensions.EnumExtension;

namespace TD.WPF.ViewModels.Trades
{
    public partial class TradeCreateVM
    {
        IReadOnlyList<Symbol> _allSymbols = null;

        partial void OnInitialized()
        {
            EntryOrderTypeValues = GetValues<EntryOrderType>();
            ExitOrderTypeValues = GetValues<ExitOrderType>();
            ExitOrderForTpValues = ExitOrderTypeValues.Where(v => v.Value <= ExitOrderType.TrailingStop).ToList();
            PropertyChanged += OnPropertyChanged;

            EntryOrderType = EntryOrderType.Limit;
            StopLossOrderType = ExitOrderType.Market;
            TakeProfitOrderType = ExitOrderType.Limit;
        }

        public IEnumerable<EnumValueItem<EntryOrderType>> EntryOrderTypeValues { get; private set; } = Array.Empty<EnumValueItem<EntryOrderType>>();
        public IEnumerable<EnumValueItem<ExitOrderType>> ExitOrderTypeValues { get; private set; } = Array.Empty<EnumValueItem<ExitOrderType>>();
        public IEnumerable<EnumValueItem<ExitOrderType>> ExitOrderForTpValues { get; private set; } = Array.Empty<EnumValueItem<ExitOrderType>>();


        private void OnPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(OrderQuantity) || e.PropertyName == nameof(PlannedEntryPrice))
            {
                if (!_calculatingOrderQuantity)
                    RaisePropertyChanged(nameof(PlannedPositionValue));
            }

            if (e.PropertyName == nameof(PlannedTP) || e.PropertyName == nameof(OrderQuantity))
            {
                RaisePropertyChanged(nameof(PlannedRiskRewardRatio));
                RaisePropertyChanged(nameof(PlannedProfit));
            }

            if (e.PropertyName == nameof(PlannedSL) || e.PropertyName == nameof(OrderQuantity))
            {
                RaisePropertyChanged(nameof(PlannedRiskRewardRatio));
                RaisePropertyChanged(nameof(PlannedRiskAmount));
            }

            if (e.PropertyName == nameof(TradingAccountId))
            {
                SetSymbols();
            }

            if (e.PropertyName == nameof(SymbolId))
            {
                SetMarketType();
                ValidateProperty(Trade, nameof(MarketType));
            }

            if (e.PropertyName == nameof(TradeDirection) || e.PropertyName == nameof(TradeStatus) || e.PropertyName == nameof(PlannedEntryPrice))
            {
                RaisePropertyChanged(nameof(PlannedRiskRewardRatio));
                RaisePropertyChanged(nameof(PlannedRiskAmount));
                RaisePropertyChanged(nameof(PlannedProfit));
                ValidateProperty(Trade, nameof(PlannedTP));
                ValidateProperty(Trade, nameof(PlannedSL));
            }

            if (e.PropertyName == nameof(TradeStatus))
            {
                ValidateProperty(_trade, nameof(EntryTime));
            }

            if (e.PropertyName == nameof(ExitTime))
            {
                ValidateProperty(_trade, nameof(EntryTime));
            }

            if (e.PropertyName == nameof(EntryTime))
            {
                ValidateProperty(_trade, nameof(ExitTime));
                ValidateProperty(_trade, nameof(TradeStatus));
            }
        }

        public EntryOrderType EntryOrderType
        {
            get => _entryOrderType;
            set
            {
                if (_entryOrderType != value)
                {
                    _entryOrderType = value;
                    RaisePropertyChanged(nameof(EntryOrderType));
                }
            }
        }
        EntryOrderType _entryOrderType;

        public ExitOrderType StopLossOrderType
        {
            get => _stopLossOrderType;
            set
            {
                if (_stopLossOrderType != value)
                {
                    _stopLossOrderType = value;
                    RaisePropertyChanged(nameof(StopLossOrderType));
                }
            }
        }
        ExitOrderType _stopLossOrderType;

        public ExitOrderType TakeProfitOrderType
        {
            get => _takeProfitOrderType;
            set
            {
                if (_takeProfitOrderType != value)
                {
                    _takeProfitOrderType = value;
                    RaisePropertyChanged(nameof(TakeProfitOrderType));
                }
            }
        }
        ExitOrderType _takeProfitOrderType;



        public void AddOrders()
        {
            AddEntryOrder();
            AddStopLossOrder();
            AddTakeProfitOrder();
        }

        private void AddEntryOrder()
        {
            if (PlannedEntryPrice == null || OrderQuantity == null)
                return;

            var entryOrder = new EntryOrder
            {
                Id = 0,
                TradeId = Trade.Id,
                OrderType = EntryOrderType,
                OrderQuantity = OrderQuantity,
                OrderPrice = PlannedEntryPrice.Value
            };
            Trade.EntryOrders.Add(entryOrder);
        }

        private void AddStopLossOrder()
        {
            if (PlannedSL == null || OrderQuantity == null)
                return;
            var slOrder = new StopLossOrder
            {
                Id = 0,
                TradeId = Trade.Id,
                OrderType = StopLossOrderType,
                OrderQuantity = OrderQuantity,
                OrderPrice = PlannedSL.Value
            };
            Trade.StopLossOrders.Add(slOrder);
        }

        private void AddTakeProfitOrder()
        {
            if (PlannedTP == null || OrderQuantity == null)
                return;
            var tpOrder = new TakeProfitOrder
            {
                Id = 0,
                TradeId = Trade.Id,
                OrderType = TakeProfitOrderType,
                OrderQuantity = OrderQuantity,
                OrderPrice = PlannedTP.Value
            };
            Trade.TakeProfitOrders.Add(tpOrder);
        }

        public void SetMarketType()
        {
            var symbol = _allSymbols.FirstOrDefault(s => s.Id == SymbolId);
            if (symbol != null)
            {
                Trade.MarketType = symbol.MarketType;
                RaisePropertyChanged(nameof(MarketType));
            }
        }

        public void SetOrderQuantity(decimal positionValue)
        {
            Trade.SetQuantity(positionValue, planned: true);
            RaisePropertyChanged(nameof(OrderQuantity));
        }

        public void SetOrderQuantity(string positionValue)
        {
            decimal posValue = 0;
            if (decimal.TryParse(positionValue, out posValue))
            {
                _calculatingOrderQuantity = true;
                SetOrderQuantity(posValue);
                _calculatingOrderQuantity = false;
            }
        }
        bool _calculatingOrderQuantity = false;

        private void SetSymbols()
        {
            TradingAccount? selectedAccount = TradingAccountId > 0 && TradingAccounts != null
                                                    ? TradingAccounts.FirstOrDefault(a => a.Id == TradingAccountId)
                                                    : null;
            if (Symbols == null)
                return;

            _ = SetAllSymbolsAsync();
            if (_allSymbols == null)
                return;

            var accountSymbols = selectedAccount != null
                               ? _allSymbols.Where(s => s.ExchangeId == selectedAccount.ExchangeId
                                                     && s.MarketType == selectedAccount.MarketType)
                               : _allSymbols;

            Symbols.Clear();
            foreach (var item in accountSymbols)
            {
                Symbols.Add(item);
            }
        }

        public async Task SetAllSymbolsAsync()
        {
            if (_allSymbols == null)
                _allSymbols = await _symbolLookupService.GetSymbolsAsync(isActive: true);
        }
    }
}
