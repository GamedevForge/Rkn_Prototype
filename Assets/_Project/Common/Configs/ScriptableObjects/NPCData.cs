using UnityEngine;
using Sirenix.OdinInspector;

namespace Project.Common.Configs
{
    [CreateAssetMenu(fileName = "NPCData", menuName = "Project/NPCData")]
    public class NPCData : SerializedScriptableObject
    {
        [field: SerializeField] public string ID { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
    }
}
