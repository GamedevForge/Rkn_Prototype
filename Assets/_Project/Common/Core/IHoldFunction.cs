namespace Project.Common.Core
{
    public interface IHoldFunction<IInteractableObject> : IObjectChangedEvent<IInteractableObject>
    {
        void Hold(float time, float endTime, bool isPressed);
    }
}


