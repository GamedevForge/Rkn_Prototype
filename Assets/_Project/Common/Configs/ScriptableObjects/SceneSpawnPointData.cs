using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace Project.Common.Configs
{
    [CreateAssetMenu(fileName = "SceneSpawnPointData", menuName = "Project/SceneSpawnPointData")]
    public class SceneSpawnPointData : SerializedScriptableObject
    {
        [field: SerializeField] public Dictionary<Scene, Vector3> SpawnPoint { get; private set; }
    }
}
