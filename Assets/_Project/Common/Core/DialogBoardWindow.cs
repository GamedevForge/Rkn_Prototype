using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Project.Common.UI;

namespace Project.Common.Core
{
    [RequireComponent(typeof(CanvasGroup))]
    public class DialogBoardWindow : MonoBehaviour, IOpenCloseAnimation
    {
        [SerializeField] private float _animationDuration;

        private CanvasGroup CanvasGroup => GetComponent<CanvasGroup>();

        private void Awake() =>
            CanvasGroup.alpha = 0f;

        public async UniTask PlayShowAnimationAsync()
        {
            Tween tween;
            gameObject.SetActive(true);

            tween = CanvasGroup.DOFade(1f, _animationDuration);
            await tween.AsyncWaitForCompletion();
        }

        public async UniTask PlayCloseAnimationAsync()
        {
            Tween tween;

            tween = CanvasGroup.DOFade(0f, _animationDuration);
            await tween.AsyncWaitForCompletion();

            gameObject.SetActive(false);
        }
    }
}