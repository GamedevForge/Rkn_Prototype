using Cysharp.Threading.Tasks;
using Project.Common.Core;
using UnityEngine;

namespace Project.Common.UI
{
    public class NewsWindowController : BaseWindowController
    {
        private readonly PlayerState _playerState;
        
        public NewsWindowController(
            IOpenCloseAnimation windowAnimation,
            IWindowViewModel windowViewModel,
            RectTransform windowRectTransform,
            PlayerState playerState) : base(windowAnimation, windowViewModel, windowRectTransform) 
        { 
            _playerState = playerState;
        }

        public override async UniTask OpenWindow()
        {
            _playerState.OnKeyboard();
            await base.OpenWindow();
        }
        
        public override async UniTask CloseWindow()
        {
            _playerState.NotAtKeyboard();
            await base.CloseWindow();
        }
        
        public override void HighUpThePeckingOrder()
        {
            _playerState.OnKeyboard();
            base.HighUpThePeckingOrder();
        }

        public override void DownThePeckingOrder()
        {
            _playerState.NotAtKeyboard();
            base.DownThePeckingOrder();
        }
    }
}