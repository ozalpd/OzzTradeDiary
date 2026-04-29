using System.Windows;
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
                    var Currency = dialogResult.Currency;
                    _viewModel.Currencies.Add(Currency);
                    await _viewModel.SaveCurrenciesAsync();
                    await _viewModel.LoadCurrenciesAsync();

                    _viewModel.SelectedCurrency = _viewModel.Currencies.FirstOrDefault(x => x.Id == Currency.Id);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Create trading account failed.\n{ex.Message}", "Ozz Trade Diary", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
