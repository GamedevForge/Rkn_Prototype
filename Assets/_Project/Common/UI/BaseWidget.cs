using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Project.Common.UI
{
    public abstract class BaseWidget : MonoBehaviour, IWidgetInteractable
    {
        public abstract UniTask Interact();

        protected virtual async UniTask PlayOpenAnimation(Vector2 sizeDelta, 
            float duration, 
            RectTransform rectTransform)
        {
            Tween tween = rectTransform.DOSizeDelta(sizeDelta, duration);
            await tween.AsyncWaitForCompletion();
        }
    }
}
