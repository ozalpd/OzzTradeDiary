using System.Windows;
using TD.WPF.Services;
using TD.WPF.ViewModels;

namespace TD.WPF.Commands.Maintenance
{
    internal class EditExchangeCommand : AbstractCommand
    {
        private readonly AbstractDiaryVM _viewModel;
        private readonly IWindowDialogService _windowDialogService;

        public EditExchangeCommand(AbstractDiaryVM viewModel, IWindowDialogService windowDialogService)
        {
            _viewModel = viewModel;
            _windowDialogService = windowDialogService;
        }

        public override bool CanExecute(object? parameter)
        {
            return _viewModel.SelectedExchange is not null;
        }

        public override async void Execute(object? parameter)
        {
            if (_viewModel.SelectedExchange is null)
                return;

            if (parameter is not Window owner)
                return;

            int id = _viewModel.SelectedExchange.Id;

            try
            {
                var dialogResult = _windowDialogService.ShowExchangeEditDialog(owner, _viewModel.SelectedExchange);
                if (dialogResult.IsConfirmed && dialogResult.IsDirty)
                {
                    await _viewModel.SaveExchangesAsync();
                    await _viewModel.LoadExchangesAsync();
                }
                else if (dialogResult.IsDirty)
                {
                    await _viewModel.LoadCurrenciesAsync();
                }

                _viewModel.SelectedExchange = _viewModel.Exchanges.FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Edit trading account failed.\n{ex.Message}", "Ozz Trade Diary", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
