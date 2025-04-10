using Zenject;
using System;
using Project.Common.Core;

namespace Project.Common.UI
{
    public class InteractiveObjectsTextController : IInitializable, IDisposable
    {
        private readonly InteractableObjectsView _textView;
        private readonly IHoldEvent<IInteractableObject> _objectChangedEvent;

        public InteractiveObjectsTextController(InteractableObjectsView textView,
            IHoldEvent<IInteractableObject> objectChangedEvent)
        {
            _textView = textView;
            _objectChangedEvent = objectChangedEvent;
        }

        public void Initialize()
        {
            _objectChangedEvent.OnCurrentObjectChanged += OnObjectChanged;
            _objectChangedEvent.OnHold += OnHold;
        }

        public void Dispose()
        {
            _objectChangedEvent.OnCurrentObjectChanged -= OnObjectChanged;
            _objectChangedEvent.OnHold -= OnHold;
        }

        private void OnObjectChanged(IInteractableObject interactiveObject)
        {
            if (interactiveObject == null)
                Reset();
            else if (interactiveObject.CanInteract && interactiveObject.InteractType == InteractType.Click)
                Draw(interactiveObject.Name);
            else if (interactiveObject.CanInteract && interactiveObject.InteractType == InteractType.Hold)
                DrawHoldText(interactiveObject.Name);
        }

        private void OnHold(float time, float endTime, bool active)
        {
            _textView.DrawSlider(time / endTime);
            _textView.SetSliderActive(active);
        }

        public void Draw(string objectName) =>
            _textView.DrawText($"{objectName} interact");

        public void Reset() =>
            _textView.DrawText("");

        public void DrawHoldText(string objectName) =>
            _textView.DrawText($"Hold to {objectName} interact");
    }
}
