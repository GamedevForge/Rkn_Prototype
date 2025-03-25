using Zenject;
using System;
using Project.Common.Core;
using UnityEngine;

namespace Project.Common.UI
{
    public class InteractiveObjectsTextController : IInitializable, IDisposable
    {
        private readonly TextView _textView;
        private readonly IObjectChangedEvent<IInteractableObject> _objectChangedEvent;

        public InteractiveObjectsTextController(TextView textView,
            IObjectChangedEvent<IInteractableObject> objectChangedEvent)
        {
            _textView = textView;
            _objectChangedEvent = objectChangedEvent;
        }

        public void Initialize() =>
            _objectChangedEvent.OnCurrentObjectChanged += OnObjectChanged;

        public void Dispose() =>
            _objectChangedEvent.OnCurrentObjectChanged -= OnObjectChanged;

        private void OnObjectChanged(IInteractableObject interactiveObject)
        {
            if (interactiveObject == null || interactiveObject.CanInteract)
                Reset();
            else
                Draw(interactiveObject.Name);
        }

        public void Draw(string objectName) =>
            _textView.DrawText($"{objectName} interact");

        public void Reset() =>
            _textView.DrawText("");
    }
}
