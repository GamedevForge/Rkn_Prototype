using UnityEngine;
using Sirenix.OdinInspector;

namespace Project.Common.Configs
{
    [CreateAssetMenu(fileName = "PlayerRayCastData", menuName = "Project/Player/PlayerRayCastData")]
    public class PlayerRayCastData : SerializedScriptableObject
    {
        [field: SerializeField] public float Distance { get; private set; }
        [field: SerializeField] public LayerMask InteractableObjectLayerMask { get; private set; }
        [field: SerializeField] public LayerMask RayCastIgnoreLayerMask { get; private set; }

        public Vector3 RayStartPosition => new Vector3(
            Screen.width / 2f,
            Screen.height / 2f,
            0f);
    }
}
