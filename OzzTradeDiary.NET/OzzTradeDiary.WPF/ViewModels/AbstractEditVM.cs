namespace TD.WPF.ViewModels
{
    public interface IIsDirty
    {
        bool IsDirty { get; }
    }

    internal class AbstractEditVM : AbstractDataErrorInfoVM, IIsDirty
    {
        public AbstractEditVM()
        {
            ErrorsChanged += (_, _) => RaisePropertyChanged(nameof(IsValid));
        }

        public bool IsDirty { get; private set; }

        public bool IsValid => !HasErrors;

        protected override void RaisePropertyChanged(string propertyName)
        {
            base.RaisePropertyChanged(propertyName);
            IsDirty = true;
            base.RaisePropertyChanged(nameof(IsDirty));
        }
    }
}
