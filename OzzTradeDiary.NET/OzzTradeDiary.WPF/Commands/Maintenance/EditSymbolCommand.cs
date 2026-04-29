using System.Windows;
using TD.i18n;
using TD.WPF.Services;
using TD.WPF.ViewModels;

namespace TD.WPF.Commands.Maintenance
{
    internal class EditSymbolCommand : AbstractCommand
    {
        private readonly AbstractDiaryVM _viewModel;
        private readonly IWindowDialogService _windowDialogService;

        public EditSymbolCommand(AbstractDiaryVM viewModel, IWindowDialogService windowDialogService)
        {
            _viewModel = viewModel;
            _windowDialogService = windowDialogService;
        }

        public override bool CanExecute(object? parameter)
        {
            return _viewModel.SelectedSymbol is not null;
        }

        public override async void Execute(object? parameter)
        {
            if (_viewModel.SelectedSymbol is null)
                return;

            if (parameter is not Window owner)
                return;

            var symbol = _viewModel.SelectedSymbol;
            try
            {
                var dialogResult = _windowDialogService.ShowSymbolEditDialog(owner, _viewModel.SelectedSymbol);
                if (dialogResult.IsConfirmed && dialogResult.IsDirty)
                {
                    await _viewModel.SaveSymbolAsync(symbol);
                    await _viewModel.LoadSymbolsAsync();
                }
                else if (dialogResult.IsDirty)
                {
                    await _viewModel.LoadCurrenciesAsync();
                }

                _viewModel.SelectedSymbol = _viewModel.Symbols.FirstOrDefault(x => x.Id == symbol.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(MessageStrings.SaveOperationFailed, CommonStrings.AppTitle, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
