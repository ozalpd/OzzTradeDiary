using TD.AppInfra.ViewModels;
using TD.Models;

namespace TD.WPF.ViewModels.Maintenance
{
    internal class SymbolEditVM : AbstractCreateEditVM
    {
        private Symbol _symbol;
        public Symbol Symbol => _symbol;

        public SymbolEditVM(Symbol symbol)
        {
            _symbol = symbol;
        }

        public string Ticker=> _symbol.Ticker;

        public string TickerFull => _symbol.TickerFull;

        public string ExchangeCode => _symbol.Exchange?.ExchangeCode ?? string.Empty;

        public MarketType MarketType => _symbol.MarketType;

        public string? BaseCurrency => _symbol.BaseCurrency;

        public string PriceCurrency => _symbol.PriceCurrency;

        public bool IsActive
        {
            get { return _symbol.IsActive; }
            set
            {
                if (_symbol.IsActive != value)
                {
                    _symbol.IsActive = value;
                    RaisePropertyChanged(nameof(IsActive));
                }
            }
        }

        public int DisplayOrder
        {
            get { return _symbol.DisplayOrder; }
            set
            {
                if (_symbol.DisplayOrder != value)
                {
                    _symbol.DisplayOrder = value;
                    RaisePropertyChanged(nameof(DisplayOrder));
                    ValidateProperty(_symbol, nameof(DisplayOrder));
                }
            }
        }

        public string? Description
        {
            get { return _symbol.Description; }
            set
            {
                if (_symbol.Description != value)
                {
                    _symbol.Description = value;
                    RaisePropertyChanged(nameof(Description));
                }
            }
        }
    }
}
