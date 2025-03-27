using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Project.Common.UI
{
    public abstract class BaseWidget : MonoBehaviour, IWidgetInteractable
    {
        public abstract UniTask Interact();

        protected virtual async UniTask PlayOpenCloseAnimation(Vector3 sizeDelta, 
            float duration, 
            RectTransform rectTransform)
        {
            Tween tween = rectTransform.DOScale(sizeDelta, duration);
            await tween.AsyncWaitForCompletion();
        }
    }
}
