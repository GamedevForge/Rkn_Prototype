using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Project.Common.Core
{
    public class ArmAnimationController : MonoBehaviour
    {
        [SerializeField] private Transform _armTransform;
        [SerializeField] private Vector3 _offSet;
        [SerializeField] private float _duration;

        private void Awake()
        {
            gameObject.transform.localPosition += _offSet;
            gameObject.SetActive(false);
        }

        public UniTask PlayShowAnimationAsync()
        {
            gameObject.SetActive(true);
            return PlayAnimationAsync(-_offSet);
        }

        public async UniTask PlayHideAnimationAsync()
        {
            await PlayAnimationAsync(_offSet);
            gameObject.SetActive(false);
        }

        private async UniTask PlayAnimationAsync(Vector3 offSet)
        {
            Tween tween = _armTransform.DOLocalMove(transform.localPosition + offSet, _duration);
            await tween.AsyncWaitForCompletion();
        }
    }
}
