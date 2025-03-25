namespace Project.Common.Core
{
    public interface IInteractableObject : IObjectName
    {
        bool CanInteract {  get; }
        void Interact();
    }
}


