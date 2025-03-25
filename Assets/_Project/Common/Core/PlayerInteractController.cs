using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Common.Core
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerInteractController : MonoBehaviour
    {
        private IObjectChangedEvent<IInteractableObject> _objectChangedEvent;

        private IInteractableObject _currentInteractableObject;

        public void Initialize(IObjectChangedEvent<IInteractableObject> objectChangedEvent)
        {
            _objectChangedEvent = objectChangedEvent;
            _objectChangedEvent.OnCurrentObjectChanged += ChangeCurrentInteractableObject;
        }

        private void OnDestroy() =>
            _objectChangedEvent.OnCurrentObjectChanged -= ChangeCurrentInteractableObject;

        private void ChangeCurrentInteractableObject(IInteractableObject gameObject) =>
            _currentInteractableObject = gameObject;

        public void OnInteract()
        {
            if (_currentInteractableObject != null || _currentInteractableObject.CanInteract)
                _currentInteractableObject.Interact();
        }
    }
}


