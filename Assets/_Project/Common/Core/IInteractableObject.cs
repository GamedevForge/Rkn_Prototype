namespace Project.Common.Core
{
    public interface IInteractableObject : IObjectName
    {
        bool CanInteract { get; }
        InteractType InteractType { get; }
        float HoldTime { get; }

        void Interact();
    }

    public enum InteractType
    {
        Click,
        Hold,
    }
}