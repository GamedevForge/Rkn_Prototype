using UnityEngine;
using Zenject;
using Project.Common.Configs;

namespace Project.Common.Core
{
    public class PlayerRayCasterController : MonoBehaviour
    {
        private PlayerRayCasterModel _model;
        private PlayerRayCastData _data;

        [Inject] private void Construct(PlayerRayCastData data) =>
            _data = data;

        public void Initialize(PlayerRayCasterModel model) =>
            _model = model;

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


