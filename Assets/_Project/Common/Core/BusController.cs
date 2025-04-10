using UnityEngine;
using Zenject;
using Project.Common.Configs;
using Cysharp.Threading.Tasks;

namespace Project.Common.Core
{
    public class BusController : MonoBehaviour, IInteractableObject
    {
        private ObjectsDataService _dataService;
        private ZenjectSceneLoader _sceneLoader;

        [field: SerializeField] public float HoldTime { get; private set; }
        
        public InteractType InteractType => InteractType.Click;
        public bool CanInteract { get; private set; } = true;
        public string Name => _dataService.GetObjectConfig(ObjectType.Bus).Name;

        [Inject] private void Construct(ObjectsDataService dataService, ZenjectSceneLoader sceneLoader)
        {
            _dataService = dataService;
            _sceneLoader = sceneLoader;
        }
 
        public void Interact()
        {
            _sceneLoader.LoadScene("Playground");
        }

        private async UniTask PlayAnimationAsync()
        {
            //
        }
    }
}