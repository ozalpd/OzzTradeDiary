using System.Windows;
using TD.WPF.Services;
using TD.WPF.ViewModels;

namespace TD.WPF.Commands.Maintenance
{
    internal class CreateExchangeCommand : AbstractCommand
    {
        private readonly AbstractDiaryVM _viewModel;
        private readonly IWindowDialogService _windowDialogService;

        public CreateExchangeCommand(AbstractDiaryVM viewModel, IWindowDialogService windowDialogService)
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
                var dialogResult = _windowDialogService.ShowExchangeCreateDialog(owner);

                if (dialogResult.IsConfirmed && dialogResult.Exchange is not null)
                {
                    var Exchange = dialogResult.Exchange;
                    _viewModel.Exchanges.Add(Exchange);
                    await _viewModel.SaveExchangesAsync();
                    await _viewModel.LoadExchangesAsync();

                    _viewModel.SelectedExchange = _viewModel.Exchanges.FirstOrDefault(x => x.Id == Exchange.Id);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Create trading account failed.\n{ex.Message}", "Ozz Trade Diary", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
