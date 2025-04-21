using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using UnityEngine.EventSystems;

namespace Project.Common.Core
{
    [RequireComponent(typeof(CanvasGroup))]
    public class DialogUIButtonGoToNextTake : MonoBehaviour, IPointerClickHandler
    {
        public event Action OnClicked;

        [SerializeField] private TMPro.TMP_Text _text;
        [SerializeField] private float _animationDuration;

        private bool _isActive = false;

        private CanvasGroup CanvasGroup => GetComponent<CanvasGroup>();

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_isActive)
                OnClicked?.Invoke();
        }

        public async UniTask PlayShowAnimationAsync()
        {
            Tween tween;
            _isActive = false;
            gameObject.SetActive(true);

            tween = CanvasGroup.DOFade(1f, _animationDuration);
            await tween.AsyncWaitForCompletion();

            _isActive = true;
        }

        public async UniTask PlayCloseAnimationAsync()
        {
            Tween tween;
            _isActive = false;

            tween = CanvasGroup.DOFade(1f, _animationDuration);
            await tween.AsyncWaitForCompletion();

            gameObject.SetActive(false);
        }
    }
}