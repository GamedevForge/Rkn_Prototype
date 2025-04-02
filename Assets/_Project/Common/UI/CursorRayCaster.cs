using Project.Common.Core;
using StarterAssets;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Project.Common.UI
{
    public class CursorRayCaster : CursorBaseRaycaster
    {
        [SerializeField] private StarterAssetsInputs _assetsInputs;
        
        private PlayerState _playerState;

        [Inject] private void Construct(PlayerState playerState) =>
            _playerState = playerState; 

        protected override void Awake()
        {
            _assetsInputs.OnClick += OnLeftClick;
        }

        protected override void OnDestroy()
        {
            _assetsInputs.OnClick -= OnLeftClick;          
        }

        public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
        {
            if (_playerState.InComputer == false)
                return;

            base.Raycast(eventData, resultAppendList);
        }

        public void OnLeftClick()
        {
            /*Ray ray = new(transform.position, Vector3.forward);

            Debug.DrawRay(transform.position, Vector3.forward);

            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, _layerMask) 
                && hit.transform.TryGetComponent(out IWidgetInteractable widgetInteractable))
            {
                widgetInteractable.Interact();
            }*/
        }
    }
}
