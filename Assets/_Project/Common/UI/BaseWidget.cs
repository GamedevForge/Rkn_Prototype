using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Project.Common.UI
{
    [RequireComponent(typeof(Image))]
    public class BaseWidget : 
        MonoBehaviour, 
        IPointerClickHandler, 
        IPointerEnterHandler, 
        IPointerExitHandler
    {
        [SerializeField] private Color _baseColor;
        [SerializeField] private Color _onEnterColor;

        private IWindowController _windowController;
        private IOpenCloseUI _openCloseUI;

        private Image Image => GetComponent<Image>();

        public void Initialize(IWindowController windowController,
            IOpenCloseUI openCloseUI)
        {
            _windowController = windowController;
            _openCloseUI = openCloseUI; 
        }

        private void Awake() =>
            Image.color = _baseColor;

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (_openCloseUI.IsOpen)
                _windowController.CloseWindow();
            else
                _windowController.OpenWindow();
        }

        public virtual void OnPointerEnter(PointerEventData eventData) =>
            Image.color = _onEnterColor;

        public virtual void OnPointerExit(PointerEventData eventData) =>
            Image.color = _baseColor;
    }
}