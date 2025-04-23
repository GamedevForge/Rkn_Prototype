using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Common.Core
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerInteractController : MonoBehaviour
    {
        private IHoldFunction<IInteractableObject> _objectChangedEvent;
        private IInteractableObject _currentInteractableObject;

        private bool _holdIsProcessing = false;
        private bool _isPressed;

        public void Initialize(IHoldFunction<IInteractableObject> objectChangedEvent)
        {
            _objectChangedEvent = objectChangedEvent;
            _objectChangedEvent.OnCurrentObjectChanged += ChangeCurrentInteractableObject;
        }

        private void OnDestroy() =>
            _objectChangedEvent.OnCurrentObjectChanged -= ChangeCurrentInteractableObject;

        private void ChangeCurrentInteractableObject(IInteractableObject gameObject) =>
            _currentInteractableObject = gameObject;

        public void OnInteract(InputValue value)
        {
            _isPressed = value.isPressed;
            if (_currentInteractableObject != null && 
                _currentInteractableObject.CanInteract)
            {
                if (_currentInteractableObject.InteractType == InteractType.Click)
                    _currentInteractableObject.Interact();
                else if (_holdIsProcessing == false)
                    HoldClick().Forget();
            }
        }

        private async UniTask HoldClick()
        {
            float timer = 0f;
            _holdIsProcessing = true;

            while (_isPressed && 
                _currentInteractableObject != null && 
                _currentInteractableObject.CanInteract &&
                timer < _currentInteractableObject.HoldTime)
            {
                await UniTask.WaitForFixedUpdate();

                if (_currentInteractableObject == null)
                {
                    _objectChangedEvent.Hold(0f, 0f, false);
                    _holdIsProcessing = false;
                    return;
                }

                _objectChangedEvent.Hold(timer, _currentInteractableObject.HoldTime, _isPressed);
                timer += Time.deltaTime;

                if (timer >= _currentInteractableObject.HoldTime) 
                    _currentInteractableObject.Interact();
            }

            _objectChangedEvent.Hold(0f, 0f, false);
            _holdIsProcessing = false;
        }
    }
}