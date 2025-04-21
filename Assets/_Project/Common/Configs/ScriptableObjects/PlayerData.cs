using UnityEngine;
using Sirenix.OdinInspector;

namespace Project.Common.Configs
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Project/Player/PlayerData")]
    public class PlayerData : SerializedScriptableObject, IPlayerName
    {
        [field: SerializeField] public string PlayerName { get; private set; }
    }
}
