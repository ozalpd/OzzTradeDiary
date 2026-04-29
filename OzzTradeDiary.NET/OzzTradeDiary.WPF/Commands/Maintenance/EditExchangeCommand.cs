using System.Windows;
using TD.i18n;
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

            var exchange = _viewModel.SelectedExchange;

            try
            {
                var dialogResult = _windowDialogService.ShowExchangeEditDialog(owner, _viewModel.SelectedExchange);
                if (dialogResult.IsConfirmed && dialogResult.IsDirty)
                {
                    await _viewModel.SaveExchangeAsync(exchange);
                    await _viewModel.LoadExchangesAsync();
                }
                else if (dialogResult.IsDirty)
                {
                    await _viewModel.LoadCurrenciesAsync();
                }

                _viewModel.SelectedExchange = _viewModel.Exchanges.FirstOrDefault(x => x.Id == exchange.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(MessageStrings.SaveOperationFailed, CommonStrings.AppTitle, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
