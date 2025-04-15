using UnityEngine;
using Sirenix.OdinInspector;
using System;

namespace Project.Common.Configs
{
    [CreateAssetMenu(fileName = "WindowsData", menuName = "Project/WindowsData")]
    public class WindowsData : SerializedScriptableObject
    {
        [field: SerializeField] public float AnimationDuration { get; private set; }
    }
}
