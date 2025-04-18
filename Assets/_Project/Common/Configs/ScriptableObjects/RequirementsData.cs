using UnityEngine;
using Sirenix.OdinInspector;

namespace Project.Common.Configs
{
    [CreateAssetMenu(fileName = "RequirementsData", menuName = "Project/RequirementsData")]
    public class RequirementsData : SerializedScriptableObject
    {
        [field: SerializeField] public RequirementsConfig[] RequirementsConfigs { get; private set; }
    }
}
