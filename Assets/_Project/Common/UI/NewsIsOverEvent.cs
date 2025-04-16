using Project.Common.Core;
using System;
using UnityEngine;

namespace Project.Common.UI
{
    public class NewsIsOverEvent : MonoBehaviour, IQuestEvent<Transform>
    {
        public Transform MarkerTarget => null;

        public event Action OnEvent;

        public void TriggerEvent() =>
            OnEvent?.Invoke();
    }
}
