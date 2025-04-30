using UnityEngine;
using Project.Common.Configs;

namespace Project.Common.Core
{
    public class PlayerRayCasterController : MonoBehaviour
    {
        private PlayerRayCasterModel _model;
        private PlayerRayCastData _data;

        public void Initialize(
            PlayerRayCasterModel model,
            PlayerRayCastData data)
        {
            _model = model;
            _data = data;
        }

        private void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(_data.RayStartPosition);

            if (Physics.Raycast(ray, _data.Distance, _data.RayCastIgnoreLayerMask))
                return;

            if (Physics.Raycast(ray, out RaycastHit hit, _data.Distance, _data.InteractableObjectLayerMask) &&
                hit.transform.TryGetComponent(out IInteractableObject interactableObject) &&
                interactableObject.CanInteract)
            {
                _model.ChangeCurrentObject(interactableObject);
            }
            else
                _model.ChangeCurrentObject(null);
        }
    }
}


