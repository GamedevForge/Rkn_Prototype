using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Common.Core
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerInteractController : MonoBehaviour
    {
        private IObjectChangedEvent<GameObject> _objectChangedEvent;

        private GameObject _currentInteractableObject;

        public void Initialize(IObjectChangedEvent<GameObject> objectChangedEvent)
        {
            _objectChangedEvent = objectChangedEvent;
            _objectChangedEvent.OnCurrentObjectChanged += ChangeCurrentInteractableObject;
        }

        private void OnDestroy() =>
            _objectChangedEvent.OnCurrentObjectChanged -= ChangeCurrentInteractableObject;

        private void ChangeCurrentInteractableObject(GameObject gameObject) =>
            _currentInteractableObject = gameObject;

        public void Interact()
        {
            Debug.Log(_currentInteractableObject);
            if (_currentInteractableObject != null)
            {
                if (_currentInteractableObject.TryGetComponent(out IIntertableObject intertableObject))
                    intertableObject.Interact();
            }
        }
    }
}


