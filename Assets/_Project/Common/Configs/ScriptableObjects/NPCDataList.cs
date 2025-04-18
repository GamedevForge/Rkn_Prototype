using UnityEngine;
using Sirenix.OdinInspector;

namespace Project.Common.Configs
{
    [CreateAssetMenu(fileName = "NPCDataList", menuName = "Project/NPCDataList")]
    public class NPCDataList : SerializedScriptableObject
    {
        [field: SerializeField] public NPCData[] NPCSData { get; private set; }
    }
}
