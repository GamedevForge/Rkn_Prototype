using UnityEngine;
using Sirenix.OdinInspector;

namespace Project.Common.Configs
{
    [CreateAssetMenu(fileName = "ApproveOrRejectData", menuName = "Project/ApproveOrRejectData")]
    public class ApproveOrRejectData : SerializedScriptableObject
    {
        [Header("Animation:")]
        [field: SerializeField] public float ArmAnimationDuration { get; private set; }
        [field: SerializeField] public float CameraAnimationDuration { get; private set; }
    }
}
