using System.Windows;
using TD.i18n;
using TD.WPF.ViewModels;

namespace TD.WPF.Views
{
    public class AbstractEditView : Window
    {
        protected IIsDirty _isDirty;
        public AbstractEditView()
        {
            _isDirty = new DummyEditView();
            DataContext = _isDirty;
        }

        public AbstractEditView(IIsDirty isDirty)
        {
            _isDirty = isDirty;
            DataContext = _isDirty;
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (_isDirty.IsDirty && !DialogResult.HasValue)
            {
                var result = MessageBox.Show(this,
                    MessageStrings.UnsavedChangesWarning,
                    MessageStrings.UnsavedChangesTitle,
                    MessageBoxButton.YesNoCancel,
                    MessageBoxImage.Warning);
                if (result == MessageBoxResult.Cancel)
                {
                    e.Cancel = true; // Cancel the closing
                }
                else if (result == MessageBoxResult.Yes)
                {
                    DialogResult = true;
                }
            }
            base.OnClosing(e);
        }

        public bool IsDirty => _isDirty.IsDirty;
    }

    internal class DummyEditView : AbstractEditVM { }
}
