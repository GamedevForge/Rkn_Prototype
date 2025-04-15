using System;
using UnityEngine;
using Zenject;
using Sirenix.Serialization;
using Sirenix.OdinInspector;

namespace Project.Common.Core.Quest
{
    [Serializable]
    public class QuestEvent : SerializedMonoBehaviour
    {
        public event Action<string> OnEvent;

        [field: SerializeField] public string ID { get; private set; }
        [field: SerializeField] public QuestEventType Type { get; private set; }
        
        [OdinSerialize] private IQuestEvent<Transform> _event;

        private QuestController _controller;

        private Transform Target => _event.MarkerTarget;

        [Inject] private void Construct(QuestController controller) =>
            _controller = controller;

        private void Awake()
        {
            _event.OnEvent += TriggerEvent;
            _controller.AddQuestEvent(this);
        }

        private void OnDestroy()
        {
            _event.OnEvent -= TriggerEvent;
            _controller.RemoveQuestEvent(this);
        }

        private void TriggerEvent() =>
            OnEvent?.Invoke(ID);
    }

    public enum QuestEventType
    {
        WithTarget,
        WithoutTarget
    }
}
