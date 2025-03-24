using System.Collections.Generic;
using UnityEngine;

namespace Project.Common.Configs
{
    [CreateAssetMenu(fileName = "ObjectsData", menuName = "Project/ObjectsData")]
    public class ObjectsData : ScriptableObject
    {
        [field: SerializeField] public Dictionary<Core.ObjectType, ObjectConfig> Configs { get; private set; }    
    }

    public class ObjectsDataService
    {
        private readonly ObjectsData _objectsData;

        public ObjectsDataService(ObjectsData objectsData) =>
            _objectsData = objectsData;

        public ObjectConfig GetObjectConfig(Core.ObjectType objectType) =>
            _objectsData.Configs[objectType];
    }
}
