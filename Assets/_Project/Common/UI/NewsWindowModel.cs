namespace Project.Common.UI
{
    public class NewsWindowModel
    { 
        public bool IsOpen { get; private set; } = false;

        public void Open() =>
            IsOpen = true;

        public void Close() =>
            IsOpen = false;
    }
}
