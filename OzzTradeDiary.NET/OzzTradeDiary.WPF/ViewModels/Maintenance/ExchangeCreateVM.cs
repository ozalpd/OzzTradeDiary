using TD.Models;
using TD.WPF.Models;

namespace TD.WPF.ViewModels.Maintenance
{
    internal class ExchangeCreateVM : AbstractEditVM
    {
        private Exchange _exchange;
        public Exchange Exchange => _exchange;
        public ExchangeCreateVM()
        {
            var appSettings = AppSettings.GetAppSettings();
            var databasePath = appSettings.DatabasePath;

            _exchange = new Exchange();
            IsActive = true;
            DisplayOrder = 1000;
            ValidateModel(_exchange);
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
            set
            {
                if (_exchange.ExchangeCode != value)
                {
                    _exchange.ExchangeCode = value;
                    RaisePropertyChanged(nameof(ExchangeCode));
                    ValidateProperty(_exchange, nameof(ExchangeCode));
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
