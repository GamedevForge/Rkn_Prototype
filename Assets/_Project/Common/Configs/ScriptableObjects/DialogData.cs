using UnityEngine;
using Sirenix.OdinInspector;

namespace Project.Common.Configs
{
    [CreateAssetMenu(fileName = "DialogData", menuName = "Project/DialogData")]
    public class DialogData : SerializedScriptableObject
    {
        [field: SerializeField] public string NPCID { get; private set; }
        [field: SerializeField] public TakeData[] DialogTakes { get; private set; }
    }
}
