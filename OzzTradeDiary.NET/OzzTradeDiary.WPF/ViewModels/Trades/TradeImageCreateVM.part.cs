using TD.WPF.Commands;

namespace TD.WPF.ViewModels.Trades
{
    public partial class TradeImageCreateVM : IOpenFileContext
    {
        partial void OnInitialized()
        {
            OpenFileCommand = new OpenFileCommand(this);
        }

        public OpenFileCommand OpenFileCommand { get; private set; }

        public string OpenFileDialogFilter => openImgFilter;

        public void SetFilePath(string filePath)
        {
            ImageURL = filePath;
        }

        static string openImgFilter = "All supported graphics|*.jpg;*.jpeg;*.png;|" +
                                      "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                                      "Portable Network Graphic (*.png)|*.png";
    }
}
