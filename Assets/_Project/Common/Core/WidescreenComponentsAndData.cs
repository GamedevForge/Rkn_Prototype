using UnityEngine;

namespace Project.Common.Core
{
    public class WidescreenComponentsAndData : MonoBehaviour
    {
        [field: SerializeField] public RectTransform TopImageTransform { get; private set; }
        [field: SerializeField] public RectTransform BottomImageTransform { get; private set; }
        [field: SerializeField] public float Duration { get; private set; } = 0.4f;
    }
}