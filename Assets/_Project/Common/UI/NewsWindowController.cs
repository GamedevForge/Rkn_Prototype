using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Project.Common.UI
{
    public class NewsWindowController : IWindowController
    {
        private readonly IWindowAnimation _windowAnimation;
        private readonly NewsWindowModelView _windowModel;
        private readonly RectTransform _windowRectTransform;

        public bool OpenOrCloseInProcessing { get; private set; } = false;

        public NewsWindowController(
            IWindowAnimation windowAnimation, 
            NewsWindowModelView windowModel,
            RectTransform windowRectTransform)
        {
            _windowAnimation = windowAnimation;
            _windowModel = windowModel;
            _windowRectTransform = windowRectTransform;
        }

        public async UniTask OpenWindow()
        {
            OpenOrCloseInProcessing = true;
            _windowRectTransform.SetAsLastSibling();
            await _windowAnimation.PlayOpenAnimationAsync();
            _windowModel.Open();
            OpenOrCloseInProcessing = false;
        }
        
        public async UniTask CloseWindow()
        {
            OpenOrCloseInProcessing = true;
            await _windowAnimation.PlayCloseAnimationAsync();
            _windowRectTransform.SetAsFirstSibling();
            _windowModel.Close();
            OpenOrCloseInProcessing = false;
        }
    }
}