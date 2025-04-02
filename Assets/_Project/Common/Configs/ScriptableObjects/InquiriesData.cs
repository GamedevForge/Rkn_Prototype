using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Project.Common.Configs
{
    [CreateAssetMenu(fileName = "InquiriesData", menuName = "Project/InquiriesData")]
    public class InquiriesData : SerializedScriptableObject
    {
        [field: SerializeField] public Dictionary<string, string> Inquiries { get; private set; }
    }
}
