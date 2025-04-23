using UnityEngine;
using Zenject;
using Project.Common.Configs;
using Cysharp.Threading.Tasks;
using Project.Common.UI;
using System;
using System.Collections.Generic;

namespace Project.Common.Core
{
    public class BedController : MonoBehaviour, IInteractableObject, IQuestEvent<Transform>
    {
        public event Action OnEvent;
        
        private ObjectsDataService _dataService;
        private DayHandler _dayHandler;
        private QuestConfigService _questConfigService;
        private bool _holdIsProcessing = false;

        [field: SerializeField] public float HoldTime { get; private set; }

        public bool CanInteract => (_questConfigService.GetQuestConfig() == null ||
            _questConfigService.GetQuestConfig().ID == "sleep") &&
            _holdIsProcessing == false;
        public InteractType InteractType => InteractType.Hold;
        public string Name => _dataService.GetObjectConfig(ObjectType.Bed).Name;
        [field: SerializeField] public Transform MarkerTarget { get; private set; }    

        [Inject] private void Construct(
            ObjectsDataService objectsDataService, 
            DayHandler dayHandler,
            QuestConfigService questConfigService)
        {
            _dataService = objectsDataService;  
            _dayHandler = dayHandler;
            _questConfigService = questConfigService;
        }

        public async void Interact()
        {
            OnEvent?.Invoke();
            _dayHandler.SetDayCount(_dayHandler.DayNumber + 1);
            await PlayAnimationAsync();
        }

        private async UniTask PlayAnimationAsync()
        {
            _holdIsProcessing = true;
            await UniTask.WaitForSeconds(1);
            _holdIsProcessing = false;
        }
    }
}