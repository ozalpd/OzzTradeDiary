using System.Windows;
using TD.AppInfra.Commands;
using TD.RepositoryContracts;
using TD.WPF.Views.Maintenance;

namespace TD.WPF.Commands;

internal class ShowMaintenanceCommand : AbstractCommand
{
    private readonly ICurrencyRepository _currencyRepository;
    private readonly IExchangeRepository _exchangeRepository;
    private readonly ISymbolRepository _symbolRepository;
    private readonly ITradingAccountRepository _tradingAccountRepository;
    private MaintenanceWindow? _maintenanceWindow;

    public ShowMaintenanceCommand(ICurrencyRepository currencyRepository,
                                  IExchangeRepository exchangeRepository,
                                  ISymbolRepository symbolRepository,
                                  ITradingAccountRepository tradingAccountRepository)
    {
        _currencyRepository = currencyRepository;
        _exchangeRepository = exchangeRepository;
        _symbolRepository = symbolRepository;
        _tradingAccountRepository = tradingAccountRepository;
    }

    public override void Execute(object? parameter)
    {
        if (_maintenanceWindow == null || !_maintenanceWindow.IsLoaded)
        {
            _maintenanceWindow = new MaintenanceWindow(_currencyRepository, _exchangeRepository,
                                                       _symbolRepository, _tradingAccountRepository);
            _maintenanceWindow.Closed += (s, e) => _maintenanceWindow = null;
            _maintenanceWindow.Show();
        }
        else
        {
            if (_maintenanceWindow.WindowState == WindowState.Minimized)
                _maintenanceWindow.WindowState = WindowState.Normal;
            _maintenanceWindow.Activate();
            _maintenanceWindow.Focus();
        }
    }
}
