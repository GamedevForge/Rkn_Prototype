using Project.Common.Core.Quest;
using UnityEngine;
using UnityEngine.AI;

namespace Project.Common.Core
{
    public class PlayerComponents : MonoBehaviour
    {
        [field: SerializeField] public NavMeshAgent Agent { get; private set; }
        [field: SerializeField] public Transform CameraTransform { get; private set; }
        [field: SerializeField] public Transform PlayerTransform { get; private set; }
        [field: SerializeField] public QuestEvent[] QuestEvents { get; private set; }
        [field: SerializeField] public PlayerTransform OnDestroyEvent { get; private set; }
    }
}