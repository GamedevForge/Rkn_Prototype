using Cysharp.Threading.Tasks;

namespace Project.Common.UI
{
    public class NewsWindowController : IWindowController
    {
        private readonly IWindowAnimation _windowAnimation;
        private readonly NewsWindowModel _windowModel;

        public bool OpenOrCloseInProcessing { get; private set; } = false;

        public NewsWindowController(IWindowAnimation windowAnimation, NewsWindowModel windowModel)
        {
            _windowAnimation = windowAnimation;
            _windowModel = windowModel;
        }

        public async UniTask OpenWindow()
        {
            OpenOrCloseInProcessing = true;
            await _windowAnimation.PlayOpenAnimationAsync();
            _windowModel.Open();
            OpenOrCloseInProcessing = false;
        }
        
        public async UniTask CloseWindow()
        {
            OpenOrCloseInProcessing = true;
            await _windowAnimation.PlayCloseAnimationAsync();
            _windowModel.Close();
            OpenOrCloseInProcessing = false;
        }
    }
}