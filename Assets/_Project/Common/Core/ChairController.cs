using UnityEngine;
using Zenject;
using Project.Common.Configs;

namespace Project.Common.Core
{
    public class ChairController : MonoBehaviour, IIntertableObject
    {
        public string Name => _dataService.GetObjectConfig(ObjectType.Chair).Name;

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


