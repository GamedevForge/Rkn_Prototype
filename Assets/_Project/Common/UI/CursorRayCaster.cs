using StarterAssets;
using UnityEngine;

namespace Project.Common.UI
{
    public class CursorRayCaster : MonoBehaviour
    {
        [SerializeField] private StarterAssetsInputs _assetsInputs;
        [SerializeField] private LayerMask _layerMask;

        private void Awake()
        {
            _assetsInputs.OnClick += OnLeftClick;
        }

        private void OnDestroy()
        {
            _assetsInputs.OnClick -= OnLeftClick;          
        }

        public void OnLeftClick()
        {
            Ray ray = new(transform.position, Vector3.forward);

            Debug.DrawRay(transform.position, Vector3.forward);

            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, _layerMask) 
                && hit.transform.TryGetComponent(out IWidgetInteractable widgetInteractable))
            {
                widgetInteractable.Interact();
            }
        }
    }
}
