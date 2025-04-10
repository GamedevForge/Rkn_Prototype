using System;

namespace Project.Common.Core
{
    public class PlayerRayCasterModel : IHoldEvent<IInteractableObject>, IHoldFunction<IInteractableObject>
    {
        public event Action<float, float, bool> OnHold;
        public Action<float, bool> OnHoldDelegate {  get; set; }
        public event Action<IInteractableObject> OnCurrentObjectChanged;
        public IInteractableObject CurrentGameObject { get; private set; }

        public void ChangeCurrentObject(IInteractableObject interactableObject)
        {
            if (interactableObject != CurrentGameObject)
            {
                CurrentGameObject = interactableObject;
                OnCurrentObjectChanged?.Invoke(interactableObject);
            }
            else 
                CurrentGameObject = interactableObject;
        }

        public void Hold(float time, float endTime, bool isPressed) =>
            OnHold?.Invoke(time, endTime, isPressed);
    }
}


