namespace Project.Common.UI
{
    public abstract class BaseWindowViewModel : IOpenCloseUI, IWindowViewModel
    {
        public virtual bool IsOpen { get; private set; } = false;

        public virtual bool OnTopOfHierarchy { get; private set; } = false;

        public virtual void Close()
        {
            IsOpen = false;
        }

        public virtual void Open()
        {
            IsOpen = true;
        }

        public virtual void UpTheHierarchy()
        {
            OnTopOfHierarchy = true;
        }

        public virtual void DescendInHierarchy()
        {
            OnTopOfHierarchy = false;
        }
    }
}