using TD.Models;
using TD.WPF.Models;

namespace TD.WPF.ViewModels.Maintenance
{
    internal class CurrencyCreateVM : AbstractEditVM
    {
        private Currency _currency;
        public Currency Currency => _currency;
        public CurrencyCreateVM()
        {
            var appSettings = AppSettings.GetAppSettings();
            var databasePath = appSettings.DatabasePath;

            _currency = new Currency();
            IsActive = true;
            DisplayOrder = 1000;
            ValidateModel(_currency);
        }


        public string CurrencyTicker
        {
            get { return _currency.CurrencyTicker; }
            set
            {
                if (_currency.CurrencyTicker != value)
                {
                    _currency.CurrencyTicker = value;
                    RaisePropertyChanged(nameof(CurrencyTicker));
                    ValidateProperty(_currency, nameof(CurrencyTicker));
                }
            }
        }

        public string? Description
        {
            get { return _currency.Description; }
            set
            {
                if (_currency.Description != value)
                {
                    _currency.Description = value;
                    RaisePropertyChanged(nameof(Description));
                    ValidateProperty(_currency, nameof(Description));
                }
            }
        }

        public int DisplayOrder
        {
            get { return _currency.DisplayOrder; }
            set
            {
                if (_currency.DisplayOrder != value)
                {
                    _currency.DisplayOrder = value;
                    RaisePropertyChanged(nameof(DisplayOrder));
                    ValidateProperty(_currency, nameof(DisplayOrder));
                }
            }
        }

        public bool IsActive
        {
            get { return _currency.IsActive; }
            set
            {
                if (_currency.IsActive != value)
                {
                    _currency.IsActive = value;
                    RaisePropertyChanged(nameof(IsActive));
                }
            }
        }
    }
}
