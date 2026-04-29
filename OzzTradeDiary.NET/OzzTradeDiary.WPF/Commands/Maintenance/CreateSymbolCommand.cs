using System.Windows;
using TD.AppInfra.Services;
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
                    var Symbol = dialogResult.Symbol;
                    _viewModel.Symbols.Add(Symbol);
                    await _viewModel.SaveSymbolsAsync();
                    await _viewModel.LoadSymbolsAsync();

                    _viewModel.SelectedSymbol = _viewModel.Symbols.FirstOrDefault(x => x.Id == Symbol.Id);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Create trading account failed.\n{ex.Message}", "Ozz Trade Diary", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
