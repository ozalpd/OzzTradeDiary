using System.Collections.ObjectModel;
using TD.Models;
using TD.SQLite;
using TD.WPF.Models;

namespace TD.WPF.ViewModels.Maintenance
{
    internal class CreateTradingAccountVM : AbstractDataErrorInfoVM
    {
        public SqliteDatabaseMetadataRepository MetadataRepository { get; }

        private TradingAccount _tradingAccount;
        public TradingAccount TradingAccount => _tradingAccount;
        public CreateTradingAccountVM()
        {
            var appSettings = AppSettings.GetAppSettings();
            var databasePath = appSettings.DatabasePath;

            MetadataRepository = new SqliteDatabaseMetadataRepository(databasePath);
            ExchangeRepository = new SqliteDatabaseExchangeRepository(databasePath, MetadataRepository);

            Exchanges = new ObservableCollection<Exchange>();
            _tradingAccount = new TradingAccount();
            IsActive = true;
            DisplayOrder = 1000;
            ErrorsChanged += (_, _) => RaisePropertyChanged(nameof(IsValid));
            ValidateModel(_tradingAccount);
        }

        public IDatabaseExchangeRepository ExchangeRepository { get; }
        public ObservableCollection<Exchange> Exchanges { get; }

        public async Task LoadExchangesAsync()
        {
            var items = await ExchangeRepository.GetAllAsync(isActive:true);
            Exchanges.Clear();
            foreach (var item in items)
            {
                Exchanges.Add(item);
            }
        }

        public bool IsValid => !HasErrors;

        public string Title
        {
            get { return _tradingAccount.Title; }
            set
            {
                if (_tradingAccount.Title != value)
                {
                    _tradingAccount.Title = value;
                    RaisePropertyChanged(nameof(Title));
                    ValidateProperty(_tradingAccount, nameof(Title));
                }
            }
        }

        public int ExchangeId
        {
            get { return _tradingAccount.ExchangeId; }
            set
            {
                if (_tradingAccount.ExchangeId != value)
                {
                    _tradingAccount.ExchangeId = value;
                    RaisePropertyChanged(nameof(ExchangeId));
                    ValidateProperty(_tradingAccount, nameof(ExchangeId));
                }
            }
        }

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
    }
}
