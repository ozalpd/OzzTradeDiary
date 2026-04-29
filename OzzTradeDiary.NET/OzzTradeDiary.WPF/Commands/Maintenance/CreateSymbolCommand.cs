using System.Windows;
using TD.AppInfra.Services;
using TD.i18n;
using TD.WPF.Services;
using TD.WPF.ViewModels;

namespace TD.WPF.Commands.Maintenance
{
    internal class CreateSymbolCommand : AbstractCommand
    {
        private readonly AbstractDiaryVM _viewModel;
        private readonly IWindowDialogService _windowDialogService;

        public CreateSymbolCommand(AbstractDiaryVM viewModel, IWindowDialogService windowDialogService)
        {
            _viewModel = viewModel;
            _windowDialogService = windowDialogService;
        }

        public override async void Execute(object? parameter)
        {
            if (parameter is not Window owner)
                return;

            try
            {
                var exchangeLookupService = new ExchangeLookupService(_viewModel.ExchangeRepository);
                var currencyLookupService = new CurrencyLookupService(_viewModel.CurrencyRepository);
                var dialogResult = _windowDialogService.ShowSymbolCreateDialog(owner, exchangeLookupService, currencyLookupService);

                if (dialogResult.IsConfirmed && dialogResult.Symbol is not null)
                {
                    var symbol = dialogResult.Symbol;
                    await _viewModel.SaveSymbolAsync(symbol);
                    await _viewModel.LoadSymbolsAsync();

                    _viewModel.SelectedSymbol = _viewModel.Symbols.FirstOrDefault(x => x.Id == symbol.Id);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(MessageStrings.SaveOperationFailed, CommonStrings.AppTitle, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
