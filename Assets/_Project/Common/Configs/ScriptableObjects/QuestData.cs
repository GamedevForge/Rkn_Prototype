using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace Project.Common.Configs
{
    [CreateAssetMenu(fileName = "QuestData", menuName = "Project/QuestData")]
    public class QuestData : SerializedScriptableObject
    {
        [field: SerializeField] public List<QuestConfig[]> QuestConfigs { get; private set; }
    }
}
