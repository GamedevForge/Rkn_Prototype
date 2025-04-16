using TMPro;
using UnityEngine;
using Zenject;

namespace Project.Common.UI
{
    public class RequirementsWindowViewController : BaseWindowController, IInitializable
    {
        private readonly RequirementsWindowViewModel _windowViewModel;
        private readonly TMP_Text _text;
        
        public RequirementsWindowViewController(
            IOpenCloseAnimation windowAnimation,
            RequirementsWindowViewModel windowViewModel,
            RectTransform windowRectTransform,
            TMP_Text text) : base(windowAnimation, windowViewModel, windowRectTransform)
        {
            _windowViewModel = windowViewModel;
            _text = text;
        }

        public void Initialize() =>
            ChangeNews();

        private void ChangeNews() =>
            _text.text = _windowViewModel.Text;
    }
}