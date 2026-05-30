using TD.Extensions;
using TD.Models;
using static TD.Extensions.EnumExtension;

namespace TD.WPF.ViewModels.Trades
{
    public partial class TradeCreateVM
    {
        public IEnumerable<EnumValueItem<EntryOrderType>> EntryOrderTypeValues { get; private set; }
        IReadOnlyList<Symbol> _allSymbols;

        partial void OnInitialized()
        {
            EntryOrderTypeValues = GetValues<EntryOrderType>();
            PropertyChanged += OnPropertyChanged;
            EntryOrderType = EntryOrderType.Market;
        }

        private void OnPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(OrderQuantity) || e.PropertyName == nameof(PlannedEntryPrice))
            {
                if (!_calculatingOrderQuantity)
                    RaisePropertyChanged(nameof(PlannedPositionValue));
            }

            if (e.PropertyName == nameof(PlannedPositionValue) || e.PropertyName == nameof(PlannedTP) || e.PropertyName == nameof(PlannedSL))
            {
                RaisePropertyChanged(nameof(PlannedRiskRewardRatio));
                RaisePropertyChanged(nameof(PlannedProfit));
            }

            if (e.PropertyName == nameof(TradingAccountId))
            {
                SetSymbols();
            }

            if (e.PropertyName == nameof(SymbolId))
            {
                SetMarketType();
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



        public void AddOrders()
        {
            AddEntryOrder();

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
                               ? _allSymbols.Where(s => s.ExchangeId == selectedAccount.ExchangeId)
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
