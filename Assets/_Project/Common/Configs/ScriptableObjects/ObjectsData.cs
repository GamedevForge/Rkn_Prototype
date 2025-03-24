using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Project.Common.Configs
{
    [CreateAssetMenu(fileName = "ObjectsData", menuName = "Project/ObjectsData")]
    public class ObjectsData : SerializedScriptableObject
    {
        [field: SerializeField] public Dictionary<Core.ObjectType, ObjectConfig> Configs { get; private set; }    
    }
}
