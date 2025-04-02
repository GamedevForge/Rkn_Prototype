using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

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

        protected IWindowController WindowController {  get; private set; }
        protected IOpenCloseUI OpenCloseUI { get; private set; }
        protected WindowsRepository Repository { get; private set; }

        private Image Image => GetComponent<Image>();

        [Inject] private void Construct(WindowsRepository repository) =>
            Repository = repository;

        public void Initialize(IWindowController windowController,
            IOpenCloseUI openCloseUI)
        {
            WindowController = windowController;
            OpenCloseUI = openCloseUI; 
        }

        private void Awake() =>
            Image.color = _baseColor;

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (WindowController.OpenOrCloseInProcessing)
                return;
            
            if (OpenCloseUI.IsOpen == false)
            {
                WindowController.OpenWindow();
                Repository.DowngradeEverythingInHierarchyExcept(OpenCloseUI);
            }
            else if (OpenCloseUI.OnTopOfHierarchy || 
                Repository.CheckAllObjectsAtTheBottomOfHierarchy())
            {
                WindowController.CloseWindow();
            }
            else if(OpenCloseUI.OnTopOfHierarchy == false &&
                OpenCloseUI.IsOpen == true)
            {
                WindowController.HighUpThePeckingOrder();
                Repository.DowngradeEverythingInHierarchyExcept(OpenCloseUI);
            }
        }

        public virtual void OnPointerEnter(PointerEventData eventData) =>
            Image.color = _onEnterColor;

        public virtual void OnPointerExit(PointerEventData eventData) =>
            Image.color = _baseColor;
    }
}