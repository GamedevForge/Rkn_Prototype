using Project.Common.Configs;
using UnityEngine;

namespace Project.Common.Core
{
    public class PlayerPositionController
    {
        private readonly Transform _playerTransform;
        private readonly SceneSpawnPointData _spawnPointData;

        public Vector3 CurrentPosition => _playerTransform.position;
        
        public PlayerPositionController(
            PlayerComponents playerComponents,
            SceneSpawnPointData sceneSpawnPointData) 
        { 
            _playerTransform = playerComponents.PlayerTransform;
            _spawnPointData = sceneSpawnPointData;
        }

        public void SetOriginPositionOnPlayground() =>
            _playerTransform.position = _spawnPointData.SpawnPoint[Scene.Playground];

        public void SetOriginPositionOnNeighborhood() =>
            _playerTransform.position = _spawnPointData.SpawnPoint[Scene.Neighborhood];

        public void SetPosition(Vector3 position) =>
            _playerTransform.position = position;
    }
}