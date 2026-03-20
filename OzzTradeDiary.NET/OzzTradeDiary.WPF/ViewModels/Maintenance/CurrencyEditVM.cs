using TD.Models;

namespace TD.WPF.ViewModels.Maintenance
{
    internal class CurrencyEditVM : AbstractEditVM
    {
        private Currency _currency;
        public Currency Currency => _currency;

        public CurrencyEditVM(Currency currency)
        {
            _currency = currency;
        }


        public string CurrencyTicker
        {
            get { return _currency.CurrencyTicker; }
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
