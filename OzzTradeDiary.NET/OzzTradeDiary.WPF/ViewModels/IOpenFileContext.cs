namespace TD.WPF.ViewModels
{
    public interface IOpenFileContext
    {
        string OpenFileDialogFilter { get; }
        void SetFilePath(string filePath);
    }
}
