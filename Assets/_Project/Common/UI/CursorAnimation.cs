using DG.Tweening;
using Project.Common.Configs;
using StarterAssets;
using System;
using UnityEngine;
using Zenject;

namespace Project.Common.UI
{
    public class CursorAnimation : IInitializable, IDisposable
    {
        private readonly StarterAssetsInputs _assetsInputs;
        private readonly RectTransform _cursorRectTransform;
        private readonly float _duration;
        private readonly float _smallScaleDelta;
        private readonly float _firstHalfAnimationDurationDelta;
        private readonly float _secondHalfAnimationDurationDelta;

        private bool _animationIsProcess = false;

        public CursorAnimation(StarterAssetsInputs assetsInputs,
            RectTransform cursorRectTransform,
            CursorData cursorAnimationData) 
        {  
            _assetsInputs = assetsInputs;
            _cursorRectTransform = cursorRectTransform;
            _duration = cursorAnimationData.Duration;
            _smallScaleDelta = cursorAnimationData.SmallScaleDelta;
            _firstHalfAnimationDurationDelta = cursorAnimationData.FirstHalfAnimationDurationDelta;
            _secondHalfAnimationDurationDelta = cursorAnimationData.SecondHalfAnimationDurationDelta;
        }

        public void Initialize() =>
            _assetsInputs.OnClick += PlayAnimation;

        public void Dispose() =>
            _assetsInputs.OnClick -= PlayAnimation;

        private async void PlayAnimation()
        {
            if (_animationIsProcess)
                return;
            
            Tween tween1;
            Tween tween2;
            Vector2 originSizeDelta = _cursorRectTransform.sizeDelta;

            _animationIsProcess = true;

            tween1 = _cursorRectTransform.DOSizeDelta(
                _cursorRectTransform.sizeDelta * _smallScaleDelta,
                _duration * _firstHalfAnimationDurationDelta);

            await tween1.AsyncWaitForCompletion();

            tween2 = _cursorRectTransform.DOSizeDelta(
                originSizeDelta,
                _duration * _secondHalfAnimationDurationDelta);

            await tween2.AsyncWaitForCompletion();

            _animationIsProcess = false;
        }
    }
}
