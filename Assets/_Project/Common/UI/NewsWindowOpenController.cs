using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Common.UI
{
    [RequireComponent(typeof(Image))]
    public class NewsWindowOpenController : BaseWidget
    {
        [SerializeField] private RectTransform _windowRectTransform;
        [SerializeField] private float _duration;

        private bool _openInProcessing = false;
        private Vector2 _originSizeDelta;
        private NewsWindowModel _model;

        private Image Image => GetComponent<Image>();

        public void Initialize(NewsWindowModel model) =>
            _model = model;

        private void Awake()
        {
            Image.raycastTarget = true;
            _originSizeDelta = _windowRectTransform.sizeDelta;
            _windowRectTransform.sizeDelta = Vector2.zero;
        }

        public override async UniTask Interact()
        {
            if (_openInProcessing && _model.IsOpen)
                return;
            
            _openInProcessing = true;

            await PlayOpenAnimation(_originSizeDelta, 
                _duration, 
                _windowRectTransform);
            _model.Open();

            _openInProcessing = false;
        }
    }
}
