using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Project.Common.UI
{
    public class QuestUIElement : MonoBehaviour, IOpenCloseAnimation
    {
        [SerializeField] private TMPro.TMP_Text _text;
        [SerializeField] private CanvasGroup _canvasGroup;

        [Header("Animation settings:")]
        [SerializeField] private float _animationDuration;

        private Tween _currentTween;

        public string CurrentID { get; private set; }

        public void SetDescription(string description, string id)
        {
            _text.text = description;
            CurrentID = id;
        }

        public async UniTask PlayHideAnimationAsync()
        {
            _currentTween = _canvasGroup.DOFade(0f, _animationDuration);
            await _currentTween.AsyncWaitForCompletion();
            gameObject.SetActive(false);
        }

        public async UniTask PlayShowAnimationAsync()
        {
            gameObject.SetActive(true);
            _currentTween = _canvasGroup.DOFade(1f, _animationDuration);
            await _currentTween.AsyncWaitForCompletion();
        }
    }
}