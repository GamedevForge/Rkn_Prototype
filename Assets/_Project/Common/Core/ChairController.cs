using UnityEngine;
using Zenject;
using Project.Common.Configs;

namespace Project.Common.Core
{
    public class ChairController : MonoBehaviour, IInteractableObject
    {
        [SerializeField] private Transform _endPointTransform;

        private ObjectsDataService _dataService;
        private PlayerState _playerState;
        private PlayerComponents _playerComponents;

        public string Name => _dataService.GetObjectConfig(ObjectType.Chair).Name;
        public bool CanInteract => _playerState.IsSitting;

        [Inject] private void Construct(ObjectsDataService objectsDataService,
            PlayerState playerState,
            PlayerComponents playerComponents)
        {
            _dataService = objectsDataService;
            _playerState = playerState;
            _playerComponents = playerComponents;
        }

        public void Interact()
        {
            _playerState.Sit();
            _playerComponents.Agent.enabled = true;
            _playerComponents.Agent.destination = _endPointTransform.position;
            //Debug.Log("Interact!");
        }
    }
}


