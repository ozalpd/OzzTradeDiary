using TD.Models;

namespace TD.WPF.ViewModels.Maintenance
{
    public partial class SymbolCreateVM
    {
        partial void OnInitialized()
        {
            PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ExchangeId) && ExchangeId > 0)
            {
                Exchange? exchange = Exchanges.FirstOrDefault(x => x.Id == ExchangeId);
                SelectedExchange = exchange;

                SetTickerFull();
            }

            if (e.PropertyName == nameof(Ticker))
            {
                TryToCatchCurrency();
                SetTickerFull();
            }

            if (e.PropertyName == nameof(MarketType))
            {
                TryToCatchCurrency();
            }
        }

        public Exchange? SelectedExchange
        {
            get { return _selectedExchange; }
            set
            {
                if (value != null && _selectedExchange?.Id != value.Id)
                {
                    _selectedExchange = value;
                    RaisePropertyChanged(nameof(SelectedExchange));

                    if (PriceCurrencyId == 0 && SelectedExchange != null)
                    {
                        PriceCurrencyId = SelectedExchange.DefaultCurrencyId;
                    }
                }
            }
        }
        Exchange? _selectedExchange;

        private void SetTickerFull()
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
                PriceCurrencyId = currency.Id;
            }

            bool needToCatchBaseCurrency = MarketType == MarketType.Unspecified
                                 || MarketType == MarketType.Forex
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
    }
}
