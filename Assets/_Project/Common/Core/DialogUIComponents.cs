using UnityEngine;

namespace Project.Common.Core
{
    public class DialogUIComponents : MonoBehaviour
    {
        [field: SerializeField] public Transform TextParent { get; private set; }
        [field: SerializeField] public Transform ButtonsParent { get; private set; }
        [field: SerializeField] public TMPro.TMP_Text NameText { get; private set; }
        [field: SerializeField] public DialogUIButtonGoToNextTake DialogUIButtonGoToNextTake { get; private set; }
    }
}