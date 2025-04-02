using Cysharp.Threading.Tasks;

namespace Project.Common.UI
{
    public class NewsWindowController : IWindowController
    {
        private IWindowAnimation _windowAnimation;
        private NewsWindowModel _windowModel;

        public void Initialize(IWindowAnimation windowAnimation, NewsWindowModel newsWindowModel)
        {
            _windowAnimation = windowAnimation;
            _windowModel = newsWindowModel;
        }
        
        public async UniTask OpenWindow()
        {
            await _windowAnimation.PlayOpenAnimationAsync();
            _windowModel.Open();
        }
        
        public async UniTask CloseWindow()
        {
            await _windowAnimation.PlayCloseAnimationAsync();
            _windowModel.Close();
        }
    }
}