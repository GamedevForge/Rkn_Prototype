namespace Project.Common.UI
{
    public interface IWindowViewModel
    {
        void Open();
        void Close();
        void UpTheHierarchy();
        void DescendInHierarchy();
    }
}