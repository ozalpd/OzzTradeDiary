using TD.AppInfra.ViewModels;
using TD.Models;

namespace TD.WPF.ViewModels.Maintenance
{
    internal class TradingAccountEditVM : AbstractCreateEditVM
    {
        private TradingAccount _tradingAccount;
        public TradingAccount TradingAccount => _tradingAccount;

        public TradingAccountEditVM(TradingAccount tradingAccount)
        {
            _tradingAccount = tradingAccount;
        }

        public string Title => _tradingAccount.Title;

        public string ExchangeCode => _tradingAccount.Exchange?.ExchangeCode ?? string.Empty;

        public bool IsActive
        {
            get { return _tradingAccount.IsActive; }
            set
            {
                if (_tradingAccount.IsActive != value)
                {
                    _tradingAccount.IsActive = value;
                    RaisePropertyChanged(nameof(IsActive));
                }
            }
        }

        public int DisplayOrder
        {
            get { return _tradingAccount.DisplayOrder; }
            set
            {
                if (_tradingAccount.DisplayOrder != value)
                {
                    _tradingAccount.DisplayOrder = value;
                    RaisePropertyChanged(nameof(DisplayOrder));
                    ValidateProperty(_tradingAccount, nameof(DisplayOrder));
                }
            }
        }

        public string? Notes
        {
            get { return _tradingAccount.Notes; }
            set
            {
                if (_tradingAccount.Notes != value)
                {
                    _tradingAccount.Notes = value;
                    RaisePropertyChanged(nameof(Notes));
                }
            }
        }

        public bool ValidateModel()
        {
            return ValidateModel(_tradingAccount);
        }
    }
}
