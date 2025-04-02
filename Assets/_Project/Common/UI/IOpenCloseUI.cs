namespace Project.Common.UI
{
    public interface IOpenCloseUI
    { 
        bool IsOpen { get; }
        bool OnTopOfHierarchy { get; }
    }
}