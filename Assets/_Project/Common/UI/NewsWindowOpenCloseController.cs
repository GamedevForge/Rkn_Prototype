using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project.Common.UI
{
    [RequireComponent(typeof(Image))]
    public class NewsWindowOpenCloseController : BaseWidget
    {
        [SerializeField] private RectTransform _windowRectTransform;
        [SerializeField] private float _duration;
        [SerializeField] private OpenCloseWidgetType _openCloseWidgetType;

        private bool _animationInProcessing = false;
        private Vector3 _originSizeDelta;
        private NewsWindowModel _model;

        private Image Image => GetComponent<Image>();

        [Inject] private void Construct(NewsWindowModel model) =>
            _model = model;

        private void Awake()
        {
            Image.raycastTarget = true;
            _originSizeDelta = Vector3.one;
            _windowRectTransform.localScale = Vector3.zero;
        }

        public override async UniTask Interact()
        {
            if (_animationInProcessing)
                return;
            else if (_model.IsOpen && _openCloseWidgetType == OpenCloseWidgetType.Open)
                return;
            else if (_model.IsOpen == false && _openCloseWidgetType == OpenCloseWidgetType.Close)
                return;
            
            _animationInProcessing = true;

            if (_openCloseWidgetType == OpenCloseWidgetType.Open)
            {
                await PlayOpenCloseAnimation(_originSizeDelta,
                    _duration,
                    _windowRectTransform);
                _model.Open();
            }
            else if (_openCloseWidgetType == OpenCloseWidgetType.Close)
            {
                await PlayOpenCloseAnimation(Vector3.zero,
                    _duration,
                    _windowRectTransform);
                _model.Close();
            }

            _animationInProcessing = false;
        }
    }

    public enum OpenCloseWidgetType
    {
        Open,
        Close,
    }
}
