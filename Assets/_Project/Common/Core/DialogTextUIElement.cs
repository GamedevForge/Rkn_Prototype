using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Text;

namespace Project.Common.Core
{
    [RequireComponent(typeof(CanvasGroup))]
    public class DialogTextUIElement : MonoBehaviour
    {
        [SerializeField] private TMPro.TMP_Text _text;
        [SerializeField] private float _animationSpeed;
        [SerializeField] private float _speedUpAnimationDuration;
        [SerializeField] private float _showAndCloseAnimationDuration;

        private bool _animationIsProcessing = false;
        private bool _speedUpIsProcessing = false;
        private float _sourceAnimationSpeed;

        private CanvasGroup CanvasGroup => GetComponent<CanvasGroup>();

        private void Awake() =>
            _sourceAnimationSpeed = _animationSpeed;

        public async UniTask ShowTextAsync(string text)
        {
            CanvasGroup.alpha = 1f;
            _animationIsProcessing = true;

            StringBuilder stringBuilder = new();
            _text.text = string.Empty;

            foreach(char symbol in text)
            {
                stringBuilder.Append(symbol);
                _text.text = stringBuilder.ToString();
                await UniTask.WaitForSeconds(_animationSpeed);
            }

            _animationIsProcessing = false;
        }

        public async UniTask CloseTextAsync()
        {
            Tween tween = CanvasGroup.DOFade(0f, _showAndCloseAnimationDuration);
            await tween.AsyncWaitForCompletion();
        }

        public async UniTask SpeedUpAnimation()
        {
            if (_animationIsProcessing == false 
                && _speedUpIsProcessing == false)
                return;

            _speedUpIsProcessing = true;
            _animationSpeed = _speedUpAnimationDuration;
            await UniTask.WaitWhile(() => _animationIsProcessing == true);
            _animationSpeed = _sourceAnimationSpeed;
            _speedUpIsProcessing = false;
        }
    }
}