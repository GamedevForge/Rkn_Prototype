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

        [field: SerializeField] public Transform MarkerTarget { get; private set; }
        [field: SerializeField] public float HoldTime { get; private set; }
        
        public virtual InteractType InteractType => InteractType.Click;
        public virtual bool CanInteract { get; private set; } = true;
        public virtual string Name => _dataService.GetObjectConfig(ObjectType.Bus).Name;

        [Inject] private void Construct(ObjectsDataService dataService, ZenjectSceneLoader sceneLoader)
        {
            _dataService = dataService;
            _sceneLoader = sceneLoader;
        }

        public virtual void Interact()
        {
            OnEvent?.Invoke();
            LoadScene();
        }

        protected virtual async UniTask PlayAnimationAsync()
        {
            await UniTask.Yield();
        }

        protected ObjectConfig GetObjectConfig(ObjectType objectType) =>
            _dataService.GetObjectConfig(objectType);

        private void LoadScene() =>
            _sceneLoader.LoadScene(SceneName);
    }
}