using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Project.Common.UI
{
    public class WindowBaseAnimation : IOpenCloseAnimation, IInitializable
    {
        private readonly RectTransform _target;
        private readonly RectTransform _canvasTransform;
        private readonly RectTransform _buttonTransform;
        private readonly float _duration;

        private Vector3 _originScale;

        public WindowBaseAnimation(
            RectTransform target, 
            RectTransform canvasTransform,
            RectTransform buttonTransform,
            float duration)
        {
            _target = target;
            _canvasTransform = canvasTransform;
            _buttonTransform = buttonTransform;
            _duration = duration;
        }
        
        public void Initialize() =>
            _originScale = _target.localScale;

        public async UniTask PlayOpenAnimationAsync() =>
            await Task.WhenAll(MoveAnimation(_canvasTransform.position).AsyncWaitForCompletion(), 
                ScaleAnimation(_originScale).AsyncWaitForCompletion());

        public async UniTask PlayCloseAnimationAsync() =>
            await Task.WhenAll(MoveAnimation(_buttonTransform.position).AsyncWaitForCompletion(),
                ScaleAnimation(Vector3.zero).AsyncWaitForCompletion());

        private Tween MoveAnimation(Vector3 endPosition) =>
            _target.DOMove(endPosition, _duration);

        private Tween ScaleAnimation(Vector3 scale) =>
            _target.DOScale(scale, _duration);
    }
}
