using TD.Models;

namespace TD.WPF.ViewModels.Maintenance
{
    internal class ExchangeEditVM : AbstractEditVM
    {
        private Exchange _exchange;
        public Exchange Exchange => _exchange;

        public ExchangeEditVM(Exchange exchange)
        {
            _exchange = exchange;
        }


        public string ExchangeName
        {
            get { return _exchange.ExchangeName; }
            set
            {
                if (_exchange.ExchangeName != value)
                {
                    _exchange.ExchangeName = value;
                    RaisePropertyChanged(nameof(ExchangeName));
                    ValidateProperty(_exchange, nameof(ExchangeName));
                }
            }
        }

        public string ExchangeCode
        {
            get { return _exchange.ExchangeCode; }
        }

        public string? DefaultCurrency
        {
            get { return _exchange.DefaultCurrency; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && !value.Equals(_exchange.DefaultCurrency, StringComparison.OrdinalIgnoreCase))
                {
                    _exchange.DefaultCurrency = value.ToUpperInvariant();
                    RaisePropertyChanged(nameof(DefaultCurrency));
                    ValidateProperty(_exchange, nameof(DefaultCurrency));
                }
                else if (string.IsNullOrWhiteSpace(value))
                {
                    _exchange.DefaultCurrency = null;
                    RaisePropertyChanged(nameof(DefaultCurrency));
                    ValidateProperty(_exchange, nameof(DefaultCurrency));
                }
            }
        }

        public int DisplayOrder
        {
            get { return _exchange.DisplayOrder; }
            set
            {
                if (_exchange.DisplayOrder != value)
                {
                    _exchange.DisplayOrder = value;
                    RaisePropertyChanged(nameof(DisplayOrder));
                    ValidateProperty(_exchange, nameof(DisplayOrder));
                }
            }
        }

        public bool IsActive
        {
            get { return _exchange.IsActive; }
            set
            {
                if (_exchange.IsActive != value)
                {
                    _exchange.IsActive = value;
                    RaisePropertyChanged(nameof(IsActive));
                }
            }
        }
    }
}
