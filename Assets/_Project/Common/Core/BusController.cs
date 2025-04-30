using UnityEngine;
using Zenject;
using Project.Common.Configs;
using Cysharp.Threading.Tasks;
using System;

namespace Project.Common.Core
{
    public class BusController : MonoBehaviour, IInteractableObject, IQuestEvent<Transform>
    {
        public event Action OnEvent;
        
        [SerializeField] private string SceneName = "Playground";

        private ObjectsDataService _dataService;
        private ZenjectSceneLoader _sceneLoader;
        private PlayerPositionController _playerPositionController;

        protected SceneSpawnPointData PlayerSpawnPointData {  get; private set; }

        protected virtual Vector3 SpawnPointOnSceneChange => PlayerSpawnPointData.SpawnPoint[Scene.Playground];

        [field: SerializeField] public Transform MarkerTarget { get; private set; }
        [field: SerializeField] public float HoldTime { get; private set; }
        
        public virtual InteractType InteractType => InteractType.Click;
        public virtual bool CanInteract { get; private set; } = true;
        public virtual string Name => _dataService.GetObjectConfig(ObjectType.Bus).Name;

        [Inject] private void Construct(
            ObjectsDataService dataService, 
            ZenjectSceneLoader sceneLoader,
            PlayerPositionController playerPositionController,
            SceneSpawnPointData sceneSpawnPointData)
        {
            _dataService = dataService;
            _sceneLoader = sceneLoader;
            PlayerSpawnPointData = sceneSpawnPointData;
            _playerPositionController = playerPositionController;
        }

        public virtual void Interact()
        {
            OnEvent?.Invoke();
            SetPositionForNextScene();
            LoadScene();
        }

        protected virtual async UniTask PlayAnimationAsync()
        {
            await UniTask.Yield();
        }

        protected ObjectConfig GetObjectConfig(ObjectType objectType) =>
            _dataService.GetObjectConfig(objectType);

        protected void SetPositionForNextScene() =>
            _playerPositionController.SetPosition(SpawnPointOnSceneChange);

        private void LoadScene() =>
            _sceneLoader.LoadScene(SceneName);
    }
}