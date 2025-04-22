using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using Zenject;

namespace Project.Common.Core
{
    public class WidescreenAnimation : IInitializable
    {
        private readonly RectTransform _topImageTransform;
        private readonly RectTransform _bottomImageTransform;
        private readonly float _duration;

        public WidescreenAnimation(
            RectTransform topImageTransform, 
            RectTransform bottomImageTransform, 
            float duration)
        {
            _topImageTransform = topImageTransform;
            _bottomImageTransform = bottomImageTransform;
            _duration = duration;
        }

        public void Initialize()
        {
            PlayTweenAnimation(_topImageTransform, (_topImageTransform.sizeDelta.y * 2f + 10f), 0f);
            PlayTweenAnimation(_bottomImageTransform, -(_topImageTransform.sizeDelta.y * 2f + 10f), 0f);
        }

        public async Task HideImageAsync()
        {
            await Task.WhenAll(
                PlayTweenAnimation(_topImageTransform, (_topImageTransform.sizeDelta.y * 2f + 10f), _duration).AsyncWaitForCompletion(),
                PlayTweenAnimation(_bottomImageTransform, -(_topImageTransform.sizeDelta.y * 2f + 10f), _duration).AsyncWaitForCompletion());
        }

        public async Task ShowImageAsync()
        {
            await Task.WhenAll(
                PlayTweenAnimation(_topImageTransform, -(_topImageTransform.sizeDelta.y * 2f + 10f), _duration).AsyncWaitForCompletion(),
                PlayTweenAnimation(_bottomImageTransform, (_topImageTransform.sizeDelta.y * 2f + 10f), _duration).AsyncWaitForCompletion());
        }

        private Tween PlayTweenAnimation(RectTransform target, float offset, float duration) =>
            target.DOAnchorPos3DY(target.anchoredPosition3D.y + offset, duration);
    }
}