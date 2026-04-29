using System.Windows;
using TD.WPF.Services;
using TD.WPF.ViewModels;

namespace TD.WPF.Commands.Maintenance
{
    internal class EditCurrencyCommand : AbstractCommand
    {
        private readonly AbstractDiaryVM _viewModel;
        private readonly IWindowDialogService _windowDialogService;

        public EditCurrencyCommand(AbstractDiaryVM viewModel, IWindowDialogService windowDialogService)
        {
            _viewModel = viewModel;
            _windowDialogService = windowDialogService;
        }

        public override bool CanExecute(object? parameter)
        {
            return _viewModel.SelectedCurrency is not null;
        }

        public override async void Execute(object? parameter)
        {
            if (_viewModel.SelectedCurrency is null)
                return;

            if (parameter is not Window owner)
                return;

            int id = _viewModel.SelectedCurrency.Id;

            try
            {
                var dialogResult = _windowDialogService.ShowCurrencyEditDialog(owner, _viewModel.SelectedCurrency);
                if (dialogResult.IsConfirmed && dialogResult.IsDirty)
                {
                    await _viewModel.SaveCurrenciesAsync();
                    await _viewModel.LoadCurrenciesAsync();
                }
                else if (dialogResult.IsDirty)
                {
                    await _viewModel.LoadCurrenciesAsync();
                }

                _viewModel.SelectedCurrency = _viewModel.Currencies.FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Edit trading account failed.\n{ex.Message}", "Ozz Trade Diary", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
