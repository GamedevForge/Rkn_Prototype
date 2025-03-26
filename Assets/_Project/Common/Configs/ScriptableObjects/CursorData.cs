using UnityEngine;
using Sirenix.OdinInspector;

namespace Project.Common.Configs
{
    [CreateAssetMenu(fileName = "PlayerCursorData", menuName = "Project/Player/PlayerCursorData")]
    public class CursorData : SerializedScriptableObject
    {
        [Header("Animation:")]
        [field: SerializeField, Min(0)] public float Duration { get; private set; }
        [field: SerializeField, Range(0, 1)] public float SmallScaleDelta { get; private set; }
        [field: SerializeField, Range(0,1)] public float FirstHalfAnimationDurationDelta { get; private set; }
        [ReadOnly, ShowInInspector] public float SecondHalfAnimationDurationDelta => 1 - FirstHalfAnimationDurationDelta;
    }
}
