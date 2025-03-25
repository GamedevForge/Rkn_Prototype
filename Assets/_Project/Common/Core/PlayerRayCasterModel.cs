using UnityEngine;
using System;

namespace Project.Common.Core
{
    public class PlayerRayCasterModel : IObjectChangedEvent<IInteractableObject>
    {
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
    }
}


