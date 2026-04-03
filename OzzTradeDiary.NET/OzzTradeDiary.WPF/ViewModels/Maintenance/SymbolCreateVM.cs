using System.Collections.ObjectModel;
using TD.Models;
using TD.SQLite;
using TD.Extensions;
using TD.WPF.Models;

namespace TD.WPF.ViewModels.Maintenance
{
    internal class SymbolCreateVM : AbstractEditVM
    {
        private const string NoBaseCurrencyDisplayText = "No Base Currency";
        private readonly IReadOnlyList<MarketTypeValueItem> _marketTypeValues;
        private readonly ObservableCollection<BaseCurrencyValueItem> _baseCurrencyValues;
        private Symbol _symbol;
        public Symbol Symbol => _symbol;

        public SymbolCreateVM()
        {
            var appSettings = AppSettings.GetAppSettings();
            var databasePath = appSettings.DatabasePath;

            CurrencyRepository = new CurrencyRepository(databasePath);
            ExchangeRepository = new ExchangeRepository(databasePath);
            Currencies = new ObservableCollection<Currency>();
            Exchanges = new ObservableCollection<Exchange>();
            _baseCurrencyValues = new ObservableCollection<BaseCurrencyValueItem>
            {
                new BaseCurrencyValueItem { Value = null, DisplayValue = NoBaseCurrencyDisplayText }
            };

            _marketTypeValues = EnumExtension
                .GetValues<MarketType>()
                .Select(x => new MarketTypeValueItem { Value = x, DisplayValue = x.GetDisplayValue() })
                .ToList();

            _symbol = new Symbol();
            IsActive = true;
            DisplayOrder = 1000;
            ValidateModel(_symbol);
        }

        public async Task LoadAllAsync()
        {
            await LoadCurrenciesAsync();
            await LoadExchangesAsync();
        }

        public async Task LoadCurrenciesAsync()
        {
            var items = await CurrencyRepository.GetAllAsync(isActive: true);
            Currencies.Clear();
            _baseCurrencyValues.Clear();
            _baseCurrencyValues.Add(new BaseCurrencyValueItem { Value = null, DisplayValue = NoBaseCurrencyDisplayText });

            foreach (var item in items)
            {
                Currencies.Add(item);
                _baseCurrencyValues.Add(new BaseCurrencyValueItem { Value = item.CurrencyTicker, DisplayValue = item.CurrencyTicker });
            }
        }

        public async Task LoadExchangesAsync()
        {
            var items = await ExchangeRepository.GetAllAsync(isActive: true);
            Exchanges.Clear();
            foreach (var item in items)
            {
                Exchanges.Add(item);
            }
        }

        public ICurrencyRepository CurrencyRepository { get; }
        public ObservableCollection<Currency> Currencies { get; }

        public IExchangeRepository ExchangeRepository { get; }
        public ObservableCollection<Exchange> Exchanges { get; }

        public Exchange? SelectedExchange
        {
            get { return _selectedExchange; }
            set
            {
                if (value != null && _selectedExchange?.Id != value.Id)
                {
                    _selectedExchange = value;
                    RaisePropertyChanged(nameof(SelectedExchange));
                    ExchangeId = _selectedExchange.Id;
                    SetTickerFull();

                    if (string.IsNullOrWhiteSpace(PriceCurrency) && SelectedExchange != null)
                    {
                        PriceCurrency = SelectedExchange.DefaultCurrency ?? string.Empty;
                    }
                }
            }
        }
        Exchange? _selectedExchange;


        public string Ticker
        {
            get { return _symbol.Ticker; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && !value.Equals(_symbol.Ticker, StringComparison.OrdinalIgnoreCase))
                {
                    _symbol.Ticker = value.ToUpperInvariant();
                    RaisePropertyChanged(nameof(Ticker));
                    ValidateProperty(_symbol, nameof(Ticker));
                    SetTickerFull();
                    TryToCatchCurrency();
                }
            }
        }

        public int ExchangeId
        {
            get { return _symbol.ExchangeId; }
            set
            {
                if (_symbol.ExchangeId != value)
                {
                    _symbol.ExchangeId = value;
                    RaisePropertyChanged(nameof(ExchangeId));
                    ValidateProperty(_symbol, nameof(ExchangeId));
                }
            }
        }

        /// <summary>
        /// Contains a string (CurrencyTicker) that representing the symbol's base currency if the instrument is a Crypto pair or a Forex pair or a derivative based on such a pair.
        /// Otherwise, it contains empty string. For example, this property holds "GBP" for "GBPJPY", "BTC" for "BTCUSDT" and empty string for "NASDAQ:MSFT".
        /// </summary>
        public string? BaseCurrency
        {
            get { return _symbol.BaseCurrency; }
            set
            {
                if (_symbol.BaseCurrency != value)
                {
                    _symbol.BaseCurrency = value;
                    RaisePropertyChanged(nameof(BaseCurrency));
                    ValidateProperty(_symbol, nameof(BaseCurrency));
                }
            }
        }

        /// <summary>
        /// Contains a string (CurrencyTicker) that representing currency of the symbol's price. For example, this property holds "JPY" for "GBPJPY", "USDT" for "BTCUSDT" and "USD" for "NASDAQ:MSFT".
        /// </summary>
        public string PriceCurrency
        {
            get { return _symbol.PriceCurrency; }
            set
            {
                if (_symbol.PriceCurrency != value)
                {
                    _symbol.PriceCurrency = value;
                    RaisePropertyChanged(nameof(PriceCurrency));
                    ValidateProperty(_symbol, nameof(PriceCurrency));
                }
            }
        }

        /// <summary>
        /// Symbol name with exchange prefix, e.g. 'BYBIT:BTCUSD.P'
        /// </summary>
        public string TickerFull
        {
            get { return _symbol.TickerFull; }
            set
            {
                if (_symbol.TickerFull != value)
                {
                    _symbol.TickerFull = value;
                    RaisePropertyChanged(nameof(TickerFull));
                    ValidateProperty(_symbol, nameof(TickerFull));
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
                    ValidateProperty(_symbol, nameof(Description));
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

        public MarketType MarketType
        {
            get { return _symbol.MarketType; }
            set
            {
                if (_symbol.MarketType != value)
                {
                    _symbol.MarketType = value;
                    RaisePropertyChanged(nameof(MarketType));
                    TryToCatchCurrency();
                    ValidateProperty(_symbol, nameof(MarketType));
                }
            }
        }

        public ObservableCollection<BaseCurrencyValueItem> BaseCurrencyValues
        {
            get
            {
                return _baseCurrencyValues;
            }
        }

        public IEnumerable<MarketTypeValueItem> MarketTypeValues
        {
            get
            {
                return _marketTypeValues;
            }
        }



        public void SetTickerFull()
        {
            if (string.IsNullOrWhiteSpace(Ticker))
                return;

            if (SelectedExchange != null)
            {
                TickerFull = $"{SelectedExchange.ExchangeCode}:{Ticker}";
            }
            else
            {
                TickerFull = Ticker;
            }
        }

        private void TryToCatchCurrency()
        {
            if (string.IsNullOrWhiteSpace(Ticker) || Ticker.Length < 4)
                return;

            string candidate = string.Empty;
            Currency? currency = null;
            string tickerWoPerp = Ticker.EndsWith(".P", StringComparison.OrdinalIgnoreCase)
                                ? Ticker[..^2]
                                : Ticker;

            if (tickerWoPerp.Length > 4)
            {
                // First check last four characters of the ticker.
                candidate = tickerWoPerp[^4..];
                currency = Currencies.FirstOrDefault(x => x.CurrencyTicker.Equals(candidate, StringComparison.OrdinalIgnoreCase));
            }

            if (currency == null)
            {
                //then check last three characters of the ticker.
                candidate = tickerWoPerp[^3..];
                currency = Currencies.FirstOrDefault(x => x.CurrencyTicker.Equals(candidate, StringComparison.OrdinalIgnoreCase));
            }

            if (currency != null)
            {
                PriceCurrency = currency.CurrencyTicker;
            }

            bool needToCatchBaseCurrency = MarketType == MarketType.Forex
                                 || MarketType == MarketType.Crypto
                                 || MarketType == MarketType.CryptoPerpetual;
            if (needToCatchBaseCurrency)
            {
                currency = null;
                if (tickerWoPerp.Length > 5)
                {
                    candidate = tickerWoPerp[..4];
                    currency = Currencies.FirstOrDefault(x => x.CurrencyTicker.Equals(candidate, StringComparison.OrdinalIgnoreCase));
                }

                if (currency == null)
                {
                    candidate = tickerWoPerp[..3];
                    currency = Currencies.FirstOrDefault(x => x.CurrencyTicker.Equals(candidate, StringComparison.OrdinalIgnoreCase));
                }

                if (currency != null)
                {
                    BaseCurrency = currency.CurrencyTicker;
                }
                else
                {
                    BaseCurrency = string.Empty;
                }
            }
        }


        public sealed class BaseCurrencyValueItem
        {
            public string? Value { get; set; }
            public string DisplayValue { get; set; } = string.Empty;
        }

        public sealed class MarketTypeValueItem
        {
            public MarketType Value { get; set; }
            public string DisplayValue { get; set; } = string.Empty;
        }
    }
}
