using System.Windows;
using TD.i18n;
using TD.WPF.Services;
using TD.WPF.ViewModels;

namespace TD.WPF.Commands.Maintenance
{
    internal class CreateCurrencyCommand : AbstractCommand
    {
        private readonly AbstractDiaryVM _viewModel;
        private readonly IWindowDialogService _windowDialogService;

        public CreateCurrencyCommand(AbstractDiaryVM viewModel, IWindowDialogService windowDialogService)
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
                var dialogResult = _windowDialogService.ShowCurrencyCreateDialog(owner);

                if (dialogResult.IsConfirmed && dialogResult.Currency is not null)
                {
                    var currency = dialogResult.Currency;
                    await _viewModel.SaveCurrencyAsync(currency);
                    await _viewModel.LoadCurrenciesAsync();

                    _viewModel.SelectedCurrency = _viewModel.Currencies.FirstOrDefault(x => x.Id == currency.Id);
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
        }
    }
}
