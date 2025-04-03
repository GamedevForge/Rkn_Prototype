using System.Collections.Generic;

namespace Project.Common.UI
{
    public class WindowsRepository
    {
        public IEnumerable<KeyValuePair<IOpenCloseUI, IWindowController>> Objects => _openClosesUI;

        private Dictionary<IOpenCloseUI, IWindowController> _openClosesUI = new();

        public void Add(IOpenCloseUI windowViewModel, IWindowController windowController) =>
            _openClosesUI.Add(windowViewModel, windowController);

        public void Remove(IOpenCloseUI windowViewModel) =>
            _openClosesUI.Remove(windowViewModel);

        public bool CheckIfOtherWindowsAreOpen(IOpenCloseUI windowViewModel)
        {
            foreach (var window in _openClosesUI)
            {
                if (window.Key.IsOpen && window.Key !=  windowViewModel)
                    return true;
            }
            return false;
        }

        public bool CheckAllObjectsAtTheBottomOfHierarchy()
        {
            foreach (var window in _openClosesUI)
            {
                if (window.Key.OnTopOfHierarchy)
                    return false;
            }
            return true;
        }

        public void DowngradeEverythingInHierarchyExcept(IOpenCloseUI windowViewModel)
        {
            foreach (var window in _openClosesUI)
            {
                if (window.Key.IsOpen && window.Key != windowViewModel)
                    window.Value.DownThePeckingOrder();
            }
        }
    }
}