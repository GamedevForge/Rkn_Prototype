using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Project.Common.UI
{
    public class BaseWindowController : IWindowController
    {
        private readonly IWindowAnimation _windowAnimation;
        private readonly IWindowViewModel _windowViewModel;
        private readonly RectTransform _windowRectTransform;

        public bool OpenOrCloseInProcessing { get; private set; } = false;

        public BaseWindowController(
            IWindowAnimation windowAnimation,
            IWindowViewModel windowViewModel,
            RectTransform windowRectTransform)
        {
            _windowAnimation = windowAnimation;
            _windowViewModel = windowViewModel;
            _windowRectTransform = windowRectTransform;
        }

        public async UniTask OpenWindow()
        {
            OpenOrCloseInProcessing = true;
            HighUpThePeckingOrder();
            await _windowAnimation.PlayOpenAnimationAsync();
            _windowViewModel.Open();
            OpenOrCloseInProcessing = false;
        }
        
        public async UniTask CloseWindow()
        {
            OpenOrCloseInProcessing = true;
            await _windowAnimation.PlayCloseAnimationAsync();
            DownThePeckingOrder();
            _windowViewModel.Close();
            OpenOrCloseInProcessing = false;
        }

        public void HighUpThePeckingOrder()
        {
            _windowRectTransform.SetAsLastSibling();
            _windowViewModel.UpTheHierarchy();
        }

        public void DownThePeckingOrder()
        {
            _windowViewModel.DescendInHierarchy();
        }
    }
}