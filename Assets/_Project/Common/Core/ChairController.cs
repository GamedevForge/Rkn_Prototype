using UnityEngine;
using Zenject;
using Project.Common.Configs;
using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;

namespace Project.Common.Core
{
    public class ChairController : MonoBehaviour, IInteractableObject
    {
        public string Name => _dataService.GetObjectConfig(ObjectType.Chair).Name;
        public bool CanInteract => _playerState.IsSitting;

        private ObjectsDataService _dataService;
        private PlayerState _playerState;

        [Inject] private void Construct(ObjectsDataService objectsDataService,
            PlayerState playerState)
        {
            _dataService = objectsDataService;
            _playerState = playerState;
        }

        public void Interact()
        {
            _playerState.Sit();
            Debug.Log("Interact!");
        }
    }
}


