using UnityEngine;
using Zenject;
using Project.Common.Configs;

namespace Project.Common.Core
{
    public class MonitorController : MonoBehaviour, IInteractableObject
    {
        private ObjectsDataService _dataService;
        private PlayerState _playerState;

        public bool CanInteract => _playerState.IsSitting && 
            _playerState.IsProcessing == false;
        public string Name => _dataService.GetObjectConfig(ObjectType.Monitor).Name;

        [Inject] private void Construct(ObjectsDataService dataService,
            PlayerState playerState)
        {
            _dataService = dataService;
            _playerState = playerState;
        }

        public void Interact()
        {
            Debug.Log("Interact");
        }
    }
}