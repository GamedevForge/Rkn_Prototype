using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Project.Common.UI
{
    public class DataBaseWindowController : BaseWindowController
    {
        private readonly ResultSearchRepository _repository;
        
        public DataBaseWindowController(
            IWindowAnimation windowAnimation,
            IWindowViewModel windowViewModel,
            RectTransform windowRectTransform,
            ResultSearchRepository repository) : base(windowAnimation, windowViewModel, windowRectTransform) 
        { 
            _repository = repository;
        }

        public override async UniTask OpenWindow()
        {
            await base.OpenWindow();
            _repository.EnableAllSearchButtons();
        }

        public override async UniTask CloseWindow()
        {
            await base.CloseWindow();
            _repository.DisableAllSearchButtons();
        }

        public override void HighUpThePeckingOrder()
        {
            base.HighUpThePeckingOrder();
            _repository.EnableAllSearchButtons();
        }

        public override void DownThePeckingOrder()
        {
            base.DownThePeckingOrder();
            _repository.DisableAllSearchButtons();
        }
    }
}