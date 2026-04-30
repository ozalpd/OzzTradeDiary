using System.Windows;
using TD.AppInfra.Services;
using TD.AppInfra.ViewModels;
using TD.i18n;
using TD.Models;
using TD.WPF.Services;

namespace TD.WPF.Commands.Maintenance
{
    internal class SymbolCreateCommand : AbstractCommand
    {
        private readonly ISymbolCreationContext _viewModel;
        private readonly IWindowDialogService _windowDialogService;
        private readonly IExchangeLookupService _exchangeLookupService;
        private readonly ICurrencyLookupService _currencyLookupService;

        public SymbolCreateCommand(ISymbolCreationContext viewModel, IWindowDialogService windowDialogService,
            IExchangeLookupService exchangeLookupService, ICurrencyLookupService currencyLookupService)
        {
            _viewModel = viewModel;
            _windowDialogService = windowDialogService;
            _exchangeLookupService = exchangeLookupService;
            _currencyLookupService = currencyLookupService;
        }

        public override async void Execute(object? parameter)
        {
            if (parameter is not Window owner)
                return;

            try
            {
                var dialogResult = _windowDialogService.ShowSymbolCreateDialog(owner, _exchangeLookupService, _currencyLookupService, PreselectedExchange);

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
                string innerExceptionMsg = ex.InnerException != null ? $"\n{ex.InnerException.Message}" : "";
                MessageBox.Show($"{MessageStrings.SaveOperationFailed}\n{ex.Message}{innerExceptionMsg}",
                    CommonStrings.AppTitle,
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            finally
            {
                PreselectedExchange = null;
            }
        }

        /// <summary>
        /// Before executing the command, we can set this property to have the exchange preselected in the trading account creation dialog.
        /// This is useful when you want to create a trading account for a specific exchange and want to save the user from having to select it manually.
        /// </summary>
        public Exchange? PreselectedExchange { get; set; } = null;
    }
}
