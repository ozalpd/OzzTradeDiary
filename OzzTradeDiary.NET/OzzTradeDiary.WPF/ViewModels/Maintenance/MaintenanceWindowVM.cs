using System.Collections.Specialized;
using TD.WPF.Commands;
using TD.WPF.Models;

namespace TD.WPF.ViewModels.Maintenance
{
    internal class MaintenanceWindowVM : AbstractDiaryVM
    {
        private readonly DeleteExchangeCommand _deleteExchangeCommand;

        public MaintenanceWindowVM()
        {
            var appSettings = AppSettings.GetAppSettings();
            var databasePath = appSettings.DatabasePath;

            _deleteExchangeCommand = new DeleteExchangeCommand(this);
            DeleteExchangeCommand = _deleteExchangeCommand;

            PropertyChanged += OnPropertyChanged;
            Symbols.CollectionChanged += OnDependencyCollectionChanged;
            TradingAccounts.CollectionChanged += OnDependencyCollectionChanged;
        }

        public DeleteExchangeCommand DeleteExchangeCommand { get; }

        public bool CanDeleteSelectedExchange()
        {
            if (SelectedExchange is null)
                return false;

            var exchangeId = SelectedExchange.Id;
            return !Symbols.Any(symbol => symbol.ExchangeId == exchangeId)
                && !TradingAccounts.Any(account => account.ExchangeId == exchangeId);
        }

        public void DeleteSelectedExchange()
        {
            if (!CanDeleteSelectedExchange() || SelectedExchange is null)
                return;

            var exchangeToDelete = SelectedExchange;

            if (exchangeToDelete.Id > 0)
            {
                var deleted = ExchangeRepository.DeleteAsync(exchangeToDelete.Id).GetAwaiter().GetResult();
                if (!deleted)
                    return;
            }

            Exchanges.Remove(exchangeToDelete);
            SelectedExchange = null;
            _deleteExchangeCommand.RaiseCanExecuteChanged();
        }

        public async Task LoadAllAsync()
        {
            await LoadCurrenciesAsync();
            await LoadExchangesAsync();
            await LoadTradingAccountsAsync();
            await LoadSymbolsAsync();
            _deleteExchangeCommand.RaiseCanExecuteChanged();
        }

        private void OnPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedExchange))
                _deleteExchangeCommand.RaiseCanExecuteChanged();
        }

        private void OnDependencyCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            _deleteExchangeCommand.RaiseCanExecuteChanged();
        }
    }
}
