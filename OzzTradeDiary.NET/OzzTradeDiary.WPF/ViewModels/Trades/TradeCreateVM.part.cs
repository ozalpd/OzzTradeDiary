using System.Collections.ObjectModel;
using TD.Models;

namespace TD.WPF.ViewModels.Trades
{
    public partial class TradeCreateVM
    {
        IReadOnlyList<Symbol> _allSymbols;

        partial void OnInitialized()
        {
            PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(OrderQuantity) || e.PropertyName == nameof(PlannedEntryPrice))
            {
                if (!_calculatingOrderQuantity)
                    RaisePropertyChanged(nameof(PlannedPositionValue));
            }

            if (e.PropertyName == nameof(TradingAccountId))
            {
                SetSymbols();
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
            TradingAccount? _selectedTradingAccount = TradingAccountId > 0 && TradingAccounts != null
                                                    ? TradingAccounts.FirstOrDefault(a => a.Id == TradingAccountId)
                                                    : null;
            if (Symbols == null)
                return;

            _ = SetAllSymbolsAsync();
            if (_allSymbols == null)
                return;

            var accountSymbols = _selectedTradingAccount != null
                               ? _allSymbols.Where(s => s.ExchangeId == _selectedTradingAccount.ExchangeId)
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
