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
        private CharacterController _characterController;
        private QuestConfigService _questConfigService;

        protected SceneSpawnPointData PlayerSpawnPointData {  get; private set; }

        protected virtual Vector3 SpawnPointOnSceneChange => PlayerSpawnPointData.SpawnPoint[Scene.Playground];

        [field: SerializeField] public Transform MarkerTarget { get; private set; }
        [field: SerializeField] public float HoldTime { get; private set; }
        
        public virtual InteractType InteractType => InteractType.Click;
        public virtual bool CanInteract => 
            _questConfigService.GetQuestConfig() == null || 
            _questConfigService.GetQuestConfig().ID != "sleep";
        public virtual string Name => _dataService.GetObjectConfig(ObjectType.Bus).Name;

        [Inject] private void Construct(
            ObjectsDataService dataService, 
            ZenjectSceneLoader sceneLoader,
            PlayerPositionController playerPositionController,
            SceneSpawnPointData sceneSpawnPointData,
            CharacterController characterController,
            QuestConfigService questConfigService)
        {
            _dataService = dataService;
            _sceneLoader = sceneLoader;
            PlayerSpawnPointData = sceneSpawnPointData;
            _playerPositionController = playerPositionController;
            _characterController = characterController;
            _questConfigService = questConfigService;
        }

        public virtual async void Interact()
        {
            OnEvent?.Invoke();
            await LoadScene();
            SetPositionForNextScene();
        }

        protected virtual async UniTask PlayAnimationAsync()
        {
            await UniTask.Yield();
        }

        protected ObjectConfig GetObjectConfig(ObjectType objectType) =>
            _dataService.GetObjectConfig(objectType);

        protected void SetPositionForNextScene()
        {
            _characterController.enabled = false;
            _playerPositionController.SetPosition(SpawnPointOnSceneChange);
            _characterController.enabled = true;
        }

        private async UniTask LoadScene() =>
            await _sceneLoader.LoadSceneAsync(SceneName);
    }
}