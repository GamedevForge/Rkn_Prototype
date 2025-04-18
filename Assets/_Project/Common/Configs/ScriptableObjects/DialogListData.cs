using UnityEngine;
using Sirenix.OdinInspector;

namespace Project.Common.Configs
{
    [CreateAssetMenu(fileName = "DialogListData", menuName = "Project/DialogListData")]
    public class DialogListData : SerializedScriptableObject
    {
        [field: SerializeField] public DialogData[] DialogsData { get; private set; }
    }
}
