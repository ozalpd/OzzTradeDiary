using Microsoft.Win32;
using TD.AppInfra.Commands;
using TD.WPF.ViewModels;

namespace TD.WPF.Commands
{
    public class OpenFileCommand : AbstractCommand
    {
        private readonly IOpenFileContext _viewModel;

        public OpenFileCommand(IOpenFileContext viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = _viewModel.OpenFileDialogFilter;

            if (openFileDialog.ShowDialog() != true)
                return;

            _viewModel.SetFilePath(openFileDialog.FileName);
        }
    }
}
