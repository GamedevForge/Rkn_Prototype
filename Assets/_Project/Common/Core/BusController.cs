using UnityEngine;
using Zenject;
using Project.Common.Configs;

namespace Project.Common.Core
{
    public class BusController : MonoBehaviour, IInteractableObject
    {
        private ObjectsDataService _dataService;
        private ZenjectSceneLoader _sceneLoader;

        public bool CanInteract => true;
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
    }
}