using UnityEngine;
using Zenject;
using Project.Common.Configs;
using Cysharp.Threading.Tasks;
using Project.Common.UI;

namespace Project.Common.Core
{
    public class BedController : MonoBehaviour, IInteractableObject
    {
        private ObjectsDataService _dataService;
        private DayHandler _dayHandler;

        [field: SerializeField] public float HoldTime { get; private set; }

        public bool CanInteract { get; private set; } = true;
        public InteractType InteractType => InteractType.Hold;
        public string Name => _dataService.GetObjectConfig(ObjectType.Bed).Name;

        [Inject] private void Construct(ObjectsDataService objectsDataService, DayHandler dayHandler)
        {
            _dataService = objectsDataService;  
            _dayHandler = dayHandler;
        }

        public async void Interact()
        {
            _dayHandler.SetDayCount(_dayHandler.DayNumber + 1);
            await PlayAnimationAsync();
        }

        private async UniTask PlayAnimationAsync()
        {
            CanInteract = false;
            await UniTask.WaitForSeconds(1);
            CanInteract = true;
        }
    }
}