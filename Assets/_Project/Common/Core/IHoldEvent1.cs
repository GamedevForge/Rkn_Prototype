using System;

namespace Project.Common.Core
{
    public interface IHoldEvent<IInteractableObject> : IObjectChangedEvent<IInteractableObject>
    {
        event Action<float, float, bool> OnHold;
    }
}


