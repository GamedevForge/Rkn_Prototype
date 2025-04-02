namespace Project.Common.UI
{
    public class NewsWindowModel1
    { 
        public bool IsOpen { get; private set; } = false;

        public void Open() =>
            IsOpen = true;

        public void Close() =>
            IsOpen = false;
    }
}
