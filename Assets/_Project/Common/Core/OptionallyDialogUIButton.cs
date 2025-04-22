using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using Project.Common.UI;
using UnityEngine.EventSystems;

namespace Project.Common.Core
{
    [RequireComponent(typeof(CanvasGroup))]
    public class OptionallyDialogUIButton : MonoBehaviour, IPointerClickHandler, IOpenCloseAnimation
    {
        public event Action<string> OnClicked;
        
        [SerializeField] private TMPro.TMP_Text _text;
        [SerializeField] private float _animationDuration;

        private bool _isActive = false;
        private string _key;

        private CanvasGroup CanvasGroup => GetComponent<CanvasGroup>();

        private void Awake() =>
            CanvasGroup.alpha = 0f;

        public void SetButtonText(string text)
        {
            _key = text;
            _text.text = text;
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (_isActive)
                OnClicked?.Invoke(_key);
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

            tween = CanvasGroup.DOFade(0f, _animationDuration);
            await tween.AsyncWaitForCompletion();
            
            gameObject.SetActive(false);
        }
    }
}